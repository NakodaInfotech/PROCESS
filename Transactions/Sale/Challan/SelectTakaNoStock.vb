
Imports BL

Public Class SelectTakaNoStock

    Public DT1 As New DataTable
    Public GODOWN As String = ""
    Public DODATE As Date

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectTakaNoStock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectTakaNoStock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillgrid()
    End Sub

    Sub fillgrid(Optional ByVal where As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            where = where & " and DATE<= '" & Format(DODATE.Date, "MM/dd/yyyy") & "'"
            If GODOWN <> "" Then where = where & " AND GODOWN = '" & GODOWN & "'"
            If ClientName = "SHREEJI" Then where = where & " AND MTRS >0  "

            Dim OBJCMN As New ClsCommon()
            Dim DT1 As DataTable = OBJCMN.search(" CAST (0 AS BIT) AS CHK, * ", "", " GREYSTOCKTAKANO ", "  AND YEARID = " & YearId & where)
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
            DT1.Columns.Add("NO")
            DT1.Columns.Add("DATE")
            DT1.Columns.Add("GODOWN")
            DT1.Columns.Add("QUALITY")
            DT1.Columns.Add("SHADE")
            DT1.Columns.Add("TAKANO")
            DT1.Columns.Add("PCS")
            DT1.Columns.Add("MTRS")
            DT1.Columns.Add("SRNO")
            DT1.Columns.Add("GRIDTYPE")


            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT1.Rows.Add(dtrow("NO"), dtrow("DATE"), dtrow("GODOWN"), dtrow("GREYQUALITY"), dtrow("SHADE"), dtrow("TAKANO"), dtrow("PCS"), dtrow("MTRS"), dtrow("SRNO"), dtrow("TYPE"))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridbill_KeyDown(sender As Object, e As KeyEventArgs) Handles gridbill.KeyDown
        Try
            If e.KeyCode = Keys.Space Then
                Dim dtrow As DataRow = gridbill.GetFocusedDataRow()
                dtrow("CHK") = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkall_CheckedChanged(sender As Object, e As EventArgs) Handles chkall.CheckedChanged
        Try
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                dtrow("CHK") = chkall.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class