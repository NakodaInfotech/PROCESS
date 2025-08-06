
Imports BL

Public Class GRNPaidReport

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public multi As Boolean = False
    Public DT As DataTable
    Public fromno, tono As Integer
    Public PARTYNAME As String
    Dim REGNAME As String
    Public TEMP As String

    Private Sub GRNPaidReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRNPaidReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            CMBPAID.SelectedIndex = 0
            fillgrid(" and GRN.GRN_yearid = " & YearId)

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
            ''Dim TEMP As String = "/  /"
            TEMP = " "
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("  INVOICEMASTER.INVOICE_NO AS SRNO, LEDGERS.Acc_cmpname AS NAME, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(INVOICEMASTER_DESC.INVOICE_LRNO, '') AS LRNO, INVOICEMASTER.INVOICE_DATE AS DATE, ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS TOTALAMT, (CASE WHEN CAST(INVOICEMASTER.INVOICE_LIFTDATE AS DATE) ='' THEN NULL ELSE CAST(INVOICEMASTER.INVOICE_LIFTDATE AS DATE) END) AS LIFTDATE, ISNULL(INVOICEMASTER_DESC.INVOICE_SRNO, 0) AS GRIDSRNO", " ", "  INVOICEMASTER INNER JOIN INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id AND INVOICEMASTER.INVOICE_YEARID = LEDGERS.Acc_yearid LEFT OUTER JOIN GODOWNMASTER ON INVOICEMASTER.INVOICE_GODOWNID = GODOWN_id ", " AND (INVOICEMASTER.INVOICE_YEARID = '" & YearId & "') and (CAST(INVOICEMASTER.INVOICE_LIFTDATE AS VARCHAR)='' OR CAST(INVOICEMASTER.INVOICE_LIFTDATE AS VARCHAR) = '/  /' ) ORDER BY INVOICEMASTER.INVOICE_NO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid(" AND INVOICEMASTER.INVOICE_yearid = " & YearId & " and INVOICEMASTER.INVOICE_LIFTDATE= '" & TEMP & "' ORDER BY INVOICEMASTER.INVOICE_NO")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class