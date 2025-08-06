
Imports BL

Public Class BaleOpen

    Public TEMPBONO As Integer
    Public EDIT As Boolean
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor

            Dim alParaval As New ArrayList

            alParaval.Add(BODATE.Value)
            alParaval.Add(TXTBALENO.Text.Trim)
            alParaval.Add(CMBOURGODOWN.Text.Trim)

            alParaval.Add(TXTBALEQUALITY.Text.Trim)
            alParaval.Add(Val(TXTLOTNO.Text.Trim))
            alParaval.Add(Val(TXTBALETAKA.Text.Trim))
            alParaval.Add(Val(TXTBALEMTRS.Text.Trim))

            alParaval.Add(Val(TXTTOTALTAKA.Text.Trim))
            alParaval.Add(Val(TXTTOTALMTRS.Text.Trim))

            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)



            Dim GRIDSRNO As String = ""
            Dim QUALITY As String = ""
            Dim TAKA As String = ""
            Dim MTRS As String = ""
            Dim NARR As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim TYPE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDGREY.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = row.Cells(GSRNO.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString

                        TAKA = Val(row.Cells(GTAKA.Index).Value)
                        MTRS = Val(row.Cells(GMTRS.Index).Value)
                        NARR = row.Cells(GNARR.Index).Value
                        FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)
                        TYPE = row.Cells(GTYPE.Index).Value
                    Else
                        GRIDSRNO = GRIDSRNO & "|" & row.Cells(GSRNO.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value
                        TAKA = TAKA & "|" & Val(row.Cells(GTAKA.Index).Value)
                        MTRS = MTRS & "|" & Val(row.Cells(GMTRS.Index).Value)
                        NARR = NARR & "|" & row.Cells(GNARR.Index).Value
                        FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)
                        TYPE = TYPE & "|" & row.Cells(GTYPE.Index).Value
                    End If
                End If
            Next

            alParaval.Add(GRIDSRNO)
            alParaval.Add(QUALITY)
            alParaval.Add(TAKA)
            alParaval.Add(MTRS)
            alParaval.Add(NARR)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(TYPE)

            Dim OBJMFG As New ClsBO()
            OBJMFG.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = OBJMFG.save()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPBONO)
                IntResult = OBJMFG.Update()
                MsgBox("Details Updated")
                EDIT = False
            End If

            clear()
            BODATE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
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

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDGREY.Rows.Add(Val(TXTSRNO.Text.Trim), CMBQUALITY.Text.Trim, Format(Val(TXTTAKA.Text.Trim), "0"), Format(Val(TXTMTRS.Text.Trim), "0.00"), TXTNARR.Text.Trim, Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim), TXTTYPE.Text.Trim)
            Else
                GRIDGREY.Item(GSRNO.Index, TEMPROW).Value = TXTSRNO.Text.Trim
                GRIDGREY.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim

                GRIDGREY.Item(GTAKA.Index, TEMPROW).Value = Format(Val(TXTTAKA.Text.Trim), "0")
                GRIDGREY.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")

                GRIDGREY.Item(GNARR.Index, TEMPROW).Value = TXTNARR.Text.Trim
                GRIDGREY.Item(GFROMNO.Index, TEMPROW).Value = TXTFROMNO.Text.Trim
                GRIDGREY.Item(GFROMSRNO.Index, TEMPROW).Value = TXTFROMSRNO.Text.Trim
                GRIDGREY.Item(GTYPE.Index, TEMPROW).Value = TXTTYPE.Text.Trim

                GRIDDOUBLECLICK = False
            End If

            TXTTAKA.Clear()
            TXTMTRS.Clear()
            TXTNARR.Clear()

            getsrno(GRIDGREY)
            TOTAL()
            TXTTAKA.Focus()

            If GRIDGREY.RowCount > 0 Then TXTSRNO.Text = Val(GRIDGREY.RowCount) + 1 Else TXTSRNO.Text = 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub clear()

        TXTBONO.Clear()
        BODATE.Value = Mydate
        TXTBALENO.Clear()
        TXTBALEQUALITY.Clear()
        TXTBALEMTRS.Clear()
        TXTBALETAKA.Clear()
        TXTTOTALMTRS.Clear()
        TXTTOTALTAKA.Clear()
        CMBOURGODOWN.Text = ""

        CMBQUALITY.Text = ""
        TXTTAKA.Clear()
        TXTMTRS.Clear()
        TXTNARR.Clear()
        TXTFROMNO.Clear()
        TXTFROMSRNO.Clear()
        TXTTYPE.Clear()

        txtremarks.Clear()

        lbllocked.Visible = False
        PBlock.Visible = False
        CMDSELECTBALESTOCK.Enabled = True
        GRIDGREY.RowCount = 0
        getmaxno()
        TXTSRNO.Text = 1

    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(BO_NO),0) + 1 ", " BALEOPEN", " AND BO_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTBONO.Text = Val(DTTABLE.Rows(0).Item(0))
    End Sub

    Sub fillcmb()
        Try
            If CMBOURGODOWN.Text = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR = 'True'")
            If CMBQUALITY.Text.Trim = "" Then FILLGREY(CMBQUALITY, EDIT, " AND GREY_TYPE ='FINISHED'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBOURGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBOURGODOWN.Enter
        Try
            If CMBOURGODOWN.Text.Trim <> "" Then fillGODOWN(CMBOURGODOWN, EDIT, " and GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOURGODOWN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBOURGODOWN.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGodown
                OBJGODOWN.FRMSTRING = "GODOWN"
                OBJGODOWN.SEARCH = " AND GODOWN_ISOUR = 'True'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBOURGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOURGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBOURGODOWN.Validating
        Try
            If CMBOURGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBOURGODOWN, e, Me, " and GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then FILLGREY(CMBQUALITY, EDIT, " AND GREY_TYPE ='FINISHED'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGREY As New SelectGreyQuality
                OBJGREY.WHERECLAUSE = " AND GREY_TYPE ='FINISHED'"
                OBJGREY.ShowDialog()
                If OBJGREY.TEMPNAME <> "" Then CMBQUALITY.Text = OBJGREY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBQUALITY, e, Me, " AND GREY_TYPE = 'FINISHED'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True


        If BODATE.Text = "__/__/____" Then
            EP.SetError(BODATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(BODATE.Text) Then
                EP.SetError(BODATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If CMBOURGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBOURGODOWN, "Please Fill Godown Name")
            bln = False
        End If

        If Val(GRIDGREY.RowCount) = 0 Then
            EP.SetError(TXTBALENO, "Enter Item Details in Grid Below")
            bln = False
        End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Modify, entry Locked")
            bln = False
        End If
        Return bln
    End Function

    Private Sub BaleOpen_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
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


    Private Sub BALEOPEN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            fillcmb()
            clear()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJCHECKING As New ClsBO()
                Dim ALPARAVAL As New ArrayList

                Dim dttable As New DataTable

                ALPARAVAL.Add(TEMPBONO)
                ALPARAVAL.Add(YearId)

                OBJCHECKING.alParaval = ALPARAVAL
                dttable = OBJCHECKING.selectBO()
                If dttable.Rows.Count > 0 Then
                    For Each DR As DataRow In dttable.Rows
                        TXTBONO.Text = TEMPBONO
                        BODATE.Value = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")
                        TXTBALENO.Text = Convert.ToString(DR("BALENO").ToString)
                        TXTLOTNO.Text = Convert.ToString(DR("LOTNO").ToString)
                        TXTBALETAKA.Text = Convert.ToString(Val(DR("BALETAKA").ToString))
                        TXTBALEMTRS.Text = Convert.ToString(Val(DR("BALEMTRS").ToString))

                        CMBOURGODOWN.Text = Convert.ToString(DR("OURGODOWN").ToString)
                        TXTBALEQUALITY.Text = Convert.ToString(DR("BALEQUALITY").ToString)

                        txtremarks.Text = DR("REMARKS")
                        GRIDGREY.Rows.Add(Val(DR("GRIDSRNO")), DR("QUALITY").ToString, Val(DR("TAKA")), Format(Val(DR("MTRS")), "0.00"), DR("NARR"), Val(DR("FROMNO")), Val(DR("FROMSRNO")), DR("TYPE"))
                    Next
                    TOTAL()
                End If

            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try

            tempbono = Val(tstxtbillno.Text)
            If tempbono > 0 Then
                edit = True
                BALEOPEN_Load(sender, e)
            Else
                clear()
                edit = False
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor

            tempbono = Val(TXTBONO.Text) - 1
            If tempbono > 0 Then
                edit = True
                BALEOPEN_Load(sender, e)
            Else
                clear()
                edit = False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            tempbono = Val(TXTBONO.Text) + 1
            getmaxno()
            clear()
            If Val(TXTBONO.Text) - 1 >= tempbono Then
                edit = True
                BALEOPEN_Load(sender, e)
            Else
                clear()
                edit = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try

            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    MsgBox("Form Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Delete Bale Open?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPBONO)
                    alParaval.Add(YearId)

                    Dim OBJBO As New ClsBO
                    Dim IntResult As Integer = OBJBO.Delete()
                    MsgBox("Bale Open Deleted")
                    clear()
                    EDIT = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try
            TXTTOTALMTRS.Text = 0.0
            TXTTOTALTAKA.Text = 0.0
            If GRIDGREY.RowCount > 0 Then
                For Each ROW As DataGridViewRow In GRIDGREY.Rows
                    TXTTOTALMTRS.Text = Format(Val(TXTTOTALMTRS.Text.Trim) + Val(ROW.Cells(GMTRS.Index).Value), "0.00")
                    TXTTOTALTAKA.Text = Format(Val(TXTTOTALTAKA.Text.Trim) + Val(ROW.Cells(GTAKA.Index).Value), "0")
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJPSDETAILS As New BaleOpenDetails
            OBJPSDETAILS.MdiParent = MDIMain
            OBJPSDETAILS.Show()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        Try
            clear()
            EDIT = False
            BODATE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTBALESTOCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTBALESTOCK.Click
        Try

            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If CMBOURGODOWN.Text.Trim = "" Then
                MsgBox("Select godown First", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim OBJSELECTSTOCK As New SelectBaleStock
            OBJSELECTSTOCK.TEMPGODOWNNAME = CMBOURGODOWN.Text.Trim
            Dim DTBEAMSTOCK As DataTable = OBJSELECTSTOCK.DT
            OBJSELECTSTOCK.ShowDialog()
            If DTBEAMSTOCK.Rows.Count > 0 Then

                TXTBALENO.Text = DTBEAMSTOCK.Rows(0).Item("BALENO")
                TXTBALEQUALITY.Text = DTBEAMSTOCK.Rows(0).Item("GREYQUALITY")
                TXTLOTNO.Text = Val(DTBEAMSTOCK.Rows(0).Item("LOTNO"))
                TXTBALETAKA.Text = Val(DTBEAMSTOCK.Rows(0).Item("PCS"))
                TXTBALEMTRS.Text = Val(DTBEAMSTOCK.Rows(0).Item("MTRS"))
                TXTFROMNO.Text = Val(DTBEAMSTOCK.Rows(0).Item("FROMNO"))
                TXTFROMSRNO.Text = Val(DTBEAMSTOCK.Rows(0).Item("FROMSRNO"))
                TXTTYPE.Text = DTBEAMSTOCK.Rows(0).Item("TYPE")

                CMDSELECTBALESTOCK.Enabled = False
                CMBQUALITY.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNARR_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNARR.Validating
        If CMBQUALITY.Text.Trim <> "" And Val(TXTTAKA.Text) > 0 And Val(TXTMTRS.Text) > 0 Then FILLGRID() Else MsgBox("Please Enter proper Details")
    End Sub

    Private Sub GRIDGREY_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDGREY.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDGREY.Item(GSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = Val(GRIDGREY.Item(GSRNO.Index, e.RowIndex).Value)
                CMBQUALITY.Text = GRIDGREY.Item(GQUALITY.Index, e.RowIndex).Value
                TXTTAKA.Text = Val(GRIDGREY.Item(GTAKA.Index, e.RowIndex).Value)
                TXTMTRS.Text = Val(GRIDGREY.Item(GMTRS.Index, e.RowIndex).Value)
                
                TXTNARR.Text = GRIDGREY.Item(GNARR.Index, e.RowIndex).Value
                TXTFROMNO.Text = Val(GRIDGREY.Item(GFROMNO.Index, e.RowIndex).Value)
                TXTFROMSRNO.Text = Val(GRIDGREY.Item(GFROMSRNO.Index, e.RowIndex).Value)
                TXTTYPE.Text = GRIDGREY.Item(GTYPE.Index, e.RowIndex).Value

                TEMPROW = e.RowIndex
                CMBQUALITY.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTTAKA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTAKA.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub BaleOpen_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ALLOWMFG = False Then Exit Sub
    End Sub
End Class