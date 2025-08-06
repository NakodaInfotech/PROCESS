Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class RollsReceivedDesign

    Public WHERECLAUSE As String
    Public FRMSTRING As String
    Public PERIOD As String


    Dim RPTROLLRECDDTLS As New RollsRecdDtlsReport
    Dim RPTROLLRECDSUMM As New RollsRecdSummReport

    Dim RPTWARPERWISEDTLS As New RollsRecdWarperDtlsReport
    Dim RPTWARPERWISESUMM As New RollsRecdWarperSummReport

    Dim RPTMILLWISEDTLS As New RollsRecdMillDtlsReport
    Dim RPTMILLWISESUMM As New RollsRecdMillSummReport

    Dim RPTQUALITYWISEDTLS As New RollsRecdQualityDtlsReport
    Dim RPTQUALITYWISESUMM As New RollsRecdQualitySummReport

    Private Sub RollsReceivedDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RollsReceivedDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Cursor.Current = Cursors.WaitCursor

            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table


            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            If FRMSTRING = "ROLLSRECDDTLS" Then crTables = RPTROLLRECDDTLS.Database.Tables
            If FRMSTRING = "ROLLSRECDSUMM" Then crTables = RPTROLLRECDSUMM.Database.Tables

            If FRMSTRING = "WARPERWISEDTLS" Then crTables = RPTWARPERWISEDTLS.Database.Tables
            If FRMSTRING = "WARPERWISESUMM" Then crTables = RPTWARPERWISESUMM.Database.Tables

            If FRMSTRING = "MILLWISEDTLS" Then crTables = RPTMILLWISEDTLS.Database.Tables
            If FRMSTRING = "MILLWISESUMM" Then crTables = RPTMILLWISESUMM.Database.Tables

            If FRMSTRING = "QUALITYWISEDTLS" Then crTables = RPTQUALITYWISEDTLS.Database.Tables
            If FRMSTRING = "QUALITYWISESUMM" Then crTables = RPTQUALITYWISESUMM.Database.Tables


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "ROLLSRECDDTLS" Then
                CRPO.ReportSource = RPTROLLRECDDTLS
                RPTROLLRECDDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "ROLLSRECDSUMM" Then
                CRPO.ReportSource = RPTROLLRECDSUMM
                RPTROLLRECDSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "WARPERWISEDTLS" Then
                CRPO.ReportSource = RPTWARPERWISEDTLS
                RPTWARPERWISEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "WARPERWISESUMM" Then
                CRPO.ReportSource = RPTWARPERWISESUMM
                RPTWARPERWISESUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "MILLWISEDTLS" Then
                CRPO.ReportSource = RPTMILLWISEDTLS
                RPTMILLWISEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "MILLWISESUMM" Then
                CRPO.ReportSource = RPTMILLWISESUMM
                RPTMILLWISESUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                CRPO.ReportSource = RPTQUALITYWISEDTLS
                RPTQUALITYWISEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                CRPO.ReportSource = RPTQUALITYWISESUMM
                RPTQUALITYWISESUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            End If

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
End Class