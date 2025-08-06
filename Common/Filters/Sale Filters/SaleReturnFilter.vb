
Imports BL

Public Class SaleReturnFilter

    Dim edit As Boolean
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then FILLGREY(CMBQUALITY, edit)
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

    Private Sub CMBQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBEFFECTQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBEFFECTQUALITY.Enter
        Try
            If CMBEFFECTQUALITY.Text.Trim = "" Then FILLGREY(CMBEFFECTQUALITY, edit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBEFFECTQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBEFFECTQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGREY As New SelectGreyQuality
                OBJGREY.ShowDialog()
                If OBJGREY.TEMPNAME <> "" Then CMBEFFECTQUALITY.Text = OBJGREY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBEFFECTQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBEFFECTQUALITY.Validating
        Try
            If CMBEFFECTQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBEFFECTQUALITY, e, Me)
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

    Sub fillcmb()
        Try
            fillregister(cmbregister, " and register_type = 'SALERETURN'")
            If CMBNAME.Text.Trim <> "" Then fillname(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE='ACCOUNTS'")
            If CMBAGENT.Text.Trim = "" Then fillname(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
            If CMBQUALITY.Text.Trim = "" Then FILLGREY(CMBQUALITY, edit)
            If CMBEFFECTQUALITY.Text.Trim = "" Then FILLGREY(CMBEFFECTQUALITY, edit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SaleReturnFilter_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub SaleReturnFilter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            fillcmb()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdshow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Try
            Dim OBJSALE As New SaleReturnDesign
            OBJSALE.MdiParent = MDIMain
            OBJSALE.WHERECLAUSE = " {SALERETURN.SALRET_yearid}=" & YearId

            If chkdate.Checked = True Then
                getFromToDate()
                OBJSALE.FROMDATE = dtfrom.Value.Date
                OBJSALE.TODATE = dtto.Value.Date
                OBJSALE.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
            Else
                OBJSALE.FROMDATE = AccFrom
                OBJSALE.TODATE = AccTo
                OBJSALE.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If

            If cmbregister.Text.Trim <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " AND {REGISTERMASTER.REGISTER_NAME} = '" & cmbregister.Text.Trim & "'"
            If CMBNAME.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {LEDGERS.ACC_CMPNAME}='" & CMBNAME.Text.Trim & "'"
            If CMBAGENT.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {AGENTLEDGERS.ACC_CMPNAME}='" & CMBAGENT.Text.Trim & "'"
            If CMBQUALITY.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {GREYQUALITYMASTER.GREY_NAME}='" & CMBQUALITY.Text.Trim & "'"
            If CMBEFFECTQUALITY.Text <> "" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {EFFECTGREYQUALITYMASTER.GREY_NAME}='" & CMBQUALITY.Text.Trim & "'"


            If RDBDETAILS.Checked = True Then
                OBJSALE.FRMSTRING = "SALERETURNDTLS"
                If Val(TXTRATE.Text.Trim) > 0 Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " AND {SALERETURN_DESC.SALRET_RATE} = " & Val(TXTRATE.Text.Trim)

            ElseIf RDBPARTY.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJSALE.FRMSTRING = "PARTYWISEDTLS" Else OBJSALE.FRMSTRING = "PARTYWISESUMM"

            ElseIf RDBAGENT.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJSALE.FRMSTRING = "JOBBERWISEDTLS" Else OBJSALE.FRMSTRING = "JOBBERWISESUMM"

            ElseIf RDQUALITY.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJSALE.FRMSTRING = "QUALITYWISEDTLS" Else OBJSALE.FRMSTRING = "QUALITYWISESUMM"


            ElseIf RDBGODOWN.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJSALE.FRMSTRING = "GODOWNWISEDTLS" Else OBJSALE.FRMSTRING = "GODOWNWISESUMM"

            ElseIf RDBMONTHLY.Checked = True Then
                OBJSALE.FRMSTRING = "MONTHLY"

            ElseIf RDBREGISTER.Checked = True Then
                OBJSALE.FRMSTRING = "REGISTERDTLS"

            End If

            If TXTAMT.Text <> "" And CMBSIGN.Text <> "" Then
                If CMBSIGN.Text = "=" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {SALERETURN.SALRET_GRANDTOTAL}=" & TXTAMT.Text.Trim & ""
                If CMBSIGN.Text = ">" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {SALERETURN.SALRET_GRANDTOTAL}>" & TXTAMT.Text.Trim & ""
                If CMBSIGN.Text = "<" Then OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & " and {SALERETURN.SALRET_GRANDTOTAL}<" & TXTAMT.Text.Trim & ""
            End If

            gridbill.ClearColumnsFilter()
            Dim NAMECLAUSE As String = ""
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If NAMECLAUSE = "" Then
                        NAMECLAUSE = " AND ({LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                    Else
                        NAMECLAUSE = NAMECLAUSE & " OR {LEDGERS.ACC_CMPNAME} = '" & dtrow("NAME") & "'"
                    End If
                End If
            Next

            If NAMECLAUSE <> "" Then
                NAMECLAUSE = NAMECLAUSE & ")"
                OBJSALE.WHERECLAUSE = OBJSALE.WHERECLAUSE & NAMECLAUSE
            End If

            OBJSALE.SHOWHEADER = CHKHEADER.CheckState
            OBJSALE.SHOWPRINTDATE = CHKPRINTDATE.CheckState
            OBJSALE.NEWPAGE = CHKGROUPONNEWPG.CheckState
            If CHKADDRESS.Checked = True Then OBJSALE.ADDRESS = 1 Else OBJSALE.ADDRESS = 0

            OBJSALE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBAGENT.Enter
        Try
            If CMBAGENT.Text.Trim = "" Then fillledger(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBAGENT.KeyDown
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

    Private Sub TXTAMT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTAMT.KeyPress
        Try
            numkeypress(e, TXTAMT, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBSELECTED_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSELECTED.CheckedChanged
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" CAST (0 AS BIT) AS CHK,LEDGERS.Acc_cmpname AS NAME, GROUPMASTER.group_secondary AS UNDER, ISNULL(CITYMASTER.city_name, '') AS CITY  ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id AND LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid LEFT OUTER JOIN CITYMASTER ON LEDGERS.Acc_yearid = CITYMASTER.city_yearid AND LEDGERS.Acc_locationid = CITYMASTER.city_locationid AND LEDGERS.Acc_cmpid = CITYMASTER.city_cmpid AND LEDGERS.Acc_cityid = CITYMASTER.city_id ", " AND (LEDGERS.ACC_CMPID = '" & CmpId & "') AND (LEDGERS.ACC_LOCATIONID = '" & Locationid & "') AND (LEDGERS.ACC_YEARID = '" & YearId & "') and GROUPMASTER.group_secondary='SUNDRY DEBTORS' ORDER BY LEDGERS.Acc_cmpname")
            gridbilldetails.DataSource = dt


            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRATE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRATE.KeyPress
        numdotkeypress(e, TXTRATE, Me)
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, txtDeliveryadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then namevalidate(CMBAGENT, CMBCODE, e, Me, txtDeliveryadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class