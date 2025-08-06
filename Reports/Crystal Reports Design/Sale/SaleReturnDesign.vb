
Imports System.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO
Imports BL

Public Class SaleReturnDesign

    Dim RPTSALERETDETAILSREPORT As New SaleRetDetailsReport
  
    Dim RPTSALERETPARTYWISEDETAILS As New SaleRetPartyWiseDetails
    Dim RPTSALERETPARTYWISESUMMARY As New SaleRetPartyWiseSummary

    Dim RPTSALERETAGENTWISEDETAILS As New SaleRetAgentWiseDetails
    Dim RPTSALERETAGENTWISESUMMARY As New SaleRetAgentWiseSummary


    Dim RPTSALERETQUALITYWISEDETAILS As New SaleRetQualityWiseDetails
    Dim RPTSALERETQUALITYWISESUMMARY As New SaleRetQualityWiseSummary


    Dim RPTSALERETMONTHLY As New SaleRetMonthly
  
    Dim RPTSALERETREGISTERWISEDETAILS As New SaleRetRegisterWiseDetails

    Dim RPTSALERETREPORT As New SaleReturnReport
    Dim RPTSALERETGODOWNWISEDETAILS As New SaleRetGodownWiseDetails
    Dim RPTSALERETGODOWNWISESUMMARY As New SaleRetGodownWiseSummary


    Dim RPTSALERETTRANSWISEDETAILS As New SaleRetTransWiseDetails
    Dim RPTSALERETTRANSWISESUMMARY As New SaleRetTransWiseSummary



    Public WHERECLAUSE As String
    Public PERIOD As String
    Public strsumm As String
    Public FRMSTRING As String
    Public registername As String
    Public FROMDATE As Date
    Public TODATE As Date
    Public strsearch As String
    Public INVNO As Integer
    Public COMM As Double
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

    Public NEWPAGE As Boolean
    Public ADDRESS As Integer
    Public SHOWHEADER As Boolean
    Public SHOWPRINTDATE As Boolean

    Public FORMULA As String
    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1
    Dim tempattachment As String
    Public SALERETNO As Integer
    Public PARTYNAME As String
    Public AGENTNAME As String

    Sub getFromToDate()
        a1 = DatePart(DateInterval.Day, FROMDATE)
        a2 = DatePart(DateInterval.Month, FROMDATE)
        a3 = DatePart(DateInterval.Year, FROMDATE)
        fromD = "(" & a3 & "," & a2 & "," & a1 & ")"

        a11 = DatePart(DateInterval.Day, TODATE)
        a12 = DatePart(DateInterval.Month, TODATE)
        a13 = DatePart(DateInterval.Year, TODATE)
        toD = "(" & a13 & "," & a12 & "," & a11 & ")"
    End Sub

    Private Sub SaleReturndesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleReturndesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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


            If FRMSTRING = "SALERETURN" Then crTables = RPTSALERETREPORT.Database.Tables
            If FRMSTRING = "SALERETURNDTLS" Then crTables = RPTSALERETDETAILSREPORT.Database.Tables


            If FRMSTRING = "PARTYWISEDTLS" Then crTables = RPTSALERETPARTYWISEDETAILS.Database.Tables
            If FRMSTRING = "PARTYWISESUMM" Then crTables = RPTSALERETPARTYWISESUMMARY.Database.Tables


            If FRMSTRING = "GODOWNWISEDTLS" Then crTables = RPTSALERETGODOWNWISEDETAILS.Database.Tables
            If FRMSTRING = "GODOWNWISESUMM" Then crTables = RPTSALERETGODOWNWISESUMMARY.Database.Tables


            If FRMSTRING = "TRANSWISEDTLS" Then crTables = RPTSALERETTRANSWISEDETAILS.Database.Tables
            If FRMSTRING = "TRANSWISESUMM" Then crTables = RPTSALERETTRANSWISESUMMARY.Database.Tables



            If FRMSTRING = "JOBBERWISEDTLS" Then crTables = RPTSALERETAGENTWISEDETAILS.Database.Tables
            If FRMSTRING = "JOBBERWISESUMM" Then crTables = RPTSALERETAGENTWISESUMMARY.Database.Tables


            If FRMSTRING = "QUALITYWISEDTLS" Then crTables = RPTSALERETQUALITYWISEDETAILS.Database.Tables
            If FRMSTRING = "QUALITYWISESUMM" Then crTables = RPTSALERETQUALITYWISESUMMARY.Database.Tables



            If FRMSTRING = "MONTHLY" Then crTables = RPTSALERETMONTHLY.Database.Tables



            If FRMSTRING = "REGISTERDTLS" Then crTables = RPTSALERETREGISTERWISEDETAILS.Database.Tables


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next


            '************************ END *******************
            getFromToDate()


            CRPO.SelectionFormula = WHERECLAUSE


            If FRMSTRING = "SALERETURN" Then
                If ClientName = "NIRMALA" Or ClientName = "YESHA" Then RPTSALERETREPORT.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                CRPO.ReportSource = RPTSALERETREPORT
                If ClientName = "NIRMALA" Or ClientName = "YESHA" Then RPTSALERETREPORT.DataDefinition.FormulaFields("SENDMAIL").Text = 1

            ElseIf FRMSTRING = "SALERETURNDTLS" Then
                CRPO.ReportSource = RPTSALERETDETAILSREPORT
                RPTSALERETDETAILSREPORT.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSALERETDETAILSREPORT.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSALERETDETAILSREPORT.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSALERETDETAILSREPORT.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSALERETDETAILSREPORT.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "PARTYWISEDTLS" Then
                CRPO.ReportSource = RPTSALERETPARTYWISEDETAILS
                RPTSALERETPARTYWISEDETAILS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSALERETPARTYWISEDETAILS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSALERETPARTYWISEDETAILS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSALERETPARTYWISEDETAILS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSALERETPARTYWISEDETAILS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTSALERETPARTYWISEDETAILS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                'RPTSALERETPARTYWISEDETAILS.GroupFooterSection2.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "PARTYWISESUMM" Then
                CRPO.ReportSource = RPTSALERETPARTYWISESUMMARY
                RPTSALERETPARTYWISESUMMARY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSALERETPARTYWISESUMMARY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSALERETPARTYWISESUMMARY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSALERETPARTYWISESUMMARY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSALERETPARTYWISESUMMARY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "JOBBERWISEDTLS" Then
                CRPO.ReportSource = RPTSALERETAGENTWISEDETAILS
                RPTSALERETAGENTWISEDETAILS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSALERETAGENTWISEDETAILS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSALERETAGENTWISEDETAILS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSALERETAGENTWISEDETAILS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSALERETAGENTWISEDETAILS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTSALERETAGENTWISEDETAILS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTSALERETAGENTWISEDETAILS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "JOBBERWISESUMM" Then
                CRPO.ReportSource = RPTSALERETAGENTWISESUMMARY
                RPTSALERETAGENTWISESUMMARY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSALERETAGENTWISESUMMARY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSALERETAGENTWISESUMMARY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSALERETAGENTWISESUMMARY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSALERETAGENTWISESUMMARY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                CRPO.ReportSource = RPTSALERETQUALITYWISEDETAILS
                RPTSALERETQUALITYWISEDETAILS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSALERETQUALITYWISEDETAILS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSALERETQUALITYWISEDETAILS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSALERETQUALITYWISEDETAILS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSALERETQUALITYWISEDETAILS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTSALERETQUALITYWISEDETAILS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTSALERETQUALITYWISEDETAILS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                CRPO.ReportSource = RPTSALERETQUALITYWISESUMMARY
                RPTSALERETQUALITYWISESUMMARY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSALERETQUALITYWISESUMMARY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSALERETQUALITYWISESUMMARY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSALERETQUALITYWISESUMMARY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSALERETQUALITYWISESUMMARY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"


            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                CRPO.ReportSource = RPTSALERETGODOWNWISEDETAILS
                RPTSALERETGODOWNWISEDETAILS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSALERETGODOWNWISEDETAILS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSALERETGODOWNWISEDETAILS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSALERETGODOWNWISEDETAILS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSALERETGODOWNWISEDETAILS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTSALERETGODOWNWISEDETAILS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTSALERETGODOWNWISEDETAILS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                CRPO.ReportSource = RPTSALERETGODOWNWISESUMMARY
                RPTSALERETGODOWNWISESUMMARY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSALERETGODOWNWISESUMMARY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSALERETGODOWNWISESUMMARY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSALERETGODOWNWISESUMMARY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSALERETGODOWNWISESUMMARY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                CRPO.ReportSource = RPTSALERETTRANSWISEDETAILS
                RPTSALERETTRANSWISEDETAILS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSALERETTRANSWISEDETAILS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSALERETTRANSWISEDETAILS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSALERETTRANSWISEDETAILS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSALERETTRANSWISEDETAILS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTSALERETTRANSWISEDETAILS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTSALERETTRANSWISEDETAILS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "TRANSWISESUMM" Then
                CRPO.ReportSource = RPTSALERETTRANSWISESUMMARY
                RPTSALERETTRANSWISESUMMARY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSALERETTRANSWISESUMMARY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSALERETTRANSWISESUMMARY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSALERETTRANSWISESUMMARY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSALERETTRANSWISESUMMARY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"


            ElseIf FRMSTRING = "MONTHLY" Then
                CRPO.ReportSource = RPTSALERETMONTHLY
                RPTSALERETMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSALERETMONTHLY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSALERETMONTHLY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSALERETMONTHLY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSALERETMONTHLY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "REGISTERDTLS" Then
                CRPO.ReportSource = RPTSALERETREGISTERWISEDETAILS
                RPTSALERETREGISTERWISEDETAILS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSALERETREGISTERWISEDETAILS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSALERETREGISTERWISEDETAILS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSALERETREGISTERWISEDETAILS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSALERETREGISTERWISEDETAILS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"


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

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()
        Dim tempattachment As String

        If FRMSTRING = "SALERETURN" Then
            tempattachment = "SALERETURN"
        ElseIf FRMSTRING = "SUPPLIERWISEDTLS" Then
            tempattachment = "SALERETURNDETAILS"
        ElseIf FRMSTRING = "SUPPLIERCOMMWISEDTLS" Then
            tempattachment = "COMMISSIONDEBITNOTE"
        Else
            tempattachment = "SALESUMMARY"
        End If
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

            If FRMSTRING = "SALERETURN" Then
                If ClientName = "NIRMALA" Or ClientName = "YESHA" Then RPTSALERETREPORT.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                oDfDopt.DiskFileName = Application.StartupPath & "\SALERETURN.pdf"
                expo = RPTSALERETREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALERETREPORT.Export()
                If ClientName = "NIRMALA" Or ClientName = "YESHA" Then RPTSALERETREPORT.DataDefinition.FormulaFields("SENDMAIL").Text = 0
            ElseIf FRMSTRING = "PARTYWISEDTLS" Then
                expo = RPTSALERETPARTYWISEDETAILS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALERETPARTYWISEDETAILS.Export()
            ElseIf FRMSTRING = "PARTYWISESUMM" Then
                expo = RPTSALERETPARTYWISESUMMARY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALERETPARTYWISESUMMARY.Export()
            ElseIf FRMSTRING = "JOBBERWISEDTLS" Then
                expo = RPTSALERETAGENTWISEDETAILS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALERETAGENTWISEDETAILS.Export()
            ElseIf FRMSTRING = "MONTHLY" Then
                expo = RPTSALERETMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALERETMONTHLY.Export()
            ElseIf FRMSTRING = "JOBBERWISESUMM" Then
                expo = RPTSALERETAGENTWISESUMMARY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALERETAGENTWISESUMMARY.Export()

            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                expo = RPTSALERETQUALITYWISEDETAILS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALERETQUALITYWISEDETAILS.Export()
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                expo = RPTSALERETQUALITYWISESUMMARY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALERETQUALITYWISESUMMARY.Export()


            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                expo = RPTSALERETGODOWNWISEDETAILS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALERETGODOWNWISEDETAILS.Export()
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                expo = RPTSALERETGODOWNWISESUMMARY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALERETGODOWNWISESUMMARY.Export()


            ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                expo = RPTSALERETTRANSWISEDETAILS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALERETTRANSWISEDETAILS.Export()
            ElseIf FRMSTRING = "TRANSWISESUMM" Then
                expo = RPTSALERETTRANSWISESUMMARY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALERETTRANSWISESUMMARY.Export()

            ElseIf FRMSTRING = "REGISTERDTLS" Then
                expo = RPTSALERETREGISTERWISEDETAILS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSALERETREGISTERWISEDETAILS.Export()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub


    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()

            If FRMSTRING = "SALERETURN" Then
                tempattachment = "SALERETURN"

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

            If FRMSTRING = "SALERETURN" Then
                strsearch = "{SALERETURN.SALRET_NO} = " & SALERETNO & " AND {SALERETURN.SALRET_YEARID} = " & YearId
                CRPO.SelectionFormula = strsearch
            End If

            Dim OBJ As New Object
            If FRMSTRING = "SALERETURN" Then
                OBJ = New SaleReturnReport
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


            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions
                If FRMSTRING = "SALERETURN" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_SALERETURN_NO-" & SALERETNO & ".pdf"
                End If


                'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                'If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)
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