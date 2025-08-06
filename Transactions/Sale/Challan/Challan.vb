
Imports System.ComponentModel
Imports BL

Public Class Challan

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPCHALLANNO As Integer
    Dim DTPCS As New DataTable
    Dim ALLOWMANUALCHALLANNO As Boolean
    Dim DTWHATSAPP As New DataTable

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        cmbname.Focus()
    End Sub

    Sub HIDEVIEW()
        Try
            If CMBTYPE.Text = "GREY" Then

                CMBOURGODOWN.TabStop = False
                TBITEMDETAILS.Width = 933
                GRIDCHALLAN.Width = 913
                BTNBALENO.Visible = True
                GBALENO.Visible = True
                BTNTAKA.Text = "Taka"
                BTNTAKA.Left = BTNBALENO.Left + BTNBALENO.Width
                TXTTAKA.Left = TXTBALENO.Left + TXTBALENO.Width
                BTNMTRS.Text = "Mtrs"
                BTNMTRS.Left = BTNTAKA.Left + BTNTAKA.Width
                TXTMTRS.Left = TXTTAKA.Left + TXTTAKA.Width
                BTNTP.Visible = True
                GTP.Visible = True
                BTNLRNO.Visible = False
                TXTLRNO.Visible = False
                GLRNO.Visible = False
                BTNLRDATE.Visible = False
                LRDATE.Visible = False
                GLRDATE.Visible = False
                BTNMILLNAME.Visible = False
                CMBMILLNAME.Visible = False
                GMILLNAME.Visible = False
                BTNNARR.Left = BTNTP.Left + BTNTP.Width
                TXTNARRATION.Left = TXTTP.Left + TXTTP.Width

                CMDSELECTSTOCK.Visible = False
                CMDPENDING.Visible = True
                GRIDCHALLAN.Top = CMBQUALITY.Top + CMBQUALITY.Height
                GTAKA.ReadOnly = True
                If ClientName <> "HARIA" Then GMTRS.ReadOnly = True Else GMTRS.ReadOnly = False
                GRIDCHALLAN.Height = 162


                If MULTIYARN = False Then
                    TXTSRNO.Visible = True
                    CMBQUALITY.Visible = True
                    TXTBALENO.Visible = True
                    TXTTAKA.Visible = True
                    TXTMTRS.Visible = True
                    TXTTP.Visible = True
                    TXTNARRATION.Visible = True
                Else
                    GRIDCHALLAN.Top = BTNBALENO.Top + BTNBALENO.Height
                    GRIDCHALLAN.Height = GRIDCHALLAN.Height + CMBQUALITY.Height
                    BTNBALENO.Text = "Taka No"
                    CMDPENDING.Visible = False
                    CMDSELECTSTOCK.Visible = True
                End If

            Else
                CMBOURGODOWN.TabStop = True
                TBITEMDETAILS.Width = 1183
                GRIDCHALLAN.Width = 1163
                BTNBALENO.Visible = False
                TXTBALENO.Visible = False
                GBALENO.Visible = False
                BTNTAKA.Text = "Bags"
                BTNTAKA.Left = BTNBALENO.Left
                TXTTAKA.Left = TXTBALENO.Left
                BTNMTRS.Text = "Wt"
                BTNMTRS.Left = BTNTAKA.Left + BTNTAKA.Width
                TXTMTRS.Left = TXTTAKA.Left + TXTTAKA.Width
                BTNTP.Visible = False
                TXTTP.Visible = False
                GTP.Visible = False
                BTNLRNO.Visible = True
                BTNLRNO.Left = BTNMTRS.Left + BTNMTRS.Width
                TXTLRNO.Visible = True
                TXTLRNO.Left = TXTMTRS.Left + TXTMTRS.Width
                GLRNO.Visible = True
                BTNLRDATE.Visible = True
                BTNLRDATE.Left = BTNLRNO.Left + BTNLRNO.Width
                LRDATE.Visible = True
                LRDATE.Left = TXTLRNO.Left + TXTLRNO.Width
                GLRDATE.Visible = True
                BTNMILLNAME.Visible = True
                CMBMILLNAME.Visible = True
                GMILLNAME.Visible = True
                BTNNARR.Left = BTNMILLNAME.Left + BTNMILLNAME.Width
                TXTNARRATION.Left = CMBMILLNAME.Left + CMBMILLNAME.Width
                TXTSRNO.Visible = False
                CMBQUALITY.Visible = False
                TXTBALENO.Visible = False
                TXTTAKA.Visible = False
                TXTMTRS.Visible = False
                TXTTP.Visible = False
                TXTLRNO.Visible = False
                LRDATE.Visible = False
                CMBMILLNAME.Visible = False
                TXTNARRATION.Visible = False
                CMDSELECTSTOCK.Visible = True
                CMDPENDING.Visible = False
                GRIDCHALLAN.Top = CMBQUALITY.Top
                GTAKA.ReadOnly = False
                GMTRS.ReadOnly = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        DTPCS.Rows.Clear()



        CMBTYPE.Enabled = True
        HIDEVIEW()

        TXTCHALLANNO.Clear()
        If ALLOWMANUALCHALLANNO = True Then
            TXTCHALLANNO.ReadOnly = False
            TXTCHALLANNO.BackColor = Color.LemonChiffon
        End If

        DTCHALLANDATE.Text = Mydate
        CMBOURGODOWN.Text = GETDEFAULTGODOWN()
        cmbname.Text = ""
        cmbname.Enabled = True
        CMBBROKER.Text = ""

        cmbtrans.Text = ""
        TXTVEHICLENO.Clear()
        TXTEWBNO.Clear()
        TXTFOLD.Text = 100
        CMBACOF.Text = ""
        TXTBALEFROM.Clear()
        TXTBALETO.Clear()
        CMBDELIVERYAT.Text = ""
        TXTDISPLAYQUALITY.Clear()

        TXTORDERNO.Clear()
        TXTORDERSRNO.Clear()
        TXTLOTNO.Clear()


        txtremarks.Clear()
        CHKNOGR.CheckState = CheckState.Unchecked
        TXTGRDONO.Clear()
        TXTGRPCS.Clear()
        TXTGRMTRS.Clear()
        TXTSHORTAGE.Clear()

        CMBQUALITY.Text = ""
        TXTBALENO.Clear()
        TXTMTRS.Clear()
        TXTTAKA.Clear()
        TXTTP.Clear()
        TXTLRNO.Clear()
        LRDATE.Value = Mydate
        CMBMILLNAME.Text = ""
        TXTSTOCKMTRS.Clear()
        TXTSTOCKTAKA.Clear()
        TXTNARRATION.Clear()
        tstxtbillno.Clear()


        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        txtremarks.Clear()

        GRIDCHALLAN.RowCount = 0

        GETMAX_CHALLAN_NO()

        LBLTOTALBALES.Text = 0
        LBLTOTALTAKA.Text = 0
        LBLTOTALMTRS.Text = 0.0
        LBLTOTALTP.Text = 0

        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        CHKDYEING.CheckState = CheckState.Unchecked
        CHKDYEING.Enabled = True

        CMDSELECTSTOCK.Enabled = True
        CMDSELECTORDER.Enabled = True

        TBITEMDETAILS.SelectedIndex = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Text = 1
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0

        TXTAVGMTRS.Clear()
        TXTQUALITYAVG.Clear()
        TXTAVGDIFF.Clear()
        LBLWHATSAPP.Visible = False

        TXTSRNO.Text = 1

    End Sub

    Sub TOTAL()
        Try
            LBLTOTALBALES.Text = 0
            LBLTOTALTAKA.Text = 0
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALTP.Text = 0
            TXTAVGMTRS.Clear()
            TXTQUALITYAVG.Clear()
            TXTAVGDIFF.Clear()

            For Each ROW As DataGridViewRow In GRIDCHALLAN.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALTAKA.Text = Format(Val(LBLTOTALTAKA.Text) + Val(ROW.Cells(GTAKA.Index).EditedFormattedValue), "0")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    If Val(ROW.Cells(GBALENO.Index).Value) > 0 Then LBLTOTALBALES.Text = Val(LBLTOTALBALES.Text) + 1
                    LBLTOTALTP.Text = Format(Val(LBLTOTALTP.Text) + Val(ROW.Cells(GTP.Index).EditedFormattedValue), "0")
                End If
            Next


            If GRIDCHALLAN.Rows.Count > 0 And CMBTYPE.Text.Trim = "GREY" Then
                TXTAVGMTRS.Text = Format(Val(LBLTOTALMTRS.Text.Trim) / Val(LBLTOTALTAKA.Text.Trim), "0.00")
                Dim OBJCMN As New ClsCommon
                Dim DTMTRS As DataTable = OBJCMN.search("ISNULL(GREY_MTRS,0) AS AVGMTRS", "", " GREYQUALITYMASTER ", " AND GREY_NAME = '" & GRIDCHALLAN.Rows(0).Cells(GQUALITY.Index).Value & "' AND GREY_YEARID = " & YearId)
                If DTMTRS.Rows.Count > 0 Then TXTQUALITYAVG.Text = Val(DTMTRS.Rows(0).Item("AVGMTRS"))
                TXTAVGDIFF.Text = Format(Val(TXTAVGMTRS.Text.Trim) - Val(TXTQUALITYAVG.Text.Trim), "0.00")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAX_CHALLAN_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(CHALLAN_NO),0)+1", "CHALLANMASTER", "AND CHALLAN_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTCHALLANNO.Text = DTTABLE.Rows(0).Item(0)
        GETMAXSERIES(TXTSERIES)
    End Sub

    Private Sub Challan_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                TBITEMDETAILS.SelectedIndex = (0)
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2) Then       'for CLEAR
                TBITEMDETAILS.SelectedIndex = (1)
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
        If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR='TRUE'")
        If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE ='ACCOUNTS'")
        If CMBACOF.Text.Trim = "" Then fillname(CMBACOF, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE ='ACCOUNTS'")
        If cmbtrans.Text = "" Then fillname(cmbtrans, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        If CMBBROKER.Text.Trim = "" Then fillname(CMBBROKER, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'AGENTS'")
        If CMBDELIVERYAT.Text.Trim = "" Then fillname(CMBDELIVERYAT, EDIT, "and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors') AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'PROCESSOR' OR LEDGERS.ACC_SUBTYPE = 'ACCOUNTS')")
        If CMBMILLNAME.Text.Trim = "" Then fillname(CMBMILLNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'")

        'DO NOT FILL ALL QUALITY FILL ONLY THOSE WHICH ARE PRESENT IN GREYSTOCK
        'If CMBQUALITY.Text.Trim = "" Then FILLGREY(CMBQUALITY, EDIT)
        FILLSTOCKQUALITY()
    End Sub

    Sub FILLSTOCKQUALITY()
        Try
            If CMBQUALITY.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As New DataTable
                If CMBTYPE.Text = "GREY" Then
                    dt = OBJCMN.search(" EFFECTGREYQUALITY ", "", " GREYSTOCK ", " AND YEARID = " & YearId & " GROUP BY EFFECTGREYQUALITY HAVING SUM(MTRS) > 0")
                Else
                    dt = OBJCMN.search(" EFFECTQUALITY AS EFFECTGREYQUALITY ", "", " YARNSTOCK_ONHAND ", " AND YEARID = " & YearId & " GROUP BY EFFECTQUALITY HAVING SUM(WT) > 0")
                End If
                CMBQUALITY.DataSource = dt
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "EFFECTGREYQUALITY"
                    CMBQUALITY.DisplayMember = "EFFECTGREYQUALITY"
                    If EDIT = False Then CMBQUALITY.Text = ""
                End If
                CMBQUALITY.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Challan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GDN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            If ClientName = "HARIA" Then ALLOWMANUALCHALLANNO = True


            FILLCMB()
            CLEAR()

            If EDIT = True Then
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dttable As New DataTable
                Dim OBJCHALLAN As New ClsChallan

                OBJCHALLAN.alParaval.Add(TEMPCHALLANNO)
                OBJCHALLAN.alParaval.Add(YearId)
                dttable = OBJCHALLAN.SELECTCHALLAN()

                If dttable.Rows.Count > 0 Then
                    cmbname.Focus()

                    TXTSERIES.Text = Val(dttable.Rows(0).Item("SERIES"))
                    TXTCHALLANNO.Text = TEMPCHALLANNO
                    DTCHALLANDATE.Text = dttable.Rows(0).Item("CHALLANDATE")
                    CMBTYPE.Text = dttable.Rows(0).Item("TYPE")
                    CMBTYPE.Enabled = False
                    HIDEVIEW()
                    TXTORDERNO.Text = dttable.Rows(0).Item("ORDERNO")
                    TXTORDERSRNO.Text = dttable.Rows(0).Item("ORDERSRNO")
                    TXTORDERTYPE.Text = dttable.Rows(0).Item("ORDERTYPE")
                    TXTLOTNO.Text = dttable.Rows(0).Item("LOTNO")
                    CMBOURGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                    cmbname.Text = dttable.Rows(0).Item("NAME").ToString
                    CMBBROKER.Text = dttable.Rows(0).Item("BROKER").ToString
                    cmbtrans.Text = dttable.Rows(0).Item("TRANSPORT").ToString
                    TXTVEHICLENO.Text = dttable.Rows(0).Item("VEHICLENO").ToString
                    TXTEWBNO.Text = dttable.Rows(0).Item("EWBNO").ToString
                    TXTFOLD.Text = dttable.Rows(0).Item("FOLD").ToString
                    CMBACOF.Text = dttable.Rows(0).Item("ACOF").ToString
                    TXTBALEFROM.Text = Val(dttable.Rows(0).Item("BALEFROM"))
                    TXTBALETO.Text = Val(dttable.Rows(0).Item("BALETO"))
                    TXTDISPLAYQUALITY.Text = dttable.Rows(0).Item("DISPLAYQUALITYNAME").ToString
                    CMBDELIVERYAT.Text = dttable.Rows(0).Item("DELIVERYAT").ToString
                    txtremarks.Text = dttable.Rows(0).Item("REMARKS").ToString

                    If Convert.ToBoolean(dttable.Rows(0).Item("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True

                    If Convert.ToBoolean(dttable.Rows(0).Item("FORDYEING")) = False Then CHKDYEING.Checked = False Else CHKDYEING.Checked = True
                    If Convert.ToBoolean(dttable.Rows(0).Item("CUTPACK")) = False Then CHKCUTPACK.Checked = False Else CHKCUTPACK.Checked = True
                    CHKDYEING.Enabled = False
                    If ClientName = "NIRMALA" Then cmbname.Enabled = False

                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDCHALLAN.Rows.Add(Val(ROW("SRNO")), ROW("QUALITY").ToString, ROW("BALENO"), Val(ROW("TAKA")), Format(Val(ROW("MTRS")), "0.00"), Val(ROW("TP")), ROW("LRNO"), Format(Convert.ToDateTime(ROW("LRDATE")).Date, "dd/MM/yyyy"), ROW("MILLNAME"), ROW("NARR"), Val(ROW("FROMNO")), Val(ROW("FROMSRNO")), ROW("GRIDTYPE"))
                    Next

                    If Convert.ToBoolean(dttable.Rows(0).Item("DONE")) = True Then
                        lbllocked.Visible = True
                        PBlock.Visible = True
                    End If

                    'CMDSELECTORDER.Enabled = False

                    If Convert.ToBoolean(dttable.Rows(0).Item("NOGR")) = False Then CHKNOGR.Checked = False Else CHKNOGR.Checked = True
                    TXTGRDONO.Text = dttable.Rows(0).Item("GRDONO")
                    TXTGRPCS.Text = Val(dttable.Rows(0).Item("GRPCS"))
                    TXTGRMTRS.Text = Val(dttable.Rows(0).Item("GRMTRS"))
                    TXTSHORTAGE.Text = Val(dttable.Rows(0).Item("SHORTAGE"))


                    'UPLOAD(GRID)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" CHALLANMASTER_UPLOAD.CHALLAN_SRNO AS GRIDSRNO, CHALLANMASTER_UPLOAD.CHALLAN_REMARKS AS REMARKS, CHALLANMASTER_UPLOAD.CHALLAN_NAME AS NAME, CHALLANMASTER_UPLOAD.CHALLAN_PHOTO AS IMGPATH ", "", " CHALLANMASTER_UPLOAD ", " AND CHALLANMASTER_UPLOAD.CHALLAN_NO = " & TEMPCHALLANNO & " AND CHALLAN_YEARID = " & YearId & " ORDER BY CHALLANMASTER_UPLOAD.CHALLAN_SRNO")
                    If DT.Rows.Count > 0 Then
                        For Each DTR As DataRow In DT.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If

                    DTPCS = OBJCMN.search("ISNULL(CHALLANMASTER_MTRS.CHALLAN_GRIDSRNO, 0) AS SRNO, ISNULL(CHALLANMASTER_MTRS.CHALLAN_MTRS, 0) AS MTRS, ISNULL(CHALLANMASTER_MTRS.CHALLAN_TP, 0) AS TP, ISNULL(CHALLANMASTER_MTRS.CHALLAN_MAINLINENO, 0) AS MAINLINENO", "", "CHALLANMASTER INNER JOIN CHALLANMASTER_MTRS ON CHALLANMASTER.CHALLAN_NO = CHALLANMASTER_MTRS.CHALLAN_NO AND CHALLANMASTER.CHALLAN_YEARID = CHALLANMASTER_MTRS.CHALLAN_YEARID", "AND CHALLANMASTER_MTRS.CHALLAN_NO =" & TEMPCHALLANNO & "  AND CHALLANMASTER_MTRS.CHALLAN_YEARID = " & YearId & " ORDER BY CHALLANMASTER_MTRS.CHALLAN_GRIDSRNO ")

                    'CMDSELECTGREYSTOCK.Enabled = False
                    If GRIDCHALLAN.RowCount > 0 Then TXTSRNO.Text = Val(GRIDCHALLAN.Rows(GRIDCHALLAN.RowCount - 1).Cells(0).Value) + 1 Else TXTSRNO.Text = 1
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

            If TXTCHALLANNO.ReadOnly = False Then
                alParaval.Add(Val(TXTCHALLANNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If
            alParaval.Add(Format(Convert.ToDateTime(DTCHALLANDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBTYPE.Text.Trim)
            alParaval.Add(Val(TXTORDERNO.Text.Trim))
            alParaval.Add(Val(TXTORDERSRNO.Text.Trim))
            alParaval.Add(TXTORDERTYPE.Text.Trim)
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(CMBBROKER.Text.Trim)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(TXTVEHICLENO.Text.Trim)
            alParaval.Add(TXTEWBNO.Text.Trim)
            alParaval.Add(TXTFOLD.Text.Trim)
            alParaval.Add(CMBACOF.Text.Trim)
            alParaval.Add(Val(TXTBALEFROM.Text.Trim))
            alParaval.Add(Val(TXTBALETO.Text.Trim))
            alParaval.Add(TXTDISPLAYQUALITY.Text.Trim)
            alParaval.Add(CMBDELIVERYAT.Text.Trim)

            If CHKDYEING.CheckState = CheckState.Checked Then alParaval.Add(1) Else alParaval.Add(0)
            If CHKCUTPACK.CheckState = CheckState.Checked Then alParaval.Add(1) Else alParaval.Add(0)
            alParaval.Add(Val(TXTLOTNO.Text.Trim))

            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(LBLTOTALBALES.Text.Trim)
            alParaval.Add(LBLTOTALTAKA.Text.Trim)
            alParaval.Add(LBLTOTALMTRS.Text.Trim)
            alParaval.Add(LBLTOTALTP.Text.Trim)

            If CHKNOGR.CheckState = CheckState.Checked Then alParaval.Add(1) Else alParaval.Add(0)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim SRNO As String = ""
            Dim QUALITY As String = ""
            Dim BALENO As String = ""
            Dim TAKA As String = ""
            Dim MTRS As String = ""
            Dim TP As String = ""
            Dim LRNO As String = ""
            Dim LRDATE As String = ""
            Dim MILLNAME As String = ""
            Dim NARR As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim GRIDTYPE As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDCHALLAN.Rows
                If row.Cells(GSRNO.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = Val(row.Cells(GSRNO.Index).Value)
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        BALENO = row.Cells(GBALENO.Index).Value
                        TAKA = Format(Val(row.Cells(GTAKA.Index).Value), "0")
                        MTRS = Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        TP = Format(Val(row.Cells(GTP.Index).Value), "0")
                        LRNO = row.Cells(GLRNO.Index).Value.ToString
                        LRDATE = Format(Convert.ToDateTime(row.Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy")
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        If row.Cells(GNARR.Index).Value <> Nothing Then NARR = row.Cells(GNARR.Index).Value.ToString Else NARR = ""
                        FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)
                        GRIDTYPE = row.Cells(GGRIDTYPE.Index).Value.ToString
                    Else
                        SRNO = SRNO & "|" & row.Cells(GSRNO.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value
                        TAKA = TAKA & "|" & Format(Val(row.Cells(GTAKA.Index).Value), "0")
                        MTRS = MTRS & "|" & Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        TP = TP & "|" & Format(Val(row.Cells(GTP.Index).Value), "0")
                        LRNO = LRNO & "|" & row.Cells(GLRNO.Index).Value.ToString
                        LRDATE = LRDATE & "|" & Format(Convert.ToDateTime(row.Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy")
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        If row.Cells(GNARR.Index).Value <> Nothing Then NARR = NARR & "|" & row.Cells(GNARR.Index).Value.ToString Else NARR = NARR & "|" & ""
                        FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)
                        GRIDTYPE = GRIDTYPE & "|" & row.Cells(GGRIDTYPE.Index).Value.ToString
                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(QUALITY)
            alParaval.Add(BALENO)
            alParaval.Add(TAKA)
            alParaval.Add(MTRS)
            alParaval.Add(TP)
            alParaval.Add(LRNO)
            alParaval.Add(LRDATE)
            alParaval.Add(MILLNAME)
            alParaval.Add(NARR)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(GRIDTYPE)

            Dim GRIDMTRSSRNO As String = ""
            Dim GRIDMTRS As String = ""
            Dim GRIDTP As String = ""
            Dim GRIDMAINNO As String = ""
            For Each DTROW As DataRow In DTPCS.Rows
                If GRIDMTRS = "" Then
                    GRIDMTRSSRNO = Val(DTROW("SRNO"))
                    GRIDMTRS = Val(DTROW("MTRS"))
                    GRIDTP = Val(DTROW("TP"))
                    GRIDMAINNO = Val(DTROW("MAINLINENO"))
                Else
                    GRIDMTRSSRNO = GRIDMTRSSRNO & "|" & Val(DTROW("SRNO"))
                    GRIDMTRS = GRIDMTRS & "|" & Val(DTROW("MTRS"))
                    GRIDTP = GRIDTP & "|" & Val(DTROW("TP"))
                    GRIDMAINNO = GRIDMAINNO & "|" & DTROW("MAINLINENO")
                End If
            Next

            alParaval.Add(GRIDMTRSSRNO)
            alParaval.Add(GRIDMTRS)
            alParaval.Add(GRIDTP)
            alParaval.Add(GRIDMAINNO)

            Dim OBJCHALLAN As New ClsChallan
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


            'IF INVOICE IS CREATED THEN AUTO SAVE THE INVOICE
            'FOR THIS WE NEED CHALLANNO FIRST AND WITH RESPECT TO THAT CHALLAN WE WILL UPDATE INVOICE
            If ClientName = "MASHOK" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("INVOICE_NO AS INVNO, REGISTER_NAME AS REGNAME", "", "INVOICEMASTER_DESC INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID ", " AND INVOICE_FROMNO = " & Val(TEMPCHALLANNO) & " AND INVOICE_GRIDTYPE = 'CHALLAN' AND INVOICE_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    Dim OBJINV As New InvoiceMaster
                    OBJINV.edit = True
                    OBJINV.TEMPINVOICENO = Val(DT.Rows(0).Item("INVNO"))
                    OBJINV.TEMPREGNAME = DT.Rows(0).Item("REGNAME")
                    OBJINV.AUTOSAVEFROMGR = True
                    OBJINV.Show()
                End If
            End If

            PRINTREPORT()

            If gridupload.RowCount > 0 Then SAVEUPLOAD()
            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTCHALLANDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            If MsgBox("Wish to Print Challan?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim OBJCHALLAN As New ChallanDesign
            OBJCHALLAN.WHERECLAUSE = "{CHALLANMASTER.CHALLAN_NO} = " & TEMPCHALLANNO & " AND {CHALLANMASTER.CHALLAN_YEARID} = " & YearId
            OBJCHALLAN.FRMSTRING = "CHALLAN"
            OBJCHALLAN.SCREENTYPE = CMBTYPE.Text
            OBJCHALLAN.MdiParent = MDIMain
            OBJCHALLAN.CHALLANNO = TEMPCHALLANNO
            OBJCHALLAN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJBEAMISSUE As New ClsChallan

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


        If DTCHALLANDATE.Text = "__/__/____" Then
            EP.SetError(DTCHALLANDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTCHALLANDATE.Text) Then
                EP.SetError(DTCHALLANDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If CMBTYPE.Text.Trim = "GREY" Then
            If ClientName <> "HARIA" And CHKDYEING.CheckState = CheckState.Unchecked Then
                If Val(TXTORDERNO.Text.Trim) = 0 Then
                    EP.SetError(TXTORDERNO, "Select Order")
                    bln = False
                End If
            End If
        End If

        If CHKDYEING.CheckState = CheckState.Checked And CMBDELIVERYAT.Text.Trim = "" Then
            EP.SetError(CMBDELIVERYAT, "Select Dyeing House")
            bln = False
        End If

        If cmbname.Text.Trim.Length = 0 Then
            EP.SetError(cmbname, "Please Fill Name")
            bln = False
        End If

        If CMBOURGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBOURGODOWN, " Please Fill Our Godown Name ")
            bln = False
        End If

        'If ClientName <> "JASHOK" Then
        '    If cmbtrans.Text.Trim.Length = 0 Then
        '        EP.SetError(cmbtrans, " Please Select Transport")
        '        bln = False
        '    End If
        'End If

        'If ClientName <> "NIRMALA" And ClientName <> "MASHOK" Then
        '    If Val(LBLTOTALTAKA.Text) > 200 Then
        '        EP.SetError(LBLTOTALTAKA, "Taka Cannot be Greater than 200")
        '        bln = False
        '    End If
        'End If

        'DONE TEMPORARILY
        'If lbllocked.Visible = True Then
        '    EP.SetError(lbllocked, "Unable to Modify, Entry Locked")
        '    bln = False
        'End If

        If GRIDCHALLAN.RowCount = 0 Then
            EP.SetError(cmbtrans, "Select Stock")
            bln = False
        End If

        For Each row As DataGridViewRow In GRIDCHALLAN.Rows
            If Val(row.Cells(GMTRS.Index).Value) = 0 Then
                EP.SetError(cmbtrans, "Mtrs Cannot be 0")
                bln = False
            End If

            If Val(row.Cells(GTAKA.Index).Value) = 0 And CMBTYPE.Text.Trim <> "YARN" Then
                EP.SetError(cmbtrans, "Pcs Cannot be 0")
                bln = False
            End If

            'STOCK VALIDATE FROM GREYSTOCK
            Dim BALPCS As Double = 0
            Dim BALMTRS As Double = 0
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If CMBTYPE.Text.Trim = "GREY" Then
                DT = OBJCMN.search("ISNULL(SUM(PCS),0) AS PCS, ISNULL(SUM(MTRS),0) AS MTRS", "", "GREYSTOCK", " AND GODOWN = '" & CMBOURGODOWN.Text.Trim & "' AND EFFECTGREYQUALITY = '" & row.Cells(GQUALITY.Index).Value & "' AND YEARID = " & YearId)
            Else
                'WE HAVE CHANGED THE CODE COZ WE HAVE REMOVED OURGODOWN STOCK FROM STOCKVIEW 
                'WE HAVE MISTAKENLY ADDED OURSTOCK QUERIES IN STOCKVIEW, STOCKVIEW IS JUST FOR WAREHOUSE STOCK
                'SO HERE WE WILL FETCH DATA FROMM STOCKVIEW AND STOCKVIEW_OURGODOWN
                'DT = OBJCMN.search("ROUND(ISNULL(BAGS,0),2) AS PCS,ROUND(ISNULL(WT,0),2) as MTRS", "", " STOCKVIEW", " AND GODOWN='" & CMBOURGODOWN.Text.Trim & "' AND NO= " & Val(row.Cells(GFROMNO.Index).Value) & " AND SRNO= " & Val(row.Cells(GFROMSRNO.Index).Value) & " AND GRIDTYPE='" & row.Cells(GGRIDTYPE.Index).Value & "' AND Yearid = " & YearId)
                DT = OBJCMN.search("*", "", "(SELECT ROUND(ISNULL(BAGS,0),2) AS PCS,ROUND(ISNULL(WT,0),2) as MTRS FROM STOCKVIEW WHERE GODOWN='" & CMBOURGODOWN.Text.Trim & "' AND NO= " & Val(row.Cells(GFROMNO.Index).Value) & " AND SRNO= " & Val(row.Cells(GFROMSRNO.Index).Value) & " AND GRIDTYPE='" & row.Cells(GGRIDTYPE.Index).Value & "' AND Yearid = " & YearId & " UNION ALL SELECT ROUND(ISNULL(BAGS,0),2) AS PCS,ROUND(ISNULL(WT,0),2) as MTRS FROM STOCKVIEW_OURGODOWN WHERE GODOWN='" & CMBOURGODOWN.Text.Trim & "' AND NO= " & Val(row.Cells(GFROMNO.Index).Value) & " AND SRNO= " & Val(row.Cells(GFROMSRNO.Index).Value) & " AND TYPE='" & row.Cells(GGRIDTYPE.Index).Value & "' AND YEARID = " & YearId & ") AS T", "")
            End If
            If DT.Rows.Count > 0 Then
                BALPCS = Val(DT.Rows(0).Item("PCS"))
                BALMTRS = Val(DT.Rows(0).Item("MTRS"))
            End If
            If EDIT = True Then
                If CMBTYPE.Text.Trim = "GREY" Then
                    DT = OBJCMN.search("ISNULL(SUM(CHALLAN_TAKA),0) AS PCS, ISNULL(SUM(CHALLAN_MTRS),0) AS MTRS", "", "CHALLANMASTER_DESC INNER JOIN GREYQUALITYMASTER ON GREY_ID  = CHALLAN_QUALITYID  ", " AND GREY_NAME = '" & row.Cells(GQUALITY.Index).Value & "' AND CHALLAN_NO = " & TEMPCHALLANNO & " AND CHALLAN_YEARID = " & YearId)
                Else
                    DT = OBJCMN.search("ISNULL(SUM(CHALLAN_TAKA),0) AS PCS, ISNULL(SUM(CHALLAN_MTRS),0) AS MTRS", "", "CHALLANMASTER_DESC INNER JOIN QUALITYMASTER ON QUALITY_ID  = CHALLAN_QUALITYID  ", " AND QUALITY_NAME = '" & row.Cells(GQUALITY.Index).Value & "' AND CHALLAN_NO = " & TEMPCHALLANNO & " AND CHALLAN_YEARID = " & YearId)
                End If
                If DT.Rows.Count > 0 Then
                    BALPCS = BALPCS + Val(DT.Rows(0).Item("PCS"))
                    BALMTRS = BALMTRS + Val(DT.Rows(0).Item("MTRS"))
                End If
            End If
            row.DefaultCellStyle.BackColor = Color.Empty


            If Val(row.Cells(GTAKA.Index).Value) > BALPCS Then
                EP.SetError(TXTSTOCKMTRS, "Stock Not Present, Only " & Val(BALPCS) & " allowed")
                bln = False
                row.DefaultCellStyle.BackColor = Color.Yellow
            End If
            If ClientName <> "YESHA" Then
                If Val(row.Cells(GMTRS.Index).Value) > BALMTRS Then
                    EP.SetError(TXTSTOCKMTRS, "Stock Not Present, Only " & Val(BALMTRS) & " allowed")
                    bln = False
                    row.DefaultCellStyle.BackColor = Color.Yellow
                End If
            End If
        Next


        If ALLOWMANUALCHALLANNO = True Then
            If Val(TXTCHALLANNO.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(CHALLANMASTER.CHALLAN_NO,0)  AS GDNNO", "", " CHALLANMASTER ", "  AND CHALLANMASTER.CHALLAN_NO=" & TXTCHALLANNO.Text.Trim & " AND CHALLANMASTER.CHALLAN_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    EP.SetError(TXTCHALLANNO, "Challan No Already Exists")
                    bln = False
                End If
            End If
        End If


        Return bln
    End Function

    Private Sub DTCHALLANDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTCHALLANDATE.GotFocus
        DTCHALLANDATE.SelectAll()
    End Sub

    Private Sub DTCHALLANDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTCHALLANDATE.Validating
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
            Dim WHERECLAUSE As String = ""
            If CMBTYPE.Text.Trim = "GREY" Then WHERECLAUSE = " AND GODOWN_ISOUR='TRUE'" Else WHERECLAUSE = ""
            If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, WHERECLAUSE)
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
                Dim WHERECLAUSE As String = ""
                If CMBTYPE.Text.Trim = "GREY" Then WHERECLAUSE = " AND GODOWN_ISOUR='TRUE'" Else WHERECLAUSE = ""
                OBJGODOWN.SEARCH = WHERECLAUSE
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBOURGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOURGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBOURGODOWN.Validating
        Try
            Dim WHERECLAUSE As String = ""
            If CMBTYPE.Text.Trim = "GREY" Then WHERECLAUSE = " AND GODOWN_ISOUR='TRUE'" Else WHERECLAUSE = ""
            If CMBOURGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBOURGODOWN, e, Me, WHERECLAUSE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'ACCOUNTS' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE='ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'", "SUNDRY DEBTORS", "ACCOUNTS", "", CMBBROKER.Text)
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
                Dim DT As DataTable = OBJCMN.search(" CHALLAN_NO ", "", "  CHALLANMASTER", " AND CHALLAN_NO = " & TEMPCHALLANNO & " AND CHALLANMASTER.CHALLAN_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    Challan_Load(sender, e)
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
                Challan_Load(sender, e)
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
                    Challan_Load(sender, e)
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
                    alParaval.Add(TEMPCHALLANNO)
                    alParaval.Add(YearId)

                    Dim OBJSO As New ClsChallan()
                    OBJSO.alParaval = alParaval
                    IntResult = OBJSO.Delete()
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

    Private Sub CMDSELECTGREYSTOCK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDSELECTSTOCK.Click
        Try

            If CMBOURGODOWN.Text.Trim = "" Then
                MsgBox("Please Select Godown First")
                CMBOURGODOWN.Focus()
                Exit Sub
            End If

            If DTCHALLANDATE.Text = "__/__/____" Then
                MsgBox("Please Select Date First")
                DTCHALLANDATE.Focus()
                Exit Sub
            End If

            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor

            Dim DTTABLE As New DataTable
            If MULTIYARN = False Or (MULTIYARN = True And CMBTYPE.Text = "YARN") Then
                Dim OBJSELECTYARN As New SelectGRNforDO
                OBJSELECTYARN.GODOWN = CMBOURGODOWN.Text.Trim
                OBJSELECTYARN.DODATE = Convert.ToDateTime(DTCHALLANDATE.Text).Date
                OBJSELECTYARN.ShowDialog()

                DTTABLE = OBJSELECTYARN.DT1

                Dim i As Integer = 0
                If DTTABLE.Rows.Count > 0 Then
                    For Each dr As DataRow In DTTABLE.Rows

                        'PARSNG LRDATE
                        Dim TEMPLRDATE As Date = Now.Date
                        'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                        Dim TEMP As DateTime
                        If Not DateTime.TryParse(dr("LRDATE"), TEMP) Then TEMPLRDATE = Convert.ToDateTime(DTCHALLANDATE.Text).Date Else TEMPLRDATE = Convert.ToDateTime(dr("LRDATE")).Date

                        GRIDCHALLAN.Rows.Add(0, dr("QUALITY"), "", Format(Val(dr("BAGS")), "0"), Format(Val(dr("WT")), "0.00"), 0, dr("LRNO"), Format(TEMPLRDATE, "dd/MM/yyyy"), dr("MILLNAME"), "", Val(dr("NO")), Val(dr("SRNO")), dr("GRIDTYPE"))
                    Next
                    GRIDCHALLAN.FirstDisplayedScrollingRowIndex = GRIDCHALLAN.RowCount - 1
                    getsrno(GRIDCHALLAN)
                End If

            Else
                Dim OBJSELECTTAKANO As New SelectTakaNoStock
                OBJSELECTTAKANO.GODOWN = CMBOURGODOWN.Text.Trim
                OBJSELECTTAKANO.DODATE = Convert.ToDateTime(DTCHALLANDATE.Text).Date
                OBJSELECTTAKANO.ShowDialog()

                DTTABLE = OBJSELECTTAKANO.DT1

                Dim i As Integer = 0
                If DTTABLE.Rows.Count > 0 Then
                    For Each dr As DataRow In DTTABLE.Rows
                        GRIDCHALLAN.Rows.Add(0, dr("QUALITY"), dr("TAKANO"), Format(Val(dr("PCS")), "0"), Format(Val(dr("MTRS")), "0.00"), 0, "", Format(LRDATE.Value.Date, "dd/MM/yyyy"), "", "", Val(dr("NO")), Val(dr("SRNO")), dr("GRIDTYPE"))
                    Next
                    GRIDCHALLAN.FirstDisplayedScrollingRowIndex = GRIDCHALLAN.RowCount - 1
                    getsrno(GRIDCHALLAN)
                End If
            End If
            TOTAL()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
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


            ElseIf e.KeyCode = Keys.F8 Then
                'FILLPCSDETAILS()
                If CMBTYPE.Text.Trim = "GREY" Then
                    Dim OBJPCS As New PcsWiseDetails
                    OBJPCS.DT = DTPCS
                    OBJPCS.FROMNO = Val(TXTCHALLANNO.Text.Trim)
                    OBJPCS.MAINLINENO = GRIDCHALLAN.CurrentRow.Cells(GSRNO.Index).Value
                    OBJPCS.ShowDialog()

                    ''GET TOTAL MTRS AND ADD IN GRID
                    If DTPCS.Rows.Count > 0 Then
                        GRIDCHALLAN.Rows(GRIDCHALLAN.CurrentRow.Index).Cells(GMTRS.Index).Value = 0
                        GRIDCHALLAN.Rows(GRIDCHALLAN.CurrentRow.Index).Cells(GTP.Index).Value = 0
                        For Each DTROW As DataRow In DTPCS.Rows
                            If DTROW("MAINLINENO") = GRIDCHALLAN.CurrentRow.Cells(GSRNO.Index).Value Then
                                Dim RESULT() As DataRow = DTPCS.Select("MAINLINENO = " & GRIDCHALLAN.CurrentRow.Cells(GSRNO.Index).Value)
                                GRIDCHALLAN.Rows(GRIDCHALLAN.CurrentRow.Index).Cells(GMTRS.Index).Value = Format(Val(GRIDCHALLAN.Rows(GRIDCHALLAN.CurrentRow.Index).Cells(GMTRS.Index).Value) + Val(DTROW("MTRS")), "0.000")
                                GRIDCHALLAN.Rows(GRIDCHALLAN.CurrentRow.Index).Cells(GTAKA.Index).Value = RESULT.Length
                                GRIDCHALLAN.Rows(GRIDCHALLAN.CurrentRow.Index).Cells(GTP.Index).Value = Format(Val(GRIDCHALLAN.Rows(GRIDCHALLAN.CurrentRow.Index).Cells(GTP.Index).Value) + Val(DTROW("TP")), "0")
                            End If
                        Next
                        getsrno(GRIDCHALLAN)
                        TOTAL()
                    End If
                End If
            End If


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLPCSDETAILS()

        Dim OBJPCS As New PcsWiseDetails
        OBJPCS.DT = DTPCS
        OBJPCS.MAINLINENO = Val(TXTSRNO.Text.Trim)
        OBJPCS.ShowDialog()


        ''GET TOTAL MTRS AND ADD IN GRID
        If DTPCS.Rows.Count > 0 Then
            TXTMTRS.Text = 0.0
            TXTTP.Text = 0
            For Each DTROW As DataRow In DTPCS.Rows
                If DTROW("MAINLINENO") = Val(TXTSRNO.Text.Trim) Then
                    Dim RESULT() As DataRow = DTPCS.Select("MAINLINENO = " & Val(TXTSRNO.Text.Trim))
                    TXTMTRS.Text = Format(Val(TXTMTRS.Text) + Val(DTROW("MTRS")), "0.000")
                    TXTTP.Text = Format(Val(TXTTP.Text) + Val(DTROW("TP")), "0")
                    TXTTAKA.Text = RESULT.Length
                End If
            Next
            getsrno(GRIDCHALLAN)
            TOTAL()
        End If
        'DTPCS.Rows.Clear()
    End Sub

    Private Sub GRIDCHALLAN_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDCHALLAN.CellValidating
        Try
            Dim colNum As Integer = GRIDCHALLAN.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GMTRS.Index, GTAKA.Index ', gcount.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDCHALLAN.CurrentCell.Value = Nothing Then GRIDCHALLAN.CurrentCell.Value = "0.000"
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
            Dim OBJCHALLAN As New ChallanDetails
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
            OBJSELECTSO.TYPE = CMBTYPE.Text.Trim
            OBJSELECTSO.ShowDialog()
            Dim DTORDER As DataTable = OBJSELECTSO.DT
            If DTORDER.Rows.Count > 0 Then

                If CMBBROKER.Text.Trim = "" Then CMBBROKER.Text = DTORDER.Rows(0).Item("AGENT")
                TXTORDERNO.Text = DTORDER.Rows(0).Item("ORDERNO")
                TXTORDERSRNO.Text = DTORDER.Rows(0).Item("ORDERSRNO")
                TXTORDERTYPE.Text = DTORDER.Rows(0).Item("ORDERTYPE")
                If CMBDELIVERYAT.Text.Trim = "" Then CMBDELIVERYAT.Text = DTORDER.Rows(0).Item("DELIVERYAT")
                CMBQUALITY.Text = DTORDER.Rows(0).Item("QUALITY")
                If cmbtrans.Text.Trim = "" Then cmbtrans.Text = DTORDER.Rows(0).Item("TRANSPORT")
                CMDSELECTORDER.Enabled = False

                CMBQUALITY.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDELIVERYAT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBDELIVERYAT.Enter
        Try
            If CMBDELIVERYAT.Text.Trim = "" Then fillname(cmbname, EDIT, "and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors') AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'PROCESSOR' OR LEDGERS.ACC_SUBTYPE = 'ACCOUNTS')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDELIVERYAT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBDELIVERYAT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors') AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'PROCESSOR' OR LEDGERS.ACC_SUBTYPE = 'ACCOUNTS')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBDELIVERYAT.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDELIVERYAT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDELIVERYAT.Validating
        Try
            If CMBDELIVERYAT.Text.Trim <> "" Then namevalidate(CMBDELIVERYAT, cmbcode, e, Me, TXTADD, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors') AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'PROCESSOR' OR LEDGERS.ACC_SUBTYPE = 'ACCOUNTS')", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTTAKA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTAKA.KeyPress, TXTTP.KeyPress, TXTBALENO.KeyPress, TXTBALEFROM.KeyPress, TXTBALETO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress
        numdotkeypress(e, TXTMTRS, Me)
    End Sub

    Private Sub TXTNARRATION_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNARRATION.Validating
        Try
            If CMBQUALITY.Text <> "" And Val(TXTTAKA.Text) > 0 And Val(TXTMTRS.Text) > 0 Then FILLGRID() Else MsgBox("Enter Proper Details !")
            getsrno(GRIDCHALLAN)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()

        GRIDCHALLAN.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDCHALLAN.Rows.Add(Val(TXTSRNO.Text.Trim), CMBQUALITY.Text.Trim, TXTBALENO.Text.Trim, Format(Val(TXTTAKA.Text.Trim), "0"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Val(TXTTP.Text.Trim), TXTLRNO.Text.Trim, Format(LRDATE.Value.Date, "dd/MM/yyyy"), CMBMILLNAME.Text.Trim, TXTNARRATION.Text.Trim, 0, 0, "")
            getsrno(GRIDCHALLAN)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDCHALLAN.Item(GSRNO.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDCHALLAN.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDCHALLAN.Item(GBALENO.Index, TEMPROW).Value = Format(Val(TXTBALENO.Text.Trim), "0")
            GRIDCHALLAN.Item(GTAKA.Index, TEMPROW).Value = Format(Val(TXTTAKA.Text.Trim), "0")
            GRIDCHALLAN.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDCHALLAN.Item(GTP.Index, TEMPROW).Value = Format(Val(TXTTP.Text.Trim), "0")
            GRIDCHALLAN.Item(GLRNO.Index, TEMPROW).Value = TXTLRNO.Text.Trim
            GRIDCHALLAN.Item(GLRDATE.Index, TEMPROW).Value = Format(LRDATE.Value.Date, "dd/MM/yyyy")
            GRIDCHALLAN.Item(GMILLNAME.Index, TEMPROW).Value = CMBMILLNAME.Text.Trim
            GRIDCHALLAN.Item(GNARR.Index, TEMPROW).Value = TXTNARRATION.Text.Trim
            GRIDDOUBLECLICK = False
        End If
        TOTAL()
        GRIDCHALLAN.FirstDisplayedScrollingRowIndex = GRIDCHALLAN.RowCount - 1

        TXTSRNO.Clear()
        CMBQUALITY.Text = ""
        TXTBALENO.Clear()
        TXTTAKA.Clear()
        TXTMTRS.Clear()
        TXTTP.Clear()
        TXTLRNO.Clear()
        LRDATE.Value = Mydate
        CMBMILLNAME.Text = ""
        TXTSTOCKTAKA.Clear()
        TXTSTOCKMTRS.Clear()
        TXTNARRATION.Clear()

        If GRIDCHALLAN.RowCount > 0 Then TXTSRNO.Text = Val(GRIDCHALLAN.Rows(GRIDCHALLAN.RowCount - 1).Cells(0).Value) + 1 Else TXTSRNO.Text = 1
        CMBQUALITY.Focus()

    End Sub

    Private Sub GRIDCHALLAN_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDCHALLAN.CellDoubleClick
        If e.RowIndex >= 0 And GRIDCHALLAN.Item(GSRNO.Index, e.RowIndex).Value <> Nothing Then

            'If Val(GRIDCHALLAN.Rows(e.RowIndex).Cells(GOUTPCS.Index).Value) > 0 Or Val(GRIDSO.Rows(e.RowIndex).Cells(GOUTMTRS.Index).Value) > 0 Then
            '    MsgBox("Item Locked", MsgBoxStyle.Critical)
            '    Exit Sub
            'End If
            If CMBTYPE.Text.Trim = "GREY" Then
                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDCHALLAN.Item(GSRNO.Index, e.RowIndex).Value.ToString
                CMBQUALITY.Text = GRIDCHALLAN.Item(GQUALITY.Index, e.RowIndex).Value.ToString
                TXTBALENO.Text = Val(GRIDCHALLAN.Item(GBALENO.Index, e.RowIndex).Value)
                TXTTAKA.Text = Val(GRIDCHALLAN.Item(GTAKA.Index, e.RowIndex).Value)
                TXTMTRS.Text = Val(GRIDCHALLAN.Item(GMTRS.Index, e.RowIndex).Value)
                TXTTP.Text = Val(GRIDCHALLAN.Item(GTP.Index, e.RowIndex).Value)
                TXTNARRATION.Text = GRIDCHALLAN.Item(GNARR.Index, e.RowIndex).Value.ToString

                TEMPROW = e.RowIndex
                CMBQUALITY.Focus()
            End If
        End If
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then FILLSTOCKQUALITY()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Validated
        Try
            TXTSTOCKTAKA.Clear()
            TXTSTOCKMTRS.Clear()

            If CMBQUALITY.Text.Trim <> "" And CMBTYPE.Text.Trim = "GREY" Then
                'GET GREYQUALITY STOCK
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(SUM(PCS),0) AS PCS, ISNULL(SUM(MTRS),0) AS MTRS ", "", " GREYSTOCK ", " AND GODOWN = '" & CMBOURGODOWN.Text.Trim & "' AND EFFECTGREYQUALITY = '" & CMBQUALITY.Text.Trim & "' AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TXTSTOCKTAKA.Text = Val(DT.Rows(0).Item("PCS"))
                    TXTSTOCKMTRS.Text = Format(Val(DT.Rows(0).Item("MTRS")), "0.00")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then
                If CMBTYPE.Text = "YARN" Then
                    QUALITYVALIDATE(CMBQUALITY, e, Me)
                Else
                    GREYVALIDATE(CMBQUALITY, e, Me, " AND GREY_TYPE = '" & CMBTYPE.Text.Trim & "'")
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTSRNO_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTSRNO.GotFocus
        If GRIDDOUBLECLICK = False Then
            If GRIDCHALLAN.RowCount > 0 Then
                TXTSRNO.Text = Val(GRIDCHALLAN.Rows(GRIDCHALLAN.RowCount - 1).Cells(0).Value) + 1
            Else
                TXTSRNO.Text = 1
            End If
        End If
    End Sub

    Private Sub TXTTAKA_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTTAKA.Enter
        Try

            If GRIDDOUBLECLICK = False Then TXTSRNO.Text = GRIDCHALLAN.RowCount + 1

            If ClientName = "MASHOK" Then Exit Sub
            If CMBQUALITY.Text.Trim <> "" And CMBTYPE.Text.Trim = "GREY" Then
                'FILLPCSDETAILS()
                Dim OBJPCS As New PcsWiseDetails
                OBJPCS.DT = DTPCS
                OBJPCS.FROMNO = Val(TXTCHALLANNO.Text.Trim)
                OBJPCS.MAINLINENO = TXTSRNO.Text.Trim
                OBJPCS.ShowDialog()

                ''GET TOTAL MTRS AND ADD IN GRID
                If DTPCS.Rows.Count > 0 Then
                    TXTMTRS.Clear()
                    TXTTP.Clear()
                    For Each DTROW As DataRow In DTPCS.Rows
                        If DTROW("MAINLINENO") = Val(TXTSRNO.Text) Then
                            Dim RESULT() As DataRow = DTPCS.Select("MAINLINENO = " & TXTSRNO.Text.Trim)
                            TXTMTRS.Text = Format(Val(TXTMTRS.Text.Trim) + Val(DTROW("MTRS")), "0.000")
                            TXTTAKA.Text = RESULT.Length
                            TXTTP.Text = Format(Val(TXTTP.Text.Trim) + Val(DTROW("TP")), "0")
                        End If
                    Next
                    If Val(TXTTAKA.Text.Trim) > 0 And Val(TXTMTRS.Text.Trim) > 0 Then FILLGRID()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Challan_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "SASHWINKUMAR" Then cmbtrans.BackColor = Color.LemonChiffon

            If ClientName = "MASHOK" Then
                TXTTAKA.ReadOnly = False
                TXTTAKA.BackColor = Color.LemonChiffon
                TXTTAKA.TabStop = True
                TXTMTRS.ReadOnly = False
                TXTMTRS.BackColor = Color.LemonChiffon
                TXTMTRS.TabStop = True
                TXTLOTNO.ReadOnly = False
                TXTLOTNO.BackColor = Color.White
                TXTLOTNO.TabStop = True
            End If
            If EDIT = False Then CMBTYPE.SelectedIndex = 0


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPENDING_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPENDING.Click
        Try
            Dim OBJLOT As New PendingLotno
            OBJLOT.MdiParent = MDIMain
            OBJLOT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBALENO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TXTBALENO.KeyDown
        If e.KeyCode = Keys.F1 Then

            If CMBQUALITY.Text.Trim <> "" And GRIDDOUBLECLICK = False And CMBTYPE.Text.Trim = "GREY" Then
                TXTSRNO.Text = GRIDCHALLAN.RowCount + 1

                'FILLPCSDETAILS()
                Dim OBJSELECTPS As New SelectPS
                OBJSELECTPS.DT = DTPCS
                OBJSELECTPS.FROMNO = Val(TXTCHALLANNO.Text.Trim)
                OBJSELECTPS.MAINLINENO = TXTSRNO.Text.Trim
                OBJSELECTPS.QUALITY = CMBQUALITY.Text.Trim
                Dim BALEDT As DataTable = OBJSELECTPS.DTBALE
                OBJSELECTPS.ShowDialog()

                ''GET TOTAL MTRS AND ADD IN GRID
                If BALEDT.Rows.Count > 0 Then
                    For Each BALEDTROW As DataRow In BALEDT.Rows
                        CMBQUALITY.Text = BALEDTROW("QUALITY")
                        TXTBALENO.Text = Val(BALEDTROW("PSNO"))
                        TXTTAKA.Text = Val(BALEDTROW("TOTALTAKA"))
                        TXTMTRS.Text = Val(BALEDTROW("TOTALMTRS"))
                        TXTTP.Text = Val(BALEDTROW("TOTALTP"))
                        If Val(TXTTAKA.Text.Trim) > 0 And Val(TXTMTRS.Text.Trim) > 0 Then FILLGRID()
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub CMBTYPE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTYPE.Validated
        HIDEVIEW()
        CMBTYPE.Enabled = False
        If CMBTYPE.Text = "YARN" Then
            CMBOURGODOWN.Text = ""
            fillGODOWN(CMBOURGODOWN, False, "")
        End If
    End Sub

    Private Sub CMBACOF_Enter(sender As Object, e As EventArgs) Handles CMBACOF.Enter
        Try
            If CMBACOF.Text.Trim = "" Then fillname(CMBACOF, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'ACCOUNTS' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBACOF_Validating(sender As Object, e As CancelEventArgs) Handles CMBACOF.Validating
        Try
            If CMBACOF.Text.Trim <> "" Then namevalidate(CMBACOF, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'", "SUNDRY DEBTORS", "ACCOUNTS", "", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHALLANNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTCHALLANNO.Validating
        Try
            If Val(TXTCHALLANNO.Text.Trim) <> 0 And EDIT = False And ALLOWMANUALCHALLANNO = True Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(CHALLANMASTER.CHALLAN_NO,0)  AS GDNNO", "", " CHALLANMASTER ", "  AND CHALLANMASTER.CHALLAN_NO=" & TXTCHALLANNO.Text.Trim & " AND CHALLANMASTER.CHALLAN_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Challan No Already Exist")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHALLANNO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTCHALLANNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If EDIT = True Then SENDWHATSAPP(TEMPCHALLANNO)
            DT = OBJCMN.Execute_Any_String("UPDATE CHALLANMASTER SET CHALLAN_SENDWHATSAPP = 1 WHERE challan_no = " & TEMPCHALLANNO & " AND challan_YEARID = " & YearId, "", "")
            LBLWHATSAPP.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(CHALLANNO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJCN As New ChallanDesign
            OBJCN.MdiParent = MDIMain
            OBJCN.FRMSTRING = "CHALLAN"
            OBJCN.DIRECTMAIL = False
            OBJCN.DIRECTPRINT = True
            OBJCN.DIRECTWHATSAPP = True
            OBJCN.REGNAME = CMBTYPE.Text.Trim
            OBJCN.PARTYNAME = cmbname.Text.Trim
            OBJCN.CHALLANNO = Val(CHALLANNO)
            OBJCN.NOOFCOPIES = 1
            OBJCN.Show()
            OBJCN.Close()


            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = cmbname.Text.Trim
            OBJWHATSAPP.AGENTNAME = CMBBROKER.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & cmbname.Text.Trim & "_CHALLAN_NO-" & Val(CHALLANNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add(cmbname.Text.Trim & "challan_" & Val(CHALLANNO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class