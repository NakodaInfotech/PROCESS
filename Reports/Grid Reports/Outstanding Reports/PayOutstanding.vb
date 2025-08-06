
Imports BL
Imports System.Windows.Forms

Public Class PayOutstanding

    Private Sub PayOutstanding_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Alt = True Then
                showform(False, 0, "")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal tepmcondition)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable
            dt = objclsCMST.search(" * ", "", " OUTSTANDINGREPORT ", tepmcondition)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal INVNO As Integer, ByVal REGISTER As String)
        Try
            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objgdn As New PurchaseMaster
                objgdn.MdiParent = MDIMain
                objgdn.EDIT = editval
                objgdn.TEMPREGNAME = REGISTER
                objgdn.TEMPBILLNO = INVNO
                objgdn.Show()
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridINVOICE_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("BILL"), gridbill.GetFocusedRowCellValue("REGTYPE"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Payable Outstanding Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next
            opti.SheetName = "Payable Outstanding Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Payable Outstanding Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PayOutstanding_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            CMBSEARCH.Text = "COMPANY"
            fillgrid(" and CMPID=" & CmpId & " and locationid=" & Locationid & " and yearid=" & YearId & " and order by DATE,TYPE,BILL ")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSEARCH_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSEARCH.Validating
        If CMBSEARCH.Text = "COMPANY" Then fillgrid(" and CMPID=" & CmpId & " and locationid=" & Locationid & " and yearid=" & YearId & " and order by DATE,TYPE,BILL ")
        If CMBSEARCH.Text = "ALL" Then fillgrid(" order by DATE,TYPE,BILL ")
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("BILL"), gridbill.GetFocusedRowCellValue("REGTYPE"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

  
End Class