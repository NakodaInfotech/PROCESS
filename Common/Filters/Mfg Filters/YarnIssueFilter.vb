
Imports BL

Public Class YarnIssueFilter

    Public FRMSTRING As String
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

    Private Sub YarnIssueFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub YarnIssueFilter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If FRMSTRING = "ISSUETOWARPER" Then
                LBLNAME.Text = "Warper Name"
                RDBNAME.Text = "Warper Wise"
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                LBLNAME.Text = "Sizer Name"
                RDBNAME.Text = "Sizer Wise"
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                LBLNAME.Text = "Weaver Name"
                RDBNAME.Text = "Weaver Wise"
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                LBLNAME.Text = "Dyeing Name"
                RDBNAME.Text = "Dyeing Wise"
            End If
            FILLCMB()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()

        ''''''''''''******* CHANGE ACC_SUBTYPE *******'''''''''
        If FRMSTRING = "ISSUETOWARPER" Then
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'") ''''TYPE = WARPER
        ElseIf FRMSTRING = "ISSUETOSIZER" Then
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'") ''''TYPE = SIZER
        ElseIf FRMSTRING = "ISSUETOWEAVER" Then
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'") ''''TYPE = WEAVER
        ElseIf FRMSTRING = "ISSUETODYEING" Then
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'DYEING'") ''''TYPE = DYEING
        End If
        If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, False, " AND GODOWN_ISOUR = 'True'")
        If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        If CMBQUALITY.Text = "" Then fillQUALITY(CMBQUALITY, False)
        If CMBMILL.Text.Trim = "" Then fillname(CMBMILL, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'")

    End Sub

    Private Sub CMBOURGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBOURGODOWN.Enter
        Try
            If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, False, " AND GODOWN_ISOUR = 'True'")
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
                OBJGODOWN.SEARCH = " AND GODOWN_ISOUR = 'True'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBOURGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOURGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBOURGODOWN.Validating
        Try
            If CMBOURGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBOURGODOWN, e, Me, " AND GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If FRMSTRING = "ISSUETOWARPER" Then
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'") ''''TYPE = WARPER
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'") ''''TYPE = SIZER
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'") ''''TYPE = WEAVER
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'") ''''TYPE = PROCESSOR
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If FRMSTRING = "ISSUETOWARPER" Then
                If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WARPER")
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "SIZER")
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WEAVER")
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, txtadd, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                If FRMSTRING = "ISSUETOWARPER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'"
                ElseIf FRMSTRING = "ISSUETOSIZER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'"
                ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'"
                ElseIf FRMSTRING = "ISSUETODYEING" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                End If
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, False, "AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'AND LEDGERS.ACC_TYPE= 'TRANSPORT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then namevalidate(cmbtrans, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'", "Sundry Creditors", "TRANSPORT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTRANS.KeyDown
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

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJROLLS As New SelectQuality
                OBJROLLS.ShowDialog()
                If OBJROLLS.TEMPNAME <> "" Then CMBQUALITY.Text = OBJROLLS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then fillname(CMBMILL, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'")
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
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILL.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then namevalidate(CMBMILL, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
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

    Private Sub cmdshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Try
            Dim OBJYARN As New YarnIssueDesign
            OBJYARN.MdiParent = MDIMain


            If FRMSTRING = "ISSUETOWARPER" Then
                OBJYARN.WHERECLAUSE = " {YARNISSUEWARPER.YISSUEWARPER_YEARID}=" & YearId
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                OBJYARN.WHERECLAUSE = " {YARNISSUESIZER.YISSUESIZER_YEARID}=" & YearId
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                OBJYARN.WHERECLAUSE = " {YARNISSUEWEAVER.YISSUEWEAVER_YEARID}=" & YearId
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                OBJYARN.WHERECLAUSE = " {YARNISSUEDYEING.YISSUEDYEING_YEARID}=" & YearId
            End If

            If chkdate.Checked = True Then
                getFromToDate()
                OBJYARN.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
            Else
                OBJYARN.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If

            If CMBOURGODOWN.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {GODOWN.GODOWN_NAME}='" & CMBOURGODOWN.Text.Trim & "'"
            If CMBNAME.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {LEDGERS.ACC_CMPNAME}='" & CMBNAME.Text.Trim & "'"
            If CMBTRANS.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {TRANSLEDGERS.ACC_CMPNAME}='" & CMBTRANS.Text.Trim & "'"
            If CMBQUALITY.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {QUALITYMASTER.QUALITY_NAME}='" & CMBQUALITY.Text.Trim & "'"
            If CMBMILL.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {MILLLEDGERS.ACC_CMPNAME}='" & CMBMILL.Text.Trim & "'"


            If FRMSTRING = "ISSUETOWARPER" Then
                If RDDETAILS.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "WARPERISSUEDTLS" Else OBJYARN.FRMSTRING = "WARPERISSUESUMM"
                ElseIf RDBNAME.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "WARPERWISEDTLS" Else OBJYARN.FRMSTRING = "WARPERWISESUMM"
                ElseIf RDBTRANS.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "WARPERTRANSWISEDTLS" Else OBJYARN.FRMSTRING = "WARPERTRANSWISESUMM"
                ElseIf RDBMILL.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "WARPERMILLWISEDTLS" Else OBJYARN.FRMSTRING = "WARPERMILLWISESUMM"
                ElseIf RDBQUALITY.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "WARPERQUALITYWISEDTLS" Else OBJYARN.FRMSTRING = "WARPERQUALITYWISESUMM"
                End If
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                If RDDETAILS.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "SIZERISSUEDTLS" Else OBJYARN.FRMSTRING = "SIZERISSUESUMM"
                ElseIf RDBNAME.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "SIZERWISEDTLS" Else OBJYARN.FRMSTRING = "SIZERWISESUMM"
                ElseIf RDBTRANS.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "SIZERTRANSWISEDTLS" Else OBJYARN.FRMSTRING = "SIZERTRANSWISESUMM"
                ElseIf RDBMILL.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "SIZERMILLWISEDTLS" Else OBJYARN.FRMSTRING = "SIZERMILLWISESUMM"
                ElseIf RDBQUALITY.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "SIZERQUALITYWISEDTLS" Else OBJYARN.FRMSTRING = "SIZERQUALITYWISESUMM"
                End If
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                If RDDETAILS.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "WEAVERISSUEDTLS" Else OBJYARN.FRMSTRING = "WEAVERISSUESUMM"
                ElseIf RDBNAME.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "WEAVERWISEDTLS" Else OBJYARN.FRMSTRING = "WEAVERWISESUMM"
                ElseIf RDBTRANS.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "WEAVERTRANSWISEDTLS" Else OBJYARN.FRMSTRING = "WEAVERTRANSWISESUMM"
                ElseIf RDBMILL.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "WEAVERMILLWISEDTLS" Else OBJYARN.FRMSTRING = "WEAVERMILLWISESUMM"
                ElseIf RDBQUALITY.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "WEAVERQUALITYWISEDTLS" Else OBJYARN.FRMSTRING = "WEAVERQUALITYWISESUMM"
                End If
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                If RDDETAILS.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "DYEINGISSUEDTLS" Else OBJYARN.FRMSTRING = "DYEINGISSUESUMM"
                ElseIf RDBNAME.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "DYEINGWISEDTLS" Else OBJYARN.FRMSTRING = "DYEINGWISESUMM"
                ElseIf RDBTRANS.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "DYEINGTRANSWISEDTLS" Else OBJYARN.FRMSTRING = "DYEINGTRANSWISESUMM"
                ElseIf RDBMILL.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "DYEINGMILLWISEDTLS" Else OBJYARN.FRMSTRING = "DYEINGMILLWISESUMM"
                ElseIf RDBQUALITY.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJYARN.FRMSTRING = "DYEINGQUALITYWISEDTLS" Else OBJYARN.FRMSTRING = "DYEINGQUALITYWISESUMM"
                End If
            End If
            OBJYARN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class