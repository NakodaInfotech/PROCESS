
Imports BL

Public Class journal

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Public edit As Boolean
    Dim temprefno As String
    Dim jvregabbr, jvreginitial As String
    Dim jvregid As Integer
    Dim TEMPROW As Integer
    Dim tempamt, temptds As Double
    Dim temprecodate As Date    'for recodate
    Public tempjvno As Integer
    Public TEMPREGNAME As String
    Public Shared SELECTEDBILLNO As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        edit = False
        JOURNALDATE.Text = Mydate
        cmbregister.Enabled = True
        cmbregister.Focus()
    End Sub

    Sub clear()

        tstxtbillno.Clear()
        EP.Clear()
        cmbname.Text = ""
        txtremarks.Clear()
        jvregabbr = ""
        jvreginitial = ""
        GRIDDOUBLECLICK = False
        cmbtype.Text = ""
        cmbpaytype.Text = ""
        txtrefno.Clear()
        'txtrefno.ReadOnly = False
        txtdebit.Clear()
        txtcredit.Clear()
        gridjournal.RowCount = 0
        txtremarks.Clear()
        TXTINVAMT.Clear()
        TXTPARTYBILLNO.Clear()

        cmbpaytype.SelectedIndex = 0

        TXTTOTALDR.Text = 0.0
        TXTTOTALCR.Text = 0.0

        lbllocked.Visible = False
        PBlock.Visible = False

        edit = False
        getmaxno_journalmaster()

    End Sub

    Sub getmaxno_journalmaster()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(JOURNAL_NO),0) + 1 ", "JOURNALMASTER", " AND JOURNAL_YEARID = " & YearId)
        If DTTABLE.Rows.Count > 0 Then
            txtjournalno.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'JOURNAL'")
            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'JOURNAL' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If
            getmaxno_journalmaster()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtype_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtype.Validated

        If cmbtype.SelectedIndex = 0 Then
            txtcredit.ReadOnly = False
            txtdebit.ReadOnly = True
        Else
            txtcredit.ReadOnly = True
            txtdebit.ReadOnly = False
        End If
    End Sub

    Private Sub cmbpaytype_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbpaytype.Validated
        'If cmbpaytype.Text = "On Account" Then
        '    txtrefno.ReadOnly = True
        'Else
        '    txtrefno.ReadOnly = False
        'End If
    End Sub

    Private Sub txtrefno_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtrefno.Validating
        Try

            EP.Clear()
            If cmbtype.Text.Trim <> "" And txtrefno.Text.Trim <> "" And cmbpaytype.Text.Trim = "Against Bill" And GRIDDOUBLECLICK = False And edit = False Then
                'CHANGED BY GULKIT, COZ IF WE WANT TO ENTER ANY OTHER NO THEN IT VALIDATES US
                ' GETTING BILL DETAILS DIRECTLY FROM REFNO
                '******* OG CODE **************
                'If Not GETBILL(txtrefno.Text.Trim) Then
                '    e.Cancel = True
                '    Exit Sub
                'End If
                '************* END OF OG CODE *************************
                GETBILL(txtrefno.Text.Trim)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getsrno()
        Try
            For Each row As DataGridViewRow In gridjournal.Rows
                If Val(row.Cells(0).Value) > 0 Then row.Cells(8).Value = row.Index + 1
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            'used to assign gridsrno in receiptgrid
            getsrno()


            'GET BILLREMARKS
            TXTBILLREMARKS.Clear()
            For Each ROW As DataGridViewRow In gridjournal.Rows
                If ROW.Cells(GPAYTYPE.Index).Value = "Against Bill" Then
                    If TXTBILLREMARKS.Text = "" Then
                        TXTBILLREMARKS.Text = "Against Bill - " & ROW.Cells(GREFNO.Index).Value
                    Else
                        TXTBILLREMARKS.Text = TXTBILLREMARKS.Text & ", " & ROW.Cells(GREFNO.Index).Value
                    End If
                End If
            Next


            Dim alParaval As New ArrayList

            alParaval.Add(cmbregister.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(JOURNALDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(TXTBILLREMARKS.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            Dim type As String = ""
            Dim name As String = ""
            Dim paytype As String = ""
            Dim refno As String = ""
            Dim debit As String = ""
            Dim credit As String = ""
            Dim gridsrno As String = ""

            For Each row As Windows.Forms.DataGridViewRow In gridjournal.Rows
                If row.Cells(1).Value <> Nothing Then
                    If type = "" Then
                        type = row.Cells(0).Value.ToString
                        name = row.Cells(1).Value
                        paytype = row.Cells(2).Value.ToString
                        refno = row.Cells(3).Value
                        debit = row.Cells(4).Value
                        credit = row.Cells(5).Value
                        gridsrno = row.Cells(8).Value
                    Else
                        type = type & "," & row.Cells(0).Value.ToString
                        name = name & "," & row.Cells(1).Value
                        paytype = paytype & "," & row.Cells(2).Value.ToString
                        refno = refno & "," & row.Cells(3).Value
                        debit = debit & "," & row.Cells(4).Value
                        credit = credit & "," & row.Cells(5).Value
                        gridsrno = gridsrno & "," & row.Cells(8).Value
                    End If
                End If
            Next

            alParaval.Add(type)
            alParaval.Add(name)
            alParaval.Add(paytype)
            alParaval.Add(refno)
            alParaval.Add(debit)
            alParaval.Add(credit)
            alParaval.Add(gridsrno)

            Dim objclsjvmaster As New ClsJournalMaster()
            objclsjvmaster.alParaval = alParaval
            Dim DT As DataTable

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                DT = objclsjvmaster.save()
                MessageBox.Show("Details Added")

                If CHKTDS.CheckState = CheckState.Checked Then
                    Dim OBJTDS As New DeductTDS
                    OBJTDS.BILLNO = DT.Rows(0).Item(0)
                    OBJTDS.REGISTER = cmbregister.Text.Trim
                    OBJTDS.ShowDialog()
                End If

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(tempjvno)
                alParaval.Add(tempamt)
                alParaval.Add(temptds)
                DT = objclsjvmaster.update()
                MsgBox("Details Updated")
            End If

            'ACCOUNTS ENTRY TO BE DONE HERE COZ LOOP IS NOT POSSIBLE IN SP
            txtjournalno.Text = DT.Rows(0).Item(0)
            ACCOUNTSENTRY(DT.Rows(0).Item(0))

            'clear()
            edit = False

            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            cmbtype.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ACCOUNTSENTRY(ByVal JVNO As Integer)
        Try
            Dim OBJJV As New ClsJournalMaster
            Dim INTRESULT As Integer

            If edit = True Then
                Dim ALP As New ArrayList
                ALP.Add(TEMPREGNAME)
                ALP.Add(JVNO)
                ALP.Add(CmpId)
                ALP.Add(Locationid)
                ALP.Add(YearId)
                OBJJV.alP = ALP
                INTRESULT = OBJJV.ACCDELETE()
            End If

            Dim tempcredit, creditbal, tempdebit, debitbal As Double
            Dim tempr As Integer    'for row no of grid
            Dim I, J As Integer     'FOR LOOP

            tempcredit = 0
            creditbal = 0
            tempdebit = 0
            debitbal = 0
            tempr = 0

            For I = 0 To gridjournal.RowCount - 1
                If gridjournal.Item(0, I).Value.ToString = "Cr" Then
                    If Val(gridjournal.Item(6, I).Value.ToString) = 0 Then

                        tempcredit = Val(gridjournal.Item(5, I).Value.ToString)
                        If creditbal = 0 Then creditbal = Val(gridjournal.Item(5, I).Value.ToString)

                        J = I + 1

                        'getting amt
                        While (creditbal > 0)
                            If J < gridjournal.RowCount Then

                                Dim ALPARAVAL As New ArrayList

                                ALPARAVAL.Add(gridjournal.Item(1, I).Value.ToString)    'ADDING NAME FROMID

                                If Val(gridjournal.Item(6, J).Value.ToString) = 0 Then

                                    If gridjournal.Item(0, J).Value.ToString = "Dr" Then

                                        If debitbal = 0 Then debitbal = Val(gridjournal.Item(4, J).Value.ToString)

                                        If Val(creditbal) > Val(debitbal) Then

                                            ALPARAVAL.Add(Val(debitbal))        'AMOUNT
                                            creditbal = Val(creditbal) - Val(debitbal)
                                            debitbal = 0
                                            gridjournal.Item(6, J).Value = 1

                                        ElseIf Val(creditbal) < Val(debitbal) Then

                                            ALPARAVAL.Add(Val(creditbal))        'AMOUNT
                                            debitbal = Val(debitbal) - Val(creditbal)
                                            creditbal = 0
                                            gridjournal.Item(6, I).Value = 1

                                        ElseIf Val(creditbal) = Val(debitbal) Then

                                            ALPARAVAL.Add(Val(creditbal))        'AMOUNT
                                            debitbal = 0
                                            creditbal = 0
                                            gridjournal.Item(6, I).Value = 1
                                            gridjournal.Item(6, J).Value = 1

                                        End If

                                        ALPARAVAL.Add(gridjournal.Item(1, J).Value.ToString)    'ADDING NAME TOID
                                        ALPARAVAL.Add(Val(txtjournalno.Text))                   'JOURNAL NO
                                        ALPARAVAL.Add(Format(Convert.ToDateTime(JOURNALDATE.Text).Date, "MM/dd/yyyy"))                        'JOURNAL DATE
                                        ALPARAVAL.Add(cmbregister.Text.Trim)                    'REGISTER
                                        ALPARAVAL.Add(CmpId)
                                        ALPARAVAL.Add(Locationid)
                                        ALPARAVAL.Add(Userid)
                                        ALPARAVAL.Add(YearId)

                                        OBJJV.alParaval = ALPARAVAL
                                        INTRESULT = OBJJV.ACCOUNTS()

                                    End If
                                End If

                                J = J + 1
                            Else
                                creditbal = 0
                            End If
                        End While


                    End If

                ElseIf gridjournal.Item(0, I).Value.ToString = "Dr" Then

                    If Val(gridjournal.Item(6, I).Value.ToString) = 0 Then

                        tempdebit = Val(gridjournal.Item(4, I).Value.ToString)
                        If debitbal = 0 Then debitbal = Val(gridjournal.Item(4, I).Value.ToString)

                        J = I + 1

                        'getting amt
                        While (debitbal > 0)
                            If J < gridjournal.RowCount Then
                                Dim ALPARAVAL As New ArrayList

                                If Val(gridjournal.Item(6, J).Value.ToString) = 0 Then

                                    If gridjournal.Item(0, J).Value.ToString = "Cr" Then

                                        'getting ledgerid
                                        ALPARAVAL.Add(gridjournal.Item(1, J).Value.ToString) 'ADDING NAME FROMID

                                        If creditbal = 0 Then creditbal = Val(gridjournal.Item(5, J).Value.ToString)

                                        If Val(creditbal) > Val(debitbal) Then

                                            ALPARAVAL.Add(Val(debitbal))        'AMOUNT
                                            creditbal = Val(creditbal) - Val(debitbal)
                                            debitbal = 0
                                            gridjournal.Item(6, I).Value = 1    'DONE BY GULKIT IF ERROR THEN COMMENT THIS SND OPEN NEXT LINE
                                            'gridjournal.Item(6, J).Value = 1

                                        ElseIf Val(creditbal) < Val(debitbal) Then

                                            ALPARAVAL.Add(Val(creditbal))        'AMOUNT
                                            debitbal = Val(debitbal) - Val(creditbal)
                                            creditbal = 0
                                            gridjournal.Item(6, J).Value = 1    'DONE BY GULKIT IF ERROR THEN COMMENT THIS SND OPEN NEXT LINE
                                            'gridjournal.Item(6, I).Value = 1

                                        ElseIf Val(creditbal) = Val(debitbal) Then

                                            ALPARAVAL.Add(Val(debitbal))        'AMOUNT
                                            debitbal = 0
                                            creditbal = 0
                                            gridjournal.Item(6, I).Value = 1
                                            gridjournal.Item(6, J).Value = 1

                                        End If


                                        ALPARAVAL.Add(gridjournal.Item(1, I).Value.ToString)    'ADDING NAME TOID
                                        ALPARAVAL.Add(Val(txtjournalno.Text))                   'JOURNAL NO
                                        ALPARAVAL.Add(Format(Convert.ToDateTime(JOURNALDATE.Text).Date, "MM/dd/yyyy"))                        'JOURNAL DATE
                                        ALPARAVAL.Add(cmbregister.Text.Trim)                    'REGISTER
                                        ALPARAVAL.Add(CmpId)
                                        ALPARAVAL.Add(Locationid)
                                        ALPARAVAL.Add(Userid)
                                        ALPARAVAL.Add(YearId)

                                        OBJJV.alParaval = ALPARAVAL
                                        INTRESULT = OBJJV.ACCOUNTS()


                                    End If
                                End If

                                J = J + 1
                            Else
                                debitbal = 0
                            End If
                        End While


                    End If

                End If

            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If UserName <> "Admin" Then
            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, "Journal Locked")
                bln = False
            End If
        End If



        If cmbregister.Text.Trim.Length = 0 Then
            EP.SetError(cmbregister, "Select Register Name")
            bln = False
        End If

        If gridjournal.RowCount = 0 Then
            EP.SetError(txtcredit, "Enter Transactions")
            bln = False
        End If

        'cheking whether cr and dr is equal or not
        Dim tempdebit, tempcredit As Double
        For Each row As DataGridViewRow In gridjournal.Rows
            If row.Cells(2).Value = "" Then
                EP.SetError(txtcredit, "Fill PayType")
                bln = False
            End If
            tempdebit = Format(Val(tempdebit) + Val(row.Cells(4).Value), "0.00")
            tempcredit = Format(Val(tempcredit) + Val(row.Cells(5).Value), "0.00")

            If Val(row.Cells(GAMTPAIDREC.Index).Value) > 0 Then
                EP.SetError(cmbregister, "Journal Locked, Rec/Pay/JV Raised")
                bln = False
            End If
        Next
        If Val(tempdebit) <> Val(tempcredit) Then
            EP.SetError(txtcredit, "Total Does Not Match")
            bln = False
        End If


        If JOURNALDATE.Text = "__/__/____" Then
            EP.SetError(JOURNALDATE, "Enter Proper Date")
            bln = False
        ElseIf Not datecheck(JOURNALDATE.Text) Then
            EP.SetError(JOURNALDATE, "Date Not in Current Accounting Year")
            bln = False
        End If

        Return bln

    End Function

    Private Sub Journal_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D Then       'for Delete
                'Call cmddelete_Click(sender, e)
            ElseIf e.Control = True And e.Shift = True And e.KeyCode = Windows.Forms.Keys.R Then       'for Copy Old Narration
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" TOP 1 ISNULL(JOURNAL_REMARKS,'') AS REMARKS", "", " JOURNALMASTER INNER JOIN REGISTERMASTER  ON REGISTER_ID = JOURNAL_REGISTERID AND REGISTER_CMPID = JOURNAL_CMPID AND REGISTER_LOCATIONID = JOURNAL_LOCATIONID AND REGISTER_YEARID = JOURNAL_YEARID", "  AND JOURNAL_CMPID = " & CmpId & " AND JOURNAL_LOCATIONID = " & Locationid & " AND JOURNAL_YEARID = " & YearId & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND JOURNAL_NO = (SELECT MAX(JOURNAL_NO) FROM JOURNALMASTER INNER JOIN REGISTERMASTER  ON REGISTER_ID = JOURNAL_REGISTERID AND REGISTER_CMPID = JOURNAL_CMPID AND REGISTER_LOCATIONID = JOURNAL_LOCATIONID AND REGISTER_YEARID = JOURNAL_YEARID WHERE JOURNAL_CMPID = " & CmpId & " AND JOURNAL_LOCATIONID = " & Locationid & " AND JOURNAL_YEARID = " & YearId & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "')")
                If DT.Rows.Count > 0 Then txtremarks.Text = DT.Rows(0).Item("REMARKS")
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.F2 Then
                tstxtbillno.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Journal_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        'esc
        If AscW(e.KeyChar) <> 33 Then
            chkchange.CheckState = CheckState.Checked
        End If
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            If cmbregister.Text.Trim.Length > 0 And edit = False Then
                'clear()
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_abbr, register_initials, register_id", "", " RegisterMaster", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'JOURNAL' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
                If dt.Rows.Count > 0 Then
                    jvregabbr = dt.Rows(0).Item(0).ToString
                    jvreginitial = dt.Rows(0).Item(1).ToString
                    jvregid = dt.Rows(0).Item(2)
                    getmaxno_journalmaster()
                    cmbregister.Enabled = False
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtjournalno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtjournalno.KeyPress
        enterkeypress(e, Me)
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Function AMOUNTVALIDATE() As Boolean
        Try
            Dim BLN As Boolean = True

            If edit = False Then
                If GRIDDOUBLECLICK = False Then
                    'checking WHETHER AMT IS GREATER THEN BALANCE AMT OR NOT
                    If (cmbtype.Text = "Dr" And Val(txtdebit.Text) > Val(TXTBILLAMT.Text)) Or (cmbtype.Text = "Cr" And Val(txtcredit.Text) > Val(TXTBILLAMT.Text)) Then
                        EP.SetError(txtcredit, "Amount Exceeds Balance Amt.")
                        BLN = False
                    End If
                Else
                    'checking WHETHER AMT IS GREATER THEN BALANCE AMT OR NOT
                    If ((cmbtype.Text = "Dr" And (Val(txtdebit.Text) - Val(gridjournal.Item(GDEBIT.Index, TEMPROW).Value))) > Val(TXTBILLAMT.Text)) Or ((cmbtype.Text = "Cr" And (Val(txtcredit.Text) - Val(gridjournal.Item(GCREDIT.Index, TEMPROW).Value))) > Val(TXTBILLAMT.Text)) Then
                        EP.SetError(txtcredit, "Amount Exceeds Balance Amt.")
                        BLN = False
                    End If
                End If
            End If

            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub txtcredit_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtcredit.Validating
        Try
            If (Val(txtcredit.Text) <> 0 Or Val(txtdebit.Text) <> 0) And cmbname.Text.Trim <> "" And cmbtype.Text.Trim <> "" Then

                If cmbpaytype.Text.Trim = "Against Bill" Then
                    'NO NEED FOR THIS
                    'EP.Clear()
                    'If Not AMOUNTVALIDATE() Then
                    '    Exit Sub
                    'End If
                    If TXTPARTYBILLNO.Text.Trim <> "" Then txtremarks.Text = "Against Bill No " & TXTPARTYBILLNO.Text.Trim & " Bill Dt. " & Format(PARTYBILLDATE.Value.Date, "dd/MM/yyy") Else txtremarks.Text = "Against Bill No " & txtrefno.Text.Trim & " Bill Dt. " & Format(PARTYBILLDATE.Value.Date, "dd/MM/yyy")
                End If

                If cmbtype.Text = "Dr" Then
                    txtcredit.Clear()
                Else
                    txtdebit.Clear()
                End If

                fillgrid()
                txtdebit.Clear()
                txtcredit.Clear()
                txtrefno.Clear()
                cmbpaytype.SelectedIndex = 0
                cmbname.Text = ""
                cmbtype.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function checkledger() As Boolean
        Try
            Dim bln As Boolean = True
            For Each row As DataGridViewRow In gridjournal.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> row.Index) Or GRIDDOUBLECLICK = False Then
                    If cmbname.Text.Trim = row.Cells(1).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub fillgrid()

        'GETTING CURRENT BALANCE FROM TRIAL BALANCE
        Dim OBJCOMMON As New ClsCommon
        Dim DT As New DataTable
        Dim BAL As String = String.Empty
        Dim DRCR As String = String.Empty
        DT = OBJCOMMON.search(" (CASE WHEN DR-CR>0 THEN DR-CR ELSE CR-DR END), (CASE WHEN DR-CR>0 THEN 'Dr' ELSE 'Cr' END)", "", " TRIALBALANCE", " AND NAME = '" & cmbname.Text.Trim & "' AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND YEARID = " & YearId)
        If DT.Rows.Count > 0 Then
            BAL = DT.Rows(0).Item(0)
            DRCR = DT.Rows(0).Item(1)
        End If

        If GRIDDOUBLECLICK = False Then

            If DRCR = "Dr" Then
                BAL = Val(BAL) + Val(txtdebit.Text) - Val(txtcredit.Text)
                If Val(BAL) > 0 Then
                    BAL = Val(BAL) & "Dr"
                Else
                    BAL = Val(BAL) & "Cr"
                End If
                gridjournal.Rows.Add(cmbtype.Text, cmbname.Text.Trim, cmbpaytype.Text, txtrefno.Text.Trim, Format(Val(txtdebit.Text), "0.00"), Format(Val(txtcredit.Text), "0.00"), "", BAL, 0, 0)
            Else
                BAL = Val(BAL) + Val(txtcredit.Text) - Val(txtdebit.Text)
                If Val(BAL) > 0 Then
                    BAL = Val(BAL) & "Cr"
                Else
                    BAL = Val(BAL) & "Dr"
                End If
                gridjournal.Rows.Add(cmbtype.Text, cmbname.Text.Trim, cmbpaytype.Text, txtrefno.Text.Trim, Format(Val(txtdebit.Text), "0.00"), Format(Val(txtcredit.Text), "0.00"), "", BAL, 0, 0)
            End If

        ElseIf GRIDDOUBLECLICK = True Then

            gridjournal.Item(0, TEMPROW).Value = cmbtype.Text
            gridjournal.Item(1, TEMPROW).Value = cmbname.Text.Trim
            gridjournal.Item(2, TEMPROW).Value = cmbpaytype.Text
            gridjournal.Item(3, TEMPROW).Value = txtrefno.Text.Trim
            gridjournal.Item(4, TEMPROW).Value = Format(Val(txtdebit.Text), "0.00")
            gridjournal.Item(5, TEMPROW).Value = Format(Val(txtcredit.Text), "0.00")
            gridjournal.Item(6, TEMPROW).Value = ""
            GRIDDOUBLECLICK = False

            If DRCR = "Dr" Then
                BAL = Val(BAL) + Val(txtdebit.Text) - Val(txtcredit.Text)
                If Val(BAL) > 0 Then
                    BAL = Val(BAL) & "Dr"
                Else
                    BAL = Val(BAL) * (-1) & "Cr"
                End If
                gridjournal.Item(7, TEMPROW).Value = BAL
            Else
                BAL = Val(BAL) + Val(txtcredit.Text) - Val(txtdebit.Text)
                If Val(BAL) > 0 Then
                    BAL = Val(BAL) & "Cr"
                Else
                    BAL = Val(BAL) * (-1) & "Dr"
                End If
                gridjournal.Item(7, TEMPROW).Value = BAL
            End If
        End If

        TOTAL()
    End Sub

    Sub TOTAL()
        Try
            TXTTOTALCR.Text = 0.0
            TXTTOTALDR.Text = 0.0

            For Each ROW As DataGridViewRow In gridjournal.Rows
                If Val(ROW.Cells(GDEBIT.Index).Value) > 0 Or Val(ROW.Cells(GCREDIT.Index).Value) > 0 Then
                    TXTTOTALDR.Text = Format(Val(TXTTOTALDR.Text.Trim) + Val(ROW.Cells(GDEBIT.Index).Value), "0.00")
                    TXTTOTALCR.Text = Format(Val(TXTTOTALCR.Text.Trim) + Val(ROW.Cells(GCREDIT.Index).Value), "0.00")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridjournal_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridjournal.KeyDown
        If e.KeyCode = Keys.Delete Then

            If Val(gridjournal.CurrentRow.Cells(GAMTPAIDREC.Index).Value) > 0 Then
                MsgBox("Journal Locked, Rec/Pay/JV Raised")
                Exit Sub
            End If

            gridjournal.Rows.RemoveAt(gridjournal.CurrentRow.Index)
            TOTAL()
        End If
    End Sub

    Private Sub gridjournal_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridjournal.CellDoubleClick
        If e.RowIndex >= 0 And gridjournal.Item(0, e.RowIndex).Value <> Nothing Then

            If Val(gridjournal.CurrentRow.Cells(GAMTPAIDREC.Index).Value) > 0 Then
                MsgBox("Journal Locked, Rec/Pay/JV Raised")
                Exit Sub
            End If


            GRIDDOUBLECLICK = True
            TEMPROW = e.RowIndex
            cmbtype.Text = gridjournal.Item(0, e.RowIndex).Value
            cmbname.Text = gridjournal.Item(1, e.RowIndex).Value
            cmbpaytype.Text = gridjournal.Item(2, e.RowIndex).Value
            txtrefno.Text = gridjournal.Item(3, e.RowIndex).Value
            txtdebit.Text = gridjournal.Item(4, e.RowIndex).Value
            txtcredit.Text = gridjournal.Item(5, e.RowIndex).Value
            cmbtype.Focus()
        End If
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim objjvdtls As New JournalDetails
            objjvdtls.MdiParent = MDIMain
            objjvdtls.Show()
            objjvdtls.BringToFront()
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Journal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'JOURNAL VOUCHER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            getmaxno_journalmaster()
            fillregister(cmbregister, " and register_type = 'JOURNAL'")
            fillledger(cmbname, edit, " and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
            cmbpaytype.SelectedIndex = 0
            cmbname.Text = ""
            JOURNALDATE.Text = Mydate

            If edit = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dt As New DataTable
                Dim objclsJV As New ClsJournalMaster()
                dt = objclsJV.selectjournal_edit(tempjvno, TEMPREGNAME, CmpId, Locationid, YearId)

                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows

                        txtjournalno.Text = tempjvno
                        cmbregister.Text = TEMPREGNAME
                        JOURNALDATE.Text = Format(Convert.ToDateTime(dr("JOURNAL_DATE")).Date, "dd/MM/yyyy")
                        tempamt = Convert.ToString(dr("JOURNAL_AMT"))
                        temptds = Convert.ToString(dr("JOURNAL_TDS"))
                        txtremarks.Text = Convert.ToString(dr("JOURNAL_remarks"))
                        gridjournal.Rows.Add(dr("JOURNAL_TYPE").ToString, dr("ACC_CMPNAME").ToString, dr("JOURNAL_PAYTYPE").ToString, dr("JOURNAL_REFNO").ToString, dr("JOURNAL_DEBIT").ToString, dr("JOURNAL_CREDIT").ToString, 0, 0, 0, Val(dr("JOURNAL_AMT")))
                        If IsDBNull(dr("JOURNAL_recodate")) = False Then temprecodate = Convert.ToDateTime(dr("JOURNAL_recodate"))
                        If Val(dr("JOURNAL_AMT")) > 0 Then gridjournal.Rows(gridjournal.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow

                        If dr("JOURNAL_PAYTYPE") = "Against Bill" Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                    Next
                    TOTAL()
                    cmbregister.Enabled = False
                    cmbtype.Focus()
                    chkchange.CheckState = CheckState.Checked

                Else
                    edit = False
                    clear()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            'OPEN ALL LEDGERS
            'If cmbname.Text.Trim = "" Then fillname(cmbname, edit, " and GROUPMASTER.GROUP_SECONDARY NOT IN ('CASH IN HAND','BANK A/C','BANK OD A/C') and acc_cmpid = " & CmpId)
            If cmbname.Text.Trim = "" Then fillname(cmbname, edit, " AND GROUPMASTER.GROUP_SECONDARY <> 'Bank A/C' AND GROUPMASTER.GROUP_SECONDARY <> 'Bank OD A/C' AND GROUPMASTER.GROUP_SECONDARY <> 'Cash In Hand' and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
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
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY <> 'Bank A/C' AND GROUPMASTER.GROUP_SECONDARY <> 'Bank OD A/C' AND GROUPMASTER.GROUP_SECONDARY <> 'Cash In Hand' and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            'If cmbname.Text.Trim <> "" Then namevalidate(cmbname, e, Me, TXTADD, " AND  GROUPMASTER.GROUP_SECONDARY NOT IN ('CASH IN HAND','BANK A/C','BANK OD A/C')")
            If cmbname.Text.Trim <> "" Then namevalidate(cmbname, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY <> 'Bank A/C' AND GROUPMASTER.GROUP_SECONDARY <> 'Bank OD A/C' AND GROUPMASTER.GROUP_SECONDARY <> 'Cash In Hand'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            gridjournal.RowCount = 0
LINE1:
            tempjvno = Val(txtjournalno.Text) + 1
            TEMPREGNAME = cmbregister.Text.Trim
            getmaxno_journalmaster()
            Dim MAXNO As Integer = txtjournalno.Text.Trim
            clear()
            If Val(txtjournalno.Text) - 1 >= tempjvno Then
                edit = True
                Journal_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If gridjournal.RowCount = 0 And tempjvno < MAXNO Then
                txtjournalno.Text = tempjvno
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            gridjournal.RowCount = 0
LINE1:
            tempjvno = Val(txtjournalno.Text) - 1
            TEMPREGNAME = cmbregister.Text.Trim
            If tempjvno > 0 Then
                edit = True
                Journal_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If gridjournal.RowCount = 0 And tempjvno > 1 Then
                txtjournalno.Text = tempjvno
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripdelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripdelete.Click
        Try
            If edit = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                For Each ROW As DataGridViewRow In gridjournal.Rows
                    If Val(ROW.Cells(GAMTPAIDREC.Index).Value) > 0 Then
                        MsgBox("Journal Locked, Rec/Pay/JV Raised")
                        Exit Sub
                    End If
                Next

                Dim tempmsg As Integer = MsgBox("Delete Journal Entry Permanently?", MsgBoxStyle.YesNo, "TEXPRO")
                If tempmsg = vbYes Then

                    Dim OBJJV As New ClsJournalMaster
                    Dim ALPARAVAL As New ArrayList

                    ALPARAVAL.Add(tempjvno)
                    ALPARAVAL.Add(TEMPREGNAME)
                    ALPARAVAL.Add(CmpId)
                    ALPARAVAL.Add(Locationid)
                    ALPARAVAL.Add(YearId)
                    ALPARAVAL.Add(Userid)

                    OBJJV.alParaval = ALPARAVAL
                    Dim DT As DataTable = OBJJV.Delete()
                    MsgBox(DT.Rows(0).Item(0).ToString)

                    clear()

                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If tstxtbillno.Text.Trim.Length = 0 Then Exit Sub
            gridjournal.RowCount = 0
            tempjvno = Val(tstxtbillno.Text)
            TEMPREGNAME = cmbregister.Text.Trim
            If tempjvno > 0 Then
                edit = True
                Journal_Load(sender, e)
            Else
                clear()
                edit = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDVIEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDVIEW.Click
        Try
            If cmbname.Text.Trim <> "" Then

                SELECTEDBILLNO = ""
                txtrefno.Clear()
                TXTBILLAMT.Clear()

                Dim OBJSELECTBILL As New SelectBills
                OBJSELECTBILL.CMPNAME = cmbname.Text.Trim
                OBJSELECTBILL.ShowDialog()
                If OBJSELECTBILL.BILLNO <> "" Then SELECTEDBILLNO = OBJSELECTBILL.BILLNO

                If SELECTEDBILLNO.Trim <> "" Then
                    If Not GETBILL(SELECTEDBILLNO) Then
                        Exit Sub
                    End If
                    txtrefno.Focus()
                End If
            Else
                MsgBox("Select Name")
                cmbname.Focus()
                Exit Sub
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function GETBILL(ByVal BILLNO As String) As Boolean
        Try
            Dim BLN As Boolean = True
            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable = OBJCMN.search(" * ", "", " (SELECT OPENINGBILL.BILL_INITIALS AS SRNO, LEDGERS.Acc_cmpname AS NAME, OPENINGBILL.BILL_AMT AS BILLAMT, OPENINGBILL.BILL_BALANCE AS BALAMT, 'OPENING' AS TYPE, OPENINGBILL.BILL_INITIALS AS REFNO, OPENINGBILL.BILL_DATE AS PARTYBILLDATE, OPENINGBILL.BILL_NO AS BILLNO, OPENINGBILL.BILL_CMPID AS CMPID, OPENINGBILL.BILL_LOCATIONID AS LOCATIONID, OPENINGBILL.BILL_YEARID AS YEARID FROM OPENINGBILL INNER JOIN LEDGERS ON OPENINGBILL.BILL_LEDGERID = LEDGERS.Acc_id AND OPENINGBILL.BILL_CMPID = LEDGERS.Acc_cmpid AND OPENINGBILL.BILL_LOCATIONID = LEDGERS.Acc_locationid And OPENINGBILL.BILL_YEARID = LEDGERS.Acc_yearid WHERE(OPENINGBILL.BILL_YEARID = " & YearId & ") UNION ALL  SELECT INVOICEMASTER.INVOICE_INITIALS AS SRNO, LEDGERS.Acc_cmpname AS NAME, INVOICEMASTER.INVOICE_GRANDTOTAL AS BILLAMT, INVOICEMASTER.INVOICE_BALANCE AS BALAMT, 'INVOICE' AS TYPE, '' AS REFNO, INVOICEMASTER.INVOICE_DATE AS PARTYBILLDATE, INVOICEMASTER.INVOICE_NO AS BILLNO, INVOICEMASTER.INVOICE_CMPID AS CMPID, INVOICEMASTER.INVOICE_LOCATIONID AS LOCATIONID, INVOICEMASTER.INVOICE_YEARID AS YEARID FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id AND INVOICEMASTER.INVOICE_CMPID = LEDGERS.Acc_cmpid AND INVOICEMASTER.INVOICE_LOCATIONID = LEDGERS.Acc_locationid And INVOICEMASTER.INVOICE_YEARID = LEDGERS.Acc_yearid WHERE INVOICEMASTER.INVOICE_YEARID = " & YearId & " UNION ALL SELECT PURCHASEMASTER.BILL_INITIALS AS SRNO, LEDGERS.Acc_cmpname AS NAME, PURCHASEMASTER.BILL_GRANDTOTAL AS BILLAMT, PURCHASEMASTER.BILL_BALANCE AS BALAMT, 'PURCHASE' AS TYPE, PURCHASEMASTER.BILL_PARTYBILLNO AS REFNO, PURCHASEMASTER.BILL_PARTYBILLDATE AS PARTYBILLDATE, PURCHASEMASTER.BILL_NO AS BILLNO, PURCHASEMASTER.BILL_CMPID AS CMPID, PURCHASEMASTER.BILL_LOCATIONID AS LOCATIONID, PURCHASEMASTER.BILL_YEARID AS YEARID FROM PURCHASEMASTER INNER JOIN LEDGERS ON PURCHASEMASTER.BILL_LEDGERID = LEDGERS.Acc_id AND PURCHASEMASTER.BILL_CMPID = LEDGERS.Acc_cmpid AND PURCHASEMASTER.BILL_LOCATIONID = LEDGERS.Acc_locationid And PURCHASEMASTER.BILL_YEARID = LEDGERS.Acc_yearid WHERE PURCHASEMASTER.BILL_YEARID = " & YearId & " UNION ALL SELECT DEBITNOTEMASTER.DN_INITIALS AS SRNO, LEDGERS.Acc_cmpname AS NAME, DEBITNOTEMASTER.DN_GTOTAL AS BILLAMT, DEBITNOTEMASTER.DN_BALANCE AS BALAMT, 'DEBITNOTE' AS TYPE, DN_BILLNO AS REFNO, DEBITNOTEMASTER.DN_DATE AS PARTYBILLDATE, DEBITNOTEMASTER.DN_NO AS BILLNO, DEBITNOTEMASTER.DN_CMPID AS CMPID, DEBITNOTEMASTER.DN_LOCATIONID AS LOCATIONID, DEBITNOTEMASTER.DN_YEARID AS YEARID FROM DEBITNOTEMASTER INNER JOIN LEDGERS ON DEBITNOTEMASTER.DN_LEDGERID = LEDGERS.Acc_id  WHERE DEBITNOTEMASTER.DN_YEARID = " & YearId & " UNION ALL SELECT NONPURCHASE.NP_INITIALS AS SRNO, LEDGERS.Acc_cmpname AS NAME, NONPURCHASE.NP_GRANDTOTAL AS BILLAMT, NONPURCHASE.NP_BALANCE AS BALAMT, 'EXPENSE' AS TYPE, NONPURCHASE.NP_PARTYBILLNO AS REFNO, NONPURCHASE.NP_PARTYBILLDATE AS PARTYBILLDATE, NONPURCHASE.NP_NO AS BILLNO, NONPURCHASE.NP_CMPID AS CMPID, 0 AS LOCATIONID, NONPURCHASE.NP_YEARID AS YEARID FROM NONPURCHASE INNER JOIN LEDGERS ON NONPURCHASE.NP_LEDGERID = LEDGERS.Acc_id WHERE NONPURCHASE.NP_YEARID = " & YearId & ") AS T ", " AND T.SRNO = '" & BILLNO & "'  AND T.BALAMT > 0 AND T.YEARID =" & YearId & " ORDER BY T.TYPE, T.BILLNO ")
            If dt.Rows.Count > 0 Then
                cmbname.Text = dt.Rows(0).Item("NAME")
                txtrefno.Text = BILLNO
                cmbpaytype.Text = "Against Bill"
                TXTBILLAMT.Text = Val(dt.Rows(0).Item("BALAMT"))
                TXTPARTYBILLNO.Text = dt.Rows(0).Item("REFNO")
                PARTYBILLDATE.Value = Format(dt.Rows(0).Item("PARTYBILLDATE"), "dd/MM/yyyy")
                TXTINVAMT.Text = Val(dt.Rows(0).Item("BILLAMT"))
                LBLREFNO.Visible = True
                TXTPARTYBILLNO.Visible = True
                LBLBILLAMT.Visible = True
                TXTINVAMT.Visible = True
                If cmbtype.Text = "Dr" Then
                    txtdebit.Text = Val(dt.Rows(0).Item("BALAMT"))
                Else
                    txtcredit.Text = Val(dt.Rows(0).Item("BALAMT"))
                End If
            Else
                EP.SetError(txtcredit, "Invalid Bill No")
                BLN = False
                LBLREFNO.Visible = False
                TXTPARTYBILLNO.Visible = False
                LBLBILLAMT.Visible = False
                TXTINVAMT.Visible = False
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub JOURNALDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles JOURNALDATE.Validating
        Try
            If JOURNALDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(JOURNALDATE.Text, TEMP) Then
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