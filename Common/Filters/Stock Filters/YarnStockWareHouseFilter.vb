
Imports System.ComponentModel
Imports BL

Public Class YarnStockWareHouseFilter

    Public FRMSTRING As String
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

    Private Sub YarnStockWareHouseFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub YarnStockWareHouseFilter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FILLCMB()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()

        ''''''''''''******* CHANGE ACC_SUBTYPE *******'''''''''
        If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER' OR LEDGERS.ACC_SUBTYPE = 'WEAVER')") ''''TYPE = WARPER
        If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, False, " AND GODOWN_ISOUR = 'False'")
        If CMBQUALITY.Text = "" Then fillQUALITY(CMBQUALITY, False)
        If CMBMILL.Text.Trim = "" Then fillname(CMBMILL, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'")
        If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR)

    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, False, " AND GODOWN_ISOUR = 'False'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGODOWN.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGodown
                OBJGODOWN.FRMSTRING = "GODOWN"
                OBJGODOWN.SEARCH = " AND GODOWN_ISOUR = 'False'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me, " AND GODOWN_ISOUR = 'False'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER' OR LEDGERS.ACC_SUBTYPE = 'WEAVER')") ''''TYPE = WARPER
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, txtDeliveryadd, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER' OR LEDGERS.ACC_SUBTYPE = 'WEAVER')", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "")
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
            If CMBMILL.Text.Trim <> "" Then namevalidate(CMBMILL, CMBCODE, e, Me, txtDeliveryadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'", "Sundry Creditors", "ACCOUNTS")
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
            Dim OBJYARN As New YarnStockDesign
            OBJYARN.MdiParent = MDIMain

            If chkdate.Checked = True Then
                getFromToDate()
                OBJYARN.FROMDATE = dtfrom.Value.Date
                OBJYARN.TODATE = dtto.Value.Date
                OBJYARN.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
            Else
                OBJYARN.FROMDATE = AccFrom.Date
                OBJYARN.TODATE = AccTo.Date
                OBJYARN.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If

            If RBDOWISE.Checked = True Then
                OBJYARN.WHERECLAUSE = "{STOCKVIEW.BAGS} > 0 AND {STOCKVIEW.YEARID} = " & YearId
                If CMBGODOWN.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {STOCKVIEW.GODOWN}='" & CMBGODOWN.Text.Trim & "'"
                If CMBNAME.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {STOCKVIEW.NAME}='" & CMBNAME.Text.Trim & "'"
                If CMBQUALITY.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {STOCKVIEW.QUALITY}='" & CMBQUALITY.Text.Trim & "'"
                If CMBMILL.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {STOCKVIEW.MILLNAME}='" & CMBMILL.Text.Trim & "'"
                If CMBCOLOR.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {STOCKVIEW.COLOR}='" & CMBCOLOR.Text.Trim & "'"
                OBJYARN.FRMSTRING = "DOWAREHOUSE"
                OBJYARN.Show()
                Exit Sub
            End If



            'THIS IS FOR YARNLEDGER
            OBJYARN.WHERECLAUSE = " {YARNSTOCK_WAREHOUSE.YEARID}=" & YearId

            If CMBGODOWN.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {YARNSTOCK_WAREHOUSE.GODOWN}='" & CMBGODOWN.Text.Trim & "'"
            If CMBNAME.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {YARNSTOCK_WAREHOUSE.NAME}='" & CMBNAME.Text.Trim & "'"
            If CMBQUALITY.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {YARNSTOCK_WAREHOUSE.QUALITY}='" & CMBQUALITY.Text.Trim & "'"
            If CMBMILL.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {YARNSTOCK_WAREHOUSE.MILLNAME}='" & CMBMILL.Text.Trim & "'"
            If CMBCOLOR.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {YARNSTOCK_WAREHOUSE.COLOR}='" & CMBCOLOR.Text.Trim & "'"

            If RBDETAILS.Checked = True Then
                OBJYARN.FRMSTRING = "DETAILSWAREHOUSE"
            ElseIf RBSUMMARY.Checked = True Then
                OBJYARN.FRMSTRING = "SUMMARYWAREHOUSE"
            End If
            OBJYARN.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Enter(sender As Object, e As EventArgs) Handles CMBCOLOR.Enter
        Try
            If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Validating(sender As Object, e As CancelEventArgs) Handles CMBCOLOR.Validating
        Try
            If CMBCOLOR.Text.Trim <> "" Then COLORVALIDATE(CMBCOLOR, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class