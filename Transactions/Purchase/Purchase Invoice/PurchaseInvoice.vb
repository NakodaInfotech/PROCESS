
Imports BL
Imports System.Windows.Forms
Imports System.IO
Imports System.Net

Public Class PurchaseMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDCHGSDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPCHGSROW, TEMPUPLOADROW, PURREGID As Integer
    Dim TEMPPARTYBILLNO As String
    Public EDIT As Boolean
    Public TEMPBILLNO, TEMPREGNAME, PURTYPE, SELECTEDREG As String
    Dim PURREGABBR, PURREGINITIAL As String
    Public Shared selectGRNtable As New DataTable
    Dim TEMPFORM As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        EDIT = False
        CMBTYPE.Focus()
    End Sub

    Sub clear()

        BILLDATE.Text = Mydate
        tstxtbillno.Clear()
        CMBTYPE.Enabled = True

        TXTSACCODE.Clear()
        CHKRCM.CheckState = CheckState.Unchecked
        TXTSTATECODE.Clear()
        TXTGSTIN.Clear()
        LBLWHATSAPP.Visible = False

        cmbname.Text = ""
        cmbname.Enabled = True
        CMBTRANSPORT.Text = ""
        CMBCODE.Text = ""
        TXTPARTYBILLNO.Clear()
        DTPARTYBILLDATE.Text = Mydate

        CMBAGENT.Text = ""
        TXTCHALLANNO.Clear()
        CHALLANDATE.Value = Mydate
        TXTREFNO.Clear()

        TXTCRDAYS.Clear()
        DUEDATE.Value = Mydate
        TXTLOTNO.Clear()
        TXTLOTNO.Enabled = True
        FROMDATE.Clear()
        FROMDATE.Enabled = True
        TILLDATE.Clear()
        TILLDATE.Enabled = True

        For I As Integer = 0 To CHKFORMBOX.Items.Count - 1
            Dim DTR As DataRowView = CHKFORMBOX.Items(I)
            CHKFORMBOX.SetItemCheckState(I, CheckState.Unchecked)
        Next

        CHKFORMBOX.SetItemChecked(CHKFORMBOX.FindStringExact("GST"), True)

        CMDSELECTDATA.Enabled = True
        cmbname.Enabled = True

        txtaddlrno.Clear()
        lradddate.Value = Mydate
        TXTVEHICLENO.Clear()
        CMBFROMCITY.Text = ""
        CMBTOCITY.Text = ""
        txtadd.Clear()
        CMBMILL.Text = ""


        TXTVEHICLENO.Clear()
        txtaddlrno.Clear()
        TXTEWAYBILLNO.Clear()


        CHKBILLCHECKED.Checked = False
        CHKBILLDISPUTE.Checked = False
        CHKTDS.Checked = False


        EP.Clear()
        PBDN.Visible = False
        PBPAID.Visible = False
        PBTDS.Visible = False
        lbllocked.Visible = False
        PBlock.Visible = False
        CMDSHOWDETAILS.Visible = False

        txtremarks.Clear()
        txtinwords.Clear()
        TXTSRNO.Text = 1
        CMBQUALITY.Text = ""
        txtbags.Clear()
        TXTWT.Clear()
        TXTRATE.Clear()
        CMBPER.Text = ""
        TXTAMT.Clear()
        GRIDBILL.RowCount = 0
        CHKSPLIT.CheckState = CheckState.Unchecked
        gbags.ReadOnly = True
        gwt.ReadOnly = True

        TXTCHGSSRNO.Text = 1
        CMBCHARGES.Text = ""
        TXTCHGSPER.Clear()
        TXTCHGSAMT.Clear()
        GRIDCHGS.RowCount = 0

        CHKMANUAL.CheckState = CheckState.Unchecked
        CHKPENDINGREPORTS.CheckState = CheckState.Checked
        TXTCGSTPER.Text = 0
        TXTSGSTPER.Text = 0
        TXTIGSTPER.Text = 0
        TXTCGSTAMT.Text = 0
        TXTSGSTAMT.Text = 0
        TXTIGSTAMT.Text = 0

        CHKMANUALTCS.Checked = False
        CHKTCS.Checked = False
        TXTTOTALWITHGST.Clear()
        TXTTCSPER.Clear()
        TXTTCSAMT.Clear()

        txtuploadsrno.Text = 1
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        txtimgpath.Clear()
        TXTFILENAME.Clear()
        TXTNEWIMGPATH.Clear()
        PBSoftCopy.ImageLocation = ""
        gridupload.RowCount = 0

        getmax_BILL_no()

        GRIDDOUBLECLICK = False
        GRIDCHGSDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        TXTBILLAMT.Text = 0.0

        TXTTOTALTAXAMT.Clear()
        TXTTOTALOTHERCHGSAMT.Clear()

        TXTCHARGES.Text = 0.0
        TXTSUBTOTAL.Text = 0.0
        txtgrandtotal.Text = 0.0
        TXTBAL.Clear()
        txtroundoff.Text = 0.0
        txtremarks.Clear()

        PBSoftCopy.Image = Nothing
        txtuploadsrno.Text = 1
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        txtimgpath.Clear()
        gridupload.RowCount = 0
        GRIDUPLOADDOUBLECLICK = False

        lbltotalbags.Text = 0
        LBLTOTALWT.Text = 0.0
        lbltotalamt.Text = 0.0

        TBITEMDETAILS.SelectedIndex = 0
        HIDEVIEW()

    End Sub

    Sub FILLUPLOAD()

        If GRIDUPLOADDOUBLECLICK = False Then
            gridupload.Rows.Add(Val(txtuploadsrno.Text.Trim), txtuploadremarks.Text.Trim, txtuploadname.Text.Trim, PBSoftCopy.Image)
            getsrno(gridupload)
        ElseIf GRIDUPLOADDOUBLECLICK = True Then

            gridupload.Item(GUSRNO.Index, TEMPUPLOADROW).Value = txtuploadsrno.Text.Trim
            gridupload.Item(GUREMARKS.Index, TEMPUPLOADROW).Value = txtuploadremarks.Text.Trim
            gridupload.Item(GUNAME.Index, TEMPUPLOADROW).Value = txtuploadname.Text.Trim
            gridupload.Item(GUIMGPATH.Index, TEMPUPLOADROW).Value = PBSoftCopy.Image

            GRIDUPLOADDOUBLECLICK = False

        End If
        gridupload.FirstDisplayedScrollingRowIndex = gridupload.RowCount - 1

        txtuploadsrno.Text = gridupload.RowCount + 1
        txtuploadremarks.Clear()
        txtuploadname.Clear()
        PBSoftCopy.Image = Nothing
        txtimgpath.Clear()

        txtuploadremarks.Focus()

    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJBILL As New ClsPurchaseMaster

            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPBILLNO)
                    ALPARAVAL.Add(TEMPREGNAME)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSoftCopy.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSoftCopy.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJBILL.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJBILL.SAVEUPLOAD()
                End If
            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getmax_BILL_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(BILL_NO),0) + 1 ", "  PURCHASEMaster INNER JOIN REGISTERMASTER ON REGISTER_ID = BILL_REGISTERID ", " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND REGISTER_TYPE = 'PURCHASE' AND BILL_YEARID = " & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTBILLNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub PurchaseMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D3) Then       'for CLEAR
                TBITEMDETAILS.SelectedIndex = (2)
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call toolprevious_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call toolnext_Click(sender, e)
            ElseIf e.KeyCode = Windows.Forms.Keys.F3 Then
                CMBCHARGES.Focus()
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call PrintToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Windows.Forms.Keys.F5 Then       'for grid foucs
                GRIDBILL.Focus()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Sub FILLCMB()
        fillregister(cmbregister, " and register_type = 'PURCHASE'")
        If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        If CMBTRANSPORT.Text.Trim = "" Then fillname(CMBTRANSPORT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        If CMBCHARGES.Text.Trim = "" Then fillname(CMBCHARGES, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' or GROUPMASTER.GROUP_SECONDARY = 'Sale A/C' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' )")
        If CMBAGENT.Text.Trim = "" Then fillname(CMBAGENT, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'")

        If CMBSACDESC.Text.Trim = "" Then FILLSACCODE(CMBSACDESC, EDIT)

        fillCITY(CMBFROMCITY, EDIT)
        fillCITY(CMBTOCITY, EDIT)
        fillname(CMBMILL, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
        fillper(CMBPER, EDIT)
        fillform(CHKFORMBOX, EDIT)
    End Sub

    Private Sub PurchaseMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'PURCHASE INVOICE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            clear()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dt As New DataTable
                Dim ALPARAVAL As New ArrayList
                Dim objclsINV As New ClsPurchaseMaster

                ALPARAVAL.Add(TEMPBILLNO)
                ALPARAVAL.Add(TEMPREGNAME)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)

                objclsINV.alParaval = ALPARAVAL
                dt = objclsINV.SELECTPURCHASE()

                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        CMBTYPE.Text = dr("PURTYPE")
                        CMBTYPE.Enabled = False
                        PURTYPE = dr("PURTYPE")
                        HIDEVIEW()

                        TXTBILLNO.Text = TEMPBILLNO
                        CMBSACDESC.Text = dr("SACDESC")
                        CHKRCM.Checked = Convert.ToBoolean(dr("RCM"))

                        cmbregister.Text = Convert.ToString(dr("REGNAME"))
                        cmbname.Text = Convert.ToString(dr("NAME"))
                        TXTSTATECODE.Text = dr("STATECODE")
                        TXTGSTIN.Text = dr("GSTIN")



                        BILLDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        TXTPARTYBILLNO.Text = Convert.ToString(dr("PARTYBILLNO"))
                        TEMPPARTYBILLNO = Convert.ToString(dr("PARTYBILLNO"))

                        DTPARTYBILLDATE.Text = Format(Convert.ToDateTime(dr("PARTYBILLDATE")).Date, "dd/MM/yyyy")
                        CMBMILL.Text = Convert.ToString(dr("MILLNAME"))
                        CMBAGENT.Text = Convert.ToString(dr("AGENT"))
                        TXTCHALLANNO.Text = Convert.ToString(dr("CHALLANNO"))
                        CHALLANDATE.Value = Format(Convert.ToDateTime(dr("CHALLANDATE")).Date, "dd/MM/yyyy")
                        TXTREFNO.Text = Convert.ToString(dr("REFNO"))
                        TXTCRDAYS.Text = Val(dr("CRDAYS"))
                        DUEDATE.Value = Convert.ToDateTime(dr("DUEDATE"))
                        If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True
                        TXTLOTNO.Text = Val(dr("LOTNO"))
                        If dr("FROMDATE") <> "01/01/1900" Then FROMDATE.Text = Format(Convert.ToDateTime(dr("FROMDATE")).Date, "dd/MM/yyyy")
                        If dr("TILLDATE") <> "01/01/1900" Then TILLDATE.Text = Format(Convert.ToDateTime(dr("TILLDATE")).Date, "dd/MM/yyyy")

                        TXTVEHICLENO.Text = dr("VEHICLENO")
                        CMBFROMCITY.Text = dr("FROMCITY")
                        CMBTOCITY.Text = dr("TOCITY")
                        TXTEWAYBILLNO.Text = dr("EWAYBILLNO")
                        If dr("BILLCHECKED") = 0 Then
                            CHKBILLCHECKED.Checked = False
                        Else
                            CHKBILLCHECKED.Checked = True
                        End If
                        If dr("BILLDISPUTE") = 0 Then
                            CHKBILLDISPUTE.Checked = False
                        Else
                            CHKBILLDISPUTE.Checked = True
                        End If
                        txtremarks.Text = Convert.ToString(dr("REMARKS"))

                        TXTBILLAMT.Text = dr("BILLAMT")
                        TXTCHARGES.Text = dr("CHARGES")

                        CHKMANUAL.Checked = Convert.ToBoolean(dr("MANUALGST"))
                        If Convert.ToBoolean(dr("PENREPORT")) = True Then CHKPENDINGREPORTS.Checked = True Else CHKPENDINGREPORTS.Checked = False


                        TXTCGSTPER.Text = Val(dr("CGSTPER"))
                        TXTCGSTAMT.Text = Val(dr("CGSTAMT"))
                        TXTSGSTPER.Text = Val(dr("SGSTPER"))
                        TXTSGSTAMT.Text = Val(dr("SGSTAMT"))
                        TXTIGSTPER.Text = Val(dr("IGSTPER"))
                        TXTIGSTAMT.Text = Val(dr("IGSTAMT"))

                        If dr("MANUALTCS") = 0 Then CHKMANUALTCS.Checked = False Else CHKMANUALTCS.Checked = True
                        If dr("APPLYTCS") = 0 Then CHKTCS.Checked = False Else CHKTCS.Checked = True
                        TXTTOTALWITHGST.Text = Val(dr("TOTALWITHGST"))
                        TXTTCSPER.Text = Val(dr("TCSPER"))
                        TXTTCSAMT.Text = Val(dr("TCSAMT"))

                        txtroundoff.Text = dr("ROUNDOFF")
                        txtgrandtotal.Text = dr("GRANDTOTAL")
                        TXTFREIGHT.Text = Val(dr("FREIGHT"))

                        TXTAMTPAID.Text = dr("AMTPAID")
                        TXTEXTRAAMT.Text = dr("EXTRAAMT")
                        TXTRETURN.Text = dr("BILLRETURN")
                        TXTBAL.Text = dr("BALANCE")

                        'Item Grid
                        If Val(dr("GRIDSRNO")) > 0 Then GRIDBILL.Rows.Add(dr("GRIDSRNO").ToString, dr("QUALITY").ToString, Val(dr("BAGS")), Val(dr("WT")), Format(Val(dr("RATE")), "0.0000"), Format(Val(dr("NETTRATE")), "0.000"), dr("PER"), Format(Val(dr("AMT")), "0.00"), Convert.ToString(dr("TRANSNAME")), Convert.ToString(dr("LRNO")), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), Val(dr("BEAMS")), dr("NARR"), Val(dr("ENDS")), Val(dr("TL")), dr("HSNCODE"), dr("GRNNO"), dr("GRNGRIDSRNO"), dr("GRIDDONE"))

                        CMBTRANSPORT.Text = dr("TRANSNAME")

                        If Convert.ToBoolean(dr("GRIDDONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = False
                            GRIDBILL.Rows(GRIDBILL.RowCount - 1).DefaultCellStyle.BackColor = Drawing.Color.Yellow
                            cmbname.Enabled = False
                        End If

                        If Convert.ToBoolean(dr("PRDONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = False
                            cmbname.Enabled = False
                        End If


                        TBITEMDETAILS.SelectedIndex = (0)

                        'CHECKING WHETHER TDS IS DEDUCTED OR NOT
                        Dim OBJCMNTDS As New ClsCommon
                        Dim DTTDS As DataTable = OBJCMNTDS.search(" ISNULL(JOURNALMASTER.journal_credit,0) AS TDS", "", " JOURNALMASTER INNER JOIN PURCHASEMASTER ON JOURNALMASTER.journal_refno = PURCHASEMASTER.BILL_INITIALS AND  JOURNALMASTER.journal_yearid = PURCHASEMASTER.BILL_YEARID INNER JOIN LEDGERS ON JOURNALMASTER.journal_ledgerid = LEDGERS.Acc_id AND JOURNALMASTER.journal_yearid = LEDGERS.Acc_yearid INNER JOIN REGISTERMASTER ON PURCHASEMASTER.BILL_REGISTERID = REGISTERMASTER.register_id AND PURCHASEMASTER.BILL_YEARID = REGISTERMASTER.register_yearid", "AND (LEDGERS.ACC_TDSAC = 'True') AND BILL_NO = " & TEMPBILLNO & " AND REGISTER_NAME = '" & TEMPREGNAME & "' AND BILL_YEARID = " & YearId)
                        If DTTDS.Rows.Count > 0 Then
                            If Val(DTTDS.Rows(0).Item("TDS")) > 0 Then
                                CMDSHOWDETAILS.Visible = True
                                PBTDS.Visible = True
                                lbllocked.Visible = True
                                PBlock.Visible = True
                                cmbname.Enabled = False
                            End If
                        End If

                        If PBTDS.Visible = False Then
                            If Val(dr("AMTPAID")) > 0 Or Val(dr("EXTRAAMT")) > 0 Then
                                CMDSHOWDETAILS.Visible = True
                                PBPAID.Visible = True
                                lbllocked.Visible = True
                                PBlock.Visible = True
                                cmbname.Enabled = False
                            End If
                        End If

                        If Val(dr("BILLRETURN")) > 0 Then
                            CMDSHOWDETAILS.Visible = True
                            PBDN.Visible = True
                            lbllocked.Visible = True
                            PBlock.Visible = True
                            cmbname.Enabled = False
                        End If

                    Next

                    'CHARGES GRID
                    Dim OBJCMN As New ClsCommon
                    dt = OBJCMN.search(" PURCHASEMASTER_CHGS.BILL_gridsrno AS GRIDSRNO, ISNULL(LEDGERS.Acc_cmpname, '') AS CHARGES, ISNULL(PURCHASEMASTER_CHGS.BILL_PER, 0) AS PER, ISNULL(PURCHASEMASTER_CHGS.BILL_AMT, 0) AS AMT, ISNULL(TAXMASTER.TAX_ID, 0) AS TAXID ", "", " PURCHASEMASTER INNER JOIN REGISTERMASTER ON PURCHASEMASTER.BILL_REGISTERID = REGISTERMASTER.register_id AND PURCHASEMASTER.BILL_CMPID = REGISTERMASTER.register_cmpid AND PURCHASEMASTER.BILL_LOCATIONID = REGISTERMASTER.register_locationid AND PURCHASEMASTER.BILL_YEARID = REGISTERMASTER.register_yearid LEFT OUTER JOIN PURCHASEMASTER_CHGS LEFT OUTER JOIN TAXMASTER ON PURCHASEMASTER_CHGS.BILL_yearid = TAXMASTER.tax_yearid AND PURCHASEMASTER_CHGS.BILL_locationid = TAXMASTER.tax_locationid AND PURCHASEMASTER_CHGS.BILL_cmpid = TAXMASTER.tax_cmpid AND PURCHASEMASTER_CHGS.BILL_TAXID = TAXMASTER.tax_id ON PURCHASEMASTER.BILL_NO = PURCHASEMASTER_CHGS.BILL_no AND PURCHASEMASTER.BILL_REGISTERID = PURCHASEMASTER_CHGS.BILL_REGISTERID LEFT OUTER JOIN LEDGERS ON PURCHASEMASTER_CHGS.BILL_yearid = LEDGERS.Acc_yearid AND PURCHASEMASTER_CHGS.BILL_locationid = LEDGERS.Acc_locationid AND PURCHASEMASTER_CHGS.BILL_cmpid = LEDGERS.Acc_cmpid AND PURCHASEMASTER_CHGS.BILL_CHARGESID = LEDGERS.Acc_id", " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND REGISTER_TYPE = 'PURCHASE' AND PURCHASEMASTER_CHGS.BILL_NO = " & TEMPBILLNO & " AND PURCHASEMASTER_CHGS.BILL_CMPID = " & CmpId & " AND PURCHASEMASTER_CHGS.BILL_LOCATIONID = " & Locationid & " AND PURCHASEMASTER_CHGS.BILL_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        For Each DTR As DataRow In dt.Rows
                            GRIDCHGS.Rows.Add(DTR("GRIDSRNO"), DTR("CHARGES"), DTR("PER"), DTR("AMT"), DTR("TAXID"))
                        Next
                    End If


                    'UPLOAD(GRID)
                    dt = OBJCMN.search(" PURCHASEMASTER_UPLOAD.BILL_SRNO AS GRIDSRNO, PURCHASEMASTER_UPLOAD.BILL_REMARKS AS REMARKS, PURCHASEMASTER_UPLOAD.BILL_NAME AS NAME, PURCHASEMASTER_UPLOAD.BILL_PHOTO AS IMGPATH ", "", "PURCHASEMASTER_UPLOAD INNER JOIN REGISTERMASTER ON PURCHASEMASTER_UPLOAD.BILL_REGISTERID = REGISTERMASTER.register_id ", "AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND REGISTER_TYPE = 'PURCHASE' AND PURCHASEMASTER_UPLOAD.BILL_NO = " & TEMPBILLNO & " AND BILL_YEARID = " & YearId & " ORDER BY PURCHASEMASTER_UPLOAD.BILL_SRNO")
                    If dt.Rows.Count > 0 Then
                        For Each DTR As DataRow In dt.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If



                    dt = OBJCMN.search(" ISNULL(FORMTYPE.FORM_NAME, '') AS FORMNAME", "", "  PURCHASEMASTER_FORMTYPE INNER JOIN REGISTERMASTER ON PURCHASEMASTER_FORMTYPE.BILL_REGISTERID = REGISTERMASTER.register_id AND PURCHASEMASTER_FORMTYPE.BILL_YEARID = REGISTERMASTER.register_yearid LEFT OUTER JOIN FORMTYPE ON PURCHASEMASTER_FORMTYPE.BILL_FORMID = FORMTYPE.FORM_ID ", " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND REGISTERMASTER.REGISTER_TYPE = 'PURCHASE' AND PURCHASEMASTER_FORMTYPE.BILL_NO = " & TEMPBILLNO & " AND PURCHASEMASTER_FORMTYPE.BILL_YEARID= " & YearId)
                    If dt.Rows.Count > 0 Then
                        For Each ROW As DataRow In dt.Rows
                            For I As Integer = 0 To CHKFORMBOX.Items.Count - 1
                                Dim DTR As DataRowView = CHKFORMBOX.Items(I)
                                If ROW("FORMNAME") = DTR.Item(0) Then
                                    CHKFORMBOX.SetItemCheckState(I, CheckState.Checked)
                                End If
                            Next
                        Next
                    End If


                    dt = OBJCMN.search(" register_abbr, register_initials, register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'PURCHASE' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
                    If dt.Rows.Count > 0 Then
                        PURREGABBR = dt.Rows(0).Item(0).ToString
                        PURREGINITIAL = dt.Rows(0).Item(1).ToString
                        PURREGID = dt.Rows(0).Item(2)
                    End If
                    If GRIDBILL.RowCount > 0 Then GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1

                End If

                cmbregister.Enabled = False
                CMDSELECTDATA.Enabled = False
                TOTAL()
            Else
                EDIT = False
                clear()
            End If

            If ClientName = "MASHOK" Then
                CHKPENDINGREPORTS.Visible = True
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Dim IntResult As Integer
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(cmbregister.Text.Trim)
            alParaval.Add(TXTSACCODE.Text.Trim)
            If CHKRCM.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)

            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(BILLDATE.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(TXTPARTYBILLNO.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(DTPARTYBILLDATE.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(CMBMILL.Text.Trim)
            alParaval.Add(CMBAGENT.Text.Trim)
            alParaval.Add(TXTCHALLANNO.Text)
            alParaval.Add(CHALLANDATE.Value)
            alParaval.Add(TXTREFNO.Text)

            alParaval.Add(TXTCRDAYS.Text)
            alParaval.Add(DUEDATE.Value.Date)

            alParaval.Add(Val(TXTLOTNO.Text))
            If FROMDATE.Text <> "__/__/____" Then alParaval.Add(Format(Convert.ToDateTime(FROMDATE.Text).Date, "MM/dd/yyyy")) Else alParaval.Add("")
            If TILLDATE.Text <> "__/__/____" Then alParaval.Add(Format(Convert.ToDateTime(TILLDATE.Text).Date, "MM/dd/yyyy")) Else alParaval.Add("")

            alParaval.Add(TXTVEHICLENO.Text.Trim)
            alParaval.Add(CMBFROMCITY.Text.Trim)
            alParaval.Add(CMBTOCITY.Text.Trim)
            alParaval.Add(TXTEWAYBILLNO.Text.Trim)
            If CHKBILLCHECKED.Checked = True Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If

            If CHKBILLDISPUTE.Checked = True Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(Val(lbltotalbags.Text.Trim))
            alParaval.Add(Val(LBLTOTALWT.Text.Trim))
            alParaval.Add(Val(lbltotalamt.Text.Trim))
            alParaval.Add(txtinwords.Text)

            alParaval.Add(Val(TXTBILLAMT.Text.Trim))
            alParaval.Add(Format(Val(TXTCHARGES.Text.Trim), "0.00"))
            alParaval.Add(Format(Val(TXTSUBTOTAL.Text.Trim), "0.00"))
            If CHKMANUAL.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            If CHKPENDINGREPORTS.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            alParaval.Add(Val(TXTCGSTPER.Text.Trim))
            alParaval.Add(Val(TXTSGSTPER.Text.Trim))
            alParaval.Add(Val(TXTIGSTPER.Text.Trim))
            alParaval.Add(Val(TXTCGSTAMT.Text.Trim))
            alParaval.Add(Val(TXTSGSTAMT.Text.Trim))
            alParaval.Add(Val(TXTIGSTAMT.Text.Trim))
            alParaval.Add(Format(Val(TXTTOTALTAXAMT.Text.Trim), "0.00"))

            alParaval.Add(Val(TXTTOTALWITHGST.Text.Trim))
            If CHKMANUALTCS.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            If CHKTCS.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            alParaval.Add(Val(TXTTCSPER.Text.Trim))
            alParaval.Add(Val(TXTTCSAMT.Text.Trim))


            alParaval.Add(Format(Val(TXTTOTALOTHERCHGSAMT.Text.Trim), "0.00"))
            alParaval.Add(Format(Val(txtroundoff.Text.Trim), "0.00"))
            alParaval.Add(Format(Val(txtgrandtotal.Text.Trim), "0.00"))
            alParaval.Add(Val(TXTFREIGHT.Text.Trim))

            alParaval.Add(TXTAMTPAID.Text.Trim)
            alParaval.Add(TXTEXTRAAMT.Text.Trim)
            alParaval.Add(TXTRETURN.Text.Trim)
            alParaval.Add(TXTBAL.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            'ADDING FORMTYPE
            Dim FORMTYPE As String = ""
            For Each DTROW As DataRowView In CHKFORMBOX.CheckedItems
                If FORMTYPE = "" Then
                    FORMTYPE = DTROW.Item(0)
                Else
                    FORMTYPE = FORMTYPE & "|" & DTROW.Item(0)
                End If
            Next
            alParaval.Add(FORMTYPE)

            Dim gridsrno As String = ""
            Dim QUALITY As String = ""
            Dim BAGS As String = ""
            Dim WT As String = ""
            Dim RATE As String = ""         'value of RATE
            Dim NETTRATE As String = ""
            Dim PER As String = ""
            Dim AMT As String = ""
            Dim TRANSPORT As String = "" 'value of AMT
            Dim LRNO As String = ""
            Dim LRDATE As String = ""
            Dim BEAMS As String = ""
            Dim NARR As String = ""
            Dim ENDS As String = ""
            Dim TL As String = ""
            Dim HSNCODE As String = ""
            Dim GRNNO As String = ""        'WHETHER GRN IS DONE FOR THIS LINE
            Dim GRNGRIDSRNO As String = ""   'value of GRNGRIDSRNO
            Dim BILLDONE As String = ""      'WHETHER GRN IS DONE FOR THIS LINE

            For Each row As Windows.Forms.DataGridViewRow In GRIDBILL.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        BAGS = Val(row.Cells(gbags.Index).Value)
                        WT = Val(row.Cells(gwt.Index).Value)
                        RATE = Format(Val(row.Cells(GRATE.Index).Value), "0.0000")
                        NETTRATE = Format(Val(row.Cells(GNETTRATE.Index).Value), "0.000")
                        PER = row.Cells(GPER.Index).Value
                        If row.Cells(GAMT.Index).Value <> Nothing Then
                            AMT = Format(Val(row.Cells(GAMT.Index).Value), "0.00")
                        Else
                            AMT = "0"
                        End If
                        TRANSPORT = row.Cells(GTRANSPORT.Index).Value.ToString
                        If row.Cells(GLRNO.Index).Value = Nothing Then LRNO = "" Else LRNO = row.Cells(GLRNO.Index).Value.ToString
                        If row.Cells(GGRIDLRDATE.Index).Value = "" Then LRDATE = Format(Now.Date, "MM/dd/yyyy") Else LRDATE = Format(Convert.ToDateTime(row.Cells(GGRIDLRDATE.Index).Value).Date, "MM/dd/yyyy")
                        BEAMS = Val(row.Cells(GBEAMS.Index).Value)
                        If row.Cells(GNARRATION.Index).Value = Nothing Then NARR = "" Else NARR = row.Cells(GNARRATION.Index).Value.ToString
                        ENDS = Val(row.Cells(GENDS.Index).Value)
                        TL = Val(row.Cells(GTL.Index).Value)

                        HSNCODE = row.Cells(GHSNCODE.Index).Value
                        GRNNO = Val(row.Cells(GGRNNO.Index).Value)
                        If row.Cells(GGRNSRNO.Index).Value <> Nothing Then
                            GRNGRIDSRNO = Val(row.Cells(GGRNSRNO.Index).Value)
                        Else
                            GRNGRIDSRNO = 0
                        End If

                        If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then
                            BILLDONE = "1"
                        Else
                            BILLDONE = "0"
                        End If

                    Else

                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        BAGS = BAGS & "|" & Val(row.Cells(gbags.Index).Value)
                        WT = WT & "|" & Val(row.Cells(gwt.Index).Value)
                        RATE = RATE & "|" & Format(Val(row.Cells(GRATE.Index).Value), "0.0000")
                        NETTRATE = NETTRATE & "|" & Format(Val(row.Cells(GNETTRATE.Index).Value), "0.000")
                        PER = PER & "|" & row.Cells(GPER.Index).Value
                        If row.Cells(GAMT.Index).Value <> Nothing Then
                            AMT = AMT & "|" & Format(Val(row.Cells(GAMT.Index).Value), "0.00")
                        Else
                            AMT = AMT & "|" & "0"
                        End If
                        TRANSPORT = TRANSPORT & "|" & row.Cells(GTRANSPORT.Index).Value
                        If row.Cells(GLRNO.Index).Value = Nothing Then LRNO = LRNO & "|" & "" Else LRNO = LRNO & "|" & row.Cells(GLRNO.Index).Value
                        If row.Cells(GGRIDLRDATE.Index).Value = "" Then LRDATE = LRDATE & "|" & Format(Now.Date, "MM/dd/yyyy") Else LRDATE = LRDATE & "|" & Format(Convert.ToDateTime(row.Cells(GGRIDLRDATE.Index).Value).Date, "MM/dd/yyyy")

                        BEAMS = BEAMS & "|" & Val(row.Cells(GBEAMS.Index).Value)
                        If row.Cells(GNARRATION.Index).Value = Nothing Then NARR = NARR & "|" & "" Else NARR = NARR & "|" & row.Cells(GNARRATION.Index).Value.ToString
                        ENDS = ENDS & "|" & Val(row.Cells(GENDS.Index).Value)
                        TL = TL & "|" & Val(row.Cells(GTL.Index).Value)


                        HSNCODE = HSNCODE & "|" & row.Cells(GHSNCODE.Index).Value

                        GRNNO = GRNNO & "|" & Val(row.Cells(GGRNNO.Index).Value)
                        If row.Cells(GGRNSRNO.Index).Value <> Nothing Then
                            GRNGRIDSRNO = GRNGRIDSRNO & "|" & Val(row.Cells(GGRNSRNO.Index).Value)
                        Else
                            GRNGRIDSRNO = GRNGRIDSRNO & "|" & " 0"
                        End If

                        If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then
                            BILLDONE = BILLDONE & "|" & "1"
                        Else
                            BILLDONE = BILLDONE & "|" & "0"
                        End If

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(QUALITY)
            alParaval.Add(BAGS)
            alParaval.Add(WT)
            alParaval.Add(RATE)
            alParaval.Add(NETTRATE)
            alParaval.Add(PER)
            alParaval.Add(AMT)
            alParaval.Add(TRANSPORT)
            alParaval.Add(LRNO)
            alParaval.Add(LRDATE)
            alParaval.Add(BEAMS)
            alParaval.Add(NARR)
            alParaval.Add(ENDS)
            alParaval.Add(TL)


            alParaval.Add(HSNCODE)

            alParaval.Add(GRNNO)
            alParaval.Add(GRNGRIDSRNO)
            alParaval.Add(BILLDONE)


            Dim CSRNO As String = ""
            Dim CCHGS As String = ""
            Dim CPER As String = ""
            Dim CAMT As String = ""
            Dim CTAXID As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDCHGS.Rows
                If row.Cells(0).Value <> Nothing Then
                    If CSRNO = "" Then
                        CSRNO = Val(row.Cells(ESRNO.Index).Value)
                        CCHGS = row.Cells(ECHARGES.Index).Value.ToString
                        CPER = Val(row.Cells(EPER.Index).Value)
                        CAMT = Val(row.Cells(EAMT.Index).Value)
                        CTAXID = Val(row.Cells(ETAXID.Index).Value)

                    Else
                        CSRNO = CSRNO & "|" & Val(row.Cells(ESRNO.Index).Value)
                        CCHGS = CCHGS & "|" & row.Cells(ECHARGES.Index).Value.ToString
                        CPER = CPER & "|" & Val(row.Cells(EPER.Index).Value)
                        CAMT = CAMT & "|" & Val(row.Cells(EAMT.Index).Value)
                        CTAXID = CTAXID & "|" & Val(row.Cells(ETAXID.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(CSRNO)
            alParaval.Add(CCHGS)
            alParaval.Add(CPER)
            alParaval.Add(CAMT)
            alParaval.Add(CTAXID)

            alParaval.Add(PURTYPE)


            Dim OBJINV As New ClsPurchaseMaster
            OBJINV.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJINV.SAVE()
                TEMPBILLNO = DTTABLE.Rows(0).Item(0)
                MessageBox.Show("Details Added")

                If CHKTDS.CheckState = CheckState.Checked Then
                    Dim OBJTDS As New DeductTDS
                    OBJTDS.BILLNO = DTTABLE.Rows(0).Item(0)
                    OBJTDS.REGISTER = cmbregister.Text.Trim
                    OBJTDS.ShowDialog()
                End If

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPBILLNO)
                IntResult = OBJINV.UPDATE()
                MessageBox.Show("Details Updated")
                EDIT = False
            End If


            If gridupload.RowCount > 0 Then SAVEUPLOAD()

            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            'clear()
            Call toolnext_Click(sender, e)
            BILLDATE.Focus()


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Sub CALC()
        Try
            TXTAMT.Text = 0.0
            If Val(TXTRATE.Text) > 0 And Val(TXTWT.Text) > 0 And CMBPER.Text <> "" Then
                TXTAMT.Text = ((Val(TXTWT.Text.Trim) / Val(CMBPER.Text.Trim)) * Val(TXTRATE.Text.Trim))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If cmbregister.Text.Trim.Length = 0 Then
            EP.SetError(cmbregister, "Enter Register Name")
            bln = False
        End If

        If cmbname.Text.Trim.Length = 0 Then
            EP.SetError(cmbname, " Please Fill Company Name ")
            bln = False
        End If

        If PURTYPE = "YARN PURCHASE" And CMBMILL.Text.Trim.Length = 0 Then
            EP.SetError(CMBMILL, " Please Fill Mill Name ")
            bln = False
        End If

        If TXTPARTYBILLNO.Text.Trim.Length = 0 Then
            EP.SetError(TXTPARTYBILLNO, "Enter Party Bill No")
            bln = False
        End If

        If GRIDBILL.RowCount = 0 And PURTYPE <> "COMMON PURCHASE" Then
            EP.SetError(cmbname, "Select Item Details")
            bln = False
        End If

        If PURTYPE <> "COMMON PURCHASE" Then
            For Each row As DataGridViewRow In GRIDBILL.Rows
                If Val(row.Cells(GAMT.Index).Value) = 0 Then
                    EP.SetError(cmbname, "Amt Cannot be 0")
                    bln = False
                End If
            Next
        End If

        'If TXTBAL.ForeColor = Color.Red Then
        '    If ClientName = "AMIGO" Then
        '        EP.SetError(cmbname, "Amt Exceeds Cr Limit")
        '    Else
        '        EP.SetError(LBLACCBAL, "Amt Exceeds Cr Limit")
        '    End If
        '    bln = False
        'End If

        Dim FORMTYPE As String = ""
        For Each DTROW As DataRowView In CHKFORMBOX.CheckedItems
            FORMTYPE = DTROW.Item(0)
        Next
        If FORMTYPE = Nothing Then
            EP.SetError(CHKFORMBOX, "Pls Select Form Type")
            bln = False
        End If

        'REMOVE THIS LOCK, COZ ONCE TDS IS DEDUCTED IN YARN PUCHASE, WE WONT BE ABLE TO ENTER VEHICLE DETAILS FOR EWAY BILL
        'AS PER YESHA 
        'If lbllocked.Visible = True Then
        '    EP.SetError(lbllocked, "Rec/Pay/TDS Made, Delete Rec/Pay/TDS First")
        '    bln = False
        'End If

        If BILLDATE.Text = "__/__/____" Then
            EP.SetError(BILLDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(BILLDATE.Text) Then
                EP.SetError(BILLDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If DTPARTYBILLDATE.Text = "__/__/____" Then
            EP.SetError(DTPARTYBILLDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTPARTYBILLDATE.Text) Then
                EP.SetError(DTPARTYBILLDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        'If Convert.ToDateTime(DTPARTYBILLDATE.Text).Date >= "01/02/2018" Or txtgrandtotal.Text > 50000 Then
        '    If TXTEWAYBILLNO.Text.Trim.Length = 0 Then
        '        If MsgBox("E-Way No. Not Entered, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
        '            EP.SetError(TXTEWAYBILLNO, " Please Enter E-Way No..... ")
        '            bln = False
        '        End If
        '    End If
        'End If

        If Convert.ToDateTime(DTPARTYBILLDATE.Text).Date >= "01/07/2017" Then
            If TXTSTATECODE.Text.Trim.Length = 0 Then
                EP.SetError(TXTSTATECODE, "Please enter the state code")
                bln = False
            End If

            'If TXTGSTIN.Text.Trim.Length = 0 And CHKRCM.CheckState = CheckState.Unchecked Then
            '    EP.SetError(CHKRCM, "Select Reverse Charge")
            '    bln = False
            'End If


            'If TXTGSTIN.Text.Trim.Length > 0 And CHKRCM.CheckState = CheckState.Checked Then
            '    If MsgBox("Reverse Charge Not Applicable, Wish to Continue?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            '        EP.SetError(CHKRCM, "Reverse Charge Not Applicable")
            '        bln = False
            '    End If
            'End If


            If CMPSTATECODE <> TXTSTATECODE.Text.Trim And (Val(TXTCGSTAMT.Text) > 0 Or Val(TXTSGSTAMT.Text.Trim) > 0) Then
                EP.SetError(TXTSTATECODE, "Invaid Entry Done in CGST/SGST")
                bln = False
            End If

            If CMPSTATECODE = TXTSTATECODE.Text.Trim And Val(TXTIGSTAMT.Text) > 0 Then
                EP.SetError(TXTSTATECODE, "Invaid Entry Done in IGST")
                bln = False
            End If

            'CHECK WHETHER PURCHASER HAS CROSSED 50LAKHS OR NOT
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If CHKTDS.CheckState = CheckState.Unchecked Then
                Dim TEMPTDSTOTAL As Double = Val(txtgrandtotal.Text.Trim)
                DT = OBJCMN.Execute_Any_String("SELECT ISNULL(SUM(BILL_GRANDTOTAL),0) AS GTOTAL FROM PURCHASEMASTER INNER JOIN LEDGERS ON BILL_LEDGERID = LEDGERS.ACC_ID WHERE BILL_YEARID = " & YearId & " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "'", "", "")
                If DT.Rows.Count > 0 Then TEMPTDSTOTAL += Val(DT.Rows(0).Item("GTOTAL"))
                If TEMPTDSTOTAL > 5000000 Then
                    If MsgBox("Amount Exceeds 5000000, and TDS is not Applied, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        EP.SetError(cmbname, "Apply TDS")
                        bln = False
                    End If
                End If
            End If



            'CHECK WHETHER WEAVER HAS CROSSED 20LAKHS OR NOT
            If ClientName = "YESHA" And (CMBTYPE.Text = "WEAVING CHGS" Or CMBTYPE.Text = "SIZING CHGS" Or CMBTYPE.Text = "WARPER CHGS") Then
                Dim TEMPTDSTOTAL As Double = Val(txtgrandtotal.Text.Trim)
                DT = OBJCMN.Execute_Any_String("SELECT ISNULL(SUM(BILL_GRANDTOTAL),0) AS GTOTAL FROM PURCHASEMASTER INNER JOIN LEDGERS ON BILL_LEDGERID = LEDGERS.ACC_ID WHERE BILL_YEARID = " & YearId & " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "'", "", "")
                If DT.Rows.Count > 0 Then TEMPTDSTOTAL += Val(DT.Rows(0).Item("GTOTAL"))
                If TEMPTDSTOTAL > 2000000 Then
                    If MsgBox("Amount Exceeds 2000000, and TDS is not Applied, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        EP.SetError(cmbname, "Apply TDS")
                        bln = False
                    End If
                End If
            End If


            If PURTYPE <> "YARN PURCHASE" And PURTYPE <> "GREY PURCHASE" And PURTYPE <> "FINISHED PURCHASE" And CMBSACDESC.Text.Trim = "" Then
                EP.SetError(CMBSACDESC, "Select SAC Desc")
                bln = False
            End If
        End If

        Return bln
    End Function

    Private Sub cmbname_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.GotFocus
        Try
            If cmbname.Text.Trim = "" Then
                If PURTYPE = "YARN PURCHASE" Or PURTYPE = "GREY PURCHASE" Or PURTYPE = "FINISHED PURCHASE" Then
                    fillname(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_SUBTYPE NOT IN('PROCESSOR','SIZER','WEAVER')")
                ElseIf PURTYPE = "COMMON PURCHASE" Then
                    fillname(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' ")
                ElseIf PURTYPE = "PROCESS CHGS" Then
                    fillname(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (ACC_SUBTYPE= 'PROCESSOR' OR ACC_SUBTYPE= 'JOBBER')")
                ElseIf PURTYPE = "SIZING CHGS" Or PURTYPE = "WARPER CHGS" Then
                    fillname(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (ACC_SUBTYPE= 'SIZER' OR ACC_SUBTYPE= 'WARPER')")
                ElseIf PURTYPE = "WEAVING CHGS" Then
                    fillname(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE= 'WEAVER'")
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try

            TXTTCSPER.Text = 0
            If CHKMANUALTCS.Checked = False Then TXTTCSAMT.Text = 0

            'FETCH TCSPERCENT WITH RESPECT TO DATE
            Dim OBJCMN As New ClsCommon
            Dim DTTCS As DataTable = OBJCMN.search("TOP 1 ISNULL(TCSPER,0) AS TCSPER", "", "TCSPERCENT", " AND TCSDATE <= '" & Format(Convert.ToDateTime(DTPARTYBILLDATE.Text).Date, "MM/dd/yyyy") & "' ORDER BY TCSDATE DESC")
            If DTTCS.Rows.Count > 0 Then TXTTCSPER.Text = Val(DTTCS.Rows(0).Item("TCSPER"))

            lbltotalbags.Text = "0"
            LBLTOTALWT.Text = "0.0"
            lbltotalamt.Text = "0.0"
            TXTTOTALTAXAMT.Clear()
            TXTTOTALOTHERCHGSAMT.Clear()

            If PURTYPE <> "COMMON PURCHASE" Then TXTBILLAMT.Text = 0.0

            TXTCHARGES.Text = 0.0
            TXTSUBTOTAL.Text = 0
            txtroundoff.Text = 0
            txtgrandtotal.Text = 0

            TXTSACCODE.Clear()

            Dim DT As New DataTable

            If GRIDBILL.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDBILL.Rows
                    If PURTYPE = "YARN PURCHASE" Then
                        If Val(row.Cells(GPER.Index).EditedFormattedValue) = "1" Then
                            row.Cells(GAMT.Index).Value = Format((row.Cells(gwt.Index).EditedFormattedValue * row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        ElseIf Val(row.Cells(GPER.Index).EditedFormattedValue) = "5" Then
                            row.Cells(GAMT.Index).Value = Format(((row.Cells(gwt.Index).EditedFormattedValue / 5) * row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        ElseIf Val(row.Cells(GPER.Index).EditedFormattedValue) = "5.5" Then
                            row.Cells(GAMT.Index).Value = Format(((row.Cells(gwt.Index).EditedFormattedValue / 5.5) * row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        ElseIf Val(row.Cells(GPER.Index).EditedFormattedValue) = "10" Then
                            row.Cells(GAMT.Index).Value = Format(((row.Cells(gwt.Index).EditedFormattedValue / 10) * row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        ElseIf row.Cells(GPER.Index).EditedFormattedValue = "LBS" Then
                            row.Cells(GAMT.Index).Value = Format((row.Cells(gwt.Index).EditedFormattedValue * 0.22046 * row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        End If
                    Else
                        If row.Cells(GPER.Index).EditedFormattedValue = "MTRS" Then row.Cells(GAMT.Index).Value = Format((row.Cells(gwt.Index).EditedFormattedValue * row.Cells(GRATE.Index).EditedFormattedValue), "0.00") Else row.Cells(GAMT.Index).Value = Format((row.Cells(gbags.Index).EditedFormattedValue * row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                    End If
                    If Val(row.Cells(gbags.Index).Value) > 0 Then lbltotalbags.Text = Format(Val(lbltotalbags.Text) + Val(row.Cells(gbags.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(gwt.Index).Value) > 0 Then LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(row.Cells(gwt.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GAMT.Index).Value) > 0 Then lbltotalamt.Text = Format(Val(lbltotalamt.Text) + Val(row.Cells(GAMT.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GAMT.Index).Value) > 0 Then TXTBILLAMT.Text = Format(Val(TXTBILLAMT.Text) + Val(row.Cells(GAMT.Index).EditedFormattedValue), "0.00")


                    If DTPARTYBILLDATE.Text <> "__/__/____" AndAlso Convert.ToDateTime(DTPARTYBILLDATE.Text).Date >= "01/07/2017" Then
                        If PURTYPE <> "YARN PURCHASE" And PURTYPE <> "GREY PURCHASE" And PURTYPE <> "FINISHED PURCHASE" Then
                            DT = OBJCMN.search("  ISNULL(HSN_CODE, '') AS HSNCODE, ISNULL(HSN_CGST, 0) AS CGSTPER, ISNULL(HSN_SGST, 0) AS SGSTPER, ISNULL(HSN_IGST, 0) AS IGSTPER", "", " HSNMASTER ", " AND HSNMASTER.HSN_ITEMDESC = '" & CMBSACDESC.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
                            If DT.Rows.Count > 0 Then
                                TXTSACCODE.Text = DT.Rows(0).Item("HSNCODE")
                                If cmbname.Text.Trim <> "" Then
                                    If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                                        TXTIGSTPER.Text = 0
                                        TXTCGSTPER.Text = Val(DT.Rows(0).Item("CGSTPER"))
                                        TXTSGSTPER.Text = Val(DT.Rows(0).Item("SGSTPER"))
                                    Else
                                        TXTCGSTPER.Text = 0
                                        TXTSGSTPER.Text = 0
                                        TXTIGSTPER.Text = Val(DT.Rows(0).Item("IGSTPER"))
                                    End If
                                End If
                            End If
                        Else
                            If PURTYPE = "YARN PURCHASE" Then DT = OBJCMN.search("  ISNULL(HSN_CODE, '') AS HSNCODE, ISNULL(HSN_CGST, 0) AS CGSTPER, ISNULL(HSN_SGST, 0) AS SGSTPER, ISNULL(HSN_IGST, 0) AS IGSTPER", "", " QUALITYMASTER LEFT OUTER JOIN HSNMASTER ON QUALITY_HSNCODEID = HSN_ID", " AND QUALITY_NAME = '" & row.Cells(GQUALITY.Index).Value & "' AND HSNMASTER.HSN_YEARID=" & YearId) Else DT = OBJCMN.search("  ISNULL(HSN_CODE, '') AS HSNCODE, ISNULL(HSN_CGST, 0) AS CGSTPER, ISNULL(HSN_SGST, 0) AS SGSTPER, ISNULL(HSN_IGST, 0) AS IGSTPER", "", " GREYQUALITYMASTER LEFT OUTER JOIN HSNMASTER ON GREY_HSNCODEID = HSN_ID ", " AND GREY_NAME = '" & row.Cells(GQUALITY.Index).Value & "' AND HSNMASTER.HSN_YEARID=" & YearId)
                            If DT.Rows.Count > 0 Then
                                row.Cells(GHSNCODE.Index).Value = DT.Rows(0).Item("HSNCODE")
                                If cmbname.Text.Trim <> "" Then
                                    If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                                        TXTIGSTPER.Text = 0
                                        TXTCGSTPER.Text = Val(DT.Rows(0).Item("CGSTPER"))
                                        TXTSGSTPER.Text = Val(DT.Rows(0).Item("SGSTPER"))
                                    Else
                                        TXTCGSTPER.Text = 0
                                        TXTSGSTPER.Text = 0
                                        TXTIGSTPER.Text = Val(DT.Rows(0).Item("IGSTPER"))
                                    End If
                                End If
                            End If
                        End If
                    End If

                Next
            End If


            'THIS IS WRITTEN HERE COZ ABOVE CODE RUNS WITH RESPECT TO GRID, AND IN COMMPON WE DONT HAVE GRID LINES
            If DTPARTYBILLDATE.Text <> "__/__/____" AndAlso PURTYPE = "COMMON PURCHASE" And Convert.ToDateTime(DTPARTYBILLDATE.Text).Date >= "01/07/2017" Then
                DT = OBJCMN.search("  ISNULL(HSN_CODE, '') AS HSNCODE, ISNULL(HSN_CGST, 0) AS CGSTPER, ISNULL(HSN_SGST, 0) AS SGSTPER, ISNULL(HSN_IGST, 0) AS IGSTPER", "", " HSNMASTER ", " AND HSNMASTER.HSN_ITEMDESC = '" & CMBSACDESC.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
                If DT.Rows.Count > 0 Then
                    TXTSACCODE.Text = DT.Rows(0).Item("HSNCODE")
                    If cmbname.Text.Trim <> "" Then
                        If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                            TXTIGSTPER.Text = 0
                            TXTCGSTPER.Text = Val(DT.Rows(0).Item("CGSTPER"))
                            TXTSGSTPER.Text = Val(DT.Rows(0).Item("SGSTPER"))
                        Else
                            TXTCGSTPER.Text = 0
                            TXTSGSTPER.Text = 0
                            TXTIGSTPER.Text = Val(DT.Rows(0).Item("IGSTPER"))
                        End If
                    End If
                End If
            End If


            If GRIDCHGS.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDCHGS.Rows
                    TXTCHARGES.Text = Format(Val(TXTCHARGES.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
                    If Val(row.Cells(ETAXID.Index).Value) > 0 Then TXTTOTALTAXAMT.Text = Format(Val(TXTTOTALTAXAMT.Text) + Val(row.Cells(EAMT.Index).Value), "0.00") Else TXTTOTALOTHERCHGSAMT.Text = Format(Val(TXTTOTALOTHERCHGSAMT.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
                Next
            End If

            'DONT DO IT HERE THIS MIGHT CREATE ISSUE 
            'If GRIDCHGS.RowCount > 0 Then
            '    For Each row As DataGridViewRow In GRIDCHGS.Rows
            '        'IF PERCENT IS > 0 THEN GETAUTO CHARGES
            '        DT = OBJCMN.search("ISNULL(ACC_CALC,'GROSS') AS CALC", "", "LEDGERS", "AND ACC_CMPNAME = '" & row.Cells(ECHARGES.Index).Value & "' AND ACC_YEARID = " & YearId)
            '        If DT.Rows(0).Item("CALC") = "GROSS" And Val(row.Cells(EPER.Index).Value) <> 0 Then
            '            row.Cells(EAMT.Index).Value = Format((Val(row.Cells(EPER.Index).Value) * Val(TXTTOTALPURAMT.Text.Trim)) / 100, "0.00")
            '        ElseIf DT.Rows(0).Item("CALC") = "NETT" And Val(row.Cells(EPER.Index).Value) <> 0 Then
            '            TXTNETTAMT.Text = Val(TXTTOTALPURAMT.Text.Trim)
            '            For I As Integer = 0 To row.Index - 1
            '                TXTNETTAMT.Text = Format(Val(TXTNETTAMT.Text) + Val(GRIDCHGS.Rows(I).Cells(EAMT.Index).Value), "0.00")
            '            Next
            '            row.Cells(EAMT.Index).Value = Format((Val(row.Cells(EPER.Index).Value) * Val(TXTNETTAMT.Text.Trim)) / 100, "0.00")
            '        End If
            '        TXTCHARGES.Text = Format(Val(TXTCHARGES.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
            '        If Val(row.Cells(ETAXID.Index).Value) > 0 Then TXTTOTALTAXAMT.Text = Format(Val(TXTTOTALTAXAMT.Text) + Val(row.Cells(EAMT.Index).Value), "0.00") Else TXTTOTALOTHERCHGSAMT.Text = Format(Val(TXTTOTALOTHERCHGSAMT.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
            '    Next
            'End If



            If CHKRCM.CheckState = CheckState.Checked Then TXTSUBTOTAL.Text = Format(Val(TXTBILLAMT.Text) + Val(TXTCHARGES.Text.Trim), "0") Else TXTSUBTOTAL.Text = Format(Val(TXTBILLAMT.Text) + Val(TXTCHARGES.Text.Trim), "0.00")

            If CHKMANUAL.CheckState = CheckState.Unchecked Then
                TXTCGSTAMT.Text = Format(Val(TXTCGSTPER.Text.Trim) * Val(TXTSUBTOTAL.Text.Trim) / 100, "0.00")
                TXTSGSTAMT.Text = Format(Val(TXTSGSTPER.Text.Trim) * Val(TXTSUBTOTAL.Text.Trim) / 100, "0.00")
                TXTIGSTAMT.Text = Format(Val(TXTIGSTPER.Text.Trim) * Val(TXTSUBTOTAL.Text.Trim) / 100, "0.00")
            End If

            TXTTOTALWITHGST.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT.Text.Trim) + Val(TXTSGSTAMT.Text.Trim) + Val(TXTIGSTAMT.Text.Trim), "0.00")
            If CHKTCS.CheckState = CheckState.Checked And CHKMANUALTCS.CheckState = CheckState.Unchecked Then TXTTCSAMT.Text = Format((Val(TXTTOTALWITHGST.Text.Trim) * Val(TXTTCSPER.Text.Trim)) / 100, "0.00")

            txtgrandtotal.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT.Text.Trim) + Val(TXTSGSTAMT.Text.Trim) + Val(TXTIGSTAMT.Text.Trim) + Val(TXTTCSAMT.Text.Trim), "0")
            txtroundoff.Text = Format(Val(txtgrandtotal.Text) - (Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT.Text.Trim) + Val(TXTSGSTAMT.Text.Trim) + Val(TXTIGSTAMT.Text.Trim) + Val(TXTTCSAMT.Text.Trim)), "0.00")
            txtgrandtotal.Text = Format(Val(txtgrandtotal.Text), "0.00")


            'GET NETTRATE
            If Val(txtgrandtotal.Text.Trim) > 0 Then
                For Each row As DataGridViewRow In GRIDBILL.Rows
                    If PURTYPE = "YARN PURCHASE" Then
                        row.Cells(GNETTRATE.Index).Value = Format((Val(txtgrandtotal.Text) / Val(LBLTOTALWT.Text.Trim)), "0.000")
                    Else
                        If row.Cells(GPER.Index).EditedFormattedValue = "MTRS" Then row.Cells(GNETTRATE.Index).Value = Format((Val(txtgrandtotal.Text) / Val(LBLTOTALWT.Text.Trim)), "0.000") Else row.Cells(GNETTRATE.Index).Value = Format((Val(txtgrandtotal.Text) / Val(lbltotalbags.Text.Trim)), "0.000")
                    End If
                Next
            End If

            If Val(txtgrandtotal.Text) > 0 Then txtinwords.Text = CurrencyToWord(txtgrandtotal.Text)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If EDIT = True Then
                If lbllocked.Visible = True Then
                    MsgBox("Invoice Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                'CHECKING WHETHER CFORM OR ANY OTHER FORM HAS BEEN RECD OR NOT, IF RECD THEN LOCK IT, CHECK IN CFORMVIEW WITH THIS INVOICENO
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("FORMNO", "", " CFORMVIEW ", " AND BILL = " & TEMPBILLNO & " AND REGTYPE = '" & TEMPREGNAME & "' AND TYPE = 'PURCHASE' AND FORMNO <> '' AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    MsgBox("Form Recd, Delete Form First", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                Dim intresult As Integer
                Dim objcls As New ClsPurchaseMaster()
                Dim TEMPMSG As Integer = MsgBox("Wish To Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then

                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPBILLNO)
                    alParaval.Add(TEMPREGNAME)
                    alParaval.Add(CmpId)
                    alParaval.Add(Locationid)
                    alParaval.Add(YearId)
                    alParaval.Add(Userid)
                    objcls.alParaval = alParaval
                    intresult = objcls.DELETE()
                    MsgBox("Purchase Invoice Delete Successfully")
                    clear()
                    EDIT = False
                    BILLDATE.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDBILL.RowCount = 0
LINE1:
            TEMPBILLNO = Val(TXTBILLNO.Text) - 1
            TEMPREGNAME = cmbregister.Text.Trim
            If TEMPBILLNO > 0 Then
                EDIT = True
                PurchaseMaster_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If Val(txtgrandtotal.Text.Trim) = 0 And TEMPBILLNO > 1 Then
                TXTBILLNO.Text = TEMPBILLNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDBILL.RowCount = 0
LINE1:
            TEMPBILLNO = Val(TXTBILLNO.Text) + 1
            TEMPREGNAME = cmbregister.Text.Trim
            getmax_BILL_no()
            Dim MAXNO As Integer = TXTBILLNO.Text.Trim
            clear()
            If Val(TXTBILLNO.Text) - 1 >= TEMPBILLNO Then
                EDIT = True
                PurchaseMaster_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If Val(txtgrandtotal.Text.Trim) = 0 And TEMPBILLNO < MAXNO Then
                TXTBILLNO.Text = TEMPBILLNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objINVDTLS As New PurchaseInvoiceDetails
            objINVDTLS.MdiParent = MDIMain
            objINVDTLS.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub cmbname_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Validated
        Try
            Dim OBJCMN As New ClsCommon
            If cmbname.Text.Trim <> "" Then
                'GET REGISTER , AGENCT AND TRANS
                Dim DT As DataTable = OBJCMN.search("ISNULL(LEDGERS_1.ACC_CMPNAME,'') AS TRANSNAME,ISNULL(LEDGERS_2.ACC_CMPNAME,'') AS AGENTNAME, ISNULL(REGISTER_NAME,'') AS REGISTERNAME, ISNULL(LEDGERS.ACC_CRDAYS,0) AS CRDAYS , ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN ", "", "    LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid AND LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_1.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_1.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_1.Acc_yearid LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_2.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_2.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_2.Acc_yearid LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.Acc_cmpid = REGISTER_cmpid AND LEDGERS.Acc_locationid = REGISTER_locationid AND LEDGERS.Acc_yearid = REGISTER_yearid AND LEDGERS.Acc_REGISTERID = REGISTER_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id  ", " and LEDGERS.acc_cmpname = '" & cmbname.Text.Trim & "' and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then
                    CMBAGENT.Text = DT.Rows(0).Item("AGENTNAME")
                    TXTCRDAYS.Text = Val(DT.Rows(0).Item("CRDAYS"))
                    TXTSTATECODE.Text = DT.Rows(0).Item("STATECODE")
                    TXTGSTIN.Text = DT.Rows(0).Item("GSTIN")
                    If Val(TXTCRDAYS.Text.Trim) > 0 Then DUEDATE.Text = DateAdd(DateInterval.Day, Val(TXTCRDAYS.Text.Trim), Convert.ToDateTime(DTPARTYBILLDATE.Text).Date)

                    If DT.Rows(0).Item("REGISTERNAME") <> cmbregister.Text.Trim And DT.Rows(0).Item("REGISTERNAME") <> "" Then
                        Dim TEMPMSG As Integer = MsgBox("Register is Different Change to Default?", MsgBoxStyle.YesNo)
                        If TEMPMSG = vbYes Then
                            cmbregister.Text = DT.Rows(0).Item("REGISTERNAME")
                            getmax_BILL_no()
                        End If
                    End If
                    TOTAL()
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then namevalidate(cmbname, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        cmdok_Click(sender, e)
    End Sub

    Private Sub cmdselectgrn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTDATA.Click
        Try

            'If cmbname.Text.Trim = "" And CMBMILL.Text.Trim = "" Then
            '    MsgBox("Please Select Name First")
            '    cmbname.Focus()
            '    Exit Sub
            'End If

            If PURTYPE = "YARN PURCHASE" Then

                Dim DTTABLE As DataTable
                Dim OBJSELECTPO As New SelectGRN
                OBJSELECTPO.PARTYNAME = cmbname.Text.Trim
                OBJSELECTPO.MILLNAME = CMBMILL.Text.Trim
                OBJSELECTPO.FRMSTRING = "YARN"
                OBJSELECTPO.ShowDialog()

                DTTABLE = OBJSELECTPO.DT

                Dim i As Integer = 0
                If DTTABLE.Rows.Count > 0 Then
                    For Each ROW As DataRow In DTTABLE.Rows
                        Dim objclspreq As New ClsCommon()
                        Dim DT As DataTable = objclspreq.search(" GRN.grn_no AS GRNNO, ISNULL(GRN.grn_challanno, '') AS RECNO,LEDGERS.Acc_cmpname AS NAME, GRN.grn_date AS DATE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(MILLNAME.Acc_cmpname, '') AS MILLNAME, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, ISNULL(GRN.GRN_TOTALBAGS,0) AS TOTALBAGS,ISNULL(GRN.GRN_TOTALWT,0) AS TOTALWT, ISNULL(BROKER.Acc_cmpname, '') AS BROKER, ISNULL(GRN.GRN_PONO,'') AS PONO ", "", " GRN INNER JOIN LEDGERS ON GRN.grn_ledgerid = LEDGERS.Acc_id INNER JOIN GODOWNMASTER ON GRN.GRN_GODOWNID = GODOWNMASTER.GODOWN_ID LEFT OUTER JOIN LEDGERS AS MILLNAME ON GRN.GRN_MILLID = MILLNAME.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON GRN.grn_transledgerid = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON GRN.GRN_BROKERID = BROKER.Acc_id ", " AND GRN.GRN_NO = " & ROW("GRNNO") & " AND GRN_TYPE = 'YARN' AND grn.grn_YEARID = " & YearId & "  ORDER BY GRN.grn_no")
                        If DT.Rows.Count > 0 Then
                            For Each dr As DataRow In DT.Rows
                                cmbname.Text = dr("NAME")
                                CMBAGENT.Text = dr("BROKER")
                                CMBMILL.Text = dr("MILLNAME")
                                TXTCHALLANNO.Text = dr("GRNNO")
                                TXTREFNO.Text = dr("PONO")
                                cmbGodown.Text = dr("GODOWN")
                            Next
                        End If

                        Dim OBJCMN As New ClsCommon()
                        Dim DT2 As DataTable = OBJCMN.search(" ISNULL(GRN_DESC.grn_no, 0) AS GRNNO,ISNULL(GRN_DESC.grn_gridsrno, 0) AS SRNO, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(GRN_DESC.GRN_BAGS, 0) AS BAGS, ISNULL(GRN_DESC.GRN_WT, 0)-ISNULL(GRN_DESC.GRN_PURWT, 0) AS WT, ISNULL(LEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(GRN_DESC.GRN_LRNO, '') AS LRNO, GRN_DESC.GRN_LRDATE AS LRDATE, ISNULL(GRN_DESC.GRN_NARRATION, '') AS NARRATION, ISNULL(PURCHASEORDER_DESC.PO_RATE, 0) AS RATE ", "", "   GRN INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS ON GRN.grn_transledgerid = LEDGERS.Acc_id LEFT OUTER JOIN QUALITYMASTER ON GRN_DESC.GRN_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN PURCHASEORDER_DESC ON GRN_DESC.GRN_POSRNO = PURCHASEORDER_DESC.PO_GRIDSRNO AND GRN_DESC.GRN_PONO = PURCHASEORDER_DESC.PO_NO AND GRN_DESC.GRN_YEARID = PURCHASEORDER_DESC.PO_YEARID AND GRN_DESC.GRN_GRIDTYPE = 'PO' ", " and GRN.GRN_NO=" & Val(TXTCHALLANNO.Text.Trim) & " AND GRN_DESC.GRN_GRIDSRNO = " & Val(ROW("GRIDSRNO")) & " AND GRN.GRN_YEARID = " & YearId)
                        If DT2.Rows.Count > 0 Then
                            For Each dr As DataRow In DT2.Rows
                                'IF RATE IS 0 THEN CHECK OPENING PO
                                If Val(dr("RATE")) = 0 Then
                                    Dim DTPORATE As DataTable = OBJCMN.search(" ISNULL(OPENINGPURCHASEORDER_DESC.OPPO_RATE, 0) AS RATE ", "", "   GRN INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS ON GRN.grn_transledgerid = LEDGERS.Acc_id LEFT OUTER JOIN QUALITYMASTER ON GRN_DESC.GRN_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN OPENINGPURCHASEORDER_DESC ON GRN_DESC.GRN_POSRNO = OPENINGPURCHASEORDER_DESC.OPPO_GRIDSRNO AND GRN_DESC.GRN_PONO = OPENINGPURCHASEORDER_DESC.OPPO_NO AND GRN_DESC.GRN_YEARID = OPENINGPURCHASEORDER_DESC.OPPO_YEARID AND GRN_DESC.GRN_GRIDTYPE = 'OPENING' ", " and GRN.GRN_NO=" & Val(TXTCHALLANNO.Text.Trim) & " AND GRN.GRN_YEARID = " & YearId)
                                    If DTPORATE.Rows.Count > 0 Then dr("RATE") = Val(DTPORATE.Rows(0).Item("RATE"))
                                End If
                                GRIDBILL.Rows.Add(0, dr("QUALITY"), Format(Val(dr("BAGS")), "0"), Format(Val(dr("WT")), "0.00"), Format(Val(dr("RATE")), "0.0000"), 0, "1", "0.00", dr("TRANSPORT"), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), 0, "", 0, 0, "", dr("GRNNO"), dr("SRNO"), 0)
                                CMBTRANSPORT.Text = dr("TRANSPORT")
                            Next
                        End If
                    Next

                    'cmbname.Enabled = False

                    GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1
                    getsrno(GRIDBILL)
                    If GRIDBILL.RowCount > 0 Then
                        GRIDBILL.Focus()
                        GRIDBILL.CurrentCell = GRIDBILL.Rows(0).Cells(GRATE.Index)
                    End If
                End If

            ElseIf PURTYPE = "FINISHED PURCHASE" Then

                Dim DTTABLE As DataTable
                Dim OBJSELECTPO As New SelectGRN
                OBJSELECTPO.PARTYNAME = cmbname.Text.Trim
                OBJSELECTPO.FRMSTRING = "FINISHED"
                OBJSELECTPO.ShowDialog()

                DTTABLE = OBJSELECTPO.DT

                Dim i As Integer = 0
                If DTTABLE.Rows.Count > 0 Then

                    For i = 0 To DTTABLE.Rows.Count - 1
                        Dim objclspreq As New ClsCommon()
                        Dim DT As DataTable = objclspreq.search(" GRN.grn_no AS GRNNO, ISNULL(GRN.grn_challanno, '') AS RECNO,LEDGERS.Acc_cmpname AS NAME, GRN.grn_date AS DATE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(MILLNAME.Acc_cmpname, '') AS MILLNAME, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, ISNULL(GRN.GRN_TOTALBAGS,0) AS TOTALBAGS,ISNULL(GRN.GRN_TOTALWT,0) AS TOTALWT, ISNULL(BROKER.Acc_cmpname, '') AS BROKER ", "", " GRN INNER JOIN LEDGERS ON GRN.grn_ledgerid = LEDGERS.Acc_id INNER JOIN GODOWNMASTER ON GRN.GRN_GODOWNID = GODOWNMASTER.GODOWN_ID LEFT OUTER JOIN LEDGERS AS MILLNAME ON GRN.GRN_MILLID = MILLNAME.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON GRN.grn_transledgerid = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON GRN.GRN_BROKERID = BROKER.Acc_id ", " AND GRN.GRN_NO = " & DTTABLE.Rows(0).Item("GRNNO") & " AND GRN_TYPE = 'FINISHED' AND GRN.GRN_DONE=0 AND grn.grn_YEARID = " & YearId & "  ORDER BY GRN.grn_no")

                        If DT.Rows.Count > 0 Then
                            For Each dr As DataRow In DT.Rows
                                cmbname.Text = dr("NAME")
                                CMBAGENT.Text = dr("BROKER")
                                CMBMILL.Text = dr("MILLNAME")
                                TXTCHALLANNO.Text = dr("GRNNO")
                                cmbGodown.Text = dr("GODOWN")
                            Next
                        End If
                    Next

                    Dim OBJCMN As New ClsCommon()
                    Dim DT2 As DataTable = OBJCMN.search("  ISNULL(GRN_DESC.GRN_NO, 0) AS GRNNO, ISNULL(GRN_DESC.GRN_GRIDSRNO, 0) AS SRNO, ISNULL(greyqualitymaster.GREY_NAME, '') AS QUALITY,  ISNULL(GRN_DESC.GRN_BAGS, 0) AS BAGS, ISNULL(GRN_DESC.GRN_WT, 0) AS WT, ISNULL(LEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(GRN_DESC.GRN_LRNO, '') AS LRNO, GRN_DESC.GRN_LRDATE AS LRDATE, ISNULL(GRN_DESC.GRN_NARRATION, '') AS NARRATION ,ISNULL(PURCHASEORDER_DESC.PO_RATE, 0) AS RATE", "", "   GRN INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS ON GRN.grn_transledgerid = LEDGERS.Acc_id LEFT OUTER JOIN greyqualitymaster ON GRN_DESC.GRN_QUALITYID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN PURCHASEORDER_DESC ON GRN_DESC.GRN_POSRNO = PURCHASEORDER_DESC.PO_GRIDSRNO AND GRN_DESC.GRN_PONO = PURCHASEORDER_DESC.PO_NO AND GRN_DESC.GRN_YEARID = PURCHASEORDER_DESC.PO_YEARID AND GRN_DESC.GRN_GRIDTYPE = 'PO' ", " and GRN.GRN_DONE=0 AND GRN.GRN_NO=" & Val(DTTABLE.Rows(0).Item("GRNNO")) & " AND GRN.GRN_YEARID = " & YearId)
                    If DT2.Rows.Count > 0 Then
                        For Each dr As DataRow In DT2.Rows
                            'IF RATE IS 0 THEN CHECK OPENING PO
                            If Val(dr("RATE")) = 0 Then
                                Dim DTPORATE As DataTable = OBJCMN.search(" ISNULL(OPENINGPURCHASEORDER_DESC.OPPO_RATE, 0) AS RATE ", "", "   GRN INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS ON GRN.grn_transledgerid = LEDGERS.Acc_id LEFT OUTER JOIN QUALITYMASTER ON GRN_DESC.GRN_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN OPENINGPURCHASEORDER_DESC ON GRN_DESC.GRN_POSRNO = OPENINGPURCHASEORDER_DESC.OPPO_GRIDSRNO AND GRN_DESC.GRN_PONO = OPENINGPURCHASEORDER_DESC.OPPO_NO AND GRN_DESC.GRN_YEARID = OPENINGPURCHASEORDER_DESC.OPPO_YEARID AND GRN_DESC.GRN_GRIDTYPE = 'OPENING' ", " and GRN.GRN_NO=" & Val(TXTCHALLANNO.Text.Trim) & " AND GRN.GRN_YEARID = " & YearId)
                                If DTPORATE.Rows.Count > 0 Then dr("RATE") = Val(DTPORATE.Rows(0).Item("RATE"))
                            End If
                            GRIDBILL.Rows.Add(0, dr("QUALITY"), Format(Val(dr("BAGS")), "0"), Format(Val(dr("WT")), "0.00"), Format(Val(dr("RATE")), "0.0000"), 0, "MTRS", "0.00", dr("TRANSPORT"), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), 0, "", 0, 0, "", dr("GRNNO"), dr("SRNO"), 0)
                            CMBTRANSPORT.Text = dr("TRANSPORT")
                        Next
                    End If

                    'cmbname.Enabled = False
                    GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1
                    getsrno(GRIDBILL)
                    If GRIDBILL.RowCount > 0 Then
                        GRIDBILL.Focus()
                        GRIDBILL.CurrentCell = GRIDBILL.Rows(0).Cells(GRATE.Index)
                    End If
                End If

            ElseIf PURTYPE = "GREY PURCHASE" Then

                Dim DTTABLE As DataTable
                Dim OBJSELECTPO As New SelectGRN
                OBJSELECTPO.PARTYNAME = cmbname.Text.Trim
                OBJSELECTPO.FRMSTRING = "GREY"
                OBJSELECTPO.ShowDialog()

                DTTABLE = OBJSELECTPO.DT

                Dim i As Integer = 0
                If DTTABLE.Rows.Count > 0 Then

                    For i = 0 To DTTABLE.Rows.Count - 1
                        Dim objclspreq As New ClsCommon()
                        Dim DT As DataTable = objclspreq.search(" GRN.grn_no AS GRNNO, ISNULL(GRN.grn_challanno, '') AS RECNO,LEDGERS.Acc_cmpname AS NAME, GRN.grn_date AS DATE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(MILLNAME.Acc_cmpname, '') AS MILLNAME, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, GRN.GRN_TOTALBAGS AS TOTALBAGS, GRN.GRN_TOTALWT AS TOTALWT, ISNULL(BROKER.Acc_cmpname, '') AS BROKER, GRN_DATE AS DATE ", "", " GRN INNER JOIN LEDGERS ON GRN.grn_ledgerid = LEDGERS.Acc_id INNER JOIN GODOWNMASTER ON GRN.GRN_GODOWNID = GODOWNMASTER.GODOWN_ID LEFT OUTER JOIN LEDGERS AS MILLNAME ON GRN.GRN_MILLID = MILLNAME.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON GRN.grn_transledgerid = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON GRN.GRN_BROKERID = BROKER.Acc_id ", " AND GRN.GRN_NO = " & DTTABLE.Rows(0).Item("GRNNO") & " AND GRN_TYPE = 'GREY' AND  grn.grn_YEARID = " & YearId & "  ORDER BY GRN.grn_no")

                        If DT.Rows.Count > 0 Then
                            For Each dr As DataRow In DT.Rows
                                cmbname.Text = dr("NAME")
                                CMBAGENT.Text = dr("BROKER")
                                CMBMILL.Text = dr("MILLNAME")
                                TXTCHALLANNO.Text = dr("GRNNO")
                                BILLDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                                DTPARTYBILLDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                                DUEDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                            Next
                        End If
                    Next

                    Dim OBJCMN As New ClsCommon()
                    Dim DT2 As DataTable = OBJCMN.search("  ISNULL(GRN_DESC.GRN_NO, 0) AS GRNNO, ISNULL(GRN_DESC.GRN_GRIDSRNO, 0) AS SRNO, ISNULL(greyqualitymaster.GREY_NAME, '') AS QUALITY, ISNULL(GRN_DESC.GRN_BAGS, 0) AS BAGS, (ISNULL(GRN_DESC.GRN_WT, 0) - ISNULL(GRN_DESC.GRN_PURWT, 0)) AS WT, ISNULL(LEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(GRN_DESC.GRN_LRNO, '') AS LRNO, GRN_DESC.GRN_LRDATE AS LRDATE, ISNULL(GRN_DESC.GRN_NARRATION, '') AS NARRATION,ISNULL(PURCHASEORDER_DESC.PO_RATE, 0) AS RATE ", "", "   GRN INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS ON GRN.grn_transledgerid = LEDGERS.Acc_id LEFT OUTER JOIN greyqualitymaster ON GRN_DESC.GRN_QUALITYID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN PURCHASEORDER_DESC ON GRN_DESC.GRN_POSRNO = PURCHASEORDER_DESC.PO_GRIDSRNO AND GRN_DESC.GRN_PONO = PURCHASEORDER_DESC.PO_NO AND GRN_DESC.GRN_YEARID = PURCHASEORDER_DESC.PO_YEARID AND GRN_DESC.GRN_GRIDTYPE = 'PO'  ", " AND GRN.GRN_NO=" & Val(DTTABLE.Rows(0).Item("GRNNO")) & " AND GRN.GRN_YEARID = " & YearId)
                    If DT2.Rows.Count > 0 Then
                        For Each dr As DataRow In DT2.Rows
                            'IF RATE IS 0 THEN CHECK OPENING PO
                            If Val(dr("RATE")) = 0 Then
                                Dim DTPORATE As DataTable = OBJCMN.search(" ISNULL(OPENINGPURCHASEORDER_DESC.OPPO_RATE, 0) AS RATE ", "", "   GRN INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS ON GRN.grn_transledgerid = LEDGERS.Acc_id LEFT OUTER JOIN QUALITYMASTER ON GRN_DESC.GRN_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN OPENINGPURCHASEORDER_DESC ON GRN_DESC.GRN_POSRNO = OPENINGPURCHASEORDER_DESC.OPPO_GRIDSRNO AND GRN_DESC.GRN_PONO = OPENINGPURCHASEORDER_DESC.OPPO_NO AND GRN_DESC.GRN_YEARID = OPENINGPURCHASEORDER_DESC.OPPO_YEARID AND GRN_DESC.GRN_GRIDTYPE = 'OPENING' ", " and GRN.GRN_NO=" & Val(TXTCHALLANNO.Text.Trim) & " AND GRN.GRN_YEARID = " & YearId)
                                If DTPORATE.Rows.Count > 0 Then dr("RATE") = Val(DTPORATE.Rows(0).Item("RATE"))
                            End If
                            GRIDBILL.Rows.Add(0, dr("QUALITY"), Format(Val(dr("BAGS")), "0"), Format(Val(dr("WT")), "0.00"), Format(Val(dr("RATE")), "0.0000"), 0, "MTRS", "0.00", dr("TRANSPORT"), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), 0, "", 0, 0, "", dr("GRNNO"), dr("SRNO"), 0)
                            CMBTRANSPORT.Text = dr("TRANSPORT")
                        Next
                    End If

                    'cmbname.Enabled = False
                    GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1
                    getsrno(GRIDBILL)
                    If GRIDBILL.RowCount > 0 Then
                        GRIDBILL.Focus()
                        GRIDBILL.CurrentCell = GRIDBILL.Rows(0).Cells(GRATE.Index)
                    End If
                End If

            ElseIf PURTYPE = "PROCESS CHGS" Then

                Dim DTTABLE As DataTable
                Dim OBJSELECTPO As New SelectGRN
                OBJSELECTPO.PARTYNAME = cmbname.Text.Trim
                OBJSELECTPO.FRMSTRING = "PROCESS"
                OBJSELECTPO.ShowDialog()

                DTTABLE = OBJSELECTPO.DT

                Dim i As Integer = 0
                If DTTABLE.Rows.Count > 0 Then

                    Dim OBJCMN As New ClsCommon()

                    'WE HAVE ADDED SAREEJOBIN IN THE PROCESS CHGS
                    'Dim DT2 As DataTable = OBJCMN.search("  GREYQUALITYMASTER.GREY_NAME AS QUALITY, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_TAKA AS TAKA, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_MTRS AS MTRS, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_BALENO AS BALENO, ISNULL(GREYRECEIVEDPROCESSING.GRECDPROCESSOR_PURDONE, 0) AS PURDONE  ", "", " GREYRECEIVEDPROCESSING INNER JOIN LEDGERS ON GREYRECEIVEDPROCESSING.GRECDPROCESSOR_LEDGERID = LEDGERS.Acc_id INNER JOIN GREYRECEIVEDPROCESSING_DESC ON GREYRECEIVEDPROCESSING.GRECDPROCESSOR_NO = GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_NO AND GREYRECEIVEDPROCESSING.GRECDPROCESSOR_YEARID = GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_YEARID INNER JOIN GREYQUALITYMASTER ON GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_GREYID = GREYQUALITYMASTER.GREY_ID  ", " AND GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_LOTNO=" & Val(DTTABLE.Rows(0).Item("GRNNO")) & " AND GREYRECEIVEDPROCESSING.GRECDPROCESSOR_YEARID = " & YearId)
                    Dim DT2 As DataTable = OBJCMN.search(" * ", "", "(SELECT GREYQUALITYMASTER.GREY_NAME AS QUALITY, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_TAKA AS TAKA, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_MTRS AS MTRS, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_BALENO AS BALENO, ISNULL(GREYRECEIVEDPROCESSING.GRECDPROCESSOR_PURDONE, 0) AS PURDONE FROM GREYRECEIVEDPROCESSING INNER JOIN LEDGERS ON GREYRECEIVEDPROCESSING.GRECDPROCESSOR_LEDGERID = LEDGERS.Acc_id INNER JOIN GREYRECEIVEDPROCESSING_DESC ON GREYRECEIVEDPROCESSING.GRECDPROCESSOR_NO = GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_NO AND GREYRECEIVEDPROCESSING.GRECDPROCESSOR_YEARID = GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_YEARID INNER JOIN GREYQUALITYMASTER ON GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_GREYID = GREYQUALITYMASTER.GREY_ID WHERE GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_LOTNO=" & Val(DTTABLE.Rows(0).Item("GRNNO")) & " AND GREYRECEIVEDPROCESSING.GRECDPROCESSOR_YEARID = " & YearId & " UNION ALL select GREYQUALITYMASTER.GREY_NAME AS QUALITY, SAREEJOBIN_DESC.JI_PCS AS TAKA, SAREEJOBIN_DESC.JI_MTRS AS MTRS, '' AS BALENO, 0 AS PURDONE FROM SAREEJOBIN INNER JOIN LEDGERS ON SAREEJOBIN.JI_LEDGERID = LEDGERS.Acc_id INNER JOIN SAREEJOBIN_DESC ON SAREEJOBIN.JI_NO = SAREEJOBIN_DESC.JI_NO AND SAREEJOBIN.JI_YEARID = SAREEJOBIN_DESC.JI_YEARID INNER JOIN GREYQUALITYMASTER ON SAREEJOBIN_DESC.JI_QUALITYID = GREYQUALITYMASTER.GREY_ID  WHERE SAREEJOBIN_DESC.JI_LOTNO= " & Val(DTTABLE.Rows(0).Item("GRNNO")) & " AND SAREEJOBIN.JI_YEARID = " & YearId & ") AS T", "")
                    If DT2.Rows.Count > 0 Then

                        If Convert.ToBoolean(DT2.Rows(0).Item("PURDONE")) = True Then MsgBox("Bill of this lot is already made, Please check before proceeding", MsgBoxStyle.Critical)

                        TXTLOTNO.Text = Val(DTTABLE.Rows(0).Item("GRNNO"))
                        TXTLOTNO.Enabled = False
                        For Each dr As DataRow In DT2.Rows
                            GRIDBILL.Rows.Add(0, dr("QUALITY"), Format(Val(dr("TAKA")), "0"), Format(Val(dr("MTRS")), "0.00"), "0.00", 0, "MTRS", "0.00", "", dr("BALENO"), "", 0, "", 0, 0, "", 0, 0, 0)
                        Next
                    End If

                    'cmbname.Enabled = False
                    GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1
                    getsrno(GRIDBILL)
                    If GRIDBILL.RowCount > 0 Then
                        GRIDBILL.Focus()
                        GRIDBILL.CurrentCell = GRIDBILL.Rows(0).Cells(GRATE.Index)
                    End If
                End If

            End If
            TOTAL()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            'If edit = False Then
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
            'End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'PURCHASE'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'PURCHASE' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If
            getmax_BILL_no()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            If cmbregister.Text.Trim.Length > 0 And EDIT = False Then
                'clear()
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_abbr, register_initials, register_id", "", " RegisterMaster", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'PURCHASE' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    PURREGABBR = dt.Rows(0).Item(0).ToString
                    PURREGINITIAL = dt.Rows(0).Item(1).ToString
                    PURREGID = dt.Rows(0).Item(2)
                    getmax_BILL_no()
                    cmbregister.Enabled = False
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridinv_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDBILL.CellValidating
        Dim colNum As Integer = GRIDBILL.Columns(e.ColumnIndex).Index
        If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return
        Select Case colNum
            Case GRATE.Index, gbags.Index, gwt.Index
                Dim dDebit As Decimal
                Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                If bValid Then
                    If GRIDBILL.CurrentCell.Value = Nothing Then GRIDBILL.CurrentCell.Value = "0.00"
                    GRIDBILL.CurrentCell.Value = Convert.ToDecimal(GRIDBILL.Item(colNum, e.RowIndex).Value)
                Else
                    MessageBox.Show("Invalid Number Entered")
                    e.Cancel = True
                    Exit Sub
                End If



        End Select


        If EDIT = False And ClientName = "SASHWINKUMAR" Then
            For I As Integer = GRIDBILL.CurrentRow.Index + 1 To GRIDBILL.RowCount - 1
                GRIDBILL.Item(GRATE.Index, I).Value = GRIDBILL.Item(GRATE.Index, I - 1).EditedFormattedValue
                GRIDBILL.Item(GPER.Index, I).Value = GRIDBILL.Item(GPER.Index, I - 1).EditedFormattedValue
            Next
        End If
        TOTAL()

    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDBILL.RowCount = 0

                TEMPBILLNO = Val(tstxtbillno.Text)
                TEMPREGNAME = cmbregister.Text.Trim
                If TEMPBILLNO > 0 Then
                    EDIT = True
                    PurchaseMaster_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Sub fillchgsgrid()

        If GRIDCHGSDOUBLECLICK = False Then
            GRIDCHGS.Rows.Add(Val(TXTCHGSSRNO.Text.Trim), CMBCHARGES.Text.Trim, Val(TXTCHGSPER.Text.Trim), Val(TXTCHGSAMT.Text.Trim), Val(TXTTAXID.Text.Trim))
            getsrno(GRIDCHGS)
        ElseIf GRIDCHGSDOUBLECLICK = True Then
            GRIDCHGS.Item(ESRNO.Index, TEMPCHGSROW).Value = Val(TXTCHGSSRNO.Text.Trim)
            GRIDCHGS.Item(ECHARGES.Index, TEMPCHGSROW).Value = CMBCHARGES.Text.Trim
            GRIDCHGS.Item(EPER.Index, TEMPCHGSROW).Value = Format(Val(TXTCHGSPER.Text.Trim), "0.00")
            GRIDCHGS.Item(EAMT.Index, TEMPCHGSROW).Value = Format(Val(TXTCHGSAMT.Text.Trim), "0.00")
            GRIDCHGS.Item(ETAXID.Index, TEMPCHGSROW).Value = Format(Val(TXTTAXID.Text.Trim))

            GRIDCHGSDOUBLECLICK = False

        End If
        TOTAL()
        TXTCHGSPER.ReadOnly = False
        GRIDCHGS.FirstDisplayedScrollingRowIndex = GRIDCHGS.RowCount - 1

        TXTCHGSSRNO.Text = Val(GRIDCHGS.RowCount - 1) + 1
        CMBCHARGES.Text = ""
        TXTCHGSPER.Clear()
        TXTCHGSAMT.Clear()
        TXTTAXID.Clear()

        If TXTCHGSPER.ReadOnly = True Then TXTCHGSPER.ReadOnly = False

        CMBCHARGES.Focus()
    End Sub

    Private Sub CMDSHOWDETAILS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSHOWDETAILS.Click
        Try
            Dim OBJRECPAY As New ShowRecPay
            OBJRECPAY.MdiParent = MDIMain

            OBJRECPAY.PURBILLINITIALS = PURREGINITIAL & "-" & TEMPBILLNO
            OBJRECPAY.SALEBILLINITIALS = PURREGINITIAL & "-" & TEMPBILLNO
            OBJRECPAY.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLDN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDN.Click
        Try
            If PBPAID.Visible = True Then
                MsgBox("Pay made, Delete Pay First", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If lbllocked.Visible = True Or PBlock.Visible = True Then
                MsgBox("Booking Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If EDIT = True Then
                Dim TEMPMSG As Integer = MsgBox("Wish to create Debit Note?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim OBJdN As New DebitNote
                    OBJdN.MdiParent = MDIMain
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" REGISTER_INITIALS AS INITIALS", "", " REGISTERMASTER ", " AND REGISTER_NAME  = '" & cmbregister.Text.Trim & "' AND REGISTER_CMPID = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                    OBJdN.BILLNO = DT.Rows(0).Item("INITIALS") & "-" & Val(TXTBILLNO.Text.Trim)
                    OBJdN.Show()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCHARGES.Enter
        Try
            If CMBCHARGES.Text.Trim = "" Then fillname(CMBCHARGES, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCHARGES.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" Then namevalidate(CMBCHARGES, CMBCODE, e, Me, TXTTRANSADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses'  or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCHGS_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDCHGS.CellDoubleClick
        Try
            If GRIDCHGS.CurrentRow.Index >= 0 And GRIDCHGS.Item(ESRNO.Index, GRIDCHGS.CurrentRow.Index).Value <> Nothing Then
                GRIDCHGSDOUBLECLICK = True
                TXTCHGSSRNO.Text = GRIDCHGS.Item(ESRNO.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                CMBCHARGES.Text = GRIDCHGS.Item(ECHARGES.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTCHGSPER.Text = GRIDCHGS.Item(EPER.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTCHGSAMT.Text = GRIDCHGS.Item(EAMT.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTTAXID.Text = GRIDCHGS.Item(ETAXID.Index, GRIDCHGS.CurrentRow.Index).Value.ToString

                TEMPCHGSROW = GRIDCHGS.CurrentRow.Index
                CMBCHARGES.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCHGS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCHGS.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDCHGS.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDCHGSDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDCHGS.Rows.RemoveAt(GRIDCHGS.CurrentRow.Index)
                getsrno(GRIDCHGS)
                TOTAL()
            ElseIf e.KeyCode = Keys.F5 Then
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSAMT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCHGSAMT.KeyPress
        Try
            AMOUNTNUMDOTKYEPRESS(e, TXTCHGSAMT, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AMOUNTNUMDOTKYEPRESS(ByVal han As KeyPressEventArgs, ByVal sen As Control, ByVal frm As System.Windows.Forms.Form)
        Try
            Dim mypos As Integer

            If AscW(han.KeyChar) >= 48 And AscW(han.KeyChar) <= 57 Or AscW(han.KeyChar) = 8 Or AscW(han.KeyChar) = 45 Then
                han.KeyChar = han.KeyChar
            ElseIf AscW(han.KeyChar) = 46 Or AscW(han.KeyChar) = 45 Then
                mypos = InStr(1, sen.Text, ".")
                If mypos = 0 Then
                    han.KeyChar = han.KeyChar
                Else
                    han.KeyChar = ""
                End If
            Else
                han.KeyChar = ""
            End If

            If AscW(han.KeyChar) = Keys.Escape Then
                frm.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSAMT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHGSAMT.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" And Val(TXTCHGSAMT.Text.Trim) <> 0 Then
                Dim dDebit As Decimal
                Dim bValid As Boolean = Decimal.TryParse(TXTCHGSAMT.Text.Trim, dDebit)
                If bValid Then
                    TXTCHGSAMT.Text = Convert.ToDecimal(Val(TXTCHGSAMT.Text))
                    ' everything is good
                    fillchgsgrid()
                    TOTAL()
                Else
                    MessageBox.Show("Invalid Number Entered")
                    'e.Cancel = True
                    TXTCHGSAMT.Clear()
                    TXTCHGSAMT.Focus()
                    Exit Sub
                End If
            Else
                If CMBCHARGES.Text.Trim = "" Then
                    MsgBox("Please Fill Charges Name ")

                ElseIf Val(TXTCHGSPER.Text.Trim) = 0 And Val(TXTCHGSAMT.Text.Trim) = 0 Then
                    MsgBox("Amount can not be zero")
                    TXTCHGSAMT.Clear()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdupload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdupload.Click
        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpg;*.png"
        OpenFileDialog1.ShowDialog()
        txtimgpath.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If txtimgpath.Text.Trim.Length <> 0 Then PBSoftCopy.ImageLocation = txtimgpath.Text.Trim
    End Sub

    Private Sub txtuploadname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtuploadname.Validating
        Try
            If txtuploadremarks.Text.Trim <> "" And txtuploadname.Text.Trim <> "" And PBSoftCopy.ImageLocation <> "" Then
                FILLUPLOAD()
            Else
                MsgBox("Enter Proper Details")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And gridupload.Item(GUSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDUPLOADDOUBLECLICK = True
                txtuploadsrno.Text = gridupload.Item(GUSRNO.Index, e.RowIndex).Value
                txtuploadremarks.Text = gridupload.Item(GUREMARKS.Index, e.RowIndex).Value
                txtuploadname.Text = gridupload.Item(GUNAME.Index, e.RowIndex).Value
                PBSoftCopy.Image = gridupload.Item(GUIMGPATH.Index, e.RowIndex).Value

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

    Private Sub gridupload_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If e.RowIndex >= 0 Then PBSoftCopy.Image = gridupload.Rows(e.RowIndex).Cells(GUIMGPATH.Index).Value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtuploadsrno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuploadsrno.GotFocus
        If GRIDUPLOADDOUBLECLICK = False Then
            If gridupload.RowCount > 0 Then
                txtuploadsrno.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(GUSRNO.Index).Value) + 1
            Else
                txtuploadsrno.Text = 1
            End If
        End If
    End Sub

    Private Sub CMDVIEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDVIEW.Click
        Try
            If gridupload.SelectedRows.Count > 0 Then
                Dim objVIEW As New ViewImage
                objVIEW.pbsoftcopy.Image = PBSoftCopy.Image
                objVIEW.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBAGENT.Enter
        Try
            If CMBAGENT.Text.Trim = "" Then fillname(CMBAGENT, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then namevalidate(CMBAGENT, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBAGENT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
                If OBJLEDGER.TEMPAGENT <> "" Then CMBAGENT.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSCITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBFROMCITY.Enter
        Try
            If CMBFROMCITY.Text.Trim = "" Then fillCITY(CMBFROMCITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSCITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBFROMCITY.Validating
        Try
            If CMBFROMCITY.Text.Trim <> "" Then CITYVALIDATE(CMBFROMCITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTOCITY.Enter
        Try
            If CMBTOCITY.Text.Trim = "" Then fillCITY(CMBTOCITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTOCITY.Validating
        Try
            If CMBTOCITY.Text.Trim <> "" Then CITYVALIDATE(CMBTOCITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCRDAYS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCRDAYS.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTCRDAYS_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTCRDAYS.Validated
        Try
            If Val(TXTCRDAYS.Text.Trim) > 0 Then DUEDATE.Value = DateAdd(DateInterval.Day, Val(TXTCRDAYS.Text.Trim), Convert.ToDateTime(DTPARTYBILLDATE.Text).Date)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtremarks_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtremarks.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJREMARKS As New SelectRemarks
                OBJREMARKS.FRMSTRING = "NARRATION"
                OBJREMARKS.ShowDialog()
                If OBJREMARKS.TEMPNAME <> "" Then txtremarks.Text = OBJREMARKS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMCITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBFROMCITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCITY As New SelectCity
                OBJCITY.FRMSTRING = "CITY"
                OBJCITY.ShowDialog()
                If OBJCITY.TEMPNAME <> "" Then CMBFROMCITY.Text = OBJCITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTOCITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCITY As New SelectCity
                OBJCITY.FRMSTRING = "CITY"
                OBJCITY.ShowDialog()
                If OBJCITY.TEMPNAME <> "" Then CMBTOCITY.Text = OBJCITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub calchgs()
        Try
            If Val(TXTCHGSPER.Text) <> 0 Then TXTCHGSAMT.Text = Format((Val(TXTBILLAMT.Text) * Val(TXTCHGSPER.Text)) / 100, "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSPER_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCHGSPER.KeyPress
        Try
            AMOUNTNUMDOTKYEPRESS(e, TXTCHGSPER, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSPER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHGSPER.Validating
        Try
            calchgs()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub filltax()
        Try
            TXTCHGSPER.ReadOnly = False
            TXTCHGSAMT.ReadOnly = False
            TXTTAXID.Text = 0
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable = OBJCMN.search(" ISNULL(tax_tax, 0) as TAX, TAX_ID AS TAXID ", "", " TAXMASTER", " AND tax_name = '" & CMBCHARGES.Text & "'  AND tax_cmpid=" & CmpId & " AND tax_LOCATIONID = " & Locationid & " AND tax_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                TXTCHGSPER.Text = dt.Rows(0).Item("TAX")
                TXTTAXID.Text = Val(dt.Rows(0).Item("TAXID"))
                'DONE BY GULKIT
                'If Val(TXTCHGSPER.Text.Trim) > 0 Then TXTCHGSAMT.ReadOnly = True
                TXTCHGSPER.ReadOnly = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBCHARGES.Validated
        Try
            If CMBCHARGES.Text.Trim <> "" Then filltax()

            'GET ADDLESS DONE BY GULKIT
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("ISNULL(LEDGERS.ACC_ADDLESS,'ADD') AS ADDLESS ", "", "LEDGERS", " AND ACC_CMPNAME = '" & CMBCHARGES.Text.Trim & "' AND ACC_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                If DT.Rows(0).Item("ADDLESS") = "LESS" Then
                    If Val(TXTCHGSPER.Text.Trim) = 0 Then TXTCHGSPER.Text = "-"
                    If Val(TXTCHGSAMT.Text.Trim) = 0 Then TXTCHGSAMT.Text = "-"
                    TXTCHGSPER.Select(TXTCHGSPER.Text.Length, 0)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub HIDEVIEW()
        Try
            LBLNAME.Text = "Supplier Name"
            LBLMILL.Text = "Mill Name"
            LBLMILL.Visible = True
            CMBMILL.Visible = True


            LBLFROMDATE.Visible = False
            FROMDATE.Visible = False
            TXTLOTNO.Visible = False
            LBLLOTNO.Visible = False
            LBLTILDATE.Visible = False
            TILLDATE.Visible = False

            LBLSACDESC.Visible = False
            CMBSACDESC.Visible = False
            LBLSACCODE.Visible = False
            TXTSACCODE.Visible = False

            CMDSELECTDATA.Visible = True
            CMDSELECTDATA.Enabled = True
            cmdok.Left = 493
            cmdclear.Left = 587
            cmddelete.Left = 446
            cmdexit.Left = 541
            GRIDBILL.Width = 1090

            TBITEMDETAILS.Visible = True
            BTNQUALITY.Visible = True
            BTNBAGS.Visible = True
            BTNBAGS.Text = "Bags"
            BTNWT.Visible = True
            BTNWT.Text = "Wt"
            BTNRATE.Visible = True
            BTNNETT.Visible = True
            BTNPER.Visible = True
            BTNAMT.Visible = True
            BTNTRANS.Visible = True
            BTNLRNO.Visible = True
            BTNLRNO.Text = "LR No."
            BTNLRDATE.Visible = True
            BTNLRDATE.Text = "LR Date"

            BTNBEAMS.Visible = False
            BTNNARRATION.Visible = False
            BTNENDS.Visible = False
            BTNTL.Visible = False

            BTNHSNCODE.Visible = True

            GQUALITY.Visible = True
            gbags.Visible = True
            gwt.Visible = True
            GRATE.Visible = True
            GNETTRATE.Visible = True
            GPER.Visible = True
            GAMT.Visible = True
            GTRANSPORT.Visible = True
            GLRNO.Visible = True
            GGRIDLRDATE.Visible = True

            GBEAMS.Visible = False
            GNARRATION.Visible = False
            GENDS.Visible = False
            GTL.Visible = False

            GHSNCODE.Visible = True

            BTNHSNCODE.Visible = True
            BTNQUALITY.Left = Button1.Left + Button1.Width
            BTNBAGS.Left = BTNQUALITY.Left + BTNQUALITY.Width
            BTNWT.Left = BTNBAGS.Left + BTNBAGS.Width
            BTNRATE.Left = BTNWT.Left + BTNWT.Width
            BTNNETT.Left = BTNRATE.Left + BTNRATE.Width
            BTNPER.Left = BTNNETT.Left + BTNNETT.Width
            BTNAMT.Left = BTNPER.Left + BTNPER.Width
            BTNTRANS.Left = BTNAMT.Left + BTNAMT.Width
            BTNLRNO.Left = BTNTRANS.Left + BTNTRANS.Width
            BTNLRDATE.Left = BTNLRNO.Left + BTNLRNO.Width

            BTNBEAMS.Left = BTNLRDATE.Left + BTNLRDATE.Width
            BTNNARRATION.Left = BTNBEAMS.Left + BTNBEAMS.Width
            BTNENDS.Left = BTNNARRATION.Left + BTNNARRATION.Width
            BTNTL.Left = BTNENDS.Left + BTNENDS.Width

            BTNHSNCODE.Left = BTNLRDATE.Left + BTNLRDATE.Width

            TXTBILLAMT.ReadOnly = True
            TXTBILLAMT.BackColor = Color.Linen
            TXTBILLAMT.TabStop = False

            If PURTYPE = "YARN PURCHASE" Then
                Me.Text = "Yarn Purchase Invoice"
                CHKSPLIT.Visible = True

            ElseIf PURTYPE = "GREY PURCHASE" Then
                LBLMILL.Visible = True
                LBLMILL.Text = "Processor Name"
                CMBMILL.Visible = True
                Me.Text = "Grey Purchase Invoice"

                BTNBAGS.Text = "Pcs"
                BTNWT.Text = "Mtrs"
                BTNTRANS.Visible = False
                BTNLRNO.Visible = False
                BTNLRDATE.Visible = False


                GTRANSPORT.Visible = False
                GLRNO.Visible = False
                GGRIDLRDATE.Visible = False
                GRIDBILL.Width -= GTRANSPORT.Width
                GRIDBILL.Width -= GLRNO.Width
                GRIDBILL.Width -= GGRIDLRDATE.Width

                BTNHSNCODE.Left = BTNAMT.Left + BTNAMT.Width
                CHKSPLIT.Visible = True

            ElseIf PURTYPE = "FINISHED PURCHASE" Then
                LBLMILL.Visible = False
                CMBMILL.Visible = False
                Me.Text = "Finished Purchase Invoice"

                BTNBAGS.Text = "Taka"
                BTNWT.Text = "Mtrs"
                BTNTRANS.Visible = False
                BTNLRNO.Visible = True
                BTNLRNO.Text = "Bale No"
                BTNLRNO.Left = BTNTRANS.Left
                BTNLRDATE.Visible = False

                GTRANSPORT.Visible = False
                GLRNO.Visible = True
                GGRIDLRDATE.Visible = False
                GRIDBILL.Width -= GTRANSPORT.Width
                GRIDBILL.Width -= GGRIDLRDATE.Width

                BTNHSNCODE.Left = BTNLRNO.Left + BTNLRNO.Width

            ElseIf PURTYPE = "COMMON PURCHASE" Then
                LBLMILL.Visible = False
                CMBMILL.Visible = False
                Me.Text = "Other Purchase Invoice"

                TBITEMDETAILS.Visible = False
                TXTBILLAMT.ReadOnly = False
                TXTBILLAMT.BackColor = Color.LemonChiffon
                TXTBILLAMT.TabStop = True
                CMDSELECTDATA.Enabled = False
                CMDSELECTDATA.Visible = False
                cmdok.Left = cmddelete.Left
                cmdclear.Left = cmdexit.Left

                LBLSACDESC.Visible = True
                CMBSACDESC.Visible = True
                LBLSACCODE.Visible = True
                TXTSACCODE.Visible = True

            ElseIf PURTYPE = "PROCESS CHGS" Then
                LBLMILL.Visible = False
                CMBMILL.Visible = False
                Me.Text = "Process Charges Invoice"
                LBLNAME.Text = "Processor Name"

                LBLSACDESC.Visible = True
                CMBSACDESC.Visible = True
                LBLSACCODE.Visible = True
                TXTSACCODE.Visible = True

                BTNBAGS.Text = "Taka"
                BTNWT.Text = "Mtrs"
                BTNTRANS.Visible = False
                BTNLRNO.Visible = True
                BTNLRNO.Text = "Bale No"
                BTNLRNO.Left = BTNTRANS.Left
                BTNLRDATE.Visible = False
                BTNHSNCODE.Visible = False

                GTRANSPORT.Visible = False
                GLRNO.Visible = True
                GGRIDLRDATE.Visible = False
                GHSNCODE.Visible = False
                GRIDBILL.Width -= GTRANSPORT.Width
                GRIDBILL.Width -= GGRIDLRDATE.Width
                GRIDBILL.Width -= GHSNCODE.Width

                LBLLOTNO.Visible = True
                TXTLOTNO.Visible = True

            ElseIf PURTYPE = "SIZING CHGS" Or PURTYPE = "WARPER CHGS" Then
                LBLMILL.Visible = False
                CMBMILL.Visible = False

                If PURTYPE = "SIZING CHGS" Then
                    Me.Text = "Sizer Charges Invoice"
                    LBLNAME.Text = "Sizer Name"
                Else
                    Me.Text = "Warping Charges Invoice"
                    LBLNAME.Text = "Warper Name"
                End If


                LBLSACDESC.Visible = True
                CMBSACDESC.Visible = True
                LBLSACCODE.Visible = True
                TXTSACCODE.Visible = True

                BTNQUALITY.Visible = False
                BTNBAGS.Text = "Cut"
                BTNTRANS.Visible = False
                BTNLRNO.Visible = True
                BTNLRNO.Text = "Ch. No"
                BTNLRDATE.Text = "Ch. Date"
                BTNLRDATE.Visible = True
                BTNBEAMS.Visible = True
                BTNNARRATION.Visible = True
                BTNENDS.Visible = True
                BTNTL.Visible = True
                BTNHSNCODE.Visible = False

                GQUALITY.Visible = False
                GTRANSPORT.Visible = False
                GBEAMS.Visible = True
                GNARRATION.Visible = True
                GENDS.Visible = True
                GTL.Visible = True
                GHSNCODE.Visible = False

                GRIDBILL.Width -= GQUALITY.Width
                GRIDBILL.Width -= GTRANSPORT.Width
                GRIDBILL.Width += GBEAMS.Width
                GRIDBILL.Width += GNARRATION.Width
                GRIDBILL.Width += GENDS.Width
                GRIDBILL.Width += GTL.Width
                GRIDBILL.Width -= GHSNCODE.Width

                BTNBAGS.Left = BTNQUALITY.Left
                BTNWT.Left = BTNBAGS.Left + BTNBAGS.Width
                BTNRATE.Left = BTNWT.Left + BTNWT.Width
                BTNNETT.Left = BTNRATE.Left + BTNRATE.Width
                BTNPER.Left = BTNNETT.Left + BTNNETT.Width
                BTNAMT.Left = BTNPER.Left + BTNPER.Width
                BTNLRNO.Left = BTNAMT.Left + BTNAMT.Width
                BTNLRDATE.Left = BTNLRNO.Left + BTNLRNO.Width
                BTNBEAMS.Left = BTNLRDATE.Left + BTNLRDATE.Width
                BTNNARRATION.Left = BTNBEAMS.Left + BTNBEAMS.Width
                BTNENDS.Left = BTNNARRATION.Left + BTNNARRATION.Width
                BTNTL.Left = BTNENDS.Left + BTNENDS.Width

                LBLLOTNO.Visible = False
                TXTLOTNO.Visible = False
                LBLTILDATE.Visible = True
                TILLDATE.Visible = True

                CMDSELECTDATA.Enabled = False
                CMDSELECTDATA.Visible = False
                cmdok.Left = cmddelete.Left
                cmdclear.Left = cmdexit.Left
                gbags.ReadOnly = False
                gwt.ReadOnly = False

            ElseIf PURTYPE = "WEAVING CHGS" Then
                LBLMILL.Visible = False
                CMBMILL.Visible = False
                Me.Text = "Weaving Charges Invoice"
                LBLNAME.Text = "Weaver Name"

                LBLSACDESC.Visible = True
                CMBSACDESC.Visible = True
                LBLSACCODE.Visible = True
                TXTSACCODE.Visible = True

                BTNBAGS.Text = "Pcs"
                BTNWT.Text = "Mtrs"
                BTNTRANS.Visible = False
                BTNLRNO.Visible = False
                BTNLRDATE.Visible = False
                BTNHSNCODE.Visible = False

                GTRANSPORT.Visible = False
                GLRNO.Visible = False
                GGRIDLRDATE.Visible = False
                GHSNCODE.Visible = False
                GRIDBILL.Width -= GTRANSPORT.Width
                GRIDBILL.Width -= GLRNO.Width
                GRIDBILL.Width -= GGRIDLRDATE.Width
                GRIDBILL.Width -= GHSNCODE.Width

                LBLFROMDATE.Visible = True
                FROMDATE.Visible = True
                LBLTILDATE.Visible = True
                TILLDATE.Visible = True

                CMDSELECTDATA.Enabled = False
                CMDSELECTDATA.Visible = False
                cmdok.Left = cmddelete.Left
                cmdclear.Left = cmdexit.Left
                gbags.ReadOnly = False
                gwt.ReadOnly = False

            ElseIf PURTYPE = "TRANSPORT CHGS" Then

                LBLSACDESC.Visible = True
                CMBSACDESC.Visible = True
                LBLSACCODE.Visible = True
                TXTSACCODE.Visible = True

                BTNHSNCODE.Visible = False

                GHSNCODE.Visible = False
                GRIDBILL.Width -= GHSNCODE.Width
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPARTYBILLNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTPARTYBILLNO.Validating
        Try
            If TXTPARTYBILLNO.Text.Trim <> "" Then
                If (EDIT = False) Or (EDIT = True And LCase(TEMPPARTYBILLNO) <> LCase(TXTPARTYBILLNO.Text.Trim)) Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" BILL_INITIALS AS BILLNO", "", " PURCHASEMASTER INNER JOIN LEDGERS ON PURCHASEMASTER.BILL_LEDGERID = LEDGERS.Acc_id AND PURCHASEMASTER.BILL_CMPID = LEDGERS.Acc_cmpid AND PURCHASEMASTER.BILL_LOCATIONID = LEDGERS.Acc_locationid AND PURCHASEMASTER.BILL_YEARID = LEDGERS.Acc_yearid ", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND BILL_PARTYBILLNO = '" & TXTPARTYBILLNO.Text.Trim & "' AND BILL_CMPID = " & CmpId & " AND BILL_LOCATIONID = " & Locationid & " AND BILL_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        MsgBox("Party Bill No Already Exists in Entry No " & DT.Rows(0).Item("BILLNO"))
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then fillname(CMBMILL, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBMILL.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILL.Text = OBJLEDGER.TEMPNAME
                'If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then namevalidate(CMBMILL, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTPARTYBILLDATE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPARTYBILLDATE.Validated
        BILLDATE.Text = DTPARTYBILLDATE.Text
        If DTPARTYBILLDATE.Text <> "__/__/____" Then DUEDATE.Value = DTPARTYBILLDATE.Text
        TOTAL()
    End Sub

    Private Sub CMBCHARGES_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCHARGES.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBCHARGES.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTPARTYBILLDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPARTYBILLDATE.GotFocus
        DTPARTYBILLDATE.Select(0, 0)
    End Sub

    Private Sub DTPARTYBILLDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTPARTYBILLDATE.Validating
        Try
            If DTPARTYBILLDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTPARTYBILLDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BILLDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BILLDATE.GotFocus
        BILLDATE.Select(0, 0)
    End Sub

    Private Sub BILLDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles BILLDATE.Validating
        Try
            If BILLDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(BILLDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else
                    If Val(TXTCRDAYS.Text) = 0 Then DUEDATE.Text = BILLDATE.Text
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSPORT_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTRANSPORT.Enter
        Try
            If CMBTRANSPORT.Text.Trim = "" Then fillname(CMBTRANSPORT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSPORT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTRANSPORT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'TRANSPORT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBTRANSPORT.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSPORT_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTRANSPORT.Validated
        Try
            If ClientName <> "JASHOK" And CMBTRANSPORT.Text.Trim <> "" Then
                For Each ROW As DataGridViewRow In GRIDBILL.Rows
                    ROW.Cells(GTRANSPORT.Index).Value = CMBTRANSPORT.Text.Trim
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSPORT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANSPORT.Validating
        Try
            If CMBTRANSPORT.Text.Trim <> "" Then namevalidate(CMBTRANSPORT, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDREMOVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREMOVE.Click
        Try
            PBSoftCopy.Image = Nothing
            txtimgpath.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBILLAMT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTBILLAMT.Validating
        If PURTYPE = "COMMON PURCHASE" Then TOTAL()
    End Sub

    Private Sub TXTLOTNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTLOTNO.Validating
        Try
            If PURTYPE = "PROCESS CHGS" And Val(TXTLOTNO.Text.Trim) > 0 And cmbname.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon()
                'INITIALLY WE WERE FETCHING ONLY THOSE BALES WHOSE PURDONE = FALSE, BUT WE HAVE REMOVED THAT COZ SOMETIMES WE GET 2 BILLS FOR SALE ENTRY
                'THAT TIME IT IS DIFFICULT TO MANAGE
                'Dim DT2 As DataTable = OBJCMN.search("  GREYQUALITYMASTER.GREY_NAME AS QUALITY, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_TAKA AS TAKA, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_MTRS AS MTRS, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_BALENO AS BALENO  ", "", " GREYRECEIVEDPROCESSING INNER JOIN LEDGERS ON GREYRECEIVEDPROCESSING.GRECDPROCESSOR_LEDGERID = LEDGERS.Acc_id INNER JOIN GREYRECEIVEDPROCESSING_DESC ON GREYRECEIVEDPROCESSING.GRECDPROCESSOR_NO = GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_NO AND GREYRECEIVEDPROCESSING.GRECDPROCESSOR_YEARID = GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_YEARID INNER JOIN GREYQUALITYMASTER ON GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_GREYID = GREYQUALITYMASTER.GREY_ID  ", " and (ISNULL(GREYRECEIVEDPROCESSING.GRECDPROCESSOR_PURDONE, 0) = 0) AND GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_LOTNO=" & Val(TXTLOTNO.Text.Trim) & " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND GREYRECEIVEDPROCESSING.GRECDPROCESSOR_YEARID = " & YearId)
                Dim DT2 As DataTable = OBJCMN.search("  GREYQUALITYMASTER.GREY_NAME AS QUALITY, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_TAKA AS TAKA, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_MTRS AS MTRS, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_BALENO AS BALENO , ISNULL(GREYRECEIVEDPROCESSING.GRECDPROCESSOR_PURDONE, 0)  AS PURDONE ", "", " GREYRECEIVEDPROCESSING INNER JOIN LEDGERS ON GREYRECEIVEDPROCESSING.GRECDPROCESSOR_LEDGERID = LEDGERS.Acc_id INNER JOIN GREYRECEIVEDPROCESSING_DESC ON GREYRECEIVEDPROCESSING.GRECDPROCESSOR_NO = GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_NO AND GREYRECEIVEDPROCESSING.GRECDPROCESSOR_YEARID = GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_YEARID INNER JOIN GREYQUALITYMASTER ON GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_GREYID = GREYQUALITYMASTER.GREY_ID  ", "  AND GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_LOTNO=" & Val(TXTLOTNO.Text.Trim) & " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND GREYRECEIVEDPROCESSING.GRECDPROCESSOR_YEARID = " & YearId)
                If DT2.Rows.Count > 0 Then

                    'WE WILL JUST INTIMATE THAT THE ENTRY IS ALREADY DONE
                    If Convert.ToBoolean(DT2.Rows(0).Item("PURDONE")) = True Then MsgBox("Bill of this lot is already made, Please check before proceeding", MsgBoxStyle.Critical)

                    cmbname.Enabled = False
                    TXTLOTNO.Enabled = False
                    For Each dr As DataRow In DT2.Rows
                        GRIDBILL.Rows.Add(0, dr("QUALITY"), Format(Val(dr("TAKA")), "0"), Format(Val(dr("MTRS")), "0.00"), "0.00", 0, "MTRS", "0.00", "", dr("BALENO"), "", 0, "", 0, 0, "", 0, 0, 0)
                    Next
                    GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1
                    getsrno(GRIDBILL)
                    If GRIDBILL.RowCount > 0 Then
                        GRIDBILL.Focus()
                        GRIDBILL.CurrentCell = GRIDBILL.Rows(0).Cells(GRATE.Index)
                    End If
                    TOTAL()
                Else
                    MsgBox("Invalid Lot No", MsgBoxStyle.Critical)
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKTCS_CheckedChanged(sender As Object, e As EventArgs) Handles CHKTCS.CheckedChanged
        TOTAL()
    End Sub

    Private Sub CHKMANUALTCS_CheckedChanged(sender As Object, e As EventArgs) Handles CHKMANUALTCS.CheckedChanged
        If CHKMANUALTCS.Checked = True Then
            TXTTCSAMT.ReadOnly = False
            TXTTCSAMT.BackColor = Color.LemonChiffon
        Else
            TXTTCSAMT.ReadOnly = True
            TXTTCSAMT.BackColor = Color.Linen
            TOTAL()
        End If
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

                If PURTYPE = "SIZING CHGS" Or PURTYPE = "WARPER CHGS" Then
                    'FIRST CHECK WHETHER TILL DATE IS ALREADY TAKEN IN PURCHASE INVOICE OR NOT
                    Dim TEMPFROMDATE As Date = AccFrom.Date
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search("MAX(CAST(BILL_TILLDATE AS DATE)) AS DATE", "", "PURCHASEMASTER INNER JOIN LEDGERS ON ACC_ID = BILL_LEDGERID ", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND BILL_PURTYPE = '" & PURTYPE & "' AND BILL_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        If IsDBNull(DT.Rows(0).Item("DATE")) = True Then
                            TEMPFROMDATE = AccFrom.Date.AddDays(-1)
                            GoTo LINE1
                        Else
                            TEMPFROMDATE = DT.Rows(0).Item("DATE")
                        End If
                        If DT.Rows(0).Item("DATE") >= Format(Convert.ToDateTime(TILLDATE.Text).Date, "dd/MM/yyyy") Then
                            MsgBox("Till Date Already Taken, Please check before proceeding", MsgBoxStyle.Critical)
                            TEMPFROMDATE = AccFrom.Date
                            DT = OBJCMN.search("MAX(CAST(BILL_TILLDATE AS DATE)) AS DATE", "", "PURCHASEMASTER INNER JOIN LEDGERS ON ACC_ID = BILL_LEDGERID ", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND BILL_PURTYPE = '" & PURTYPE & "' AND CAST(BILL_TILLDATE AS DATE) <' " & Format(Convert.ToDateTime(TILLDATE.Text).Date, "MM/dd/yyyy") & "' And BILL_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then TEMPFROMDATE = DT.Rows(0).Item("DATE")
                            'e.Cancel = True
                            'Exit Sub
                        End If
                    End If

LINE1:

                    'IF DATE IS CORRECT THEN FETCH ALL SIZING / WARPING DATA
                    If PURTYPE = "SIZING CHGS" Then
                        DT = OBJCMN.search(" BEAMRECEIVED.BEAMREC_TOTALCUT As CUT, BEAMRECEIVED.BEAMREC_TOTALWT As WT, BEAMREC_CHALLANNO As CHALLANNO, BEAMREC_CHALLANDATE As CHALLANDATE, BEAMRECEIVED.BEAMREC_TOTALBEAM As BEAMS, BEAMRECEIVED.BEAMREC_FROMTOBEAM As NARR, BeamMaster.BEAM_ENDS As ENDS, BeamMaster.BEAM_TAPLINE AS TL ", "", " BEAMRECEIVED INNER JOIN BEAMRECEIVED_DESC ON BEAMRECEIVED.BEAMREC_NO = BEAMRECEIVED_DESC.BEAMREC_NO And BEAMRECEIVED.BEAMREC_YEARID = BEAMRECEIVED_DESC.BEAMREC_YEARID INNER JOIN BEAMMASTER ON BEAMRECEIVED_DESC.BEAMREC_BEAMID = BEAMMASTER.BEAM_ID INNER JOIN LEDGERS ON Acc_id = BEAMREC_LEDGERID ", " And LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND BEAMREC_DATE > '" & Format(TEMPFROMDATE.Date, "MM/dd/yyyy") & "' AND BEAMREC_DATE <= '" & Format(Convert.ToDateTime(TILLDATE.Text).Date, "MM/dd/yyyy") & "' AND BEAMRECEIVED.BEAMREC_YEARID = " & YearId & " GROUP BY BEAMRECEIVED.BEAMREC_TOTALCUT, BEAMRECEIVED.BEAMREC_TOTALWT, BEAMRECEIVED.BEAMREC_CHALLANNO, BEAMRECEIVED.BEAMREC_CHALLANDATE, BEAMRECEIVED.BEAMREC_TOTALBEAM, BEAMRECEIVED.BEAMREC_FROMTOBEAM, BEAMMASTER.BEAM_ENDS, BEAMMASTER.BEAM_TAPLINE,BEAMREC_DATE  ORDER BY BEAMREC_DATE , CHALLANNO")
                    Else
                        DT = OBJCMN.search(" ROLLRECEIVED.ROLLRECD_CUT AS CUT, (ROLLRECEIVED.ROLLRECD_TOTALWT + ISNULL(ROLLRECEIVED.ROLLRECD_LONGATION,0)) AS WT, ROLLRECD_CHALLANNO AS CHALLANNO, ROLLRECD_CHALLANDATE AS CHALLANDATE,  0 AS BEAMS, 'From ' + ROLLRECEIVED.ROLLRECD_BEAMFROM + ' To ' + ROLLRECEIVED.ROLLRECD_BEAMTO AS NARR, BEAMMASTER.BEAM_ENDS AS ENDS, BEAMMASTER.BEAM_TAPLINE AS TL ", "", " ROLLRECEIVED INNER JOIN ROLLRECEIVED_DESC ON ROLLRECEIVED.ROLLRECD_NO = ROLLRECEIVED_DESC.ROLLRECD_NO AND ROLLRECEIVED.ROLLRECD_YEARID = ROLLRECEIVED_DESC.ROLLRECD_YEARID INNER JOIN BEAMMASTER ON ROLLRECEIVED.ROLLRECD_BEAMID = BEAMMASTER.BEAM_ID INNER JOIN LEDGERS ON Acc_id = ROLLRECD_WARPERID", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ROLLRECD_DATE > '" & Format(TEMPFROMDATE.Date, "MM/dd/yyyy") & "' AND ROLLRECD_DATE <= '" & Format(Convert.ToDateTime(TILLDATE.Text).Date, "MM/dd/yyyy") & "' AND ROLLRECEIVED.ROLLRECD_YEARID = " & YearId & " GROUP BY ROLLRECEIVED.ROLLRECD_CUT, ROLLRECEIVED.ROLLRECD_TOTALWT + ISNULL(ROLLRECEIVED.ROLLRECD_LONGATION,0), ROLLRECEIVED.ROLLRECD_CHALLANNO, ROLLRECEIVED.ROLLRECD_CHALLANDATE, ROLLRECEIVED.ROLLRECD_BEAMFROM, ROLLRECD_BEAMTO, BEAMMASTER.BEAM_ENDS, BEAMMASTER.BEAM_TAPLINE,ROLLRECD_DATE  ORDER BY ROLLRECD_DATE , CHALLANNO")
                    End If

                    If DT.Rows.Count > 0 Then
                        For Each DTROW As DataRow In DT.Rows
                            GRIDBILL.Rows.Add(0, "", Val(DTROW("CUT")), Val(DTROW("WT")), 0, 0, "MTRS", 0, "", DTROW("CHALLANNO"), DTROW("CHALLANDATE"), Val(DTROW("BEAMS")), DTROW("NARR"), Val(DTROW("ENDS")), Val(DTROW("TL")), "", 0, 0, 0)
                        Next
                        TILLDATE.Enabled = False
                        GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1
                        getsrno(GRIDBILL)
                        If GRIDBILL.RowCount > 0 Then
                            GRIDBILL.Focus()
                            GRIDBILL.CurrentCell = GRIDBILL.Rows(0).Cells(GRATE.Index)
                        End If
                        TOTAL()
                    End If



                ElseIf PURTYPE = "WEAVING CHGS" Then

                    'IF DATE IS CORRECT THEN FETCH ALL WEAVING DATA
                    Dim OBJCMN As New ClsCommon
                    Dim PER As String = "MTRS"
                    'Dim DT As DataTable = OBJCMN.search("  GREYQUALITYMASTER.GREY_NAME AS GREYQUALITY, SUM(GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_PCS) AS TAKA, SUM(GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_MTRS) AS MTRS  ", "", " GREYRECEIVEDWEAVER INNER JOIN GREYRECEIVEDWEAVER_DESC ON GREYRECEIVEDWEAVER.GRECDWEAVER_NO = GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_NO AND GREYRECEIVEDWEAVER.GRECDWEAVER_YEARID = GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_YEARID INNER JOIN LEDGERS ON GREYRECEIVEDWEAVER.GRECDWEAVER_LEDGERID = LEDGERS.Acc_id INNER JOIN GREYQUALITYMASTER ON GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_GREYID = GREYQUALITYMASTER.GREY_ID  ", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND GRECDWEAVER_DATE >= '" & Format(Convert.ToDateTime(FROMDATE.Text).Date, "MM/dd/yyyy") & "' AND GRECDWEAVER_DATE <= '" & Format(Convert.ToDateTime(TILLDATE.Text).Date, "MM/dd/yyyy") & "' AND GREYQUALITYMASTER.GREY_YEARID = " & YearId & " GROUP BY GREYQUALITYMASTER.GREY_NAME")
                    Dim DT As DataTable = OBJCMN.search("*", "", " (SELECT GREYQUALITYMASTER.GREY_NAME AS GREYQUALITY, SUM(GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_PCS) AS TAKA, SUM(GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_MTRS) AS MTRS, ISNULL(GREYQUALITYMASTER.GREY_RATE,0) AS RATE FROM GREYRECEIVEDWEAVER INNER JOIN GREYRECEIVEDWEAVER_DESC ON GREYRECEIVEDWEAVER.GRECDWEAVER_NO = GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_NO AND GREYRECEIVEDWEAVER.GRECDWEAVER_YEARID = GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_YEARID INNER JOIN LEDGERS ON GREYRECEIVEDWEAVER.GRECDWEAVER_LEDGERID = LEDGERS.Acc_id INNER JOIN GREYQUALITYMASTER ON GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_GREYID = GREYQUALITYMASTER.GREY_ID WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND GRECDWEAVER_DATE >= '" & Format(Convert.ToDateTime(FROMDATE.Text).Date, "MM/dd/yyyy") & "' AND GRECDWEAVER_DATE <= '" & Format(Convert.ToDateTime(TILLDATE.Text).Date, "MM/dd/yyyy") & "' AND GREYQUALITYMASTER.GREY_YEARID = " & YearId & " GROUP BY GREYQUALITYMASTER.GREY_NAME, ISNULL(GREYQUALITYMASTER.GREY_RATE,0) UNION ALL SELECT GREYQUALITYMASTER.GREY_NAME AS GREYQUALITY, SUM(GREYRECDWEAVERSUMMARY_DESC.GREYRECDSUMM_TAKA) AS TAKA, SUM(GREYRECDWEAVERSUMMARY_DESC.GREYRECDSUMM_MTRS) AS MTRS, ISNULL(GREYQUALITYMASTER.GREY_RATE,0) FROM GREYRECDWEAVERSUMMARY INNER JOIN GREYRECDWEAVERSUMMARY_DESC ON GREYRECDWEAVERSUMMARY.GREYRECDSUMM_NO = GREYRECDWEAVERSUMMARY_DESC.GREYRECDSUMM_NO AND GREYRECDWEAVERSUMMARY.GREYRECDSUMM_YEARID = GREYRECDWEAVERSUMMARY_DESC.GREYRECDSUMM_YEARID INNER JOIN LEDGERS ON GREYRECDWEAVERSUMMARY.GREYRECDSUMM_LEDGERID = LEDGERS.Acc_id INNER JOIN GREYQUALITYMASTER ON GREYRECDWEAVERSUMMARY_DESC.GREYRECDSUMM_GREYID = GREYQUALITYMASTER.GREY_ID WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND GREYRECDSUMM_DATE >= '" & Format(Convert.ToDateTime(FROMDATE.Text).Date, "MM/dd/yyyy") & "' AND GREYRECDSUMM_DATE <= '" & Format(Convert.ToDateTime(TILLDATE.Text).Date, "MM/dd/yyyy") & "' AND GREYQUALITYMASTER.GREY_YEARID = " & YearId & " GROUP BY GREYQUALITYMASTER.GREY_NAME, ISNULL(GREYQUALITYMASTER.GREY_RATE,0)) AS T", "")
                    If DT.Rows.Count > 0 Then
                        If ClientName <> "SASHWINKUMAR" Then PER = "PCS"
                        For Each DTROW As DataRow In DT.Rows
                            GRIDBILL.Rows.Add(0, DTROW("GREYQUALITY"), Val(DTROW("TAKA")), Val(DTROW("MTRS")), Val(DTROW("RATE")), 0, PER, 0, "", 0, "", 0, "", 0, 0, "", 0, 0, 0)
                        Next

                        TXTREFNO.Text = cmbname.Text.Trim
                        FROMDATE.Enabled = False
                        TILLDATE.Enabled = False
                        GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1
                        getsrno(GRIDBILL)
                        If GRIDBILL.RowCount > 0 Then
                            GRIDBILL.Focus()
                            GRIDBILL.CurrentCell = GRIDBILL.Rows(0).Cells(GRATE.Index)
                        End If
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

    Private Sub TOOLEWB_Click(sender As Object, e As EventArgs) Handles TOOLEWB.Click
        Try
            If EDIT = False Then Exit Sub
            GENERATEEWB()
            PRINTEWB()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GENERATEEWB()
        Try
            If ALLOWEWAYBILL = False Then Exit Sub
            If cmbname.Text.Trim = "" Then Exit Sub
            If EDIT = False Then Exit Sub

            If Val(TXTCGSTAMT.Text.Trim) = 0 And Val(TXTSGSTAMT.Text.Trim) = 0 And Val(TXTIGSTAMT.Text.Trim) = 0 Then Exit Sub

            If CMBFROMCITY.Text.Trim = "" Then
                MsgBox("Enter From City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If CMBTOCITY.Text.Trim = "" Then
                MsgBox("Enter to City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Generate E-Way Bill?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If TXTEWAYBILLNO.Text.Trim <> "" Then
                MsgBox("E-Way Bill No Already Generated", MsgBoxStyle.Critical)
                Exit Sub
            End If

            MsgBox("E-Way Bill will not be Generated if there are special characters like {*,/,""""} in Quality Name ", MsgBoxStyle.Critical)

            'BEFORE GENERATING EWAY BILL WE NEED TO VALIDATE WHETHER ALL THE DATA ARE PRESENT OR NOT
            'IF DATA IS NOT PRESENT THEN VALIDATE
            'DATA TO BE CHECKED 
            '   1)CMPEWBUSER | CMPEWBPASS | CMPGSTIN | CMPPINCODE | CMPCITY | CMPSTATE | 
            '   2)PARTYGSTIN | PARTYCITY | PARTYPINCODE | PARTYSTATE | PARTYSTATECODE | PARTYKMS
            '   3)CGST OR SGST OR IGST (ALWAYS USE MTR IN QTYUNIT)
            If CMPEWBUSER = "" Or CMPEWBPASS = "" Or CMPGSTIN = "" Or CMPPINCODE = "" Or CMPCITYNAME = "" Or CMPSTATENAME = "" Then
                MsgBox(" Company Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim TEMPCMPADD1 As String = ""
            Dim TEMPCMPADD2 As String = ""
            Dim PARTYGSTIN As String = ""
            Dim PARTYPINCODE As String = ""
            Dim PARTYSTATECODE As String = ""
            Dim PARTYSTATENAME As String = ""
            Dim SHIPTOGSTIN As String = ""
            Dim SHIPTOSTATECODE As String = ""
            Dim SHIPTOSTATENAME As String = ""
            Dim PARTYKMS As Double = 0
            Dim PARTYADD1 As String = ""
            Dim PARTYADD2 As String = ""
            Dim TRANSGSTIN As String = ""
            Dim GODOWNPINCODE As String = ""
            Dim GODOWNKMS As Double = 0
            Dim GODOWNSTATENAME As String = ""
            Dim GODOWNSTATECODE As String = ""


            Dim OBJCMN As New ClsCommon
            'CMP ADDRESS DETAILS
            Dim DT As DataTable = OBJCMN.search(" ISNULL(CMP_DISPATCHFROM, '') AS ADD1, ISNULL(CMP_ADD2,'') AS ADD2 ", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
            TEMPCMPADD1 = DT.Rows(0).Item("ADD1")
            TEMPCMPADD2 = DT.Rows(0).Item("ADD2")


            'PARTY GST DETAILS 
            DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN, ISNULL(ACC_ZIPCODE,'') AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_YEARID = " & YearId)
            If DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Then
                MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            Else
                PARTYGSTIN = DT.Rows(0).Item("GSTIN")
                SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                PARTYSTATENAME = DT.Rows(0).Item("STATENAME")
                PARTYSTATECODE = DT.Rows(0).Item("STATECODE")
                SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                PARTYPINCODE = DT.Rows(0).Item("PINCODE")
                PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                PARTYADD1 = DT.Rows(0).Item("ADD1")
                PARTYADD2 = DT.Rows(0).Item("ADD2")
            End If


            'FETCH PINCODE / KMS / ADD1 / ADD2 OF SHIPTO IF IT IS NOT SAME AS CMBNAME
            If cmbGodown.Text.Trim <> "" Then
                DT = OBJCMN.search(" ISNULL(GODOWN_PINCODE,'') AS PINCODE, ISNULL(GODOWN_KMS,0) AS KMS, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE ", "", " GODOWNMASTER LEFT OUTER JOIN STATEMASTER ON GODOWN_STATEID = STATE_ID ", " AND GODOWN_NAME = '" & cmbGodown.Text.Trim & "' AND GODOWN_YEARID = " & YearId)
                If DT.Rows(0).Item("PINCODE") = "" Or Val(DT.Rows(0).Item("KMS")) = 0 Then
                    MsgBox(" Godown Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    GODOWNPINCODE = DT.Rows(0).Item("PINCODE")
                    GODOWNKMS = Val(DT.Rows(0).Item("KMS"))
                    GODOWNSTATENAME = DT.Rows(0).Item("STATENAME")
                    GODOWNSTATECODE = DT.Rows(0).Item("STATECODE")
                End If
            End If




            'TRANSPORT GSTIN IF TRANSPORT IS PRESENT
            If CMBTRANSPORT.Text.Trim <> "" Then
                DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & CMBTRANSPORT.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then TRANSGSTIN = DT.Rows(0).Item("GSTIN")
                If TRANSGSTIN = "" Then
                    MsgBox("Enter Transport GSTIN", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If


            'CHECKING COUNTER AND VALIDATE WHETHER EWAY BILL WILL BE ALLOWED OR NOT, FOR EACH EWAY BILL WE NEED TO 2 API COUNTS (1 FOR TOKEN AND ANOTHER FOR EWB)
            If CMPEWAYCOUNTER = 0 Then
                MsgBox("EWay Bill Package has Expired, Kindly contact Nakoda Infotech on +919987603607", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'GET USED EWAYCOUNTER
            Dim USEDEWAYCOUNTER As Integer = 0
            DT = OBJCMN.search("COUNT(COUNTERID) AS EWAYCOUNT", "", "EWAYENTRY", " AND CMPID =" & CmpId)
            If DT.Rows.Count > 0 Then USEDEWAYCOUNTER = Val(DT.Rows(0).Item("EWAYCOUNT"))

            'IF COUNTERS ARE FINISJED
            If CMPEWAYCOUNTER - USEDEWAYCOUNTER <= 0 Then
                MsgBox("EWay Bill Package has Expired, Kindly contact Nakoda Infotech on +919987603607", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF DATE HAS EXPIRED
            If Now.Date > EWAYEXPDATE Then
                MsgBox("EWay Bill Package has Expired, Kindly contact Nakoda Infotech on +919987603607", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF BALANCECOUNTERS ARE 1% THEN INTIMATE
            If CMPEWAYCOUNTER - USEDEWAYCOUNTER < Format((CMPEWAYCOUNTER * 0.01), "0") Then
                MsgBox("Only " & (CMPEWAYCOUNTER - USEDEWAYCOUNTER) & " API's Left, Kindly contact Nakoda Infotech for Renewal of EWB Package", MsgBoxStyle.Critical)
            End If


            'FOR GENERATING EWAY BILL WE NEED TO FIRST GENERATE THE TOKEN
            'THIS IS FOR SANDBOX TEST
            'Dim URL As New Uri("http://testapi.taxprogsp.co.in/ewaybillapi/dec/v1.03/authenticate?aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&ewbpwd=" & CMPEWBPASS)
            Dim URL As New Uri("https://einvapi.charteredinfo.com/v1.03/dec/auth?action=ACCESSTOKEN&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&ewbpwd=" & CMPEWBPASS)

            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim REQUEST As WebRequest
            Dim RESPONSE As WebResponse
            REQUEST = WebRequest.CreateDefault(URL)

            REQUEST.Method = "GET"
            Try
                RESPONSE = REQUEST.GetResponse()
            Catch ex As WebException
                RESPONSE = ex.Response
            End Try
            Dim READER As StreamReader = New StreamReader(RESPONSE.GetResponseStream())
            Dim REQUESTEDTEXT As String = READER.ReadToEnd()

            'IF STATUS IS NOT 1 THEN TOKEN IS NOT GENERATED
            Dim STARTPOS As Integer = 0
            Dim TEMPSTATUS As String = ""
            Dim TOKEN As String = ""
            Dim ENDPOS As Integer = 0

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 3
            TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
            If TEMPSTATUS = "1" Then TEMPSTATUS = "SUCCESS" Else TEMPSTATUS = "FAILED"




            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("authtoken") + Len("AUTHTOKEN") + 3
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS) - 1
            TOKEN = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            'ADD DATA IN EWAYENTRY
            DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTPARTYBILLNO.Text.Trim) & ",'PURCHASE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


            'ONCE WE REC THE TOKEN WE WILL CREATE EWAY BILL
            'IF STATUS IS FAILED THEN ERROR MESSAGE
            If TEMPSTATUS = "FAILED" Then
                MsgBox("Unable to create Eway Bill", MsgBoxStyle.Critical)
                Exit Sub
            End If



            'GENERATING EWAY BILL 
            'FOR SANBOX TEST
            'Dim FURL As New Uri("http://testapi.taxprogsp.co.in/ewaybillapi/dec/v1.03/ewayapi?action=GENEWAYBILL&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&authtoken=" & TOKEN)
            Dim FURL As New Uri("https://einvapi.charteredinfo.com/v1.03/dec/ewayapi?action=GENEWAYBILL&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&authtoken=" & TOKEN)
            REQUEST = WebRequest.CreateDefault(FURL)
            REQUEST.Method = "POST"
            Try
                REQUEST.ContentType = "application/json"


                Dim j As String = ""

                j = "{"
                j = j & """supplyType"":""I"","
                j = j & """subSupplyType"":""1"","
                j = j & """subSupplyDesc"":"""","
                j = j & """docType"":""INV"","
                j = j & """docNo"":""" & TXTPARTYBILLNO.Text.Trim & """" & ","
                j = j & """docDate"":""" & DTPARTYBILLDATE.Text & """" & ","

                j = j & """fromGstin"":""" & PARTYGSTIN & """" & ","
                j = j & """fromTrdName"":""" & cmbname.Text.Trim & """" & ","
                j = j & """fromAddr1"":""" & PARTYADD1 & """" & ","
                j = j & """fromAddr2"":""" & cmbGodown.Text.Trim & """" & ","
                j = j & """fromPlace"":""" & CMBFROMCITY.Text.Trim & """" & ","

                j = j & """fromPincode"":""" & GODOWNPINCODE & """" & ","
                j = j & """actFromStateCode"":""" & GODOWNSTATECODE & """" & ","
                j = j & """fromStateCode"":""" & GODOWNSTATECODE & """" & ","

                j = j & """toGstin"":""" & CMPGSTIN & """" & ","
                j = j & """toTrdName"":""" & CmpName & """" & ","
                j = j & """toAddr1"":""" & TEMPCMPADD1 & """" & ","
                j = j & """toAddr2"":""" & TEMPCMPADD2 & """" & ","
                j = j & """toPlace"":""" & CMBTOCITY.Text.Trim & """" & ","
                j = j & """toPincode"":""" & CMPPINCODE & """" & ","
                j = j & """actToStateCode"":""" & CMPSTATECODE & """" & ","
                j = j & """toStateCode"":""" & CMPSTATECODE & """" & ","

                j = j & """transactionType"":""4"","
                j = j & """dispatchFromGSTIN"":""" & PARTYGSTIN & """" & ","
                j = j & """dispatchFromTradeName"":""" & cmbname.Text.Trim & """" & ","
                j = j & """shipToGSTIN"":""" & CMPGSTIN & """" & ","
                j = j & """shipToTradeName"":""" & CmpName & """" & ","
                j = j & """otherValue"":""0"","


                If INVOICESCREENTYPE = "TOTAL GST" Then
                    j = j & """totalValue"":""" & Val(TXTSUBTOTAL.Text.Trim) & """" & ","
                    j = j & """cgstValue"":""" & Val(TXTCGSTAMT.Text.Trim) & """" & ","
                    j = j & """sgstValue"":""" & Val(TXTSGSTAMT.Text.Trim) & """" & ","
                    j = j & """igstValue"":""" & Val(TXTIGSTAMT.Text.Trim) & """" & ","
                End If

                j = j & """cessValue"":""" & "0" & """" & ","
                j = j & """cessNonAdvolValue"":""" & "0" & """" & ","
                j = j & """totInvValue"":""" & Val(txtgrandtotal.Text.Trim) & """" & ","
                j = j & """transporterId"":""" & TRANSGSTIN & """" & ","
                j = j & """transporterName"":""" & CMBTRANSPORT.Text.Trim & """" & ","


                If TXTVEHICLENO.Text.Trim = "" Then
                    j = j & """transDocNo"":"""","
                    j = j & """transMode"":"""","
                    j = j & """transDistance"":""" & GODOWNKMS & """" & ","
                    j = j & """transDocDate"":"""","
                    j = j & """vehicleNo"":"""","
                    j = j & """vehicleType"":"""","
                Else
                    j = j & """transDocNo"":""" & TXTLRNO.Text.Trim & """" & ","
                    j = j & """transMode"":""" & "1" & """" & ","
                    j = j & """transDistance"":""" & GODOWNKMS & """" & ","
                    j = j & """transDocDate"":"""","
                    j = j & """vehicleNo"":""" & TXTVEHICLENO.Text.Trim & """" & ","
                    j = j & """vehicleType"":""" & "R" & """" & ","
                End If


                j = j & """itemList"":[{"


                'WE NEED TO FETCH SUMMARY OF ITEMS AND HSN TO PASS HERE
                'FETCH FROM DESC TABLE 
                DT = OBJCMN.Execute_Any_String(" SELECT ISNULL(QUALITYMASTER.QUALITY_NAME,'') AS ITEMNAME, ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGST, ISNULL(HSN_SGST,0) AS SGST, ISNULL(HSN_IGST,0) AS IGST, SUM(PURCHASEMASTER_DESC.BILL_WT) AS WT, SUM(PURCHASEMASTER_DESC.BILL_AMT) AS TAXABLEAMT FROM PURCHASEMASTER INNER JOIN PURCHASEMASTER_DESC ON PURCHASEMASTER_DESC.BILL_YEARID = PURCHASEMASTER.BILL_YEARID AND PURCHASEMASTER_DESC.BILL_NO = PURCHASEMASTER.BILL_NO AND PURCHASEMASTER_DESC.BILL_REGISTERID = PURCHASEMASTER.BILL_REGISTERID  INNER JOIN QUALITYMASTER ON PURCHASEMASTER_DESC.BILL_QUALITYID = QUALITYMASTER.QUALITY_ID INNER JOIN HSNMASTER ON HSNMASTER.HSN_ID = PURCHASEMASTER_DESC.BILL_HSNCODEID INNER JOIN REGISTERMASTER ON PURCHASEMASTER.BILL_REGISTERID = REGISTER_ID WHERE PURCHASEMASTER.BILL_NO = " & Val(TEMPBILLNO) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' and PURCHASEMASTER.BILL_YEARID = " & YearId & " GROUP BY ISNULL(QUALITYMASTER.QUALITY_NAME,''), ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0)", "", "")
                Dim CURRROW As Integer = 0
                For Each DTROW As DataRow In DT.Rows
                    If CURRROW > 0 Then j = j & ",{"
                    j = j & """productName"":""" & DTROW("ITEMNAME") & """" & ","
                    j = j & """productDesc"":""" & DTROW("ITEMNAME") & """" & ","
                    j = j & """hsnCode"":""" & DTROW("HSNCODE") & """" & ","
                    j = j & """quantity"":""" & Val(DTROW("WT")) & """" & ","
                    j = j & """qtyUnit"":""" & "KGS" & """" & ","

                    If INVOICESCREENTYPE = "TOTAL GST" Then
                        j = j & """cgstRate"":""" & Val(TXTCGSTPER.Text.Trim) & """" & ","
                        j = j & """sgstRate"":""" & Val(TXTSGSTPER.Text.Trim) & """" & ","
                        j = j & """igstRate"":""" & Val(TXTIGSTPER.Text.Trim) & """" & ","
                    End If

                    j = j & """cessRate"":""" & "0" & """" & ","
                    'THIS CODE WAS IN V1.02
                    'j = j & """cessAdvol"":""" & "0" & """" & ","
                    j = j & """cessNonAdvol"":""" & "0" & """" & ","
                    j = j & """taxableAmount"":""" & Val(DTROW("TAXABLEAMT")) & """"
                    j = j & " }"
                    CURRROW += 1
                Next

                j = j & " ]}"

                Dim stream As Stream = REQUEST.GetRequestStream()
                Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(j)
                stream.Write(buffer, 0, buffer.Length)

                'POST request absenden
                RESPONSE = REQUEST.GetResponse()

            Catch ex As WebException
                RESPONSE = ex.Response
                MsgBox("Error While Generating EWB, Please check the Data Properly")
                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTBILLNO.Text.Trim) & ",'PURCHASE','" & TOKEN & "','','FAILED', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End Try

            READER = New StreamReader(RESPONSE.GetResponseStream())
            REQUESTEDTEXT = READER.ReadToEnd()




            Dim EWBNO As String = ""

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ewayBillNo") + Len("ewayBillNo") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS)
            EWBNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            TXTEWAYBILLNO.Text = EWBNO

            'WE NEED TO UPDATE THIS EWBNO IN DATABASE ALSO
            'DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_EWAYBILLNO = '" & TXTEWAYBILLNO.Text.Trim & "' FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TEMPBILLNO) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")

            'ADD DATA IN EWAYENTRY
            DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTBILLNO.Text.Trim) & ",'PURCHASE','" & TOKEN & "','" & EWBNO & "','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTEWB()
        Try

            If PRINTEWAYBILL = False Then Exit Sub
            If EDIT = False Then Exit Sub
            If TXTEWAYBILLNO.Text.Trim = "" Then Exit Sub


            If MsgBox("Print EWB?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim TOKENNO As String = ""
            Dim EWBNO As String = ""

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" ISNULL(TOKENNO, '') AS TOKENNO, ISNULL(EWBNO, '') AS EWBNO ", "", " EWAYENTRY ", " AND EWBNO = '" & TXTEWAYBILLNO.Text.Trim & "' And YearId = " & YearId)
            If DT.Rows.Count = 0 Then Exit Sub
            TOKENNO = DT.Rows(0).Item("TOKENNO")
            EWBNO = DT.Rows(0).Item("EWBNO")

            'Dim URL As New Uri("https://einvapi.charteredinfo.com/v1.03/dec/authenticate?action=ACCESSTOKEN&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&ewbpwd=" & CMPEWBPASS)
            Dim URL As New Uri("https://einvapi.charteredinfo.com/v1.03/dec/ewayapi?action=GetEwayBill&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&authtoken=" & TOKENNO & "&ewbNo=" & EWBNO)


            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim REQUEST As WebRequest
            Dim RESPONSE As WebResponse
            REQUEST = WebRequest.CreateDefault(URL)
            REQUEST.Method = "Get"
            Try
                RESPONSE = REQUEST.GetResponse()
            Catch ex As WebException
                RESPONSE = ex.Response
            End Try
            Dim READER As StreamReader = New StreamReader(RESPONSE.GetResponseStream())
            Dim REQUESTEDTEXT As String = READER.ReadToEnd()
            Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(REQUESTEDTEXT)

            Dim FURL As New Uri("https://einvapi.charteredinfo.com/aspapi/v1.0/printewb?aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN)
            REQUEST = WebRequest.CreateDefault(FURL)
            REQUEST.Method = "POST"
            Try
                REQUEST.ContentType = "application/x-www-form-urlencoded"
                REQUEST.ContentLength = buffer.Length

                Dim stream As Stream = REQUEST.GetRequestStream()
                stream.Write(buffer, 0, buffer.Length)

                'POST request absenden
                RESPONSE = REQUEST.GetResponse()
                Dim STRREADER As Stream = RESPONSE.GetResponseStream()
                Dim BINREADER As New BinaryReader(STRREADER)
                Dim BFFER As Byte() = BINREADER.ReadBytes(CInt(RESPONSE.ContentLength))
                File.WriteAllBytes(Application.StartupPath & "\EWB_" & TXTEWAYBILLNO.Text.Trim & ".pdf", BFFER)
                System.Diagnostics.Process.Start(Application.StartupPath & "\EWB_" & TXTEWAYBILLNO.Text.Trim & ".pdf")

                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTBILLNO.Text.Trim) & ",'PURCHASE','" & TOKENNO & "','" & EWBNO & "','PRINT SUCCESS1', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTBILLNO.Text.Trim) & ",'PURCHASE','" & TOKENNO & "','" & EWBNO & "','PRINT SUCCESS2', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

            Catch ex As WebException
                RESPONSE = ex.Response
                MsgBox("Error While Printing EWB, Please check the Data Properly")
                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTBILLNO.Text.Trim) & ",'PURCHASE','" & TOKENNO & "','" & EWBNO & "','PRINT FAILED1', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTBILLNO.Text.Trim) & ",'PURCHASE','" & TOKENNO & "','" & EWBNO & "','PRINT FAILED2', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If EDIT = True Then SendWhatsapp(TEMPBILLNO)
            DT = OBJCMN.Execute_Any_String("UPDATE PURCHASEMASTER SET BILL_SENDWHATSAPP = 1 WHERE BILL_NO = " & TEMPBILLNO & " AND BILL_YEARID = " & YearId, "", "")
            LBLWHATSAPP.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Async Sub SENDWHATSAPP(BILLNO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJCN As New PurchaseInvoiceDesign
            OBJCN.MdiParent = MDIMain
            OBJCN.FRMSTRING = "PURBILL"
            OBJCN.DIRECTMAIL = False
            OBJCN.DIRECTPRINT = True
            OBJCN.DIRECTWHATSAPP = True
            OBJCN.REGNAME = cmbregister.Text.Trim
            OBJCN.PARTYNAME = cmbname.Text.Trim
            OBJCN.BILLNO = Val(BILLNO)
            OBJCN.NOOFCOPIES = 1
            OBJCN.Show()
            OBJCN.Close()


            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = cmbname.Text.Trim
            OBJWHATSAPP.AGENTNAME = CMBAGENT.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & cmbname.Text.Trim & "_PURCHASEINVOICE_NO-" & Val(BILLNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add(cmbname.Text.Trim & "_PURCHASEINVOICE_" & Val(BILLNO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSPLIT_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHKSPLIT.CheckStateChanged
        Try
            gbags.ReadOnly = Not CHKSPLIT.Checked
            gwt.ReadOnly = Not CHKSPLIT.Checked
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBILL_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDBILL.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDBILL.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDBILL.Rows.RemoveAt(GRIDBILL.CurrentRow.Index)
                getsrno(GRIDBILL)

            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTYPE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTYPE.Validated
        Try
            If CMBTYPE.Text.Trim <> "" Then
                PURTYPE = CMBTYPE.Text.Trim
                CMBTYPE.Enabled = False
                HIDEVIEW()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSACDESC_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSACDESC.Validated
        Try
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKMANUAL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKMANUAL.CheckedChanged
        Try
            If CHKMANUAL.Checked = True Then
                TXTCGSTAMT.ReadOnly = False
                TXTCGSTAMT.TabStop = True
                TXTCGSTAMT.BackColor = Color.LemonChiffon
                TXTSGSTAMT.ReadOnly = False
                TXTSGSTAMT.TabStop = True
                TXTSGSTAMT.BackColor = Color.LemonChiffon
                TXTIGSTAMT.ReadOnly = False
                TXTIGSTAMT.TabStop = True
                TXTIGSTAMT.BackColor = Color.LemonChiffon
            Else
                TXTCGSTAMT.ReadOnly = True
                TXTCGSTAMT.TabStop = False
                TXTCGSTAMT.BackColor = Color.Linen
                TXTSGSTAMT.ReadOnly = True
                TXTSGSTAMT.TabStop = False
                TXTSGSTAMT.BackColor = Color.Linen
                TXTIGSTAMT.ReadOnly = True
                TXTIGSTAMT.TabStop = False
                TXTIGSTAMT.BackColor = Color.Linen
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCGSTAMT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCGSTAMT.KeyPress, TXTSGSTAMT.KeyPress, TXTIGSTAMT.KeyPress, TXTTCSAMT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTCGSTAMT_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTCGSTAMT.Validated, TXTSGSTAMT.Validated, TXTIGSTAMT.Validated, TXTTCSAMT.Validated
        TOTAL()
    End Sub

    Private Sub BILLDATE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BILLDATE.Validated
        Try
            If ClientName = "STC" And BILLDATE.Text.Trim <> "__/__/____" Then
                DTPARTYBILLDATE.Text = BILLDATE.Text
                DUEDATE.Value = BILLDATE.Text
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPBILLNO)
            PRINTEWB()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub PRINTREPORT(ByVal BILLNO As Integer)
        Try
            If MsgBox("Wish to Print Bill?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim OBJBILL As New PurchaseInvoiceDesign
                OBJBILL.MdiParent = MDIMain
                OBJBILL.FRMSTRING = "PURBILL"
                OBJBILL.BILLTYPE = CMBTYPE.Text.Trim
                OBJBILL.WHERECLAUSE = "{PURCHASEMASTER.BILL_NO}=" & Val(BILLNO) & " and {REGISTERMASTER.REGISTER_NAME} = '" & cmbregister.Text.Trim & "' AND {PURCHASEMASTER.BILL_yearid}=" & YearId
                OBJBILL.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FROMDATE_GotFocus(sender As Object, e As EventArgs) Handles FROMDATE.GotFocus
        FROMDATE.Select(0, 0)
    End Sub

    Private Sub TILLDATE_GotFocus(sender As Object, e As EventArgs) Handles TILLDATE.GotFocus
        TILLDATE.Select(0, 0)
    End Sub
End Class