
Imports BL
Imports System.Windows.Forms
Imports System.IO

Public Class GreyStockWithDyeingFilter

    Dim edit As Boolean
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

    Private Sub CMBNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE ='PROCESSOR'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGREYQUALITY.Enter
        Try
            If CMBGREYQUALITY.Text.Trim = "" Then fillQUALITY(CMBGREYQUALITY, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGREYQUALITY.Validating
        Try
            If CMBGREYQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBGREYQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyStockWithDyeingFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub GreyStockWithDyeingFilter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillcmb()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'PROCESSOR'")
            If CMBGREYQUALITY.Text = "" Then FILLGREY(CMBGREYQUALITY, edit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdshow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Try
            Dim OBJGREY As New GreyStockDyeingDesign
            OBJGREY.MdiParent = MDIMain

            If chkdate.Checked = True Then
                getFromToDate()
                OBJGREY.FROMDATE = dtfrom.Value.Date
                OBJGREY.TODATE = dtto.Value.Date
                OBJGREY.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
            Else
                OBJGREY.FROMDATE = AccFrom.Date
                OBJGREY.TODATE = AccTo.Date
                OBJGREY.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If



            If RDBDYEING.Checked = True Or RDBQUALITY.Checked = True Then
                OBJGREY.WHERECLAUSE = " {GREYSTOCKDYEINGREGISTER.YEARID} = " & YearId

                If CMBNAME.Text.Trim <> "" Then OBJGREY.WHERECLAUSE = OBJGREY.WHERECLAUSE & " and {GREYSTOCKDYEINGREGISTER.NAME}='" & CMBNAME.Text.Trim & "'"
                If Val(TXTLOTNO.Text.Trim) > 0 Then OBJGREY.WHERECLAUSE = OBJGREY.WHERECLAUSE & " and {GREYSTOCKDYEINGREGISTER.LOTNO}=" & Val(TXTLOTNO.Text.Trim)
                If CMBGREYQUALITY.Text.Trim <> "" Then OBJGREY.WHERECLAUSE = OBJGREY.WHERECLAUSE & " and {GREYSTOCKDYEINGREGISTER.GREYQUALITY}='" & CMBGREYQUALITY.Text.Trim & "'"

                If RDBQUALITY.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJGREY.FRMSTRING = "QUALITYWISEDTLS" Else OBJGREY.FRMSTRING = "QUALITYWISESUMM"
                ElseIf RDBDYEING.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJGREY.FRMSTRING = "DYEINGWISEDTLS" Else OBJGREY.FRMSTRING = "DYEINGWISESUMM"
                End If
            End If


            'FOR LOTHISTORY AND LOTREGISTER
            If RDBPENDINGLOT.Checked = True Or RDBLOTHISTORY.Checked = True Then
                OBJGREY.WHERECLAUSE = " {LOTSTATUSVIEW.YEARID} = " & YearId & " and {LOTSTATUSVIEW.FORDYEING} = 1 "
                If CMBNAME.Text.Trim <> "" Then OBJGREY.WHERECLAUSE = OBJGREY.WHERECLAUSE & " and {LOTSTATUSVIEW.PROCESSORNAME}='" & CMBNAME.Text.Trim & "'"
                If CMBGREYQUALITY.Text.Trim <> "" Then OBJGREY.WHERECLAUSE = OBJGREY.WHERECLAUSE & " and {LOTSTATUSVIEW.GREYQUALITY}='" & CMBGREYQUALITY.Text.Trim & "'"
                If Val(TXTLOTNO.Text.Trim) > 0 Then OBJGREY.WHERECLAUSE = OBJGREY.WHERECLAUSE & " and {LOTSTATUSVIEW.LOTNO}=" & Val(TXTLOTNO.Text.Trim)

                If RDBPENDINGLOT.Checked = True Then
                    OBJGREY.WHERECLAUSE = OBJGREY.WHERECLAUSE & " and {LOTSTATUSVIEW.BALANCEPCS} > 0 "
                    OBJGREY.FRMSTRING = "PENDINGLOTREGISTER"
                ElseIf RDBLOTHISTORY.Checked = True Then
                    OBJGREY.WHERECLAUSE = OBJGREY.WHERECLAUSE & " and {LOTSTATUSVIEW.BALANCEPCS} <= 0 "
                    OBJGREY.FRMSTRING = "LOTHISTORY"
                End If
            End If

            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTLOTNO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTLOTNO.KeyPress
        numkeypress(e, TXTLOTNO, Me)
    End Sub
End Class