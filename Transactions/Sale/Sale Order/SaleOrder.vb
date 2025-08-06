
Imports BL

Public Class SaleOrder

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPSONO As Integer
    Public FORMULA As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub getmax_po_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(so_no),0) + 1 ", "SALEORDER", " AND so_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTSONO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Sub clear()

        CMBTYPE.Enabled = True
        HIDEVIEW()

        tstxtbillno.Clear()
        SODATE.Text = Mydate
        CMBNAME.Text = ""
        CMBNAME.Enabled = True
        CMBBROKER.Text = ""
        CMBTRANS.Text = ""
        LBLWHATSAPP.Visible = False
        TXTPONO.Clear()
        PODATE.Clear()
        TXTDELPERIOD.Clear()
        DELDATE.Clear()
        TXTDISC.Clear()
        TXTCRDAYS.Clear()

        txtsrno.Clear()
        CMBQUALITY.Text = ""
        TXTPCS.Clear()
        TXTMTRS.Clear()
        TXTRATE.Clear()
        CMBPER.Text = "Mtrs"
        CMBDELIVERYAT.Text = ""
        TXTNARRATION.Clear()
        txtamount.Clear()
        EP.Clear()

        LBLTOTALPCS.Text = 0
        LBLTOTALMTRS.Text = 0.0
        lbltotalamt.Text = 0.0

        TXTUPLOADSRNO.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        gridupload.RowCount = 0
        TXTIMGPATH.Clear()
        PBSOFTCOPY.ImageLocation = ""

        CHKVERIFY.CheckState = CheckState.Unchecked
        lbllocked.Visible = False
        LBLCLOSED.Visible = False
        PBlock.Visible = False
        txtremarks.Clear()


        GRIDSO.RowCount = 0
        getmax_po_no()
        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        txtsrno.Text = 1
        TXTUPLOADSRNO.Text = 1

    End Sub

    Private Sub SaleOrder_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1 Then       'for Delete
                TabControl1.SelectedIndex = (0)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2 Then       'for Delete
                TabControl1.SelectedIndex = (1)
            ElseIf e.KeyCode = Keys.F5 Then     'grid focus
                GRIDSO.Focus()
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call TOOLPREVIOUS_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call TOOLNEXT_Click(sender, e)
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call PrintToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Windows.Forms.Keys.F5 Then       'for grid foucs
                GRIDSO.Focus()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try
            fillname(CMBNAME, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'")
            fillname(CMBDELIVERYAT, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors') AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'PROCESSOR' OR LEDGERS.ACC_SUBTYPE = 'ACCOUNTS')")
            fillname(CMBBROKER, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'")
            fillname(CMBTRANS, EDIT, "AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
            fillform(CHKFORMBOX, EDIT)
            FILLGREY(CMBQUALITY, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, " Please Fill Name ")
            bln = False
        End If

        If GRIDSO.RowCount = 0 Then
            EP.SetError(txtamount, "Enter Item Details")
            bln = False
        End If

        'NOT USED HERE
        'Dim FORMTYPE As String = ""
        'For Each DTROW As DataRowView In CHKFORMBOX.CheckedItems
        '    FORMTYPE = DTROW.Item(0)
        'Next
        'If FORMTYPE = Nothing Then
        '    EP.SetError(CHKFORMBOX, "Pls Select Form Type")
        '    bln = False
        'End If


        'TEMP REMOVED
        'If lbllocked.Visible = True Then
        '    EP.SetError(lbllocked, "Order Raised, Delete Order First")
        '    bln = False
        'End If

        If SODATE.Text = "__/__/____" Then
            EP.SetError(SODATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(SODATE.Text) Then
                EP.SetError(SODATE, "Date not in Accounting Year")
                bln = False
            End If

            If PODATE.Text <> "__/__/____" Then
                If Convert.ToDateTime(SODATE.Text).Date < Convert.ToDateTime(PODATE.Text).Date Then
                    EP.SetError(PODATE, " Please Enter Proper PO Date")
                    bln = False
                End If
            End If

            If DELDATE.Text <> "__/__/____" Then
                If Convert.ToDateTime(SODATE.Text).Date > Convert.ToDateTime(DELDATE.Text).Date Then
                    EP.SetError(DELDATE, " Please Enter Proper Delivery Date")
                    bln = False
                End If
            End If

        End If

        If Val(TXTSONO.Text.Trim) > 0 And EDIT = False Then
            Dim OBJCMN As New ClsCommon
            Dim dttable As DataTable = OBJCMN.search("  ISNULL(so_no, 0) AS SONO ", "", " SALEORDER ", "  AND SALEORDER.so_no=" & TXTSONO.Text.Trim & " AND SALEORDER.SO_CMPID = " & CmpId & " AND SALEORDER.SO_YEARID = " & YearId)
            If dttable.Rows.Count > 0 Then
                EP.SetError(TXTSONO, "Sale Order No Already Exist")
                bln = False
            End If
        End If
        Return bln
    End Function

    Private Sub SODATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles SODATE.GotFocus
        SODATE.SelectAll()
    End Sub

    Private Sub SODATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SODATE.Validating
        Try
            If SODATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(SODATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
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

    Private Sub DELDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DELDATE.GotFocus
        DELDATE.SelectAll()
    End Sub

    Private Sub DELDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DELDATE.Validating
        Try
            If DELDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DELDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Private Sub SaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE ORDER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
            CMBTYPE.SelectedIndex = 0
            fillcmb()
            clear()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJSO As New ClsSaleOrder()
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPSONO)
                ALPARAVAL.Add(YearId)
                OBJSO.alParaval = ALPARAVAL
                Dim DT As DataTable = OBJSO.SELECTSO()
                If DT.Rows.Count > 0 Then
                    For Each dr As DataRow In DT.Rows

                        TXTSONO.Text = dr("SONO")

                        SODATE.Text = Convert.ToDateTime(dr("DATE"))

                        CMBTYPE.Text = Convert.ToString(dr("TYPE"))
                        CMBTYPE.Enabled = False
                        HIDEVIEW()
                        CMBQUALITY.Text = ""

                        CMBNAME.Text = Convert.ToString(dr("NAME"))
                        CMBBROKER.Text = Convert.ToString(dr("AGENTNAME"))
                        CMBTRANS.Text = Convert.ToString(dr("TRANSNAME"))
                        If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True
                        TXTPONO.Text = dr("PONO")
                        PODATE.Text = dr("PODATE")

                        TXTDISC.Text = Val(dr("DISC"))
                        TXTCRDAYS.Text = Val(dr("CRDAYS"))
                        DELDATE.Text = dr("DELDATE")

                        If DELDATE.Text <> "__/__/____" Then TXTDELPERIOD.Text = DateDiff(DateInterval.Day, Convert.ToDateTime(SODATE.Text).Date, Convert.ToDateTime(DELDATE.Text).Date)

                        txtremarks.Text = Convert.ToString(dr("REMARKS"))

                        If Convert.ToBoolean(dr("VERIFIED")) = True Then CHKVERIFY.CheckState = CheckState.Checked Else CHKVERIFY.CheckState = CheckState.Unchecked

                        GRIDSO.Rows.Add(dr("GRIDSRNO").ToString, dr("QUALITY").ToString, Format(Val(dr("PCS")), "0"), Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RATE")), "0.000"), dr("PER"), Format(Val(dr("AMOUNT")), "0.00"), dr("DELIVERYAT"), dr("NARR").ToString, Val(dr("OUTPCS")), Val(dr("OUTMTRS")), dr("CLOSED"))
                        If Val(dr("OUTPCS")) > 0 Or Val(dr("OUTMTRS")) > 0 Then
                            GRIDSO.Rows(GRIDSO.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        If Convert.ToBoolean(dr("CLOSED")) = True Then
                            lbllocked.Visible = True
                            LBLCLOSED.Visible = True
                            PBlock.Visible = True
                        End If

                    Next

                    'OF NOP USE HERE
                    'Dim OBJCMN As New ClsCommon
                    'DT = OBJCMN.search(" ISNULL(FORMTYPE.FORM_NAME, '') AS FORMNAME", "", "  SALEORDER_FORMTYPE INNER JOIN FORMTYPE ON SALEORDER_FORMTYPE.SO_FORMID = FORMTYPE.FORM_ID ", " AND SALEORDER_FORMTYPE.SO_NO = " & TEMPSONO & " AND SALEORDER_FORMTYPE.SO_YEARID= " & YearId)
                    'If DT.Rows.Count > 0 Then
                    '    For Each ROW As DataRow In DT.Rows
                    '        For I As Integer = 0 To CHKFORMBOX.Items.Count - 1
                    '            Dim DTR As DataRowView = CHKFORMBOX.Items(I)
                    '            If ROW("FORMNAME") = DTR.Item(0) Then
                    '                CHKFORMBOX.SetItemCheckState(I, CheckState.Checked)
                    '            End If
                    '        Next
                    '    Next
                    'End If

                    GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1

                    Dim OBJCMN As New ClsCommon
                    DT = OBJCMN.search(" SALEORDER_UPLOAD.SO_SRNO AS GRIDSRNO, SALEORDER_UPLOAD.SO_REMARKS AS REMARKS, SALEORDER_UPLOAD.SO_NAME AS NAME, SALEORDER_UPLOAD.SO_PHOTO AS IMGPATH ", "", " SALEORDER_UPLOAD ", " AND SALEORDER_UPLOAD.SO_NO = " & TEMPSONO & " AND SO_YEARID = " & YearId & " ORDER BY SALEORDER_UPLOAD.SO_SRNO")
                    If DT.Rows.Count > 0 Then
                        For Each DTR As DataRow In DT.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If

                    If GRIDSO.RowCount > 0 Then txtsrno.Text = Val(GRIDSO.Rows(GRIDSO.RowCount - 1).Cells(0).Value) + 1 Else txtsrno.Text = 1

                    total()

                End If
            Else
                EDIT = False
                clear()
                CMBNAME.Focus()
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJSO As New ClsSaleOrder

            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPSONO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJSO.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJSO.SAVEUPLOAD()
                End If
            Next


        Catch ex As Exception
            Throw ex
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

            alParaval.Add(Format(Convert.ToDateTime(SODATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBTYPE.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBBROKER.Text.Trim)
            alParaval.Add(CMBTRANS.Text.Trim)
            alParaval.Add(TXTPONO.Text.Trim)
            alParaval.Add(PODATE.Text)
            alParaval.Add(DELDATE.Text)
            alParaval.Add(Val(TXTDISC.Text.Trim))
            alParaval.Add(Val(TXTCRDAYS.Text.Trim))

            alParaval.Add(Val(LBLTOTALPCS.Text.Trim))
            alParaval.Add(Val(LBLTOTALMTRS.Text.Trim))
            alParaval.Add(Val(lbltotalamt.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)

            If UserName = "Admin" Then
                alParaval.Add(1)    'VERIFIED
            Else
                alParaval.Add(0)    'VERIFIED
            End If

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            'NOT USED HERE
            'ADDING FORMTYPE
            'Dim FORMTYPE As String = ""
            'For Each DTROW As DataRowView In CHKFORMBOX.CheckedItems
            '    If FORMTYPE = "" Then
            '        FORMTYPE = DTROW.Item(0)
            '    Else
            '        FORMTYPE = FORMTYPE & "|" & DTROW.Item(0)
            '    End If
            'Next
            'alParaval.Add(FORMTYPE)


            Dim gridsrno As String = ""
            Dim QUALITY As String = ""
            Dim PCS As String = ""
            Dim MTRS As String = ""
            Dim rate As String = ""
            Dim PER As String = ""
            Dim amount As String = ""
            Dim DELIVERY As String = ""
            Dim NARRATION As String = ""
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDSO.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        PCS = Val(row.Cells(GPCS.Index).Value)
                        MTRS = Val(row.Cells(GMTRS.Index).Value)
                        rate = Format(Val(row.Cells(grate.Index).Value), "0.000")
                        PER = row.Cells(GPER.Index).Value
                        amount = Val(row.Cells(gamt.Index).Value)
                        DELIVERY = row.Cells(GDELIVERYAT.Index).Value.ToString
                        NARRATION = row.Cells(GNarration.Index).Value.ToString
                        OUTPCS = Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = Val(row.Cells(GOUTMTRS.Index).Value)

                    Else

                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        PCS = PCS & "|" & Val(row.Cells(GPCS.Index).Value)
                        MTRS = MTRS & "|" & Val(row.Cells(GMTRS.Index).Value)
                        rate = rate & "|" & Format(Val(row.Cells(grate.Index).Value), "0.000")
                        PER = PER & "|" & row.Cells(GPER.Index).Value
                        amount = amount & "|" & Val(row.Cells(gamt.Index).Value)
                        DELIVERY = DELIVERY & "|" & row.Cells(GDELIVERYAT.Index).Value.ToString
                        NARRATION = NARRATION & "|" & row.Cells(GNarration.Index).Value.ToString
                        OUTPCS = OUTPCS & "|" & Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = OUTMTRS & "|" & Val(row.Cells(GOUTMTRS.Index).Value)
                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(QUALITY)
            alParaval.Add(PCS)
            alParaval.Add(MTRS)
            alParaval.Add(rate)
            alParaval.Add(PER)
            alParaval.Add(amount)
            alParaval.Add(DELIVERY)
            alParaval.Add(NARRATION)
            alParaval.Add(OUTPCS)
            alParaval.Add(OUTMTRS)

            Dim OBJSO As New ClsSaleOrder()
            OBJSO.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim dttable As DataTable = OBJSO.SAVE()
                TEMPSONO = dttable.Rows(0).Item(0)
                MessageBox.Show("Details Added")
                PRINTREPORT(TEMPSONO)
            Else
                alParaval.Add(TEMPSONO)

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = OBJSO.UPDATE()
                MessageBox.Show("Details Updated")
                PRINTREPORT(TEMPSONO)
                EDIT = False

            End If

            If gridupload.RowCount > 0 Then SAVEUPLOAD()

            'clear()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call TOOLNEXT_Click(sender, e)
            CMBNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub PRINTREPORT(ByVal TEMPSONO As Integer)
        Try
            If MsgBox("Wish to Print Sale Order?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim OBJINVOICE As New SODesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.FRMSTRING = "SALEORDER"
                OBJINVOICE.WHERECLAUSE = "{SALEORDER.SO_NO}=" & Val(TEMPSONO) & " AND {SALEORDER.SO_yearid}=" & YearId
                OBJINVOICE.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPSONO)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbname_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.GotFocus
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then
                namevalidate(CMBNAME, cmbcode, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'", "Sundry debtors", "ACCOUNTS")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBBROKER_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBROKER.Enter
        Try
            If CMBBROKER.Text.Trim = "" Then fillname(CMBBROKER, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBROKER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBBROKER.KeyDown
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

    Private Sub CMBBROKER_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBROKER.Validating
        Try
            If CMBBROKER.Text.Trim <> "" Then namevalidate(CMBBROKER, cmbcode, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, EDIT, "AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTRANS.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBTRANS.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If CMBTRANS.Text.Trim <> "" Then namevalidate(CMBTRANS, cmbcode, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
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
                    row.Cells(GUSRNO.Index).Value = i + 1
                    i = i + 1
                End If
            Next
            'End If
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

    Sub fillgrid()

        GRIDSO.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDSO.Rows.Add(Val(txtsrno.Text.Trim), CMBQUALITY.Text.Trim, Format(Val(TXTPCS.Text.Trim), "0"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.000"), CMBPER.Text.Trim, Format(Val(txtamount.Text.Trim), "0.00"), CMBDELIVERYAT.Text.Trim, TXTNARRATION.Text.Trim, 0, 0)
            getsrno(GRIDSO)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDSO.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDSO.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDSO.Item(GPCS.Index, TEMPROW).Value = Format(Val(TXTPCS.Text.Trim), "0")
            GRIDSO.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDSO.Item(grate.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.000")
            GRIDSO.Item(GPER.Index, TEMPROW).Value = CMBPER.Text.Trim
            GRIDSO.Item(gamt.Index, TEMPROW).Value = Format(Val(txtamount.Text.Trim), "0.00")
            GRIDSO.Item(GDELIVERYAT.Index, TEMPROW).Value = CMBDELIVERYAT.Text.Trim
            GRIDSO.Item(GNarration.Index, TEMPROW).Value = TXTNARRATION.Text.Trim
            GRIDDOUBLECLICK = False
        End If
        total()
        GRIDSO.FirstDisplayedScrollingRowIndex = GRIDSO.RowCount - 1

        txtsrno.Clear()
        CMBQUALITY.Text = ""
        TXTPCS.Clear()
        TXTMTRS.Clear()
        TXTRATE.Clear()
        CMBPER.Text = ""
        txtamount.Clear()
        CMBDELIVERYAT.Text = ""
        TXTNARRATION.Clear()

        If GRIDSO.RowCount > 0 Then txtsrno.Text = Val(GRIDSO.Rows(GRIDSO.RowCount - 1).Cells(0).Value) + 1 Else txtsrno.Text = 1
        CMBQUALITY.Focus()

    End Sub

    Private Sub GRIDSO_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDSO.CellDoubleClick
        If e.RowIndex >= 0 And GRIDSO.Item(gsrno.Index, e.RowIndex).Value <> Nothing Then

            'DONE TEMPORARILY
            'If Val(GRIDSO.Rows(e.RowIndex).Cells(GOUTPCS.Index).Value) > 0 Or Val(GRIDSO.Rows(e.RowIndex).Cells(GOUTMTRS.Index).Value) > 0 Then
            '    MsgBox("Item Locked", MsgBoxStyle.Critical)
            '    Exit Sub
            'End If

            GRIDDOUBLECLICK = True
            txtsrno.Text = GRIDSO.Item(gsrno.Index, e.RowIndex).Value.ToString
            CMBQUALITY.Text = GRIDSO.Item(GQUALITY.Index, e.RowIndex).Value.ToString
            TXTPCS.Text = Val(GRIDSO.Item(GPCS.Index, e.RowIndex).Value)
            TXTMTRS.Text = Val(GRIDSO.Item(GMTRS.Index, e.RowIndex).Value)
            TXTRATE.Text = Val(GRIDSO.Item(grate.Index, e.RowIndex).Value)
            CMBPER.Text = GRIDSO.Item(GPER.Index, e.RowIndex).Value.ToString
            txtamount.Text = Val(GRIDSO.Item(gamt.Index, e.RowIndex).Value)
            CMBDELIVERYAT.Text = GRIDSO.Item(GDELIVERYAT.Index, e.RowIndex).Value.ToString
            TXTNARRATION.Text = GRIDSO.Item(GNarration.Index, e.RowIndex).Value.ToString

            TEMPROW = e.RowIndex
            CMBQUALITY.Focus()
        End If
    End Sub

    Private Sub GRIDSO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDSO.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSO.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDSO.Rows.RemoveAt(GRIDSO.CurrentRow.Index)
                total()
                getsrno(GRIDSO)
                'total()

            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            ElseIf e.KeyCode = Keys.F12 And GRIDSO.RowCount > 0 Then
                If GRIDSO.CurrentRow.Cells(GQUALITY.Index).Value <> "" Then GRIDSO.Rows.Add(CloneWithValues(GRIDSO.CurrentRow))
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub total()

        LBLTOTALPCS.Text = "0"
        LBLTOTALMTRS.Text = "0.00"
        lbltotalamt.Text = "0.00"

        For Each row As DataGridViewRow In GRIDSO.Rows
            If Val(row.Cells(GPCS.Index).Value) <> 0 Then LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(row.Cells(GPCS.Index).Value), "0")
            If Val(row.Cells(GMTRS.Index).Value) <> 0 Then LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(row.Cells(GMTRS.Index).Value), "0.00")
            If Val(row.Cells(gamt.Index).Value) <> 0 Then lbltotalamt.Text = Format(Val(lbltotalamt.Text) + Val(row.Cells(gamt.Index).Value), "0.00")
        Next

    End Sub

    Private Sub cmddelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Dim IntResult As Integer
        Try
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    MsgBox("Unable to Delete, SO Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Delete Sale Order ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPSONO)
                    alParaval.Add(YearId)

                    Dim OBJSO As New ClsSaleOrder()
                    OBJSO.alParaval = alParaval
                    IntResult = OBJSO.Delete()
                    MsgBox("Sale Order Deleted")
                    clear()
                    EDIT = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            If GRIDSO.CurrentRow.Index >= 0 And GRIDSO.Item(gsrno.Index, GRIDSO.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                txtsrno.Text = GRIDSO.Item(gsrno.Index, GRIDSO.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDSO.Item(GQUALITY.Index, GRIDSO.CurrentRow.Index).Value.ToString
                TXTPCS.Text = Val(GRIDSO.Item(GPCS.Index, GRIDSO.CurrentRow.Index).Value)
                TXTMTRS.Text = Val(GRIDSO.Item(GMTRS.Index, GRIDSO.CurrentRow.Index).Value)
                TXTRATE.Text = Val(GRIDSO.Item(grate.Index, GRIDSO.CurrentRow.Index).Value)
                CMBPER.Text = GRIDSO.Item(GPER.Index, GRIDSO.CurrentRow.Index).Value.ToString
                txtamount.Text = Val(GRIDSO.Item(gamt.Index, GRIDSO.CurrentRow.Index).Value)
                CMBDELIVERYAT.Text = GRIDSO.Item(GDELIVERYAT.Index, GRIDSO.CurrentRow.Index).Value.ToString
                TXTNARRATION.Text = GRIDSO.Item(GNarration.Index, GRIDSO.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDSO.CurrentRow.Index
                CMBQUALITY.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function CloneWithValues(ByVal row As DataGridViewRow) As DataGridViewRow
        CloneWithValues = CType(row.Clone(), DataGridViewRow)
        For index As Int32 = 0 To row.Cells.Count - 1
            CloneWithValues.Cells(index).Value = row.Cells(index).Value
        Next
    End Function

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDSO.RowCount = 0
                TEMPSONO = Val(tstxtbillno.Text)
                If TEMPSONO > 0 Then
                    EDIT = True
                    SaleOrder_Load(sender, e)
                Else
                    clear()
                    EDIT = False
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

            Dim OBJSODTLS As New SaleOrderDetails
            OBJSODTLS.MdiParent = MDIMain
            OBJSODTLS.Show()
            OBJSODTLS.BringToFront()
            Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TOOLPREVIOUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLPREVIOUS.Click
        Try
            GRIDSO.RowCount = 0
LINE1:
            TEMPSONO = Val(TXTSONO.Text) - 1
            If TEMPSONO > 0 Then
                EDIT = True
                SaleOrder_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDSO.RowCount = 0 And TEMPSONO > 1 Then
                TXTSONO.Text = TEMPSONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TOOLNEXT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLNEXT.Click
        Try
            GRIDSO.RowCount = 0
LINE1:
            TEMPSONO = Val(TXTSONO.Text) + 1
            getmax_po_no()
            Dim MAXNO As Integer = TXTSONO.Text.Trim
            clear()
            If Val(TXTSONO.Text) - 1 >= TEMPSONO Then
                EDIT = True
                SaleOrder_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDSO.RowCount = 0 And TEMPSONO < MAXNO Then
                TXTSONO.Text = TEMPSONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            If CMBPER.Text = "Mtrs" Then txtamount.Text = Format(Val(TXTMTRS.Text.Trim) * Val(TXTRATE.Text.Trim), "0.000") Else Format(Val(TXTPCS.Text.Trim) * Val(TXTRATE.Text.Trim), "0.000")
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

    Private Sub TXTDELPERIOD_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTDELPERIOD.Validated
        Try
            If Val(TXTDELPERIOD.Text.Trim) > 0 Then DELDATE.Text = DateAdd(DateInterval.Day, Val(TXTDELPERIOD.Text.Trim), Convert.ToDateTime(SODATE.Text).Date)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPCS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPCS.KeyPress, TXTDELPERIOD.KeyPress, TXTSONO.KeyPress, TXTCRDAYS.KeyPress
        Try
            numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress, TXTRATE.KeyPress, TXTDISC.KeyPress
        Try
            numdotkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub txtrate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRATE.KeyPress
    '    numdot(e, TXTRATE, Me)
    'End Sub

    Private Sub TXTMTRS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTMTRS.Validating
        Try
            If Val(TXTMTRS.Text) > 0 Then CALC()
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

    Private Sub cmbPER_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPER.Validating
        Try
            CALC()
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

    Private Sub gridupload_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If e.RowIndex >= 0 Then PBSOFTCOPY.Image = gridupload.Rows(e.RowIndex).Cells(GUIMGPATH.Index).Value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtuploadsrno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTUPLOADSRNO.GotFocus
        If GRIDUPLOADDOUBLECLICK = False Then
            If gridupload.RowCount > 0 Then
                TXTUPLOADSRNO.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(GUSRNO.Index).Value) + 1
            Else
                TXTUPLOADSRNO.Text = 1
            End If
        End If
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

    Private Sub txtuploadname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtuploadname.Validating
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

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub TOOLDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDELETE.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclose.Click
        Try
            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If EDIT = False Then Exit Sub


            If MsgBox("Wish to Close S.O.?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If MsgBox("Are you Sure?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPSONO)
                    alParaval.Add(GRIDSO.CurrentRow.Index + 1)
                    alParaval.Add(YearId)

                    Dim intresult As Integer
                    Dim clsobjpo As New ClsSaleOrder
                    clsobjpo.alParaval = alParaval
                    intresult = clsobjpo.CLOSESO()
                    MsgBox("S.O. Closed")
                    clear()
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
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

    Private Sub TXTPCS_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTPCS.Validated
        CALC()
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then
                If CMBTYPE.Text.Trim = "YARN" Then
                    fillQUALITY(CMBQUALITY, EDIT)
                Else
                    FILLGREY(CMBQUALITY, EDIT)
                End If
            End If
        Catch ex As Exception
            Throw ex
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

    Private Sub CMBQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then
                If CMBTYPE.Text = "YARN" Then
                    QUALITYVALIDATE(CMBQUALITY, e, Me)
                Else
                    GREYVALIDATE(CMBQUALITY, e, Me)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If EDIT = True Then SendWhatsapp(TEMPSONO)
            DT = OBJCMN.Execute_Any_String("UPDATE SALEORDER SET SO_SENDWHATSAPP = 1 WHERE SO_NO = " & TEMPSONO & " AND SO_YEARID = " & YearId, "", "")
            LBLWHATSAPP.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(SONO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJSO As New SODesign
            OBJSO.MdiParent = MDIMain
            OBJSO.DIRECTPRINT = True
            OBJSO.FRMSTRING = "SALEORDER"
            OBJSO.DIRECTWHATSAPP = True
            OBJSO.PARTYNAME = CMBNAME.Text.Trim
            OBJSO.AGENTNAME = CMBBROKER.Text.Trim
            OBJSO.FORMULA = "{SALEORDER.SO_NO}=" & Val(SONO) & " and {SALEORDER.SO_yearid}=" & YearId
            OBJSO.SONO = SONO
            OBJSO.NOOFCOPIES = 1
            OBJSO.Show()
            OBJSO.Close()

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
            OBJWHATSAPP.AGENTNAME = CMBBROKER.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & CMBNAME.Text.Trim & "_SOREPORT_NO-" & Val(SONO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add(CMBNAME.Text.Trim & "SOREPORT_" & Val(SONO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDELIVERYAT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBDELIVERYAT.Enter
        Try
            If CMBDELIVERYAT.Text.Trim = "" Then fillname(CMBDELIVERYAT, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors') AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'PROCESSOR' OR LEDGERS.ACC_SUBTYPE = 'ACCOUNTS')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDELIVERYAT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDELIVERYAT.Validating
        Try
            If CMBDELIVERYAT.Text.Trim <> "" Then namevalidate(CMBDELIVERYAT, cmbcode, e, Me, txtadd, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors') AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'PROCESSOR' OR LEDGERS.ACC_SUBTYPE = 'ACCOUNTS') ", "Sundy Creditors", "ACCOUNTS", "", "", "PROCESSOR")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTSONO_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTSONO.Validating
        Try

            If Val(TXTSONO.Text.Trim) <> 0 And edit = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search("  ISNULL(so_no, 0) AS SONO ", "", " SALEORDER ", "  AND SALEORDER.so_no=" & TXTSONO.Text.Trim & " AND SALEORDER.SO_CMPID = " & CmpId & " AND SALEORDER.SO_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Sale Order No Already Exist")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub HIDEVIEW()
        Try
            If CMBTYPE.Text = "GREY" Then
                BTNTAKA.Text = "Taka"
                BTNMTRS.Text = "Mtrs"
            ElseIf CMBTYPE.Text = "FINISHED" Then
                BTNTAKA.Text = "Bales"
                BTNMTRS.Text = "Mtrs"
            Else
                BTNTAKA.Text = "Bags"
                BTNMTRS.Text = "Wt"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTYPE_Validated(sender As Object, e As EventArgs) Handles CMBTYPE.Validated
        CMBTYPE.Enabled = False
        HIDEVIEW()
    End Sub

End Class