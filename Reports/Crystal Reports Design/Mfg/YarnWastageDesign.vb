
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class YarnWastageDesign

    Public WHERECLAUSE As String = ""
    Public FRMSTRING As String = ""
    Public PERIOD As String = ""

    Dim RPTWARPERALL As New YarnWastageWarperAllDataReport
    Dim RPTWARPERDTLS As New YarnWastageWarperDtlsReport
    Dim RPTWARPERSUMM As New YarnWastageWarperSummReport
    Dim RPTWARPERQUALITYDTLS As New YarnWastageWarperQualityDtlsReport
    Dim RPTWARPERQUALITYSUMM As New YarnWastageWarperQualitySummReport
    Dim RPTWARPERTYPEDTLS As New YarnWastageWarperTypeDtlsReport
    Dim RPTWARPERTYPESUMM As New YarnWastageWarperTypeSummReport

    Dim RPTSIZERALL As New YarnWastageAllDataReport
    Dim RPTSIZERDTLS As New YarnWastageSizerDtlsReport
    Dim RPTSIZERSUMM As New YarnWastageSizerSummReport
    Dim RPTSIZERQUALITYDTLS As New YarnWastageSizerQualityDtlsReport
    Dim RPTSIZERQUALITYSUMM As New YarnWastageSizerQualitySummReport
    Dim RPTSIZERTYPEDTLS As New YarnWastageSizerTypeDtlsReport
    Dim RPTSIZERTYPESUMM As New YarnWastageSizerTypeSummReport

    Dim RPTWEAVERALL As New YarnWastageWeaverAllDataReport
    Dim RPTWEAVERDTLS As New YarnWastageWeaverDtlsReport
    Dim RPTWEAVERSUMM As New YarnWastageWeaverSummReport
    Dim RPTWEAVERQUALITYDTLS As New YarnWastageWeaverQualityDtlsReport
    Dim RPTWEAVERQUALITYSUMM As New YarnWastageWeaverQualitySummReport
    Dim RPTWEAVERTYPEDTLS As New YarnWastageWeaverTypeDtlsReport
    Dim RPTWEAVERTYPESUMM As New YarnWastageWeaverTypeSummReport

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

            If FRMSTRING = "YARNWASTAGEWARPERALLDATA" Then
                crTables = RPTWARPERALL.Database.Tables
            ElseIf FRMSTRING = "WARPERWISEDTLS" Then
                crTables = RPTWARPERDTLS.Database.Tables
            ElseIf FRMSTRING = "WARPERWISESUMM" Then
                crTables = RPTWARPERSUMM.Database.Tables
            ElseIf FRMSTRING = "WARPERQUALITYWISEDTLS" Then
                crTables = RPTWARPERQUALITYDTLS.Database.Tables
            ElseIf FRMSTRING = "WARPERQUALITYWISESUMM" Then
                crTables = RPTWARPERQUALITYSUMM.Database.Tables
            ElseIf FRMSTRING = "WARPERTYPEWISEDTLS" Then
                crTables = RPTWARPERTYPEDTLS.Database.Tables
            ElseIf FRMSTRING = "WARPERTYPEWISESUMM" Then
                crTables = RPTWARPERTYPEDTLS.Database.Tables


            ElseIf FRMSTRING = "YARNWASTAGESIZERALLDATA" Then
                crTables = RPTSIZERALL.Database.Tables
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                crTables = RPTSIZERDTLS.Database.Tables
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                crTables = RPTSIZERSUMM.Database.Tables
            ElseIf FRMSTRING = "SIZERQUALITYWISEDTLS" Then
                crTables = RPTSIZERQUALITYDTLS.Database.Tables
            ElseIf FRMSTRING = "SIZERQUALITYWISESUMM" Then
                crTables = RPTSIZERQUALITYSUMM.Database.Tables
            ElseIf FRMSTRING = "SIZERTYPEWISEDTLS" Then
                crTables = RPTSIZERTYPEDTLS.Database.Tables
            ElseIf FRMSTRING = "SIZERTYPEWISESUMM" Then
                crTables = RPTSIZERTYPEDTLS.Database.Tables


            ElseIf FRMSTRING = "YARNWASTAGESWEAVERALLDATA" Then
                crTables = RPTWEAVERALL.Database.Tables
            ElseIf FRMSTRING = "WEAVERWISEDTLS" Then
                crTables = RPTWEAVERDTLS.Database.Tables
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                crTables = RPTWEAVERSUMM.Database.Tables
            ElseIf FRMSTRING = "WEAVERQUALITYWISEDTLS" Then
                crTables = RPTWEAVERQUALITYDTLS.Database.Tables
            ElseIf FRMSTRING = "WEAVERQUALITYWISESUMM" Then
                crTables = RPTWEAVERQUALITYSUMM.Database.Tables
            ElseIf FRMSTRING = "WEAVERTYPEWISEDTLS" Then
                crTables = RPTWEAVERTYPEDTLS.Database.Tables
            ElseIf FRMSTRING = "WEAVERTYPEWISESUMM" Then
                crTables = RPTWEAVERTYPESUMM.Database.Tables
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "YARNWASTAGEWARPERALLDATA" Then
                CRPO.ReportSource = RPTWARPERALL
            ElseIf FRMSTRING = "WARPERWISEDTLS" Then
                RPTWARPERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' WARPER WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERDTLS
            ElseIf FRMSTRING = "WARPERWISESUMM" Then
                RPTWARPERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' WARPER WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERSUMM
            ElseIf FRMSTRING = "WARPERQUALITYWISEDTLS" Then
                RPTWARPERQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERQUALITYDTLS
            ElseIf FRMSTRING = "WARPERQUALITYWISESUMM" Then
                RPTWARPERQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERQUALITYSUMM
            ElseIf FRMSTRING = "WARPERTYPEWISEDTLS" Then
                RPTWARPERTYPEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' TYPE WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERTYPEDTLS
            ElseIf FRMSTRING = "WARPERMILLWISESUMM" Then
                RPTWARPERTYPESUMM.DataDefinition.FormulaFields("PERIOD").Text = "' TYPE WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWARPERTYPESUMM

           


            ElseIf FRMSTRING = "YARNWASTAGESIZERALLDATA" Then
                CRPO.ReportSource = RPTSIZERALL
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                RPTSIZERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' SIZER WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERDTLS
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                RPTSIZERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' SIZER WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERSUMM
            ElseIf FRMSTRING = "SIZERQUALITYWISEDTLS" Then
                RPTSIZERQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERQUALITYDTLS
            ElseIf FRMSTRING = "SIZERQUALITYWISESUMM" Then
                RPTSIZERQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERQUALITYSUMM
            ElseIf FRMSTRING = "SIZERTYPEWISEDTLS" Then
                RPTSIZERTYPEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' TYPE WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERTYPEDTLS
            ElseIf FRMSTRING = "SIZERTYPEWISESUMM" Then
                RPTSIZERTYPESUMM.DataDefinition.FormulaFields("PERIOD").Text = "' TYPE WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERTYPESUMM
           

            ElseIf FRMSTRING = "YARNWASTAGEWEAVERALLDATA" Then
                CRPO.ReportSource = RPTWEAVERALL
            ElseIf FRMSTRING = "WEAVERWISEDTLS" Then
                RPTWEAVERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' WEAVER WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERDTLS
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                RPTWEAVERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' WEAVER WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERSUMM
            ElseIf FRMSTRING = "WEAVERQUALITYWISEDTLS" Then
                RPTWEAVERQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERQUALITYDTLS
            ElseIf FRMSTRING = "WEAVERQUALITYWISESUMM" Then
                RPTWEAVERQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERQUALITYSUMM
            ElseIf FRMSTRING = "WEAVERTYPEWISEDTLS" Then
                RPTWEAVERTYPEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' TYPE WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERTYPEDTLS
            ElseIf FRMSTRING = "WEAVERTYPEWISESUMM" Then
                RPTWEAVERTYPESUMM.DataDefinition.FormulaFields("PERIOD").Text = "' TYPE WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERTYPESUMM
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



            If FRMSTRING = "YARNWASTAGEWARPERALLDATA" Then
                expo = RPTWARPERALL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWARPERALL.Export()
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
            ElseIf FRMSTRING = "WARPERTYPEWISEDTLS" Then
                expo = RPTWARPERTYPEDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWARPERTYPEDTLS.Export()
            ElseIf FRMSTRING = "WARPERTYPEWISESUMM" Then
                expo = RPTWARPERTYPESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWARPERTYPESUMM.Export()
           

            ElseIf FRMSTRING = "YARNWASTAGESIZERALLDATA" Then
                expo = RPTSIZERALL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERALL.Export()
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
            ElseIf FRMSTRING = "SIZERTYPEWISEDTLS" Then
                expo = RPTSIZERTYPEDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERTYPEDTLS.Export()
            ElseIf FRMSTRING = "SIZERTYPEWISESUMM" Then
                expo = RPTSIZERTYPESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERTYPESUMM.Export()
           

            ElseIf FRMSTRING = "YARNWASTAGEWEAVERALLDATA" Then
                expo = RPTWEAVERALL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERALL.Export()
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
            ElseIf FRMSTRING = "WEAVERTYPEWISEDTLS" Then
                expo = RPTWEAVERTYPEDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERTYPEDTLS.Export()
            ElseIf FRMSTRING = "WEAVERTYPEWISESUMM" Then
                expo = RPTWEAVERTYPESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERTYPESUMM.Export()
          
           
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

End Class
