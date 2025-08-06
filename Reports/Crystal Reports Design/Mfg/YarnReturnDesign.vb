
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class YarnReturnDesign
    Public WHERECLAUSE As String = ""
    Public FRMSTRING As String = ""
    Public PERIOD As String = ""

    'Dim RPTWARPERISSUEDTLS As New YarnIssueWarperIssueDtlsReport
    'Dim RPTWARPERISSUESUMM As New YarnIssueWarperIssueSummReport
    'Dim RPTYarnReturnWarperQualityDtlsReport As New YarnIssueWarperQualityDtlsReport
    'Dim RPTWARPERQUALITYSUMM As New YarnIssueWarperQualitySummReport
    'Dim RPTWARPERMILLDTLS As New YarnIssueWarperMillDtlsReport
    'Dim RPTWARPERMILLSUMM As New YarnIssueWarperMillSummReport
    'Dim RPTWARPERDTLS As New YarnIssueWarperDtlsReport
    'Dim RPTWARPERSUMM As New YarnIssueWarperSummReport
    'Dim RPTWARPERTRANSDTLS As New YarnIssueWarperTransDtlsReport
    'Dim RPTWARPERTRANSSUMM As New YarnIssueWarperTransSummReport

    'Dim RPTSIZERISSUEDTLS As New YarnIssueSizerIssueDtlsReport
    'Dim RPTSIZERISSUESUMM As New YarnIssueSizerIssueSummReport
    'Dim RPTSIZERQUALITYDTLS As New YarnIssueSizerQualityDtlsReport
    'Dim RPTSIZERQUALITYSUMM As New YarnIssueSizerQualitySummReport
    'Dim RPTSIZERMILLDTLS As New YarnIssueSizerMillDtlsReport
    'Dim RPTSIZERMILLSUMM As New YarnIssueSizerMillSummReport
    'Dim RPTSIZERDTLS As New YarnIssueSizerDtlsReport
    'Dim RPTSIZERSUMM As New YarnIssueSizerSummReport
    'Dim RPTSIZERTRANSDTLS As New YarnIssueSizerTransDtlsReport
    'Dim RPTSIZERTRANSSUMM As New YarnIssueSizerTransSummReport

    'Dim RPTWEAVERISSUEDTLS As New YarnIssueWeaverIssueDtlsReport
    'Dim RPTWEAVERISSUESUMM As New YarnIssueWeaverIssueSummReport
    'Dim RPTWEAVERQUALITYDTLS As New YarnIssueWeaverQualityDtlsReport
    'Dim RPTWEAVERQUALITYSUMM As New YarnIssueWeaverQualitySummReport
    'Dim RPTWEAVERMILLDTLS As New YarnIssueWeaverMillDtlsReport
    'Dim RPTWEAVERMILLSUMM As New YarnIssueWeaverMillSummReport
    'Dim RPTWEAVERDTLS As New YarnIssueWeaverDtlsReport
    'Dim RPTWEAVERSUMM As New YarnIssueWeaverSummReport
    'Dim RPTWEAVERTRANSDTLS As New YarnIssueWeaverTransDtlsReport
    'Dim RPTWEAVERTRANSSUMM As New YarnIssueWeaverTransSummReport

    Dim RPTYarnReturnWarperReturnDtlsReport As New YarnReturnWarperReturnDtlsReport
    Dim RPTYarnReturnWarperReturnSummReport As New YarnReturnWarperReturnSummReport
    Dim RPTYarnReturnWarperQualityDtlsReport As New YarnReturnWarperQualityDtlsReport
    Dim RPTYarnReturnWarperQualitySummReport As New YarnReturnWarperQualitySummReport
    Dim RPTYarnReturnWarperMillDtlsReport As New YarnReturnWarperMillDtlsReport
    Dim RPTYarnReturnWarperMillSummReport As New YarnReturnWarperMillSummReport
    Dim RPTYarnReturnWarperDtlsReport As New YarnReturnWarperDtlsReport
    Dim RPTYarnReturnWarperSummReport As New YarnReturnWarperSummReport
    Dim RPTYarnReturnWarperTransDtlsReport As New YarnReturnWarperTransDtlsReport
    Dim RPTYarnReturnWarperTransSummReport As New YarnReturnWarperTransSummReport

    Dim RPTYarnReturnSizerReturnDtlsReport As New YarnReturnSizerReturnDtlsReport
    Dim RPTYarnRETURNSizerRETURNSummReport As New YarnReturnSizerReturnSummReport
    Dim RPTYarnReturnSizerQualityDtlsReport As New YarnReturnSizerQualityDtlsReport
    Dim RPTYarnReturnSizerQualitySummReport As New YarnReturnSizerQualitySummReport
    Dim RPTYarnReturnSizerMillDtlsReport As New YarnReturnSizerMillDtlsReport
    Dim RPTYarnReturnSizerMillSummReport As New YarnReturnSizerMillSummReport
    Dim RPTYarnReturnSizerDtlsReport As New YarnReturnSizerDtlsReport
    Dim RPTYarnReturnSizerSummReport As New YarnReturnSizerSummReport
    Dim RPTYarnReturnSizerTransDtlsReport As New YarnReturnSizerTransDtlsReport
    Dim RPTYarnReturnSizerTransSummReport As New YarnReturnSizerTransSummReport

    Dim RPTYarnReturnWeaverReturnDtlsReport As New YarnReturnWeaverReturnDtlsReport
    Dim RPTYarnReturnWeaverReturnSummReport As New YarnReturnWeaverReturnSummReport
    Dim RPTYarnReturnWeaverQualityDtlsReport As New YarnReturnWeaverQualityDtlsReport
    Dim RPTYarnReturnWeaverQualitySummReport As New YarnReturnWeaverQualitySummReport
    Dim RPTYarnReturnWeaverMillDtlsReport As New YarnReturnWeaverMillDtlsReport
    Dim RPTYarnReturnWeaverMillSummReport As New YarnReturnWeaverMillSummReport
    Dim RPTYarnReturnWeaverDtlsReport As New YarnReturnWeaverDtlsReport
    Dim RPTYarnReturnWeaverSummReport As New YarnReturnWeaverSummReport
    Dim RPTYarnReturnWeaverTransDtlsReport As New YarnReturnWeaverTransDtlsReport
    Dim RPTYarnReturnWeaverTransSummReport As New YarnReturnWeaverTransSummReport



    'Dim RPTYARNISSUEWARPER As New YarnIssueWarperReport
    'Dim RPTYARNISSUESIZER As New YarnIssueSizerReport
    'Dim RPTYARNISSUEWEAVER As New YarnIssueWeaverReport

    Private Sub YarnRETURNDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

            If FRMSTRING = "WARPERRETURNDTLS" Then
                crTables = RPTYarnReturnWarperReturnDtlsReport.Database.Tables
            ElseIf FRMSTRING = "WARPERRETURNSUMM" Then
                crTables = RPTYarnReturnWarperReturnSummReport.Database.Tables
            ElseIf FRMSTRING = "WARPERQUALITYWISEDTLS" Then
                crTables = RPTYarnReturnWarperQualityDtlsReport.Database.Tables
            ElseIf FRMSTRING = "WARPERQUALITYWISESUMM" Then
                crTables = RPTYarnReturnWarperQualitySummReport.Database.Tables
            ElseIf FRMSTRING = "WARPERMILLWISEDTLS" Then
                crTables = RPTYarnReturnWarperMillDtlsReport.Database.Tables
            ElseIf FRMSTRING = "WARPERMILLWISESUMM" Then
                crTables = RPTYarnReturnWarperMillSummReport.Database.Tables
            ElseIf FRMSTRING = "WARPERWISEDTLS" Then
                crTables = RPTYarnReturnWarperDtlsReport.Database.Tables
            ElseIf FRMSTRING = "WARPERWISESUMM" Then
                crTables = RPTYarnReturnWarperSummReport.Database.Tables
            ElseIf FRMSTRING = "WARPERTRANSWISEDTLS" Then
                crTables = RPTYarnReturnWarperTransDtlsReport.Database.Tables
            ElseIf FRMSTRING = "WARPERTRANSWISESUMM" Then
                crTables = RPTYarnReturnWarperTransSummReport.Database.Tables

            ElseIf FRMSTRING = "SIZERRETURNDTLS" Then
                crTables = RPTYarnReturnSizerReturnDtlsReport.Database.Tables
            ElseIf FRMSTRING = "SIZERRETURNSUMM" Then
                crTables = RPTYarnRETURNSizerRETURNSummReport.Database.Tables
            ElseIf FRMSTRING = "SIZERQUALITYWISEDTLS" Then
                crTables = RPTYarnReturnSizerQualityDtlsReport.Database.Tables
            ElseIf FRMSTRING = "SIZERQUALITYWISESUMM" Then
                crTables = RPTYarnReturnSizerQualitySummReport.Database.Tables
            ElseIf FRMSTRING = "SIZERMILLWISEDTLS" Then
                crTables = RPTYarnReturnSizerMillDtlsReport.Database.Tables
            ElseIf FRMSTRING = "SIZERMILLWISESUMM" Then
                crTables = RPTYarnReturnSizerMillSummReport.Database.Tables
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                crTables = RPTYarnReturnSizerDtlsReport.Database.Tables
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                crTables = RPTYarnReturnSizerSummReport.Database.Tables
            ElseIf FRMSTRING = "SIZERTRANSWISEDTLS" Then
                crTables = RPTYarnReturnSizerTransDtlsReport.Database.Tables
            ElseIf FRMSTRING = "SIZERTRANSWISESUMM" Then
                crTables = RPTYarnReturnSizerTransSummReport.Database.Tables

            ElseIf FRMSTRING = "WEAVERRETURNDTLS" Then
                crTables = RPTYarnReturnWeaverReturnDtlsReport.Database.Tables
            ElseIf FRMSTRING = "WEAVERRETURNSUMM" Then
                crTables = RPTYarnReturnWeaverReturnSummReport.Database.Tables
            ElseIf FRMSTRING = "WEAVERQUALITYWISEDTLS" Then
                crTables = RPTYarnReturnWeaverQualityDtlsReport.Database.Tables
            ElseIf FRMSTRING = "WEAVERQUALITYWISESUMM" Then
                crTables = RPTYarnReturnWeaverQualitySummReport.Database.Tables
            ElseIf FRMSTRING = "WEAVERMILLWISEDTLS" Then
                crTables = RPTYarnReturnWeaverMillDtlsReport.Database.Tables
            ElseIf FRMSTRING = "WEAVERMILLWISESUMM" Then
                crTables = RPTYarnReturnWeaverMillSummReport.Database.Tables
            ElseIf FRMSTRING = "WEAVERWISEDTLS" Then
                crTables = RPTYarnReturnWeaverDtlsReport.Database.Tables
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                crTables = RPTYarnReturnWeaverSummReport.Database.Tables
            ElseIf FRMSTRING = "WEAVERTRANSWISEDTLS" Then
                crTables = RPTYarnReturnWeaverTransDtlsReport.Database.Tables
            ElseIf FRMSTRING = "WEAVERTRANSWISESUMM" Then
                crTables = RPTYarnReturnWeaverTransSummReport.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

    
            If FRMSTRING = "WARPERRETURNDTLS" Then
                RPTYarnReturnWarperReturnDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' YARN RETURN DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWarperReturnDtlsReport
            ElseIf FRMSTRING = "WARPERRETURNSUMM" Then
                RPTYarnReturnWarperReturnSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' YARN RETURN SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWarperReturnSummReport
            ElseIf FRMSTRING = "WARPERQUALITYWISEDTLS" Then
                RPTYarnReturnWarperQualityDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWarperQualityDtlsReport
            ElseIf FRMSTRING = "WARPERQUALITYWISESUMM" Then
                RPTYarnReturnWarperQualitySummReport.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWarperQualitySummReport
            ElseIf FRMSTRING = "WARPERMILLWISEDTLS" Then
                RPTYarnReturnWarperMillDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWarperMillDtlsReport
            ElseIf FRMSTRING = "WARPERMILLWISESUMM" Then
                RPTYarnReturnWarperMillSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWarperMillSummReport
            ElseIf FRMSTRING = "WARPERWISEDTLS" Then
                RPTYarnReturnWarperDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' WARPER WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWarperDtlsReport
            ElseIf FRMSTRING = "WARPERWISESUMM" Then
                RPTYarnReturnWarperSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' WARPER WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWarperSummReport
            ElseIf FRMSTRING = "WARPERTRANSWISEDTLS" Then
                RPTYarnReturnWarperTransDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWarperTransDtlsReport
            ElseIf FRMSTRING = "WARPERTRANSWISESUMM" Then
                RPTYarnReturnWarperTransSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWarperTransSummReport


            ElseIf FRMSTRING = "SIZERRETURNDTLS" Then
                RPTYarnReturnSizerReturnDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' YARN RETURN DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnSizerReturnDtlsReport
            ElseIf FRMSTRING = "SIZERRETURNSUMM" Then
                RPTYarnRETURNSizerRETURNSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' YARN RETURN SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnRETURNSizerRETURNSummReport
            ElseIf FRMSTRING = "SIZERQUALITYWISEDTLS" Then
                RPTYarnReturnSizerQualityDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnSizerQualityDtlsReport
            ElseIf FRMSTRING = "SIZERQUALITYWISESUMM" Then
                RPTYarnReturnSizerQualitySummReport.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnSizerQualitySummReport
            ElseIf FRMSTRING = "SIZERMILLWISEDTLS" Then
                RPTYarnReturnSizerMillDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnSizerMillDtlsReport
            ElseIf FRMSTRING = "SIZERMILLWISESUMM" Then
                RPTYarnReturnSizerSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnSizerSummReport
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                RPTYarnReturnSizerDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' SIZER WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnSizerDtlsReport
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                RPTYarnReturnSizerTransSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' SIZER WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnSizerTransSummReport
            ElseIf FRMSTRING = "SIZERTRANSWISEDTLS" Then
                RPTYarnReturnSizerTransDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnSizerTransDtlsReport
            ElseIf FRMSTRING = "SIZERTRANSWISESUMM" Then
                RPTYarnReturnSizerTransSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnSizerTransSummReport

            ElseIf FRMSTRING = "WEAVERRETURNDTLS" Then
                RPTYarnReturnWeaverReturnDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' YARN RETURN DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWeaverReturnDtlsReport
            ElseIf FRMSTRING = "WEAVERRETURNSUMM" Then
                RPTYarnReturnWeaverReturnSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' YARN RETURN SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWeaverReturnSummReport
            ElseIf FRMSTRING = "WEAVERQUALITYWISEDTLS" Then
                RPTYarnReturnWeaverQualityDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWeaverQualityDtlsReport
            ElseIf FRMSTRING = "WEAVERQUALITYWISESUMM" Then
                RPTYarnReturnWeaverQualitySummReport.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWeaverQualitySummReport
            ElseIf FRMSTRING = "WEAVERMILLWISEDTLS" Then
                RPTYarnReturnWeaverMillDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWeaverMillDtlsReport
            ElseIf FRMSTRING = "WEAVERMILLWISESUMM" Then
                RPTYarnReturnWeaverMillSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWeaverMillSummReport
            ElseIf FRMSTRING = "WEAVERWISEDTLS" Then
                RPTYarnReturnWeaverDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' WEAVER WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWeaverDtlsReport
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                RPTYarnReturnWeaverSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' WEAVER WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWeaverSummReport
            ElseIf FRMSTRING = "WEAVERTRANSWISEDTLS" Then
                RPTYarnReturnWeaverTransDtlsReport.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWeaverTransDtlsReport
            ElseIf FRMSTRING = "WEAVERTRANSWISESUMM" Then
                RPTYarnReturnWeaverTransSummReport.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTYarnReturnWeaverTransSummReport


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
            objmail.attachment = Application.StartupPath & "\Yarn Issue Details.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\Yarn Issue Details.PDF"



            'If FRMSTRING = "YARNISSUEWARPER" Then
            '    expo = RPTYARNISSUEWARPER.ExportOptions
            '    expo.ExportDestinationType = ExportDestinationType.DiskFile
            '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
            '    expo.DestinationOptions = oDfDopt
            '    RPTYARNISSUEWARPER.Export()
            'ElseIf FRMSTRING = "YARNISSUESIZER" Then
            '    expo = RPTYARNISSUESIZER.ExportOptions
            '    expo.ExportDestinationType = ExportDestinationType.DiskFile
            '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
            '    expo.DestinationOptions = oDfDopt
            '    RPTYARNISSUESIZER.Export()
            'ElseIf FRMSTRING = "YARNISSUEWEAVER" Then
            '    expo = RPTYARNISSUEWEAVER.ExportOptions
            '    expo.ExportDestinationType = ExportDestinationType.DiskFile
            '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
            '    expo.DestinationOptions = oDfDopt
            '    RPTYARNISSUEWEAVER.Export()

            If FRMSTRING = "WARPERRETURNDTLS" Then
                expo = RPTYarnReturnWarperReturnDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWarperReturnDtlsReport.Export()
            ElseIf FRMSTRING = "WARPERRETURNSUMM" Then
                expo = RPTYarnReturnWarperReturnSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWarperReturnSummReport.Export()
            ElseIf FRMSTRING = "WARPERQUALITYWISEDTLS" Then
                expo = RPTYarnReturnWarperQualityDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWarperQualityDtlsReport.Export()
            ElseIf FRMSTRING = "WARPERQUALITYWISESUMM" Then
                expo = RPTYarnReturnWarperQualitySummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWarperQualitySummReport.Export()
            ElseIf FRMSTRING = "WARPERMILLWISEDTLS" Then
                expo = RPTYarnReturnWarperMillDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWarperMillDtlsReport.Export()
            ElseIf FRMSTRING = "WARPERMILLWISESUMM" Then
                expo = RPTYarnReturnWarperMillSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWarperMillSummReport.Export()
            ElseIf FRMSTRING = "WARPERWISEDTLS" Then
                expo = RPTYarnReturnWarperDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWarperDtlsReport.Export()
            ElseIf FRMSTRING = "WARPERWISESUMM" Then
                expo = RPTYarnReturnWarperSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWarperSummReport.Export()
            ElseIf FRMSTRING = "WARPERTRANSWISEDTLS" Then
                expo = RPTYarnReturnWarperTransDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWarperTransDtlsReport.Export()
            ElseIf FRMSTRING = "WARPERTRANSWISESUMM" Then
                expo = RPTYarnReturnWarperTransSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWarperTransSummReport.Export()


            ElseIf FRMSTRING = "SIZERRETURNDTLS" Then
                expo = RPTYarnReturnSizerReturnDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnSizerReturnDtlsReport.Export()
            ElseIf FRMSTRING = "SIZERRETURNSUMM" Then
                expo = RPTYarnRETURNSizerRETURNSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnRETURNSizerRETURNSummReport.Export()
            ElseIf FRMSTRING = "SIZERQUALITYWISEDTLS" Then
                expo = RPTYarnReturnSizerQualityDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnSizerQualityDtlsReport.Export()
            ElseIf FRMSTRING = "SIZERQUALITYWISESUMM" Then
                expo = RPTYarnReturnSizerQualitySummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnSizerQualitySummReport.Export()
            ElseIf FRMSTRING = "SIZERMILLWISEDTLS" Then
                expo = RPTYarnReturnSizerMillDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnSizerMillDtlsReport.Export()
            ElseIf FRMSTRING = "SIZERMILLWISESUMM" Then
                expo = RPTYarnReturnSizerSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnSizerSummReport.Export()
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                expo = RPTYarnReturnSizerDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnSizerDtlsReport.Export()
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                expo = RPTYarnReturnSizerTransSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnSizerTransSummReport.Export()
            ElseIf FRMSTRING = "SIZERTRANSWISEDTLS" Then
                expo = RPTYarnReturnSizerTransDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnSizerTransDtlsReport.Export()
            ElseIf FRMSTRING = "SIZERTRANSWISESUMM" Then
                expo = RPTYarnReturnSizerTransSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnSizerTransSummReport.Export()


            ElseIf FRMSTRING = "WEAVERRETURNDTLS" Then
                expo = RPTYarnReturnWeaverReturnDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWeaverReturnDtlsReport.Export()
            ElseIf FRMSTRING = "WEAVERRETURNSUMM" Then
                expo = RPTYarnReturnWeaverReturnSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWeaverReturnSummReport.Export()
            ElseIf FRMSTRING = "WEAVERQUALITYWISEDTLS" Then
                expo = RPTYarnReturnWeaverQualityDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWeaverQualityDtlsReport.Export()
            ElseIf FRMSTRING = "WEAVERQUALITYWISESUMM" Then
                expo = RPTYarnReturnWeaverQualitySummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWeaverQualitySummReport.Export()
            ElseIf FRMSTRING = "WEAVERMILLWISEDTLS" Then
                expo = RPTYarnReturnWeaverMillDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWeaverMillDtlsReport.Export()
            ElseIf FRMSTRING = "WEAVERMILLWISESUMM" Then
                expo = RPTYarnReturnWeaverMillSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWeaverMillSummReport.Export()
            ElseIf FRMSTRING = "WEAVERWISEDTLS" Then
                expo = RPTYarnReturnWeaverDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWeaverDtlsReport.Export()
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                expo = RPTYarnReturnWeaverSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWeaverSummReport.Export()
            ElseIf FRMSTRING = "WEAVERTRANSWISEDTLS" Then
                expo = RPTYarnReturnWeaverTransDtlsReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWeaverTransDtlsReport.Export()
            ElseIf FRMSTRING = "WEAVERTRANSWISESUMM" Then
                expo = RPTYarnReturnWeaverTransSummReport.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYarnReturnWeaverTransSummReport.Export()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

   
End Class
