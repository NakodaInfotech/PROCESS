
Imports BL
Imports System.IO
Imports System.Net

Public Class OutstandingReminderSMS
    Public edit As Boolean

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSENDSMS.Click
        Try

            If ALLOWSMS = True And TXTDUEDAYS.Text > 0 Then
                Dim OBJCMN As New ClsCommon
                Dim MSG As String
                Dim DT As DataTable = OBJCMN.search(" GROUP_SECONDARY", "", " LEDGERS INNER JOIN GROUPMASTER ON ACC_GROUPID = GROUP_ID AND ACC_CMPID = GROUP_CMPID AND ACC_LOCATIONID = GROUP_LOCATIONID AND ACC_YEARID = GROUP_YEARID", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    'If DT.Rows(0).Item(0) = "Bank A/C" Or DT.Rows(0).Item(0) = "Bank OD A/C" Then
                    '    MSG = MSG + "Chq No - " & TXTCHQNO.Text.Trim & Chr(13) & " - " & ACCDATE.Text & Chr(13)
                    'End If
                End If
                If SENDMSG(TXTMESSAGE.Text.Trim, TXTDUEDAYS.Text.Trim) = "1701" Then

                    MsgBox("Message Sent")

                    CMBAGENT.Text = ""
                    cmbname.Text = ""
                    TXTMESSAGE.Clear()
                    TXTDUEDAYS.Clear()
                Else
                    MsgBox("Error Sending Message")
                End If
            End If
            TXTDUEDAYS.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBAGENT.Enter
        If CMBAGENT.Text.Trim = "" Then fillledger(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
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

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then fillname(cmbname, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub clear()
        CMBAGENT.Text = ""
        cmbname.Text = ""
        TXTMESSAGE.Clear()
        TXTDUEDAYS.Clear()
    End Sub
End Class