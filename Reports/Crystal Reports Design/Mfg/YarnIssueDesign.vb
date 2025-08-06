
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class YarnIssueDesign

    Public WHERECLAUSE As String = ""
    Public FRMSTRING As String = ""
    Public PERIOD As String = ""

    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    'NEW ADD(
    Public PARTYNAME As String
    Public AGENTNAME As String
    Public ISSUENO As Integer
    Public REGNAME As String
    Public DIRECTPRINT As Boolean = False
    Public NOOFCOPIES As Integer = 1
    Public PRINTSETTING As Object = Nothing
    Public INVTYPE As String

    Dim tempattachment As String



    ')
    Dim RPTWARPERISSUEDTLS As New YarnIssueWarperIssueDtlsReport
    Dim RPTWARPERISSUESUMM As New YarnIssueWarperIssueSummReport
    Dim RPTWARPERQUALITYDTLS As New YarnIssueWarperQualityDtlsReport
    Dim RPTWARPERQUALITYSUMM As New YarnIssueWarperQualitySummReport
    Dim RPTWARPERMILLDTLS As New YarnIssueWarperMillDtlsReport
    Dim RPTWARPERMILLSUMM As New YarnIssueWarperMillSummReport
    Dim RPTWARPERDTLS As New YarnIssueWarperDtlsReport
    Dim RPTWARPERSUMM As New YarnIssueWarperSummReport
    Dim RPTWARPERTRANSDTLS As New YarnIssueWarperTransDtlsReport
    Dim RPTWARPERTRANSSUMM As New YarnIssueWarperTransSummReport

    Dim RPTSIZERISSUEDTLS As New YarnIssueSizerIssueDtlsReport
    Dim RPTSIZERISSUESUMM As New YarnIssueSizerIssueSummReport
    Dim RPTSIZERQUALITYDTLS As New YarnIssueSizerQualityDtlsReport
    Dim RPTSIZERQUALITYSUMM As New YarnIssueSizerQualitySummReport
    Dim RPTSIZERMILLDTLS As New YarnIssueSizerMillDtlsReport
    Dim RPTSIZERMILLSUMM As New YarnIssueSizerMillSummReport
    Dim RPTSIZERDTLS As New YarnIssueSizerDtlsReport
    Dim RPTSIZERSUMM As New YarnIssueSizerSummReport
    Dim RPTSIZERTRANSDTLS As New YarnIssueSizerTransDtlsReport
    Dim RPTSIZERTRANSSUMM As New YarnIssueSizerTransSummReport

    Dim RPTWEAVERISSUEDTLS As New YarnIssueWeaverIssueDtlsReport
    Dim RPTWEAVERISSUESUMM As New YarnIssueWeaverIssueSummReport
    Dim RPTWEAVERQUALITYDTLS As New YarnIssueWeaverQualityDtlsReport
    Dim RPTWEAVERQUALITYSUMM As New YarnIssueWeaverQualitySummReport
    Dim RPTWEAVERMILLDTLS As New YarnIssueWeaverMillDtlsReport
    Dim RPTWEAVERMILLSUMM As New YarnIssueWeaverMillSummReport
    Dim RPTWEAVERDTLS As New YarnIssueWeaverDtlsReport
    Dim RPTWEAVERSUMM As New YarnIssueWeaverSummReport
    Dim RPTWEAVERTRANSDTLS As New YarnIssueWeaverTransDtlsReport
    Dim RPTWEAVERTRANSSUMM As New YarnIssueWeaverTransSummReport

    Dim RPTDYEINGISSUEDTLS As New YarnIssueDyeingIssueDtlsReport
    Dim RPTDYEINGISSUESUMM As New YarnIssueDyeingIssueSummReport
    Dim RPTDYEINGQUALITYDTLS As New YarnIssueDyeingQualityDtlsReport
    Dim RPTDYEINGQUALITYSUMM As New YarnIssueDyeingQualitySummReport
    Dim RPTDYEINGMILLDTLS As New YarnIssueDyeingMillDtlsReport
    Dim RPTDYEINGMILLSUMM As New YarnIssueDyeingMillSummReport
    Dim RPTDYEINGDTLS As New YarnIssueDyeingDtlsReport
    Dim RPTDYEINGSUMM As New YarnIssueDyeingSummReport
    Dim RPTDYEINGTRANSDTLS As New YarnIssueDyeingTransDtlsReport
    Dim RPTDYEINGTRANSSUMM As New YarnIssueDyeingTransSummReport


    Dim RPTYARNISSUEWARPER As New YarnIssueWarperReport
    Dim RPTYARNISSUESIZER As New YarnIssueSizerReport
    Dim RPTYARNISSUEWEAVER As New YarnIssueWeaverReport
    Dim RPTYARNISSUEDYEING As New YarnIssueDyeingReport
    Dim RPTYARNISSUEDYEING_SONU As New YarnIssueDyeingReport_SONU

    Private Sub YarnIssueDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

            If DIRECTPRINT = True Then
                PRINTDIRECTLYTOPRINTER()
                Exit Sub
            End If

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

            If FRMSTRING = "YARNISSUEWARPER" Then
                crTables = RPTYARNISSUEWARPER.Database.Tables
            ElseIf FRMSTRING = "YARNISSUESIZER" Then
                crTables = RPTYARNISSUESIZER.Database.Tables
            ElseIf FRMSTRING = "YARNISSUEWEAVER" Then
                crTables = RPTYARNISSUEWEAVER.Database.Tables
            ElseIf FRMSTRING = "YARNISSUEDYEING" Then
                If ClientName = "SONU" Then
                    crTables = RPTYARNISSUEDYEING_SONU.Database.Tables
                Else
                    crTables = RPTYARNISSUEDYEING.Database.Tables
                End If


            ElseIf FRMSTRING = "WARPERISSUEDTLS" Then
                crTables = RPTWARPERISSUEDTLS.Database.Tables
            ElseIf FRMSTRING = "WARPERISSUESUMM" Then
                crTables = RPTWARPERISSUESUMM.Database.Tables
            ElseIf FRMSTRING = "WARPERQUALITYWISEDTLS" Then
                crTables = RPTWARPERQUALITYDTLS.Database.Tables
            ElseIf FRMSTRING = "WARPERQUALITYWISESUMM" Then
                crTables = RPTWARPERQUALITYSUMM.Database.Tables
            ElseIf FRMSTRING = "WARPERMILLWISEDTLS" Then
                crTables = RPTWARPERMILLDTLS.Database.Tables
            ElseIf FRMSTRING = "WARPERMILLWISESUMM" Then
                crTables = RPTWARPERMILLSUMM.Database.Tables
            ElseIf FRMSTRING = "WARPERWISEDTLS" Then
                crTables = RPTWARPERDTLS.Database.Tables
            ElseIf FRMSTRING = "WARPERWISESUMM" Then
                crTables = RPTWARPERSUMM.Database.Tables
            ElseIf FRMSTRING = "WARPERTRANSWISEDTLS" Then
                crTables = RPTWARPERTRANSDTLS.Database.Tables
            ElseIf FRMSTRING = "WARPERTRANSWISESUMM" Then
                crTables = RPTWARPERTRANSSUMM.Database.Tables

            ElseIf FRMSTRING = "SIZERISSUEDTLS" Then
                crTables = RPTSIZERISSUEDTLS.Database.Tables
            ElseIf FRMSTRING = "SIZERISSUESUMM" Then
                crTables = RPTSIZERISSUESUMM.Database.Tables
            ElseIf FRMSTRING = "SIZERQUALITYWISEDTLS" Then
                crTables = RPTSIZERQUALITYDTLS.Database.Tables
            ElseIf FRMSTRING = "SIZERQUALITYWISESUMM" Then
                crTables = RPTSIZERQUALITYSUMM.Database.Tables
            ElseIf FRMSTRING = "SIZERMILLWISEDTLS" Then
                crTables = RPTSIZERMILLDTLS.Database.Tables
            ElseIf FRMSTRING = "SIZERMILLWISESUMM" Then
                crTables = RPTSIZERMILLSUMM.Database.Tables
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                crTables = RPTSIZERDTLS.Database.Tables
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                crTables = RPTSIZERSUMM.Database.Tables
            ElseIf FRMSTRING = "SIZERTRANSWISEDTLS" Then
                crTables = RPTSIZERTRANSDTLS.Database.Tables
            ElseIf FRMSTRING = "SIZERTRANSWISESUMM" Then
                crTables = RPTSIZERTRANSSUMM.Database.Tables

            ElseIf FRMSTRING = "WEAVERISSUEDTLS" Then
                crTables = RPTWEAVERISSUEDTLS.Database.Tables
            ElseIf FRMSTRING = "WEAVERISSUESUMM" Then
                crTables = RPTWEAVERISSUESUMM.Database.Tables
            ElseIf FRMSTRING = "WEAVERQUALITYWISEDTLS" Then
                crTables = RPTWEAVERQUALITYDTLS.Database.Tables
            ElseIf FRMSTRING = "WEAVERQUALITYWISESUMM" Then
                crTables = RPTWEAVERQUALITYSUMM.Database.Tables
            ElseIf FRMSTRING = "WEAVERMILLWISEDTLS" Then
                crTables = RPTWEAVERMILLDTLS.Database.Tables
            ElseIf FRMSTRING = "WEAVERMILLWISESUMM" Then
                crTables = RPTWEAVERMILLSUMM.Database.Tables
            ElseIf FRMSTRING = "WEAVERWISEDTLS" Then
                crTables = RPTWEAVERDTLS.Database.Tables
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                crTables = RPTWEAVERSUMM.Database.Tables
            ElseIf FRMSTRING = "WEAVERTRANSWISEDTLS" Then
                crTables = RPTWEAVERTRANSDTLS.Database.Tables
            ElseIf FRMSTRING = "WEAVERTRANSWISESUMM" Then
                crTables = RPTWEAVERTRANSSUMM.Database.Tables

            ElseIf FRMSTRING = "DYEINGISSUEDTLS" Then
                crTables = RPTDYEINGISSUEDTLS.Database.Tables
            ElseIf FRMSTRING = "DYEINGISSUESUMM" Then
                crTables = RPTDYEINGISSUESUMM.Database.Tables
            ElseIf FRMSTRING = "DYEINGQUALITYWISEDTLS" Then
                crTables = RPTDYEINGQUALITYDTLS.Database.Tables
            ElseIf FRMSTRING = "DYEINGQUALITYWISESUMM" Then
                crTables = RPTDYEINGQUALITYSUMM.Database.Tables
            ElseIf FRMSTRING = "DYEINGMILLWISEDTLS" Then
                crTables = RPTDYEINGMILLDTLS.Database.Tables
            ElseIf FRMSTRING = "DYEINGMILLWISESUMM" Then
                crTables = RPTDYEINGMILLSUMM.Database.Tables
            ElseIf FRMSTRING = "DYEINGWISEDTLS" Then
                crTables = RPTDYEINGDTLS.Database.Tables
            ElseIf FRMSTRING = "DYEINGWISESUMM" Then
                crTables = RPTDYEINGSUMM.Database.Tables
            ElseIf FRMSTRING = "DYEINGTRANSWISEDTLS" Then
                crTables = RPTDYEINGTRANSDTLS.Database.Tables
            ElseIf FRMSTRING = "DYEINGTRANSWISESUMM" Then
                crTables = RPTDYEINGTRANSSUMM.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "YARNISSUEWARPER" Then
                CRPO.ReportSource = RPTYARNISSUEWARPER
            ElseIf FRMSTRING = "YARNISSUESIZER" Then
                CRPO.ReportSource = RPTYARNISSUESIZER
            ElseIf FRMSTRING = "YARNISSUEWEAVER" Then
                CRPO.ReportSource = RPTYARNISSUEWEAVER
            ElseIf FRMSTRING = "YARNISSUEDYEING" Then
                If ClientName = "SONU" Then
                    CRPO.ReportSource = RPTYARNISSUEDYEING_SONU
                Else
                    CRPO.ReportSource = RPTYARNISSUEDYEING
                End If

            ElseIf FRMSTRING = "WARPERISSUEDTLS" Then
                RPTWARPERISSUEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' YARN ISSUE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERISSUEDTLS
            ElseIf FRMSTRING = "WARPERISSUESUMM" Then
                RPTWARPERISSUESUMM.DataDefinition.FormulaFields("PERIOD").Text = "' YARN ISSUE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERISSUESUMM
            ElseIf FRMSTRING = "WARPERQUALITYWISEDTLS" Then
                RPTWARPERQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERQUALITYDTLS
            ElseIf FRMSTRING = "WARPERQUALITYWISESUMM" Then
                RPTWARPERQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERQUALITYSUMM
            ElseIf FRMSTRING = "WARPERMILLWISEDTLS" Then
                RPTWARPERMILLDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERMILLDTLS
            ElseIf FRMSTRING = "WARPERMILLWISESUMM" Then
                RPTWARPERMILLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERMILLSUMM
            ElseIf FRMSTRING = "WARPERWISEDTLS" Then
                RPTWARPERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' WARPER WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERDTLS
            ElseIf FRMSTRING = "WARPERWISESUMM" Then
                RPTWARPERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' WARPER WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERSUMM
            ElseIf FRMSTRING = "WARPERTRANSWISEDTLS" Then
                RPTWARPERTRANSDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERTRANSDTLS
            ElseIf FRMSTRING = "WARPERTRANSWISESUMM" Then
                RPTWARPERTRANSSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERTRANSSUMM


            ElseIf FRMSTRING = "SIZERISSUEDTLS" Then
                RPTSIZERISSUEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' YARN ISSUE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERISSUEDTLS
            ElseIf FRMSTRING = "SIZERISSUESUMM" Then
                RPTSIZERISSUESUMM.DataDefinition.FormulaFields("PERIOD").Text = "' YARN ISSUE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERISSUESUMM
            ElseIf FRMSTRING = "SIZERQUALITYWISEDTLS" Then
                RPTSIZERQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERQUALITYDTLS
            ElseIf FRMSTRING = "SIZERQUALITYWISESUMM" Then
                RPTSIZERQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERQUALITYSUMM
            ElseIf FRMSTRING = "SIZERMILLWISEDTLS" Then
                RPTSIZERMILLDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERMILLDTLS
            ElseIf FRMSTRING = "SIZERMILLWISESUMM" Then
                RPTSIZERMILLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERMILLSUMM
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                RPTSIZERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' SIZER WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERDTLS
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                RPTSIZERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' SIZER WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERSUMM
            ElseIf FRMSTRING = "SIZERTRANSWISEDTLS" Then
                RPTSIZERTRANSDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERTRANSDTLS
            ElseIf FRMSTRING = "SIZERTRANSWISESUMM" Then
                RPTSIZERTRANSSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERTRANSSUMM

            ElseIf FRMSTRING = "WEAVERISSUEDTLS" Then
                RPTWEAVERISSUEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' YARN ISSUE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERISSUEDTLS
            ElseIf FRMSTRING = "WEAVERISSUESUMM" Then
                RPTWEAVERISSUESUMM.DataDefinition.FormulaFields("PERIOD").Text = "' YARN ISSUE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERISSUESUMM
            ElseIf FRMSTRING = "WEAVERQUALITYWISEDTLS" Then
                RPTWEAVERQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERQUALITYDTLS
            ElseIf FRMSTRING = "WEAVERQUALITYWISESUMM" Then
                RPTWEAVERQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERQUALITYSUMM
            ElseIf FRMSTRING = "WEAVERMILLWISEDTLS" Then
                RPTWEAVERMILLDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERMILLDTLS
            ElseIf FRMSTRING = "WEAVERMILLWISESUMM" Then
                RPTWEAVERMILLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERMILLSUMM
            ElseIf FRMSTRING = "WEAVERWISEDTLS" Then
                RPTWEAVERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' WEAVER WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERDTLS
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                RPTWEAVERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' WEAVER WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERSUMM
            ElseIf FRMSTRING = "WEAVERTRANSWISEDTLS" Then
                RPTWEAVERTRANSDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERTRANSDTLS
            ElseIf FRMSTRING = "WEAVERTRANSWISESUMM" Then
                RPTWEAVERTRANSSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERTRANSSUMM


            ElseIf FRMSTRING = "DYEINGISSUEDTLS" Then
                RPTDYEINGISSUEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' YARN ISSUE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTDYEINGISSUEDTLS
            ElseIf FRMSTRING = "DYEINGISSUESUMM" Then
                RPTDYEINGISSUESUMM.DataDefinition.FormulaFields("PERIOD").Text = "' YARN ISSUE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTDYEINGISSUESUMM
            ElseIf FRMSTRING = "DYEINGQUALITYWISEDTLS" Then
                RPTDYEINGQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTDYEINGQUALITYDTLS
            ElseIf FRMSTRING = "DYEINGQUALITYWISESUMM" Then
                RPTDYEINGQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTDYEINGQUALITYSUMM
            ElseIf FRMSTRING = "DYEINGMILLWISEDTLS" Then
                RPTDYEINGMILLDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTDYEINGMILLDTLS
            ElseIf FRMSTRING = "DYEINGMILLWISESUMM" Then
                RPTDYEINGMILLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTDYEINGMILLSUMM
            ElseIf FRMSTRING = "DYEINGWISEDTLS" Then
                RPTDYEINGDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' DYEING WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTDYEINGDTLS
            ElseIf FRMSTRING = "DYEINGWISESUMM" Then
                RPTDYEINGSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' DYEING WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTDYEINGSUMM
            ElseIf FRMSTRING = "DYEINGTRANSWISEDTLS" Then
                RPTDYEINGTRANSDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTDYEINGTRANSDTLS
            ElseIf FRMSTRING = "DYEINGTRANSWISESUMM" Then
                RPTDYEINGTRANSSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' TRANSPORT WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTDYEINGTRANSSUMM

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



            If FRMSTRING = "YARNISSUEWARPER" Then
                expo = RPTYARNISSUEWARPER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYARNISSUEWARPER.Export()
            ElseIf FRMSTRING = "YARNISSUESIZER" Then
                expo = RPTYARNISSUESIZER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYARNISSUESIZER.Export()
            ElseIf FRMSTRING = "YARNISSUEWEAVER" Then
                expo = RPTYARNISSUEWEAVER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTYARNISSUEWEAVER.Export()
            ElseIf FRMSTRING = "YARNISSUEDYEING" Then
                If ClientName = "SONU" Then
                    expo = RPTYARNISSUEDYEING_SONU.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTYARNISSUEDYEING_SONU.Export()
                Else
                    expo = RPTYARNISSUEDYEING.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTYARNISSUEDYEING.Export()
                End If


            ElseIf FRMSTRING = "WARPERISSUEDTLS" Then
                expo = RPTWARPERISSUEDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWARPERISSUEDTLS.Export()
            ElseIf FRMSTRING = "WARPERISSUESUMM" Then
                expo = RPTWARPERISSUESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWARPERISSUESUMM.Export()
            ElseIf FRMSTRING = "WARPERQUALITYWISEDTLS" Then
                expo = RPTWARPERQUALITYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWARPERQUALITYDTLS.Export()
            ElseIf FRMSTRING = "WARPERQUALITYWISESUMM" Then
                expo = RPTWARPERQUALITYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWARPERQUALITYSUMM.Export()
            ElseIf FRMSTRING = "WARPERMILLWISEDTLS" Then
                expo = RPTWARPERMILLDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWARPERMILLDTLS.Export()
            ElseIf FRMSTRING = "WARPERMILLWISESUMM" Then
                expo = RPTWARPERMILLSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWARPERMILLSUMM.Export()
            ElseIf FRMSTRING = "WARPERWISEDTLS" Then
                expo = RPTWARPERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWARPERDTLS.Export()
            ElseIf FRMSTRING = "WARPERWISESUMM" Then
                expo = RPTWARPERSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWARPERSUMM.Export()
            ElseIf FRMSTRING = "WARPERTRANSWISEDTLS" Then
                expo = RPTWARPERTRANSDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWARPERTRANSDTLS.Export()
            ElseIf FRMSTRING = "WARPERTRANSWISESUMM" Then
                expo = RPTWARPERTRANSSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWARPERTRANSSUMM.Export()


            ElseIf FRMSTRING = "SIZERISSUEDTLS" Then
                expo = RPTSIZERISSUEDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERISSUEDTLS.Export()
            ElseIf FRMSTRING = "SIZERISSUESUMM" Then
                expo = RPTSIZERISSUESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERISSUESUMM.Export()
            ElseIf FRMSTRING = "SIZERQUALITYWISEDTLS" Then
                expo = RPTSIZERQUALITYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERQUALITYDTLS.Export()
            ElseIf FRMSTRING = "SIZERQUALITYWISESUMM" Then
                expo = RPTSIZERQUALITYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERQUALITYSUMM.Export()
            ElseIf FRMSTRING = "SIZERMILLWISEDTLS" Then
                expo = RPTSIZERMILLDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERMILLDTLS.Export()
            ElseIf FRMSTRING = "SIZERMILLWISESUMM" Then
                expo = RPTSIZERMILLSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERMILLSUMM.Export()
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                expo = RPTSIZERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERDTLS.Export()
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                expo = RPTSIZERSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERSUMM.Export()
            ElseIf FRMSTRING = "SIZERTRANSWISEDTLS" Then
                expo = RPTSIZERTRANSDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERTRANSDTLS.Export()
            ElseIf FRMSTRING = "SIZERTRANSWISESUMM" Then
                expo = RPTSIZERTRANSSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERTRANSSUMM.Export()


            ElseIf FRMSTRING = "WEAVERISSUEDTLS" Then
                expo = RPTWEAVERISSUEDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERISSUEDTLS.Export()
            ElseIf FRMSTRING = "WEAVERISSUESUMM" Then
                expo = RPTWEAVERISSUESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERISSUESUMM.Export()
            ElseIf FRMSTRING = "WEAVERQUALITYWISEDTLS" Then
                expo = RPTWEAVERQUALITYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERQUALITYDTLS.Export()
            ElseIf FRMSTRING = "WEAVERQUALITYWISESUMM" Then
                expo = RPTWEAVERQUALITYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERQUALITYSUMM.Export()
            ElseIf FRMSTRING = "WEAVERMILLWISEDTLS" Then
                expo = RPTWEAVERMILLDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERMILLDTLS.Export()
            ElseIf FRMSTRING = "WEAVERMILLWISESUMM" Then
                expo = RPTWEAVERMILLSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERMILLSUMM.Export()
            ElseIf FRMSTRING = "WEAVERWISEDTLS" Then
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
            ElseIf FRMSTRING = "WEAVERTRANSWISEDTLS" Then
                expo = RPTWEAVERTRANSDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERTRANSDTLS.Export()
            ElseIf FRMSTRING = "WEAVERTRANSWISESUMM" Then
                expo = RPTWEAVERTRANSSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERTRANSSUMM.Export()


            ElseIf FRMSTRING = "DYEINGISSUEDTLS" Then
                expo = RPTDYEINGISSUEDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDYEINGISSUEDTLS.Export()
            ElseIf FRMSTRING = "DYEINGISSUESUMM" Then
                expo = RPTDYEINGISSUESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDYEINGISSUESUMM.Export()
            ElseIf FRMSTRING = "DYEINGQUALITYWISEDTLS" Then
                expo = RPTDYEINGQUALITYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDYEINGQUALITYDTLS.Export()
            ElseIf FRMSTRING = "DYEINGQUALITYWISESUMM" Then
                expo = RPTDYEINGQUALITYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDYEINGQUALITYSUMM.Export()
            ElseIf FRMSTRING = "DYEINGMILLWISEDTLS" Then
                expo = RPTDYEINGMILLDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDYEINGMILLDTLS.Export()
            ElseIf FRMSTRING = "DYEINGMILLWISESUMM" Then
                expo = RPTDYEINGMILLSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDYEINGMILLSUMM.Export()
            ElseIf FRMSTRING = "DYEINGWISEDTLS" Then
                expo = RPTDYEINGDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDYEINGDTLS.Export()
            ElseIf FRMSTRING = "DYEINGWISESUMM" Then
                expo = RPTDYEINGSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDYEINGSUMM.Export()
            ElseIf FRMSTRING = "DYEINGTRANSWISEDTLS" Then
                expo = RPTDYEINGTRANSDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDYEINGTRANSDTLS.Export()
            ElseIf FRMSTRING = "DYEINGTRANSWISESUMM" Then
                expo = RPTDYEINGTRANSSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDYEINGTRANSSUMM.Export()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()

            If FRMSTRING = "YARNISSUEWEAVER" Then
                tempattachment = "YARNISSUEWEAVER"
            ElseIf FRMSTRING = "DEBIT" Or FRMSTRING = "PROFORMADEBIT" Then
                tempattachment = "DEBIT NOTE"
            End If

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = PARTYNAME
            OBJWHATSAPP.AGENTNAME = AGENTNAME
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & tempattachment & ".PDF")
            OBJWHATSAPP.FILENAME.Add(tempattachment & ".pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTDIRECTLYTOPRINTER()
        Try
            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

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

            'strsearch = "{PACKINGSLIP.PS_NO} = " & PSNO & " AND {PACKINGSLIP.PS_YEARID} = " & YearId
            'CRPO.SelectionFormula = strsearch
            Dim OBJ As New Object
            If FRMSTRING = "YARNISSUEWEAVER" Then
                strsearch = "  {YARNISSUEWEAVER.YISSUEWEAVER_no} = " & ISSUENO & " AND {YARNISSUEWEAVER.YISSUEWEAVER_yearid} = " & YearId
                OBJ = New YarnIssueWeaverReport
            ElseIf FRMSTRING = "YARNISSUEWARPER" Then
                strsearch = "  {YARNISSUEWARPER.YISSUEWARPER_no} = " & ISSUENO & " AND {YARNISSUEWARPER.YISSUEWARPER_yearid} = " & YearId
                OBJ = New YarnIssueWarperReport
            ElseIf FRMSTRING = "YARNISSUESIZER" Then
                strsearch = "  {YARNISSUESIZER.YISSUESIZER_no} = " & ISSUENO & " AND {YARNISSUESIZER.YISSUESIZER_yearid} = " & YearId
                OBJ = New YarnIssueSizerReport
            ElseIf FRMSTRING = "YARNISSUEDYEING" Then
                strsearch = "  {YARNISSUEDYEING.YISSUEDYEING_no} = " & ISSUENO & " AND {YARNISSUEDYEING.YISSUEDYEING_yearid} = " & YearId
                OBJ = New YarnIssueDyeingReport
            End If

SKIPINVOICE:
            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = strsearch
            OBJ.REFRESH()

            If DIRECTWHATSAPP = False Then
                OBJ.RecordSelectionFormula = strsearch
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            End If


            'If DIRECTMAIL = False Then
            '    OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
            '    OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            'Else
            '    Dim expo As New ExportOptions
            '    Dim oDfDopt As New DiskFileDestinationOptions
            '    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE_" & INVNO & ".pdf"
            '    expo = OBJ.ExportOptions
            '    If ClientName = "SAKARIA" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = 1
            '    expo.ExportDestinationType = ExportDestinationType.DiskFile
            '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
            '    expo.DestinationOptions = oDfDopt
            '    OBJ.Export()
            'End If

            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions
                If FRMSTRING = "YARNISSUEWEAVER" Then
                    OBJ.RecordSelectionFormula = strsearch
                    oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_YARNISSUEWEAVER_" & ISSUENO & ".pdf"
                ElseIf FRMSTRING = "YARNISSUEWARPER" Then
                    OBJ.RecordSelectionFormula = strsearch
                    oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_YARNISSUEWARPER_" & ISSUENO & ".pdf"
                ElseIf FRMSTRING = "YARNISSUESIZER" Then
                    OBJ.RecordSelectionFormula = strsearch
                    oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_YARNISSUESIZER_" & ISSUENO & ".pdf"
                ElseIf FRMSTRING = "YARNISSUEDYEING" Then
                    OBJ.RecordSelectionFormula = strsearch
                    oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_YARNISSUEDYEING_" & ISSUENO & ".pdf"
                End If


                'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                'If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)
                'OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                expo = OBJ.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJ.Export()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class