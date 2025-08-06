
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class BaleReturnDesign

    Public WHERECLAUSE As String
    Public FRMSTRING As String
    Public PERIOD As String

    Dim RPTDTLS As New BaleReturnDetailsReport
    Dim RPTPROCESSORDTLS As New BaleReturnProcessorWiseDtlsReport
    Dim RPTPROCESSORSUMM As New BaleReturnProcessorWiseSummReport

    Dim RPTQUALITYDTLS As New BaleReturnQualityWiseDtlsReport
    Dim RPTQUALITYSUMM As New BaleReturnQualityWiseSummReport

    Dim RPTGODOWNDTLS As New BaleReturnGodownWiseDtlsReport
    Dim RPTGODOWNSUMM As New BaleReturnGodownWiseSummReport


    Private Sub GreyRecdFromProcessingDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub GreyRecdFromProcessingDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                crTables = RPTDTLS.Database.Tables
            ElseIf FRMSTRING = "PROCESSORWISEDTLS" Then
                crTables = RPTPROCESSORDTLS.Database.Tables
            ElseIf FRMSTRING = "PROCESSORWISESUMM" Then
                crTables = RPTPROCESSORSUMM.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                crTables = RPTQUALITYDTLS.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                crTables = RPTQUALITYSUMM.Database.Tables
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                crTables = RPTGODOWNDTLS.Database.Tables
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                crTables = RPTGODOWNSUMM.Database.Tables
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE


            If FRMSTRING = "BALEDTLS" Then
                RPTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BALE RETURN - " & PERIOD & "'"
                CRPO.ReportSource = RPTDTLS
            ElseIf FRMSTRING = "PROCESSORWISEDTLS" Then
                RPTPROCESSORDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BALE RETURN PROCESSOR WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTPROCESSORDTLS
            ElseIf FRMSTRING = "PROCESSORWISESUMM" Then
                RPTPROCESSORSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' BALE RETURN PROCESSOR WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTPROCESSORSUMM
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                RPTQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BALE RETURN QUALITY WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTQUALITYDTLS
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                RPTQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' BALE RETURN QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTQUALITYSUMM
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                RPTGODOWNDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BALE RETURN GODOWN WISE DETAILS- " & PERIOD & "'"
                CRPO.ReportSource = RPTGODOWNDTLS
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                RPTGODOWNSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' BALE RETURN GODOWN WISE SUMMARY- " & PERIOD & "'"
                CRPO.ReportSource = RPTGODOWNSUMM
            End If

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch ex As Exception
            Throw ex
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
            objmail.attachment = Application.StartupPath & "\Bale RETURN Details.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\Bale RETURN Details.PDF"

            If FRMSTRING = "BALEDTLS" Then
                expo = RPTDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDTLS.Export()
            ElseIf FRMSTRING = "PROCESSORWISEDTLS" Then
                expo = RPTPROCESSORDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPROCESSORDTLS.Export()
            ElseIf FRMSTRING = "PROCESSORWISESUMM" Then
                expo = RPTPROCESSORSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPROCESSORSUMM.Export()
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                expo = RPTQUALITYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTQUALITYDTLS.Export()
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                expo = RPTQUALITYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTQUALITYSUMM.Export()
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                expo = RPTGODOWNDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGODOWNDTLS.Export()
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                expo = RPTGODOWNSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGODOWNSUMM.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class
