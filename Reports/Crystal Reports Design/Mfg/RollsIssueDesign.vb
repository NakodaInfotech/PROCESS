
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class RollsIssueDesign

    Public WHERECLAUSE As String = ""
    Public FRMSTRING As String = ""
    Public PERIOD As String = ""

    Dim RPTROLLSISSUEDTLS As New RollsIssueDtlsReport
    Dim RPTROLLSISSUESUMM As New RollsIssueSummReport
    Dim RPTROLLSQUALITYDTLS As New RollsIssueQualityDtlsReport
    Dim RPTROLLSQUALITYSUMM As New RollsIssueQualitySummReport
    Dim RPTROLLSMILLDTLS As New RollsIssueMillDtlsReport
    Dim RPTROLLSMILLSUMM As New RollsIssueMillSummReport
    Dim RPTROLLSSIZERDTLS As New RollsIssueSizerDtlsReport
    Dim RPTROLLSSIZERSUMM As New RollsIssueSizerSummReport
    Dim RPTROLLSTRANSDTLS As New RollsIssueTransDtlsReport
    Dim RPTROLLSTRANSSUMM As New RollsIssueTransSummReport

    Private Sub RollsIssueDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub RollsIssueDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If FRMSTRING = "ROLLSISSUEDTLS" Then
                crTables = RPTROLLSISSUEDTLS.Database.Tables
            ElseIf FRMSTRING = "ROLLSISSUESUMM" Then
                crTables = RPTROLLSISSUESUMM.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                crTables = RPTROLLSQUALITYDTLS.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                crTables = RPTROLLSQUALITYSUMM.Database.Tables
            ElseIf FRMSTRING = "MILLWISEDTLS" Then
                crTables = RPTROLLSMILLDTLS.Database.Tables
            ElseIf FRMSTRING = "MILLWISESUMM" Then
                crTables = RPTROLLSMILLSUMM.Database.Tables
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                crTables = RPTROLLSSIZERDTLS.Database.Tables
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                crTables = RPTROLLSSIZERSUMM.Database.Tables
            ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                crTables = RPTROLLSTRANSDTLS.Database.Tables
            ElseIf FRMSTRING = "TRANSWISESUMM" Then
                crTables = RPTROLLSTRANSSUMM.Database.Tables
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "ROLLSISSUEDTLS" Then
                RPTROLLSISSUEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' ROLLS ISSUE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTROLLSISSUEDTLS
            ElseIf FRMSTRING = "ROLLSISSUESUMM" Then
                RPTROLLSISSUESUMM.DataDefinition.FormulaFields("PERIOD").Text = "' ROLLS ISSUE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTROLLSISSUESUMM
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                RPTROLLSQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTROLLSQUALITYDTLS
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                RPTROLLSQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTROLLSQUALITYSUMM
            ElseIf FRMSTRING = "MILLWISEDTLS" Then
                RPTROLLSMILLDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTROLLSMILLDTLS
            ElseIf FRMSTRING = "MILLWISESUMM" Then
                RPTROLLSMILLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTROLLSMILLSUMM
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                RPTROLLSSIZERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' SIZZER WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTROLLSSIZERDTLS
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                RPTROLLSSIZERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' SIZER WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTROLLSSIZERSUMM
            ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                RPTROLLSTRANSDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTROLLSTRANSDTLS
            ElseIf FRMSTRING = "TRANSWISESUMM" Then
                RPTROLLSTRANSSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTROLLSTRANSSUMM
            End If

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub


    Private Sub SendMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendMail.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()

        Try
            Dim objmail As New SendMail
            objmail.attachment = Application.StartupPath & "\Rolls Issue Details.PDF"
            If emailid <> "" Then
                objmail.cmbfirstadd.Text = emailid
            End If
            objmail.Show()
            objmail.BringToFront()
        Catch ex As Exception
            Throw ex
        End Try
        Windows.Forms.Cursor.Current = Cursors.Arrow
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\Rolls Issue Details.PDF"

            If FRMSTRING = "ROLLSISSUEDTLS" Then
                expo = RPTROLLSISSUEDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTROLLSISSUEDTLS.Export()
            ElseIf FRMSTRING = "ROLLSISSUESUMM" Then
                expo = RPTROLLSISSUESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTROLLSISSUESUMM.Export()
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                expo = RPTROLLSQUALITYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTROLLSQUALITYDTLS.Export()
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                expo = RPTROLLSQUALITYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTROLLSQUALITYSUMM.Export()
            ElseIf FRMSTRING = "MILLWISEDTLS" Then
                expo = RPTROLLSMILLDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTROLLSMILLDTLS.Export()
            ElseIf FRMSTRING = "MILLWISESUMM" Then
                expo = RPTROLLSMILLSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTROLLSMILLSUMM.Export()
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                expo = RPTROLLSSIZERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTROLLSSIZERDTLS.Export()
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                expo = RPTROLLSSIZERSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTROLLSSIZERSUMM.Export()
            ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                expo = RPTROLLSTRANSDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTROLLSTRANSDTLS.Export()
            ElseIf FRMSTRING = "TRANSWISESUMM" Then
                expo = RPTROLLSTRANSSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTROLLSTRANSSUMM.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class