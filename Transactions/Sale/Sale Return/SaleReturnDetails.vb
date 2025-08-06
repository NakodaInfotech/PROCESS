
Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class SaleReturnDetails
    Dim SALEREGID As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public fromno, tono As Integer
    Public PARTYNAME As String

    Private Sub SALERETURNDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Alt = True Then
                showform(False, 0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" SALERETURN.SALRET_NO AS SRNO, SALERETURN.SALRET_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(SALRET_CHALLANNO,'') AS CHALLANNO, ISNULL(SALRET_EWAYBILLNO,'') AS EWAYBILLNO,  ISNULL(SALERETURN.SALRET_TOTALPCS, 0) AS TOTALPCS, ISNULL(SALERETURN.SALRET_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(SALERETURN.SALRET_AMOUNT, 0) AS TOTALAMT, ISNULL(SALERETURN.SALRET_REMARKS, '') AS REMARKS, ISNULL(SALERETURN.SALRET_BILLCHECKED,0) AS CHECKED, ISNULL(SALERETURN.SALRET_BILLDISPUTE,0) AS DISPUTED, ISNULL(SALERETURN.SALRET_INVOICENO, '') AS INVOICENO, SALERETURN.SALRET_CGSTPER AS CGSTPER, SALERETURN.SALRET_TOTALCGSTAMT AS CGSTAMT, SALERETURN.SALRET_SGSTPER AS SGSTPER, SALERETURN.SALRET_TOTALSGSTAMT AS SGSTAMT, SALERETURN.SALRET_IGSTPER AS IGSTPER, SALERETURN.SALRET_TOTALIGSTAMT AS IGSTAMT, SALERETURN.SALRET_SUBTOTAL AS SUBTOTAL, SALERETURN.SALRET_GRANDTOTAL AS GRANDTOTAL, ISNULL(SALERETURN.SALRET_INVOICENO, '') AS INVNO, ISNULL(SALERETURN.SALRET_INVOICETYPE, '') AS INVTYPE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN , ISNULL(LEDGERS_1.Acc_cmpname, '') AS DYEING,  ISNULL(SALERETURN.SALRET_DONO, '') AS DONO, ISNULL(SALERETURN.SALRET_DODATE, '') AS DODATE, ISNULL(SALERETURN.SALRET_SHORTAGE, 0) AS SHORTAGE, ISNULL(SALERETURN.SALRET_NETTTAKA, 0) AS NETTTAKA, ISNULL(SALERETURN.SALRET_NETTMTRS, 0) AS NETTMTRS, ISNULL(SALERETURN.SALRET_INVTAKA, 0) AS INVTAKA, ISNULL(SALERETURN.SALRET_INVMTRS, 0) AS INVMTRS, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME", " ", " SALERETURN INNER JOIN LEDGERS ON SALERETURN.SALRET_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON SALERETURN.SALRET_AGENTID = AGENTLEDGERS.Acc_id INNER JOIN REGISTERMASTER ON SALERETURN.SALRET_REGISTERID = REGISTERMASTER.register_id INNER JOIN GODOWNMASTER ON SALERETURN.SALRET_GODOWNID = GODOWNMASTER.GODOWN_ID LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON SALERETURN.SALRET_DYEINGID = LEDGERS_1.Acc_id ", " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND (SALERETURN.SALRET_YEARID = '" & YearId & "') ORDER BY SALERETURN.SALRET_NO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal billno As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJBILL As New SaleReturn
                OBJBILL.MdiParent = MDIMain
                OBJBILL.edit = editval
                OBJBILL.TEMPSALERETNO = billno
                OBJBILL.TEMPREGNAME = cmbregister.Text.Trim
                OBJBILL.Show()
                '  Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'SALERETURN'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'SALERETURN' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            If cmbregister.Text.Trim.Length > 0 Then
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'SALERETURN' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    SALEREGID = dt.Rows(0).Item(0)
                    fillgrid()
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDDTLS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDTLS.Click
        Try
            Dim OBJDTLS As New SaleReturnGridDetails
            OBJDTLS.MdiParent = MDIMain
            OBJDTLS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridINVOICE_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleReturnDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE RETURN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillregister(cmbregister, " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'SALERETURN' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("CHECKED")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightGreen
                ElseIf View.GetRowCellDisplayText(e.RowHandle, View.Columns("DISPUTED")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Sale Return Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next
            opti.SheetName = "Sale Return Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sale Return Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class