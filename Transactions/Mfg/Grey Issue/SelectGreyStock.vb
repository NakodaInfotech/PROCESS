
Imports BL
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls

Public Class SelectGreyStock

    Public DT As New DataTable
    Public TEMPGODOWNNAME As String
    Dim BEAMNO As Integer

    Sub fillgrid(ByVal WHERE As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon()
            Dim DT As DataTable = OBJCMN.search(" CAST (0 AS BIT) AS CHK , GODOWN, GREYQUALITY,SUM(PCS) AS PCS, SUM(MTRS) AS MTRS, YEARID ", "", " GREYSTOCK ", " AND GODOWN='" & TEMPGODOWNNAME & "' AND YEARID = " & YearId & " GROUP BY GODOWN,GREYQUALITY,YEARID HAVING SUM(PCS)>0 AND SUM(MTRS)>0")
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

            DT.Columns.Add("GODOWN")
            DT.Columns.Add("GREYQUALITY")
            DT.Columns.Add("PCS")
            DT.Columns.Add("MTRS")
            'DT.Columns.Add("LOTNO")
            'DT.Columns.Add("BALENO")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    'DT.Rows.Add(dtrow("GODOWN"), dtrow("GREYQUALITY"), dtrow("PCS"), dtrow("MTRS"), dtrow("WT"), dtrow("LOTNO"), dtrow("BALENO"))
                    DT.Rows.Add(dtrow("GODOWN"), dtrow("GREYQUALITY"), dtrow("PCS"), dtrow("MTRS"))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SelectGreyStock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectGreyStock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillgrid("")
    End Sub

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
End Class