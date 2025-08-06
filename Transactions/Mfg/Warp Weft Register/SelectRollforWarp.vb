
Imports BL

Public Class SelectRollforWarp

    Public DT As New DataTable
    Public TEMPGODOWNNAME As String

    Sub fillgrid(ByVal WHERE As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon()
            'THIS IS THE OG CODE
            'Dim DT As DataTable = OBJCMN.search(" TOP 1 CAST(0 AS BIT) AS CHK, ROLLRECEIVED.ROLLRECD_NO AS ROLLRECDNO, ROLLRECEIVED.ROLLRECD_DATE AS [DATE], LEDGERS.Acc_cmpname AS NAME, MILLLEDGERS.Acc_cmpname AS MILLNAME, QUALITYMASTER.QUALITY_NAME AS QUALITY,ISNULL( ROLLRECEIVED.ROLLRECD_TOTALENDS,0) AS TOTALENDS, ISNULL(ROLLRECEIVED.ROLLRECD_LENGTH,0) AS [LENGTH], ISNULL(ROLLRECEIVED.ROLLRECD_CUT,0) AS CUT, ISNULL(ROLLRECEIVED.ROLLRECD_TAPLINE,0) AS TAPLINE, ISNULL(ROLLRECEIVED.ROLLRECD_WARPINGNO,0) AS WARPINGNO, (ROLLRECD_FRESHNETT + ROLLRECD_WINDINGNETT + ROLLRECD_FIRKANETT) AS GIVENWT, (ROLLRECD_RETFRESHNETT + ROLLRECD_RETWINDINGNETT + ROLLRECD_RETFIRKANETT) AS RETURNWT", "", " ROLLRECEIVED INNER JOIN LEDGERS ON ROLLRECEIVED.ROLLRECD_WARPERID = LEDGERS.Acc_id INNER JOIN ROLLRECEIVED_DESC ON ROLLRECEIVED.ROLLRECD_NO = ROLLRECEIVED_DESC.ROLLRECD_NO AND ROLLRECEIVED.ROLLRECD_YEARID = ROLLRECEIVED_DESC.ROLLRECD_YEARID INNER JOIN LEDGERS AS MILLLEDGERS ON ROLLRECEIVED_DESC.ROLLRECD_MILLID = MILLLEDGERS.Acc_id INNER JOIN QUALITYMASTER ON ROLLRECEIVED_DESC.ROLLRECD_QUALITYID = QUALITYMASTER.QUALITY_ID ", " AND ROLLRECEIVED.ROLLRECD_YEARID = " & YearId & " AND ROLLRECEIVED.ROLLRECD_NO NOT IN (SELECT WARP_ROLLRECDNO FROM WARPREGISTER WHERE WARP_YEARID = " & YearId & ")")
            Dim DT As DataTable = OBJCMN.search(" CAST(0 AS BIT) AS CHK, ROLLRECEIVED.ROLLRECD_NO AS ROLLRECDNO, ROLLRECEIVED.ROLLRECD_DATE AS [DATE], LEDGERS.Acc_cmpname AS NAME, MILLLEDGERS.Acc_cmpname AS MILLNAME, QUALITYMASTER.QUALITY_NAME AS QUALITY,ISNULL( ROLLRECEIVED.ROLLRECD_TOTALENDS,0) AS TOTALENDS, ISNULL(ROLLRECEIVED.ROLLRECD_LENGTH,0) AS [LENGTH], ISNULL(ROLLRECEIVED.ROLLRECD_CUT,0) AS CUT, ISNULL(ROLLRECEIVED.ROLLRECD_TAPLINE,0) AS TAPLINE, ISNULL(ROLLRECEIVED.ROLLRECD_WARPINGNO,0) AS WARPINGNO, (ROLLRECD_FRESHNETT + ROLLRECD_WINDINGNETT + ROLLRECD_FIRKANETT) AS GIVENWT, (ROLLRECD_RETFRESHNETT + ROLLRECD_RETWINDINGNETT + ROLLRECD_RETFIRKANETT) AS RETURNWT, ROLLRECEIVED.ROLLRECD_PROGRAMSRNO AS PROGRAMSRNO", "", " ROLLRECEIVED INNER JOIN LEDGERS ON ROLLRECEIVED.ROLLRECD_WARPERID = LEDGERS.Acc_id INNER JOIN ROLLRECEIVED_DESC ON ROLLRECEIVED.ROLLRECD_NO = ROLLRECEIVED_DESC.ROLLRECD_NO AND ROLLRECEIVED.ROLLRECD_YEARID = ROLLRECEIVED_DESC.ROLLRECD_YEARID INNER JOIN LEDGERS AS MILLLEDGERS ON ROLLRECEIVED_DESC.ROLLRECD_MILLID = MILLLEDGERS.Acc_id INNER JOIN QUALITYMASTER ON ROLLRECEIVED_DESC.ROLLRECD_QUALITYID = QUALITYMASTER.QUALITY_ID ", " AND ROLLRECEIVED.ROLLRECD_YEARID = " & YearId & " AND ROLLRECEIVED.ROLLRECD_NO NOT IN (SELECT WARP_ROLLRECDNO FROM WARPREGISTER WHERE WARP_YEARID = " & YearId & ") ORDER BY ROLLRECEIVED.ROLLRECD_NO")
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
            Dim COUNT As Integer = 0
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    COUNT = COUNT + 1
                End If
            Next
            If COUNT > 1 Then
                MsgBox("You Can Select Only One Entry")
                Exit Sub
            End If


            DT.Columns.Add("ROLLRECDNO")
            DT.Columns.Add("DATE")
            DT.Columns.Add("NAME")
            DT.Columns.Add("MILLNAME")
            DT.Columns.Add("QUALITY")
            DT.Columns.Add("TOTALENDS")
            DT.Columns.Add("LENGTH")
            DT.Columns.Add("CUT")
            DT.Columns.Add("TAPLINE")
            DT.Columns.Add("WARPINGNO")
            DT.Columns.Add("GIVENWT")
            DT.Columns.Add("RETURNWT")
            DT.Columns.Add("PROGRAMSRNO")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("ROLLRECDNO"), dtrow("DATE"), dtrow("NAME"), dtrow("MILLNAME"), dtrow("QUALITY"), Val(dtrow("TOTALENDS")), Val(dtrow("LENGTH")), Val(dtrow("CUT")), Val(dtrow("TAPLINE")), Val(dtrow("WARPINGNO")), Val(dtrow("GIVENWT")), Val(dtrow("RETURNWT")), Val(dtrow("PROGRAMSRNO")))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SelectRollforWarp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectRollforWarp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillgrid("")
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
End Class