
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class PipeDestroyedDesign

    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Public PERIOD As String
    Public FROMDATE As Date
    Public TODATE As Date
    Public PARTYNAME As String
    Public PARTYADDRESS As String
    Public SHOWHEADER As Boolean
    Public NEWPAGE As Boolean
    Public ADDRESS As Integer
    Public SHOWPRINTDATE As Boolean

    Dim RPTDTLS As New PipeDestroyedAllDetails

    Dim RPTGODOWNDTLS As New PipeDestroyedGodownWiseDetails
    Dim RPTGODOWNSUMM As New PipeDestroyedGodownWiseSummary

    Dim RPTJOBBERDTLS As New PipeDestroyedJobberWiseDetails
    Dim RPTJOBBERSUMM As New PipeDestroyedJobberWiseSummary

    Private Sub PipeDestroyedDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub PipeDESTROYEDDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If FRMSTRING = "PDDTLS" Then crTables = RPTDTLS.Database.Tables

            If FRMSTRING = "GODOWNWISEDTLS" Then crTables = RPTGODOWNDTLS.Database.Tables
            If FRMSTRING = "GODOWNWISESUMM" Then crTables = RPTGODOWNSUMM.Database.Tables

            If FRMSTRING = "JOBBERWISEDTLS" Then crTables = RPTJOBBERDTLS.Database.Tables
            If FRMSTRING = "JOBBERWISESUMM" Then crTables = RPTJOBBERSUMM.Database.Tables


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            crpo.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "PDDTLS" Then
                crpo.ReportSource = RPTDTLS
                RPTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "JOBBERWISEDTLS" Then
                crpo.ReportSource = RPTJOBBERDTLS
                RPTJOBBERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTJOBBERDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTJOBBERDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTJOBBERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTJOBBERDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTJOBBERDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTJOBBERDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "JOBBERWISESUMM" Then
                crpo.ReportSource = RPTJOBBERSUMM
                RPTJOBBERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTJOBBERSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTJOBBERSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTJOBBERSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTJOBBERSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                crpo.ReportSource = RPTGODOWNDTLS
                RPTGODOWNDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGODOWNDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGODOWNDTLS.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGODOWNDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGODOWNDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"
                RPTGODOWNDTLS.DataDefinition.FormulaFields("SHOWADDRESS").Text = ADDRESS
                RPTGODOWNDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE

            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                crpo.ReportSource = RPTGODOWNSUMM
                RPTGODOWNSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                If SHOWHEADER = True Then RPTGODOWNSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTGODOWNSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                If SHOWPRINTDATE = True Then RPTGODOWNSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'1'" Else RPTGODOWNSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = "'0'"

            End If

            crpo.Zoom(100)
            crpo.Refresh()
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
            oDfDopt.DiskFileName = Application.StartupPath & "\PIPEDESTROYED.pdf"

            If FRMSTRING = "PDDTLS" Then
                expo = RPTDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDTLS.Export()
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
            ElseIf FRMSTRING = "JOBBERWISEDTLS" Then
                expo = RPTJOBBERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJOBBERDTLS.Export()
            ElseIf FRMSTRING = "JOBBERWISESUMM" Then
                expo = RPTJOBBERSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTJOBBERSUMM.Export()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub SENDMAILTOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Try
            Dim emailid As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Transfer()

            Dim TEMPATTACHMENT As String = Application.StartupPath & "\PIPEDESTROYED.pdf"
            Dim objmail As New SendMail
            objmail.attachment = TEMPATTACHMENT
            objmail.subject = "Pipe Destroyed Details"
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