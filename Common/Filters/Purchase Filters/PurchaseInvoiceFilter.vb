
Imports BL

Public Class PurchaseInvoiceFilter

    Dim edit As Boolean
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String
    Public FRMSTRING As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            'If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, edit)
            If CMBQUALITY.Text.Trim = "" Then

                If FRMSTRING = "PURCHASEYARN" Then
                    fillQUALITY(CMBQUALITY, edit)
                ElseIf FRMSTRING = "PURCHASEGREY" Then
                    FILLGREY(CMBQUALITY, edit)
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                If FRMSTRING = "PURCHASEYARN" Then

                    Dim OBJQ As New SelectQuality
                    OBJQ.FRMSTRING = "QUALITY"
                    OBJQ.ShowDialog()
                    If OBJQ.TEMPNAME <> "" Then CMBQUALITY.Text = OBJQ.TEMPNAME
                ElseIf FRMSTRING = "PURCHASEGREY" Then
                    Dim OBJGQ As New SelectGreyQuality
                    OBJGQ.FRMSTRING = "GREYQUALITY"
                    OBJGQ.ShowDialog()
                    If OBJGQ.TEMPNAME <> "" Then CMBQUALITY.Text = OBJGQ.TEMPNAME
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then

                If FRMSTRING = "PURCHASEYARN" Then
                    QUALITYVALIDATE(CMBQUALITY, e, Me)
                ElseIf FRMSTRING = "PURCHASEGREY" Then
                    GREYVALIDATE(CMBQUALITY, e, Me)
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBEFFECTQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBEFFECTQUALITY.Enter
        Try
            If CMBEFFECTQUALITY.Text.Trim = "" Then

                If FRMSTRING = "PURCHASEYARN" Then
                    fillQUALITY(CMBEFFECTQUALITY, edit)
                ElseIf FRMSTRING = "PURCHASEGREY" Then
                    FILLGREY(CMBEFFECTQUALITY, edit)
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBEFFECTQUALITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBEFFECTQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                If FRMSTRING = "PURCHASEYARN" Then

                    Dim OBJQ As New SelectQuality
                    OBJQ.FRMSTRING = "QUALITY"
                    OBJQ.ShowDialog()
                    If OBJQ.TEMPNAME <> "" Then CMBEFFECTQUALITY.Text = OBJQ.TEMPNAME
                ElseIf FRMSTRING = "PURCHASEGREY" Then
                    Dim OBJGQ As New SelectGreyQuality
                    OBJGQ.FRMSTRING = "GREYQUALITY"
                    OBJGQ.ShowDialog()
                    If OBJGQ.TEMPNAME <> "" Then CMBEFFECTQUALITY.Text = OBJGQ.TEMPNAME
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBEFFECTQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBEFFECTQUALITY.Validating
        Try
            If CMBEFFECTQUALITY.Text.Trim <> "" Then

                If FRMSTRING = "PURCHASEYARN" Then
                    QUALITYVALIDATE(CMBEFFECTQUALITY, e, Me)
                ElseIf FRMSTRING = "PURCHASEGREY" Then
                    GREYVALIDATE(CMBEFFECTQUALITY, e, Me)
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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
            fillregister(cmbregister, " AND REGISTER_TYPE = 'PURCHASE'")
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
            If CMBAGENT.Text.Trim = "" Then fillname(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")

            If FRMSTRING = "PURCHASEYARN" Then
                If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, edit)
                If CMBEFFECTQUALITY.Text.Trim = "" Then fillQUALITY(CMBEFFECTQUALITY, edit)
                If CMBMILL.Text.Trim = "" Then fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
                If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")

            ElseIf FRMSTRING = "PURCHASEGREY" Then
                If CMBQUALITY.Text.Trim = "" Then FILLGREY(CMBQUALITY, edit)
                If CMBEFFECTQUALITY.Text.Trim = "" Then fillQUALITY(CMBEFFECTQUALITY, edit)
                If CMBMILL.Text.Trim = "" Then fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
                If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")

            End If


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PurchaseInvoiceFilter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If FRMSTRING = "PURCHASEYARN" Then
                LBLNAME.Text = "Mill Name"
                RDBNAME.Text = "Mill Wise"
                LBLQUALITY.Text = "Quality Wise"
                RDQUALITY.Text = "Quality Wise"
                Me.Text = "Purchase Invoice Filter (Yarn)"
            ElseIf FRMSTRING = "PURCHASEGREY" Then
                LBLNAME.Text = "Processor"
                RDBNAME.Text = "Processor Wise"
                LBLQUALITY.Text = "Grey Qual"
                RDQUALITY.Text = "Grey Quality Wise"
                Me.Text = "Purchase Invoice Filter (Grey)"
                RDBTRANS.Visible = False
                RDAVGPURQUALITY.Visible = False
            ElseIf FRMSTRING = "OTHERPURCHASE" Then
                LBLAGENT.Visible = False
                CMBAGENT.Visible = False
                LBLEFFECTQTY.Visible = False
                CMBEFFECTQUALITY.Visible = False
                LBLNAME.Visible = False
                CMBQUALITY.Visible = False
                LBLQUALITY.Visible = False
                CMBMILL.Visible = False
                LBLTRANS.Visible = False
                cmbtrans.Visible = False
                RDBNAME.Visible = False
                RDQUALITY.Visible = False
                RDBTRANS.Visible = False
                RDAVGPURQUALITY.Visible = False
                RDAVGPURMONTHLY.Visible = False
                RDBAGENT.Visible = False
                RDBTRANS.Visible = False
                RDBNAME.Visible = False
                LBLRATE.Visible = False
                TXTRATE.Visible = False
                TXTAMT.Visible = False
                LBLAMT.Visible = False
                CMBSIGN.Visible = False
                Me.Text = "Purchase Invoice Filter (OTHER)"
            End If
            fillcmb()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseInvoiceFilter_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub cmdshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Try
            Dim OBJPUR As New PurchaseInvoiceDesign
            OBJPUR.MdiParent = MDIMain

            'OBJPUR.WHERECLAUSE = " {PURCHASEMASTER.BILL_PURTYPE} = 'YARN PURCHASE' AND {PURCHASEMASTER.BILL_yearid}=" & YearId

            If FRMSTRING = "PURCHASEYARN" Then
                OBJPUR.WHERECLAUSE = " {PURCHASEMASTER.BILL_PURTYPE} = 'YARN PURCHASE' AND {PURCHASEMASTER.BILL_yearid}=" & YearId
            ElseIf FRMSTRING = "PURCHASEGREY" Then
                OBJPUR.WHERECLAUSE = " {PURCHASEMASTER.BILL_PURTYPE} = 'GREY PURCHASE' AND {PURCHASEMASTER.BILL_yearid}=" & YearId
            ElseIf FRMSTRING = "OTHERPURCHASE" Then
                OBJPUR.WHERECLAUSE = " {PURCHASEMASTER.BILL_yearid}=" & YearId
            End If


            If chkdate.Checked = True Then
                getFromToDate()
                OBJPUR.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
            Else
                OBJPUR.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If

            If cmbregister.Text.Trim <> "" Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " AND {REGISTERMASTER.REGISTER_NAME} = '" & cmbregister.Text.Trim & "'"
            If CMBTYPE.Text.Trim <> "" Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " AND {PURCHASEMASTER.BILL_PURTYPE} = '" & CMBTYPE.Text.Trim & "'"
            If CMBNAME.Text <> "" Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " and {LEDGERS.ACC_CMPNAME}='" & CMBNAME.Text.Trim & "'"
            If CMBAGENT.Text <> "" Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " and {AGENTLEDGERS.ACC_CMPNAME}='" & CMBAGENT.Text.Trim & "'"

            If FRMSTRING = "PURCHASEYARN" Then
                If CMBQUALITY.Text <> "" Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " and {QUALITYMASTER.QUALITY_NAME}='" & CMBQUALITY.Text.Trim & "'"
                If CMBEFFECTQUALITY.Text <> "" Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " and {EFFECTQUALITYMASTER.QUALITY_NAME}='" & CMBEFFECTQUALITY.Text.Trim & "'"
            ElseIf FRMSTRING = "PURCHASEGREY" Then
                If CMBQUALITY.Text <> "" Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " and {GREYQUALITYMASTER.GREY_NAME}='" & CMBQUALITY.Text.Trim & "'"
                If CMBEFFECTQUALITY.Text <> "" Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " and {EFFECTGREYQUALITYMASTER.GREY_NAME}='" & CMBEFFECTQUALITY.Text.Trim & "'"
            End If

            
            If CMBMILL.Text <> "" Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " and {MILLLEDGERS.ACC_CMPNAME}='" & CMBMILL.Text.Trim & "'"
            If cmbtrans.Text <> "" Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " and {TRANSLEDGERS.ACC_CMPNAME}='" & cmbtrans.Text.Trim & "'"




            If FRMSTRING = "PURCHASEYARN" Then

                If RDBDETAILS.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then
                        OBJPUR.FRMSTRING = "PURDTLS"
                        If Val(TXTRATE.Text.Trim) > 0 Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " AND {PURCHASEMASTER_DESC.BILL_RATE} = " & Val(TXTRATE.Text.Trim)
                    Else
                        OBJPUR.FRMSTRING = "PURSUMM"
                    End If

                ElseIf RDBPARTY.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJPUR.FRMSTRING = "PARTYWISEDTLS" Else OBJPUR.FRMSTRING = "PARTYWISESUMM"

                ElseIf RDBAGENT.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJPUR.FRMSTRING = "BROKERWISEDTLS" Else OBJPUR.FRMSTRING = "BROKERWISESUMM"

                ElseIf RDQUALITY.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJPUR.FRMSTRING = "QUALITYWISEDTLS" Else OBJPUR.FRMSTRING = "QUALITYWISESUMM"

                ElseIf RDBNAME.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJPUR.FRMSTRING = "MILLWISEDTLS" Else OBJPUR.FRMSTRING = "MILLWISESUMM"

                ElseIf RDBTRANS.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJPUR.FRMSTRING = "TRANSWISEDTLS" Else OBJPUR.FRMSTRING = "TRANSWISESUMM"

                ElseIf RDBMONTHLY.Checked = True Then
                    OBJPUR.FRMSTRING = "MONTHLY"

                ElseIf RDBREGISTER.Checked = True Then
                    OBJPUR.FRMSTRING = "REGISTERDTLS"

                ElseIf RDAVGPURQUALITY.Checked = True Then
                    OBJPUR.FRMSTRING = "AVGPURQUALITYWISESUMM"

                ElseIf RDAVGPURMONTHLY.Checked = True Then
                    OBJPUR.FRMSTRING = "AVGPURMONTHLY"

                End If
            End If


            If FRMSTRING = "PURCHASEGREY" Then

                If RDBDETAILS.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then
                        OBJPUR.FRMSTRING = "PURGREYDTLS"
                        If Val(TXTRATE.Text.Trim) > 0 Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " AND {PURCHASEMASTER_DESC.BILL_RATE} = " & Val(TXTRATE.Text.Trim)
                        'Else
                        '    OBJPUR.FRMSTRING = "PURSUMM"
                    End If

                ElseIf RDBPARTY.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJPUR.FRMSTRING = "PARTYWISEGREYDTLS" Else OBJPUR.FRMSTRING = "PARTYWISEGREYSUMM"

                ElseIf RDBAGENT.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJPUR.FRMSTRING = "BROKERWISEGREYDTLS" Else OBJPUR.FRMSTRING = "BROKERWISEGREYSUMM"

                ElseIf RDQUALITY.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJPUR.FRMSTRING = "QUALITYWISEGREYDTLS" Else OBJPUR.FRMSTRING = "QUALITYWISEGREYSUMM"

                ElseIf RDBNAME.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJPUR.FRMSTRING = "MILLWISEGREYDTLS" Else OBJPUR.FRMSTRING = "MILLWISEGREYSUMM"

                    'ElseIf RDBTRANS.Checked = True Then
                    '    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJPUR.FRMSTRING = "TRANSWISEDTLS" Else OBJPUR.FRMSTRING = "TRANSWISESUMM"

                ElseIf RDBMONTHLY.Checked = True Then
                    OBJPUR.FRMSTRING = "MONTHLYGREY"

                ElseIf RDBREGISTER.Checked = True Then
                    OBJPUR.FRMSTRING = "REGISTERDTLSGREY"

                ElseIf RDAVGPURQUALITY.Checked = True Then
                    OBJPUR.FRMSTRING = "AVGPURQUALITYWISEGREYSUMM"

                ElseIf RDAVGPURMONTHLY.Checked = True Then
                    OBJPUR.FRMSTRING = "AVGPURMONTHLYGREY"

                End If
            End If

            If FRMSTRING = "OTHERPURCHASE" Then
                If RDBDETAILS.Checked = True Then
                    OBJPUR.FRMSTRING = "PURDTLSOTHER"

                ElseIf RDBPARTY.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJPUR.FRMSTRING = "PARTYWISEOTHERDTLS" Else OBJPUR.FRMSTRING = "PARTYWISEOTHERSUMM"
                ElseIf RDBMONTHLY.Checked = True Then
                    OBJPUR.FRMSTRING = "OTHERMONTHLY"
                ElseIf RDBREGISTER.Checked = True Then
                    OBJPUR.FRMSTRING = "OTHERREGISTERDTLS"
                ElseIf RDBGSTREGISTER.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJPUR.FRMSTRING = "GSTREGISTERDTLS" Else OBJPUR.FRMSTRING = "GSTREGISTERSUMM"
                End If
            End If

            If TXTAMT.Text <> "" And CMBSIGN.Text <> "" Then
                If CMBSIGN.Text = "=" Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " and {PURCHASEMASTER.BILL_GRANDTOTAL}=" & TXTAMT.Text.Trim & ""
                If CMBSIGN.Text = ">" Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " and {PURCHASEMASTER.BILL_GRANDTOTAL}>" & TXTAMT.Text.Trim & ""
                If CMBSIGN.Text = "<" Then OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & " and {PURCHASEMASTER.BILL_GRANDTOTAL}<" & TXTAMT.Text.Trim & ""
            End If

            'NAMECLAUSE
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
                OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & NAMECLAUSE
            End If

            'REGISTER GRID
            GRIDBILLR.ClearColumnsFilter()
            Dim REGCLAUSE As String = ""

            For i As Integer = 0 To GRIDBILLR.RowCount - 1
                Dim dtrow As DataRow = GRIDBILLR.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHKR")) = True Then
                    If REGCLAUSE = "" Then
                        REGCLAUSE = " AND ({REGISTERMASTER.REGISTER_NAME} = '" & dtrow("REGISTER") & "'"
                    Else
                        REGCLAUSE = REGCLAUSE & " OR {REGISTERMASTER.REGISTER_NAME} = '" & dtrow("REGISTER") & "'"
                    End If
                End If
            Next

            If REGCLAUSE <> "" Then
                REGCLAUSE = REGCLAUSE & ")"
                OBJPUR.WHERECLAUSE = OBJPUR.WHERECLAUSE & REGCLAUSE
            End If

            OBJPUR.SHOWHEADER = CHKHEADER.CheckState
            OBJPUR.SHOWPRINTDATE = CHKPRINTDATE.CheckState
            OBJPUR.NEWPAGE = CHKGROUPONNEWPG.CheckState
            If CHKADDRESS.Checked = True Then OBJPUR.ADDRESS = 1 Else OBJPUR.ADDRESS = 0

            OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBAGENT.Enter
        Try
            If CMBAGENT.Text.Trim = "" Then fillledger(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
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

    Private Sub cmbtrans_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, edit, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTAMT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTAMT.KeyPress
        Try
            numkeypress(e, TXTAMT, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then

                If FRMSTRING = "PURCHASEYARN" Then
                    fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
                ElseIf FRMSTRING = "PURCHASEGREY" Then
                    fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBSELECTED_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSELECTED.CheckedChanged
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" CAST (0 AS BIT) AS CHK,LEDGERS.Acc_cmpname AS NAME, GROUPMASTER.group_secondary AS UNDER, ISNULL(CITYMASTER.city_name, '') AS CITY  ", " ", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id AND LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid LEFT OUTER JOIN CITYMASTER ON LEDGERS.Acc_yearid = CITYMASTER.city_yearid AND LEDGERS.Acc_locationid = CITYMASTER.city_locationid AND LEDGERS.Acc_cmpid = CITYMASTER.city_cmpid AND LEDGERS.Acc_cityid = CITYMASTER.city_id ", " AND (LEDGERS.ACC_CMPID = '" & CmpId & "') AND (LEDGERS.ACC_LOCATIONID = '" & Locationid & "') AND (LEDGERS.ACC_YEARID = '" & YearId & "') and GROUPMASTER.group_secondary='SUNDRY CREDITORS' ORDER BY LEDGERS.Acc_cmpname")
            gridbilldetails.DataSource = dt


            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
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

    Private Sub TXTRATE_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRATE.KeyPress
        numdotkeypress(e, TXTRATE, Me)
    End Sub

    Private Sub CMBAGENT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then namevalidate(CMBAGENT, CMBCODE, e, Me, txtDeliveryadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then

                If FRMSTRING = "PURCHASEYARN" Then
                    namevalidate(CMBMILL, CMBCODE, e, Me, txtDeliveryadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'")
                ElseIf FRMSTRING = "PURCHASEGREY" Then
                    namevalidate(CMBMILL, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "Sundry Creditors", "ACCOUNTS", "", "", "PROCESSOR")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then namevalidate(cmbtrans, CMBCODE, e, Me, txtDeliveryadd, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKRSELECTALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKRSELECTALL.CheckedChanged
        Try
            If GRIDBILLDETAILSR.Visible = True Then
                For i As Integer = 0 To GRIDBILLR.RowCount - 1
                    Dim dtrow As DataRow = GRIDBILLR.GetDataRow(i)
                    dtrow("CHKR") = CHKRSELECTALL.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RDBRSELECTED_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDBRSELECTED.CheckedChanged
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("CAST (0 AS BIT) AS CHKR, Register_name AS REGISTER", "", "RegisterMaster ", " AND REGISTER_TYPE = 'PURCHASE' and Register_cmpid=" & CmpId & " and REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
            GRIDBILLDETAILSR.DataSource = dt


            If dt.Rows.Count > 0 Then
                GRIDBILLR.FocusedRowHandle = GRIDBILLR.RowCount - 1
                GRIDBILLR.TopRowIndex = GRIDBILLR.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class