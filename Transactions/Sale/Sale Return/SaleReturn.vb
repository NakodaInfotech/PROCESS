
Imports BL
Imports System.IO
Imports System.ComponentModel
Imports Newtonsoft.Json
Imports System.Net
Imports RestSharp
Imports TaxProEInvoice.API

Public Class SaleReturn

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK, GRIDCHGSDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public edit As Boolean
    Public TEMPSALERETNO, TEMPREGNAME As String
    Dim TEMPUPLOADROW, TEMPCHGSROW, saleregid As Integer
    Dim saleregabbr, salereginitial As String
    Dim TEMPFORM As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEXIT.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        edit = False
        CMBREGISTER.Enabled = True
        CMBREGISTER.Focus()
    End Sub

    Sub clear()

        CMBTYPE.SelectedIndex = 0
        CMBTYPE.Enabled = True
        SALERETDATE.Text = Mydate
        DTDODATE.Clear()
        tstxtbillno.Clear()
        TXTSALERETNO.ReadOnly = False
        CMBNAME.Text = ""
        CMBNAME.Enabled = True
        CMBAGENT.Text = ""
        cmbtrans.Text = ""
        CMBDYEINGNAME.Text = ""
        TXTDONO.Clear()
        TXTFOLD.Clear()
        TXTORDERDISC.Clear()
        TXTSHORTAGE.Clear()
        LBLWHATSAPP.Visible = False
        TXTEWAYBILLNO.Clear()

        TXTCHALLANNO.Clear()
        CHALLANDATE.Clear()
        CMBGODOWN.Text = ""
        TXTADD.Clear()
        txtremarks.Clear()
        TXTINVNO.Clear()
        TXTINVREGNAME.Clear()
        INVDATE.Value = Mydate
        TXTINVTYPE.Clear()
        TXTINVINITIALS.Clear()
        TXTINVPRINTINITIALS.Clear()

        DTRETURNDATE.Clear()
        TXTINVTAKA.Clear()
        TXTINVMTRS.Clear()
        TXTNETTTAKA.Clear()
        TXTNETTMTRS.Clear()


        CHKFORMBOX.Enabled = True
        For I As Integer = 0 To CHKFORMBOX.Items.Count - 1
            Dim DTR As DataRowView = CHKFORMBOX.Items(I)
            CHKFORMBOX.SetItemCheckState(I, CheckState.Unchecked)
        Next

        CHKFORMBOX.SetItemChecked(CHKFORMBOX.FindStringExact("GST"), True)



        CHKBILLCHECKED.Checked = False
        CHKBILLDISPUTE.Checked = False
        CHKMANUAL.CheckState = CheckState.Unchecked

        EP.Clear()
        txtinwords.Clear()
        GRIDINVOICE.RowCount = 0
        CMBQUALITY.Text = ""
        TXTLOTNO.Clear()
        CMBSHADE.Text = ""
        TXTBALENO.Clear()
        TXTPCS.Clear()
        TXTMTRS.Clear()
        TXTRATE.Clear()
        CMBPER.SelectedIndex = 0
        TXTAMOUNT.Clear()
        CMBMILLNAME.Text = ""
        TXTNARRATION.Clear()

        TXTCHGSSRNO.Clear()
        CMBCHARGES.Text = ""
        TXTCHGSPER.Clear()
        TXTCHGSAMT.Clear()
        GRIDCHGS.RowCount = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Text = 1
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0

        getmax_SALRET_no()

        GRIDDOUBLECLICK = False
        GRIDCHGSDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        txtbillamt.Text = 0.0
        TXTSUBTOTAL.Text = 0.0
        TXTTOTALTAXAMT.Clear()
        TXTTOTALOTHERCHGSAMT.Clear()
        TXTCHARGES.Text = 0.0
        txtgrandtotal.Text = 0.0
        txtroundoff.Text = 0.0
        txtremarks.Clear()

        LBLTOTALPCS.Text = 0
        lbltotalamt.Text = 0.0
        LBLTOTALMTRS.Text = 0.0

        TXTHSNCODE.Clear()
        TXTCGSTPER.Clear()
        TXTCGSTAMT.Clear()
        TXTSGSTPER.Clear()
        TXTSGSTAMT.Clear()
        TXTIGSTPER.Clear()
        TXTIGSTAMT.Clear()

        TXTSTATECODE.Clear()
        TXTGSTIN.Clear()
        TXTIRNNO.Clear()
        TXTACKNO.Clear()
        PBQRCODE.Image = Nothing

        TabControl2.SelectedIndex = 0

        TXTCHGSSRNO.Text = 1
        TXTUPLOADSRNO.Text = 1

    End Sub

    Sub getmax_SALRET_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(SALRET_no),0) + 1 ", "SALERETURN INNER JOIN REGISTERMASTER ON REGISTER_ID = SALRET_REGISTERID  AND REGISTER_YEARID = SALRET_YEARID  ", " AND REGISTERMASTER.REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "'  and SALRET_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTSALERETNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub SALERETURN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call PrintToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Windows.Forms.Keys.F5 Then       'for grid foucs
                GRIDINVOICE.Focus()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try
            fillregister(CMBREGISTER, " and register_type = 'SALERETURN'")
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
            If CMBAGENT.Text.Trim = "" Then fillname(CMBAGENT, edit, " and GROUPMASTER.GROUP_NAME = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
            If cmbtrans.Text = "" Then fillname(cmbtrans, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
            If CMBCHARGES.Text.Trim = "" Then fillname(CMBCHARGES, edit, " AND (GROUPMASTER.GROUP_SECONDARY ='Indirect Income' OR GROUPMASTER.GROUP_SECONDARY ='Indirect Expenses' or GROUPMASTER.GROUP_SECONDARY ='Direct Income' OR GROUPMASTER.GROUP_SECONDARY ='Direct Expenses' OR GROUPMASTER.GROUP_SECONDARY ='Duties & Taxes' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' OR GROUPMASTER.GROUP_SECONDARY = 'Sale A/C')")
            If CMBQUALITY.Text.Trim = "" AndAlso CMBTYPE.Text.Trim = "GREY" Then FILLGREY(CMBQUALITY, False) Else fillQUALITY(CMBQUALITY, edit)
            If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE)
            If CMBMILLNAME.Text.Trim = "" Then fillname(CMBMILLNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'")
            fillform(CHKFORMBOX, edit)
            If CMBDYEINGNAME.Text.Trim = "" Then fillname(CMBDYEINGNAME, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'PROCESSOR'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SALERETURN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE RETURN'")

            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
            CMBTYPE.SelectedIndex = 0
            fillcmb()
            clear()
            CMBNAME.Enabled = True
            CMBGODOWN.Text = GETDEFAULTGODOWN()

            If edit = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJSALERET As New ClsSaleReturn()
                Dim DT As DataTable = OBJSALERET.selectSALERETURN(TEMPSALERETNO, TEMPREGNAME, YearId)

                If DT.Rows.Count > 0 Then
                    For Each dr As DataRow In DT.Rows

                        CMBTYPE.Text = dr("SALRETTYPE")
                        CMBTYPE.Enabled = False

                        TXTSTATECODE.Text = dr("STATECODE")
                        TXTGSTIN.Text = dr("GSTIN")
                        TXTSALERETNO.Text = TEMPSALERETNO

                        TXTSALERETNO.ReadOnly = True

                        CMBREGISTER.Text = Convert.ToString(dr("REGNAME"))
                        SALERETDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(dr("NAME"))
                        CMBAGENT.Text = Convert.ToString(dr("AGENT"))
                        cmbtrans.Text = dr("TRANSNAME")
                        CMBDYEINGNAME.Text = dr("DYEING")
                        TXTDONO.Text = dr("DONO")
                        DTDODATE.Text = DT.Rows(0).Item("DODATE")
                        TXTFOLD.Text = dr("FOLD")
                        TXTORDERDISC.Text = Val(dr("ORDERDISC"))
                        TXTSHORTAGE.Text = Val(dr("SHORTAGE"))
                        TXTCHALLANNO.Text = Convert.ToString(dr("CHALLANNO"))
                        CHALLANDATE.Text = Format(Convert.ToDateTime(dr("CHALLANDATE")).Date, "dd/MM/yyyy")

                        If dr("RETURNDATE") <> "01/01/1900" Then DTRETURNDATE.Text = dr("RETURNDATE")
                        TXTINVTAKA.Text = Val(dr("INVTAKA"))
                        TXTINVMTRS.Text = Val(dr("INVMTRS"))
                        TXTNETTTAKA.Text = Val(dr("NETTTAKA"))
                        TXTNETTMTRS.Text = Val(dr("NETTMTRS"))

                        TXTINVNO.Text = dr("INVNO")
                        TXTINVREGNAME.Text = Convert.ToString(dr("INVREGNAME").ToString)
                        INVDATE.Text = Format(Convert.ToDateTime(dr("INVDATE")).Date, "dd/MM/yyyy")
                        TXTINVTYPE.Text = Convert.ToString(dr("INVTYPE").ToString)
                        TXTINVINITIALS.Text = Convert.ToString(dr("INVINITIALS").ToString)
                        TXTINVPRINTINITIALS.Text = Convert.ToString(dr("INVPRINTINITIALS").ToString)

                        CMBGODOWN.Text = dr("GODOWN")
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
                        'Item Grid

                        txtbillamt.Text = dr("AMOUNT")
                        TXTCHARGES.Text = dr("CHARGES")
                        txtroundoff.Text = dr("ROUNDOFF")
                        TXTSUBTOTAL.Text = Val(dr("SUBTOTAL"))
                        txtgrandtotal.Text = dr("GRANDTOTAL")

                        TXTIRNNO.Text = dr("IRNNO")
                        TXTACKNO.Text = dr("ACKNO")
                        ACKDATE.Value = dr("ACKDATE")
                        If IsDBNull(dr("QRCODE")) = False Then
                            PBQRCODE.Image = Image.FromStream(New IO.MemoryStream(DirectCast(dr("QRCODE"), Byte())))
                        Else
                            PBQRCODE.Image = Nothing
                        End If
                        txtinwords.Text = Convert.ToString(dr("INWORDS"))
                        txtremarks.Text = Convert.ToString(dr("REMARKS"))
                        If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True

                        'Item Grid
                        GRIDINVOICE.Rows.Add(dr("SRNO"), Convert.ToString(dr("GREYQUALITY")), dr("HSNCODE"), Val(dr("LOTNO")), dr("SHADE"), dr("BALENO"), Val(dr("PCS")), Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RATE")), "0.000"), dr("PER"), Format(Val(dr("AMOUNT")), "0.00"), dr("MILLNAME"), dr("NARRATION").ToString, dr("GRIDDONE").ToString, dr("OUTPCS"), dr("OUTMTRS"), dr("FROMNO"), dr("FROMSRNO"))
                        If Convert.ToBoolean(dr("GRIDDONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = False
                            GRIDINVOICE.Rows(GRIDINVOICE.RowCount - 1).DefaultCellStyle.BackColor = Drawing.Color.Yellow
                        End If

                    Next
                    GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1

                    'CHARGES GRID
                    Dim OBJCMN As New ClsCommon
                    Dim dttable As DataTable = OBJCMN.search("  ISNULL(SALERETURN_CHGS.SALRET_gridsrno,0) AS GRIDSRNO, ISNULL(LEDGERS.Acc_cmpname, '') AS CHARGES, ISNULL(SALERETURN_CHGS.SALRET_PER, 0) AS PER, ISNULL(SALERETURN_CHGS.SALRET_AMT, 0) AS AMOUNT, ISNULL(TAXMASTER.tax_id, 0) AS TAXID", "", "   LEDGERS RIGHT OUTER JOIN SALERETURN_CHGS LEFT OUTER JOIN TAXMASTER ON SALERETURN_CHGS.SALRET_yearid = TAXMASTER.tax_yearid AND SALERETURN_CHGS.SALRET_TAXID = TAXMASTER.tax_id ON LEDGERS.Acc_yearid = SALERETURN_CHGS.SALRET_yearid AND LEDGERS.Acc_id = SALERETURN_CHGS.SALRET_CHARGESID RIGHT OUTER JOIN REGISTERMASTER INNER JOIN SALERETURN ON REGISTERMASTER.register_id = SALERETURN.SALRET_REGISTERID AND REGISTERMASTER.register_yearid = SALERETURN.SALRET_YEARID ON SALERETURN_CHGS.SALRET_no = SALERETURN.SALRET_NO AND SALERETURN_CHGS.SALRET_REGISTERID = SALERETURN.SALRET_REGISTERID AND SALERETURN_CHGS.SALRET_yearid = SALERETURN.SALRET_YEARID", " AND REGISTERMASTER.REGISTER_NAME = '" & TEMPREGNAME & "' AND REGISTERMASTER.REGISTER_TYPE='SALERETURN' AND SALERETURN_CHGS.SALRET_NO = " & TEMPSALERETNO & "   AND SALERETURN_CHGS.SALRET_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            GRIDCHGS.Rows.Add(DTR("GRIDSRNO"), DTR("CHARGES"), DTR("PER"), DTR("AMOUNT"), DTR("TAXID"))
                        Next
                    End If

                    'UPLOAD GRID
                    Dim OBJ As New ClsCommon
                    Dim dt2 As DataTable = OBJ.search(" SALERETURN_UPLOAD.SALRET_UPSRNO AS GRIDSRNO, SALERETURN_UPLOAD.SALRET_UPREMARKS AS REMARKS, SALERETURN_UPLOAD.SALRET_UPNAME AS NAME, SALERETURN_UPLOAD.SALRET_IMGPATH AS IMGPATH", "", " SALERETURN INNER JOIN SALERETURN_UPLOAD ON SALERETURN.SALRET_YEARID = SALERETURN_UPLOAD.SALRET_YEARID AND SALERETURN.SALRET_REGISTERID = SALERETURN_UPLOAD.SALRET_REGISTERID AND SALERETURN.SALRET_NO = SALERETURN_UPLOAD.SALRET_NO LEFT OUTER JOIN REGISTERMASTER ON SALERETURN_UPLOAD.SALRET_REGISTERID = REGISTERMASTER.register_id AND SALERETURN_UPLOAD.SALRET_YEARID = REGISTERMASTER.register_yearid ", " AND SALERETURN.SALRET_NO = " & TEMPSALERETNO & " AND REGISTER_NAME ='" & TEMPREGNAME & "' AND REGISTERMASTER.REGISTER_TYPE='SALERETURN' AND SALERETURN.SALRET_YEARID = " & YearId)
                    If dt2.Rows.Count > 0 Then
                        For Each DTR As DataRow In dt2.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If

                    Dim OBJCOMMON As New ClsCommon
                    dttable = OBJCOMMON.search(" ISNULL(FORMTYPE.FORM_NAME, '') AS FORMNAME", "", "  SALERETURN_FORMTYPE INNER JOIN REGISTERMASTER ON SALERETURN_FORMTYPE.SALRET_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN FORMTYPE ON SALERETURN_FORMTYPE.SALRET_FORMID = FORMTYPE.FORM_ID ", " AND REGISTERMASTER.REGISTER_NAME = '" & TEMPREGNAME & "' AND SALERETURN_FORMTYPE.SALRET_NO = " & TEMPSALERETNO & " AND SALERETURN_FORMTYPE.SALRET_YEARID= " & YearId)
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
                    dtID = clscommon.search(" register_abbr, register_initials, register_id ", "", " RegisterMaster ", " and register_name ='" & CMBREGISTER.Text.Trim & "' and register_type = 'SALERETURN' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
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
                'CMDSELECTGDN.Enabled = False
                CMBREGISTER.Enabled = False
                SALERETDATE.Focus()
                SALERETDATE.SelectAll()
                TOTAL()
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

    Private Sub TXTMTRS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress, TXTRATE.KeyPress, TXTCGSTAMT.KeyPress, TXTSGSTAMT.KeyPress, TXTIGSTAMT.KeyPress
        Try
            numdotkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
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
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" AndAlso CMBTYPE.Text.Trim = "GREY" Then FILLGREY(CMBQUALITY, False) Else fillQUALITY(CMBQUALITY, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNARRATION_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNARRATION.Validating
        Try
            If CMBQUALITY.Text <> "" And Val(TXTPCS.Text) > 0 And Val(TXTMTRS.Text) > 0 Then fillgrid() Else MsgBox("Enter Proper Details !")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Enter(sender As Object, e As EventArgs) Handles CMBSHADE.Enter
        Try
            If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()

        GRIDINVOICE.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDINVOICE.Rows.Add(Val(txtsrno.Text.Trim), CMBQUALITY.Text.Trim, TXTHSNCODE.Text.Trim, Val(TXTLOTNO.Text.Trim), CMBSHADE.Text.Trim, TXTBALENO.Text.Trim, Format(Val(TXTPCS.Text.Trim), "0"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), CMBPER.Text.Trim, Format(Val(TXTAMOUNT.Text.Trim), "0.00"), CMBMILLNAME.Text.Trim, TXTNARRATION.Text.Trim, 0, 0, 0, 0, 0)
            getsrno(GRIDINVOICE)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDINVOICE.Item(GSRNO.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDINVOICE.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDINVOICE.Item(GHSNCODE.Index, TEMPROW).Value = TXTHSNCODE.Text.Trim
            GRIDINVOICE.Item(GLOTNO.Index, TEMPROW).Value = TXTLOTNO.Text.Trim
            GRIDINVOICE.Item(GSHADE.Index, TEMPROW).Value = CMBSHADE.Text.Trim
            GRIDINVOICE.Item(GBALENO.Index, TEMPROW).Value = TXTBALENO.Text.Trim
            GRIDINVOICE.Item(GPCS.Index, TEMPROW).Value = Format(Val(TXTPCS.Text.Trim), "0")
            GRIDINVOICE.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDINVOICE.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.000")
            GRIDINVOICE.Item(GPER.Index, TEMPROW).Value = CMBPER.Text.Trim
            GRIDINVOICE.Item(GAMT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GMILLNAME.Index, TEMPROW).Value = CMBMILLNAME.Text.Trim
            GRIDINVOICE.Item(GNARRATION.Index, TEMPROW).Value = TXTNARRATION.Text.Trim
            GRIDDOUBLECLICK = False
        End If
        TOTAL()
        GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1

        txtsrno.Clear()
        CMBQUALITY.Text = ""
        TXTHSNCODE.Clear()
        TXTLOTNO.Clear()
        CMBSHADE.Text = ""
        TXTBALENO.Clear()
        TXTPCS.Clear()
        TXTMTRS.Clear()
        TXTRATE.Clear()
        CMBPER.Text = ""
        TXTAMOUNT.Clear()
        CMBMILLNAME.Text = ""
        TXTNARRATION.Clear()

        If GRIDINVOICE.RowCount > 0 Then txtsrno.Text = Val(GRIDINVOICE.Rows(GRIDINVOICE.RowCount - 1).Cells(0).Value) + 1 Else txtsrno.Text = 1
        CMBQUALITY.Focus()

    End Sub

    Private Sub GRIDINVOICE_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDINVOICE.CellDoubleClick
        If e.RowIndex >= 0 And GRIDINVOICE.Item(GSRNO.Index, e.RowIndex).Value <> Nothing Then

            'If Convert.ToBoolean(GRIDINVOICE.Rows(e.RowIndex).Cells(GDONE.Index).Value) = True Then
            '    MsgBox("Item Locked", MsgBoxStyle.Critical)
            '    Exit Sub
            'End If

            'If Val(GRIDINVOICE.Rows(e.RowIndex).Cells(GFROMNO.Index).Value) > 0 Then
            '    MsgBox("Cannot Update, Line Fetched from Challan, You can only change the Rate and Narration", MsgBoxStyle.Critical)
            '    Exit Sub
            'End If

            GRIDDOUBLECLICK = True
            txtsrno.Text = GRIDINVOICE.Item(GSRNO.Index, e.RowIndex).Value.ToString
            CMBQUALITY.Text = GRIDINVOICE.Item(GQUALITY.Index, e.RowIndex).Value.ToString
            TXTHSNCODE.Text = GRIDINVOICE.Item(GHSNCODE.Index, e.RowIndex).Value.ToString
            TXTLOTNO.Text = GRIDINVOICE.Item(GLOTNO.Index, e.RowIndex).Value.ToString
            CMBSHADE.Text = GRIDINVOICE.Item(GSHADE.Index, e.RowIndex).Value.ToString
            TXTBALENO.Text = GRIDINVOICE.Item(GBALENO.Index, e.RowIndex).Value.ToString
            TXTPCS.Text = Val(GRIDINVOICE.Item(GPCS.Index, e.RowIndex).Value)
            TXTMTRS.Text = Val(GRIDINVOICE.Item(GMTRS.Index, e.RowIndex).Value)
            TXTRATE.Text = Val(GRIDINVOICE.Item(GRATE.Index, e.RowIndex).Value)
            CMBPER.Text = GRIDINVOICE.Item(GPER.Index, e.RowIndex).Value.ToString
            TXTAMOUNT.Text = Val(GRIDINVOICE.Item(GAMT.Index, e.RowIndex).Value)
            CMBMILLNAME.Text = GRIDINVOICE.Item(GMILLNAME.Index, e.RowIndex).Value.ToString
            TXTNARRATION.Text = GRIDINVOICE.Item(GNARRATION.Index, e.RowIndex).Value.ToString

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
                TOTAL()
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

    Private Sub CMBQUALITY_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Validated
        Try
            If CMBQUALITY.Text.Trim <> "" And Convert.ToDateTime(SALERETDATE.Text).Date >= "01/07/2017" Then
                TXTHSNCODE.Clear()
                TXTCGSTPER.Clear()
                TXTCGSTAMT.Clear()
                TXTSGSTPER.Clear()
                TXTSGSTAMT.Clear()
                TXTIGSTPER.Clear()
                TXTIGSTAMT.Clear()

                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                If CMBTYPE.Text = "GREY" Then DT = OBJCMN.search("   ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER,ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER ", "", "  GREYQUALITYMASTER LEFT OUTER JOIN HSNMASTER ON GREYQUALITYMASTER.GREY_HSNCODEID = HSNMASTER.HSN_ID ", " AND GREYQUALITYMASTER.GREY_NAME= '" & CMBQUALITY.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC") Else DT = OBJCMN.search("   ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER,ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER ", "", "  QUALITYMASTER LEFT OUTER JOIN HSNMASTER ON QUALITYMASTER.QUALITY_HSNCODEID = HSNMASTER.HSN_ID ", " AND QUALITYMASTER.QUALITY_NAME= '" & CMBQUALITY.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
                If DT.Rows.Count > 0 Then
                    TXTHSNCODE.Text = DT.Rows(0).Item("HSNCODE")
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
                CALC()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" AndAlso CMBTYPE.Text.Trim = "GREY" Then GREYVALIDATE(CMBQUALITY, e, Me) Else QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim IntResult As Integer

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(CMBTYPE.Text.Trim)
            alParaval.Add(Val(TXTSALERETNO.Text.Trim))
            alParaval.Add(CMBREGISTER.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)

            alParaval.Add(Format(Convert.ToDateTime(SALERETDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBAGENT.Text.Trim)

            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(CMBDYEINGNAME.Text.Trim)
            alParaval.Add(TXTDONO.Text.Trim)
            If DTDODATE.Text = "__/__/____" Then alParaval.Add("") Else alParaval.Add(Format(Convert.ToDateTime(DTDODATE.Text).Date, "dd/MM/yyyy"))

            alParaval.Add(TXTFOLD.Text.Trim)
            alParaval.Add(Val(TXTORDERDISC.Text.Trim))

            alParaval.Add(Val(TXTSHORTAGE.Text.Trim))

            If DTRETURNDATE.Text = "__/__/____" Then alParaval.Add("") Else alParaval.Add(Format(Convert.ToDateTime(DTRETURNDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(Val(TXTINVTAKA.Text.Trim))
            alParaval.Add(Val(TXTINVMTRS.Text.Trim))
            alParaval.Add(Val(TXTNETTTAKA.Text.Trim))
            alParaval.Add(Val(TXTNETTMTRS.Text.Trim))

            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(CHALLANDATE.Text).Date, "MM/dd/yyyy"))


            alParaval.Add(TXTINVNO.Text.Trim)
            alParaval.Add(TXTINVREGNAME.Text.Trim)
            alParaval.Add(INVDATE.Value.Date)
            alParaval.Add(TXTINVTYPE.Text.Trim)
            alParaval.Add(TXTINVINITIALS.Text.Trim)
            alParaval.Add(TXTINVPRINTINITIALS.Text.Trim)

            alParaval.Add(CMBGODOWN.Text.Trim)
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


            alParaval.Add(Val(TXTCGSTPER.Text.Trim))
            alParaval.Add(Val(TXTCGSTAMT.Text.Trim))
            alParaval.Add(Val(TXTSGSTPER.Text.Trim))
            alParaval.Add(Val(TXTSGSTAMT.Text.Trim))
            alParaval.Add(Val(TXTIGSTPER.Text.Trim))
            alParaval.Add(Val(TXTIGSTAMT.Text.Trim))

            alParaval.Add(txtinwords.Text)

            alParaval.Add(Val(txtbillamt.Text.Trim))
            alParaval.Add(Val(TXTTOTALTAXAMT.Text.Trim))
            alParaval.Add(Val(TXTTOTALOTHERCHGSAMT.Text.Trim))
            alParaval.Add(Val(TXTCHARGES.Text.Trim))
            alParaval.Add(Val(TXTSUBTOTAL.Text.Trim))
            alParaval.Add(Val(txtroundoff.Text.Trim))
            alParaval.Add(Val(txtgrandtotal.Text.Trim))

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
            Dim SHADE As String = ""
            Dim BALENO As String = ""
            Dim PCS As String = ""
            Dim MTRS As String = ""
            Dim RATE As String = ""         'value of RATE
            Dim PER As String = ""
            Dim AMT As String = ""         'value of AMT
            Dim MILLNAME As String = ""
            Dim NARRATION As String = ""
            Dim DONE As String = ""
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDINVOICE.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(GSRNO.Index).Value
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        HSNCODE = row.Cells(GHSNCODE.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value
                        SHADE = row.Cells(GSHADE.Index).Value

                        BALENO = row.Cells(GBALENO.Index).Value
                        PCS = Val(row.Cells(GPCS.Index).Value)
                        MTRS = Val(row.Cells(GMTRS.Index).Value)

                        RATE = Format(Val(row.Cells(GRATE.Index).Value), "0.000")
                        PER = row.Cells(GPER.Index).Value.ToString
                        AMT = Val(row.Cells(GAMT.Index).Value)
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        If row.Cells(GNARRATION.Index).Value <> Nothing Then NARRATION = row.Cells(GNARRATION.Index).Value.ToString Else NARRATION = ""
                        If row.Cells(GDONE.Index).Value = True Then
                            DONE = 1
                        Else
                            DONE = 0
                        End If
                        OUTPCS = row.Cells(GOUTPCS.Index).Value
                        OUTMTRS = row.Cells(GOUTMTRS.Index).Value
                        FROMNO = row.Cells(GFROMNO.Index).Value
                        FROMSRNO = row.Cells(GFROMSRNO.Index).Value

                    Else

                        gridsrno = gridsrno & "|" & row.Cells(GSRNO.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        HSNCODE = HSNCODE & "|" & row.Cells(GHSNCODE.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        SHADE = SHADE & "|" & row.Cells(GSHADE.Index).Value.ToString
                        BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString
                        PCS = PCS & "|" & row.Cells(GPCS.Index).Value
                        MTRS = MTRS & "|" & row.Cells(GMTRS.Index).Value
                        RATE = RATE & "|" & Format(Val(row.Cells(GRATE.Index).Value), "0.000")
                        PER = PER & "|" & row.Cells(GPER.Index).Value.ToString
                        AMT = AMT & "|" & Val(row.Cells(GAMT.Index).Value)
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        If row.Cells(GNARRATION.Index).Value <> Nothing Then NARRATION = NARRATION & "|" & row.Cells(GNARRATION.Index).Value.ToString Else NARRATION = NARRATION & "|" & ""
                        If row.Cells(GDONE.Index).Value = True Then
                            DONE = DONE & "|" & "1"
                        Else
                            DONE = DONE & "|" & "0"
                        End If
                        OUTPCS = OUTPCS & "|" & row.Cells(GOUTPCS.Index).Value
                        OUTMTRS = OUTMTRS & "|" & row.Cells(GOUTMTRS.Index).Value
                        FROMNO = FROMNO & "|" & row.Cells(GFROMNO.Index).Value
                        FROMSRNO = FROMSRNO & "|" & row.Cells(GFROMSRNO.Index).Value


                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(QUALITY)
            alParaval.Add(HSNCODE)
            alParaval.Add(LOTNO)
            alParaval.Add(SHADE)
            alParaval.Add(BALENO)
            alParaval.Add(PCS)
            alParaval.Add(MTRS)
            alParaval.Add(RATE)
            alParaval.Add(PER)

            alParaval.Add(AMT)
            alParaval.Add(MILLNAME)
            alParaval.Add(NARRATION)
            alParaval.Add(DONE)
            alParaval.Add(OUTPCS)
            alParaval.Add(OUTMTRS)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)


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


            Dim OBJINV As New ClsSaleReturn()
            OBJINV.alParaval = alParaval

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = OBJINV.SAVE()
                TEMPSALERETNO = DT.Rows(0).Item(0)
                MessageBox.Show("Details Added")
                PRINTREPORT(DT.Rows(0).Item(0))
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPSALERETNO)
                IntResult = OBJINV.UPDATE()
                MessageBox.Show("Details Updated")
                PRINTREPORT(TEMPSALERETNO)
                edit = False
            End If
            'clear()
            If gridupload.RowCount > 0 Then SAVEUPLOAD()

            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            SALERETDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub PRINTREPORT(ByVal INVOICENO As Integer)
        Try
            If MsgBox("Wish to Print Sale Return?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim OBJINVOICE As New SaleReturnDesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.FRMSTRING = "SALERETURN"
                OBJINVOICE.WHERECLAUSE = "{SALERETURN.SALRET_NO}=" & Val(INVOICENO) & " and {REGISTERMASTER.REGISTER_NAME} = '" & CMBREGISTER.Text.Trim & "' AND {SALERETURN.SALRET_yearid}=" & YearId
                OBJINVOICE.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If CMBREGISTER.Text.Trim.Length = 0 Then
            EP.SetError(CMBREGISTER, "Enter Register Name")
            bln = False
        End If

        If Val(TXTSALERETNO.Text.Trim) = 0 Then
            EP.SetError(TXTSALERETNO, "Enter Invoice No")
            bln = False
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, " Please Fill Company Name ")
            bln = False
        End If

        If CMBGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBGODOWN, " Please Select Godown ")
            bln = False
        End If

        If GRIDINVOICE.RowCount = 0 Then
            EP.SetError(CMBNAME, "Enter Bill Details")
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
        If TXTSALERETNO.Text <> "" And CMBNAME.Text.Trim <> "" And edit = False Then
            DT = OBJCMN.search(" ISNULL(SALERETURN.SALRET_NO, '') AS SALRETNO, REGISTERMASTER.register_name AS REGNAME ", "", " SALERETURN INNER JOIN REGISTERMASTER ON SALERETURN.SALRET_REGISTERID = REGISTERMASTER.register_id  AND SALERETURN.SALRET_YEARID = REGISTERMASTER.register_yearid ", "  AND SALRET_NO = " & Val(TXTSALERETNO.Text.Trim) & " AND REGISTERMASTER.REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "'  AND SALERETURN.SALRET_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                EP.SetError(TXTSALERETNO, "Sale Return No Already Exist")
                bln = False
            End If
        End If

        For Each row As DataGridViewRow In GRIDINVOICE.Rows
            If Val(row.Cells(GAMT.Index).Value) = 0 Then
                EP.SetError(CMBNAME, "Amt Cannot be 0")
                bln = False
            End If
        Next

        If Val(txtgrandtotal.Text.Trim) = 0 Then
            EP.SetError(txtgrandtotal, "Amt Cannot be 0")
            bln = False
        End If


        'If lbllocked.Visible = True Then
        '    EP.SetError(lbllocked, "Rec/Return Made , Delete Rec/Return First")
        '    bln = False
        'End If


        'IF INVOICENO IS NOT BLANK THEN CHECK THAT FIGURES CANNOT BE GREATER THEN BALANCEAMT
        If Val(TXTINVNO.Text.Trim) > 0 Then
            Dim BALANCE As Double = 0
            If TXTINVTYPE.Text.Trim = "INVOICE" Then
                DT = OBJCMN.search("INVOICE_BALANCE AS INVBAL", "", "INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID", " AND INVOICE_NO = " & Val(TXTINVNO.Text.Trim) & " AND REGISTER_NAME = '" & TXTINVREGNAME.Text.Trim & "' AND INVOICE_YEARID = " & YearId)
            Else
                DT = OBJCMN.search("BILL_BALANCE AS INVBAL", "", "OPENINGBILL INNER JOIN REGISTERMASTER ON BILL_REGISTERID = REGISTER_ID", " AND BILL_INITIALS = '" & TXTINVINITIALS.Text.Trim & "' AND REGISTER_NAME = '" & TXTINVREGNAME.Text.Trim & "' AND BILL_YEARID = " & YearId)
            End If
            BALANCE = Val(DT.Rows(0).Item("INVBAL"))
            If edit = True Then
                Dim DT1 As DataTable = OBJCMN.search("SALRET_GRANDTOTAL AS RETTOTAL", "", "SALERETURN", " AND SALRET_NO = " & Val(TEMPSALERETNO) & " AND SALRET_YEARID = " & YearId)
                BALANCE += Val(DT1.Rows(0).Item("RETTOTAL"))
            End If
            If Val(txtgrandtotal.Text.Trim) > Val(BALANCE) Then
                EP.SetError(TXTSUBTOTAL, "Amount Greater then Balance Amt, only " & Val(BALANCE) & " can be Used")
                bln = False
            End If

        End If

        If CMBTYPE.Text.Trim = "YARN" Then
            If TXTDONO.Text.Trim = "" Then
                EP.SetError(TXTDONO, " Please Enter DO No")
                bln = False
            End If
            If DTDODATE.Text = "__/__/____" Then
                EP.SetError(DTDODATE, " Please Enter DO Date")
                bln = False
            End If
        End If


        If SALERETDATE.Text = "__/__/____" Then
            EP.SetError(SALERETDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(SALERETDATE.Text) Then
                EP.SetError(SALERETDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If


        If Val(TXTINVNO.Text.Trim) = 0 Then
            EP.SetError(TXTINVNO, "Please Enter Invoice No.")
            bln = False
        End If

        Return bln
    End Function

    Sub TOTAL()
        Try
            LBLTOTALPCS.Text = "0"
            LBLTOTALMTRS.Text = "0.0"
            lbltotalamt.Text = "0.0"

            'TXTGRAMT.Text = 0.0
            'TXTSHORTAGEAMT.Text = 0.0
            txtbillamt.Text = 0.0
            TXTCHARGES.Text = 0.0
            TXTSUBTOTAL.Text = 0
            txtroundoff.Text = 0
            txtgrandtotal.Text = 0

            TXTNETTTAKA.Text = 0.0
            TXTNETTMTRS.Text = 0.0

            If GRIDINVOICE.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDINVOICE.Rows
                    If row.Cells(GPER.Index).EditedFormattedValue = "Mtrs" Then
                        row.Cells(GAMT.Index).Value = Format((Val(row.Cells(GMTRS.Index).EditedFormattedValue)) * Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        'TXTGRAMT.Text = Format(Val(TXTGRMTRS.Text.Trim) * Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        'TXTSHORTAGEAMT.Text = Format(Val(TXTGRSHORTAGE.Text.Trim) * Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                    ElseIf Val(row.Cells(GPER.Index).EditedFormattedValue) = "5" Then
                        row.Cells(GAMT.Index).Value = Format(((row.Cells(GMTRS.Index).EditedFormattedValue / 5) * row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                    Else
                        row.Cells(GAMT.Index).Value = Format(Val(row.Cells(GPCS.Index).EditedFormattedValue) * Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        'TXTGRAMT.Text = Format(Val(TXTGRPCS.Text.Trim) * Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                        'TXTSHORTAGEAMT.Text = Format(Val(TXTGRSHORTAGE.Text.Trim) * Val(row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                    End If
                    If Val(row.Cells(GPCS.Index).Value) > 0 Then LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(row.Cells(GPCS.Index).EditedFormattedValue), "0")
                    If Val(row.Cells(GMTRS.Index).Value) > 0 Then LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(row.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GAMT.Index).Value) > 0 Then lbltotalamt.Text = Format(Val(lbltotalamt.Text) + Val(row.Cells(GAMT.Index).EditedFormattedValue), "0.00")
                Next
            End If

            TXTNETTTAKA.Text = Val(TXTINVTAKA.Text.Trim) - Val(LBLTOTALPCS.Text.Trim)
            TXTNETTMTRS.Text = Format(Val(TXTINVMTRS.Text.Trim) - Val(LBLTOTALMTRS.Text.Trim) - Val(TXTSHORTAGE.Text.Trim), "0.00")
            txtbillamt.Text = Format(Val(lbltotalamt.Text.Trim), "0.00")

            'If GRIDCHGS.RowCount > 0 Then
            '    For Each row As DataGridViewRow In GRIDCHGS.Rows
            '        If row.Cells(EPER.Index).Value > 0 Then row.Cells(EAMT.Index).Value = Format((Val(row.Cells(EPER.Index).Value) * Val(txtbillamt.Text.Trim)) / 100, "0.00")
            '        TXTCHARGES.Text = Format(Val(TXTCHARGES.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
            '        If Val(row.Cells(ETAXID.Index).Value) > 0 Then TXTTOTALTAXAMT.Text = Format(Val(TXTTOTALTAXAMT.Text) + Val(row.Cells(EAMT.Index).Value), "0.00") Else TXTTOTALOTHERCHGSAMT.Text = Format(Val(TXTTOTALOTHERCHGSAMT.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
            '    Next
            'End If

            If GRIDCHGS.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDCHGS.Rows
                    'IF PERCENT IS > 0 THEN GETAUTO CHARGES
                    Dim OBJCMN As New ClsCommon
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

            If CHKMANUAL.CheckState = CheckState.Unchecked Then
                TXTCGSTAMT.Text = Format((Val(TXTSUBTOTAL.Text.Trim) * Val(TXTCGSTPER.Text.Trim)) / 100, "0.00")
                TXTSGSTAMT.Text = Format((Val(TXTSUBTOTAL.Text.Trim) * Val(TXTSGSTPER.Text.Trim)) / 100, "0.00")
                TXTIGSTAMT.Text = Format((Val(TXTSUBTOTAL.Text.Trim) * Val(TXTIGSTPER.Text.Trim)) / 100, "0.00")
            End If

            txtgrandtotal.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT.Text.Trim) + Val(TXTSGSTAMT.Text.Trim) + Val(TXTIGSTAMT.Text.Trim), "0")


            ' txtgrandtotal.Text = Format(Val(TXTSUBTOTAL.Text), "0")
            ' txtroundoff.Text = Format(Val(txtgrandtotal.Text) - Val(TXTSUBTOTAL.Text), "0.00")
            ' txtgrandtotal.Text = Format(Val(txtgrandtotal.Text), "0.000")
            txtroundoff.Text = Format(Val(txtgrandtotal.Text) - (Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT.Text.Trim) + Val(TXTSGSTAMT.Text.Trim) + Val(TXTIGSTAMT.Text.Trim)), "0.00")

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

                'If lbllocked.Visible = True Then
                '    MsgBox("Rec / Return Made, Delete Rec First", MsgBoxStyle.Critical)
                '    Exit Sub
                'End If


                'CHECKING WHETHER CFORM OR ANY OTHER FORM HAS BEEN RECD OR NOT, IF RECD THEN LOCK IT, CHECK IN CFORMVIEW WITH THIS INVOICENO
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("FORMNO", "", " CFORMVIEW ", " AND BILL = " & TEMPSALERETNO & " AND REGTYPE = '" & TEMPREGNAME & "' AND TYPE = 'SALERETURN' AND FORMNO <> '' AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    MsgBox("Form Recd, Delete Form First", MsgBoxStyle.Critical)
                    Exit Sub
                End If


                If MsgBox("Delete Invoice ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTSALERETNO.Text.Trim)
                    alParaval.Add(TEMPREGNAME)
                    alParaval.Add(YearId)

                    Dim clspo As New ClsSaleReturn()
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
            TEMPREGNAME = CMBREGISTER.Text.Trim
            TEMPSALERETNO = Val(TXTSALERETNO.Text) - 1
            If TEMPSALERETNO > 0 Then
                edit = True
                SALERETURN_Load(sender, e)
            Else
                clear()
                edit = False
            End If

            If GRIDINVOICE.RowCount = 0 And TEMPSALERETNO > 1 Then
                TXTSALERETNO.Text = TEMPSALERETNO
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
            TEMPSALERETNO = Val(TXTSALERETNO.Text) + 1
            TEMPREGNAME = CMBREGISTER.Text.Trim
            getmax_SALRET_no()
            Dim MAXNO As Integer = TXTSALERETNO.Text.Trim
            clear()
            If Val(TXTSALERETNO.Text) - 1 >= TEMPSALERETNO Then
                edit = True
                SALERETURN_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If GRIDINVOICE.RowCount = 0 And TEMPSALERETNO < MAXNO Then
                TXTSALERETNO.Text = TEMPSALERETNO
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

            Dim objINVDTLS As New SaleReturnDetails
            objINVDTLS.MdiParent = MDIMain
            objINVDTLS.Show()
            objINVDTLS.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            namevalidate(CMBNAME, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors' ", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        cmdOK_Click(sender, e)
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If edit = True Then PRINTREPORT(TEMPSALERETNO)
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
                TEMPSALERETNO = Val(tstxtbillno.Text)
                TEMPREGNAME = CMBREGISTER.Text.Trim
                If TEMPSALERETNO > 0 Then
                    edit = True
                    SALERETURN_Load(sender, e)
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

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBREGISTER.Enter
        Try
            If CMBREGISTER.Text.Trim = "" Then fillregister(CMBREGISTER, " and register_type = 'SALERETURN'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'SALERETURN' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                CMBREGISTER.Text = dt.Rows(0).Item(0).ToString
            End If
            getmax_SALRET_no()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBREGISTER.Validating
        Try
            If CMBREGISTER.Text.Trim.Length > 0 And edit = False Then
                clear()
                CMBREGISTER.Text = UCase(CMBREGISTER.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_abbr, register_initials, register_id", "", " RegisterMaster", " and register_name ='" & CMBREGISTER.Text.Trim & "' and register_type = 'SALERETURN' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    saleregabbr = dt.Rows(0).Item(0).ToString
                    salereginitial = dt.Rows(0).Item(1).ToString
                    saleregid = dt.Rows(0).Item(2)
                    getmax_SALRET_no()
                    CMBREGISTER.Enabled = False
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

    Private Sub cmbname_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
        Try
            Dim OBJCMN As New ClsCommon
            If CMBNAME.Text.Trim <> "" Then
                'GET REGISTER , AGENCT AND TRANS
                Dim DT As DataTable = OBJCMN.search("ISNULL(LEDGERS_1.ACC_CMPNAME,'') AS TRANSNAME,ISNULL(LEDGERS_2.ACC_CMPNAME,'') AS AGENTNAME, ISNULL(REGISTER_NAME,'') AS REGISTERNAME, ISNULL(LEDGERS.ACC_CRDAYS,0) AS CRDAYS,ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(CAST(STATEMASTER.state_remark AS VARCHAR(20)), '') AS STATECODE ", "", "   LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid AND LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_1.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_1.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_1.Acc_yearid LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_2.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_2.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_2.Acc_yearid LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.Acc_cmpid = REGISTERMASTER.register_cmpid AND LEDGERS.Acc_locationid = REGISTERMASTER.register_locationid AND LEDGERS.Acc_yearid = RegisterMaster.register_yearid And LEDGERS.ACC_REGISTERID = RegisterMaster.register_id ", " and LEDGERS.acc_cmpname = '" & CMBNAME.Text.Trim & "' and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' and LEDGERS.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then
                    'cmbtrans.Text = DT.Rows(0).Item("TRANSNAME")
                    CMBAGENT.Text = DT.Rows(0).Item("AGENTNAME")
                    TXTSTATECODE.Text = DT.Rows(0).Item("STATECODE")
                    TXTGSTIN.Text = DT.Rows(0).Item("GSTIN")
                    'TXTCRDAYS.Text = Val(DT.Rows(0).Item("CRDAYS"))
                    'If Val(TXTCRDAYS.Text.Trim) > 0 Then DUEDATE.Text = DateAdd(DateInterval.Day, Val(TXTCRDAYS.Text.Trim), Convert.ToDateTime(INVOICEDATE.Text).Date)

                    If DT.Rows(0).Item("REGISTERNAME") <> CMBREGISTER.Text.Trim And DT.Rows(0).Item("REGISTERNAME") <> "" Then
                        Dim TEMPMSG As Integer = MsgBox("Register is Different Change to Default?", MsgBoxStyle.YesNo)
                        If TEMPMSG = vbYes Then
                            CMBREGISTER.Text = DT.Rows(0).Item("REGISTERNAME")
                            getmax_SALRET_no()
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
        If CMBREGISTER.Text <> "" Then
            DTTABLE = getmax(" isnull(max(SALRET_no),0) + 1 ", " SALERETURN INNER JOIN REGISTERMASTER ON SALRET_REGISTERID = REGISTER_ID AND SALRET_CMPID = REGISTER_CMPID AND SALRET_LOCATIONID = REGISTER_LOCATIONID AND SALRET_YEARID = REGISTER_YEARID ", " and REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' AND SALRET_cmpid=" & CmpId & " and SALRET_yearid=" & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTSALERETNO.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Sub calchgs()
        Try
            If Val(TXTCHGSPER.Text) <> 0 Then TXTCHGSAMT.Text = Format((Val(txtbillamt.Text) * Val(TXTCHGSPER.Text)) / 100, "0.00")
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
        TOTAL()

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
        'OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpg;*.png"
        'OpenFileDialog1.ShowDialog()
        'TXTIMGPATH.Text = OpenFileDialog1.FileName
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
            Dim OBJPO As New ClsSaleReturn

            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPSALERETNO)
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
                TOTAL()
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

    Private Sub cmbname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors' AND ACC_SUBTYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
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

            Case GRATE.Index
                Dim dDebit As Decimal
                Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                If bValid Then
                    If GRIDINVOICE.CurrentCell.Value = Nothing Then GRIDINVOICE.CurrentCell.Value = "0.00"
                    GRIDINVOICE.CurrentCell.Value = Convert.ToDecimal(GRIDINVOICE.Item(colNum, e.RowIndex).Value)
                    '' everything is good
                    TOTAL()
                Else
                    MessageBox.Show("Invalid Number Entered")
                    e.Cancel = True
                    Exit Sub
                End If
            Case GPER.Index
                TOTAL()
        End Select

    End Sub

    Private Sub TXTSALERETNO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTSALERETNO.KeyPress
        numkeypress(e, TXTSALERETNO, Me)
    End Sub

    Private Sub TXTSALERETNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTSALERETNO.Validating
        Try
            If Val(TXTSALERETNO.Text.Trim) <> 0 And CMBREGISTER.Text.Trim <> "" And edit = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(SALERETURN.SALRET_NO,0)  AS SALRETNO", "", " SALERETURN INNER JOIN REGISTERMASTER ON SALRET_REGISTERID = REGISTER_ID  AND SALRET_YEARID = REGISTER_YEARID ", "  AND SALERETURN.SALRET_NO=" & TXTSALERETNO.Text.Trim & " AND REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' AND SALERETURN.SALRET_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Sale Return No Already Exist")
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

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALERETDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles SALERETDATE.GotFocus
        SALERETDATE.SelectAll()
    End Sub

    Private Sub SALERETDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SALERETDATE.Validating
        Try
            If SALERETDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(SALERETDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
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

    Private Sub cmbtrans_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then namevalidate(cmbtrans, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'", "Sundry Creditors", "TRANSPORT")
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

    Private Sub CMBGODOWN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim = "" Then
                Dim WHERECLAUSE As String = ""
                If CMBTYPE.Text.Trim = "GREY" Then WHERECLAUSE = " AND GODOWN_ISOUR='TRUE'" Else WHERECLAUSE = ""
                If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, edit, WHERECLAUSE)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGODOWN.KeyDown
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

    Private Sub CMBGODOWN_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then
                Dim WHERECLAUSE As String = ""
                If CMBTYPE.Text.Trim = "GREY" Then WHERECLAUSE = " AND GODOWN_ISOUR='TRUE'" Else WHERECLAUSE = ""
                If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me, WHERECLAUSE)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTYPE_Validated(sender As Object, e As EventArgs) Handles CMBTYPE.Validated
        Try
            CMBQUALITY.Text = ""

            CMBTYPE.Enabled = False
            If CMBTYPE.Text = "YARN" Then
                fillQUALITY(CMBQUALITY, edit)
                CMBGODOWN.Text = ""
                fillGODOWN(CMBGODOWN, False, "")
            Else
                FILLGREY(CMBQUALITY, False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTRET_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTINVOICE.Click
        Try
            If CMBNAME.Text.Trim = "" Then
                MsgBox("Please Select Party name First", MsgBoxStyle.Critical)
                CMBNAME.Focus()
                Exit Sub
            End If

            Dim DTRET As New DataTable
            Dim OBJSELECTRET As New SelectInvoiceForReturn
            OBJSELECTRET.PARTYNAME = CMBNAME.Text.Trim
            OBJSELECTRET.ShowDialog()
            DTRET = OBJSELECTRET.DT

            If DTRET.Rows.Count > 0 Then

                Dim objclspreq As New ClsCommon()
                Dim DT As New DataTable
                If DTRET.Rows(0).Item("INVOICETYPE") = "INVOICE" Then
                    If CMBTYPE.Text.Trim = "GREY" Then DT = objclspreq.search(" ISNULL(INVOICEMASTER.INVOICE_NO, 0) AS INVNO, ISNULL(INVOICEMASTER.INVOICE_DATE, GETDATE()) AS INVDATE, ISNULL(INVOICEMASTER.INVOICE_CHALLANNO, '') AS GDNNO, ISNULL(INVOICEMASTER.INVOICE_CHALLANDATE, GETDATE()) AS GDNDATE, GREYQUALITYMASTER.GREY_NAME AS GREYQUALITY, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(INVOICEMASTER_DESC.INVOICE_PCS, 0) AS PCS, ISNULL(INVOICEMASTER_DESC.INVOICE_MTRS, 0) AS MTRS, ISNULL(INVOICEMASTER_DESC.INVOICE_RATE, 0) AS RATE, ISNULL(INVOICEMASTER_DESC.INVOICE_PER, '') AS PER, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(INVOICEMASTER.INVOICE_TOTALPCS, 0) AS TOTALPCS, ISNULL(INVOICEMASTER.INVOICE_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER, ISNULL(REGISTERMASTER.register_name, '') AS INVREGNAME, ISNULL(INVOICEMASTER_DESC.INVOICE_LOTNO, 0) AS LOTNO, ISNULL(INVOICEMASTER_DESC.INVOICE_BALENO, '') AS BALENO , ISNULL(INVOICEMASTER_DESC.INVOICE_AMOUNT, 0) AS AMOUNT,REGISTERMASTER.register_id AS REGID, INVOICEMASTER_DESC.INVOICE_FROMNO AS FROMNO, INVOICEMASTER_DESC.INVOICE_FROMSRNO AS FROMSRNO, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME ,ISNULL(DYEING.Acc_cmpname, '') AS DYEING, ISNULL(INVOICE_FOLD,'') AS FOLD, ISNULL(INVOICE_ORDERDISC,0) AS ORDERDISC ", "", " INVOICEMASTER_DESC INNER JOIN INVOICEMASTER ON INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN HSNMASTER ON INVOICEMASTER_DESC.INVOICE_HSNCODEID = HSNMASTER.HSN_ID INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id INNER JOIN GREYQUALITYMASTER ON INVOICEMASTER_DESC.INVOICE_QUALITYID = GREYQUALITYMASTER.GREY_ID   LEFT OUTER JOIN LEDGERS AS DYEING ON INVOICEMASTER.INVOICE_DELIVERYID = DYEING.Acc_id ", " AND INVOICEMASTER.INVOICE_TYPE = 'GREY' AND INVOICEMASTER.INVOICE_NO =" & Val(DTRET.Rows(0).Item("INVNO")) & "  AND INVOICEMASTER.INVOICE_REGISTERID =" & Val(DTRET.Rows(0).Item("REGID")) & "  AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVNO") Else DT = objclspreq.search(" ISNULL(INVOICEMASTER.INVOICE_NO, 0) AS INVNO, ISNULL(INVOICEMASTER.INVOICE_DATE, GETDATE()) AS INVDATE, ISNULL(INVOICEMASTER.INVOICE_CHALLANNO, '') AS GDNNO, ISNULL(INVOICEMASTER.INVOICE_CHALLANDATE, GETDATE()) AS GDNDATE, QUALITYMASTER.QUALITY_NAME AS GREYQUALITY, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(INVOICEMASTER_DESC.INVOICE_PCS, 0) AS PCS, ISNULL(INVOICEMASTER_DESC.INVOICE_MTRS, 0) AS MTRS, ISNULL(INVOICEMASTER_DESC.INVOICE_RATE, 0) AS RATE, ISNULL(INVOICEMASTER_DESC.INVOICE_PER, '') AS PER, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(INVOICEMASTER.INVOICE_TOTALPCS, 0) AS TOTALPCS, ISNULL(INVOICEMASTER.INVOICE_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER, ISNULL(REGISTERMASTER.register_name, '') AS INVREGNAME, ISNULL(INVOICEMASTER_DESC.INVOICE_LOTNO, 0) AS LOTNO, ISNULL(INVOICEMASTER_DESC.INVOICE_BALENO, '') AS BALENO , ISNULL(INVOICEMASTER_DESC.INVOICE_AMOUNT, 0) AS AMOUNT,REGISTERMASTER.register_id AS REGID, INVOICEMASTER_DESC.INVOICE_FROMNO AS FROMNO, INVOICEMASTER_DESC.INVOICE_FROMSRNO AS FROMSRNO, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME ,ISNULL(DYEING.Acc_cmpname, '') AS DYEING, '' as FOLD, 0 AS ORDERDISC ", "", " INVOICEMASTER_DESC INNER JOIN INVOICEMASTER ON INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN HSNMASTER ON INVOICEMASTER_DESC.INVOICE_HSNCODEID = HSNMASTER.HSN_ID INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id INNER JOIN QUALITYMASTER ON INVOICEMASTER_DESC.INVOICE_QUALITYID = QUALITYMASTER.QUALITY_ID   LEFT OUTER JOIN LEDGERS AS DYEING ON INVOICEMASTER.INVOICE_DELIVERYID = DYEING.Acc_id ", " AND INVOICEMASTER.INVOICE_TYPE = 'YARN' AND INVOICEMASTER.INVOICE_NO =" & Val(DTRET.Rows(0).Item("INVNO")) & "  AND INVOICEMASTER.INVOICE_REGISTERID =" & Val(DTRET.Rows(0).Item("REGID")) & "  AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVNO")
                Else
                    DT = objclspreq.search(" ISNULL(OPENINGBILL.BILL_NO, 0) AS INVNO, ISNULL(OPENINGBILL.BILL_DATE, GETDATE()) AS INVDATE, 0 AS GDNNO, ISNULL(OPENINGBILL.BILL_DATE, GETDATE()) AS GDNDATE, '' AS GREYQUALITY, '' AS HSNCODE,  0 AS PCS, 0 AS MTRS, 0 AS RATE, 'Mtrs' AS PER, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, 0 AS TOTALPCS, 0 AS TOTALMTRS, 0 AS CGSTPER, 0 AS SGSTPER, 0 AS IGSTPER, '' AS BALENO,0 AS LOTNO,0 AS AMOUNT, ISNULL(REGISTERMASTER.register_name, '') AS INVREGNAME, 0 AS FROMNO, 0 AS FROMSRNO, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME,'' AS DYEING, '' AS FOLD, 0 AS ORDERDISC", "", " OPENINGBILL INNER JOIN LEDGERS ON OPENINGBILL.BILL_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON OPENINGBILL.BILL_AGENTID = AGENTLEDGERS.Acc_id  INNER JOIN REGISTERMASTER ON OPENINGBILL.BILL_REGISTERID = REGISTERMASTER.register_id ", "  AND OPENINGBILL.BILL_NO =" & Val(DTRET.Rows(0).Item("INVNO")) & "  AND OPENINGBILL.BILL_REGISTERID =" & Val(DTRET.Rows(0).Item("REGID")) & "  AND OPENINGBILL.BILL_YEARID = " & YearId & " ORDER BY INVNO")
                End If
                TXTINVNO.Text = DT.Rows(0).Item("INVNO")
                TXTINVREGNAME.Text = DT.Rows(0).Item("INVREGNAME")
                TXTINVTYPE.Text = DTRET.Rows(0).Item("INVOICETYPE")
                TXTINVINITIALS.Text = DTRET.Rows(0).Item("INVINITIALS")
                TXTINVPRINTINITIALS.Text = DTRET.Rows(0).Item("PRINTINITIALS")

                INVDATE.Value = DT.Rows(0).Item("INVDATE")
                TXTCHALLANNO.Text = DT.Rows(0).Item("GDNNO")
                CHALLANDATE.Text = DT.Rows(0).Item("GDNDATE")
                CMBAGENT.Text = DT.Rows(0).Item("AGENTNAME")
                CMBDYEINGNAME.Text = DT.Rows(0).Item("DYEING")
                TXTLOTNO.Text = DT.Rows(0).Item("LOTNO")
                TXTINVTAKA.Text = Val(DT.Rows(0).Item("TOTALPCS"))
                TXTINVMTRS.Text = Val(DT.Rows(0).Item("TOTALMTRS"))

                TXTFOLD.Text = DT.Rows(0).Item("FOLD")
                TXTORDERDISC.Text = Val(DT.Rows(0).Item("ORDERDISC"))


                If TXTINVTYPE.Text.Trim = "INVOICE" Then
                    Dim SNO As Integer = 0
                    For Each DTROWPS As DataRow In DT.Rows
                        If edit = False Then
                            GRIDINVOICE.Rows.Add(0, DTROWPS("GREYQUALITY"), DTROWPS("HSNCODE"), DTROWPS("LOTNO"), "", DTROWPS("BALENO"), Val(DTROWPS("PCS")), Format(Val(DTROWPS("MTRS")), "0.00"), Format(Val(DTROWPS("RATE")), "0.000"), DTROWPS("PER"), Format(Val(DTROWPS("AMOUNT")), "0.00"), "", "", 0, 0, 0, Val(DTROWPS("FROMNO")), Val(DTROWPS("FROMSRNO")))
                        Else
                            GRIDINVOICE.Rows.Add(0, DTROWPS("GREYQUALITY"), DTROWPS("HSNCODE"), DTROWPS("LOTNO"), "", DTROWPS("BALENO"), Val(DTROWPS("PCS")), Format(Val(DTROWPS("MTRS")), "0.00"), Format(Val(DTROWPS("RATE")), "0.000"), DTROWPS("PER"), Format(Val(DTROWPS("AMOUNT")), "0.00"), "", "", 0, 0, 0, Val(DTROWPS("FROMNO")), Val(DTROWPS("FROMSRNO")))
                        End If
                        SNO += 1

                        If DTROWPS("GREYQUALITY").ToString <> "" And Convert.ToDateTime(SALERETDATE.Text).Date >= "01/07/2017" Then
                            If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                                TXTCGSTPER.Text = Val(DTROWPS("CGSTPER"))
                                TXTSGSTPER.Text = Val(DTROWPS("SGSTPER"))
                                TXTIGSTPER.Text = 0
                            Else
                                TXTCGSTPER.Text = 0
                                TXTSGSTPER.Text = 0
                                TXTIGSTPER.Text = Val(DTROWPS("IGSTPER"))
                            End If
                        End If
                    Next
                    getsrno(GRIDINVOICE)
                    TOTAL()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHALLANNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHALLANNO.Validating
        Try
            If Convert.ToDateTime(SALERETDATE.Text).Date < "01/07/2017" Then
                If TXTCHALLANNO.Text.Trim.Length > 0 Then
                    If (edit = False) Or (edit = True And LCase(TXTCHALLANNO.Text.Trim)) Then
                        'for search
                        Dim objclscommon As New ClsCommon()
                        Dim dt As DataTable = objclscommon.search(" SALRET_challanno, LEDGERS.ACC_cmpname", "", " SALERETURN inner join LEDGERS on LEDGERS.ACC_id = SALRET_ledgerid ", " and SALRET_challanno = '" & TXTCHALLANNO.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & CMBNAME.Text.Trim & "' AND SALRET_YEARID =" & YearId)
                        If dt.Rows.Count > 0 Then
                            MsgBox("Challan No. Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                            e.Cancel = True
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

    Private Sub TXTCGSTAMT_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTCGSTAMT.Validated, TXTSGSTAMT.Validated, TXTIGSTAMT.Validated
        TOTAL()
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
            If CMBNAME.Text.Trim = "" Then Exit Sub

            If Val(TXTCGSTAMT.Text.Trim) = 0 And Val(TXTSGSTAMT.Text.Trim) = 0 And Val(TXTIGSTAMT.Text.Trim) = 0 Then Exit Sub

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
            Dim PARTYCITYNAME As String = ""
            Dim PARTYKMS As Double = 0
            Dim PARTYADD1 As String = ""
            Dim PARTYADD2 As String = ""
            Dim SHIPTOADD1 As String = ""
            Dim SHIPTOADD2 As String = ""
            Dim TRANSGSTIN As String = ""


            Dim OBJCMN As New ClsCommon
            'CMP ADDRESS DETAILS
            Dim DT As DataTable = OBJCMN.search(" ISNULL(CMP_ADD1, '') AS ADD1, ISNULL(CMP_ADD2,'') AS ADD2, ISNULL(CMP_DISPATCHFROM, '') AS DISPATCHADD ", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
            TEMPCMPADD1 = DT.Rows(0).Item("ADD1")
            TEMPCMPADD2 = DT.Rows(0).Item("ADD2")
            TEMPCMPDISPATCHADD1 = DT.Rows(0).Item("DISPATCHADD")


            'PARTY GST DETAILS 
            DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN, ISNULL(ACC_ZIPCODE,'') AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITYMASTER.CITY_ID ", " AND ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ACC_YEARID = " & YearId)
            If DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Or DT.Rows(0).Item("CITYNAME") = "" Or Val(DT.Rows(0).Item("KMS")) = 0 Then
                MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            Else
                PARTYGSTIN = DT.Rows(0).Item("GSTIN")
                SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                PARTYSTATENAME = DT.Rows(0).Item("STATENAME")
                PARTYSTATECODE = DT.Rows(0).Item("STATECODE")
                PARTYCITYNAME = DT.Rows(0).Item("CITYNAME")
                SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                PARTYPINCODE = DT.Rows(0).Item("PINCODE")
                PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                PARTYADD1 = DT.Rows(0).Item("ADD1")
                PARTYADD2 = DT.Rows(0).Item("ADD2")
            End If


            'DELIVERYAT IS NOT PRESENT IN DEBIT NOTE
            ''FETCH PINCODE / KMS / ADD1 / ADD2 OF SHIPTO IF IT IS NOT SAME AS CMBNAME
            'If CMBPACKING.Text.Trim <> "" AndAlso CMBNAME.Text.Trim <> CMBPACKING.Text.Trim Then
            '    DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN,  (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_RANGE,'') AS KOTHARIPLACE ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBPACKING.Text.Trim & "' AND ACC_YEARID = " & YearId)
            '    If DT.Rows(0).Item("PINCODE") = "" Or Val(DT.Rows(0).Item("KMS")) = 0 Then
            '        MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
            '        Exit Sub
            '    Else
            '        SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
            '        SHIPTOPINCODE = DT.Rows(0).Item("PINCODE")
            '        PARTYKMS = Val(DT.Rows(0).Item("KMS"))
            '        SHIPTOADD1 = DT.Rows(0).Item("ADD1")
            '        SHIPTOADD2 = DT.Rows(0).Item("ADD2")
            '        SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
            '        SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
            '        KOTHARIPLACE = DT.Rows(0).Item("KOTHARIPLACE")
            '    End If
            'End If


            'TRANSPORT GSTING IS NOT MANDATORY
            'FOR LOCAL TRANSPORT THERE IS NO GSTIN
            'TRANSPORT GSTIN IF TRANSPORT IS PRESENT
            'If cmbtrans.Text.Trim <> "" Then
            '    DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
            '    If DT.Rows.Count > 0 Then TRANSGSTIN = DT.Rows(0).Item("GSTIN")
            '    'FOR LOCAL TRANSPORT THERE IS NO GSTIN
            '    'If TRANSGSTIN = "" Then
            '    '    MsgBox("Enter Transport GSTIN", MsgBoxStyle.Critical)
            '    '    Exit Sub
            '    'End If
            'End If



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
            'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTSALRETNO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


            'ONCE WE REC THE TOKEN WE WILL CREATE EWAY BILL
            'IF STATUS IS FAILED THEN ERROR MESSAGE
            If TEMPSTATUS = "FAILED" Then
                MsgBox("Unable to create E-Invoice", MsgBoxStyle.Critical)
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTSALERETNO.Text.Trim) & ",'SALERETURN','" & TOKEN & "','','" & TEMPSTATUS & "','" & REQUESTEDTEXT & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
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
                Dim DTINI As DataTable = OBJCMN.search("SALRET_PRINTINITIALS AS PRINTINITIALS", "", "SALERETURN", " AND SALRET_NO = " & Val(TXTSALERETNO.Text.Trim) & " AND SALRET_YEARID = " & YearId)
                PRINTINITIALS = DTINI.Rows(0).Item("PRINTINITIALS")

                j = j & """DocDtls"": {"
                j = j & """Typ"":""CRN"","
                j = j & """No"":""" & DTINI.Rows(0).Item("PRINTINITIALS") & """" & ","
                j = j & """Dt"":""" & SALERETDATE.Text & """" & "},"


                'For WORKING ON SANDBOX
                'CMPGSTIN = "34AACCC1596Q002"
                'CMPPINCODE = "605001"
                'CMPSTATECODE = "34"


                j = j & """SellerDtls"": {"
                j = j & """Gstin"":""" & CMPGSTIN & """" & ","
                j = j & """LglNm"":""" & CmpName & """" & ","
                j = j & """TrdNm"":""" & CmpName & """" & ","
                j = j & """Addr1"":""" & TEMPCMPADD1.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & TEMPCMPADD2.Replace(vbCrLf, " ") & """" & ","
                j = j & """Loc"":""" & CMPCITYNAME & """" & ","
                j = j & """Pin"":" & CMPPINCODE & "" & ","
                j = j & """Stcd"":""" & CMPSTATECODE & """" & "},"

                If PARTYADD1 = "" Then PARTYADD1 = PARTYSTATENAME
                If PARTYADD2 = "" Then PARTYADD2 = PARTYSTATENAME

                j = j & """BuyerDtls"": {"
                j = j & """Gstin"":""" & PARTYGSTIN & """" & ","
                j = j & """LglNm"":""" & CMBNAME.Text.Trim & """" & ","
                j = j & """TrdNm"":""" & CMBNAME.Text.Trim & """" & ","
                j = j & """Pos"":""" & PARTYSTATECODE & """" & ","
                j = j & """Addr1"":""" & PARTYADD1.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & PARTYADD2.Replace(vbCrLf, " ") & """" & ","
                j = j & """Loc"":""" & PARTYCITYNAME & """" & ","
                j = j & """Pin"":" & PARTYPINCODE & "" & ","
                j = j & """Stcd"":""" & PARTYSTATECODE & """" & "},"


                j = j & """DispDtls"": {"
                j = j & """Nm"":""" & CmpName & """" & ","
                j = j & """Addr1"":""" & TEMPCMPDISPATCHADD1.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & TEMPCMPADD2.Replace(vbCrLf, " ") & """" & ","
                j = j & """Loc"":""" & CMPCITYNAME & """" & ","
                j = j & """Pin"":" & CMPPINCODE & "" & ","
                j = j & """Stcd"":""" & CMPSTATECODE & """" & "},"

                j = j & """ShipDtls"": {"
                If SHIPTOGSTIN <> "" Then j = j & """Gstin"":""" & SHIPTOGSTIN & """" & ","
                j = j & """LglNm"":""" & CMBNAME.Text.Trim & """" & ","
                j = j & """TrdNm"":""" & CMBNAME.Text.Trim & """" & ","
                If SHIPTOADD1 = "" Then j = j & """Addr1"":""" & PARTYADD1.Replace(vbCrLf, " ") & """" & "," Else j = j & """Addr1"":""" & SHIPTOADD1.Replace(vbCrLf, " ") & """" & ","
                If SHIPTOADD2 = "" Then SHIPTOADD2 = " ADDRESS2 "
                j = j & """Addr2"":""" & SHIPTOADD2 & """" & ","
                j = j & """Loc"":""" & PARTYCITYNAME & """" & ","
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
                    TEMPLINEDISC = Format(Val(TXTCHARGES.Text.Trim) / Val(LBLTOTALMTRS.Text.Trim), "0.0000")
                Else
                    If GRIDINVOICE.Rows(0).Cells(GPER.Index).Value = "Pcs" Then TEMPRATE = Format(Val(TXTCHARGES.Text.Trim) / Val(LBLTOTALPCS.Text.Trim), "0.0000") Else TEMPRATE = Format(Val(TXTCHARGES.Text.Trim) / Val(LBLTOTALMTRS.Text.Trim), "0.0000")
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
                'DT = OBJCMN.Execute_Any_String(" SELECT ISNULL(SALERETURN_DESC.SALRET_GRIDSRNO,0) AS SRNO, ISNULL(ITEMMASTER.item_name,'') AS ITEMNAME, ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGST, ISNULL(HSN_SGST,0) AS SGST, ISNULL(HSN_IGST,0) AS IGST, ISNULL(SALERETURN_DESC.SALRET_QTY,0) AS PCS, ISNULL(SALERETURN_DESC.SALRET_MTRS,0) AS MTRS, ISNULL(SALERETURN_DESC.SALRET_PER,'Mtrs') AS PER, ISNULL(SALERETURN_DESC.SALRET_RATE,0) AS RATE, ISNULL(SALERETURN_DESC.SALRET_AMT,0) AS TOTALAMT, 0 AS LINEDISC, 0 AS LINETAXABLEAMT, 0 AS LINECGSTAMT, 0 AS LINESGSTAMT, 0 AS LINEIGSTAMT, ISNULL(SALERETURN_DESC.SALRET_AMT,0) AS LINEGRIDDTOTAL, ISNULL(HSN_TYPE,'Goods') HSNTYPE FROM SALERETURN INNER JOIN SALERETURN_DESC ON SALERETURN.SALRET_NO = SALERETURN_DESC.SALRET_NO AND SALERETURN.SALRET_YEARID = SALERETURN_DESC.SALRET_YEARID INNER JOIN ITEMMASTER ON item_id = SALRET_ITEMID INNER JOIN HSNMASTER ON HSN_ID = SALRET_HSNCODEID WHERE SALERETURN.SALRET_NO = " & Val(TXTSALERETNO.Text.Trim) & " and SALERETURN.SALRET_YEARID = " & YearId & " ORDER BY SALERETURN_DESC.SALRET_GRIDSRNO", "", "")
                DT = OBJCMN.Execute_Any_String(" SELECT ISNULL(SALERETURN_DESC.SALRET_SRNO,0) AS SRNO, (CASE WHEN ISNULL(SALERETURN.SALRET_TYPE, 'GREY') = 'GREY' THEN ISNULL(GREYQUALITYMASTER.GREY_NAME, '') ELSE ISNULL(QUALITYMASTER.QUALITY_NAME, '') END) AS ITEMNAME, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGSTPER, ISNULL(HSN_SGST,0) AS SGSTPER, ISNULL(HSN_IGST,0) AS IGSTPER, ISNULL(SALERETURN_DESC.SALRET_PCS,0) AS PCS, ISNULL(SALERETURN_DESC.SALRET_MTRS,0) AS MTRS, CASE WHEN ISNULL(SALERETURN.SALRET_TYPE, 'GREY') = 'GREY' THEN 'MTRS' ELSE 'KGS' END AS PER, ISNULL(SALERETURN_DESC.SALRET_RATE,0) AS RATE, ISNULL(SALERETURN_DESC.SALRET_AMOUNT,0) AS TOTALAMT, 0 AS LINEDISC, 0 AS LINETAXABLEAMT, 0 AS LINECGSTAMT, 0 AS LINESGSTAMT, 0 AS LINEIGSTAMT, 0 AS LINEGRIDDTOTAL, ISNULL(HSN_TYPE,'Goods') HSNTYPE FROM SALERETURN INNER JOIN SALERETURN_DESC ON SALERETURN.SALRET_NO = SALERETURN_DESC.SALRET_NO AND SALERETURN.SALRET_REGISTERID = SALERETURN_DESC.SALRET_REGISTERID AND SALERETURN.SALRET_YEARID = SALERETURN_DESC.SALRET_YEARID LEFT OUTER JOIN QUALITYMASTER ON SALERETURN_DESC.SALRET_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN GREYQUALITYMASTER ON SALERETURN_DESC.SALRET_QUALITYID = GREYQUALITYMASTER.GREY_ID INNER JOIN HSNMASTER ON SALERETURN_DESC.SALRET_HSNCODEID = HSNMASTER.HSN_ID INNER JOIN REGISTERMASTER ON SALERETURN.SALRET_REGISTERID = REGISTER_ID WHERE SALERETURN.SALRET_NO = " & Val(TXTSALERETNO.Text.Trim) & " AND REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' and SALERETURN.SALRET_YEARID = " & YearId & " ORDER BY SALERETURN_DESC.SALRET_SRNO", "", "")
                Dim CURRROW As Integer = 0
                For Each DTROW As DataRow In DT.Rows

                    TEMPLINECHARGES = Format(Val(TEMPLINEDISC) * Val(DTROW("MTRS")), "0.00")
                    TEMPLINETAXABLEAMT = Format(Val(DTROW("TOTALAMT")) + Val(TEMPLINECHARGES), "0.00")
                    TEMPLINECGSTAMT = Format(Val(TXTCGSTPER.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    TEMPLINESGSTAMT = Format(Val(TXTSGSTPER.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    TEMPLINEIGSTAMT = Format(Val(TXTIGSTPER.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
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

                        If INVOICESCREENTYPE = "LINE GST" Then j = j & """TotItemVal"":" & Val(DTROW("LINEGRIDTOTAL")) & "" & "," Else j = j & """TotItemVal"":" & Val(TEMPLINEGRIDTOTALAMT) & "" & ","
                        j = j & """OrdLineRef"":"" "","
                        j = j & """OrgCntry"":""IN"","
                        j = j & """PrdSlNo"":""123"","

                        j = j & """BchDtls"": {"
                        j = j & """Nm"":""123"","
                        j = j & """Expdt"":""" & SALERETDATE.Text & """" & ","
                        j = j & """wrDt"":""" & SALERETDATE.Text & """" & "},"

                        j = j & """AttribDtls"": [{"
                        j = j & """Nm"":""" & DTROW("ITEMNAME") & """" & ","
                        j = j & """Val"":""" & Val(TEMPLINEGRIDTOTALAMT) & """" & "}]"

                    Else

                        j = j & """UnitPrice"":" & Format(Val(DTROW("RATE")) + TEMPRATE, "0.00") & "" & ","
                        If DTROW("PER") = "Pcs" Then TEMPLINETAXABLEAMT = Format(Val(Val(DTROW("RATE")) + TEMPRATE) * Val(DTROW("PCS")), "0.00") Else TEMPLINETAXABLEAMT = Format(Val(Val(DTROW("RATE")) + TEMPRATE) * Val(DTROW("MTRS")), "0.00")

                        TEMPLINECGSTAMT = Format(Val(TXTCGSTPER.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                        TEMPLINESGSTAMT = Format(Val(TXTSGSTPER.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                        TEMPLINEIGSTAMT = Format(Val(TXTIGSTPER.Text.Trim) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
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
                        j = j & """Expdt"":""" & SALERETDATE.Text & """" & ","
                        j = j & """wrDt"":""" & SALERETDATE.Text & """" & "},"

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
                j = j & """AssVal"":" & Val(TXTSUBTOTAL.Text.Trim) & "" & ","
                j = j & """CgstVal"":" & Val(TXTCGSTAMT.Text.Trim) & "" & ","
                j = j & """SgstVal"":" & Val(TXTSGSTAMT.Text.Trim) & "" & ","
                j = j & """IgstVal"":" & Val(TXTIGSTAMT.Text.Trim) & "" & ","
                j = j & """CesVal"":" & "0" & "" & ","
                j = j & """StCesVal"":" & "0" & "" & ","
                j = j & """Discount"":" & "0" & "" & ","
                j = j & """OthChrg"":" & "0" & "" & ","
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
                j = j & """Crday"":" & "0" & "" & ","
                j = j & """Paidamt"":" & "0" & "" & ","
                j = j & """Paymtdue"":" & Val(txtgrandtotal.Text.Trim) & "" & "},"


                j = j & """RefDtls"": {"
                j = j & """InvRm"":""TEST"","
                j = j & """DocPerdDtls"": {"
                j = j & """InvStDt"":""" & SALERETDATE.Text & """" & ","
                j = j & """InvEndDt"":""" & SALERETDATE.Text & """" & "},"

                j = j & """PrecDocDtls"": [{"
                j = j & """InvNo"":""" & DTINI.Rows(0).Item("PRINTINITIALS") & """" & ","
                j = j & """InvDt"":""" & SALERETDATE.Text & """" & ","
                j = j & """OthRefNo"":"" ""}],"

                j = j & """ContrDtls"": [{"
                j = j & """RecAdvRefr"":"" "","
                j = j & """RecAdvDt"":""" & SALERETDATE.Text & """" & ","
                j = j & """Tendrefr"":"" "","
                j = j & """Contrrefr"":"" "","
                j = j & """Extrefr"":"" "","
                j = j & """Projrefr"":"" "","
                j = j & """Porefr"":"" "","
                j = j & """PoRefDt"":""" & SALERETDATE.Text & """" & "}]"
                j = j & "},"




                j = j & """AddlDocDtls"": [{"
                j = j & """Url"":""https://einv-apisandbox.nic.in"","
                j = j & """Docs"":""DEBITNOTE"","
                j = j & """Info"":""DEBITNOTE""}],"

                j = j & """TransDocNo"":""   """ & ","



                j = j & """ExpDtls"": {"
                j = j & """ShipBNo"":"" "","
                j = j & """ShipBDt"":""" & SALERETDATE.Text & """" & ","
                j = j & """Port"":""INBOM1"","
                j = j & """RefClm"":""N"","
                j = j & """ForCur"":""AED"","
                j = j & """CntCode"":""AE""}"




                'If TXTVEHICLENO.Text.Trim <> "" Then
                '    j = j & ","
                '    j = j & """EwbDtls"": {"
                '    j = j & """TransId"":""" & TRANSGSTIN & """" & ","
                '    j = j & """TransName"":""" & cmbtrans.Text.Trim & """" & ","
                '    j = j & """Distance"":" & PARTYKMS & "" & ","
                '    If LRDATE.Text <> "__/__/____" Then j = j & """TransDocDt"":""" & LRDATE.Text & """" & "," Else j = j & """TransDocDt"":"""","
                '    j = j & """VehNo"":""" & TXTVEHICLENO.Text.Trim & """" & ","
                '    j = j & """VehType"":""" & "R" & """" & ","
                '    j = j & """TransMode"":""1""" & "}"
                'End If

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
                'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTSALRETNO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

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
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTSALERETNO.Text.Trim) & ",'SALERETURN','" & TOKEN & "','','FAILED','" & REQUESTEDTEXT & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

                MsgBox("Error While Generating E-Invoice, " & REQUESTEDTEXT)

                Exit Sub
            End If


            Dim IRNNO As String = ""
            Dim ACKNO As String = ""
            Dim ADATE As String = ""


            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ackno") + Len("ACKNO") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("\", STARTPOS)
            ACKNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            TXTACKNO.Text = ACKNO


            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ackdt") + Len("ACKDT") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("\", STARTPOS)
            ADATE = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            ACKDATE.Value = ADATE

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("irn") + Len("IRN") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("\", STARTPOS)
            IRNNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            TXTIRNNO.Text = IRNNO

            'WE NEED TO UPDATE THIS IRNNO IN DATABASE ALSO
            DT = OBJCMN.Execute_Any_String("UPDATE SALERETURN SET SALRET_IRNNO = '" & TXTIRNNO.Text.Trim & "', SALRET_ACKNO = '" & TXTACKNO.Text.Trim & "', SALRET_ACKDATE = '" & Format(ACKDATE.Value.Date, "MM/dd/yyyy") & "' WHERE SALRET_NO = " & Val(TXTSALERETNO.Text.Trim) & " AND SALRET_YEARID = " & YearId, "", "")

            'ADD DATA IN EINVOICEENTRY
            DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTSALERETNO.Text.Trim) & ",'SALERETURN','" & TOKEN & "','" & IRNNO & "','" & TEMPSTATUS & "', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


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
                bitmap1.Save(Application.StartupPath & "\SR" & Val(TXTSALERETNO.Text.Trim) & AccFrom.Year & ".png")
                PBQRCODE.ImageLocation = Application.StartupPath & "\SR" & Val(TXTSALERETNO.Text.Trim) & AccFrom.Year & ".png"
                PBQRCODE.Refresh()

                If PBQRCODE.Image IsNot Nothing Then
                    Dim OBJINVOICE As New ClsSaleReturn
                    Dim MS As New IO.MemoryStream
                    PBQRCODE.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    OBJINVOICE.alParaval.Add(TXTSALERETNO.Text.Trim)
                    OBJINVOICE.alParaval.Add(MS.ToArray)
                    OBJINVOICE.alParaval.Add(YearId)
                    Dim INTRES As Integer = OBJINVOICE.SAVEQRCODE()
                End If

                'DT = OBJCMN.Execute_Any_String("UPDATE SALERETURN SET SALRET_QRCODE = (SELECT * FROM OPENROWSET(BULK '" & Application.StartupPath & "\" & Val(TXTSALRETNO.Text.Trim) & AccFrom.Year & ".png',SINGLE_BLOB) AS IMG) FROM SALERETURN INNER JOIN REGISTERMASTER ON SALRET_REGISTERID = REGISTER_ID WHERE SALRET_NO = " & Val(TXTSALRETNO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND SALRET_YEARID = " & YearId, "", "")


                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTSALERETNO.Text.Trim) & ",'SALERETURN','" & TOKEN & "','" & IRNNO & "','QRCODE SUCCESS', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTSALERETNO.Text.Trim) & ",'SALERETURN','" & TOKEN & "','" & IRNNO & "','QRCODE SUCCESS1', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

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
            'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTSALRETNO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','" & TEMPSTATUS & "', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")



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
                    MsgBox("Unable to create E-Invoice", MsgBoxStyle.Critical)
                    DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTSALERETNO.Text.Trim) & ",'SALERETURN','" & TOKEN & "','','" & TEMPSTATUS & "','" & REQUESTEDTEXT & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
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
                bitmap1.Save(Application.StartupPath & "\SR" & Val(TXTSALERETNO.Text.Trim) & AccFrom.Year & ".png")
                PBQRCODE.ImageLocation = Application.StartupPath & "\SR" & Val(TXTSALERETNO.Text.Trim) & AccFrom.Year & ".png"
                PBQRCODE.Refresh()

                If PBQRCODE.Image IsNot Nothing Then
                    Dim OBJINVOICE As New ClsSaleReturn
                    Dim MS As New IO.MemoryStream
                    PBQRCODE.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    OBJINVOICE.alParaval.Add(TXTSALERETNO.Text.Trim)
                    OBJINVOICE.alParaval.Add(MS.ToArray)
                    OBJINVOICE.alParaval.Add(YearId)
                    Dim INTRES As Integer = OBJINVOICE.SAVEQRCODE()
                End If

                'DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_QRCODE = (SELECT * FROM OPENROWSET(BULK '" & Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png',SINGLE_BLOB) AS IMG) FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")

                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTSALERETNO.Text.Trim) & ",'SALERETURN','" & TOKEN & "','" & TXTIRNNO.Text.Trim & "','QRCODE SUCCESS', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTSALERETNO.Text.Trim) & ",'SALERETURN','" & TOKEN & "','" & TXTIRNNO.Text.Trim & "','QRCODE SUCCESS1', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                'cmdok_Click(sender, e)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If edit = True Then SENDWHATSAPP(TEMPSALERETNO)
            DT = OBJCMN.Execute_Any_String("UPDATE SALERETURN SET SALRET_SENDWHATSAPP = 1 WHERE SALRET_NO = " & TEMPSALERETNO & " AND SALRET_YEARID = " & YearId, "", "")
            LBLWHATSAPP.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(SALERETNO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJSO As New SaleReturnDesign
            OBJSO.MdiParent = MDIMain
            OBJSO.DIRECTPRINT = True
            OBJSO.FRMSTRING = "SALERETURN"
            OBJSO.DIRECTWHATSAPP = True
            OBJSO.PARTYNAME = CMBNAME.Text.Trim
            OBJSO.AGENTNAME = CMBAGENT.Text.Trim
            OBJSO.FORMULA = "{SALERETURN.SALRET_NO}=" & Val(SALERETNO) & " and {SALERETURN.SALRET_YEARID}=" & YearId
            OBJSO.SALERETNO = SALERETNO
            OBJSO.NOOFCOPIES = 1
            OBJSO.Show()
            OBJSO.Close()

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
            OBJWHATSAPP.AGENTNAME = CMBAGENT.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & CMBNAME.Text.Trim & "_SALERETURN_NO-" & Val(SALERETNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add(CMBNAME.Text.Trim & "SALERETURN_" & Val(SALERETNO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTDODATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTDODATE.Validating
        Try
            If DTDODATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTDODATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DODATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTDODATE.GotFocus
        DTDODATE.SelectAll()
    End Sub

    Private Sub CMBDYEINGNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBDYEINGNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub CMBDYEINGNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDYEINGNAME.Validating
        Try
            If CMBDYEINGNAME.Text.Trim <> "" Then namevalidate(CMBDYEINGNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTSHORTAGE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTSHORTAGE.KeyPress
        AMOUNTNUMDOTKYEPRESS(e, sender, Me)
    End Sub

    Private Sub CMBSHADE_Validating(sender As Object, e As CancelEventArgs) Handles CMBSHADE.Validating
        Try
            If CMBSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBSHADE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_Enter(sender As Object, e As EventArgs) Handles CMBMILLNAME.Enter
        Try
            If CMBMILLNAME.Text.Trim = "" Then fillname(CMBMILLNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBMILLNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILLNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBMILLNAME.Validating
        Try
            If CMBMILLNAME.Text.Trim <> "" Then namevalidate(CMBMILLNAME, CMBCODE, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "MILL")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class