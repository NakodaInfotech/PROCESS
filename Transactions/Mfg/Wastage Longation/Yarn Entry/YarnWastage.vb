
Imports BL

Public Class YarnWastage

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
        txtremarks.Clear()

        LBLTOTALWT.Text = 0.0

        TXTSRNO.Text = 1
        CMBTYPE.SelectedIndex = 0
        CMBQUALITY.Text = ""
        CMBMILLNAME.Text = ""
        TXTTAKA.Clear()
        TXTSTOCKWT.Clear()
        TXTACTUALWT.Clear()
        TXTWT.Clear()
        TXTNARR.Clear()

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
        If FRMSTRING = "WASTAGEWARPER" Then
            DTTABLE = getmax("ISNULL(MAX(YWASWARPER_NO),0)+ 1", "YARNWASTAGEWARPER", "AND YWASWARPER_YEARID = " & YearId)
        ElseIf FRMSTRING = "WASTAGESIZER" Then
            DTTABLE = getmax("ISNULL(MAX(YWASSIZER_NO),0)+ 1", "YARNWASTAGESIZER", "AND YWASSIZER_YEARID = " & YearId)
        ElseIf FRMSTRING = "WASTAGEWEAVER" Then
            DTTABLE = getmax("ISNULL(MAX(YWASWEAVER_NO),0)+ 1", "YARNWASTAGEWEAVER", "AND YWASWEAVER_YEARID = " & YearId)
        ElseIf FRMSTRING = "WASTAGEDYEING" Then
            DTTABLE = getmax("ISNULL(MAX(YWASDYEING_NO),0)+ 1", "YARNWASTAGEDYEING", "AND YWASDYEING_YEARID = " & YearId)
        ElseIf FRMSTRING = "WASTAGEGODOWN" Then
            DTTABLE = getmax("ISNULL(MAX(YWASGODOWN_NO),0)+ 1", "YARNWASTAGEGODOWN", "AND YWASGODOWN_YEARID = " & YearId)
        ElseIf FRMSTRING = "WASTAGEJOBBER" Then
            DTTABLE = getmax("ISNULL(MAX(YWASJOBBER_NO),0)+ 1", "YARNWASTAGEJOBBER", "AND YWASJOBBER_YEARID = " & YearId)
        ElseIf FRMSTRING = "WASTAGEMACHINE" Then
            DTTABLE = getmax("ISNULL(MAX(YWASMACHINE_NO),0)+ 1", "YARNWASTAGEMACHINE", "AND YWASMACHINE_YEARID = " & YearId)
        End If
        If DTTABLE.Rows.Count > 0 Then TXTWASTAGENO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub YarnWastage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

        If FRMSTRING = "WASTAGEWARPER" Then
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'") ''''TYPE = WARPER
        ElseIf FRMSTRING = "WASTAGESIZER" Then
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'") ''''TYPE = SIZER
        ElseIf FRMSTRING = "WASTAGEWEAVER" Then
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'") ''''TYPE = WEAVER
        ElseIf FRMSTRING = "WASTAGEDYEING" Then
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'") ''''TYPE = PROCESSOR
        ElseIf FRMSTRING = "WASTAGEJOBBER" Then
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'") ''''TYPE = JOBBER
        ElseIf FRMSTRING = "WASTAGEMACHINE" Then
            If CMBNAME.Text.Trim = "" Then FILLMACHINE(CMBNAME)
        End If
        If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR = 'True'")
        If CMBMILLNAME.Text = "" Then fillname(CMBMILLNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'MILL'")
        If CMBQUALITY.Text = "" Then fillQUALITY(CMBQUALITY, EDIT)
    End Sub

    Private Sub YarnWastage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)


            If FRMSTRING = "WASTAGEWARPER" Then
                LBLNAME.Text = "Warper Name"
                Me.Text = "Yarn Wastage From Warper"
            ElseIf FRMSTRING = "WASTAGESIZER" Then
                LBLNAME.Text = "Sizer Name"
                Me.Text = "Yarn Wastage From Sizer"
                LBLTILDATE.Visible = True
                TILLDATE.Visible = True

            ElseIf FRMSTRING = "WASTAGEWEAVER" Then
                LBLNAME.Text = "Weaver Name"
                Me.Text = "Yarn Wastage From Weaver"
                LBLFROMDATE.Visible = True
                FROMDATE.Visible = True
                LBLTILDATE.Visible = True
                TILLDATE.Visible = True

            ElseIf FRMSTRING = "WASTAGEDYEING" Then
                LBLNAME.Text = "Dyeing Name"
                Me.Text = "Yarn Wastage From Dyeing"
            ElseIf FRMSTRING = "WASTAGEGODOWN" Then
                LBLNAME.Visible = False
                CMBNAME.Visible = False
                LBLGODOWN.Visible = True
                CMBOURGODOWN.Visible = True
                Me.Text = "Yarn Wastage at Godown"
            ElseIf FRMSTRING = "WASTAGEJOBBER" Then
                LBLNAME.Text = "Jobber Name"
                Me.Text = "Yarn Wastage From Jobber"
            ElseIf FRMSTRING = "WASTAGEMACHINE" Then
                LBLNAME.Text = "Machine Name"
                Me.Text = "Yarn Wastage From Machine"
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
                If FRMSTRING = "WASTAGEWARPER" Then

                    Dim OBJWAS As New ClsYarnWastageWarper
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
                            GRIDWASTAGE.Rows.Add(dr("SRNO"), dr("TYPE"), dr("QUALITY").ToString, dr("MILLNAME").ToString, Val(dr("TAKA")), Format(Val(dr("STOCKWT")), "0.000"), Format(Val(dr("ACTUALWT")), "0.000"), Format(Val(dr("WT")), "0.000"), dr("NARRATION").ToString, Val(dr("OUTWT")))
                        Next
                        TOTAL()
                        getsrno(GRIDWASTAGE)
                        If GRIDWASTAGE.RowCount > 0 Then TXTSRNO.Text = Val(GRIDWASTAGE.RowCount) + 1 Else TXTSRNO.Text = 1

                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" YWASWARPER_SRNO AS GRIDSRNO, YWASWARPER_REMARKS AS REMARKS, YWASWARPER_NAME AS NAME, YWASWARPER_PHOTO AS IMGPATH ", "", " YARNWASTAGEWARPER_UPLOAD", " AND YWASWARPER_NO = " & TEMPWASTAGENO & " AND YWASWARPER_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If

                    End If

                ElseIf FRMSTRING = "WASTAGESIZER" Then

                    Dim OBJWAS As New ClsYarnWastageSizer
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
                            GRIDWASTAGE.Rows.Add(dr("SRNO"), dr("TYPE"), dr("QUALITY").ToString, dr("MILLNAME").ToString, Val(dr("TAKA")), Format(Val(dr("STOCKWT")), "0.000"), Format(Val(dr("ACTUALWT")), "0.000"), Format(Val(dr("WT")), "0.000"), dr("NARRATION").ToString, Val(dr("OUTWT")))
                        Next
                        TOTAL()
                        getsrno(GRIDWASTAGE)
                        If GRIDWASTAGE.RowCount > 0 Then TXTSRNO.Text = Val(GRIDWASTAGE.RowCount) + 1 Else TXTSRNO.Text = 1


                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" YWASSIZER_SRNO AS GRIDSRNO, YWASSIZER_REMARKS AS REMARKS, YWASSIZER_NAME AS NAME, YWASSIZER_PHOTO AS IMGPATH ", "", " YARNWASTAGESIZER_UPLOAD", " AND YWASSIZER_NO = " & TEMPWASTAGENO & " AND YWASSIZER_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If
                    End If

                ElseIf FRMSTRING = "WASTAGEWEAVER" Then

                    Dim OBJWAS As New ClsYarnWastageWeaver
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
                            GRIDWASTAGE.Rows.Add(dr("SRNO"), dr("TYPE"), dr("QUALITY").ToString, dr("MILLNAME").ToString, Val(dr("TAKA")), Format(Val(dr("STOCKWT")), "0.000"), Format(Val(dr("ACTUALWT")), "0.000"), Format(Val(dr("WT")), "0.000"), dr("NARRATION").ToString, Val(dr("OUTWT")))
                        Next
                        TOTAL()
                        getsrno(GRIDWASTAGE)
                        If GRIDWASTAGE.RowCount > 0 Then TXTSRNO.Text = Val(GRIDWASTAGE.RowCount) + 1 Else TXTSRNO.Text = 1


                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" YWASWEAVER_SRNO AS GRIDSRNO, YWASWEAVER_REMARKS AS REMARKS, YWASWEAVER_NAME AS NAME, YWASWEAVER_PHOTO AS IMGPATH ", "", " YARNWASTAGEWEAVER_UPLOAD", " AND YWASWEAVER_NO = " & TEMPWASTAGENO & " AND YWASWEAVER_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If
                    End If

                ElseIf FRMSTRING = "WASTAGEDYEING" Then

                    Dim OBJWAS As New ClsYarnWastageDyeing
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
                            GRIDWASTAGE.Rows.Add(dr("SRNO"), dr("TYPE"), dr("QUALITY").ToString, dr("MILLNAME").ToString, Val(dr("TAKA")), Format(Val(dr("STOCKWT")), "0.000"), Format(Val(dr("ACTUALWT")), "0.000"), Format(Val(dr("WT")), "0.000"), dr("NARRATION").ToString, Val(dr("OUTWT")))
                        Next
                        TOTAL()
                        getsrno(GRIDWASTAGE)
                        If GRIDWASTAGE.RowCount > 0 Then TXTSRNO.Text = Val(GRIDWASTAGE.RowCount) + 1 Else TXTSRNO.Text = 1


                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" YWASDYEING_SRNO AS GRIDSRNO, YWASDYEING_REMARKS AS REMARKS, YWASDYEING_NAME AS NAME, YWASDYEING_PHOTO AS IMGPATH ", "", " YARNWASTAGEDYEING_UPLOAD", " AND YWASDYEING_NO = " & TEMPWASTAGENO & " AND YWASDYEING_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If
                    End If

                ElseIf FRMSTRING = "WASTAGEGODOWN" Then

                    Dim OBJWAS As New ClsYarnWastageGodown
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
                            GRIDWASTAGE.Rows.Add(dr("SRNO"), dr("TYPE"), dr("QUALITY").ToString, dr("MILLNAME").ToString, Val(dr("TAKA")), Format(Val(dr("STOCKWT")), "0.000"), Format(Val(dr("ACTUALWT")), "0.000"), Format(Val(dr("WT")), "0.000"), dr("NARRATION").ToString, Val(dr("OUTWT")))
                        Next
                        TOTAL()
                        getsrno(GRIDWASTAGE)
                        If GRIDWASTAGE.RowCount > 0 Then TXTSRNO.Text = Val(GRIDWASTAGE.RowCount) + 1 Else TXTSRNO.Text = 1


                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" YWASWEAVER_SRNO AS GRIDSRNO, YWASWEAVER_REMARKS AS REMARKS, YWASWEAVER_NAME AS NAME, YWASWEAVER_PHOTO AS IMGPATH ", "", " YARNWASTAGEWEAVER_UPLOAD", " AND YWASWEAVER_NO = " & TEMPWASTAGENO & " AND YWASWEAVER_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If
                    End If

                ElseIf FRMSTRING = "WASTAGEJOBBER" Then

                    Dim OBJWAS As New ClsYarnWastageJobber
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
                            GRIDWASTAGE.Rows.Add(dr("SRNO"), dr("TYPE"), dr("QUALITY").ToString, dr("MILLNAME").ToString, Val(dr("TAKA")), Format(Val(dr("STOCKWT")), "0.000"), Format(Val(dr("ACTUALWT")), "0.000"), Format(Val(dr("WT")), "0.000"), dr("NARRATION").ToString, Val(dr("OUTWT")))
                        Next
                        TOTAL()
                        getsrno(GRIDWASTAGE)
                        If GRIDWASTAGE.RowCount > 0 Then TXTSRNO.Text = Val(GRIDWASTAGE.RowCount) + 1 Else TXTSRNO.Text = 1


                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" YWASWEAVER_SRNO AS GRIDSRNO, YWASWEAVER_REMARKS AS REMARKS, YWASWEAVER_NAME AS NAME, YWASWEAVER_PHOTO AS IMGPATH ", "", " YARNWASTAGEWEAVER_UPLOAD", " AND YWASWEAVER_NO = " & TEMPWASTAGENO & " AND YWASWEAVER_YEARID = " & YearId)
                        If dttable.Rows.Count > 0 Then
                            For Each DTR As DataRow In dttable.Rows
                                gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                            Next
                        End If
                    End If

                ElseIf FRMSTRING = "WASTAGEMACHINE" Then

                    Dim OBJWAS As New ClsYarnWastageMachine
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
                            GRIDWASTAGE.Rows.Add(dr("SRNO"), dr("TYPE"), dr("QUALITY").ToString, dr("MILLNAME").ToString, Val(dr("TAKA")), Format(Val(dr("STOCKWT")), "0.000"), Format(Val(dr("ACTUALWT")), "0.000"), Format(Val(dr("WT")), "0.000"), dr("NARRATION").ToString, Val(dr("OUTWT")))
                        Next
                        TOTAL()
                        getsrno(GRIDWASTAGE)
                        If GRIDWASTAGE.RowCount > 0 Then TXTSRNO.Text = Val(GRIDWASTAGE.RowCount) + 1 Else TXTSRNO.Text = 1


                        Dim OBJCMN As New ClsCommon
                        dttable = OBJCMN.search(" YWASMACHINE_SRNO AS GRIDSRNO, YWASMACHINE_REMARKS AS REMARKS, YWASMACHINE_NAME AS NAME, YWASMACHINE_PHOTO AS IMGPATH ", "", " YARNWASTAGEMACHINE_UPLOAD", " AND YWASMACHINE_NO = " & TEMPWASTAGENO & " AND YWASMACHINE_YEARID = " & YearId)
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
            alParaval.Add(FROMDATE.Text)
            alParaval.Add(TILLDATE.Text)
            alParaval.Add(Val(LBLTOTALWT.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim GRIDSRNO As String = ""
            Dim TYPE As String = ""
            Dim QUALITY As String = ""
            Dim MILLNAME As String = ""
            Dim TAKA As String = ""
            Dim STOCKWT As String = ""
            Dim ACTUALWT As String = ""
            Dim WT As String = ""
            Dim NARRATION As String = ""
            Dim OUTWT As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDWASTAGE.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = row.Cells(gsrno.Index).Value.ToString
                        TYPE = row.Cells(GTYPE.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        TAKA = Val(row.Cells(GTAKA.Index).Value)
                        STOCKWT = Val(row.Cells(GSTOCKWT.Index).Value)
                        ACTUALWT = Val(row.Cells(GACTUALWT.Index).Value)
                        WT = Val(row.Cells(Gwt.Index).Value)
                        NARRATION = row.Cells(GNARRATION.Index).Value.ToString
                        OUTWT = Val(row.Cells(GOUTWT.Index).Value)



                    Else

                        GRIDSRNO = GRIDSRNO & "|" & row.Cells(gsrno.Index).Value
                        TYPE = TYPE & "|" & row.Cells(GTYPE.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        TAKA = TAKA & "|" & Val(row.Cells(GTAKA.Index).Value)
                        STOCKWT = STOCKWT & "|" & Val(row.Cells(GSTOCKWT.Index).Value)
                        ACTUALWT = ACTUALWT & "|" & Val(row.Cells(GACTUALWT.Index).Value)
                        WT = WT & "|" & Val(row.Cells(Gwt.Index).Value)
                        NARRATION = NARRATION & "|" & row.Cells(GNARRATION.Index).Value.ToString
                        OUTWT = OUTWT & "|" & Val(row.Cells(GOUTWT.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(GRIDSRNO)
            alParaval.Add(TYPE)
            alParaval.Add(QUALITY)
            alParaval.Add(MILLNAME)
            alParaval.Add(TAKA)
            alParaval.Add(STOCKWT)
            alParaval.Add(ACTUALWT)
            alParaval.Add(WT)
            alParaval.Add(NARRATION)
            alParaval.Add(OUTWT)



            If FRMSTRING = "WASTAGEWARPER" Then
                Dim OBJWARPER As New ClsYarnWastageWarper
                OBJWARPER.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJWARPER.SAVE()
                    TEMPWASTAGENO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPWASTAGENO)
                    IntResult = OBJWARPER.UPDATE()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If

            ElseIf FRMSTRING = "WASTAGESIZER" Then
                Dim OBJSIZER As New ClsYarnWastageSizer
                OBJSIZER.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJSIZER.SAVE()
                    TEMPWASTAGENO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPWASTAGENO)
                    IntResult = OBJSIZER.UPDATE()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If


            ElseIf FRMSTRING = "WASTAGEWEAVER" Then
                Dim OBJWEAVER As New ClsYarnWastageWeaver
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
                    IntResult = OBJWEAVER.Update()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If


            ElseIf FRMSTRING = "WASTAGEDYEING" Then
                Dim OBJDYEING As New ClsYarnWastageDyeing
                OBJDYEING.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJDYEING.save()
                    TEMPWASTAGENO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPWASTAGENO)
                    IntResult = OBJDYEING.Update()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If


            ElseIf FRMSTRING = "WASTAGEJOBBER" Then
                Dim OBJWEAVER As New ClsYarnWastageJobber
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

            ElseIf FRMSTRING = "WASTAGEGODOWN" Then
                Dim OBJWEAVER As New ClsYarnWastageGodown
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

            ElseIf FRMSTRING = "WASTAGEMACHINE" Then
                Dim OBJWEAVER As New ClsYarnWastageMachine
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
            If FRMSTRING = "WASTAGEWARPER" Then
                Dim OBJDO As New ClsYarnWastageWarper
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

                        OBJDO.alParaval = ALPARAVAL
                        Dim INTRES As Integer = OBJDO.SAVEUPLOAD()
                    End If
                Next

            ElseIf FRMSTRING = "WASTAGESIZER" Then
                Dim OBJDO As New ClsYarnWastageSizer
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

                        OBJDO.alParaval = ALPARAVAL
                        Dim INTRES As Integer = OBJDO.SAVEUPLOAD()
                    End If
                Next

            ElseIf FRMSTRING = "WASTAGEWEAVER" Then
                Dim OBJWEAVER As New ClsYarnWastageWeaver
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

            ElseIf FRMSTRING = "WASTAGEDYEING" Then
                Dim OBJDYEING As New ClsYarnWastageDyeing
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

                        OBJDYEING.alParaval = ALPARAVAL
                        Dim INTRES As Integer = OBJDYEING.SAVEUPLOAD()
                    End If
                Next

            ElseIf FRMSTRING = "WASTAGEJOBBER" Then
                Dim OBJWEAVER As New ClsYarnWastageJobber
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
                Dim OBJWEAVER As New ClsYarnWastageGodown
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

            ElseIf FRMSTRING = "WASTAGEMACHINE" Then
                Dim OBJWEAVER As New ClsYarnWastageMachine
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
            If FRMSTRING = "WASTAGEWARPER" Then
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'") ''''TYPE = WARPER
            ElseIf FRMSTRING = "WASTAGESIZER" Then
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'") ''''TYPE = SIZER
            ElseIf FRMSTRING = "WASTAGEWEAVER" Then
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'") ''''TYPE = WEAVER
            ElseIf FRMSTRING = "WASTAGEDYEING" Then
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'") ''''TYPE = processor
            ElseIf FRMSTRING = "WASTAGEJOBBER" Then
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'") ''''TYPE = JOBBER
            ElseIf FRMSTRING = "WASTAGEMACHINE" Then
                If CMBNAME.Text.Trim = "" Then FILLMACHINE(CMBNAME)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If FRMSTRING = "WASTAGEWARPER" Then
                If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WARPER")
            ElseIf FRMSTRING = "WASTAGESIZER" Then
                If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "SIZER")
            ElseIf FRMSTRING = "WASTAGEWEAVER" Then
                If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WEAVER")
            ElseIf FRMSTRING = "WASTAGEDYEING" Then
                If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
            ElseIf FRMSTRING = "WASTAGEJOBBER" Then
                If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "JOBBER")
            ElseIf FRMSTRING = "WASTAGEMACHINE" Then
                If cmbname.Text.Trim <> "" Then MACHINEVALIDATE(cmbname, e, Me)

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
                If FRMSTRING = "WASTAGEWARPER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'"
                ElseIf FRMSTRING = "WASTAGESIZER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'"
                ElseIf FRMSTRING = "WASTAGEWEAVER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'"
                ElseIf FRMSTRING = "WASTAGEDYEING" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                ElseIf FRMSTRING = "WASTAGEJOBBER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'"
                ElseIf FRMSTRING = "WASTAGEMACHINE" Then
                    Exit Sub
                End If
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
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
            LBLTOTALWT.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDWASTAGE.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    If Val(ROW.Cells(GSTOCKWT.Index).EditedFormattedValue) <> 0 And Val(ROW.Cells(GACTUALWT.Index).EditedFormattedValue) <> 0 Then ROW.Cells(Gwt.Index).Value = Format(Val(ROW.Cells(GSTOCKWT.Index).EditedFormattedValue) - Val(ROW.Cells(GACTUALWT.Index).EditedFormattedValue), "0.000")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(Gwt.Index).EditedFormattedValue), "0.000")
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
                If FRMSTRING = "WASTAGEWARPER" Then
                    DT = OBJCMN.search("YWASWARPER_NO ", "", "  YARNWASTAGEWARPER ", " AND YWASWARPER_NO = '" & TEMPWASTAGENO & "' AND YARNWASTAGEWARPER.YWASWARPER_YEARID = " & YearId)
                ElseIf FRMSTRING = "WASTAGESIZER" Then
                    DT = OBJCMN.search("YWASSIZER_NO ", "", "  YARNWASTAGESIZER ", " AND YWASSIZER_NO = '" & TEMPWASTAGENO & "' AND YARNWASTAGESIZER.YWASSIZER_YEARID = " & YearId)
                ElseIf FRMSTRING = "WASTAGEWEAVER" Then
                    DT = OBJCMN.search("YWASWEAVER_NO ", "", "  YARNWASTAGEWEAVER ", " AND YWASWEAVER_NO = '" & TEMPWASTAGENO & "' AND YARNWASTAGEWEAVER.YWASWEAVER_YEARID = " & YearId)
                ElseIf FRMSTRING = "WASTAGEDYEING" Then
                    DT = OBJCMN.search("YWASDYEING_NO ", "", "  YARNWASTAGEDYEING ", " AND YWASDYEING_NO = '" & TEMPWASTAGENO & "' AND YARNWASTAGEDYEING.YWASDYEING_YEARID = " & YearId)
                ElseIf FRMSTRING = "WASTAGEJOBBER" Then
                    DT = OBJCMN.search("YWASJOBBER_NO ", "", "  YARNWASTAGEJOBBER ", " AND YWASJOBBER_NO = '" & TEMPWASTAGENO & "' AND YARNWASTAGEJOBBER.YWASJOBBER_YEARID = " & YearId)
                ElseIf FRMSTRING = "WASTAGEGODOWN" Then
                    DT = OBJCMN.search("YWASGODOWN_NO ", "", "  YARNWASTAGEGODOWN ", " AND YWASGODOWN_NO = '" & TEMPWASTAGENO & "' AND YARNWASTAGEGODOWN.YWASGODOWN_YEARID = " & YearId)
                ElseIf FRMSTRING = "WASTAGEMACHINE" Then
                    DT = OBJCMN.search("YWASMACHINE_NO ", "", "  YARNWASTAGEMACHINE ", " AND YWASMACHINE_NO = '" & TEMPWASTAGENO & "' AND YARNWASTAGEMACHINE.YWASMACHINE_YEARID = " & YearId)
                End If
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    YarnWastage_Load(sender, e)
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
                YarnWastage_Load(sender, e)
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
                    YarnWastage_Load(sender, e)
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
                CMBQUALITY.Text = GRIDWASTAGE.Item(GQUALITY.Index, e.RowIndex).Value
                CMBMILLNAME.Text = GRIDWASTAGE.Item(GMILLNAME.Index, e.RowIndex).Value
                TXTTAKA.Text = Format(Val(GRIDWASTAGE.Item(GTAKA.Index, e.RowIndex).Value), "0.00")
                TXTSTOCKWT.Text = Format(Val(GRIDWASTAGE.Item(GSTOCKWT.Index, e.RowIndex).Value), "0.000")
                TXTACTUALWT.Text = Format(Val(GRIDWASTAGE.Item(GACTUALWT.Index, e.RowIndex).Value), "0.000")
                TXTWT.Text = Format(Val(GRIDWASTAGE.Item(Gwt.Index, e.RowIndex).Value), "0.000")
                TXTNARR.Text = GRIDWASTAGE.Item(GNARRATION.Index, e.RowIndex).Value

                TEMPROW = e.RowIndex
                CMBQUALITY.Focus()

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
            Dim OBJYARN As New YarnWastageDetails
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
                If MsgBox("Delete Yarn Return?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPWASTAGENO)
                    alParaval.Add(YearId)

                    If FRMSTRING = "WASTAGEWARPER" Then
                        Dim ClsDO As New ClsYarnWastageWarper
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "WASTAGESIZER" Then
                        Dim ClsDO As New ClsYarnWastageSizer
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "WASTAGEWEAVER" Then
                        Dim ClsDO As New ClsYarnWastageWeaver
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "WASTAGEDYEING" Then
                        Dim ClsDO As New ClsYarnWastageDyeing
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "WASTAGEJOBBER" Then
                        Dim ClsDO As New ClsYarnWastageJobber
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "WASTAGEGODOWN" Then
                        Dim ClsDO As New ClsYarnWastageGodown
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "WASTAGEMACHINE" Then
                        Dim ClsDO As New ClsYarnWastageMachine
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    End If

                    MsgBox("Yarn Wastage Deleted")
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

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress
        numdot3(e, TXTWT, Me)
    End Sub

    Private Sub TXTNARR_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNARR.Validating
        If CMBQUALITY.Text.Trim <> "" And CMBTYPE.Text.Trim <> "" And Val(TXTWT.Text.Trim) <> 0 Then FILLGRID() Else MsgBox("Please Enter proper details")
    End Sub

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDWASTAGE.Rows.Add(Val(TXTSRNO.Text.Trim), CMBTYPE.Text.Trim, CMBQUALITY.Text.Trim, CMBMILLNAME.Text.Trim, Val(TXTTAKA.Text.Trim), Format(Val(TXTSTOCKWT.Text.Trim), "0.000"), Format(Val(TXTACTUALWT.Text.Trim), "0.000"), Format(Val(TXTWT.Text.Trim), "0.000"), TXTNARR.Text.Trim, 0)
            Else
                GRIDWASTAGE.Item(gsrno.Index, TEMPROW).Value = TXTSRNO.Text.Trim
                GRIDWASTAGE.Item(GTYPE.Index, TEMPROW).Value = CMBTYPE.Text.Trim
                GRIDWASTAGE.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
                GRIDWASTAGE.Item(GMILLNAME.Index, TEMPROW).Value = CMBMILLNAME.Text.Trim
                GRIDWASTAGE.Item(GTAKA.Index, TEMPROW).Value = Format(Val(TXTTAKA.Text.Trim), "0")
                GRIDWASTAGE.Item(GSTOCKWT.Index, TEMPROW).Value = Format(Val(TXTSTOCKWT.Text.Trim), "0.000")
                GRIDWASTAGE.Item(GACTUALWT.Index, TEMPROW).Value = Format(Val(TXTACTUALWT.Text.Trim), "0.000")
                GRIDWASTAGE.Item(Gwt.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.000")
                GRIDWASTAGE.Item(GNARRATION.Index, TEMPROW).Value = TXTNARR.Text.Trim

                GRIDDOUBLECLICK = False
            End If
            'TXTSRNO.Clear()
            CMBQUALITY.Text = ""
            CMBMILLNAME.Text = ""
            TXTTAKA.Clear()
            TXTSTOCKWT.Clear()
            TXTACTUALWT.Clear()
            TXTWT.Clear()
            TXTNARR.Clear()
            getsrno(GRIDWASTAGE)
            TOTAL()
            CMBQUALITY.Focus()
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

    Private Sub YarnWastage_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ALLOWMFG = False Then Exit Sub
    End Sub

    Private Sub TILLDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TILLDATE.Validating
        Try
            If TILLDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(TILLDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If


                If FRMSTRING = "WASTAGESIZER" Or FRMSTRING = "WASTAGEWARPER" Then

                    '                    'FIRST CHECK WHETHER TILL DATE IS ALREADY TAKEN IN PURCHASE INVOICE OR NOT
                    '                    Dim TEMPFROMDATE As Date = AccFrom.Date
                    '                    Dim OBJCMN As New ClsCommon
                    '                    Dim DT As DataTable = OBJCMN.search("MAX(CAST(BILL_TILLDATE AS DATE)) AS DATE", "", "PURCHASEMASTER INNER JOIN LEDGERS ON ACC_ID = BILL_LEDGERID ", " AND ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND BILL_FRMSTRING = '" & FRMSTRING & "' AND BILL_YEARID = " & YearId)
                    '                    If DT.Rows.Count > 0 Then
                    '                        If IsDBNull(DT.Rows(0).Item("DATE")) = True Then
                    '                            TEMPFROMDATE = AccFrom.Date.AddDays(-1)
                    '                            GoTo LINE1
                    '                        Else
                    '                            TEMPFROMDATE = DT.Rows(0).Item("DATE")
                    '                        End If
                    '                        If DT.Rows(0).Item("DATE") >= Format(Convert.ToDateTime(TILLDATE.Text).Date, "dd/MM/yyyy") Then
                    '                            MsgBox("Till Date Already Taken, Please check before proceeding", MsgBoxStyle.Critical)
                    '                            TEMPFROMDATE = AccFrom.Date
                    '                            DT = OBJCMN.search("MAX(CAST(BILL_TILLDATE AS DATE)) AS DATE", "", "PURCHASEMASTER INNER JOIN LEDGERS ON ACC_ID = BILL_LEDGERID ", " AND ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND BILL_FRMSTRING = '" & FRMSTRING & "' AND CAST(BILL_TILLDATE AS DATE) <' " & Format(Convert.ToDateTime(TILLDATE.Text).Date, "MM/dd/yyyy") & "' And BILL_YEARID = " & YearId)
                    '                            If DT.Rows.Count > 0 Then TEMPFROMDATE = DT.Rows(0).Item("DATE")
                    '                            'e.Cancel = True
                    '                            'Exit Sub
                    '                        End If
                    '                    End If

                    'LINE1:


                    ''IF DATE IS CORRECT THEN FETCH ALL SIZING / WARPING DATA
                    'Dim OBJCMN As New ClsCommon
                    'Dim DT As New DataTable
                    'If FRMSTRING = "WASTAGESIZER" Then
                    '    DT = OBJCMN.search(" BEAMRECEIVED.BEAMREC_TOTALCUT As CUT, BEAMRECEIVED.BEAMREC_TOTALWT As WT, BEAMREC_CHALLANNO As CHALLANNO, BEAMREC_CHALLANDATE As CHALLANDATE, BEAMRECEIVED.BEAMREC_TOTALBEAM As BEAMS, BEAMRECEIVED.BEAMREC_FROMTOBEAM As NARR, BeamMaster.BEAM_ENDS As ENDS, BeamMaster.BEAM_TAPLINE AS TL ", "", " BEAMRECEIVED INNER JOIN BEAMRECEIVED_DESC ON BEAMRECEIVED.BEAMREC_NO = BEAMRECEIVED_DESC.BEAMREC_NO And BEAMRECEIVED.BEAMREC_YEARID = BEAMRECEIVED_DESC.BEAMREC_YEARID INNER JOIN BEAMMASTER ON BEAMRECEIVED_DESC.BEAMREC_BEAMID = BEAMMASTER.BEAM_ID INNER JOIN LEDGERS ON Acc_id = BEAMREC_LEDGERID ", " And LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND BEAMREC_DATE > '" & Format(TEMPFROMDATE.Date, "MM/dd/yyyy") & "' AND BEAMREC_DATE <= '" & Format(Convert.ToDateTime(TILLDATE.Text).Date, "MM/dd/yyyy") & "' AND BEAMRECEIVED.BEAMREC_YEARID = " & YearId & " GROUP BY BEAMRECEIVED.BEAMREC_TOTALCUT, BEAMRECEIVED.BEAMREC_TOTALWT, BEAMRECEIVED.BEAMREC_CHALLANNO, BEAMRECEIVED.BEAMREC_CHALLANDATE, BEAMRECEIVED.BEAMREC_TOTALBEAM, BEAMRECEIVED.BEAMREC_FROMTOBEAM, BEAMMASTER.BEAM_ENDS, BEAMMASTER.BEAM_TAPLINE,BEAMREC_DATE  ORDER BY BEAMREC_DATE , CHALLANNO")
                    'Else
                    '    DT = OBJCMN.search(" ROLLRECEIVED.ROLLRECD_CUT AS CUT, ROLLRECEIVED.ROLLRECD_TOTALWT AS WT, ROLLRECD_CHALLANNO AS CHALLANNO, ROLLRECD_CHALLANDATE AS CHALLANDATE,  0 AS BEAMS, 'From ' + ROLLRECEIVED.ROLLRECD_BEAMFROM + ' To ' + ROLLRECEIVED.ROLLRECD_BEAMTO AS NARR, BEAMMASTER.BEAM_ENDS AS ENDS, BEAMMASTER.BEAM_TAPLINE AS TL ", "", " ROLLRECEIVED INNER JOIN ROLLRECEIVED_DESC ON ROLLRECEIVED.ROLLRECD_NO = ROLLRECEIVED_DESC.ROLLRECD_NO AND ROLLRECEIVED.ROLLRECD_YEARID = ROLLRECEIVED_DESC.ROLLRECD_YEARID INNER JOIN BEAMMASTER ON ROLLRECEIVED.ROLLRECD_BEAMID = BEAMMASTER.BEAM_ID INNER JOIN LEDGERS ON Acc_id = ROLLRECD_WARPERID", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ROLLRECD_DATE > '" & Format(TEMPFROMDATE.Date, "MM/dd/yyyy") & "' AND ROLLRECD_DATE <= '" & Format(Convert.ToDateTime(TILLDATE.Text).Date, "MM/dd/yyyy") & "' AND ROLLRECEIVED.ROLLRECD_YEARID = " & YearId & " GROUP BY ROLLRECEIVED.ROLLRECD_CUT, ROLLRECEIVED.ROLLRECD_TOTALWT, ROLLRECEIVED.ROLLRECD_CHALLANNO, ROLLRECEIVED.ROLLRECD_CHALLANDATE, ROLLRECEIVED.ROLLRECD_BEAMFROM, ROLLRECD_BEAMTO, BEAMMASTER.BEAM_ENDS, BEAMMASTER.BEAM_TAPLINE,ROLLRECD_DATE  ORDER BY ROLLRECD_DATE , CHALLANNO")
                    'End If

                    'If DT.Rows.Count > 0 Then
                    '    For Each DTROW As DataRow In DT.Rows
                    '        GRIDWASTAGE.Rows.Add(0, "", Val(DTROW("CUT")), Val(DTROW("WT")), 0, 0, "MTRS", 0, "", DTROW("CHALLANNO"), DTROW("CHALLANDATE"), Val(DTROW("BEAMS")), DTROW("NARR"), Val(DTROW("ENDS")), Val(DTROW("TL")), "", 0, 0, 0)
                    '    Next
                    '    CMBNAME.Enabled = False
                    '    TILLDATE.Enabled = False
                    '    GRIDWASTAGE.FirstDisplayedScrollingRowIndex = GRIDWASTAGE.RowCount - 1
                    '    getsrno(GRIDWASTAGE)
                    '    If GRIDWASTAGE.RowCount > 0 Then
                    '        GRIDWASTAGE.Focus()
                    '        GRIDWASTAGE.CurrentCell = GRIDWASTAGE.Rows(0).Cells(GACTUALWT.Index)
                    '    End If
                    '    TOTAL()
                    'End If



                ElseIf FRMSTRING = "WASTAGEWEAVER" Then


                    If FROMDATE.Text.Trim <> "__/__/____" Then
                        'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                        If Not DateTime.TryParse(FROMDATE.Text, TEMP) Then
                            MsgBox("Enter Proper Date")
                            e.Cancel = True
                            Exit Sub
                        End If
                    End If


                    'IF DATE IS CORRECT THEN FETCH ALL WEAVING DATA
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.Execute_Any_String(" SELECT QUALITY, ROUND(SUM(WT) - SUM(RECD),2) BALANCEWT FROM WEAVERYARNSTOCKREGISTER WHERE WEAVERNAME = '" & CMBNAME.Text.Trim & "' AND DATE <= '" & Format(Convert.ToDateTime(TILLDATE.Text).Date, "MM/dd/yyyy") & "' AND YEARID = " & YearId & " GROUP BY QUALITY ", "", "")
                    If DT.Rows.Count > 0 Then
                        For Each DTROW As DataRow In DT.Rows

                            'GET TOTALPSC WITH RESPECT TO FROM AND TO DATE,
                            'DONT ADD THIS IN ABOVE CODE, AS WE HAVE FETCHED STOCK TILL DATE AND FROMDATE CLAUSE IS NOT MENTIONED IN ABOVE CODE
                            'WE NEED TO FETCH TOTALPCS WITH RESPECT TO FROM AND TILL DATE
                            Dim DTPCS As DataTable = OBJCMN.Execute_Any_String(" SELECT ISNULL(SUM(TOTALPCS),0) TOTALPCS FROM WEAVERYARNSTOCKREGISTER WHERE WEAVERNAME = '" & CMBNAME.Text.Trim & "' AND QUALITY = '" & DTROW("QUALITY") & "' AND DATE >= '" & Format(Convert.ToDateTime(FROMDATE.Text).Date, "MM/dd/yyyy") & "' AND DATE <= '" & Format(Convert.ToDateTime(TILLDATE.Text).Date, "MM/dd/yyyy") & "' AND YEARID = " & YearId, "", "")
                            GRIDWASTAGE.Rows.Add(0, "Wastage", DTROW("QUALITY"), "", Val(DTPCS.Rows(0).Item("TOTALPCS")), Val(DTROW("BALANCEWT")), 0, 0, "", 0)

                        Next
                        getsrno(GRIDWASTAGE)
                        TOTAL()
                    End If

                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FROMDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles FROMDATE.Validating
        Try
            If FROMDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(FROMDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWASTAGE_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDWASTAGE.CellValidating
        Try
            Dim colNum As Integer = GRIDWASTAGE.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GSTOCKWT.Index, GACTUALWT.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDWASTAGE.CurrentCell.Value = Nothing Then GRIDWASTAGE.CurrentCell.Value = "0.00"
                        GRIDWASTAGE.CurrentCell.Value = Convert.ToDecimal(GRIDWASTAGE.Item(colNum, e.RowIndex).Value)
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

    Private Sub TXTACTUALWT_Validated(sender As Object, e As EventArgs) Handles TXTACTUALWT.Validated
        Try
            If Val(TXTACTUALWT.Text.Trim) <> 0 And Val(TXTSTOCKWT.Text.Trim) <> 0 Then TXTWT.Text = Format(Val(TXTACTUALWT.Text.Trim) - Val(TXTSTOCKWT.Text.Trim), "0.000")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class