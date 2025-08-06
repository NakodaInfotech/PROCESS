
Imports BL

Public Class SOFilter

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
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors'")
            If CMBAGENT.Text.Trim = "" Then fillname(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND ACC_TYPE='AGENT'")
            If CMBQUALITY.Text = "" Then FILLGREY(CMBQUALITY, edit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then
                If CMBTYPE.Text.Trim = "YARN" Then
                    fillQUALITY(CMBQUALITY, edit)
                Else
                    FILLGREY(CMBQUALITY, edit)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
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

    Private Sub CMBGREYQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
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

    Private Sub SOFilter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            fillcmb()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SOFilter_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub cmdshow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Try
            Dim OBJSO As New SODesign
            OBJSO.MdiParent = MDIMain
            OBJSO.WHERECLAUSE = " {ALLSALEORDER.SO_yearid}=" & YearId

            If chkdate.Checked = True Then
                getFromToDate()
                OBJSO.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                OBJSO.WHERECLAUSE = OBJSO.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
            Else
                OBJSO.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If

            If CMBTYPE.Text <> "" Then OBJSO.WHERECLAUSE = OBJSO.WHERECLAUSE & " and {ALLSALEORDER.TYPE}='" & CMBTYPE.Text.Trim & "'"
            If CMBNAME.Text <> "" Then OBJSO.WHERECLAUSE = OBJSO.WHERECLAUSE & " and {LEDGERS.ACC_CMPNAME}='" & CMBNAME.Text.Trim & "'"
            If CMBAGENT.Text <> "" Then OBJSO.WHERECLAUSE = OBJSO.WHERECLAUSE & " and {agentledgers.ACC_CMPNAME}='" & CMBAGENT.Text.Trim & "'"
            If CMBQUALITY.Text <> "" Then OBJSO.WHERECLAUSE = OBJSO.WHERECLAUSE & " and {GREYQUALITYMASTER.GREY_NAME}='" & CMBQUALITY.Text.Trim & "'"

            If RBALL.Checked = True Then
                OBJSO.FRMSTRING = "SOALLDATADTLS"
            ElseIf RBPARTY.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJSO.FRMSTRING = "SOPARTYWISEDTLS" Else OBJSO.FRMSTRING = "SOPARTYWISESUMM"
            ElseIf RBAGENT.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJSO.FRMSTRING = "SOAGENTWISEDTLS" Else OBJSO.FRMSTRING = "SOAGENTWISESUMM"
            ElseIf RBGREYQUALITY.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJSO.FRMSTRING = "SOQUALITYWISEDTLS" Else OBJSO.FRMSTRING = "SOQUALITYWISESUMM"
            ElseIf RBPENDING.Checked = True Then
                OBJSO.FRMSTRING = "SOPENDINGDTLS"
                OBJSO.WHERECLAUSE = OBJSO.WHERECLAUSE & " AND ({ALLSALEORDER_DESC.SO_PCS}-{ALLSALEORDER_DESC.SO_OUTPCS}) > 0"
            ElseIf RBCLOSED.Checked = True Then
                OBJSO.FRMSTRING = "SOCLOSEDDTLS"
                OBJSO.WHERECLAUSE = OBJSO.WHERECLAUSE & " AND {ALLSALEORDER_DESC.SO_CLOSED}= TRUE"
            ElseIf RBSOCHALLAN.Checked = True Then
                OBJSO.FRMSTRING = "SOCHALLANDTLS"
            End If

            OBJSO.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBER_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBAGENT.Enter
        Try
            If CMBAGENT.Text.Trim = "" Then fillname(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry CREDITORS' AND ACC_TYPE='AGENT'")
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
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry CREDITORS' AND LEDGERS.ACC_TYPE='AGENT' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBAGENT.Text = OBJLEDGER.TEMPNAME
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class