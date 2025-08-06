
Imports BL
Imports System.Windows.Forms

Public Class ReceiptDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim PAYREGID As Integer

    Private Sub ReceiptDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.N) Or (e.Alt = True And e.KeyCode = Windows.Forms.Keys.A) Then       'for AddNew 
                showform(False, 0)
            ElseIf e.KeyCode = Keys.E And e.Alt = True Then
                CMDOK_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal tepmcondition)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable
            dt = objclsCMST.search(" dbo.RECEIPTMASTER.RECEIPT_no AS [Sr. No], dbo.Ledgers.acc_cmpname AS [Name] ,dbo.RECEIPTMASTER.RECEIPT_date AS [Date], dbo.RECEIPTMASTER.RECEIPT_total AS [Total], dbo.RECEIPTMASTER.RECEIPT_chqno AS [Chq. No.], dbo.RECEIPTMASTER.RECEIPT_registerid AS [Registerid],dbo.RECEIPTMASTER.RECEIPT_remarks AS [Remarks], BANKLEDGERS.Acc_cmpname as [BankName], GROUPMASTER.group_secondary AS GROUPNAME, LEDGERS.Acc_panno AS PANNO, LEDGERS.Acc_add AS ADDRESS", "", "   RECEIPTMASTER INNER JOIN LEDGERS ON RECEIPTMASTER.receipt_ledgerid = LEDGERS.Acc_id INNER JOIN LEDGERS AS BANKLEDGERS ON RECEIPTMASTER.receipt_accid = BANKLEDGERS.Acc_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id ", tepmcondition)
            griddetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridpayment.FocusedRowHandle = gridpayment.RowCount - 1
                gridpayment.TopRowIndex = gridpayment.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal billno As Integer)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If (editval = False) Or (editval = True And gridpayment.RowCount > 0) Then
                Dim OBJREC As New Receipt
                OBJREC.MdiParent = MDIMain
                OBJREC.edit = editval
                OBJREC.TEMPREGNAME = cmbregister.Text.Trim
                OBJREC.TEMPRECEIPTNO = billno
                OBJREC.Show()
                'Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
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

    Private Sub gridRECEIPT_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridpayment.DoubleClick
        Try
            showform(True, gridpayment.GetFocusedRowCellValue("Sr. No"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.GotFocus
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'RECEIPT'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'RECEIPT' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If cmbregister.Text.Trim.Length > 0 Then
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'RECEIPT' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
                If dt.Rows.Count > 0 Then
                    PAYREGID = dt.Rows(0).Item(0)
                    'cmbregister.Enabled = False
                    fillgrid(" and dbo.RECEIPTMASTER.RECEIPT_YEARid=" & YearId & " AND RECEIPTMASTER.RECEIPT_registerid = " & PAYREGID & " order by dbo.RECEIPTMASTER.RECEIPT_no ")
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

    Private Sub CMDOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridpayment.GetFocusedRowCellValue("Sr. No"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ReceiptDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'RECEIPT'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Receipt Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next

            Dim PERIOD As String = ""
            PERIOD = AccFrom & " - " & AccTo

            opti.SheetName = "Receipt Details"
            gridpayment.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Receipt Details", gridpayment.VisibleColumns.Count + gridpayment.GroupCount, "", PERIOD)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ReceiptDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ClientName = "SVS" Then Me.Close()
    End Sub
End Class