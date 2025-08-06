
Imports BL
Imports System.ComponentModel

Public Class YarnIssue

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDISSUEUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPISSUENO As Integer
    Dim TEMPMSG As Integer
    Public FRMSTRING As String
    Dim TEMPADDANO As Integer
    Dim DTWHATSAPP As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        cmbname.Focus()
    End Sub

    Sub CLEAR()

        CMBOURGODOWN.Text = GETDEFAULTGODOWN()
        TXTSERIES.Clear()
        cmbname.Text = ""
        cmbtrans.Text = ""
        TXTVEHICLENO.Clear()
        TXTEWBNO.Clear()
        TXTADDANO.Clear()
        LBLWHATSAPP.Visible = False
        DTISSUEDATE.Text = Mydate
        txtremarks.Clear()

        LBLTOTALBAGS.Text = 0
        LBLBAGNOCOUNT.Text = 0
        LBLTOTALGROSSWT.Text = 0.0
        LBLTOTALTAREWT.Text = 0.0
        LBLTOTALWT.Text = 0.0
        LBLTOTALFIRKA.Text = 0
        LBLTOTALFRESH.Text = 0
        LBLTOTALWINDING.Text = 0

        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        txtremarks.Clear()

        GRIDISSUE.RowCount = 0
        GETMAX_ISSUE_NO()

        GRIDISSUEUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        CMDSELECTSTOCK.Enabled = True

        TabControl1.SelectedIndex = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0

        If gridupload.RowCount > 0 Then TXTUPLOADSRNO.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1 Else TXTUPLOADSRNO.Text = 1

    End Sub

    Sub GETMAX_ISSUE_NO()
        Dim DTTABLE As New DataTable
        If FRMSTRING = "ISSUETOWARPER" Then
            DTTABLE = getmax("ISNULL(MAX(YISSUEWARPER_NO),0)+ 1", "YARNISSUEWARPER", "AND YISSUEWARPER_YEARID = " & YearId)
        ElseIf FRMSTRING = "ISSUETOADDA" Then
            DTTABLE = getmax("ISNULL(MAX(YISSUEADDA_NO),0)+ 1", "YARNISSUEADDA", "AND YISSUEADDA_YEARID = " & YearId)
        ElseIf FRMSTRING = "ISSUETOSIZER" Then
            DTTABLE = getmax("ISNULL(MAX(YISSUESIZER_NO),0)+ 1", "YARNISSUESIZER", "AND YISSUESIZER_YEARID = " & YearId)
        ElseIf FRMSTRING = "ISSUETOWEAVER" Then
            DTTABLE = getmax("ISNULL(MAX(YISSUEWEAVER_NO),0)+ 1", "YARNISSUEWEAVER", "AND YISSUEWEAVER_YEARID = " & YearId)
        ElseIf FRMSTRING = "ISSUETODYEING" Then
            DTTABLE = getmax("ISNULL(MAX(YISSUEDYEING_NO),0)+ 1", "YARNISSUEDYEING", "AND YISSUEDYEING_YEARID = " & YearId)
        End If
        If DTTABLE.Rows.Count > 0 Then TXTISSUENO.Text = DTTABLE.Rows(0).Item(0)
        GETMAXSERIES(TXTSERIES)
        GETMAXADDANO()
    End Sub

    Sub GETMAXADDANO()
        Try
            Dim DTTABLE As New DataTable
            If FRMSTRING = "ISSUETOADDA" Then
                DTTABLE = getmax("ISNULL(MAX(YISSUEADDA_ADDANO),0)+ 1", "YARNISSUEADDA", "AND YISSUEADDA_YEARID = " & YearId)
                If DTTABLE.Rows.Count > 0 Then TXTADDANO.Text = DTTABLE.Rows(0).Item(0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YarnIssueToWarper_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        ''''''''''''******* CHANGE ACC_SUBTYPE *******'''''''''
        If FRMSTRING = "ISSUETOWARPER" Or FRMSTRING = "ISSUETOADDA" Then
            If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')") ''''TYPE = WARPER
        ElseIf FRMSTRING = "ISSUETOSIZER" Then
            If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'") ''''TYPE = SIZER
        ElseIf FRMSTRING = "ISSUETOWEAVER" Then
            If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'") ''''TYPE = WEAVER
        ElseIf FRMSTRING = "ISSUETODYEING" Then
            If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'") ''''TYPE = PROCESSOR
        End If
        If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR = 'True'")
        If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
    End Sub

    Private Sub YarnIssueToWarper_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)


            If FRMSTRING = "ISSUETOWARPER" Then
                LBLNAME.Text = "Warper Name"
                Me.Text = "Yarn Issue To Warper"

            ElseIf FRMSTRING = "ISSUETOADDA" Then
                LBLNAME.Text = "Warper Name"
                Me.Text = "Yarn Issue To Adda"
                LBLADDANO.Visible = True
                TXTADDANO.Visible = True

                LBLGODOWN.Visible = False
                CMBOURGODOWN.Visible = False

            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                LBLNAME.Text = "Sizer Name"
                Me.Text = "Yarn Issue To Sizer"

            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                LBLNAME.Text = "Weaver Name"
                Me.Text = "Yarn Issue To Weaver"

            ElseIf FRMSTRING = "ISSUETODYEING" Then
                LBLNAME.Text = "Dyeing Name"
                Me.Text = "Yarn Issue To Dyeing"
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
                If FRMSTRING = "ISSUETOWARPER" Then

                    Dim objclsDO As New ClsYarnIssueToWarper
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPISSUENO)
                    ALPARAVAL.Add(YearId)
                    objclsDO.alParaval = ALPARAVAL
                    dttable = objclsDO.selectYARNISSUE()

                    If dttable.Rows.Count > 0 Then

                        For Each dr As DataRow In dttable.Rows

                            TXTSERIES.Text = Val(dr("SERIES"))
                            TXTISSUENO.Text = TEMPISSUENO
                            DTISSUEDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            cmbname.Text = Convert.ToString(dr("NAME").ToString)
                            cmbtrans.Text = Convert.ToString(dr("TRANSNAME").ToString)
                            TXTVEHICLENO.Text = Convert.ToString(dr("VEHICALNO").ToString)
                            TXTEWBNO.Text = Convert.ToString(dr("EWBNO").ToString)

                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                            If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True
                            'Item Grid
                            GRIDISSUE.Rows.Add(dr("SRNO"), dr("QUALITY").ToString, dr("MILLNAME").ToString, dr("SUPPLIER").ToString, dr("LOTNO"), dr("SHADE"), Val(dr("BAGS")), dr("BAGNO"), Format(Val(dr("GROSSWT")), "0.000"), Format(Val(dr("TAREWT")), "0.000"), Format(Val(dr("WT")), "0.000"), Val(dr("FRESH")), Val(dr("WINDING")), Val(dr("FIRKA")), dr("NARRATION").ToString, Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("GRIDTYPE"))

                            If Val(dr("OUTWT")) > 0 Then
                                lbllocked.Visible = True
                                PBlock.Visible = True
                            End If

                        Next
                        TOTAL()
                    Else
                        EDIT = False
                        CLEAR()
                    End If

                    Dim OBJCMN As New ClsCommon
                    dttable = OBJCMN.search(" YISSUEWARPER_SRNO AS GRIDSRNO, YISSUEWARPER_REMARKS AS REMARKS, YISSUEWARPER_NAME AS NAME, YISSUEWARPER_PHOTO AS IMGPATH ", "", " YARNISSUEWARPER_UPLOAD", " AND YISSUEWARPER_NO = " & TEMPISSUENO & " AND YISSUEWARPER_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If



                ElseIf FRMSTRING = "ISSUETOADDA" Then

                    Dim objclsDO As New ClsYarnIssueToAdda
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPISSUENO)
                    ALPARAVAL.Add(YearId)
                    objclsDO.alParaval = ALPARAVAL
                    dttable = objclsDO.selectYARNISSUE()

                    If dttable.Rows.Count > 0 Then

                        For Each dr As DataRow In dttable.Rows

                            TXTSERIES.Text = Val(dr("SERIES"))
                            TXTISSUENO.Text = TEMPISSUENO
                            DTISSUEDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            cmbname.Text = Convert.ToString(dr("NAME").ToString)
                            cmbtrans.Text = Convert.ToString(dr("TRANSNAME").ToString)
                            TXTVEHICLENO.Text = Convert.ToString(dr("VEHICALNO").ToString)
                            TXTEWBNO.Text = Convert.ToString(dr("EWBNO").ToString)

                            TXTADDANO.Text = Val(dr("ADDANO"))
                            TEMPADDANO = Val(dr("ADDANO"))

                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                            'Item Grid
                            GRIDISSUE.Rows.Add(dr("SRNO"), dr("QUALITY").ToString, dr("MILLNAME").ToString, dr("SUPPLIER").ToString, dr("LOTNO"), dr("SHADE"), Val(dr("BAGS")), dr("BAGNO"), Format(Val(dr("GROSSWT")), "0.000"), Format(Val(dr("TAREWT")), "0.000"), Format(Val(dr("WT")), "0.000"), Val(dr("FRESH")), Val(dr("WINDING")), Val(dr("FIRKA")), dr("NARRATION").ToString, Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("GRIDTYPE"))
                            If Convert.ToBoolean(dr("ADDADONE")) = True Then
                                lbllocked.Visible = True
                                PBlock.Visible = True
                            End If
                        Next
                        'DONE BY GULKIT AS PER JASHOK
                        'CMDSELECTSTOCK.Enabled = False
                        TOTAL()
                    Else
                        EDIT = False
                        CLEAR()
                    End If

                    Dim OBJCMN As New ClsCommon
                    dttable = OBJCMN.search(" YISSUEADDA_SRNO AS GRIDSRNO, YISSUEADDA_REMARKS AS REMARKS, YISSUEADDA_NAME AS NAME, YISSUEADDA_PHOTO AS IMGPATH ", "", " YARNISSUEADDA_UPLOAD", " AND YISSUEADDA_NO = " & TEMPISSUENO & " AND YISSUEADDA_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If


                ElseIf FRMSTRING = "ISSUETOSIZER" Then

                    Dim objclsDO As New ClsYarnIssueToSizer
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPISSUENO)
                    ALPARAVAL.Add(YearId)
                    objclsDO.alParaval = ALPARAVAL
                    dttable = objclsDO.selectYARNISSUE()

                    If dttable.Rows.Count > 0 Then

                        For Each dr As DataRow In dttable.Rows

                            TXTSERIES.Text = Val(dr("SERIES"))
                            TXTISSUENO.Text = TEMPISSUENO
                            DTISSUEDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            cmbname.Text = Convert.ToString(dr("NAME").ToString)
                            cmbtrans.Text = Convert.ToString(dr("TRANSNAME").ToString)
                            TXTVEHICLENO.Text = Convert.ToString(dr("VEHICALNO").ToString)
                            TXTEWBNO.Text = Convert.ToString(dr("EWBNO").ToString)

                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                            'Item Grid
                            GRIDISSUE.Rows.Add(dr("SRNO"), dr("QUALITY").ToString, dr("MILLNAME").ToString, dr("SUPPLIER").ToString, dr("LOTNO"), dr("SHADE"), Val(dr("BAGS")), dr("BAGNO"), Format(Val(dr("GROSSWT")), "0.000"), Format(Val(dr("TAREWT")), "0.000"), Format(Val(dr("WT")), "0.000"), Val(dr("FRESH")), Val(dr("WINDING")), Val(dr("FIRKA")), dr("NARRATION").ToString, Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("GRIDTYPE"))
                            If Val(dr("OUTWT")) > 0 Then
                                lbllocked.Visible = True
                                PBlock.Visible = True
                            End If
                        Next
                        'DONE BY GULKIT AS PER JASHOK
                        'CMDSELECTSTOCK.Enabled = False
                        TOTAL()
                    Else
                        EDIT = False
                        CLEAR()
                    End If

                    Dim OBJCMN As New ClsCommon
                    dttable = OBJCMN.search(" YISSUESIZER_SRNO AS GRIDSRNO, YISSUESIZER_REMARKS AS REMARKS, YISSUESIZER_NAME AS NAME, YISSUESIZER_PHOTO AS IMGPATH ", "", " YARNISSUESIZER_UPLOAD", " AND YISSUESIZER_NO = " & TEMPISSUENO & " AND YISSUESIZER_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If

                ElseIf FRMSTRING = "ISSUETOWEAVER" Then

                    Dim objclsDO As New ClsYarnIssueToWeaver
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPISSUENO)
                    ALPARAVAL.Add(YearId)
                    objclsDO.alParaval = ALPARAVAL
                    dttable = objclsDO.selectYARNISSUE()

                    If dttable.Rows.Count > 0 Then

                        For Each dr As DataRow In dttable.Rows

                            TXTSERIES.Text = Val(dr("SERIES"))
                            TXTISSUENO.Text = TEMPISSUENO
                            DTISSUEDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            cmbname.Text = Convert.ToString(dr("NAME").ToString)
                            cmbtrans.Text = Convert.ToString(dr("TRANSNAME").ToString)
                            TXTVEHICLENO.Text = Convert.ToString(dr("VEHICALNO").ToString)
                            TXTEWBNO.Text = Convert.ToString(dr("EWBNO").ToString)

                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                            'Item Grid
                            GRIDISSUE.Rows.Add(dr("SRNO"), dr("QUALITY").ToString, dr("MILLNAME").ToString, dr("SUPPLIER").ToString, dr("LOTNO"), dr("SHADE"), Val(dr("BAGS")), dr("BAGNO"), Format(Val(dr("GROSSWT")), "0.000"), Format(Val(dr("TAREWT")), "0.000"), Format(Val(dr("WT")), "0.000"), Val(dr("FRESH")), Val(dr("WINDING")), Val(dr("FIRKA")), dr("NARRATION").ToString, Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("GRIDTYPE").ToString)
                            'If Convert.ToBoolean(dr("DONE")) = True Then
                            '    lbllocked.Visible = True
                            '    PBlock.Visible = True
                            'End If
                        Next
                        'DONE BY GULKIT AS PER JASHOK
                        'CMDSELECTSTOCK.Enabled = False
                        TOTAL()
                    Else
                        EDIT = False
                        CLEAR()
                    End If

                    Dim OBJCMN As New ClsCommon
                    dttable = OBJCMN.search(" YISSUEWEAVER_SRNO AS GRIDSRNO, YISSUEWEAVER_REMARKS AS REMARKS, YISSUEWEAVER_NAME AS NAME, YISSUEWEAVER_PHOTO AS IMGPATH ", "", " YARNISSUEWEAVER_UPLOAD", " AND YISSUEWEAVER_NO = " & TEMPISSUENO & " AND YISSUEWEAVER_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If


                ElseIf FRMSTRING = "ISSUETODYEING" Then

                    Dim objclsDO As New ClsYarnIssueToDyeing
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPISSUENO)
                    ALPARAVAL.Add(YearId)
                    objclsDO.alParaval = ALPARAVAL
                    dttable = objclsDO.selectYARNISSUE()

                    If dttable.Rows.Count > 0 Then

                        For Each dr As DataRow In dttable.Rows

                            TXTSERIES.Text = Val(dr("SERIES"))
                            TXTISSUENO.Text = TEMPISSUENO
                            DTISSUEDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                            cmbname.Text = Convert.ToString(dr("NAME").ToString)
                            cmbtrans.Text = Convert.ToString(dr("TRANSNAME").ToString)
                            TXTVEHICLENO.Text = Convert.ToString(dr("VEHICALNO").ToString)
                            TXTEWBNO.Text = Convert.ToString(dr("EWBNO").ToString)

                            txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                            'Item Grid
                            GRIDISSUE.Rows.Add(dr("SRNO"), dr("QUALITY").ToString, dr("MILLNAME").ToString, dr("SUPPLIER").ToString, dr("LOTNO"), dr("SHADE"), Val(dr("BAGS")), dr("BAGNO"), Format(Val(dr("GROSSWT")), "0.000"), Format(Val(dr("TAREWT")), "0.000"), Format(Val(dr("WT")), "0.000"), Val(dr("FRESH")), Val(dr("WINDING")), Val(dr("FIRKA")), dr("NARRATION").ToString, Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("GRIDTYPE").ToString)
                            'If Convert.ToBoolean(dr("DONE")) = True Then
                            '    lbllocked.Visible = True
                            '    PBlock.Visible = True
                            'End If
                        Next
                        'DONE BY GULKIT AS PER JASHOK
                        'CMDSELECTSTOCK.Enabled = False
                        TOTAL()
                    Else
                        EDIT = False
                        CLEAR()
                    End If

                    Dim OBJCMN As New ClsCommon
                    dttable = OBJCMN.search(" YISSUEDYEING_SRNO AS GRIDSRNO, YISSUEDYEING_REMARKS AS REMARKS, YISSUEDYEING_NAME AS NAME, YISSUEDYEING_PHOTO AS IMGPATH ", "", " YARNISSUEDYEING_UPLOAD", " AND YISSUEDYEING_NO = " & TEMPISSUENO & " AND YISSUEDYEING_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
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

            alParaval.Add(Format(Convert.ToDateTime(DTISSUEDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(TXTVEHICLENO.Text.Trim)
            alParaval.Add(TXTEWBNO.Text.Trim)
            If FRMSTRING = "ISSUETOADDA" Then alParaval.Add(Val(TXTADDANO.Text.Trim))
            alParaval.Add(Val(LBLTOTALBAGS.Text.Trim))
            alParaval.Add(Val(LBLBAGNOCOUNT.Text.Trim))
            alParaval.Add(Val(LBLTOTALGROSSWT.Text.Trim))
            alParaval.Add(Val(LBLTOTALTAREWT.Text.Trim))
            alParaval.Add(Val(LBLTOTALWT.Text.Trim))
            alParaval.Add(Val(LBLTOTALFRESH.Text.Trim))
            alParaval.Add(Val(LBLTOTALWINDING.Text.Trim))
            alParaval.Add(Val(LBLTOTALFIRKA.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim gridsrno As String = ""
            Dim QUALITY As String = ""
            Dim MILLNAME As String = ""
            Dim SUPPLIER As String = ""
            Dim LOTNO As String = ""
            Dim SHADE As String = ""
            Dim BAGS As String = ""
            Dim BAGNO As String = ""
            Dim GROSSWT As String = ""
            Dim TAREWT As String = ""
            Dim WT As String = ""
            Dim FRESH As String = ""
            Dim WINDING As String = ""
            Dim FIRKA As String = ""
            Dim NARRATION As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim GRIDTYPE As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDISSUE.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        SUPPLIER = row.Cells(GSUPPILERNAME.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        SHADE = row.Cells(GSHADE.Index).Value.ToString
                        BAGS = Val(row.Cells(GBAGS.Index).Value)
                        BAGNO = row.Cells(GBAGNO.Index).Value
                        GROSSWT = row.Cells(Gwt.Index).Value
                        TAREWT = row.Cells(GTAREWT.Index).Value
                        WT = Val(row.Cells(Gwt.Index).Value)
                        FRESH = Val(row.Cells(GFRESH.Index).Value)
                        WINDING = Val(row.Cells(GWINDING.Index).Value)
                        FIRKA = Val(row.Cells(GFIRKA.Index).Value)
                        If row.Cells(GNARRATION.Index).Value <> Nothing Then NARRATION = row.Cells(GNARRATION.Index).Value.ToString Else NARRATION = ""
                        FROMNO = row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = row.Cells(GFROMSRNO.Index).Value.ToString
                        GRIDTYPE = row.Cells(GGRIDTYPE.Index).Value.ToString

                    Else

                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        SUPPLIER = SUPPLIER & "|" & row.Cells(GSUPPILERNAME.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        SHADE = SHADE & "|" & row.Cells(GSHADE.Index).Value.ToString
                        BAGS = BAGS & "|" & Val(row.Cells(GBAGS.Index).Value)
                        BAGNO = BAGNO & "|" & row.Cells(GBAGNO.Index).Value
                        GROSSWT = GROSSWT & "|" & row.Cells(Gwt.Index).Value
                        TAREWT = TAREWT & "|" & row.Cells(GTAREWT.Index).Value
                        WT = WT & "|" & Val(row.Cells(Gwt.Index).Value)
                        FRESH = FRESH & "|" & Val(row.Cells(GFRESH.Index).Value)
                        WINDING = WINDING & "|" & Val(row.Cells(GWINDING.Index).Value)
                        FIRKA = FIRKA & "|" & Val(row.Cells(GFIRKA.Index).Value)
                        If row.Cells(GNARRATION.Index).Value <> Nothing Then NARRATION = NARRATION & "|" & row.Cells(GNARRATION.Index).Value.ToString Else NARRATION = NARRATION & "|" & ""
                        FROMNO = FROMNO & "|" & row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = FROMSRNO & "|" & row.Cells(GFROMSRNO.Index).Value.ToString
                        GRIDTYPE = GRIDTYPE & "|" & row.Cells(GGRIDTYPE.Index).Value.ToString


                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(QUALITY)
            alParaval.Add(MILLNAME)
            alParaval.Add(SUPPLIER)
            alParaval.Add(LOTNO)
            alParaval.Add(SHADE)
            alParaval.Add(BAGS)
            alParaval.Add(BAGNO)
            alParaval.Add(GROSSWT)
            alParaval.Add(TAREWT)
            alParaval.Add(WT)
            alParaval.Add(FRESH)
            alParaval.Add(WINDING)
            alParaval.Add(FIRKA)
            alParaval.Add(NARRATION)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(GRIDTYPE)


            If FRMSTRING = "ISSUETOWARPER" Then
                Dim OBJWARPER As New ClsYarnIssueToWarper
                OBJWARPER.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJWARPER.SAVE()
                    TEMPISSUENO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPISSUENO)
                    IntResult = OBJWARPER.UPDATE()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If

            ElseIf FRMSTRING = "ISSUETOADDA" Then
                Dim OBJADDA As New ClsYarnIssueToAdda
                OBJADDA.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJADDA.SAVE()
                    TEMPISSUENO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPISSUENO)
                    IntResult = OBJADDA.UPDATE()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If

            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                Dim OBJSIZER As New ClsYarnIssueToSizer
                OBJSIZER.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJSIZER.SAVE()
                    TEMPISSUENO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPISSUENO)
                    IntResult = OBJSIZER.UPDATE()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If


            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                Dim OBJWARPER As New ClsYarnIssueToWeaver
                OBJWARPER.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJWARPER.SAVE()
                    TEMPISSUENO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPISSUENO)
                    IntResult = OBJWARPER.UPDATE()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If

            ElseIf FRMSTRING = "ISSUETODYEING" Then
                Dim OBJDYEING As New ClsYarnIssueToDyeing
                OBJDYEING.alParaval = alParaval
                If EDIT = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    Dim DTTABLE As DataTable = OBJDYEING.SAVE()
                    TEMPISSUENO = DTTABLE.Rows(0).Item(0)
                    MessageBox.Show("Details Added")

                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TEMPISSUENO)
                    IntResult = OBJDYEING.UPDATE()
                    MessageBox.Show("Details Updated")
                    EDIT = False
                End If
            End If

            PRINTREPORT()
            If gridupload.RowCount > 0 Then SAVEUPLOAD()

            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            cmbname.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True


        If DTISSUEDATE.Text = "__/__/____" Then
            EP.SetError(DTISSUEDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTISSUEDATE.Text) Then
                EP.SetError(DTISSUEDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If cmbname.Text.Trim.Length = 0 Then
            EP.SetError(cmbname, "Please Fill Warper Name")
            bln = False
        End If

        If cmbtrans.Text.Trim.Length = 0 Then
            EP.SetError(cmbtrans, " Please Fill Transport Name ")
            bln = False
        End If

        If CMBOURGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBOURGODOWN, " Please Fill Godown ")
            bln = False
        End If

        If GRIDISSUE.RowCount = 0 Then
            EP.SetError(cmbtrans, "Select Stock")
            bln = False
        End If

        For Each row As DataGridViewRow In GRIDISSUE.Rows
            If Val(row.Cells(Gwt.Index).Value) = 0 Then
                EP.SetError(cmbname, "Wt Cannot be 0")
                bln = False
            End If

            'If ClientName <> "JASHOK" Then
            '    If Val(row.Cells(GFRESH.Index).Value) = 0 And Val(row.Cells(GWINDING.Index).Value) = 0 And Val(row.Cells(GFIRKA.Index).Value) = 0 Then
            '        EP.SetError(cmbname, "Fresh / Winding / Firka Cannot be 0")
            '        bln = False
            '    End If
            'End If
        Next


        If FRMSTRING = "ISSUETOADDA" And lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Entry Locked")
            bln = False
        End If



        If TXTADDANO.Text.Trim <> "" AndAlso ((EDIT = False) Or (EDIT = True And TXTADDANO.Text.Trim <> TEMPADDANO)) Then
            Dim OBJCMN As New ClsCommon
            Dim dttable As DataTable = OBJCMN.search(" ISNULL(YISSUEADDA_NO, '') AS ISSUENO", "", " YARNISSUEADDA ", " AND YISSUEADDA_ADDANO = " & Val(TXTADDANO.Text.Trim) & " AND YISSUEADDA_YEARID = " & YearId)
            If dttable.Rows.Count > 0 Then
                EP.SetError(TXTADDANO, "Adda No Already Present !")
                bln = False
            End If
        End If


        'checking in stock
        If GRIDISSUE.RowCount > 0 And FRMSTRING <> "ISSUETOADDA" Then
            For Each row As DataGridViewRow In GRIDISSUE.Rows
                If Val(row.Cells(Gwt.Index).Value) > 0 Then

                    Dim BALFRESH, BALWINDING, BALFIRKA As Integer
                    Dim BALWT As Double

                    Dim OBJCMN As New ClsCommonMaster
                    Dim dt As DataTable = OBJCMN.search(" WT, BAGS, FRESH, WINDING, FIRKA ", "", " STOCKVIEW_OURGODOWN ", " AND GODOWN='" & CMBOURGODOWN.Text.Trim & "' AND NO= " & Val(row.Cells(GFROMNO.Index).Value) & " AND SRNO= " & Val(row.Cells(GFROMSRNO.Index).Value) & " AND TYPE='" & row.Cells(GGRIDTYPE.Index).Value & "' AND Yearid = " & YearId)
                    If dt.Rows.Count <= 0 And EDIT = False Then GoTo LINE1
                    If dt.Rows.Count > 0 Then
                        BALWT = dt.Rows(0).Item("WT")
                        BALFRESH = dt.Rows(0).Item("FRESH")
                        BALWINDING = dt.Rows(0).Item("WINDING")
                        BALFIRKA = dt.Rows(0).Item("FIRKA")
                    End If
                    If EDIT = True Then
                        Dim DT1 As New DataTable
                        If FRMSTRING = "ISSUETOWARPER" Then
                            DT1 = OBJCMN.search(" ROUND(ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_WT, 0),2) AS WT, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_BAGS, 0) AS BAGS, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_FRESH, 0) AS FRESH, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_WINDING, 0) AS WINDING, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_FIRKA, 0) AS FIRKA ", "", "   YARNISSUEWARPER_DESC ", " AND YARNISSUEWARPER_DESC.YISSUEWARPER_NO = " & TEMPISSUENO & " AND YISSUEWARPER_FROMNO = " & Val(row.Cells(GFROMNO.Index).Value) & " AND YISSUEWARPER_FROMSRNO = " & Val(row.Cells(GFROMSRNO.Index).Value) & " AND YISSUEWARPER_TYPE = '" & row.Cells(GGRIDTYPE.Index).Value & "' AND YARNISSUEWARPER_DESC.YISSUEWARPER_Yearid = " & YearId)
                        ElseIf FRMSTRING = "ISSUETOSIZER" Then
                            DT1 = OBJCMN.search(" ROUND(ISNULL(YARNISSUESIZER_DESC.YISSUESIZER_WT, 0),2) AS WT, ISNULL(YARNISSUESIZER_DESC.YISSUESIZER_BAGS, 0) AS BAGS, ISNULL(YARNISSUESIZER_DESC.YISSUESIZER_FRESH, 0) AS FRESH, ISNULL(YARNISSUESIZER_DESC.YISSUESIZER_WINDING, 0) AS WINDING, ISNULL(YARNISSUESIZER_DESC.YISSUESIZER_FIRKA, 0) AS FIRKA ", "", "   YARNISSUESIZER_DESC ", " AND YARNISSUESIZER_DESC.YISSUESIZER_NO = " & TEMPISSUENO & " AND YISSUESIZER_FROMNO = " & Val(row.Cells(GFROMNO.Index).Value) & " AND YISSUESIZER_FROMSRNO = " & Val(row.Cells(GFROMSRNO.Index).Value) & " AND YISSUESIZER_TYPE = '" & row.Cells(GGRIDTYPE.Index).Value & "' AND YARNISSUESIZER_DESC.YISSUESIZER_Yearid = " & YearId)
                        ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                            DT1 = OBJCMN.search(" ROUND(ISNULL(YARNISSUEWEAVER_DESC.YISSUEWEAVER_WT, 0),2) AS WT, ISNULL(YARNISSUEWEAVER_DESC.YISSUEWEAVER_BAGS, 0) AS BAGS, ISNULL(YARNISSUEWEAVER_DESC.YISSUEWEAVER_FRESH, 0) AS FRESH, ISNULL(YARNISSUEWEAVER_DESC.YISSUEWEAVER_WINDING, 0) AS WINDING, ISNULL(YARNISSUEWEAVER_DESC.YISSUEWEAVER_FIRKA, 0) AS FIRKA ", "", "  YARNISSUEWEAVER_DESC ", " AND YARNISSUEWEAVER_DESC.YISSUEWEAVER_NO = " & TEMPISSUENO & " AND YISSUEWEAVER_FROMNO = " & Val(row.Cells(GFROMNO.Index).Value) & " AND YISSUEWEAVER_FROMSRNO = " & Val(row.Cells(GFROMSRNO.Index).Value) & " AND YISSUEWEAVER_TYPE = '" & row.Cells(GGRIDTYPE.Index).Value & "'  AND YARNISSUEWEAVER_DESC.YISSUEWEAVER_Yearid = " & YearId)
                        ElseIf FRMSTRING = "ISSUETODYEING" Then
                            DT1 = OBJCMN.search(" ROUND(ISNULL(YARNISSUEDYEING_DESC.YISSUEDYEING_WT, 0),2) AS WT, ISNULL(YARNISSUEDYEING_DESC.YISSUEDYEING_BAGS, 0) AS BAGS, ISNULL(YARNISSUEDYEING_DESC.YISSUEDYEING_FRESH, 0) AS FRESH, ISNULL(YARNISSUEDYEING_DESC.YISSUEDYEING_WINDING, 0) AS WINDING, ISNULL(YARNISSUEDYEING_DESC.YISSUEDYEING_FIRKA, 0) AS FIRKA ", "", "  YARNISSUEDYEING_DESC ", " AND YARNISSUEDYEING_DESC.YISSUEDYEING_NO = " & TEMPISSUENO & " AND YISSUEDYEING_FROMNO = " & Val(row.Cells(GFROMNO.Index).Value) & " AND YISSUEDYEING_FROMSRNO = " & Val(row.Cells(GFROMSRNO.Index).Value) & " AND YISSUEDYEING_TYPE = '" & row.Cells(GGRIDTYPE.Index).Value & "'  AND YARNISSUEDYEING_DESC.YISSUEDYEING_Yearid = " & YearId)
                        End If
                        If DT1.Rows.Count > 0 Then
                            BALWT = BALWT + Val(DT1.Rows(0).Item("WT"))
                            BALFRESH = BALFRESH + Val(DT1.Rows(0).Item("FRESH"))
                            BALWINDING = BALWINDING + Val(DT1.Rows(0).Item("WINDING"))
                            BALFIRKA = BALFIRKA + Val(DT1.Rows(0).Item("FIRKA"))
                        End If
                    End If

                    If Val(row.Cells(Gwt.Index).Value) > Format(Val(BALWT), "0.000") Then
LINE1:
                        EP.SetError(LBLTOTALWT, "Wt Not Present only  " & Val(BALWT) & " Wt Allowed")
                        row.DefaultCellStyle.BackColor = Drawing.Color.Yellow
                        bln = False
                    End If


                    'If ClientName <> "JASHOK" Then
                    '    If Val(row.Cells(GFRESH.Index).Value) > 0 Then
                    '        If Val(row.Cells(GFRESH.Index).Value) > Format(Val(BALFRESH), "0") Then
                    '            EP.SetError(LBLTOTALFRESH, "Fresh Cones Not Present only " & Val(BALFRESH) & " Cones Allowed")
                    '            GRIDISSUE.CurrentRow.DefaultCellStyle.BackColor = Drawing.Color.Yellow
                    '            bln = False
                    '        End If
                    '    End If

                    '    If Val(row.Cells(GWINDING.Index).Value) > 0 Then
                    '        If Val(row.Cells(GWINDING.Index).Value) > Format(Val(BALWINDING), "0") Then
                    '            EP.SetError(LBLTOTALWINDING, "Winding Cones Not Present only " & Val(BALWINDING) & " Cones Allowed")
                    '            GRIDISSUE.CurrentRow.DefaultCellStyle.BackColor = Drawing.Color.Yellow
                    '            bln = False
                    '        End If
                    '    End If

                    '    If Val(row.Cells(GFIRKA.Index).Value) > 0 Then
                    '        If Val(row.Cells(GFIRKA.Index).Value) > Format(Val(BALFIRKA), "0") Then
                    '            EP.SetError(LBLTOTALFIRKA, "Firka Cones Not Present only " & Val(BALFIRKA) & " Cones Allowed")
                    '            GRIDISSUE.CurrentRow.DefaultCellStyle.BackColor = Drawing.Color.Yellow
                    '            bln = False
                    '        End If
                    '    End If

                    'End If
                End If
            Next
        End If


        'old code
        '        If GRIDISSUE.RowCount > 0 And EDIT = False Then
        '            For Each ROW As DataGridViewRow In GRIDISSUE.Rows
        '                If Val(ROW.Cells(GFROMNO.Index).Value) > 0 And Val(ROW.Cells(GFROMSRNO.Index).Value) > 0 And (ROW.Cells(GGRIDTYPE.Index).Value) <> "" And Val(ROW.Cells(Gwt.Index).Value) > 0 Then
        '                    Dim OBJCMN As New ClsCommonMaster
        '                    Dim dt As DataTable = OBJCMN.search(" WT, BAGS, FRESH, WINDING, FIRKA ", "", " STOCKVIEW_OURGODOWN ", " AND GODOWN='" & CMBOURGODOWN.Text.Trim & "' AND NO= " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND SRNO= " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND TYPE='" & ROW.Cells(GGRIDTYPE.Index).Value & "' AND Yearid = " & YearId)
        '                    If dt.Rows.Count <= 0 Then GoTo LINE1
        '                    If Val(ROW.Cells(Gwt.Index).Value) > Val(dt.Rows(0).Item("WT")) Or Val(ROW.Cells(GFRESH.Index).Value) > Val(dt.Rows(0).Item("FRESH")) Or Val(ROW.Cells(GWINDING.Index).Value) > Val(dt.Rows(0).Item("WINDING")) Or Val(ROW.Cells(GFIRKA.Index).Value) > Val(dt.Rows(0).Item("FIRKA")) Then
        'LINE1:
        '                        EP.SetError(LBLTOTALWT, "Stock Not Present ! ")
        '                        GRIDISSUE.CurrentRow.DefaultCellStyle.BackColor = Drawing.Color.Yellow
        '                        bln = False
        '                    End If
        '                End If
        '            Next
        '        End If

        '        If GRIDISSUE.RowCount > 0 And EDIT = True Then
        '            For Each ROW As DataGridViewRow In GRIDISSUE.Rows
        '                Dim BALWT As Double
        '                Dim BALFRESH, BALWINDING, BALFIRKA As Integer
        '                Dim OBJCMN As New ClsCommonMaster
        '                Dim dt As DataTable = OBJCMN.search(" WT, BAGS, FRESH, WINDING, FIRKA ", "", " STOCKVIEW_OURGODOWN ", " AND GODOWN='" & CMBOURGODOWN.Text.Trim & "' AND NO= " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND SRNO= " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND TYPE='" & ROW.Cells(GGRIDTYPE.Index).Value & "' AND Yearid = " & YearId)
        '                If dt.Rows.Count > 0 Then
        '                    BALWT = Format(Val(dt.Rows(0).Item("WT")), "0.000")
        '                    BALFRESH = Format(Val(dt.Rows(0).Item("FRESH")), "0")
        '                    BALWINDING = Format(Val(dt.Rows(0).Item("WINDING")), "0")
        '                    BALFIRKA = Format(Val(dt.Rows(0).Item("FIRKA")), "0")
        '                End If

        '                Dim DT1 As DataTable
        '                If FRMSTRING = "ISSUETOWARPER" Then
        '                    DT1 = OBJCMN.search(" ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_WT, 0) AS WT, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_BAGS, 0) AS BAGS, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_FRESH, 0) AS FRESH, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_WINDING, 0) AS WINDING, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_FIRKA, 0) AS FIRKA ", "", "   YARNISSUEWARPER_DESC ", " AND YARNISSUEWARPER_DESC.YISSUEWARPER_NO = " & TEMPISSUENO & " AND YISSUEWARPER_FROMNO = " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND YISSUEWARPER_FROMSRNO = " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND YISSUEWARPER_TYPE = '" & ROW.Cells(GGRIDTYPE.Index).Value & "' AND YARNISSUEWARPER_DESC.YISSUEWARPER_Yearid = " & YearId)
        '                ElseIf FRMSTRING = "ISSUETOSIZER" Then
        '                    DT1 = OBJCMN.search(" ISNULL(YARNISSUESIZER_DESC.YISSUESIZER_WT, 0) AS WT, ISNULL(YARNISSUESIZER_DESC.YISSUESIZER_BAGS, 0) AS BAGS, ISNULL(YARNISSUESIZER_DESC.YISSUESIZER_FRESH, 0) AS FRESH, ISNULL(YARNISSUESIZER_DESC.YISSUESIZER_WINDING, 0) AS WINDING, ISNULL(YARNISSUESIZER_DESC.YISSUESIZER_FIRKA, 0) AS FIRKA ", "", "   YARNISSUESIZER_DESC ", " AND YARNISSUESIZER_DESC.YISSUESIZER_NO = " & TEMPISSUENO & " AND YISSUESIZER_FROMNO = " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND YISSUESIZER_FROMSRNO = " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND YISSUESIZER_TYPE = '" & ROW.Cells(GGRIDTYPE.Index).Value & "' AND YARNISSUESIZER_DESC.YISSUESIZER_Yearid = " & YearId)
        '                ElseIf FRMSTRING = "ISSUETOWEAVER" Then
        '                    DT1 = OBJCMN.search(" ISNULL(YARNISSUEWEAVER_DESC.YISSUEWEAVER_WT, 0) AS WT, ISNULL(YARNISSUEWEAVER_DESC.YISSUEWEAVER_BAGS, 0) AS BAGS, ISNULL(YARNISSUEWEAVER_DESC.YISSUEWEAVER_FRESH, 0) AS FRESH, ISNULL(YARNISSUEWEAVER_DESC.YISSUEWEAVER_WINDING, 0) AS WINDING, ISNULL(YARNISSUEWEAVER_DESC.YISSUEWEAVER_FIRKA, 0) AS FIRKA ", "", "  YARNISSUEWEAVER_DESC ", " AND YARNISSUEWEAVER_DESC.YISSUEWEAVER_NO = " & TEMPISSUENO & " AND YISSUEWEAVER_FROMNO = " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND YISSUEWEAVER_FROMSRNO = " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND YISSUEWEAVER_TYPE = '" & ROW.Cells(GGRIDTYPE.Index).Value & "'  AND YARNISSUEWEAVER_DESC.YISSUEWEAVER_Yearid = " & YearId)
        '                End If
        '                If DT1.Rows.Count > 0 Then
        '                    BALWT = BALWT + Val(DT1.Rows(0).Item("WT"))
        '                    BALFRESH = BALFRESH + Val(DT1.Rows(0).Item("FRESH"))
        '                    BALWINDING = BALWINDING + Val(DT1.Rows(0).Item("WINDING"))
        '                    BALFIRKA = BALFIRKA + Val(DT1.Rows(0).Item("FIRKA"))
        '                End If

        '                If Val(ROW.Cells(Gwt.Index).Value) > 0 Then
        '                    If Val(ROW.Cells(Gwt.Index).Value) > Format(Val(BALWT), "0.000") Then
        '                        EP.SetError(LBLTOTALWT, "Wt Not Present only  " & Val(BALWT) & " Wt Allowed")
        '                        GRIDISSUE.CurrentRow.DefaultCellStyle.BackColor = Drawing.Color.Yellow
        '                        bln = False
        '                    End If
        '                End If
        '                If Val(ROW.Cells(GFRESH.Index).Value) > 0 Then
        '                    If Val(ROW.Cells(GFRESH.Index).Value) > Format(Val(BALFRESH), "0") Then
        '                        EP.SetError(LBLTOTALFRESH, "Fresh Cones Not Present only " & Val(BALFRESH) & " Cones Allowed")
        '                        GRIDISSUE.CurrentRow.DefaultCellStyle.BackColor = Drawing.Color.Yellow
        '                        bln = False
        '                    End If
        '                End If

        '                If Val(ROW.Cells(GWINDING.Index).Value) > 0 Then
        '                    If Val(ROW.Cells(GWINDING.Index).Value) > Format(Val(BALWINDING), "0") Then
        '                        EP.SetError(LBLTOTALWINDING, "Winding Cones Not Present only " & Val(BALWINDING) & " Cones Allowed")
        '                        GRIDISSUE.CurrentRow.DefaultCellStyle.BackColor = Drawing.Color.Yellow
        '                        bln = False
        '                    End If
        '                End If

        '                If Val(ROW.Cells(GFIRKA.Index).Value) > 0 Then
        '                    If Val(ROW.Cells(GFIRKA.Index).Value) > Format(Val(BALFIRKA), "0") Then
        '                        EP.SetError(LBLTOTALFIRKA, "Firka Cones Not Present only " & Val(BALFIRKA) & " Cones Allowed")
        '                        GRIDISSUE.CurrentRow.DefaultCellStyle.BackColor = Drawing.Color.Yellow
        '                        bln = False
        '                    End If
        '                End If
        '                BALWT = 0
        '                BALFIRKA = 0
        '                BALWINDING = 0
        '                BALFRESH = 0
        '            Next
        '        End If

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
            If FRMSTRING = "ISSUETOWARPER" Then
                Dim OBJDO As New ClsYarnIssueToWarper
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPISSUENO)
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

            ElseIf FRMSTRING = "ISSUETOADDA" Then
                Dim OBJDO As New ClsYarnIssueToAdda
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPISSUENO)
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

            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                Dim OBJDO As New ClsYarnIssueToSizer
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPISSUENO)
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

            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                Dim OBJWEAVER As New ClsYarnIssueToWeaver
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPISSUENO)
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

            ElseIf FRMSTRING = "ISSUETODYEING" Then
                Dim OBJDYEING As New ClsYarnIssueToDyeing
                For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                    Dim MS As New IO.MemoryStream
                    Dim ALPARAVAL As New ArrayList
                    If row.Cells(GUSRNO.Index).Value <> Nothing Then
                        ALPARAVAL.Add(TEMPISSUENO)
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
            If FRMSTRING = "ISSUETOWARPER" Or FRMSTRING = "ISSUETOADDA" Then
                If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')") ''''TYPE = WARPER
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'") ''''TYPE = SIZER
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'") ''''TYPE = WEAVER
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'") ''''TYPE = PROCESSOR
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If FRMSTRING = "ISSUETOWARPER" Or FRMSTRING = "ISSUETOADDA" Then
                If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WARPER")
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "SIZER")
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WEAVER")
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
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
                If FRMSTRING = "ISSUETOWARPER" Or FRMSTRING = "ISSUETOADDA" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')"
                ElseIf FRMSTRING = "ISSUETOSIZER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'"
                ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'"
                ElseIf FRMSTRING = "ISSUETODYEING" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                End If
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
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
            LBLTOTALBAGS.Text = 0
            LBLTOTALGROSSWT.Text = 0.0
            LBLTOTALTAREWT.Text = 0.0
            LBLTOTALFRESH.Text = 0.0
            LBLTOTALWINDING.Text = 0.0
            LBLTOTALFIRKA.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDISSUE.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTALBAGS.Text = Format(Val(LBLTOTALBAGS.Text) + Val(ROW.Cells(GBAGS.Index).EditedFormattedValue), "0")
                    LBLTOTALGROSSWT.Text = Format(Val(LBLTOTALGROSSWT.Text) + Val(ROW.Cells(GGROSSWT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALTAREWT.Text = Format(Val(LBLTOTALTAREWT.Text) + Val(ROW.Cells(GTAREWT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(Gwt.Index).EditedFormattedValue), "0.000")
                    LBLTOTALFRESH.Text = Format(Val(LBLTOTALFRESH.Text) + Val(ROW.Cells(GFRESH.Index).EditedFormattedValue), "0")
                    LBLTOTALWINDING.Text = Format(Val(LBLTOTALWINDING.Text) + Val(ROW.Cells(GWINDING.Index).EditedFormattedValue), "0")
                    LBLTOTALFIRKA.Text = Format(Val(LBLTOTALFIRKA.Text) + Val(ROW.Cells(GFIRKA.Index).EditedFormattedValue), "0")
                End If
            Next
            BAGNOCOUNT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub BAGNOCOUNT()
        Try
            LBLBAGNOCOUNT.Text = 0
            Dim dic As New Dictionary(Of String, Integer)()
            Dim cellValue As String
            For i = 0 To GRIDISSUE.Rows.Count - 1
                If Not GRIDISSUE.Rows(i).IsNewRow Then
                    cellValue = GRIDISSUE(GBAGNO.Index, i).EditedFormattedValue.ToString()
                    If cellValue <> "" Then
                        If Not dic.ContainsKey(cellValue) Then
                            dic.Add(cellValue, 1)
                        Else
                            dic(cellValue) += 1
                        End If
                    End If
                End If
            Next
            LBLBAGNOCOUNT.Text = Val(dic.Count)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTSTOCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTSTOCK.Click
        Try
            If CMBOURGODOWN.Text.Trim = "" And GRIDISSUE.RowCount = 0 Then
                MsgBox("Please Select Godown First", MsgBoxStyle.Critical)
                CMBOURGODOWN.Focus()
                Exit Sub
            End If

            Dim DTTABLE As New DataTable
            Dim OBJSELECTGDN As New SelectYarnStock
            OBJSELECTGDN.GODOWN = CMBOURGODOWN.Text.Trim
            OBJSELECTGDN.PARTYNAME = cmbname.Text.Trim
            OBJSELECTGDN.FRMSTRING = FRMSTRING
            OBJSELECTGDN.ENTRYDATE = Format(Convert.ToDateTime(DTISSUEDATE.Text).Date, "dd/MM/yyyy")
            OBJSELECTGDN.ShowDialog()
            DTTABLE = OBJSELECTGDN.DT

            If DTTABLE.Rows.Count > 0 Then
                For Each dr As DataRow In DTTABLE.Rows
                    'FETCH CONES | GROSS | TARE | NETT WITH RESPECT TO BAGNO
                    If dr("BAGNO") <> "" And (dr("FROMTYPE") = "MACHINEYARNRECD" Or dr("FROMTYPE") = "DYEINGYARNRECD") Then
                        Dim OBJCMN As New ClsCommon
                        Dim DTMAC As New DataTable
                        If dr("FROMTYPE") = "DYEINGYARNRECD" Then DTMAC = OBJCMN.search(" ISNULL(YRECDDYEING_CONES, 0) AS CONES, ISNULL(YRECDDYEING_GROSSWT, 0) AS GROSSWT, ISNULL(YRECDDYEING_TAREWT, 0) AS TAREWT, ISNULL(YRECDDYEING_WT, 0) AS WT ", "", " YARNRECEIVEDDYEING_DESC ", " AND YRECDDYEING_NO = " & Val(dr("FROMNO")) & " AND YRECDDYEING_BAGNO = '" & dr("BAGNO") & "' AND YRECDDYEING_YEARID = " & YearId) Else DTMAC = OBJCMN.search(" ISNULL(YRECEIVEDMAC_CONES, 0) AS CONES, ISNULL(YRECEIVEDMAC_GROSSWT, 0) AS GROSSWT, ISNULL(YRECEIVEDMAC_TAREWT, 0) AS TAREWT, ISNULL(YRECEIVEDMAC_NETTWT, 0) AS WT ", "", " YARNRECEIVEDMACHINE_DESC ", " AND YRECEIVEDMAC_NO = " & Val(dr("FROMNO")) & " AND YRECEIVEDMAC_BAGNO = '" & dr("BAGNO") & "' AND YRECEIVEDMAC_YEARID = " & YearId)
                        If DTMAC.Rows.Count > 0 Then
                            dr("FRESH") = Val(DTMAC.Rows(0).Item("CONES"))
                            dr("GROSSWT") = Val(DTMAC.Rows(0).Item("GROSSWT"))
                            dr("TAREWT") = Val(DTMAC.Rows(0).Item("TAREWT"))
                            dr("WT") = Val(DTMAC.Rows(0).Item("WT"))
                        End If
                    End If
                    GRIDISSUE.Rows.Add(0, dr("QUALITY"), dr("MILLNAME"), dr("NAME"), dr("LOTNO"), dr("SHADE"), Format(Val(dr("BAGS")), "0"), dr("BAGNO"), Format(Val(dr("GROSSWT")), "0.000"), Format(Val(dr("TAREWT")), "0.000"), Format(Val(dr("WT")), "0.000"), Val(dr("FRESH")), Val(dr("WINDING")), Val(dr("FIRKA")), "", dr("FROMNO"), dr("FROMSRNO"), dr("FROMTYPE"))
                Next
                GRIDISSUE.FirstDisplayedScrollingRowIndex = GRIDISSUE.RowCount - 1
                getsrno(GRIDISSUE)
                CMDSELECTSTOCK.Enabled = False

                If GRIDISSUE.RowCount > 0 Then
                    GRIDISSUE.CurrentCell = GRIDISSUE.Rows(0).Cells(GBAGS.Index)
                    GRIDISSUE.Focus()
                End If

                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDISSUE.RowCount = 0
LINE1:
            TEMPISSUENO = Val(TXTISSUENO.Text) - 1
Line2:
            If TEMPISSUENO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                If FRMSTRING = "ISSUETOWARPER" Then
                    DT = OBJCMN.search("YISSUEWARPER_NO ", "", "  YARNISSUEWARPER ", " AND YISSUEWARPER_NO = '" & TEMPISSUENO & "' AND YARNISSUEWARPER.YISSUEWARPER_YEARID = " & YearId)
                ElseIf FRMSTRING = "ISSUETOADDA" Then
                    DT = OBJCMN.search("YISSUEADDA_NO ", "", "  YARNISSUEADDA ", " AND YISSUEADDA_NO = '" & TEMPISSUENO & "' AND YARNISSUEADDA.YISSUEADDA_YEARID = " & YearId)
                ElseIf FRMSTRING = "ISSUETOSIZER" Then
                    DT = OBJCMN.search("YISSUESIZER_NO ", "", "  YARNISSUESIZER ", " AND YISSUESIZER_NO = '" & TEMPISSUENO & "' AND YARNISSUESIZER.YISSUESIZER_YEARID = " & YearId)
                ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                    DT = OBJCMN.search("YISSUEWEAVER_NO ", "", "  YARNISSUEWEAVER ", " AND YISSUEWEAVER_NO = '" & TEMPISSUENO & "' AND YARNISSUEWEAVER.YISSUEWEAVER_YEARID = " & YearId)
                ElseIf FRMSTRING = "ISSUETODYEING" Then
                    DT = OBJCMN.search("YISSUEDYEING_NO ", "", "  YARNISSUEDYEING ", " AND YISSUEDYEING_NO = '" & TEMPISSUENO & "' AND YARNISSUEDYEING.YISSUEDYEING_YEARID = " & YearId)
                End If
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    YarnIssueToWarper_Load(sender, e)
                Else
                    TEMPISSUENO = Val(TEMPISSUENO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDISSUE.RowCount = 0 And TEMPISSUENO > 1 Then
                TXTISSUENO.Text = TEMPISSUENO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDISSUE.RowCount = 0
LINE1:
            TEMPISSUENO = Val(TXTISSUENO.Text) + 1
            GETMAX_ISSUE_NO()
            Dim MAXNO As Integer = TXTISSUENO.Text.Trim
            CLEAR()
            If Val(TXTISSUENO.Text) - 1 >= TEMPISSUENO Then
                EDIT = True
                YarnIssueToWarper_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDISSUE.RowCount = 0 And TEMPISSUENO < MAXNO Then
                TXTISSUENO.Text = TEMPISSUENO
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
                GRIDISSUE.RowCount = 0
                TEMPISSUENO = Val(tstxtbillno.Text)
                If TEMPISSUENO > 0 Then
                    EDIT = True
                    YarnIssueToWarper_Load(sender, e)
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

    Private Sub GRIDISSUE_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDISSUE.CellValidating
        Try
            Dim colNum As Integer = GRIDISSUE.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case Gwt.Index, GGROSSWT.Index, GTAREWT.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDISSUE.CurrentCell.Value = Nothing Then GRIDISSUE.CurrentCell.Value = "0.000"
                        GRIDISSUE.CurrentCell.Value = Convert.ToDecimal(GRIDISSUE.Item(colNum, e.RowIndex).Value)
                        '' everything is good
                        TOTAL()
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If

                Case GBAGS.Index, GFRESH.Index, GWINDING.Index, GFIRKA.Index
                    Dim dDebit As Integer
                    Dim bValid As Boolean = Integer.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDISSUE.CurrentCell.Value = Nothing Then GRIDISSUE.CurrentCell.Value = "0"
                        GRIDISSUE.CurrentCell.Value = Convert.ToInt32(GRIDISSUE.Item(colNum, e.RowIndex).Value)
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

    Private Sub GRIDISSUE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDISSUE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDISSUE.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDISSUEUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDISSUE.Rows.RemoveAt(GRIDISSUE.CurrentRow.Index)
                getsrno(GRIDISSUE)
                TOTAL()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJWARPER As New YarnIssueDetails
            OBJWARPER.MdiParent = MDIMain
            OBJWARPER.FRMSTRING = FRMSTRING
            OBJWARPER.Show()
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
                TEMPMSG = MsgBox("Delete Yarn Isuue?", MsgBoxStyle.YesNo)


                If TEMPMSG = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPISSUENO)
                    alParaval.Add(YearId)

                    If FRMSTRING = "ISSUETOWARPER" Then
                        Dim ClsDO As New ClsYarnIssueToWarper
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "ISSUETOADDA" Then
                        Dim ClsDO As New ClsYarnIssueToADDA
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                        Dim ClsDO As New ClsYarnIssueToWeaver
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "ISSUETOSIZER" Then
                        Dim ClsDO As New ClsYarnIssueToSizer
                        ClsDO.alParaval = alParaval
                        IntResult = ClsDO.Delete()
                    ElseIf FRMSTRING = "ISSUETODYEING" Then
                        Dim ClsDO As New ClsYarnIssueToDyeing
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

    Private Sub DTISSUEDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTISSUEDATE.GotFocus
        DTISSUEDATE.Select(0, 0)
    End Sub

    Private Sub DTISSUEDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTISSUEDATE.Validating
        Try
            If DTISSUEDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTISSUEDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            If MsgBox("Wish To Print Report?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim OBJYARNISSUE As New YarnIssueDesign
            OBJYARNISSUE.MdiParent = MDIMain
            If FRMSTRING = "ISSUETOWARPER" Then
                OBJYARNISSUE.FRMSTRING = "YARNISSUEWARPER"
                OBJYARNISSUE.WHERECLAUSE = "{YARNISSUEWARPER.YISSUEWARPER_NO} = " & TEMPISSUENO & " AND {YARNISSUEWARPER.YISSUEWARPER_YEARID} = " & YearId
            ElseIf FRMSTRING = "ISSUETOADDA" Then
                OBJYARNISSUE.FRMSTRING = "YARNISSUEADDA"
                OBJYARNISSUE.WHERECLAUSE = "{YARNISSUEADDA.YISSUEADDA_NO} = " & TEMPISSUENO & " AND {YARNISSUEADDA.YISSUEADDA_YEARID} = " & YearId
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                OBJYARNISSUE.FRMSTRING = "YARNISSUESIZER"
                OBJYARNISSUE.WHERECLAUSE = "{YARNISSUESIZER.YISSUESIZER_NO} = " & TEMPISSUENO & " AND {YARNISSUESIZER.YISSUESIZER_YEARID} = " & YearId
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                OBJYARNISSUE.FRMSTRING = "YARNISSUEWEAVER"
                OBJYARNISSUE.WHERECLAUSE = "{YARNISSUEWEAVER.YISSUEWEAVER_NO} = " & TEMPISSUENO & " AND {YARNISSUEWEAVER.YISSUEWEAVER_YEARID} = " & YearId
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                OBJYARNISSUE.FRMSTRING = "YARNISSUEDYEING"
                OBJYARNISSUE.WHERECLAUSE = "{YARNISSUEDYEING.YISSUEDYEING_NO} = " & TEMPISSUENO & " AND {YARNISSUEDYEING.YISSUEDYEING_YEARID} = " & YearId
            End If
            OBJYARNISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Dim DT As New DataTable
        Dim OBJCMN As New ClsCommon
        If EDIT = True Then SENDWHATSAPP(TEMPISSUENO)
        DT = OBJCMN.Execute_Any_String("UPDATE YARNISSUEWEAVER SET YISSUEWEAVER_SENDWHATSAPP = 1 WHERE YISSUEWEAVER_no = " & TEMPISSUENO & " AND YISSUEWEAVER_yearid = " & YearId, "", "")
        LBLWHATSAPP.Visible = True
    End Sub

    Async Sub SENDWHATSAPP(ISSUENO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJCN As New YarnIssueDesign
            OBJCN.MdiParent = MDIMain

            If FRMSTRING = "ISSUETOWARPER" Then
                OBJCN.FRMSTRING = "YARNISSUEWARPER"
            ElseIf FRMSTRING = "ISSUETOADDA" Then
                OBJCN.FRMSTRING = "YARNISSUEADDA"
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                OBJCN.FRMSTRING = "YARNISSUESIZER"
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                OBJCN.FRMSTRING = "YARNISSUEWEAVER"
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                OBJCN.FRMSTRING = "YARNISSUEDYEING"
            End If

            OBJCN.DIRECTMAIL = False
            OBJCN.DIRECTPRINT = True
            OBJCN.DIRECTWHATSAPP = True
            OBJCN.PARTYNAME = cmbname.Text.Trim
            OBJCN.ISSUENO = Val(ISSUENO)
            OBJCN.NOOFCOPIES = 1
            OBJCN.Show()
            OBJCN.Close()


            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = cmbname.Text.Trim
            'OBJWHATSAPP.AGENTNAME = cmbtrans.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & cmbname.Text.Trim & "_" & OBJCN.FRMSTRING & "_" & Val(ISSUENO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add(cmbname.Text.Trim & OBJCN.FRMSTRING & "_" & Val(ISSUENO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        If EDIT = True Then PRINTREPORT()
    End Sub

    Private Sub YarnIssue_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName <> "SASHWINKUMAR" Then
                GBAGNO.ReadOnly = True
                GGROSSWT.ReadOnly = True
                GTAREWT.ReadOnly = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDISSUE_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDISSUE.CellEnter
        Try
            If ClientName <> "SASHWINKUMAR" Then
                If GRIDISSUE.CurrentRow.Cells(e.ColumnIndex).ReadOnly = True Then SendKeys.Send("{tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTADDANO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTADDANO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTADDANO_Validating(sender As Object, e As CancelEventArgs) Handles TXTADDANO.Validating
        Try
            If TXTADDANO.Text.Trim <> "" AndAlso ((EDIT = False) Or (EDIT = True And TXTADDANO.Text.Trim <> TEMPADDANO)) Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(YISSUEADDA_NO, '') AS ISSUENO", "", " YARNISSUEADDA ", " AND YISSUEADDA_ADDANO = " & Val(TXTADDANO.Text.Trim) & " AND YISSUEADDA_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Adda No Already Present !", MsgBoxStyle.Critical)
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class