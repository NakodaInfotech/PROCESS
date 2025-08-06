
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class ProgramDesign

    Public WHERECLAUSE As String = ""
    Public FRMSTRING As String = ""
    Public PERIOD As String = ""
    Public PROGRAMNO As Integer

    Dim RPTPROG As New ProgramReport
    Dim RPTPROGDTLS As New ProgramDetailsReport
    Dim RPTPROGQUALITY As New ProgramQualityWiseReport
    Dim RPTPROGMILL As New ProgramMillWiseReport
    Dim RPTPROGWARPER As New ProgramWarperWiseReport

    Private Sub ProgramDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub CRPO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CRPO.Load
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

            If FRMSTRING = "PROGRAMREPORT" Then
                crTables = RPTPROG.Database.Tables
            ElseIf FRMSTRING = "PROGDTLS" Then
                crTables = RPTPROGDTLS.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                crTables = RPTPROGQUALITY.Database.Tables
            ElseIf FRMSTRING = "MILLWISEDTLS" Then
                crTables = RPTPROGMILL.Database.Tables
            ElseIf FRMSTRING = "WARPERWISEDTLS" Then
                crTables = RPTPROGWARPER.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "PROGRAMREPORT" Then
                CRPO.ReportSource = RPTPROG
            ElseIf FRMSTRING = "PROGDTLS" Then
                RPTPROGDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' PROGRAM DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTPROGDTLS
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                RPTPROGQUALITY.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTPROGQUALITY
            ElseIf FRMSTRING = "MILLWISEDTLS" Then
                RPTPROGMILL.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTPROGMILL
            ElseIf FRMSTRING = "WARPERWISEDTLS" Then
                RPTPROGWARPER.DataDefinition.FormulaFields("PERIOD").Text = "' WARPER WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTPROGWARPER
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
            objmail.attachment = Application.StartupPath & "\Program Details.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\Program Details.PDF"

            If FRMSTRING = "PROGRAMREPORT" Then
                expo = RPTPROG.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPROG.Export()
            ElseIf FRMSTRING = "PROGDTLS" Then
                expo = RPTPROGDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPROGDTLS.Export()
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                expo = RPTPROGQUALITY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPROGQUALITY.Export()
            ElseIf FRMSTRING = "MILLWISEDTLS" Then
                expo = RPTPROGMILL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPROGMILL.Export()
            ElseIf FRMSTRING = "WARPERWISEDTLS" Then
                expo = RPTPROGWARPER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPROGWARPER.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
           If PrintDialog.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PrintDialog.PrinterSettings
            serverprop(Val(PROGRAMNO), Val(PROGRAMNO), "", "PROGRAMREPORT", Val(PRINTDIALOG.PrinterSettings.Copies), PRINTDIALOG)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class