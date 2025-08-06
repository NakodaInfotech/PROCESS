
Imports BL
Imports System.Windows.Forms
Imports System.IO

Public Class oldGreyRecdFromWeaver

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPGREYRECDNO As Integer
    Dim TEMPMSG As Integer
    Dim TEMPCUT As Integer

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
            LBLTOTALPCS.Text = 0
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALWT.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDGREY.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(ROW.Cells(GPCS.Index).EditedFormattedValue), "0")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.000")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.000")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()

        CMBBEAMNO.DataSource = Nothing
        TXTGREYRECDNO.Clear()
        DTGREYRECDDATE.Text = Mydate
        CMBNAME.Text = ""
        CMBNAME.Enabled = True
        CMBGODOWN.Text = ""
        TXTCHALLANNO.Clear()
        DTCHALLANDATE.Text = Mydate
        CMBBEAMNO.DataSource = Nothing
        LBLTOTALPCS.Text = 0
        LBLTOTALMTRS.Text = 0.0
        LBLTOTALWT.Text = 0.0

        TXTREMARKS.Clear()
        TXTSRNO.Clear()
        CMBGREYQUALITY.Text = ""
        CMBBEAMNO.Text = ""
        TXTPCS.Clear()
        TXTMTRS.Clear()
        TXTWT.Clear()
        TXTQUALITYWT.Clear()
        TXTNARR.Clear()
        TXTFROMNO.Clear()
        TXTFROMSRNO.Clear()
        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False
        GRIDGREY.RowCount = 0

        GETMAX_BEAMRECD_NO()

        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        TabControl1.SelectedIndex = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0

        TXTSRNO.Text = 1
        TXTUPLOADSRNO.Text = 1

    End Sub

    Sub GETMAX_BEAMRECD_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(GRECDWEAVER_NO),0)+1", "GREYRECEIVEDWEAVER", "AND GRECDWEAVER_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            TXTGREYRECDNO.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Private Sub GreyRecdFromWeaver_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                'ElseIf e.KeyCode = Keys.F8 Then
                '    GRIDGREY.Focus()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Sub FILLCMB()
        If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
        If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT, " and GODOWN_ISOUR = 'True'")
        If CMBGREYQUALITY.Text = "" Then fillBEAM(CMBGREYQUALITY, EDIT)

        'If CMBBEAMNO.Text.Trim = "" Then
        '    Dim OBJCMN As New ClsCommonMaster
        '    Dim dt As DataTable
        '    dt = OBJCMN.search(" BEAMISSUE_BEAMNO", "", "  LEDGERS INNER JOIN BEAMISSUETOWEAVER ON LEDGERS.Acc_id = BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID", " AND LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID = " & YearId)
        '    If dt.Rows.Count > 0 Then
        '        dt.DefaultView.Sort = "BEAMISSUE_BEAMNO"
        '        CMBBEAMNO.DataSource = dt
        '        CMBBEAMNO.DisplayMember = "BEAMISSUE_BEAMNO"
        '        If EDIT = False Then CMBBEAMNO.Text = ""
        '    End If
        '    CMBBEAMNO.SelectAll()
        'End If

    End Sub

    Private Sub GreyRecdFromWeaver_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                Dim OBJGREYREC As New ClsGreyRecdFromWeaver

                OBJGREYREC.alParaval.Add(TEMPGREYRECDNO)
                OBJGREYREC.alParaval.Add(YearId)
                dttable = OBJGREYREC.SELECTGREY()

                If dttable.Rows.Count > 0 Then
                    CMBNAME.Focus()

                    TXTGREYRECDNO.Text = TEMPGREYRECDNO
                    DTGREYRECDDATE.Text = dttable.Rows(0).Item("DATE")
                    CMBNAME.Text = dttable.Rows(0).Item("NAME").ToString
                    CMBGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                    TXTCHALLANNO.Text = dttable.Rows(0).Item("CHALLANNO").ToString
                    DTCHALLANDATE.Text = dttable.Rows(0).Item("CHALLANDATE")
                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString

                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDGREY.Rows.Add(Val(ROW("SRNO")), ROW("GREYQUALITY"), ROW("BEAMNO"), Format(Val(ROW("PCS")), "0"), Format(Val(ROW("MTRS")), "0.000"), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("WTMTR")), "0.000"), ROW("NARR"), ROW("FROMNO"), ROW("FROMSRNO"))
                    Next


                    'UPLOAD(GRID)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" GREYRECEIVEDWEAVER_UPLOAD.GRECDWEAVER_SRNO AS GRIDSRNO, GREYRECEIVEDWEAVER_UPLOAD.GRECDWEAVER_REMARKS AS REMARKS, GREYRECEIVEDWEAVER_UPLOAD.GRECDWEAVER_NAME AS NAME, GREYRECEIVEDWEAVER_UPLOAD.GRECDWEAVER_PHOTO AS IMGPATH ", "", " GREYRECEIVEDWEAVER_UPLOAD ", " AND GREYRECEIVEDWEAVER_UPLOAD.GRECDWEAVER_NO = " & TEMPGREYRECDNO & " AND GRECDWEAVER_YEARID = " & YearId & " ORDER BY GREYRECEIVEDWEAVER_UPLOAD.GRECDWEAVER_SRNO")
                    If DT.Rows.Count > 0 Then
                        For Each DTR As DataRow In DT.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If
                    TOTAL()
                    getsrno(GRIDGREY)
                    'CMBNAME.Enabled = False
                End If
            Else
                EDIT = False
                DTGREYRECDDATE.Focus()
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

            alParaval.Add(Format(Convert.ToDateTime(DTGREYRECDDATE.Text.Trim).Date, "MM/dd/yyyy"))

            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBGODOWN.Text.Trim)
            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(DTCHALLANDATE.Text.Trim)

            alParaval.Add(Val(LBLTOTALPCS.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))
            alParaval.Add(Val(LBLTOTALWT.Text))
            alParaval.Add(TXTREMARKS.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim SRNO As String = ""
            Dim GREYQUALITY As String = ""
            Dim BEAMNO As String = ""
            Dim PCS As String = ""
            Dim MTRS As String = ""
            Dim WT As String = ""
            Dim WTMTR As String = ""
            Dim NARR As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDGREY.Rows
                If row.Cells(GSRNO.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = row.Cells(GSRNO.Index).Value
                        GREYQUALITY = row.Cells(GGREYQUALITY.Index).Value.ToString
                        BEAMNO = row.Cells(GBEAMNO.Index).Value.ToString
                        PCS = Val(row.Cells(GPCS.Index).Value)
                        MTRS = Val(row.Cells(GMTRS.Index).Value)
                        WT = Val(row.Cells(GWT.Index).Value)
                        WTMTR = Val(row.Cells(GWTMTR.Index).Value)
                        NARR = row.Cells(GNARR.Index).Value.ToString
                        FROMNO = row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = row.Cells(GFROMSRNO.Index).Value.ToString
                    Else
                        SRNO = SRNO & "|" & row.Cells(GSRNO.Index).Value
                        GREYQUALITY = GREYQUALITY & "|" & row.Cells(GGREYQUALITY.Index).Value.ToString
                        BEAMNO = BEAMNO & "|" & row.Cells(GBEAMNO.Index).Value.ToString
                        PCS = PCS & "|" & Val(row.Cells(GPCS.Index).Value)
                        MTRS = MTRS & "|" & Val(row.Cells(GMTRS.Index).Value)
                        WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
                        WTMTR = WTMTR & "|" & Val(row.Cells(GWTMTR.Index).Value)
                        NARR = NARR & "|" & row.Cells(GNARR.Index).Value.ToString
                        FROMNO = FROMNO & "|" & row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = FROMSRNO & "|" & row.Cells(GFROMSRNO.Index).Value.ToString
                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(GREYQUALITY)
            alParaval.Add(BEAMNO)
            alParaval.Add(PCS)
            alParaval.Add(MTRS)
            alParaval.Add(WT)
            alParaval.Add(WTMTR)
            alParaval.Add(NARR)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)


            Dim OBJGREYREC As New ClsGreyRecdFromWeaver
            OBJGREYREC.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJGREYREC.save()
                TEMPGREYRECDNO = DT.Rows(0).Item(0)
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPGREYRECDNO)
                IntResult = OBJGREYREC.Update()
                EDIT = False
                MsgBox("Details Updated")

            End If

            If gridupload.RowCount > 0 Then SAVEUPLOAD()
            CLEAR()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJGREYREC As New ClsGreyRecdFromWeaver
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

    Private Function errorvalid() As Boolean
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

        If DTCHALLANDATE.Text = "__/__/____" Then
            EP.SetError(DTCHALLANDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTCHALLANDATE.Text) Then
                EP.SetError(DTCHALLANDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If DTGREYRECDDATE.Text.Trim <> "__/__/____" And DTCHALLANDATE.Text.Trim <> "__/__/____" Then
            If Convert.ToDateTime(DTGREYRECDDATE.Text).Date > Convert.ToDateTime(DTCHALLANDATE.Text).Date Then
                EP.SetError(DTCHALLANDATE, " Please Enter Proper Challan Date")
                bln = False
            End If
        End If

        If TXTCHALLANNO.Text.Trim.Length = 0 Then
            EP.SetError(TXTCHALLANNO, "Please Fill Challan No")
            bln = False
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Fill Jobber Name")
            bln = False
        End If

        If CMBGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBGODOWN, " Please Fill Godown ")
            bln = False
        End If

        If GRIDGREY.RowCount = 0 Then
            EP.SetError(TXTNARR, "Enter Proper Details")
            bln = False
        End If

        For Each row As DataGridViewRow In GRIDGREY.Rows
            If Val(row.Cells(GPCS.Index).Value) = 0 Then
                EP.SetError(TXTNARR, "Taka Cannot be 0")
                bln = False
            End If

            If Val(row.Cells(GWT.Index).Value) = 0 Then
                EP.SetError(TXTNARR, "Wt Cannot be 0")
                bln = False
            End If

            If Val(row.Cells(GMTRS.Index).Value) = 0 Then
                EP.SetError(TXTNARR, "Mtrs Cannot be 0")
                bln = False
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            DT = OBJCMN.search("(BEAMISSUE_CUT - BEAMISSUE_OUTCUT) AS ALLOWEDCUT ", "", " BEAMISSUETOWEAVER_DESC ", " AND BEAMISSUE_NO = " & row.Cells(GFROMNO.Index).Value & " AND BEAMISSUE_GRIDSRNO = " & row.Cells(GFROMSRNO.Index).Value & " AND BEAMISSUE_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                If EDIT = False Then
                    If Val(row.Cells(GPCS.Index).Value) > Val(DT.Rows(0).Item("ALLOWEDCUT")) Then
                        EP.SetError(TXTPCS, "Taka Not Present, Maximum " & Val(DT.Rows(0).Item("ALLOWEDCUT")) & " Taka Allowed")
                        bln = False
                    End If
                Else
                    Dim DT1 As DataTable = OBJCMN.search("GRECDWEAVER_PCS AS OLDPCS ", "", " GREYRECEIVEDWEAVER_DESC ", " AND GRECDWEAVER_NO = " & TEMPGREYRECDNO & " AND GRECDWEAVER_FROMNO = " & row.Cells(GFROMNO.Index).Value & " AND GRECDWEAVER_FROMSRNO = " & row.Cells(GFROMSRNO.Index).Value & " AND GRECDWEAVER_YEARID = " & YearId)
                    If DT1.Rows.Count > 0 Then
                        If Val(row.Cells(GPCS.Index).Value) > (Val(DT.Rows(0).Item("ALLOWEDCUT")) + Val(DT1.Rows(0).Item("OLDPCS"))) Then
                            EP.SetError(TXTPCS, "Taka Not Present, Maximum " & (Val(DT.Rows(0).Item("ALLOWEDCUT")) + Val(DT1.Rows(0).Item("OLDPCS"))) & " Taka Allowed")
                            bln = False
                        End If
                    End If
                End If
            End If
        Next


        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Modify, entry Locked")
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
            If CMBNAME.Text <> "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" BEAMISSUE_BEAMNO", "", "  LEDGERS INNER JOIN BEAMISSUETOWEAVER ON LEDGERS.Acc_id = BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID", " AND LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND  (BEAMISSUE_CUT - BEAMISSUE_OUTCUT) >0  AND  BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "BEAMISSUE_BEAMNO"
                    CMBBEAMNO.DataSource = dt
                    CMBBEAMNO.DisplayMember = "BEAMISSUE_BEAMNO"
                    If EDIT = False Then CMBBEAMNO.Text = ""
                End If
                CMBBEAMNO.SelectAll()
            End If
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
                Dim DT As DataTable = OBJCMN.search(" GRECDWEAVER_NO ", "", "  GREYRECEIVEDWEAVER", " AND GRECDWEAVER_NO = '" & TEMPGREYRECDNO & "' AND GREYRECEIVEDWEAVER.GRECDWEAVER_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    GreyRecdFromWeaver_Load(sender, e)
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
                GreyRecdFromWeaver_Load(sender, e)
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
                    GreyRecdFromWeaver_Load(sender, e)
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

    Private Sub GRIDGREY_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDGREY.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDGREY.Item(GSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDGREY.Item(GSRNO.Index, e.RowIndex).Value
                CMBGREYQUALITY.Text = GRIDGREY.Item(GGREYQUALITY.Index, e.RowIndex).Value
                CMBBEAMNO.Text = GRIDGREY.Item(GBEAMNO.Index, e.RowIndex).Value
                TXTPCS.Text = Val(GRIDGREY.Item(GPCS.Index, e.RowIndex).Value)
                TXTMTRS.Text = Val(GRIDGREY.Item(GMTRS.Index, e.RowIndex).Value)
                TXTWT.Text = Val(GRIDGREY.Item(GWT.Index, e.RowIndex).Value)
                TXTQUALITYWT.Text = Val(GRIDGREY.Item(GWTMTR.Index, e.RowIndex).Value)
                TXTNARR.Text = GRIDGREY.Item(GNARR.Index, e.RowIndex).Value

                TEMPROW = e.RowIndex
                CMBGREYQUALITY.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGREY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDGREY.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDGREY.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDGREY.Rows.RemoveAt(GRIDGREY.CurrentRow.Index)
                getsrno(GRIDGREY)
                TXTSRNO.Text = GRIDGREY.RowCount + 1
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
                    alParaval.Add(TXTGREYRECDNO.Text.Trim)
                    alParaval.Add(YearId)

                    Dim OBJDEL As New ClsGreyRecdFromWeaver
                    OBJDEL.alParaval = alParaval
                    IntResult = OBJDEL.Delete()
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
                GRIDGREY.Rows.Add(Val(TXTSRNO.Text.Trim), CMBGREYQUALITY.Text.Trim, CMBBEAMNO.Text.Trim, Format(Val(TXTPCS.Text.Trim), "0"), Format(Val(TXTMTRS.Text.Trim), "0.000"), Format(Val(TXTWT.Text.Trim), "0.000"), Format(Val(TXTQUALITYWT.Text.Trim), "0.000"), TXTNARR.Text.Trim, TXTFROMNO.Text.Trim, TXTFROMSRNO.Text.Trim)
            Else
                GRIDGREY.Item(GSRNO.Index, TEMPROW).Value = TXTSRNO.Text.Trim
                GRIDGREY.Item(GGREYQUALITY.Index, TEMPROW).Value = CMBGREYQUALITY.Text.Trim
                GRIDGREY.Item(GBEAMNO.Index, TEMPROW).Value = CMBBEAMNO.Text.Trim
                GRIDGREY.Item(GPCS.Index, TEMPROW).Value = Format(Val(TXTPCS.Text.Trim), "0")
                GRIDGREY.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.000")
                GRIDGREY.Item(GWT.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.000")
                GRIDGREY.Item(GWTMTR.Index, TEMPROW).Value = Format(Val(TXTQUALITYWT.Text.Trim), "0.000")
                GRIDGREY.Item(GNARR.Index, TEMPROW).Value = TXTNARR.Text.Trim

                GRIDDOUBLECLICK = False
            End If
            CMBGREYQUALITY.Text = ""
            CMBBEAMNO.Text = ""
            TXTPCS.Clear()
            TXTMTRS.Clear()
            TXTWT.Clear()
            TXTQUALITYWT.Clear()
            TXTNARR.Clear()
            getsrno(GRIDGREY)
            TOTAL()
            CMBGREYQUALITY.Focus()
            If GRIDGREY.RowCount > 0 Then TXTSRNO.Text = Val(GRIDGREY.RowCount) + 1 Else TXTSRNO.Text = 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub calc()
        Try
            If CMBGREYQUALITY.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(GREY_QUALITYWT, 0) AS QUALITYWT, ISNULL(GREY_MTRS, 0) AS MTRS", "", "GREYQUALITYMASTER", "AND GREYQUALITYMASTER.GREY_NAME = '" & CMBGREYQUALITY.Text.Trim & "' AND GREY_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TXTMTRS.Text = Format(Val(TXTPCS.Text.Trim) * DT.Rows(0).Item("MTRS"), "0.000")
                    TXTQUALITYWT.Text = DT.Rows(0).Item("QUALITYWT")
                End If
                TXTWT.Text = Format((Val(TXTQUALITYWT.Text.Trim) * Val(TXTMTRS.Text.Trim)) / 100, "0.000")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function CHECKGRID() As Boolean
        Try
            Dim bln As Boolean = True
            For Each ROW As DataGridViewRow In GRIDGREY.Rows
                If (GRIDDOUBLECLICK = False And LCase(CMBBEAMNO.Text.Trim) = LCase(ROW.Cells(GBEAMNO.Index).Value)) Or (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index And LCase(CMBBEAMNO.Text.Trim) = LCase(ROW.Cells(GBEAMNO.Index).Value)) Then
                    bln = False
                    Exit For
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub TXTNARR_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNARR.Validating
        If CMBGREYQUALITY.Text.Trim <> "" And CMBBEAMNO.Text.Trim <> "" And Val(TXTPCS.Text.Trim) > 0 And Val(TXTMTRS.Text.Trim) > 0 And Val(TXTWT.Text.Trim) > 0 Then
            If Not CHECKGRID() Then
                MsgBox("Beam No Already Present Below", MsgBoxStyle.Critical)
                Exit Sub
            End If
            FILLGRID()
        Else
            MsgBox("Please Enter proper details")
        End If
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress
        numdotkeypress(e, TXTMTRS, Me)
    End Sub

    Private Sub TXTMTRS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTMTRS.Validating
        TXTWT.Text = Format((Val(TXTQUALITYWT.Text.Trim) * Val(TXTMTRS.Text.Trim)) / 100, "0.000")
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress
        numdotkeypress(e, TXTWT, Me)
    End Sub

    Private Sub TXTPCS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPCS.KeyPress
        numkeypress(e, TXTPCS, Me)
    End Sub

    Private Sub CMBGREYQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGREYQUALITY.Enter
        Try
            If CMBGREYQUALITY.Text.Trim = "" Then FILLGREY(CMBGREYQUALITY, EDIT)
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

    Private Sub CMBGREYQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGREYQUALITY.Validating
        Try
            If CMBGREYQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBGREYQUALITY, e, Me)
            If CMBGREYQUALITY.Text = "" Then TXTREMARKS.Focus()
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

    Private Sub TXTPCS_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTPCS.Validated
        Dim OBJCMN As New ClsCommon
        Dim ALLOWED As Boolean

        If CMBNAME.Text <> "" And CMBBEAMNO.Text <> "" And Val(TXTPCS.Text) > 0 And GRIDDOUBLECLICK = False Then
            Dim DT As DataTable = OBJCMN.search("ISNULL(BEAMISSUE_CUT - BEAMISSUE_OUTCUT, 0) AS BALCUT,ISNULL(BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO, 0) AS FROMNO, ISNULL(BEAMISSUETOWEAVER_DESC.BEAMISSUE_GRIDSRNO, 0) AS FROMSRNO", "", " BEAMISSUETOWEAVER INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID INNER JOIN LEDGERS ON BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID = LEDGERS.Acc_id ", " AND   LEDGERS.Acc_cmpname='" & CMBNAME.Text.Trim & "' AND  BEAMISSUE_BEAMNO = '" & CMBBEAMNO.Text.Trim & "' AND  ISNULL(BEAMISSUE_CUT - BEAMISSUE_OUTCUT, 0)>0  AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = " & YearId)
            'If DT.Rows.Count > 0 Then
            '    For Each ROW As DataRow In DT.Rows
            '        Dim DT2 As DataTable = OBJCMN.search("ISNULL(BEAMISSUE_CUT - BEAMISSUE_OUTCUT, 0) AS BALCUT", "", " BEAMISSUETOWEAVER INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID INNER JOIN LEDGERS ON BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID = LEDGERS.Acc_id ", " AND   LEDGERS.Acc_cmpname='" & CMBNAME.Text.Trim & "' AND  BEAMISSUE_BEAMNO = '" & CMBBEAMNO.Text.Trim & "' AND  ISNULL(BEAMISSUE_CUT - BEAMISSUE_OUTCUT, 0)>0 AND BEAMISSUETOWEAVER.BEAMISSUE_NO=" & Val(ROW("FROMNO")) & " AND BEAMISSUETOWEAVER_DESC.BEAMISSUE_GRIDSRNO=" & Val(ROW("FROMSRNO")) & "  AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = " & YearId)
            '        If DT2.Rows.Count > 0 Then
            '            If Val(DT2.Rows(0).Item("BALCUT")) >= Val(TXTPCS.Text.Trim) Then
            '                TXTFROMNO.Text = Val(ROW("FROMNO"))
            '                TXTFROMSRNO.Text = Val(ROW("FROMSRNO"))
            '            Else
            '                MsgBox("Invalid Entry!")
            '                Exit Sub
            '                TXTFROMNO.Clear()
            '                TXTFROMSRNO.Clear()
            '            End If
            '        End If
            '    Next
            'End If

            If DT.Rows.Count > 0 Then
                For Each ROW As DataRow In DT.Rows
                    If Val(ROW("BALCUT")) >= Val(TXTPCS.Text.Trim) Then
                        ALLOWED = True
                        TXTFROMNO.Text = ROW("FROMNO")
                        TXTFROMSRNO.Text = ROW("FROMSRNO")
                    End If
                Next
                If Not ALLOWED Then
                    MsgBox("Not Allowed")
                    TXTPCS.Focus()
                    Exit Sub
                End If
            End If

        End If
    End Sub

    Private Sub TXTPCS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTPCS.Validating
        If GRIDDOUBLECLICK = False Then calc()
    End Sub

    Private Sub CMBBEAMNO_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBEAMNO.Validating
        Try
            Dim OBJCMN As New ClsCommon
            If CMBNAME.Text <> "" And CMBBEAMNO.Text <> "" Then
                Dim DT As DataTable = OBJCMN.search("top 1 ISNULL(BEAMISSUE_CUT - BEAMISSUE_OUTCUT, 0) AS CUT,ISNULL(BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO, 0) AS FROMNO, ISNULL(BEAMISSUETOWEAVER_DESC.BEAMISSUE_GRIDSRNO, 0) AS FROMSRNO ", "", " BEAMISSUETOWEAVER INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID INNER JOIN LEDGERS ON BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID = LEDGERS.Acc_id ", " AND   LEDGERS.Acc_cmpname='" & CMBNAME.Text.Trim & "' AND  BEAMISSUE_BEAMNO = '" & CMBBEAMNO.Text.Trim & "' AND  ISNULL(BEAMISSUE_CUT - BEAMISSUE_OUTCUT, 0)>0  AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPCUT = Val(DT.Rows(0).Item("CUT"))
                End If

                If GRIDGREY.RowCount > 0 Then
                    For Each DTROW As DataGridViewRow In GRIDGREY.Rows
                        Dim DT1 As DataTable = OBJCMN.search("GRECDWEAVER_PCS AS OLDPCS ", "", " GREYRECEIVEDWEAVER_DESC ", " AND GRECDWEAVER_NO = " & TEMPGREYRECDNO & " AND GRECDWEAVER_BEAMNO = '" & DTROW.Cells(GBEAMNO.Index).Value & "' AND GRECDWEAVER_FROMNO = " & DTROW.Cells(GFROMNO.Index).Value & " AND GRECDWEAVER_FROMSRNO = " & DTROW.Cells(GFROMSRNO.Index).Value & " AND GRECDWEAVER_YEARID = " & YearId)
                        If DT1.Rows.Count > 0 Then
                            If DT.Rows(0).Item("FROMNO") = DTROW.Cells(GFROMNO.Index).Value And DT.Rows(0).Item("FROMSRNO") = DTROW.Cells(GFROMSRNO.Index).Value Then
                                TXTPCS.Text = Val(TEMPCUT) + Val(DT1.Rows(0).Item("OLDPCS"))
                            Else
                                TXTPCS.Text = TEMPCUT
                            End If
                        End If
                    Next
                Else
                    TXTPCS.Text = TEMPCUT
                End If

            End If

            'For Each DTROW As DataGridViewRow In GRIDGREY.Rows
            '    Dim DT As DataTable = OBJCMN.search("(BEAMISSUE_CUT - BEAMISSUE_OUTCUT) AS ALLOWEDCUT ", "", " BEAMISSUETOWEAVER_DESC ", " AND BEAMISSUE_NO = " & row.Cells(GFROMNO.Index).Value & " AND BEAMISSUE_GRIDSRNO = " & row.Cells(GFROMSRNO.Index).Value & " AND BEAMISSUE_YEARID = " & YearId)
            '    If DT.Rows.Count > 0 Then
            '        If EDIT = False Then
            '            If Val(row.Cells(GPCS.Index).Value) > Val(DT.Rows(0).Item("ALLOWEDCUT")) Then
            '                EP.SetError(TXTPCS, "Taka Not Present, Maximum " & Val(DT.Rows(0).Item("ALLOWEDCUT")) & " Taka Allowed")
            '            End If
            '        Else
            '            Dim DT1 As DataTable = OBJCMN.search("GRECDWEAVER_PCS AS OLDPCS ", "", " GREYRECEIVEDWEAVER_DESC ", " AND GRECDWEAVER_NO = " & TEMPGREYRECDNO & " AND GRECDWEAVER_FROMNO = " & row.Cells(GFROMNO.Index).Value & " AND GRECDWEAVER_FROMSRNO = " & row.Cells(GFROMSRNO.Index).Value & " AND GRECDWEAVER_YEARID = " & YearId)
            '            If DT1.Rows.Count > 0 Then
            '                If Val(row.Cells(GPCS.Index).Value) > (Val(DT.Rows(0).Item("ALLOWEDCUT")) + Val(DT1.Rows(0).Item("OLDPCS"))) Then
            '                    EP.SetError(TXTPCS, "Taka Not Present, Maximum " & (Val(DT.Rows(0).Item("ALLOWEDCUT")) + Val(DT1.Rows(0).Item("OLDPCS"))) & " Taka Allowed")
            '                End If
            '            End If
            '        End If
            '    End If
            'Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
        If CMBNAME.Text = "" Then
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable
            dt = OBJCMN.search("BEAMISSUE_BEAMNO", "", "  LEDGERS INNER JOIN BEAMISSUETOWEAVER ON LEDGERS.Acc_id = BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID", " AND LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND  (BEAMISSUE_CUT - BEAMISSUE_OUTCUT) >0  AND  BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "BEAMISSUE_BEAMNO"
                CMBBEAMNO.DataSource = dt
                CMBBEAMNO.DisplayMember = "BEAMISSUE_BEAMNO"
                If EDIT = False Then CMBBEAMNO.Text = ""
            End If
            CMBBEAMNO.SelectAll()
        End If
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
End Class