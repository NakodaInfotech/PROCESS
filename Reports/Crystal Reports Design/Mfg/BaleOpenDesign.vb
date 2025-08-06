
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class BaleOpenDesign
    Public WHERECLAUSE As String
    Public FRMSTRING As String
    Public PERIOD As String

    Dim RPTBaleOpenDetailsReport As New BaleOpenDetailsReport
    Dim RPTBaleOpenGodownWiseDtlsReport As New BaleOpenGodownWiseDtlsReport
    Dim RPTBaleOpenGodownWiseSummReport As New BaleOpenGodownWiseSummReport

    Dim RPTBaleOpenGreyQualityWiseDtlsReport As New BaleOpenGreyQualityWiseDtlsReport
    Dim RPTBaleOpenGreyQualityWiseSummReport As New BaleOpenGreyQualityWiseSummReport

    Private Sub BaleOpenDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub BaleOpenDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If FRMSTRING = "BALEDTLS" Then
                crTables = RPTBaleOpenDetailsReport.Database.Tables
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                crTables = RPTBaleOpenGodownWiseDtlsReport.Database.Tables
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                crTables = RPTBaleOpenGodownWiseSummReport.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                crTables = RPTBaleOpenGreyQualityWiseDtlsReport.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                crTables = RPTBaleOpenGreyQualityWiseSummReport.Database.Tables
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE


            If FRMSTRING = "BALEDTLS" Then
                RPTBaleOpenDetailsReport.DataDefinition.FormulaFields("PERIOD").Text = "' BALE OPEN - " & PERIOD & "'"
                CRPO.ReportSource = RPTBaleOpenDetailsReport
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                RPTBaleOpenGodownWiseDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' BALE OPEN GODOWN WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTBaleOpenGodownWiseDtlsReport
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                RPTBaleOpenGodownWiseSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' BALE OPEN GODOWN WISE SUMMARY- " & PERIOD & "'"
                CRPO.ReportSource = RPTBaleOpenGodownWiseSummReport
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                RPTBaleOpenGreyQualityWiseDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' BALE OPEN QUALITY WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTBaleOpenGreyQualityWiseDtlsReport
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                RPTBaleOpenGreyQualityWiseSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' BALE OPEN QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTBaleOpenGreyQualityWiseSummReport
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
            objmail.attachment = Application.StartupPath & "\Bale Open Details.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\Bale Recd Details.PDF"

            If FRMSTRING = "BALEDTLS" Then
                expo = RPTBaleOpenDetailsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBaleOpenDetailsReport.Export()
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                expo = RPTBaleOpenGodownWiseDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBaleOpenGodownWiseDtlsReport.Export()
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                expo = RPTBaleOpenGodownWiseSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBaleOpenGodownWiseSummReport.Export()
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                expo = RPTBaleOpenGreyQualityWiseDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBaleOpenGreyQualityWiseDtlsReport.Export()
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                expo = RPTBaleOpenGreyQualityWiseSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBaleOpenGreyQualityWiseSummReport.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

End Class