Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class GRNDesign
    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Public PERIOD As String
    Public SHOWHEADER As Boolean
    Public SHOWPRINTDATE As Boolean
    Public SHOWADDRESS As Boolean
    Public NEWPAGE As Boolean

    Dim RPTDTLS As New GRNDetailReport
    Dim RPTSUMM As New GRNSummary
    Dim RPTPARTYDTLS As New GRNPartyWiseDetails
    Dim RPTPARTYSUMM As New GRNPartyWiseSummary
    Dim RPTBROKERDTLS As New GRNBrokerWiseDetails
    Dim RPTBROKERSUMM As New GRNBrokerWiseSummary
    Dim RPTGODOWNDTLS As New GRNGodownWiseDetails
    Dim RPTGODOWNSUMM As New GRNGodownWiseSummary
    Dim RPTQUALITYDTLS As New GRNQualityWiseDetails
    Dim RPTQUALITYSUMM As New GRNQualityWiseSummary
    Dim RPTMILLDTLS As New GRNMillWiseDetails
    Dim RPTMILLSUMM As New GRNMillWiseSummary
    Dim RPTTRANSDTLS As New GRNTransWiseDetails
    Dim RPTTRANSSUMM As New GRNTransWiseSummary
    Dim RPTMONTHLY As New GRNMonthWise
    Dim RPTBAGWISE As New GRNGodownBagWise
    Dim RPTBILLNOTRECD As New BillNotRecdReport


    Private Sub GRNDesign_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub GRNDesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

            If FRMSTRING = "GRNDTLS" Then crTables = RPTDTLS.Database.Tables
            If FRMSTRING = "GRNSUMM" Then crTables = RPTSUMM.Database.Tables

            If FRMSTRING = "PARTYWISEDTLS" Then crTables = RPTPARTYDTLS.Database.Tables
            If FRMSTRING = "PARTYWISESUMM" Then crTables = RPTPARTYSUMM.Database.Tables

            If FRMSTRING = "BROKERWISEDTLS" Then crTables = RPTBROKERDTLS.Database.Tables
            If FRMSTRING = "BROKERWISESUMM" Then crTables = RPTBROKERSUMM.Database.Tables

            If FRMSTRING = "TRANSWISEDTLS" Then crTables = RPTTRANSDTLS.Database.Tables
            If FRMSTRING = "TRANSWISESUMM" Then crTables = RPTTRANSSUMM.Database.Tables

            If FRMSTRING = "QUALITYWISEDTLS" Then crTables = RPTQUALITYDTLS.Database.Tables
            If FRMSTRING = "QUALITYWISESUMM" Then crTables = RPTQUALITYSUMM.Database.Tables

            If FRMSTRING = "GODOWNWISEDTLS" Then crTables = RPTGODOWNDTLS.Database.Tables
            If FRMSTRING = "GODOWNWISESUMM" Then crTables = RPTGODOWNSUMM.Database.Tables

            If FRMSTRING = "MILLWISEDTLS" Then crTables = RPTMILLDTLS.Database.Tables
            If FRMSTRING = "MILLWISESUMM" Then crTables = RPTMILLSUMM.Database.Tables

            If FRMSTRING = "MONTHLY" Then crTables = RPTMONTHLY.Database.Tables

            If FRMSTRING = "BAGWISE" Then crTables = RPTBAGWISE.Database.Tables
            If FRMSTRING = "BILLNOTRECD" Then crTables = RPTBILLNOTRECD.Database.Tables
            'If FRMSTRING = "GOODSRECDTLS" Then crTables = RPTGOODSRECDTLS.Database.Tables


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            crpo.SelectionFormula = WHERECLAUSE


            If FRMSTRING = "GRNDTLS" Then
                crpo.ReportSource = RPTDTLS
                RPTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "GRNSUMM" Then
                crpo.ReportSource = RPTSUMM
                RPTSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "PARTYWISEDTLS" Then
                crpo.ReportSource = RPTPARTYDTLS
                RPTPARTYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                If SHOWADDRESS = True Then RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'1'" Else RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'0'"
                RPTPARTYDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "PARTYWISESUMM" Then
                crpo.ReportSource = RPTPARTYSUMM
                RPTPARTYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "BROKERWISEDTLS" Then
                crpo.ReportSource = RPTBROKERDTLS
                RPTBROKERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTBROKERDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTBROKERDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTBROKERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTBROKERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                If SHOWADDRESS = True Then RPTBROKERDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'1'" Else RPTBROKERDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'0'"
                RPTBROKERDTLS.GroupFooterSection4.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "BROKERWISESUMM" Then
                crpo.ReportSource = RPTBROKERSUMM
                RPTBROKERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTBROKERSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTBROKERSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTBROKERSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTBROKERSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                crpo.ReportSource = RPTGODOWNDTLS
                RPTGODOWNDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGODOWNDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGODOWNDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGODOWNDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGODOWNDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                If SHOWADDRESS = True Then RPTGODOWNDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'1'" Else RPTGODOWNDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'0'"
                RPTGODOWNDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                crpo.ReportSource = RPTGODOWNSUMM
                RPTGODOWNSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGODOWNSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGODOWNSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGODOWNSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGODOWNSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                crpo.ReportSource = RPTQUALITYDTLS
                RPTQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTQUALITYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTQUALITYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTQUALITYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTQUALITYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTQUALITYDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                crpo.ReportSource = RPTQUALITYSUMM
                RPTQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTQUALITYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTQUALITYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTQUALITYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTQUALITYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "MILLWISEDTLS" Then
                crpo.ReportSource = RPTMILLDTLS
                RPTMILLDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTMILLDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTMILLDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTMILLDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTMILLDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                If SHOWADDRESS = True Then RPTMILLDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'1'" Else RPTMILLDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'0'"
                RPTMILLDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "MILLWISESUMM" Then
                crpo.ReportSource = RPTMILLSUMM
                RPTMILLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTMILLSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTMILLSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTMILLSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTMILLSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "TRANSWISEDTLS" Then
                crpo.ReportSource = RPTTRANSDTLS
                RPTTRANSDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                If SHOWADDRESS = True Then RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'1'" Else RPTTRANSDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'0'"
                RPTTRANSDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "TRANSWISESUMM" Then
                crpo.ReportSource = RPTTRANSSUMM
                RPTTRANSSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTTRANSSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTTRANSSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTTRANSSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTTRANSSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "MONTHLY" Then
                crpo.ReportSource = RPTMONTHLY
                RPTMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTMONTHLY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTMONTHLY.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTMONTHLY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTMONTHLY.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "BAGWISE" Then
                crpo.ReportSource = RPTBAGWISE
                RPTBAGWISE.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTBAGWISE.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTBAGWISE.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTBAGWISE.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTBAGWISE.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTBAGWISE.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "BILLNOTRECD" Then
                CRPO.ReportSource = RPTBILLNOTRECD
                RPTBILLNOTRECD.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTBILLNOTRECD.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTBILLNOTRECD.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTBILLNOTRECD.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTBILLNOTRECD.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

                'ElseIf FRMSTRING = "GOODSRECDTLS" Then
                '    crpo.ReportSource = RPTGOODSRECDTLS
                '    RPTGOODSRECDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                '    If SHOWHEADER = True Then RPTGOODSRECDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGOODSRECDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                '    If SHOWPRINTDATE = True Then RPTGOODSRECDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGOODSRECDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                '    RPTGOODSRECDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            End If

            crpo.Zoom(100)
            crpo.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub SENDMAILTOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SENDMAILTOOL.Click
        Try
            Dim emailid As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Transfer()
            Dim TEMPATTACHMENT As String = "GRN"
            Try
                Dim objmail As New SendMail
                objmail.attachment = Application.StartupPath & "\" & TEMPATTACHMENT & ".PDF"
                objmail.subject = "GRN REPORT"
                If emailid <> "" Then
                    objmail.cmbfirstadd.Text = emailid
                End If
                objmail.Show()
                objmail.BringToFront()
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\GRN.pdf"

            If FRMSTRING = "GRNDTLS" Then
                expo = RPTDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDTLS.Export()
            ElseIf FRMSTRING = "GRNSUMM" Then
                expo = RPTSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSUMM.Export()
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
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                expo = RPTGODOWNDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGODOWNDTLS.Export()
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                expo = RPTGODOWNSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGODOWNSUMM.Export()
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
            ElseIf FRMSTRING = "MONTHLY" Then
                expo = RPTMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTMONTHLY.Export()
            ElseIf FRMSTRING = "BAGWISE" Then
                expo = RPTBAGWISE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBAGWISE.Export()
                'ElseIf FRMSTRING = "GOODSRECDTLS" Then
                '    expo = RPTGOODSRECDTLS.ExportOptions
                '    expo.ExportDestinationType = ExportDestinationType.DiskFile
                '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                '    expo.DestinationOptions = oDfDopt
                '    RPTGOODSRECDTLS.Export()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class