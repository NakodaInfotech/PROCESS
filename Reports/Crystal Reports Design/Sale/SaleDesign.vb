
'Imports BL
Imports System.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO
Imports BL

Public Class SaleDesign

    Dim RPTDTLS As New InvoiceDetailsReport
    Dim RPTPARTYDTLS As New InvoicePartyWiseDetails
    Dim RPTPARTYSUMM As New InvoicePartyWiseSummary
    Dim RPTAGENTDTLS As New InvoiceAgentWiseDetails
    Dim RPTAGENTSUMM As New InvoiceAgentWiseSummary
    Dim RPTQUALITYDTLS As New InvoiceQualityWiseDetails
    Dim RPTQUALITYSUMM As New InvoiceQualityWiseSummary
    Dim RPTMONTHLY As New InvoiceMonthly
    Dim RPTAVGMONTHLY As New InvoiceAvgMonthWise
    Dim RPTAVGQUALITY As New InvoiceAvgQualityWiseSummary
    Dim RPTREGISTERDTLS As New SaleRegisterWiseDetails
    Dim RPTGSTREGISTERDTLS As New SaleGSTRegisterWiseDetails
    Dim RPTGSTREGISTERSUMM As New SaleGSTRegisterWiseSummary

    Dim RPTINVOICE_COMM As New InvoiceReport_COMMON
    Dim RPTINVOICE_TOTALGST As New InvoiceReport_TOTALGST
    Dim RPTINVOICE_YARN As New InvoiceReport_Yarn
    Dim RPTINVOICEYARN_TOTALGST As New InvoiceReport_YarnTOTALGST

    Public WHERECLAUSE As String
    Public PERIOD As String
    Public strsumm As String
    Public FRMSTRING As String
    Public registername As String
    Public FROMDATE As Date
    Public TODATE As Date
    Public strsearch As String
    Public INVNO As Integer
    Public INVTYPE As String
    Public INVSCREENTYPE As String = "LINE GST"
    Public COMM As Double
    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String
    Dim tempattachment As String

    Public NEWPAGE As Boolean
    Public ADDRESS As Integer
    Public SHOWHEADER As Boolean
    Public SHOWPRINTDATE As Boolean

    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PARTYNAME As String
    Public AGENTNAME As String
    Public BILLNO As Integer
    Public REGNAME As String
    Public DIRECTPRINT As Boolean = False
    Public NOOFCOPIES As Integer = 1
    Public PRINTSETTING As Object = Nothing
    Public INVOICECOPYNAME As String
    Public INVOICETRANS As Boolean
    Public INVOICERETAIL As Boolean
    Public IGSTFORMAT As Boolean = False
    Public BLANKPAPER As Boolean = False


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

    Private Sub saledesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub saledesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

            If FRMSTRING = "INVOICE" Then
                If INVTYPE = "GREY" Then
                    If INVSCREENTYPE = "LINE GST" Then crTables = RPTINVOICE_COMM.Database.Tables Else crTables = RPTINVOICE_TOTALGST.Database.Tables
                Else
                    If INVSCREENTYPE = "LINE GST" Then crTables = RPTINVOICE_YARN.Database.Tables Else crTables = RPTINVOICEYARN_TOTALGST.Database.Tables
                End If
            End If


            If FRMSTRING = "SALEDTLS" Then crTables = RPTDTLS.Database.Tables

            If FRMSTRING = "PARTYWISEDTLS" Then crTables = RPTPARTYDTLS.Database.Tables
            If FRMSTRING = "PARTYWISESUMM" Then crTables = RPTPARTYSUMM.Database.Tables


            If FRMSTRING = "JOBBERWISEDTLS" Then crTables = RPTAGENTDTLS.Database.Tables
            If FRMSTRING = "JOBBERWISESUMM" Then crTables = RPTAGENTSUMM.Database.Tables

            If FRMSTRING = "QUALITYWISEDTLS" Then crTables = RPTQUALITYDTLS.Database.Tables
            If FRMSTRING = "QUALITYWISESUMM" Then crTables = RPTQUALITYSUMM.Database.Tables

            If FRMSTRING = "MONTHLY" Then crTables = RPTMONTHLY.Database.Tables

            If FRMSTRING = "AVGSALEQUALITYWISESUMM" Then crTables = RPTAVGQUALITY.Database.Tables
            If FRMSTRING = "AVGSALEMONTHLY" Then crTables = RPTAVGMONTHLY.Database.Tables

            If FRMSTRING = "REGISTERDTLS" Then crTables = RPTREGISTERDTLS.Database.Tables
            If FRMSTRING = "GSTREGISTERDTLS" Then crTables = RPTGSTREGISTERDTLS.Database.Tables
            If FRMSTRING = "GSTREGISTERSUMM" Then crTables = RPTGSTREGISTERSUMM.Database.Tables


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next


            '************************ END *******************
            getFromToDate()


            CRPO.SelectionFormula = WHERECLAUSE


            If FRMSTRING = "INVOICE" Then
                If INVTYPE = "GREY" Then
                    If INVSCREENTYPE = "LINE GST" Then
                        CRPO.ReportSource = RPTINVOICE_COMM
                    Else
                        CRPO.ReportSource = RPTINVOICE_TOTALGST
                        If ClientName = "NIRMALA" Or ClientName = "YESHA" Then RPTINVOICE_TOTALGST.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                    End If
                Else
                    If INVSCREENTYPE = "LINE GST" Then
                        CRPO.ReportSource = RPTINVOICE_YARN
                        If ClientName = "NIRMALA" Or ClientName = "YESHA" Then RPTINVOICE_YARN.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                    Else
                        CRPO.ReportSource = RPTINVOICEYARN_TOTALGST
                        If ClientName = "NIRMALA" Or ClientName = "YESHA" Then RPTINVOICEYARN_TOTALGST.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                    End If
                End If

            ElseIf FRMSTRING = "SALEDTLS" Then
                CRPO.ReportSource = RPTDTLS
                RPTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "PARTYWISEDTLS" Then
                CRPO.ReportSource = RPTPARTYDTLS
                RPTPARTYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTPARTYDTLS.GroupFooterSection2.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "PARTYWISESUMM" Then
                CRPO.ReportSource = RPTPARTYSUMM
                RPTPARTYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "JOBBERWISEDTLS" Then
                CRPO.ReportSource = RPTAGENTDTLS
                RPTAGENTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTAGENTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTAGENTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTAGENTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTAGENTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTAGENTDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTAGENTDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "JOBBERWISESUMM" Then
                CRPO.ReportSource = RPTAGENTSUMM
                RPTAGENTSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTAGENTSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTAGENTSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTAGENTSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTAGENTSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                CRPO.ReportSource = RPTQUALITYDTLS
                RPTQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTQUALITYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTQUALITYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTQUALITYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTQUALITYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTQUALITYDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTQUALITYDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                CRPO.ReportSource = RPTQUALITYSUMM
                RPTQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTQUALITYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTQUALITYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTQUALITYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTQUALITYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "MONTHLY" Then
                CRPO.ReportSource = RPTMONTHLY
                RPTMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTMONTHLY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTMONTHLY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTMONTHLY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTMONTHLY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "REGISTERDTLS" Then
                CRPO.ReportSource = RPTREGISTERDTLS
                RPTREGISTERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTREGISTERDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTREGISTERDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTREGISTERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTREGISTERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "GSTREGISTERDTLS" Then
                CRPO.ReportSource = RPTGSTREGISTERDTLS
                RPTGSTREGISTERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGSTREGISTERDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGSTREGISTERDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGSTREGISTERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGSTREGISTERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "GSTREGISTERSUMM" Then
                CRPO.ReportSource = RPTGSTREGISTERSUMM
                RPTGSTREGISTERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGSTREGISTERSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGSTREGISTERSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGSTREGISTERSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGSTREGISTERSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "AVGSALEQUALITYWISESUMM" Then
                CRPO.ReportSource = RPTAVGQUALITY
                RPTAVGQUALITY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTAVGQUALITY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTAVGQUALITY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTAVGQUALITY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTAVGQUALITY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "AVGSALEMONTHLY" Then
                CRPO.ReportSource = RPTAVGMONTHLY
                RPTAVGMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTAVGMONTHLY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTAVGMONTHLY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTAVGMONTHLY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTAVGMONTHLY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

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

            If FRMSTRING = "INVOICE" Then
                tempattachment = "INVOICE"

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
            If FRMSTRING = "INVOICE" Then
                strsearch = "{INVOICEMASTER.INVOICE_NO} = " & INVNO & " AND {INVOICEMASTER.INVOICE_YEARID} = " & YearId
            End If


            Dim OBJ As New Object
            If FRMSTRING = "INVOICE" Then
                OBJ = New InvoiceReport_TOTALGST
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
                If FRMSTRING = "INVOICE" Then
                    OBJ.RecordSelectionFormula = strsearch
                    oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "INVOICE_" & INVNO & ".pdf"
                End If


                'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                'If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                expo = OBJ.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJ.Export()
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Try
            Dim objmail As New SendMail
            If FRMSTRING = "INVOICE" Then
                tempattachment = "INVOICE"
            ElseIf FRMSTRING = "SUPPLIERWISEDTLS" Then
                tempattachment = "SALEDETAILS"
            ElseIf FRMSTRING = "SUPPLIERCOMMWISEDTLS" Then
                tempattachment = "COMMISSIONDEBITNOTE"
            Else
                tempattachment = "SALESUMMARY"
            End If

            Dim emailid As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Transfer()

            objmail.subject = tempattachment
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

            oDfDopt.DiskFileName = Application.StartupPath & "\" & tempattachment & ".pdf"
            If FRMSTRING = "INVOICE" Then
                If INVTYPE = "GREY" Then
                    If INVSCREENTYPE = "LINE GST" Then
                        expo = RPTINVOICE_COMM.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTINVOICE_COMM.Export()
                    Else
                        expo = RPTINVOICE_TOTALGST.ExportOptions
                        RPTINVOICE_TOTALGST.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTINVOICE_TOTALGST.Export()
                        RPTINVOICE_TOTALGST.DataDefinition.FormulaFields("SENDMAIL").Text = 0
                    End If
                Else
                    If INVSCREENTYPE = "LINE GST" Then
                        expo = RPTINVOICE_YARN.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTINVOICE_YARN.Export()
                    Else
                        expo = RPTINVOICEYARN_TOTALGST.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTINVOICEYARN_TOTALGST.Export()
                    End If
                End If

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
            ElseIf FRMSTRING = "JOBBERWISEDTLS" Then
                expo = RPTAGENTDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTAGENTDTLS.Export()
            ElseIf FRMSTRING = "MONTHLY" Then
                expo = RPTMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTMONTHLY.Export()
            ElseIf FRMSTRING = "JOBBERWISESUMM" Then
                expo = RPTAGENTSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTAGENTSUMM.Export()

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

            ElseIf FRMSTRING = "AVGSALEQUALITYWISESUMM" Then
                expo = RPTAVGQUALITY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTAVGQUALITY.Export()

            ElseIf FRMSTRING = "AVGSALEMONTHLY" Then
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


End Class