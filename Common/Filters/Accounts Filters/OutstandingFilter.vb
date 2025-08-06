
Imports BL

Public Class OutstandingFilter
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String
    Public FRMSTRING As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub FILLGROUP(ByVal WHERECLAUSE As String)
        Try
            If CMBGROUP.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("group_name", "", "GroupMaster", " and group_cmpid = " & CmpId & " and group_Locationid = " & Locationid & " and group_Yearid = " & YearId & WHERECLAUSE)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "Group_name"
                    CMBGROUP.DataSource = dt
                    CMBGROUP.DisplayMember = "group_name"
                    CMBGROUP.Text = ""
                End If
                CMBGROUP.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OutstandingFilter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            fillcmb(" AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            fillname(CMBBROKERNAME, False, " and LEDGERS.ACC_TYPE = 'AGENT'")
            FILLGROUP(" AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OutstandingFilter_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.S) Then
                cmdshow_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getFromToDate()
        a1 = DatePart(DateInterval.Day, dtfrom.Value)
        a2 = DatePart(DateInterval.Month, dtfrom.Value)
        a3 = DatePart(DateInterval.Year, dtfrom.Value)
        fromD = "(" & a3 & "," & a2 & "," & a1 & ")"

        a11 = DatePart(DateInterval.Day, dtto.Value)
        a12 = DatePart(DateInterval.Month, dtto.Value)
        a13 = DatePart(DateInterval.Year, dtto.Value)
        toD = "(" & a13 & "," & a12 & "," & a11 & ")"
    End Sub

    Sub fillcmb(ByVal WHERECLAUSE As String)
        Try
            fillname(CMBPARTYNAME, False, WHERECLAUSE)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Try

            Dim OBJOUTSTAND As New OutstandingDesign
            OBJOUTSTAND.MdiParent = MDIMain
            OBJOUTSTAND.selfor_ss = " {@YEARID}=" & YearId

            If chkdate.Checked = True Then
                getFromToDate()
                OBJOUTSTAND.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                If RBBILLDATE.Checked = True Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DATE} in date " & fromD & " to date " & toD & "" Else OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@DUEDATE} in date " & fromD & " to date " & toD & ""
            Else
                OBJOUTSTAND.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If

            If RBALL.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Checked Then
                    If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "OUTSTANDINGALLSUMMREC" Else OBJOUTSTAND.FRMSTRING = "OUTSTANDINGALLSUMMPAY"
                Else
                    OBJOUTSTAND.FRMSTRING = "OUTSTANDINGALLDTLS"
                End If
            ElseIf RBOUTSTANDING.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Checked Then
                    If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "OUTSTANDINGRECSUMM" Else OBJOUTSTAND.FRMSTRING = "OUTSTANDINGPAYSUMM"
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_ALL.BALANCE}>0"
                Else
                    If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "OUTSTANDINGRECDTLS" Else OBJOUTSTAND.FRMSTRING = "OUTSTANDINGPAYDTLS"
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.BALANCE}>0"
                End If
            ElseIf RBONLYPAYMENT.Checked = True Then
                OBJOUTSTAND.FRMSTRING = "ONLYPAYMENTDTLS"
                OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.BALANCE}=0"
            ElseIf RBINT.Checked = True Then
                If Val(TXTDAYS.Text.Trim) = 0 Or Val(TXTINTEREST.Text.Trim) = 0 Then
                    MsgBox("Enter Proper Days / Interest", MsgBoxStyle.Critical)
                    TXTINTEREST.Focus()
                    Exit Sub
                End If
                OBJOUTSTAND.FRMSTRING = "INTOUTSTANDING"
            ElseIf RBINVENTORY.Checked = True Then
                If RBREC.Checked = True Then
                    OBJOUTSTAND.FRMSTRING = "RECINVENTORYOUTSTANDING"
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " AND {OUTSTANDINGREPORT_ALL.DEBIT} - {OUTSTANDINGREPORT_ALL.CREDIT}>0 "
                Else
                    OBJOUTSTAND.FRMSTRING = "PAYINVENTORYOUTSTANDING"
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " AND {OUTSTANDINGREPORT_ALL.CREDIT} - {OUTSTANDINGREPORT_ALL.DEBIT}>0 "
                End If

            ElseIf RBALLBILLS.Checked = True Then
                If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "ALLBILLOUTSTANDINGREC" Else OBJOUTSTAND.FRMSTRING = "ALLBILLOUTSTANDINGPAY"
                OBJOUTSTAND.REPORTNAME = "Outstanding Report (All Bills)"
                OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and ({OUTSTANDINGREPORT_ALL.TYPE}='SALE' OR {OUTSTANDINGREPORT_ALL.TYPE}='PURCHASE' OR {OUTSTANDINGREPORT_ALL.TYPE}='OPENING' OR {OUTSTANDINGREPORT_ALL.TYPE}='DIFF IN OPEN' OR {OUTSTANDINGREPORT_ALL.TYPE}='PAYMENT' OR {OUTSTANDINGREPORT_ALL.TYPE}='RECEIPT')"

            ElseIf RBOUTSTANDINGBILLS.Checked = True Then
                OBJOUTSTAND.REPORTNAME = "Outstanding Report (Only Outstanding Bills)"
                If RBREC.Checked = True Then
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and ({OUTSTANDINGREPORT_ALL.TYPE}='SALE' OR {OUTSTANDINGREPORT_ALL.TYPE}='PURCHASE' OR {OUTSTANDINGREPORT_ALL.TYPE}='OPENING' OR {OUTSTANDINGREPORT_ALL.TYPE}='DIFF IN OPEN') AND {OUTSTANDINGREPORT_ALL.DEBIT} - {OUTSTANDINGREPORT_ALL.CREDIT}>0 "
                    OBJOUTSTAND.FRMSTRING = "ONLYBILLOUTSTANDINGREC"
                ElseIf RBPAYABLE.Checked = True Then
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and ({OUTSTANDINGREPORT_ALL.TYPE}='SALE' OR {OUTSTANDINGREPORT_ALL.TYPE}='PURCHASE' OR {OUTSTANDINGREPORT_ALL.TYPE}='OPENING' OR {OUTSTANDINGREPORT_ALL.TYPE}='DIFF IN OPEN') AND {OUTSTANDINGREPORT_ALL.CREDIT} - {OUTSTANDINGREPORT_ALL.DEBIT}>0 "
                    OBJOUTSTAND.FRMSTRING = "ONLYBILLOUTSTANDINGPAY"
                End If

            ElseIf RBREMINDER.Checked = True Then
                OBJOUTSTAND.FRMSTRING = "REMINDERLETTER"
                OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " AND {OUTSTANDINGREPORT_ALL.BALANCE} > 0"
            ElseIf RBBROKEROUTSTANDING.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Checked Then
                    If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Else OBJOUTSTAND.FRMSTRING = "BROKEROUTSTANDINGPAYSUMM"
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_ALL.BALANCE}>0"
                    If CMBBROKERNAME.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_ALL.AGENT} = '" & CMBBROKERNAME.Text.Trim & "'" Else OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_ALL.AGENT} <> '' "
                Else
                    If RBREC.Checked = True Then OBJOUTSTAND.FRMSTRING = "BROKEROUTSTANDINGRECDTLS" Else OBJOUTSTAND.FRMSTRING = "BROKEROUTSTANDINGPAYDTLS"
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.BALANCE}>0"
                    If CMBBROKERNAME.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.AGENT} = '" & CMBBROKERNAME.Text.Trim & "'" Else OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {OUTSTANDINGREPORT_DETAILS.AGENT} <> '' "
                End If
            End If

            'If RBOLDNEWREPORT.Checked = False Then
            If RBREC.Checked = True Or RBREMINDER.Checked = True Then
                If CMBGROUP.Text.Trim = "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@PARTYGROUP}='SUNDRY DEBTORS'" Else OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@GROUPNAME}='" & CMBGROUP.Text.Trim & "'"
            Else
                If CMBGROUP.Text.Trim = "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@PARTYGROUP}='SUNDRY CREDITORS'" Else OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@GROUPNAME}='" & CMBGROUP.Text.Trim & "'"
            End If

            If CMBPARTYNAME.Text.Trim <> "" Then OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & " and {@NAME}='" & CMBPARTYNAME.Text.Trim & "'"
            If Val(TXTDUDAYS.Text.Trim) > 0 Then OBJOUTSTAND.DUEDAYS = Val(TXTDUDAYS.Text.Trim)

            If RBSELECTED.Checked = True Then
                gridbill.ClearColumnsFilter()
                Dim NAMECLAUSE As String = ""
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If NAMECLAUSE = "" Then
                            If RBGROUP.Checked = True Then NAMECLAUSE = " AND ({@GROUPNAME} = '" & dtrow("NAME") & "'" Else NAMECLAUSE = " AND ({@NAME} = '" & dtrow("NAME") & "'"
                        Else
                            If RBGROUP.Checked = True Then NAMECLAUSE = NAMECLAUSE & " OR {@GROUPNAME} = '" & dtrow("NAME") & "'" Else NAMECLAUSE = NAMECLAUSE & " OR {@NAME} = '" & dtrow("NAME") & "'"
                        End If
                    End If
                Next
                If NAMECLAUSE <> "" Then
                    NAMECLAUSE = NAMECLAUSE & ")"
                    OBJOUTSTAND.selfor_ss = OBJOUTSTAND.selfor_ss & NAMECLAUSE
                End If
            End If

            If RBBILLDATE.Checked = True Then OBJOUTSTAND.DAYS = "BILLDATE" Else OBJOUTSTAND.DAYS = "DUEDATE"
            If CHKADDRESS.Checked = True Then OBJOUTSTAND.ADDRESS = 1 Else OBJOUTSTAND.ADDRESS = 0
            If CHKPRINTDATE.Checked = True Then OBJOUTSTAND.SHOWPRINTDATE = 1 Else OBJOUTSTAND.SHOWPRINTDATE = 0
            If CHKGROUPONNEWPG.Checked = True Then OBJOUTSTAND.NEWPAGE = CHKGROUPONNEWPG.Checked
            OBJOUTSTAND.INTEREST = Val(TXTINTEREST.Text.Trim)
            OBJOUTSTAND.INTDAYS = Val(TXTDAYS.Text.Trim)

            OBJOUTSTAND.Show()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub RBREC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBREC.Click
        Try
            CMBPARTYNAME.Text = ""
            CMBGROUP.Text = ""
            fillcmb(" AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            FILLGROUP(" AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBPAYABLE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBPAYABLE.Click
        Try
            CMBPARTYNAME.Text = ""
            CMBGROUP.Text = ""
            fillcmb(" AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'")
            FILLGROUP(" AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTINTEREST_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTINTEREST.KeyPress
        numdotkeypress(e, TXTINTEREST, Me)
    End Sub

    Private Sub TXTDAYS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTDAYS.KeyPress
        numkeypress(e, TXTDAYS, Me)
    End Sub

    Sub FILLGRID()
        Try
            Dim WHERECLAUSE As String = ""
            If RBREC.Checked = True Then WHERECLAUSE = " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors'" Else WHERECLAUSE = " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'"
            Dim objclsCMST As New ClsCommonMaster

            Dim dt As New DataTable
            If RBACCOUNT.Checked = True Then
                dt = objclsCMST.search(" CAST (0 AS BIT) AS CHK,LEDGERS.Acc_cmpname AS NAME, GROUPMASTER.group_secondary AS UNDER, ISNULL(CITYMASTER.city_name, '') AS CITY  ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id AND LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid LEFT OUTER JOIN CITYMASTER ON LEDGERS.Acc_yearid = CITYMASTER.city_yearid AND LEDGERS.Acc_locationid = CITYMASTER.city_locationid AND LEDGERS.Acc_cmpid = CITYMASTER.city_cmpid AND LEDGERS.Acc_cityid = CITYMASTER.city_id ", WHERECLAUSE & " AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_YEARID = '" & YearId & "') ORDER BY LEDGERS.Acc_cmpname")
            Else
                dt = objclsCMST.search(" CAST (0 AS BIT) AS CHK, group_name AS NAME, group_under AS UNDER, group_secondary AS CITY ", " ", " GROUPMASTER ", " AND (GROUPMASTER.GROUP_CMPID = '" & CmpId & "') AND (GROUPMASTER.GROUP_LOCATIONID = '" & Locationid & "') AND (GROUPMASTER.GROUP_YEARID = '" & YearId & "') ORDER BY group_name")
            End If
            gridbilldetails.DataSource = dt

            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBACCOUNT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBACCOUNT.CheckedChanged
        FILLGRID()
    End Sub

    Private Sub RBGROUP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBGROUP.CheckedChanged
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBSELECTED_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSELECTED.CheckedChanged
        gridbilldetails.Visible = True
        FILLGRID()
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        gridbilldetails.Visible = False
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            If gridbilldetails.Visible = True Then
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    dtrow("CHK") = CHKSELECTALL.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBPAYABLE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBPAYABLE.CheckedChanged
        FILLGRID()
    End Sub

    Private Sub RBREC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBREC.CheckedChanged
        FILLGRID()
    End Sub

    Private Sub CMBBROKERNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBROKERNAME.Enter
        Try
            If CMBBROKERNAME.Text.Trim = "" Then fillname(CMBBROKERNAME, False, " AND LEDGERS.ACC_TYPE = 'AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBROKERNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBROKERNAME.Validating
        Try
            If CMBBROKERNAME.Text.Trim <> "" Then namevalidate(CMBBROKERNAME, cmbacccode, e, Me, txtadd, " AND LEDGERS.ACC_TYPE = 'AGENT' ", "SUNDRY CREDITORS", "AGENT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class