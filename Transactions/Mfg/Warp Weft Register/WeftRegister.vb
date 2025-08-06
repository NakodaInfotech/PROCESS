
Imports BL

Public Class WeftRegister

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean
    Public TEMPWEFTNO As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
    End Sub

    Sub TOTAL()
        Try
            TXTTOTALLENGTH.Text = 0
            TXTCOUNT.Text = Format((Val(TXTTOTALLENGTH.Text.Trim) / 1850) / Val(TXTCONEWT.Text.Trim), "0.00")

            For Each ROW As DataGridViewRow In GRIDWEFT.Rows
                ROW.Cells(GTOTAL.Index).Value = Val(ROW.Cells(GLENGTH.Index).Value) * Val(ROW.Cells(GROLL.Index).Value)
                TXTTOTALLENGTH.Text += Val(ROW.Cells(GTOTAL.Index).Value)
            Next
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()

        WEFTDATE.Text = Mydate
        TXTCOUNT.Clear()
        TXTCONEWT.Clear()

        CMBMILL.Text = ""
        CMBQUALITY.Text = ""

        TXTLENGTH.Clear()
        TXTROLL.Clear()
        GRIDWEFT.RowCount = 0

        TXTTOTALLENGTH.Clear()
        TXTREMARKS.Clear()

        EP.Clear()
        GRIDDOUBLECLICK = False

        tstxtbillno.Clear()

        GETMAX_WEFT_NO()

    End Sub

    Sub GETMAX_WEFT_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(WEFT_NO),0)+1", "WEFTREGISTER", "AND WEFT_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTWEFTNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub TXTWTRETURNED_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCONEWT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTLENGTH_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTROLL.KeyPress, TXTLENGTH.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub WeftRegister_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then CMDSAVE_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call toolprevious_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call toolnext_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Sub FILLCMB()
        Try
            fillname(CMBMILL, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'")
            fillQUALITY(CMBQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then fillname(CMBMILL, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBMILL.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILL.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            TXTCOUNT.Text = Format(((Val(TXTTOTALLENGTH.Text.Trim) / 1850) / Val(TXTCONEWT.Text.Trim)), "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then namevalidate(CMBMILL, CMBCODE, e, Me, txtadd, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "MILL")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJROLLS As New SelectQuality
                OBJROLLS.ShowDialog()
                If OBJROLLS.TEMPNAME <> "" Then CMBQUALITY.Text = OBJROLLS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WeftRegister_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            CLEAR()

            If EDIT = True Then
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dttable As New DataTable
                Dim OBJWEFT As New ClsWeftRegister

                OBJWEFT.alParaval.Add(TEMPWEFTNO)
                OBJWEFT.alParaval.Add(YearId)
                dttable = OBJWEFT.SELECTWARP()

                If dttable.Rows.Count > 0 Then

                    TXTWEFTNO.Text = TEMPWEFTNO
                    WEFTDATE.Text = dttable.Rows(0).Item("DATE")
                    TXTCOUNT.Text = dttable.Rows(0).Item("COUNT")
                    TXTCONEWT.Text = dttable.Rows(0).Item("CONEWT")
                    CMBMILL.Text = dttable.Rows(0).Item("MILLNAME").ToString
                    CMBQUALITY.Text = dttable.Rows(0).Item("QUALITY").ToString


                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString

                    For Each ROW As DataRow In dttable.Rows
                        GRIDWEFT.Rows.Add(Val(ROW("LENGTH")), Val(ROW("ROLLS")), Val(ROW("TOTALLEN")))
                    Next

                    TOTAL()
                    CMBMILL.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(WEFTDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(Val(TXTCOUNT.Text.Trim))
            alParaval.Add(Val(TXTCONEWT.Text.Trim))

            alParaval.Add(CMBMILL.Text.Trim)
            alParaval.Add(CMBQUALITY.Text.Trim)
            alParaval.Add(Val(TXTTOTALLENGTH.Text.Trim))

            Dim LENGTH As String = ""
            Dim ROLLS As String = ""
            Dim TOTALLEN As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDWEFT.Rows
                If row.Cells(GLENGTH.Index).Value <> Nothing Then
                    If LENGTH = "" Then
                        LENGTH = Val(row.Cells(GLENGTH.Index).Value)
                        ROLLS = Val(row.Cells(GROLL.Index).Value)
                        TOTALLEN = Val(row.Cells(GTOTAL.Index).Value)
                    Else
                        LENGTH = LENGTH & "|" & Val(row.Cells(GLENGTH.Index).Value)
                        ROLLS = ROLLS & "|" & Val(row.Cells(GROLL.Index).Value)
                        TOTALLEN = TOTALLEN & "|" & Val(row.Cells(GTOTAL.Index).Value)
                    End If
                End If
            Next

            alParaval.Add(LENGTH)
            alParaval.Add(ROLLS)
            alParaval.Add(TOTALLEN)

            alParaval.Add(TXTREMARKS.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim OBJWEFT As New ClsWeftRegister
            OBJWEFT.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJWEFT.save()
                TEMPWEFTNO = DT.Rows(0).Item(0)
                TXTWEFTNO.Text = TEMPWEFTNO
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPWEFTNO)
                IntResult = OBJWEFT.Update()
                EDIT = False
                MsgBox("Details Updated")

            End If

            CLEAR()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True


        If WEFTDATE.Text = "__/__/____" Then
            EP.SetError(WEFTDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(WEFTDATE.Text) Then
                EP.SetError(WEFTDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If Val(TXTCONEWT.Text.Trim) = 0 Then
            EP.SetError(TXTCONEWT, "Please Enter Wt")
            bln = False
        End If

        If CMBMILL.Text.Trim.Length = 0 Then
            EP.SetError(CMBMILL, "Please Select Mill Name")
            bln = False
        End If

        If CMBQUALITY.Text.Trim.Length = 0 Then
            EP.SetError(CMBQUALITY, "Please Select Quality")
            bln = False
        End If

        If GRIDWEFT.RowCount = 0 Then
            EP.SetError(TXTROLL, "Enter Roll Details")
            bln = False
        End If

        Return bln
    End Function

    Private Sub WEFTDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles WEFTDATE.GotFocus
        WEFTDATE.SelectAll()
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
LINE1:
            TEMPWEFTNO = Val(TXTWEFTNO.Text) - 1
Line2:
            If TEMPWEFTNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" WEFT_NO ", "", "  WEFTREGISTER", " AND WEFT_NO = '" & TEMPWEFTNO & "' AND WEFTREGISTER.WEFT_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    WeftRegister_Load(sender, e)
                Else
                    TEMPWEFTNO = Val(TEMPWEFTNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDWEFT.RowCount = 0 And TEMPWEFTNO > 1 Then
                TXTWEFTNO.Text = TEMPWEFTNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
LINE1:
            TEMPWEFTNO = Val(TXTWEFTNO.Text) + 1
            GETMAX_WEFT_NO()
            Dim MAXNO As Integer = TXTWEFTNO.Text.Trim
            CLEAR()
            If Val(TXTWEFTNO.Text) - 1 >= TEMPWEFTNO Then
                EDIT = True
                WeftRegister_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDWEFT.RowCount = 0 And TEMPWEFTNO < MAXNO Then
                TXTWEFTNO.Text = TEMPWEFTNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tstxtbillno.KeyPress
        numkeypress(e, tstxtbillno, Me)
    End Sub

    Private Sub tstxtbillno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tstxtbillno.Validated
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                TEMPWEFTNO = Val(tstxtbillno.Text)
                If TEMPWEFTNO > 0 Then
                    EDIT = True
                    WeftRegister_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call CMDSAVE_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call CMDDELETE_Click(sender, e)
    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click

    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJWEFT As New WeftRegisterDetails
            OBJWEFT.MdiParent = MDIMain
            OBJWEFT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WEFTDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles WEFTDATE.Validating
        Try
            If WEFTDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(WEFTDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDWEFT.Rows.Add(Val(TXTLENGTH.Text.Trim), Val(TXTROLL.Text.Trim))
            Else
                GRIDWEFT.Item(GLENGTH.Index, TEMPROW).Value = Val(TXTLENGTH.Text.Trim)
                GRIDWEFT.Item(GROLL.Index, TEMPROW).Value = Val(TXTROLL.Text.Trim)
                GRIDDOUBLECLICK = False
                TEMPROW = 0
            End If
            TOTAL()
            TXTLENGTH.Clear()
            TXTROLL.Clear()

            TXTLENGTH.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFT_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDWEFT.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDWEFT.Item(GLENGTH.Index, e.RowIndex).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTLENGTH.Text = Val(GRIDWEFT.Item(GLENGTH.Index, e.RowIndex).Value)
                TXTROLL.Text = Val(GRIDWEFT.Item(GROLL.Index, e.RowIndex).Value)

                TEMPROW = e.RowIndex
                TXTLENGTH.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDWEFT.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDWEFT.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDWEFT.Rows.RemoveAt(GRIDWEFT.CurrentRow.Index)
                TOTAL()

            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTROLL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTROLL.Validating
        Try
            If Val(TXTLENGTH.Text.Trim) > 0 And Val(TXTROLL.Text.Trim) > 0 Then FILLGRID() Else MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCONEWT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCONEWT.Validating
        TOTAL()
    End Sub
End Class