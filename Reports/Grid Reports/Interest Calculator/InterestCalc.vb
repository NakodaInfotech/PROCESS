
Imports BL

Public Class InterestCalc

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdshowdetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshowdetails.Click
        fillgrid()
    End Sub

    Sub fillgrid()
        Try
            EP.Clear()
            If cmbname.Text.Trim.Length = 0 Then
                EP.SetError(cmbname, "Select Party")
                Exit Sub
            End If

            If Val(TXTPERCENT.Text.Trim) = 0 Then
                EP.SetError(TXTPERCENT, "Enter Rate Of Interest")
                Exit Sub
            End If

            If Val(TXTDAYS.Text.Trim) = 0 Then
                EP.SetError(TXTDAYS, "Enter Days")
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            Dim WHERE As String = " AND CMPID= " & CmpId & " AND LOCATIONID = " & Locationid & " AND YEARID = " & YearId
            Dim OPWHERE As String = " AND CMPID= " & CmpId & " AND LOCATIONID = " & Locationid & " AND YEARID = " & YearId
            If CHKDN.CheckState = CheckState.Unchecked Then WHERE = WHERE & " AND TYPE <> 'INVOICEDEBITNOTE' "
            If CHKDATE.Checked = True Then
                If RBDUEDATE.Checked = True Then
                    WHERE = WHERE & " AND DUEDATE > '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "' AND DUEDATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
                    OPWHERE = OPWHERE & " AND DUEDATE <= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'"
                ElseIf RBBILLDATE.Checked = True Then
                    WHERE = WHERE & " AND DATE > '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "' AND DATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
                    OPWHERE = OPWHERE & " AND DATE <= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'"
                ElseIf RBPASSDATE.Checked = True Then
                    WHERE = WHERE & " AND APPDATE > '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "' AND APPDATE <='" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
                    OPWHERE = OPWHERE & " AND APPDATE <= '" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'"
                End If
            Else
                If RBDUEDATE.Checked = True Then
                    WHERE = WHERE & " AND DUEDATE > '" & Format(AccFrom, "MM/dd/yyyy") & "' AND DUEDATE <='" & Format(AccTo, "MM/dd/yyyy") & "'"
                    OPWHERE = OPWHERE & " AND DUEDATE <= '" & Format(AccFrom.Date, "MM/dd/yyyy") & "'"
                ElseIf RBBILLDATE.Checked = True Then
                    WHERE = WHERE & " AND DATE > '" & Format(AccFrom, "MM/dd/yyyy") & "' AND DATE <='" & Format(AccTo, "MM/dd/yyyy") & "'"
                    OPWHERE = OPWHERE & " AND DATE <= '" & Format(AccFrom.Date, "MM/dd/yyyy") & "'"
                ElseIf RBPASSDATE.Checked = True Then
                    WHERE = WHERE & " AND APPDATE > '" & Format(AccFrom, "MM/dd/yyyy") & "' AND APPDATE <='" & Format(AccTo, "MM/dd/yyyy") & "'"
                    OPWHERE = OPWHERE & " AND APPDATE <= '" & Format(AccFrom.Date, "MM/dd/yyyy") & "'"
                End If
            End If

            Dim dt As DataTable = OBJCMN.search("1 AS SORTNO, SRNO, REGTYPE, BILLINITIALS, TYPE, NAME, DATE, APPDATE, DUEDATE, TOTALBALES,DEBIT, CREDIT, ISNULL( DATEDIFF(DAY,(SELECT DUEDATE FROM (SELECT     ROW_NUMBER() OVER ( ORDER BY DUEDATE)AS ROWNO, DUEDATE FROM INTERESTVIEW WHERE (NAME = '" & cmbname.Text.Trim & "' AND CMPID = " & CmpId & " AND LOCATIONID = " & Locationid & " AND YEARID = " & YearId & WHERE & ")) AS T WHERE T.ROWNO = NEWT.ROWNO -1 ), NEWT.[DUEDATE]),0) AS [DAYS], 0 AS NETTBALANCE, 0 AS TOPAY, 0 AS TOREC  ", "", " (SELECT     ROW_NUMBER() OVER (ORDER BY DUEDATE)AS ROWNO, SRNO, REGTYPE, BILLINITIALS, TYPE, NAME, DATE, APPDATE, DUEDATE,TOTALBALES, DEBIT, CREDIT,  CMPID, LOCATIONID, YEARID FROM INTERESTVIEW WHERE (NAME = '" & cmbname.Text.Trim & "' AND CMPID = " & CmpId & " AND LOCATIONID = " & Locationid & " AND YEARID = " & YearId & WHERE & ")) AS NEWT ", WHERE & " ORDER BY NEWT.NAME, NEWT.DUEDATE  ")
            Dim DTROW() As DataRow

            Dim DTOPENING As DataTable
            DTOPENING = OBJCMN.search(" (CASE WHEN (SUM(DEBIT) - SUM(CREDIT)> 0) THEN (SUM(DEBIT) - SUM(CREDIT)) ELSE 0 END )AS DEBITBAL, (CASE WHEN (SUM(CREDIT) - SUM(DEBIT)> 0 )THEN (SUM(CREDIT) - SUM(DEBIT)) ELSE 0 END)  AS CREDITBAL ", "", " INTERESTVIEW ", OPWHERE & " AND NAME = '" & cmbname.Text.Trim & "'")
            If DTOPENING.Rows.Count > 0 Then
                If CHKDATE.CheckState = CheckState.Checked Then
                    If (Val(DTOPENING.Rows(0).Item("DEBITBAL")) > 0 Or Val(DTOPENING.Rows(0).Item("CREDITBAL")) > 0) Then dt.Rows.Add(0, 0, "", "OPENING", "", "", dtfrom.Value.Date, dtfrom.Value.Date, dtfrom.Value.Date, 0, Val(DTOPENING.Rows(0).Item("DEBITBAL")), Val(DTOPENING.Rows(0).Item("CREDITBAL")), 0, 0, 0, 0)
                Else
                    If (Val(DTOPENING.Rows(0).Item("DEBITBAL")) > 0 Or Val(DTOPENING.Rows(0).Item("CREDITBAL")) > 0) Then dt.Rows.Add(0, 0, "", "OPENING", "", "", AccFrom.Date, AccFrom.Date, AccFrom.Date, 0, Val(DTOPENING.Rows(0).Item("DEBITBAL")), Val(DTOPENING.Rows(0).Item("CREDITBAL")), 0, 0, 0, 0)
                End If
            End If


            If dt.Rows.Count > 0 Then

                Dim CLODAYS As Integer = 0
                If RBDUEDATE.Checked = True Then
                    DTROW = dt.Select("DUEDATE = MAX(DUEDATE)")
                    Dim NETBAL As Double = dt.Compute("(SUM(DEBIT) - SUM(CREDIT))", "")
                    If Val(NETBAL) <> 0 Then CLODAYS = 1
                    If CHKDATE.CheckState = CheckState.Checked Then
                        CLODAYS = CLODAYS + DateDiff(DateInterval.Day, DTROW(0).Item("DUEDATE"), dtto.Value.Date)
                        If CLODAYS > 0 Then dt.Rows.Add(2, 0, "", "CLOSING", "", "", dtto.Value.Date, dtto.Value.Date, dtto.Value.Date, 0, 0, 0, CLODAYS, 0, 0, 0)
                    Else
                        CLODAYS = CLODAYS + DateDiff(DateInterval.Day, DTROW(0).Item("DUEDATE"), AccTo.Date)
                        If CLODAYS > 0 Then dt.Rows.Add(2, 0, "", "CLOSING", "", "", AccTo.Date, AccTo.Date, AccTo.Date, 0, 0, 0, CLODAYS, 0, 0, 0)
                    End If

                ElseIf RBBILLDATE.Checked = True Then
                    DTROW = dt.Select("DATE = MAX(DATE)")
                    If CHKDATE.CheckState = CheckState.Checked Then
                        CLODAYS = DateDiff(DateInterval.Day, DTROW(0).Item("DATE"), dtto.Value.Date)
                        If CLODAYS > 0 Then dt.Rows.Add(2, 0, "", "CLOSING", "", "", dtto.Value.Date, dtto.Value.Date, dtto.Value.Date, 0, 0, 0, CLODAYS, 0, 0, 0)
                    Else
                        CLODAYS = DateDiff(DateInterval.Day, DTROW(0).Item("DATE"), AccTo.Date)
                        If CLODAYS > 0 Then dt.Rows.Add(2, 0, "", "CLOSING", "", "", AccTo.Date, AccTo.Date, AccTo.Date, 0, 0, 0, CLODAYS, 0, 0, 0)
                    End If

                ElseIf RBPASSDATE.Checked = True Then
                    DTROW = dt.Select("APPDATE = MAX(APPDATE)")
                    If CHKDATE.CheckState = CheckState.Checked Then
                        CLODAYS = DateDiff(DateInterval.Day, DTROW(0).Item("APPDATE"), dtto.Value.Date)
                        If CLODAYS > 0 Then dt.Rows.Add(2, 0, "", "CLOSING", "", "", dtto.Value.Date, dtto.Value.Date, dtto.Value.Date, 0, 0, 0, CLODAYS, 0, 0, 0)
                    Else
                        CLODAYS = DateDiff(DateInterval.Day, DTROW(0).Item("APPDATE"), AccTo.Date)
                        If CLODAYS > 0 Then dt.Rows.Add(2, 0, "", "CLOSING", "", "", AccTo.Date, AccTo.Date, AccTo.Date, 0, 0, 0, CLODAYS, 0, 0, 0)
                    End If

                End If

            End If

            Dim DV As New DataView(dt)
            If RBDUEDATE.Checked = True Then
                DV.Sort = "SORTNO ASC, NAME ASC,DUEDATE ASC"
            ElseIf RBBILLDATE.Checked = True Then
                DV.Sort = "SORTNO ASC, NAME ASC,DATE ASC"
            ElseIf RBPASSDATE.Checked = True Then
                DV.Sort = "SORTNO ASC, NAME ASC,APPDATE ASC"
            End If

            griddetails.DataSource = DV
            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try

            txttotal.Text = 0.0

            'FOR RUNNING BALANCE
            Dim dtrow As DataRow
            Dim i As Integer
            Dim RUNNINGBAL As Double
            For i = 0 To gridregister.RowCount - 1
                dtrow = gridregister.GetDataRow(i)
                dtrow("NETTBALANCE") = (Val(dtrow("DEBIT")) + Val(RUNNINGBAL)) - Val(dtrow("CREDIT"))
                RUNNINGBAL = dtrow("NETTBALANCE")
            Next


            RUNNINGBAL = 0
            For i = 0 To gridregister.RowCount - 1
                dtrow = gridregister.GetDataRow(i)
                Dim NEWDTROW As DataRow = gridregister.GetDataRow(i + 1)
                If dtrow("BILLINITIALS") = "OPENING" Then
                    If NEWDTROW("BILLINITIALS") <> "CLOSING" Then NEWDTROW("DAYS") = DateDiff(DateInterval.Day, dtrow("DUEDATE"), NEWDTROW("DUEDATE"))
                End If

                If dtrow("BILLINITIALS") = "CLOSING" Then
                    'IF LAST DATE IS SAME AS CLOSING DATE THEN THERE WILL BE NOT CALCULATIONS OF DATS IN CLOSING
                    'THIS IS DONE BY GULKIT DO NOT CHANGE 
                    Dim TEMPDTROW As DataRow = gridregister.GetDataRow(i - 1)
                    If dtrow("DUEDATE") = TEMPDTROW("DUEDATE") And TEMPDTROW("BILLINITIALS") <> "OPENING" Then
                        TEMPDTROW("DAYS") = TEMPDTROW("DAYS") + 1
                        Dim TEMPDTROW1 As DataRow = gridregister.GetDataRow(i - 2)
                        If TEMPDTROW("NETTBALANCE") > 0 Then
                            TEMPDTROW("TOREC") = (((Val(TXTPERCENT.Text.Trim) * Val(TEMPDTROW1("NETTBALANCE"))) / 100) / Val(TXTDAYS.Text.Trim) * Val(TEMPDTROW("DAYS")))
                        Else
                            TEMPDTROW("TOPAY") = (((Val(TXTPERCENT.Text.Trim) * (Val(TEMPDTROW1("NETTBALANCE")) * (-1))) / 100) / Val(TXTDAYS.Text.Trim) * Val(TEMPDTROW("DAYS")))
                        End If
                        dtrow("DAYS") = 0
                    End If
                End If

                If Val(dtrow("DAYS")) > 0 Then
                    If Val(RUNNINGBAL) > 0 Then
                        dtrow("TOREC") = (((Val(TXTPERCENT.Text.Trim) * RUNNINGBAL) / 100) / Val(TXTDAYS.Text.Trim) * Val(dtrow("DAYS")))
                    Else
                        dtrow("TOPAY") = (((Val(TXTPERCENT.Text.Trim) * (RUNNINGBAL * (-1))) / 100) / Val(TXTDAYS.Text.Trim) * Val(dtrow("DAYS")))
                    End If
                End If
                RUNNINGBAL = dtrow("NETTBALANCE")

            Next


            If Val(GTOPAY.SummaryText) > Val(GTOREC.SummaryText) Then
                txttotal.Text = Format(Val(GTOPAY.SummaryText) - Val(GTOREC.SummaryText), "0.00")
                lbldrcrclosing.Text = "Cr"
            Else
                txttotal.Text = Format(Val(GTOREC.SummaryText) - Val(GTOPAY.SummaryText), "0.00")
                lbldrcrclosing.Text = "Dr"
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then fillname(cmbname, False, " AND (GROUP_SECONDARY = 'Sundry Creditors' or GROUP_SECONDARY = 'Sundry Debtors'  OR GROUP_SECONDARY = 'Loans' OR GROUP_SECONDARY = 'Loans & Advances' OR GROUP_SECONDARY = 'Capital A/C') and ledgers.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.ACC_YEARID = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND (GROUP_SECONDARY = 'Sundry Creditors' or GROUP_SECONDARY = 'Sundry Debtors'  OR GROUP_SECONDARY = 'Loans' OR GROUP_SECONDARY = 'Loans & Advances' OR GROUP_SECONDARY = 'Capital A/C') and ledgers.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.ACC_YEARID = " & YearId
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBACCCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Validated
        Try
            If cmbname.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ACC_INTPER AS INTPER", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_YEARID = " & YearId)
                TXTPERCENT.Text = Val(DT.Rows(0).Item("INTPER"))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then namevalidate(cmbname, CMBACCCODE, e, Me, txtadd, " and ledgers.acc_cmpname = '" & cmbname.Text.Trim & "' and ledgers.acc_cmpid = " & CmpId & " and ledgers.acc_LOCATIONid = " & Locationid & " and ledgers.ACC_YEARID = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InterestCalc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.S And e.Alt = True Then
                cmdshowdetails_Click(sender, e)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                cmdok_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InterestCalc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillname(cmbname, False, " AND (GROUP_SECONDARY = 'Sundry Creditors' or GROUP_SECONDARY = 'Sundry Debtors' OR GROUP_SECONDARY = 'Loans' OR GROUP_SECONDARY = 'Loans & Advances' OR GROUP_SECONDARY = 'Capital A/C') and ledgers.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.ACC_YEARID = " & YearId)
            If cmbname.Text.Trim <> "" Then fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkdate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkdate.CheckedChanged
        Try
            dtfrom.Enabled = chkdate.CheckState
            dtto.Enabled = chkdate.CheckState
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridregister_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridregister.DoubleClick
        Try
            showform()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform()
        Try
            If gridregister.RowCount > 0 Then
                Dim dtrow As DataRow = gridregister.GetFocusedDataRow
                VIEWFORM(dtrow("TYPE"), True, dtrow("SRNO"), dtrow("REGTYPE"))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Interest Calculator.XLS"
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next

            Dim PERIOD As String = ""
            If chkdate.Checked = False Then
                PERIOD = AccFrom & " - " & AccTo
            Else
                PERIOD = dtfrom.Value.Date & " - " & dtto.Value.Date
            End If

            opti.SheetName = "Interest Calculator"
            griddetails.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Interest Calculator", gridregister.VisibleColumns.Count + gridregister.GroupCount, cmbname.Text.Trim, PERIOD)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTDAYS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTDAYS.KeyPress
        numkeypress(e, TXTDAYS, Me)
    End Sub

    Private Sub TXTPERCENT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPERCENT.KeyPress
        numdotkeypress(e, TXTPERCENT, Me)
    End Sub
End Class