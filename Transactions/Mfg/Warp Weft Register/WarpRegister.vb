
Imports BL

Public Class WarpRegister

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean
    Public TEMPWARPNO As Integer

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        CMDSELECTROLLRECD.Focus()
    End Sub

    Sub CALC()
        Try
            TXTCONSUMED.Text = Format((((Val(TXTTOTALENDS.Text.Trim) * Val(TXTTL.Text.Trim) / 1850) / Val(TXTCALCCOUNT.Text.Trim))) * Val(TXTCUT.Text.Trim), "0.000")
            TXTLONGATION.Text = Format((Val(TXTCONSUMED.Text.Trim) + Val(TXTWTRETURNED.Text.Trim)) - Val(TXTWTGIVEN.Text.Trim), "0.000")
            TXTCOUNT.Text = Format(((Val(TXTTOTALENDS.Text.Trim) * Val(TXTTL.Text.Trim)) / 1850) / ((Val(TXTWTGIVEN.Text.Trim) - Val(TXTWTRETURNED.Text.Trim)) / Val(TXTCUT.Text.Trim)), "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()


        TXTROLLSRECDNO.Clear()
        TXTPROGRAMSRNO.Clear()
        WARPDATE.Text = Mydate
        TXTWARPINGNO.Clear()
        TXTMILLNAME.Clear()
        TXTSIZERNAME.Clear()

        TXTWTGIVEN.Clear()
        TXTWTRETURNED.Clear()
        TXTCALCCOUNT.Clear()
        TXTCONSUMED.Clear()
        TXTLONGATION.Clear()
        TXTCOUNT.Clear()

        TXTTOTALENDS.Clear()
        TXTCUT.Clear()
        TXTLENGTH.Clear()
        TXTTL.Clear()

        TXTREMARKS.Clear()

        EP.Clear()
        CMDSELECTROLLRECD.Enabled = True

        tstxtbillno.Clear()

        GETMAX_WARP_NO()

    End Sub

    Sub GETMAX_WARP_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(WARP_NO),0)+1", "WARPREGISTER", "AND WARP_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTWARPNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub TXTWTGIVEN_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWTRETURNED.KeyPress, TXTWTGIVEN.KeyPress, TXTCALCCOUNT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTWARPINGNO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWARPINGNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub WarpRegister_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub WarpRegister_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            CLEAR()

            If EDIT = True Then
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dttable As New DataTable
                Dim OBJWARP As New ClsWarpRegister

                OBJWARP.alParaval.Add(TEMPWARPNO)
                OBJWARP.alParaval.Add(YearId)
                dttable = OBJWARP.SELECTWARP()

                If dttable.Rows.Count > 0 Then

                    TXTWARPNO.Text = TEMPWARPNO
                    WARPDATE.Text = dttable.Rows(0).Item("DATE")
                    TXTROLLSRECDNO.Text = Val(dttable.Rows(0).Item("ROLLRECDNO"))
                    TXTPROGRAMSRNO.Text = Val(dttable.Rows(0).Item("PROGRAMSRNO"))
                    TXTWARPINGNO.Text = dttable.Rows(0).Item("WARPINGNO")
                    TXTMILLNAME.Text = dttable.Rows(0).Item("MILLNAME").ToString
                    TXTSIZERNAME.Text = dttable.Rows(0).Item("SIZERNAME").ToString

                    TXTWTGIVEN.Text = Val(dttable.Rows(0).Item("GIVEN"))
                    TXTWTRETURNED.Text = Val(dttable.Rows(0).Item("RETURNED"))
                    TXTCALCCOUNT.Text = Val(dttable.Rows(0).Item("CALCCOUNT"))

                    TXTCONSUMED.Text = Val(dttable.Rows(0).Item("CONSUMED"))
                    TXTLONGATION.Text = Val(dttable.Rows(0).Item("LONGATION"))
                    TXTCOUNT.Text = Val(dttable.Rows(0).Item("COUNT"))

                    TXTTOTALENDS.Text = Val(dttable.Rows(0).Item("TOTALENDS"))
                    TXTCUT.Text = Val(dttable.Rows(0).Item("CUT"))
                    TXTLENGTH.Text = Val(dttable.Rows(0).Item("LENGTH"))
                    TXTTL.Text = Val(dttable.Rows(0).Item("TAPLINE"))

                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString

                    CMDSELECTROLLRECD.Enabled = False

                    CALC()
                    TXTWTGIVEN.Focus()
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

            alParaval.Add(Format(Convert.ToDateTime(WARPDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(Val(TXTROLLSRECDNO.Text.Trim))
            alParaval.Add(Val(TXTPROGRAMSRNO.Text.Trim))
            alParaval.Add(Val(TXTWARPINGNO.Text.Trim))

            alParaval.Add(TXTMILLNAME.Text.Trim)
            alParaval.Add(TXTSIZERNAME.Text.Trim)
            alParaval.Add(Val(TXTWTGIVEN.Text.Trim))
            alParaval.Add(Val(TXTWTRETURNED.Text.Trim))
            alParaval.Add(Val(TXTCALCCOUNT.Text.Trim))

            alParaval.Add(Val(TXTCONSUMED.Text.Trim))
            alParaval.Add(Val(TXTLONGATION.Text.Trim))
            alParaval.Add(Val(TXTCOUNT.Text.Trim))

            alParaval.Add(Val(TXTTOTALENDS.Text.Trim))
            alParaval.Add(Val(TXTCUT.Text.Trim))
            alParaval.Add(Val(TXTLENGTH.Text.Trim))
            alParaval.Add(Val(TXTTL.Text.Trim))

            alParaval.Add(TXTREMARKS.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim OBJWARP As New ClsWarpRegister
            OBJWARP.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJWARP.save()
                TEMPWARPNO = DT.Rows(0).Item(0)
                TXTWARPNO.Text = TEMPWARPNO
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPWARPNO)
                IntResult = OBJWARP.Update()
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


        If WARPDATE.Text = "__/__/____" Then
            EP.SetError(WARPDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(WARPDATE.Text) Then
                EP.SetError(WARPDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If Val(TXTWTGIVEN.Text.Trim) = 0 Then
            EP.SetError(TXTWTGIVEN, "Please Enter Wt")
            bln = False
        End If

        If Val(TXTWTRETURNED.Text.Trim) = 0 Then
            EP.SetError(TXTWTRETURNED, "Please Enter Wt")
            bln = False
        End If

        If Val(TXTCALCCOUNT.Text.Trim) = 0 Then
            EP.SetError(TXTCALCCOUNT, "Please Enter Count")
            bln = False
        End If

        If Val(TXTROLLSRECDNO.Text.Trim) = 0 Then
            EP.SetError(TXTROLLSRECDNO, "Please Select Roll Recd Entry")
            bln = False
        End If

        Return bln
    End Function

    Private Sub WARPDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles WARPDATE.GotFocus
        WARPDATE.SelectAll()
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
LINE1:
            TEMPWARPNO = Val(TXTWARPNO.Text) - 1
Line2:
            If TEMPWARPNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" WARP_NO ", "", "  WARPREGISTER", " AND WARP_NO = '" & TEMPWARPNO & "' AND WARPREGISTER.WARP_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    WarpRegister_Load(sender, e)
                Else
                    TEMPWARPNO = Val(TEMPWARPNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If TXTMILLNAME.Text.Trim = "" And TEMPWARPNO > 1 Then
                TXTWARPNO.Text = TEMPWARPNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
LINE1:
            TEMPWARPNO = Val(TXTWARPNO.Text) + 1
            GETMAX_WARP_NO()
            Dim MAXNO As Integer = TXTWARPNO.Text.Trim
            CLEAR()
            If Val(TXTWARPNO.Text) - 1 >= TEMPWARPNO Then
                EDIT = True
                WarpRegister_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If TXTMILLNAME.Text.Trim = "" And TEMPWARPNO < MAXNO Then
                TXTWARPNO.Text = TEMPWARPNO
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
                TEMPWARPNO = Val(tstxtbillno.Text)
                If TEMPWARPNO > 0 Then
                    EDIT = True
                    WarpRegister_Load(sender, e)
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
            Dim OBJWARP As New WarpRegisterDetails
            OBJWARP.MdiParent = MDIMain
            OBJWARP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTROLLRECD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTROLLRECD.Click
        Try
            Dim OBJWARP As New SelectRollforWarp
            Dim DT As DataTable = OBJWARP.DT
            OBJWARP.ShowDialog()
            If DT.Rows.Count > 0 Then
                TXTROLLSRECDNO.Text = Val(DT.Rows(0).Item("ROLLRECDNO"))
                TXTPROGRAMSRNO.Text = Val(DT.Rows(0).Item("PROGRAMSRNO"))
                WARPDATE.Text = DT.Rows(0).Item("DATE")
                TXTMILLNAME.Text = DT.Rows(0).Item("MILLNAME")
                TXTSIZERNAME.Text = DT.Rows(0).Item("NAME")
                TXTTOTALENDS.Text = Val(DT.Rows(0).Item("TOTALENDS"))
                TXTLENGTH.Text = Val(DT.Rows(0).Item("LENGTH"))
                TXTCUT.Text = DT.Rows(0).Item("CUT")
                TXTTL.Text = Val(DT.Rows(0).Item("TAPLINE"))
                TXTWARPINGNO.Text = Val(DT.Rows(0).Item("WARPINGNO"))
                TXTWTGIVEN.Text = Val(DT.Rows(0).Item("GIVENWT"))
                TXTWTRETURNED.Text = Val(DT.Rows(0).Item("RETURNWT"))
                CMDSELECTROLLRECD.Enabled = False
                TXTWTGIVEN.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WARPDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles WARPDATE.Validating
        Try
            If WARPDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(WARPDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWTGIVEN_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTWTRETURNED.Validated, TXTWTGIVEN.Validated, TXTCALCCOUNT.Validated
        CALC()
    End Sub
End Class