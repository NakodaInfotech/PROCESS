
Imports System.Windows.Forms
Imports BL

Public Class SelectPurchaseInvoice

    Dim addcol As Integer = 0
    Public DT As New DataTable
    Dim N As Integer = 0
    Dim tempindex, i As Integer
    Dim col As New DataGridViewCheckBoxColumn  'Dim dt As New DataTable
    Public PARTYNAME As String = ""  'for whereclause in fillgrid
    Public MILLNAME As String = ""  'for whereclause in fillgrid
    Public FRMSTRING As String = ""

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectPurchaseInvoice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectPurchaseInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Top = 100
        fillgrid()
    End Sub

    Sub fillgrid()
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon

            Dim WHERECLAUSE As String = ""
            If PARTYNAME <> "" Then WHERECLAUSE = WHERECLAUSE & " AND LEDGERS.Acc_cmpname= '" & PARTYNAME & "'"
            If MILLNAME <> "" Then WHERECLAUSE = WHERECLAUSE & " AND MILLNAME.Acc_cmpname= '" & MILLNAME & "'"
            Dim DT As DataTable = OBJCMN.search("*", "", " (SELECT CAST (0 AS BIT) AS CHK , CAST(PURCHASEMASTER.BILL_NO AS VARCHAR(10)) AS SRNO, PURCHASEMASTER.BILL_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(PURCHASEMASTER.BILL_PARTYBILLNO, '') AS PARTYBILLNO, PURCHASEMASTER.BILL_PARTYBILLDATE AS PARTYBILLDATE, ISNULL(PURCHASEMASTER.BILL_TOTALBAGS, 0) AS TOTALBAGS, ISNULL(PURCHASEMASTER.BILL_TOTALWT, 0) AS TOTALWT, ISNULL(PURCHASEMASTER.BILL_GRANDTOTAL, 0) AS GRANDTOTAL, PURCHASEMASTER.BILL_REGISTERID AS REGID FROM PURCHASEMASTER INNER JOIN LEDGERS ON PURCHASEMASTER.BILL_LEDGERID = LEDGERS.Acc_id WHERE PURCHASEMASTER.BILL_PRDONE=0 " & WHERECLAUSE & " AND PURCHASEMASTER.BILL_PURTYPE = '" & FRMSTRING & "' AND PURCHASEMASTER.BILL_YEARID = " & YearId & " UNION ALL SELECT CAST (0 AS BIT) As CHK , OPENINGBILL.BILL_INITIALS As SRNO, OPENINGBILL.BILL_DATE As Date, LEDGERS.Acc_cmpname As NAME, ISNULL(OPENINGBILL.BILL_NO, '') AS PARTYBILLNO, OPENINGBILL.BILL_DATE AS PARTYBILLDATE, 0 AS TOTALBAGS, 0 AS TOTALWT, ISNULL(OPENINGBILL.BILL_AMT, 0) AS GRANDTOTAL, OPENINGBILL.BILL_REGISTERID AS REGID  FROM OPENINGBILL INNER Join LEDGERS On OPENINGBILL.BILL_LEDGERID = LEDGERS.Acc_id  WHERE OPENINGBILL.BILL_BALANCE > 0 AND OPENINGBILL.BILL_YEARID = " & YearId & " AND LEDGERS.ACC_CMPNAME = '" & PARTYNAME & "') AS T", "")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            'Dim n As String = ""
            'For i As Integer = 0 To gridbill.RowCount - 1
            '                Dim dtrow As DataRow = gridbill.GetDataRow(i)
            '                If Convert.ToBoolean(dtrow("CHK")) = True Then
            '                    If n <> "" Then
            '                        If n = (dtrow("GRNNO")) Then
            '                            GoTo Line1
            '                        Else
            '                            MsgBox("Pls select Only One GRN !")
            '                            Exit Sub
            '                        End If
            '                    End If
            'Line1:
            '                    n = (dtrow("GRNNO"))
            '                End If
            '            Next

            DT.Columns.Add("SRNO")
            DT.Columns.Add("REGID")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("SRNO"), Val(dtrow("REGID")))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub
End Class