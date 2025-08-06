
Imports BL

Public Class MISFilter

    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

    Sub getFromToDate()
        a1 = DatePart(DateInterval.Day, dtfrom.Value)
        a2 = DatePart(DateInterval.Month, dtfrom.Value)
        a3 = DatePart(DateInterval.Year, dtfrom.Value)
        fromD = "(" & a3 & "," & a2 & "," & a1 & ")"

        a11 = DatePart(DateInterval.Day, dtto.Value)
        a12 = DatePart(DateInterval.Month, dtto.Value)
        a13 = DatePart(DateInterval.Year, dtto.Value)
        toD = "(" & a13 & "," & a12 & "," & a11 & ")"
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Try
            Dim OBJMIS As New StockDesign
            OBJMIS.MdiParent = MDIMain
            OBJMIS.FRMSTRING = "MIS"
            OBJMIS.WHERECLAUSE = "{YEARMASTER.YEAR_ID} = " & YearId
            If chkdate.CheckState = CheckState.Checked Then
                OBJMIS.FROMDATE = dtfrom.Value.Date
                OBJMIS.TODATE = dtto.Value.Date
                OBJMIS.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
            Else
                OBJMIS.FROMDATE = AccFrom.Date
                OBJMIS.TODATE = AccTo.Date
                OBJMIS.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If
            OBJMIS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MISFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MISFilter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            dtfrom.Value = Mydate
            dtto.Value = Mydate
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class