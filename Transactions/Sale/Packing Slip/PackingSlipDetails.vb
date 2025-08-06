
Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class PackingSlipDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub CMDEXIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub PACKINGSLIPDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.Alt = True And e.KeyCode = Keys.R Then
            Call TOOLREFRESH_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.P Then
            Call TOOLEXCEL_Click(sender, e)
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub PACKINGSLIPDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'PACKING SLIP'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub showform(ByVal EDITVAL As Boolean, ByVal PACKINGSLIPNO As Integer)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJPACKINGSLIP As New PackingSlip
            OBJPACKINGSLIP.EDIT = EDITVAL
            OBJPACKINGSLIP.MdiParent = MDIMain
            OBJPACKINGSLIP.TEMPPSNO = PACKINGSLIPNO
            OBJPACKINGSLIP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim OBJCMN As New ClsCommon
            Dim dttable As DataTable = OBJCMN.search("PACKINGSLIP.PS_NO AS PSNO, PACKINGSLIP.PS_DATE AS DATE, PACKINGSLIP.PS_CHALLANNO AS CHALLANNO, PACKINGSLIP.PS_CHALLANDATE AS CHDATE, PACKINGSLIP.PS_LRNO AS LRNO,ISNULL(TRANSLEDGERS.Acc_cmpname,'') AS TRANSNAME, GODOWNMASTER.GODOWN_NAME AS GODOWN, FROMCITYMASTER.city_name AS FROMCITY, TOCITYMASTER.city_name AS TOCITY, LEDGERS.Acc_cmpname AS NAME, FROMLEDGERS.Acc_cmpname AS FROMNAME, PACKERLEDGERS.Acc_cmpname AS PACKERNAME, GREYQUALITYMASTER.GREY_NAME AS QUALITY, PACKINGSLIP.PS_TOTALTAKA AS TOTALTAKA, PACKINGSLIP.PS_TOTALMTRS AS TOTALMTRS, PACKINGSLIP.PS_AVGWT AS AVGWT, PACKINGSLIP.PS_TOTALTP AS TOTALTP, PACKINGSLIP.PS_REMARKS AS REMARKS, PACKINGSLIP.PS_DONE AS DONE", "", " LEDGERS AS TRANSLEDGERS RIGHT OUTER JOIN PACKINGSLIP INNER JOIN GODOWNMASTER ON PACKINGSLIP.PS_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN CITYMASTER AS FROMCITYMASTER ON PACKINGSLIP.PS_FROMCITYID = FROMCITYMASTER.city_id INNER JOIN CITYMASTER AS TOCITYMASTER ON PACKINGSLIP.PS_TOCITYID = TOCITYMASTER.city_id INNER JOIN LEDGERS ON PACKINGSLIP.PS_LEDGERID = LEDGERS.Acc_id INNER JOIN LEDGERS AS FROMLEDGERS ON PACKINGSLIP.PS_FROMLEDGERID = FROMLEDGERS.Acc_id INNER JOIN LEDGERS AS PACKERLEDGERS ON PACKINGSLIP.PS_PACKERLEDGERID = PACKERLEDGERS.Acc_id INNER JOIN GREYQUALITYMASTER ON PACKINGSLIP.PS_QUALITYID = GREYQUALITYMASTER.GREY_ID ON TRANSLEDGERS.Acc_id = PACKINGSLIP.PS_TRANSID ", " AND PACKINGSLIP.PS_YEARID = " & YearId & " ORDER BY PS_NO")
            gridbilldetails.DataSource = dttable
            If dttable.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEDIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(True, gridbill.GetFocusedRowCellValue("PSNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(True, gridbill.GetFocusedRowCellValue("PSNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDADD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDADD.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0 Then Exit Sub
            If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                MsgBox("Enter Propoer Bale Nos", MsgBoxStyle.Critical)
                Exit Sub
            End If
            Dim tempMsg As Integer
            tempMsg = MsgBox("Wish to Print Bales from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo)
            If tempMsg = vbYes Then
                SERVERPROPDIRECT()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPDIRECT(Optional ByVal INVOICEMAIL As Boolean = False)
        Try
            Dim ALATTACHMENT As New ArrayList
            If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings
            For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                Dim OBJPC As New ChallanDesign
                OBJPC.MdiParent = MDIMain
                OBJPC.FRMSTRING = "PACKINGSLIP"
                OBJPC.DIRECTPRINT = True
                OBJPC.PRINTSETTING = PRINTDIALOG
                OBJPC.PSNO = Val(I)
                OBJPC.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJPC.Show()
                OBJPC.Close()
            Next

            'If INVOICEMAIL Then
            '    Dim OBJMAIL As New SendMail
            '    OBJMAIL.ALATTACHMENT = ALATTACHMENT
            '    OBJMAIL.subject = "Invoice"
            '    OBJMAIL.ShowDialog()
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TOOLREFRESH.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("DONE")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Bale Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim workbook As String = PATH
            If FileIO.FileSystem.FileExists(PATH) = True Then Interaction.GetObject(workbook).close(False)
            GC.Collect()

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Bale Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Bale Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class