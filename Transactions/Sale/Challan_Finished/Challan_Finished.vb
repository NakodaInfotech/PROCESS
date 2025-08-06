
Imports BL
Imports System.Windows.Forms
Imports System.IO
Imports System.ComponentModel

Public Class Challan_Finished

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW, PURREGID As Integer
    Public EDIT As Boolean
    Public TEMPCHALLANNO As Integer
    Dim TEMPMSG As Integer

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBDELIVERYFROM.Focus()
    End Sub

    Sub CLEAR()

        TXTCHALLANNO.Clear()
        DTCHALLANDATE.Text = Mydate

        cmbname.Text = ""
        cmbname.Enabled = True
        CMBOURGODOWN.Text = ""
        CMBOURGODOWN.Enabled = True

        cmbtrans.Text = ""
        TXTLRNO.Clear()
        CMBTOCITY.Text = ""
        CMBBROKER.Text = ""
        CMBSHIPTO.Text = ""
        CMBDELIVERYFROM.Text = ""
        CMBDELIVERYFROM.Enabled = True
        txtremarks.Clear()

        TXTORDERNO.Clear()
        TXTORDERSRNO.Clear()


        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        txtremarks.Clear()

        GRIDCHALLAN.RowCount = 0

        GETMAX_CHALLAN_NO()

        LBLTOTALTAKA.Text = 0
        LBLTOTALMTRS.Text = 0.0
        LBLTOTALMTRS.Text = 0.0


        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False
        CMDSELECTBALESTOCK.Enabled = True
        CMDSELECTORDER.Enabled = True

        TabControl1.SelectedIndex = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0
        TXTUPLOADSRNO.Text = 1

    End Sub

    Sub TOTAL()
        Try
            LBLTOTALTAKA.Text = 0
            LBLTOTALMTRS.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDCHALLAN.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALTAKA.Text = Format(Val(LBLTOTALTAKA.Text) + Val(ROW.Cells(GTAKA.Index).EditedFormattedValue), "0")
                    If CMBTYPE.Text = "SAREE" Then ROW.Cells(GMTRS.Index).Value = Format(Val(ROW.Cells(GTAKA.Index).EditedFormattedValue) * Val(ROW.Cells(GCUT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAX_CHALLAN_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(CHALLANFINISH_NO),0)+1", "CHALLANFINISHMASTER", "AND CHALLANFINISH_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTCHALLANNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub Challan_Finished_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call PrintToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Windows.Forms.Keys.F5 Then       'for grid foucs
                GRIDCHALLAN.Focus()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Sub FILLCMB()
        If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        If CMBDELIVERYFROM.Text.Trim = "" Then fillname(CMBDELIVERYFROM, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (ACC_SUBTYPE = 'PROCESSOR' OR ACC_SUBTYPE = 'JOBBER') ")
        If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR='TRUE'")
        If cmbtrans.Text = "" Then fillname(cmbtrans, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        If CMBBROKER.Text.Trim = "" Then fillname(CMBBROKER, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'AGENTS'")
        If CMBSHIPTO.Text.Trim = "" Then fillname(CMBSHIPTO, EDIT, "AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        If CMBTOCITY.Text.Trim = "" Then fillCITY(CMBTOCITY, EDIT)
    End Sub

    Private Sub Challan_Finished_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GDN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            CLEAR()
            CMBTYPE.SelectedIndex = 0

            If EDIT = True Then
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dttable As New DataTable
                Dim OBJCHALLAN As New ClsChallanFinish

                OBJCHALLAN.alParaval.Add(TEMPCHALLANNO)
                OBJCHALLAN.alParaval.Add(YearId)
                dttable = OBJCHALLAN.SELECTCHALLAN()

                If dttable.Rows.Count > 0 Then
                    cmbname.Focus()

                    TXTCHALLANNO.Text = TEMPCHALLANNO
                    CMBTYPE.Text = dttable.Rows(0).Item("TYPE")
                    SETGRID()

                    DTCHALLANDATE.Text = dttable.Rows(0).Item("DATE")
                    TXTORDERNO.Text = dttable.Rows(0).Item("ORDERNO")
                    TXTORDERSRNO.Text = dttable.Rows(0).Item("ORDERSRNO")
                    TXTORDERTYPE.Text = dttable.Rows(0).Item("ORDERTYPE")
                    CMBDELIVERYFROM.Text = dttable.Rows(0).Item("DELIVERYAT").ToString
                    CMBOURGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                    cmbname.Text = dttable.Rows(0).Item("NAME").ToString
                    cmbtrans.Text = dttable.Rows(0).Item("TRANS").ToString
                    TXTLRNO.Text = dttable.Rows(0).Item("LRNO").ToString
                    CMBTOCITY.Text = dttable.Rows(0).Item("DESTINATION").ToString
                    CMBBROKER.Text = dttable.Rows(0).Item("BROKER").ToString
                    CMBSHIPTO.Text = dttable.Rows(0).Item("SHIPTO").ToString
                    txtremarks.Text = dttable.Rows(0).Item("REMARKS").ToString

                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDCHALLAN.Rows.Add(Val(ROW("SRNO")), ROW("GREYQUALITY").ToString, ROW("LOTNO").ToString, ROW("BALENO"), Val(ROW("TAKA")), Format(Val(ROW("CUT")), "0.00"), Format(Val(ROW("MTRS")), "0.00"), ROW("NARRATION"), ROW("FROMNO"), ROW("FROMSRNO"), ROW("GRIDTYPE"))
                    Next

                    If Convert.ToBoolean(dttable.Rows(0).Item("DONE")) = True Then
                        lbllocked.Visible = True
                        PBlock.Visible = True
                    End If


                    'UPLOAD(GRID)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" CHALLANMASTER_UPLOAD.CHALLAN_SRNO AS GRIDSRNO, CHALLANMASTER_UPLOAD.CHALLAN_REMARKS AS REMARKS, CHALLANMASTER_UPLOAD.CHALLAN_NAME AS NAME, CHALLANMASTER_UPLOAD.CHALLAN_PHOTO AS IMGPATH ", "", " CHALLANMASTER_UPLOAD ", " AND CHALLANMASTER_UPLOAD.CHALLAN_NO = " & TEMPCHALLANNO & " AND CHALLAN_YEARID = " & YearId & " ORDER BY CHALLANMASTER_UPLOAD.CHALLAN_SRNO")
                    If DT.Rows.Count > 0 Then
                        For Each DTR As DataRow In DT.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If

                    'CMDSELECTBALESTOCK.Enabled = False
                    'CMDSELECTORDER.Enabled = False
                    cmbname.Enabled = False
                    CMBDELIVERYFROM.Enabled = False
                    CMBOURGODOWN.Enabled = False
                    TOTAL()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DTCHALLANDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBTYPE.Text.Trim)
            alParaval.Add(Val(TXTORDERNO.Text.Trim))
            alParaval.Add(Val(TXTORDERSRNO.Text.Trim))
            alParaval.Add(TXTORDERTYPE.Text.Trim)
            alParaval.Add(CMBDELIVERYFROM.Text.Trim)
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(TXTLRNO.Text.Trim)
            alParaval.Add(CMBTOCITY.Text.Trim)
            alParaval.Add(CMBBROKER.Text.Trim)
            alParaval.Add(CMBSHIPTO.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(Val(LBLTOTALTAKA.Text.Trim))
            alParaval.Add(Val(LBLTOTALMTRS.Text.Trim))

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim SRNO As String = ""
            Dim QUALITY As String = ""
            Dim LOTNO As String = ""
            Dim BALENO As String = ""
            Dim TAKA As String = ""
            Dim CUT As String = ""
            Dim MTRS As String = ""
            Dim NARR As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim GRIDTYPE As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDCHALLAN.Rows
                If row.Cells(GSRNO.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = Val(row.Cells(GSRNO.Index).Value)
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = row.Cells(GBALENO.Index).Value.ToString Else BALENO = ""
                        TAKA = Format(Val(row.Cells(GTAKA.Index).Value), "0")
                        CUT = Format(Val(row.Cells(GCUT.Index).Value), "0.00")
                        MTRS = Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        If row.Cells(GNARR.Index).Value <> Nothing Then NARR = row.Cells(GNARR.Index).Value.ToString Else NARR = ""
                        FROMNO = row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = row.Cells(GFROMSRNO.Index).Value.ToString
                        GRIDTYPE = row.Cells(GTYPE.Index).Value.ToString

                    Else

                        SRNO = SRNO & "|" & row.Cells(GSRNO.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString Else BALENO = BALENO & "|" & ""
                        TAKA = TAKA & "|" & Format(Val(row.Cells(GTAKA.Index).Value), "0")
                        CUT = CUT & "|" & Format(Val(row.Cells(GCUT.Index).Value), "0.00")
                        MTRS = MTRS & "|" & Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        If row.Cells(GNARR.Index).Value <> Nothing Then NARR = NARR & "|" & row.Cells(GNARR.Index).Value.ToString Else NARR = NARR & "|" & ""
                        FROMNO = FROMNO & "|" & row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = FROMSRNO & "|" & row.Cells(GFROMSRNO.Index).Value.ToString
                        GRIDTYPE = GRIDTYPE & "|" & row.Cells(GTYPE.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(QUALITY)
            alParaval.Add(LOTNO)
            alParaval.Add(BALENO)
            alParaval.Add(TAKA)
            alParaval.Add(CUT)
            alParaval.Add(MTRS)
            alParaval.Add(NARR)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(GRIDTYPE)

            Dim OBJCHALLAN As New ClsChallanFinish
            OBJCHALLAN.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJCHALLAN.SAVE()
                TEMPCHALLANNO = DT.Rows(0).Item(0)
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPCHALLANNO)
                IntResult = OBJCHALLAN.UPDATE()
                EDIT = False
                MsgBox("Details Updated")

            End If

            If gridupload.RowCount > 0 Then SAVEUPLOAD()
            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTCHALLANDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJBEAMISSUE As New ClsChallanFinish


            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPCHALLANNO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJBEAMISSUE.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJBEAMISSUE.SAVEUPLOAD()
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


        If CMBSHIPTO.Text.Trim = "" And cmbname.Text.Trim <> "" Then CMBSHIPTO.Text = cmbname.Text.Trim


        If DTCHALLANDATE.Text = "__/__/____" Then
            EP.SetError(DTCHALLANDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTCHALLANDATE.Text) Then
                EP.SetError(DTCHALLANDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If Val(TXTORDERNO.Text.Trim) = 0 And ClientName <> "HARIA" Then
            EP.SetError(TXTORDERNO, "Select Order")
            bln = False
        End If

        If cmbname.Text.Trim.Length = 0 Then
            EP.SetError(cmbname, "Please Fill Jobber Name")
            bln = False
        End If

        'BALE ARE NOT AT OUR GODOWN
        'If CMBOURGODOWN.Text.Trim.Length = 0 Then
        '    EP.SetError(CMBOURGODOWN, " Please Fill Our Godown Name ")
        '    bln = False
        'End If

        If cmbtrans.Text.Trim.Length = 0 Then
            EP.SetError(cmbtrans, " Please Select Transport")
            bln = False
        End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Modify, entry Locked")
            bln = False
        End If

        If GRIDCHALLAN.RowCount = 0 Then
            EP.SetError(cmbname, "Enter Proper Details")
            bln = False
        Else
            For Each ROW As DataGridViewRow In GRIDCHALLAN.Rows
                If Val(ROW.Cells(GTAKA.Index).Value) = 0 Then
                    EP.SetError(cmbname, "Pcs cannot be 0")
                    bln = False
                End If
                If Val(ROW.Cells(GMTRS.Index).Value) = 0 Then
                    EP.SetError(cmbname, "Mtrs cannot be 0")
                    bln = False
                End If
            Next
        End If

        Return bln
    End Function

    Private Sub DTCHALLANDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTCHALLANDATE.GotFocus
        DTCHALLANDATE.SelectAll()
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

    Private Sub CMBOURGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBOURGODOWN.Enter
        Try
            If CMBOURGODOWN.Text.Trim <> "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR='TRUE'")
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
            If CMBOURGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBOURGODOWN, e, Me, " AND GODOWN_ISOUR='TRUE'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE='ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'", "SUNDRY DEBTORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBROKER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBBROKER.Enter
        Try
            If CMBBROKER.Text.Trim = "" Then fillname(CMBBROKER, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBROKER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBBROKER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBROKER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBROKER.Validating
        Try
            If CMBBROKER.Text.Trim <> "" Then namevalidate(CMBBROKER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'AGENT'", "SUNDRY CREDITORS", "AGENT")
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

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDCHALLAN.RowCount = 0
LINE1:
            TEMPCHALLANNO = Val(TXTCHALLANNO.Text) - 1
Line2:
            If TEMPCHALLANNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" CHALLANFINISH_NO ", "", "  CHALLANFINISHMASTER", " AND CHALLANFINISH_NO = '" & TEMPCHALLANNO & "' AND CHALLANFINISHMASTER.CHALLANFINISH_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    Challan_Finished_Load(sender, e)
                Else
                    TEMPCHALLANNO = Val(TEMPCHALLANNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDCHALLAN.RowCount = 0 And TEMPCHALLANNO > 1 Then
                TXTCHALLANNO.Text = TEMPCHALLANNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDCHALLAN.RowCount = 0
LINE1:
            TEMPCHALLANNO = Val(TXTCHALLANNO.Text) + 1
            GETMAX_CHALLAN_NO()
            Dim MAXNO As Integer = TXTCHALLANNO.Text.Trim
            CLEAR()
            If Val(TXTCHALLANNO.Text) - 1 >= TEMPCHALLANNO Then
                EDIT = True
                Challan_Finished_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDCHALLAN.RowCount = 0 And TEMPCHALLANNO < MAXNO Then
                TXTCHALLANNO.Text = TEMPCHALLANNO
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
                GRIDCHALLAN.RowCount = 0
                TEMPCHALLANNO = Val(tstxtbillno.Text)
                If TEMPCHALLANNO > 0 Then
                    EDIT = True
                    Challan_Finished_Load(sender, e)
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
                    MsgBox("Unable to Delete, Challan Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Delete Challan?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(CMBTYPE.Text.Trim)
                    alParaval.Add(TEMPCHALLANNO)
                    alParaval.Add(YearId)

                    Dim OBJSO As New ClsChallanFinish()
                    OBJSO.alParaval = alParaval
                    IntResult = OBJSO.DELETE()
                    MsgBox("Challan Deleted")
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

    Private Sub CMDSELECTBALESTOCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTBALESTOCK.Click
        Try

            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If CMBDELIVERYFROM.Text.Trim <> "" Then

                If CMBTYPE.Text = "FINISHED" Then
                    Dim OBJSELECTSTOCK As New SelectBaleStock
                    OBJSELECTSTOCK.TEMPGODOWNNAME = CMBOURGODOWN.Text.Trim

                    'THIS CODE IS WRITTEN BY GULKIT, TO OPEN ONLY THOSE BALES WHICH ARE WITH PROCESSOR
                    If CMBOURGODOWN.Text.Trim = "" Then OBJSELECTSTOCK.FRMSTRING = "BALESTOCKPROCESSOR"

                    OBJSELECTSTOCK.PROCESSORNAME = CMBDELIVERYFROM.Text.Trim
                    Dim DTBEAMSTOCK As DataTable = OBJSELECTSTOCK.DT
                    OBJSELECTSTOCK.ShowDialog()
                    If DTBEAMSTOCK.Rows.Count > 0 Then
                        For Each ROW As DataRow In DTBEAMSTOCK.Rows
                            GRIDCHALLAN.Rows.Add(0, ROW("GREYQUALITY"), ROW("LOTNO"), ROW("BALENO"), Val(ROW("PCS")), 0, Format(Val(ROW("MTRS")), "0.00"), "", ROW("FROMNO"), ROW("FROMSRNO"), ROW("TYPE"))
                        Next
                        'DONE BY GULKIT
                        'USER NEEDS TO SEND BALES OF MULTIPLE LOT IN SAME CHALLAN
                        'CMDSELECTBALESTOCK.Enabled = False
                        TOTAL()
                        getsrno(GRIDCHALLAN)
                    End If
                Else
                    Dim OBJSELECTSTOCK As New SelectSareeStock
                    OBJSELECTSTOCK.TEMPGODOWNNAME = CMBOURGODOWN.Text.Trim
                    OBJSELECTSTOCK.JOBBERNAME = CMBDELIVERYFROM.Text.Trim
                    Dim DTBEAMSTOCK As DataTable = OBJSELECTSTOCK.DT
                    OBJSELECTSTOCK.ShowDialog()
                    If DTBEAMSTOCK.Rows.Count > 0 Then
                        For Each ROW As DataRow In DTBEAMSTOCK.Rows
                            GRIDCHALLAN.Rows.Add(0, ROW("GREYQUALITY"), ROW("LOTNO"), ROW("BALENO"), Val(ROW("PCS")), Format(Val(ROW("CUT")), "0.00"), Format(Val(ROW("MTRS")), "0.00"), "", ROW("FROMNO"), ROW("FROMSRNO"), ROW("TYPE"))
                        Next
                        CMDSELECTBALESTOCK.Enabled = False
                        TOTAL()
                        getsrno(GRIDCHALLAN)
                    End If
                End If

            Else
                MsgBox("Select Processor Name")
                CMBDELIVERYFROM.Focus()
            End If
            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCHALLAN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCHALLAN.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDCHALLAN.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDCHALLAN.Rows.RemoveAt(GRIDCHALLAN.CurrentRow.Index)
                getsrno(GRIDCHALLAN)
                TOTAL()

            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRIDCHALLAN_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDCHALLAN.CellValidating
        Try
            If CMBTYPE.Text <> "SAREE" Then Exit Sub

            Dim colNum As Integer = GRIDCHALLAN.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GTAKA.Index ', gcount.Index
                    Dim dDebit As Integer
                    Dim bValid As Boolean = Integer.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDCHALLAN.CurrentCell.Value = Nothing Then GRIDCHALLAN.CurrentCell.Value = "0"
                        GRIDCHALLAN.CurrentCell.Value = Convert.ToDecimal(GRIDCHALLAN.Item(colNum, e.RowIndex).Value)
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

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJCHALLAN As New Challan_Finished_Details
            OBJCHALLAN.MdiParent = MDIMain
            OBJCHALLAN.Show()
        Catch EX As Exception
            Throw EX
        End Try
    End Sub

    Private Sub CMDSELECTORDER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTORDER.Click
        Try

            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If cmbname.Text.Trim = "" Then
                MsgBox("Select Party Name First !", MsgBoxStyle.Critical)
                cmbname.Focus()
                Exit Sub
            End If

            Dim OBJSELECTSO As New SelectOrder
            OBJSELECTSO.PARTYNAME = cmbname.Text.Trim
            OBJSELECTSO.TYPE = "FINISHED"
            OBJSELECTSO.ShowDialog()
            Dim DTORDER As DataTable = OBJSELECTSO.DT
            If DTORDER.Rows.Count > 0 Then

                If DTORDER.Rows(0).Item("AGENT") <> "" Then CMBBROKER.Text = DTORDER.Rows(0).Item("AGENT")
                TXTORDERNO.Text = DTORDER.Rows(0).Item("ORDERNO")
                TXTORDERSRNO.Text = DTORDER.Rows(0).Item("ORDERSRNO")
                TXTORDERTYPE.Text = DTORDER.Rows(0).Item("ORDERTYPE")
                If DTORDER.Rows(0).Item("TRANSPORT") <> "" Then cmbtrans.Text = DTORDER.Rows(0).Item("TRANSPORT")
                CMDSELECTORDER.Enabled = False

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDELIVERYAT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBDELIVERYFROM.Enter
        Try
            If CMBDELIVERYFROM.Text.Trim = "" Then
                If CMBTYPE.Text = "FINISHED" Then
                    fillname(CMBDELIVERYFROM, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'PROCESSOR'")
                Else
                    fillname(CMBDELIVERYFROM, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'JOBBER'")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDELIVERYAT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBDELIVERYFROM.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                If CMBTYPE.Text = "FINISHED" Then OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'" Else OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'JOBBER'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBDELIVERYFROM.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDELIVERYAT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDELIVERYFROM.Validating
        Try
            If CMBDELIVERYFROM.Text.Trim <> "" Then
                If CMBTYPE.Text = "FINISHED" Then
                    namevalidate(CMBDELIVERYFROM, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
                Else
                    namevalidate(CMBDELIVERYFROM, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'JOBBER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "JOBBER")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTOCITY.Enter
        Try
            If CMBTOCITY.Text.Trim = "" Then fillCITY(CMBTOCITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTOCITY.Validating
        Try
            If CMBTOCITY.Text.Trim <> "" Then CITYVALIDATE(CMBTOCITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SETGRID()
        Try
            If CMBTYPE.Text.Trim = "FINISHED" Then
                BTNQUALITY.Text = "Finished Quality"
                BTNTAKA.Text = "Taka"
                BTNCUT.Visible = False
                GCUT.Visible = False
                BTNMTRS.Left = 507
                BTNNARR.Left = 587
                GTAKA.ReadOnly = True
                GBALENO.ReadOnly = True
                fillname(CMBDELIVERYFROM, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'PROCESSOR'")
                CMDSELECTBALESTOCK.Text = "Bale S&tock"
            Else
                BTNQUALITY.Text = "Merchant Name"
                BTNTAKA.Text = "Pcs"
                BTNCUT.Visible = True
                GCUT.Visible = True
                BTNMTRS.Left = 557
                BTNNARR.Left = 637
                GTAKA.ReadOnly = False
                GBALENO.ReadOnly = False
                fillname(CMBDELIVERYFROM, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'JOBBER'")
                CMDSELECTBALESTOCK.Text = "Saree S&tock"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTYPE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTYPE.Validated
        Try
            SETGRID()
            If CMBTYPE.Text <> "" Then CMBTYPE.Enabled = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            If MsgBox("Wish to Print Challan?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim OBJCHALLAN As New ChallanDesign
            OBJCHALLAN.WHERECLAUSE = "{CHALLANFINISHMASTER.CHALLANFINISH_NO} = " & TEMPCHALLANNO & " AND {CHALLANFINISHMASTER.CHALLANFINISH_YEARID} = " & YearId
            OBJCHALLAN.FRMSTRING = "CHALLANFINISH"
            OBJCHALLAN.MdiParent = MDIMain
            OBJCHALLAN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validated(sender As Object, e As EventArgs) Handles cmbname.Validated
        Try
            If CMBSHIPTO.Text.Trim = "" Then CMBSHIPTO.Text = cmbname.Text.Trim
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHIPTO_Enter(sender As Object, e As EventArgs) Handles CMBSHIPTO.Enter
        Try
            If CMBSHIPTO.Text.Trim = "" Then fillname(CMBSHIPTO, False, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHIPTO_Validating(sender As Object, e As CancelEventArgs) Handles CMBSHIPTO.Validating
        Try
            If CMBSHIPTO.Text.Trim <> "" Then namevalidate(CMBSHIPTO, cmbcode, e, Me, TXTADD, "AND  (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Challan_Finished_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "SASHWINKUMAR" Then
                LBLSHIPTO.Visible = False
                CMBSHIPTO.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class