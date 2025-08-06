
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class JobOutDesign

    Public WHERECLAUSE As String
    Public FRMSTRING As String
    Public PERIOD As String

    Dim RPTJOREPORT As New SareeJobOutReport
    Dim RPTJobOutDetailsReport As New JobOutDetailsReport

    Dim RPTJobOutGodownWiseDtlsReport As New JobOutGodownWiseDtlsReport
    Dim RPTJobOutGodownWiseSummReport As New JobOutGodownWiseSummReport

    Dim RPTJobOutJobberWiseDtlsReport As New JobOutJobberWiseDtlsReport
    Dim RPTJobOutJobberWiseSummReport As New JobOutJobberWiseSummReport

    Dim RPTJobOutProcessorWiseDtlsReport As New JobOutProcessorWiseDtlsReport
    Dim RPTJobOutProcessorWiseSummReport As New JobOutProcessorWiseSummReport

    Private Sub JobOutDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub JobOutDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If FRMSTRING = "JOBOUT" Then crTables = RPTJOREPORT.Database.Tables
            If FRMSTRING = "JOBOUTDTLS" Then crTables = RPTJobOutDetailsReport.Database.Tables
            If FRMSTRING = "GODOWNWISEDTLS" Then crTables = RPTJobOutGodownWiseDtlsReport.Database.Tables
            If FRMSTRING = "GODOWNWISESUMM" Then crTables = RPTJobOutGodownWiseSummReport.Database.Tables
            If FRMSTRING = "PROCESSORWISEDTLS" Then crTables = RPTJobOutProcessorWiseDtlsReport.Database.Tables
            If FRMSTRING = "PROCESSORWISESUMM" Then crTables = RPTJobOutProcessorWiseSummReport.Database.Tables
            If FRMSTRING = "JOBBERWISEDTLS" Then crTables = RPTJobOutJobberWiseDtlsReport.Database.Tables
            If FRMSTRING = "JOBBERWISESUMM" Then crTables = RPTJobOutJobberWiseSummReport.Database.Tables


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE


            If FRMSTRING = "JOBOUT" Then
                CRPO.ReportSource = RPTJOREPORT
            ElseIf FRMSTRING = "JOBOUTDTLS" Then
                RPTJobOutDetailsReport.DataDefinition.FormulaFields("PERIOD").Text = "' JOB OUT - " & PERIOD & "'"
                CRPO.ReportSource = RPTJobOutDetailsReport
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                RPTJobOutGodownWiseDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' JOB OUT GODOWN WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTJobOutGodownWiseDtlsReport
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                RPTJobOutGodownWiseSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' JOB OUT GODOWN WISE SUMMARY- " & PERIOD & "'"
                CRPO.ReportSource = RPTJobOutGodownWiseSummReport
            ElseIf FRMSTRING = "PROCESSORWISEDTLS" Then
                RPTJobOutProcessorWiseDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' JOB OUT PROCESSOR WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTJobOutProcessorWiseDtlsReport
            ElseIf FRMSTRING = "PROCESSORWISESUMM" Then
                RPTJobOutProcessorWiseSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' JOB OUT PROCESSOR WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTJobOutProcessorWiseSummReport
            ElseIf FRMSTRING = "JOBBERWISEDTLS" Then
                RPTJobOutJobberWiseDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' JOB OUT JOBBER WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTJobOutJobberWiseDtlsReport
            ElseIf FRMSTRING = "JOBBERWISESUMM" Then
                RPTJobOutJobberWiseSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' JOB OUT JOBBER WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTJobOutJobberWiseSummReport
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
            objmail.attachment = Application.StartupPath & "\Job Out.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\Job Out.PDF"

            If FRMSTRING = "JOBOUT" Then
                expo = RPTJOREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJOREPORT.Export()
            ElseIf FRMSTRING = "JOBOUTDTLS" Then
                expo = RPTJobOutDetailsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJobOutDetailsReport.Export()
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                expo = RPTJobOutGodownWiseDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJobOutGodownWiseDtlsReport.Export()
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                expo = RPTJobOutGodownWiseSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJobOutGodownWiseSummReport.Export()
            ElseIf FRMSTRING = "PROCESSORWISEDTLS" Then
                expo = RPTJobOutProcessorWiseDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJobOutProcessorWiseDtlsReport.Export()
            ElseIf FRMSTRING = "PROCESSORWISESUMM" Then
                expo = RPTJobOutProcessorWiseSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJobOutProcessorWiseSummReport.Export()
            ElseIf FRMSTRING = "JOBBERWISEDTLS" Then
                expo = RPTJobOutJobberWiseDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJobOutJobberWiseDtlsReport.Export()
            ElseIf FRMSTRING = "JOBBERWISESUMM" Then
                expo = RPTJobOutJobberWiseSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJobOutJobberWiseSummReport.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub



End Class