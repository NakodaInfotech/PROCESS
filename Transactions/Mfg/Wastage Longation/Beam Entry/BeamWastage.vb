
Imports BL

Public Class BeamWastage

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPWASTAGENO As Integer
    Public FRMSTRING As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBOURGODOWN.Focus()
    End Sub

    Sub CLEAR()

        DTENTRYDATE.Text = Mydate
        CMBNAME.Text = ""
        CMBNAME.Enabled = True
        txtremarks.Clear()

        LBLTOTALCUT.Text = 0.0

        TXTSRNO.Text = 1
        CMBTYPE.SelectedIndex = 0
        CMBBEAMNAME.Text = ""
        CMBBEAMNAME.Items.Clear()
        CMBBEAMNO.Text = ""
        CMBBEAMNO.Items.Clear()
        TXTCUT.Clear()
        TXTNARR.Clear()
        TXTFROMNO.Clear()
        TXTFROMSRNO.Clear()
        TXTGRIDTYPE.Clear()

        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        GRIDWASTAGE.RowCount = 0
        GETMAX_WASTAGE_NO()

        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False


        TabControl1.SelectedIndex = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Text = 1
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0


    End Sub

    Sub GETMAX_WASTAGE_NO()
        Dim DTTABLE As New DataTable
        If FRMSTRING = "WASTAGEWEAVER" Then
            DTTABLE = getmax("ISNULL(MAX(BWASWEAVER_NO),0)+ 1", "BEAMWASTAGEWEAVER", "AND BWASWEAVER_YEARID = " & YearId)
        ElseIf FRMSTRING = "WASTAGESIZER" Then
            DTTABLE = getmax("ISNULL(MAX(BWASSIZER_NO),0)+ 1", "BEAMWASTAGESIZER", "AND BWASSIZER_YEARID = " & YearId)
        ElseIf FRMSTRING = "WASTAGEGODOWN" Then
            DTTABLE = getmax("ISNULL(MAX(BWASGODOWN_NO),0)+ 1", "BEAMWASTAGEGODOWN", "AND BWASGODOWN_YEARID = " & YearId)
        End If
        If DTTABLE.Rows.Count > 0 Then TXTWASTAGENO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub BeamWastage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        If FRMSTRING = "WASTAGEWEAVER" Then If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'") ''''TYPE = WEAVER
        If FRMSTRING = "WASTAGESIZER" Then If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'") ''''TYPE = SIZER
        If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR = 'True'")
        GETBEAMNAME()
        GETBEAMNO()
    End Sub

    Private Sub BeamWastage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)


            If FRMSTRING = "WASTAGEWEAVER" Then
                Me.Text = "Beam Wastage From Weaver"
            ElseIf FRMSTRING = "WASTAGEGODOWN" Then
                LBLNAME.Visible = False
                CMBNAME.Visible = False
                LBLGODOWN.Visible = True
                CMBOURGODOWN.Visible = True
                Me.Text = "Beam Wastage at Godown"

            ElseIf FRMSTRING = "WASTAGESIZER" Then
                Me.Text = "Beam Wastage From Sizer"
                LBLNAME.Text = "Sizer Name"
            End If

            FILLCMB()
            CLEAR()
            CMBOURGODOWN.Text = GETDEFAULTGODOWN()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dttable As New DataTable
                If FRMSTRING = "WASTAGEWEAVER" Then

                    Dim OBJWAS As New ClsBeamWastageWeaver
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPWASTAGENO)
                    ALPARAVAL.Add(YearId)
                    OBJWAS.alParaval = ALPARAVAL
                    dttable = OBJWAS.SELECTBEAMWASTAGE()

                    If dttable.Rows.Count > 0 Then

                        For Each dr As DataRow In dttable.Rows

                            TXTWASTAGENO.Text = TEMPWASTAGENO
                            DTENTRYDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                            'Item Grid
                            GRIDWASTAGE.Rows.Add(dr("SRNO"), dr("TYPE"), dr("BEAMNAME").ToString, dr("BEAMNO").ToString, Format(Val(dr("CUT")), "0.00"), dr("NARRATION").ToString, Val(dr("FROMNO")), dr("FROMSRNO"), dr("GRIDTYPE"))
                        Next
                        TOTAL()
                        getsrno(GRIDWASTAGE)
                        If GRIDWASTAGE.RowCount > 0 Then TXTSRNO.Text = Val(GRIDWASTAGE.RowCount) + 1 Else TXTSRNO.Text = 1


                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" BWASWEAVER_SRNO AS GRIDSRNO, BWASWEAVER_REMARKS AS REMARKS, BWASWEAVER_NAME AS NAME, BWASWEAVER_PHOTO AS IMGPATH ", "", " BEAMWASTAGEWEAVER_UPLOAD", " AND BWASWEAVER_NO = " & TEMPWASTAGENO & " AND BWASWEAVER_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If
                    End If

                ElseIf FRMSTRING = "WASTAGESIZER" Then
                    Dim OBJWAS As New ClsBeamWastageSizer
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPWASTAGENO)
                    ALPARAVAL.Add(YearId)
                    OBJWAS.alParaval = ALPARAVAL
                    dttable = OBJWAS.SELECTBEAMWASTAGE()

                    If dttable.Rows.Count > 0 Then

                        For Each dr As DataRow In dttable.Rows

                            TXTWASTAGENO.Text = TEMPWASTAGENO
                            DTENTRYDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                            'Item Grid
                            GRIDWASTAGE.Rows.Add(dr("SRNO"), dr("TYPE"), dr("BEAMNAME").ToString, dr("BEAMNO").ToString, Format(Val(dr("CUT")), "0.00"), dr("NARRATION").ToString, Val(dr("FROMNO")), dr("FROMSRNO"), dr("GRIDTYPE"))
                        Next
                        TOTAL()
                        getsrno(GRIDWASTAGE)
                        If GRIDWASTAGE.RowCount > 0 Then TXTSRNO.Text = Val(GRIDWASTAGE.RowCount) + 1 Else TXTSRNO.Text = 1


                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" BWASSIZER_SRNO AS GRIDSRNO, BWASSIZER_REMARKS AS REMARKS, BWASSIZER_NAME AS NAME, BWASSIZER_PHOTO AS IMGPATH ", "", " BEAMWASTAGESIZER_UPLOAD", " AND BWASSIZER_NO = " & TEMPWASTAGENO & " AND BWASSIZER_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If
                    End If

                ElseIf FRMSTRING = "WASTAGEGODOWN" Then

                    Dim OBJWAS As New ClsBeamWastageGodown
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPWASTAGENO)
                    ALPARAVAL.Add(YearId)
                    OBJWAS.alParaval = ALPARAVAL
                    dttable = OBJWAS.SELECTYARNWASTAGE()

                    If dttable.Rows.Count > 0 Then

                        For Each dr As DataRow In dttable.Rows

                            TXTWASTAGENO.Text = TEMPWASTAGENO
                            DTENTRYDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                            'Item Grid
                            GRIDWASTAGE.Rows.Add(dr("SRNO"), dr("TYPE"), dr("BEAMNAME").ToString, dr("BEAMNO").ToString, Format(Val(dr("CUT")), "0.00"), dr("NARRATION").ToString, Val(dr("FROMNO")), dr("FROMSRNO"), dr("GRIDTYPE"))
                        Next
                        TOTAL()
                        getsrno(GRIDWASTAGE)
                        If GRIDWASTAGE.RowCount > 0 Then TXTSRNO.Text = Val(GRIDWASTAGE.RowCount) + 1 Else TXTSRNO.Text = 1


                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" BWASWEAVER_SRNO AS GRIDSRNO, BWASWEAVER_REMARKS AS REMARKS, BWASWEAVER_NAME AS NAME, BWASWEAVER_PHOTO AS IMGPATH ", "", " BEAMWASTAGEWEAVER_UPLOAD", " AND BWASWEAVER_NO = " & TEMPWASTAGENO & " AND BWASWEAVER_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If
                    End If

                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Dim IntResult As Integer
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DTENTRYDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(Val(LBLTOTALCUT.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim GRIDSRNO As String = ""
            Dim TYPE As String = ""
            Dim BEAMNAME As String = ""
            Dim BEAMNO As String = ""
            Dim CUT As String = ""
            Dim NARRATION As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim GRIDTYPE As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDWASTAGE.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = row.Cells(gsrno.Index).Value.ToString
                        TYPE = row.Cells(GTYPE.Index).Value.ToString
                        BEAMNAME = row.Cells(GBEAMNAME.Index).Value.ToString
                        BEAMNO = row.Cells(GBEAMNO.Index).Value.ToString
                        CUT = Val(row.Cells(GCUT.Index).Value)
                        NARRATION = row.Cells(GNARRATION.Index).Value.ToString
                        FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)
                        GRIDTYPE = row.Cells(GGRIDTYPE.Index).Value.ToString

                    Else

                        GRIDSRNO = GRIDSRNO & "|" & row.Cells(gsrno.Index).Value
                        TYPE = TYPE & "|" & row.Cells(GTYPE.Index).Value
                        BEAMNAME = BEAMNAME & "|" & row.Cells(GBEAMNAME.Index).Value.ToString
                        BEAMNO = BEAMNO & "|" & row.Cells(GBEAMNO.Index).Value.ToString
                        CUT = CUT & "|" & Val(row.Cells(GCUT.Index).Value)
                        NARRATION = NARRATION & "|" & row.Cells(GNARRATION.Index).Value.ToString
                        FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)
                        GRIDTYPE = GRIDTYPE & "|" & row.Cells(GGRIDTYPE.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(GRIDSRNO)
            alParaval.Add(TYPE)
            alParaval.Add(BEAMNAME)
            alParaval.Add(BEAMNO)
            alParaval.Add(CUT)
            alParaval.Add(NARRATION)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(GRIDTYPE)



            If FRMSTRING = "WASTAGEWEAVER" Then
                Dim OBJWEAVER As New ClsBeamWastageWeaver
                OBJWEAVER.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJWEAVER.save()
                    TEMPWASTAGENO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPWASTAGENO)
                    IntResult = OBJWEAVER.Update()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If

            ElseIf FRMSTRING = "WASTAGESIZER" Then
                Dim OBJWEAVER As New ClsBeamWastageSizer
                OBJWEAVER.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJWEAVER.save()
                    TEMPWASTAGENO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPWASTAGENO)
                    IntResult = OBJWEAVER.Update()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If

            ElseIf FRMSTRING = "WASTAGEGODOWN" Then
                Dim OBJWEAVER As New ClsBeamWastageGodown
                OBJWEAVER.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJWEAVER.SAVE()
                    TEMPWASTAGENO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPWASTAGENO)
                    IntResult = OBJWEAVER.UPDATE()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If

            End If

            'PRINTREPORT(TEMPISSUENO)
            If gridupload.RowCount > 0 Then SAVEUPLOAD()

            CLEAR()
            CMBOURGODOWN.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True


        If DTENTRYDATE.Text = "__/__/____" Then
            EP.SetError(DTENTRYDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTENTRYDATE.Text) Then
                EP.SetError(DTENTRYDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If FRMSTRING <> "WASTAGEGODOWN" Then
            If CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, "Please Select Name")
                bln = False
            End If
        Else
            If CMBOURGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBOURGODOWN, "Please Select Godown Name")
                bln = False
            End If
        End If


        If GRIDWASTAGE.RowCount = 0 Then
            EP.SetError(TXTNARR, "Enter Yarn Details")
            bln = False
        End If


        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Entry Locked")
            bln = False
        End If




        Return bln
    End Function

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

    Sub SAVEUPLOAD()
        Try
            If FRMSTRING = "WASTAGEWEAVER" Then
                Dim OBJWEAVER As New ClsBeamWastageWeaver
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPWASTAGENO)
                        ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                        PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                        PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                        ALPARAVAL.Add(MS.ToArray)
                        ALPARAVAL.Add(YearId)

                        OBJWEAVER.alParaval = ALPARAVAL
                        Dim INTRES As Integer = OBJWEAVER.SAVEUPLOAD()
                    End If
                Next

            ElseIf FRMSTRING = "WASTAGESIZER" Then
                Dim OBJWEAVER As New ClsBeamWastageSizer
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPWASTAGENO)
                        ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                        PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                        PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                        ALPARAVAL.Add(MS.ToArray)
                        ALPARAVAL.Add(YearId)

                        OBJWEAVER.alParaval = ALPARAVAL
                        Dim INTRES As Integer = OBJWEAVER.SAVEUPLOAD()
                    End If
                Next

            ElseIf FRMSTRING = "WASTAGEGODOWN" Then
                Dim OBJWEAVER As New ClsBeamWastageGodown
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPWASTAGENO)
                        ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                        PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                        PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                        ALPARAVAL.Add(MS.ToArray)
                        ALPARAVAL.Add(YearId)

                        OBJWEAVER.alParaval = ALPARAVAL
                        Dim INTRES As Integer = OBJWEAVER.SAVEUPLOAD()
                    End If
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOURGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBOURGODOWN.Enter
        Try
            If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR = 'True'")
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
            If CMBOURGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBOURGODOWN, e, Me, " AND GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If FRMSTRING = "WASTAGEWEAVER" Then
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'") ''''TYPE = WEAVER

            ElseIf FRMSTRING = "WASTAGESIZER" Then
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'") ''''TYPE = SIZER
            End If
Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If FRMSTRING = "WASTAGESIZER" Then
                If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "SIZER")


            ElseIf FRMSTRING = "WASTAGEWEAVER" Then
                If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WEAVER")

            End If

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

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try
            LBLTOTALCUT.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDWASTAGE.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTALCUT.Text = Format(Val(LBLTOTALCUT.Text) + Val(ROW.Cells(GCUT.Index).EditedFormattedValue), "0.00")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDWASTAGE.RowCount = 0
LINE1:
            TEMPWASTAGENO = Val(TXTWASTAGENO.Text) - 1
Line2:
            If TEMPWASTAGENO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                If FRMSTRING = "WASTAGEWEAVER" Then
                    DT = OBJCMN.search("BWASWEAVER_NO ", "", "  BEAMWASTAGEWEAVER ", " AND BWASWEAVER_NO = '" & TEMPWASTAGENO & "' AND BEAMWASTAGEWEAVER.BWASWEAVER_YEARID = " & YearId)
                ElseIf FRMSTRING = "WASTAGESIZER" Then
                    DT = OBJCMN.search("BWASSIZER_NO ", "", "  BEAMWASTAGESIZER ", " AND BWASSIZER_NO = '" & TEMPWASTAGENO & "' AND BEAMWASTAGESIZER.BWASSIZER_YEARID = " & YearId)
                ElseIf FRMSTRING = "WASTAGEGODOWN" Then
                    DT = OBJCMN.search("BWASGODOWN_NO ", "", "  BEAMWASTAGEGODOWN ", " AND BWASGODOWN_NO = '" & TEMPWASTAGENO & "' AND BEAMWASTAGEGODOWN.BWASGODOWN_YEARID = " & YearId)
                End If
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    BeamWastage_Load(sender, e)
                Else
                    TEMPWASTAGENO = Val(TEMPWASTAGENO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDWASTAGE.RowCount = 0 And TEMPWASTAGENO > 1 Then
                TXTWASTAGENO.Text = TEMPWASTAGENO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDWASTAGE.RowCount = 0
LINE1:
            TEMPWASTAGENO = Val(TXTWASTAGENO.Text) + 1
            GETMAX_WASTAGE_NO()
            Dim MAXNO As Integer = TXTWASTAGENO.Text.Trim
            CLEAR()
            If Val(TXTWASTAGENO.Text) - 1 >= TEMPWASTAGENO Then
                EDIT = True
                BeamWastage_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDWASTAGE.RowCount = 0 And TEMPWASTAGENO < MAXNO Then
                TXTWASTAGENO.Text = TEMPWASTAGENO
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
                GRIDWASTAGE.RowCount = 0
                TEMPWASTAGENO = Val(tstxtbillno.Text)
                If TEMPWASTAGENO > 0 Then
                    EDIT = True
                    BeamWastage_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridupload_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
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

    Private Sub gridupload_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridupload.KeyDown
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

    Private Sub gridupload_RowEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If e.RowIndex >= 0 Then PBSOFTCOPY.Image = gridupload.Rows(e.RowIndex).Cells(GUIMGPATH.Index).Value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWASTAGE_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDWASTAGE.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDWASTAGE.Item(gsrno.Index, e.RowIndex).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDWASTAGE.Item(gsrno.Index, e.RowIndex).Value
                CMBTYPE.Text = GRIDWASTAGE.Item(GTYPE.Index, e.RowIndex).Value
                CMBBEAMNAME.Text = GRIDWASTAGE.Item(GBEAMNAME.Index, e.RowIndex).Value
                CMBBEAMNO.Text = GRIDWASTAGE.Item(GBEAMNO.Index, e.RowIndex).Value
                TXTCUT.Text = Format(Val(GRIDWASTAGE.Item(GCUT.Index, e.RowIndex).Value), "0.00")
                TXTNARR.Text = GRIDWASTAGE.Item(GNARRATION.Index, e.RowIndex).Value

                TEMPROW = e.RowIndex
                CMBBEAMNAME.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWASTAGE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDWASTAGE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDWASTAGE.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDWASTAGE.Rows.RemoveAt(GRIDWASTAGE.CurrentRow.Index)
                getsrno(GRIDWASTAGE)
                TOTAL()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJBEAM As New BeamWastageDetails
            OBJBEAM.MdiParent = MDIMain
            OBJBEAM.FRMSTRING = FRMSTRING
            OBJBEAM.Show()
        Catch EX As Exception
            Throw EX
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Dim IntResult As Integer
        Try
            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If lbllocked.Visible = True Then
                    MsgBox("Unable to Delete, Checking Done / Item Used", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Delete Beam Wastage?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPWASTAGENO)
                    alParaval.Add(YearId)

                    If FRMSTRING = "WASTAGEWEAVER" Then
                        Dim ClsDO As New ClsBeamWastageWeaver
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()

                    ElseIf FRMSTRING = "WASTAGESIZER" Then
                        Dim ClsDO As New ClsBeamWastageSizer
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "WASTAGEGODOWN" Then
                        Dim ClsDO As New ClsBeamWastageGodown
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    End If

                    MsgBox("Beam Wastage Deleted")
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

    Private Sub DTENTRYDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTENTRYDATE.GotFocus
        DTENTRYDATE.SelectAll()
    End Sub

    Private Sub TXTCUT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCUT.KeyPress
        numdot3(e, TXTCUT, Me)
    End Sub

    Private Sub TXTNARR_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNARR.Validating
        'WE HAVE REMOVED BEAM NO MANDATE
        'If CMBBEAMNAME.Text.Trim <> "" And CMBTYPE.Text.Trim <> "" And CMBBEAMNO.Text.Trim <> "" And Val(TXTCUT.Text.Trim) > 0 And Val(TXTFROMNO.Text.Trim) > 0 And Val(TXTFROMSRNO.Text.Trim) > 0 Then
        If CMBBEAMNAME.Text.Trim <> "" And CMBTYPE.Text.Trim <> "" And Val(TXTCUT.Text.Trim) > 0 Then
            EP.Clear()
            If Not CHECKGRID() Then
                Exit Sub
            End If
            FILLGRID()
        Else
            MsgBox("Please Enter proper details")
        End If
    End Sub

    Function CHECKGRID() As Boolean
        Try
            Dim BLN As Boolean = True
            For Each ROW As DataGridViewRow In GRIDWASTAGE.Rows
                If ROW.Cells(GBEAMNAME.Index).Value = CMBBEAMNAME.Text.Trim And CMBBEAMNO.Text.Trim = ROW.Cells(GBEAMNO.Index).Value Then
                    If GRIDDOUBLECLICK = False Or (GRIDDOUBLECLICK = True And ROW.Index <> TEMPROW) Then
                        EP.SetError(TXTNARR, "Beam No already present in Grid Below")
                        BLN = False
                    End If
                End If
            Next
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDWASTAGE.Rows.Add(Val(TXTSRNO.Text.Trim), CMBTYPE.Text.Trim, CMBBEAMNAME.Text.Trim, CMBBEAMNO.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), TXTNARR.Text.Trim, Val(TXTFROMNO.Text), Val(TXTFROMSRNO.Text), TXTGRIDTYPE.Text.Trim)
            Else
                GRIDWASTAGE.Item(gsrno.Index, TEMPROW).Value = TXTSRNO.Text.Trim
                GRIDWASTAGE.Item(GTYPE.Index, TEMPROW).Value = CMBTYPE.Text.Trim
                GRIDWASTAGE.Item(GBEAMNAME.Index, TEMPROW).Value = CMBBEAMNAME.Text.Trim
                GRIDWASTAGE.Item(GBEAMNO.Index, TEMPROW).Value = CMBBEAMNO.Text.Trim
                GRIDWASTAGE.Item(GCUT.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
                GRIDWASTAGE.Item(GNARRATION.Index, TEMPROW).Value = TXTNARR.Text.Trim

                GRIDDOUBLECLICK = False
            End If
            'TXTSRNO.Clear()
            CMBBEAMNAME.Text = ""
            CMBBEAMNO.Text = ""
            TXTCUT.Clear()
            TXTNARR.Clear()
            TXTFROMNO.Clear()
            TXTFROMSRNO.Clear()
            TXTGRIDTYPE.Clear()
            getsrno(GRIDWASTAGE)
            TOTAL()
            CMBBEAMNAME.Focus()
            If GRIDWASTAGE.RowCount > 0 Then TXTSRNO.Text = Val(GRIDWASTAGE.RowCount) + 1 Else TXTSRNO.Text = 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTENTRYDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTENTRYDATE.Validating
        Try
            If DTENTRYDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTENTRYDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETBEAMNO()
        Try
            'FILL ALL BEAMNO WHICH ARE PENDING WITH THIS WEAVER WITH SELECTED BEAMNAME
            CMBBEAMNO.Items.Clear()
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If FRMSTRING = "WASTAGEWEAVER" Then
                DT = OBJCMN.search("DISTINCT BEAMNO ", "", " BEAMSTOCK_WEAVER", " AND WEAVERNAME = '" & CMBNAME.Text.Trim & "' AND BEAMNAME = '" & CMBBEAMNAME.Text.Trim & "' AND YEARID = " & YearId)
            ElseIf FRMSTRING = "WASTAGESIZER" Then
                DT = OBJCMN.search("DISTINCT BEAMNO ", "", " BEAMSTOCK", " AND NAME = '" & CMBNAME.Text.Trim & "' AND BEAMNAME = '" & CMBBEAMNAME.Text.Trim & "' AND YEARID = " & YearId)
            Else
                DT = OBJCMN.search("DISTINCT BEAMNO ", "", " BEAMSTOCK", " AND GODOWN = '" & CMBOURGODOWN.Text.Trim & "' AND BEAMNAME = '" & CMBBEAMNAME.Text.Trim & "' AND YEARID = " & YearId)
            End If

            If DT.Rows.Count > 0 Then
                For Each DTROW As DataRow In DT.Rows
                    CMBBEAMNO.Items.Add(DTROW("BEAMNO"))
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETBEAMNAME()
        Try
            'FILL ALL BEAMNAME WHICH ARE PENDING WITH THIS WEAVER
            CMBBEAMNAME.Items.Clear()
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If FRMSTRING = "WASTAGEWEAVER" Then
                DT = OBJCMN.search("DISTINCT BEAMNAME ", "", " BEAMSTOCK_WEAVER", " AND WEAVERNAME = '" & CMBNAME.Text.Trim & "' AND YEARID = " & YearId)
            ElseIf FRMSTRING = "WASTAGESIZER" Then
                DT = OBJCMN.search("DISTINCT BEAMNAME ", "", " BEAMSTOCK", " AND NAME = '" & CMBNAME.Text.Trim & "' AND YEARID = " & YearId)
            Else
                DT = OBJCMN.search("DISTINCT BEAMNAME ", "", " BEAMSTOCK", " AND GODOWN = '" & CMBOURGODOWN.Text.Trim & "' AND YEARID = " & YearId)
            End If

            If DT.Rows.Count > 0 Then
                For Each DTROW As DataRow In DT.Rows
                    CMBBEAMNAME.Items.Add(DTROW("BEAMNAME"))
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
        Try
            If CMBNAME.Text.Trim <> "" Then
                CMBNAME.Enabled = False
                GETBEAMNAME()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBEAMNAME.Validated
        Try
            If CMBBEAMNAME.Text.Trim <> "" Then GETBEAMNO()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNO_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBEAMNO.Validated
        Try
            If CMBBEAMNO.Text.Trim <> "" Then
                'GET FROMNO, FROMSRNO ADN GRIDTYPE
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                If FRMSTRING = "WASTAGEWEAVER" Then
                    DT = OBJCMN.search("CUT, NO AS FROMNO, SRNO AS FROMSRNO, TYPE AS GRIDTYPE", "", " BEAMSTOCK_WEAVER ", " AND WEAVERNAME = '" & CMBNAME.Text.Trim & "' AND BEAMNAME = '" & CMBBEAMNAME.Text.Trim & "' AND BEAMNO = '" & CMBBEAMNO.Text.Trim & "' AND YEARID = " & YearId)
                ElseIf FRMSTRING = "WASTAGESIZER" Then
                    DT = OBJCMN.search("CUT, RECNO AS FROMNO, RECSRNO AS FROMSRNO, TYPE AS GRIDTYPE", "", " BEAMSTOCK", " AND NAME = '" & CMBNAME.Text.Trim & "' AND BEAMNAME = '" & CMBBEAMNAME.Text.Trim & "' AND BEAMNO = '" & CMBBEAMNO.Text.Trim & "' AND YEARID = " & YearId)
                Else
                    DT = OBJCMN.search("CUT, RECNO AS FROMNO, RECSRNO AS FROMSRNO, TYPE AS GRIDTYPE", "", " BEAMSTOCK ", " AND GODOWN = '" & CMBOURGODOWN.Text.Trim & "' AND BEAMNAME = '" & CMBBEAMNAME.Text.Trim & "' AND BEAMNO = '" & CMBBEAMNO.Text.Trim & "' AND YEARID = " & YearId)
                End If
                If DT.Rows.Count > 0 Then
                    TXTCUT.Text = Val(DT.Rows(0).Item("CUT"))
                    TXTFROMNO.Text = Val(DT.Rows(0).Item("FROMNO"))
                    TXTFROMSRNO.Text = Val(DT.Rows(0).Item("FROMSRNO"))
                    TXTGRIDTYPE.Text = DT.Rows(0).Item("GRIDTYPE")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamWastage_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ALLOWMFG = False Then Exit Sub
    End Sub
End Class