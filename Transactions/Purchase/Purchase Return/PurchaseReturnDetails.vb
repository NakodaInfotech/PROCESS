
Imports BL

Public Class PurchaseReturnDetails


    Dim PURCHASEREGID As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public PURTYPE, SELECTEDREG As String

    Private Sub PURCHASERETURNDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Alt = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                CMDOK_Click(sender, e)
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call PrintToolStripButton_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal tepmcondition)
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" PURCHASERETURN.PURRET_NO AS SRNO,PURCHASERETURN.PURRET_DATE AS DATE,ISNULL(LEDGERS.ACC_CMPNAME, '') AS NAME,  ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(PURCHASERETURN.PURRET_TOTALBAGS, 0) AS TOTALBAGS, ISNULL(PURCHASERETURN.PURRET_TOTALWT, 0) AS TOTALWT, ISNULL(PURCHASERETURN.PURRET_REMARKS, '') AS REMARKS,ISNULL(PURCHASERETURN.PURRET_EWAYBILLNO, '') AS EWAYBILLNO,ISNULL(PURCHASERETURN.PURRET_BILLAMT, 0) AS BILLAMT, ISNULL(PURCHASERETURN.PURRET_TOTALCGSTAMT, 0) AS CGSTAMT, ISNULL(PURCHASERETURN.PURRET_TOTALSGSTAMT, 0) AS SGSTAMT, ISNULL(PURCHASERETURN.PURRET_TOTALIGSTAMT, 0) AS IGSTAMT, ISNULL(PURCHASERETURN.PURRET_ROUNDOFF, 0) AS ROUNDOFF, ISNULL(PURCHASERETURN.PURRET_GRANDTOTAL, 0) AS GRANDTOTAL, ISNULL(PURCHASERETURN.PURRET_CHARGES, 0) AS CHARGES, ISNULL(PURCHASERETURN.PURRET_SUBTOTAL, 0) AS SUBTOTAL", " ", " PURCHASERETURN INNER JOIN LEDGERS ON PURCHASERETURN.PURRET_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON PURCHASERETURN.PURRET_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON PURCHASERETURN.PURRET_AGENTID = AGENTLEDGERS.Acc_id ", tepmcondition)
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
                Dim OBJBILL As New PurchaseReturn
                OBJBILL.MdiParent = MDIMain
                OBJBILL.edit = editval
                OBJBILL.tempBILLno = billno
                OBJBILL.PURTYPE = PURTYPE
                OBJBILL.SELECTEDREG = SELECTEDREG
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
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'PURCHASERETURN'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'PURCHASERETURN' and register_yearid = " & YearId)
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
                dt = clscommon.search(" ISNULL(register_id,0)", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'PURCHASERETURN' and register_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    PURCHASEREGID = dt.Rows(0).Item(0)
                    ' fillregister(cmbregister, " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'PURCHASERETURN' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)

                    fillgrid("  AND PURCHASERETURN.PURRET_yearid = " & YearId & " AND  REGISTERMASTER.REGISTER_NAME ='" & cmbregister.Text.Trim & "'  order by dbo.PURCHASERETURN.PURRET_no ")
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDDTLS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDTLS.Click
        Try
            Dim OBJPUR As New PurchaseReturnGridDetails
            OBJPUR.MdiParent = MDIMain
            OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Purchase Return Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next
            opti.SheetName = "Purchase Return Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Purchase Return Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseReturnDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'PURCHASE RETURN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillregister(cmbregister, " and register_type = 'PURCHASERETURN'")
            cmbregister.Text = SELECTEDREG
            fillgrid(" AND PURCHASERETURN.PURRET_yearid = " & YearId & " AND REGISTERMASTER.REGISTER_NAME = '" & SELECTEDREG & "' order by dbo.PURCHASERETURN.PURRET_no ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class