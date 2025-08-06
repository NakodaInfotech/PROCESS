
Imports BL

Public Class PendingChallanForInvoice

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub GRNUnchekedReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRNUnchekedReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As New DataTable
            'If ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
            '    dt = objclsCMST.search(" GDN.GDN_NO AS SRNO, ISNULL(GDN.GDN_date, GETDATE()) AS DATE, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(GDN.GDN_TOTALBALES, 0) AS TOTALBALES, ISNULL(GDN.GDN_TOTALPCS, 0) AS TOTALPCS, ISNULL(GDN.GDN_TOTALMTRS, 0) AS MTRS, ISNULL(GDN.GDN_TOTALAMT, 0) AS TOTALAMT, ISNULL(GDN.GDN_TRANSREFNO, '') AS CHALLANNO  ", "", "   GDN INNER JOIN GODOWNMASTER ON GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id ", " AND ROUND((GDN.GDN_TOTALMTRS - GDN.GDN_OUTMTRS),0) > 0 AND GDN.GDN_YEARID = " & YearId)
            'Else
            'dt = objclsCMST.search(" CHALLANMASTER.CHALLAN_NO AS SRNO, ISNULL(CHALLANMASTER.CHALLAN_date, GETDATE()) AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME,ISNULL(CHALLANMASTER.CHALLAN_TOTALTAKA, 0) AS TOTALPCS, ISNULL(CHALLANMASTER.CHALLAN_TOTALMTRS, 0) AS MTRS, ISNULL(CHALLANMASTER.CHALLAN_TOTALBALES, 0) AS TOTALBALES  ", "", "   CHALLANMASTER INNER JOIN LEDGERS ON CHALLANMASTER.CHALLAN_ledgerid = LEDGERS.Acc_id ", " AND ROUND(ISNULL(CHALLANMASTER.CHALLAN_TOTALMTRS,0),0) = 0 AND CHALLANMASTER.CHALLAN_YEARID = " & YearId)
            dt = objclsCMST.search(" CHALLANMASTER.CHALLAN_NO AS SRNO, ISNULL(CHALLANMASTER.CHALLAN_date, GETDATE()) AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME,ISNULL(CHALLANMASTER.CHALLAN_TOTALTAKA, 0) AS TOTALPCS, ISNULL(CHALLANMASTER.CHALLAN_TOTALMTRS, 0) AS MTRS, ISNULL(CHALLANMASTER.CHALLAN_TOTALBALES, 0) AS TOTALBALES  ", "", "   CHALLANMASTER INNER JOIN LEDGERS ON CHALLANMASTER.CHALLAN_ledgerid = LEDGERS.Acc_id ", " AND CHALLAN_DONE=0 AND CHALLANMASTER.CHALLAN_YEARID = " & YearId)

            'End If
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Challan Pending For Invoice Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next
            opti.SheetName = "Challan Pending For Invoice"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Challan Pending For Invoice", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSHOWDETAILS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSHOWDETAILS.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class