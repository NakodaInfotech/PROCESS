
Imports BL
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class SareeLotDone
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public DT As New DataTable

    Private Sub SareeLotDone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub SareeLotDone_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'JOB IN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If RBPENDING.Checked = True Then
                DT = OBJCMN.search("DISTINCT NAME, LOTNO, QUALITY,0.0 AS DAMAGE, 0.0 AS FENT, '' AS LOTDONE", "", " JOBBERPENDINGLOT AS A ", " AND NOT EXISTS (SELECT * FROM SAREELOTDONE AS B INNER JOIN LEDGERS ON B.LOTDONE_LEDGERID = LEDGERS.ACC_ID WHERE A.NAME = LEDGERS.Acc_cmpname AND A.YEARID = LOTDONE_YEARID AND A.LOTNO = B.LOTDONE_LOTNO AND B.LOTDONE_YEARID = " & YearId & ") AND YEARID = " & YearId)
            Else
                DT = OBJCMN.search(" LEDGERS.Acc_cmpname AS NAME, SAREELOTDONE.LOTDONE_LOTNO AS LOTNO, GREYQUALITYMASTER.GREY_NAME AS QUALITY, SAREELOTDONE.LOTDONE_DAMAGE AS DAMAGE, SAREELOTDONE.LOTDONE_FENT AS FENT, 'YES' AS LOTDONE", "", " SAREELOTDONE INNER JOIN LEDGERS ON SAREELOTDONE.LOTDONE_LEDGERID = LEDGERS.Acc_id INNER JOIN GREYQUALITYMASTER ON SAREELOTDONE.LOTDONE_GREYID = GREYQUALITYMASTER.GREY_ID ", " AND LOTDONE_YEARID = " & YearId)
            End If
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
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

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                Dim OBJCMN As New ClsCommon
                If dtrow("LOTDONE") = "YES" Then
                    DT = OBJCMN.Execute_Any_String("INSERT INTO SAREELOTDONE VALUES ((SELECT ACC_ID FROM LEDGERS WHERE ACC_CMPNAME = '" & dtrow("NAME") & "' AND ACC_YEARID = " & YearId & ")," & Val(dtrow("LOTNO")) & ", (SELECT GREY_ID FROM GREYQUALITYMASTER WHERE GREY_NAME = '" & dtrow("QUALITY") & "' AND GREY_YEARID = " & YearId & ")," & Format(Val(dtrow("DAMAGE")), "0.00") & "," & Format(Val(dtrow("FENT")), "0.00") & ",1," & CmpId & "," & Userid & "," & YearId & ", GETDATE(), GETDATE(), " & Userid & ")", "", "")
                ElseIf dtrow("LOTDONE") = "NO" Then
                    DT = OBJCMN.Execute_Any_String("DELETE SAREELOTDONE FROM SAREELOTDONE AS A INNER JOIN LEDGERS ON A.LOTDONE_LEDGERID = LEDGERS.ACC_ID WHERE LEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "' AND A.LOTDONE_LOTNO = " & Val(dtrow("LOTNO")) & " AND A.LOTDONE_YEARID = " & YearId, "", "")
                End If
            Next
            fillgrid()
            gridbill.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridbill_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles gridbill.InvalidRowException
        e.ExceptionMode = ExceptionMode.NoAction
    End Sub

    Private Sub gridbill_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles gridbill.ValidateRow
        Try
            If gridbill.GetRowCellValue(e.RowHandle, "LOTDONE") <> "" Then
                If MsgBox("Save Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then Call CMDOK_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try

            Dim ROW As DataRow = gridbill.GetFocusedDataRow
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If MsgBox("Delete Data?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            DT = OBJCMN.Execute_Any_String("DELETE SAREELOTDONE FROM SAREELOTDONE AS A INNER JOIN LEDGERS ON A.LOTDONE_LEDGERID = LEDGERS.ACC_ID WHERE LEDGERS.ACC_CMPNAME = '" & gridbill.GetFocusedRowCellValue("NAME") & "' AND A.LOTDONE_LOTNO = " & Val(gridbill.GetFocusedRowCellValue("LOTNO")) & " AND A.LOTDONE_YEARID = " & YearId, "", "")
            fillgrid()
            gridbill.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class