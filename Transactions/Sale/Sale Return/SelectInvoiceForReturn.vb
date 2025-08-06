
Imports BL

Public Class SelectInvoiceForReturn

    Dim SELECTIONFORMULA As String = ""
    Public DT As New DataTable
    Public PARTYNAME As String = ""

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectStockReturn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectStockReturn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillgrid(" ")
    End Sub

    Sub fillgrid(ByVal WHERE As String)
        Try
            If PARTYNAME <> "" Then WHERE = WHERE & " AND LEDGERS.ACC_CMPNAME = '" & PARTYNAME & "'"
            Dim OBJCMN As New ClsCommon()
            Dim DT As DataTable = OBJCMN.search("*", "", "(SELECT DISTINCT CAST(0 AS BIT) AS CHK, ISNULL(OPENINGBILL.BILL_NO, 0) AS INVNO, ISNULL(OPENINGBILL.BILL_DATE, GETDATE()) AS INVDATE, '0' AS GDNNO, ISNULL(OPENINGBILL.BILL_DATE, GETDATE()) AS GDNDATE, 0 AS TOTALPCS, 0 AS TOTALMTRS, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(OPENINGBILL.BILL_REGISTERID,0) AS REGID, 'OPENING' AS INVOICETYPE, 0 AS LOTNO, ISNULL(OPENINGBILL.BILL_INITIALS,'') AS INVINITIALS, ISNULL(OPENINGBILL.BILL_PRINTINITIALS,'') AS PRINTINITIALS FROM LEDGERS INNER JOIN OPENINGBILL ON LEDGERS.Acc_id = OPENINGBILL.BILL_LEDGERID WHERE OPENINGBILL.BILL_TYPE = 'SALE' AND OPENINGBILL.BILL_YEARID = " & YearId & WHERE & " AND OPENINGBILL.BILL_BALANCE > 0 UNION ALL SELECT DISTINCT CAST(0 AS BIT) AS CHK, ISNULL(INVOICEMASTER.INVOICE_NO, 0) AS INVNO, ISNULL(INVOICEMASTER.INVOICE_DATE, GETDATE()) AS INVDATE, ISNULL(INVOICEMASTER.INVOICE_CHALLANNO, '') AS GDNNO, ISNULL(INVOICEMASTER.INVOICE_CHALLANDATE, GETDATE()) AS GDNDATE, SUM(INVOICEMASTER_DESC.INVOICE_PCS) AS TOTALPCS, SUM(ISNULL(INVOICEMASTER_DESC.INVOICE_MTRS, 0)) AS TOTALMTRS, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(INVOICEMASTER.INVOICE_REGISTERID, 0) AS REGID, 'INVOICE' AS INVOICETYPE, ISNULL(INVOICEMASTER_DESC.INVOICE_LOTNO, 0) AS LOTNO, ISNULL(INVOICEMASTER.INVOICE_INITIALS,'') AS INVINITIALS, ISNULL(INVOICEMASTER.INVOICE_PRINTINITIALS,'') AS PRINTINITIALS FROM LEDGERS INNER JOIN INVOICEMASTER ON LEDGERS.Acc_id = INVOICEMASTER.INVOICE_LEDGERID INNER JOIN INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID WHERE INVOICEMASTER.INVOICE_YEARID = " & YearId & WHERE & " AND INVOICEMASTER.INVOICE_BALANCE > 0 GROUP BY ISNULL(INVOICEMASTER.INVOICE_NO, 0), ISNULL(INVOICEMASTER.INVOICE_DATE, GETDATE()), ISNULL(INVOICEMASTER.INVOICE_CHALLANNO, ''), ISNULL(INVOICEMASTER.INVOICE_CHALLANDATE, GETDATE()), ISNULL(LEDGERS.Acc_cmpname, ''), ISNULL(INVOICEMASTER.INVOICE_REGISTERID, 0), ISNULL(INVOICEMASTER_DESC.INVOICE_LOTNO, 0), ISNULL(INVOICEMASTER.INVOICE_INITIALS,''), ISNULL(INVOICEMASTER.INVOICE_PRINTINITIALS,'')) AS T", " ORDER BY T.INVNO")
            gridbilldetails.DataSource = DT
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Dim COUNT As Integer
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    COUNT = COUNT + 1
                End If
            Next

            If COUNT > 1 Then
                MsgBox("You Can Select Only One Invoice")
                Exit Sub
            End If

            DT.Columns.Add("INVNO")
            DT.Columns.Add("REGID")
            DT.Columns.Add("INVOICETYPE")
            DT.Columns.Add("INVINITIALS")
            DT.Columns.Add("PRINTINITIALS")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(Val(dtrow("INVNO")), Val(dtrow("REGID")), dtrow("INVOICETYPE"), dtrow("INVINITIALS"), dtrow("PRINTINITIALS"))
                End If
            Next

            Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

End Class