Imports BL

Public Class SelectJobOut
    Public DT As New DataTable
    Public TEMPGODOWNNAME As String
    Public TEMPJOBBERNAME As String

    Sub fillgrid(ByVal WHERE As String)
        Try

            'Cursor.Current = Cursors.WaitCursor

            'If TEMPJOBBERNAME <> "" Then WHERE = WHERE & " AND JOBBERNAME = '" & TEMPJOBBERNAME & "'"

            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon()
            'Dim DT As DataTable = OBJCMN.search("  CAST(0 AS BIT) AS CHK, JOBOUT.JO_no AS JOBNO, ISNULL(JOBOUT_DESC.JO_GRIDSRNO, 0) AS SRNO, ISNULL(LEDGERS.Acc_cmpname, '') AS JOBBERNAME, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL((JOBOUT_DESC.JO_WT - JOBOUT_DESC.JO_OUTWT), 0) AS WT, ISNULL((JOBOUT_DESC.JO_WINDING - JOBOUT_DESC.JO_OUTWINDING), 0) AS WINDING, ISNULL((JOBOUT_DESC.JO_FIRKA - JOBOUT_DESC.JO_OUTFIRKA), 0) AS FIRKA, JOBOUT_DESC.JO_TYPE AS GRIDTYPE", "", "JOBOUT INNER JOIN JOBOUT_DESC ON JOBOUT.JO_no = JOBOUT_DESC.JO_NO AND JOBOUT.JO_yearid = JOBOUT_DESC.JO_YEARID INNER JOIN LEDGERS ON JOBOUT.JO_ledgerid = LEDGERS.Acc_id INNER JOIN QUALITYMASTER ON JOBOUT_DESC.JO_QUALITYID = QUALITYMASTER.QUALITY_ID INNER JOIN GODOWNMASTER ON JOBOUT.JO_GODOWNID = GODOWNMASTER.GODOWN_ID", "and LEDGERS.Acc_cmpname='" & TEMPJOBBERNAME & "' AND JOBOUT.JO_YEARID = " & YearId)
            Dim DT As DataTable = OBJCMN.search("  CAST(0 AS BIT) AS CHK, JOBOUT.JO_no AS JOBNO, ISNULL(JOBOUT_DESC.JO_GRIDSRNO, 0) AS SRNO, ISNULL(LEDGERS.Acc_cmpname, '') AS JOBBERNAME, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY,JOBOUT_DESC.JO_LOTNO AS LOTNO, ISNULL(COLORMASTER.COLOR_NAME, '') AS SHADE, ISNULL((JOBOUT_DESC.JO_WT - JOBOUT_DESC.JO_OUTWT), 0) AS WT, ISNULL((JOBOUT_DESC.JO_WINDING), 0) AS WINDING, ISNULL((JOBOUT_DESC.JO_FIRKA), 0) AS FIRKA, JOBOUT_DESC.JO_TYPE AS GRIDTYPE", "", " JOBOUT INNER JOIN JOBOUT_DESC ON JOBOUT.JO_no = JOBOUT_DESC.JO_NO AND JOBOUT.JO_yearid = JOBOUT_DESC.JO_YEARID INNER JOIN LEDGERS ON JOBOUT.JO_JOBBERID = LEDGERS.Acc_id INNER JOIN QUALITYMASTER ON JOBOUT_DESC.JO_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN COLORMASTER ON JOBOUT_DESC.JO_SHADEID = COLORMASTER.COLOR_ID", "AND (JOBOUT_DESC.JO_WT - JOBOUT_DESC.JO_OUTWT) > 0 and LEDGERS.Acc_cmpname='" & TEMPJOBBERNAME & "' AND JOBOUT.JO_YEARID = " & YearId)
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

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            Dim n As String = ""
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If n <> "" Then
                        If n = (dtrow("JOBBERNAME")) Then
                            GoTo Line1
                        Else
                            MsgBox("Pls select same jobbername !")
                            Exit Sub
                        End If
                    End If
Line1:
                    n = (dtrow("JOBBERNAME"))
                End If
            Next


            DT.Columns.Add("JOBBERNAME")
            DT.Columns.Add("QUALITY")
            DT.Columns.Add("LOTNO")
            DT.Columns.Add("SHADE")
            DT.Columns.Add("WT")
            DT.Columns.Add("WINDING")
            DT.Columns.Add("FIRKA")
            DT.Columns.Add("JOBNO")
            DT.Columns.Add("SRNO")
            DT.Columns.Add("GRIDTYPE")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("JOBBERNAME"), dtrow("QUALITY"), dtrow("LOTNO"), dtrow("SHADE"), dtrow("WT"), dtrow("WINDING"), dtrow("FIRKA"), dtrow("JOBNO"), dtrow("SRNO"), dtrow("GRIDTYPE"))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SelectJobOut_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectJobOut_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillgrid("")
    End Sub

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
End Class