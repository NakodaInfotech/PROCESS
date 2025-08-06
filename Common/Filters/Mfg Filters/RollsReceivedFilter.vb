Imports BL

Public Class RollsReceivedFilter
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

    Sub fillcmb()
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, edit)
            If CMBWARPER.Text.Trim = "" Then fillname(CMBWARPER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')")
            If CMBMILLNAME.Text.Trim = "" Then fillname(CMBMILLNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'")
            If CMBQUALITY.Text = "" Then fillQUALITY(CMBQUALITY, edit)

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim <> "" Then fillGODOWN(CMBGODOWN, edit)
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
                OBJGODOWN.SEARCH = ""
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me, " ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWARPER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBWARPER.Enter
        Try
            If CMBWARPER.Text.Trim = "" Then fillname(CMBWARPER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWARPER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBWARPER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBWARPER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWARPER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBWARPER.Validating
        Try
            If CMBWARPER.Text.Trim <> "" Then namevalidate(CMBWARPER, CMBCODE, e, Me, txtadd, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBMILLNAME.Enter
        Try
            If CMBMILLNAME.Text = "" Then fillname(CMBMILLNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBMILLNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILLNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILLNAME.Validating
        Try
            If CMBMILLNAME.Text.Trim = "" Then fillname(CMBMILLNAME, edit, "AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJQUALITY As New SelectQuality
                OBJQUALITY.ShowDialog()
                If OBJQUALITY.TEMPNAME <> "" Then CMBQUALITY.Text = OBJQUALITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
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

    Private Sub RollsReceivedFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub RollsReceivedFilter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillcmb()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Try

            If RDROLLSRECVREGISTER.Checked = True Then

                Dim OBJREG As New RollsRecdRegisterDetails

                If chkdate.Checked = True Then
                    OBJREG.WHERECLAUSE = OBJREG.WHERECLAUSE & " and ROLLRECEIVED.ROLLRECD_DATE BETWEEN '" & dtfrom.Value.Date & "' AND '" & dtto.Value.Date & "'"
                End If
                If CMBGODOWN.Text.Trim <> "" Then OBJREG.WHERECLAUSE = OBJREG.WHERECLAUSE & " and GODOWNMASTER.GODOWN_NAME = '" & CMBGODOWN.Text.Trim & "'"
                If CMBWARPER.Text.Trim <> "" Then OBJREG.WHERECLAUSE = OBJREG.WHERECLAUSE & " and WARPERLEDGERS.ACC_CMPNAME = '" & CMBWARPER.Text.Trim & "'"
                If CMBQUALITY.Text.Trim <> "" Then OBJREG.WHERECLAUSE = OBJREG.WHERECLAUSE & " and QUALITYMASTER.QUALITY_NAME = '" & CMBQUALITY.Text.Trim & "'"
                If CMBMILLNAME.Text.Trim <> "" Then OBJREG.WHERECLAUSE = OBJREG.WHERECLAUSE & " and MILLLEDGERS.ACC_CMPNAME = '" & CMBMILLNAME.Text.Trim & "'"
                OBJREG.MdiParent = MDIMain
                OBJREG.Show()
                Exit Sub
            End If




            Dim OBJROLL As New RollsReceivedDesign
            OBJROLL.MdiParent = MDIMain
            OBJROLL.WHERECLAUSE = " {ROLLRECEIVED.ROLLRECD_yearid}=" & YearId

            If chkdate.Checked = True Then
                getFromToDate()
                OBJROLL.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                OBJROLL.WHERECLAUSE = OBJROLL.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
            Else
                OBJROLL.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If

            If CMBGODOWN.Text.Trim <> "" Then OBJROLL.WHERECLAUSE = OBJROLL.WHERECLAUSE & " and {GODOWNMASTER.GODOWN_NAME}='" & CMBGODOWN.Text.Trim & "'"
            If CMBWARPER.Text.Trim <> "" Then OBJROLL.WHERECLAUSE = OBJROLL.WHERECLAUSE & " and {LEDGERS.ACC_CMPNAME}='" & CMBWARPER.Text.Trim & "'"
            If CMBMILLNAME.Text.Trim <> "" Then OBJROLL.WHERECLAUSE = OBJROLL.WHERECLAUSE & " and {MILLLEDGERS.ACC_CMPNAME}='" & CMBMILLNAME.Text.Trim & "'"
            If CMBQUALITY.Text.Trim <> "" Then OBJROLL.WHERECLAUSE = OBJROLL.WHERECLAUSE & " and {QUALITYMASTER.QUALITY_NAME}='" & CMBQUALITY.Text.Trim & "'"

            If RDDETAILS.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJROLL.FRMSTRING = "ROLLSRECDDTLS" Else OBJROLL.FRMSTRING = "ROLLSRECDSUMM"
            ElseIf RDBWARPER.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJROLL.FRMSTRING = "WARPERWISEDTLS" Else OBJROLL.FRMSTRING = "WARPERWISESUMM"
            ElseIf RDBMILL.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJROLL.FRMSTRING = "MILLWISEDTLS" Else OBJROLL.FRMSTRING = "MILLWISESUMM"
            ElseIf RDBQUALITY.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJROLL.FRMSTRING = "QUALITYWISEDTLS" Else OBJROLL.FRMSTRING = "QUALITYWISESUMM"
            End If
            OBJROLL.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class