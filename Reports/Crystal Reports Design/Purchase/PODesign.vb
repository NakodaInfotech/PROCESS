
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class PODesign
    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Public PERIOD As String
    Public SHOWHEADER As Boolean
    Public SHOWPRINTDATE As Boolean
    Public SHOWADDRESS As Boolean
    Public NEWPAGE As Boolean

    Dim RPTPO As New POReport
    Dim RPTDTLS As New PODetailsReport
    Dim RPTSUMM As New POSummaryReport
    Dim RPTPARTYDTLS As New POPartyWiseDetails
    Dim RPTPARTYSUMM As New POPartyWiseSummary
    Dim RPTAGENTDTLS As New POBrokerWiseDetails
    Dim RPTAGENTSUMM As New POBrokerWiseSummary
    'Dim RPTITEMSUMM As New POItemWiseSummary
    Dim RPTQUALITYDTLS As New POQualityWiseDetails
    Dim RPTQUALITYSUMM As New POQualityWiseSummary
    Dim RPTMILLDTLS As New POMillWiseDetails
    Dim RPTMILLSUMM As New POMillWiseSummary


    Dim RPTPENDING As New POPendingOrders
    Dim RPTCLOSED As New POClosedOrders
    Dim RPTCOMPLETED As New POCompletedOrders

    Private Sub PODesign_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub PODesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

            If FRMSTRING = "PO" Then crTables = RPTPO.Database.Tables
            If FRMSTRING = "PODTLS" Then crTables = RPTDTLS.Database.Tables
            If FRMSTRING = "POSUMM" Then crTables = RPTSUMM.Database.Tables

            If FRMSTRING = "PARTYWISEDTLS" Then crTables = RPTPARTYDTLS.Database.Tables
            If FRMSTRING = "PARTYWISESUMM" Then crTables = RPTPARTYSUMM.Database.Tables

            If FRMSTRING = "BROKERWISEDTLS" Then crTables = RPTAGENTDTLS.Database.Tables
            If FRMSTRING = "BROKERWISESUMM" Then crTables = RPTAGENTSUMM.Database.Tables

            If FRMSTRING = "QUALITYWISEDTLS" Then crTables = RPTQUALITYDTLS.Database.Tables
            If FRMSTRING = "QUALITYWISESUMM" Then crTables = RPTQUALITYSUMM.Database.Tables

            If FRMSTRING = "MILLWISEDTLS" Then crTables = RPTMILLDTLS.Database.Tables
            If FRMSTRING = "MILLWISESUMM" Then crTables = RPTMILLSUMM.Database.Tables


            If FRMSTRING = "PENDINGORDERS" Then crTables = RPTPENDING.Database.Tables
            If FRMSTRING = "CLOSEDORDERS" Then crTables = RPTCLOSED.Database.Tables
            If FRMSTRING = "COMPLETEORDERS" Then crTables = RPTCOMPLETED.Database.Tables


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "PO" Then
                CRPO.ReportSource = RPTPO

            ElseIf FRMSTRING = "PODTLS" Then
                CRPO.ReportSource = RPTDTLS
                RPTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                If SHOWADDRESS = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'1'" Else RPTDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'0'"

            ElseIf FRMSTRING = "POSUMM" Then
                CRPO.ReportSource = RPTSUMM
                RPTSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "PARTYWISEDTLS" Then
                CRPO.ReportSource = RPTPARTYDTLS
                RPTPARTYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTPARTYDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTPARTYDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "PARTYWISESUMM" Then
                CRPO.ReportSource = RPTPARTYSUMM
                RPTPARTYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTPARTYSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "BROKERWISEDTLS" Then
                CRPO.ReportSource = RPTAGENTDTLS
                RPTAGENTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTAGENTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTAGENTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTAGENTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTAGENTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                If SHOWADDRESS = True Then RPTAGENTDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'1'" Else RPTAGENTDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'0'"
                RPTAGENTDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "BROKERWISESUMM" Then
                CRPO.ReportSource = RPTAGENTSUMM
                RPTAGENTSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTAGENTSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTAGENTSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTAGENTSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTAGENTSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

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
                If SHOWADDRESS = True Then RPTMILLDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'1'" Else RPTMILLDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = "'0'"
                RPTMILLDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "MILLWISESUMM" Then
                CRPO.ReportSource = RPTMILLSUMM
                RPTMILLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTMILLSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTMILLSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTMILLSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTMILLSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "PENDINGORDERS" Then
                CRPO.ReportSource = RPTPENDING
                RPTPENDING.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "CLOSEDORDERS" Then
                CRPO.ReportSource = RPTCLOSED
                RPTCLOSED.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "COMPLETEORDERS" Then
                CRPO.ReportSource = RPTCOMPLETED
                RPTCOMPLETED.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            End If

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\PURCHASE ORDER.pdf"


            If FRMSTRING = "PO" Then
                expo = RPTPO.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPO.Export()
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
            ElseIf FRMSTRING = "AGENTWISEDTLS" Then
                expo = RPTAGENTDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTAGENTDTLS.Export()
            ElseIf FRMSTRING = "AGENTWISESUMM" Then
                expo = RPTAGENTSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTAGENTSUMM.Export()

            ElseIf FRMSTRING = "QUALITYDTLS" Then
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


            ElseIf FRMSTRING = "PENDINGORDERS" Then
                expo = RPTPENDING.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPENDING.Export()
            ElseIf FRMSTRING = "CLOSEDORDERS" Then
                expo = RPTCLOSED.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCLOSED.Export()
            ElseIf FRMSTRING = "COMPLETEORDERS" Then
                expo = RPTCOMPLETED.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCOMPLETED.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Try
            Dim emailid As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Transfer()
            Dim TEMPATTACHMENT As String = "PURCHASE ORDER"
            Dim objmail As New SendMail
            objmail.attachment = TEMPATTACHMENT
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
End Class