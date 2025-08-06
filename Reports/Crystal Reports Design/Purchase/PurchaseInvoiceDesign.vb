
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class PurchaseInvoiceDesign
    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Public PERIOD As String
    Public BILLTYPE As String


    Dim RPTBILL As New PurchaseInvoiceReport
    Dim RPTBILLDATA_MASHOK As New PurchaseDataReport_MASHOK
    Dim RPTDTLS As New PurchaseDetails
    Dim RPTGREYDTLS As New PurchaseGreyDetails
    Dim RPTOTHERDTLS As New PurchaseOtherDetails

    Dim RPTSUMM As New PurchaseSummary

    Dim RPTPARTYDTLS As New PurchasePartyWiseDetails
    Dim RPTGREYPARTYDTLS As New PurchaseGreyPartyWiseDetails
    Dim RPTOTHERPARTYDTLS As New PurchaseOtherPartyWiseDetails

    Dim RPTPARTYSUMM As New PurchasePartyWiseSummary
    Dim RPTGREYPARTYSUMM As New PurchaseGreyPartyWiseSummary
    Dim RPTOTHERPARTYSUMM As New PurchaseOtherPartyWiseSummary

    Dim RPTBROKERDTLS As New PurchaseAgentWiseDetails
    Dim RPTGREYBROKERDTLS As New PurchaseGreyAgentWiseDetails

    Dim RPTBROKERSUMM As New PurchaseAgentWiseSummary
    Dim RPTGREYBROKERSUMM As New PurchaseGreyAgentWiseSummary

    Dim RPTQUALITYDTLS As New PurchaseQualityWiseDetails
    Dim RPTGREYQUALITYDTLS As New PurchaseGreyQualityWiseDetails

    Dim RPTQUALITYSUMM As New PurchaseQualityWiseSummary
    Dim RPTGREYQUALITYSUMM As New PurchaseGreyQualityWiseSummary

    Dim RPTMILLDTLS As New PurchaseMillWiseDetails
    Dim RPTGREYMILLDTLS As New PurchaseGreyMillWiseDetails

    Dim RPTMILLSUMM As New PurchaseMillWiseSummary
    Dim RPTGREYMILLSUMM As New PurchaseGreyMillWiseSummary

    Dim RPTTRANSDTLS As New PurchaseTransWiseDetails

    Dim RPTTRANSSUMM As New PurchaseTransWiseSummary

    Dim RPTMONTHLY As New PurchaseMonthWise
    Dim RPTGREYMONTHLY As New PurchaseGreyMonthWise
    Dim RPTOTHERMONTHLY As New PurchaseOtherMonthWise

    Dim RPTAVGMONTHLY As New PurchaseAvgMonthWise
    Dim RPTGREYAVGMONTHLY As New PurchaseGreyAvgMonthWise

    Dim RPTAVGQUALITY As New PurchaseAvgQualityWiseSummary
    Dim RPTGREYAVGQUALITY As New PurchaseGreyAvgQualityWiseSummary

    Dim RPTREGISTERDTLS As New PurchaseRegisterWiseDetails
    Dim RPTGREYREGISTERDTLS As New PurchaseGreyRegisterWiseDetails
    Dim RPTOTHERREGISTERDTLS As New PurchaseOtherRegisterWiseDetails
    Dim RPTGSTREGISTERDTLS As New PurchaseGSTRegisterWiseDetails
    Dim RPTGSTREGISTERSUMM As New PurchaseGSTRegisterWiseSummary

    Public NEWPAGE As Boolean
    Public ADDRESS As Integer
    Public SHOWHEADER As Boolean
    Public SHOWPRINTDATE As Boolean

    Dim tempattachment As String
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PARTYNAME As String
    Public AGENTNAME As String
    Public BILLNO As Integer
    Public REGNAME As String
    Public DIRECTPRINT As Boolean = False
    Public NOOFCOPIES As Integer = 1
    Public PRINTSETTING As Object = Nothing
    Public registername As String



    Private Sub PurchaseInvoiceDesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

            If FRMSTRING = "PURBILL" Then crTables = RPTBILL.Database.Tables
            If FRMSTRING = "MASHOKPURDATA" Then crTables = RPTBILLDATA_MASHOK.Database.Tables

            If FRMSTRING = "PURDTLS" Then crTables = RPTDTLS.Database.Tables
            If FRMSTRING = "PURSUMM" Then crTables = RPTSUMM.Database.Tables


            If FRMSTRING = "PARTYWISEDTLS" Then crTables = RPTPARTYDTLS.Database.Tables
            If FRMSTRING = "PARTYWISESUMM" Then crTables = RPTPARTYSUMM.Database.Tables


            If FRMSTRING = "BROKERWISEDTLS" Then crTables = RPTBROKERDTLS.Database.Tables
            If FRMSTRING = "BROKERWISESUMM" Then crTables = RPTBROKERSUMM.Database.Tables

            If FRMSTRING = "QUALITYWISEDTLS" Then crTables = RPTQUALITYDTLS.Database.Tables
            If FRMSTRING = "QUALITYWISESUMM" Then crTables = RPTQUALITYSUMM.Database.Tables

            If FRMSTRING = "TRANSWISEDTLS" Then crTables = RPTTRANSDTLS.Database.Tables
            If FRMSTRING = "TRANSWISESUMM" Then crTables = RPTTRANSSUMM.Database.Tables

            If FRMSTRING = "MILLWISEDTLS" Then crTables = RPTMILLDTLS.Database.Tables
            If FRMSTRING = "MILLWISESUMM" Then crTables = RPTMILLSUMM.Database.Tables

            If FRMSTRING = "MONTHLY" Then crTables = RPTMONTHLY.Database.Tables

            If FRMSTRING = "AVGPURQUALITYWISESUMM" Then crTables = RPTAVGQUALITY.Database.Tables
            If FRMSTRING = "AVGPURMONTHLY" Then crTables = RPTAVGMONTHLY.Database.Tables

            If FRMSTRING = "REGISTERDTLS" Then crTables = RPTREGISTERDTLS.Database.Tables


            'for grey quality

            If FRMSTRING = "PURGREYDTLS" Then crTables = RPTGREYDTLS.Database.Tables

            If FRMSTRING = "PARTYWISEGREYDTLS" Then crTables = RPTGREYPARTYDTLS.Database.Tables
            If FRMSTRING = "PARTYWISEGREYSUMM" Then crTables = RPTGREYPARTYSUMM.Database.Tables

            If FRMSTRING = "BROKERWISEGREYDTLS" Then crTables = RPTGREYBROKERDTLS.Database.Tables
            If FRMSTRING = "BROKERWISEGREYSUMM" Then crTables = RPTGREYBROKERSUMM.Database.Tables

            If FRMSTRING = "QUALITYWISEGREYDTLS" Then crTables = RPTGREYQUALITYDTLS.Database.Tables
            If FRMSTRING = "QUALITYWISEGREYSUMM" Then crTables = RPTGREYQUALITYSUMM.Database.Tables

            If FRMSTRING = "MILLWISEGREYDTLS" Then crTables = RPTGREYMILLDTLS.Database.Tables
            If FRMSTRING = "MILLWISEGREYSUMM" Then crTables = RPTGREYMILLSUMM.Database.Tables

            If FRMSTRING = "MONTHLYGREY" Then crTables = RPTGREYMONTHLY.Database.Tables

            If FRMSTRING = "AVGPURQUALITYWISEGREYSUMM" Then crTables = RPTGREYAVGQUALITY.Database.Tables
            If FRMSTRING = "AVGPURMONTHLYGREY" Then crTables = RPTGREYAVGMONTHLY.Database.Tables

            If FRMSTRING = "REGISTERDTLSGREY" Then crTables = RPTGREYREGISTERDTLS.Database.Tables


            'FOR OTHER PURCHASE
            If FRMSTRING = "PURDTLSOTHER" Then crTables = RPTOTHERDTLS.Database.Tables
            If FRMSTRING = "PARTYWISEOTHERDTLS" Then crTables = RPTOTHERPARTYDTLS.Database.Tables
            If FRMSTRING = "PARTYWISEOTHERSUMM" Then crTables = RPTOTHERPARTYSUMM.Database.Tables
            If FRMSTRING = "OTHERMONTHLY" Then crTables = RPTOTHERMONTHLY.Database.Tables
            If FRMSTRING = "OTHERREGISTERDTLS" Then crTables = RPTOTHERREGISTERDTLS.Database.Tables
            If FRMSTRING = "GSTREGISTERDTLS" Then crTables = RPTGSTREGISTERDTLS.Database.Tables
            If FRMSTRING = "GSTREGISTERSUMM" Then crTables = RPTGSTREGISTERSUMM.Database.Tables


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "PURDTLS" Then
                CRPO.ReportSource = RPTDTLS
                RPTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "PURBILL" Then
                CRPO.ReportSource = RPTBILL

            ElseIf FRMSTRING = "MASHOKPURDATA" Then
                CRPO.ReportSource = RPTBILLDATA_MASHOK

            ElseIf FRMSTRING = "PURSUMM" Then
                CRPO.ReportSource = RPTSUMM
                RPTSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "PARTYWISEDTLS" Then
                CRPO.ReportSource = RPTPARTYDTLS
                RPTPARTYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTPARTYDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "PARTYWISESUMM" Then
                CRPO.ReportSource = RPTPARTYSUMM
                RPTPARTYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"


            ElseIf FRMSTRING = "BROKERWISEDTLS" Then
                CRPO.ReportSource = RPTBROKERDTLS
                RPTBROKERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTBROKERDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTBROKERDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTBROKERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTBROKERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTBROKERDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTBROKERDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "BROKERWISESUMM" Then
                CRPO.ReportSource = RPTBROKERSUMM
                RPTBROKERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTBROKERSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTBROKERSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTBROKERSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTBROKERSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                CRPO.ReportSource = RPTQUALITYDTLS
                RPTQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTQUALITYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTQUALITYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTQUALITYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTQUALITYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTQUALITYDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                CRPO.ReportSource = RPTQUALITYSUMM
                RPTQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTQUALITYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTQUALITYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTQUALITYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTQUALITYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "MILLWISEDTLS" Then
                CRPO.ReportSource = RPTMILLDTLS
                RPTMILLDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTMILLDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTMILLDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTMILLDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTMILLDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTMILLDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTMILLDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "MILLWISESUMM" Then
                CRPO.ReportSource = RPTMILLSUMM
                RPTMILLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTMILLSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTMILLSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTMILLSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTMILLSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                CRPO.ReportSource = RPTTRANSDTLS
                RPTTRANSDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                'RPTTRANSDTLS.REPORTH.SectionFormat.EnableSuppress = Not SHOWHEADER
                If SHOWPRINTDATE = True Then RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTTRANSDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "TRANSWISESUMM" Then
                CRPO.ReportSource = RPTTRANSSUMM
                RPTTRANSSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWPRINTDATE = True Then RPTTRANSSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTTRANSSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTTRANSSUMM.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS

            ElseIf FRMSTRING = "MONTHLY" Then
                CRPO.ReportSource = RPTMONTHLY
                RPTMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWPRINTDATE = True Then RPTMONTHLY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTMONTHLY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTMONTHLY.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS

            ElseIf FRMSTRING = "AVGPURQUALITYWISESUMM" Then
                CRPO.ReportSource = RPTAVGQUALITY
                RPTAVGQUALITY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWPRINTDATE = True Then RPTAVGQUALITY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTAVGQUALITY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
            ElseIf FRMSTRING = "AVGPURMONTHLY" Then
                CRPO.ReportSource = RPTAVGMONTHLY
            ElseIf FRMSTRING = "REGISTERDTLS" Then
                CRPO.ReportSource = RPTREGISTERDTLS
                RPTREGISTERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWPRINTDATE = True Then RPTREGISTERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTREGISTERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            End If



            'for grey quality

            If FRMSTRING = "PURGREYDTLS" Then
                CRPO.ReportSource = RPTGREYDTLS
                RPTGREYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGREYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGREYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGREYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGREYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTGREYDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTGREYDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE


            ElseIf FRMSTRING = "PARTYWISEGREYDTLS" Then
                CRPO.ReportSource = RPTGREYPARTYDTLS
                RPTGREYPARTYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGREYPARTYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGREYPARTYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGREYPARTYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGREYPARTYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTGREYPARTYDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTGREYPARTYDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "PARTYWISEGREYSUMM" Then
                CRPO.ReportSource = RPTGREYPARTYSUMM
                RPTGREYPARTYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGREYPARTYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGREYPARTYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGREYPARTYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGREYPARTYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "BROKERWISEGREYDTLS" Then
                CRPO.ReportSource = RPTGREYBROKERDTLS
                RPTGREYBROKERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGREYBROKERDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGREYBROKERDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGREYBROKERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGREYBROKERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTGREYBROKERDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTGREYBROKERDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "BROKERWISEGREYSUMM" Then
                CRPO.ReportSource = RPTGREYBROKERSUMM
                RPTGREYBROKERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGREYBROKERSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGREYBROKERSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGREYBROKERSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGREYBROKERSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "QUALITYWISEGREYDTLS" Then
                CRPO.ReportSource = RPTGREYQUALITYDTLS
                RPTGREYQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGREYQUALITYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGREYQUALITYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGREYQUALITYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGREYQUALITYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTGREYQUALITYDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "QUALITYWISEGREYSUMM" Then
                CRPO.ReportSource = RPTGREYQUALITYSUMM
                RPTGREYQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGREYQUALITYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGREYQUALITYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGREYQUALITYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGREYQUALITYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "MILLWISEGREYDTLS" Then
                CRPO.ReportSource = RPTGREYMILLDTLS
                RPTGREYMILLDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGREYMILLDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGREYMILLDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGREYMILLDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGREYMILLDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTGREYMILLDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTGREYMILLDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "MILLWISEGREYSUMM" Then
                CRPO.ReportSource = RPTGREYMILLSUMM
                RPTGREYMILLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGREYMILLSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGREYMILLSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGREYMILLSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGREYMILLSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"



            ElseIf FRMSTRING = "MONTHLYGREY" Then
                CRPO.ReportSource = RPTGREYMONTHLY
                RPTGREYMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWPRINTDATE = True Then RPTGREYMONTHLY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGREYMONTHLY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTGREYMONTHLY.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS

            ElseIf FRMSTRING = "AVGPURQUALITYWISEGREYSUMM" Then
                CRPO.ReportSource = RPTGREYAVGQUALITY
                RPTGREYAVGQUALITY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWPRINTDATE = True Then RPTGREYAVGQUALITY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGREYAVGQUALITY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "AVGPURMONTHLYGREY" Then
                CRPO.ReportSource = RPTGREYAVGMONTHLY

            ElseIf FRMSTRING = "REGISTERDTLSGREY" Then
                CRPO.ReportSource = RPTGREYREGISTERDTLS
                RPTGREYREGISTERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWPRINTDATE = True Then RPTGREYREGISTERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGREYREGISTERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
            End If


            'FOR OTHER PURCHASE
            If FRMSTRING = "PURDTLSOTHER" Then
                CRPO.ReportSource = RPTOTHERDTLS
                RPTOTHERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTOTHERDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTOTHERDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTOTHERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTOTHERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTOTHERDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTOTHERDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE


            ElseIf FRMSTRING = "PARTYWISEOTHERDTLS" Then
                CRPO.ReportSource = RPTOTHERPARTYDTLS
                RPTOTHERPARTYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTOTHERPARTYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTOTHERPARTYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTOTHERPARTYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTOTHERPARTYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTOTHERPARTYDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTOTHERPARTYDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "PARTYWISEOTHERSUMM" Then
                CRPO.ReportSource = RPTOTHERPARTYSUMM
                RPTOTHERPARTYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTOTHERPARTYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTOTHERPARTYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTOTHERPARTYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTOTHERPARTYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                'RPTOTHERPARTYSUMM.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                'RPTOTHERPARTYSUMM.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE


            ElseIf FRMSTRING = "OTHERMONTHLY" Then
                CRPO.ReportSource = RPTOTHERMONTHLY
                RPTOTHERMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWPRINTDATE = True Then RPTOTHERMONTHLY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTOTHERMONTHLY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTOTHERMONTHLY.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS


            ElseIf FRMSTRING = "OTHERREGISTERDTLS" Then
                CRPO.ReportSource = RPTOTHERREGISTERDTLS
                RPTOTHERREGISTERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWPRINTDATE = True Then RPTOTHERREGISTERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTOTHERREGISTERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "GSTREGISTERDTLS" Then
                CRPO.ReportSource = RPTGSTREGISTERDTLS
                RPTGSTREGISTERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWPRINTDATE = True Then RPTGSTREGISTERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGSTREGISTERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "GSTREGISTERSUMM" Then
                CRPO.ReportSource = RPTGSTREGISTERSUMM
                RPTGSTREGISTERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWPRINTDATE = True Then RPTGSTREGISTERSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGSTREGISTERSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            End If


            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Sub PurchaseInvoiceDesign_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\PURCHASE.pdf"

            If FRMSTRING = "PURBILL" Then
                expo = RPTBILL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBILL.Export()
            ElseIf FRMSTRING = "MASHOKPURDATA" Then
                expo = RPTBILLDATA_MASHOK.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBILLDATA_MASHOK.Export()
            ElseIf FRMSTRING = "PARTYWISEDTLS" Then
                expo = RPTPARTYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPARTYDTLS.Export()
            ElseIf FRMSTRING = "PARTYWISESUMM" Then
                expo = RPTPARTYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPARTYSUMM.Export()
            ElseIf FRMSTRING = "BROKERWISEDTLS" Then
                expo = RPTBROKERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBROKERDTLS.Export()
            ElseIf FRMSTRING = "BROKERWISESUMM" Then
                expo = RPTBROKERSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBROKERSUMM.Export()
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
            ElseIf FRMSTRING = "MILLWISEDTLS" Then
                expo = RPTMILLDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTMILLDTLS.Export()
            ElseIf FRMSTRING = "MILLWISESUMM" Then
                expo = RPTMILLSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTMILLSUMM.Export()
            ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                expo = RPTTRANSDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTTRANSDTLS.Export()
            ElseIf FRMSTRING = "TRANSWISESUMM" Then
                expo = RPTTRANSSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTTRANSSUMM.Export()
            ElseIf FRMSTRING = "MONTHLY" Then
                expo = RPTMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTMONTHLY.Export()
            ElseIf FRMSTRING = "AVGPURQUALITYWISESUMM" Then
                expo = RPTAVGQUALITY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTAVGQUALITY.Export()
            ElseIf FRMSTRING = "AVGPURMONTHLY" Then
                expo = RPTAVGMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTAVGMONTHLY.Export()
            ElseIf FRMSTRING = "REGISTERDTLS" Then
                expo = RPTREGISTERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTREGISTERDTLS.Export()
            End If



            'for grey quality


            If FRMSTRING = "PARTYWISEGREYDTLS" Then
                expo = RPTGREYPARTYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYPARTYDTLS.Export()
            ElseIf FRMSTRING = "PARTYWISEGREYSUMM" Then
                expo = RPTGREYPARTYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYPARTYSUMM.Export()
            ElseIf FRMSTRING = "BROKERWISEGREYDTLS" Then
                expo = RPTGREYBROKERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYBROKERDTLS.Export()
            ElseIf FRMSTRING = "BROKERWISEGREYSUMM" Then
                expo = RPTGREYBROKERSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYBROKERSUMM.Export()
            ElseIf FRMSTRING = "QUALITYWISEGREYDTLS" Then
                expo = RPTGREYQUALITYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYQUALITYDTLS.Export()
            ElseIf FRMSTRING = "QUALITYWISEGREYSUMM" Then
                expo = RPTGREYQUALITYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYQUALITYSUMM.Export()
            ElseIf FRMSTRING = "MILLWISEGREYDTLS" Then
                expo = RPTGREYMILLDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYMILLDTLS.Export()
            ElseIf FRMSTRING = "MILLWISEGREYSUMM" Then
                expo = RPTGREYMILLSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYMILLSUMM.Export()
         
            ElseIf FRMSTRING = "MONTHLYGREY" Then
                expo = RPTGREYMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYMONTHLY.Export()
            ElseIf FRMSTRING = "AVGPURQUALITYWISEGREYSUMM" Then
                expo = RPTGREYAVGQUALITY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYAVGQUALITY.Export()
            ElseIf FRMSTRING = "AVGPURMONTHLYGREY" Then
                expo = RPTGREYAVGMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYAVGMONTHLY.Export()
            ElseIf FRMSTRING = "REGISTERGREYDTLS" Then
                expo = RPTGREYREGISTERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYREGISTERDTLS.Export()
            End If

            'OTHER PURCHASE

            If FRMSTRING = "PURDTLSOTHER" Then
                expo = RPTOTHERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOTHERDTLS.Export()
            ElseIf FRMSTRING = "PARTYWISEOTHERDTLS" Then
                expo = RPTOTHERPARTYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOTHERPARTYDTLS.Export()
            ElseIf FRMSTRING = "PARTYWISEOTHERSUMM" Then
                expo = RPTOTHERPARTYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOTHERPARTYSUMM.Export()
            ElseIf FRMSTRING = "OTHERMONTHLY" Then
                expo = RPTOTHERMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOTHERMONTHLY.Export()
            ElseIf FRMSTRING = "OTHERREGISTERDTLS" Then
                expo = RPTOTHERREGISTERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOTHERREGISTERDTLS.Export()
            ElseIf FRMSTRING = "GSTREGISTERDTLS" Then
                expo = RPTGSTREGISTERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGSTREGISTERDTLS.Export()
            ElseIf FRMSTRING = "GSTREGISTERSUMM" Then
                expo = RPTGSTREGISTERSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGSTREGISTERSUMM.Export()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub SENDMAILTOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SENDMAILTOOL.Click
        Try
            Dim emailid As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Transfer()

            Dim TEMPATTACHMENT As String = Application.StartupPath & "\PURCHASE.pdf"
            Dim objmail As New SendMail
            objmail.attachment = TEMPATTACHMENT
            objmail.subject = "Purchase Details"
            If emailid <> "" Then
                objmail.cmbfirstadd.Text = emailid
            End If
            objmail.Show()
            objmail.BringToFront()
            Windows.Forms.Cursor.Current = Cursors.Arrow
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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
            If FRMSTRING = "PURBILL" Then
                strsearch = "{PURCHASEMASTER.BILL_NO} = " & BILLNO & " AND {PURCHASEMASTER.BILL_YEARID} = " & YearId
            End If


            Dim OBJ As New Object
            If FRMSTRING = "PURBILL" Then
                OBJ = New PurchaseInvoiceReport
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
                If FRMSTRING = "PURBILL" Then
                    OBJ.RecordSelectionFormula = strsearch
                    oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_PURCHASEINVOICE_NO-" & BILLNO & ".pdf"
                End If


                'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                'If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)
                'OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                'expo = OBJ.ExportOptions
                'expo.ExportDestinationType = ExportDestinationType.DiskFile
                'expo.ExportFormatType = ExportFormatType.PortableDocFormat
                'expo.DestinationOptions = oDfDopt
                'OBJ.Export()
                'OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
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

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()

            If FRMSTRING = "PURBILL" Then
                tempattachment = "PURBILL"

                'ElseIf FRMSTRING = "DEBIT" Or FRMSTRING = "PROFORMADEBIT" Then
                '    tempattachment = "DEBIT NOTE"

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
End Class