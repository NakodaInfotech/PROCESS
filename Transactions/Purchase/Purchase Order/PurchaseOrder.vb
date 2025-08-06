
Imports System.Windows.Forms
Imports BL
Imports System.IO

Public Class PurchaseOrder

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDCHGSDOUBLECLICK As Boolean
    Dim GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPCHGSROW As Integer
    Dim TEMPUPLOADROW As Integer
    Public edit As Boolean
    Public tempono As Integer
    Dim tempMsg As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub getmax_po_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(po_no),0) + 1 ", "PURCHASEORDER", " and po_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            txtpono.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Sub clear()

        CMBTYPE.Enabled = True
        CMBTYPE.SelectedIndex = 0

        tstxtbillno.Clear()
        cmbname.Text = ""
        txtquotation.Clear()
        quotationdate.Value = Mydate
        PODATE.Text = Mydate
        TXTDELPERIOD.Clear()
        cmbtrans1.Text = ""
        CMBTRANS2.Text = ""
        CMBTRANS3.Text = ""
        TXTNOTE.Clear()

        cmbname.Enabled = True
        CMBINVNAME.Text = ""
        CMBBROKER.Text = ""
        cmdselectQuot.Enabled = True
        txtsrno.Clear()
        CMBQUALITY.Text = ""
        CMBTYPE.Text = ""
        txtbags.Clear()
        txtrate.Clear()
        txtamount.Clear()
        txtcount.Clear()
        txtwtg.Clear()
        cmbPER.Text = ""
        EP.Clear()
        CHKFORMBOX.SelectedItems.Clear()

        TXTCHGSSRNO.Clear()
        CMBCHARGES.Text = ""
        TXTCHGSPER.Clear()
        TXTCHGSAMT.Clear()
        GRIDCHGS.RowCount = 0

        CMBMILL.Text = ""

        txtbillamt.Text = 0.0
        TXTCHARGES.Text = 0.0
        TXTSUBTOTAL.Text = 0.0
        txtgrandtotal.Text = 0.0
        txtroundoff.Text = 0.0

        txtuploadsrno.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        gridupload.RowCount = 0
        txtimgpath.Clear()
        TXTNEWIMGPATH.Clear()
        TXTFILENAME.Clear()
        PBSoftCopy.ImageLocation = ""

        CHKVERIFY.CheckState = CheckState.Unchecked
        lbllocked.Visible = False
        LBLCLOSED.Visible = False
        PBlock.Visible = False
        txtremarks.Clear()

        txtadd.Clear()
        lbltotalamt.Text = "0.00"
        lbltotalbag.Text = "0.00"
        gridpo.RowCount = 0
        getmax_po_no()
        cmdselectQuot.Enabled = True
        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        txtremarks.Clear()

        If gridpo.RowCount > 0 Then
            txtsrno.Text = Val(gridpo.Rows(gridpo.RowCount - 1).Cells(0).Value) + 1
        Else
            txtsrno.Text = 1
        End If

        If GRIDCHGS.RowCount > 0 Then
            TXTCHGSSRNO.Text = Val(GRIDCHGS.Rows(GRIDCHGS.RowCount - 1).Cells(0).Value) + 1
        Else
            TXTCHGSSRNO.Text = 1
        End If


        If gridupload.RowCount > 0 Then
            txtuploadsrno.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1
        Else
            txtuploadsrno.Text = 1
        End If

        cmdselectQuot.Enabled = True

    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        edit = False
        CMBTYPE.Focus()
    End Sub

    Private Sub PurchaseOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.F5 Then     'grid focus
                gridpo.Focus()
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call TOOLPREVIOUS_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call TOOLNEXT_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.D1 Then
                TabControl1.SelectedIndex = 0
            ElseIf e.Alt = True And e.KeyCode = Keys.D2 Then
                TabControl1.SelectedIndex = 1
            ElseIf e.Alt = True And e.KeyCode = Keys.D3 Then
                TabControl1.SelectedIndex = 2
            ElseIf e.KeyCode = Windows.Forms.Keys.F3 Then
                CMBCHARGES.Focus()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try
            If cmbname.Text.Trim = "" Then fillname(cmbname, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS' OR LEDGERS.ACC_SUBTYPE = 'MILL')")
            If CMBBROKER.Text.Trim = "" Then fillname(CMBBROKER, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'")
            If CMBCHARGES.Text.Trim = "" Then fillname(CMBCHARGES, edit, " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses')")
            fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'")
            fillper(cmbPER, edit)
            fillform(CHKFORMBOX, edit)
            fillQUALITY(CMBQUALITY, edit)
            If cmbtrans1.Text.Trim = "" Then fillname(cmbtrans1, edit, "AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
            If CMBTRANS2.Text.Trim = "" Then fillname(CMBTRANS2, edit, "AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
            If CMBTRANS3.Text.Trim = "" Then fillname(CMBTRANS3, edit, "AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
            If CMBINVNAME.Text.Trim = "" Then fillname(CMBINVNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PurchaseOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'PURCHASE ORDER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
            fillcmb()
            clear()
            cmbname.Enabled = True
            cmdselectQuot.Enabled = True

            If edit = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If


                Dim objclsPO As New ClsPurchaseOrder()
                Dim dt_po As DataTable = objclsPO.selectpo(tempono, CmpId, Locationid, YearId)


                If dt_po.Rows.Count > 0 Then
                    For Each dr As DataRow In dt_po.Rows

                        'txtpono.Text = dr("PONO")
                        txtpono.Text = tempono

                        PODATE.Text = Convert.ToDateTime(dr("DATE"))

                        CMBTYPE.Text = Convert.ToString(dr("TYPE"))
                        CMBTYPE.Enabled = False
                        HIDEVIEW()
                        CMBQUALITY.Text = ""

                        cmbname.Text = Convert.ToString(dr("NAME"))
                        CMBBROKER.Text = Convert.ToString(dr("BROKER"))

                        txtquotation.Text = Convert.ToString(dr("QUOTNO"))
                        quotationdate.Value = Convert.ToDateTime(dr("QUOTDATE"))
                        TXTDELPERIOD.Text = Val(dr("DELPERIOD"))
                        CMBMILL.Text = Convert.ToString(dr("MILLNAME"))
                        CMBINVNAME.Text = Convert.ToString(dr("INVNAME"))

                        txtremarks.Text = Convert.ToString(dr("REMARKS"))
                        TXTNOTE.Text = Convert.ToString(dr("NOTE"))

                        cmbtrans1.Text = Convert.ToString(dr("TRANS1"))
                        CMBTRANS2.Text = Convert.ToString(dr("TRANS2"))
                        CMBTRANS3.Text = Convert.ToString(dr("TRANS3"))

                        lbltotalbag.Text = Val(dr("TOTALBAGS"))
                        lbltotalamt.Text = Val(dr("TOTALAMT"))

                        txtbillamt.Text = dr("BILLAMT")
                        TXTCHARGES.Text = dr("CHARGES")
                        txtroundoff.Text = dr("ROUNDOFF")
                        txtgrandtotal.Text = dr("GRANDTOTAL")

                        TXTAMTPAID.Text = dr("AMTPAID")
                        TXTEXTRAAMT.Text = dr("EXTRAAMT")
                        TXTRETURN.Text = dr("BILLRETURN")
                        TXTBAL.Text = dr("BALANCE")

                        gridpo.Rows.Add(dr("SRNO").ToString, dr("QUALITY").ToString, dr("COUNT").ToString, dr("BAGS").ToString, dr("WT").ToString, Format(Val(dr("RATE")), "0.00"), dr("PER"), Format(Val(dr("amt")), "0.00"), dr("NARRATION").ToString, dr("GRIDQUOTNO").ToString, dr("GRIDQUOTSRNO").ToString, Val(dr("OUTBAGS")), Val(dr("OUTWT")))

                        If Convert.ToBoolean(dr("VERIFY")) = True Then CHKVERIFY.CheckState = CheckState.Checked

                        If Val(dr("OUTBAGS")) > 0 Or Val(dr("OUTWT")) > 0 Then
                            gridpo.Rows(gridpo.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        If Convert.ToBoolean(dr("CLOSE")) = True Then
                            LBLCLOSED.Visible = True
                            PBlock.Visible = True
                        End If

                    Next
                    gridpo.FirstDisplayedScrollingRowIndex = gridpo.RowCount - 1

                End If
                'total()
                cmbname.Focus()
            Else
                edit = False
                clear()
                CMBTYPE.Focus()
            End If


            'CHARGES GRID
            Dim OBJCM2 As New ClsCommon
            'Dim dt2 As DataTable = OBJCM2.search(" PURCHASEMASTER_CHGS.BILL_gridsrno AS GRIDSRNO, ISNULL(LEDGERS.Acc_cmpname, '') AS CHARGES, ISNULL(PURCHASEMASTER_CHGS.BILL_PER, 0) AS PER, ISNULL(PURCHASEMASTER_CHGS.BILL_AMT, 0) AS AMT, ISNULL(TAXMASTER.TAX_ID, 0) AS TAXID ", "", " PURCHASEMASTER INNER JOIN REGISTERMASTER ON PURCHASEMASTER.BILL_REGISTERID = REGISTERMASTER.register_id AND PURCHASEMASTER.BILL_CMPID = REGISTERMASTER.register_cmpid AND PURCHASEMASTER.BILL_LOCATIONID = REGISTERMASTER.register_locationid AND PURCHASEMASTER.BILL_YEARID = REGISTERMASTER.register_yearid LEFT OUTER JOIN PURCHASEMASTER_CHGS LEFT OUTER JOIN TAXMASTER ON PURCHASEMASTER_CHGS.BILL_yearid = TAXMASTER.tax_yearid AND PURCHASEMASTER_CHGS.BILL_locationid = TAXMASTER.tax_locationid AND PURCHASEMASTER_CHGS.BILL_cmpid = TAXMASTER.tax_cmpid AND PURCHASEMASTER_CHGS.BILL_TAXID = TAXMASTER.tax_id ON PURCHASEMASTER.BILL_NO = PURCHASEMASTER_CHGS.BILL_no AND PURCHASEMASTER.BILL_REGISTERID = PURCHASEMASTER_CHGS.BILL_REGISTERID LEFT OUTER JOIN LEDGERS ON PURCHASEMASTER_CHGS.BILL_yearid = LEDGERS.Acc_yearid AND PURCHASEMASTER_CHGS.BILL_locationid = LEDGERS.Acc_locationid AND PURCHASEMASTER_CHGS.BILL_cmpid = LEDGERS.Acc_cmpid AND PURCHASEMASTER_CHGS.BILL_CHARGESID = LEDGERS.Acc_id", " AND PURCHASEMASTER_CHGS.BILL_NO = " & tempono & " AND PURCHASEMASTER_CHGS.BILL_CMPID = " & CmpId & " AND PURCHASEMASTER_CHGS.BILL_LOCATIONID = " & Locationid & " AND PURCHASEMASTER_CHGS.BILL_YEARID = " & YearId)
            Dim dt2 As DataTable = OBJCM2.search(" PURCHASEORDER_CHGS.PO_gridsrno AS GRIDSRNO, ISNULL(LEDGERS.Acc_cmpname, '') AS CHARGES, ISNULL(PURCHASEORDER_CHGS.PO_PER, 0) AS PER, ISNULL(PURCHASEORDER_CHGS.PO_AMT, 0) AS AMT, ISNULL(TAXMASTER.tax_id, 0) AS TAXID", "", " PURCHASEORDER_CHGS INNER JOIN PURCHASEORDER ON PURCHASEORDER_CHGS.PO_no = PURCHASEORDER.PO_NO AND PURCHASEORDER_CHGS.PO_cmpid = PURCHASEORDER.PO_CMPID AND PURCHASEORDER_CHGS.PO_locationid = PURCHASEORDER.PO_LOCATIONID AND PURCHASEORDER_CHGS.PO_yearid = PURCHASEORDER.PO_YEARID INNER JOIN LEDGERS ON PURCHASEORDER_CHGS.PO_CHARGESID = LEDGERS.Acc_id AND PURCHASEORDER_CHGS.PO_cmpid = LEDGERS.Acc_cmpid AND PURCHASEORDER_CHGS.PO_locationid = LEDGERS.Acc_locationid AND PURCHASEORDER_CHGS.PO_yearid = LEDGERS.Acc_yearid LEFT OUTER JOIN TAXMASTER ON PURCHASEORDER_CHGS.PO_TAXID = TAXMASTER.tax_id AND PURCHASEORDER_CHGS.PO_cmpid = TAXMASTER.tax_cmpid AND PURCHASEORDER_CHGS.PO_locationid = TAXMASTER.tax_locationid AND PURCHASEORDER_CHGS.PO_yearid = TAXMASTER.tax_yearid", " AND PURCHASEORDER_CHGS.PO_no = " & tempono & " AND PURCHASEORDER_CHGS.PO_CMPID = " & CmpId & " AND PURCHASEORDER_CHGS.PO_LOCATIONID = " & Locationid & " AND PURCHASEORDER_CHGS.PO_YEARID = " & YearId)
            If dt2.Rows.Count > 0 Then
                For Each DTR As DataRow In dt2.Rows
                    GRIDCHGS.Rows.Add(DTR("GRIDSRNO"), DTR("CHARGES"), DTR("PER"), DTR("AMT"), DTR("TAXID"))
                Next
            End If

            Dim OBJCMN As New ClsCommon
            Dim dttable As DataTable = OBJCMN.search(" PO_GRIDSRNO AS GRIDSRNO, PO_REMARKS AS REMARKS, PO_NAME AS NAME, PO_IMGPATH AS IMGPATH, PO_NEWIMGPATH AS NEWIMGPATH", "", " PURCHASEORDER_UPLOAD", " AND PO_NO = " & tempono & " AND PO_YEARID = " & YearId)
            If dttable.Rows.Count > 0 Then
                For Each DTR As DataRow In dttable.Rows
                    gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), DTR("IMGPATH"), DTR("NEWIMGPATH"))
                Next
            End If

            'Dim OBJCOMMON As New ClsCommon
            'dttable = OBJCOMMON.search(" ISNULL(FORMTYPE.FORM_NAME, '') AS FORMNAME", "", " PURCHASEORDER_FORMTYPE INNER JOIN FORMTYPE ON PURCHASEORDER_FORMTYPE.PO_FORMID = FORMTYPE.FORM_ID AND PURCHASEORDER_FORMTYPE.PO_YEARID = FORMTYPE.FORM_YEARID INNER JOIN PURCHASEORDER ON PURCHASEORDER_FORMTYPE.PO_NO = PURCHASEORDER.PO_NO AND PURCHASEORDER_FORMTYPE.PO_YEARID = PURCHASEORDER.PO_YEARID", " AND PURCHASEORDER_FORMTYPE.PO_NO = " & tempono & " AND PURCHASEORDER_FORMTYPE.PO_YEARID = " & YearId)
            'If dttable.Rows.Count > 0 Then
            '    For Each ROW As DataRow In dttable.Rows
            '        For I As Integer = 0 To CHKFORMBOX.Items.Count
            '            Dim DTR As DataRowView = CHKFORMBOX.Items(I)
            '            If ROW("FORMNAME") = DTR.Item(0) Then
            '                CHKFORMBOX.SetItemCheckState(I, CheckState.Checked)
            '            End If
            '        Next
            '    Next
            'End If

            total()

            If gridpo.RowCount > 0 Then
                txtsrno.Text = Val(gridpo.Rows(gridpo.RowCount - 1).Cells(0).Value) + 1
            Else
                txtsrno.Text = 1
            End If
            CMBTYPE.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub CMBBROKER_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBROKER.Enter
        Try
            If CMBBROKER.Text.Trim = "" Then fillname(CMBBROKER, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBROKER_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBROKER.Validating
        Try
            If CMBBROKER.Text.Trim <> "" Then namevalidate(CMBBROKER, cmbcode, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor

            Dim IntResult As Integer

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(PODATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBTYPE.Text.Trim)
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(CMBBROKER.Text.Trim)
            alParaval.Add(txtquotation.Text.Trim)
            alParaval.Add(quotationdate.Value)
            alParaval.Add(Val(TXTDELPERIOD.Text.Trim))
            alParaval.Add(CMBMILL.Text.Trim)
            alParaval.Add(CMBINVNAME.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(TXTNOTE.Text.Trim)
            alParaval.Add(cmbtrans1.Text.Trim)
            alParaval.Add(CMBTRANS2.Text.Trim)
            alParaval.Add(CMBTRANS3.Text.Trim)

            alParaval.Add(lbltotalbag.Text.Trim)
            alParaval.Add(lbltotalamt.Text.Trim)

            If lbllocked.Visible = False Then
                alParaval.Add(0)    'PO DONE
            Else
                alParaval.Add(1)    'PO DONE
            End If

            If UserName = "Admin" Then
                alParaval.Add(CHKVERIFY.Checked)    'VERIFIED
            Else
                alParaval.Add(0)    'VERIFIED
            End If

            alParaval.Add(txtbillamt.Text.Trim)
            alParaval.Add(TXTCHARGES.Text.Trim)
            alParaval.Add(txtroundoff.Text.Trim)
            alParaval.Add(txtgrandtotal.Text.Trim)

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
            Dim COUNT As String = ""
            Dim BAGS As String = ""
            Dim WT As String = ""
            Dim rate As String = ""
            Dim PER As String = ""
            Dim amount As String = ""
            Dim NARRATION As String = ""
            Dim QUOTNO As String = ""         'value of QUOTNO
            Dim QUOTgridsrno As String = ""   'value of QUOTGRIDSRNO
            Dim OUTBAGS As String = ""
            Dim OUTWT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In gridpo.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        COUNT = row.Cells(gcount.Index).Value
                        BAGS = row.Cells(gBag.Index).Value
                        WT = row.Cells(Gwt.Index).Value
                        rate = row.Cells(grate.Index).Value
                        PER = row.Cells(GPER.Index).Value
                        amount = row.Cells(gamt.Index).Value
                        NARRATION = row.Cells(GNarration.Index).Value.ToString
                        If row.Cells(gquotno.Index).Value <> Nothing Then
                            QUOTNO = row.Cells(gquotno.Index).Value
                        Else
                            QUOTNO = "0"
                        End If

                        If row.Cells(gquogridsrno.Index).Value <> Nothing Then
                            QUOTgridsrno = row.Cells(gquogridsrno.Index).Value
                        Else
                            QUOTgridsrno = 0
                        End If

                        OUTBAGS = Val(row.Cells(GOUTBAGS.Index).Value)
                        OUTWT = Val(row.Cells(GOUTWT.Index).Value)

                    Else

                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        COUNT = COUNT & "|" & row.Cells(gcount.Index).Value
                        BAGS = BAGS & "|" & row.Cells(gBag.Index).Value
                        WT = WT & "|" & row.Cells(Gwt.Index).Value
                        rate = rate & "|" & row.Cells(grate.Index).Value
                        PER = PER & "|" & row.Cells(GPER.Index).Value
                        amount = amount & "|" & row.Cells(gamt.Index).Value
                        NARRATION = NARRATION & "|" & row.Cells(GNarration.Index).Value.ToString

                        If row.Cells(gquotno.Index).Value <> Nothing Then
                            QUOTNO = QUOTNO & "|" & row.Cells(gquotno.Index).Value
                        Else
                            QUOTNO = QUOTNO & "|" & "0"
                        End If

                        If row.Cells(gquogridsrno.Index).Value <> Nothing Then
                            QUOTgridsrno = QUOTgridsrno & "|" & row.Cells(gquogridsrno.Index).Value
                        Else
                            QUOTgridsrno = QUOTgridsrno & "|" & "0"
                        End If
                        OUTBAGS = OUTBAGS & "|" & Val(row.Cells(GOUTBAGS.Index).Value)
                        OUTWT = OUTWT & "|" & Val(row.Cells(GOUTWT.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(QUALITY)
            alParaval.Add(COUNT)
            alParaval.Add(BAGS)
            alParaval.Add(WT)
            alParaval.Add(rate)
            alParaval.Add(PER)
            alParaval.Add(amount)
            alParaval.Add(NARRATION)
            alParaval.Add(QUOTNO)
            alParaval.Add(QUOTgridsrno)
            alParaval.Add(OUTBAGS)
            alParaval.Add(OUTWT)


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
                        CAMT = row.Cells(EAMT.Index).Value.ToString
                        CTAXID = Val(row.Cells(ETAXID.Index).Value)

                    Else
                        CSRNO = CSRNO & "," & row.Cells(ESRNO.Index).Value.ToString
                        CCHGS = CCHGS & "," & row.Cells(ECHARGES.Index).Value.ToString
                        CPER = CPER & "," & row.Cells(EPER.Index).Value.ToString
                        CAMT = CAMT & "," & row.Cells(EAMT.Index).Value.ToString
                        CTAXID = CTAXID & "," & Val(row.Cells(ETAXID.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(CSRNO)
            alParaval.Add(CCHGS)
            alParaval.Add(CPER)
            alParaval.Add(CAMT)
            alParaval.Add(CTAXID)



            Dim griduploadsrno As String = ""
            Dim imgpath As String = ""
            Dim uploadremarks As String = ""
            Dim name As String = ""
            Dim NEWIMGPATH As String = ""
            Dim FILENAME As String = ""

            'Saving Upload Grid
            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                If row.Cells(0).Value <> Nothing Then
                    If griduploadsrno = "" Then
                        griduploadsrno = row.Cells(0).Value.ToString
                        uploadremarks = row.Cells(1).Value.ToString
                        name = row.Cells(2).Value.ToString
                        imgpath = row.Cells(3).Value.ToString
                        NEWIMGPATH = row.Cells(GNEWIMGPATH.Index).Value.ToString

                    Else
                        griduploadsrno = griduploadsrno & "|" & row.Cells(0).Value.ToString
                        uploadremarks = uploadremarks & "|" & row.Cells(1).Value.ToString
                        name = name & "|" & row.Cells(2).Value.ToString
                        imgpath = imgpath & "|" & row.Cells(3).Value.ToString
                        NEWIMGPATH = NEWIMGPATH & "|" & row.Cells(GNEWIMGPATH.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(griduploadsrno)
            alParaval.Add(uploadremarks)
            alParaval.Add(name)
            alParaval.Add(imgpath)
            alParaval.Add(NEWIMGPATH)
            alParaval.Add(FILENAME)

            Dim objclsPurord As New ClsPurchaseOrder()
            objclsPurord.alParaval = alParaval

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim intres As Integer = objclsPurord.save()

                MessageBox.Show("Details Added")
                'PRINTREPORT(DT.Rows(0).Item(0))
            Else
                alParaval.Add(tempono)

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                IntResult = objclsPurord.Update()
                MessageBox.Show("Details Updated")
                PRINTREPORT(tempono)
                edit = False

            End If

            'COPY SCANNED DOCS FILES 
            For Each ROW As DataGridViewRow In gridupload.Rows
                If FileIO.FileSystem.DirectoryExists(Application.StartupPath & "\UPLOADDOCS") = False Then
                    FileIO.FileSystem.CreateDirectory(Application.StartupPath & "\UPLOADDOCS")
                End If
                If FileIO.FileSystem.FileExists(Application.StartupPath & "\UPLOADDOCS") = False Then
                    System.IO.File.Copy(ROW.Cells(GIMGPATH.Index).Value, ROW.Cells(GNEWIMGPATH.Index).Value, True)
                End If
            Next

            'clear()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            CMBTYPE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub PRINTREPORT(ByVal PONO As Integer)
        Try
            tempMsg = MsgBox("Wish to Print PO?", MsgBoxStyle.YesNo)
            If tempMsg = vbYes Then
                Dim OBJPO As New PODesign
                OBJPO.FRMSTRING = "PO"
                OBJPO.MdiParent = MDIMain
                OBJPO.WHERECLAUSE = "{PURCHASEORDER.po_no}=" & tempono & " and {PURCHASEORDER.po_yearid}=" & YearId
                OBJPO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If PODATE.Text = "__/__/____" Then
            EP.SetError(PODATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(PODATE.Text) Then
                EP.SetError(PODATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If ClientName <> "JASHOK" Then
            If cmbname.Text.Trim.Length = 0 Then
                EP.SetError(cmbname, " Please Fill Company Name ")
                bln = False
            End If
        End If

        If CMBMILL.Text.Trim.Length = 0 Then
            EP.SetError(CMBMILL, " Please Fill Mill Name ")
            bln = False
        End If

        If gridpo.RowCount = 0 Then
            EP.SetError(txtamount, "Enter Item Details")
            bln = False
        End If

        If CMBTYPE.Text.Trim = "" Then
            EP.SetError(CMBTYPE, "Select Order Type")
            bln = False
        End If

        If ClientName = "MASHOK" Then
            If CMBINVNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBINVNAME, " Please Fill Sale Name ")
                bln = False
            End If
        End If

        'DONE TEMPORARILY
        'If lbllocked.Visible = True Then
        '    EP.SetError(lbllocked, "Rec/Return Made , Delete Rec/Return First")
        '    bln = False
        'End If

        If LBLCLOSED.Visible = True Then
            EP.SetError(LBLCLOSED, "Purchase Order Closed")
            bln = False
        End If



        Return bln
    End Function

    Sub total()

        If gridpo.RowCount > 0 Then

            lbltotalbag.Text = "0"
            lbltotalamt.Text = "0.00"

            txtbillamt.Text = 0.0
            TXTCHARGES.Text = 0.0
            TXTSUBTOTAL.Text = 0
            txtroundoff.Text = 0
            txtgrandtotal.Text = 0

            For Each row As DataGridViewRow In gridpo.Rows
                If Val(row.Cells(gBag.Index).Value) > 0 Then lbltotalbag.Text = Format(Val(lbltotalbag.Text) + Val(row.Cells(gBag.Index).Value), "0")
                If Val(row.Cells(gamt.Index).Value) > 0 Then lbltotalamt.Text = Format(Val(lbltotalamt.Text) + Val(row.Cells(gamt.Index).Value), "0.00")
                If Val(row.Cells(gamt.Index).Value) > 0 Then txtbillamt.Text = Format(Val(txtbillamt.Text) + Val(row.Cells(gamt.Index).EditedFormattedValue), "0.00")
            Next
        Else
            lbltotalbag.Text = "0"
            lbltotalamt.Text = "0.00"
        End If

        If GRIDCHGS.RowCount > 0 Then
            For Each row As DataGridViewRow In GRIDCHGS.Rows
                TXTCHARGES.Text = Format(Val(TXTCHARGES.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
            Next
        End If

        TXTSUBTOTAL.Text = Format(Val(txtbillamt.Text) + Val(TXTCHARGES.Text.Trim), "0.00")
        txtgrandtotal.Text = Format(Val(TXTSUBTOTAL.Text), "0")
        txtroundoff.Text = Format(Val(txtgrandtotal.Text) - Val(TXTSUBTOTAL.Text), "0.00")
        txtgrandtotal.Text = Format(Val(txtgrandtotal.Text), "0.00")

    End Sub

    Private Sub txtsrno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsrno.GotFocus
        If GRIDDOUBLECLICK = False Then
            If gridpo.RowCount > 0 Then
                txtsrno.Text = Val(gridpo.Rows(gridpo.RowCount - 1).Cells(gsrno.Index).Value) + 1
            Else
                txtsrno.Text = 1
            End If
        End If
    End Sub

    Private Sub cmbQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then
                If CMBTYPE.Text.Trim = "YARN" Then
                    fillQUALITY(CMBQUALITY, edit)
                Else
                    FILLGREY(CMBQUALITY, edit, " AND GREY_TYPE = '" & CMBTYPE.Text.Trim & "'")
                End If
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
                If CMBTYPE.Text = "YARN" Then
                    Dim OBJROLLS As New SelectQuality
                    OBJROLLS.ShowDialog()
                    If OBJROLLS.TEMPNAME <> "" Then CMBQUALITY.Text = OBJROLLS.TEMPNAME
                Else
                    Dim OBJROLLS As New SelectGreyQuality
                    OBJROLLS.ShowDialog()
                    If OBJROLLS.TEMPNAME <> "" Then CMBQUALITY.Text = OBJROLLS.TEMPNAME
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
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

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub uploadgetsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            'If edit = False Then
            Dim i As Integer = 0
            For Each row As DataGridViewRow In grid.Rows
                If row.Visible = True Then
                    row.Cells(GGRIDUPLOADSRNO.Index).Value = i + 1
                    i = i + 1
                End If
            Next
            'End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()

        gridpo.Enabled = True

        If GRIDDOUBLECLICK = False Then
            gridpo.Rows.Add(Val(txtsrno.Text.Trim), CMBQUALITY.Text.Trim, Val(txtcount.Text.Trim), Val(txtbags.Text.Trim), Val(txtwtg.Text.Trim), Val(txtrate.Text.Trim), cmbPER.Text.Trim, Format(Val(txtamount.Text.Trim), "0.00"), TXTNARRATION.Text.Trim, 0, 0, 0)
            getsrno(gridpo)
        ElseIf GRIDDOUBLECLICK = True Then
            gridpo.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            gridpo.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            gridpo.Item(gcount.Index, TEMPROW).Value = txtcount.Text.Trim
            gridpo.Item(gBag.Index, TEMPROW).Value = txtbags.Text.Trim
            gridpo.Item(Gwt.Index, TEMPROW).Value = txtwtg.Text.Trim
            gridpo.Item(grate.Index, TEMPROW).Value = Val(txtrate.Text.Trim)
            gridpo.Item(GPER.Index, TEMPROW).Value = cmbPER.Text
            gridpo.Item(gamt.Index, TEMPROW).Value = Format(Val(txtamount.Text.Trim), "0.00")
            gridpo.Item(GNarration.Index, TEMPROW).Value = TXTNARRATION.Text.Trim
            GRIDDOUBLECLICK = False
        End If
        total()
        gridpo.FirstDisplayedScrollingRowIndex = gridpo.RowCount - 1

        txtsrno.Clear()
        CMBQUALITY.Text = ""
        txtcount.Clear()
        txtbags.Clear()
        txtwtg.Clear()
        txtrate.Clear()
        cmbPER.Text = ""
        txtamount.Clear()
        TXTNARRATION.Clear()
        txtsrno.Focus()

    End Sub

    Private Sub gridpo_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridpo.CellDoubleClick

        If e.RowIndex >= 0 And gridpo.Item(gsrno.Index, e.RowIndex).Value <> Nothing Then

            'DONE TEMPORARILY
            'If Val(gridpo.Rows(e.RowIndex).Cells(GOUTBAGS.Index).Value) > 0 Or Val(gridpo.Rows(e.RowIndex).Cells(GOUTWT.Index).Value) > 0 Then 'If row.Cells(16).Value <> "0" Then 
            '    MsgBox("Item Locked. First Delete from GRN")
            '    Exit Sub
            'End If

            GRIDDOUBLECLICK = True
            txtsrno.Text = gridpo.Item(gsrno.Index, e.RowIndex).Value.ToString
            CMBQUALITY.Text = gridpo.Item(GQUALITY.Index, e.RowIndex).Value.ToString
            txtcount.Text = gridpo.Item(gcount.Index, e.RowIndex).Value.ToString
            txtbags.Text = gridpo.Item(gBag.Index, e.RowIndex).Value.ToString
            txtwtg.Text = gridpo.Item(Gwt.Index, e.RowIndex).Value.ToString
            txtrate.Text = gridpo.Item(grate.Index, e.RowIndex).Value.ToString
            cmbPER.Text = gridpo.Item(GPER.Index, e.RowIndex).Value.ToString
            txtamount.Text = gridpo.Item(gamt.Index, e.RowIndex).Value.ToString
            TXTNARRATION.Text = gridpo.Item(GNarration.Index, e.RowIndex).Value.ToString

            TEMPROW = e.RowIndex
            txtsrno.Focus()
        End If
    End Sub

    Sub fillgridscan()
        Try
            If GRIDUPLOADDOUBLECLICK = False Then

                gridupload.Rows.Add(Val(txtuploadsrno.Text.Trim), txtuploadremarks.Text.Trim, txtuploadname.Text.Trim, txtimgpath.Text.Trim, TXTNEWIMGPATH.Text.Trim, TXTFILENAME.Text.Trim)
                uploadgetsrno(gridupload)

            ElseIf GRIDUPLOADDOUBLECLICK = True Then

                gridupload.Item(0, TEMPUPLOADROW).Value = txtuploadsrno.Text.Trim
                gridupload.Item(1, TEMPUPLOADROW).Value = txtuploadremarks.Text.Trim
                gridupload.Item(2, TEMPUPLOADROW).Value = txtuploadname.Text.Trim
                gridupload.Item(3, TEMPUPLOADROW).Value = txtimgpath.Text.Trim
                gridupload.Item(GNEWIMGPATH.Index, TEMPUPLOADROW).Value = TXTNEWIMGPATH.Text.Trim
                gridupload.Item(GFILENAME.Index, TEMPUPLOADROW).Value = TXTFILENAME.Text.Trim

                GRIDUPLOADDOUBLECLICK = False

            End If
            gridupload.FirstDisplayedScrollingRowIndex = gridupload.RowCount - 1
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbags.KeyPress
        numkeypress(e, txtbags, Me)
    End Sub

    Private Sub txtrate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrate.KeyPress
        numdot(e, txtrate, Me)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDELETE.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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
                    MsgBox("Unable to Delete, PO Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                tempMsg = MsgBox("Delete Purchase Order ?", MsgBoxStyle.YesNo)
                If tempMsg = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(tempono)
                    alParaval.Add(YearId)

                    Dim clspo As New ClsPurchaseOrder()
                    clspo.alParaval = alParaval
                    IntResult = clspo.Delete()
                    MsgBox("Purchase Order Deleted")
                    clear()
                    edit = False
                    CMBTYPE.Focus()
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'  AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS' OR LEDGERS.ACC_SUBTYPE = 'MILL')", "SUNDRY CREDITORS", "ACCOUNTS ", cmbtrans1.Text, CMBBROKER.Text)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPER.GotFocus
        Try
            If cmbPER.Text.Trim = "" Then fillper(cmbPER, edit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbPER.Validating
        Try
            If cmbPER.Text.Trim <> "" Then PERVALIDATE(cmbPER, e, Me)
            CALC()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    'Private Sub gridpo_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles gridpo.CellValidating
    '    ''  CODE FOR NUMERIC CHECK ONLY
    '    Dim colNum As Integer = gridpo.Columns(e.ColumnIndex).Index
    '    If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

    '    Select Case colNum

    '        Case grate.Index, gBag.Index, gamt.Index
    '            Dim dDebit As Decimal
    '            Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

    '            If bValid Then
    '                If gridpo.CurrentCell.Value = Nothing Then gridpo.CurrentCell.Value = "0.00"
    '                gridpo.CurrentCell.Value = Convert.ToDecimal(gridpo.Item(colNum, e.RowIndex).Value)
    '                '' everything is good
    '                gridpo.Rows(e.RowIndex).Cells(gamt.Index).Value = Format(Val(gridpo.Rows(e.RowIndex).Cells(grate.Index).EditedFormattedValue) * (Val(gridpo.Rows(e.RowIndex).Cells(Gwt.Index).EditedFormattedValue) / Val(gridpo.Rows(e.RowIndex).Cells(GPER.Index).EditedFormattedValue)), "0.00")
    '            Else
    '                MessageBox.Show("Invalid Number Entered")
    '                e.Cancel = True
    '            End If
    '            total()

    '    End Select
    'End Sub

    Sub EDITROW()
        Try
            If gridpo.CurrentRow.Index >= 0 And gridpo.Item(gsrno.Index, gridpo.CurrentRow.Index).Value <> Nothing Then

                If Val(gridpo.Rows(gridpo.CurrentRow.Index).Cells(GOUTBAGS.Index).Value) > 0 Or Val(gridpo.Rows(gridpo.CurrentRow.Index).Cells(GOUTWT.Index).Value) > 0 Then 'If row.Cells(16).Value <> "0" Then 
                    MsgBox("Item Locked. First Delete from GRN")
                    Exit Sub
                End If
                GRIDDOUBLECLICK = True
                txtsrno.Text = gridpo.Item(gsrno.Index, gridpo.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = gridpo.Item(GQUALITY.Index, gridpo.CurrentRow.Index).Value.ToString
                txtcount.Text = gridpo.Item(gcount.Index, gridpo.CurrentRow.Index).Value.ToString
                txtbags.Text = gridpo.Item(gBag.Index, gridpo.CurrentRow.Index).Value.ToString
                txtwtg.Text = gridpo.Item(Gwt.Index, gridpo.CurrentRow.Index).Value.ToString
                txtrate.Text = gridpo.Item(grate.Index, gridpo.CurrentRow.Index).Value.ToString
                cmbPER.Text = gridpo.Item(GPER.Index, gridpo.CurrentRow.Index).Value.ToString
                txtamount.Text = gridpo.Item(gamt.Index, gridpo.CurrentRow.Index).Value.ToString
                TXTNARRATION.Text = gridpo.Item(GNarration.Index, gridpo.CurrentRow.Index).Value.ToString

                TEMPROW = gridpo.CurrentRow.Index
                txtsrno.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridpo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridpo.KeyDown

        Try
            If e.KeyCode = Keys.Delete And gridpo.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                gridpo.Rows.RemoveAt(gridpo.CurrentRow.Index)
                total()
                getsrno(gridpo)
                total()

            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            ElseIf e.KeyCode = Keys.F12 And gridpo.RowCount > 0 Then
                If gridpo.CurrentRow.Cells(GQUALITY.Index).Value <> "" Then gridpo.Rows.Add(CloneWithValues(gridpo.CurrentRow))
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

    Private Sub cmdclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclose.Click
        Try
            If (edit = True And USEREDIT = False And USERVIEW = False) Or (edit = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If edit = True Then
                tempMsg = MsgBox("Wish to Close P.O.?", MsgBoxStyle.YesNo)
                If tempMsg = vbYes Then
                    tempMsg = MsgBox("Are you Sure?", MsgBoxStyle.YesNo)
                    If tempMsg = vbYes Then
                        If TXTNOTE.Text.Length = 0 Then
                            MsgBox("Please Fill Note For Closing PO")
                            Exit Sub
                        End If

                        Dim alParaval As New ArrayList
                        alParaval.Add(txtpono.Text)
                        alParaval.Add(TXTNOTE.Text)
                        alParaval.Add(1)
                        alParaval.Add(YearId)

                        Dim intresult As Integer
                        Dim clsobjpo As New ClsPurchaseOrder()
                        clsobjpo.alParaval = alParaval
                        intresult = clsobjpo.closepo()
                        MsgBox("P.O. Closed")
                        clear()
                        CMBTYPE.Focus()
                    End If
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                gridpo.RowCount = 0
                tempono = Val(tstxtbillno.Text)
                If tempono > 0 Then
                    edit = True
                    PurchaseOrder_Load(sender, e)
                Else
                    clear()
                    edit = False
                    CMBTYPE.Focus()
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objpodtls As New PurchaseOrderDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Try
            If edit = True Then PRINTREPORT(tempono)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TOOLPREVIOUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLPREVIOUS.Click
        Try
            gridpo.RowCount = 0
LINE1:
            tempono = Val(txtpono.Text) - 1
            If tempono > 0 Then
                edit = True
                PurchaseOrder_Load(sender, e)
            Else
                clear()
                edit = False
                CMBTYPE.Focus()
            End If
            If gridpo.RowCount = 0 And tempono > 1 Then
                txtpono.Text = tempono
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TOOLNEXT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLNEXT.Click
        Try
            gridpo.RowCount = 0
LINE1:
            tempono = Val(txtpono.Text) + 1
            getmax_po_no()
            Dim MAXNO As Integer = txtpono.Text.Trim
            clear()
            If Val(txtpono.Text) - 1 >= tempono Then
                edit = True
                PurchaseOrder_Load(sender, e)
                CMBTYPE.Focus()
            Else
                clear()
                edit = False
            End If
            If gridpo.RowCount = 0 And tempono < MAXNO Then
                txtpono.Text = tempono
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcode_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcode.Enter
        Try
            If cmbcode.Text.Trim = "" Then fillACCCODE(cmbcode, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND  LEDGERS.ACC_TYPE='ACCOUNTS'")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbcode.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then cmbcode.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
                If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcode_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcode.Validating
        Try
            If cmbcode.Text.Trim <> "" Then ACCCODEVALIDATE(cmbcode, cmbname, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "SUNDRY CREDITORS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS' OR LEDGERS.ACC_SUBTYPE = 'MILL')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then cmbcode.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
                If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            txtamount.Text = 0.0
            If Val(txtrate.Text) > 0 And Val(txtwtg.Text) > 0 And cmbPER.Text <> "" Then txtamount.Text = Format(((Val(txtwtg.Text.Trim) / Val(cmbPER.Text.Trim)) * Val(txtrate.Text.Trim)), "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtcount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcount.KeyPress
        numkeypress(e, txtcount, Me)
    End Sub

    Private Sub CMBBROKER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBBROKER.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtwtg_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwtg.KeyPress
        Try
            numdotkeypress(e, txtwtg, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then fillname(cmbname, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS' OR LEDGERS.ACC_SUBTYPE = 'MILL')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbPER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPER.Enter
        Try
            If cmbPER.Text.Trim = "" Then fillper(cmbPER, edit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTDELPERIOD_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTDELPERIOD.KeyPress
        Try
            numkeypress(e, TXTDELPERIOD, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtwtg_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtwtg.Validating
        Try
            If Val(txtwtg.Text) > 0 Then
                CALC()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtrate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtrate.Validating
        Try
            If Val(txtrate.Text) > 0 Then
                CALC()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNARRATION_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNARRATION.Validating
        Try
            If CMBQUALITY.Text <> "" And Val(txtbags.Text) > 0 Then
                fillgrid()
            Else
                MsgBox("Enter Proper Details !")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbPER_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbPER.KeyPress
        numdotkeypress(e, cmbPER, Me)
    End Sub

    Private Sub cmbtrans1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtrans1.Enter
        Try
            If cmbtrans1.Text.Trim = "" Then fillname(cmbtrans1, edit, "AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtrans1.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbtrans1.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans1_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans1.Validating
        Try
            If cmbtrans1.Text.Trim <> "" Then namevalidate(cmbtrans1, cmbcode, e, Me, TXTTRANSADD, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANS2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTRANS2.Enter
        Try
            If CMBTRANS2.Text.Trim = "" Then fillname(CMBTRANS2, edit, "AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANS3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTRANS3.Enter
        Try
            If CMBTRANS3.Text.Trim = "" Then fillname(CMBTRANS3, edit, "AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANS2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTRANS2.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbtrans1.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANS3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTRANS3.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbtrans1.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANS3_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS3.Validating
        Try
            If CMBTRANS3.Text.Trim <> "" Then namevalidate(CMBTRANS3, cmbcode, e, Me, TXTTRANSADD, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANS2_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS2.Validating
        Try
            If CMBTRANS2.Text.Trim <> "" Then namevalidate(CMBTRANS2, cmbcode, e, Me, TXTTRANSADD, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdupload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdupload.Click
        If (edit = True And USEREDIT = False And USERVIEW = False) Or (edit = False And USERADD = False) Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png;*.pdf)|*.bmp;*.jpg;*.png;*.pdf"
        OpenFileDialog1.ShowDialog()

        OpenFileDialog1.AddExtension = True
        TXTFILENAME.Text = OpenFileDialog1.SafeFileName
        txtimgpath.Text = OpenFileDialog1.FileName
        TXTNEWIMGPATH.Text = Application.StartupPath & "\UPLOADDOCS\" & txtpono.Text.Trim & txtuploadsrno.Text.Trim & TXTFILENAME.Text.Trim
        On Error Resume Next

        If txtimgpath.Text.Trim.Length <> 0 Then
            PBSoftCopy.ImageLocation = txtimgpath.Text.Trim
            PBSoftCopy.Load(txtimgpath.Text.Trim)
            txtuploadsrno.Focus()
        End If
    End Sub

    Private Sub txtuploadname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtuploadname.Validating
        Try
            If txtimgpath.Text.Trim <> "" And txtuploadname.Text.Trim <> "" Then
                fillgridscan()
                txtuploadremarks.Clear()
                txtuploadname.Clear()
                txtimgpath.Clear()
                PBSoftCopy.ImageLocation = ""
                txtuploadsrno.Focus()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridupload_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
        Try
            If gridupload.Rows(e.RowIndex).Cells(GGRIDUPLOADSRNO.Index).Value <> Nothing Then
                GRIDUPLOADDOUBLECLICK = True
                TEMPUPLOADROW = e.RowIndex
                txtuploadsrno.Text = gridupload.Rows(e.RowIndex).Cells(GGRIDUPLOADSRNO.Index).Value
                txtuploadremarks.Text = gridupload.Rows(e.RowIndex).Cells(GREMARKS.Index).Value
                txtuploadname.Text = gridupload.Rows(e.RowIndex).Cells(GNAME.Index).Value
                txtimgpath.Text = gridupload.Rows(e.RowIndex).Cells(GIMGPATH.Index).Value
                TXTNEWIMGPATH.Text = gridupload.Rows(e.RowIndex).Cells(GNEWIMGPATH.Index).Value
                TXTFILENAME.Text = gridupload.Rows(e.RowIndex).Cells(GFILENAME.Index).Value
                txtuploadsrno.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridupload.KeyDown
        If e.KeyCode = Keys.Delete And gridupload.RowCount > 0 Then
            Dim TEMPMSG As Integer = MsgBox("This Will Delete File, Wish to Proceed?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                If FileIO.FileSystem.FileExists(gridupload.Rows(gridupload.CurrentRow.Index).Cells(GNEWIMGPATH.Index).Value) Then FileIO.FileSystem.DeleteFile(gridupload.Rows(gridupload.CurrentRow.Index).Cells(GNEWIMGPATH.Index).Value)
                gridupload.Rows.RemoveAt(gridupload.CurrentRow.Index)
                uploadgetsrno(gridupload)
            End If
        End If
    End Sub

    Private Sub gridupload_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If gridupload.RowCount > 0 Then
                If Not FileIO.FileSystem.FileExists(gridupload.Rows(e.RowIndex).Cells(GNEWIMGPATH.Index).Value) Then
                    PBSoftCopy.ImageLocation = gridupload.Rows(e.RowIndex).Cells(GIMGPATH.Index).Value
                Else
                    PBSoftCopy.ImageLocation = gridupload.Rows(e.RowIndex).Cells(GNEWIMGPATH.Index).Value
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtuploadsrno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuploadsrno.GotFocus
        If GRIDUPLOADDOUBLECLICK = False Then
            If gridupload.RowCount > 0 Then
                txtuploadsrno.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(GGRIDUPLOADSRNO.Index).Value) + 1
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

    Private Sub CMBCHARGES_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCHARGES.Enter
        Try
            If CMBCHARGES.Text.Trim = "" Then fillname(CMBCHARGES, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses'  OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C')")
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
                OBJLEDGER.STRSEARCH = " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBCHARGES.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBCHARGES.Validated
        Try
            If CMBCHARGES.Text.Trim <> "" Then filltax()
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

        TXTCHGSSRNO.Clear()
        CMBCHARGES.Text = ""
        TXTCHGSPER.Clear()
        TXTCHGSAMT.Clear()
        TXTTAXID.Clear()

        If TXTCHGSPER.ReadOnly = True Then TXTCHGSPER.ReadOnly = False

        If GRIDCHGS.RowCount > 0 Then
            TXTCHGSSRNO.Text = Val(GRIDCHGS.Rows(GRIDCHGS.RowCount - 1).Cells(0).Value) + 1
        Else
            TXTCHGSSRNO.Text = 1
        End If
        TXTCHGSSRNO.Focus()
    End Sub

    Private Sub GRIDCHGS_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDCHGS.CellDoubleClick
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

    Private Sub GRIDCHGS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCHGS.KeyDown
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

    Private Sub TXTCHGSPER_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCHGSPER.KeyPress
        Try
            AMOUNTNUMDOTKYEPRESS(e, TXTCHGSPER, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSAMT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCHGSAMT.KeyPress
        Try
            AMOUNTNUMDOTKYEPRESS(e, TXTCHGSAMT, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub calchgs()
        Try
            If Val(TXTCHGSPER.Text) <> 0 Then TXTCHGSAMT.Text = Format((Val(txtbillamt.Text) * Val(TXTCHGSPER.Text)) / 100, "0.00")
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

    Private Sub CMBTYPE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTYPE.Validating
        HIDEVIEW()
    End Sub

    Sub HIDEVIEW()
        Try
            If CMBTYPE.Text.Trim <> "" Then
                If CMBTYPE.Text = "YARN" Then
                    LBLMILL.Text = "Mill Name"
                    BTNBAGS.Text = "Bags"
                    BTNWT.Text = "Wt"
                    If ClientName = "JASHOK" Then
                        LBLNAME.Visible = False
                        cmbname.Visible = False
                    End If
                    fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'")
                    fillQUALITY(CMBQUALITY, edit)
                Else
                    LBLNAME.Visible = True
                    cmbname.Visible = True
                    LBLMILL.Text = "Delivery At"
                    BTNBAGS.Text = "Pcs"
                    BTNWT.Text = "Mtrs"
                    fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
                    FILLGREY(CMBQUALITY, edit, " AND GREY_TYPE = '" & CMBTYPE.Text.Trim & "'")
                End If
                CMBTYPE.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMILL.Enter
        Try

            If CMBMILL.Text.Trim = "" Then
                If CMBTYPE.Text.Trim = "YARN" Then
                    fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'")
                Else
                    fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
                End If
            End If
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
                If CMBTYPE.Text = "YARN" Then
                    OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'"
                Else
                    OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                End If
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILL.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then
                If CMBTYPE.Text = "YARN" Then
                    namevalidate(CMBMILL, cmbcode, e, Me, txtadd, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "MILL")
                Else
                    namevalidate(CMBMILL, cmbcode, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'  AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "Sundry Creditors", "PROCESSOR")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Validated
        'THIS IS DONE BCOZ CMBMILL IS NOT ONLY MILL PROCESSEOR ARE ALSO OPENED THERE
        'If cmbname.Text <> "" And edit = False Then CMBMILL.Text = cmbname.Text
    End Sub

    Private Sub txtremarks_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtremarks.KeyDown
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

    Private Sub PODATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles PODATE.GotFocus
        PODATE.SelectAll()
    End Sub

    Private Sub PODATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PODATE.Validating
        Try
            If PODATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(PODATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseOrder_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "JASHOK" Then
                cmbname.BackColor = Color.White

                BTNAMT.Visible = False
                txtamount.Visible = False
                lbltotalamt.Visible = False

                BTNNARR.Width = BTNNARR.Width + BTNAMT.Width
                BTNNARR.Left = BTNAMT.Left
                TXTNARRATION.Width = TXTNARRATION.Width + txtamount.Width
                TXTNARRATION.Left = txtamount.Left
                gamt.Visible = False
                GNarration.Width = GNarration.Width + gamt.Width

                TXTCHGSSRNO.Visible = False
                CMBCHARGES.Visible = False
                TXTCHGSPER.Visible = False
                TXTCHGSAMT.Visible = False
                GRIDCHGS.Visible = False

                LBLAMT.Visible = False
                txtbillamt.Visible = False
                LBLCHGS.Visible = False
                TXTCHARGES.Visible = False
                LBLSUBTOTAL.Visible = False
                TXTSUBTOTAL.Visible = False
                LBLROUNDOFF.Visible = False
                txtroundoff.Visible = False

                LBLGTOTAL.Visible = False
                txtgrandtotal.Visible = False

                LBLNAME.Visible = False
                cmbname.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBINVNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBINVNAME.Enter
        Try
            If CMBINVNAME.Text.Trim = "" Then fillname(CMBINVNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbINVname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBINVNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors' AND ACC_SUBTYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then cmbcode.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbINVname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBINVNAME.Validating
        Try
            namevalidate(CMBINVNAME, cmbcode, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors' ", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

End Class