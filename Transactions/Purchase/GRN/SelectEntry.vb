
Imports BL
Imports System.Windows.Forms

Public Class SelectEntry
    Dim tempindex, i As Integer
    Dim ADDCOL As Boolean = False
    Dim col As New DataGridViewCheckBoxColumn
    Dim WO As Integer
    Public baleno As String = ""
    Public PARTYNAME As String = ""
    Public TYPE As String = ""
    Public DT1 As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Top = 100
        fillgrid()
    End Sub

    Sub fillgrid(Optional ByVal where As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor

            Dim objclspreq As New ClsCommon()
            Dim DT1 As DataTable = objclspreq.search(" CAST (0 AS BIT) AS CHK ,ISNULL(MATERIALDESPATCH.MATDES_NO, 0) AS MATDESNO,ISNULL(MATERIALDESPATCH_DESC.MATDES_SRNO, 0) AS MSRNO, MATERIALDESPATCH.MATDES_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(MATERIALDESPATCH_DESC.MATDES_BAGS, 0) AS BAGS, ISNULL(MATERIALDESPATCH_DESC.MATDES_WT, '') AS WT, ISNULL(MILLNAME.Acc_cmpname, '') AS MILLNAME, ISNULL(TRANSNAME.Acc_cmpname, '') AS TRANS, ISNULL(MATERIALDESPATCH_DESC.MATDES_LRNO, '') AS LRNO", "", "    MATERIALDESPATCH INNER JOIN MATERIALDESPATCH_DESC ON MATERIALDESPATCH.MATDES_NO = MATERIALDESPATCH_DESC.MATDES_NO AND MATERIALDESPATCH.MATDES_YEARID = MATERIALDESPATCH_DESC.MATDES_YEARID INNER JOIN LEDGERS AS MILLNAME ON MATERIALDESPATCH.MATDES_MILLID = MILLNAME.Acc_id AND MATERIALDESPATCH.MATDES_YEARID = MILLNAME.Acc_yearid INNER JOIN LEDGERS AS TRANSNAME ON MATERIALDESPATCH.MATDES_TRANSID = TRANSNAME.Acc_id AND MATERIALDESPATCH.MATDES_YEARID = TRANSNAME.Acc_yearid LEFT OUTER JOIN LEDGERS ON MATERIALDESPATCH.MATDES_YEARID = LEDGERS.Acc_yearid AND MATERIALDESPATCH.MATDES_LEDGERID = LEDGERS.Acc_id ", "  AND MATERIALDESPATCH_DESC.MATDES_DONE = 0 AND LEDGERS.Acc_cmpname='" & PARTYNAME & "' AND MATERIALDESPATCH.MATDES_YEARID = " & YearId)

            gridbilldetails.DataSource = DT1
            If DT1.Rows.Count > 0 Then
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
            DT1.Columns.Add("MATDESNO")
            DT1.Columns.Add("MSRNO")
            'DT1.Columns.Add("DATE")
            'DT1.Columns.Add("NAME")
            'DT1.Columns.Add("BAGS")
            'DT1.Columns.Add("WT")


            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    'DT1.Rows.Add(dtrow("PONO"), Format(Convert.ToDateTime(dtrow("DATE")).Date, "dd/MM/yyyy"), dtrow("NAME"), dtrow("BAGS"), dtrow("WT"))
                    'DT1.Rows.Add(dtrow("MATDESNO"), Format(Convert.ToDateTime(dtrow("DATE")).Date, "dd/MM/yyyy"))
                    DT1.Rows.Add(dtrow("MATDESNO"), dtrow("MSRNO"))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub


End Class