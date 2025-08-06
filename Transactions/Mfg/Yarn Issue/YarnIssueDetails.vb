
Imports BL

Public Class YarnIssueDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public FRMSTRING As String
    Dim DTMAIL As New DataTable
    Dim DTWHATSAPP As New DataTable

    Private Sub CMDEXIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub YarnISSUETOWARPERDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                CMDEDIT_Click(sender, e)
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                TOOLEXCEL_Click(sender, e)
            ElseIf e.KeyCode = Keys.R And e.Alt = True Then
                TOOLREFRESH_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub YarnISSUETOWARPERDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
            GADDANO.Visible = False

            If FRMSTRING = "ISSUETOWARPER" Then
                Me.Text = "Yarn Issue To Warper Details"
            ElseIf FRMSTRING = "ISSUETOADDA" Then
                Me.Text = "Yarn Issue To Adda Details"
                GADDANO.Visible = True
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                Me.Text = "Yarn Issue To Sizer Details"
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                Me.Text = "Yarn Issue To Weaver Details"
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                Me.Text = "Yarn Issue To Dyeing Details"
            End If

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If FRMSTRING = "ISSUETOWARPER" Then
                fillgrid(" AND dbo.YARNISSUEWARPER.YISSUEWARPER_yearid=" & YearId & " ORDER by dbo.YARNISSUEWARPER.YISSUEWARPER_NO ")
            ElseIf FRMSTRING = "ISSUETOADDA" Then
                fillgrid(" AND dbo.YARNISSUEADDA.YISSUEADDA_yearid=" & YearId & " ORDER by dbo.YARNISSUEADDA.YISSUEADDA_NO ")
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                fillgrid(" AND dbo.YARNISSUESIZER.YISSUESIZER_yearid=" & YearId & " ORDER by dbo.YARNISSUESIZER.YISSUESIZER_NO ")
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                fillgrid(" AND dbo.YARNISSUEWEAVER.YISSUEWEAVER_yearid=" & YearId & " ORDER by dbo.YARNISSUEWEAVER.YISSUEWEAVER_NO ")
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                fillgrid(" AND dbo.YARNISSUEDYEING.YISSUEDYEING_yearid=" & YearId & " ORDER by dbo.YARNISSUEDYEING.YISSUEDYEING_NO ")
            End If

            DTMAIL.Columns.Add("SRNO")
            DTMAIL.Columns.Add("REGID")
            DTMAIL.Columns.Add("REGNAME")
            DTMAIL.Columns.Add("PRINTINITIALS")
            DTMAIL.Columns.Add("DATE")
            DTMAIL.Columns.Add("NAME")
            DTMAIL.Columns.Add("PARTYEMAILID")
            DTMAIL.Columns.Add("AGENTNAME")
            DTMAIL.Columns.Add("AGENTEMAILID")
            DTMAIL.Columns.Add("GRANDTOTAL")
            DTMAIL.Columns.Add("SUBJECT")
            DTMAIL.Columns.Add("ATTACHMENT")
            DTMAIL.Columns.Add("FILENAME")

            DTWHATSAPP.Columns.Add("SRNO")
            DTWHATSAPP.Columns.Add("REGID")
            DTWHATSAPP.Columns.Add("REGNAME")
            DTWHATSAPP.Columns.Add("PRINTINITIALS")
            DTWHATSAPP.Columns.Add("DATE")
            DTWHATSAPP.Columns.Add("NAME")
            DTWHATSAPP.Columns.Add("PARTYWHATSAPP")
            DTWHATSAPP.Columns.Add("AGENTNAME")
            DTWHATSAPP.Columns.Add("AGENTWHATSAPP")
            DTWHATSAPP.Columns.Add("GRANDTOTAL")
            DTWHATSAPP.Columns.Add("SUBJECT")
            DTWHATSAPP.Columns.Add("ATTACHMENT")
            DTWHATSAPP.Columns.Add("FILENAME")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal tepmcondition)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As New DataTable
            If FRMSTRING = "ISSUETOWARPER" Then
                dt = objclsCMST.search(" CAST(0 AS BIT) AS CHK, YARNISSUEWARPER.YISSUEWARPER_no AS SRNO, YARNISSUEWARPER.YISSUEWARPER_date AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSNAME, ISNULL(YARNISSUEWARPER.YISSUEWARPER_TOTALBAGS, 0) AS TOTALBAGS, ISNULL(YARNISSUEWARPER.YISSUEWARPER_TOTALWT, 0) AS TOTALWT, ISNULL(YARNISSUEWARPER.YISSUEWARPER_TOTALFRESH, 0) AS TOTALFRESH, ISNULL(YARNISSUEWARPER.YISSUEWARPER_TOTALWINDING, 0) AS TOTALWINDING, ISNULL(YARNISSUEWARPER.YISSUEWARPER_TOTALFIRKA, 0) AS TOTALFIRKA, ISNULL(YARNISSUEWARPER.YISSUEWARPER_remarks, '') AS REMARKS ,ISNULL(LEDGERS.ACC_MOBILE, '') AS PARTYWHATSAPP ", "", " YARNISSUEWARPER INNER JOIN LEDGERS ON YARNISSUEWARPER.YISSUEWARPER_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON YARNISSUEWARPER.YISSUEWARPER_transledgerid = TRANSPORT.Acc_id LEFT OUTER JOIN GODOWNMASTER ON YARNISSUEWARPER.YISSUEWARPER_GODOWNID = GODOWNMASTER.GODOWN_ID", tepmcondition)
            ElseIf FRMSTRING = "ISSUETOADDA" Then
                dt = objclsCMST.search("CAST(0 AS BIT) AS CHK, YARNISSUEADDA.YISSUEADDA_no AS SRNO, YARNISSUEADDA.YISSUEADDA_date AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSNAME, ISNULL(YARNISSUEADDA.YISSUEADDA_TOTALBAGS, 0) AS TOTALBAGS, ISNULL(YARNISSUEADDA.YISSUEADDA_TOTALWT, 0) AS TOTALWT, ISNULL(YARNISSUEADDA.YISSUEADDA_TOTALFRESH, 0) AS TOTALFRESH, ISNULL(YARNISSUEADDA.YISSUEADDA_TOTALWINDING, 0) AS TOTALWINDING, ISNULL(YARNISSUEADDA.YISSUEADDA_TOTALFIRKA, 0) AS TOTALFIRKA, ISNULL(YARNISSUEADDA.YISSUEADDA_remarks, '') AS REMARKS, ISNULL(YARNISSUEADDA.YISSUEADDA_ADDANO, 0) AS ADDANO, ISNULL(LEDGERS.ACC_MOBILE, '') AS PARTYWHATSAPP ", "", " YARNISSUEADDA INNER JOIN LEDGERS ON YARNISSUEADDA.YISSUEADDA_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON YARNISSUEADDA.YISSUEADDA_transledgerid = TRANSPORT.Acc_id LEFT OUTER JOIN GODOWNMASTER ON YARNISSUEADDA.YISSUEADDA_GODOWNID = GODOWNMASTER.GODOWN_ID", tepmcondition)
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                dt = objclsCMST.search("CAST(0 AS BIT) AS CHK, YARNISSUESIZER.YISSUESIZER_no AS SRNO, YARNISSUESIZER.YISSUESIZER_date AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSNAME, ISNULL(YARNISSUESIZER.YISSUESIZER_TOTALBAGS, 0) AS TOTALBAGS, ISNULL(YARNISSUESIZER.YISSUESIZER_TOTALWT, 0) AS TOTALWT, ISNULL(YARNISSUESIZER.YISSUESIZER_TOTALFRESH, 0) AS TOTALFRESH, ISNULL(YARNISSUESIZER.YISSUESIZER_TOTALWINDING, 0) AS TOTALWINDING, ISNULL(YARNISSUESIZER.YISSUESIZER_TOTALFIRKA, 0) AS TOTALFIRKA, ISNULL(YARNISSUESIZER.YISSUESIZER_remarks, '') AS REMARKS, ISNULL(LEDGERS.ACC_MOBILE, '') AS PARTYWHATSAPP ", "", " YARNISSUESIZER INNER JOIN LEDGERS ON YARNISSUESIZER.YISSUESIZER_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON YARNISSUESIZER.YISSUESIZER_transledgerid = TRANSPORT.Acc_id LEFT OUTER JOIN GODOWNMASTER ON YARNISSUESIZER.YISSUESIZER_GODOWNID = GODOWNMASTER.GODOWN_ID", tepmcondition)
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                dt = objclsCMST.search("  CAST(0 AS BIT) AS CHK, YARNISSUEWEAVER.YISSUEWEAVER_no AS SRNO, YARNISSUEWEAVER.YISSUEWEAVER_date AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSNAME, ISNULL(YARNISSUEWEAVER.YISSUEWEAVER_TOTALBAGS, 0) AS TOTALBAGS, ISNULL(YARNISSUEWEAVER.YISSUEWEAVER_TOTALWT, 0) AS TOTALWT,ISNULL(YARNISSUEWEAVER.YISSUEWEAVER_TOTALFRESH, 0) AS TOTALFRESH, ISNULL(YARNISSUEWEAVER.YISSUEWEAVER_TOTALWINDING, 0) AS TOTALWINDING, ISNULL(YARNISSUEWEAVER.YISSUEWEAVER_TOTALFIRKA, 0) AS TOTALFIRKA, ISNULL(YARNISSUEWEAVER.YISSUEWEAVER_remarks, '') AS REMARKS, ISNULL(LEDGERS.ACC_MOBILE, '') AS PARTYWHATSAPP ", "", "   YARNISSUEWEAVER INNER JOIN LEDGERS ON YARNISSUEWEAVER.YISSUEWEAVER_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GODOWNMASTER ON YARNISSUEWEAVER.YISSUEWEAVER_GODOWNID = GODOWNMASTER.GODOWN_ID LEFT OUTER JOIN LEDGERS AS TRANSPORT ON YARNISSUEWEAVER.YISSUEWEAVER_transledgerid = TRANSPORT.Acc_id", tepmcondition)
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                dt = objclsCMST.search(" CAST(0 AS BIT) AS CHK, YARNISSUEDYEING.YISSUEDYEING_no AS SRNO, YARNISSUEDYEING.YISSUEDYEING_date AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSNAME, ISNULL(YARNISSUEDYEING.YISSUEDYEING_TOTALBAGS, 0) AS TOTALBAGS, ISNULL(YARNISSUEDYEING.YISSUEDYEING_TOTALWT, 0) AS TOTALWT, ISNULL(YARNISSUEDYEING.YISSUEDYEING_TOTALFRESH, 0) AS TOTALFRESH, ISNULL(YARNISSUEDYEING.YISSUEDYEING_TOTALWINDING, 0) AS TOTALWINDING, ISNULL(YARNISSUEDYEING.YISSUEDYEING_TOTALFIRKA, 0) AS TOTALFIRKA, ISNULL(YARNISSUEDYEING.YISSUEDYEING_remarks, '') AS REMARKS, ISNULL(LEDGERS.ACC_MOBILE, '') AS PARTYWHATSAPP ", "", "  YARNISSUEDYEING INNER JOIN LEDGERS ON YARNISSUEDYEING.YISSUEDYEING_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GODOWNMASTER ON YARNISSUEDYEING.YISSUEDYEING_GODOWNID = GODOWNMASTER.GODOWN_ID LEFT OUTER JOIN LEDGERS AS TRANSPORT ON YARNISSUEDYEING.YISSUEDYEING_transledgerid = TRANSPORT.Acc_id", tepmcondition)
            End If
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal ISSUENO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objDO As New YarnIssue
                objDO.MdiParent = MDIMain
                objDO.EDIT = editval
                objDO.TEMPISSUENO = ISSUENO
                objDO.FRMSTRING = FRMSTRING
                objDO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDADD.Click
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

    Private Sub gridbilldetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
        Try

            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try
            If FRMSTRING = "ISSUETOWARPER" Then
                Dim PATH As String = Application.StartupPath & "\Yarn Issue To Warper Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Issue To Warper Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Issue To Warper Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "ISSUETOADDA" Then
                Dim PATH As String = Application.StartupPath & "\Yarn Issue To ADDA Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Issue To ADDA Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Issue To ADDA Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                Dim PATH As String = Application.StartupPath & "\Yarn Issue To Sizer Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Issue To Sizer Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Issue To Sizer Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "ISSUETOWEAVER" Then

                Dim PATH As String = Application.StartupPath & "\Yarn Issue To Weaver Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Issue To Weaver Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Issue To Weaver Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "ISSUETODYEING" Then

                Dim PATH As String = Application.StartupPath & "\Yarn Issue To Dyeing Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Issue To Dyeing Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Issue To Dyeing Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLREFRESH.Click
        Try
            If FRMSTRING = "ISSUETOWARPER" Then
                fillgrid(" AND dbo.YARNISSUEWARPER.YISSUEWARPER_yearid=" & YearId & " ORDER by dbo.YARNISSUEWARPER.YISSUEWARPER_NO ")
            ElseIf FRMSTRING = "ISSUETOADDA" Then
                fillgrid(" AND dbo.YARNISSUEADDA.YISSUEADDA_yearid=" & YearId & " ORDER by dbo.YARNISSUEADDA.YISSUEADDA_NO ")
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                fillgrid(" AND dbo.YARNISSUESIZER.YISSUESIZER_yearid=" & YearId & " ORDER by dbo.YARNISSUESIZER.YISSUESIZER_NO ")
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                fillgrid(" AND dbo.YARNISSUEWEAVER.YISSUEWEAVER_yearid=" & YearId & " ORDER by dbo.YARNISSUEWEAVER.YISSUEWEAVER_NO ")
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                fillgrid(" AND dbo.YARNISSUEDYEING.YISSUEDYEING_yearid=" & YearId & " ORDER by dbo.YARNISSUEDYEING.YISSUEDYEING_NO ")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            PRINTREPORT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDDTLS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDTLS.Click
        Try
            Dim OBJYARNISS As New YarnIssueGridDetails
            OBJYARNISS.MdiParent = MDIMain
            OBJYARNISS.FRMSTRING = FRMSTRING
            OBJYARNISS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try

            If Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Then Exit Sub
            If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                MsgBox("Enter Proper Yarn Issue Nos", MsgBoxStyle.Critical)
                Exit Sub
            End If
            If MsgBox("Wish to Yarn Issue from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings

            If FRMSTRING = "ISSUETOWARPER" Then
                serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), "", "YARNISSUEWARPER", Val(TXTCOPIES.Text.Trim), PRINTDIALOG)
            ElseIf FRMSTRING = "ISSUETOADDA" Then
                serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), "", "YARNISSUEADDA", Val(TXTCOPIES.Text.Trim), PRINTDIALOG)
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), "", "YARNISSUESIZER", Val(TXTCOPIES.Text.Trim), PRINTDIALOG)
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), "", "YARNISSUEWEAVER", Val(TXTCOPIES.Text.Trim), PRINTDIALOG)
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), "", "YARNISSUEDYEING", Val(TXTCOPIES.Text.Trim), PRINTDIALOG)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTFROM.Validated
        If TXTFROM.Text.Trim <> "" Then TXTTO.Focus()
    End Sub

    Private Sub TXTCOPIES_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCOPIES.Validating
        If Val(TXTCOPIES.Text.Trim) <= 0 Then TXTCOPIES.Text = 1
    End Sub
    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Invoice Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Invoice from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(False, True)
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected Invoice ?", MsgBoxStyle.YesNo) = vbYes Then
                    CMDOK.Focus()
                    SERVERPROPSELECTED(False, True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub SERVERPROPDIRECT(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try
            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                Dim OBJREC As New YarnIssueDesign
                OBJREC.MdiParent = MDIMain
                OBJREC.DIRECTPRINT = True

                'OBJREC.FRMSTRING = "RECEIPT"
                If FRMSTRING = "ISSUETOWARPER" Then
                    OBJREC.FRMSTRING = "YARNISSUEWARPER"
                ElseIf FRMSTRING = "ISSUETOADDA" Then
                    OBJREC.FRMSTRING = "YARNISSUEADDA"
                ElseIf FRMSTRING = "ISSUETOSIZER" Then
                    OBJREC.FRMSTRING = "YARNISSUESIZER"
                ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                    OBJREC.FRMSTRING = "YARNISSUEWEAVER"
                ElseIf FRMSTRING = "ISSUETODYEING" Then
                    OBJREC.FRMSTRING = "YARNISSUEDYEING"
                End If

                OBJREC.DIRECTMAIL = INVOICEMAIL
                OBJREC.DIRECTWHATSAPP = WHATSAPP
                'OBJREC.REGNAME = cmbregister.Text.Trim
                OBJREC.PRINTSETTING = PRINTDIALOG
                OBJREC.ISSUENO = Val(I)
                OBJREC.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJREC.Show()
                OBJREC.Close()
                ALATTACHMENT.Add(Application.StartupPath & "_" & OBJREC.FRMSTRING & "_" & I & ".pdf")
                FILENAME.Add(OBJREC.FRMSTRING & "_" & I & ".pdf")
            Next

            'If INVOICEMAIL Then
            '    Dim OBJMAIL As New SendMail
            '    OBJMAIL.ALATTACHMENT = ALATTACHMENT
            '    OBJMAIL.subject = "RECEIPT"
            '    OBJMAIL.ShowDialog()
            'End If

            If WHATSAPP = True Then
                Dim OBJWHATSAPP As New SendWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.ShowDialog()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPSELECTED(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try

            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            DTMAIL.Rows.Clear()
            DTWHATSAPP.Rows.Clear()

            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            'Dim SELECTEDROWS As Int32() = gridpayment.GetSelectedRows()
            'For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
            For I As Integer = 0 To Val(gridbill.RowCount - 1)

                Dim ROW As DataRow = gridbill.GetDataRow(I)
                If ROW("CHK") = True Then
                    Dim OBJREC As New YarnIssueDesign
                    OBJREC.MdiParent = MDIMain
                    OBJREC.DIRECTPRINT = True

                    If FRMSTRING = "ISSUETOWARPER" Then
                        OBJREC.FRMSTRING = "YARNISSUEWARPER"
                    ElseIf FRMSTRING = "ISSUETOADDA" Then
                        OBJREC.FRMSTRING = "YARNISSUEADDA"
                    ElseIf FRMSTRING = "ISSUETOSIZER" Then
                        OBJREC.FRMSTRING = "YARNISSUESIZER"
                    ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                        OBJREC.FRMSTRING = "YARNISSUEWEAVER"
                    ElseIf FRMSTRING = "ISSUETODYEING" Then
                        OBJREC.FRMSTRING = "YARNISSUEDYEING"
                    End If

                    OBJREC.DIRECTMAIL = INVOICEMAIL
                    OBJREC.DIRECTWHATSAPP = WHATSAPP
                    'OBJREC.REGNAME = cmbregister.Text.Trim
                    OBJREC.PRINTSETTING = PRINTDIALOG
                    OBJREC.PARTYNAME = ROW("NAME")
                    OBJREC.ISSUENO = Val(ROW("SRNO"))
                    OBJREC.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJREC.Show()
                    OBJREC.Close()
                    ALATTACHMENT.Add(Application.StartupPath & "\" & ROW("NAME") & "_" & OBJREC.FRMSTRING & Val(ROW("SRNO")) & ".pdf")
                    FILENAME.Add(ROW("NAME") & "_" & OBJREC.FRMSTRING & Val(ROW("SRNO")) & ".pdf")



                    Dim OBJCMN As New ClsCommon

                    Dim DT As DataTable = OBJCMN.search(" register_name,ISNULL (REGISTERMASTER.register_id, 0) AS Registerid ", "", " RegisterMaster ", " AND REGISTER_NAME = '" & "" & "' and register_YEARid = " & YearId)
                    'ADDINT IN DTEMAIL
                    'DTMAIL.Rows.Add(ROW("SRNO"), DT.Rows(0).Item("Registerid"), "", ROW("INITIALS"), ROW("DATE"), ROW("NAME"), ROW("PARTYEMAILID"), ROW("AGENTNAME"), ROW("AGENTEMAILID"), "", UCase(CmpName) & " - REC No. " & ROW("INITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "RECEIPT_" & Val(ROW("SRNO")) & ".pdf", ROW("NAME") & "RECEIPT_" & Val(ROW("SRNO")) & ".pdf")

                    'ADDING IN WHATSAPP

                    DTWHATSAPP.Rows.Add(ROW("SRNO"), "", "", ROW("SRNO"), ROW("DATE"), ROW("NAME"), ROW("PARTYWHATSAPP"), "", "", "", UCase(CmpName) & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "_" & OBJREC.FRMSTRING & "_" & Val(ROW("SRNO")) & ".pdf", ROW("NAME") & "_" & OBJREC.FRMSTRING & "_" & Val(ROW("SRNO")) & ".pdf")


                End If

            Next


            'If INVOICEMAIL = True Then
            '    If DTMAIL.Rows.Count = 0 Then Exit Sub
            '    Dim OBJEMAIL As New SendMultipleMail
            '    OBJEMAIL.FORMTYPE = "INVOICE"
            '    OBJEMAIL.DT = DTMAIL
            '    OBJEMAIL.ShowDialog()
            '    Exit Sub
            'End If


            If WHATSAPP = True Then
                If DTWHATSAPP.Rows.Count = 0 Then Exit Sub
                Dim OBJWHATSAPP As New SendMultipleWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.DT = DTWHATSAPP
                OBJWHATSAPP.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class