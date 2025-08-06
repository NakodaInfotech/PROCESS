
Imports BL

Public Class BeamReturn

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPBEAMRETURNNO As Integer
    Dim TEMPMSG As Integer

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Sub TOTAL()
        Try
            LBLTOTALWT.Text = 0.0
            LBLTOTALCUT.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDBEAM.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALCUT.Text = Format(Val(LBLTOTALCUT.Text) + Val(ROW.Cells(GCUT.Index).EditedFormattedValue), "0.000")
                    ROW.Cells(GWT.Index).Value = Format(Val(ROW.Cells(GCUT.Index).EditedFormattedValue) * Val(ROW.Cells(GWTCUT.Index).Value), "0.00")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.000")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()

        CMDSELECTBEAM.Enabled = True
        TXTBEAMRETURNNO.Clear()
        DTBEAMRETURNDATE.Text = Mydate
        CMBSIZER.Text = ""
        CMBNAME.Text = ""
        CMBGODOWN.Text = GETDEFAULTGODOWN()
        TXTCHALLANNO.Clear()
        DTCHALLANDATE.Text = Mydate

        TXTREMARKS.Clear()
        TXTSRNO.Clear()
        TXTBEAMNO.Clear()
        CMBBEAMNAME.Text = ""
        TXTENDS.Clear()
        TXTTAPLINE.Clear()
        TXTCUT.Clear()
        TXTWT.Clear()
        TXTWTCUT.Clear()
        TXTNARR.Clear()

        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        TXTREMARKS.Clear()

        GRIDBEAM.RowCount = 0

        GETMAX_BEAMRETURN_NO()

        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False


        TabControl1.SelectedIndex = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0

        LBLTOTALCUT.Text = 0.0
        LBLTOTALWT.Text = 0.0

        TXTSRNO.Text = 1
        TXTUPLOADSRNO.Text = 1
    End Sub

    Sub GETMAX_BEAMRETURN_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(BEAMRETURN_NO),0)+1", "BEAMRETURN", "AND BEAMRETURN_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTBEAMRETURNNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub BeamReturn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1) Then       'for CLEAR
                TabControl1.SelectedIndex = (0)
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2) Then       'for CLEAR
                TabControl1.SelectedIndex = (1)
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
        If CMBSIZER.Text.Trim = "" Then fillname(CMBSIZER, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'")
        If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
        If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT, " and GODOWN_ISOUR = 'True'")
        If CMBBEAMNAME.Text = "" Then fillBEAM(CMBBEAMNAME, EDIT)
        If cmbtrans.Text = "" Then fillname(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'TRANSPORT' ")
    End Sub

    Private Sub BeamReturn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            CLEAR()
            CMBGODOWN.Text = GETDEFAULTGODOWN()

            If EDIT = True Then
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dttable As New DataTable
                Dim OBJBEAMRETURN As New ClsBeamReturn

                OBJBEAMRETURN.alParaval.Add(TEMPBEAMRETURNNO)
                OBJBEAMRETURN.alParaval.Add(YearId)
                dttable = OBJBEAMRETURN.selectBEAMRETURN()

                If dttable.Rows.Count > 0 Then
                    CMBNAME.Focus()

                    TXTBEAMRETURNNO.Text = TEMPBEAMRETURNNO
                    DTBEAMRETURNDATE.Text = dttable.Rows(0).Item("DATE")
                    CMBSIZER.Text = dttable.Rows(0).Item("SIZER").ToString
                    CMBNAME.Text = dttable.Rows(0).Item("NAME").ToString
                    CMBGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                    TXTCHALLANNO.Text = dttable.Rows(0).Item("CHALLANNO").ToString
                    DTCHALLANDATE.Text = dttable.Rows(0).Item("CHALLANDATE")
                    cmbtrans.Text = dttable.Rows(0).Item("TRANSPORT").ToString
                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString

                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDBEAM.Rows.Add(Val(ROW("SRNO")), ROW("BEAMNAME"), ROW("BEAMNO"), Val(ROW("ENDS")), Val(ROW("TAPLINE")), Format(Val(ROW("CUT")), "0.00"), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("WTCUT")), "0.000"), ROW("NARR"), ROW("FROMNO"), ROW("FROMSRNO"), ROW("GRIDTYPE"), ROW("DONE"))

                        If Convert.ToBoolean(ROW("DONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If
                    Next


                    'UPLOAD(GRID)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" BEAMRETURN_UPLOAD.BEAMRETURN_SRNO AS GRIDSRNO, BEAMRETURN_UPLOAD.BEAMRETURN_REMARKS AS REMARKS, BEAMRETURN_UPLOAD.BEAMRETURN_NAME AS NAME, BEAMRETURN_UPLOAD.BEAMRETURN_PHOTO AS IMGPATH ", "", " BEAMRETURN_UPLOAD ", " AND BEAMRETURN_UPLOAD.BEAMRETURN_NO = " & TEMPBEAMRETURNNO & " AND BEAMRETURN_YEARID = " & YearId & " ORDER BY BEAMRETURN_UPLOAD.BEAMRETURN_SRNO")
                    If DT.Rows.Count > 0 Then
                        For Each DTR As DataRow In DT.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If
                    TOTAL()
                    CMDSELECTBEAM.Enabled = False

                End If
            Else
                CLEAR()
                EDIT = False
                DTBEAMRETURNDATE.Focus()
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

            alParaval.Add(Format(Convert.ToDateTime(DTBEAMRETURNDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBSIZER.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBGODOWN.Text.Trim)
            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(DTCHALLANDATE.Text.Trim)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(LBLTOTALCUT.Text.Trim)
            alParaval.Add(LBLTOTALWT.Text.Trim)
            alParaval.Add(TXTREMARKS.Text.Trim)


            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim SRNO As String = ""
            Dim BEAMNAME As String = ""
            Dim BEAMNO As String = ""
            Dim ENDS As String = ""
            Dim TL As String = ""
            Dim CUT As String = ""
            Dim WT As String = ""
            Dim WTCUT As String = ""
            Dim NARR As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim GRIDTYPE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDBEAM.Rows
                If row.Cells(GSRNO.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = row.Cells(GSRNO.Index).Value
                        BEAMNAME = row.Cells(GBEAMNAME.Index).Value.ToString
                        BEAMNO = row.Cells(GBEAMNO.Index).Value.ToString
                        ENDS = Val(row.Cells(GENDS.Index).Value)
                        TL = Val(row.Cells(GTAPLINE.Index).Value)
                        CUT = Val(row.Cells(GCUT.Index).Value)
                        WT = Val(row.Cells(GWT.Index).Value)
                        WTCUT = Val(row.Cells(GWTCUT.Index).Value)
                        If row.Cells(GNARR.Index).Value <> Nothing Then NARR = row.Cells(GNARR.Index).Value.ToString Else NARR = ""
                        FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)
                        GRIDTYPE = row.Cells(GTYPE.Index).Value

                    Else

                        SRNO = SRNO & "|" & row.Cells(GSRNO.Index).Value
                        BEAMNAME = BEAMNAME & "|" & row.Cells(GBEAMNAME.Index).Value.ToString
                        BEAMNO = BEAMNO & "|" & row.Cells(GBEAMNO.Index).Value.ToString
                        ENDS = ENDS & "|" & Val(row.Cells(GENDS.Index).Value)
                        TL = TL & "|" & Val(row.Cells(GTAPLINE.Index).Value)
                        CUT = CUT & "|" & Val(row.Cells(GCUT.Index).Value)
                        WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
                        WTCUT = WTCUT & "|" & Val(row.Cells(GWTCUT.Index).Value)
                        If row.Cells(GNARR.Index).Value <> Nothing Then NARR = NARR & "|" & row.Cells(GNARR.Index).Value.ToString Else NARR = NARR & "|" & ""
                        FROMNO = FROMNO & "|" & row.Cells(GFROMNO.Index).Value
                        FROMSRNO = FROMSRNO & "|" & row.Cells(GFROMSRNO.Index).Value
                        GRIDTYPE = GRIDTYPE & "|" & row.Cells(GTYPE.Index).Value

                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(BEAMNAME)
            alParaval.Add(BEAMNO)
            alParaval.Add(ENDS)
            alParaval.Add(TL)
            alParaval.Add(CUT)
            alParaval.Add(WT)
            alParaval.Add(WTCUT)
            alParaval.Add(NARR)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(GRIDTYPE)


            Dim OBJBEAMRET As New ClsBeamReturn
            OBJBEAMRET.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJBEAMRET.save()
                TEMPBEAMRETURNNO = DT.Rows(0).Item(0)
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPBEAMRETURNNO)
                IntResult = OBJBEAMRET.Update()
                EDIT = False
                MsgBox("Details Updated")

            End If

            If gridupload.RowCount > 0 Then SAVEUPLOAD()
            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTBEAMRETURNDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJBEAMRET As New ClsBeamReturn


            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPBEAMRETURNNO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJBEAMRET.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJBEAMRET.SAVEUPLOAD()
                End If
            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLUPLOAD()

        If GRIDUPLOADDOUBLECLICK = False Then
            gridupload.Rows.Add(Val(TXTUPLOADSRNO.Text.Trim), txtuploadremarks.Text.Trim, txtuploadname.Text.Trim, PBSOFTCOPY.Image)
            getsrno(gridupload)
        ElseIf GRIDUPLOADDOUBLECLICK = True Then

            gridupload.Item(GUSRNO.Index, TEMPUPLOADROW).Value = TXTUPLOADSRNO.Text.Trim
            gridupload.Item(GUREMARKS.Index, TEMPUPLOADROW).Value = txtuploadremarks.Text.Trim
            gridupload.Item(GUNAME.Index, TEMPUPLOADROW).Value = txtuploadname.Text.Trim
            gridupload.Item(GUIMGPATH.Index, TEMPUPLOADROW).Value = PBSOFTCOPY.Image

            GRIDUPLOADDOUBLECLICK = False

        End If
        gridupload.FirstDisplayedScrollingRowIndex = gridupload.RowCount - 1

        TXTUPLOADSRNO.Clear()
        txtuploadremarks.Clear()
        txtuploadname.Clear()
        PBSOFTCOPY.Image = Nothing
        TXTIMGPATH.Clear()

        txtuploadremarks.Focus()

    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True


        If DTBEAMRETURNDATE.Text = "__/__/____" Then
            EP.SetError(DTBEAMRETURNDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTBEAMRETURNDATE.Text) Then
                EP.SetError(DTBEAMRETURNDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If DTCHALLANDATE.Text = "__/__/____" Then
            EP.SetError(DTCHALLANDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTCHALLANDATE.Text) Then
                EP.SetError(DTCHALLANDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If DTBEAMRETURNDATE.Text.Trim <> "__/__/____" And DTCHALLANDATE.Text.Trim <> "__/__/____" Then
            If Convert.ToDateTime(DTBEAMRETURNDATE.Text).Date > Convert.ToDateTime(DTCHALLANDATE.Text).Date Then
                EP.SetError(DTCHALLANDATE, " Please Enter Proper Challan Date")
                bln = False
            End If
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Fill Weaver Name")
            bln = False
        End If

        If CMBGODOWN.Text.Trim.Length = 0 And CMBSIZER.Text.Trim.Length = 0 Then
            EP.SetError(CMBGODOWN, " Please Select Godown Or Sizer")
            bln = False
        End If

        If cmbtrans.Text.Trim.Length = 0 Then
            EP.SetError(cmbtrans, "Please Select Transport Name")
            bln = False
        End If

        If GRIDBEAM.RowCount = 0 Then
            EP.SetError(TXTNARR, "Enter Proper Details")
            bln = False
        End If

        For Each row As DataGridViewRow In GRIDBEAM.Rows
            If Val(row.Cells(GWT.Index).Value) = 0 Then
                EP.SetError(TXTNARR, "Wt Cannot be 0 or Less")
                bln = False
            End If

            If Val(row.Cells(GCUT.Index).Value) = 0 Then
                EP.SetError(TXTNARR, "Cut Cannot be 0 or Less")
                bln = False
            End If

        Next


        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Modify, entry Locked")
            bln = False
        End If

        Return bln
    End Function

    Private Sub DTBEAMRETURNDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTBEAMRETURNDATE.GotFocus
        DTBEAMRETURNDATE.SelectAll()
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

    Private Sub CMBSIZER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBSIZER.Enter
        Try
            If CMBSIZER.Text.Trim = "" Then fillname(CMBSIZER, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBSIZER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBSIZER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSIZER.Validating
        Try
            If CMBSIZER.Text.Trim <> "" Then namevalidate(CMBSIZER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "SIZER")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WEAVER")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim <> "" Then fillGODOWN(CMBGODOWN, EDIT, " and GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGODOWN.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGodown
                OBJGODOWN.FRMSTRING = "GODOWN"
                OBJGODOWN.SEARCH = " AND GODOWN_ISOUR = 'True'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me, " and GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDBEAM.RowCount = 0
LINE1:
            TEMPBEAMRETURNNO = Val(TXTBEAMRETURNNO.Text) - 1
Line2:
            If TEMPBEAMRETURNNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" BEAMRETURN_NO ", "", "  BEAMRETURN", " AND BEAMRETURN_NO = '" & TEMPBEAMRETURNNO & "' AND BEAMRETURN.BEAMRETURN_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    BeamReturn_Load(sender, e)
                Else
                    TEMPBEAMRETURNNO = Val(TEMPBEAMRETURNNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDBEAM.RowCount = 0 And TEMPBEAMRETURNNO > 1 Then
                TXTBEAMRETURNNO.Text = TEMPBEAMRETURNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDBEAM.RowCount = 0
LINE1:
            TEMPBEAMRETURNNO = Val(TXTBEAMRETURNNO.Text) + 1
            GETMAX_BEAMRETURN_NO()
            Dim MAXNO As Integer = TXTBEAMRETURNNO.Text.Trim
            CLEAR()
            If Val(TXTBEAMRETURNNO.Text) - 1 >= TEMPBEAMRETURNNO Then
                EDIT = True
                BeamReturn_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDBEAM.RowCount = 0 And TEMPBEAMRETURNNO < MAXNO Then
                TXTBEAMRETURNNO.Text = TEMPBEAMRETURNNO
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
                GRIDBEAM.RowCount = 0
                TEMPBEAMRETURNNO = Val(tstxtbillno.Text)
                If TEMPBEAMRETURNNO > 0 Then
                    EDIT = True
                    BeamReturn_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridupload_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And gridupload.Item(GUSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDUPLOADDOUBLECLICK = True
                TXTUPLOADSRNO.Text = gridupload.Item(GUSRNO.Index, e.RowIndex).Value
                txtuploadremarks.Text = gridupload.Item(GUREMARKS.Index, e.RowIndex).Value
                txtuploadname.Text = gridupload.Item(GUNAME.Index, e.RowIndex).Value
                PBSOFTCOPY.Image = gridupload.Item(GUIMGPATH.Index, e.RowIndex).Value

                TEMPUPLOADROW = e.RowIndex
                txtuploadremarks.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridupload.KeyDown
        Try
            If e.KeyCode = Keys.Delete And gridupload.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                If GRIDUPLOADDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                gridupload.Rows.RemoveAt(gridupload.CurrentRow.Index)
                getsrno(gridupload)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtuploadname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtuploadname.Validating
        Try
            If txtuploadremarks.Text.Trim <> "" And txtuploadname.Text.Trim <> "" And PBSOFTCOPY.ImageLocation <> "" Then
                FILLUPLOAD()
            Else
                MsgBox("Enter Proper Details")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTUPLOADSRNO_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTUPLOADSRNO.GotFocus
        If GRIDUPLOADDOUBLECLICK = False Then
            If gridupload.RowCount > 0 Then
                TXTUPLOADSRNO.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1
            Else
                TXTUPLOADSRNO.Text = 1
            End If
        End If
    End Sub

    Private Sub CMDUPLOAD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDUPLOAD.Click
        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpg;*.png"
        OpenFileDialog1.ShowDialog()
        TXTIMGPATH.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If TXTIMGPATH.Text.Trim.Length <> 0 Then PBSOFTCOPY.ImageLocation = TXTIMGPATH.Text.Trim
    End Sub

    Private Sub CMDREMOVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREMOVE.Click
        Try
            PBSOFTCOPY.Image = Nothing
            TXTIMGPATH.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDVIEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDVIEW.Click
        Try
            If gridupload.SelectedRows.Count > 0 Then
                Dim objVIEW As New ViewImage
                objVIEW.pbsoftcopy.Image = PBSOFTCOPY.Image
                objVIEW.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If e.RowIndex >= 0 Then PBSOFTCOPY.Image = gridupload.Rows(e.RowIndex).Cells(GUIMGPATH.Index).Value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBEAM_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDBEAM.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDBEAM.Item(GSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDBEAM.Item(GSRNO.Index, e.RowIndex).Value
                TXTBEAMNO.Text = GRIDBEAM.Item(GBEAMNO.Index, e.RowIndex).Value
                CMBBEAMNAME.Text = GRIDBEAM.Item(GBEAMNAME.Index, e.RowIndex).Value
                TXTENDS.Text = GRIDBEAM.Item(GENDS.Index, e.RowIndex).Value
                TXTTAPLINE.Text = GRIDBEAM.Item(GTAPLINE.Index, e.RowIndex).Value
                TXTCUT.Text = GRIDBEAM.Item(GCUT.Index, e.RowIndex).Value
                TXTWT.Text = GRIDBEAM.Item(GWT.Index, e.RowIndex).Value
                TXTWTCUT.Text = GRIDBEAM.Item(GWTCUT.Index, e.RowIndex).Value
                TXTNARR.Text = GRIDBEAM.Item(GNARR.Index, e.RowIndex).Value

                TEMPROW = e.RowIndex
                TXTBEAMNO.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBEAM_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDBEAM.CellValidating
        Try
            Dim colNum As Integer = GRIDBEAM.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GCUT.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDBEAM.CurrentCell.Value = Nothing Then GRIDBEAM.CurrentCell.Value = "0.000"
                        GRIDBEAM.CurrentCell.Value = Convert.ToDecimal(GRIDBEAM.Item(colNum, e.RowIndex).Value)
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

    Private Sub GRIDBEAM_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDBEAM.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDBEAM.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDBEAM.Rows.RemoveAt(GRIDBEAM.CurrentRow.Index)
                getsrno(GRIDBEAM)
                TXTSRNO.Text = GRIDBEAM.RowCount + 1
                TOTAL()

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

        Dim IntResult As Integer
        Try

            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If lbllocked.Visible = True Then
                    MsgBox("Unable to Delete, Entry Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                TEMPMSG = MsgBox("Delete Entry?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTBEAMRETURNNO.Text.Trim)
                    alParaval.Add(YearId)

                    Dim OBJBEAMDEL As New ClsBeamReturn
                    OBJBEAMDEL.alParaval = alParaval
                    IntResult = OBJBEAMDEL.Delete()
                    MsgBox("Entry Deleted")
                    CLEAR()
                    EDIT = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDBEAM.Rows.Add(Val(TXTSRNO.Text.Trim), TXTBEAMNO.Text.Trim, CMBBEAMNAME.Text.Trim, Val(TXTENDS.Text.Trim), Val(TXTTAPLINE.Text.Trim), Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTWT.Text.Trim), "0.000"), Format(Val(TXTWTCUT.Text.Trim), "0.000"), TXTNARR.Text.Trim)
            Else
                GRIDBEAM.Item(GSRNO.Index, TEMPROW).Value = TXTSRNO.Text.Trim
                GRIDBEAM.Item(GBEAMNO.Index, TEMPROW).Value = TXTBEAMNO.Text.Trim
                GRIDBEAM.Item(GBEAMNAME.Index, TEMPROW).Value = CMBBEAMNAME.Text.Trim
                GRIDBEAM.Item(GENDS.Index, TEMPROW).Value = Val(TXTENDS.Text.Trim)
                GRIDBEAM.Item(GTAPLINE.Index, TEMPROW).Value = Format(Val(TXTTAPLINE.Text.Trim))
                GRIDBEAM.Item(GCUT.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
                GRIDBEAM.Item(GWT.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.000")
                GRIDBEAM.Item(GWTCUT.Index, TEMPROW).Value = Format(Val(TXTWTCUT.Text.Trim), "0.000")
                GRIDBEAM.Item(GNARR.Index, TEMPROW).Value = TXTNARR.Text.Trim

                GRIDDOUBLECLICK = False
            End If
            TXTBEAMNO.Clear()
            CMBBEAMNAME.Text = ""
            TXTENDS.Clear()
            TXTTAPLINE.Clear()
            TXTCUT.Clear()
            TXTWT.Clear()
            TXTWTCUT.Clear()
            TXTNARR.Clear()
            getsrno(GRIDBEAM)
            TOTAL()
            TXTBEAMNO.Focus()
            If GRIDBEAM.RowCount > 0 Then TXTSRNO.Text = Val(GRIDBEAM.RowCount) + 1 Else TXTSRNO.Text = 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub calc()
        TXTWTCUT.Text = Format(Val(TXTWT.Text) / Val(TXTCUT.Text.Trim), "0.000")
    End Sub

    Private Sub TXTNARR_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNARR.Validating
        If TXTBEAMNO.Text.Trim <> "" And CMBBEAMNAME.Text.Trim <> "" And Val(TXTCUT.Text.Trim) > 0 And Val(TXTWT.Text.Trim) > 0 Then FILLGRID() Else MsgBox("Please Enter proper details")
    End Sub

    Private Sub TXTCUT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCUT.Validating, TXTWT.Validating
        calc()
    End Sub

    Private Sub CMBBEAMNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBEAMNAME.Enter
        Try
            If CMBBEAMNAME.Text.Trim = "" Then fillBEAM(CMBBEAMNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBBEAMNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJBEAM As New SelectBeam
                OBJBEAM.ShowDialog()
                If OBJBEAM.TEMPNAME <> "" Then CMBBEAMNAME.Text = OBJBEAM.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBBEAMNAME.Validated
        Try
            If CMBBEAMNAME.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(BEAM_ENDS, 0) AS ENDS, ISNULL(BEAM_TAPLINE, 0) AS TAPLINE", "", "BEAMMASTER", "AND BEAMMASTER.BEAM_NAME = '" & CMBBEAMNAME.Text.Trim & "' AND BEAM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TXTENDS.Text = DT.Rows(0).Item("ENDS")
                    TXTTAPLINE.Text = DT.Rows(0).Item("TAPLINE")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBEAMNAME.Validating
        Try
            If CMBBEAMNAME.Text.Trim <> "" Then BEAMVALIDATE(CMBBEAMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJBEAM As New BeamReturnDetails
            OBJBEAM.MdiParent = MDIMain
            OBJBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCUT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCUT.KeyPress, TXTCHALLANNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress
        numdot3(e, TXTWT, Me)
    End Sub

    Private Sub DTBEAMRETURNDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTBEAMRETURNDATE.Validating
        Try
            If DTBEAMRETURNDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTBEAMRETURNDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTCHALLANDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTCHALLANDATE.Validating
        Try
            If DTCHALLANDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTCHALLANDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTBEAM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTBEAM.Click
        Try

            If CMBNAME.Text.Trim = "" Then
                MsgBox("Select Weaver Name First", MsgBoxStyle.Critical)
                CMBNAME.Focus()
                Exit Sub
            End If

            Dim OBJSELECTSTOCK As New SelectBeamWeaverStock
            OBJSELECTSTOCK.WEAVERNAME = CMBNAME.Text.Trim
            Dim DTBEAMSTOCK As DataTable = OBJSELECTSTOCK.DT
            OBJSELECTSTOCK.ShowDialog()
            If DTBEAMSTOCK.Rows.Count > 0 Then
                GRIDBEAM.RowCount = 0
                For Each ROW As DataRow In DTBEAMSTOCK.Rows
                    GRIDBEAM.Rows.Add(0, ROW("BEAMNAME"), ROW("BEAMNO"), Val(ROW("ENDS")), Val(ROW("TAPLINE")), Format(Val(ROW("CUT")), "0.00"), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("WTCUT")), "0.000"), "", Val(ROW("FROMNO")), Val(ROW("FROMSRNO")), ROW("TYPE"), 0)
                Next
                TOTAL()
                getsrno(GRIDBEAM)
                CMDSELECTBEAM.Enabled = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamReturn_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ALLOWMFG = False Then Exit Sub
    End Sub
End Class