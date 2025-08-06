
Imports BL

Public Class WeaverYarnStockFilter

    Public FRMSTRING As String
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

    Private Sub WeaverYarnStockFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub WeaverYarnStockFilter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FILLCMB()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        ''''''''''''******* CHANGE ACC_SUBTYPE *******'''''''''
        If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'") ''''TYPE = WARPER
        If CMBQUALITY.Text = "" Then fillQUALITY(CMBQUALITY, False)
        If CMBGREYQUALITY.Text = "" Then FILLGREY(CMBGREYQUALITY, False)

    End Sub

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER' ") ''''TYPE = WARPER
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, txtDeliveryadd, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "")
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

    Private Sub CMBGREYQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGREYQUALITY.Enter
        Try
            If CMBGREYQUALITY.Text.Trim = "" Then FILLGREY(CMBGREYQUALITY, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGREYQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGreyQuality
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBGREYQUALITY.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGREYQUALITY.Validating
        Try
            If CMBGREYQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBGREYQUALITY, e, Me)
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


            'THIS IS FOR YARNLEDGER
            OBJYARN.WHERECLAUSE = " {WEAVERYARNSTOCKREGISTER.YEARID}=" & YearId

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

            If CMBNAME.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {WEAVERYARNSTOCKREGISTER.WEAVERNAME}='" & CMBNAME.Text.Trim & "'"
            If CMBQUALITY.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {WEAVERYARNSTOCKREGISTER.QUALITY}='" & CMBQUALITY.Text.Trim & "'"
            If CMBGREYQUALITY.Text.Trim <> "" Then OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & " and {WEAVERYARNSTOCKREGISTER.GREYQUALITY}='" & CMBGREYQUALITY.Text.Trim & "'"

            'IF NAME FILTER IS BLANK THEN WE NEED TO FETCH ONLY THOSE WEAVERS WHOSE BALANCE>0
            If CMBNAME.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("WEAVERNAME", "", " WEAVERYARNSTOCKREGISTER ", " AND YEARID = " & YearId & " AND DATE <= '" & Format(dtto.Value.Date, "MM/dd/yyyy") & "' GROUP BY WEAVERNAME HAVING ROUND(ISNULL(sum(WT)-SUM(RECD),0),2) > 0 ORDER BY WEAVERNAME")
                Dim NAMECLAUSE As String = ""
                For Each DTROW As DataRow In DT.Rows
                    If NAMECLAUSE = "" Then
                        NAMECLAUSE = " AND ({WEAVERYARNSTOCKREGISTER.WEAVERNAME}='" & DTROW("WEAVERNAME") & "'"
                    Else
                        NAMECLAUSE = NAMECLAUSE & " OR {WEAVERYARNSTOCKREGISTER.WEAVERNAME}='" & DTROW("WEAVERNAME") & "'"
                    End If

                Next
                If NAMECLAUSE <> "" Then
                    NAMECLAUSE = NAMECLAUSE & ")"
                    OBJYARN.WHERECLAUSE = OBJYARN.WHERECLAUSE & NAMECLAUSE
                End If
            End If

            If RBDETAILS.Checked = True Then
                OBJYARN.FRMSTRING = "WEAVERDTLS"
            ElseIf RBBEAMDETAILS.Checked = True Then
                OBJYARN.FRMSTRING = "WEAVERGREYDTLS"
            ElseIf RBSUMMARY.Checked = True Then
                OBJYARN.FRMSTRING = "WEAVERSUMM"
            ElseIf RBWEAVERWISESUMM.Checked = True Then
                OBJYARN.FRMSTRING = "WEAVERQUALITYSUMM"
            End If
            OBJYARN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class