
Imports BL
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Windows.Forms

Public Class E1FormDesign

    Dim RPTE1FORMLETTER As New E1FormLetterReport
    Dim RPTE1FORMPENDINGDTLS As New E1FormPendingReport_Details
    Dim RPTE1FORMPENDINGSUMM As New E1FormPendingReport_Summary
    Dim RPTE1FORMRECDDTLS As New E1FormRecdReport_Details
    Dim RPTE1FORMRECDSUMM As New E1FormRecdReport_Summary
    Dim RPTE1FORMALLDTLS As New E1FormAllReport_Details
    Dim RPTE1FORMALLSUMM As New E1FormAllReport_Summary
    Dim RPTE1FORMBROKERPENDINGDTLS As New E1FormBrokerPendingReport_Details
    Dim RPTE1FORMBROKERPENDINGSUMM As New E1FormBrokerPendingReport_Summary
    Dim RPTE1FORMBROKERRECDDTLS As New E1FormBrokerRecdReport_Details
    Dim RPTE1FORMBROKERRECDSUMM As New E1FormBrokerRecdReport_Summary
    Dim RPTE1FORMBROKERALLDTLS As New E1FormBrokerAllReport_Details
    Dim RPTE1FORMBROKERALLSUMM As New E1FormBrokerAllReport_Summary
    Dim RPTE1FORMTRANSIT As New E1TransitReport

    Public FORMNO As String
    Public STRSEARCH As String
    Public FRMSTRING As String
    Public REPORTTITLE As String
    Public PERIOD As String
    Public NEWPAGE As Boolean

    Public FROMDATE As Date
    Public TODATE As Date
    Public CHECK As Boolean
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

    Private Sub crpo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles crpo.Load
        Try

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

            If FRMSTRING = "LETTER" Then
                crTables = RPTE1FORMLETTER.Database.Tables
            ElseIf FRMSTRING = "PENDINGDTLS" Then
                crTables = RPTE1FORMPENDINGDTLS.Database.Tables
            ElseIf FRMSTRING = "PENDINGSUMM" Then
                crTables = RPTE1FORMPENDINGSUMM.Database.Tables
            ElseIf FRMSTRING = "RECDDTLS" Then
                crTables = RPTE1FORMRECDDTLS.Database.Tables
            ElseIf FRMSTRING = "RECDSUMM" Then
                crTables = RPTE1FORMRECDSUMM.Database.Tables
            ElseIf FRMSTRING = "ALLDTLS" Then
                crTables = RPTE1FORMALLDTLS.Database.Tables
            ElseIf FRMSTRING = "ALLSUMM" Then
                crTables = RPTE1FORMALLSUMM.Database.Tables
            ElseIf FRMSTRING = "BROKERPENDINGDTLS" Then
                crTables = RPTE1FORMBROKERPENDINGDTLS.Database.Tables
            ElseIf FRMSTRING = "BROKERPENDINGSUMM" Then
                crTables = RPTE1FORMBROKERPENDINGSUMM.Database.Tables
            ElseIf FRMSTRING = "BROKERRECDDTLS" Then
                crTables = RPTE1FORMBROKERRECDDTLS.Database.Tables
            ElseIf FRMSTRING = "BROKERRECDSUMM" Then
                crTables = RPTE1FORMBROKERRECDSUMM.Database.Tables
            ElseIf FRMSTRING = "BROKERALLDTLS" Then
                crTables = RPTE1FORMBROKERALLDTLS.Database.Tables
            ElseIf FRMSTRING = "BROKERALLSUMM" Then
                crTables = RPTE1FORMBROKERALLSUMM.Database.Tables
            ElseIf FRMSTRING = "UNDERTRANSIT" Then
                crTables = RPTE1FORMTRANSIT.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next
            '************************ END *******************
            getFromToDate()

            crpo.SelectionFormula = STRSEARCH
            If FRMSTRING = "LETTER" Then
                RPTE1FORMLETTER.DataDefinition.FormulaFields("FORMNO").Text = "'" & FORMNO & "'"
                RPTE1FORMLETTER.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTE1FORMLETTER.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                crpo.ReportSource = RPTE1FORMLETTER
            ElseIf FRMSTRING = "PENDINGDTLS" Then
                RPTE1FORMPENDINGDTLS.DataDefinition.FormulaFields("REPORTTITLE").Text = "'" & REPORTTITLE & "'"
                RPTE1FORMPENDINGDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTE1FORMPENDINGDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                crpo.ReportSource = RPTE1FORMPENDINGDTLS
            ElseIf FRMSTRING = "PENDINGSUMM" Then
                RPTE1FORMPENDINGSUMM.DataDefinition.FormulaFields("REPORTTITLE").Text = "'" & REPORTTITLE & "'"
                RPTE1FORMPENDINGSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                crpo.ReportSource = RPTE1FORMPENDINGSUMM
            ElseIf FRMSTRING = "RECDDTLS" Then
                RPTE1FORMRECDDTLS.DataDefinition.FormulaFields("REPORTTITLE").Text = "'" & REPORTTITLE & "'"
                RPTE1FORMRECDDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTE1FORMRECDDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                crpo.ReportSource = RPTE1FORMRECDDTLS
            ElseIf FRMSTRING = "RECDSUMM" Then
                RPTE1FORMRECDSUMM.DataDefinition.FormulaFields("REPORTTITLE").Text = "'" & REPORTTITLE & "'"
                RPTE1FORMRECDSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                crpo.ReportSource = RPTE1FORMRECDSUMM
            ElseIf FRMSTRING = "ALLDTLS" Then
                RPTE1FORMALLDTLS.DataDefinition.FormulaFields("REPORTTITLE").Text = "'" & REPORTTITLE & "'"
                RPTE1FORMALLDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTE1FORMALLDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                crpo.ReportSource = RPTE1FORMALLDTLS
            ElseIf FRMSTRING = "ALLSUMM" Then
                RPTE1FORMALLSUMM.DataDefinition.FormulaFields("REPORTTITLE").Text = "'" & REPORTTITLE & "'"
                RPTE1FORMALLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                crpo.ReportSource = RPTE1FORMALLSUMM
            ElseIf FRMSTRING = "BROKERPENDINGDTLS" Then
                RPTE1FORMBROKERPENDINGDTLS.DataDefinition.FormulaFields("REPORTTITLE").Text = "'" & REPORTTITLE & "'"
                RPTE1FORMBROKERPENDINGDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTE1FORMBROKERPENDINGDTLS.GroupFooterSection3.SectionFormat.EnableNewPageAfter = NEWPAGE
                crpo.ReportSource = RPTE1FORMBROKERPENDINGDTLS
            ElseIf FRMSTRING = "BROKERPENDINGSUMM" Then
                RPTE1FORMBROKERPENDINGSUMM.DataDefinition.FormulaFields("REPORTTITLE").Text = "'" & REPORTTITLE & "'"
                RPTE1FORMBROKERPENDINGSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                crpo.ReportSource = RPTE1FORMBROKERPENDINGSUMM
            ElseIf FRMSTRING = "BROKERRECDDTLS" Then
                RPTE1FORMBROKERRECDDTLS.DataDefinition.FormulaFields("REPORTTITLE").Text = "'" & REPORTTITLE & "'"
                RPTE1FORMBROKERRECDDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTE1FORMBROKERRECDDTLS.GroupFooterSection3.SectionFormat.EnableNewPageAfter = NEWPAGE
                crpo.ReportSource = RPTE1FORMBROKERRECDDTLS
            ElseIf FRMSTRING = "BROKERRECDSUMM" Then
                RPTE1FORMBROKERRECDSUMM.DataDefinition.FormulaFields("REPORTTITLE").Text = "'" & REPORTTITLE & "'"
                RPTE1FORMBROKERRECDSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                crpo.ReportSource = RPTE1FORMBROKERRECDSUMM
            ElseIf FRMSTRING = "BROKERALLDTLS" Then
                RPTE1FORMBROKERALLDTLS.DataDefinition.FormulaFields("REPORTTITLE").Text = "'" & REPORTTITLE & "'"
                RPTE1FORMBROKERALLDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTE1FORMBROKERALLDTLS.GroupFooterSection3.SectionFormat.EnableNewPageAfter = NEWPAGE
                crpo.ReportSource = RPTE1FORMBROKERALLDTLS
            ElseIf FRMSTRING = "BROKERALLSUMM" Then
                RPTE1FORMBROKERALLSUMM.DataDefinition.FormulaFields("REPORTTITLE").Text = "'" & REPORTTITLE & "'"
                RPTE1FORMBROKERALLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                crpo.ReportSource = RPTE1FORMBROKERALLSUMM
            ElseIf FRMSTRING = "UNDERTRANSIT" Then
                RPTE1FORMTRANSIT.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                crpo.ReportSource = RPTE1FORMTRANSIT
            End If

            crpo.Zoom(100)
            crpo.Refresh()

        Catch Exp As LoadSaveReportException
            MsgBox("Incorrect path for loading report.", _
                    MsgBoxStyle.Critical, "Load Report Error")
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()

        Try
            Dim objmail As New SendMail
            objmail.attachment = Application.StartupPath & "\E1Form Details.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\E1Form Details.PDF"

            If FRMSTRING = "LETTER" Then
                expo = RPTE1FORMLETTER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTE1FORMLETTER.Export()
            ElseIf FRMSTRING = "PENDINGDTLS" Then
                expo = RPTE1FORMPENDINGDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTE1FORMPENDINGDTLS.Export()
            ElseIf FRMSTRING = "PENDINGSUMM" Then
                expo = RPTE1FORMPENDINGSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTE1FORMPENDINGSUMM.Export()
            ElseIf FRMSTRING = "RECDDTLS" Then
                expo = RPTE1FORMLETTER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTE1FORMRECDDTLS.Export()
            ElseIf FRMSTRING = "RECDSUMM" Then
                expo = RPTE1FORMRECDSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTE1FORMRECDSUMM.Export()
            ElseIf FRMSTRING = "ALLDTLS" Then
                expo = RPTE1FORMALLDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTE1FORMALLDTLS.Export()
            ElseIf FRMSTRING = "ALLSUMM" Then
                expo = RPTE1FORMALLSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTE1FORMALLSUMM.Export()
            ElseIf FRMSTRING = "BROKERPENDINGDTLS" Then
                expo = RPTE1FORMBROKERPENDINGDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTE1FORMBROKERPENDINGDTLS.Export()
            ElseIf FRMSTRING = "BROKERPENDINGSUMM" Then
                expo = RPTE1FORMBROKERPENDINGSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTE1FORMBROKERPENDINGSUMM.Export()
            ElseIf FRMSTRING = "BROKERRECDDTLS" Then
                expo = RPTE1FORMBROKERRECDDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTE1FORMBROKERRECDDTLS.Export()
            ElseIf FRMSTRING = "BROKERRECDSUMM" Then
                expo = RPTE1FORMBROKERRECDSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTE1FORMBROKERRECDSUMM.Export()
            ElseIf FRMSTRING = "BROKERALLDTLS" Then
                expo = RPTE1FORMBROKERALLDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTE1FORMBROKERALLDTLS.Export()
            ElseIf FRMSTRING = "BROKERALLSUMM" Then
                expo = RPTE1FORMBROKERALLSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTE1FORMBROKERALLSUMM.Export()
            ElseIf FRMSTRING = "UNDERTRANSIT" Then
                expo = RPTE1FORMTRANSIT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTE1FORMTRANSIT.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub E1FormDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class