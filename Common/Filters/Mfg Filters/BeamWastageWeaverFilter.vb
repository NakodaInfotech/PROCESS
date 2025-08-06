
Imports BL

Public Class BeamWastageWeaverFilter

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
            If CMBWEAVER.Text.Trim = "" Then fillname(CMBWEAVER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
            If CMBBEAMNAME.Text = "" Then fillBEAM(CMBBEAMNAME, edit)

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBWEAVER.Enter
        Try
            If CMBWEAVER.Text.Trim = "" Then fillname(CMBWEAVER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBWEAVER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBWEAVER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBWEAVER.Validating
        Try
            If CMBWEAVER.Text.Trim <> "" Then namevalidate(CMBWEAVER, CMBCODE, e, Me, txtadd, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBEAMNAME.Enter
        Try
            If CMBBEAMNAME.Text.Trim = "" Then fillBEAM(CMBBEAMNAME, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBEAMNAME.Validating
        Try
            If CMBBEAMNAME.Text.Trim <> "" Then BEAMVALIDATE(CMBBEAMNAME, e, Me)
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

    Private Sub BeamIssueFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub BeamIssueFilter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillcmb()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub cmdshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Try
            Dim OBJBEAM As New BeamWastageDesign
            OBJBEAM.MdiParent = MDIMain
            OBJBEAM.WHERECLAUSE = " {BEAMWASTAGEWEAVER.BWASWEAVER_yearid}=" & YearId

            If chkdate.Checked = True Then
                getFromToDate()
                OBJBEAM.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                OBJBEAM.WHERECLAUSE = OBJBEAM.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
            Else
                OBJBEAM.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If


            If CMBWEAVER.Text.Trim <> "" Then OBJBEAM.WHERECLAUSE = OBJBEAM.WHERECLAUSE & " and {LEDGERS.ACC_CMPNAME}='" & CMBWEAVER.Text.Trim & "'"
            If CMBBEAMNAME.Text.Trim <> "" Then OBJBEAM.WHERECLAUSE = OBJBEAM.WHERECLAUSE & " and {BEAMMASTER.BEAM_NAME}='" & CMBBEAMNAME.Text.Trim & "'"

            If RDDETAILS.Checked = True Then
                OBJBEAM.FRMSTRING = "BEAMWASTAGEALLDATAWEAVER"
            ElseIf RDBWEAVWER.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJBEAM.FRMSTRING = "WEAVERWISEDTLS" Else OBJBEAM.FRMSTRING = "WEAVERWISESUMM"
            ElseIf RDBBEAM.Checked = True Then
                If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJBEAM.FRMSTRING = "BEAMWISEDTLSWEAVER" Else OBJBEAM.FRMSTRING = "BEAMWISESUMMWEAVER"
            End If

            OBJBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class