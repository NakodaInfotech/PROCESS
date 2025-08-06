
Imports System.ComponentModel
Imports BL

Public Class GreyRecdWeaverWeftChange

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean   'USED FOR RIGHT MANAGEMAENT
    Dim GRIDUPLOADDOUBLECLICK, GRIDDOUBLECLICK As Boolean
    Dim TEMPUPLOADROW, TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPGREYRECDNO As Integer
    Dim UPLOADDT As New DataTable
    Dim LOOMNOUPDATED As New List(Of Integer)

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        DTGREYRECDDATE.Focus()
    End Sub

    Sub TOTAL()
        Try
            TXTTOTAL.Text = 0.0
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALBALANCECUT.Text = 0

            Dim DONE As Boolean = False
            GRIDGREYSUMMARY.RowCount = 0


            For Each ROW As DataGridViewRow In GRIDGREY.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    TXTTOTAL.Text = Format(Val(TXTTOTAL.Text) + Val(ROW.Cells(GCUT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.000")
                End If

                DONE = False
                If Val(ROW.Cells(GCUT.Index).EditedFormattedValue) > 0 Then
                    If GRIDGREYSUMMARY.RowCount = 0 Then
                        GRIDGREYSUMMARY.Rows.Add(ROW.Cells(GQUALITY.Index).Value, ROW.Cells(GSHADE.Index).Value, Format(Val(ROW.Cells(GCUT.Index).EditedFormattedValue), "0.00"), Format(Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00"), Format(Val(ROW.Cells(GQUALITYWT.Index).Value), "0.000"))
                    Else
                        For Each SUMMROW As DataGridViewRow In GRIDGREYSUMMARY.Rows
                            If SUMMROW.Cells(TGREYQUALITY.Index).Value = ROW.Cells(GQUALITY.Index).Value And SUMMROW.Cells(TSHADE.Index).Value = ROW.Cells(GSHADE.Index).Value Then 'And SUMMROW.Cells(TPCS.Index).Value = ROW.Cells(GCUT.Index).EditedFormattedValue And SUMMROW.Cells(TMTRS.Index).Value = ROW.Cells(GMTRS.Index).EditedFormattedValue And SUMMROW.Cells(TQUALITYWT.Index).Value = ROW.Cells(GQUALITYWT.Index).EditedFormattedValue Then
                                SUMMROW.Cells(TPCS.Index).Value = Val(SUMMROW.Cells(TPCS.Index).Value) + Val(ROW.Cells(GCUT.Index).EditedFormattedValue)
                                SUMMROW.Cells(TMTRS.Index).Value = Val(SUMMROW.Cells(TMTRS.Index).Value) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue)
                                SUMMROW.Cells(TQUALITYWT.Index).Value = Val(SUMMROW.Cells(TQUALITYWT.Index).Value) + Val(ROW.Cells(GQUALITYWT.Index).EditedFormattedValue)
                                DONE = True
                            End If
                        Next
                        If DONE = False Then GRIDGREYSUMMARY.Rows.Add(ROW.Cells(GQUALITY.Index).Value, ROW.Cells(GSHADE.Index).Value, Format(Val(ROW.Cells(GCUT.Index).EditedFormattedValue), "0.00"), Format(Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00"), Val(ROW.Cells(GQUALITYWT.Index).Value))
                    End If
                End If

                LBLTOTALBALANCECUT.Text = Format(Val(LBLTOTALBALANCECUT.Text) + Val(ROW.Cells(GBALANCE.Index).Value), "0.00")
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        If LOOMNOUPDATED IsNot Nothing Then LOOMNOUPDATED.Clear()

        UPLOADDT.Clear()
        tstxtbillno.Clear()
        TXTGREYRECDNO.Clear()
        DTGREYRECDDATE.Text = Mydate
        CMBGODOWN.Text = GETDEFAULTGODOWN()
        CMBNAME.Text = ""
        CMBNAME.Enabled = True
        cmbtrans.Text = ""
        TXTCHALLANNO.Clear()

        CMBBEAMNO.Items.Clear()
        CMBBEAMNO.Text = ""
        CMBUPLOADLOOMNO.Items.Clear()
        CMBUPLOADLOOMNO.Text = ""
        TXTUPLOADBEAMNAME.Clear()
        TXTBALANCECUT.Clear()

        CMBUNLOADLOOMNO.Items.Clear()
        CMBUNLOADLOOMNO.Text = ""
        TXTUNLOADBEAMNO.Clear()
        TXTUNLOADBEAMNAME.Clear()


        TXTSRNO.Clear()
        TXTLOOMNO.Clear()
        TXTBEAMNAME.Clear()
        TXTBEAMNO.Clear()
        TXTGRIDBALANCECUT.Clear()
        TXTTAKA.Text = 1
        CMBGRIDGREYQUALITY.Items.Clear()
        CMBGRIDGREYQUALITY.Text = ""
        CMBGRIDSHADE.Items.Clear()
        CMBGRIDSHADE.Text = ""
        TXTTAKANO.Clear()
        TXTMTRS.Clear()
        TXTWT.Clear()
        CMBGRIDPIECETYPE.Text = ""
        TXTTPNARRATION.Clear()
        TXTNARRATION.Clear()


        CMBGREYQUALITY.Items.Clear()
        CMBGREYQUALITY.Text = ""
        CMBSHADE.Items.Clear()
        CMBSHADE.Text = ""
        CMBPIECETYPE.Text = ""


        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False
        GRIDGREY.RowCount = 0

        TXTTOTAL.Text = 0
        LBLTOTALMTRS.Text = 0.0
        LBLTOTALWT.Text = 0.0
        LBLTOTALBALANCECUT.Text = 0

        GETMAX_BEAMRECD_NO()

        GRIDUPLOADDOUBLECLICK = False

        TabControl1.SelectedIndex = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0

        TXTUPLOADSRNO.Text = 1

        GRIDGREYSUMMARY.RowCount = 0

    End Sub

    Sub GETMAX_BEAMRECD_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(GRECDWEAVER_NO),0)+1", "GREYRECEIVEDWEAVERDESIGN", "AND GRECDWEAVER_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTGREYRECDNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub GreyRecdFromWeaver_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
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
        If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
        If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT, " and GODOWN_ISOUR = 'True'")
        If CMBGRIDPIECETYPE.Text.Trim = "" Then fillPIECETYPE(CMBGRIDPIECETYPE)
    End Sub

    Private Sub GreyRecdWeaverWeftChange_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If UPLOADDT.Columns.Count = 0 Then
                UPLOADDT.Columns.Add("WEAVERNAME")
                UPLOADDT.Columns.Add("LOOMNO")
                UPLOADDT.Columns.Add("BEAMNAME")
                UPLOADDT.Columns.Add("BEAMNO")
                UPLOADDT.Columns.Add("TOTALCUT")
                UPLOADDT.Columns.Add("BALANCECUT")
                UPLOADDT.Columns.Add("FROMNO")
                UPLOADDT.Columns.Add("FROMSRNO")
                UPLOADDT.Columns.Add("TYPE")
            End If


            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dttable As New DataTable
                Dim OBJGREYREC As New ClsGreyRecdFromWeaverDesign

                OBJGREYREC.alParaval.Add(TEMPGREYRECDNO)
                OBJGREYREC.alParaval.Add(YearId)
                dttable = OBJGREYREC.SELECTGREY()

                If dttable.Rows.Count > 0 Then

                    TXTGREYRECDNO.Text = TEMPGREYRECDNO
                    DTGREYRECDDATE.Text = dttable.Rows(0).Item("DATE")
                    CMBNAME.Text = dttable.Rows(0).Item("NAME").ToString
                    CMBGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                    cmbtrans.Text = dttable.Rows(0).Item("TRANSNAME").ToString
                    TXTCHALLANNO.Text = dttable.Rows(0).Item("CHALLANNO").ToString

                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDGREY.Rows.Add(Val(ROW("SRNO")), ROW("LOOMNO"), ROW("BEAMNAME"), ROW("BEAMNO"), Val(ROW("BALANCEBEAM")), Format(Val(ROW("PCS")), "0.00"), ROW("GREYQUALITY"), ROW("SHADE"), Format(Val(ROW("TAKANO")), "0"), Format(Val(ROW("MTRS")), "0.000"), Format(Val(ROW("WT")), "0.000"), ROW("PIECETYPE"), ROW("TPNARRATION"), ROW("NARRATION"), Format(Val(ROW("QUALITYWT")), "0.000"), ROW("FROMNO"), ROW("FROMSRNO"), ROW("TYPE"), Val(ROW("OUTPCS")))
                        If Val(ROW("OUTPCS")) > 0 Then
                            GRIDGREY.Rows(GRIDGREY.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                        End If
                    Next


                    'UPLOAD(GRID)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" GREYRECEIVEDWEAVER_UPLOAD.GRECDWEAVER_SRNO AS GRIDSRNO, GREYRECEIVEDWEAVER_UPLOAD.GRECDWEAVER_REMARKS AS REMARKS, GREYRECEIVEDWEAVER_UPLOAD.GRECDWEAVER_NAME AS NAME, GREYRECEIVEDWEAVER_UPLOAD.GRECDWEAVER_PHOTO AS IMGPATH ", "", " GREYRECEIVEDWEAVER_UPLOAD ", " AND GREYRECEIVEDWEAVER_UPLOAD.GRECDWEAVER_NO = " & TEMPGREYRECDNO & " AND GRECDWEAVER_YEARID = " & YearId & " ORDER BY GREYRECEIVEDWEAVER_UPLOAD.GRECDWEAVER_SRNO")
                    If DT.Rows.Count > 0 Then
                        For Each DTR As DataRow In DT.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If
                    FILLPREVIOUSBEAM()
                    FILLDATABEAM()
                    FILLUPLOADLOOM()
                    FILLUPLOADBEAM()
                    FILLUNLOADLOOM()

                    TOTAL()
                    getsrno(GRIDGREY)

                    CMBNAME.Enabled = False
                    cmbtrans.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLPREVIOUSBEAM()
        Try
            Dim WHERECLAUSE As String = ""
            If EDIT = False Then WHERECLAUSE = " AND BEAMUPLOAD_GREYRECNO = (SELECT TOP 1 BEAMUPLOAD_GREYRECNO FROM BEAMUPLOAD INNER JOIN LEDGERS ON BEAMUPLOAD_WEAVERID = LEDGERS.ACC_ID  WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND BEAMUPLOAD_YEARID = " & YearId & WHERECLAUSE & " ORDER BY BEAMUPLOAD_GREYRECNO DESC) " Else WHERECLAUSE = " AND BEAMUPLOAD_GREYRECNO = " & TEMPGREYRECDNO

            'FETCH DATA FROM BEAMUPLOAD IN UPLOADDATATABLE
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" LEDGERS.Acc_cmpname AS WEAVERNAME, BEAMUPLOAD.BEAMUPLOAD_LOOMNO AS LOOMNO, BEAMUPLOAD.BEAMUPLOAD_BEAMNO AS BEAMNO, BEAMUPLOAD.BEAMUPLOAD_FROMNO AS FROMNO, BEAMUPLOAD.BEAMUPLOAD_FROMSRNO AS FROMSRNO, BEAMUPLOAD.BEAMUPLOAD_TYPE AS TYPE  ", "", " BEAMUPLOAD INNER JOIN LEDGERS ON BEAMUPLOAD.BEAMUPLOAD_WEAVERID = LEDGERS.Acc_id  ", " AND ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND BEAMUPLOAD_YEARID = " & YearId & WHERECLAUSE & "  ORDER BY BEAMUPLOAD_LOOMNO ")
            If DT.Rows.Count > 0 Then
                For Each DTR As DataRow In DT.Rows
                    Dim DTDATA As DataTable = OBJCMN.search(" T.BEAMNAME, T.TOTALCUT, T.BALANCECUT ", "", " (SELECT ISNULL(BEAMMASTER.BEAM_NAME,'') AS BEAMNAME, BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT as TOTALCUT, (BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT - BEAMISSUETOWEAVER_DESC.BEAMISSUE_OUTCUT) AS BALANCECUT, BEAMISSUETOWEAVER.BEAMISSUE_NO AS FROMNO, BEAMISSUETOWEAVER_DESC.BEAMISSUE_GRIDSRNO AS FROMSRNO, 'BEAMISSUE' AS TYPE FROM  BEAMISSUETOWEAVER INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID INNER JOIN LEDGERS ON BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID = LEDGERS.Acc_id INNER JOIN BEAMMASTER ON BEAMISSUETOWEAVER_DESC.BEAMISSUE_BEAMID = BEAMMASTER.BEAM_ID WHERE BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID = " & YearId & " UNION ALL SELECT ISNULL(BEAMMASTER.BEAM_NAME,'') AS BEAMNAME, SMBEAMWEAVER_CUT as TOTALCUT, (SMBEAMWEAVER_CUT - SMBEAMWEAVER_OUTCUT) AS BALANCECUT, SMBEAMWEAVER_NO AS FROMNO, SMBEAMWEAVER_NO AS FROMSRNO, 'OPENING' AS TYPE  FROM  STOCKMASTER_BEAMWEAVER  INNER JOIN LEDGERS ON SMBEAMWEAVER_WEAVERID= LEDGERS.Acc_id INNER JOIN BEAMMASTER ON SMBEAMWEAVER_BEAMID = BEAMMASTER.BEAM_ID WHERE SMBEAMWEAVER_YEARID  = " & YearId & ") AS T", " AND T.FROMNO = " & DTR("FROMNO") & " AND T.FROMSRNO = " & DTR("FROMSRNO") & " AND T.TYPE = '" & DTR("TYPE") & "'")
                    UPLOADDT.Rows.Add(CMBNAME.Text.Trim, DTR("LOOMNO"), DTDATA.Rows(0).Item("BEAMNAME"), DTR("BEAMNO"), Val(DTDATA.Rows(0).Item("TOTALCUT")), Val(DTDATA.Rows(0).Item("BALANCECUT")), DTR("FROMNO"), DTR("FROMSRNO"), DTR("TYPE"))
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DTGREYRECDDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBGODOWN.Text.Trim)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(TXTCHALLANNO.Text.Trim)

            alParaval.Add(Val(TXTTOTAL.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim SRNO As String = ""
            Dim LOOMNO As String = ""
            Dim BEAMNAME As String = ""
            Dim BEAMNO As String = ""
            Dim BALANCEBEAM As String = ""
            Dim PCS As String = ""
            Dim GREYQUALITY As String = ""
            Dim SHADE As String = ""
            Dim TAKANO As String = ""
            Dim MTRS As String = ""
            Dim WT As String = ""
            Dim PIECETYPE As String = ""
            Dim TPNARRATION As String = ""
            Dim NARRATION As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim TYPE As String = ""
            Dim OUTPCS As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDGREY.Rows
                If SRNO = "" Then
                    SRNO = row.Cells(GSRNO.Index).Value
                    LOOMNO = Val(row.Cells(GLOOMNO.Index).Value)
                    BEAMNAME = row.Cells(GBEAMNAME.Index).Value.ToString
                    BEAMNO = row.Cells(GBEAMNO.Index).Value.ToString
                    BALANCEBEAM = Val(row.Cells(GBALANCE.Index).Value)
                    PCS = Val(row.Cells(GCUT.Index).Value)
                    GREYQUALITY = row.Cells(GQUALITY.Index).Value.ToString
                    SHADE = row.Cells(GSHADE.Index).Value.ToString
                    TAKANO = Val(row.Cells(GTAKANO.Index).Value)
                    MTRS = Val(row.Cells(GMTRS.Index).Value)
                    WT = Val(row.Cells(GWT.Index).Value)
                    PIECETYPE = row.Cells(GPIECETYPE.Index).Value
                    TPNARRATION = row.Cells(GTPNARRATION.Index).Value
                    NARRATION = row.Cells(GNARRATION.Index).Value
                    FROMNO = row.Cells(GFROMNO.Index).Value.ToString
                    FROMSRNO = row.Cells(GFROMSRNO.Index).Value.ToString
                    TYPE = row.Cells(GTYPE.Index).Value.ToString
                    OUTPCS = Val(row.Cells(GOUTPCS.Index).Value)
                Else
                    SRNO = SRNO & "|" & row.Cells(GSRNO.Index).Value
                    LOOMNO = LOOMNO & "|" & Val(row.Cells(GLOOMNO.Index).Value)
                    BEAMNAME = BEAMNAME & "|" & row.Cells(GBEAMNAME.Index).Value.ToString
                    BEAMNO = BEAMNO & "|" & row.Cells(GBEAMNO.Index).Value.ToString
                    BALANCEBEAM = BALANCEBEAM & "|" & Val(row.Cells(GBALANCE.Index).Value)
                    PCS = PCS & "|" & Val(row.Cells(GCUT.Index).Value)
                    GREYQUALITY = GREYQUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                    SHADE = SHADE & "|" & row.Cells(GSHADE.Index).Value.ToString
                    TAKANO = TAKANO & "|" & Val(row.Cells(GTAKANO.Index).Value)
                    MTRS = MTRS & "|" & Val(row.Cells(GMTRS.Index).Value)
                    WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
                    PIECETYPE = PIECETYPE & "|" & row.Cells(GPIECETYPE.Index).Value
                    TPNARRATION = TPNARRATION & "|" & row.Cells(GTPNARRATION.Index).Value
                    NARRATION = NARRATION & "|" & row.Cells(GNARRATION.Index).Value
                    FROMNO = FROMNO & "|" & row.Cells(GFROMNO.Index).Value.ToString
                    FROMSRNO = FROMSRNO & "|" & row.Cells(GFROMSRNO.Index).Value.ToString
                    TYPE = TYPE & "|" & row.Cells(GTYPE.Index).Value.ToString
                    OUTPCS = OUTPCS & "|" & Val(row.Cells(GOUTPCS.Index).Value)
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(LOOMNO)
            alParaval.Add(BEAMNAME)
            alParaval.Add(BEAMNO)
            alParaval.Add(BALANCEBEAM)
            alParaval.Add(PCS)
            alParaval.Add(GREYQUALITY)
            alParaval.Add(SHADE)
            alParaval.Add(TAKANO)
            alParaval.Add(MTRS)
            alParaval.Add(WT)
            alParaval.Add(PIECETYPE)
            alParaval.Add(TPNARRATION)
            alParaval.Add(NARRATION)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(TYPE)
            alParaval.Add(OUTPCS)


            Dim BEAMUPLOADLOOMNO As String = ""
            Dim BEAMUPLOADBEAMNO As String = ""
            Dim BEAMUPLOADFROMNO As String = ""
            Dim BEAMUPLOADFROMSRNO As String = ""
            Dim BEAMUPLOADTYPE As String = ""

            For Each DTROW As DataRow In UPLOADDT.Rows
                If BEAMUPLOADLOOMNO = "" Then
                    BEAMUPLOADLOOMNO = DTROW("LOOMNO")
                    BEAMUPLOADBEAMNO = DTROW("BEAMNO")
                    BEAMUPLOADFROMNO = DTROW("FROMNO")
                    BEAMUPLOADFROMSRNO = DTROW("FROMSRNO")
                    BEAMUPLOADTYPE = DTROW("TYPE")
                Else
                    BEAMUPLOADLOOMNO = BEAMUPLOADLOOMNO & "|" & DTROW("LOOMNO")
                    BEAMUPLOADBEAMNO = BEAMUPLOADBEAMNO & "|" & DTROW("BEAMNO")
                    BEAMUPLOADFROMNO = BEAMUPLOADFROMNO & "|" & DTROW("FROMNO")
                    BEAMUPLOADFROMSRNO = BEAMUPLOADFROMSRNO & "|" & DTROW("FROMSRNO")
                    BEAMUPLOADTYPE = BEAMUPLOADTYPE & "|" & DTROW("TYPE")
                End If
            Next
            alParaval.Add(BEAMUPLOADLOOMNO)
            alParaval.Add(BEAMUPLOADBEAMNO)
            alParaval.Add(BEAMUPLOADFROMNO)
            alParaval.Add(BEAMUPLOADFROMSRNO)
            alParaval.Add(BEAMUPLOADTYPE)


            Dim OBJGREYREC As New ClsGreyRecdFromWeaverDesign
            OBJGREYREC.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJGREYREC.SAVE()
                TEMPGREYRECDNO = DT.Rows(0).Item(0)
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPGREYRECDNO)
                IntResult = OBJGREYREC.UPDATE()
                EDIT = False
                MsgBox("Details Updated")


                'AFTER UPDATING THE RECORDS WE NEED TO CHECK WHETHER NEW BEAMS ARE UPLOADED OR NOT 
                'IF NEW BEAMS ARE UPLOADED IN EDIT MODE THEN WE NEED TO UPDATE ALL THE FURTHER RECORDS WITHN RESPECT
                'TO THE BEAM UPLOADED

                If LOOMNOUPDATED IsNot Nothing Then
                    If LOOMNOUPDATED.Count > 0 Then
                        'FIRST GET DISTINCT ARRAYLIST AND THEN LOOP
                        Dim DISTINCTLOOMNO As List(Of Integer) = LOOMNOUPDATED.Distinct().ToList
                        For Each TEMPLOOMNO As Integer In DISTINCTLOOMNO

                            'GET THE GRIDROWNO FROM WHERE WE NEED TO PICK THE DATA OF THE LOOM
                            Dim TEMPGREYROWNO As Integer = 0
                            For Each ROW As DataGridViewRow In GRIDGREY.Rows
                                If Val(ROW.Cells(GLOOMNO.Index).Value) = TEMPLOOMNO Then
                                    TEMPGREYROWNO = ROW.Index
                                    Exit For
                                End If
                            Next

                            'ONCE WE GET THE LOOM NO WE WILL FIND THE NEXT ENTRIES FROM MASTER AND THEN UPDATE THE TABLE
                            'THE TABLE NAME IS BEAMUPLOAD AND GREYRECEIVEDWEAVER_DESC
                            Dim OBJCMN As New ClsCommon
                            Dim GREYNOTOUPADTE As DataTable = OBJCMN.search("GRECDWEAVER_NO AS GREYRECDNO", "", " GREYRECEIVEDWEAVERDESIGN INNER JOIN LEDGERS ON GRECDWEAVER_LEDGERID = LEDGERS.ACC_ID ", " AND GRECDWEAVER_NO > " & TEMPGREYRECDNO & " AND GRECDWEAVER_DATE > '" & Format(Convert.ToDateTime(DTGREYRECDDATE.Text).Date, "MM/dd/yyyy") & "' AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND GRECDWEAVER_YEARID = " & YearId & " ORDER BY GRECDWEAVER_NO")
                            If GREYNOTOUPADTE.Rows.Count > 0 Then

                                'THESE ARE THE GREY NOS WE NEED TO UPDATE
                                Dim BALANCECUT As Double = Val(GRIDGREY.Rows(TEMPGREYROWNO).Cells(GBALANCE.Index).Value) - Val(GRIDGREY.Rows(TEMPGREYROWNO).Cells(GCUT.Index).Value)
                                Dim INTRES As New DataTable
                                For Each GREYROW As DataRow In GREYNOTOUPADTE.Rows

                                    'FIRST UNDO THE OUTCUT AND THEN UPDATE NEW CUTS
                                    INTRES = OBJCMN.Execute_Any_String("SELECT GRECDWEAVER_PCS AS OLDPCS, GRECDWEAVER_FROMNO AS OLDFROMNO, GRECDWEAVER_FROMSRNO AS OLDFROMSRNO, GRECDWEAVER_TYPE AS OLDTYPE FROM GREYRECEIVEDWEAVER_DESC WHERE GRECDWEAVER_NO = " & Val(GREYROW("GREYRECDNO")) & " AND GRECDWEAVER_LOOMNO = '" & TEMPLOOMNO & "' AND GRECDWEAVER_YEARID = " & YearId, "", "")
                                    If INTRES.Rows(0).Item("OLDTYPE") = "BEAMISSUE" Then
                                        INTRES = OBJCMN.Execute_Any_String("UPDATE BEAMISSUETOWEAVER_DESC SET BEAMISSUE_OUTCUT = BEAMISSUE_OUTCUT - " & Val(INTRES.Rows(0).Item("OLDPCS")) & "  WHERE BEAMISSUE_NO = " & Val(INTRES.Rows(0).Item("OLDFROMNO")) & " and BEAMISSUE_GRIDSRNO = " & Val(INTRES.Rows(0).Item("OLDFROMSRNO")) & " AND BEAMISSUE_YEARID = " & YearId, "", "")
                                    ElseIf INTRES.Rows(0).Item("OLDTYPE") = "OPENING" Then
                                        INTRES = OBJCMN.Execute_Any_String("UPDATE STOCKMASTER_BEAMWEAVER SET SMBEAMWEAVER_OUTCUT=SMBEAMWEAVER_OUTCUT - " & Val(INTRES.Rows(0).Item("OLDPCS")) & " WHERE SMBEAMWEAVER_NO = " & Val(INTRES.Rows(0).Item("OLDFROMNO")) & " AND SMBEAMWEAVER_YEARID = " & YearId, "", "")
                                    End If


                                    'UPDATE THE RECORDS IN DESC TABLE FIRST
                                    INTRES = OBJCMN.Execute_Any_String("UPDATE GREYRECEIVEDWEAVER_DESC SET GRECDWEAVER_BEAMID = (SELECT BEAM_ID FROM BEAMMASTER WHERE BEAM_NAME = '" & GRIDGREY.Rows(TEMPGREYROWNO).Cells(GBEAMNAME.Index).Value & "' AND BEAM_YEARID = " & YearId & "), GRECDWEAVER_BEAMNO = '" & GRIDGREY.Rows(TEMPGREYROWNO).Cells(GBEAMNO.Index).Value & "', GRECDWEAVER_BALANCE = " & Val(BALANCECUT) & ", GRECDWEAVER_FROMNO = " & Val(GRIDGREY.Rows(TEMPGREYROWNO).Cells(GFROMNO.Index).Value) & ", GRECDWEAVER_FROMSRNO = " & Val(GRIDGREY.Rows(TEMPGREYROWNO).Cells(GFROMSRNO.Index).Value) & ", GRECDWEAVER_TYPE = '" & GRIDGREY.Rows(TEMPGREYROWNO).Cells(GTYPE.Index).Value & "' WHERE GRECDWEAVER_NO = " & Val(GREYROW("GREYRECDNO")) & " AND GRECDWEAVER_LOOMNO = '" & TEMPLOOMNO & "' AND GRECDWEAVER_YEARID = " & YearId, "", "")


                                    'UPDATE THE RECORDS IN BEAMUPLOAD
                                    INTRES = OBJCMN.Execute_Any_String("UPDATE BEAMUPLOAD SET BEAMUPLOAD_BEAMNO = '" & GRIDGREY.Rows(TEMPGREYROWNO).Cells(GBEAMNO.Index).Value & "', BEAMUPLOAD_FROMNO = " & Val(GRIDGREY.Rows(TEMPGREYROWNO).Cells(GFROMNO.Index).Value) & ", BEAMUPLOAD_FROMSRNO = " & Val(GRIDGREY.Rows(TEMPGREYROWNO).Cells(GFROMSRNO.Index).Value) & ", BEAMUPLOAD_TYPE = '" & GRIDGREY.Rows(TEMPGREYROWNO).Cells(GTYPE.Index).Value & "' WHERE BEAMUPLOAD_GREYRECNO = " & Val(GREYROW("GREYRECDNO")) & " AND BEAMUPLOAD_LOOMNO = '" & TEMPLOOMNO & "' AND BEAMUPLOAD_YEARID = " & YearId, "", "")


                                    'UPDATE BALANCECUT VARIABLE
                                    'FOR THIS WE NEED TO GET THE CUTS IN GREYRECDNO AND SUBTRACT FROM BALANCECUT
                                    INTRES = OBJCMN.search("GRECDWEAVER_PCS AS PCS", "", " GREYRECEIVEDWEAVER_DESC ", " AND GRECDWEAVER_NO = " & Val(GREYROW("GREYRECDNO")) & " AND GRECDWEAVER_LOOMNO = '" & TEMPLOOMNO & "' AND GRECDWEAVER_YEARID = " & YearId)
                                    BALANCECUT -= Val(INTRES.Rows(0).Item("PCS"))

                                    'ALTER UPDATING ALL GREY NOS WE NEED TO UPDATE OUTCUT IN BEAMISSUE TO WEAVER OR OPENING TABLE
                                    If GRIDGREY.Rows(TEMPGREYROWNO).Cells(GTYPE.Index).Value = "BEAMISSUE" Then
                                        INTRES = OBJCMN.Execute_Any_String("UPDATE BEAMISSUETOWEAVER_DESC SET BEAMISSUE_OUTCUT = BEAMISSUE_OUTCUT + " & Val(INTRES.Rows(0).Item("PCS")) & " WHERE BEAMISSUE_NO = " & Val(GRIDGREY.Rows(TEMPGREYROWNO).Cells(GFROMNO.Index).Value) & " and BEAMISSUE_GRIDSRNO = " & Val(GRIDGREY.Rows(TEMPGREYROWNO).Cells(GFROMSRNO.Index).Value) & " AND BEAMISSUE_YEARID = " & YearId, "", "")
                                    ElseIf GRIDGREY.Rows(TEMPGREYROWNO).Cells(GTYPE.Index).Value = "OPENING" Then
                                        INTRES = OBJCMN.Execute_Any_String("UPDATE STOCKMASTER_BEAMWEAVER SET SMBEAMWEAVER_OUTCUT=SMBEAMWEAVER_OUTCUT + " & Val(INTRES.Rows(0).Item("PCS")) & " WHERE SMBEAMWEAVER_NO = " & Val(GRIDGREY.Rows(TEMPGREYROWNO).Cells(GFROMNO.Index).Value) & " AND SMBEAMWEAVER_YEARID = " & YearId, "", "")
                                    End If
                                Next



                            End If
                        Next
                    End If
                End If

            End If

            If gridupload.RowCount > 0 Then SAVEUPLOAD()
            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTGREYRECDDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJGREYREC As New ClsGreyRecdFromWeaverDesign
            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPGREYRECDNO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJGREYREC.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJGREYREC.SAVEUPLOAD()
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

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True


        If DTGREYRECDDATE.Text = "__/__/____" Then
            EP.SetError(DTGREYRECDDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTGREYRECDDATE.Text) Then
                EP.SetError(DTGREYRECDDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If


        If ClientName = "JASHOK" And TXTCHALLANNO.Text.Trim.Length = 0 Then
            EP.SetError(TXTCHALLANNO, "Please Fill Challan No")
            bln = False
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Fill Jobber Name")
            bln = False
        End If

        If cmbtrans.Text.Trim.Length = 0 Then
            EP.SetError(cmbtrans, "Please Fill Transport Name")
            bln = False
        End If

        If CMBGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBGODOWN, " Please Fill Godown ")
            bln = False
        End If

        If GRIDGREY.RowCount = 0 Then
            EP.SetError(CMBNAME, "Enter Proper Details")
            bln = False
        Else
            For Each ROW As DataGridViewRow In GRIDGREY.Rows
                If Val(ROW.Cells(GCUT.Index).Value) > 0 And ROW.Cells(GQUALITY.Index).Value.ToString = "" Then
                    EP.SetError(CMBNAME, "Grey Quality cannot be Blank")
                    bln = False
                End If

                If ClientName <> "SHREEJI" Then
                    If Val(ROW.Cells(GCUT.Index).Value) > 0 And ROW.Cells(GSHADE.Index).Value.ToString = "" Then
                        EP.SetError(CMBNAME, "Shade cannot be Blank")
                        bln = False
                    End If

                    If Val(ROW.Cells(GCUT.Index).Value) > 0 And Val(ROW.Cells(GMTRS.Index).Value) <= 0 Then
                        EP.SetError(CMBNAME, "Mtrs cannot be 0")
                        bln = False
                    End If
                End If

            Next
        End If

        If Val(TXTTOTAL.Text.Trim) = 0 Then
            EP.SetError(CMBNAME, "Enter Taka Details")
            bln = False
        End If

        'DONE TEMPORARILY
        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Modify, Entry Locked")
            bln = False
        End If

        Return bln
    End Function

    Private Sub DTGREYRECDDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTGREYRECDDATE.GotFocus
        DTGREYRECDDATE.SelectAll()
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
            GRIDGREY.RowCount = 0
LINE1:
            TEMPGREYRECDNO = Val(TXTGREYRECDNO.Text) - 1
Line2:
            If TEMPGREYRECDNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" GRECDWEAVER_NO ", "", "  GREYRECEIVEDWEAVERDESIGN", " AND GRECDWEAVER_NO = '" & TEMPGREYRECDNO & "' AND GREYRECEIVEDWEAVERDESIGN.GRECDWEAVER_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    GreyRecdWeaverWeftChange_Load(sender, e)
                Else
                    TEMPGREYRECDNO = Val(TEMPGREYRECDNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDGREY.RowCount = 0 And TEMPGREYRECDNO > 1 Then
                TXTGREYRECDNO.Text = TEMPGREYRECDNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDGREY.RowCount = 0
LINE1:
            TEMPGREYRECDNO = Val(TXTGREYRECDNO.Text) + 1
            GETMAX_BEAMRECD_NO()
            Dim MAXNO As Integer = TXTGREYRECDNO.Text.Trim
            CLEAR()
            If Val(TXTGREYRECDNO.Text) - 1 >= TEMPGREYRECDNO Then
                EDIT = True
                GreyRecdWeaverWeftChange_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDGREY.RowCount = 0 And TEMPGREYRECDNO < MAXNO Then
                TXTGREYRECDNO.Text = TEMPGREYRECDNO
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
                GRIDGREY.RowCount = 0
                TEMPGREYRECDNO = Val(tstxtbillno.Text)
                If TEMPGREYRECDNO > 0 Then
                    EDIT = True
                    GreyRecdWeaverWeftChange_Load(sender, e)
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

                If MsgBox("Delete Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim alParaval As New ArrayList
                alParaval.Add(TXTGREYRECDNO.Text.Trim)
                alParaval.Add(YearId)

                Dim OBJDEL As New ClsGreyRecdFromWeaverDesign
                OBJDEL.alParaval = alParaval
                IntResult = OBJDEL.Delete()
                MsgBox("Entry Deleted")
                CLEAR()
                EDIT = False
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub CALC(ByVal QUALITYNAME As String)
        Try
            If QUALITYNAME <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(GREY_QUALITYWT, 0) AS QUALITYWT, ISNULL(GREY_MTRS, 0) AS MTRS", "", "GREYQUALITYMASTER", "AND GREYQUALITYMASTER.GREY_NAME = '" & QUALITYNAME & "' AND GREY_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    'dont get mtrs auto
                    'GRIDGREY.CurrentRow.Cells(GMTRS.Index).Value = Val(GRIDGREY.CurrentRow.Cells(GCUT.Index).EditedFormattedValue) * Val(DT.Rows(0).Item("MTRS"))
                    GRIDGREY.CurrentRow.Cells(GQUALITYWT.Index).Value = Format(Val(DT.Rows(0).Item("QUALITYWT")), "0.000")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGREYQUALITY.Enter
        Try
            CMBGREYQUALITY.Items.Clear()
            If CMBGREYQUALITY.Text.Trim = "" And GRIDGREY.CurrentRow.Cells(GBEAMNAME.Index).Value.ToString <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("GREYQUALITYMASTER.GREY_NAME AS GREYNAME", "", " GREYQUALITYMASTER INNER JOIN BEAMMASTER ON GREYQUALITYMASTER.GREY_BEAMID = BEAMMASTER.BEAM_ID ", " AND BEAM_NAME ='" & GRIDGREY.CurrentRow.Cells(GBEAMNAME.Index).Value.ToString & "' AND GREY_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    CMBGREYQUALITY.Items.Clear()
                    CMBGREYQUALITY.Text = ""
                    For Each DTROW As DataRow In DT.Rows
                        CMBGREYQUALITY.Items.Add(DTROW("GREYNAME"))
                    Next
                    CMBGREYQUALITY.Text = GRIDGREY.CurrentRow.Cells(GQUALITY.Index).Value
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGREYQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGREY As New SelectGreyQuality
                OBJGREY.ShowDialog()
                If OBJGREY.TEMPNAME <> "" Then CMBGREYQUALITY.Text = OBJGREY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGREYQUALITY.Validated
        On Error Resume Next
        CALC(CMBGREYQUALITY.Text.Trim)
        GRIDGREY.CurrentRow.Cells(GQUALITY.Index).Value = CMBGREYQUALITY.Text.Trim
        GRIDGREY.CurrentRow.Cells(GSHADE.Index).Selected = True
        GRIDGREY.Focus()
        CMBGREYQUALITY.Visible = False
    End Sub

    Private Sub CMBGREYQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGREYQUALITY.Validating
        Try
            If CMBGREYQUALITY.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("GREYQUALITYMASTER.GREY_NAME AS GREYNAME, GREY_MTRS AS MTRS, GREY_QUALITYWT AS QUALITYWT", "", " GREYQUALITYMASTER ", " AND GREY_NAME ='" & CMBGREYQUALITY.Text.Trim & "' AND GREY_YEARID = " & YearId)
                If DT.Rows.Count = 0 Then
                    If MsgBox("Quality Not Present, Add New?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        e.Cancel = True
                    Else
                        Dim OBJGREY As New GreyQualityMaster
                        OBJGREY.MdiParent = MDIMain
                        OBJGREY.TEMPQUALITYNAME = CMBGREYQUALITY.Text.Trim
                        OBJGREY.Show()
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJGREY As New GreyRecdFromWeaverDetails
            OBJGREY.MdiParent = MDIMain
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
        Try
            If CMBNAME.Text.Trim <> "" And EDIT = False Then
                'FILL GRID WITH BLANK DATA (FOR FIRST TIME WHEN NO BEAMS IS UPLOADED ON THE LOOMS)
                'If ClientName <> "SHREEJI" Then
                '    FILLGRIDLOOM()
                '    FILLPREVIOUSBEAM()
                '    FILLDATABEAM()
                '    FILLUPLOADLOOM()
                '    FILLUPLOADBEAM()
                '    FILLUNLOADLOOM()
                'End If
                CMBNAME.Enabled = False
                TOTAL()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRIDLOOM()
        Try
            GRIDGREY.RowCount = 0
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable = OBJCMN.search("  LOOMMASTER_DESC.LOOM_NO AS LOOMNO ", "", "  LOOMMASTER INNER JOIN LOOMMASTER_DESC ON LOOMMASTER.LOOM_ID = LOOMMASTER_DESC.LOOM_ID INNER JOIN LEDGERS ON LOOMMASTER.LOOM_WEAVERID = LEDGERS.Acc_id", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND LOOM_YEARID = " & YearId & " ORDER BY CAST(LOOM_NO AS INT)")
            If dt.Rows.Count > 0 Then
                For Each DTROW As DataRow In dt.Rows
                    GRIDGREY.Rows.Add(0, Val(DTROW("LOOMNO")), "", "", 0.0, 0.0, "", "", 0, 0.0, 0.0, 0.0, 0, 0, "", 0)
                Next
                getsrno(GRIDGREY)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLDATABEAM()
        Try
            If UPLOADDT.Rows.Count > 0 Then
                For Each ROW As DataGridViewRow In GRIDGREY.Rows
                    Dim DTROW() As DataRow = UPLOADDT.Select("LOOMNO = '" & Val(ROW.Cells(GLOOMNO.Index).Value) & "'")
                    If DTROW.Count > 0 Then
                        ROW.Cells(GBEAMNAME.Index).Value = DTROW(0).Item("BEAMNAME")
                        ROW.Cells(GBEAMNO.Index).Value = DTROW(0).Item("BEAMNO")

                        If EDIT = False Then ROW.Cells(GBALANCE.Index).Value = DTROW(0).Item("BALANCECUT")
                        'IF WE COMMENT THE ABOVE CODE AND OPEN THE CODE GIVEN BELOW THEN THIS WILL GIVEC US THE CURRENT CUT BALANCE,
                        'WHICH IS PRACTICALLY WRONG, COZ WE NEED TO KNOW WHAT WAS THE ACTUAL BALANCE ON THAT DAY
                        'BUT SUPPOSE WE CHANGE THE CUTS THEN THE BALANCE CUT IN NEXT ENTRY SHOULD CHANGE AUTO
                        'SUPPOSE WE HAVE ENYETED DATA IN ON 10TH BALANCE WAS 10CUTS WE RECD 1 CUT, SO ON 11TH BALANCE WAS 9CUTS WE RECD 1 CUT, SO ON 12TH BALANCE WAS 8CUTS 
                        'NOW IF I GO ON 10TH AND CHANGE THE RECD CUTS FROM 1 TO 3 THEN ON 11TH THE BALANCE CUT WHICH WAS 9 SHOULD AUTO CHANGE TO 7CUTS AND ON 12TH IT SHOULD BE 6CUTS
                        'TO ACHIVE THIS WE NEED TO LOOP ON SAVE SP, WITH RESPECT TO BEAM 

                        'ROW.Cells(GBALANCE.Index).Value = DTROW(0).Item("BALANCECUT")

                        ROW.Cells(GFROMNO.Index).Value = DTROW(0).Item("FROMNO")
                        ROW.Cells(GFROMSRNO.Index).Value = DTROW(0).Item("FROMSRNO")
                        ROW.Cells(GTYPE.Index).Value = DTROW(0).Item("TYPE")
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLUPLOADLOOM()
        Try
            'FILL ALL LOOM NOS IN UPLOAD LOOM NO CMB
            'EXCEPT THOSE LOOMNO WHICH ARE NOT EMPTY
            CMBUPLOADLOOMNO.Items.Clear()
            CMBUPLOADLOOMNO.Text = ""
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable = OBJCMN.search("  LOOMMASTER_DESC.LOOM_NO AS LOOMNO ", "", "  LOOMMASTER INNER JOIN LOOMMASTER_DESC ON LOOMMASTER.LOOM_ID = LOOMMASTER_DESC.LOOM_ID INNER JOIN LEDGERS ON LOOMMASTER.LOOM_WEAVERID = LEDGERS.Acc_id", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND LOOM_YEARID = " & YearId & " ORDER BY CAST(LOOM_NO AS INT)")
            If dt.Rows.Count > 0 Then
                For Each DTROW As DataRow In dt.Rows
                    Dim ROW() As DataRow = UPLOADDT.Select("LOOMNO = '" & DTROW("LOOMNO") & "'")
                    If ROW.Count = 0 Then
                        CMBUPLOADLOOMNO.Items.Add(DTROW("LOOMNO"))
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLUPLOADBEAM()
        Try
            'GET THOSE BEAMS WHICH ARE ISSUES TO SELECTED WEAVER
            'ALSO FETCH DATA FROM OPENINGSTOCKBEAM WITH WEAVER WHERE LOOM NO IS NOT PRESENT AND DONE = 0
            'AND WHICH ARE YET TO BE UPLOADED ON LOOM, IF BEAM IS UPLOADED THEN DO NOT SHOW THEM HERE
            CMBBEAMNO.Items.Clear()
            CMBBEAMNO.Text = ""
            TXTUPLOADBEAMNAME.Clear()
            TXTBALANCECUT.Clear()

            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable = OBJCMN.search("T.BEAMNO", "", "  (SELECT BEAMISSUE_BEAMNO AS BEAMNO FROM LEDGERS INNER JOIN BEAMISSUETOWEAVER ON LEDGERS.Acc_id = BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID WHERE LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND  BEAMISSUE_DONE = 'FALSE' AND (BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT - BEAMISSUETOWEAVER_DESC.BEAMISSUE_OUTCUT) > 0 AND  BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID = " & YearId & " UNION ALL SELECT SMBEAMWEAVER_BEAMNO AS BEAMNO FROM  LEDGERS INNER JOIN STOCKMASTER_BEAMWEAVER  ON LEDGERS.Acc_id = SMBEAMWEAVER_WEAVERID WHERE LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND  SMBEAMWEAVER_DONE = 'FALSE' AND (SMBEAMWEAVER_CUT - SMBEAMWEAVER_OUTCUT) > 0 AND  SMBEAMWEAVER_YEARID = " & YearId & " ) AS T", "")
            If dt.Rows.Count > 0 Then
                For Each DTROW As DataRow In dt.Rows
                    Dim ROW() As DataRow = UPLOADDT.Select("BEAMNO = '" & DTROW("BEAMNO") & "'")
                    If ROW.Count = 0 Then
                        CMBBEAMNO.Items.Add(DTROW("BEAMNO"))
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLUNLOADLOOM()
        Try
            CMBUNLOADLOOMNO.Items.Clear()
            CMBUNLOADLOOMNO.Text = ""
            TXTUNLOADBEAMNAME.Clear()
            For Each DTROW As DataRow In UPLOADDT.Rows
                For I As Integer = 1 To CMBUNLOADLOOMNO.Items.Count
                    If CMBUNLOADLOOMNO.Items(I - 1) = Val(DTROW("LOOMNO")) Then GoTo LINE1
                Next
                CMBUNLOADLOOMNO.Items.Add(DTROW("LOOMNO"))
LINE1:
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTGREYRECDDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTGREYRECDDATE.Validating
        Try
            If DTGREYRECDDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTGREYRECDDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGREY_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDGREY.CellEnter
        Try
            If e.ColumnIndex = GQUALITY.Index And GRIDGREY.CurrentRow.Cells(GBEAMNAME.Index).Value.ToString <> "" Then
                CMBGREYQUALITY.Top = 56 + ((e.RowIndex - GRIDGREY.FirstDisplayedScrollingRowIndex) * 20)
                CMBGREYQUALITY.Visible = True

                CMBGREYQUALITY.Text = ""
                CMBGREYQUALITY.BringToFront()
                CMBGREYQUALITY.Focus()
            End If

            If e.ColumnIndex = GSHADE.Index And GRIDGREY.CurrentRow.Cells(GBEAMNAME.Index).Value.ToString <> "" Then
                CMBSHADE.Top = 56 + ((e.RowIndex - GRIDGREY.FirstDisplayedScrollingRowIndex) * 20)
                CMBSHADE.Visible = True

                CMBSHADE.Text = ""
                CMBSHADE.BringToFront()
                CMBSHADE.Focus()
            End If

            If e.ColumnIndex = GPIECETYPE.Index And GRIDGREY.CurrentRow.Cells(GBEAMNAME.Index).Value.ToString <> "" Then
                CMBPIECETYPE.Top = 56 + ((e.RowIndex - GRIDGREY.FirstDisplayedScrollingRowIndex) * 20)
                CMBPIECETYPE.Visible = True

                CMBPIECETYPE.Text = ""
                CMBPIECETYPE.BringToFront()
                CMBPIECETYPE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyRecdFromWeaver_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ALLOWMFG = False Then Exit Sub
            If UPLOADDT.Columns.Count = 0 Then
                UPLOADDT.Columns.Add("WEAVERNAME")
                UPLOADDT.Columns.Add("LOOMNO")
                UPLOADDT.Columns.Add("BEAMNAME")
                UPLOADDT.Columns.Add("BEAMNO")
                UPLOADDT.Columns.Add("TOTALCUT")
                UPLOADDT.Columns.Add("BALANCECUT")
                UPLOADDT.Columns.Add("FROMNO")
                UPLOADDT.Columns.Add("FROMSRNO")
                UPLOADDT.Columns.Add("TYPE")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDUPLOADBEAM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDUPLOADBEAM.Click
        Try
            If CMBUPLOADLOOMNO.Text.Trim <> "" And CMBNAME.Text.Trim <> "" And CMBBEAMNO.Text.Trim <> "" Then
                If MsgBox("Wish to Upload Beam on Loom No " & CMBUPLOADLOOMNO.Text.Trim, MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                UPLOADDT.Rows.Add(CMBNAME.Text.Trim, Val(CMBUPLOADLOOMNO.Text), TXTUPLOADBEAMNAME.Text.Trim, CMBBEAMNO.Text.Trim, Val(TXTTOTALCUT.Text.Trim), Val(TXTBALANCECUT.Text.Trim), Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim), TXTTYPE.Text.Trim)
                FILLDATABEAM()
                'WE ARE WRITING THIS CODE HERE COZ IF WE WANT TO FORCEFULLY UPLOAD A NEW BEAM ON EDIT MODE THEN
                'BALANCE CUT SHOULD BE SHOWN HERE IN THE GRID
                'IF WE WRITE THIS CODE IN FILLDATABEAM FUNCTION THEN IT WONT BE CORRECT
                For Each ROW As DataGridViewRow In GRIDGREY.Rows
                    If Val(ROW.Cells(GLOOMNO.Index).Value) = Val(CMBUPLOADLOOMNO.Text.Trim) Then
                        ROW.Cells(GBALANCE.Index).Value = Val(TXTBALANCECUT.Text.Trim)
                        Exit For
                    End If
                Next

                'IF WE ARE UPLOADING BEAM ON EDIT MODE THEN SAVE THESE LOOM NOS IN AN ARRAY
                'COZ ON SAVE WE NEED TO UPDATE ALL THE FURTHER ENTRYIES WITH AUTO
                If EDIT = True Then LOOMNOUPDATED.Add(Val(CMBUPLOADLOOMNO.Text.Trim))

                FILLUPLOADLOOM()
                FILLUPLOADBEAM()
                FILLUNLOADLOOM()
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNO_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBEAMNO.Validated
        Try
            If CMBBEAMNO.Text.Trim <> "" And CMBNAME.Text.Trim <> "" And CMBUPLOADLOOMNO.Text.Trim <> "" Then
                If CMBUPLOADLOOMNO.Text.Trim <> "" And CMBBEAMNO.Text.Trim <> "" Then
                    'GET BEAMNAME FROM BEAMNO
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" T.BEAMNAME, T.TOTALCUT, T.BALANCECUT, T.FROMNO, T.FROMSRNO,T.TYPE ", "", " (SELECT ISNULL(BEAMMASTER.BEAM_NAME,'') AS BEAMNAME, BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT as TOTALCUT, (BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT - BEAMISSUETOWEAVER_DESC.BEAMISSUE_OUTCUT) AS BALANCECUT, BEAMISSUETOWEAVER.BEAMISSUE_NO AS FROMNO, BEAMISSUETOWEAVER_DESC.BEAMISSUE_GRIDSRNO AS FROMSRNO, 'BEAMISSUE' AS TYPE FROM  BEAMISSUETOWEAVER INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID INNER JOIN LEDGERS ON BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID = LEDGERS.Acc_id INNER JOIN BEAMMASTER ON BEAMISSUETOWEAVER_DESC.BEAMISSUE_BEAMID = BEAMMASTER.BEAM_ID WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND BEAMISSUE_BEAMNO = '" & CMBBEAMNO.Text.Trim & "' AND BEAMISSUE_DONE = 0 AND (BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT - BEAMISSUETOWEAVER_DESC.BEAMISSUE_OUTCUT) > 0 AND BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID = " & YearId & " UNION ALL SELECT ISNULL(BEAMMASTER.BEAM_NAME,'') AS BEAMNAME, SMBEAMWEAVER_CUT as TOTALCUT, (SMBEAMWEAVER_CUT - SMBEAMWEAVER_OUTCUT) AS BALANCECUT, SMBEAMWEAVER_NO AS FROMNO, SMBEAMWEAVER_NO AS FROMSRNO, 'OPENING' AS TYPE  FROM  STOCKMASTER_BEAMWEAVER  INNER JOIN LEDGERS ON SMBEAMWEAVER_WEAVERID= LEDGERS.Acc_id INNER JOIN BEAMMASTER ON SMBEAMWEAVER_BEAMID = BEAMMASTER.BEAM_ID WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND SMBEAMWEAVER_BEAMNO = '" & CMBBEAMNO.Text.Trim & "' AND SMBEAMWEAVER_DONE = 0 AND (SMBEAMWEAVER_CUT - SMBEAMWEAVER_OUTCUT) > 0 AND SMBEAMWEAVER_YEARID = " & YearId & ") AS T", "")
                    If DT.Rows.Count > 0 Then
                        TXTUPLOADBEAMNAME.Text = DT.Rows(0).Item("BEAMNAME")
                        TXTTOTALCUT.Text = Val(DT.Rows(0).Item("TOTALCUT"))
                        TXTBALANCECUT.Text = Val(DT.Rows(0).Item("BALANCECUT"))
                        TXTFROMNO.Text = Val(DT.Rows(0).Item("FROMNO"))
                        TXTFROMSRNO.Text = Val(DT.Rows(0).Item("FROMSRNO"))
                        TXTTYPE.Text = DT.Rows(0).Item("TYPE")
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGREY_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDGREY.CellValidating
        Try
            Dim colNum As Integer = GRIDGREY.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GCUT.Index, GMTRS.Index, GWT.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDGREY.CurrentCell.Value = Nothing Then GRIDGREY.CurrentCell.Value = "0.00"
                        GRIDGREY.CurrentCell.Value = Convert.ToDecimal(GRIDGREY.Item(colNum, e.RowIndex).Value)
                        '' everything is good
                        If Val(GRIDGREY.CurrentCell.EditedFormattedValue) > 0 Then
                            If GRIDGREY.CurrentRow.Cells(GBEAMNO.Index).Value.ToString <> "" Then
                                CALC(GRIDGREY.CurrentRow.Cells(GQUALITY.Index).Value.ToString)
                                TOTAL()
                                If colNum = GCUT.Index And GRIDGREY.CurrentRow.Cells(GQUALITY.Index).Value.ToString = "" And e.RowIndex > 0 Then GRIDGREY.CurrentRow.Cells(GQUALITY.Index).Value = GRIDGREY.Rows(e.RowIndex - 1).Cells(GQUALITY.Index).Value
                            End If
                        End If
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If

                Case GLOOMNO.Index
                    Dim dDebit As Integer
                    Dim bValid As Boolean = Integer.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        'FETCH BEAMDETAILS WITH RESPECT TO LOOM NO | BEAMNO | WEAVERNAME
                        If Val(GRIDGREY.CurrentCell.Value) = 0 And Val(GRIDGREY.CurrentCell.EditedFormattedValue) > 0 Then
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.search("*", "", " BEAMSTOCK_WEAVER ", " AND WEAVERNAME = '" & CMBNAME.Text.Trim & "' AND LOOMNO = " & Val(GRIDGREY.CurrentCell.EditedFormattedValue) & " AND YearId = " & YearId)
                            If DT.Rows.Count > 0 Then
                                GRIDGREY.CurrentRow.Cells(GBEAMNAME.Index).Value = DT.Rows(0).Item("BEAMNAME")
                                GRIDGREY.CurrentRow.Cells(GBEAMNO.Index).Value = DT.Rows(0).Item("BEAMNO")
                                GRIDGREY.CurrentRow.Cells(GBALANCE.Index).Value = Val(DT.Rows(0).Item("CUT"))
                                GRIDGREY.CurrentRow.Cells(GFROMNO.Index).Value = Val(DT.Rows(0).Item("NO"))
                                GRIDGREY.CurrentRow.Cells(GFROMSRNO.Index).Value = Val(DT.Rows(0).Item("SRNO"))
                                GRIDGREY.CurrentRow.Cells(GTYPE.Index).Value = DT.Rows(0).Item("TYPE")
                            End If
                        ElseIf Val(GRIDGREY.CurrentCell.EditedFormattedValue) = 0 Then
                            GRIDGREY.CurrentRow.Cells(GBEAMNAME.Index).Value = ""
                            GRIDGREY.CurrentRow.Cells(GBEAMNO.Index).Value = ""
                            GRIDGREY.CurrentRow.Cells(GBALANCE.Index).Value = 0
                            GRIDGREY.CurrentRow.Cells(GFROMNO.Index).Value = 0
                            GRIDGREY.CurrentRow.Cells(GFROMSRNO.Index).Value = 0
                            GRIDGREY.CurrentRow.Cells(GTYPE.Index).Value = ""
                        End If
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

    Private Sub CMBUNLOADLOOMNO_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBUNLOADLOOMNO.Validated
        Try
            If CMBUNLOADLOOMNO.Text.Trim <> "" Then
                'GET BEAM DETAILS
                Dim ROW() As DataRow = UPLOADDT.Select("LOOMNO = '" & CMBUNLOADLOOMNO.Text.Trim & "'")
                            If ROW.Count = 0 Then
                    MsgBox("Invalid Loom No", MsgBoxStyle.Critical)
                    CMBUNLOADLOOMNO.Text = ""
                    TXTUNLOADBEAMNAME.Clear()
                    TXTUNLOADBEAMNO.Clear()
                    Exit Sub
                End If
                TXTUNLOADBEAMNO.Text = ROW(0).Item("BEAMNO")
                TXTUNLOADBEAMNAME.Text = ROW(0).Item("BEAMNAME")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDUNLOAD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDUNLOAD.Click
        Try
            If CMBUNLOADLOOMNO.Text.Trim <> "" And TXTUNLOADBEAMNO.Text.Trim <> "" And TXTUNLOADBEAMNAME.Text.Trim <> "" Then
                If MsgBox("Wish to Unload Beam from Loom No " & CMBUNLOADLOOMNO.Text.Trim, MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim DTROW As DataRow() = UPLOADDT.Select("LOOMNO = '" & CMBUNLOADLOOMNO.Text.Trim & "'")
                For I As Integer = 0 To DTROW.Count - 1
                    DTROW(I).Delete()
                Next
                'WE CANT USE FILLGRID....DUE TO THIS DATA GETS BLANK
                'SUPPOSE WE HAVE ENTERED SOME DATA, THEN ITGETS BLANK
                'SO IN THIS CASE WE HAVE LOOP ON GRID AND THEN WE HAVE TO REMOVE THAT BEAM
                'FILLGRID()

                For Each ROW As DataGridViewRow In GRIDGREY.Rows
                    If Val(ROW.Cells(GLOOMNO.Index).Value) = Val(CMBUNLOADLOOMNO.Text.Trim) Then
                        ROW.Cells(GBEAMNAME.Index).Value = ""
                        ROW.Cells(GBEAMNO.Index).Value = ""
                        ROW.Cells(GBALANCE.Index).Value = 0.0
                        ROW.Cells(GCUT.Index).Value = 0.0
                        ROW.Cells(GQUALITY.Index).Value = ""
                        ROW.Cells(GSHADE.Index).Value = ""
                        ROW.Cells(GTAKANO.Index).Value = 0
                        ROW.Cells(GMTRS.Index).Value = 0.0
                        ROW.Cells(GWT.Index).Value = 0.0
                        ROW.Cells(GQUALITYWT.Index).Value = 0.0
                        ROW.Cells(GFROMNO.Index).Value = 0.0
                        ROW.Cells(GFROMSRNO.Index).Value = 0.0
                        ROW.Cells(GTYPE.Index).Value = ""
                    End If
                Next

                TXTUNLOADBEAMNAME.Clear()
                TXTUNLOADBEAMNO.Clear()
                CMBUNLOADLOOMNO.Text = ""

                FILLDATABEAM()
                FILLUPLOADLOOM()
                FILLUPLOADBEAM()
                FILLUNLOADLOOM()

                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGREY_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles GRIDGREY.Scroll
        Try
            If CMBGREYQUALITY.Visible = True Then
                CMBGREYQUALITY.Top = 56 + ((GRIDGREY.CurrentRow.Index - GRIDGREY.FirstDisplayedScrollingRowIndex) * 20)
            End If

            If CMBSHADE.Visible = True Then
                CMBSHADE.Top = 56 + ((GRIDGREY.CurrentRow.Index - GRIDGREY.FirstDisplayedScrollingRowIndex) * 20)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then namevalidate(cmbtrans, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'", "Sundry Creditors", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Enter(sender As Object, e As EventArgs) Handles CMBSHADE.Enter
        Try
            CMBSHADE.Items.Clear()
            If CMBSHADE.Text.Trim = "" And GRIDGREY.CurrentRow.Cells(GBEAMNAME.Index).Value.ToString <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("COLORMASTER.COLOR_NAME AS SHADE", "", " GREYQUALITYMASTER INNER JOIN BEAMMASTER ON GREYQUALITYMASTER.GREY_BEAMID = BEAMMASTER.BEAM_ID INNER JOIN GREYQUALITYMASTER_WEFTCHANGE ON GREYQUALITYMASTER.GREY_ID = GREYQUALITYMASTER_WEFTCHANGE.GREY_ID INNER JOIN COLORMASTER ON GREYQUALITYMASTER_WEFTCHANGE.GREY_WEFTCHANGEID = COLORMASTER.COLOR_ID", " AND BEAM_NAME ='" & GRIDGREY.CurrentRow.Cells(GBEAMNAME.Index).Value.ToString & "' AND GREYQUALITYMASTER.GREY_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    CMBSHADE.Items.Clear()
                    CMBSHADE.Text = ""
                    For Each DTROW As DataRow In DT.Rows
                        CMBSHADE.Items.Add(DTROW("SHADE"))
                    Next
                    CMBSHADE.Text = GRIDGREY.CurrentRow.Cells(GSHADE.Index).Value
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Validated(sender As Object, e As EventArgs) Handles CMBSHADE.Validated
        On Error Resume Next
        GRIDGREY.CurrentRow.Cells(GSHADE.Index).Value = CMBSHADE.Text.Trim
        GRIDGREY.CurrentRow.Cells(GWT.Index).Selected = True
        GRIDGREY.Focus()
        CMBSHADE.Visible = False
        TOTAL()
    End Sub

    Private Sub CMBSHADE_Validating(sender As Object, e As CancelEventArgs) Handles CMBSHADE.Validating
        Try
            If CMBSHADE.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("COLORMASTER.COLOR_NAME AS SHADE", "", " GREYQUALITYMASTER INNER JOIN BEAMMASTER ON GREYQUALITYMASTER.GREY_BEAMID = BEAMMASTER.BEAM_ID INNER JOIN GREYQUALITYMASTER_WEFTCHANGE ON GREYQUALITYMASTER.GREY_ID = GREYQUALITYMASTER_WEFTCHANGE.GREY_ID INNER JOIN COLORMASTER ON GREYQUALITYMASTER_WEFTCHANGE.GREY_WEFTCHANGEID = COLORMASTER.COLOR_ID", " AND BEAM_NAME ='" & GRIDGREY.CurrentRow.Cells(GBEAMNAME.Index).Value.ToString & "' AND COLOR_NAME = '" & CMBSHADE.Text.Trim & "' AND GREYQUALITYMASTER.GREY_YEARID = " & YearId)
                If DT.Rows.Count = 0 Then
                    If MsgBox("Shade Not Present, Add New?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDGREYQUALITY_Enter(sender As Object, e As EventArgs) Handles CMBGRIDGREYQUALITY.Enter
        Try
            CMBGRIDGREYQUALITY.Items.Clear()
            If CMBGRIDGREYQUALITY.Text.Trim = "" Then
                Dim WHERECLAUSE As String = ""
                If TXTBEAMNAME.Text.Trim <> "" Then WHERECLAUSE = " AND BEAMMASTER.BEAM_NAME ='" & TXTBEAMNAME.Text.Trim & "' "
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("DISTINCT GREYQUALITYMASTER.GREY_NAME AS GREYNAME", "", " GREYQUALITYMASTER INNER JOIN GREYQUALITYMASTER_BEAMDETAILS ON GREYQUALITYMASTER.GREY_ID = GREYQUALITYMASTER_BEAMDETAILS.GREY_ID INNER JOIN BEAMMASTER ON GREYQUALITYMASTER_BEAMDETAILS.GREY_BEAMID = BEAMMASTER.BEAM_ID ", WHERECLAUSE & " AND GREYQUALITYMASTER.GREY_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    CMBGRIDGREYQUALITY.Items.Clear()
                    CMBGRIDGREYQUALITY.Text = ""
                    For Each DTROW As DataRow In DT.Rows
                        CMBGRIDGREYQUALITY.Items.Add(DTROW("GREYNAME"))
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDGREYQUALITY_Validating(sender As Object, e As CancelEventArgs) Handles CMBGRIDGREYQUALITY.Validating
        Try
            If CMBGREYQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBGRIDGREYQUALITY, e, Me, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDSHADE_Enter(sender As Object, e As EventArgs) Handles CMBGRIDSHADE.Enter
        Try
            CMBGRIDSHADE.Items.Clear()
            If CMBGRIDSHADE.Text.Trim = "" And CMBGRIDGREYQUALITY.Text.Trim <> "" Then
                Dim WHERECLAUSE As String = ""
                If TXTBEAMNAME.Text.Trim <> "" Then WHERECLAUSE = " AND BEAMMASTER.BEAM_NAME ='" & TXTBEAMNAME.Text.Trim & "' "
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("DISTINCT COLORMASTER.COLOR_NAME AS SHADE", "", " GREYQUALITYMASTER INNER JOIN GREYQUALITYMASTER_BEAMDETAILS ON GREYQUALITYMASTER.GREY_ID = GREYQUALITYMASTER_BEAMDETAILS.GREY_ID INNER JOIN BEAMMASTER ON GREYQUALITYMASTER_BEAMDETAILS.GREY_BEAMID = BEAMMASTER.BEAM_ID INNER JOIN GREYQUALITYMASTER_WEFTCHANGE ON GREYQUALITYMASTER.GREY_ID = GREYQUALITYMASTER_WEFTCHANGE.GREY_ID INNER JOIN COLORMASTER ON GREYQUALITYMASTER_WEFTCHANGE.GREY_WEFTCHANGEID = COLORMASTER.COLOR_ID", WHERECLAUSE & " AND GREYQUALITYMASTER.GREY_NAME = '" & CMBGRIDGREYQUALITY.Text.Trim & "' AND GREYQUALITYMASTER.GREY_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    CMBGRIDSHADE.Items.Clear()
                    CMBGRIDSHADE.Text = ""
                    For Each DTROW As DataRow In DT.Rows
                        CMBGRIDSHADE.Items.Add(DTROW("SHADE"))
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNARRATION_Validated(sender As Object, e As EventArgs) Handles TXTNARRATION.Validated
        Try
            If Val(TXTTAKA.Text.Trim) > 0 And TXTTAKANO.Text.Trim <> "" And CMBGRIDGREYQUALITY.Text.Trim <> "" And CMBGRIDPIECETYPE.Text.Trim <> "" Then FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDGREY.Rows.Add(0, TXTLOOMNO.Text.Trim, TXTBEAMNAME.Text.Trim, TXTBEAMNO.Text.Trim, Val(TXTBALANCECUT.Text.Trim), Val(TXTTAKA.Text.Trim), CMBGRIDGREYQUALITY.Text.Trim, CMBGRIDSHADE.Text.Trim, TXTTAKANO.Text.Trim, Val(TXTMTRS.Text.Trim), Val(TXTWT.Text.Trim), CMBGRIDPIECETYPE.Text.Trim, TXTTPNARRATION.Text.Trim, TXTNARRATION.Text.Trim, 0, Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim), TXTTYPE.Text.Trim, 0)
            Else
                GRIDGREY.Item(GLOOMNO.Index, TEMPROW).Value = TXTLOOMNO.Text.Trim
                GRIDGREY.Item(GBEAMNAME.Index, TEMPROW).Value = TXTBEAMNAME.Text.Trim
                GRIDGREY.Item(GBEAMNO.Index, TEMPROW).Value = TXTBEAMNO.Text.Trim
                GRIDGREY.Item(GBALANCE.Index, TEMPROW).Value = Val(TXTBALANCECUT.Text.Trim)
                GRIDGREY.Item(GCUT.Index, TEMPROW).Value = Val(TXTTAKA.Text.Trim)
                GRIDGREY.Item(GQUALITY.Index, TEMPROW).Value = CMBGRIDGREYQUALITY.Text.Trim
                GRIDGREY.Item(GSHADE.Index, TEMPROW).Value = CMBGRIDSHADE.Text.Trim
                GRIDGREY.Item(GMTRS.Index, TEMPROW).Value = Val(TXTMTRS.Text.Trim)
                GRIDGREY.Item(GWT.Index, TEMPROW).Value = Val(TXTWT.Text.Trim)
                GRIDGREY.Item(GPIECETYPE.Index, TEMPROW).Value = CMBGRIDPIECETYPE.Text.Trim
                GRIDGREY.Item(GTPNARRATION.Index, TEMPROW).Value = TXTTPNARRATION.Text.Trim
                GRIDGREY.Item(GNARRATION.Index, TEMPROW).Value = TXTNARRATION.Text.Trim
                GRIDDOUBLECLICK = False
                TEMPROW = 0
            End If

            TXTLOOMNO.Text = ""
            TXTBEAMNAME.Clear()
            TXTBEAMNO.Text = ""
            TXTBALANCECUT.Clear()
            'TXTTAKA.Text = 1
            'CMBGRIDGREYQUALITY.Text = ""
            'CMBGRIDSHADE.Text = ""
            TXTTAKANO.Text = Val(TXTTAKANO.Text.Trim) + 1
            TXTMTRS.Clear()
            TXTWT.Clear()
            'CMBGRIDPIECETYPE.Text = ""
            TXTTPNARRATION.Clear()
            TXTNARRATION.Clear()

            TXTFROMNO.Clear()
            TXTFROMSRNO.Clear()
            TXTTYPE.Clear()

            getsrno(GRIDGREY)
            TXTTAKANO.Focus()
            TXTSRNO.Text = Val(GRIDGREY.RowCount) + 1
            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDSHADE_Validating(sender As Object, e As CancelEventArgs) Handles CMBGRIDSHADE.Validating
        Try
            If CMBGRIDSHADE.Text.Trim <> "" And GRIDGREY.RowCount > 0 Then
                Dim OBJCMN As New ClsCommon
                Dim WHERECLAUSE As String = ""
                If TXTBEAMNAME.Text.Trim <> "" Then WHERECLAUSE = " AND BEAM_NAME ='" & TXTBEAMNAME.Text.Trim & "'"
                Dim DT As DataTable = OBJCMN.search("DISTINCT COLORMASTER.COLOR_NAME AS SHADE", "", " GREYQUALITYMASTER INNER JOIN GREYQUALITYMASTER_BEAMDETAILS ON GREYQUALITYMASTER.GREY_ID = GREYQUALITYMASTER_BEAMDETAILS.GREY_ID INNER JOIN BEAMMASTER ON GREYQUALITYMASTER_BEAMDETAILS.GREY_BEAMID = BEAMMASTER.BEAM_ID INNER JOIN GREYQUALITYMASTER_WEFTCHANGE ON GREYQUALITYMASTER.GREY_ID = GREYQUALITYMASTER_WEFTCHANGE.GREY_ID INNER JOIN COLORMASTER ON GREYQUALITYMASTER_WEFTCHANGE.GREY_WEFTCHANGEID = COLORMASTER.COLOR_ID", WHERECLAUSE & " AND GREYQUALITYMASTER.GREY_NAME = '" & CMBGRIDGREYQUALITY.Text.Trim & "' AND GREYQUALITYMASTER.GREY_YEARID = " & YearId)
                If DT.Rows.Count = 0 Then
                    If MsgBox("Shade Not Present, Add New?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTQUALITYWT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTWT.KeyPress, TXTMTRS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub GRIDGREY_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDGREY.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDGREY.Item(GSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDGREY.Item(GSRNO.Index, e.RowIndex).Value
                TXTLOOMNO.Text = GRIDGREY.Item(GLOOMNO.Index, e.RowIndex).Value
                TXTBEAMNAME.Text = GRIDGREY.Item(GBEAMNAME.Index, e.RowIndex).Value
                TXTBEAMNO.Text = GRIDGREY.Item(GBEAMNO.Index, e.RowIndex).Value
                TXTBALANCECUT.Text = Val(GRIDGREY.Item(GBALANCE.Index, e.RowIndex).Value)
                TXTTAKA.Text = Val(GRIDGREY.Item(GCUT.Index, e.RowIndex).Value)
                CMBGRIDGREYQUALITY.Text = GRIDGREY.Item(GQUALITY.Index, e.RowIndex).Value
                CMBGRIDSHADE.Text = GRIDGREY.Item(GSHADE.Index, e.RowIndex).Value
                TXTTAKANO.Text = Val(GRIDGREY.Item(GTAKANO.Index, e.RowIndex).Value)
                TXTMTRS.Text = Val(GRIDGREY.Item(GMTRS.Index, e.RowIndex).Value)
                TXTWT.Text = Val(GRIDGREY.Item(GQUALITYWT.Index, e.RowIndex).Value)
                CMBGRIDPIECETYPE.Text = GRIDGREY.Item(GPIECETYPE.Index, e.RowIndex).Value
                TXTTPNARRATION.Text = GRIDGREY.Item(GTPNARRATION.Index, e.RowIndex).Value
                TXTNARRATION.Text = GRIDGREY.Item(GNARRATION.Index, e.RowIndex).Value

                TXTFROMNO.Text = Val(GRIDGREY.Item(GFROMNO.Index, e.RowIndex).Value)
                TXTFROMSRNO.Text = Val(GRIDGREY.Item(GFROMSRNO.Index, e.RowIndex).Value)
                TXTTYPE.Text = GRIDGREY.Item(GTYPE.Index, e.RowIndex).Value

                TEMPROW = e.RowIndex
                CMBGRIDGREYQUALITY.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGREY_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDGREY.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDGREY.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                If MsgBox("Wish To Delete Taka?", MsgBoxStyle.YesNo) = vbNo Then Exit Sub

                GRIDGREY.Rows.RemoveAt(GRIDGREY.CurrentRow.Index)
                getsrno(GRIDGREY)
                TXTSRNO.Text = GRIDGREY.RowCount + 1
                TOTAL()
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBPIECETYPE_Enter(sender As Object, e As EventArgs) Handles CMBPIECETYPE.Enter
        Try
            CMBPIECETYPE.Items.Clear()
            If CMBPIECETYPE.Text.Trim = "" And GRIDGREY.CurrentRow.Cells(GBEAMNAME.Index).Value.ToString <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("PIECETYPEMASTER.PIECETYPE_NAME AS PIECETYPE", "", " PIECETYPEMASTER ", " AND PIECETYPE_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    CMBPIECETYPE.Items.Clear()
                    CMBPIECETYPE.Text = ""
                    For Each DTROW As DataRow In DT.Rows
                        CMBPIECETYPE.Items.Add(DTROW("PIECETYPE"))
                    Next
                    CMBPIECETYPE.Text = GRIDGREY.CurrentRow.Cells(GPIECETYPE.Index).Value
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPIECETYPE_Validating(sender As Object, e As CancelEventArgs) Handles CMBPIECETYPE.Validating
        Try
            If CMBPIECETYPE.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("PIECETYPEMASTER.PIECETYPE_NAME AS PIECETYPE", "", " PIECETYPEMASTER ", " AND PIECETYPE_NAME = '" & CMBPIECETYPE.Text.Trim & "' AND PIECETYPE_YEARID = " & YearId)
                If DT.Rows.Count = 0 Then
                    If MsgBox("Piecetype Not Present, Add New?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDPIECETYPE_Enter(sender As Object, e As EventArgs) Handles CMBGRIDPIECETYPE.Enter
        Try
            If CMBGRIDPIECETYPE.Text.Trim = "" Then fillPIECETYPE(CMBGRIDPIECETYPE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDPIECETYPE_Validating(sender As Object, e As CancelEventArgs) Handles CMBGRIDPIECETYPE.Validating
        Try
            If CMBGRIDPIECETYPE.Text.Trim <> "" Then PIECETYPEvalidate(CMBGRIDPIECETYPE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPIECETYPE_Validated(sender As Object, e As EventArgs) Handles CMBPIECETYPE.Validated
        On Error Resume Next
        GRIDGREY.CurrentRow.Cells(GPIECETYPE.Index).Value = CMBPIECETYPE.Text.Trim
        GRIDGREY.CurrentRow.Cells(GTPNARRATION.Index).Selected = True
        GRIDGREY.Focus()
        CMBPIECETYPE.Visible = False
        TOTAL()
    End Sub
End Class