
Imports BL
Imports System.Windows.Forms

Public Class ComplaintRegisterDetails

    Public edit As Boolean
    Dim TEMPCOMPLAINTNO As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub COMPLAINTREGISTERDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.Enter Then
                cmdok_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.R Then
                Call TOOLREFRESH_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.P Then
                Call TOOLEXCEL_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub COMPLAINTREGISTERDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillgrid(" AND dbo.COMPLAINTREGISTER.COM_yearid=" & YearId & " ORDER by dbo.COMPLAINTREGISTER.COM_NO ")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal tepmcondition)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable
            dt = objclsCMST.search(" ISNULL(COMPLAINTREGISTER.COM_NO, 0) AS SRNO, COMPLAINTREGISTER.COM_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(COMPLAINTREGISTER.COM_COMPLAINT, '') AS COMPLAINT, ISNULL(COMPLAINTREGISTER.COM_SOLUTION, '') AS SOLUTION, ISNULL(COMPLAINTREGISTER.COM_STATUS, '') AS STATUS, ISNULL(COMPLAINTREGISTER.COM_RECEIVEDBY, '') AS RECEIVEDBY ", "", " LEDGERS INNER JOIN COMPLAINTREGISTER ON LEDGERS.Acc_id = COMPLAINTREGISTER.COM_LEDGERID  ", tepmcondition)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal COMPLAINTNO As Integer)
        Try
            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objDO As New ComplaintRegister
                objDO.MdiParent = MDIMain
                objDO.edit = editval
                objDO.TEMPCOMPLAINTNO = COMPLAINTNO
                objDO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
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

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Complaint Register Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next
            opti.SheetName = "Complaint Register Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Complaint Register Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid(" AND dbo.COMPLAINTREGISTER.COM_yearid=" & YearId & " order by dbo.COMPLAINTREGISTER.COM_no")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class