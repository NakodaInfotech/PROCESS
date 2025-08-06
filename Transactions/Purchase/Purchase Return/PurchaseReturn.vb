
Imports BL
Imports System.Windows.Forms
Imports System.IO

Public Class PurchaseReturn

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDCHGSDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPCHGSROW, TEMPUPLOADROW, PURREGID As Integer
    Public EDIT As Boolean
    Public TEMPBILLNO, TEMPREGNAME, PURTYPE, SELECTEDREG As String
    Dim PURREGABBR, PURREGINITIAL As String
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

        TXTEWAYBILLNO.Clear()
        CMBTYPE.Enabled = True
        TXTSACCODE.Clear()

        PURRETDATE.Text = Mydate
        tstxtbillno.Clear()
        cmbname.Text = ""
        cmbname.Enabled = True
        TXTSTATECODE.Clear()
        TXTGSTIN.Clear()
        CMBMILL.Text = ""
        TXTCHALLANNO.Clear()
        CHALLANDATE.Clear()

        CMBWAREHOUSE.Text = ""
        CMBWAREHOUSE.Enabled = True
        CMBOURGODOWN.Text = ""
        CMBOURGODOWN.Enabled = True
        CMBJOBBER.Text = ""
        CMBJOBBER.Enabled = True

        CMBAGENT.Text = ""
        CMBTRANSPORT.Text = ""

        For I As Integer = 0 To CHKFORMBOX.Items.Count - 1
            Dim DTR As DataRowView = CHKFORMBOX.Items(I)
            CHKFORMBOX.SetItemCheckState(I, CheckState.Unchecked)
        Next

        CHKFORMBOX.SetItemChecked(CHKFORMBOX.FindStringExact("GST"), True)

        CMDSELECTBILL.Enabled = True
        CMDSELECTSTOCK.Enabled = True
        txtadd.Clear()

        CHKBILLCHECKED.Checked = False
        CHKBILLDISPUTE.Checked = False

        EP.Clear()

        txtremarks.Clear()
        txtinwords.Clear()

        GRIDBILL.RowCount = 0

        TXTCHGSSRNO.Text = 1
        CMBCHARGES.Text = ""
        TXTCHGSPER.Clear()
        TXTCHGSAMT.Clear()
        GRIDCHGS.RowCount = 0

        txtuploadsrno.Text = 1
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        txtimgpath.Clear()
        TXTFILENAME.Clear()
        TXTNEWIMGPATH.Clear()
        PBSoftCopy.Image = Nothing
        gridupload.RowCount = 0
        GRIDUPLOADDOUBLECLICK = False

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
        txtroundoff.Text = 0.0
        txtremarks.Clear()

        lbltotalbags.Text = 0
        LBLTOTALWT.Text = 0.0
        lbltotalamt.Text = 0.0

        TBITEMDETAILS.SelectedIndex = 0
        HIDEVIEW()
        TXTBILLNO.Clear()
        TXTPARTYBILLNO.Clear()
        BILLDATE.Text = Mydate
        PARTYBILLDATE.Text = Mydate
        TXTINVREGNAME.Clear()
        TXTINVTYPE.Clear()

        CHKMANUAL.CheckState = CheckState.Unchecked
        TXTCGSTPER.Text = 0
        TXTSGSTPER.Text = 0
        TXTIGSTPER.Text = 0
        TXTCGSTAMT.Text = 0
        TXTSGSTAMT.Text = 0
        TXTIGSTAMT.Text = 0
        LBLWHATSAPP.Visible = False
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

        txtuploadsrno.Text = Val(gridupload.RowCount) + 1
        txtuploadremarks.Clear()
        txtuploadname.Clear()
        PBSoftCopy.Image = Nothing
        txtimgpath.Clear()

        txtuploadremarks.Focus()

    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJBILL As New ClsPurchaseReturn

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
        DTTABLE = getmax(" isnull(max(PURRET_NO),0) + 1 ", "  PURCHASERETURN INNER JOIN REGISTERMASTER ON REGISTER_ID = PURRET_REGISTERID ", " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND REGISTER_TYPE = 'PURCHASERETURN' AND PURRET_YEARID = " & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTPURRETNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub PURCHASERETURN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        fillregister(cmbregister, " and register_type = 'PURCHASERETURN'")
        If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        If CMBTRANSPORT.Text.Trim = "" Then fillname(CMBTRANSPORT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        If CMBCHARGES.Text.Trim = "" Then fillname(CMBCHARGES, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' or GROUPMASTER.GROUP_SECONDARY = 'Sale A/C' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' )")
        If CMBAGENT.Text.Trim = "" Then fillname(CMBAGENT, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'")
        If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, False, " AND GODOWN_ISOUR = 'TRUE'")
        If CMBWAREHOUSE.Text.Trim = "" Then fillGODOWN(CMBWAREHOUSE, False, " AND GODOWN_ISOUR = 'FALSE'")
        If CMBJOBBER.Text.Trim = "" Then fillname(CMBJOBBER, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER' OR LEDGERS.ACC_SUBTYPE = 'WEAVER')")
        fillname(CMBMILL, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'")
        fillform(CHKFORMBOX, EDIT)
        If CMBSACDESC.Text.Trim = "" Then FILLSACCODE(CMBSACDESC, EDIT)

    End Sub

    Private Sub PurchaseRETURN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'PURCHASE RETURN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            clear()
            CMBOURGODOWN.Text = GETDEFAULTGODOWN()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dt As New DataTable
                Dim ALPARAVAL As New ArrayList
                Dim objclsINV As New ClsPurchaseReturn

                ALPARAVAL.Add(TEMPBILLNO)
                ALPARAVAL.Add(TEMPREGNAME)
                ALPARAVAL.Add(YearId)

                objclsINV.alParaval = ALPARAVAL
                dt = objclsINV.SELECTPR()

                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows

                        TXTPURRETNO.Text = TEMPBILLNO

                        CMBTYPE.Text = dr("PURTYPE")
                        CMBTYPE.Enabled = False
                        PURTYPE = dr("PURTYPE")
                        HIDEVIEW()

                        cmbregister.Text = Convert.ToString(dr("REGNAME"))
                        cmbname.Text = Convert.ToString(dr("NAME"))
                        TXTSTATECODE.Text = dr("STATECODE")
                        TXTGSTIN.Text = dr("GSTIN")

                        PURRETDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")


                        CMBMILL.Text = Convert.ToString(dr("MILLNAME"))
                        TXTCHALLANNO.Text = Convert.ToString(dr("CHALLANNO"))
                        CHALLANDATE.Text = Format(Convert.ToDateTime(dr("CHALLANDATE")).Date, "dd/MM/yyyy")
                        CMBWAREHOUSE.Text = Convert.ToString(dr("WAREHOUSE"))
                        CMBWAREHOUSE.Enabled = False
                        CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN"))
                        CMBOURGODOWN.Enabled = False
                        CMBJOBBER.Text = Convert.ToString(dr("JOBER"))
                        CMBJOBBER.Enabled = False

                        CMBAGENT.Text = Convert.ToString(dr("AGENT"))
                        CMBTRANSPORT.Text = dr("TRANSNAME")

                        TXTEWAYBILLNO.Text = dr("EWAYBILLNO")
                        If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True
                        TXTBILLNO.Text = Convert.ToString(dr("BILLNO"))
                        BILLDATE.Text = Format(Convert.ToDateTime(dr("BILLDATE")), "dd/MM/yyyy")
                        TXTPARTYBILLNO.Text = Convert.ToString(dr("PARTYBILL"))
                        PARTYBILLDATE.Text = Format(Convert.ToDateTime(dr("PARTYDATE")), "dd/MM/yyyy")
                        TXTINVREGNAME.Text = Convert.ToString(dr("PURREGNAME"))
                        TXTINVTYPE.Text = Convert.ToString(dr("INVOICETYPE"))

                        If dr("BILLCHECKED") = 0 Then CHKBILLCHECKED.Checked = False Else CHKBILLCHECKED.Checked = True
                        If dr("BILLDISPUTE") = 0 Then CHKBILLDISPUTE.Checked = False Else CHKBILLDISPUTE.Checked = True

                        If dr("MANUALGST") = 0 Then
                            CHKMANUAL.Checked = False
                        Else
                            CHKMANUAL.Checked = True
                        End If

                        TXTCGSTPER.Text = Val(dr("TOTALCGSTPER"))
                        TXTSGSTPER.Text = Val(dr("TOTALSGSTPER"))
                        TXTIGSTPER.Text = Val(dr("TOTALIGSTPER"))
                        TXTCGSTAMT.Text = Val(dr("TOTALCGSTAMT"))
                        TXTSGSTAMT.Text = Val(dr("TOTALSGSTAMT"))
                        TXTIGSTAMT.Text = Val(dr("TOTALIGSTAMT"))

                        txtremarks.Text = Convert.ToString(dr("REMARKS"))

                        'Item Grid
                        GRIDBILL.Rows.Add(dr("GRIDSRNO").ToString, dr("QUALITY").ToString, dr("HSNCODE"), Val(dr("BAGS")), Val(dr("WT")), Format(Val(dr("RATE")), "0.0000"), Format(Val(dr("NETTRATE")), "0.0000"), dr("PER"), Format(Val(dr("AMT")), "0.00"), Convert.ToString(dr("TRANSNAME")), Convert.ToString(dr("LRNO")), dr("LRDATE"), dr("FROMNO"), dr("FROMSRNO"), dr("GRIDTYPE"))


                        TBITEMDETAILS.SelectedIndex = (0)


                    Next

                    'CHARGES GRID
                    Dim OBJCMN As New ClsCommon
                    dt = OBJCMN.search(" PURCHASERETURN_CHGS.PURRET_gridsrno AS GRIDSRNO, ISNULL(LEDGERS.Acc_cmpname, '') AS CHARGES, ISNULL(PURCHASERETURN_CHGS.PURRET_PER, 0) AS PER, ISNULL(PURCHASERETURN_CHGS.PURRET_AMT, 0) AS AMT, ISNULL(TAXMASTER.TAX_ID, 0) AS TAXID ", "", " PURCHASERETURN INNER JOIN REGISTERMASTER ON PURCHASERETURN.PURRET_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN PURCHASERETURN_CHGS LEFT OUTER JOIN TAXMASTER ON PURCHASERETURN_CHGS.PURRET_TAXID = TAXMASTER.tax_id ON PURCHASERETURN.PURRET_NO = PURCHASERETURN_CHGS.PURRET_no AND PURCHASERETURN.PURRET_REGISTERID = PURCHASERETURN_CHGS.PURRET_REGISTERID LEFT OUTER JOIN LEDGERS ON PURCHASERETURN_CHGS.PURRET_CHARGESID = LEDGERS.Acc_id", " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND REGISTER_TYPE = 'PURCHASERETURN' AND PURCHASERETURN_CHGS.PURRET_NO = " & TEMPBILLNO & " AND PURCHASERETURN_CHGS.PURRET_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        For Each DTR As DataRow In dt.Rows
                            GRIDCHGS.Rows.Add(DTR("GRIDSRNO"), DTR("CHARGES"), DTR("PER"), DTR("AMT"), DTR("TAXID"))
                        Next
                    End If


                    'UPLOAD(GRID)
                    dt = OBJCMN.search(" PURCHASERETURN_UPLOAD.PURRET_SRNO AS GRIDSRNO, PURCHASERETURN_UPLOAD.PURRET_REMARKS AS REMARKS, PURCHASERETURN_UPLOAD.PURRET_NAME AS NAME, PURCHASERETURN_UPLOAD.PURRET_PHOTO AS IMGPATH ", "", "  PURCHASERETURN_UPLOAD INNER JOIN REGISTERMASTER ON PURCHASERETURN_UPLOAD.PURRET_REGISTERID = REGISTERMASTER.register_id ", " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND REGISTER_TYPE = 'PURCHASERETURN' AND PURCHASERETURN_UPLOAD.PURRET_NO = " & TEMPBILLNO & " AND PURRET_YEARID = " & YearId & " ORDER BY PURCHASERETURN_UPLOAD.PURRET_SRNO")
                    If dt.Rows.Count > 0 Then
                        For Each DTR As DataRow In dt.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If



                    dt = OBJCMN.search(" ISNULL(FORMTYPE.FORM_NAME, '') AS FORMNAME", "", "  PURCHASERETURN_FORMTYPE INNER JOIN REGISTERMASTER ON PURCHASERETURN_FORMTYPE.PURRET_REGISTERID = REGISTERMASTER.register_id AND PURCHASERETURN_FORMTYPE.PURRET_YEARID = REGISTERMASTER.register_yearid LEFT OUTER JOIN FORMTYPE ON PURCHASERETURN_FORMTYPE.PURRET_FORMID = FORMTYPE.FORM_ID ", " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND REGISTERMASTER.REGISTER_TYPE = 'PURCHASERETURN' AND PURCHASERETURN_FORMTYPE.PURRET_NO = " & TEMPBILLNO & " AND PURCHASERETURN_FORMTYPE.PURRET_YEARID= " & YearId)
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


                    dt = OBJCMN.search(" register_abbr, register_initials, register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'PURCHASERETURN' and register_YEARid = " & YearId)
                    If dt.Rows.Count > 0 Then
                        PURREGABBR = dt.Rows(0).Item(0).ToString
                        PURREGINITIAL = dt.Rows(0).Item(1).ToString
                        PURREGID = dt.Rows(0).Item(2)
                    End If
                    If GRIDBILL.RowCount > 0 Then GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1

                End If

                cmbregister.Enabled = False
                CMDSELECTBILL.Enabled = False
                total()
            Else
                EDIT = False
                clear()
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
            alParaval.Add(Format(Convert.ToDateTime(PURRETDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(CMBMILL.Text.Trim)
            alParaval.Add(TXTCHALLANNO.Text)
            alParaval.Add(Format(Convert.ToDateTime(CHALLANDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBWAREHOUSE.Text.Trim)
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(CMBJOBBER.Text.Trim)

            alParaval.Add(CMBAGENT.Text.Trim)
            alParaval.Add(CMBTRANSPORT.Text.Trim)
            alParaval.Add(TXTEWAYBILLNO.Text.Trim)

            alParaval.Add(TXTBILLNO.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(BILLDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(TXTPARTYBILLNO.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(PARTYBILLDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(TXTINVREGNAME.Text.Trim)
            alParaval.Add(TXTINVTYPE.Text.Trim)

            If CHKBILLCHECKED.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            If CHKBILLDISPUTE.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)

            If CHKMANUAL.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)

            alParaval.Add(Val(TXTCGSTPER.Text.Trim))
            alParaval.Add(Val(TXTCGSTAMT.Text.Trim))
            alParaval.Add(Val(TXTSGSTPER.Text.Trim))
            alParaval.Add(Val(TXTSGSTAMT.Text.Trim))
            alParaval.Add(Val(TXTIGSTPER.Text.Trim))
            alParaval.Add(Val(TXTIGSTAMT.Text.Trim))

            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(txtinwords.Text)
            alParaval.Add(Val(lbltotalbags.Text.Trim))
            alParaval.Add(Val(LBLTOTALWT.Text.Trim))
            alParaval.Add(Val(lbltotalamt.Text.Trim))


            alParaval.Add(Val(TXTBILLAMT.Text.Trim))
            alParaval.Add(Format(Val(TXTTOTALTAXAMT.Text.Trim), "0.00"))
            alParaval.Add(Format(Val(TXTTOTALOTHERCHGSAMT.Text.Trim), "0.00"))
            alParaval.Add(Format(Val(TXTCHARGES.Text.Trim), "0.00"))
            alParaval.Add(Format(Val(TXTSUBTOTAL.Text.Trim), "0.00"))
            alParaval.Add(Format(Val(txtroundoff.Text.Trim), "0.00"))
            alParaval.Add(Format(Val(txtgrandtotal.Text.Trim), "0.00"))


            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
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
            Dim BAGS As String = ""
            Dim WT As String = ""
            Dim RATE As String = ""         'value of RATE
            Dim NETTRATE As String = ""
            Dim PER As String = ""
            Dim AMT As String = ""
            Dim TRANSPORT As String = "" 'value of AMT
            Dim LRNO As String = ""
            Dim LRDATE As String = ""
            Dim FROMNO As String = ""        'WHETHER GRN IS DONE FOR THIS LINE
            Dim FROMSRNO As String = ""   'value of GRNGRIDSRNO
            Dim GRIDTYPE As String = ""      'WHETHER GRN IS DONE FOR THIS LINE

            For Each row As Windows.Forms.DataGridViewRow In GRIDBILL.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        HSNCODE = row.Cells(GHSNCODE.Index).Value.ToString
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
                        LRDATE = row.Cells(GGRIDLRDATE.Index).Value
                        FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)
                        GRIDTYPE = row.Cells(GGRIDTYPE.Index).Value

                    Else

                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        HSNCODE = HSNCODE & "|" & row.Cells(GHSNCODE.Index).Value.ToString
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
                        LRDATE = LRDATE & "|" & row.Cells(GGRIDLRDATE.Index).Value
                        FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)
                        GRIDTYPE = GRIDTYPE & "|" & row.Cells(GGRIDTYPE.Index).Value
                    End If

                End If

            Next

            alParaval.Add(gridsrno)
            alParaval.Add(QUALITY)
            alParaval.Add(HSNCODE)
            alParaval.Add(BAGS)
            alParaval.Add(WT)
            alParaval.Add(RATE)
            alParaval.Add(NETTRATE)
            alParaval.Add(PER)
            alParaval.Add(AMT)
            alParaval.Add(TRANSPORT)
            alParaval.Add(LRNO)
            alParaval.Add(LRDATE)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(GRIDTYPE)


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


            Dim OBJINV As New ClsPurchaseReturn
            OBJINV.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJINV.SAVE()
                TEMPBILLNO = DTTABLE.Rows(0).Item(0)
                MessageBox.Show("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPBILLNO)
                IntResult = OBJINV.UPDATE()
                MessageBox.Show("Details Updated")
                PRINTREPORT(TEMPBILLNO)
                EDIT = False
            End If


            If gridupload.RowCount > 0 Then SAVEUPLOAD()

            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            'clear()
            Call toolnext_Click(sender, e)
            PURRETDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
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

        If PURTYPE = "YARN PURCHASE" And CMBWAREHOUSE.Text.Trim.Length = 0 And CMBOURGODOWN.Text.Trim.Length = 0 And CMBJOBBER.Text.Trim.Length = 0 Then
            EP.SetError(CMBWAREHOUSE, " Please Select Either Warehouse / Godown / Jobber ")
            bln = False
        End If


        If GRIDBILL.RowCount = 0 Then
            EP.SetError(cmbname, "Select Item Details")
            bln = False
        End If

        For Each row As DataGridViewRow In GRIDBILL.Rows
            If Val(row.Cells(GAMT.Index).Value) = 0 Then
                EP.SetError(cmbname, "Amt Cannot be 0")
                bln = False
            End If

            'Pcs can be 0 in Jobber Stock
            'If Val(row.Cells(gbags.Index).Value) = 0 Then
            '    EP.SetError(cmbname, "Pcs/Bags Cannot be 0")
            '    bln = False
            'End If

            If Val(row.Cells(gwt.Index).Value) = 0 Then
                EP.SetError(cmbname, "Mtrs/Wt Cannot be 0")
                bln = False
            End If
        Next


        Dim FORMTYPE As String = ""
        For Each DTROW As DataRowView In CHKFORMBOX.CheckedItems
            FORMTYPE = DTROW.Item(0)
        Next
        If FORMTYPE = Nothing Then
            EP.SetError(CHKFORMBOX, "Pls Select Form Type")
            bln = False
        End If

        If CHALLANDATE.Text = "__/__/____" Then
            EP.SetError(CHALLANDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(CHALLANDATE.Text) Then
                EP.SetError(CHALLANDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If


        'If Convert.ToDateTime(PURRETDATE.Text).Date >= "01/02/2018" Or txtgrandtotal.Text > 50000 Then
        '    If TXTEWAYBILLNO.Text.Trim.Length = 0 Then
        '        If MsgBox("E-Way No. Not Entered, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
        '            EP.SetError(TXTEWAYBILLNO, " Please Enter E-Way No..... ")
        '            bln = False
        '        End If
        '    End If
        'End If

        If PURRETDATE.Text = "__/__/____" Then
            EP.SetError(PURRETDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(PURRETDATE.Text) Then
                EP.SetError(PURRETDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If TXTCHALLANNO.Text.Trim = "" Then
            EP.SetError(TXTCHALLANNO, "Pls Enter Proper Challan No.")
            bln = False
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
                    fillname(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE= 'PROCESSOR'")
                ElseIf PURTYPE = "SIZING CHGS" Then
                    fillname(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE= 'SIZER'")
                ElseIf PURTYPE = "WEAVING CHGS" Then
                    fillname(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE= 'WEAVER'")
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub total()
        Try
            lbltotalbags.Text = "0"
            LBLTOTALWT.Text = "0.0"
            lbltotalamt.Text = "0.0"
            TXTTOTALTAXAMT.Clear()
            TXTTOTALOTHERCHGSAMT.Clear()

            TXTBILLAMT.Text = 0.0
            TXTCHARGES.Text = 0.0
            TXTSUBTOTAL.Text = 0
            txtroundoff.Text = 0
            txtgrandtotal.Text = 0

            Dim OBJCMN As New ClsCommon
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



                    If Convert.ToDateTime(PURRETDATE.Text).Date >= "01/07/2017" Then
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
            If PURTYPE = "COMMON PURCHASE" And Convert.ToDateTime(PURRETDATE.Text).Date >= "01/07/2017" Then
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

            TXTSUBTOTAL.Text = Format(Val(TXTBILLAMT.Text) + Val(TXTCHARGES.Text.Trim), "0.00")

            If CHKMANUAL.CheckState = CheckState.Unchecked Then
                TXTCGSTAMT.Text = Format(Val(TXTCGSTPER.Text.Trim) * Val(TXTSUBTOTAL.Text.Trim) / 100, "0.00")
                TXTSGSTAMT.Text = Format(Val(TXTSGSTPER.Text.Trim) * Val(TXTSUBTOTAL.Text.Trim) / 100, "0.00")
                TXTIGSTAMT.Text = Format(Val(TXTIGSTPER.Text.Trim) * Val(TXTSUBTOTAL.Text.Trim) / 100, "0.00")
            End If

            txtgrandtotal.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT.Text.Trim) + Val(TXTSGSTAMT.Text.Trim) + Val(TXTIGSTAMT.Text.Trim), "0")
            txtroundoff.Text = Format(Val(txtgrandtotal.Text) - (Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT.Text.Trim) + Val(TXTSGSTAMT.Text.Trim) + Val(TXTIGSTAMT.Text.Trim)), "0.00")
            txtgrandtotal.Text = Format(Val(txtgrandtotal.Text), "0.00")


            'GET NETTRATE
            If Val(txtgrandtotal.Text.Trim) > 0 Then
                For Each row As DataGridViewRow In GRIDBILL.Rows
                    If PURTYPE = "YARN PURCHASE" Then
                        row.Cells(GNETTRATE.Index).Value = Format((Val(txtgrandtotal.Text) / Val(LBLTOTALWT.Text.Trim)), "0.000")
                    Else
                        If row.Cells(GPER.Index).EditedFormattedValue = "MTRS" Then row.Cells(GNETTRATE.Index).Value = Format((Val(txtgrandtotal.Text) / Val(LBLTOTALWT.Text.Trim)) * Val(row.Cells(gwt.Index).EditedFormattedValue), "0.000") Else row.Cells(GNETTRATE.Index).Value = Format((Val(txtgrandtotal.Text) / Val(lbltotalbags.Text.Trim)) * Val(row.Cells(gbags.Index).EditedFormattedValue), "0.000")
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

            If EDIT = False Then Exit Sub

            Dim intresult As Integer
            Dim objcls As New ClsPurchaseReturn()
            If MsgBox("Wish To Delete?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim alParaval As New ArrayList
            alParaval.Add(TEMPBILLNO)
            alParaval.Add(TEMPREGNAME)
            alParaval.Add(CMBWAREHOUSE.Text.Trim)
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            objcls.alParaval = alParaval
            intresult = objcls.Delete()
            MsgBox("Purchase Return Delete Successfully")
            clear()
            EDIT = False
            PURRETDATE.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDBILL.RowCount = 0
LINE1:
            TEMPBILLNO = Val(TXTPURRETNO.Text) - 1
            TEMPREGNAME = cmbregister.Text.Trim
            If TEMPBILLNO > 0 Then
                EDIT = True
                PurchaseRETURN_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If Val(txtgrandtotal.Text.Trim) = 0 And TEMPBILLNO > 1 Then
                TXTPURRETNO.Text = TEMPBILLNO
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
            TEMPBILLNO = Val(TXTPURRETNO.Text) + 1
            TEMPREGNAME = cmbregister.Text.Trim
            getmax_BILL_no()
            Dim MAXNO As Integer = TXTPURRETNO.Text.Trim
            clear()
            If Val(TXTPURRETNO.Text) - 1 >= TEMPBILLNO Then
                EDIT = True
                PurchaseRETURN_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If Val(txtgrandtotal.Text.Trim) = 0 And TEMPBILLNO < MAXNO Then
                TXTPURRETNO.Text = TEMPBILLNO
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

            Dim OBJPURRETURN As New PurchaseReturnDetails
            OBJPURRETURN.MdiParent = MDIMain
            OBJPURRETURN.SELECTEDREG = SELECTEDREG
            OBJPURRETURN.PURTYPE = PURTYPE
            OBJPURRETURN.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub CMBOURGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBOURGODOWN.Enter
        Try
            If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR = 'TRUE'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOURGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBOURGODOWN.Validating
        Try
            If CMBOURGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBOURGODOWN, e, Me, " AND GODOWN_ISOUR = 'TRUE'")
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
                OBJGODOWN.SEARCH = " AND GODOWN_ISOUR = 'TRUE'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBOURGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWAREHOUSE_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBWAREHOUSE.Enter
        Try
            If CMBWAREHOUSE.Text.Trim = "" Then fillGODOWN(CMBWAREHOUSE, EDIT, " AND GODOWN_ISOUR = 'FALSE'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWAREHOUSE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBWAREHOUSE.Validating
        Try
            If CMBWAREHOUSE.Text.Trim <> "" Then GODOWNVALIDATE(CMBWAREHOUSE, e, Me, " AND GODOWN_ISOUR = 'FALSE'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWAREHOUSE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBWAREHOUSE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGodown
                OBJGODOWN.FRMSTRING = "GODOWN"
                OBJGODOWN.SEARCH = " AND GODOWN_ISOUR = 'FALSE'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBWAREHOUSE.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBER_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBJOBBER.Enter
        Try
            If CMBJOBBER.Text.Trim = "" Then fillname(CMBJOBBER, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER' OR LEDGERS.ACC_SUBTYPE = 'WEAVER')")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBJOBER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBJOBBER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER' OR LEDGERS.ACC_SUBTYPE = 'WEAVER')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBJOBBER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBER_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBJOBBER.Validating
        Try
            If CMBJOBBER.Text.Trim <> "" Then namevalidate(CMBJOBBER, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER' OR LEDGERS.ACC_SUBTYPE = 'WEAVER')", "Sundry Creditors", "ACCOUNTS")
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
                    TXTSTATECODE.Text = DT.Rows(0).Item("STATECODE")
                    TXTGSTIN.Text = DT.Rows(0).Item("GSTIN")
                    total()
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

    'Private Sub CMDSELECTBILL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTBILL.Click
    '    Try

    '        If cmbname.Text.Trim = "" And CMBMILL.Text.Trim = "" Then
    '            MsgBox("Please Select Name First")
    '            cmbname.Focus()
    '            Exit Sub
    '        End If

    '        'WE HAVE GIVEN SELECT BILL AND SELECT STOCK COZ SOMTIME USER CAN SELECT FORM PURCHASE INVOICE, AND SOME TIME THEY CAN SELECT MULTIPLE STOCK DO OR YARN
    '        'DIRECTLY FROM STOCK
    '        If PURTYPE = "YARN PURCHASE" Then

    '            Dim DTTABLE As DataTable
    '            Dim OBJSELECTPO As New SelectGRN
    '            OBJSELECTPO.PARTYNAME = cmbname.Text.Trim
    '            OBJSELECTPO.MILLNAME = CMBMILL.Text.Trim
    '            OBJSELECTPO.FRMSTRING = "YARN"
    '            OBJSELECTPO.ShowDialog()

    '            DTTABLE = OBJSELECTPO.DT

    '            Dim i As Integer = 0
    '            If DTTABLE.Rows.Count > 0 Then

    '                Dim objclspreq As New ClsCommon()
    '                Dim DT As DataTable = objclspreq.search(" GRN.grn_no AS GRNNO, ISNULL(GRN.grn_challanno, '') AS RECNO,LEDGERS.Acc_cmpname AS NAME, GRN.grn_date AS DATE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(MILLNAME.Acc_cmpname, '') AS MILLNAME, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, ISNULL(GRN.GRN_TOTALBAGS,0) AS TOTALBAGS,ISNULL(GRN.GRN_TOTALWT,0) AS TOTALWT, ISNULL(BROKER.Acc_cmpname, '') AS BROKER, ISNULL(GRN.GRN_PONO,'') AS PONO ", "", " GRN INNER JOIN LEDGERS ON GRN.grn_ledgerid = LEDGERS.Acc_id INNER JOIN GODOWNMASTER ON GRN.GRN_GODOWNID = GODOWNMASTER.GODOWN_ID LEFT OUTER JOIN LEDGERS AS MILLNAME ON GRN.GRN_MILLID = MILLNAME.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON GRN.grn_transledgerid = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON GRN.GRN_BROKERID = BROKER.Acc_id ", " AND GRN.GRN_NO = " & DTTABLE.Rows(0).Item("GRNNO") & " AND GRN_TYPE = 'YARN' AND grn.grn_YEARID = " & YearId & "  ORDER BY GRN.grn_no")
    '                If DT.Rows.Count > 0 Then
    '                    For Each dr As DataRow In DT.Rows
    '                        cmbname.Text = dr("NAME")
    '                        CMBAGENT.Text = dr("BROKER")
    '                        CMBMILL.Text = dr("MILLNAME")
    '                        TXTCHALLANNO.Text = dr("GRNNO")

    '                    Next
    '                End If

    '                Dim OBJCMN As New ClsCommon()
    '                Dim DT2 As DataTable = OBJCMN.search(" ISNULL(GRN_DESC.grn_no, 0) AS GRNNO,ISNULL(GRN_DESC.grn_gridsrno, 0) AS SRNO, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(GRN_DESC.GRN_BAGS, 0)-ISNULL(GRN_DESC.GRN_PURBAGS, 0) AS BAGS, ISNULL(GRN_DESC.GRN_WT, 0)-ISNULL(GRN_DESC.GRN_PURWT, 0) AS WT, ISNULL(LEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(GRN_DESC.GRN_LRNO, '') AS LRNO, GRN_DESC.GRN_LRDATE AS LRDATE, ISNULL(GRN_DESC.GRN_NARRATION, '') AS NARRATION, ISNULL(PURCHASEORDER_DESC.PO_RATE, 0) AS RATE ", "", "   GRN INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS ON GRN.grn_transledgerid = LEDGERS.Acc_id LEFT OUTER JOIN QUALITYMASTER ON GRN_DESC.GRN_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN PURCHASEORDER_DESC ON GRN_DESC.GRN_POSRNO = PURCHASEORDER_DESC.PO_GRIDSRNO AND GRN_DESC.GRN_PONO = PURCHASEORDER_DESC.PO_NO AND GRN_DESC.GRN_GRIDTYPE = 'PO' ", " and GRN.GRN_NO=" & Val(TXTCHALLANNO.Text.Trim) & " AND GRN.GRN_YEARID = " & YearId)
    '                If DT2.Rows.Count > 0 Then
    '                    For Each dr As DataRow In DT2.Rows
    '                        'IF RATE IS 0 THEN CHECK OPENING PO
    '                        If Val(dr("RATE")) = 0 Then
    '                            Dim DTPORATE As DataTable = OBJCMN.search(" ISNULL(OPENINGPURCHASEORDER_DESC.OPPO_RATE, 0) AS RATE ", "", "   GRN INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS ON GRN.grn_transledgerid = LEDGERS.Acc_id LEFT OUTER JOIN QUALITYMASTER ON GRN_DESC.GRN_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN OPENINGPURCHASEORDER_DESC ON GRN_DESC.GRN_POSRNO = OPENINGPURCHASEORDER_DESC.OPPO_GRIDSRNO AND GRN_DESC.GRN_PONO = OPENINGPURCHASEORDER_DESC.OPPO_NO AND GRN_DESC.GRN_GRIDTYPE = 'OPENING' ", " and GRN.GRN_NO=" & Val(TXTCHALLANNO.Text.Trim) & " AND GRN.GRN_YEARID = " & YearId)
    '                            If DTPORATE.Rows.Count > 0 Then dr("RATE") = Val(DTPORATE.Rows(0).Item("RATE"))
    '                        End If
    '                        GRIDBILL.Rows.Add(0, dr("QUALITY"), Format(Val(dr("BAGS")), "0"), Format(Val(dr("WT")), "0.00"), Format(Val(dr("RATE")), "0.00"), 0, "1", "0.00", dr("TRANSPORT"), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), dr("INVNO"), Format(Convert.ToDateTime(dr("INVDATE")).Date, "dd/MM/yyyy"), dr("GRNNO"), dr("SRNO"), 0)
    '                        CMBTRANSPORT.Text = dr("TRANSPORT")
    '                    Next
    '                End If

    '                'cmbname.Enabled = False

    '                GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1
    '                getsrno(GRIDBILL)
    '                If GRIDBILL.RowCount > 0 Then
    '                    GRIDBILL.Focus()
    '                    GRIDBILL.CurrentCell = GRIDBILL.Rows(0).Cells(GRATE.Index)
    '                End If
    '            End If

    '        ElseIf PURTYPE = "FINISHED PURCHASE" Then

    '            Dim DTTABLE As DataTable
    '            Dim OBJSELECTPO As New SelectGRN
    '            OBJSELECTPO.PARTYNAME = cmbname.Text.Trim
    '            OBJSELECTPO.FRMSTRING = "FINISHED"
    '            OBJSELECTPO.ShowDialog()

    '            DTTABLE = OBJSELECTPO.DT

    '            Dim i As Integer = 0
    '            If DTTABLE.Rows.Count > 0 Then

    '                For i = 0 To DTTABLE.Rows.Count - 1
    '                    Dim objclspreq As New ClsCommon()
    '                    Dim DT As DataTable = objclspreq.search(" GRN.grn_no AS GRNNO, ISNULL(GRN.grn_challanno, '') AS RECNO,LEDGERS.Acc_cmpname AS NAME, GRN.grn_date AS DATE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(MILLNAME.Acc_cmpname, '') AS MILLNAME, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, ISNULL(GRN.GRN_TOTALBAGS,0) AS TOTALBAGS,ISNULL(GRN.GRN_TOTALWT,0) AS TOTALWT, ISNULL(BROKER.Acc_cmpname, '') AS BROKER ", "", " GRN INNER JOIN LEDGERS ON GRN.grn_ledgerid = LEDGERS.Acc_id INNER JOIN GODOWNMASTER ON GRN.GRN_GODOWNID = GODOWNMASTER.GODOWN_ID LEFT OUTER JOIN LEDGERS AS MILLNAME ON GRN.GRN_MILLID = MILLNAME.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON GRN.grn_transledgerid = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON GRN.GRN_BROKERID = BROKER.Acc_id ", " AND GRN.GRN_NO = " & DTTABLE.Rows(0).Item("GRNNO") & " AND GRN_TYPE = 'FINISHED' AND GRN.GRN_DONE=0 AND grn.grn_YEARID = " & YearId & "  ORDER BY GRN.grn_no")

    '                    If DT.Rows.Count > 0 Then
    '                        For Each dr As DataRow In DT.Rows
    '                            cmbname.Text = dr("NAME")
    '                            CMBAGENT.Text = dr("BROKER")
    '                            CMBMILL.Text = dr("MILLNAME")
    '                            TXTCHALLANNO.Text = dr("GRNNO")
    '                        Next
    '                    End If
    '                Next

    '                Dim OBJCMN As New ClsCommon()
    '                Dim DT2 As DataTable = OBJCMN.search("  ISNULL(GRN_DESC.GRN_NO, 0) AS GRNNO, ISNULL(GRN_DESC.GRN_GRIDSRNO, 0) AS SRNO, ISNULL(greyqualitymaster.GREY_NAME, '') AS QUALITY,  ISNULL(GRN_DESC.GRN_BAGS, 0) AS BAGS, ISNULL(GRN_DESC.GRN_WT, 0) AS WT, ISNULL(LEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(GRN_DESC.GRN_LRNO, '') AS LRNO, GRN_DESC.GRN_LRDATE AS LRDATE, ISNULL(GRN_DESC.GRN_NARRATION, '') AS NARRATION ", "", "   GRN INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS ON GRN.grn_transledgerid = LEDGERS.Acc_id LEFT OUTER JOIN greyqualitymaster ON GRN_DESC.GRN_QUALITYID = GREYQUALITYMASTER.GREY_ID  ", " and GRN.GRN_DONE=0 AND GRN.GRN_NO=" & Val(DTTABLE.Rows(0).Item("GRNNO")) & " AND GRN.GRN_YEARID = " & YearId)
    '                If DT2.Rows.Count > 0 Then
    '                    For Each dr As DataRow In DT2.Rows
    '                        GRIDBILL.Rows.Add(0, dr("QUALITY"), Format(Val(dr("BAGS")), "0"), Format(Val(dr("WT")), "0.00"), "0.00", 0, "MTRS", "0.00", dr("TRANSPORT"), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), dr("INVNO"), Format(Convert.ToDateTime(dr("INVDATE")).Date, "dd/MM/yyyy"), dr("GRNNO"), dr("SRNO"), 0)
    '                        CMBTRANSPORT.Text = dr("TRANSPORT")
    '                    Next
    '                End If

    '                'cmbname.Enabled = False
    '                GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1
    '                getsrno(GRIDBILL)
    '                If GRIDBILL.RowCount > 0 Then
    '                    GRIDBILL.Focus()
    '                    GRIDBILL.CurrentCell = GRIDBILL.Rows(0).Cells(GRATE.Index)
    '                End If
    '            End If

    '        ElseIf PURTYPE = "GREY PURCHASE" Then

    '            Dim DTTABLE As DataTable
    '            Dim OBJSELECTPO As New SelectGRN
    '            OBJSELECTPO.PARTYNAME = cmbname.Text.Trim
    '            OBJSELECTPO.FRMSTRING = "GREY"
    '            OBJSELECTPO.ShowDialog()

    '            DTTABLE = OBJSELECTPO.DT

    '            Dim i As Integer = 0
    '            If DTTABLE.Rows.Count > 0 Then

    '                For i = 0 To DTTABLE.Rows.Count - 1
    '                    Dim objclspreq As New ClsCommon()
    '                    Dim DT As DataTable = objclspreq.search(" GRN.grn_no AS GRNNO, ISNULL(GRN.grn_challanno, '') AS RECNO,LEDGERS.Acc_cmpname AS NAME, GRN.grn_date AS DATE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(MILLNAME.Acc_cmpname, '') AS MILLNAME, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, ISNULL(GRN.GRN_TOTALBAGS,0) AS TOTALBAGS,ISNULL(GRN.GRN_TOTALWT,0) AS TOTALWT, ISNULL(BROKER.Acc_cmpname, '') AS BROKER ", "", " GRN INNER JOIN LEDGERS ON GRN.grn_ledgerid = LEDGERS.Acc_id INNER JOIN GODOWNMASTER ON GRN.GRN_GODOWNID = GODOWNMASTER.GODOWN_ID LEFT OUTER JOIN LEDGERS AS MILLNAME ON GRN.GRN_MILLID = MILLNAME.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON GRN.grn_transledgerid = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON GRN.GRN_BROKERID = BROKER.Acc_id ", " AND GRN.GRN_NO = " & DTTABLE.Rows(0).Item("GRNNO") & " AND GRN_TYPE = 'GREY' AND GRN.GRN_DONE=0 AND grn.grn_YEARID = " & YearId & "  ORDER BY GRN.grn_no")

    '                    If DT.Rows.Count > 0 Then
    '                        For Each dr As DataRow In DT.Rows
    '                            cmbname.Text = dr("NAME")
    '                            CMBAGENT.Text = dr("BROKER")
    '                            CMBMILL.Text = dr("MILLNAME")
    '                            TXTCHALLANNO.Text = dr("GRNNO")
    '                        Next
    '                    End If
    '                Next

    '                Dim OBJCMN As New ClsCommon()
    '                Dim DT2 As DataTable = OBJCMN.search("  ISNULL(GRN_DESC.GRN_NO, 0) AS GRNNO, ISNULL(GRN_DESC.GRN_GRIDSRNO, 0) AS SRNO, ISNULL(greyqualitymaster.GREY_NAME, '') AS QUALITY,  ISNULL(GRN_DESC.GRN_BAGS, 0) AS BAGS, ISNULL(GRN_DESC.GRN_WT, 0) AS WT, ISNULL(LEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(GRN_DESC.GRN_LRNO, '') AS LRNO, GRN_DESC.GRN_LRDATE AS LRDATE, ISNULL(GRN_DESC.GRN_NARRATION, '') AS NARRATION ", "", "   GRN INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS ON GRN.grn_transledgerid = LEDGERS.Acc_id LEFT OUTER JOIN greyqualitymaster ON GRN_DESC.GRN_QUALITYID = GREYQUALITYMASTER.GREY_ID  ", " and GRN.GRN_DONE=0 AND GRN.GRN_NO=" & Val(DTTABLE.Rows(0).Item("GRNNO")) & " AND GRN.GRN_YEARID = " & YearId)
    '                If DT2.Rows.Count > 0 Then
    '                    For Each dr As DataRow In DT2.Rows
    '                        GRIDBILL.Rows.Add(0, dr("QUALITY"), Format(Val(dr("BAGS")), "0"), Format(Val(dr("WT")), "0.00"), "0.00", 0, "MTRS", "0.00", dr("TRANSPORT"), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), dr("INVNO"), Format(Convert.ToDateTime(dr("INVDATE")).Date, "dd/MM/yyyy"), dr("GRNNO"), dr("SRNO"), 0)
    '                        CMBTRANSPORT.Text = dr("TRANSPORT")
    '                    Next
    '                End If

    '                'cmbname.Enabled = False
    '                GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1
    '                getsrno(GRIDBILL)
    '                If GRIDBILL.RowCount > 0 Then
    '                    GRIDBILL.Focus()
    '                    GRIDBILL.CurrentCell = GRIDBILL.Rows(0).Cells(GRATE.Index)
    '                End If
    '            End If

    '        End If
    '        total()
    '    Catch ex As Exception
    '        If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
    '    End Try
    'End Sub

    Private Sub CMDSELECTBILL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTBILL.Click
        Try
            If cmbname.Text.Trim = "" Then
                MsgBox("Please Select Name First")
                cmbname.Focus()
                Exit Sub
            End If

            'WE HAVE GIVEN SELECT BILL AND SELECT STOCK COZ SOMTIME USER CAN SELECT FORM PURCHASE INVOICE, AND SOME TIME THEY CAN SELECT MULTIPLE STOCK DO OR YARN
            'DIRECTLY FROM STOCK
            'If PURTYPE = "YARN PURCHASE" Then

            Dim DTTABLE As DataTable
            Dim OBJSELECTPO As New SelectPurchaseInvoice
            OBJSELECTPO.PARTYNAME = cmbname.Text.Trim
            OBJSELECTPO.FRMSTRING = PURTYPE
            OBJSELECTPO.ShowDialog()

            DTTABLE = OBJSELECTPO.DT

            For Each ROW As DataRow In DTTABLE.Rows
                GETDATA(ROW("SRNO"), CMBTYPE.Text.Trim, Val(ROW("REGID")))
            Next
            total()
            CMDSELECTBILL.Enabled = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETDATA(ByVal SRNO As String, ByVal TYPE As String, ByVal REGID As Integer)
        Try
            Dim OBJCMN As New ClsCommon
            Dim DTTABLE As DataTable
            DTTABLE = OBJCMN.search("*", "", " (SELECT CAST(PURCHASEMASTER.BILL_NO AS VARCHAR(10)) AS BILLNO, PURCHASEMASTER.BILL_DATE AS INVDATE,  LEDGERS.Acc_cmpname AS NAME, PURCHASEMASTER.BILL_DATE AS DATE, ISNULL(PURCHASEMASTER_DESC.BILL_gridsrno, 0) AS GRIDSRNO, CASE WHEN BILL_PURTYPE = 'YARN PURCHASE' THEN ISNULL(QUALITYMASTER.QUALITY_NAME, '') ELSE ISNULL(GREYQUALITYMASTER.GREY_NAME, '') END AS QUALITY, ISNULL(PURCHASEMASTER_DESC.BILL_BAGS, 0) AS BAGS, ISNULL(PURCHASEMASTER_DESC.BILL_WT, 0) AS WT, ISNULL(PURCHASEMASTER_DESC.BILL_rate, 0) AS RATE, ISNULL(PURCHASEMASTER_DESC.BILL_NETTRATE, 0) AS NETTRATE, ISNULL(PURCHASEMASTER_DESC.BILL_PER, '1') AS PER, ISNULL(PURCHASEMASTER_DESC.BILL_amt, 0) AS AMT, ISNULL(TRANSLEDGER.Acc_cmpname, '')  AS TRANSNAME, ISNULL(PURCHASEMASTER_DESC.BILL_LRNO, '') AS LRNO, ISNULL(PURCHASEMASTER_DESC.BILL_LRDATE, GETDATE()) AS LRDATE,ISNULL(PURCHASEMASTER_DESC.BILL_gridsrno, '') AS FROMSRNO, PURCHASEMASTER.BILL_PURTYPE AS PURTYPE, CASE WHEN BILL_PURTYPE = 'YARN PURCHASE' THEN ISNULL(HSNMASTER.HSN_CODE,'') ELSE ISNULL(GREYHSNMASTER.HSN_CODE,'') END  AS HSNCODE, PURCHASEMASTER.BILL_REGISTERID AS PURREGID,'PURCHASE' AS INVOICETYPE, CASE WHEN BILL_PURTYPE = 'YARN PURCHASE' THEN ISNULL(HSNMASTER.HSN_CGST,'') ELSE ISNULL(GREYHSNMASTER.HSN_CGST,'') END  AS CGSTPER, CASE WHEN BILL_PURTYPE = 'YARN PURCHASE' THEN ISNULL(HSNMASTER.HSN_SGST,'') ELSE ISNULL(GREYHSNMASTER.HSN_SGST,'') END  AS SGSTPER, CASE WHEN BILL_PURTYPE = 'YARN PURCHASE' THEN ISNULL(HSNMASTER.HSN_IGST,'') ELSE ISNULL(GREYHSNMASTER.HSN_IGST,'') END  AS IGSTPER FROM LEDGERS AS TRANSLEDGER RIGHT OUTER JOIN PURCHASEMASTER_DESC LEFT OUTER JOIN GREYQUALITYMASTER LEFT OUTER JOIN HSNMASTER AS GREYHSNMASTER ON GREYQUALITYMASTER.GREY_HSNCODEID = GREYHSNMASTER.HSN_ID ON  PURCHASEMASTER_DESC.BILL_QUALITYID = GREYQUALITYMASTER.GREY_ID RIGHT OUTER JOIN PURCHASEMASTER INNER JOIN LEDGERS ON PURCHASEMASTER.BILL_LEDGERID = LEDGERS.Acc_id ON PURCHASEMASTER_DESC.BILL_yearid = PURCHASEMASTER.BILL_YEARID AND  PURCHASEMASTER_DESC.BILL_no = PURCHASEMASTER.BILL_NO AND PURCHASEMASTER_DESC.BILL_REGISTERID = PURCHASEMASTER.BILL_REGISTERID ON TRANSLEDGER.Acc_id = PURCHASEMASTER_DESC.BILL_TRANSPORTID LEFT OUTER JOIN HSNMASTER RIGHT OUTER JOIN QUALITYMASTER ON HSNMASTER.HSN_ID = QUALITYMASTER.QUALITY_HSNCODEID ON PURCHASEMASTER_DESC.BILL_QUALITYID = QUALITYMASTER.QUALITY_ID WHERE PURCHASEMASTER.BILL_NO = " & Val(SRNO) & " AND PURCHASEMASTER.BILL_REGISTERID = " & Val(REGID) & "  AND PURCHASEMASTER.BILL_PURTYPE= '" & TYPE & "'  AND PURCHASEMASTER.BILL_YEARID= " & YearId & " UNION ALL SELECT OPENINGBILL.BILL_INITIALS AS BILLNO, OPENINGBILL.BILL_DATE AS INVDATE,  LEDGERS.Acc_cmpname AS NAME, OPENINGBILL.BILL_DATE AS DATE, 0 AS GRIDSRNO, '' AS QUALITY, 0 AS BAGS, 0 AS WT, 0 AS RATE, 0 AS NETTRATE,'1' AS PER, 0 AS AMT, '' AS TRANSNAME, '' AS LRNO, BILL_DATE AS LRDATE, 0 AS FROMSRNO, '' AS PURTYPE, ''  AS HSNCODE, OPENINGBILL.BILL_REGISTERID AS PURREGID,'OPENING' AS INVOICETYPE, 0 AS CGSTPER, 0 AS SGSTPER, 0 AS IGSTPER FROM OPENINGBILL INNER JOIN LEDGERS ON OPENINGBILL.BILL_LEDGERID = LEDGERS.Acc_id WHERE OPENINGBILL.BILL_INITIALS = '" & SRNO & "' AND OPENINGBILL.BILL_REGISTERID = " & Val(REGID) & " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND OPENINGBILL.BILL_YEARID= " & YearId & ") AS T", "")
            If DTTABLE.Rows.Count > 0 Then
                If DTTABLE.Rows(0).Item("INVOICETYPE") = "PURCHASE" Then
                    For Each dr As DataRow In DTTABLE.Rows
                        GRIDBILL.Rows.Add(0, dr("QUALITY"), dr("HSNCODE"), Format(Val(dr("BAGS")), "0"), Format(Val(dr("WT")), "0.000"), Format(Val(dr("RATE")), "0.0000"), Format(Val(dr("NETTRATE")), "0.000"), dr("PER"), Format(Val(dr("AMT")), "0.00"), dr("TRANSNAME"), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), 0, 0, "")
                    Next
                End If

                ''  GETTING DISTINCT CHALLAN NO IN TEXTBOX
                Dim DV As DataView = DTTABLE.DefaultView
                Dim DT As DataTable = DV.ToTable(True, "BILLNO")
                For Each DTR As DataRow In DT.Rows
                    If TXTBILLNO.Text.Trim = "" Then
                        TXTBILLNO.Text = DTR("BILLNO").ToString
                    Else
                        TXTBILLNO.Text = TXTBILLNO.Text & ", " & DTR("BILLNO").ToString
                    End If
                Next

                TXTCGSTPER.Text = Val(DTTABLE.Rows(0).Item("CGSTPER"))
                TXTSGSTPER.Text = Val(DTTABLE.Rows(0).Item("SGSTPER"))
                TXTIGSTPER.Text = Val(DTTABLE.Rows(0).Item("IGSTPER"))

                For i = 0 To DTTABLE.Rows.Count - 1
                    Dim objclspreq As New ClsCommon()

                    If DTTABLE.Rows(0).Item("INVOICETYPE") = "PURCHASE" Then
                        DT = objclspreq.search("  LEDGERS.Acc_cmpname As NAME, PurchaseMaster.BILL_NO As BILLNO, PurchaseMaster.BILL_DATE As Date, ISNULL(PurchaseMaster.BILL_PARTYBILLNO, '') AS PARTYBILL, PURCHASEMASTER.BILL_PARTYBILLDATE AS PARTYDATE, ISNULL(AGENT.Acc_cmpname, '') AS AGENT, REGISTER_NAME AS PURREGNAME ", "", " PURCHASEMASTER INNER JOIN LEDGERS ON PURCHASEMASTER.BILL_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENT ON PURCHASEMASTER.BILL_AGENTID = AGENT.Acc_id INNER JOIN REGISTERMASTER ON PURCHASEMASTER.BILL_REGISTERID = REGISTERMASTER.REGISTER_ID ", "  and PURCHASEMASTER.BILL_NO='" & DTTABLE.Rows(i).Item("BILLNO") & "' and PURCHASEMASTER.BILL_REGISTERID =" & Val(REGID) & " AND PURCHASEMASTER.BILL_YEARID = " & YearId)
                    Else
                        DT = objclspreq.search("  LEDGERS.Acc_cmpname AS NAME, OPENINGBILL.BILL_INITIALS AS BILLNO, OPENINGBILL.BILL_DATE AS DATE, ISNULL(OPENINGBILL.BILL_NO, '') AS PARTYBILL, OPENINGBILL.BILL_DATE AS PARTYDATE, ISNULL(AGENT.Acc_cmpname, '') AS AGENT, REGISTER_NAME AS PURREGNAME ", "", "OPENINGBILL INNER JOIN LEDGERS ON OPENINGBILL.BILL_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENT ON OPENINGBILL.BILL_AGENTID = AGENT.Acc_id INNER JOIN REGISTERMASTER ON OPENINGBILL.BILL_REGISTERID = REGISTERMASTER.REGISTER_ID ", "  and OPENINGBILL.BILL_INITIALS='" & DTTABLE.Rows(i).Item("BILLNO") & "' and OPENINGBILL.BILL_REGISTERID=" & Val(REGID) & " AND OPENINGBILL.BILL_YEARID = " & YearId)
                    End If

                    TXTBILLNO.Text = DT.Rows(0).Item("BILLNO")
                    BILLDATE.Text = Format(Convert.ToDateTime(DT.Rows(0).Item("DATE")), "dd/MM/yyyy")
                    TXTPARTYBILLNO.Text = DT.Rows(0).Item("PARTYBILL")
                    PARTYBILLDATE.Text = Format(Convert.ToDateTime(DT.Rows(0).Item("PARTYDATE")), "dd/MM/yyyy")
                    TXTINVREGNAME.Text = DT.Rows(0).Item("PURREGNAME")
                    TXTINVTYPE.Text = DTTABLE.Rows(0).Item("INVOICETYPE")
                    CMBAGENT.Text = DT.Rows(0).Item("AGENT")

                Next
                total()
                'If PURTYPE = "YARN PURCHASE" Then
                '    For Each dr As DataRow In DTTABLE.Rows
                '        GRIDBILL.Rows.Add(0, dr("QUALITY"), dr("HSNCODE"), Format(Val(dr("BAGS")), "0"), Format(Val(dr("WT")), "0.000"), Format(Val(dr("RATE")), "0.000"), Format(Val(dr("NETTRATE")), "0.000"), "1", Format(Val(dr("AMT")), "0.00"), dr("TRANSNAME"), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), Val(dr("BILLNO")), Format(Convert.ToDateTime(dr("INVDATE")).Date, "dd/MM/yyyy"), Val(dr("PURREGID")), 0, 0, "")
                '    Next

                'ElseIf PURTYPE = "GREY PURCHASE" Then
                '    For Each dr As DataRow In DTTABLE.Rows
                '        GRIDBILL.Rows.Add(0, dr("QUALITY"), dr("HSNCODE"), Format(Val(dr("BAGS")), "0"), Format(Val(dr("WT")), "0.000"), Format(Val(dr("RATE")), "0.000"), Format(Val(dr("NETTRATE")), "0.000"), "1", Format(Val(dr("AMT")), "0.00"), dr("TRANSNAME"), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), Val(dr("BILLNO")), Format(Convert.ToDateTime(dr("INVDATE")).Date, "dd/MM/yyyy"), Val(dr("PURREGID")), 0, 0, "")
                '    Next

                'ElseIf PURTYPE = "FINISHED PURCHASE" Then
                '    For Each dr As DataRow In DTTABLE.Rows
                '        GRIDBILL.Rows.Add(0, dr("QUALITY"), dr("HSNCODE"), Format(Val(dr("BAGS")), "0"), Format(Val(dr("WT")), "0.000"), Format(Val(dr("RATE")), "0.000"), Format(Val(dr("NETTRATE")), "0.000"), "1", Format(Val(dr("AMT")), "0.00"), dr("TRANSNAME"), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), Val(dr("BILLNO")), Format(Convert.ToDateTime(dr("INVDATE")).Date, "dd/MM/yyyy"), Val(dr("PURREGID")), 0, 0, "")
                '    Next
                'End If
            End If
                getsrno(GRIDBILL)
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

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'PURCHASERETURN'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'PURCHASERETURN' and REGISTER_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then cmbregister.Text = dt.Rows(0).Item(0).ToString
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
                dt = clscommon.search(" register_abbr, register_initials, register_id", "", " RegisterMaster", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'PURCHASERETURN' AND REGISTER_YEARID = " & YearId)
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

        If EDIT = False Then
            For I As Integer = GRIDBILL.CurrentRow.Index + 1 To GRIDBILL.RowCount - 1
                GRIDBILL.Item(GRATE.Index, I).Value = GRIDBILL.Item(GRATE.Index, I - 1).EditedFormattedValue
                GRIDBILL.Item(GPER.Index, I).Value = GRIDBILL.Item(GPER.Index, I - 1).EditedFormattedValue
            Next
        End If
        total()
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDBILL.RowCount = 0

                TEMPBILLNO = Val(tstxtbillno.Text)
                TEMPREGNAME = cmbregister.Text.Trim
                If TEMPBILLNO > 0 Then
                    EDIT = True
                    PurchaseRETURN_Load(sender, e)
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
        total()
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
                total()
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
            Dim dt As DataTable = OBJCMN.search(" ISNULL(tax_tax, 0) as TAX, TAX_ID AS TAXID ", "", " TAXMASTER", " AND tax_name = '" & CMBCHARGES.Text & "' AND tax_YEARID = " & YearId)
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

            LBLWAREHOUSE.Visible = True
            CMBWAREHOUSE.Visible = True
            LBLJOBBER.Visible = True
            CMBJOBBER.Visible = True

            LBLSACDESC.Visible = False
            CMBSACDESC.Visible = False
            LBLSACCODE.Visible = False
            TXTSACCODE.Visible = False


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

            GHSNCODE.Visible = True

            BTNQUALITY.Left = Button1.Left + Button1.Width
            BTNHSNCODE.Left = BTNQUALITY.Left + BTNQUALITY.Width
            BTNBAGS.Left = BTNHSNCODE.Left + BTNHSNCODE.Width
            BTNWT.Left = BTNBAGS.Left + BTNBAGS.Width
            BTNRATE.Left = BTNWT.Left + BTNWT.Width
            BTNNETT.Left = BTNRATE.Left + BTNRATE.Width
            BTNPER.Left = BTNNETT.Left + BTNNETT.Width
            BTNAMT.Left = BTNPER.Left + BTNPER.Width
            BTNTRANS.Left = BTNAMT.Left + BTNAMT.Width
            BTNLRNO.Left = BTNTRANS.Left + BTNTRANS.Width
            BTNLRDATE.Left = BTNLRNO.Left + BTNLRNO.Width

            If PURTYPE = "YARN PURCHASE" Then
                Me.Text = "Yarn Purchase Return"

            ElseIf PURTYPE = "GREY PURCHASE" Then
                Me.Text = "Grey Purchase Return"
                LBLMILL.Visible = False
                CMBMILL.Visible = False
                LBLWAREHOUSE.Visible = False
                CMBWAREHOUSE.Visible = False
                LBLJOBBER.Visible = False
                CMBJOBBER.Visible = False
                BTNBAGS.Text = "Pcs"
                BTNWT.Text = "Mtrs"
                BTNTRANS.Visible = False
                BTNLRNO.Visible = False
                BTNLRDATE.Visible = False
                GTRANSPORT.Visible = False
                GLRNO.Visible = False
                GGRIDLRDATE.Visible = False

            ElseIf PURTYPE = "FINISHED PURCHASE" Then
                Me.Text = "Finished Purchase Return"
                LBLMILL.Visible = False
                CMBMILL.Visible = False
                LBLWAREHOUSE.Visible = False
                CMBWAREHOUSE.Visible = False
                LBLJOBBER.Visible = False
                CMBJOBBER.Visible = False
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
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURCHASERETURN_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            HIDEVIEW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then fillname(CMBMILL, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'")
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
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'"
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
            If CMBMILL.Text.Trim <> "" Then namevalidate(CMBMILL, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'  AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'", "Sundry Creditors", "ACCOUNTS", "", "", "MILL")
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
                OBJLEDGER.STRSEARCH = " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBCHARGES.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURRETDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles PURRETDATE.GotFocus
        PURRETDATE.Focus()
    End Sub

    Private Sub PURRETDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PURRETDATE.Validating
        Try
            If PURRETDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(PURRETDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPBILLNO)
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



    Sub PRINTREPORT(ByVal PURCHASERETNO As Integer)
        Try
            If MsgBox("Wish to Print Purchase Return?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim OBJINVOICE As New PurchaseReturnDesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.FRMSTRING = "PURRETURN"
                OBJINVOICE.WHERECLAUSE = "{PURCHASERETURN.PURRET_NO}=" & Val(TEMPBILLNO) & " and {REGISTERMASTER.REGISTER_NAME} = '" & cmbregister.Text.Trim & "' AND {PURCHASERETURN.PURRET_yearid}=" & YearId
                OBJINVOICE.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub CMBSACDESC_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSACDESC.Validated
        Try
            total()
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

    Private Sub CHALLANDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CHALLANDATE.Validating
        Try
            If CHALLANDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(CHALLANDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTSTOCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTSTOCK.Click
        Try
            'WE HAVE GIVEN SELECT BILL AND SELECT STOCK COZ SOMTIME USER CAN SELECT FROM PURCHASE INVOICE, AND SOME TIME THEY CAN SELECT MULTIPLE STOCK DO OR YARN
            'DIRECTLY FROM STOCK
            If PURTYPE = "YARN PURCHASE" Then

                'OPEN ONLY YARN STOCK 
                'BOTH WAREHOUSE STOCK AND OUR GODOWN STOCK
                'DEPENDING UPON THE GODOWN SELECTED
                If CMBWAREHOUSE.Text.Trim = "" And CMBOURGODOWN.Text.Trim = "" And CMBJOBBER.Text.Trim = "" Then
                    MsgBox("Please Select Godown Or Jobber First")
                    CMBWAREHOUSE.Focus()
                    Exit Sub
                End If

                If PURRETDATE.Text = "__/__/____" Then
                    MsgBox("Please Select Date First")
                    PURRETDATE.Focus()
                    Exit Sub
                End If

                Dim OBJCMN As New ClsCommon
                Dim DTTABLE As DataTable
                Dim I As Integer = 0

                'IF WAREHOUSE IS SELECTED THEN DEDUCT STOCK FROM WAREHOUSE
                If CMBWAREHOUSE.Text.Trim <> "" Then

                    Dim OBJSELECTGRN As New SelectGRNforDO
                    OBJSELECTGRN.GODOWN = CMBWAREHOUSE.Text.Trim
                    OBJSELECTGRN.DODATE = Convert.ToDateTime(PURRETDATE.Text).Date
                    OBJSELECTGRN.ShowDialog()

                    DTTABLE = OBJSELECTGRN.DT1
                    If DTTABLE.Rows.Count > 0 Then
                        CMBMILL.Text = DTTABLE.Rows(0).Item("MILLNAME")

                        For Each dr As DataRow In DTTABLE.Rows
                            GRIDBILL.Rows.Add(0, dr("QUALITY"), "", Format(Val(dr("BAGS")), "0"), Format(Val(dr("WT")), "0.00"), 0, 0, "1", "0.00", dr("TRANSPORT"), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), Val(dr("NO")), Val(dr("SRNO")), dr("GRIDTYPE"))
                        Next
                        GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1
                        getsrno(GRIDBILL)

                        'IF WAREHOUSE IS SELECTED THEN OTHER GODOWN AND JOBBER SHOULD BE DISABLED AND BLANK
                        CMBWAREHOUSE.Enabled = False
                        CMBOURGODOWN.Text = ""
                        CMBOURGODOWN.Enabled = False
                        CMBJOBBER.Text = ""
                        CMBJOBBER.Enabled = False
                    End If
                    total()
                End If


                'IF GODOWN IS SELECTED THEN DEDUCT STOCK FROM OURGODOWN
                If CMBOURGODOWN.Text.Trim <> "" Then
                    Dim OBJSELECTGDN As New SelectYarnStock
                    OBJSELECTGDN.GODOWN = CMBOURGODOWN.Text.Trim
                    OBJSELECTGDN.FRMSTRING = "YARNRETURN"
                    OBJSELECTGDN.ENTRYDATE = Format(Convert.ToDateTime(PURRETDATE.Text).Date, "dd/MM/yyyy")
                    OBJSELECTGDN.ShowDialog()
                    DTTABLE = OBJSELECTGDN.DT

                    If DTTABLE.Rows.Count > 0 Then
                        CMBMILL.Text = DTTABLE.Rows(0).Item("MILLNAME")
                        For Each dr As DataRow In DTTABLE.Rows
                            GRIDBILL.Rows.Add(0, dr("QUALITY"), "", Format(Val(dr("BAGS")), "0"), Format(Val(dr("WT")), "0.000"), 0, 0, "1", "0.00", "", "", "", Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("FROMTYPE"))
                        Next
                        GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1
                        getsrno(GRIDBILL)
                        CMBWAREHOUSE.Enabled = False
                        CMBWAREHOUSE.Text = ""
                        CMBOURGODOWN.Enabled = False
                        CMBJOBBER.Text = ""
                        CMBJOBBER.Enabled = False
                        total()
                    End If
                End If


                'IF JOBBER IS SELECTED THEN DEDUCT STOCK FROM JOBBERSTOCK
                If CMBJOBBER.Text.Trim <> "" Then
                    Dim OBJSTOCK As New SelectStockforWinding

                    'GET STOCKTYPE WITH RESPECT TO SELECTED JOBBER
                    Dim DT As DataTable = OBJCMN.search("ACC_SUBTYPE AS SUBTYPE", "", "LEDGERS", " AND ACC_CMPNAME = '" & CMBJOBBER.Text.Trim & "' AND ACC_YEARID = " & YearId)
                    OBJSTOCK.STOCKTYPE = DT.Rows(0).Item("SUBTYPE")
                    OBJSTOCK.STOCKNAME = CMBJOBBER.Text.Trim

                    OBJSTOCK.ShowDialog()
                    DTTABLE = OBJSTOCK.DT

                    If DTTABLE.Rows.Count > 0 Then
                        For Each dr As DataRow In DTTABLE.Rows
                            GRIDBILL.Rows.Add(0, dr("QUALITY"), "", 0, Format(Val(dr("WT")), "0.000"), 0, 0, "1", "0.00", "", "", "", 0, "", 0, Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("FROMTYPE"))
                        Next
                        GRIDBILL.FirstDisplayedScrollingRowIndex = GRIDBILL.RowCount - 1
                        getsrno(GRIDBILL)
                        CMBWAREHOUSE.Enabled = False
                        CMBWAREHOUSE.Text = ""
                        CMBOURGODOWN.Enabled = False
                        CMBOURGODOWN.Text = ""
                        CMBJOBBER.Enabled = False
                        total()
                    End If
                End If

            End If


            If PURTYPE = "GREY PURCHASE" Then

                'OPEN ONLY GREY STOCK DEPENDING UPON THE GODOWN SELECTED
                If CMBOURGODOWN.Text.Trim = "" Then
                    MsgBox("Please Select Godown First")
                    CMBOURGODOWN.Focus()
                    Exit Sub
                End If

                Dim OBJSELECTSTOCK As New SelectGreyStock
                OBJSELECTSTOCK.TEMPGODOWNNAME = CMBOURGODOWN.Text.Trim
                Dim DTTABLE As DataTable = OBJSELECTSTOCK.DT
                OBJSELECTSTOCK.ShowDialog()
                If DTTABLE.Rows.Count > 0 Then
                    For Each ROW As DataRow In DTTABLE.Rows
                        GRIDBILL.Rows.Add(0, ROW("GREYQUALITY"), "", Val(ROW("PCS")), Format(Val(ROW("MTRS")), "0.00"), 0, 0, "1", "0.00", "", "", "", 0, "", 0, 0, 0, "")
                    Next

                    total()
                    getsrno(GRIDBILL)
                End If
            End If


            If PURTYPE = "FINISHED PURCHASE" Then

            End If
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

                total()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCGSTAMT_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTCGSTAMT.Validated, TXTSGSTAMT.Validated, TXTIGSTAMT.Validated
        total()
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If EDIT = True Then SENDWHATSAPP(TEMPBILLNO)
            DT = OBJCMN.Execute_Any_String("UPDATE PURCHASERETURN SET PURRET_SENDWHATSAPP = 1 WHERE PURRET_NO = " & TEMPBILLNO & " AND PURRET_YEARID = " & YearId, "", "")
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
            Dim OBJSO As New PurchaseReturnDesign
            OBJSO.MdiParent = MDIMain
            OBJSO.DIRECTPRINT = True
            OBJSO.FRMSTRING = "PURRETURN"
            OBJSO.DIRECTWHATSAPP = True
            OBJSO.PARTYNAME = cmbname.Text.Trim
            OBJSO.AGENTNAME = CMBAGENT.Text.Trim
            OBJSO.FORMULA = "{PURCHASERETURN.SALRET_NO}=" & Val(BILLNO) & " and {PURCHASERETURN.PURRET_YEARID}=" & YearId
            OBJSO.BILLNO = BILLNO
            OBJSO.NOOFCOPIES = 1
            OBJSO.Show()
            OBJSO.Close()

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = cmbname.Text.Trim
            OBJWHATSAPP.AGENTNAME = CMBAGENT.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & cmbname.Text.Trim & "_PURCHASERETURN_NO-" & Val(BILLNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add(cmbname.Text.Trim & "PURCHASERETURN_" & Val(BILLNO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class



