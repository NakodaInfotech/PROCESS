

Imports BL

Public Class SelectBeamWeaverStock

    Public DT As New DataTable
    Public WEAVERNAME As String = ""

    Sub fillgrid()
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim WHERE As String = ""
            If WEAVERNAME <> "" Then WHERE = " AND WEAVERNAME = '" & WEAVERNAME & "'"
            Dim OBJCMN As New ClsCommon()
            Dim DT As DataTable = OBJCMN.search(" CAST (0 AS BIT) AS CHK , BEAMSTOCK_WEAVER.WEAVERNAME, BEAMSTOCK_WEAVER.TYPE, BEAMSTOCK_WEAVER.BEAMNAME, BEAMSTOCK_WEAVER.BEAMNO, BEAMSTOCK_WEAVER.CUT,  BEAMSTOCK_WEAVER.WT,  BEAMSTOCK_WEAVER.WTCUT, BEAMSTOCK_WEAVER.NO AS FROMNO, BEAMSTOCK_WEAVER.SRNO AS FROMSRNO, BEAMSTOCK_WEAVER.ENDS AS ENDS, BEAMMASTER.BEAM_TAPLINE AS TAPLINE ", "", "BEAMSTOCK_WEAVER INNER JOIN BEAMMASTER ON BEAMSTOCK_WEAVER.BEAMNAME = BEAMMASTER.BEAM_NAME AND BEAMSTOCK_WEAVER.YEARID = BEAMMASTER.BEAM_YEARID", WHERE & " AND BEAMSTOCK_WEAVER.YEARID = " & YearId & " ORDER BY TYPE, FROMNO, FROMSRNO ")
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

            DT.Columns.Add("BEAMNAME")
            DT.Columns.Add("BEAMNO")
            DT.Columns.Add("ENDS")
            DT.Columns.Add("TAPLINE")
            DT.Columns.Add("CUT")
            DT.Columns.Add("WT")
            DT.Columns.Add("WTCUT")
            DT.Columns.Add("FROMNO")
            DT.Columns.Add("FROMSRNO")
            DT.Columns.Add("TYPE")


            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("BEAMNAME"), dtrow("BEAMNO"), dtrow("ENDS"), dtrow("TAPLINE"), Val(dtrow("CUT")), Val(dtrow("WT")), Val(dtrow("WTCUT")), dtrow("FROMNO"), dtrow("FROMSRNO"), dtrow("TYPE"))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SelectBeamWeaverStock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectBeamWeaverStock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillgrid()
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
End Class