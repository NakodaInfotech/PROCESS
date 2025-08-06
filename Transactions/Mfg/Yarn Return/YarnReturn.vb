
Imports BL

Public Class YarnReturn

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPRETURNNO As Integer
    Dim TEMPMSG As Integer
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

        DTRETURNDATE.Text = Mydate
        cmbname.Text = ""
        TXTCHALLANNO.Clear()
        DTCHALLANDATE.Clear()
        txtremarks.Clear()
        TXTADDANO.Clear()

        LBLTOTALBAGS.Text = 0
        LBLTOTALWT.Text = 0.0
        LBLTOTALFRESH.Text = 0
        LBLTOTALWINDING.Text = 0
        LBLTOTALFIRKA.Text = 0
        LBLTOTALNALI.Text = 0

        TXTSRNO.Clear()
        CMBQUALITY.Text = ""
        CMBMILLNAME.Text = ""
        TXTLOTNO.Text = ""
        CMBCOLOR.Text = ""
        TXTBAGS.Clear()
        TXTWT.Clear()
        TXTFRESH.Clear()
        TXTWINDING.Clear()
        TXTFIRKA.Clear()
        TXTNALI.Clear()
        TXTNARR.Clear()


        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        GRIDRETURN.RowCount = 0
        GETMAX_RETURN_NO()

        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False


        TabControl1.SelectedIndex = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0

        If gridupload.RowCount > 0 Then TXTUPLOADSRNO.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1 Else TXTUPLOADSRNO.Text = 1
        If GRIDRETURN.RowCount > 0 Then TXTSRNO.Text = Val(GRIDRETURN.RowCount) + 1 Else TXTSRNO.Text = 1

    End Sub

    Sub GETMAX_RETURN_NO()
        Dim DTTABLE As New DataTable
        If FRMSTRING = "RETURNFROMWARPER" Then
            DTTABLE = getmax("ISNULL(MAX(YRETWARPER_NO),0)+ 1", "YARNRETURNWARPER", "AND YRETWARPER_YEARID = " & YearId)
        ElseIf FRMSTRING = "RETURNFROMADDA" Then
            DTTABLE = getmax("ISNULL(MAX(YRETADDA_NO),0)+ 1", "YARNRETURNADDA", "AND YRETADDA_YEARID = " & YearId)
        ElseIf FRMSTRING = "RETURNFROMSIZER" Then
            DTTABLE = getmax("ISNULL(MAX(YRETSIZER_NO),0)+ 1", "YARNRETURNSIZER", "AND YRETSIZER_YEARID = " & YearId)
        ElseIf FRMSTRING = "RETURNFROMWEAVER" Then
            DTTABLE = getmax("ISNULL(MAX(YRETWEAVER_NO),0)+ 1", "YARNRETURNWEAVER", "AND YRETWEAVER_YEARID = " & YearId)
        ElseIf FRMSTRING = "RETURNFROMDYEING" Then
            DTTABLE = getmax("ISNULL(MAX(YRETDYEING_NO),0)+ 1", "YARNRETURNDYEING", "AND YRETDYEING_YEARID = " & YearId)
        ElseIf FRMSTRING = "RETURNFROMJOBBER" Then
            DTTABLE = getmax("ISNULL(MAX(YRETJOBBER_NO),0)+ 1", "YARNRETURNJOBBER", "AND YRETJOBBER_YEARID = " & YearId)
        ElseIf FRMSTRING = "RETURNFROMMACHINE" Then
            DTTABLE = getmax("ISNULL(MAX(YRETMACHINE_NO),0)+ 1", "YARNRETURNMACHINE", "AND YRETMACHINE_YEARID = " & YearId)
        End If
        If DTTABLE.Rows.Count > 0 Then TXTRETURNNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub Yarn_Return_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

        If FRMSTRING = "RETURNFROMWARPER" Then
            If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')") ''''TYPE = WARPER
        ElseIf FRMSTRING = "RETURNFROMADDA" Then
            If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'ADDA' OR LEDGERS.ACC_SUBTYPE = 'SIZER')") ''''TYPE = ADDA
        ElseIf FRMSTRING = "RETURNFROMSIZER" Then
            If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'") ''''TYPE = SIZER
        ElseIf FRMSTRING = "RETURNFROMWEAVER" Then
            If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'") ''''TYPE = WEAVER
        ElseIf FRMSTRING = "RETURNFROMDYEING" Then
            If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'") ''''TYPE = PROCESSOR
        ElseIf FRMSTRING = "RETURNFROMJOBBER" Then
            If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'") ''''TYPE = JOBBRE
        ElseIf FRMSTRING = "RETURNFROMMACHINE" Then
            If cmbname.Text.Trim = "" Then FILLMACHINE(cmbname)
        End If
        If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR = 'True'")
        If cmbtrans.Text = "" Then fillname(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'TRANSPORT' ")
        If CMBMILLNAME.Text = "" Then fillname(CMBMILLNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'MILL'")
        If CMBQUALITY.Text = "" Then fillQUALITY(CMBQUALITY, EDIT)
        If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR)
    End Sub

    Private Sub Yarn_Return_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)


            If FRMSTRING = "RETURNFROMWARPER" Then
                LBLNAME.Text = "Warper Name"
                Me.Text = "Yarn Return From Warper"
            ElseIf FRMSTRING = "RETURNFROMADDA" Then
                LBLNAME.Text = "Warper Name"
                Me.Text = "Yarn Return From Adda"
                LBLADDANO.Visible = True
                TXTADDANO.Visible = True
            ElseIf FRMSTRING = "RETURNFROMSIZER" Then
                LBLNAME.Text = "Sizer Name"
                Me.Text = "Yarn Return From Sizer"
            ElseIf FRMSTRING = "RETURNFROMWEAVER" Then
                LBLNAME.Text = "Weaver Name"
                Me.Text = "Yarn Return From Weaver"
            ElseIf FRMSTRING = "RETURNFROMDYEING" Then
                LBLNAME.Text = "Dyeing Name"
                Me.Text = "Yarn Return From Dyeing"
            ElseIf FRMSTRING = "RETURNFROMJOBBER" Then
                LBLNAME.Text = "Weaver Name"
                Me.Text = "Yarn Return From Jobber"
            ElseIf FRMSTRING = "RETURNFROMMACHINE" Then
                LBLNAME.Text = "Machine Name"
                Me.Text = "Yarn Return From Machine"
                cmbtrans.BackColor = Color.White
            End If

            FILLCMB()
            CLEAR()
            CMBOURGODOWN.Text = GETDEFAULTGODOWN()
            If GRIDRETURN.RowCount > 0 Then TXTSRNO.Text = Val(GRIDRETURN.RowCount) + 1 Else TXTSRNO.Text = 1

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dttable As New DataTable
                If FRMSTRING = "RETURNFROMWARPER" Then

                    Dim objclsDO As New ClsYarnReturnFromWarper
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPRETURNNO)
                    ALPARAVAL.Add(YearId)
                    objclsDO.alParaval = ALPARAVAL
                    dttable = objclsDO.selectYARNRETURN()

                    If dttable.Rows.Count > 0 Then
                        For Each dr As DataRow In dttable.Rows

                            TXTRETURNNO.Text = TEMPRETURNNO
                            DTRETURNDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            cmbname.Text = Convert.ToString(dr("NAME").ToString)
                            cmbtrans.Text = Convert.ToString(dr("TRANSPORT").ToString)
                            TXTCHALLANNO.Text = Convert.ToString(dr("CHALLANNO").ToString)
                            DTCHALLANDATE.Text = Convert.ToString(dr("CHALLANDATE").ToString)
                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                            'Item Grid
                            GRIDRETURN.Rows.Add(dr("SRNO"), dr("QUALITY").ToString, dr("MILLNAME").ToString, dr("LOTNO").ToString, dr("SHADE").ToString, Val(dr("BAGS")), Format(Val(dr("WT")), "0.000"), Val(dr("FRESH")), Val(dr("WINDING")), Val(dr("FIRKA")), Val(dr("NALI")), dr("NARRATION").ToString, Val(dr("OUTWT")), Val(dr("OUTFRESH")), Val(dr("OUTWINDING")), Val(dr("OUTFIRKA")), Val(dr("OUTNALI")))
                        Next
                        TOTAL()
                        getsrno(GRIDRETURN)
                        If GRIDRETURN.RowCount > 0 Then TXTSRNO.Text = Val(GRIDRETURN.RowCount) + 1 Else TXTSRNO.Text = 1

                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" YRETWARPER_SRNO AS GRIDSRNO, YRETWARPER_REMARKS AS REMARKS, YRETWARPER_NAME AS NAME, YRETWARPER_PHOTO AS IMGPATH ", "", " YARNRETURNWARPER_UPLOAD", " AND YRETWARPER_NO = " & TEMPRETURNNO & " AND YRETWARPER_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If

                    End If

                ElseIf FRMSTRING = "RETURNFROMADDA" Then

                    Dim objclsDO As New ClsYarnReturnFromAdda
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPRETURNNO)
                    ALPARAVAL.Add(YearId)
                    objclsDO.alParaval = ALPARAVAL
                    dttable = objclsDO.selectYARNRETURN()

                    If dttable.Rows.Count > 0 Then
                        For Each dr As DataRow In dttable.Rows

                            TXTRETURNNO.Text = TEMPRETURNNO
                            DTRETURNDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            cmbname.Text = Convert.ToString(dr("NAME").ToString)
                            cmbtrans.Text = Convert.ToString(dr("TRANSPORT").ToString)
                            TXTCHALLANNO.Text = Convert.ToString(dr("CHALLANNO").ToString)
                            DTCHALLANDATE.Text = Convert.ToString(dr("CHALLANDATE").ToString)
                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                            TXTADDANO.Text = Val(dr("ADDANO"))

                            'Item Grid
                            GRIDRETURN.Rows.Add(dr("SRNO"), dr("QUALITY").ToString, dr("MILLNAME").ToString, dr("LOTNO").ToString, dr("SHADE").ToString, Val(dr("BAGS")), Format(Val(dr("WT")), "0.000"), Val(dr("FRESH")), Val(dr("WINDING")), Val(dr("FIRKA")), Val(dr("NALI")), dr("NARRATION").ToString, Val(dr("OUTWT")), Val(dr("OUTFRESH")), Val(dr("OUTWINDING")), Val(dr("OUTFIRKA")), Val(dr("OUTNALI")))
                        Next
                        TOTAL()
                        getsrno(GRIDRETURN)
                        If GRIDRETURN.RowCount > 0 Then TXTSRNO.Text = Val(GRIDRETURN.RowCount) + 1 Else TXTSRNO.Text = 1

                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" YRETADDA_SRNO AS GRIDSRNO, YRETADDA_REMARKS AS REMARKS, YRETADDA_NAME AS NAME, YRETADDA_PHOTO AS IMGPATH ", "", " YARNRETURNADDA_UPLOAD", " AND YRETADDA_NO = " & TEMPRETURNNO & " AND YRETADDA_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If

                    End If


                ElseIf FRMSTRING = "RETURNFROMSIZER" Then

                    Dim objclsDO As New ClsYarnReturnFromSizer
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPRETURNNO)
                    ALPARAVAL.Add(YearId)
                    objclsDO.alParaval = ALPARAVAL
                    dttable = objclsDO.selectYARNRETURN()

                    If dttable.Rows.Count > 0 Then

                        For Each dr As DataRow In dttable.Rows

                            TXTRETURNNO.Text = TEMPRETURNNO
                            DTRETURNDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            cmbname.Text = Convert.ToString(dr("NAME").ToString)
                            cmbtrans.Text = Convert.ToString(dr("TRANSPORT").ToString)
                            TXTCHALLANNO.Text = Convert.ToString(dr("CHALLANNO").ToString)
                            DTCHALLANDATE.Text = Convert.ToString(dr("CHALLANDATE").ToString)
                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                            'Item Grid
                            GRIDRETURN.Rows.Add(dr("SRNO"), dr("QUALITY").ToString, dr("MILLNAME").ToString, dr("LOTNO").ToString, dr("SHADE").ToString, Val(dr("BAGS")), Format(Val(dr("WT")), "0.000"), Val(dr("FRESH")), Val(dr("WINDING")), Val(dr("FIRKA")), Val(dr("NALI")), dr("NARRATION").ToString, Val(dr("OUTWT")), Val(dr("OUTFRESH")), Val(dr("OUTWINDING")), Val(dr("OUTFIRKA")), Val(dr("OUTNALI")))
                        Next
                        TOTAL()
                        getsrno(GRIDRETURN)
                        If GRIDRETURN.RowCount > 0 Then TXTSRNO.Text = Val(GRIDRETURN.RowCount) + 1 Else TXTSRNO.Text = 1


                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" YRETSIZER_SRNO AS GRIDSRNO, YRETSIZER_REMARKS AS REMARKS, YRETSIZER_NAME AS NAME, YRETSIZER_PHOTO AS IMGPATH ", "", " YARNRETURNSIZER_UPLOAD", " AND YRETSIZER_NO = " & TEMPRETURNNO & " AND YRETSIZER_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If
                    End If

                ElseIf FRMSTRING = "RETURNFROMWEAVER" Then

                    Dim objclsDO As New ClsYarnReturnFromWeaver
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPRETURNNO)
                    ALPARAVAL.Add(YearId)
                    objclsDO.alParaval = ALPARAVAL
                    dttable = objclsDO.selectYARNRETURN()

                    If dttable.Rows.Count > 0 Then

                        For Each dr As DataRow In dttable.Rows

                            TXTRETURNNO.Text = TEMPRETURNNO
                            DTRETURNDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            cmbname.Text = Convert.ToString(dr("NAME").ToString)
                            cmbtrans.Text = Convert.ToString(dr("TRANSPORT").ToString)
                            TXTCHALLANNO.Text = Convert.ToString(dr("CHALLANNO").ToString)
                            DTCHALLANDATE.Text = Convert.ToString(dr("CHALLANDATE").ToString)
                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                            'Item Grid
                            GRIDRETURN.Rows.Add(dr("SRNO"), dr("QUALITY").ToString, dr("MILLNAME").ToString, dr("LOTNO").ToString, dr("SHADE").ToString, Val(dr("BAGS")), Format(Val(dr("WT")), "0.000"), Val(dr("FRESH")), Val(dr("WINDING")), Val(dr("FIRKA")), Val(dr("NALI")), dr("NARRATION").ToString, Val(dr("OUTWT")), Val(dr("OUTFRESH")), Val(dr("OUTWINDING")), Val(dr("OUTFIRKA")), Val(dr("OUTNALI")))
                        Next
                        TOTAL()
                        getsrno(GRIDRETURN)
                        If GRIDRETURN.RowCount > 0 Then TXTSRNO.Text = Val(GRIDRETURN.RowCount) + 1 Else TXTSRNO.Text = 1


                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" YRETWEAVER_SRNO AS GRIDSRNO, YRETWEAVER_REMARKS AS REMARKS, YRETWEAVER_NAME AS NAME, YRETWEAVER_PHOTO AS IMGPATH ", "", " YARNRETURNWEAVER_UPLOAD", " AND YRETWEAVER_NO = " & TEMPRETURNNO & " AND YRETWEAVER_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If
                    End If

                ElseIf FRMSTRING = "RETURNFROMDYEING" Then

                    Dim objclsDO As New ClsYarnReturnFromDyeing
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPRETURNNO)
                    ALPARAVAL.Add(YearId)
                    objclsDO.alParaval = ALPARAVAL
                    dttable = objclsDO.selectYARNRETURN()

                    If dttable.Rows.Count > 0 Then

                        For Each dr As DataRow In dttable.Rows

                            TXTRETURNNO.Text = TEMPRETURNNO
                            DTRETURNDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            cmbname.Text = Convert.ToString(dr("NAME").ToString)
                            cmbtrans.Text = Convert.ToString(dr("TRANSPORT").ToString)
                            TXTCHALLANNO.Text = Convert.ToString(dr("CHALLANNO").ToString)
                            DTCHALLANDATE.Text = Convert.ToString(dr("CHALLANDATE").ToString)
                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                            'Item Grid
                            GRIDRETURN.Rows.Add(dr("SRNO"), dr("QUALITY").ToString, dr("MILLNAME").ToString, dr("LOTNO").ToString, dr("SHADE").ToString, Val(dr("BAGS")), Format(Val(dr("WT")), "0.000"), Val(dr("FRESH")), Val(dr("WINDING")), Val(dr("FIRKA")), Val(dr("NALI")), dr("NARRATION").ToString, Val(dr("OUTWT")), Val(dr("OUTFRESH")), Val(dr("OUTWINDING")), Val(dr("OUTFIRKA")), Val(dr("OUTNALI")))
                        Next
                        TOTAL()
                        getsrno(GRIDRETURN)
                        If GRIDRETURN.RowCount > 0 Then TXTSRNO.Text = Val(GRIDRETURN.RowCount) + 1 Else TXTSRNO.Text = 1


                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" YRETDYEING_SRNO AS GRIDSRNO, YRETDYEING_REMARKS AS REMARKS, YRETDYEING_NAME AS NAME, YRETDYEING_PHOTO AS IMGPATH ", "", " YARNRETURNDYEING_UPLOAD", " AND YRETDYEING_NO = " & TEMPRETURNNO & " AND YRETDYEING_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If
                    End If

                ElseIf FRMSTRING = "RETURNFROMJOBBER" Then

                    Dim objclsDO As New ClsYarnReturnFromJobber
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPRETURNNO)
                    ALPARAVAL.Add(YearId)
                    objclsDO.alParaval = ALPARAVAL
                    dttable = objclsDO.selectYARNRETURN()

                    If dttable.Rows.Count > 0 Then
                        For Each dr As DataRow In dttable.Rows

                            TXTRETURNNO.Text = TEMPRETURNNO
                            DTRETURNDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            cmbname.Text = Convert.ToString(dr("NAME").ToString)
                            cmbtrans.Text = Convert.ToString(dr("TRANSPORT").ToString)
                            TXTCHALLANNO.Text = Convert.ToString(dr("CHALLANNO").ToString)
                            DTCHALLANDATE.Text = Convert.ToString(dr("CHALLANDATE").ToString)
                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                            'Item Grid
                            GRIDRETURN.Rows.Add(dr("SRNO"), dr("QUALITY").ToString, dr("MILLNAME").ToString, dr("LOTNO").ToString, dr("SHADE").ToString, Val(dr("BAGS")), Format(Val(dr("WT")), "0.000"), Val(dr("FRESH")), Val(dr("WINDING")), Val(dr("FIRKA")), Val(dr("NALI")), dr("NARRATION").ToString, Val(dr("OUTWT")), Val(dr("OUTFRESH")), Val(dr("OUTWINDING")), Val(dr("OUTFIRKA")), Val(dr("OUTNALI")))
                        Next
                        TOTAL()
                        getsrno(GRIDRETURN)
                        If GRIDRETURN.RowCount > 0 Then TXTSRNO.Text = Val(GRIDRETURN.RowCount) + 1 Else TXTSRNO.Text = 1

                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" YRETJOBBER_SRNO AS GRIDSRNO, YRETJOBBER_REMARKS AS REMARKS, YRETJOBBER_NAME AS NAME, YRETJOBBER_PHOTO AS IMGPATH ", "", " YARNRETURNJOBBER_UPLOAD", " AND YRETJOBBER_NO = " & TEMPRETURNNO & " AND YRETJOBBER_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If

                    End If


                ElseIf FRMSTRING = "RETURNFROMMACHINE" Then

                    Dim objclsDO As New ClsYarnReturnFromMachine
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPRETURNNO)
                    ALPARAVAL.Add(YearId)
                    objclsDO.alParaval = ALPARAVAL
                    dttable = objclsDO.SELECTYARNRETURN()

                    If dttable.Rows.Count > 0 Then
                        For Each dr As DataRow In dttable.Rows

                            TXTRETURNNO.Text = TEMPRETURNNO
                            DTRETURNDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            cmbname.Text = Convert.ToString(dr("MACHINENAME").ToString)
                            cmbtrans.Text = Convert.ToString(dr("TRANSPORT").ToString)
                            TXTCHALLANNO.Text = Convert.ToString(dr("CHALLANNO").ToString)
                            DTCHALLANDATE.Text = Convert.ToString(dr("CHALLANDATE").ToString)
                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                            'Item Grid
                            GRIDRETURN.Rows.Add(dr("SRNO"), dr("QUALITY").ToString, dr("MILLNAME").ToString, dr("LOTNO").ToString, dr("SHADE").ToString, Val(dr("BAGS")), Format(Val(dr("WT")), "0.000"), Val(dr("FRESH")), Val(dr("WINDING")), Val(dr("FIRKA")), Val(dr("NALI")), dr("NARRATION").ToString, Val(dr("OUTWT")), Val(dr("OUTFRESH")), Val(dr("OUTWINDING")), Val(dr("OUTFIRKA")), Val(dr("OUTNALI")))
                        Next
                        TOTAL()
                        getsrno(GRIDRETURN)
                        If GRIDRETURN.RowCount > 0 Then TXTSRNO.Text = Val(GRIDRETURN.RowCount) + 1 Else TXTSRNO.Text = 1

                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search("YRETMACHINE_SRNO AS GRIDSRNO, YRETMACHINE_REMARKS AS REMARKS, YRETMACHINE_NAME AS NAME, YRETMACHINE_PHOTO AS IMGPATH ", "", " YARNRETURNMACHINE_UPLOAD", " AND YRETMACHINE_NO = " & TEMPRETURNNO & " AND YRETMACHINE_YEARID = " & YearId)
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

            alParaval.Add(Format(Convert.ToDateTime(DTRETURNDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(DTCHALLANDATE.Text)
            alParaval.Add(cmbtrans.Text.Trim)

            If FRMSTRING = "RETURNFROMADDA" Then alParaval.Add(Val(TXTADDANO.Text.Trim))

            alParaval.Add(Val(LBLTOTALBAGS.Text.Trim))
            alParaval.Add(Val(LBLTOTALWT.Text.Trim))
            alParaval.Add(Val(LBLTOTALFRESH.Text.Trim))
            alParaval.Add(Val(LBLTOTALWINDING.Text.Trim))
            alParaval.Add(Val(LBLTOTALFIRKA.Text.Trim))
            alParaval.Add(Val(LBLTOTALNALI.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim gridsrno As String = ""
            Dim QUALITY As String = ""
            Dim MILLNAME As String = ""
            Dim LOTNO As String = ""
            Dim SHADE As String = ""
            Dim BAGS As String = ""
            Dim WT As String = ""
            Dim FRESH As String = ""
            Dim WINDING As String = ""
            Dim FIRKA As String = ""
            Dim NALI As String = ""
            Dim NARRATION As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDRETURN.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        SHADE = row.Cells(GSHADE.Index).Value.ToString
                        BAGS = Val(row.Cells(GBAGS.Index).Value)
                        WT = Val(row.Cells(Gwt.Index).Value)
                        FRESH = Val(row.Cells(GFRESH.Index).Value)
                        WINDING = Val(row.Cells(GWINDING.Index).Value)
                        FIRKA = Val(row.Cells(GFIRKA.Index).Value)
                        NALI = Val(row.Cells(gnali.Index).Value)
                        NARRATION = row.Cells(GNARRATION.Index).Value.ToString
                    Else
                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        SHADE = SHADE & "|" & row.Cells(GSHADE.Index).Value.ToString
                        BAGS = BAGS & "|" & Val(row.Cells(GBAGS.Index).Value)
                        WT = WT & "|" & Val(row.Cells(Gwt.Index).Value)
                        FRESH = FRESH & "|" & Val(row.Cells(GFRESH.Index).Value)
                        WINDING = WINDING & "|" & Val(row.Cells(GWINDING.Index).Value)
                        FIRKA = FIRKA & "|" & Val(row.Cells(GFIRKA.Index).Value)
                        NALI = NALI & "|" & Val(row.Cells(gnali.Index).Value)
                        NARRATION = NARRATION & "|" & row.Cells(GNARRATION.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(QUALITY)
            alParaval.Add(MILLNAME)
            alParaval.Add(LOTNO)
            alParaval.Add(SHADE)
            alParaval.Add(BAGS)
            alParaval.Add(WT)
            alParaval.Add(FRESH)
            alParaval.Add(WINDING)
            alParaval.Add(FIRKA)
            alParaval.Add(NALI)
            alParaval.Add(NARRATION)



            If FRMSTRING = "RETURNFROMWARPER" Then
                Dim OBJWARPER As New ClsYarnReturnFromWarper
                OBJWARPER.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJWARPER.save()
                    TEMPRETURNNO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPRETURNNO)
                    IntResult = OBJWARPER.Update()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If

            ElseIf FRMSTRING = "RETURNFROMADDA" Then
                Dim OBJADDA As New ClsYarnReturnFromADDA
                OBJADDA.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJADDA.save()
                    TEMPRETURNNO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPRETURNNO)
                    IntResult = OBJADDA.Update()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If

            ElseIf FRMSTRING = "RETURNFROMSIZER" Then
                Dim OBJSIZER As New ClsYarnReturnFromSizer
                OBJSIZER.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJSIZER.save()
                    TEMPRETURNNO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPRETURNNO)
                    IntResult = OBJSIZER.Update()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If


            ElseIf FRMSTRING = "RETURNFROMWEAVER" Then
                Dim OBJWEAVER As New ClsYarnReturnFromWeaver
                OBJWEAVER.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJWEAVER.save()
                    TEMPRETURNNO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPRETURNNO)
                    IntResult = OBJWEAVER.Update()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If

            ElseIf FRMSTRING = "RETURNFROMDYEING" Then
                Dim OBJDYEING As New ClsYarnReturnFromDyeing
                OBJDYEING.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJDYEING.save()
                    TEMPRETURNNO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPRETURNNO)
                    IntResult = OBJDYEING.Update()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If

            ElseIf FRMSTRING = "RETURNFROMJOBBER" Then
                Dim OBJJOBBER As New ClsYarnReturnFromJobber
                OBJJOBBER.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJJOBBER.save()
                    TEMPRETURNNO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")
                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPRETURNNO)
                    IntResult = OBJJOBBER.Update()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If

            ElseIf FRMSTRING = "RETURNFROMMACHINE" Then
                Dim OBJMACHINE As New ClsYarnReturnFromMachine
                OBJMACHINE.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJMACHINE.save()
                    TEMPRETURNNO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")
                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPRETURNNO)
                    IntResult = OBJMACHINE.UPDATE()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If


            End If

            'PRINTREPORT(TEMPISSUENO)
            If gridupload.RowCount > 0 Then SAVEUPLOAD()

            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            CMBOURGODOWN.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True


        If DTRETURNDATE.Text = "__/__/____" Then
            EP.SetError(DTRETURNDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTRETURNDATE.Text) Then
                EP.SetError(DTRETURNDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If cmbname.Text.Trim.Length = 0 Then
            EP.SetError(cmbname, "Please Select Name")
            bln = False
        End If

        If cmbtrans.Text.Trim.Length = 0 And FRMSTRING <> "RETURNFROMMACHINE" Then
            EP.SetError(cmbtrans, "Please Fill Transport Name")
            bln = False
        End If


        If CMBOURGODOWN.Text.Trim.Length = 0 And cmbname.Text.Trim.Length = 0 Then
            EP.SetError(CMBOURGODOWN, " Please Fill Godown ")
            bln = False
        End If

        If GRIDRETURN.RowCount = 0 Then
            EP.SetError(TXTNARR, "Select Stock")
            bln = False
        End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Rec/Pay/TDS Made, Delete Rec/Pay/TDS First")
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
            If FRMSTRING = "RETURNFROMWARPER" Then
                Dim OBJDO As New ClsYarnReturnFromWarper
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPRETURNNO)
                        ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                        PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                        PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                        ALPARAVAL.Add(MS.ToArray)
                        ALPARAVAL.Add(YearId)

                        OBJDO.alParaval = ALPARAVAL
                        Dim INTRES As Integer = OBJDO.SAVEUPLOAD()
                    End If
                Next

            ElseIf FRMSTRING = "RETURNFROMADDA" Then
                Dim OBJDO As New ClsYarnReturnFromADDA
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPRETURNNO)
                        ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                        PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                        PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                        ALPARAVAL.Add(MS.ToArray)
                        ALPARAVAL.Add(YearId)

                        OBJDO.alParaval = ALPARAVAL
                        Dim INTRES As Integer = OBJDO.SAVEUPLOAD()
                    End If
                Next

            ElseIf FRMSTRING = "RETURNFROMSIZER" Then
                Dim OBJDO As New ClsYarnReturnFromSizer
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPRETURNNO)
                        ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                        PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                        PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                        ALPARAVAL.Add(MS.ToArray)
                        ALPARAVAL.Add(YearId)

                        OBJDO.alParaval = ALPARAVAL
                        Dim INTRES As Integer = OBJDO.SAVEUPLOAD()
                    End If
                Next

            ElseIf FRMSTRING = "RETURNFROMWEAVER" Then
                Dim OBJWEAVER As New ClsYarnReturnFromWeaver
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPRETURNNO)
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

            ElseIf FRMSTRING = "RETURNFROMDYEING" Then
                Dim OBJDYEING As New ClsYarnReturnFromDyeing
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPRETURNNO)
                        ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                        PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                        PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                        ALPARAVAL.Add(MS.ToArray)
                        ALPARAVAL.Add(YearId)

                        OBJDYEING.alParaval = ALPARAVAL
                        Dim INTRES As Integer = OBJDYEING.SAVEUPLOAD()
                    End If
                Next

            ElseIf FRMSTRING = "RETURNFROMJOBBER" Then
                Dim OBJJOBBER As New ClsYarnReturnFromJobber
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPRETURNNO)
                        ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                        PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                        PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                        ALPARAVAL.Add(MS.ToArray)
                        ALPARAVAL.Add(YearId)

                        OBJJOBBER.alParaval = ALPARAVAL
                        Dim INTRES As Integer = OBJJOBBER.SAVEUPLOAD()
                    End If
                Next
            ElseIf FRMSTRING = "RETURNFROMMACHINE" Then
                Dim OBJJOBBER As New ClsYarnReturnFromMachine
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPRETURNNO)
                        ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                        ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                        PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                        PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                        ALPARAVAL.Add(MS.ToArray)
                        ALPARAVAL.Add(YearId)

                        OBJJOBBER.alParaval = ALPARAVAL
                        Dim INTRES As Integer = OBJJOBBER.SAVEUPLOAD()
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

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If FRMSTRING = "RETURNFROMWARPER" Or FRMSTRING = "RETURNFROMADDA" Then
                If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')") ''''TYPE = WARPER
            ElseIf FRMSTRING = "RETURNFROMSIZER" Then
                If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'") ''''TYPE = SIZER
            ElseIf FRMSTRING = "RETURNFROMWEAVER" Then
                If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'") ''''TYPE = WEAVER
            ElseIf FRMSTRING = "RETURNFROMDYEING" Then
                If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'") ''''TYPE = PROCESSOR
            ElseIf FRMSTRING = "RETURNFROMJOBBER" Then
                If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'") ''''TYPE = JOBBER
            ElseIf FRMSTRING = "RETURNFROMMACHINE" Then
                If cmbname.Text.Trim = "" Then FILLMACHINE(cmbname)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If FRMSTRING = "RETURNFROMWARPER" Or FRMSTRING = "RETURNFROMADDA" Then
                If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WARPER")
            ElseIf FRMSTRING = "RETURNFROMSIZER" Then
                If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "SIZER")
            ElseIf FRMSTRING = "RETURNFROMWEAVER" Then
                If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WEAVER")
            ElseIf FRMSTRING = "RETURNFROMDYEING" Then
                If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
            ElseIf FRMSTRING = "RETURNFROMJOBBER" Then
                If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "JOBBER")
            ElseIf FRMSTRING = "RETURNFROMMACHINE" Then
                If cmbname.Text.Trim <> "" Then MACHINEVALIDATE(cmbname, e, Me)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                If FRMSTRING = "RETURNFROMWARPER" Or FRMSTRING = "RETURNFROMADDA" Then
                    OBJLEDGER.STRSEARCH = " And GroupMaster.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR  LEDGERS.ACC_SUBTYPE = 'SIZER')"
                ElseIf FRMSTRING = "RETURNFROMSIZER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'"
                ElseIf FRMSTRING = "RETURNFROMWEAVER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'"
                ElseIf FRMSTRING = "RETURNFROMDYEING" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                ElseIf FRMSTRING = "RETURNFROMJOBBER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'"
                ElseIf FRMSTRING = "RETURNFROMMACHINE" Then
                    Exit Sub
                End If
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJYARN As New SelectQuality
                OBJYARN.ShowDialog()
                If OBJYARN.TEMPNAME <> "" Then CMBQUALITY.Text = OBJYARN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMILLNAME.Enter
        Try
            If CMBMILLNAME.Text.Trim = "" Then fillname(CMBMILLNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBMILLNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then cmbcode.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILLNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILLNAME.Validating
        Try
            If CMBMILLNAME.Text.Trim <> "" Then namevalidate(CMBMILLNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "MILL")
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
            LBLTOTALBAGS.Text = 0
            LBLTOTALWT.Text = 0.0
            LBLTOTALFRESH.Text = 0.0
            LBLTOTALWINDING.Text = 0.0
            LBLTOTALFIRKA.Text = 0.0
            LBLTOTALNALI.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDRETURN.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTALBAGS.Text = Format(Val(LBLTOTALBAGS.Text) + Val(ROW.Cells(GBAGS.Index).EditedFormattedValue), "0")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(Gwt.Index).EditedFormattedValue), "0.000")
                    LBLTOTALFRESH.Text = Format(Val(LBLTOTALFRESH.Text) + Val(ROW.Cells(GFRESH.Index).EditedFormattedValue), "0")
                    LBLTOTALWINDING.Text = Format(Val(LBLTOTALWINDING.Text) + Val(ROW.Cells(GWINDING.Index).EditedFormattedValue), "0")
                    LBLTOTALFIRKA.Text = Format(Val(LBLTOTALFIRKA.Text) + Val(ROW.Cells(GFIRKA.Index).EditedFormattedValue), "0")
                    LBLTOTALNALI.Text = Format(Val(LBLTOTALNALI.Text) + Val(ROW.Cells(gnali.Index).EditedFormattedValue), "0")

                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDRETURN.RowCount = 0
LINE1:
            TEMPRETURNNO = Val(TXTRETURNNO.Text) - 1
Line2:
            If TEMPRETURNNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                If FRMSTRING = "RETURNFROMWARPER" Then
                    DT = OBJCMN.search("YRETWARPER_NO ", "", "  YARNRETURNWARPER ", " AND YRETWARPER_NO = '" & TEMPRETURNNO & "' AND YARNRETURNWARPER.YRETWARPER_YEARID = " & YearId)
                ElseIf FRMSTRING = "RETURNFROMADDA" Then
                    DT = OBJCMN.search("YRETADDA_NO ", "", "  YARNRETURNADDA ", " AND YRETADDA_NO = '" & TEMPRETURNNO & "' AND YARNRETURNADDA.YRETADDA_YEARID = " & YearId)
                ElseIf FRMSTRING = "RETURNFROMSIZER" Then
                    DT = OBJCMN.search("YRETSIZER_NO ", "", "  YARNRETURNSIZER ", " AND YRETSIZER_NO = '" & TEMPRETURNNO & "' AND YARNRETURNSIZER.YRETSIZER_YEARID = " & YearId)
                ElseIf FRMSTRING = "RETURNFROMWEAVER" Then
                    DT = OBJCMN.search("YRETWEAVER_NO ", "", "  YARNRETURNWEAVER ", " AND YRETWEAVER_NO = '" & TEMPRETURNNO & "' AND YARNRETURNWEAVER.YRETWEAVER_YEARID = " & YearId)
                ElseIf FRMSTRING = "RETURNFROMDYEING" Then
                    DT = OBJCMN.search("YRETDYEING_NO ", "", "  YARNRETURNDYEING ", " AND YRETDYEING_NO = '" & TEMPRETURNNO & "' AND YARNRETURNDYEING.YRETDYEING_YEARID = " & YearId)
                ElseIf FRMSTRING = "RETURNFROMJOBBER" Then
                    DT = OBJCMN.search("YRETJOBBER_NO ", "", "  YARNRETURNJOBBER ", " AND YRETJOBBER_NO = '" & TEMPRETURNNO & "' AND YARNRETURNJOBBER.YRETJOBBER_YEARID = " & YearId)
                ElseIf FRMSTRING = "RETURNFROMMACHINE" Then
                    DT = OBJCMN.search("YRETMACHINE_NO ", "", "  YARNRETURNMACHINE ", " AND YRETMACHINE_NO = '" & TEMPRETURNNO & "' AND YARNRETURNMACHINE.YRETMACHINE_YEARID = " & YearId)

                End If
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    Yarn_Return_Load(sender, e)
                Else
                    TEMPRETURNNO = Val(TEMPRETURNNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDRETURN.RowCount = 0 And TEMPRETURNNO > 1 Then
                TXTRETURNNO.Text = TEMPRETURNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDRETURN.RowCount = 0
LINE1:
            TEMPRETURNNO = Val(TXTRETURNNO.Text) + 1
            GETMAX_RETURN_NO()
            Dim MAXNO As Integer = TXTRETURNNO.Text.Trim
            CLEAR()
            If Val(TXTRETURNNO.Text) - 1 >= TEMPRETURNNO Then
                EDIT = True
                Yarn_Return_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDRETURN.RowCount = 0 And TEMPRETURNNO < MAXNO Then
                TXTRETURNNO.Text = TEMPRETURNNO
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
                GRIDRETURN.RowCount = 0
                TEMPRETURNNO = Val(tstxtbillno.Text)
                If TEMPRETURNNO > 0 Then
                    EDIT = True
                    Yarn_Return_Load(sender, e)
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

    Private Sub GRIDRETURN_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDRETURN.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDRETURN.Item(gsrno.Index, e.RowIndex).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDRETURN.Item(gsrno.Index, e.RowIndex).Value
                CMBQUALITY.Text = GRIDRETURN.Item(GQUALITY.Index, e.RowIndex).Value
                CMBMILLNAME.Text = GRIDRETURN.Item(GMILLNAME.Index, e.RowIndex).Value
                TXTLOTNO.Text = GRIDRETURN.Item(GLOTNO.Index, e.RowIndex).Value
                CMBCOLOR.Text = GRIDRETURN.Item(GSHADE.Index, e.RowIndex).Value
                TXTBAGS.Text = Format(Val(GRIDRETURN.Item(GBAGS.Index, e.RowIndex).Value), "0")
                TXTWT.Text = Format(Val(GRIDRETURN.Item(Gwt.Index, e.RowIndex).Value), "0.000")
                TXTFRESH.Text = Val(GRIDRETURN.Item(GFRESH.Index, e.RowIndex).Value)
                TXTWINDING.Text = Val(GRIDRETURN.Item(GWINDING.Index, e.RowIndex).Value)
                TXTFIRKA.Text = Val(GRIDRETURN.Item(GFIRKA.Index, e.RowIndex).Value)
                TXTNALI.Text = Val(GRIDRETURN.Item(gnali.Index, e.RowIndex).Value)
                TXTNARR.Text = GRIDRETURN.Item(GNARRATION.Index, e.RowIndex).Value

                TEMPROW = e.RowIndex
                CMBQUALITY.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDRETURN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDRETURN.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDRETURN.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDRETURN.Rows.RemoveAt(GRIDRETURN.CurrentRow.Index)
                getsrno(GRIDRETURN)
                TOTAL()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJYARN As New YarnReturnDetails
            OBJYARN.MdiParent = MDIMain
            OBJYARN.FRMSTRING = FRMSTRING
            OBJYARN.Show()
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
                TEMPMSG = MsgBox("Delete Yarn Return?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPRETURNNO)
                    alParaval.Add(YearId)

                    If FRMSTRING = "RETURNFROMWARPER" Then
                        Dim ClsDO As New ClsYarnReturnFromWarper
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "RETURNFROMADDA" Then
                        Dim ClsDO As New ClsYarnReturnFromADDA
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "RETURNFROMSIZER" Then
                        Dim ClsDO As New ClsYarnReturnFromSizer
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "RETURNFROMWEAVER" Then
                        Dim ClsDO As New ClsYarnReturnFromWeaver
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "RETURNFROMDYEING" Then
                        Dim ClsDO As New ClsYarnReturnFromDyeing
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "RETURNFROMJOBBER" Then
                        Dim ClsDO As New ClsYarnReturnFromJobber
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "RETURNFROMMACHINE" Then
                        Dim ClsDO As New ClsYarnReturnFromMachine
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    End If

                    MsgBox("Yarn Issue Deleted")
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

    Private Sub DTRETURNDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTRETURNDATE.GotFocus
        DTRETURNDATE.SelectAll()
    End Sub

    Private Sub DTCHALLANDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTCHALLANDATE.GotFocus
        DTRETURNDATE.SelectAll()
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress
        numdot3(e, TXTWT, Me)
    End Sub

    Private Sub TXT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTFRESH.KeyPress, TXTWINDING.KeyPress, TXTFIRKA.KeyPress, TXTNALI.KeyPress, TXTBAGS.KeyPress, TXTADDANO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTNARR_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNARR.Validating
        If CMBQUALITY.Text.Trim <> "" And Val(TXTWT.Text.Trim) > 0 Then FILLGRID() Else MsgBox("Please Enter proper details")
    End Sub

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDRETURN.Rows.Add(Val(TXTSRNO.Text.Trim), CMBQUALITY.Text.Trim, CMBMILLNAME.Text.Trim, TXTLOTNO.Text.Trim, CMBCOLOR.Text.Trim, Val(TXTBAGS.Text.Trim), Format(Val(TXTWT.Text.Trim), "0.000"), Val(TXTFRESH.Text.Trim), Val(TXTWINDING.Text.Trim), Val(TXTFIRKA.Text.Trim), Val(TXTNALI.Text.Trim), TXTNARR.Text.Trim)
            Else
                GRIDRETURN.Item(gsrno.Index, TEMPROW).Value = TXTSRNO.Text.Trim
                GRIDRETURN.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
                GRIDRETURN.Item(GMILLNAME.Index, TEMPROW).Value = CMBMILLNAME.Text.Trim
                GRIDRETURN.Item(GLOTNO.Index, TEMPROW).Value = TXTLOTNO.Text.Trim
                GRIDRETURN.Item(GSHADE.Index, TEMPROW).Value = CMBCOLOR.Text.Trim
                GRIDRETURN.Item(GBAGS.Index, TEMPROW).Value = Format(Val(TXTBAGS.Text.Trim), "0")
                GRIDRETURN.Item(Gwt.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.000")
                GRIDRETURN.Item(GFRESH.Index, TEMPROW).Value = Val(TXTFRESH.Text.Trim)
                GRIDRETURN.Item(GWINDING.Index, TEMPROW).Value = Val(TXTWINDING.Text.Trim)
                GRIDRETURN.Item(GFIRKA.Index, TEMPROW).Value = Val(TXTFIRKA.Text.Trim)
                GRIDRETURN.Item(gnali.Index, TEMPROW).Value = Val(TXTNALI.Text.Trim)
                GRIDRETURN.Item(GNARRATION.Index, TEMPROW).Value = TXTNARR.Text.Trim

                GRIDDOUBLECLICK = False
            End If
            'TXTSRNO.Clear()
            CMBQUALITY.Text = ""
            CMBMILLNAME.Text = ""
            TXTLOTNO.Clear()
            CMBCOLOR.Text = ""
            TXTBAGS.Clear()
            TXTWT.Clear()
            TXTFRESH.Clear()
            TXTWINDING.Clear()
            TXTFIRKA.Clear()
            TXTNALI.Clear()
            TXTNARR.Clear()
            getsrno(GRIDRETURN)
            TOTAL()
            CMBQUALITY.Focus()
            If GRIDRETURN.RowCount > 0 Then TXTSRNO.Text = Val(GRIDRETURN.RowCount) + 1 Else TXTSRNO.Text = 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTRETURNDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTRETURNDATE.Validating
        Try
            If DTRETURNDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTRETURNDATE.Text, TEMP) Then
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

    Private Sub Yarn_Return_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ALLOWMFG = False Then Exit Sub
        TXTFRESH.BackColor = Color.White
        TXTWINDING.BackColor = Color.White
        TXTFIRKA.BackColor = Color.White
        TXTNALI.BackColor = Color.White
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, "AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'AND LEDGERS.ACC_TYPE= 'TRANSPORT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then namevalidate(cmbtrans, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'", "Sundry Creditors", "TRANSPORT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtrans.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbtrans.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCOLOR.Enter
        Try
            If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCOLOR.Validating
        Try
            If CMBCOLOR.Text.Trim <> "" Then COLORVALIDATE(CMBCOLOR, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class