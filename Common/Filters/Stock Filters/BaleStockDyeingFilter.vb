
Imports BL
Imports System.Windows.Forms
Imports System.IO

Public Class BaleStockDyeingFilter

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
            If CMBGREYQUALITY.Text.Trim = "" Then FILLGREY(CMBGREYQUALITY, False, " AND GREY_TYPE = 'FINISHED'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGREYQUALITY.Validating
        Try
            If CMBGREYQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBGREYQUALITY, e, Me, " AND GREY_TYPE = 'FINISHED'")
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

    Private Sub BaleStockWithDyeingFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub BaleStockWithDyeingFilter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillcmb()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'PROCESSOR'")
            If CMBGREYQUALITY.Text = "" Then FILLGREY(CMBGREYQUALITY, edit, " AND GREY_TYPE = 'FINISHED'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdshow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Try
            Dim OBJBALE As New BaleStockDesign
            OBJBALE.MdiParent = MDIMain

            If chkdate.Checked = True Then
                getFromToDate()
                OBJBALE.FROMDATE = dtfrom.Value.Date
                OBJBALE.TODATE = dtto.Value.Date
                OBJBALE.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
                OBJBALE.WHERECLAUSE = OBJBALE.WHERECLAUSE & " and {@DATE} in date " & fromD & " to date " & toD & ""
            Else
                OBJBALE.FROMDATE = AccFrom.Date
                OBJBALE.TODATE = AccTo.Date
                OBJBALE.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If

            If RDBREGISTER.Checked = True Then
                'THIS IS WRITTEN BY GULKIT, COZ WE NEED TO FETCH ONLY THOSE BALE WHICH ARE AT DYEINGN HOUSE
                OBJBALE.WHERECLAUSE = " {BALEHISTORY.GODOWN} = '' AND {BALEHISTORY.YEARID} = " & YearId

                If TXTBALENO.Text.Trim <> "" Then OBJBALE.WHERECLAUSE = OBJBALE.WHERECLAUSE & " and {BALEHISTORY.BALENO}='" & TXTBALENO.Text.Trim & "'"
                If Val(TXTLOTNO.Text.Trim) <> 0 Then OBJBALE.WHERECLAUSE = OBJBALE.WHERECLAUSE & " and {BALEHISTORY.LOTNO}=" & Val(TXTLOTNO.Text.Trim)
                If CMBNAME.Text.Trim <> "" Then OBJBALE.WHERECLAUSE = OBJBALE.WHERECLAUSE & " and {BALEHISTORY.NAME}='" & CMBNAME.Text.Trim & "'"
                If CMBGREYQUALITY.Text.Trim <> "" Then OBJBALE.WHERECLAUSE = OBJBALE.WHERECLAUSE & " and {BALEHISTORY.GREYQUALITY}='" & CMBGREYQUALITY.Text.Trim & "'"

                OBJBALE.FRMSTRING = "BALEHISTORYDYEING"
            End If


            If RDBREGISTER.Checked = False Then
                'THIS IS WRITTEN BY GULKIT, COZ WE NEED TO FETCH ONLY THOSE BALE WHICH ARE AT DYEINGN HOUSE
                OBJBALE.WHERECLAUSE = " {BALESTOCK.GODOWN} = '' AND {BALESTOCK.YEARID} = " & YearId

                If TXTBALENO.Text.Trim <> "" Then OBJBALE.WHERECLAUSE = OBJBALE.WHERECLAUSE & " and {BALESTOCK.BALENO}='" & TXTBALENO.Text.Trim & "'"
                If Val(TXTLOTNO.Text.Trim) <> 0 Then OBJBALE.WHERECLAUSE = OBJBALE.WHERECLAUSE & " and {BALESTOCK.LOTNO}=" & Val(TXTLOTNO.Text.Trim)
                If CMBNAME.Text.Trim <> "" Then OBJBALE.WHERECLAUSE = OBJBALE.WHERECLAUSE & " and {BALESTOCK.NAME}='" & CMBNAME.Text.Trim & "'"
                If CMBGREYQUALITY.Text.Trim <> "" Then OBJBALE.WHERECLAUSE = OBJBALE.WHERECLAUSE & " and {BALESTOCK.GREYQUALITY}='" & CMBGREYQUALITY.Text.Trim & "'"

                If RDBSTOCK.Checked = True Then
                    OBJBALE.FRMSTRING = "BALESTOCKDYEING"
                ElseIf RDBQUALITY.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJBALE.FRMSTRING = "QUALITYWISEDTLS" Else OBJBALE.FRMSTRING = "QUALITYWISESUMM"
                ElseIf RDBDYEING.Checked = True Then
                    If CHKSUMMARY.CheckState = CheckState.Unchecked Then OBJBALE.FRMSTRING = "DYEINGWISEDTLS" Else OBJBALE.FRMSTRING = "DYEINGWISESUMM"
                End If
            End If


            OBJBALE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTLOTNO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTLOTNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub
End Class