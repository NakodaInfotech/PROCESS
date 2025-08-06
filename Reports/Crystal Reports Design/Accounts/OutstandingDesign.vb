
Imports BL
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Windows.Forms
Imports System.IO

Public Class OutstandingDesign

    Dim RPTOUTSTANDINGALLSUMMREC As New OutstandingReport_All_Summary_Rec
    Dim RPTOUTSTANDINGALLSUMMPAY As New OutstandingReport_All_Summary_Pay
    Dim RPTOUTSTANDINGALLDTLS As New OutstandingReport_All_Details

    Dim RPTOUTSTANDINGPAYSUMM As New OutstandingReport_Summary_Pay
    Dim RPTOUTSTANDINGRECSUMM As New OutstandingReport_Summary_Rec
    Dim RPTOUTSTANDINGPAYDTLS As New OutstandingReport_Details_Pay
    Dim RPTOUTSTANDINGRECDTLS As New OutstandingReport_Details_Rec

    Dim RPTONLYPAYMENTDTLS As New PaidBills_All_Details

    Dim RPTBROKEROUTSTANDINGPAYSUMM As New OutstandingReport_Broker_Summary_Pay
    Dim RPTBROKEROUTSTANDINGRECSUMM As New OutstandingReport_Broker_Summary_Rec
    Dim RPTBROKEROUTSTANDINGPAYDTLS As New OutstandingReport_Broker_Details_Pay
    Dim RPTBROKEROUTSTANDINGRECDTLS As New OutstandingReport_Broker_Details_Rec

    Dim RPTINTOUTSTANDING As New OutstandingReport_Interest_Details

    Dim RPTRECOUTSTANDING As New OutstandingReport_Inventory_Rec
    Dim RPTPAYOUTSTANDING As New OutstandingReport_Inventory_Pay

    Dim RPTALLOUTSTANDINGREC As New OutstandingReport_AllBills_Summary_Rec
    Dim RPTALLOUTSTANDINGPAY As New OutstandingReport_AllBills_Summary_Pay

    Dim RPTONLYOUTSTANDINGREC As New OutstandingReport_AllBills_Summary_Rec
    Dim RPTONLYOUTSTANDINGPAY As New OutstandingReport_AllBills_Summary_Pay

    Dim RPTREMINDERLETTER As New OutstandingReport_Letter


    'NEWLY ADDED
    Public REPORTNAME As String
    Public DAYS As String
    Public ADDRESS As Integer
    Public NEWPAGE As Boolean
    Public FRMSTRING As String
    Public selfor_ss As String
    Public PERIOD As String
    Public INTEREST As Double
    Public INTDAYS As Integer
    Public DUEDAYS As Integer
    Public SHOWPRINTDATE As Integer

    'FOR WHATSAPP
    Public FORMULA As String
    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1
    Dim tempattachment As String
    Public BILLNO As Integer
    Public PARTYNAME As String
    Public AGENTNAME As String

    Private Sub OutstandingDesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

            If FRMSTRING = "OUTSTANDINGALLSUMMREC" Then crTables = RPTOUTSTANDINGALLSUMMREC.Database.Tables
            If FRMSTRING = "OUTSTANDINGALLSUMMPAY" Then crTables = RPTOUTSTANDINGALLSUMMPAY.Database.Tables
            If FRMSTRING = "OUTSTANDINGALLDTLS" Then crTables = RPTOUTSTANDINGALLDTLS.Database.Tables

            If FRMSTRING = "OUTSTANDINGPAYSUMM" Then crTables = RPTOUTSTANDINGPAYSUMM.Database.Tables
            If FRMSTRING = "OUTSTANDINGRECSUMM" Then crTables = RPTOUTSTANDINGRECSUMM.Database.Tables
            If FRMSTRING = "OUTSTANDINGPAYDTLS" Then crTables = RPTOUTSTANDINGPAYDTLS.Database.Tables
            If FRMSTRING = "OUTSTANDINGRECDTLS" Then crTables = RPTOUTSTANDINGRECDTLS.Database.Tables

            If FRMSTRING = "ONLYPAYMENTDTLS" Then crTables = RPTONLYPAYMENTDTLS.Database.Tables

            If FRMSTRING = "BROKEROUTSTANDINGPAYSUMM" Then crTables = RPTBROKEROUTSTANDINGPAYSUMM.Database.Tables
            If FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Then crTables = RPTBROKEROUTSTANDINGRECSUMM.Database.Tables
            If FRMSTRING = "BROKEROUTSTANDINGPAYDTLS" Then crTables = RPTBROKEROUTSTANDINGPAYDTLS.Database.Tables
            If FRMSTRING = "BROKEROUTSTANDINGRECDTLS" Then crTables = RPTBROKEROUTSTANDINGRECDTLS.Database.Tables

            If FRMSTRING = "INTOUTSTANDING" Then crTables = RPTINTOUTSTANDING.Database.Tables

            If FRMSTRING = "RECINVENTORYOUTSTANDING" Then crTables = RPTRECOUTSTANDING.Database.Tables
            If FRMSTRING = "PAYINVENTORYOUTSTANDING" Then crTables = RPTPAYOUTSTANDING.Database.Tables

            If FRMSTRING = "ALLBILLOUTSTANDINGREC" Then crTables = RPTALLOUTSTANDINGREC.Database.Tables
            If FRMSTRING = "ALLBILLOUTSTANDINGPAY" Then crTables = RPTALLOUTSTANDINGPAY.Database.Tables

            If FRMSTRING = "ONLYBILLOUTSTANDINGREC" Then crTables = RPTONLYOUTSTANDINGREC.Database.Tables
            If FRMSTRING = "ONLYBILLOUTSTANDINGPAY" Then crTables = RPTONLYOUTSTANDINGPAY.Database.Tables

            If FRMSTRING = "REMINDERLETTER" Then crTables = RPTREMINDERLETTER.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            If FRMSTRING = "OUTSTANDINGALLSUMMREC" Then

                CRPO.ReportSource = RPTOUTSTANDINGALLSUMMREC
                RPTOUTSTANDINGALLSUMMREC.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGALLSUMMREC.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGALLSUMMREC.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                crTables = RPTOUTSTANDINGALLSUMMREC.Database.Tables

            ElseIf FRMSTRING = "OUTSTANDINGALLSUMMPAY" Then

                CRPO.ReportSource = RPTOUTSTANDINGALLSUMMPAY
                RPTOUTSTANDINGALLSUMMPAY.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGALLSUMMPAY.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGALLSUMMPAY.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                crTables = RPTOUTSTANDINGALLSUMMPAY.Database.Tables

            ElseIf FRMSTRING = "OUTSTANDINGALLDTLS" Then

                CRPO.ReportSource = RPTOUTSTANDINGALLDTLS
                RPTOUTSTANDINGALLDTLS.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGALLDTLS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGALLDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                crTables = RPTOUTSTANDINGALLDTLS.Database.Tables

            ElseIf FRMSTRING = "OUTSTANDINGPAYSUMM" Then

                CRPO.ReportSource = RPTOUTSTANDINGPAYSUMM
                RPTOUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGPAYSUMM.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTOUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                crTables = RPTOUTSTANDINGPAYSUMM.Database.Tables

            ElseIf FRMSTRING = "OUTSTANDINGRECSUMM" Then

                CRPO.ReportSource = RPTOUTSTANDINGRECSUMM
                RPTOUTSTANDINGRECSUMM.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGRECSUMM.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGRECSUMM.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTOUTSTANDINGRECSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTOUTSTANDINGRECSUMM.DataDefinition.FormulaFields("ENTEREDDUEDAYS").Text = DUEDAYS
                crTables = RPTOUTSTANDINGRECSUMM.Database.Tables

            ElseIf FRMSTRING = "OUTSTANDINGPAYDTLS" Then

                CRPO.ReportSource = RPTOUTSTANDINGPAYDTLS
                RPTOUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGPAYDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTOUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                crTables = RPTOUTSTANDINGPAYDTLS.Database.Tables

            ElseIf FRMSTRING = "OUTSTANDINGRECDTLS" Then

                CRPO.ReportSource = RPTOUTSTANDINGRECDTLS
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGRECDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("ENTEREDDUEDAYS").Text = DUEDAYS
                crTables = RPTOUTSTANDINGRECDTLS.Database.Tables

            ElseIf FRMSTRING = "ONLYPAYMENTDTLS" Then

                CRPO.ReportSource = RPTONLYPAYMENTDTLS
                RPTONLYPAYMENTDTLS.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTONLYPAYMENTDTLS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTONLYPAYMENTDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTONLYPAYMENTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                RPTONLYPAYMENTDTLS.DataDefinition.FormulaFields("ENTEREDDUEDAYS").Text = DUEDAYS
                RPTONLYPAYMENTDTLS.DataDefinition.FormulaFields("INTEREST").Text = INTEREST
                RPTONLYPAYMENTDTLS.DataDefinition.FormulaFields("INTDAYS").Text = INTDAYS
                crTables = RPTONLYPAYMENTDTLS.Database.Tables

            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYSUMM" Then

                CRPO.ReportSource = RPTBROKEROUTSTANDINGPAYSUMM
                RPTBROKEROUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTBROKEROUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTBROKEROUTSTANDINGPAYSUMM.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTBROKEROUTSTANDINGPAYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                crTables = RPTBROKEROUTSTANDINGPAYSUMM.Database.Tables

            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Then

                CRPO.ReportSource = RPTBROKEROUTSTANDINGRECSUMM
                RPTBROKEROUTSTANDINGRECSUMM.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTBROKEROUTSTANDINGRECSUMM.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTBROKEROUTSTANDINGRECSUMM.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTBROKEROUTSTANDINGRECSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                crTables = RPTBROKEROUTSTANDINGRECSUMM.Database.Tables

            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYDTLS" Then

                CRPO.ReportSource = RPTBROKEROUTSTANDINGPAYDTLS
                RPTBROKEROUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTBROKEROUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTBROKEROUTSTANDINGPAYDTLS.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTBROKEROUTSTANDINGPAYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                crTables = RPTBROKEROUTSTANDINGPAYDTLS.Database.Tables

            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECDTLS" Then

                CRPO.ReportSource = RPTBROKEROUTSTANDINGRECDTLS
                RPTBROKEROUTSTANDINGRECDTLS.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTBROKEROUTSTANDINGRECDTLS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTBROKEROUTSTANDINGRECDTLS.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTBROKEROUTSTANDINGRECDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                crTables = RPTBROKEROUTSTANDINGRECDTLS.Database.Tables

            ElseIf FRMSTRING = "INTOUTSTANDING" Then

                CRPO.ReportSource = RPTINTOUTSTANDING
                RPTINTOUTSTANDING.DataDefinition.FormulaFields("INTDAYS").Text = INTDAYS
                RPTINTOUTSTANDING.DataDefinition.FormulaFields("INTEREST").Text = INTEREST
                RPTINTOUTSTANDING.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTINTOUTSTANDING.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTINTOUTSTANDING.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                crTables = RPTINTOUTSTANDING.Database.Tables

            ElseIf FRMSTRING = "RECINVENTORYOUTSTANDING" Then

                CRPO.ReportSource = RPTRECOUTSTANDING
                RPTRECOUTSTANDING.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTRECOUTSTANDING.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTRECOUTSTANDING.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                crTables = RPTRECOUTSTANDING.Database.Tables

            ElseIf FRMSTRING = "PAYINVENTORYOUTSTANDING" Then

                CRPO.ReportSource = RPTPAYOUTSTANDING
                RPTPAYOUTSTANDING.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTPAYOUTSTANDING.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTPAYOUTSTANDING.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                crTables = RPTPAYOUTSTANDING.Database.Tables

            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGREC" Then

                CRPO.ReportSource = RPTALLOUTSTANDINGREC
                RPTALLOUTSTANDINGREC.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                RPTALLOUTSTANDINGREC.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTALLOUTSTANDINGREC.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                crTables = RPTALLOUTSTANDINGREC.Database.Tables

            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGPAY" Then

                CRPO.ReportSource = RPTALLOUTSTANDINGPAY
                RPTALLOUTSTANDINGPAY.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                RPTALLOUTSTANDINGPAY.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTALLOUTSTANDINGPAY.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                crTables = RPTALLOUTSTANDINGPAY.Database.Tables


            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGREC" Then

                CRPO.ReportSource = RPTONLYOUTSTANDINGREC
                RPTONLYOUTSTANDINGREC.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                RPTONLYOUTSTANDINGREC.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTONLYOUTSTANDINGREC.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTONLYOUTSTANDINGREC.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                crTables = RPTONLYOUTSTANDINGREC.Database.Tables

            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGPAY" Then

                CRPO.ReportSource = RPTONLYOUTSTANDINGPAY
                RPTONLYOUTSTANDINGPAY.DataDefinition.FormulaFields("REPORTNAME").Text = "'" & REPORTNAME & "'"
                RPTONLYOUTSTANDINGPAY.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTONLYOUTSTANDINGPAY.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTONLYOUTSTANDINGPAY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                crTables = RPTONLYOUTSTANDINGPAY.Database.Tables

            ElseIf FRMSTRING = "REMINDERLETTER" Then

                CRPO.ReportSource = RPTREMINDERLETTER
                crTables = RPTREMINDERLETTER.Database.Tables
            End If

            CRPO.SelectionFormula = selfor_ss

            If FRMSTRING = "OUTSTANDINGALLSUMMREC" Then
                CRPO.ReportSource = RPTOUTSTANDINGALLSUMMREC
            ElseIf FRMSTRING = "OUTSTANDINGALLSUMMPAY" Then
                CRPO.ReportSource = RPTOUTSTANDINGALLSUMMPAY
            ElseIf FRMSTRING = "OUTSTANDINGALLDTLS" Then
                CRPO.ReportSource = RPTOUTSTANDINGALLDTLS
            ElseIf FRMSTRING = "OUTSTANDINGPAYSUMM" Then
                CRPO.ReportSource = RPTOUTSTANDINGPAYSUMM
            ElseIf FRMSTRING = "OUTSTANDINGRECSUMM" Then
                CRPO.ReportSource = RPTOUTSTANDINGRECSUMM
            ElseIf FRMSTRING = "OUTSTANDINGPAYDTLS" Then
                CRPO.ReportSource = RPTOUTSTANDINGPAYDTLS
            ElseIf FRMSTRING = "OUTSTANDINGRECDTLS" Then
                CRPO.ReportSource = RPTOUTSTANDINGRECDTLS
            ElseIf FRMSTRING = "ONLYPAYMENTDTLS" Then
                CRPO.ReportSource = RPTONLYPAYMENTDTLS
            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYSUMM" Then
                CRPO.ReportSource = RPTBROKEROUTSTANDINGPAYSUMM
            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Then
                CRPO.ReportSource = RPTBROKEROUTSTANDINGRECSUMM
            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYDTLS" Then
                CRPO.ReportSource = RPTBROKEROUTSTANDINGPAYDTLS
            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECDTLS" Then
                CRPO.ReportSource = RPTBROKEROUTSTANDINGRECDTLS
            ElseIf FRMSTRING = "INTOUTSTANDING" Then
                CRPO.ReportSource = RPTINTOUTSTANDING
            ElseIf FRMSTRING = "RECINVENTORYOUTSTANDING" Then
                CRPO.ReportSource = RPTRECOUTSTANDING
            ElseIf FRMSTRING = "PAYINVENTORYOUTSTANDING" Then
                CRPO.ReportSource = RPTPAYOUTSTANDING
            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGREC" Then
                CRPO.ReportSource = RPTALLOUTSTANDINGREC
            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGPAY" Then
                CRPO.ReportSource = RPTALLOUTSTANDINGPAY
            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGREC" Then
                CRPO.ReportSource = RPTONLYOUTSTANDINGREC
            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGPAY" Then
                CRPO.ReportSource = RPTONLYOUTSTANDINGPAY
            ElseIf FRMSTRING = "REMINDERLETTER" Then
                CRPO.ReportSource = RPTREMINDERLETTER
            End If
            '************************ END *******************

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Try
            Dim emailid As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Transfer()
            Dim objmail As New SendMail
            objmail.attachment = Application.StartupPath & "\Outstanding Report.PDF"
            If emailid <> "" Then objmail.cmbfirstadd.Text = emailid
            objmail.Show()
            objmail.BringToFront()
            Windows.Forms.Cursor.Current = Cursors.Arrow
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\Outstanding Report.pdf"

            If FRMSTRING = "OUTSTANDINGALLSUMMREC" Then
                expo = RPTOUTSTANDINGALLSUMMREC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGALLSUMMREC.Export()
            ElseIf FRMSTRING = "OUTSTANDINGALLSUMMPAY" Then
                expo = RPTOUTSTANDINGALLSUMMPAY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGALLSUMMPAY.Export()
            ElseIf FRMSTRING = "OUTSTANDINGALLDTLS" Then
                expo = RPTOUTSTANDINGALLDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGALLDTLS.Export()
            ElseIf FRMSTRING = "OUTSTANDINGPAYSUMM" Then
                expo = RPTOUTSTANDINGPAYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGPAYSUMM.Export()
            ElseIf FRMSTRING = "OUTSTANDINGRECSUMM" Then
                expo = RPTOUTSTANDINGRECSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGRECSUMM.Export()
            ElseIf FRMSTRING = "OUTSTANDINGPAYDTLS" Then
                expo = RPTOUTSTANDINGPAYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGPAYDTLS.Export()
            ElseIf FRMSTRING = "OUTSTANDINGRECDTLS" Then
                expo = RPTOUTSTANDINGRECDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGRECDTLS.Export()
            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYSUMM" Then
                expo = RPTBROKEROUTSTANDINGPAYSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBROKEROUTSTANDINGPAYSUMM.Export()
            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Then
                expo = RPTBROKEROUTSTANDINGRECSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBROKEROUTSTANDINGRECSUMM.Export()
            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYDTLS" Then
                expo = RPTBROKEROUTSTANDINGPAYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBROKEROUTSTANDINGPAYDTLS.Export()
            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECDTLS" Then
                expo = RPTBROKEROUTSTANDINGRECDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBROKEROUTSTANDINGRECDTLS.Export()
            ElseIf FRMSTRING = "INTOUTSTANDING" Then
                expo = RPTINTOUTSTANDING.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINTOUTSTANDING.Export()
            ElseIf FRMSTRING = "RECINVENTORYOUTSTANDING" Then
                expo = RPTRECOUTSTANDING.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTRECOUTSTANDING.Export()
            ElseIf FRMSTRING = "PAYINVENTORYOUTSTANDING" Then
                expo = RPTPAYOUTSTANDING.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPAYOUTSTANDING.Export()
            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGREC" Then
                expo = RPTALLOUTSTANDINGREC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTALLOUTSTANDINGREC.Export()
            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGPAY" Then
                expo = RPTALLOUTSTANDINGPAY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTALLOUTSTANDINGPAY.Export()
            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGREC" Then
                expo = RPTONLYOUTSTANDINGREC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTONLYOUTSTANDINGREC.Export()
            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGPAY" Then
                expo = RPTONLYOUTSTANDINGPAY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTONLYOUTSTANDINGPAY.Export()
            ElseIf FRMSTRING = "REMINDERLETTER" Then
                expo = RPTREMINDERLETTER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTREMINDERLETTER.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub OutstandingDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If File.Exists(Application.StartupPath & "\" & PARTYNAME & "Outstanding Report" & ".PDF") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\" & PARTYNAME & "Outstanding Report" & ".PDF")

            Transfer()

            If FRMSTRING = "OUTSTANDINGALLSUMMREC" Then
                tempattachment = "OUTSTANDINGALLSUMMREC"

            ElseIf FRMSTRING = "OUTSTANDINGALLSUMMPAY" Then
                tempattachment = "OUTSTANDINGALLSUMMPAY"

            ElseIf FRMSTRING = "OUTSTANDINGALLDTLS" Then
                tempattachment = "OUTSTANDINGALLDTLS"

            ElseIf FRMSTRING = "OUTSTANDINGPAYSUMM" Then
                tempattachment = "OUTSTANDINGPAYSUMM"

            ElseIf FRMSTRING = "OUTSTANDINGRECSUMM" Then
                tempattachment = "OUTSTANDINGRECSUMM"

            ElseIf FRMSTRING = "OUTSTANDINGPAYDTLS" Then
                tempattachment = "OUTSTANDINGPAYDTLS"

            ElseIf FRMSTRING = "OUTSTANDINGRECDTLS" Then
                tempattachment = "OUTSTANDINGRECDTLS"

            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYSUMM" Then
                tempattachment = "BROKEROUTSTANDINGPAYSUMM"

            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECSUMM" Then
                tempattachment = "BROKEROUTSTANDINGRECSUMM"

            ElseIf FRMSTRING = "BROKEROUTSTANDINGPAYDTLS" Then
                tempattachment = "BROKEROUTSTANDINGPAYDTLS"

            ElseIf FRMSTRING = "BROKEROUTSTANDINGRECDTLS" Then
                tempattachment = "BROKEROUTSTANDINGRECDTLS"

            ElseIf FRMSTRING = "RECINVENTORYOUTSTANDING" Then
                tempattachment = "RECINVENTORYOUTSTANDING"

            ElseIf FRMSTRING = "PAYINVENTORYOUTSTANDING" Then
                tempattachment = "PAYINVENTORYOUTSTANDING"

            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGREC" Then
                tempattachment = "ALLBILLOUTSTANDINGREC"

            ElseIf FRMSTRING = "ALLBILLOUTSTANDINGPAY" Then
                tempattachment = "ALLBILLOUTSTANDINGPAY"

            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGREC" Then
                tempattachment = "ONLYBILLOUTSTANDINGREC"

            ElseIf FRMSTRING = "ONLYBILLOUTSTANDINGPAY" Then
                tempattachment = "ONLYBILLOUTSTANDINGPAY"

            ElseIf FRMSTRING = "REMINDERLETTER" Then
                tempattachment = "REMINDERLETTER"

            End If

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = PARTYNAME
            OBJWHATSAPP.AGENTNAME = AGENTNAME

            If File.Exists(Application.StartupPath & "\Outstanding Report" & ".PDF") And PARTYNAME <> "" Then My.Computer.FileSystem.RenameFile(Application.StartupPath & "\Outstanding Report" & ".PDF", "\Outstanding Report" & ".PDF")

            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\Outstanding Report" & ".PDF")
            OBJWHATSAPP.FILENAME.Add("Outstanding Report.pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class