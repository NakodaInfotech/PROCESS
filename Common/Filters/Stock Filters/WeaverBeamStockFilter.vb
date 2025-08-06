
Imports BL

Public Class WeaverBeamStockFilter

    Public FRMSTRING As String
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

    Private Sub WeaverBeamStockFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub WeaverBeamStockFilter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FILLCMB()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        ''''''''''''******* CHANGE ACC_SUBTYPE *******'''''''''
        If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'") ''''TYPE = WARPER
        If CMBBEAMNAME.Text = "" Then fillBEAM(CMBBEAMNAME, False)
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

    Private Sub CMBBEAMNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBEAMNAME.Enter
        Try
            If CMBBEAMNAME.Text.Trim = "" Then fillBEAM(CMBBEAMNAME, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBBEAMNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJBEAM As New SelectBeam
                OBJBEAM.ShowDialog()
                If OBJBEAM.TEMPNAME <> "" Then CMBBEAMNAME.Text = OBJBEAM.TEMPNAME
            End If
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
            Dim OBJBEAM As New BeamStockDesign
            OBJBEAM.MdiParent = MDIMain


            OBJBEAM.WHERECLAUSE = " {WEAVERBEAMSTOCKREGISTER.YEARID}=" & YearId

            If chkdate.Checked = True Then
                getFromToDate()
                OBJBEAM.FROMDATE = dtfrom.Value.Date
                OBJBEAM.TODATE = dtto.Value.Date
                OBJBEAM.PERIOD = Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
            Else
                OBJBEAM.FROMDATE = AccFrom.Date
                OBJBEAM.TODATE = AccTo.Date
                OBJBEAM.PERIOD = Format(AccFrom, "dd/MM/yyyy") & " - " & Format(AccTo, "dd/MM/yyyy")
            End If

            If CMBNAME.Text.Trim <> "" Then OBJBEAM.WHERECLAUSE = OBJBEAM.WHERECLAUSE & " and {WEAVERBEAMSTOCKREGISTER.WEAVERNAME}='" & CMBNAME.Text.Trim & "'"
            If CMBBEAMNAME.Text.Trim <> "" Then OBJBEAM.WHERECLAUSE = OBJBEAM.WHERECLAUSE & " and {WEAVERBEAMSTOCKREGISTER.BEAMNAME}='" & CMBBEAMNAME.Text.Trim & "'"
            If TXTBEAMNO.Text.Trim <> "" Then OBJBEAM.WHERECLAUSE = OBJBEAM.WHERECLAUSE & " and {WEAVERBEAMSTOCKREGISTER.BEAMNO}='" & TXTBEAMNO.Text.Trim & "'"
            If CHKBALANCE.CheckState = CheckState.Checked Then OBJBEAM.ONLYBALANCE = 1 Else OBJBEAM.ONLYBALANCE = 0


            'IF NAME FILTER IS BLANK THEN WE NEED TO FETCH ONLY THOSE WEAVERS WHOSE BALANCE>0
            If CMBNAME.Text.Trim = "" And CHKBALANCE.CheckState = CheckState.Checked Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("WEAVERNAME", "", " WEAVERBEAMSTOCKREGISTER ", " AND YEARID = " & YearId & " AND DATE <= '" & Format(dtto.Value.Date, "MM/dd/yyyy") & "' GROUP BY WEAVERNAME HAVING ROUND(ISNULL(sum(CUT)-SUM(RECD)-SUM([RETURN])-SUM(WASTAGE),0),2) > 0 ORDER BY WEAVERNAME")
                Dim NAMECLAUSE As String = ""
                For Each DTROW As DataRow In DT.Rows
                    If NAMECLAUSE = "" Then
                        NAMECLAUSE = " AND ({WEAVERBEAMSTOCKREGISTER.WEAVERNAME}='" & DTROW("WEAVERNAME") & "'"
                    Else
                        NAMECLAUSE = NAMECLAUSE & " OR {WEAVERBEAMSTOCKREGISTER.WEAVERNAME}='" & DTROW("WEAVERNAME") & "'"
                    End If

                Next
                If NAMECLAUSE <> "" Then
                    NAMECLAUSE = NAMECLAUSE & ")"
                    OBJBEAM.WHERECLAUSE = OBJBEAM.WHERECLAUSE & NAMECLAUSE
                End If
            End If


            If RBDETAILS.Checked = True Then
                OBJBEAM.FRMSTRING = "WEAVERBEAMDTLS"
            ElseIf RBBEAMWEAVERWISE.Checked = True Then
                OBJBEAM.FRMSTRING = "BEAMWISEWEAVERSTOCK"
            ElseIf RBWEAVERBEAMWISE.Checked = True Then
                OBJBEAM.FRMSTRING = "WEAVERWISEBEAMSTOCK"
            ElseIf RBBEAMINOUT.Checked = True Then
                OBJBEAM.FRMSTRING = "BEAMINOUT"
            ElseIf RBBEAMCUTBALANCE.Checked = True Then
                OBJBEAM.FRMSTRING = "BEAMCUTBALANCE"
            End If
            OBJBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class