
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO
Imports BL

Public Class registerdesign

    Dim rptBr As New bankregistermonthly
    Dim rptBrd As New bankregisterdetailreport
    Dim rptcr As New cashregistermonthly
    Dim rptdb As New daybookreport
    Dim rptdbd As New daybookdetailreport
    Dim rptpr As New purchaseregisterreport
    Dim rptprd As New purchaseregisterdetailreport
    Dim rptsr As New saleregisterreport
    Dim rptsrd As New saleregisterdetailreport



    Dim rptsrm As New saleregistermonthly
    Dim rptprm As New purchaseregistermonthly
    Dim rptconr As New contraregisterreport
    Dim rptconrd As New contraregisterdetailreport
    Dim rptconrm As New contraregistermonthly
    Dim rptjvr As New jvregisterreport
    Dim rptjvrd As New jvregisterdetailreport
    Dim rptjvrm As New jvregistermonthly


    Dim RPTLEDGERAC As New LedgerBookAcReport
    Dim rptlbc As New LedgerBookAcConfirmReport
    Dim RPTLBCSUMM As New LedgerBookConfirmationReport_Summ
    Dim RPTLEDGERACSUMM As New LedgerBookAcSummReport

    Dim RPTSTATEMENT As New FFormPartyStatement
    Dim RPTSTATEMENTDTLS As New FFormPartyStatement_Details
    Dim RPTLEDGERACDETAILS As New LedgerBookAcReportDetails
    Dim RPTLEDGERACTFORMAT As New LedgerBookAcReport_TReport

    Public NEWPAGE As Boolean
    Public ADDRESS As Integer
    Public PANNO As Integer
    Public INDEX As Boolean
    Public strsearch As String
    Public frmstring As String
    Public reg As String
    Public OPENING As Double
    Public DRTOTAL As Double
    Public CRTOTAL As Double
    Public CLOSINGAMT As Double
    Public crdr As String
    Public CLOSINGDRCR As String
    Public PARTYNAME As String
    Public PERIOD As String
    Public FROMDATE As Date
    Public TODATE As Date
    Public CHECK As Boolean
    Public SHOWHEADER As Boolean
    Public SHOWNARRATION As Boolean = False

    Public bankname As String

    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

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

    Private Sub CRPO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CRPO.Load
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

            getFromToDate()
            With crConnecttionInfo
                .ServerName = Servername
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            If frmstring = "CashRegister" Or frmstring = "BankRegister" Or frmstring = "BankBookDetails" Then
                If frmstring = "CashRegister" Then
                    crTables = rptcr.Database.Tables
                ElseIf frmstring = "BankRegister" Then
                    crTables = rptBr.Database.Tables
                ElseIf frmstring = "BankBookDetails" Then
                    crTables = rptBrd.Database.Tables
                End If
            ElseIf frmstring = "DayBook" Or frmstring = "DayBookDetails" Then
                If frmstring = "DayBook" Then
                    crTables = rptdb.Database.Tables
                ElseIf frmstring = "DayBookDetails" Then
                    crTables = rptdbd.Database.Tables
                End If
            ElseIf frmstring = "PurchaseRegister" Or frmstring = "PurchaseRegisterDetails" Then
                If frmstring = "PurchaseRegister" Then
                    crTables = rptpr.Database.Tables
                ElseIf frmstring = "PurchaseRegisterDetails" Then
                    crTables = rptprd.Database.Tables
                End If
            ElseIf frmstring = "JvRegister" Or frmstring = "JvRegisterDetails" Or frmstring = "JvRegisterMonthly" Then
                If frmstring = "JvRegister" Then
                    crTables = rptjvr.Database.Tables
                ElseIf frmstring = "JvRegisterDetails" Then
                    crTables = rptjvrd.Database.Tables
                ElseIf frmstring = "JvRegisterMonthly" Then
                    crTables = rptjvrm.Database.Tables
                End If
            ElseIf frmstring = "ContraRegister" Or frmstring = "ContraRegisterDetails" Then
                If frmstring = "ContraRegister" Then
                    crTables = rptconr.Database.Tables
                ElseIf frmstring = "ContraRegisterDetails" Then
                    crTables = rptconrd.Database.Tables
                End If

            ElseIf frmstring = "SaleRegister" Or frmstring = "SaleRegisterDetails" Then
                If frmstring = "SaleRegister" Then
                    crTables = rptsr.Database.Tables
                ElseIf frmstring = "SaleRegisterDetails" Then
                    crTables = rptsrd.Database.Tables
                End If
            ElseIf frmstring = "LedgerBook" Or frmstring = "LedgerBookDetails" Or frmstring = "LedgerBookConfirm" Or frmstring = "LedgerBookConfirmSumm" Or frmstring = "LedgerBookTFormat" Or frmstring = "LedgerPartySumm" Then
                If frmstring = "LedgerBook" Then
                    crTables = RPTLEDGERAC.Database.Tables
                ElseIf frmstring = "LedgerBookTFormat" Then
                    crTables = RPTLEDGERACTFORMAT.Database.Tables
                ElseIf frmstring = "LedgerBookDetails" Then
                    crTables = RPTLEDGERACDETAILS.Database.Tables
                ElseIf frmstring = "LedgerBookConfirm" Then
                    crTables = rptlbc.Database.Tables
                ElseIf frmstring = "LedgerBookConfirmSumm" Then
                    crTables = RPTLBCSUMM.Database.Tables
                ElseIf frmstring = "LedgerPartySumm" Then
                    crTables = RPTLEDGERACSUMM.Database.Tables
                End If

            ElseIf frmstring = "PARTYSTATEMENT" Then
                crTables = RPTSTATEMENT.Database.Tables
            ElseIf frmstring = "PARTYSTATEMENTDTLS" Then
                crTables = RPTSTATEMENTDTLS.Database.Tables
            ElseIf frmstring = "SaleRegisterMonthly" Or frmstring = "PurchaseRegisterMonthly" Then
                If frmstring = "SaleRegisterMonthly" Then
                    crTables = rptsrm.Database.Tables
                ElseIf frmstring = "PurchaseRegisterMonthly" Then
                    crTables = rptprm.Database.Tables
                End If
           
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next
            '************************ END *******************

            If frmstring = "CashRegister" Or frmstring = "BankRegister" Or frmstring = "BankBookDetails" Then
                If frmstring = "CashRegister" Then
                    crParameterFieldDefinitions = rptcr.DataDefinition.ParameterFields
                ElseIf frmstring = "BankRegister" Then
                    crParameterFieldDefinitions = rptBr.DataDefinition.ParameterFields
                    'ElseIf frmstring = "BankBookDetails" Then
                    'crParameterFieldDefinitions = rptBrd.DataDefinition.ParameterFields
                End If
                crParameterDiscreteValue.Value = reg
                If frmstring = "CashRegister" Then
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CASHNAME")
                ElseIf frmstring = "BankRegister" Then
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@BANKNAME")
                    'ElseIf frmstring = "BankBookDetails" Then
                    '  crParameterFieldDefinition = crParameterFieldDefinitions.Item("@BANKNAME")
                End If

                If frmstring <> "BankBookDetails" Then
                    crParameterValues = crParameterFieldDefinition.CurrentValues

                    crParameterValues.Clear()
                End If

                If frmstring <> "BankBookDetails" Then
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                    crParameterDiscreteValue.Value = CHECK
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CHECK")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                    crParameterDiscreteValue.Value = CmpId
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CMPID")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                    crParameterDiscreteValue.Value = Locationid
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@LOCATIONID")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                    crParameterDiscreteValue.Value = YearId
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@YEARID")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)


                    crParameterDiscreteValue.Value = FROMDATE.Date
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@FROMDATE")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)


                    If frmstring = "CashRegister" Then
                        rptcr.DataDefinition.FormulaFields("OPENING").Text = OPENING
                    ElseIf frmstring = "BankRegister" Then
                        rptBr.DataDefinition.FormulaFields("OPENING").Text = OPENING
                    End If
                    crParameterDiscreteValue.Value = TODATE.Date
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("@TODATE")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)
                End If
                If frmstring = "CashRegister" Then
                    CRPO.ReportSource = rptcr
                ElseIf frmstring = "BankRegister" Then
                    CRPO.ReportSource = rptBr
                ElseIf frmstring = "BankBookDetails" Then
                    strsearch = strsearch & " {REPORT_SP_BANKBOOK_DETAILS.Name}='" & PARTYNAME & "'"
                    strsearch = strsearch & " and {@DATE} in date " & fromD & " to date " & toD
                    strsearch = strsearch & "  and {REPORT_SP_BANKBOOK_DETAILS.CMPID}=" & CmpId & " and {REPORT_SP_BANKBOOK_DETAILS.LOCATIONID}=" & Locationid & " and {REPORT_SP_BANKBOOK_DETAILS.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = rptBrd
                End If
            ElseIf frmstring = "DayBook" Or frmstring = "DayBookDetails" Then


                If frmstring = "DayBook" Then
                    strsearch = strsearch & " {@DATE} in date " & fromD & " to date " & toD
                    strsearch = strsearch & " and {REPORT_SP_DAYBOOK.CMPID}=" & CmpId & " AND {REPORT_SP_DAYBOOK.LOCATIONID}=" & Locationid & " AND {REPORT_SP_DAYBOOK.YEARID}=" & YearId

                    CRPO.SelectionFormula = strsearch

                    CRPO.ReportSource = rptdb
                ElseIf frmstring = "DayBookDetails" Then
                    strsearch = strsearch & " ({@DATE} in date " & fromD & " to date " & toD & ")"
                    strsearch = strsearch & "  and {REPORT_SP_DAYBOOK_DETAILS.CMPID}=" & CmpId & " and {REPORT_SP_DAYBOOK_DETAILS.LOCATIONID}=" & Locationid & " and {REPORT_SP_DAYBOOK_DETAILS.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = rptdbd
                End If

                'for purchase
            ElseIf frmstring = "PurchaseRegister" Or frmstring = "PurchaseRegisterDetails" Then


                If frmstring = "PurchaseRegister" Then
                    If reg <> "" Then
                        strsearch = strsearch & " {REGISTERMASTER.REGISTER_NAME}='" & reg & "'"
                        strsearch = strsearch & " and ({@DATE} in date " & fromD & " to date " & toD & ")"
                    Else
                        strsearch = strsearch & "({@DATE} in date " & fromD & " to date " & toD & ")"
                    End If

                    strsearch = strsearch & "  and {PURCHASEMASTER.BILL_YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch

                    CRPO.ReportSource = rptpr
                ElseIf frmstring = "PurchaseRegisterDetails" Then
                    If reg <> "" Then
                        strsearch = strsearch & " {REPORT_SP_PURCHASEDETAILS.regtype}='" & reg & "'"
                        strsearch = strsearch & " and ({@DATE} in date " & fromD & " to date " & toD & ")"
                    Else
                        strsearch = strsearch & " ({@DATE} in date " & fromD & " to date " & toD & ")"
                    End If

                    strsearch = strsearch & "  and {REPORT_SP_PURCHASEDETAILS.CMPID}=" & CmpId & "  and {REPORT_SP_PURCHASEDETAILS.LOCATIONID}=" & Locationid & "  and {REPORT_SP_PURCHASEDETAILS.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = rptprd
                End If

            ElseIf frmstring = "JvRegister" Or frmstring = "JvRegisterDetails" Or frmstring = "JvRegisterMonthly" Then
                If frmstring = "JvRegister" Then
                    If reg <> "" Then
                        strsearch = strsearch & " {REPORT_SP_JOURNALSUMMARY.regtype}='" & reg & "'"
                        strsearch = strsearch & " and ({@DATE} in date " & fromD & " to date " & toD & ")"
                    Else
                        strsearch = strsearch & "({@DATE} in date " & fromD & " to date " & toD & ")"
                    End If

                    strsearch = strsearch & "  and {REPORT_SP_JOURNALSUMMARY.CMPID}=" & CmpId & "  and {REPORT_SP_JOURNALSUMMARY.LOCATIONID}=" & Locationid & "  and {REPORT_SP_JOURNALSUMMARY.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch

                    CRPO.ReportSource = rptjvr
                ElseIf frmstring = "JvRegisterDetails" Then
                    If reg <> "" Then
                        strsearch = strsearch & " {REPORT_SP_JOURNALDETAILS.regtype}='" & reg & "'"
                        strsearch = strsearch & " and ({@DATE} in date " & fromD & " to date " & toD & ")"
                    Else
                        strsearch = strsearch & " ({@DATE} in date " & fromD & " to date " & toD & ")"
                    End If

                    strsearch = strsearch & "  and {REPORT_SP_JOURNALDETAILS.CMPID}=" & CmpId & " and {REPORT_SP_JOURNALDETAILS.LOCATIONID}=" & Locationid & " and {REPORT_SP_JOURNALDETAILS.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = rptjvrd

                ElseIf frmstring = "JvRegisterMonthly" Then

                End If

            ElseIf frmstring = "ContraRegister" Or frmstring = "ContraRegisterDetails" Then
                If frmstring = "ContraRegister" Then
                    If reg <> "" Then
                        strsearch = strsearch & " {REPORT_SP_ContraSUMMARY.regtype}='" & reg & "'"
                        strsearch = strsearch & " and ({@DATE} in date " & fromD & " to date " & toD & ")"
                    Else
                        strsearch = strsearch & "({@DATE} in date " & fromD & " to date " & toD & ")"
                    End If

                    strsearch = strsearch & "  and {REPORT_SP_contraSUMMARY.CMPID}=" & CmpId & "  and {REPORT_SP_contraSUMMARY.LOCATIONID}=" & Locationid & "  and {REPORT_SP_contraSUMMARY.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch

                    CRPO.ReportSource = rptconr
                ElseIf frmstring = "ContraRegisterDetails" Then
                    If reg <> "" Then
                        strsearch = strsearch & " {REPORT_SP_contra_DETAILS.regtype}='" & reg & "'"
                        strsearch = strsearch & " and ({@DATE} in date " & fromD & " to date " & toD & ")"
                    Else
                        strsearch = strsearch & " ({@DATE} in date " & fromD & " to date " & toD & ")"
                    End If

                    strsearch = strsearch & "  and {REPORT_SP_contra_DETAILS.CMPID}=" & CmpId & "  and {REPORT_SP_contra_DETAILS.LOCATIONID}=" & Locationid & "  and {REPORT_SP_contra_DETAILS.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = rptconrd
                End If

                'for sale register
            ElseIf frmstring = "SaleRegister" Or frmstring = "SaleRegisterDetails" Then

                If frmstring = "SaleRegister" Then
                    If reg <> "" Then
                        strsearch = strsearch & " {REPORT_SP_accounts_invoiceSUMMARY.regtype}='" & reg & "'"
                        strsearch = strsearch & " and ({@DATE} in date " & fromD & " to date " & toD & ")"
                    Else
                        strsearch = strsearch & "({@DATE} in date " & fromD & " to date " & toD & ")"
                    End If

                    strsearch = strsearch & "  and {REPORT_SP_accounts_invoiceSUMMARY.CMPID}=" & CmpId & "  and {REPORT_SP_accounts_invoiceSUMMARY.LOCATIONID}=" & Locationid & "  and {REPORT_SP_accounts_invoiceSUMMARY.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch

                    CRPO.ReportSource = rptsr
                ElseIf frmstring = "SaleRegisterDetails" Then
                    If reg <> "" Then
                        strsearch = strsearch & " {REPORT_SP_INVOICEDETAILS.regtype}='" & reg & "'"
                        strsearch = strsearch & " and ({@DATE} in date " & fromD & " to date " & toD & ")"
                    Else
                        strsearch = strsearch & " ({@DATE} in date " & fromD & " to date " & toD & ")"
                    End If

                    strsearch = strsearch & "  and {REPORT_SP_INVOICEDETAILS.CMPID}=" & CmpId & "  and {REPORT_SP_INVOICEDETAILS.LOCATIONID}=" & Locationid & "  and {REPORT_SP_INVOICEDETAILS.YEARID}=" & YearId
                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = rptsrd
                End If


            ElseIf frmstring = "PARTYSTATEMENT" Then


                RPTSTATEMENT.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If OPENING > 0 Then RPTSTATEMENT.DataDefinition.FormulaFields("OPBALDR").Text = OPENING Else RPTSTATEMENT.DataDefinition.FormulaFields("OPBALCR").Text = OPENING * (-1)
                strsearch = strsearch & " and {@DATE} in date " & fromD & " to date " & toD
                If SHOWHEADER = True Then RPTSTATEMENT.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSTATEMENT.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                RPTSTATEMENT.ReportHeaderSection1.SectionFormat.EnableSuppress = Not SHOWHEADER

                CRPO.SelectionFormula = strsearch
                CRPO.ReportSource = RPTSTATEMENT
                RPTSTATEMENT.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf frmstring = "PARTYSTATEMENTDTLS" Then


                RPTSTATEMENTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If OPENING > 0 Then RPTSTATEMENTDTLS.DataDefinition.FormulaFields("OPBALDR").Text = OPENING Else RPTSTATEMENTDTLS.DataDefinition.FormulaFields("OPBALCR").Text = OPENING * (-1)
                strsearch = strsearch & " and {@DATE} in date " & fromD & " to date " & toD
                If SHOWHEADER = True Then RPTSTATEMENTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSTATEMENTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                RPTSTATEMENTDTLS.ReportHeaderSection1.SectionFormat.EnableSuppress = Not SHOWHEADER

                CRPO.SelectionFormula = strsearch
                CRPO.ReportSource = RPTSTATEMENTDTLS
                RPTSTATEMENTDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

                'ledger Book
            ElseIf frmstring = "LedgerBook" Or frmstring = "LedgerBookDetails" Or frmstring = "LedgerBookConfirm" Or frmstring = "LedgerBookConfirmSumm" Or frmstring = "LedgerBookTFormat" Or frmstring = "LedgerPartySumm" Then

                'If frmstring = "LedgerBook" Then

                '    RPTLEDGERAC.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                '    RPTLEDGERAC.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                '    RPTLEDGERAC.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                '    CRPO.SelectionFormula = strsearch
                '    CRPO.ReportSource = RPTLEDGERAC
                '    RPTLEDGERAC.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                '    RPTLEDGERAC.Section4.SectionFormat.EnableSuppress = Not (INDEX)
                '    RPTLEDGERAC.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                '    'RPTLEDGERAC.DataDefinition.FormulaFields("PANNO").Text = PANNO

                'ElseIf frmstring = "LedgerBookTFormat" Then

                '    RPTLEDGERACTFORMAT.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                '    RPTLEDGERACTFORMAT.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                '    RPTLEDGERACTFORMAT.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                '    RPTLEDGERACTFORMAT.Subreports("LedgerBook_Debit").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                '    RPTLEDGERACTFORMAT.Subreports("LedgerBook_Debit").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                '    RPTLEDGERACTFORMAT.Subreports("LedgerBook_Credit").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                '    RPTLEDGERACTFORMAT.Subreports("LedgerBook_Credit").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                '    CRPO.SelectionFormula = strsearch
                '    CRPO.ReportSource = RPTLEDGERACTFORMAT
                '    RPTLEDGERACTFORMAT.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                '    RPTLEDGERACTFORMAT.Section4.SectionFormat.EnableSuppress = Not (INDEX)

                'ElseIf frmstring = "LedgerBookDetails" Then

                '    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                '    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                '    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                '    CRPO.SelectionFormula = strsearch
                '    CRPO.ReportSource = RPTLEDGERACDETAILS
                '    RPTLEDGERACDETAILS.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                '    RPTLEDGERACDETAILS.Section4.SectionFormat.EnableSuppress = Not (INDEX)
                '    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                '    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("PANNO").Text = PANNO

                'ElseIf frmstring = "LedgerBookConfirm" Then

                '    rptlbc.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                '    rptlbc.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                '    rptlbc.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                '    CRPO.SelectionFormula = strsearch
                '    CRPO.ReportSource = rptlbc
                '    rptlbc.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                '    rptlbc.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS

                'ElseIf frmstring = "LedgerBookConfirmSumm" Then

                '    RPTLBCSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                '    RPTLBCSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                '    'rptlbc.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                '    CRPO.SelectionFormula = strsearch
                '    CRPO.ReportSource = RPTLBCSUMM
                '    RPTLBCSUMM.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                '    RPTLBCSUMM.Section4.SectionFormat.EnableSuppress = Not (INDEX)
                '    RPTLBCSUMM.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS

                'ElseIf frmstring = "LedgerPartySumm" Then

                '    RPTLEDGERACSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                '    RPTLEDGERACSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(FROMDATE.Date, "dd/MM/yyyy") & "'"
                '    RPTLEDGERACSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(TODATE.Date, "dd/MM/yyyy") & "'"

                '    CRPO.SelectionFormula = strsearch
                '    CRPO.ReportSource = RPTLEDGERACSUMM

                'End If


                If frmstring = "LedgerBook" Then

                    RPTLEDGERAC.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    RPTLEDGERAC.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERAC.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = RPTLEDGERAC
                    RPTLEDGERAC.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                    RPTLEDGERAC.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                    If SHOWNARRATION = True Then RPTLEDGERAC.DataDefinition.FormulaFields("SHOWREMARKS").Text = 1

                ElseIf frmstring = "LedgerBookTFormat" Then

                    RPTLEDGERACTFORMAT.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    RPTLEDGERACTFORMAT.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACTFORMAT.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACTFORMAT.Subreports("LedgerBook_Debit").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACTFORMAT.Subreports("LedgerBook_Debit").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACTFORMAT.Subreports("LedgerBook_Credit").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACTFORMAT.Subreports("LedgerBook_Credit").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = RPTLEDGERACTFORMAT
                    RPTLEDGERACTFORMAT.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                    RPTLEDGERACTFORMAT.Section4.SectionFormat.EnableSuppress = Not (INDEX)

                ElseIf frmstring = "LedgerBookDetails" Then

                    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = RPTLEDGERACDETAILS
                    RPTLEDGERACDETAILS.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                    If SHOWNARRATION = True Then RPTLEDGERACDETAILS.DataDefinition.FormulaFields("SHOWREMARKS").Text = 1
                    RPTLEDGERACDETAILS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS

                ElseIf frmstring = "LedgerBookConfirm" Then

                    rptlbc.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    rptlbc.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    rptlbc.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = rptlbc
                    rptlbc.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                    rptlbc.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                    If SHOWNARRATION = True Then rptlbc.DataDefinition.FormulaFields("SHOWREMARKS").Text = 1

                ElseIf frmstring = "LedgerBookConfirmSumm" Then

                    RPTLBCSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    RPTLBCSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                    RPTLBCSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = RPTLBCSUMM
                    RPTLBCSUMM.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                    RPTLBCSUMM.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS

                ElseIf frmstring = "LedgerPartySumm" Then

                    RPTLEDGERACSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                    RPTLEDGERACSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(FROMDATE.Date, "MM/dd/yyyy") & "'"
                    RPTLEDGERACSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(TODATE.Date, "MM/dd/yyyy") & "'"

                    CRPO.SelectionFormula = strsearch
                    CRPO.ReportSource = RPTLEDGERACSUMM

                End If

            ElseIf frmstring = "SaleRegisterMonthly" Or frmstring = "PurchaseRegisterMonthly" Then
                If frmstring = "SaleRegisterMonthly" Then
                    crParameterFieldDefinitions = rptsrm.DataDefinition.ParameterFields
                ElseIf frmstring = "PurchaseRegisterMonthly" Then
                    crParameterFieldDefinitions = rptprm.DataDefinition.ParameterFields
                ElseIf frmstring = "ContraRegisterMonthly" Then
                    crParameterFieldDefinitions = rptconrm.DataDefinition.ParameterFields
                ElseIf frmstring = "JvRegisterMonthly" Then
                    crParameterFieldDefinitions = rptjvrm.DataDefinition.ParameterFields
                End If

                crParameterDiscreteValue.Value = reg
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@REGISTERNAME")
                crParameterValues = crParameterFieldDefinition.CurrentValues


                crParameterValues.Clear()
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)


                crParameterDiscreteValue.Value = CmpId
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CMPID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = Locationid
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@LOCATIONID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = YearId
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@YEARID")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = CHECK
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CHECK")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = FROMDATE.Date
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@FROMDATE")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                crParameterDiscreteValue.Value = TODATE.Date
                crParameterFieldDefinition = crParameterFieldDefinitions.Item("@TODATE")
                crParameterValues = crParameterFieldDefinition.CurrentValues
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)
                If frmstring = "SaleRegisterMonthly" Then
                    CRPO.ReportSource = rptsrm
                ElseIf frmstring = "PurchaseRegisterMonthly" Then
                    CRPO.ReportSource = rptprm
                ElseIf frmstring = "ContraRegisterMonthly" Then
                    CRPO.ReportSource = rptconrm
                ElseIf frmstring = "JvRegisterMonthly" Then
                    CRPO.ReportSource = rptjvrm
                End If

            End If
            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch Exp As LoadSaveReportException
            MsgBox("Incorrect path for loading report.", _
                    MsgBoxStyle.Critical, "Load Report Error")
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()
        Dim tempattachment As String = ""

        If frmstring = "CashRegister" Then
            tempattachment = "CASHREGISTER"
        ElseIf frmstring = "BankRegister" Then
            tempattachment = "BANKREGISTER"
        ElseIf frmstring = "BankBookDetails" Then
            tempattachment = "BANKREGISTER"
        ElseIf frmstring = "DayBook" Or frmstring = "DayBookDetails" Then
            tempattachment = "DAYBOOK"
        ElseIf frmstring = "PurchaseRegister" Or frmstring = "PurchaseRegisterDetails" Or frmstring = "PurchaseRegisterMonthly" Then
            tempattachment = "PURCHASEREGISTER"
        ElseIf frmstring = "JvRegister" Or frmstring = "JvRegisterDetails" Then
            tempattachment = "JOURNALREGISTER"
        ElseIf frmstring = "ContraRegister" Or frmstring = "ContraRegisterDetails" Then
            tempattachment = "CONTRAREGISTER"
        ElseIf frmstring = "SaleRegister" Or frmstring = "SaleRegisterDetails" Or frmstring = "SaleRegisterMonthly" Then
            tempattachment = "SALEREGISTER"
        ElseIf frmstring = "PARTYSTATEMENT" Or frmstring = "PARTYSTATEMENTDTLS" Then
            tempattachment = "PARTYSTATEMENT"
        ElseIf frmstring = "LedgerBook" Or frmstring = "LedgerBookDetails" Or frmstring = "LedgerBookConfirm" Or frmstring = "LedgerPartySumm" Then
            tempattachment = "LEDGERBOOK"
        ElseIf frmstring = "purchasetax" Or frmstring = "purchasetaxdetail" Then
            tempattachment = "PURCHASETAXREGISTER"
        ElseIf frmstring = "saletax" Or frmstring = "saletaxdetail" Then
            tempattachment = "SALETAXREGISTER"
        End If

        Try
            Dim objmail As New SendMail
            objmail.attachment = Application.StartupPath & "\" & tempattachment & ".PDF"
            'objmail.attachment = Application.StartupPath & "\Register Details.PDF"
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
            'oDfDopt.DiskFileName = Application.StartupPath & "\Register Details.pdf"

            If frmstring = "CashRegister" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\CASHREGISTER.PDF"
                expo = rptcr.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptcr.Export()

            ElseIf frmstring = "BankRegister" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\BANKREGISTER.PDF"
                expo = rptBr.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptBr.Export()

            ElseIf frmstring = "BankBookDetails" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\BANKREGISTER.PDF"
                expo = rptBrd.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptBrd.Export()

            ElseIf frmstring = "DayBook" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\DAYBOOK.PDF"
                expo = rptdb.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptdb.Export()

            ElseIf frmstring = "DayBookDetails" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\DAYBOOK.PDF"
                expo = rptdbd.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptdbd.Export()

            ElseIf frmstring = "PurchaseRegister" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PURCHASEREGISTER.PDF"
                expo = rptpr.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptpr.Export()

            ElseIf frmstring = "PurchaseRegisterDetails" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PURCHASEREGISTER.PDF"
                expo = rptprd.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptprd.Export()

            ElseIf frmstring = "PurchaseRegisterMonthly" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PURCHASEREGISTER.PDF"
                expo = rptprm.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptprm.Export()

            ElseIf frmstring = "JvRegister" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\JOURNALREGISTER.PDF"
                expo = rptjvr.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptjvr.Export()

            ElseIf frmstring = "JvRegisterDetails" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\JOURNALREGISTER.PDF"
                expo = rptjvrd.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptjvrd.Export()

            ElseIf frmstring = "ContraRegister" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\CONTRAREGISTER.PDF"
                expo = rptconr.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptconr.Export()

            ElseIf frmstring = "ContraRegisterDetails" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\CONTRAREGISTER.PDF"
                expo = rptconrd.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptconrd.Export()

            ElseIf frmstring = "SaleRegister" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SALEREGISTER.PDF"
                expo = rptsr.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptsr.Export()

            ElseIf frmstring = "SaleRegisterDetails" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SALEREGISTER.PDF"
                expo = rptsrd.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptsrd.Export()

            ElseIf frmstring = "SaleRegisterMonthly" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\SALEREGISTER.PDF"
                expo = rptsrm.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptsrm.Export()

            ElseIf frmstring = "LedgerBookDetails" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\LEDGERBOOK.PDF"
                expo = RPTLEDGERACDETAILS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLEDGERACDETAILS.Export()

            ElseIf frmstring = "LedgerBook" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\LEDGERBOOK.PDF"
                expo = RPTLEDGERAC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLEDGERAC.Export()

            ElseIf frmstring = "LedgerPartySumm" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\LEDGERBOOK.PDF"
                expo = RPTLEDGERACSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLEDGERACSUMM.Export()

            ElseIf frmstring = "PARTYSTATEMENT" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PARTYSTATEMENT.PDF"
                expo = RPTSTATEMENT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSTATEMENT.Export()

            ElseIf frmstring = "PARTYSTATEMENTDTLS" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PARTYSTATEMENT.PDF"
                expo = RPTSTATEMENTDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSTATEMENTDTLS.Export()

            ElseIf frmstring = "LedgerBookConfirm" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\LEDGERBOOK.PDF"
                expo = rptlbc.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                rptlbc.Export()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TOOLLEDGER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    frmstring = "LedgerBookConfirm"
        '    Dim crParameterFieldDefinitions As ParameterFieldDefinitions
        '    Dim crParameterFieldDefinition As ParameterFieldDefinition
        '    Dim crParameterValues As New ParameterValues
        '    Dim crParameterDiscreteValue As New ParameterDiscreteValue

        '    '**************** SET SERVER ************************
        '    Dim crtableLogonInfo As New TableLogOnInfo
        '    Dim crConnecttionInfo As New ConnectionInfo
        '    Dim crTables As Tables
        '    Dim crTable As Table

        '    getFromToDate()
        '    With crConnecttionInfo
        '        .ServerName = SERVERNAME
        '        .DatabaseName = DatabaseName
        '        .UserID = DBUSERNAME
        '        .Password = Dbpassword
        '        .IntegratedSecurity = Dbsecurity
        '    End With
        '    If frmstring = "LedgerBookConfirm" Then
        '        crTables = rptlbc.Database.Tables
        '    End If

        '    For Each crTable In crTables
        '        crtableLogonInfo = crTable.LogOnInfo
        '        crtableLogonInfo.ConnectionInfo = crConnecttionInfo
        '        crTable.ApplyLogOnInfo(crtableLogonInfo)
        '    Next
        '    '************************ END *******************

        '    If frmstring = "LedgerBookConfirm" Then
        '        strsearch = ""
        '        strsearch = strsearch & " {REPORT_SP_LEDGERBOOK_DETAILS.name}='" & PARTYNAME & "'"
        '        strsearch = strsearch & "  and {REPORT_SP_LEDGERBOOK_DETAILS.CMPID}=" & CmpId & " and {REPORT_SP_LEDGERBOOK_DETAILS.LOCATIONID}=" & Locationid & " and {REPORT_SP_LEDGERBOOK_DETAILS.YEARID}=" & YearId
        '        CRPO.SelectionFormula = strsearch
        '        CRPO.ReportSource = rptlbc
        '    End If

        '    CRPO.Zoom(100)
        '    CRPO.Refresh()

        'Catch Exp As LoadSaveReportException
        '    MsgBox("Incorrect path for loading report.", _
        '            MsgBoxStyle.Critical, "Load Report Error")
        'End Try
    End Sub

    Private Sub registerdesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class