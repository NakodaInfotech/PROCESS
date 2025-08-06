
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class GreyRecdFromWeaverDesign

    Public WHERECLAUSE As String
    Public FRMSTRING As String
    Public PERIOD As String

    Dim RPTWEAVERDTLS As New GreyRecdWeaverWiseReport
    Dim RPTWEAVERSUMM As New GreyRecdWeaverWiseSummReport
    Dim RPTWEAVERQUALITYDTLS As New GreyRecdWeaverQualityWiseSummReport
    Dim RPTBEAMDTLS As New GreyRecdBeamWiseReport
    Dim RPTBEAMSUMM As New GreyRecdBeamWiseSummReport
    Dim RPTQUALITYDTLS As New GreyRecdQualityWiseReport
    Dim RPTQUALITYSUMM As New GreyRecdQualityWiseSummReport
    Dim RPTMONTH As New GreyRecdMonthWiseReport

    Private Sub GreyRecdFromWeaverDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub GreyRecdFromWeaverDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If FRMSTRING = "BEAMWISE" Then
                crTables = RPTBEAMDTLS.Database.Tables
            ElseIf FRMSTRING = "BEAMWISESUMM" Then
                crTables = RPTBEAMSUMM.Database.Tables
            ElseIf FRMSTRING = "WEAVERWISE" Then
                crTables = RPTWEAVERDTLS.Database.Tables
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                crTables = RPTWEAVERSUMM.Database.Tables
            ElseIf FRMSTRING = "WEAVERQUALITYWISE" Then
                crTables = RPTWEAVERQUALITYDTLS.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISE" Then
                crTables = RPTQUALITYDTLS.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                crTables = RPTQUALITYSUMM.Database.Tables
            ElseIf FRMSTRING = "MONTHWISE" Then
                crTables = RPTMONTH.Database.Tables
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE


            If FRMSTRING = "BEAMWISE" Then
                RPTBEAMDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' GREY RECD BEAM WISE - " & PERIOD & "'"
                CRPO.ReportSource = RPTBEAMDTLS
            ElseIf FRMSTRING = "BEAMWISESUMM" Then
                RPTBEAMSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' GREY RECD BEAM WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTBEAMSUMM
            ElseIf FRMSTRING = "WEAVERWISE" Then
                RPTWEAVERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' GREY RECD WEAVER WISE - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERDTLS
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                RPTWEAVERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' GREY RECD WEAVER WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERSUMM
            ElseIf FRMSTRING = "WEAVERQUALITYWISE" Then
                RPTWEAVERQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' WEAVER - QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERQUALITYDTLS
            ElseIf FRMSTRING = "QUALITYWISE" Then
                RPTQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' GREY RECD QUALITY WISE - " & PERIOD & "'"
                CRPO.ReportSource = RPTQUALITYDTLS
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                RPTQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' GREY RECD QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTQUALITYSUMM
            ElseIf FRMSTRING = "MONTHWISE" Then
                RPTMONTH.DataDefinition.FormulaFields("PERIOD").Text = "' GREY RECD MONTH WISE  - " & PERIOD & "'"
                CRPO.ReportSource = RPTMONTH
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
            objmail.attachment = Application.StartupPath & "\Grey Recd Details.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\Grey Recd Details.PDF"

            If FRMSTRING = "BEAMWISE" Then
                expo = RPTBEAMDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBEAMDTLS.Export()
            ElseIf FRMSTRING = "BEAMWISESUMM" Then
                expo = RPTBEAMSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBEAMSUMM.Export()
            ElseIf FRMSTRING = "WEAVERWISE" Then
                expo = RPTWEAVERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERDTLS.Export()
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                expo = RPTWEAVERSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERSUMM.Export()
            ElseIf FRMSTRING = "WEAVERQUALITYWISE" Then
                expo = RPTWEAVERQUALITYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERQUALITYDTLS.Export()
            ElseIf FRMSTRING = "QUALITYWISE" Then
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
            ElseIf FRMSTRING = "MONTHWISE" Then
                expo = RPTMONTH.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTMONTH.Export()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class