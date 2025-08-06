
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class PurchaseReturnDesign

    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Public PERIOD As String

    Dim RPTDTLS As New PurchaseReturnDetailsReport
    Dim RPTSUMM As New PurchaseReturnSummary
    Dim RPTPARTYDTLS As New PurchaseReturnPartyWiseDetails
    Dim RPTPARTYSUMM As New PurchaseReturnPartyWiseSummary
    Dim RPTBROKERDTLS As New PurchaseReturnAgentWiseDetails
    Dim RPTBROKERSUMM As New PurchaseReturnAgentWiseSummary
    Dim RPTQUALITYDTLS As New PurchaseReturnQualityWiseDetails
    Dim RPTQUALITYSUMM As New PurchaseReturnQualityWiseSummary
    Dim RPTMILLDTLS As New PurchaseReturnMillWiseDetails
    Dim RPTMILLSUMM As New PurchaseReturnMillWiseSummary
    Dim RPTPURRETURN As New PurchaseReturnReport

    Dim RPTMONTHLY As New PurchaseReturnMonthWise
    'Dim RPTAVGMONTHLY As New PurchaseReturnAvgMonthWise
    'Dim RPTAVGQUALITY As New PurchaseReturnAvgQualityWiseSummary
    Dim RPTREGISTERDTLS As New PurchaseReturnRegisterWiseDetails

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
    Public BILLNO As Integer
    Public PARTYNAME As String
    Public AGENTNAME As String

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

            ' If FRMSTRING = "PURDTLS" Then crTables = RPTDTLS.Database.Tables
            If FRMSTRING = "PURSUMM" Then crTables = RPTSUMM.Database.Tables

            If FRMSTRING = "PARTYWISEDTLS" Then crTables = RPTPARTYDTLS.Database.Tables
            If FRMSTRING = "PARTYWISESUMM" Then crTables = RPTPARTYSUMM.Database.Tables

            If FRMSTRING = "BROKERWISEDTLS" Then crTables = RPTBROKERDTLS.Database.Tables
            If FRMSTRING = "BROKERWISESUMM" Then crTables = RPTBROKERSUMM.Database.Tables

            If FRMSTRING = "QUALITYWISEDTLS" Then crTables = RPTQUALITYDTLS.Database.Tables
            If FRMSTRING = "QUALITYWISESUMM" Then crTables = RPTQUALITYSUMM.Database.Tables

            'If FRMSTRING = "TRANSWISEDTLS" Then crTables = RPTTRANSDTLS.Database.Tables
            'If FRMSTRING = "TRANSWISESUMM" Then crTables = RPTTRANSSUMM.Database.Tables

            If FRMSTRING = "MILLWISEDTLS" Then crTables = RPTMILLDTLS.Database.Tables
            If FRMSTRING = "MILLWISESUMM" Then crTables = RPTMILLSUMM.Database.Tables
            If FRMSTRING = "PURRETURN" Then crTables = RPTPURRETURN.Database.Tables
            If FRMSTRING = "MONTHLY" Then crTables = RPTMONTHLY.Database.Tables

            'If FRMSTRING = "AVGPURQUALITYWISESUMM" Then crTables = RPTAVGQUALITY.Database.Tables
            'If FRMSTRING = "AVGPURMONTHLY" Then crTables = RPTAVGMONTHLY.Database.Tables

            If FRMSTRING = "REGISTERDTLS" Then crTables = RPTREGISTERDTLS.Database.Tables


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE


            If FRMSTRING = "PURRETURN" Then
                CRPO.ReportSource = RPTPURRETURN

            ElseIf FRMSTRING = "PURDTLS" Then
                'CRPO.ReportSource = RPTDTLS
                'RPTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                'If SHOWHEADER = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                'If SHOWPRINTDATE = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                'RPTDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                'RPTDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE


            ElseIf FRMSTRING = "PURSUMM" Then
                'CRPO.ReportSource = RPTSUMM
                'RPTSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                'If SHOWHEADER = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                'If SHOWPRINTDATE = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

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

                'ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                '    CRPO.ReportSource = RPTTRANSDTLS
                '    RPTTRANSDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                '    If SHOWHEADER = True Then RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                '    'RPTTRANSDTLS.REPORTH.SectionFormat.EnableSuppress = Not SHOWHEADER
                '    If SHOWPRINTDATE = True Then RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                '    RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                '    RPTTRANSDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

                'ElseIf FRMSTRING = "TRANSWISESUMM" Then
                '    CRPO.ReportSource = RPTTRANSSUMM
                '    RPTTRANSSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                '    If SHOWPRINTDATE = True Then RPTTRANSSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTTRANSSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                '    RPTTRANSSUMM.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS

            ElseIf FRMSTRING = "MONTHLY" Then
                CRPO.ReportSource = RPTMONTHLY
                'ElseIf FRMSTRING = "AVGPURQUALITYWISESUMM" Then
                '    CRPO.ReportSource = RPTAVGQUALITY
                '    RPTAVGQUALITY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                '    If SHOWPRINTDATE = True Then RPTAVGQUALITY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTAVGQUALITY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                'ElseIf FRMSTRING = "AVGPURMONTHLY" Then
                '    CRPO.ReportSource = RPTAVGMONTHLY
                'ElseIf FRMSTRING = "REGISTERDTLS" Then
                'CRPO.ReportSource = RPTREGISTERDTLS
                RPTREGISTERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWPRINTDATE = True Then RPTREGISTERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTREGISTERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
            End If

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Sub PurchaseReturnDesign_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            oDfDopt.DiskFileName = Application.StartupPath & "\PURCHASERETURN.pdf"

            If FRMSTRING = "PARTYWISEDTLS" Then
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
                'ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                '    expo = RPTTRANSDTLS.ExportOptions
                '    expo.ExportDestinationType = ExportDestinationType.DiskFile
                '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                '    expo.DestinationOptions = oDfDopt
                '    RPTTRANSDTLS.Export()
                'ElseIf FRMSTRING = "TRANSWISESUMM" Then
                '    expo = RPTTRANSSUMM.ExportOptions
                '    expo.ExportDestinationType = ExportDestinationType.DiskFile
                '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                '    expo.DestinationOptions = oDfDopt
                '    RPTTRANSSUMM.Export()
            ElseIf FRMSTRING = "MONTHLY" Then
                expo = RPTMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTMONTHLY.Export()
            ElseIf FRMSTRING = "PURRETURN" Then
                expo = RPTPURRETURN.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPURRETURN.Export()
                'ElseIf FRMSTRING = "AVGPURQUALITYWISESUMM" Then
                '    expo = RPTAVGQUALITY.ExportOptions
                '    expo.ExportDestinationType = ExportDestinationType.DiskFile
                '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                '    expo.DestinationOptions = oDfDopt
                '    RPTAVGQUALITY.Export()
                'ElseIf FRMSTRING = "AVGPURMONTHLY" Then
                '    expo = RPTAVGMONTHLY.ExportOptions
                '    expo.ExportDestinationType = ExportDestinationType.DiskFile
                '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                '    expo.DestinationOptions = oDfDopt
                '    RPTAVGMONTHLY.Export()
            ElseIf FRMSTRING = "REGISTERDTLS" Then
                expo = RPTREGISTERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTREGISTERDTLS.Export()
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

            Dim TEMPATTACHMENT As String = Application.StartupPath & "\PURCHASERETURN.pdf"
            Dim objmail As New SendMail
            objmail.attachment = TEMPATTACHMENT
            objmail.subject = "Purchase Return Details"
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

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()

            If FRMSTRING = "PURRETURN" Then
                tempattachment = "PURRETURN"

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

            If FRMSTRING = "PURRETURN" Then
                strsearch = "{PURCHASERETURN.PURRET_NO} = " & BILLNO & " AND {PURCHASERETURN.PURRET_YEARID} = " & YearId
                CRPO.SelectionFormula = strsearch
            End If

            Dim OBJ As New Object
            If FRMSTRING = "PURRETURN" Then
                OBJ = New PurchaseReturnReport
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
                If FRMSTRING = "PURRETURN" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_PURCHASERETURN_NO-" & BILLNO & ".pdf"
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
