
Imports BL
Imports System.IO
Imports System.Net
Imports System.ComponentModel
Imports RestSharp
Imports Newtonsoft.Json
Imports TaxProEInvoice.API


Public Class InvoiceMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK, GRIDCHGSDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public edit As Boolean
    Public TEMPINVOICENO, TEMPREGNAME As String
    Dim TEMPUPLOADROW, TEMPCHGSROW, saleregid As Integer
    Dim saleregabbr, salereginitial As String
    Dim TEMPFORM As String
    Public AUTOSAVEFROMGR As Boolean = False
    Dim DTWHATSAPP As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEXIT.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        edit = False
        cmbregister.Enabled = True
        cmbregister.Focus()
    End Sub

    Sub HIDEVIEW()
        Try

            If CMBSCREENTYPE.Text = "LINE GST" Then
                BTNOTHERAMT.Visible = True
                TXTOTHERAMT.Visible = True
                GOTHERAMT.Visible = True
                LBLTOTALOTHERAMT.Visible = True

                BTNTAXABLEAMT.Visible = True
                TXTTAXABLEAMT.Visible = True
                GTAXABLEAMT.Visible = True
                LBLTOTALTAXABLEAMT.Visible = True

                BTNCGSTPER.Visible = True
                TXTCGSTPER.Visible = True
                GCGSTPER.Visible = True
                BTNCGSTAMT.Visible = True
                TXTCGSTAMT.Visible = True
                GCGSTAMT.Visible = True
                LBLTOTALCGSTAMT.Visible = True

                BTNSGSTPER.Visible = True
                TXTSGSTPER.Visible = True
                GSGSTPER.Visible = True
                BTNSGSTAMT.Visible = True
                TXTSGSTAMT.Visible = True
                GSGSTAMT.Visible = True
                LBLTOTALSGSTAMT.Visible = True

                BTNIGSTPER.Visible = True
                TXTIGSTPER.Visible = True
                GIGSTPER.Visible = True
                BTNIGSTAMT.Visible = True
                TXTIGSTAMT.Visible = True
                GIGSTAMT.Visible = True
                LBLTOTALIGSTAMT.Visible = True

                BTNGRIDTOTAL.Visible = True
                TXTGRIDTOTAL.Visible = True
                GGRIDTOTAL.Visible = True

                GRIDINVOICE.Width = 1650


                LBLCGST.Visible = False
                TXTCGSTPER1.Visible = False
                TXTCGSTAMT1.Visible = False
                LBLSGST.Visible = False
                TXTSGSTPER1.Visible = False
                TXTSGSTAMT1.Visible = False
                LBLIGST.Visible = False
                TXTIGSTPER1.Visible = False
                TXTIGSTAMT1.Visible = False

            Else

                BTNOTHERAMT.Visible = False
                TXTOTHERAMT.Visible = False
                GOTHERAMT.Visible = False
                LBLTOTALOTHERAMT.Visible = False

                BTNTAXABLEAMT.Visible = False
                TXTTAXABLEAMT.Visible = False
                GTAXABLEAMT.Visible = False
                LBLTOTALTAXABLEAMT.Visible = False

                BTNCGSTPER.Visible = False
                TXTCGSTPER.Visible = False
                GCGSTPER.Visible = False
                BTNCGSTAMT.Visible = False
                TXTCGSTAMT.Visible = False
                GCGSTAMT.Visible = False
                LBLTOTALCGSTAMT.Visible = False

                BTNSGSTPER.Visible = False
                TXTSGSTPER.Visible = False
                GSGSTPER.Visible = False
                BTNSGSTAMT.Visible = False
                TXTSGSTAMT.Visible = False
                GSGSTAMT.Visible = False
                LBLTOTALSGSTAMT.Visible = False

                BTNIGSTPER.Visible = False
                TXTIGSTPER.Visible = False
                GIGSTPER.Visible = False
                BTNIGSTAMT.Visible = False
                TXTIGSTAMT.Visible = False
                GIGSTAMT.Visible = False
                LBLTOTALIGSTAMT.Visible = False

                BTNGRIDTOTAL.Visible = False
                TXTGRIDTOTAL.Visible = False
                GGRIDTOTAL.Visible = False

                GRIDINVOICE.Width = 1080


                LBLCGST.Visible = True
                TXTCGSTPER1.Visible = True
                TXTCGSTAMT1.Visible = True
                LBLSGST.Visible = True
                TXTSGSTPER1.Visible = True
                TXTSGSTAMT1.Visible = True
                LBLIGST.Visible = True
                TXTIGSTPER1.Visible = True
                TXTIGSTAMT1.Visible = True

            End If

            If CMBTYPE.Text = "GREY" Then
                LBLLBS.Visible = False
                CMBLBS.Visible = False
                LBLLBS.Visible = False
                TXTLBSRATE.Visible = False
                LBLLBSRATE.Visible = False
                BTNLOTNO.Text = "Lot No"
                GLOTNO.Visible = True
                BTNBALENO.Text = "Bale No"
                GBALENO.Visible = True
                GLRNO.Visible = False
                GLRDATE.Visible = False
                BTNMILLNAME.Visible = False
                GMILLNAME.Visible = False
                BTNTAKA.Text = "Taka"
                BTNTAKA.Left = BTNMILLNAME.Left
                BTNMTRS.Text = "Mtrs"
                txtsrno.Visible = True
                CMBQUALITY.Visible = True
                TXTHSNCODE.Visible = True
                TXTLOTNO.Visible = True
                TXTBALENO.Visible = True
                TXTPCS.Visible = True
                TXTMTRS.Visible = True
                TXTRATE.Visible = True
                CMBPER.Visible = True
                TXTAMOUNT.Visible = True
                TXTNARRATION.Visible = True
                'TXTOTHERAMT.Visible = True
                'TXTTAXABLEAMT.Visible = True
                'TXTCGSTPER.Visible = True
                'TXTCGSTAMT.Visible = True
                'TXTSGSTPER.Visible = True
                'TXTSGSTAMT.Visible = True
                'TXTIGSTPER.Visible = True
                'TXTIGSTAMT.Visible = True
                'TXTGRIDTOTAL.Visible = True
                GRIDINVOICE.Top = txtsrno.Top + txtsrno.Height
                GRIDINVOICE.Height = 104
                'GRIDINVOICE.Width = 1650
            Else
                LBLLBS.Visible = False
                CMBLBS.Visible = False
                LBLLBS.Visible = False
                TXTLBSRATE.Visible = False
                LBLLBSRATE.Visible = False
                BTNLOTNO.Text = "LR No"
                GLOTNO.Visible = False
                BTNBALENO.Text = "LR Date"
                GBALENO.Visible = False
                GLRNO.Visible = True
                GLRDATE.Visible = True
                BTNMILLNAME.Visible = True
                GMILLNAME.Visible = True
                BTNTAKA.Text = "Bags"
                BTNTAKA.Left = BTNMILLNAME.Left + BTNMILLNAME.Width
                BTNMTRS.Text = "Wt."
                txtsrno.Visible = False
                CMBQUALITY.Visible = False
                TXTHSNCODE.Visible = False
                TXTLOTNO.Visible = False
                TXTBALENO.Visible = False
                TXTPCS.Visible = False
                TXTMTRS.Visible = False
                TXTRATE.Visible = False
                CMBPER.Visible = False
                TXTAMOUNT.Visible = False
                TXTNARRATION.Visible = False
                TXTOTHERAMT.Visible = False
                TXTTAXABLEAMT.Visible = False
                TXTCGSTPER.Visible = False
                TXTCGSTAMT.Visible = False
                TXTSGSTPER.Visible = False
                TXTSGSTAMT.Visible = False
                TXTIGSTPER.Visible = False
                TXTIGSTAMT.Visible = False
                TXTGRIDTOTAL.Visible = False
                GRIDINVOICE.Top = txtsrno.Top
                GRIDINVOICE.Height = 104 + txtsrno.Height
                If CMBSCREENTYPE.Text.Trim = "LINE GST" Then GRIDINVOICE.Width = 1650 + BTNMILLNAME.Width Else GRIDINVOICE.Width = 1080 + BTNMILLNAME.Width
            End If

            'THIS IS COMMON CODE
            BTNMTRS.Left = BTNTAKA.Left + BTNTAKA.Width
            BTNRATE.Left = BTNMTRS.Left + BTNMTRS.Width
            BTNPER.Left = BTNRATE.Left + BTNRATE.Width
            BTNAMT.Left = BTNPER.Left + BTNPER.Width
            BTNNARR.Left = BTNAMT.Left + BTNAMT.Width
            BTNOTHERAMT.Left = BTNNARR.Left + BTNNARR.Width
            BTNTAXABLEAMT.Left = BTNOTHERAMT.Left + BTNOTHERAMT.Width
            BTNCGSTPER.Left = BTNTAXABLEAMT.Left + BTNTAXABLEAMT.Width
            BTNCGSTAMT.Left = BTNCGSTPER.Left + BTNCGSTPER.Width
            BTNSGSTPER.Left = BTNCGSTAMT.Left + BTNCGSTAMT.Width
            BTNSGSTAMT.Left = BTNSGSTPER.Left + BTNSGSTPER.Width
            BTNIGSTPER.Left = BTNSGSTAMT.Left + BTNSGSTAMT.Width
            BTNIGSTAMT.Left = BTNIGSTPER.Left + BTNIGSTPER.Width
            BTNGRIDTOTAL.Left = BTNIGSTAMT.Left + BTNIGSTAMT.Width

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub clear()

        INVOICEDATE.Text = Mydate
        CMBTYPE.Enabled = True
        CMBSCREENTYPE.Text = INVOICESCREENTYPE
        HIDEVIEW()

        TXTEWAYBILLNO.Clear()

        tstxtbillno.Clear()
        TXTINVOICENO.ReadOnly = False
        cmbname.Text = ""
        cmbname.Enabled = True
        CMBAGENT.Text = ""
        cmbtrans.Text = ""
        TXTDISPLAYQUALITY.Clear()

        TXTCHALLANTYPE.Clear()
        TXTCHALLANNO.Clear()
        CHALLANDATE.Clear()

        TXTCRDAYS.Clear()
        DUEDATE.Text = Mydate

        TXTDELIVERYAT.Clear()
        TXTLRNO.Clear()
        TXTVEHICLENO.Clear()
        If CMPCITYNAME <> "" Then CMBFROMCITY.Text = CMPCITYNAME Else CMBFROMCITY.Text = ""
        CMBTOCITY.Text = ""
        TXTADD.Clear()
        txtremarks.Clear()

        TXTFOLD.Clear()
        TXTORDERDISC.Clear()
        TXTPARTYPONO.Clear()

        CHKFORMBOX.Enabled = True
        For I As Integer = 0 To CHKFORMBOX.Items.Count - 1
            Dim DTR As DataRowView = CHKFORMBOX.Items(I)
            CHKFORMBOX.SetItemCheckState(I, CheckState.Unchecked)
        Next

        CHKFORMBOX.SetItemChecked(CHKFORMBOX.FindStringExact("GST"), True)


        CHKBILLCHECKED.Checked = False
        CHKBILLDISPUTE.Checked = False
        CMDSELECTGDN.Enabled = True

        EP.Clear()
        PBCN.Visible = False
        PBRECD.Visible = False
        lbllocked.Visible = False
        PBlock.Visible = False
        cmdshowdetails.Visible = False

        txtinwords.Clear()

        GRIDINVOICE.RowCount = 0
        CMBQUALITY.Text = ""
        TXTLOTNO.Clear()
        TXTBALENO.Clear()
        TXTPCS.Clear()
        TXTMTRS.Clear()
        TXTRATE.Clear()
        CMBPER.SelectedIndex = 0
        TXTAMOUNT.Clear()
        TXTNARRATION.Clear()

        TXTCHGSSRNO.Clear()
        CMBCHARGES.Text = ""
        TXTCHGSPER.Clear()
        TXTCHGSAMT.Clear()
        TXTNETTAMT.Clear()
        GRIDCHGS.RowCount = 0


        TXTCGSTPER1.Clear()
        TXTCGSTAMT1.Clear()
        TXTSGSTPER1.Clear()
        TXTSGSTAMT1.Clear()
        TXTIGSTPER1.Clear()
        TXTIGSTAMT1.Clear()
        CHKMANUAL.CheckState = CheckState.Unchecked

        If ClientName = "NIRMALA" Then CHKTCS.CheckState = CheckState.Checked Else CHKTCS.Checked = False
        TXTTOTALWITHGST.Clear()
        TXTTCSPER.Clear()
        TXTTCSAMT.Clear()

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Text = 1
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0

        getmax_INVOICE_no()

        GRIDDOUBLECLICK = False
        GRIDCHGSDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        TXTGRAMT.Text = 0.0
        TXTSHORTAGEAMT.Text = 0.0
        txtbillamt.Text = 0.0
        TXTSUBTOTAL.Text = 0.0
        TXTTOTALTAXAMT.Clear()
        TXTTOTALOTHERCHGSAMT.Clear()
        TXTCHARGES.Text = 0.0
        txtgrandtotal.Text = 0.0
        txtroundoff.Text = 0.0
        txtremarks.Clear()

        LBLTOTALPCS.Text = "0"
        LBLTOTALMTRS.Text = "0.0"
        lbltotalamt.Text = "0.0"
        LBLTOTALOTHERAMT.Text = "0.0"
        LBLTOTALTAXABLEAMT.Text = "0.0"
        LBLTOTALCGSTAMT.Text = "0.0"
        LBLTOTALSGSTAMT.Text = "0.0"
        LBLTOTALIGSTAMT.Text = "0.0"

        TXTGRNO.Clear()
        TXTGRPCS.Clear()
        TXTGRMTRS.Clear()
        TXTGRSHORTAGE.Clear()
        TXTACCEPTEDPCS.Clear()
        TXTACCEPTEDMTRS.Clear()
        CHKNOGR.CheckState = CheckState.Unchecked

        TabControl2.SelectedIndex = 0

        TXTCHGSSRNO.Text = 1
        TXTUPLOADSRNO.Text = 1
        TXTIRNNO.Clear()
        TXTIRNNO.Clear()
        TXTACKNO.Clear()
        ACKDATE.Value = Now.Date
        PBQRCODE.Image = Nothing
        CMBDISPATCHFROM.Text = ""
        LBLWHATSAPP.Visible = False
    End Sub

    Sub getmax_INVOICE_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(INVOICE_no),0) + 1 ", "INVOICEMASTER INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICE_REGISTERID  AND REGISTER_YEARID = INVOICE_YEARID  ", " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "'  and INVOICE_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTINVOICENO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub INVOICEMASTER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdOK_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.Alt = True And e.KeyCode = Keys.D1 Then
                TabControl2.SelectedIndex = 0
            ElseIf e.Alt = True And e.KeyCode = Keys.D2 Then
                TabControl2.SelectedIndex = 1
            ElseIf e.Alt = True And e.KeyCode = Keys.D3 Then
                TabControl2.SelectedIndex = 2
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for DIRECT LINK TO INV NO
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call toolprevious_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call toolnext_Click(sender, e)
            ElseIf e.KeyCode = Windows.Forms.Keys.F3 Then
                CMBCHARGES.Focus()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try
            fillregister(cmbregister, " and register_type = 'SALE'")
            If cmbname.Text.Trim = "" Then fillname(cmbname, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
            If CMBAGENT.Text.Trim = "" Then fillname(CMBAGENT, edit, " and GROUPMASTER.GROUP_NAME = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
            If cmbtrans.Text = "" Then fillname(cmbtrans, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
            If CMBCHARGES.Text.Trim = "" Then fillname(CMBCHARGES, edit, " AND (GROUPMASTER.GROUP_SECONDARY ='Indirect Income' OR GROUPMASTER.GROUP_SECONDARY ='Indirect Expenses' or GROUPMASTER.GROUP_SECONDARY ='Direct Income' OR GROUPMASTER.GROUP_SECONDARY ='Direct Expenses' OR GROUPMASTER.GROUP_SECONDARY ='Duties & Taxes' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' OR GROUPMASTER.GROUP_SECONDARY = 'Sale A/C')")
            If CMBDISPATCHFROM.Text.Trim = "" Then fillname(CMBDISPATCHFROM, edit, " and GROUPMASTER.GROUP_NAME = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='ACCOUNTS'")
            FILLGREY(CMBQUALITY, edit)
            fillform(CHKFORMBOX, edit)
            'fillper(CMBPER, edit)

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub INVOICEMASTER_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE INVOICE'")

            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
            fillcmb()
            clear()
            cmbname.Enabled = True
            If edit = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If


                Dim OBJINVOICE As New ClsInvoiceMaster()
                Dim DT As DataTable = OBJINVOICE.selectINVOICE(TEMPINVOICENO, TEMPREGNAME, YearId)


                If DT.Rows.Count > 0 Then
                    For Each dr As DataRow In DT.Rows

                        CMBSCREENTYPE.Text = dr("SCREENTYPE")

                        TXTINVOICENO.Text = TEMPINVOICENO
                        TXTINVOICENO.ReadOnly = True
                        CMBTYPE.Text = dr("TYPE")
                        CMBTYPE.Enabled = False
                        HIDEVIEW()

                        cmbregister.Text = Convert.ToString(dr("REGNAME"))
                        INVOICEDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        cmbname.Text = Convert.ToString(dr("NAME"))

                        TXTSTATECODE.Text = dr("STATECODE")
                        TXTGSTIN.Text = dr("GSTIN")

                        CMBAGENT.Text = Convert.ToString(dr("AGENT"))
                        cmbtrans.Text = dr("TRANSNAME")
                        TXTDISPLAYQUALITY.Text = dr("DISPLAYQUALITYNAME")

                        TXTCHALLANNO.Text = Convert.ToString(dr("CHALLANNO"))
                        CHALLANDATE.Text = Format(Convert.ToDateTime(dr("CHALLANDATE")).Date, "dd/MM/yyyy")
                        TXTCHALLANTYPE.Text = Convert.ToString(dr("CHALLANTYPE"))
                        TXTCRDAYS.Text = Convert.ToString(dr("CRDAYS"))
                        DUEDATE.Text = Format(Convert.ToDateTime(dr("DUEDATE")).Date, "dd/MM/yyyy")

                        TXTFOLD.Text = dr("FOLD")
                        TXTORDERDISC.Text = Val(dr("ORDERDISC"))
                        TXTPARTYPONO.Text = dr("PARTYPONO")


                        TXTDELIVERYAT.Text = dr("DELIVERYAT")
                        TXTLRNO.Text = Convert.ToString(dr("LRNO"))
                        TXTVEHICLENO.Text = dr("VEHICLENO")
                        CMBFROMCITY.Text = Convert.ToString(dr("FROMCITY"))
                        CMBTOCITY.Text = Convert.ToString(dr("TOCITY"))
                        TXTEWAYBILLNO.Text = dr("EWAYBILLNO")

                        If dr("BILLCHECKED") = 0 Then CHKBILLCHECKED.Checked = False Else CHKBILLCHECKED.Checked = True
                        If dr("BILLDISPUTE") = 0 Then CHKBILLDISPUTE.Checked = False Else CHKBILLDISPUTE.Checked = True
                        If Convert.ToBoolean(dr("MANUALGST")) = False Then CHKMANUAL.Checked = False Else CHKMANUAL.Checked = True
                        If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True


                        TXTCGSTPER1.Text = Val(dr("TOTALCGSTPER"))
                        TXTSGSTPER1.Text = Val(dr("TOTALSGSTPER"))
                        TXTIGSTPER1.Text = Val(dr("TOTALIGSTPER"))

                        If CMBSCREENTYPE.Text = "TOTAL GST" And CHKMANUAL.Checked = True Then
                            TXTCGSTAMT1.Text = Format(Val(dr("TOTALCGSTAMT")), "0.00")
                            TXTSGSTAMT1.Text = Format(Val(dr("TOTALSGSTAMT")), "0.00")
                            TXTIGSTAMT1.Text = Format(Val(dr("TOTALIGSTAMT")), "0.00")
                        End If
                        If dr("APPLYTCS") = 0 Then CHKTCS.Checked = False Else CHKTCS.Checked = True
                        TXTTOTALWITHGST.Text = Val(dr("TOTALWITHGST"))
                        TXTTCSPER.Text = Val(dr("TCSPER"))
                        TXTTCSAMT.Text = Val(dr("TCSAMT"))


                        TXTGRAMT.Text = dr("GRAMT")
                        TXTSHORTAGEAMT.Text = dr("SHORTAGEAMT")
                        txtbillamt.Text = dr("AMOUNT")
                        TXTCHARGES.Text = dr("CHARGES")
                        txtroundoff.Text = dr("ROUNDOFF")
                        txtgrandtotal.Text = dr("GRANDTOTAL")

                        TXTAMTREC.Text = dr("AMTREC")
                        TXTEXTRAAMT.Text = dr("EXTRAAMT")
                        TXTRETURN.Text = dr("RETURN")
                        TXTBAL.Text = dr("BALANCE")


                        TXTGRNO.Text = dr("GRNO")
                        TXTGRPCS.Text = Val(dr("GRPCS"))
                        TXTGRMTRS.Text = Val(dr("GRMTRS"))
                        TXTGRSHORTAGE.Text = Val(dr("GRSHORTAGE"))

                        If Convert.ToBoolean(dr("NOGR")) = True Then CHKNOGR.Checked = True Else CHKNOGR.Checked = False

                        If Val(dr("AMTREC")) > 0 Or Val(dr("EXTRAAMT")) > 0 Then
                            cmdshowdetails.Visible = True
                            PBRECD.Visible = True
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        If Val(dr("RETURN")) > 0 Then
                            cmdshowdetails.Visible = True
                            PBCN.Visible = True
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If


                        txtinwords.Text = Convert.ToString(dr("INWORDS"))
                        txtremarks.Text = Convert.ToString(dr("REMARKS"))


                        'Item Grid
                        'GRIDINVOICE.Rows.Add(dr("SRNO"), Convert.ToString(dr("GREYQUALITY")), Val(dr("LOTNO")), dr("BALENO"), Val(dr("PCS")), Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER"), Format(Val(dr("AMOUNT")), "0.00"), dr("NARRATION").ToString, dr("FROMNO"), dr("FROMSRNO"), dr("GRIDTYPE"), dr("GRIDDONE"))
                        GRIDINVOICE.Rows.Add(dr("SRNO"), Convert.ToString(dr("GREYQUALITY")), Convert.ToString(dr("HSNCODE")), Val(dr("LOTNO")), dr("BALENO"), dr("GRIDLRNO"), Format(Convert.ToDateTime(dr("GRIDLRDATE")).Date, "dd/MM/yyyy"), dr("MILLNAME"), Val(dr("PCS")), Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RATE")), "0.0000"), dr("PER"), Format(Val(dr("AMOUNT")), "0.00"), dr("NARRATION").ToString, Format(Val(dr("OTHERAMT")), "0.00"), Format(Val(dr("TAXABLEAMT")), "0.00"), Format(Val(dr("CGSTPER")), "0.00"), Format(Val(dr("CGSTAMT")), "0.00"), Format(Val(dr("SGSTPER")), "0.00"), Format(Val(dr("SGSTAMT")), "0.00"), Format(Val(dr("IGSTPER")), "0.00"), Format(Val(dr("IGSTAMT")), "0.00"), Format(Val(dr("GRIDTOTAL")), "0.00"), dr("FROMNO"), dr("FROMSRNO"), dr("GRIDTYPE"), dr("GRIDDONE"))

                        If Convert.ToBoolean(dr("GRIDDONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = False
                            GRIDINVOICE.Rows(GRIDINVOICE.RowCount - 1).DefaultCellStyle.BackColor = Drawing.Color.Yellow
                        End If

                        TXTIRNNO.Text = dr("IRNNO")
                        TXTACKNO.Text = dr("ACKNO")
                        ACKDATE.Value = dr("ACKDATE")
                        If IsDBNull(dr("QRCODE")) = False Then
                            PBQRCODE.Image = Image.FromStream(New IO.MemoryStream(DirectCast(dr("QRCODE"), Byte())))
                        Else
                            PBQRCODE.Image = Nothing
                        End If
                        CMBDISPATCHFROM.Text = dr("DISPATCHFROM")
                    Next
                    GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1

                    'CHARGES GRID
                    Dim OBJCMN As New ClsCommon
                    Dim dttable As DataTable = OBJCMN.search("  ISNULL(INVOICEMASTER_CHGS.INVOICE_gridsrno,0) AS GRIDSRNO, ISNULL(LEDGERS.Acc_cmpname, '') AS CHARGES, ISNULL(INVOICEMASTER_CHGS.INVOICE_PER, 0) AS PER, ISNULL(INVOICEMASTER_CHGS.INVOICE_AMT, 0) AS AMT, ISNULL(TAXMASTER.tax_id, 0) AS TAXID", "", "   LEDGERS RIGHT OUTER JOIN INVOICEMASTER_CHGS LEFT OUTER JOIN TAXMASTER ON INVOICEMASTER_CHGS.INVOICE_yearid = TAXMASTER.tax_yearid AND INVOICEMASTER_CHGS.INVOICE_TAXID = TAXMASTER.tax_id ON LEDGERS.Acc_yearid = INVOICEMASTER_CHGS.INVOICE_yearid AND LEDGERS.Acc_id = INVOICEMASTER_CHGS.INVOICE_CHARGESID RIGHT OUTER JOIN REGISTERMASTER INNER JOIN INVOICEMASTER ON REGISTERMASTER.register_id = INVOICEMASTER.INVOICE_REGISTERID AND REGISTERMASTER.register_yearid = INVOICEMASTER.INVOICE_YEARID ON INVOICEMASTER_CHGS.INVOICE_no = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_CHGS.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_CHGS.INVOICE_yearid = INVOICEMASTER.INVOICE_YEARID", " AND REGISTERMASTER.REGISTER_NAME = '" & TEMPREGNAME & "' AND REGISTERMASTER.REGISTER_TYPE='SALE' AND INVOICEMASTER_CHGS.INVOICE_NO = " & TEMPINVOICENO & "   AND INVOICEMASTER_CHGS.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER_CHGS.INVOICE_gridsrno")
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            GRIDCHGS.Rows.Add(DTR("GRIDSRNO"), DTR("CHARGES"), DTR("PER"), DTR("AMT"), DTR("TAXID"))
                        Next
                    End If

                    'UPLOAD GRID
                    Dim OBJ As New ClsCommon
                    Dim dt2 As DataTable = OBJ.search(" INVOICEMASTER_UPLOAD.INVOICE_UPSRNO AS GRIDSRNO, INVOICEMASTER_UPLOAD.INVOICE_UPREMARKS AS REMARKS, INVOICEMASTER_UPLOAD.INVOICE_UPNAME AS NAME, INVOICEMASTER_UPLOAD.INVOICE_IMGPATH AS IMGPATH", "", " INVOICEMASTER INNER JOIN INVOICEMASTER_UPLOAD ON INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_UPLOAD.INVOICE_YEARID AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_UPLOAD.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_NO = INVOICEMASTER_UPLOAD.INVOICE_NO LEFT OUTER JOIN REGISTERMASTER ON INVOICEMASTER_UPLOAD.INVOICE_REGISTERID = REGISTERMASTER.register_id AND INVOICEMASTER_UPLOAD.INVOICE_YEARID = REGISTERMASTER.register_yearid ", " AND INVOICEMASTER.INVOICE_NO = " & TEMPINVOICENO & " AND REGISTER_NAME ='" & TEMPREGNAME & "' AND REGISTERMASTER.REGISTER_TYPE='SALE' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                    If dt2.Rows.Count > 0 Then
                        For Each DTR As DataRow In dt2.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If

                    Dim OBJCOMMON As New ClsCommon
                    dttable = OBJCOMMON.search(" ISNULL(FORMTYPE.FORM_NAME, '') AS FORMNAME", "", "  INVOICEMASTER_FORMTYPE INNER JOIN REGISTERMASTER ON INVOICEMASTER_FORMTYPE.INVOICE_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN FORMTYPE ON INVOICEMASTER_FORMTYPE.INVOICE_FORMID = FORMTYPE.FORM_ID ", " AND REGISTERMASTER.REGISTER_NAME = '" & TEMPREGNAME & "' AND INVOICEMASTER_FORMTYPE.INVOICE_NO = " & TEMPINVOICENO & " AND INVOICEMASTER_FORMTYPE.INVOICE_YEARID= " & YearId)
                    If dttable.Rows.Count > 0 Then
                        For Each ROW As DataRow In dttable.Rows
                            For I As Integer = 0 To CHKFORMBOX.Items.Count - 1
                                Dim DTR As DataRowView = CHKFORMBOX.Items(I)
                                If ROW("FORMNAME") = DTR.Item(0) Then
                                    CHKFORMBOX.SetItemCheckState(I, CheckState.Checked)
                                    TEMPFORM = ROW("FORMNAME")
                                End If
                            Next
                        Next
                    End If


                    Dim clscommon As New ClsCommon
                    Dim dtID As DataTable
                    dtID = clscommon.search(" register_abbr, register_initials, register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
                    If dtID.Rows.Count > 0 Then
                        saleregabbr = dtID.Rows(0).Item(0).ToString
                        salereginitial = dtID.Rows(0).Item(1).ToString
                        saleregid = dtID.Rows(0).Item(2)
                    End If
                    GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1






                Else
                    edit = False
                    clear()
                End If
                CHKFORMBOX.Enabled = False
                CMDSELECTGDN.Enabled = False
                cmbregister.Enabled = False
                INVOICEDATE.Focus()
                INVOICEDATE.SelectAll()
                total()
            End If


            'THIS CODE IS WRITTEN BY GULKIT 
            'WE NEED TO SAVE THE INVOICE AUTO WHEN WE SAVE GR
            If AUTOSAVEFROMGR = True Then
                Call cmdOK_Click(sender, e)
                Me.Close()
            End If


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub TXTPCS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPCS.KeyPress, TXTLOTNO.KeyPress
        Try
            numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRATE.KeyPress, TXTMTRS.KeyPress, TXTGRSHORTAGE.KeyPress
        Try
            numdotkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtrate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTRATE.Validating
        Try
            If Val(TXTRATE.Text) > 0 Then CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPCS_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTPCS.Validated
        CALC()
    End Sub

    Private Sub TXTMTRS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTMTRS.Validating
        Try
            If Val(TXTMTRS.Text) > 0 Then CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbPER_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPER.Validating
        Try
            CALC()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            'If CMBPER.Text = "Mtrs" Then txtamount.Text = Format(Val(TXTMTRS.Text.Trim) * Val(TXTRATE.Text.Trim), "0.00") Else Format(Val(TXTPCS.Text.Trim) * Val(TXTRATE.Text.Trim), "0.00")

            TXTAMOUNT.Text = 0.0
            TXTGRIDTOTAL.Text = 0.0
            If CHKMANUAL.CheckState = CheckState.Unchecked Then
                TXTCGSTAMT.Text = 0.0
                TXTSGSTAMT.Text = 0.0
                TXTIGSTAMT.Text = 0.0
            End If

            If Val(TXTRATE.Text.Trim) > 0 And Val(TXTPCS.Text.Trim) > 0 And Val(TXTMTRS.Text.Trim) > 0 Then
                If CMBPER.Text = "Taka" Then
                    TXTAMOUNT.Text = Format(Val(TXTPCS.Text) * Val(TXTRATE.Text), "0.00")
                ElseIf CMBPER.Text.Trim = "Mtrs" Then
                    TXTAMOUNT.Text = Format(Val(TXTMTRS.Text) * Val(TXTRATE.Text), "0.00")
                Else
                    TXTAMOUNT.Text = ((Val(TXTMTRS.Text.Trim) / Val(CMBPER.Text.Trim)) * Val(TXTRATE.Text.Trim))
                End If
            End If
            TXTTAXABLEAMT.Text = Format((Val(TXTAMOUNT.Text.Trim) + Val(TXTOTHERAMT.Text.Trim)), "0.00")
            If CMBSCREENTYPE.Text = "LINE GST" And CHKMANUAL.CheckState = CheckState.Unchecked Then
                TXTCGSTAMT.Text = Format(Val(TXTCGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
                TXTSGSTAMT.Text = Format(Val(TXTSGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
                TXTIGSTAMT.Text = Format(Val(TXTIGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
            End If
            TXTGRIDTOTAL.Text = Format(Val(TXTTAXABLEAMT.Text) + Val(TXTCGSTAMT.Text) + Val(TXTSGSTAMT.Text) + Val(TXTIGSTAMT.Text), "0.00")
            total()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then FILLGREY(CMBQUALITY, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub TXTNARRATION_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNARRATION.Validating
    '    Try
    '        If CMBQUALITY.Text <> "" And Val(TXTPCS.Text) > 0 And Val(TXTMTRS.Text) > 0 Then fillgrid() Else MsgBox("Enter Proper Details !")
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Sub fillgrid()

        GRIDINVOICE.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDINVOICE.Rows.Add(Val(txtsrno.Text.Trim), CMBQUALITY.Text.Trim, TXTHSNCODE.Text.Trim, Val(TXTLOTNO.Text.Trim), TXTBALENO.Text.Trim, "", Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "dd/MM/yyyy"), "", Format(Val(TXTPCS.Text.Trim), "0"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.0000"), CMBPER.Text.Trim, Format(Val(TXTAMOUNT.Text.Trim), "0.00"), TXTNARRATION.Text.Trim, Format(Val(TXTOTHERAMT.Text.Trim), "0.00"), Format(Val(TXTTAXABLEAMT.Text.Trim), "0.00"), Val(TXTCGSTPER.Text.Trim), Format(Val(TXTCGSTAMT.Text.Trim), "0.00"), Val(TXTSGSTPER.Text.Trim), Format(Val(TXTSGSTAMT.Text.Trim), "0.00"), Val(TXTIGSTPER.Text.Trim), Format(Val(TXTIGSTAMT.Text.Trim), "0.00"), Format(Val(TXTGRIDTOTAL.Text.Trim), "0.00"), 0, 0, "", 0)
            getsrno(GRIDINVOICE)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDINVOICE.Item(GSRNO.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDINVOICE.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDINVOICE.Item(GHSNCODE.Index, TEMPROW).Value = TXTHSNCODE.Text.Trim
            GRIDINVOICE.Item(GLOTNO.Index, TEMPROW).Value = TXTLOTNO.Text.Trim
            GRIDINVOICE.Item(GBALENO.Index, TEMPROW).Value = TXTBALENO.Text.Trim
            GRIDINVOICE.Item(GPCS.Index, TEMPROW).Value = Format(Val(TXTPCS.Text.Trim), "0")
            GRIDINVOICE.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDINVOICE.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.0000")
            GRIDINVOICE.Item(GPER.Index, TEMPROW).Value = CMBPER.Text.Trim
            GRIDINVOICE.Item(GAMT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GNARRATION.Index, TEMPROW).Value = TXTNARRATION.Text.Trim
            GRIDINVOICE.Item(GOTHERAMT.Index, TEMPROW).Value = Format(Val(TXTOTHERAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GTAXABLEAMT.Index, TEMPROW).Value = Format(Val(TXTTAXABLEAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GCGSTPER.Index, TEMPROW).Value = Val(TXTCGSTPER.Text.Trim)
            GRIDINVOICE.Item(GCGSTAMT.Index, TEMPROW).Value = Format(Val(TXTCGSTAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GSGSTPER.Index, TEMPROW).Value = Val(TXTSGSTPER.Text.Trim)
            GRIDINVOICE.Item(GSGSTAMT.Index, TEMPROW).Value = Format(Val(TXTSGSTAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GIGSTPER.Index, TEMPROW).Value = Val(TXTIGSTPER.Text.Trim)
            GRIDINVOICE.Item(GIGSTAMT.Index, TEMPROW).Value = Format(Val(TXTIGSTAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GGRIDTOTAL.Index, TEMPROW).Value = Format(Val(TXTGRIDTOTAL.Text.Trim), "0.00")
            GRIDDOUBLECLICK = False
        End If
        total()
        GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1

        txtsrno.Clear()
        CMBQUALITY.Text = ""
        TXTHSNCODE.Clear()
        TXTLOTNO.Clear()
        TXTBALENO.Clear()
        TXTPCS.Clear()
        TXTMTRS.Clear()
        TXTRATE.Clear()
        CMBPER.Text = ""
        TXTAMOUNT.Clear()
        TXTNARRATION.Clear()
        TXTOTHERAMT.Clear()
        TXTTAXABLEAMT.Clear()
        TXTCGSTPER.Clear()
        TXTCGSTAMT.Clear()
        TXTSGSTPER.Clear()
        TXTSGSTAMT.Clear()
        TXTIGSTPER.Clear()
        TXTIGSTAMT.Clear()
        TXTGRIDTOTAL.Clear()


        txtsrno.Text = GRIDINVOICE.RowCount + 1
        CMBQUALITY.Focus()

    End Sub

    Private Sub GRIDINVOICE_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDINVOICE.CellDoubleClick
        If e.RowIndex >= 0 And GRIDINVOICE.Item(GSRNO.Index, e.RowIndex).Value <> Nothing Then

            If Convert.ToBoolean(GRIDINVOICE.Rows(e.RowIndex).Cells(GDONE.Index).Value) = True Then
                MsgBox("Item Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Val(GRIDINVOICE.Rows(e.RowIndex).Cells(GFROMNO.Index).Value) > 0 Then
                MsgBox("Cannot Update, Line Fetched from Challan, You can only change the Rate and Narration", MsgBoxStyle.Critical)
                Exit Sub
            End If

            GRIDDOUBLECLICK = True
            txtsrno.Text = GRIDINVOICE.Item(GSRNO.Index, e.RowIndex).Value.ToString
            CMBQUALITY.Text = GRIDINVOICE.Item(GQUALITY.Index, e.RowIndex).Value.ToString
            TXTHSNCODE.Text = GRIDINVOICE.Item(GHSNCODE.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
            TXTLOTNO.Text = GRIDINVOICE.Item(GLOTNO.Index, e.RowIndex).Value.ToString
            TXTBALENO.Text = GRIDINVOICE.Item(GBALENO.Index, e.RowIndex).Value.ToString
            TXTPCS.Text = Val(GRIDINVOICE.Item(GPCS.Index, e.RowIndex).Value)
            TXTMTRS.Text = Val(GRIDINVOICE.Item(GMTRS.Index, e.RowIndex).Value)
            TXTRATE.Text = Val(GRIDINVOICE.Item(GRATE.Index, e.RowIndex).Value)
            CMBPER.Text = GRIDINVOICE.Item(GPER.Index, e.RowIndex).Value.ToString
            TXTAMOUNT.Text = Val(GRIDINVOICE.Item(GAMT.Index, e.RowIndex).Value)
            TXTNARRATION.Text = GRIDINVOICE.Item(GNARRATION.Index, e.RowIndex).Value.ToString
            TXTOTHERAMT.Text = GRIDINVOICE.Item(GOTHERAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
            TXTTAXABLEAMT.Text = GRIDINVOICE.Item(GTAXABLEAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
            TXTCGSTPER.Text = GRIDINVOICE.Item(GCGSTPER.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
            TXTCGSTAMT.Text = GRIDINVOICE.Item(GCGSTAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
            TXTSGSTPER.Text = GRIDINVOICE.Item(GSGSTPER.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
            TXTSGSTAMT.Text = GRIDINVOICE.Item(GSGSTAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
            TXTIGSTPER.Text = GRIDINVOICE.Item(GIGSTPER.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
            TXTIGSTAMT.Text = GRIDINVOICE.Item(GIGSTAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
            TXTGRIDTOTAL.Text = GRIDINVOICE.Item(GGRIDTOTAL.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString

            TEMPROW = e.RowIndex
            CMBQUALITY.Focus()
        End If
    End Sub

    Private Sub GRIDINVOICE_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDINVOICE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDINVOICE.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDINVOICE.Rows.RemoveAt(GRIDINVOICE.CurrentRow.Index)
                total()
                getsrno(GRIDINVOICE)
                'total()


                'RECHECK THE CHALLAN NOS AFTER DELETING A LINE
                'GETTING DISTINCT CHALLAN NO IN TEXTBOX
                Dim ARR As New ArrayList
                For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                    If Val(ROW.Cells(GFROMNO.Index).Value) > 0 Then
                        If ARR.Count = 0 Then
                            ARR.Add(Val(ROW.Cells(GFROMNO.Index).Value))
                        Else
                            'FIRST CHECK WHETHER THE CHALLANNO IS ALREADY PRESENT IN ARR OR NOT
                            For I As Integer = 0 To ARR.Count - 1
                                If ARR(I) = Val(ROW.Cells(GFROMNO.Index).Value) Then
                                    GoTo LINE1
                                End If
                            Next
                            ARR.Add(Val(ROW.Cells(GFROMNO.Index).Value))
LINE1:
                        End If
                    End If
                Next
                TXTCHALLANNO.Clear()
                For I As Integer = 0 To ARR.Count - 1
                    If TXTCHALLANNO.Text.Trim = "" Then
                        TXTCHALLANNO.Text = ARR.Item(I).ToString
                    Else
                        TXTCHALLANNO.Text = TXTCHALLANNO.Text & "," & ARR.Item(I).ToString
                    End If
                Next

            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGREY As New SelectGreyQuality
                OBJGREY.ShowDialog()
                If OBJGREY.TEMPNAME <> "" Then CMBQUALITY.Text = OBJGREY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETHSNCODE()
        Try
            If Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/07/2017" Then
                TXTHSNCODE.Clear()
                TXTCGSTPER.Clear()
                TXTCGSTAMT.Clear()
                TXTSGSTPER.Clear()
                TXTSGSTAMT.Clear()
                TXTIGSTPER.Clear()
                TXTIGSTAMT.Clear()

                If CHKMANUAL.CheckState = CheckState.Unchecked Then
                    TXTCGSTPER1.Clear()
                    TXTCGSTAMT1.Clear()
                    TXTSGSTPER1.Clear()
                    TXTSGSTAMT1.Clear()
                    TXTIGSTPER1.Clear()
                    TXTIGSTAMT1.Clear()
                End If

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("   ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER,ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER ", "", "  GREYQUALITYMASTER LEFT OUTER JOIN HSNMASTER ON GREYQUALITYMASTER.GREY_HSNCODEID = HSNMASTER.HSN_ID ", " AND GREYQUALITYMASTER.GREY_NAME= '" & CMBQUALITY.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
                If DT.Rows.Count > 0 Then
                    TXTHSNCODE.Text = DT.Rows(0).Item("HSNCODE")
                    If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                        TXTIGSTPER.Text = 0
                        TXTIGSTPER1.Text = 0
                        TXTCGSTPER.Text = Val(DT.Rows(0).Item("CGSTPER"))
                        TXTSGSTPER.Text = Val(DT.Rows(0).Item("SGSTPER"))
                        TXTCGSTPER1.Text = Val(DT.Rows(0).Item("CGSTPER"))
                        TXTSGSTPER1.Text = Val(DT.Rows(0).Item("SGSTPER"))
                    Else
                        TXTCGSTPER.Text = 0
                        TXTSGSTPER.Text = 0
                        TXTCGSTPER1.Text = 0
                        TXTSGSTPER1.Text = 0
                        TXTIGSTPER1.Text = Val(DT.Rows(0).Item("IGSTPER"))
                        TXTIGSTPER.Text = Val(DT.Rows(0).Item("IGSTPER"))
                    End If
                End If
                CALC()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Validated
        Try
            If CMBQUALITY.Text.Trim <> "" And Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/07/2017" Then GETHSNCODE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim IntResult As Integer

            If Not AUTOSAVEFROMGR Then
                EP.Clear()
                If Not errorvalid() Then
                    Exit Sub
                End If
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(CMBSCREENTYPE.Text.Trim)
            alParaval.Add(Val(TXTINVOICENO.Text.Trim))
            alParaval.Add(cmbregister.Text.Trim)
            alParaval.Add(CMBTYPE.Text.Trim)
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(CMBAGENT.Text.Trim)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(TXTDISPLAYQUALITY.Text.Trim)

            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(CHALLANDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(TXTCHALLANTYPE.Text.Trim)

            alParaval.Add(Val(TXTCRDAYS.Text.Trim))
            alParaval.Add(Format(Convert.ToDateTime(DUEDATE.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(TXTFOLD.Text.Trim)
            alParaval.Add(Val(TXTORDERDISC.Text.Trim))
            alParaval.Add(TXTPARTYPONO.Text.Trim)


            alParaval.Add(TXTDELIVERYAT.Text.Trim)
            alParaval.Add(TXTLRNO.Text.Trim)
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

            If CHKMANUAL.Checked = True Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(Val(LBLTOTALPCS.Text.Trim))
            alParaval.Add(Val(LBLTOTALMTRS.Text.Trim))
            alParaval.Add(Val(lbltotalamt.Text.Trim))
            alParaval.Add(Val(LBLTOTALOTHERAMT.Text.Trim))
            alParaval.Add(Val(LBLTOTALTAXABLEAMT.Text.Trim))

            If CMBSCREENTYPE.Text = "TOTAL GST" Then
                alParaval.Add(Val(TXTCGSTPER1.Text.Trim))
                alParaval.Add(Val(TXTCGSTAMT1.Text.Trim))
                alParaval.Add(Val(TXTSGSTPER1.Text.Trim))
                alParaval.Add(Val(TXTSGSTAMT1.Text.Trim))
                alParaval.Add(Val(TXTIGSTPER1.Text.Trim))
                alParaval.Add(Val(TXTIGSTAMT1.Text.Trim))
            Else
                alParaval.Add(Val(TXTCGSTPER1.Text.Trim))
                alParaval.Add(Val(LBLTOTALCGSTAMT.Text.Trim))
                alParaval.Add(Val(TXTSGSTPER1.Text.Trim))
                alParaval.Add(Val(LBLTOTALSGSTAMT.Text.Trim))
                alParaval.Add(Val(TXTIGSTPER1.Text.Trim))
                alParaval.Add(Val(LBLTOTALIGSTAMT.Text.Trim))
            End If

            alParaval.Add(Val(TXTTOTALWITHGST.Text.Trim))
            If CHKTCS.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            alParaval.Add(Val(TXTTCSPER.Text.Trim))
            alParaval.Add(Val(TXTTCSAMT.Text.Trim))

            alParaval.Add(txtinwords.Text)

            alParaval.Add(Val(TXTGRAMT.Text.Trim))
            alParaval.Add(Val(TXTSHORTAGEAMT.Text.Trim))
            alParaval.Add(Val(txtbillamt.Text.Trim))
            alParaval.Add(Val(TXTTOTALTAXAMT.Text.Trim))
            alParaval.Add(Val(TXTTOTALOTHERCHGSAMT.Text.Trim))
            alParaval.Add(Val(TXTCHARGES.Text.Trim))
            alParaval.Add(Val(TXTSUBTOTAL.Text.Trim))
            alParaval.Add(Val(txtroundoff.Text.Trim))
            alParaval.Add(Val(txtgrandtotal.Text.Trim))

            alParaval.Add(Val(TXTAMTREC.Text.Trim))
            alParaval.Add(Val(TXTEXTRAAMT.Text.Trim))
            alParaval.Add(Val(TXTRETURN.Text.Trim))
            alParaval.Add(Val(TXTBAL.Text.Trim))


            alParaval.Add(TXTGRNO.Text.Trim)
            alParaval.Add(Val(TXTGRPCS.Text.Trim))
            alParaval.Add(Val(TXTGRMTRS.Text.Trim))
            alParaval.Add(Val(TXTGRSHORTAGE.Text.Trim))

            If CHKNOGR.CheckState = CheckState.Checked Then alParaval.Add(1) Else alParaval.Add(0)


            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


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
            Dim HSNCODE As String = ""
            Dim LOTNO As String = ""
            Dim BALENO As String = ""
            Dim LRNO As String = ""
            Dim LRDATE As String = ""
            Dim MILLNAME As String = ""
            Dim PCS As String = ""
            Dim MTRS As String = ""
            Dim RATE As String = ""         'value of RATE
            Dim PER As String = ""
            Dim AMT As String = ""         'value of AMT
            Dim NARRATION As String = ""
            Dim OTHERAMT As String = ""
            Dim TAXABLEAMT As String = ""
            Dim CGSTPER As String = ""
            Dim CGSTAMT As String = ""
            Dim SGSTPER As String = ""
            Dim SGSTAMT As String = ""
            Dim IGSTPER As String = ""
            Dim IGSTAMT As String = ""
            Dim GRIDTOTAL As String = ""

            Dim FROMNO As String = ""        'WHETHER GRN IS DONE FOR THIS LINE
            Dim FROMSRNO As String = ""
            Dim GRIDTYPE As String = ""
            Dim GRIDDONE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDINVOICE.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(GSRNO.Index).Value
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        HSNCODE = row.Cells(GHSNCODE.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value
                        BALENO = row.Cells(GBALENO.Index).Value
                        LRNO = row.Cells(GLRNO.Index).Value.ToString
                        LRDATE = Format(Convert.ToDateTime(row.Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy")
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        PCS = Val(row.Cells(GPCS.Index).Value)
                        MTRS = Val(row.Cells(GMTRS.Index).Value)
                        RATE = Format(Val(row.Cells(GRATE.Index).Value), "0.0000")
                        PER = row.Cells(GPER.Index).Value.ToString
                        AMT = Val(row.Cells(GAMT.Index).Value)
                        If row.Cells(GNARRATION.Index).Value <> Nothing Then NARRATION = row.Cells(GNARRATION.Index).Value.ToString Else NARRATION = ""

                        OTHERAMT = Val(row.Cells(GOTHERAMT.Index).Value)
                        TAXABLEAMT = Val(row.Cells(GTAXABLEAMT.Index).Value)
                        CGSTPER = row.Cells(GCGSTPER.Index).Value.ToString
                        CGSTAMT = Val(row.Cells(GCGSTAMT.Index).Value)
                        SGSTPER = row.Cells(GSGSTPER.Index).Value.ToString
                        SGSTAMT = Val(row.Cells(GSGSTAMT.Index).Value)
                        IGSTPER = row.Cells(GIGSTPER.Index).Value.ToString
                        IGSTAMT = Val(row.Cells(GIGSTAMT.Index).Value)
                        GRIDTOTAL = Val(row.Cells(GGRIDTOTAL.Index).Value)

                        FROMNO = row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = row.Cells(GFROMSRNO.Index).Value.ToString
                        GRIDTYPE = row.Cells(GTYPE.Index).Value.ToString
                        If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then
                            GRIDDONE = "1"
                        Else
                            GRIDDONE = "0"
                        End If

                    Else

                        gridsrno = gridsrno & "|" & row.Cells(GSRNO.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        HSNCODE = HSNCODE & "|" & row.Cells(GHSNCODE.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString
                        LRNO = LRNO & "|" & row.Cells(GLRNO.Index).Value.ToString
                        LRDATE = LRDATE & "|" & Format(Convert.ToDateTime(row.Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy")
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        PCS = PCS & "|" & row.Cells(GPCS.Index).Value
                        MTRS = MTRS & "|" & row.Cells(GMTRS.Index).Value
                        RATE = RATE & "|" & Format(Val(row.Cells(GRATE.Index).Value), "0.0000")
                        PER = PER & "|" & row.Cells(GPER.Index).Value.ToString
                        AMT = AMT & "|" & Val(row.Cells(GAMT.Index).Value)
                        If row.Cells(GNARRATION.Index).Value <> Nothing Then NARRATION = NARRATION & "|" & row.Cells(GNARRATION.Index).Value.ToString Else NARRATION = NARRATION & "|" & ""

                        OTHERAMT = OTHERAMT & "|" & Val(row.Cells(GOTHERAMT.Index).Value)
                        TAXABLEAMT = TAXABLEAMT & "|" & Val(row.Cells(GTAXABLEAMT.Index).Value)
                        CGSTPER = CGSTPER & "|" & row.Cells(GCGSTPER.Index).Value
                        CGSTAMT = CGSTAMT & "|" & Val(row.Cells(GCGSTAMT.Index).Value)
                        SGSTPER = SGSTPER & "|" & row.Cells(GSGSTPER.Index).Value
                        SGSTAMT = SGSTAMT & "|" & Val(row.Cells(GSGSTAMT.Index).Value)
                        IGSTPER = IGSTPER & "|" & row.Cells(GIGSTPER.Index).Value
                        IGSTAMT = IGSTAMT & "|" & Val(row.Cells(GIGSTAMT.Index).Value)
                        GRIDTOTAL = GRIDTOTAL & "|" & Val(row.Cells(GGRIDTOTAL.Index).Value)

                        FROMNO = FROMNO & "|" & row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = FROMSRNO & "|" & row.Cells(GFROMSRNO.Index).Value.ToString
                        GRIDTYPE = GRIDTYPE & "|" & row.Cells(GTYPE.Index).Value.ToString
                        If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then
                            GRIDDONE = GRIDDONE & "|" & "1"
                        Else
                            GRIDDONE = GRIDDONE & "|" & "0"
                        End If

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(QUALITY)
            alParaval.Add(HSNCODE)

            alParaval.Add(LOTNO)
            alParaval.Add(BALENO)
            alParaval.Add(LRNO)
            alParaval.Add(LRDATE)
            alParaval.Add(MILLNAME)
            alParaval.Add(PCS)
            alParaval.Add(MTRS)
            alParaval.Add(RATE)
            alParaval.Add(PER)
            alParaval.Add(AMT)
            alParaval.Add(NARRATION)

            alParaval.Add(OTHERAMT)
            alParaval.Add(TAXABLEAMT)
            alParaval.Add(CGSTPER)
            alParaval.Add(CGSTAMT)
            alParaval.Add(SGSTPER)
            alParaval.Add(SGSTAMT)
            alParaval.Add(IGSTPER)
            alParaval.Add(IGSTAMT)
            alParaval.Add(GRIDTOTAL)

            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(GRIDTYPE)
            alParaval.Add(GRIDDONE)


            Dim CSRNO As String = ""
            Dim CCHGS As String = ""
            Dim CPER As String = ""
            Dim CAMT As String = ""
            Dim CTAXID As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDCHGS.Rows
                If row.Cells(0).Value <> Nothing Then
                    If CSRNO = "" Then
                        CSRNO = row.Cells(ESRNO.Index).Value.ToString
                        CCHGS = row.Cells(ECHARGES.Index).Value.ToString
                        CPER = row.Cells(EPER.Index).Value.ToString
                        CAMT = Val(row.Cells(EAMT.Index).Value)
                        CTAXID = Val(row.Cells(ETAXID.Index).Value)
                    Else
                        CSRNO = CSRNO & "," & row.Cells(ESRNO.Index).Value.ToString
                        CCHGS = CCHGS & "," & row.Cells(ECHARGES.Index).Value.ToString
                        CPER = CPER & "," & row.Cells(EPER.Index).Value.ToString
                        CAMT = CAMT & "," & Val(row.Cells(EAMT.Index).Value)
                        CTAXID = CTAXID & "," & Val(row.Cells(ETAXID.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(CSRNO)
            alParaval.Add(CCHGS)
            alParaval.Add(CPER)
            alParaval.Add(CAMT)
            alParaval.Add(CTAXID)
            alParaval.Add(ClientName)

            alParaval.Add(TXTIRNNO.Text.Trim)
            alParaval.Add(TXTACKNO.Text.Trim)
            alParaval.Add(Format(ACKDATE.Value.Date, "MM/dd/yyyy"))
            If PBQRCODE.Image IsNot Nothing Then
                Dim MS As New IO.MemoryStream
                PBQRCODE.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                alParaval.Add(MS.ToArray)
            Else
                alParaval.Add(DBNull.Value)
            End If

            alParaval.Add(CMBDISPATCHFROM.Text.Trim)

            Dim OBJINV As New ClsInvoiceMaster()
            OBJINV.alParaval = alParaval

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = OBJINV.SAVE()
                TEMPINVOICENO = DT.Rows(0).Item(0)
                MessageBox.Show("Details Added")
                PRINTREPORT(DT.Rows(0).Item(0))
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPINVOICENO)
                IntResult = OBJINV.UPDATE()

                If Not AUTOSAVEFROMGR Then
                    MessageBox.Show("Details Updated")
                    PRINTREPORT(TEMPINVOICENO)
                End If
                edit = False
            End If

            'clear()
            If gridupload.RowCount > 0 Then SAVEUPLOAD()

            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            INVOICEDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub PRINTREPORT(ByVal INVOICENO As Integer)
        Try
            If MsgBox("Wish to Print Invoice?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim OBJINVOICE As New SaleDesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.FRMSTRING = "INVOICE"
                OBJINVOICE.INVTYPE = CMBTYPE.Text.Trim
                OBJINVOICE.INVSCREENTYPE = CMBSCREENTYPE.Text.Trim
                OBJINVOICE.WHERECLAUSE = "{INVOICEMASTER.INVOICE_NO}=" & Val(INVOICENO) & " and {REGISTERMASTER.REGISTER_NAME} = '" & cmbregister.Text.Trim & "' AND {INVOICEMASTER.INVOICE_yearid}=" & YearId
                OBJINVOICE.Show()
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

        If Val(TXTINVOICENO.Text.Trim) = 0 Then
            EP.SetError(TXTINVOICENO, "Enter Invoice No")
            bln = False
        End If


        If cmbname.Text.Trim.Length = 0 Then
            EP.SetError(cmbname, " Please Fill Company Name ")
            bln = False
        End If

        If GRIDINVOICE.RowCount = 0 Then
            EP.SetError(cmbname, "Enter Bill Details")
            bln = False
        End If


        Dim FORMTYPE As String = ""
        For Each DTROW As DataRowView In CHKFORMBOX.CheckedItems
            FORMTYPE = DTROW.Item(0)
        Next
        If FORMTYPE = Nothing Then
            EP.SetError(CHKFORMBOX, "Pls Select Form Type")
            bln = False
        End If

        Dim OBJCMN As New ClsCommon
        Dim DT As New DataTable
        If TXTINVOICENO.Text <> "" And cmbname.Text.Trim <> "" And edit = False Then
            DT = OBJCMN.search(" ISNULL(INVOICEMASTER.INVOICE_NO, '') AS INVOICENO, REGISTERMASTER.register_name AS REGNAME ", "", " INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id  AND INVOICEMASTER.INVOICE_YEARID = REGISTERMASTER.register_yearid ", "  AND INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "'  AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                EP.SetError(TXTINVOICENO, "Invoice No Already Exist")
                bln = False
            End If
        End If

        'If Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/02/2018" Or txtgrandtotal.Text > 50000 Then
        '    If TXTEWAYBILLNO.Text.Trim.Length = 0 Then
        '        If MsgBox("E-Way No. Not Entered, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
        '            EP.SetError(TXTEWAYBILLNO, " Please Enter E-Way No..... ")
        '            bln = False
        '        End If
        '    End If
        'End If

        If Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/07/2017" Then
            If TXTSTATECODE.Text.Trim.Length = 0 Then
                EP.SetError(TXTSTATECODE, "Please enter the state code")
                bln = False
            End If

            If TXTGSTIN.Text.Trim.Length = 0 Then
                If MsgBox("GSTIN Not Entered, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    EP.SetError(TXTSTATECODE, "Enter GSTIN in Party Master")
                    bln = False
                End If
            End If

            If CMPSTATECODE <> TXTSTATECODE.Text.Trim And (Val(LBLTOTALCGSTAMT.Text) > 0 Or Val(LBLTOTALSGSTAMT.Text.Trim) > 0) Then
                EP.SetError(TXTSTATECODE, "Invaid Entry Done in CGST/SGST")
                bln = False
            End If

            If CMPSTATECODE = TXTSTATECODE.Text.Trim And Val(LBLTOTALIGSTAMT.Text) > 0 Then
                EP.SetError(TXTSTATECODE, "Invaid Entry Done in IGST")
                bln = False
            End If
        End If


        For Each row As DataGridViewRow In GRIDINVOICE.Rows
            If Val(row.Cells(GAMT.Index).Value) = 0 Then
                EP.SetError(cmbname, "Amt Cannot be 0")
                bln = False
            End If
        Next

        If Val(txtgrandtotal.Text.Trim) = 0 Then
            EP.SetError(txtgrandtotal, "Amt Cannot be 0")
            bln = False
        End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Rec/Return Made , Delete Rec/Return First")
            bln = False
        End If

        If INVOICEDATE.Text = "__/__/____" Then
            EP.SetError(INVOICEDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(INVOICEDATE.Text) Then
                EP.SetError(INVOICEDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If


        'CHECK WHETHER PURCHASER HAS CROSSED 50LAKHS OR NOT
        If CHKTCS.CheckState = CheckState.Unchecked Then
            Dim TEMPTDSTOTAL As Double = Val(txtgrandtotal.Text.Trim)
            DT = OBJCMN.Execute_Any_String("SELECT ISNULL(SUM(INVOICE_GRANDTOTAL),0) AS GTOTAL FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = LEDGERS.ACC_ID WHERE INVOICE_YEARID = " & YearId & " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "'", "", "")
            If DT.Rows.Count > 0 Then TEMPTDSTOTAL += Val(DT.Rows(0).Item("GTOTAL"))
            If TEMPTDSTOTAL > 5000000 Then
                If MsgBox("Amount Exceeds 5000000, and TCS is not Applied, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    EP.SetError(cmbname, "Apply TCS")
                    bln = False
                End If
            End If
        End If

        Return bln
    End Function

    Sub TOTAL()
        Try

            TXTTCSPER.Text = 0
            TXTTCSAMT.Text = 0

            'FETCH TCSPERCENT WITH RESPECT TO DATE
            Dim OBJCMN As New ClsCommon
            Dim DTTCS As DataTable = OBJCMN.search("TOP 1 ISNULL(TCSPER,0) AS TCSPER", "", "TCSPERCENT", " AND TCSDATE <= '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' ORDER BY TCSDATE DESC")
            If DTTCS.Rows.Count > 0 Then TXTTCSPER.Text = Val(DTTCS.Rows(0).Item("TCSPER"))


            LBLTOTALPCS.Text = "0"
            LBLTOTALMTRS.Text = "0.0"
            lbltotalamt.Text = "0.0"
            LBLTOTALOTHERAMT.Text = "0.0"
            LBLTOTALTAXABLEAMT.Text = "0.0"
            LBLTOTALCGSTAMT.Text = "0.0"
            LBLTOTALSGSTAMT.Text = "0.0"
            LBLTOTALIGSTAMT.Text = "0.0"

            TXTGRAMT.Text = 0.0
            TXTSHORTAGEAMT.Text = 0.0
            txtbillamt.Text = 0.0
            TXTCHARGES.Text = 0.0
            TXTSUBTOTAL.Text = 0
            txtroundoff.Text = 0
            txtgrandtotal.Text = 0

            TXTACCEPTEDPCS.Text = 0.0
            TXTACCEPTEDMTRS.Text = 0.0

            If GRIDINVOICE.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDINVOICE.Rows
                    If row.Cells(GPER.Index).EditedFormattedValue = "Mtrs" Then
                        row.Cells(GAMT.Index).Value = Format((Val(row.Cells(GMTRS.Index).EditedFormattedValue)) * Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        TXTGRAMT.Text = Format(Val(TXTGRMTRS.Text.Trim) * Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        TXTSHORTAGEAMT.Text = Format(Val(TXTGRSHORTAGE.Text.Trim) * Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                    ElseIf Val(row.Cells(GPER.Index).EditedFormattedValue) = "5" Then
                        row.Cells(GAMT.Index).Value = Format(((row.Cells(GMTRS.Index).EditedFormattedValue / 5) * row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                    Else
                        row.Cells(GAMT.Index).Value = Format(Val(row.Cells(GPCS.Index).EditedFormattedValue) * Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        TXTGRAMT.Text = Format(Val(TXTGRPCS.Text.Trim) * Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        TXTSHORTAGEAMT.Text = Format(Val(TXTGRSHORTAGE.Text.Trim) * Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")

                    End If

                    If CMBSCREENTYPE.Text = "LINE GST" Then
                        row.Cells(GTAXABLEAMT.Index).Value = Format(Val(row.Cells(GAMT.Index).EditedFormattedValue) + Val(row.Cells(GOTHERAMT.Index).EditedFormattedValue), "0.00")

                        If CHKMANUAL.CheckState = CheckState.Unchecked Then
                            row.Cells(GCGSTAMT.Index).Value = Format((Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) * Val(row.Cells(GCGSTPER.Index).EditedFormattedValue) / 100), "0.00")
                            row.Cells(GSGSTAMT.Index).Value = Format((Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) * Val(row.Cells(GSGSTPER.Index).EditedFormattedValue) / 100), "0.00")
                            row.Cells(GIGSTAMT.Index).Value = Format((Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) * Val(row.Cells(GIGSTPER.Index).EditedFormattedValue) / 100), "0.00")
                        End If
                        row.Cells(GGRIDTOTAL.Index).Value = Format(Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) + Val(row.Cells(GCGSTAMT.Index).EditedFormattedValue) + Val(row.Cells(GSGSTAMT.Index).EditedFormattedValue) + Val(row.Cells(GIGSTAMT.Index).EditedFormattedValue), "0.00")
                    Else
                        row.Cells(GTAXABLEAMT.Index).Value = Format(Val(row.Cells(GAMT.Index).EditedFormattedValue) + Val(row.Cells(GOTHERAMT.Index).EditedFormattedValue), "0.00")
                        row.Cells(GGRIDTOTAL.Index).Value = Format(Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) + Val(row.Cells(GCGSTAMT.Index).EditedFormattedValue) + Val(row.Cells(GSGSTAMT.Index).EditedFormattedValue) + Val(row.Cells(GIGSTAMT.Index).EditedFormattedValue), "0.00")
                    End If


                    If Val(row.Cells(GPCS.Index).Value) > 0 Then LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(row.Cells(GPCS.Index).EditedFormattedValue), "0")
                    If Val(row.Cells(GMTRS.Index).Value) > 0 Then LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(row.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GAMT.Index).Value) > 0 Then lbltotalamt.Text = Format(Val(lbltotalamt.Text) + Val(row.Cells(GAMT.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GOTHERAMT.Index).Value) <> 0 Then LBLTOTALOTHERAMT.Text = Format(Val(LBLTOTALOTHERAMT.Text) + Val(row.Cells(GOTHERAMT.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GTAXABLEAMT.Index).Value) > 0 Then LBLTOTALTAXABLEAMT.Text = Format(Val(LBLTOTALTAXABLEAMT.Text) + Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue), "0.00")

                    If Val(row.Cells(GCGSTAMT.Index).Value) > 0 Then LBLTOTALCGSTAMT.Text = Format(Val(LBLTOTALCGSTAMT.Text) + Val(row.Cells(GCGSTAMT.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GSGSTAMT.Index).Value) > 0 Then LBLTOTALSGSTAMT.Text = Format(Val(LBLTOTALSGSTAMT.Text) + Val(row.Cells(GSGSTAMT.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GIGSTAMT.Index).Value) > 0 Then LBLTOTALIGSTAMT.Text = Format(Val(LBLTOTALIGSTAMT.Text) + Val(row.Cells(GIGSTAMT.Index).EditedFormattedValue), "0.00")

                    If Val(row.Cells(GGRIDTOTAL.Index).Value) > 0 Then
                        If Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/07/2017" Then txtbillamt.Text = Format(Val(txtbillamt.Text) + Val(row.Cells(GGRIDTOTAL.Index).EditedFormattedValue), "0.00") Else txtbillamt.Text = Format(Val(row.Cells(GGRIDTOTAL.Index).EditedFormattedValue) - Val(TXTGRAMT.Text.Trim) - Val(TXTSHORTAGEAMT.Text.Trim), "0.00")
                    End If

                Next
            End If

            If Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/07/2017" Then
                TXTACCEPTEDPCS.Text = Val(LBLTOTALPCS.Text)
                TXTACCEPTEDMTRS.Text = Format(Val(LBLTOTALMTRS.Text), "0.00")
            Else
                TXTACCEPTEDPCS.Text = Val(LBLTOTALPCS.Text) - Val(TXTGRPCS.Text.Trim)
                TXTACCEPTEDMTRS.Text = Format(Val(LBLTOTALMTRS.Text) - Val(TXTGRMTRS.Text.Trim) - Val(TXTGRSHORTAGE.Text.Trim), "0.00")
            End If

            ' doubt
            'txtbillamt.Text = Format(Val(lbltotalamt.Text.Trim) - Val(TXTGRAMT.Text.Trim) - Val(TXTSHORTAGEAMT.Text.Trim), "0.00")


            'If GRIDCHGS.RowCount > 0 Then
            '    For Each row As DataGridViewRow In GRIDCHGS.Rows
            '        If row.Cells(EPER.Index).Value <> 0 Then row.Cells(EAMT.Index).Value = Format((Val(row.Cells(EPER.Index).Value) * Val(txtbillamt.Text.Trim)) / 100, "0.00")
            '        TXTCHARGES.Text = Format(Val(TXTCHARGES.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
            '        If Val(row.Cells(ETAXID.Index).Value) > 0 Then TXTTOTALTAXAMT.Text = Format(Val(TXTTOTALTAXAMT.Text) + Val(row.Cells(EAMT.Index).Value), "0.00") Else TXTTOTALOTHERCHGSAMT.Text = Format(Val(TXTTOTALOTHERCHGSAMT.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
            '    Next
            'End If

            If GRIDCHGS.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDCHGS.Rows
                    'IF PERCENT IS > 0 THEN GETAUTO CHARGES
                    Dim DT As DataTable = OBJCMN.search("ISNULL(ACC_CALC,'GROSS') AS CALC", "", "LEDGERS", "AND ACC_CMPNAME = '" & row.Cells(ECHARGES.Index).Value & "' AND ACC_YEARID = " & YearId)
                    If DT.Rows(0).Item("CALC") = "GROSS" And Val(row.Cells(EPER.Index).Value) <> 0 Then
                        row.Cells(EAMT.Index).Value = Format((Val(row.Cells(EPER.Index).Value) * Val(txtbillamt.Text.Trim)) / 100, "0.00")
                    ElseIf DT.Rows(0).Item("CALC") = "NETT" And Val(row.Cells(EPER.Index).Value) <> 0 Then
                        TXTNETTAMT.Text = Val(txtbillamt.Text.Trim)
                        For I As Integer = 0 To row.Index - 1
                            TXTNETTAMT.Text = Format(Val(TXTNETTAMT.Text) + Val(GRIDCHGS.Rows(I).Cells(EAMT.Index).Value), "0.00")
                        Next
                        row.Cells(EAMT.Index).Value = Format((Val(row.Cells(EPER.Index).Value) * Val(TXTNETTAMT.Text.Trim)) / 100, "0.00")
                    End If
                    TXTCHARGES.Text = Format(Val(TXTCHARGES.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
                    If Val(row.Cells(ETAXID.Index).Value) > 0 Then TXTTOTALTAXAMT.Text = Format(Val(TXTTOTALTAXAMT.Text) + Val(row.Cells(EAMT.Index).Value), "0.00") Else TXTTOTALOTHERCHGSAMT.Text = Format(Val(TXTTOTALOTHERCHGSAMT.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
                Next
            End If

            TXTSUBTOTAL.Text = Format(Val(txtbillamt.Text) + Val(TXTCHARGES.Text.Trim), "0.00")


            If CMBSCREENTYPE.Text = "TOTAL GST" Then
                If CHKMANUAL.CheckState = CheckState.Unchecked Then
                    TXTCGSTAMT1.Text = Format((Val(TXTSUBTOTAL.Text.Trim) * Val(TXTCGSTPER1.Text.Trim)) / 100, "0.00")
                    LBLTOTALCGSTAMT.Text = Val(TXTCGSTAMT1.Text.Trim)
                    TXTSGSTAMT1.Text = Format((Val(TXTSUBTOTAL.Text.Trim) * Val(TXTSGSTPER1.Text.Trim)) / 100, "0.00")
                    LBLTOTALSGSTAMT.Text = Val(TXTSGSTAMT1.Text.Trim)
                    TXTIGSTAMT1.Text = Format((Val(TXTSUBTOTAL.Text.Trim) * Val(TXTIGSTPER1.Text.Trim)) / 100, "0.00")
                    LBLTOTALIGSTAMT.Text = Val(TXTIGSTAMT1.Text.Trim)
                End If

                TXTTOTALWITHGST.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT1.Text.Trim) + Val(TXTSGSTAMT1.Text.Trim) + Val(TXTIGSTAMT1.Text.Trim), "0.00")
                If CHKTCS.CheckState = CheckState.Checked Then TXTTCSAMT.Text = Format((Val(TXTTOTALWITHGST.Text.Trim) * Val(TXTTCSPER.Text.Trim)) / 100, "0.00")

                txtgrandtotal.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT1.Text.Trim) + Val(TXTSGSTAMT1.Text.Trim) + Val(TXTIGSTAMT1.Text.Trim) + Val(TXTTCSAMT.Text.Trim), "0")
                txtroundoff.Text = Format(Val(txtgrandtotal.Text) - (Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT1.Text.Trim) + Val(TXTSGSTAMT1.Text.Trim) + Val(TXTIGSTAMT1.Text.Trim) + Val(TXTTCSAMT.Text.Trim)), "0.00")
            Else

                TXTTOTALWITHGST.Text = Format(Val(TXTSUBTOTAL.Text.Trim), "0.00")
                If CHKTCS.CheckState = CheckState.Checked Then TXTTCSAMT.Text = Format((Val(TXTTOTALWITHGST.Text.Trim) * Val(TXTTCSPER.Text.Trim)) / 100, "0.00")

                txtgrandtotal.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTTCSAMT.Text.Trim), "0")
                txtroundoff.Text = Format(Val(txtgrandtotal.Text) - (Val(TXTSUBTOTAL.Text) + Val(TXTTCSAMT.Text.Trim)), "0.00")
            End If

            txtgrandtotal.Text = Format(Val(txtgrandtotal.Text), "0.00")
            If Val(txtgrandtotal.Text) > 0 Then txtinwords.Text = CurrencyToWord(txtgrandtotal.Text)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Dim IntResult As Integer
        Try

            If edit = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    MsgBox("Rec / Return Made, Delete Rec First", MsgBoxStyle.Critical)
                    Exit Sub
                End If


                'CHECKING WHETHER CFORM OR ANY OTHER FORM HAS BEEN RECD OR NOT, IF RECD THEN LOCK IT, CHECK IN CFORMVIEW WITH THIS INVOICENO
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("FORMNO", "", " CFORMVIEW ", " AND BILL = " & TEMPINVOICENO & " AND REGTYPE = '" & TEMPREGNAME & "' AND TYPE = 'SALE' AND FORMNO <> '' AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    MsgBox("Form Recd, Delete Form First", MsgBoxStyle.Critical)
                    Exit Sub
                End If


                If MsgBox("Delete Invoice ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTINVOICENO.Text.Trim)
                    alParaval.Add(TEMPREGNAME)
                    alParaval.Add(YearId)

                    Dim clspo As New ClsInvoiceMaster()
                    clspo.alParaval = alParaval
                    IntResult = clspo.Delete()
                    MsgBox("Invoice Deleted")
                    clear()
                    edit = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Toolprevious.Click
        Try
            GRIDINVOICE.RowCount = 0
LINE1:
            TEMPREGNAME = cmbregister.Text.Trim
            TEMPINVOICENO = Val(TXTINVOICENO.Text) - 1
            If TEMPINVOICENO > 0 Then
                edit = True
                INVOICEMASTER_Load(sender, e)
            Else
                clear()
                edit = False
            End If

            If GRIDINVOICE.RowCount = 0 And TEMPINVOICENO > 1 Then
                TXTINVOICENO.Text = TEMPINVOICENO
                GoTo LINE1
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDINVOICE.RowCount = 0
LINE1:
            TEMPINVOICENO = Val(TXTINVOICENO.Text) + 1
            TEMPREGNAME = cmbregister.Text.Trim
            getmax_INVOICE_no()
            Dim MAXNO As Integer = TXTINVOICENO.Text.Trim
            clear()
            If Val(TXTINVOICENO.Text) - 1 >= TEMPINVOICENO Then
                edit = True
                INVOICEMASTER_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If GRIDINVOICE.RowCount = 0 And TEMPINVOICENO < MAXNO Then
                TXTINVOICENO.Text = TEMPINVOICENO
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

            Dim objINVDTLS As New InvoiceDetails
            objINVDTLS.MdiParent = MDIMain
            objINVDTLS.Show()
            objINVDTLS.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            namevalidate(cmbname, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors' ", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        cmdOK_Click(sender, e)
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If edit = True Then PRINTREPORT(TEMPINVOICENO)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDELETE.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDINVOICE.RowCount = 0
                TEMPINVOICENO = Val(tstxtbillno.Text)
                TEMPREGNAME = cmbregister.Text.Trim
                If TEMPINVOICENO > 0 Then
                    edit = True
                    INVOICEMASTER_Load(sender, e)
                Else
                    clear()
                    edit = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'SALE'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'SALE' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If
            getmax_INVOICE_no()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            If cmbregister.Text.Trim.Length > 0 And edit = False Then
                clear()
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_abbr, register_initials, register_id", "", " RegisterMaster", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    saleregabbr = dt.Rows(0).Item(0).ToString
                    salereginitial = dt.Rows(0).Item(1).ToString
                    saleregid = dt.Rows(0).Item(2)
                    getmax_INVOICE_no()
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

    Public Function CloneWithValues(ByVal row As DataGridViewRow) As DataGridViewRow
        CloneWithValues = CType(row.Clone(), DataGridViewRow)
        For index As Int32 = 0 To row.Cells.Count - 1
            CloneWithValues.Cells(index).Value = row.Cells(index).Value
        Next
    End Function

    Private Sub CNNOTE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CNNOTE.Click
        Try
            If PBRECD.Visible = True Then
                MsgBox("Rec made, Delete Rec First", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If lbllocked.Visible = True Or PBlock.Visible = True Then
                MsgBox("Booking Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If edit = True Then
                Dim TEMPMSG As Integer = MsgBox("Wish to create Credit Note?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim OBJdN As New CREDITNOTE
                    OBJdN.MdiParent = MDIMain
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" REGISTER_INITIALS AS INITIALS", "", " REGISTERMASTER ", " AND REGISTER_NAME  = '" & cmbregister.Text.Trim & "' AND REGISTER_CMPID = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                    OBJdN.BILLNO = DT.Rows(0).Item("INITIALS") & "-" & Val(TXTINVOICENO.Text.Trim)
                    OBJdN.Show()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbname.Validated
        Try
            Dim OBJCMN As New ClsCommon
            If cmbname.Text.Trim <> "" Then
                'GET REGISTER , AGENCT AND TRANS
                Dim DT As DataTable = OBJCMN.search("ISNULL(LEDGERS_1.ACC_CMPNAME,'') AS TRANSNAME,ISNULL(LEDGERS_2.ACC_CMPNAME,'') AS AGENTNAME, ISNULL(REGISTER_NAME,'') AS REGISTERNAME, ISNULL(LEDGERS.ACC_CRDAYS,0) AS CRDAYS , ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN ", "", "    LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid AND LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_1.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_1.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_1.Acc_yearid LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_2.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_2.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_2.Acc_yearid LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.Acc_cmpid = REGISTER_cmpid AND LEDGERS.Acc_locationid = REGISTER_locationid AND LEDGERS.Acc_yearid = REGISTER_yearid AND LEDGERS.Acc_REGISTERID = REGISTER_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id  ", " and LEDGERS.acc_cmpname = '" & cmbname.Text.Trim & "' and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' and LEDGERS.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then
                    'cmbtrans.Text = DT.Rows(0).Item("TRANSNAME")
                    CMBAGENT.Text = DT.Rows(0).Item("AGENTNAME")
                    TXTCRDAYS.Text = Val(DT.Rows(0).Item("CRDAYS"))
                    TXTSTATECODE.Text = DT.Rows(0).Item("STATECODE")
                    TXTGSTIN.Text = DT.Rows(0).Item("GSTIN")
                    If Val(TXTCRDAYS.Text.Trim) > 0 Then DUEDATE.Text = DateAdd(DateInterval.Day, Val(TXTCRDAYS.Text.Trim), Convert.ToDateTime(INVOICEDATE.Text).Date)

                    If ClientName = "MASHOK" And TXTDELIVERYAT.Text.Trim = "" Then TXTDELIVERYAT.Text = cmbname.Text.Trim

                    If DT.Rows(0).Item("REGISTERNAME") <> cmbregister.Text.Trim And DT.Rows(0).Item("REGISTERNAME") <> "" Then
                        Dim TEMPMSG As Integer = MsgBox("Register is Different Change to Default?", MsgBoxStyle.YesNo)
                        If TEMPMSG = vbYes Then
                            cmbregister.Text = DT.Rows(0).Item("REGISTERNAME")
                            getmax_BILL_no()
                        End If
                    End If

                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBAGENT.Enter
        If CMBAGENT.Text.Trim = "" Then fillledger(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
    End Sub

    Private Sub CMBAGENT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBAGENT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBAGENT.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then namevalidate(CMBAGENT, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub getmax_BILL_no()
        Dim DTTABLE As New DataTable
        If cmbregister.Text <> "" Then
            DTTABLE = getmax(" isnull(max(INVOICEMASTER_no),0) + 1 ", " INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID AND INVOICE_CMPID = REGISTER_CMPID AND INVOICE_LOCATIONID = REGISTER_LOCATIONID AND INVOICE_YEARID = REGISTER_YEARID ", " and REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER_cmpid=" & CmpId & " and INVOICEMASTER_locationid=" & Locationid & " and INVOICEMASTER_yearid=" & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTINVOICENO.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Private Sub CMBTRANSCITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBFROMCITY.Validating
        Try
            If CMBFROMCITY.Text.Trim <> "" Then CITYVALIDATE(CMBFROMCITY, e, Me)
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

    Sub calchgs()
        Try
            'If Val(TXTCHGSPER.Text) <> 0 Then TXTCHGSAMT.Text = Format((Val(txtbillamt.Text) * Val(TXTCHGSPER.Text)) / 100, "0.00")
            If Val(TXTCHGSPER.Text) <> 0 Then
                'before CALC CHECK HOW TO CALC CHARGES
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" (CASE WHEN ISNULL(ACC_CALC,'') = '' THEN 'GROSS' ELSE ACC_CALC END) AS CALC", "", "LEDGERS", " AND ACC_CMPNAME = '" & CMBCHARGES.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("CALC") = "GROSS" Then
                    TXTCHGSAMT.Text = Format((Val(txtbillamt.Text) * Val(TXTCHGSPER.Text)) / 100, "0.00")
                ElseIf DT.Rows(0).Item("CALC") = "NETT" Then
                    'FIRST CALC NETT THEN ADD CHARGES ON THAT NETT TOTAL
                    TXTNETTAMT.Text = Val(txtbillamt.Text.Trim)
                    For Each ROW As DataGridViewRow In GRIDCHGS.Rows
                        If GRIDCHGSDOUBLECLICK = True And ROW.Index >= TEMPCHGSROW Then Exit For
                        TXTNETTAMT.Text = Format(Val(TXTNETTAMT.Text) + Val(ROW.Cells(EAMT.Index).Value), "0.00")
                    Next
                    TXTCHGSAMT.Text = Format((Val(TXTNETTAMT.Text) * Val(TXTCHGSPER.Text)) / 100, "0.00")
                ElseIf DT.Rows(0).Item("CALC") = "QTY" Then
                    TXTCHGSAMT.Text = Format((Val(LBLTOTALPCS.Text) * Val(TXTCHGSPER.Text)), "0.00")
                ElseIf DT.Rows(0).Item("CALC") = "MTRS" Then
                    TXTCHGSAMT.Text = Format((Val(LBLTOTALMTRS.Text) * Val(TXTCHGSPER.Text)), "0.00")
                End If
            End If
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

    Private Sub CMBCHARGES_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBCHARGES.Enter
        Try
            If CMBCHARGES.Text.Trim = "" Then fillname(CMBCHARGES, edit, " AND (GROUPMASTER.GROUP_SECONDARY ='Indirect Income' OR GROUPMASTER.GROUP_SECONDARY ='Indirect Expenses' or GROUPMASTER.GROUP_SECONDARY ='Direct Income' OR GROUPMASTER.GROUP_SECONDARY ='Direct Expenses' OR GROUPMASTER.GROUP_SECONDARY ='Duties & Taxes' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' OR GROUPMASTER.GROUP_SECONDARY = 'Sale A/C')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCHARGES.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND (GROUPMASTER.GROUP_SECONDARY ='Indirect Income' OR GROUPMASTER.GROUP_SECONDARY ='Indirect Expenses' or GROUPMASTER.GROUP_SECONDARY ='Direct Income' OR GROUPMASTER.GROUP_SECONDARY ='Direct Expenses' OR GROUPMASTER.GROUP_SECONDARY ='Duties & Taxes' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' OR GROUPMASTER.GROUP_SECONDARY = 'Sale A/C')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBCHARGES.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCHARGES.Validated
        Try
            If CMBCHARGES.Text.Trim <> "" Then
                filltax()

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
            Else
                TXTCHGSPER.Clear()
                TXTCHGSAMT.Clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCHARGES.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" Then namevalidate(CMBCHARGES, CMBCODE, e, Me, TXTTRANSADD, " AND (GROUPMASTER.GROUP_SECONDARY ='Indirect Income' OR GROUPMASTER.GROUP_SECONDARY ='Indirect Expenses' or GROUPMASTER.GROUP_SECONDARY ='Direct Income' OR GROUPMASTER.GROUP_SECONDARY ='Direct Expenses' OR GROUPMASTER.GROUP_SECONDARY ='Duties & Taxes' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' OR GROUPMASTER.GROUP_SECONDARY = 'Sale A/C')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSAMT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCHGSAMT.KeyPress
        Try
            AMOUNTNUMDOTKYEPRESS(e, TXTCHGSAMT, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function CHECKVALIDATE() As Boolean
        Try
            Dim BLN As Boolean = True
            For Each ROW As DataGridViewRow In GRIDCHGS.Rows
                If GRIDCHGSDOUBLECLICK = False Or (GRIDCHGSDOUBLECLICK = True And TEMPCHGSROW <> ROW.Index) Then
                    If CMBCHARGES.Text.Trim = ROW.Cells(ECHARGES.Index).Value Then
                        BLN = False
                    End If
                End If
            Next
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub TXTCHGSAMT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHGSAMT.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" And Val(TXTCHGSAMT.Text.Trim) <> 0 Then
                Dim dDebit As Decimal
                Dim bValid As Boolean = Decimal.TryParse(TXTCHGSAMT.Text.Trim, dDebit)
                If bValid Then
                    TXTCHGSAMT.Text = Convert.ToDecimal(Val(TXTCHGSAMT.Text))
                    ' everything is good
                    'CHECK WHETHER IT IS ALREADY PRESENT IN GRID OR NOT
                    If Not CHECKVALIDATE() Then
                        MsgBox("Charges Already Present Below", MsgBoxStyle.Critical)
                        CMBCHARGES.Focus()
                        Exit Sub
                    End If
                    fillchgsgrid()
                    total()
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
                    CMBCHARGES.Focus()
                    Exit Sub
                ElseIf Val(TXTCHGSPER.Text.Trim) = 0 And Val(TXTCHGSAMT.Text.Trim) = 0 Then
                    MsgBox("Amount can not be zero")
                    TXTCHGSAMT.Clear()
                    TXTCHGSAMT.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
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
        total()

        GRIDCHGS.FirstDisplayedScrollingRowIndex = GRIDCHGS.RowCount - 1

        TXTCHGSSRNO.Text = Val(GRIDCHGS.RowCount - 1) + 1
        CMBCHARGES.Text = ""
        TXTCHGSPER.Clear()
        TXTCHGSAMT.Clear()
        ' TXTTAXID.Clear()
        If TXTCHGSPER.ReadOnly = True Then TXTCHGSPER.ReadOnly = False
        CMBCHARGES.Focus()
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

        TXTUPLOADSRNO.Text = gridupload.RowCount + 1
        txtuploadremarks.Clear()
        txtuploadname.Clear()
        PBSOFTCOPY.Image = Nothing
        TXTIMGPATH.Clear()

        txtuploadremarks.Focus()

    End Sub

    Private Sub gridupload_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If e.RowIndex >= 0 Then PBSOFTCOPY.Image = gridupload.Rows(e.RowIndex).Cells(GUIMGPATH.Index).Value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDUPLOAD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDUPLOAD.Click
        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpg;*.png"
        OpenFileDialog1.ShowDialog()
        TXTIMGPATH.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If TXTIMGPATH.Text.Trim.Length <> 0 Then PBSOFTCOPY.ImageLocation = TXTIMGPATH.Text.Trim
    End Sub

    Private Sub txtuploadsrno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTUPLOADSRNO.GotFocus
        If GRIDUPLOADDOUBLECLICK = False Then
            If gridupload.RowCount > 0 Then
                TXTUPLOADSRNO.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1
            Else
                TXTUPLOADSRNO.Text = 1
            End If
        End If
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

    Sub SAVEUPLOAD()
        Try
            Dim OBJPO As New ClsInvoiceMaster

            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPINVOICENO)
                    ALPARAVAL.Add(TEMPREGNAME)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJPO.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJPO.SAVEUPLOAD()
                End If
            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCHGS_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDCHGS.CellDoubleClick
        EDITCHGSROW()
    End Sub

    Private Sub GRIDCHGS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCHGS.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDCHGS.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbMERCHANT.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDCHGSDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDCHGS.Rows.RemoveAt(GRIDCHGS.CurrentRow.Index)
                total()
                getsrno(GRIDCHGS)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITCHGSROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub EDITCHGSROW()
        Try
            TXTCHGSPER.ReadOnly = False
            TXTCHGSAMT.ReadOnly = False
            If GRIDCHGS.CurrentRow.Index >= 0 And GRIDCHGS.Item(GSRNO.Index, GRIDCHGS.CurrentRow.Index).Value <> Nothing Then
                GRIDCHGSDOUBLECLICK = True
                TXTCHGSSRNO.Text = GRIDCHGS.Item(GSRNO.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                CMBCHARGES.Text = GRIDCHGS.Item(ECHARGES.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTCHGSPER.Text = GRIDCHGS.Item(EPER.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTCHGSAMT.Text = GRIDCHGS.Item(EAMT.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTTAXID.Text = GRIDCHGS.Item(ETAXID.Index, GRIDCHGS.CurrentRow.Index).Value.ToString

                If Val(TXTCHGSPER.Text.Trim) > 0 Then TXTCHGSAMT.ReadOnly = True

                TEMPCHGSROW = GRIDCHGS.CurrentRow.Index
                TXTCHGSSRNO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSCITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBFROMCITY.Enter
        Try
            If CMBFROMCITY.Text.Trim = "" Then fillCITY(CMBFROMCITY, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTOCITY.Enter
        Try
            If CMBTOCITY.Text.Trim = "" Then fillCITY(CMBTOCITY, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCRDAYS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCRDAYS.KeyPress
        numkeypress(e, TXTCRDAYS, Me)
    End Sub

    Private Sub TXTCRDAYS_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTCRDAYS.Validated
        Try
            If Val(TXTCRDAYS.Text.Trim) > 0 Then DUEDATE.Text = DateAdd(DateInterval.Day, Val(TXTCRDAYS.Text.Trim), Convert.ToDateTime(INVOICEDATE.Text).Date)
            GRIDINVOICE.Focus()
            'GRIDINVOICE.CurrentCell.Value = GRIDINVOICE.Rows(0).Cells(GRATE.Index)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors' AND ACC_SUBTYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
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

    Private Sub TXTCHGSPER_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCHGSPER.KeyPress
        Try
            AMOUNTNUMDOTKYEPRESS(e, TXTCHGSPER, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSPER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHGSPER.Validating
        Try
            Dim dDebit As Decimal
            Dim bValid As Boolean = Decimal.TryParse(TXTCHGSPER.Text.Trim, dDebit)
            If bValid Then
                If Val(TXTCHGSPER.Text) = 0 Then TXTCHGSPER.Text = ""
                TXTCHGSPER.Text = Convert.ToDecimal(Val(TXTCHGSPER.Text))
                '' everything is good
                calchgs()
            ElseIf Val(TXTCHGSPER.Text.Trim) = 0 Then
                TXTCHGSAMT.ReadOnly = False
            Else
                MessageBox.Show("Invalid Number Entered")
                'e.Cancel = True
                TXTCHGSPER.Clear()
                TXTCHGSPER.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDINVOICE_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDINVOICE.CellValidating
        Dim colNum As Integer = GRIDINVOICE.Columns(e.ColumnIndex).Index
        If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return
        Select Case colNum

            Case GRATE.Index, GOTHERAMT.Index
                Dim dDebit As Decimal
                Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                If bValid Then
                    If GRIDINVOICE.CurrentCell.Value = Nothing Then GRIDINVOICE.CurrentCell.Value = "0.00"
                    GRIDINVOICE.CurrentCell.Value = Convert.ToDecimal(GRIDINVOICE.Item(colNum, e.RowIndex).Value)
                    '' everything is good
                    total()
                Else
                    MessageBox.Show("Invalid Number Entered")
                    e.Cancel = True
                    Exit Sub
                End If
            Case GPER.Index
                total()
        End Select

    End Sub

    Private Sub TXTINVOICENO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTINVOICENO.KeyPress
        numkeypress(e, TXTINVOICENO, Me)
    End Sub

    Private Sub TXTINVOICENO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTINVOICENO.Validating
        Try
            If Val(TXTINVOICENO.Text.Trim) <> 0 And cmbregister.Text.Trim <> "" And edit = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(INVOICEMASTER.INVOICE_NO,0)  AS INVNO", "", " INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID  AND INVOICE_YEARID = REGISTER_YEARID ", "  AND INVOICEMASTER.INVOICE_NO=" & TXTINVOICENO.Text.Trim & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Invoice No Already Exist")
                    e.Cancel = True
                End If
            End If
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
                If Val(TXTCHGSPER.Text.Trim) > 0 Then TXTCHGSAMT.ReadOnly = True
                TXTCHGSPER.ReadOnly = True
            End If
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

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then fillname(cmbname, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdshowdetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshowdetails.Click
        Try
            Dim OBJRECPAY As New ShowRecPay
            OBJRECPAY.MdiParent = MDIMain
            OBJRECPAY.PURBILLINITIALS = salereginitial & "-" & TEMPINVOICENO
            OBJRECPAY.SALEBILLINITIALS = salereginitial & "-" & TEMPINVOICENO
            OBJRECPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub INVOICEDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles INVOICEDATE.GotFocus
        INVOICEDATE.SelectAll()
    End Sub

    Private Sub INVOICEDATE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles INVOICEDATE.Validated
        If ClientName = "MASHOK" And CHALLANDATE.Text.Trim = "__/__/____" Then CHALLANDATE.Text = INVOICEDATE.Text.Trim
    End Sub

    Private Sub INVOICEDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INVOICEDATE.Validating
        Try
            If INVOICEDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(INVOICEDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DUEDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DUEDATE.Validating
        Try
            If DUEDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DUEDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTCHALLAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTGDN.Click
        Try

            If (edit = True And USEREDIT = False And USERVIEW = False) Or (edit = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If


            Dim OBJCMN As New ClsCommon()
            Dim OBJSELECTGDN As New SelectGDN
            OBJSELECTGDN.PARTYNAME = cmbname.Text.Trim
            OBJSELECTGDN.TYPE = CMBTYPE.Text.Trim
            OBJSELECTGDN.ShowDialog()
            Dim DTCHALLAN As DataTable = OBJSELECTGDN.DT
            If DTCHALLAN.Rows.Count > 0 Then

                cmbname.Text = DTCHALLAN.Rows(0).Item("NAME")
                cmbname_Validated(sender, e)
                CMBAGENT.Text = DTCHALLAN.Rows(0).Item("AGENT")
                cmbtrans.Text = DTCHALLAN.Rows(0).Item("TRANSNAME")
                CHALLANDATE.Text = DTCHALLAN.Rows(0).Item("CHALLANDATE")
                TXTCHALLANTYPE.Text = DTCHALLAN.Rows(0).Item("CHALLANTYPE")
                TXTDELIVERYAT.Text = DTCHALLAN.Rows(0).Item("SHIPTO")

                'GET TOCITY WITH RESPECT TO SHIP TO... IF SHIP TO IS NOT PRESENT THEN FETCH CITY OF PARTYNAME
                If CMBTOCITY.Text.Trim = "" And TXTDELIVERYAT.Text.Trim <> "" Then
                    Dim DTCITY As DataTable = OBJCMN.search("ISNULL(CITY_NAME,'') AS CITYNAME", "", " LEDGERS LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID", " AND LEDGERS.ACC_CMPNAME = '" & TXTDELIVERYAT.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                    If DTCITY.Rows.Count > 0 Then CMBTOCITY.Text = DTCITY.Rows(0).Item("CITYNAME")
                End If

                ''  GETTING DISTINCT CHALLAN NO IN TEXTBOX
                Dim DV As DataView = DTCHALLAN.DefaultView
                    Dim NEWDT As DataTable = DV.ToTable(True, "CHALLANNO")
                For Each DTROWPS As DataRow In NEWDT.Rows

                    If TXTCHALLANNO.Text.Trim = "" Then
                        TXTCHALLANNO.Text = DTROWPS("CHALLANNO").ToString
                    Else
                        TXTCHALLANNO.Text = TXTCHALLANNO.Text & "," & DTROWPS("CHALLANNO").ToString
                    End If

                    Dim DT1 As New DataTable
                    If DTCHALLAN.Rows(0).Item("CHALLANTYPE") = "CHALLAN" Then
                        If CMBTYPE.Text = "GREY" Then

                            If ClientName = "HARIA" Then
                                'WITHOUT BALENO
                                DT1 = OBJCMN.search(" CHALLANMASTER.CHALLAN_NO AS CHALLANNO, 0 AS CHALLANSRNO, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS QUALITY, ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0) AS LOTNO, SUM(ISNULL(CHALLANMASTER_DESC.CHALLAN_TAKA, 0)) AS PCS, SUM(ISNULL(CHALLANMASTER_DESC.CHALLAN_MTRS, 0)) AS MTRS, ISNULL(CHALLANMASTER_DESC.CHALLAN_NARR, '') AS gridnarr, ISNULL(CHALLANMASTER.CHALLAN_ORDERTYPE, 'SO') AS ORDERTYPE, ISNULL(CHALLANMASTER.CHALLAN_GRDONO, '') AS GRNO, ISNULL(CHALLANMASTER.CHALLAN_GRPCS, 0) AS GRPCS, ISNULL(CHALLANMASTER.CHALLAN_GRMTRS, 0) AS GRMTRS, ISNULL(CHALLANMASTER.CHALLAN_SHORTAGE, 0) AS SHORTAGE, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER, ISNULL(CHALLANMASTER_DESC.CHALLAN_LRNO,'') AS LRNO, ISNULL(CHALLANMASTER_DESC.CHALLAN_LRDATE, GETDATE()) AS LRDATE, ISNULL(MILLLEDGERS.ACC_CMPNAME,'') AS MILLNAME , 0 AS BALENO, ISNULL(CHALLANMASTER.CHALLAN_FOLD,'') AS FOLD, ISNULL(CHALLANMASTER.CHALLAN_VEHICLENO,'') AS VEHICLENO, ISNULL(CHALLANMASTER.CHALLAN_DISPLAYQUALITYNAME,'') AS DISPLAYQUALITYNAME", "", " CHALLANMASTER INNER JOIN CHALLANMASTER_DESC ON CHALLANMASTER.CHALLAN_NO = CHALLANMASTER_DESC.CHALLAN_NO AND CHALLANMASTER.CHALLAN_YEARID = CHALLANMASTER_DESC.CHALLAN_YEARID INNER JOIN GREYQUALITYMASTER ON CHALLANMASTER_DESC.CHALLAN_QUALITYID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN HSNMASTER ON GREYQUALITYMASTER.GREY_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN LEDGERS AS MILLLEDGERS ON CHALLAN_MILLID = MILLLEDGERS.ACC_ID ", "  and CHALLANMASTER.CHALLAN_NO = " & DTROWPS("CHALLANNO") & "  AND CHALLANMASTER.CHALLAN_DONE = 0 AND CHALLANMASTER.CHALLAN_YEARID = " & YearId & " GROUP BY CHALLANMASTER.CHALLAN_NO, ISNULL(GREYQUALITYMASTER.GREY_NAME, ''), ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0), ISNULL(CHALLANMASTER_DESC.CHALLAN_NARR, ''), ISNULL(CHALLANMASTER.CHALLAN_ORDERTYPE, 'SO'), ISNULL(CHALLANMASTER.CHALLAN_GRDONO, ''), ISNULL(CHALLANMASTER.CHALLAN_GRPCS, 0), ISNULL(CHALLANMASTER.CHALLAN_GRMTRS, 0), ISNULL(CHALLANMASTER.CHALLAN_SHORTAGE, 0), ISNULL(HSNMASTER.HSN_CODE, ''), ISNULL(HSNMASTER.HSN_CGST, 0), ISNULL(HSNMASTER.HSN_SGST, 0), ISNULL(HSNMASTER.HSN_IGST, 0), ISNULL(CHALLANMASTER_DESC.CHALLAN_LRNO,''),  ISNULL(CHALLANMASTER_DESC.CHALLAN_LRDATE, GETDATE()), ISNULL(MILLLEDGERS.ACC_CMPNAME,''), ISNULL(CHALLANMASTER.CHALLAN_FOLD,''), ISNULL(CHALLANMASTER.CHALLAN_VEHICLENO,''), ISNULL(CHALLANMASTER.CHALLAN_DISPLAYQUALITYNAME,'')  ")
                            Else
                                DT1 = OBJCMN.search(" CHALLANMASTER.CHALLAN_NO AS CHALLANNO, 0 AS CHALLANSRNO, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS QUALITY, ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0) AS LOTNO, SUM(ISNULL(CHALLANMASTER_DESC.CHALLAN_TAKA, 0)) AS PCS, SUM(ISNULL(CHALLANMASTER_DESC.CHALLAN_MTRS, 0)) AS MTRS, ISNULL(CHALLANMASTER_DESC.CHALLAN_NARR, '') AS gridnarr, ISNULL(CHALLANMASTER.CHALLAN_ORDERTYPE, 'SO') AS ORDERTYPE, ISNULL(CHALLANMASTER.CHALLAN_GRDONO, '') AS GRNO, ISNULL(CHALLANMASTER.CHALLAN_GRPCS, 0) AS GRPCS, ISNULL(CHALLANMASTER.CHALLAN_GRMTRS, 0) AS GRMTRS, ISNULL(CHALLANMASTER.CHALLAN_SHORTAGE, 0) AS SHORTAGE, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER, ISNULL(CHALLANMASTER_DESC.CHALLAN_LRNO,'') AS LRNO, ISNULL(CHALLANMASTER_DESC.CHALLAN_LRDATE, GETDATE()) AS LRDATE, ISNULL(MILLLEDGERS.ACC_CMPNAME,'') AS MILLNAME ,ISNULL(CHALLANMASTER_DESC.CHALLAN_BALENO,0) AS BALENO, ISNULL(CHALLANMASTER.CHALLAN_FOLD,'') AS FOLD, ISNULL(CHALLANMASTER.CHALLAN_VEHICLENO,'') AS VEHICLENO, ISNULL(CHALLANMASTER.CHALLAN_DISPLAYQUALITYNAME,'') AS DISPLAYQUALITYNAME", "", " CHALLANMASTER INNER JOIN CHALLANMASTER_DESC ON CHALLANMASTER.CHALLAN_NO = CHALLANMASTER_DESC.CHALLAN_NO AND CHALLANMASTER.CHALLAN_YEARID = CHALLANMASTER_DESC.CHALLAN_YEARID INNER JOIN GREYQUALITYMASTER ON CHALLANMASTER_DESC.CHALLAN_QUALITYID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN HSNMASTER ON GREYQUALITYMASTER.GREY_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN LEDGERS AS MILLLEDGERS ON CHALLAN_MILLID = MILLLEDGERS.ACC_ID ", "  and CHALLANMASTER.CHALLAN_NO = " & DTROWPS("CHALLANNO") & "  AND CHALLANMASTER.CHALLAN_DONE = 0 AND CHALLANMASTER.CHALLAN_YEARID = " & YearId & " GROUP BY CHALLANMASTER.CHALLAN_NO, ISNULL(GREYQUALITYMASTER.GREY_NAME, ''), ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0), ISNULL(CHALLANMASTER_DESC.CHALLAN_NARR, ''), ISNULL(CHALLANMASTER.CHALLAN_ORDERTYPE, 'SO'), ISNULL(CHALLANMASTER.CHALLAN_GRDONO, ''), ISNULL(CHALLANMASTER.CHALLAN_GRPCS, 0), ISNULL(CHALLANMASTER.CHALLAN_GRMTRS, 0), ISNULL(CHALLANMASTER.CHALLAN_SHORTAGE, 0), ISNULL(HSNMASTER.HSN_CODE, ''), ISNULL(HSNMASTER.HSN_CGST, 0), ISNULL(HSNMASTER.HSN_SGST, 0), ISNULL(HSNMASTER.HSN_IGST, 0), ISNULL(CHALLANMASTER_DESC.CHALLAN_LRNO,''),  ISNULL(CHALLANMASTER_DESC.CHALLAN_BALENO,0),ISNULL(CHALLANMASTER_DESC.CHALLAN_LRDATE, GETDATE()), ISNULL(MILLLEDGERS.ACC_CMPNAME,''), ISNULL(CHALLANMASTER.CHALLAN_FOLD,''), ISNULL(CHALLANMASTER.CHALLAN_VEHICLENO,''), ISNULL(CHALLANMASTER.CHALLAN_DISPLAYQUALITYNAME,'')  ")
                            End If

                        Else
                            DT1 = OBJCMN.search(" CHALLANMASTER.CHALLAN_NO AS CHALLANNO, 0 AS CHALLANSRNO, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0) AS LOTNO, SUM(ISNULL(CHALLANMASTER_DESC.CHALLAN_TAKA, 0)) AS PCS, SUM(ISNULL(CHALLANMASTER_DESC.CHALLAN_MTRS, 0)) AS MTRS, ISNULL(CHALLANMASTER_DESC.CHALLAN_NARR, '') AS gridnarr, ISNULL(CHALLANMASTER.CHALLAN_ORDERTYPE, 'SO') AS ORDERTYPE, ISNULL(CHALLANMASTER.CHALLAN_GRDONO, '') AS GRNO, ISNULL(CHALLANMASTER.CHALLAN_GRPCS, 0) AS GRPCS, ISNULL(CHALLANMASTER.CHALLAN_GRMTRS, 0) AS GRMTRS, ISNULL(CHALLANMASTER.CHALLAN_SHORTAGE, 0) AS SHORTAGE, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER, ISNULL(CHALLANMASTER_DESC.CHALLAN_LRNO,'') AS LRNO, ISNULL(CHALLANMASTER_DESC.CHALLAN_LRDATE, GETDATE()) AS LRDATE, ISNULL(MILLLEDGERS.ACC_CMPNAME,'') AS MILLNAME,ISNULL(CHALLANMASTER_DESC.CHALLAN_BALENO,0) AS BALENO, ISNULL(CHALLANMASTER.CHALLAN_FOLD,'') AS FOLD, ISNULL(CHALLANMASTER.CHALLAN_VEHICLENO,'') AS VEHICLENO, ISNULL(CHALLANMASTER.CHALLAN_DISPLAYQUALITYNAME,'') AS DISPLAYQUALITYNAME ", "", " CHALLANMASTER INNER JOIN CHALLANMASTER_DESC ON CHALLANMASTER.CHALLAN_NO = CHALLANMASTER_DESC.CHALLAN_NO AND CHALLANMASTER.CHALLAN_YEARID = CHALLANMASTER_DESC.CHALLAN_YEARID INNER JOIN QUALITYMASTER ON CHALLANMASTER_DESC.CHALLAN_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN HSNMASTER ON QUALITYMASTER.QUALITY_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN LEDGERS AS MILLLEDGERS ON CHALLAN_MILLID = MILLLEDGERS.ACC_ID ", "  and CHALLANMASTER.CHALLAN_NO = " & DTROWPS("CHALLANNO") & "  AND CHALLANMASTER.CHALLAN_DONE = 0 AND CHALLANMASTER.CHALLAN_YEARID = " & YearId & " GROUP BY CHALLANMASTER.CHALLAN_NO, ISNULL(QUALITYMASTER.QUALITY_NAME, ''), ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0), ISNULL(CHALLANMASTER_DESC.CHALLAN_NARR, ''), ISNULL(CHALLANMASTER.CHALLAN_ORDERTYPE, 'SO'), ISNULL(CHALLANMASTER.CHALLAN_GRDONO, ''), ISNULL(CHALLANMASTER.CHALLAN_GRPCS, 0), ISNULL(CHALLANMASTER.CHALLAN_GRMTRS, 0), ISNULL(CHALLANMASTER.CHALLAN_SHORTAGE, 0), ISNULL(HSNMASTER.HSN_CODE, ''), ISNULL(HSNMASTER.HSN_CGST, 0), ISNULL(HSNMASTER.HSN_SGST, 0), ISNULL(HSNMASTER.HSN_IGST, 0), ISNULL(CHALLANMASTER_DESC.CHALLAN_LRNO,''), ISNULL(CHALLANMASTER_DESC.CHALLAN_BALENO,0),ISNULL(CHALLANMASTER_DESC.CHALLAN_LRDATE, GETDATE()), ISNULL(MILLLEDGERS.ACC_CMPNAME,''), ISNULL(CHALLANMASTER.CHALLAN_FOLD,''), ISNULL(CHALLANMASTER.CHALLAN_VEHICLENO,''), ISNULL(CHALLANMASTER.CHALLAN_DISPLAYQUALITYNAME,'') ")
                        End If
                        If DT1.Rows.Count > 0 Then
                            For Each dr As DataRow In DT1.Rows
                                Dim PER As String = "Mtrs"
                                Dim RATE As Double = 0
                                Dim ORDERDT As New DataTable
                                If dr("ORDERTYPE") = "OPENING" Then
                                    ORDERDT = OBJCMN.search(" ISNULL(OPENINGSALEORDER_DESC.OPSO_RATE, 0) AS RATE, ISNULL(OPENINGSALEORDER_DESC.OPSO_PER, 'Mtrs') AS PER, ISNULL(OPENINGSALEORDER.OPSO_DISC,0) AS DISC, ISNULL(OPENINGSALEORDER.OPSO_CRDAYS,0) AS CRDAYS, ISNULL(OPSO_PONO,'') AS PARTYPONO ", "", " CHALLANMASTER INNER JOIN OPENINGSALEORDER_DESC ON CHALLANMASTER.CHALLAN_ORDERNO = OPENINGSALEORDER_DESC.OPSO_NO AND CHALLANMASTER.CHALLAN_YEARID = OPENINGSALEORDER_DESC.OPSO_YEARID AND CHALLANMASTER.CHALLAN_ORDERSRNO = OPENINGSALEORDER_DESC.OPSO_GRIDSRNO INNER JOIN OPENINGSALEORDER ON OPENINGSALEORDER_DESC.OPSO_YEARID = OPENINGSALEORDER.OPSO_YEARID AND OPENINGSALEORDER_DESC.OPSO_NO = OPENINGSALEORDER.OPSO_NO ", " and CHALLANMASTER.CHALLAN_NO = " & dr("CHALLANNO") & "  AND CHALLANMASTER.CHALLAN_DONE = 0 AND CHALLANMASTER.CHALLAN_YEARID = " & YearId)
                                Else
                                    ORDERDT = OBJCMN.search(" ISNULL(SALEORDER_DESC.SO_RATE, 0) AS RATE, ISNULL(SALEORDER_DESC.SO_PER, 'Mtrs') AS PER, ISNULL(SALEORDER.SO_DISC,0) AS DISC, ISNULL(SALEORDER.SO_CRDAYS,0) AS CRDAYS, ISNULL(SO_PONO,'') AS PARTYPONO ", "", " CHALLANMASTER INNER JOIN SALEORDER_DESC ON CHALLANMASTER.CHALLAN_ORDERNO = SALEORDER_DESC.SO_NO AND CHALLANMASTER.CHALLAN_YEARID = SALEORDER_DESC.SO_YEARID AND CHALLANMASTER.CHALLAN_ORDERSRNO = SALEORDER_DESC.SO_GRIDSRNO INNER JOIN SALEORDER ON SALEORDER_DESC.SO_YEARID = SALEORDER.SO_YEARID AND SALEORDER_DESC.SO_NO = SALEORDER.SO_NO  ", " and CHALLANMASTER.CHALLAN_NO = " & dr("CHALLANNO") & "  AND CHALLANMASTER.CHALLAN_DONE = 0 AND CHALLANMASTER.CHALLAN_YEARID = " & YearId)
                                End If
                                If ORDERDT.Rows.Count > 0 Then
                                    PER = ORDERDT.Rows(0).Item("PER")
                                    RATE = Format(Val(ORDERDT.Rows(0).Item("RATE")), "0.0000")

                                    If Val(ORDERDT.Rows(0).Item("CRDAYS")) > 0 Then
                                        TXTCRDAYS.Text = Val(ORDERDT.Rows(0).Item("CRDAYS"))
                                        TXTCRDAYS_Validated(sender, e)
                                    End If
                                    If Val(ORDERDT.Rows(0).Item("DISC")) > 0 Then TXTORDERDISC.Text = Val(ORDERDT.Rows(0).Item("DISC"))
                                    TXTPARTYPONO.Text = ORDERDT.Rows(0).Item("PARTYPONO")
                                End If

                                TXTGRNO.Text = dr("GRNO")
                                TXTGRPCS.Text = Val(dr("GRPCS"))
                                TXTGRMTRS.Text = Val(dr("GRMTRS"))
                                TXTGRSHORTAGE.Text = Val(dr("SHORTAGE"))
                                TXTFOLD.Text = dr("FOLD")
                                TXTVEHICLENO.Text = dr("VEHICLENO")
                                TXTDISPLAYQUALITY.Text = dr("DISPLAYQUALITYNAME")

                                If dr("QUALITY").ToString <> "" And Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/07/2017" Then
                                    If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                                        GRIDINVOICE.Rows.Add(0, dr("QUALITY"), dr("HSNCODE"), Val(dr("LOTNO")), Val(dr("BALENO")), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), dr("MILLNAME"), Format(Val(dr("PCS")) - Val(dr("GRPCS")), "0"), Format(Val(dr("MTRS")) - Val(dr("GRMTRS")) - Val(dr("SHORTAGE")), "0.00"), Val(RATE), PER, 0.0, dr("GRIDNARR"), "0.00", "0.00", dr("CGSTPER"), "0.00", dr("SGSTPER"), "0.00", "0.00", "0.00", "0.00", dr("CHALLANNO"), dr("CHALLANSRNO"), "CHALLAN", 0)
                                        TXTCGSTPER1.Text = Val(dr("CGSTPER"))
                                        TXTSGSTPER1.Text = Val(dr("SGSTPER"))
                                        TXTIGSTPER1.Text = 0
                                    Else
                                        GRIDINVOICE.Rows.Add(0, dr("QUALITY"), dr("HSNCODE"), Val(dr("LOTNO")), Val(dr("BALENO")), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), dr("MILLNAME"), Format(Val(dr("PCS")) - Val(dr("GRPCS")), "0"), Format(Val(dr("MTRS")) - Val(dr("GRMTRS")) - Val(dr("SHORTAGE")), "0.00"), Val(RATE), PER, 0.0, dr("GRIDNARR"), "0.00", "0.00", "0.00 ", "0.00", "0.00", "0.00", dr("IGSTPER"), "0.00", "0.00", dr("CHALLANNO"), dr("CHALLANSRNO"), "CHALLAN", 0)
                                        TXTCGSTPER1.Text = 0
                                        TXTSGSTPER1.Text = 0
                                        TXTIGSTPER1.Text = Val(dr("IGSTPER"))
                                    End If
                                Else
                                    GRIDINVOICE.Rows.Add(0, dr("QUALITY"), "", Val(dr("LOTNO")), Val(dr("BALENO")), "", Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "dd/MM/yyyy"), "", Format(Val(dr("PCS")), "0"), Format(Val(dr("MTRS")), "0.00"), Val(RATE), PER, 0.0, dr("GRIDNARR"), "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", dr("CHALLANNO"), dr("CHALLANSRNO"), "CHALLAN", 0)
                                End If

                            Next
                        End If
                    Else
                        DT1 = OBJCMN.search(" CHALLANFINISHMASTER.CHALLANFINISH_NO AS CHALLANNO, CHALLANFINISHMASTER_DESC.CHALLANFINISH_GRIDSRNO AS CHALLANSRNO, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS QUALITY, ISNULL(CHALLANFINISHMASTER_DESC.CHALLANFINISH_LOTNO,0) AS LOTNO, ISNULL(CHALLANFINISHMASTER_DESC.CHALLANFINISH_BALENO,'') AS BALENO, ISNULL(CHALLANFINISHMASTER_DESC.CHALLANFINISH_TAKA, 0) AS PCS, ISNULL(CHALLANFINISHMASTER_DESC.CHALLANFINISH_MTRS, 0) AS MTRS, ISNULL(CHALLANFINISHMASTER.CHALLANFINISH_ORDERTYPE, 'SO') AS ORDERTYPE, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER  ", "", "  CHALLANFINISHMASTER INNER JOIN CHALLANFINISHMASTER_DESC ON CHALLANFINISHMASTER.CHALLANFINISH_NO = CHALLANFINISHMASTER_DESC.CHALLANFINISH_NO AND CHALLANFINISHMASTER.CHALLANFINISH_YEARID = CHALLANFINISHMASTER_DESC.CHALLANFINISH_YEARID INNER JOIN GREYQUALITYMASTER ON CHALLANFINISHMASTER_DESC.CHALLANFINISH_QUALITYID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN HSNMASTER ON GREYQUALITYMASTER.GREY_HSNCODEID = HSNMASTER.HSN_ID ", "  and CHALLANFINISHMASTER.CHALLANFINISH_NO = " & DTROWPS("CHALLANNO") & " and CHALLANFINISHMASTER.CHALLANFINISH_DONE = 0 AND CHALLANFINISHMASTER.CHALLANFINISH_YEARID = " & YearId)
                        If DT1.Rows.Count > 0 Then
                            For Each dr As DataRow In DT1.Rows
                                Dim PER As String = "Mtrs"
                                Dim RATE As Double = 0
                                Dim ORDERDT As New DataTable
                                If dr("ORDERTYPE") = "OPENING" Then
                                    ORDERDT = OBJCMN.search(" ISNULL(OPENINGSALEORDER_DESC.OPSO_RATE, 0) AS RATE, ISNULL(OPENINGSALEORDER_DESC.OPSO_PER, 'Mtrs') AS PER ", "", " CHALLANFINISHMASTER INNER JOIN OPENINGSALEORDER_DESC ON CHALLANFINISHMASTER.CHALLANFINISH_ORDERNO = OPENINGSALEORDER_DESC.OPSO_NO AND CHALLANFINISHMASTER.CHALLANFINISH_YEARID = OPENINGSALEORDER_DESC.OPSO_YEARID AND CHALLANFINISHMASTER.CHALLANFINISH_ORDERSRNO = OPENINGSALEORDER_DESC.OPSO_GRIDSRNO ", " and CHALLANFINISHMASTER.CHALLANFINISH_NO = " & dr("CHALLANNO") & "  AND CHALLANFINISHMASTER.CHALLANFINISH_DONE = 0 AND CHALLANFINISHMASTER.CHALLANFINISH_YEARID = " & YearId)
                                Else
                                    ORDERDT = OBJCMN.search(" ISNULL(SALEORDER_DESC.SO_RATE, 0) AS RATE, ISNULL(SALEORDER_DESC.SO_PER, 'Mtrs') AS PER ", "", " CHALLANFINISHMASTER INNER JOIN SALEORDER_DESC ON CHALLANFINISHMASTER.CHALLANFINISH_ORDERNO = SALEORDER_DESC.SO_NO AND CHALLANFINISHMASTER.CHALLANFINISH_YEARID = SALEORDER_DESC.SO_YEARID AND CHALLANFINISHMASTER.CHALLANFINISH_ORDERSRNO = SALEORDER_DESC.SO_GRIDSRNO ", " and CHALLANFINISHMASTER.CHALLANFINISH_NO = " & dr("CHALLANNO") & "  AND CHALLANFINISHMASTER.CHALLANFINISH_DONE = 0 AND CHALLANFINISHMASTER.CHALLANFINISH_YEARID = " & YearId)
                                End If
                                If ORDERDT.Rows.Count > 0 Then
                                    PER = ORDERDT.Rows(0).Item("PER")
                                    RATE = Format(Val(ORDERDT.Rows(0).Item("RATE")), "0.0000")
                                End If
                                If dr("QUALITY").ToString <> "" And Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/07/2017" Then
                                    If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                                        GRIDINVOICE.Rows.Add(0, dr("QUALITY"), dr("HSNCODE"), Val(dr("LOTNO")), dr("BALENO"), "", Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "dd/MM/yyyy"), "", Format(Val(dr("PCS")), "0"), Format(Val(dr("MTRS")), "0.00"), Val(RATE), PER, 0.0, "", "0.00", "0.00", dr("CGSTPER"), "0.00", dr("SGSTPER"), "0.00", "0.00", "0.00", "0.00", dr("CHALLANNO"), dr("CHALLANSRNO"), "CHALLANFINISH", 0)
                                        TXTCGSTPER1.Text = Val(dr("CGSTPER"))
                                        TXTSGSTPER1.Text = Val(dr("SGSTPER"))
                                        TXTIGSTPER1.Text = 0
                                    Else
                                        GRIDINVOICE.Rows.Add(0, dr("QUALITY"), dr("HSNCODE"), Val(dr("LOTNO")), dr("BALENO"), "", Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "dd/MM/yyyy"), "", Format(Val(dr("PCS")), "0"), Format(Val(dr("MTRS")), "0.00"), Val(RATE), PER, 0.0, "", "0.00", "0.00", "0.00 ", "0.00", "0.00", "0.00", dr("IGSTPER"), "0.00", "0.00", dr("CHALLANNO"), dr("CHALLANSRNO"), "CHALLANFINISH", 0)
                                        TXTCGSTPER1.Text = 0
                                        TXTSGSTPER1.Text = 0
                                        TXTIGSTPER1.Text = Val(dr("IGSTPER"))
                                    End If
                                Else
                                    GRIDINVOICE.Rows.Add(0, dr("QUALITY"), "", Val(dr("LOTNO")), dr("BALENO"), "", Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "dd/MM/yyyy"), "", Format(Val(dr("PCS")), "0"), Format(Val(dr("MTRS")), "0.00"), Val(RATE), PER, 0.0, "", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", dr("CHALLANNO"), dr("CHALLANSRNO"), "CHALLANFINISH", 0)
                                End If
                            Next
                        End If
                    End If
                Next

                CMDSELECTGDN.Enabled = False
                If GRIDINVOICE.RowCount > 0 Then
                    GRIDINVOICE.Focus()
                    GRIDINVOICE.CurrentCell = GRIDINVOICE.Rows(0).Cells(GRATE.Index)
                End If
                getsrno(GRIDINVOICE)
                TOTAL()

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREMOVE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDREMOVE.Click
        Try
            PBSOFTCOPY.Image = Nothing
            TXTIMGPATH.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, edit, "AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'AND LEDGERS.ACC_TYPE= 'TRANSPORT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEWB_Click(sender As Object, e As EventArgs) Handles TOOLEWB.Click
        Try
            If edit = False Then Exit Sub
            If ALLOWEINVOICE = True And TXTIRNNO.Text.Trim = "" Then
                If MsgBox("IRN not generated, First Generate IRN, Wish to Proceed Without IRN?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            End If

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
            If edit = False Then Exit Sub

            If Val(LBLTOTALCGSTAMT.Text.Trim) = 0 And Val(LBLTOTALSGSTAMT.Text.Trim) = 0 And Val(LBLTOTALIGSTAMT.Text.Trim) = 0 Then Exit Sub

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
            Dim DISPATCHFROM As String = ""
            Dim DISPATCHFROMGSTIN As String = ""
            Dim DISPATCHFROMPINCODE As String = ""
            Dim DISPATCHFROMSTATECODE As String = ""
            Dim DISPATCHFROMSTATENAME As String = ""
            Dim DISPATCHFROMKMS As Double = 0
            Dim DISPATCHFROMADD1 As String = ""
            Dim DISPATCHFROMADD2 As String = ""


            Dim OBJCMN As New ClsCommon
            'CMP ADDRESS DETAILS
            Dim DT As DataTable = OBJCMN.search(" ISNULL(CMP_DISPATCHFROM, '') AS ADD1, ISNULL(CMP_ADD2,'') AS ADD2 ", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
            TEMPCMPADD1 = DT.Rows(0).Item("ADD1")
            TEMPCMPADD2 = DT.Rows(0).Item("ADD2")
            DISPATCHFROM = CmpName
            DISPATCHFROMGSTIN = CMPGSTIN
            DISPATCHFROMPINCODE = CMPPINCODE


            'PARTY GST DETAILS 
            DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN, ISNULL(ACC_ZIPCODE,'') AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_YEARID = " & YearId)
            If DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Or Val(DT.Rows(0).Item("KMS")) = 0 Then
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
                'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                PARTYADD1 = DT.Rows(0).Item("ADD1")
                PARTYADD2 = DT.Rows(0).Item("ADD2")
            End If


            'FETCH PINCODE / KMS / ADD1 / ADD2 OF SHIPTO IF IT IS NOT SAME AS CMBNAME
            If TXTDELIVERYAT.Text.Trim <> "" AndAlso cmbname.Text.Trim <> TXTDELIVERYAT.Text.Trim Then
                DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN, ISNULL(ACC_ZIPCODE,'') AS PINCODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_RANGE,'') AS KOTHARIPLACE ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & TXTDELIVERYAT.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("PINCODE") = "" Or Val(DT.Rows(0).Item("KMS")) = 0 Then
                    MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                    PARTYPINCODE = DT.Rows(0).Item("PINCODE")
                    'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                    PARTYADD1 = DT.Rows(0).Item("ADD1")
                    PARTYADD2 = DT.Rows(0).Item("ADD2")
                    SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                    SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                End If
            End If


            'DISPATCHFROM GST DETAILS AND KMS WILL BE FETCHED FROM TXTKMS
            If CMBDISPATCHFROM.Text.Trim <> "" AndAlso cmbname.Text.Trim <> CMBDISPATCHFROM.Text.Trim Then
                DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN, ISNULL(ACC_ZIPCODE,'') AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBDISPATCHFROM.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Then
                    MsgBox(" Dispatch From Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    DISPATCHFROMGSTIN = DT.Rows(0).Item("GSTIN")
                    DISPATCHFROMSTATENAME = DT.Rows(0).Item("STATENAME")
                    DISPATCHFROMSTATECODE = DT.Rows(0).Item("STATECODE")
                    DISPATCHFROMPINCODE = DT.Rows(0).Item("PINCODE")
                    'DISPATCHFROMKMS = 0
                    DISPATCHFROMADD1 = DT.Rows(0).Item("ADD1")
                    DISPATCHFROMADD2 = DT.Rows(0).Item("ADD2")
                End If
            End If


            'TRANSPORT GSTING IS NOT MANDATORY
            'FOR LOCAL TRANSPORT THERE IS NO GSTIN
            'TRANSPORT GSTIN IF TRANSPORT IS PRESENT
            If cmbtrans.Text.Trim <> "" Then
                DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then TRANSGSTIN = DT.Rows(0).Item("GSTIN")
                'If TRANSGSTIN = "" Then
                '    MsgBox("Enter Transport GSTIN", MsgBoxStyle.Critical)
                '    Exit Sub
                'End If
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
            DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


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
                j = j & """supplyType"":""O"","
                j = j & """subSupplyType"":""1"","
                j = j & """subSupplyDesc"":"""","
                j = j & """docType"":""INV"","

                'WE NEED TO FETCH INITIALS INSTEAD OF BILLNO
                Dim DTINI As DataTable = OBJCMN.search("INVOICE_PRINTINITIALS AS INVINITIALS", "", "INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID", " AND INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId)
                j = j & """docNo"":""" & DTINI.Rows(0).Item("INVINITIALS") & """" & ","

                j = j & """docDate"":""" & INVOICEDATE.Text & """" & ","
                j = j & """fromGstin"":""" & CMPGSTIN & """" & ","
                j = j & """fromTrdName"":""" & CmpName & """" & ","
                j = j & """fromAddr1"":""" & TEMPCMPADD1 & """" & ","
                j = j & """fromAddr2"":""" & TEMPCMPADD2 & """" & ","
                j = j & """fromPlace"":""" & CMBFROMCITY.Text.Trim & """" & ","
                j = j & """fromPincode"":""" & DISPATCHFROMPINCODE & """" & ","
                j = j & """actFromStateCode"":""" & CMPSTATECODE & """" & ","
                j = j & """fromStateCode"":""" & CMPSTATECODE & """" & ","
                j = j & """toGstin"":""" & PARTYGSTIN & """" & ","
                j = j & """toTrdName"":""" & cmbname.Text.Trim & """" & ","
                j = j & """toAddr1"":""" & PARTYADD1 & """" & ","
                j = j & """toAddr2"":""" & PARTYADD2 & """" & ","
                j = j & """toPlace"":""" & TXTDELIVERYAT.Text.Trim & "-" & CMBTOCITY.Text.Trim & """" & ","
                j = j & """toPincode"":""" & PARTYPINCODE & """" & ","
                j = j & """actToStateCode"":""" & SHIPTOSTATECODE & """" & ","
                j = j & """toStateCode"":""" & PARTYSTATECODE & """" & ","

                j = j & """transactionType"":""4"","
                j = j & """dispatchFromGSTIN"":""" & DISPATCHFROMGSTIN & """" & ","
                j = j & """dispatchFromTradeName"":""" & DISPATCHFROM & """" & ","
                j = j & """shipToGSTIN"":""" & SHIPTOGSTIN & """" & ","
                j = j & """shipToTradeName"":""" & cmbname.Text.Trim & """" & ","
                j = j & """otherValue"":""0"","


                If INVOICESCREENTYPE = "TOTAL GST" Then
                    j = j & """totalValue"":""" & Val(TXTSUBTOTAL.Text.Trim) & """" & ","
                    j = j & """cgstValue"":""" & Val(TXTCGSTAMT1.Text.Trim) & """" & ","
                    j = j & """sgstValue"":""" & Val(TXTSGSTAMT1.Text.Trim) & """" & ","
                    j = j & """igstValue"":""" & Val(TXTIGSTAMT1.Text.Trim) & """" & ","
                Else
                    j = j & """totalValue"":""" & Val(LBLTOTALTAXABLEAMT.Text.Trim) & """" & ","
                    j = j & """cgstValue"":""" & Val(LBLTOTALCGSTAMT.Text.Trim) & """" & ","
                    j = j & """sgstValue"":""" & Val(LBLTOTALSGSTAMT.Text.Trim) & """" & ","
                    j = j & """igstValue"":""" & Val(LBLTOTALIGSTAMT.Text.Trim) & """" & ","
                End If

                j = j & """cessValue"":""" & "0" & """" & ","
                j = j & """cessNonAdvolValue"":""" & "0" & """" & ","
                j = j & """totInvValue"":""" & Val(txtgrandtotal.Text.Trim) & """" & ","
                j = j & """transporterId"":""" & TRANSGSTIN & """" & ","
                j = j & """transporterName"":""" & cmbtrans.Text.Trim & """" & ","



                'THIS CODE IS WRITTEN COZ WHEN BILLTO AND SHIPTO ARE IN THE SAME PINCODE THEN WE HAVE TO PASS MINIMUM 10 KMS
                'OR ELSE IT WILL GIVE ERROR
                If DISPATCHFROMPINCODE = PARTYPINCODE Then PARTYKMS = 10

                If TXTVEHICLENO.Text.Trim = "" Then
                    j = j & """transDocNo"":"""","
                    j = j & """transMode"":"""","
                    j = j & """transDistance"":""" & PARTYKMS & """" & ","
                    j = j & """transDocDate"":"""","
                    j = j & """vehicleNo"":"""","
                    j = j & """vehicleType"":"""","
                Else
                    j = j & """transDocNo"":""" & TXTLRNO.Text.Trim & """" & ","
                    j = j & """transMode"":""" & "1" & """" & ","
                    j = j & """transDistance"":""" & PARTYKMS & """" & ","
                    j = j & """transDocDate"":"""","
                    j = j & """vehicleNo"":""" & TXTVEHICLENO.Text.Trim & """" & ","
                    j = j & """vehicleType"":""" & "R" & """" & ","
                End If


                j = j & """itemList"":[{"


                'WE NEED TO FETCH SUMMARY OF ITEMS AND HSN TO PASS HERE
                'FETCH FROM DESC TABLE 
                If CMBTYPE.Text = "GREY" Then DT = OBJCMN.Execute_Any_String(" SELECT GREY_NAME AS ITEMNAME, ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGST, ISNULL(HSN_SGST,0) AS SGST, ISNULL(HSN_IGST,0) AS IGST, SUM(INVOICE_MTRS) AS MTRS, (CASE WHEN ISNULL(INVOICE_SCREENTYPE,'TOTAL GST') = 'TOTAL GST' THEN SUM(INVOICEMASTER_DESC.INVOICE_AMOUNT) ELSE SUM(INVOICE_TAXABLEAMT) END)  AS TAXABLEAMT FROM INVOICEMASTER INNER JOIN INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN GREYQUALITYMASTER ON GREY_ID = INVOICE_QUALITYID INNER JOIN HSNMASTER ON HSN_ID = INVOICE_HSNCODEID INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTER_ID WHERE INVOICEMASTER.INVOICE_NO = " & Val(TEMPINVOICENO) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' and INVOICEMASTER.INVOICE_YEARID = " & YearId & " GROUP BY GREY_name, ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), INVOICE_SCREENTYPE", "", "") Else DT = OBJCMN.Execute_Any_String(" SELECT QUALITY_NAME AS ITEMNAME, ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGST, ISNULL(HSN_SGST,0) AS SGST, ISNULL(HSN_IGST,0) AS IGST, SUM(INVOICE_MTRS) AS MTRS, (CASE WHEN ISNULL(INVOICE_SCREENTYPE,'TOTAL GST') = 'TOTAL GST' THEN SUM(INVOICEMASTER_DESC.INVOICE_AMOUNT) ELSE SUM(INVOICE_TAXABLEAMT) END)  AS TAXABLEAMT FROM INVOICEMASTER INNER JOIN INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN QUALITYMASTER ON QUALITY_id = INVOICE_QUALITYID INNER JOIN HSNMASTER ON HSN_ID = INVOICE_HSNCODEID INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTER_ID WHERE INVOICEMASTER.INVOICE_NO = " & Val(TEMPINVOICENO) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' and INVOICEMASTER.INVOICE_YEARID = " & YearId & " GROUP BY QUALITY_NAME, ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0), INVOICE_SCREENTYPE", "", "")
                Dim CURRROW As Integer = 0
                For Each DTROW As DataRow In DT.Rows
                    If CURRROW > 0 Then j = j & ",{"
                    j = j & """productName"":""" & DTROW("ITEMNAME") & """" & ","
                    j = j & """productDesc"":""" & DTROW("ITEMNAME") & """" & ","
                    j = j & """hsnCode"":""" & DTROW("HSNCODE") & """" & ","
                    j = j & """quantity"":""" & Val(DTROW("MTRS")) & """" & ","
                    j = j & """qtyUnit"":""" & "MTR" & """" & ","

                    If INVOICESCREENTYPE = "TOTAL GST" Then
                        j = j & """cgstRate"":""" & Val(TXTCGSTPER1.Text.Trim) & """" & ","
                        j = j & """sgstRate"":""" & Val(TXTSGSTPER1.Text.Trim) & """" & ","
                        j = j & """igstRate"":""" & Val(TXTIGSTPER1.Text.Trim) & """" & ","
                    Else
                        j = j & """cgstRate"":""" & Val(GRIDINVOICE.Item(GCGSTPER.Index, CURRROW).Value) & """" & ","
                        j = j & """sgstRate"":""" & Val(GRIDINVOICE.Item(GSGSTPER.Index, CURRROW).Value) & """" & ","
                        j = j & """igstRate"":""" & Val(GRIDINVOICE.Item(GIGSTPER.Index, CURRROW).Value) & """" & ","
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
                READER = New StreamReader(RESPONSE.GetResponseStream())
                REQUESTEDTEXT = READER.ReadToEnd()

            Catch ex As WebException
                RESPONSE = ex.Response
                MsgBox("Error While Generating EWB, " & REQUESTEDTEXT)
                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End Try



            Dim EWBNO As String = ""

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ewayBillNo") + Len("ewayBillNo") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS)
            EWBNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            TXTEWAYBILLNO.Text = EWBNO

            'WE NEED TO UPDATE THIS EWBNO IN DATABASE ALSO
            DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_EWAYBILLNO = '" & TXTEWAYBILLNO.Text.Trim & "' FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TEMPINVOICENO) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")

            'ADD DATA IN EWAYENTRY
            DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & EWBNO & "','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTEWB()
        Try

            If PRINTEWAYBILL = False Then Exit Sub
            If edit = False Then Exit Sub
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
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKENNO & "','" & EWBNO & "','PRINT SUCCESS1', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKENNO & "','" & EWBNO & "','PRINT SUCCESS2', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

            Catch ex As WebException
                RESPONSE = ex.Response
                MsgBox("Error While Printing EWB, Please check the Data Properly")
                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKENNO & "','" & EWBNO & "','PRINT FAILED1', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKENNO & "','" & EWBNO & "','PRINT FAILED2', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then namevalidate(cmbtrans, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'", "Sundry Creditors", "TRANSPORT")
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

                TXTCGSTAMT1.ReadOnly = False
                TXTCGSTAMT1.TabStop = True
                TXTCGSTAMT1.BackColor = Color.LemonChiffon
                TXTSGSTAMT1.ReadOnly = False
                TXTSGSTAMT1.TabStop = True
                TXTSGSTAMT1.BackColor = Color.LemonChiffon
                TXTIGSTAMT1.ReadOnly = False
                TXTIGSTAMT1.TabStop = True
                TXTIGSTAMT1.BackColor = Color.LemonChiffon
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

                TXTCGSTAMT1.ReadOnly = True
                TXTCGSTAMT1.TabStop = False
                TXTCGSTAMT1.BackColor = Color.Linen
                TXTSGSTAMT1.ReadOnly = True
                TXTSGSTAMT1.TabStop = False
                TXTSGSTAMT1.BackColor = Color.Linen
                TXTIGSTAMT1.ReadOnly = True
                TXTIGSTAMT1.TabStop = False
                TXTIGSTAMT1.BackColor = Color.Linen
                total()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCGSTAMT1_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTCGSTAMT1.Validated, TXTSGSTAMT1.Validated, TXTIGSTAMT1.Validated
        Try
            CALC()
            total()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKTCS_CheckedChanged(sender As Object, e As EventArgs) Handles CHKTCS.CheckedChanged
        TOTAL()
    End Sub

    Private Sub TOOLPRINTEWB_Click(sender As Object, e As EventArgs) Handles TOOLPRINTEWB.Click
        PRINTEWB()
    End Sub

    Private Sub TOOLEINV_Click(sender As Object, e As EventArgs) Handles TOOLEINV.Click
        Try
            If edit = False Then Exit Sub
            GENERATEINV()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub GENERATEINV()
        Try
            If ALLOWEINVOICE = False Then Exit Sub
            If cmbname.Text.Trim = "" Then Exit Sub

            If Val(LBLTOTALCGSTAMT.Text.Trim) = 0 And Val(TXTCGSTAMT1.Text.Trim) = 0 And Val(LBLTOTALSGSTAMT.Text.Trim) = 0 And Val(TXTSGSTAMT1.Text.Trim) = 0 And Val(LBLTOTALIGSTAMT.Text.Trim) = 0 And Val(TXTIGSTAMT1.Text.Trim) = 0 Then Exit Sub

            'THERE IS NO LRDAT COLUMN
            'If TXTLRNO.Text.Trim <> "" AndAlso LRDATE.Text <> "__/__/____" Then
            '    If Convert.ToDateTime(LRDATE.Text).Date < Convert.ToDateTime(INVOICEDATE.Text).Date Then
            '        MsgBox("LR Date cannot be Before Invoice Date", MsgBoxStyle.Critical)
            '        Exit Sub
            '    End If
            'End If

            If CMBFROMCITY.Text.Trim = "" Then
                MsgBox("Enter From City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If CMBTOCITY.Text.Trim = "" Then
                MsgBox("Enter to City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Generate E-Invoice?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If TXTIRNNO.Text.Trim <> "" Then
                MsgBox("E-Invoice Already Generated", MsgBoxStyle.Critical)
                Exit Sub
            End If

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
            Dim TEMPCMPDISPATCHADD1 As String = ""
            Dim PARTYGSTIN As String = ""
            Dim PARTYPINCODE As String = ""
            Dim PARTYSTATECODE As String = ""
            Dim PARTYSTATENAME As String = ""
            Dim SHIPTOGSTIN As String = ""
            Dim SHIPTOSTATECODE As String = ""
            Dim SHIPTOSTATENAME As String = ""
            Dim SHIPTOPINCODE As String = ""
            Dim PARTYKMS As Double = 0
            Dim PARTYADD1 As String = ""
            Dim PARTYADD2 As String = ""
            Dim SHIPTOADD1 As String = ""
            Dim SHIPTOADD2 As String = ""
            Dim TRANSGSTIN As String = ""
            Dim DISPATCHFROM As String = ""
            Dim DISPATCHFROMGSTIN As String = ""
            Dim DISPATCHFROMPINCODE As String = ""
            Dim DISPATCHFROMSTATECODE As String = ""
            Dim DISPATCHFROMSTATENAME As String = ""
            Dim DISPATCHFROMKMS As Double = 0
            Dim DISPATCHFROMADD1 As String = ""
            Dim DISPATCHFROMADD2 As String = ""


            Dim OBJCMN As New ClsCommon
            'CMP ADDRESS DETAILS
            Dim DT As DataTable = OBJCMN.search(" ISNULL(CMP_DISPATCHFROM, '') AS ADD1, ISNULL(CMP_ADD2,'') AS ADD2, ISNULL(CMP_DISPATCHFROM, '') AS DISPATCHADD ", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
            TEMPCMPADD1 = DT.Rows(0).Item("ADD1")
            TEMPCMPADD2 = DT.Rows(0).Item("ADD2")
            TEMPCMPDISPATCHADD1 = DT.Rows(0).Item("DISPATCHADD")
            DISPATCHFROM = CmpName
            DISPATCHFROMGSTIN = CMPGSTIN
            DISPATCHFROMPINCODE = CMPPINCODE
            DISPATCHFROMSTATECODE = CMPSTATECODE
            DISPATCHFROMSTATENAME = CMPSTATENAME


            'PARTY GST DETAILS 
            DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN, ISNULL(ACC_ZIPCODE,'') AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_YEARID = " & YearId)
            If DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Or Val(DT.Rows(0).Item("KMS")) = 0 Then
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
                SHIPTOPINCODE = DT.Rows(0).Item("PINCODE")
                'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                PARTYADD1 = DT.Rows(0).Item("ADD1")
                PARTYADD2 = DT.Rows(0).Item("ADD2")
            End If


            If TXTDELIVERYAT.Text.Trim <> "" AndAlso cmbname.Text.Trim <> TXTDELIVERYAT.Text.Trim Then
                DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN, ISNULL(ACC_ZIPCODE,'') AS PINCODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_RANGE,'') AS KOTHARIPLACE ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & TXTDELIVERYAT.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("PINCODE") = "" Or Val(DT.Rows(0).Item("KMS")) = 0 Then
                    MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                    SHIPTOPINCODE = DT.Rows(0).Item("PINCODE")
                    'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                    PARTYADD1 = DT.Rows(0).Item("ADD1")
                    PARTYADD2 = DT.Rows(0).Item("ADD2")
                    SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                    SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                End If
            End If



            'DISPATCHFROM GST DETAILS AND KMS WILL BE FETCHED FROM TXTKMS
            If CMBDISPATCHFROM.Text.Trim <> "" Then
                DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN, ISNULL(ACC_ZIPCODE,'') AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBDISPATCHFROM.Text.Trim & "' AND ACC_YEARID = " & YearId)
                ' If DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Then
                If DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Then
                    MsgBox(" Dispatch From Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    DISPATCHFROMGSTIN = DT.Rows(0).Item("GSTIN")
                    DISPATCHFROMSTATENAME = DT.Rows(0).Item("STATENAME")
                    DISPATCHFROMSTATECODE = DT.Rows(0).Item("STATECODE")
                    DISPATCHFROMPINCODE = DT.Rows(0).Item("PINCODE")
                    'DISPATCHFROMKMS = Val(TXTKMS.Text.Trim)
                    TEMPCMPDISPATCHADD1 = DT.Rows(0).Item("ADD1")
                    TEMPCMPADD2 = DT.Rows(0).Item("ADD2")
                End If
            End If




            'TRANSPORT GSTING IS NOT MANDATORY
            'FOR LOCAL TRANSPORT THERE IS NO GSTIN
            'TRANSPORT GSTIN IF TRANSPORT IS PRESENT
            If cmbtrans.Text.Trim <> "" Then
                DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then TRANSGSTIN = DT.Rows(0).Item("GSTIN")
                'FOR LOCAL TRANSPORT THERE IS NO GSTIN
                'If TRANSGSTIN = "" Then
                '    MsgBox("Enter Transport GSTIN", MsgBoxStyle.Critical)
                '    Exit Sub
                'End If
            End If



            'CHECKING COUNTER AND VALIDATE WHETHER EINVOICE WILL BE ALLOWED OR NOT, FOR EACH EINVOICE BILL WE NEED TO 2 API COUNTS (1 FOR TOKEN AND ANOTHER FOR EINVOICE)
            If CMPEINVOICECOUNTER = 0 Then
                MsgBox("E-Invoice Bill Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'GET USED EINVOICECOUNTER
            Dim USEDEINVOICECOUNTER As Integer = 0
            DT = OBJCMN.search("COUNT(COUNTERID) AS EINVOICECOUNT", "", "EINVOICEENTRY", " AND CMPID =" & CmpId)
            If DT.Rows.Count > 0 Then USEDEINVOICECOUNTER = Val(DT.Rows(0).Item("EINVOICECOUNT"))

            'IF COUNTERS ARE FINISJED
            If CMPEINVOICECOUNTER - USEDEINVOICECOUNTER <= 0 Then
                MsgBox("E-Invoice Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF DATE HAS EXPIRED
            If Now.Date > EINVOICEEXPDATE Then
                MsgBox("E-Invoice Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF BALANCECOUNTERS ARE .10 THEN INTIMATE
            If CMPEINVOICECOUNTER - USEDEINVOICECOUNTER < Format((CMPEINVOICECOUNTER * 0.1), "0") Then
                MsgBox("Only " & (CMPEINVOICECOUNTER - USEDEINVOICECOUNTER) & " API's Left, Kindly contact Nakoda Infotech for Renewal of E-Invoice Package", MsgBoxStyle.Critical)
            End If


            'FOR GENERATING EINVOICE BILL WE NEED TO FIRST GENERATE THE TOKEN
            'THIS IS FOR SANDBOX TEST
            'Dim URL As New Uri("http://gstsandbox.charteredinfo.com/eivital/dec/v1.04/auth?aspid=1602611918&password=infosys123&Gstin=34AACCC1596Q002&user_name=TaxProEnvPON&eInvPwd=abc34*")
            Dim URL As New Uri("https://einvapi.charteredinfo.com/eivital/dec/v1.04/auth?aspid=1602611918&password=infosys123&Gstin=" & CMPGSTIN & "&user_name=" & CMPEWBUSER & "&eInvPwd=" & CMPEWBPASS)

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

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 2
            TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
            If TEMPSTATUS = "1" Then TEMPSTATUS = "SUCCESS" Else TEMPSTATUS = "FAILED"




            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("authtoken") + Len("AUTHTOKEN") + 3
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS) - 1
            TOKEN = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            'ADD DATA IN EINVOICEENTRY
            'DONT ADD IN EINVOICEENTRY, DONE BY GULKIT, IF FAILED THEN ADD
            'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


            'ONCE WE REC THE TOKEN WE WILL CREATE EWAY BILL
            'IF STATUS IS FAILED THEN ERROR MESSAGE
            If TEMPSTATUS = "FAILED" Then
                MsgBox("Unable to create Eway Bill", MsgBoxStyle.Critical)
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "','" & REQUESTEDTEXT & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End If

            Dim j As String = ""
            Dim PRINTINITIALS As String = ""

            'GENERATING EINVOICE
            'FOR SANBOX TEST
            'Dim FURL As New Uri("http://gstsandbox.charteredinfo.com/eicore/dec/v1.03/Invoice?aspid=1602611918&password=infosys123&Gstin=34AACCC1596Q002&AuthToken=" & TOKEN & "&user_name=TaxProEnvPON&QrCodeSize=250")
            Dim FURL As New Uri("https://einvapi.charteredinfo.com/eicore/dec/v1.03/Invoice?aspid=1602611918&password=infosys123&Gstin=" & CMPGSTIN & "&AuthToken=" & TOKEN & "&user_name=" & CMPEWBUSER & "&QrCodeSize=250")
            REQUEST = WebRequest.CreateDefault(FURL)
            REQUEST.Method = "POST"
            Try
                REQUEST.ContentType = "application/json"



                j = "{"
                j = j & """Version"": ""1.1"","
                j = j & """TranDtls"": {"
                j = j & """TaxSch"":""GST"","
                j = j & """SupTyp"":""B2B"","
                j = j & """RegRev"":""N"","
                j = j & """IgstOnIntra"":""N""},"



                'WE NEED TO FETCH INITIALS INSTEAD OF BILLNO
                Dim DTINI As DataTable = OBJCMN.search("INVOICE_PRINTINITIALS AS PRINTINITIALS", "", "INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID", " AND INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId)
                PRINTINITIALS = DTINI.Rows(0).Item("PRINTINITIALS")

                j = j & """DocDtls"": {"
                j = j & """Typ"":""INV"","
                j = j & """No"":""" & DTINI.Rows(0).Item("PRINTINITIALS") & """" & ","
                j = j & """Dt"":""" & INVOICEDATE.Text & """" & "},"


                'For WORKING ON SANDBOX
                'CMPGSTIN = "34AACCC1596Q002"
                'CMPPINCODE = "605001"
                'CMPSTATECODE = "34"

                If TEMPCMPADD1 = "" Then TEMPCMPADD1 = CMBFROMCITY.Text.Trim
                If TEMPCMPADD2 = "" Then TEMPCMPADD2 = CMBFROMCITY.Text.Trim
                If TEMPCMPDISPATCHADD1 = "" Then TEMPCMPDISPATCHADD1 = TEMPCMPADD1

                j = j & """SellerDtls"": {"
                j = j & """Gstin"":""" & CMPGSTIN & """" & ","
                j = j & """LglNm"":""" & CmpName & """" & ","
                j = j & """TrdNm"":""" & CmpName & """" & ","
                j = j & """Addr1"":""" & TEMPCMPADD1.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & TEMPCMPADD2.Replace(vbCrLf, " ") & """" & ","
                j = j & """Loc"":""" & CMBFROMCITY.Text.Trim & """" & ","
                j = j & """Pin"":" & CMPPINCODE & "" & ","
                j = j & """Stcd"":""" & CMPSTATECODE & """" & "},"

                If PARTYADD1 = "" Then PARTYADD1 = PARTYSTATENAME
                If PARTYADD2 = "" Then PARTYADD2 = PARTYSTATENAME

                j = j & """BuyerDtls"": {"
                j = j & """Gstin"":""" & PARTYGSTIN & """" & ","
                j = j & """LglNm"":""" & cmbname.Text.Trim & """" & ","
                j = j & """TrdNm"":""" & cmbname.Text.Trim & """" & ","
                j = j & """Pos"":""" & PARTYSTATECODE & """" & ","
                j = j & """Addr1"":""" & PARTYADD1.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & PARTYADD2.Replace(vbCrLf, " ") & """" & ","
                j = j & """Loc"":""" & CMBTOCITY.Text.Trim & """" & ","
                j = j & """Pin"":" & PARTYPINCODE & "" & ","
                j = j & """Stcd"":""" & PARTYSTATECODE & """" & "},"




                j = j & """DispDtls"": {"
                j = j & """Nm"":""" & DISPATCHFROM & """" & ","
                j = j & """Addr1"":""" & TEMPCMPDISPATCHADD1.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & TEMPCMPADD2.Replace(vbCrLf, " ") & """" & ","
                j = j & """Loc"":""" & CMBFROMCITY.Text.Trim & """" & ","
                j = j & """Pin"":" & DISPATCHFROMPINCODE & "" & ","
                j = j & """Stcd"":""" & DISPATCHFROMSTATECODE & """" & "},"


                j = j & """ShipDtls"": {"
                If SHIPTOGSTIN <> "" Then j = j & """Gstin"":""" & SHIPTOGSTIN & """" & ","
                j = j & """LglNm"":""" & TXTDELIVERYAT.Text.Trim & """" & ","
                j = j & """TrdNm"":""" & TXTDELIVERYAT.Text.Trim & """" & ","
                If SHIPTOADD1 = "" Then j = j & """Addr1"":""" & PARTYADD1.Replace(vbCrLf, " ") & """" & "," Else j = j & """Addr1"":""" & SHIPTOADD1.Replace(vbCrLf, " ") & """" & ","
                If SHIPTOADD2 = "" Then SHIPTOADD2 = " ADDRESS2 "
                j = j & """Addr2"":""" & SHIPTOADD2 & """" & ","
                j = j & """Loc"":""" & CMBTOCITY.Text.Trim & """" & ","
                If SHIPTOPINCODE = "" Then j = j & """Pin"":" & PARTYPINCODE & "" & "," Else j = j & """Pin"":" & SHIPTOPINCODE & "" & ","
                j = j & """Stcd"":""" & SHIPTOSTATECODE & """" & "},"


                j = j & """ItemList"":[{"



                Dim TEMPLINEDISC As Double = 0
                Dim TEMPLINETAXABLEAMT As Double = 0
                Dim TEMPLINECGSTAMT As Double = 0
                Dim TEMPLINESGSTAMT As Double = 0
                Dim TEMPLINEIGSTAMT As Double = 0
                Dim TEMPLINEGRIDTOTALAMT As Double = 0
                Dim TEMPLINECHARGES As Double = 0
                Dim TEMPRATE As Double = 0
                If Val(TXTCHARGES.Text.Trim) < 0 Then
                    TEMPLINEDISC = Format(Val(TXTCHARGES.Text.Trim) / Val(LBLTOTALMTRS.Text.Trim), "0.00000")
                Else
                    TEMPRATE = Format(Val(TXTCHARGES.Text.Trim) / Val(LBLTOTALMTRS.Text.Trim), "0.00000")
                End If


                Dim TEMPTOTALAMT As Double = 0
                Dim TEMPTOTALDISC As Double = 0
                Dim TEMPTOTALTAXABLEAMT As Double = 0
                Dim TEMPTOTALCGSTAMT As Double = 0
                Dim TEMPTOTALSGSTAMT As Double = 0
                Dim TEMPTOTALIGSTAMT As Double = 0
                Dim TEMPGTOTALAMT As Double = 0


                'WE NEED TO FETCH ALL ITEMS HERE
                'FETCH FROM DESC TABLE 
                DT = OBJCMN.Execute_Any_String(" SELECT ISNULL(INVOICEMASTER_DESC.INVOICE_SRNO,0) AS SRNO, (CASE WHEN ISNULL(INVOICEMASTER.INVOICE_TYPE, 'GREY') = 'GREY' THEN ISNULL(GREYQUALITYMASTER.GREY_NAME, '') ELSE ISNULL(QUALITYMASTER.QUALITY_NAME, '') END) AS ITEMNAME, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(INVOICEMASTER_DESC.INVOICE_PCS,0) AS PCS, ISNULL(INVOICEMASTER_DESC.INVOICE_MTRS,0) AS MTRS, CASE WHEN ISNULL(INVOICEMASTER.INVOICE_TYPE, 'GREY') = 'GREY' THEN 'MTRS' ELSE 'KGS' END AS PER, ISNULL(INVOICEMASTER_DESC.INVOICE_RATE,0) AS RATE, ISNULL(INVOICEMASTER_DESC.INVOICE_AMOUNT,0) AS TOTALAMT, 0 AS LINEDISC, 0 AS LINETAXABLEAMT, 0 AS LINECGSTAMT, 0 AS LINESGSTAMT, 0 AS LINEIGSTAMT, 0 AS LINEGRIDDTOTAL, ISNULL(HSN_TYPE,'Goods') HSNTYPE FROM INVOICEMASTER INNER JOIN INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID LEFT OUTER JOIN QUALITYMASTER ON INVOICEMASTER_DESC.INVOICE_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN GREYQUALITYMASTER ON INVOICEMASTER_DESC.INVOICE_QUALITYID = GREYQUALITYMASTER.GREY_ID INNER JOIN HSNMASTER ON INVOICEMASTER_DESC.INVOICE_HSNCODEID = HSNMASTER.HSN_ID INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTER_ID WHERE INVOICEMASTER.INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' and INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER_DESC.INVOICE_SRNO", "", "")
                Dim CURRROW As Integer = 0
                For Each DTROW As DataRow In DT.Rows

                    TEMPLINECHARGES = Format(Val(TEMPLINEDISC) * Val(DTROW("MTRS")), "0.00")
                    TEMPLINETAXABLEAMT = Format(Val(DTROW("TOTALAMT")) + Val(TEMPLINECHARGES), "0.00")
                    TEMPLINECGSTAMT = Format(Val(TXTCGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    TEMPLINESGSTAMT = Format(Val(TXTSGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    TEMPLINEIGSTAMT = Format(Val(TXTIGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    TEMPLINEGRIDTOTALAMT = Format(Val(TEMPLINETAXABLEAMT + TEMPLINECGSTAMT + TEMPLINESGSTAMT + TEMPLINEIGSTAMT), "0.00")

                    If CURRROW > 0 Then j = j & ", {"
                    j = j & """SlNo"": """ & DTROW("SRNO") & """" & ","
                    j = j & """PrdDesc"":""" & DTROW("ITEMNAME") & """" & ","
                    If DTROW("HSNTYPE") = "Goods" Then j = j & """IsServc"":""" & "N" & """" & "," Else j = j & """IsServc"":""" & "Y" & """" & ","
                    j = j & """HsnCd"":""" & DTROW("HSNCODE") & """" & ","
                    j = j & """Barcde"":""REC9999"","
                    If DTROW("PER") = "Pcs" Then j = j & """Qty"":" & Val(DTROW("PCS")) & "" & "," Else j = j & """Qty"":" & Val(DTROW("MTRS")) & "" & ","
                    j = j & """FreeQty"":" & "0" & "" & ","
                    j = j & """Unit"":""" & "MTR" & """" & ","


                    If Val(TXTCHARGES.Text.Trim) <= 0 Then

                        j = j & """UnitPrice"":" & Val(DTROW("RATE")) & "" & ","
                        j = j & """TotAmt"":" & Format(Val(DTROW("TOTALAMT")), "0.00") & "" & ","

                        If INVOICESCREENTYPE = "LINE GST" Then
                            If Val(DTROW("LINEDISC")) < 0 Then j = j & """Discount"":" & Val(DTROW("LINEDISC")) * -1 & "" & "," Else j = j & """Discount"":" & Val(DTROW("LINEDISC")) & "" & ","
                        Else
                            If Val(TEMPLINECHARGES) < 0 Then j = j & """Discount"":" & Val(TEMPLINECHARGES) * -1 & "" & "," Else j = j & """Discount"":" & Val(TEMPLINECHARGES) & "" & ","
                        End If
                        j = j & """PreTaxVal"":" & "1" & "" & ","
                        If INVOICESCREENTYPE = "LINE GST" Then j = j & """AssAmt"":" & Val(DTROW("LINETAXABLEAMT")) & "" & "," Else j = j & """AssAmt"":" & Val(TEMPLINETAXABLEAMT) & "" & ","
                        j = j & """GstRt"":" & Val(DTROW("IGSTPER")) & "" & ","

                        If INVOICESCREENTYPE = "LINE GST" Then
                            j = j & """IgstAmt"":" & Val(DTROW("LINEIGSTAMT")) & "" & ","
                            j = j & """CgstAmt"":" & Val(DTROW("LINECGSTAMT")) & "" & ","
                            j = j & """SgstAmt"":" & Val(DTROW("LINESGSTAMT")) & "" & ","
                        Else
                            j = j & """IgstAmt"":" & Val(TEMPLINEIGSTAMT) & "" & ","
                            j = j & """CgstAmt"":" & Val(TEMPLINECGSTAMT) & "" & ","
                            j = j & """SgstAmt"":" & Val(TEMPLINESGSTAMT) & "" & ","
                        End If

                        j = j & """CesRt"":" & "0" & "" & ","
                        j = j & """CesAmt"":" & "0" & "" & ","
                        j = j & """CesNonAdvlAmt"":" & "0" & "" & ","
                        j = j & """StateCesRt"":" & "0" & "" & ","
                        j = j & """StateCesAmt"":" & "0" & "" & ","
                        j = j & """StateCesNonAdvlAmt"":" & "0" & "" & ","
                        j = j & """OthChrg"":" & "0" & "" & ","

                        If INVOICESCREENTYPE = "LINE GST" Then j = j & """TotItemVal"":" & Val(DTROW("LINEGRIDTOTAL")) & "" & "," Else j = j & """TotItemVal"":" & Val(TEMPLINEGRIDTOTALAMT) & "" & ","
                        j = j & """OrdLineRef"":"" "","
                        j = j & """OrgCntry"":""IN"","
                        j = j & """PrdSlNo"":""123"","

                        j = j & """BchDtls"": {"
                        j = j & """Nm"":""123"","
                        j = j & """Expdt"":""" & INVOICEDATE.Text & """" & ","
                        j = j & """wrDt"":""" & INVOICEDATE.Text & """" & "},"

                        j = j & """AttribDtls"": [{"
                        j = j & """Nm"":""" & DTROW("ITEMNAME") & """" & ","
                        j = j & """Val"":""" & Val(TEMPLINEGRIDTOTALAMT) & """" & "}]"

                    Else

                        j = j & """UnitPrice"":" & Format(Val(DTROW("RATE")) + TEMPRATE, "0.00") & "" & ","
                        If DTROW("PER") = "Pcs" Then TEMPLINETAXABLEAMT = Format(Val(Val(DTROW("RATE")) + TEMPRATE) * Val(DTROW("PCS")), "0.00") Else TEMPLINETAXABLEAMT = Format(Val(Val(DTROW("RATE")) + TEMPRATE) * Val(DTROW("MTRS")), "0.00")

                        TEMPLINECGSTAMT = Format(Val(TXTCGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                        TEMPLINESGSTAMT = Format(Val(TXTSGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                        TEMPLINEIGSTAMT = Format(Val(TXTIGSTPER1.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                        TEMPLINEGRIDTOTALAMT = Format(Val(TEMPLINETAXABLEAMT + TEMPLINECGSTAMT + TEMPLINESGSTAMT + TEMPLINEIGSTAMT), "0.00")

                        j = j & """TotAmt"":" & Val(TEMPLINETAXABLEAMT) & "" & ","
                        j = j & """Discount"":" & "0" & "" & ","
                        j = j & """PreTaxVal"":" & "1" & "" & ","
                        j = j & """AssAmt"":" & Val(TEMPLINETAXABLEAMT) & "" & ","
                        j = j & """GstRt"":" & Val(DTROW("IGSTPER")) & "" & ","
                        j = j & """IgstAmt"":" & Val(TEMPLINEIGSTAMT) & "" & ","
                        j = j & """CgstAmt"":" & Val(TEMPLINECGSTAMT) & "" & ","
                        j = j & """SgstAmt"":" & Val(TEMPLINESGSTAMT) & "" & ","
                        j = j & """CesRt"":" & "0" & "" & ","
                        j = j & """CesAmt"":" & "0" & "" & ","
                        j = j & """CesNonAdvlAmt"":" & "0" & "" & ","
                        j = j & """StateCesRt"":" & "0" & "" & ","
                        j = j & """StateCesAmt"":" & "0" & "" & ","
                        j = j & """StateCesNonAdvlAmt"":" & "0" & "" & ","
                        j = j & """OthChrg"":" & "0" & "" & ","
                        j = j & """TotItemVal"":" & Val(TEMPLINEGRIDTOTALAMT) & "" & ","
                        j = j & """OrdLineRef"":"" "","
                        j = j & """OrgCntry"":""IN"","
                        j = j & """PrdSlNo"":""123"","

                        j = j & """BchDtls"": {"
                        j = j & """Nm"":""123"","
                        j = j & """Expdt"":""" & INVOICEDATE.Text & """" & ","
                        j = j & """wrDt"":""" & INVOICEDATE.Text & """" & "},"

                        j = j & """AttribDtls"": [{"
                        j = j & """Nm"":""" & DTROW("ITEMNAME") & """" & ","
                        j = j & """Val"":""" & Val(TEMPLINEGRIDTOTALAMT) & """" & "}]"
                    End If



                    j = j & " }"
                    CURRROW += 1


                    'THESE VARIABLES ARE JUST FOR TESTING PURPOSE
                    TEMPTOTALAMT += Val(DTROW("TOTALAMT"))
                    TEMPTOTALDISC += Val(TEMPLINECHARGES)
                    TEMPTOTALTAXABLEAMT += Val(TEMPLINETAXABLEAMT)
                    TEMPTOTALCGSTAMT += Val(TEMPLINECGSTAMT)
                    TEMPTOTALSGSTAMT += Val(TEMPLINESGSTAMT)
                    TEMPTOTALIGSTAMT += Val(TEMPLINEIGSTAMT)
                    TEMPGTOTALAMT += Val(TEMPLINEGRIDTOTALAMT)


                Next

                j = j & " ],"



                j = j & """ValDtls"": {"
                If INVOICESCREENTYPE = "TOTAL GST" Then
                    j = j & """AssVal"":" & Val(TXTSUBTOTAL.Text.Trim) & "" & ","
                    j = j & """CgstVal"":" & Val(TXTCGSTAMT1.Text.Trim) & "" & ","
                    j = j & """SgstVal"":" & Val(TXTSGSTAMT1.Text.Trim) & "" & ","
                    j = j & """IgstVal"":" & Val(TXTIGSTAMT1.Text.Trim) & "" & ","
                Else
                    j = j & """AssVal"":" & Val(LBLTOTALTAXABLEAMT.Text.Trim) & "" & ","
                    j = j & """CgstVal"":" & Val(LBLTOTALCGSTAMT.Text.Trim) & "" & ","
                    j = j & """SgstVal"":" & Val(LBLTOTALSGSTAMT.Text.Trim) & "" & ","
                    j = j & """IgstVal"":" & Val(LBLTOTALIGSTAMT.Text.Trim) & "" & ","
                End If

                j = j & """CesVal"":" & "0" & "" & ","
                j = j & """StCesVal"":" & "0" & "" & ","
                j = j & """Discount"":" & "0" & "" & ","
                j = j & """OthChrg"":" & Val(TXTTCSAMT.Text.Trim) & "" & ","
                j = j & """RndOffAmt"":" & Val(txtroundoff.Text.Trim) & "" & ","
                j = j & """TotInvVal"":" & Val(txtgrandtotal.Text.Trim) & "" & ","
                j = j & """TotInvValFc"":" & "0" & "" & "},"


                j = j & """PayDtls"": {"
                j = j & """Nm"":"" "","
                j = j & """Accdet"":"" "","
                j = j & """Mode"":""Credit"","
                j = j & """Fininsbr"":"" "","
                j = j & """Payterm"":"" "","
                j = j & """Payinstr"":"" "","
                j = j & """Crtrn"":"" "","
                j = j & """Dirdr"":"" "","
                j = j & """Crday"":" & Val(TXTCRDAYS.Text.Trim) & "" & ","
                j = j & """Paidamt"":" & "0" & "" & ","
                j = j & """Paymtdue"":" & Val(txtgrandtotal.Text.Trim) & "" & "},"


                j = j & """RefDtls"": {"
                j = j & """InvRm"":""TEST"","
                j = j & """DocPerdDtls"": {"
                j = j & """InvStDt"":""" & INVOICEDATE.Text & """" & ","
                j = j & """InvEndDt"":""" & INVOICEDATE.Text & """" & "},"

                j = j & """PrecDocDtls"": [{"
                j = j & """InvNo"":""" & DTINI.Rows(0).Item("PRINTINITIALS") & """" & ","
                j = j & """InvDt"":""" & INVOICEDATE.Text & """" & ","
                j = j & """OthRefNo"":"" ""}],"

                j = j & """ContrDtls"": [{"
                j = j & """RecAdvRefr"":"" "","
                j = j & """RecAdvDt"":""" & INVOICEDATE.Text & """" & ","
                j = j & """Tendrefr"":"" "","
                j = j & """Contrrefr"":"" "","
                j = j & """Extrefr"":"" "","
                j = j & """Projrefr"":"" "","
                j = j & """Porefr"":"" "","
                j = j & """PoRefDt"":""" & INVOICEDATE.Text & """" & "}]"
                j = j & "},"




                j = j & """AddlDocDtls"": [{"
                j = j & """Url"":""https://einv-apisandbox.nic.in"","
                j = j & """Docs"":""INVOICE"","
                j = j & """Info"":""INVOICE""}],"




                j = j & """ExpDtls"": {"
                j = j & """ShipBNo"":"" "","
                j = j & """ShipBDt"":""" & INVOICEDATE.Text & """" & ","
                j = j & """Port"":""INBOM1"","
                j = j & """RefClm"":""N"","
                j = j & """ForCur"":""AED"","
                j = j & """CntCode"":""AE""}"




                'THIS CODE IS WRITTEN COZ WHEN BILLTO AND SHIPTO ARE IN THE SAME PINCODE THEN WE HAVE TO PASS MINIMUM 10 KMS
                'OR ELSE IT WILL GIVE ERROR
                If DISPATCHFROMPINCODE = PARTYPINCODE Then PARTYKMS = 10

                If TXTVEHICLENO.Text.Trim <> "" Then
                    j = j & ","
                    j = j & """EwbDtls"": {"
                    j = j & """TransId"":""" & TRANSGSTIN & """" & ","
                    j = j & """TransName"":""" & cmbtrans.Text.Trim & """" & ","
                    j = j & """Distance"":" & Val(PARTYKMS) & "" & ","
                    j = j & """TransDocNo"":""" & TXTLRNO.Text.Trim & """" & ","
                    j = j & """TransDocDt"":""" & INVOICEDATE.Text & """" & ","
                    j = j & """VehNo"":""" & TXTVEHICLENO.Text.Trim & """" & ","
                    j = j & """VehType"":""" & "R" & """" & ","
                    j = j & """TransMode"":""1""" & "}"
                End If

                j = j & "}"


                Dim stream As Stream = REQUEST.GetRequestStream()
                Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(j)
                stream.Write(buffer, 0, buffer.Length)

                'POST request absenden
                RESPONSE = REQUEST.GetResponse()







            Catch ex As WebException
                'RESPONSE = ex.Response
                'MsgBox("Error While Generating EWB, Please check the Data Properly")
                ''ADD DATA IN EINVOICEENTRY
                'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

                RESPONSE = ex.Response
                READER = New StreamReader(RESPONSE.GetResponseStream())
                REQUESTEDTEXT = READER.ReadToEnd()
                GoTo ERRORMESSAGE
            End Try

            READER = New StreamReader(RESPONSE.GetResponseStream())
            REQUESTEDTEXT = READER.ReadToEnd()


            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 3
            TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
            If TEMPSTATUS = "1" Then
                TEMPSTATUS = "SUCCESS"
                MsgBox("E-Invoice Generated Successfully ")

            Else

ERRORMESSAGE:
                TEMPSTATUS = "FAILED"

                Dim ERRORMSG As String = ""
                STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ErrorMessage") + Len("ErrorMessage") + 5
                ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("""", STARTPOS) - 2
                ERRORMSG = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

                'ADD DATA IN EINVOICEENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED','" & REQUESTEDTEXT & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

                MsgBox("Error While Generating E-Invoice, " & REQUESTEDTEXT)

                Exit Sub
            End If


            Dim IRNNO As String = ""

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("irn") + Len("IRN") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("\", STARTPOS)
            IRNNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            TXTIRNNO.Text = IRNNO


            'WE NEED TO UPDATE THIS IRNNO IN DATABASE ALSO
            DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_IRNNO = '" & TXTIRNNO.Text.Trim & "' FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")

            'ADD DATA IN EINVOICEENTRY
            DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','" & TEMPSTATUS & "', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


            'ADD DATA IN EINVOICEENTRY FOR QRCODE
            If TEMPSTATUS = "SUCCESS" Then

                ''GET SIGNED QRCODE
                Dim req As New RestRequest
                req.AddParameter("application/json", j, RestSharp.ParameterType.RequestBody)
                'Dim client As New RestClient("http://gstsandbox.charteredinfo.com/eicore/dec/v1.03/Invoice/irn/" & TXTIRNNO.Text.Trim & "?aspid=1602611918&password=infosys123&gstin=34AACCC1596Q002&user_name=TaxProEnvPON&AuthToken=" & TOKEN & "&QrCodeSize=250")
                Dim client As New RestClient("https://einvapi.charteredinfo.com/eicore/dec/v1.03/Invoice/irn/" & TXTIRNNO.Text.Trim & "?aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&user_name=" & CMPEWBUSER & "&AuthToken=" & TOKEN & "&QrCodeSize=250")
                Dim res As IRestResponse = Await client.ExecuteTaskAsync(req)
                Dim respPl = New RespPl()
                respPl = JsonConvert.DeserializeObject(Of RespPl)(res.Content)
                Dim respPlGenIRNDec As New RespPlGenIRNDec()
                respPlGenIRNDec = JsonConvert.DeserializeObject(Of RespPlGenIRNDec)(respPl.Data)
                'MsgBox(respPlGenIRNDec.Irn)
                Dim qrImg As Byte() = Convert.FromBase64String(respPlGenIRNDec.QrCodeImage)
                Dim tc As TypeConverter = TypeDescriptor.GetConverter(GetType(Bitmap))
                Dim bitmap1 As Bitmap = CType(tc.ConvertFrom(qrImg), Bitmap)
                bitmap1.Save(Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png")
                PBQRCODE.ImageLocation = Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png"
                PBQRCODE.Refresh()

                If PBQRCODE.Image IsNot Nothing Then
                    Dim OBJINVOICE As New ClsInvoiceMaster
                    Dim MS As New IO.MemoryStream
                    PBQRCODE.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    OBJINVOICE.alParaval.Add(TXTINVOICENO.Text.Trim)
                    OBJINVOICE.alParaval.Add(cmbregister.Text.Trim)
                    OBJINVOICE.alParaval.Add(MS.ToArray)
                    OBJINVOICE.alParaval.Add(YearId)
                    Dim INTRES As Integer = OBJINVOICE.SAVEQRCODE()
                End If

                'DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_QRCODE = (SELECT * FROM OPENROWSET(BULK '" & Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png',SINGLE_BLOB) AS IMG) FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")


                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','QRCODE SUCCESS', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','QRCODE SUCCESS1', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


                'FOR GETTING EWBNO
                If TXTVEHICLENO.Text.Trim <> "" Then GETEWBFROMIRN(TOKEN, REQUESTEDTEXT)

            End If


            'STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("QrCodeImage\", 0) + Len("QrCodeImage\") + 5
            'ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("""", STARTPOS)
            ''Dim QRSTREAM As New MemoryStream
            ''Dim bmp As New Bitmap(QRSTREAM)
            ''bmp.Save(QRSTREAM, Drawing.Imaging.ImageFormat.Bmp)
            ''QRSTREAM.Position = STARTPOS
            ''Dim data As Byte()
            ''QRSTREAM.Read(data, STARTPOS, STARTPOS - ENDPOS)

            'Dim bytes() As Byte
            'Dim ImageInStringFormat As String = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            'Dim MS As System.IO.MemoryStream
            'Dim NewImage As Bitmap

            'Dim nbyte() As Byte = System.Text.Encoding.UTF8.GetBytes(ImageInStringFormat)
            'Dim BASE64STRING As String = Convert.ToBase64String(nbyte)

            'bytes = Convert.FromBase64String(BASE64STRING)
            'NewImage = BytesToBitmap(bytes)
            'MS = New System.IO.MemoryStream(bytes)
            'MS.Write(bytes, 0, bytes.Length)
            'NewImage.Save(MS, Drawing.Imaging.ImageFormat.Bmp)    ' = System.Drawing.Image.FromStream(MS, True)
            'NewImage.Save("d:\qrcode.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

            'IRNNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            ''ADD data IN EINVOICEENTRY
            'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','" & TEMPSTATUS & "', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETEWBFROMIRN(TOKEN As String, REQUESTEDTEXT As String)
        Try
            'check ewaybillno also


            'IF STATUS IS NOT 1 THEN TOKEN IS NOT GENERATED
            Dim STARTPOS As Integer = 0
            Dim ENDPOS As Integer = 0
            Dim EWBNO As String = ""


            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ewbno") + Len("ewbno") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("\", STARTPOS)
            EWBNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            TXTEWAYBILLNO.Text = EWBNO


            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            'WE NEED TO UPDATE THIS EWBNO IN DATABASE ALSO
            DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_EWAYBILLNO = '" & TXTEWAYBILLNO.Text.Trim & "' FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TEMPINVOICENO) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")

            'ADD DATA IN EWAYENTRY
            DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & EWBNO & "','SUCCESS', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

            PRINTEWB()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDUPLOADIRN_Click(sender As Object, e As EventArgs) Handles CMDUPLOADIRN.Click
        If (edit = True And USEREDIT = False And USERVIEW = False) Or (edit = False And USERADD = False) Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        OpenFileDialog1.Filter = "Pictures (*.png)|*.png"
        OpenFileDialog1.ShowDialog()

        OpenFileDialog1.AddExtension = True
        TXTFILENAME.Text = OpenFileDialog1.SafeFileName
        TXTIMGPATH.Text = OpenFileDialog1.FileName
        TXTNEWIMGPATH.Text = Application.StartupPath & "\UPLOADDOCS\" & TXTINVOICENO.Text.Trim & TXTUPLOADSRNO.Text.Trim & TXTFILENAME.Text.Trim
        On Error Resume Next

        If TXTIMGPATH.Text.Trim.Length <> 0 Then
            PBQRCODE.ImageLocation = TXTIMGPATH.Text.Trim
            PBQRCODE.Load(TXTIMGPATH.Text.Trim)
        End If
    End Sub

    Private Async Sub CMDGETQRCODE_Click(sender As Object, e As EventArgs) Handles CMDGETQRCODE.Click
        Try
            If edit = True And TXTIRNNO.Text.Trim <> "" And IsNothing(PBQRCODE.Image) = True Then

                'FIRST GETTOKEN AND THEN GET QRCODE
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable

                'Dim URL As New Uri("http://gstsandbox.charteredinfo.com/eivital/dec/v1.04/auth?aspid=1602611918&password=infosys123&Gstin=34AACCC1596Q002&user_name=TaxProEnvPON&eInvPwd=abc34*")
                Dim URL As New Uri("https://einvapi.charteredinfo.com/eivital/dec/v1.04/auth?aspid=1602611918&password=infosys123&Gstin=" & CMPGSTIN & "&user_name=" & CMPEWBUSER & "&eInvPwd=" & CMPEWBPASS)

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

                STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 2
                TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
                If TEMPSTATUS = "1" Then TEMPSTATUS = "SUCCESS" Else TEMPSTATUS = "FAILED"




                STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("authtoken") + Len("AUTHTOKEN") + 3
                ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS) - 1
                TOKEN = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

                'ADD DATA IN EINVOICEENTRY
                'DONT ADD IN EINVOICEENTRY, DONE BY GULKIT, IF FAILED THEN ADD
                'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


                'ONCE WE REC THE TOKEN WE WILL CREATE EWAY BILL
                'IF STATUS IS FAILED THEN ERROR MESSAGE
                If TEMPSTATUS = "FAILED" Then
                    MsgBox("Unable to create Eway Bill", MsgBoxStyle.Critical)
                    DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "','" & REQUESTEDTEXT & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                    Exit Sub
                End If


                ''GET SIGNED QRCODE
                Dim req As New RestRequest
                req.AddParameter("application/json", "", RestSharp.ParameterType.RequestBody)
                'Dim client As New RestClient("http://gstsandbox.charteredinfo.com/eicore/dec/v1.03/Invoice/irn/" & TXTIRNNO.Text.Trim & "?aspid=1602611918&password=infosys123&gstin=34AACCC1596Q002&user_name=TaxProEnvPON&AuthToken=" & TOKEN & "&QrCodeSize=250")
                Dim client As New RestClient("https://einvapi.charteredinfo.com/eicore/dec/v1.03/Invoice/irn/" & TXTIRNNO.Text.Trim & "?aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&user_name=" & CMPEWBUSER & "&AuthToken=" & TOKEN & "&QrCodeSize=250")
                Dim res As IRestResponse = Await client.ExecuteTaskAsync(req)
                Dim respPl = New RespPl()
                respPl = JsonConvert.DeserializeObject(Of RespPl)(res.Content)
                Dim respPlGenIRNDec As New RespPlGenIRNDec()
                respPlGenIRNDec = JsonConvert.DeserializeObject(Of RespPlGenIRNDec)(respPl.Data)
                'MsgBox(respPlGenIRNDec.Irn)
                Dim qrImg As Byte() = Convert.FromBase64String(respPlGenIRNDec.QrCodeImage)
                Dim tc As TypeConverter = TypeDescriptor.GetConverter(GetType(Bitmap))
                Dim bitmap1 As Bitmap = CType(tc.ConvertFrom(qrImg), Bitmap)
                bitmap1.Save(Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png")
                PBQRCODE.ImageLocation = Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png"
                PBQRCODE.Refresh()

                'If PBQRCODE.Image IsNot Nothing Then
                '    Dim OBJINVOICE As New ClsInvoiceMaster
                '    Dim MS As New IO.MemoryStream
                '    PBQRCODE.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                '    OBJINVOICE.alParaval.Add(TXTINVOICENO.Text.Trim)
                '    OBJINVOICE.alParaval.Add(cmbregister.Text.Trim)
                '    OBJINVOICE.alParaval.Add(MS.ToArray)
                '    OBJINVOICE.alParaval.Add(YearId)
                '    Dim INTRES As Integer = OBJINVOICE.SAVEQRCODE()
                'End If

                'DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_QRCODE = (SELECT * FROM OPENROWSET(BULK '" & Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png',SINGLE_BLOB) AS IMG) FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")

                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & TXTIRNNO.Text.Trim & "','QRCODE SUCCESS', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & TXTIRNNO.Text.Trim & "','QRCODE SUCCESS1', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                cmdOK_Click(sender, e)

            End If
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

    Private Sub TXTLBSRATE_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTLBSRATE.KeyPress
        Try
            numdotkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Dim DT As New DataTable
        Dim OBJCMN As New ClsCommon
        If edit = True Then SENDWHATSAPP(TEMPINVOICENO)
        DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_SENDWHATSAPP = 1 WHERE INVOICE_no = " & TEMPINVOICENO & " AND INVOICE_YEARID = " & YearId, "", "")
        LBLWHATSAPP.Visible = True
    End Sub

    Async Sub SENDWHATSAPP(INVNO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJCN As New SaleDesign
            OBJCN.MdiParent = MDIMain
            OBJCN.FRMSTRING = "INVOICE"
            OBJCN.DIRECTMAIL = False
            OBJCN.DIRECTPRINT = True
            OBJCN.DIRECTWHATSAPP = True
            OBJCN.REGNAME = cmbregister.Text.Trim
            OBJCN.PARTYNAME = cmbname.Text.Trim
            OBJCN.INVNO = Val(INVNO)
            OBJCN.NOOFCOPIES = 1
            OBJCN.Show()
            OBJCN.Close()


            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = cmbname.Text.Trim
            OBJWHATSAPP.AGENTNAME = CMBAGENT.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & cmbname.Text.Trim & "INVOICE_" & Val(INVNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add(cmbname.Text.Trim & "INVOICE_" & Val(INVNO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTLBSRATE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTLBSRATE.Validated
        Try
            If Val(TXTLBSRATE.Text.Trim) > 0 And CMBLBS.Text = "LBS" And CMBTYPE.Text = "YARN" Then
                For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                    ROW.Cells(GRATE.Index).Value = Format((Val(TXTLBSRATE.Text) * 1.1023) / 5, "0.0000")
                Next
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InvoiceMaster_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        fillcmb()
        If edit = False Then
            CMBTYPE.Text = "GREY"
            CMBSCREENTYPE.Text = INVOICESCREENTYPE
        End If
        HIDEVIEW()
        If ClientName = "NIRMALA" Then GPCS.ReadOnly = False

        If CMPTYPE = "AGRO" Then
            GLRNO.ReadOnly = False
            GLRNO.HeaderText = "Batch No"
            GLRDATE.ReadOnly = False
            GLRDATE.HeaderText = "Mfg Date"
        End If
    End Sub

    Private Sub TXTGRSHORTAGE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTGRSHORTAGE.Validated
        total()
    End Sub

    Private Sub TXTOTHERAMT_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTOTHERAMT.Validated
        Try
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTGRIDTOTAL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTGRIDTOTAL.Validating
        Try
            If CMBQUALITY.Text <> "" And Val(TXTPCS.Text) > 0 And Val(TXTMTRS.Text) > 0 Then fillgrid() Else MsgBox("Enter Proper Details !")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTYPE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTYPE.Validated
        HIDEVIEW()
        CMBTYPE.Enabled = False
    End Sub

    Private Sub TXTNARRATION_Validating(sender As Object, e As CancelEventArgs) Handles TXTNARRATION.Validating
        Try
            If CMBSCREENTYPE.Text = "TOTAL GST" Then
                Call TXTGRIDTOTAL_Validating(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDISPATCHFROM_Enter(sender As Object, e As EventArgs) Handles CMBDISPATCHFROM.Enter
        Try
            If CMBDISPATCHFROM.Text.Trim = "" Then fillname(CMBDISPATCHFROM, edit, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDISPATCHFROM_Validating(sender As Object, e As CancelEventArgs) Handles CMBDISPATCHFROM.Validating
        Try
            namevalidate(CMBDISPATCHFROM, CMBCODE, e, Me, TXTADD, " and (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS')", "Sundry DEBTORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class