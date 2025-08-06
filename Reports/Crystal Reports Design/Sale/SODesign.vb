
Imports BL
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class SODesign

    Dim RPTSO As New SOReport
    Public FORMULA As String
    Dim RPTSODTLS As New SOAllDataDetails
    Dim RPTSOPARTYDTLS As New SOPartyWiseDetails
    Dim RPTSOPARTYSUMM As New SOPartyWiseSummary
    Dim RPTSOAGENTDTLS As New SOAgentWiseDetails
    Dim RPTSOAGENTSUMM As New SOAgentWiseSummary
    Dim RPTSOQUALITYDTLS As New SOQualityWiseDetails
    Dim RPTSOQUALITYSUMM As New SOQualityWiseSummary
    Dim RPTSOPENDING As New SOPendingDetails
    Dim RPTSOCLOSED As New SOPendingDetails
    Dim RPTSOCHALLANDTLS As New SOChallanDetails


    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Public PERIOD As String

    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1
    Dim tempattachment As String
    Public SONO As Integer
    Public PARTYNAME As String
    Public AGENTNAME As String

    Private Sub CRPO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CRPO.Load
        Try

            If DIRECTPRINT = True Then
                PRINTDIRECTLYTOPRINTER()
                Exit Sub
            End If

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

            If FRMSTRING = "SALEORDER" Then
                crTables = RPTSO.Database.Tables
            ElseIf FRMSTRING = "SOALLDATADTLS" Then
                crTables = RPTSODTLS.Database.Tables
            ElseIf FRMSTRING = "SOPARTYWISEDTLS" Then
                crTables = RPTSOPARTYDTLS.Database.Tables
            ElseIf FRMSTRING = "SOPARTYWISESUMM" Then
                crTables = RPTSOAGENTSUMM.Database.Tables
            ElseIf FRMSTRING = "SOAGENTWISEDTLS" Then
                crTables = RPTSOAGENTDTLS.Database.Tables
            ElseIf FRMSTRING = "SOAGENTWISESUMM" Then
                crTables = RPTSOAGENTSUMM.Database.Tables
            ElseIf FRMSTRING = "SOQUALITYWISEDTLS" Then
                crTables = RPTSOQUALITYDTLS.Database.Tables
            ElseIf FRMSTRING = "SOQUALITYWISESUMM" Then
                crTables = RPTSOQUALITYSUMM.Database.Tables
            ElseIf FRMSTRING = "SOPENDINGDTLS" Then
                crTables = RPTSOPENDING.Database.Tables
            ElseIf FRMSTRING = "SOCLOSEDDTLS" Then
                crTables = RPTSOCLOSED.Database.Tables
            ElseIf FRMSTRING = "SOCHALLANDTLS" Then
                crTables = RPTSOCHALLANDTLS.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "SALEORDER" Then
                CRPO.ReportSource = RPTSO
                RPTSO.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "SOALLDATADTLS" Then
                CRPO.ReportSource = RPTSODTLS
                RPTSODTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "SOPARTYWISEDTLS" Then
                CRPO.ReportSource = RPTSOPARTYDTLS
                RPTSOPARTYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "SOPARTYWISESUMM" Then
                CRPO.ReportSource = RPTSOPARTYSUMM
                RPTSOPARTYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "SOAGENTWISEDTLS" Then
                CRPO.ReportSource = RPTSOAGENTDTLS
                RPTSOAGENTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "SOAGENTWISESUMM" Then
                CRPO.ReportSource = RPTSOAGENTSUMM
                RPTSOAGENTSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "SOQUALITYWISEDTLS" Then
                CRPO.ReportSource = RPTSOQUALITYDTLS
                RPTSOQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "SOQUALITYWISESUMM" Then
                CRPO.ReportSource = RPTSOQUALITYSUMM
                RPTSOQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "SOPENDINGDTLS" Then
                CRPO.ReportSource = RPTSOPENDING
                RPTSOPENDING.DataDefinition.FormulaFields("PERIOD").Text = "' Sale Order Pending Report     " & PERIOD & "'"
            ElseIf FRMSTRING = "SOCLOSEDDTLS" Then
                CRPO.ReportSource = RPTSOCLOSED
                RPTSOCLOSED.DataDefinition.FormulaFields("PERIOD").Text = "'Sale Order Closed Report    " & PERIOD & "'"
            ElseIf FRMSTRING = "SOCHALLANDTLS" Then
                CRPO.ReportSource = RPTSOCHALLANDTLS
                RPTSOCHALLANDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            End If
            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch Exp As LoadSaveReportException
            MsgBox("Incorrect path for loading report.",
                    MsgBoxStyle.Critical, "Load Report Error")

        Catch Exp As Exception
            MsgBox(Exp.Message, MsgBoxStyle.Critical, "General Error")

        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()

            If FRMSTRING = "SALEORDER" Then
                tempattachment = "SALEORDER"

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

            If FRMSTRING = "SALEORDER" Then
                strsearch = "{SALEORDER.SO_NO} = " & SONO & " AND {SALEORDER.SO_YEARID} = " & YearId
                CRPO.SelectionFormula = strsearch
            End If

            Dim OBJ As New Object
            If FRMSTRING = "SALEORDER" Then
                OBJ = New SOReport
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
                If FRMSTRING = "SALEORDER" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_SOREPORT_NO-" & SONO & ".pdf"
                End If


                'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)
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

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()
        Dim tempattachment As String = "Sale Order"
        Try
            Dim objmail As New SendMail
            objmail.attachment = Application.StartupPath & "\" & tempattachment & ".PDF"
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

            oDfDopt.DiskFileName = Application.StartupPath & "\Sale Order.PDF"
            If FRMSTRING = "SALEORDER" Then
                expo = RPTSO.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSO.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub SODesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class