
Imports BL

Public Class PcsWiseDetails

    Public MAINLINENO, FROMNO As Integer
    Public TOTALMTRS As Double
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public DT As DataTable
    Public TEMPTAKA As Integer

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub GETDATA()
        Try
            For Each DTROW As DataRow In DT.Rows
                If DTROW("MAINLINENO") = MAINLINENO Then GRIDMTRS.Rows.Add(Val(DTROW("SRNO")), Format(Val(DTROW("MTRS")), "0.00"), Val(DTROW("TP")))
            Next
            TOTAL()
            getsrno(GRIDMTRS)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PcsWiseDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Enter And ClientName = "SASHWINKUMAR" Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PcsWiseDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GETDATA()
    End Sub

    Sub CLEAR()
        TXTMTRS.Clear()
        TXTTP.Clear()
    End Sub

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                'If ClientName <> "NIRMALA" Then
                '    'LIMITIATION OF 200 PCS
                '    If Val(TXTTOTALPCS.Text.Trim) = 200 Then
                '        MsgBox("Cannot Exceed 200 Pcs", MsgBoxStyle.Critical)
                '        Exit Sub
                '    End If
                'End If
                GRIDMTRS.Rows.Add(0, Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTTP.Text.Trim), "0"))
            Else
                GRIDMTRS.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
                GRIDMTRS.Item(GTP.Index, TEMPROW).Value = Format(Val(TXTTP.Text.Trim), "0")
                GRIDDOUBLECLICK = False
            End If
            TOTAL()
            TXTMTRS.Clear()
            TXTTP.Clear()
            TXTMTRS.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try
            TXTTOTALMTRS.Text = 0
            TXTTOTALTP.Text = 0
            For Each ROW As DataGridViewRow In GRIDMTRS.Rows
                TXTTOTALMTRS.Text = Format(Val(TXTTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).Value), "0.00")
                If Val(ROW.Cells(GTP.Index).Value) > 0 Then TXTTOTALTP.Text += 1
            Next
            TXTTOTALPCS.Text = GRIDMTRS.RowCount
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress, TXTGMTRS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTTP_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTP.KeyPress, TXTGTAKA.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub GRIDMTRS_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDMTRS.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Sub
            GRIDDOUBLECLICK = True
            TXTMTRS.Text = Val(GRIDMTRS.Item(GMTRS.Index, GRIDMTRS.CurrentRow.Index).Value)
            TXTTP.Text = Val(GRIDMTRS.Item(GTP.Index, GRIDMTRS.CurrentRow.Index).Value)

            TEMPROW = GRIDMTRS.CurrentRow.Index
            TXTMTRS.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            If DT.Columns.Count = 0 Then
                DT.Columns.Add("SRNO")
                DT.Columns.Add("MTRS")
                DT.Columns.Add("TP")
                DT.Columns.Add("MAINLINENO")
            End If
            'FIRST REMOVE LINES FROM DTTARE IF PRESENT FOR THE SAME MAINLINENO, THEN ADD AGAIN
LINE1:
            For Each DTROW As DataRow In DT.Rows
                If DTROW("MAINLINENO") = MAINLINENO Then
                    DT.Rows.Remove(DTROW)
                    GoTo LINE1
                End If
            Next

            For Each ROW As DataGridViewRow In GRIDMTRS.Rows
                DT.Rows.Add(Val(ROW.Cells(GSRNO.Index).Value), Val(ROW.Cells(GMTRS.Index).Value), Val(ROW.Cells(GTP.Index).Value), MAINLINENO)
            Next
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDMTRS_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDMTRS.CellValidating
        Try
            Dim colNum As Integer = GRIDMTRS.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GMTRS.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDMTRS.CurrentCell.Value = Nothing Then GRIDMTRS.CurrentCell.Value = "0.00"
                        GRIDMTRS.CurrentCell.Value = Format(Val(GRIDMTRS.Item(colNum, e.RowIndex).EditedFormattedValue), "0.00")
                        '' everything is good
                        TOTAL()
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If

                Case GTP.Index
                    Dim dDebit As Integer
                    Dim bValid As Boolean = Integer.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDMTRS.CurrentCell.Value = Nothing Then GRIDMTRS.CurrentCell.Value = "0"
                        GRIDMTRS.CurrentCell.Value = Format(Val(GRIDMTRS.Item(colNum, e.RowIndex).EditedFormattedValue), "0.00")
                        '' everything is good
                        TOTAL()
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If

            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDMTRS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDMTRS.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                GRIDMTRS.Rows.RemoveAt(GRIDMTRS.CurrentRow.Index)
                TOTAL()
                getsrno(GRIDMTRS)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_KeyDown(sender As Object, e As KeyEventArgs) Handles TXTMTRS.KeyDown
        Try
            If ClientName <> "SASHWINKUMAR" And e.KeyCode = Keys.Enter Then TXTTP_Validated(sender, e)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub CMDFILL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDFILL.Click
        Try
            'If ClientName <> "NIRMALA" Then
            '    If Val(TXTGTAKA.Text.Trim) > 200 Or ((Val(TXTTOTALPCS.Text.Trim) + Val(TXTGTAKA.Text.Trim)) > 200) Then
            '        MsgBox("Cannot Exceed 200 Pcs", MsgBoxStyle.Critical)
            '        Exit Sub
            '    End If
            'End If

            For I As Integer = 0 To Val(TXTGTAKA.Text.Trim) - 1
                ' FILLMTRGRID()
                GRIDMTRS.Rows.Add(0, Format(Val(TXTGMTRS.Text.Trim), "0.00"), 0)
            Next
            If GRIDMTRS.RowCount > 25 Then GRIDMTRS.FirstDisplayedScrollingRowIndex = GRIDMTRS.RowCount - 25
            TOTAL()
            getsrno(GRIDMTRS)
            TXTGMTRS.Clear()
            TXTGTAKA.Clear()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTTP_Validated(sender As Object, e As EventArgs) Handles TXTTP.Validated
        Try
            If Val(TXTMTRS.Text.Trim) > 0 Then
                FILLGRID()
                If GRIDMTRS.RowCount > 25 Then GRIDMTRS.FirstDisplayedScrollingRowIndex = GRIDMTRS.RowCount - 25
                getsrno(GRIDMTRS)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class