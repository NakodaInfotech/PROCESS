
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class PLDesign

    Dim RPTPL As New ProfitLossReport
    Dim RPTBS As New BalanceSheetReport
    Dim RPTINTSUMM As New InterestSummReport

    Public frmstring As String
    Public FROMDATE As Date
    Public TODATE As Date
    Public strsearch As String
    Public PERIOD As String

    Private Sub PLDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PLdesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table


            With crConnecttionInfo
                .ServerName = Servername
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            If frmstring = "BALANCESHEET" Then
                crTables = RPTBS.Database.Tables
            ElseIf frmstring = "INTERESTSUMM" Then
                crTables = RPTINTSUMM.Database.Tables
            Else
                crTables = RPTPL.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next
            '************************ END *******************

            CRPO.SelectionFormula = strsearch
            If frmstring = "BALANCESHEET" Then
                RPTBS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                CRPO.ReportSource = RPTBS
            ElseIf frmstring = "INTERESTSUMM" Then
                CRPO.ReportSource = RPTINTSUMM
            Else
                RPTPL.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                CRPO.ReportSource = RPTPL
            End If

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch Exp As LoadSaveReportException
            MsgBox("Incorrect path for loading report.", _
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

        If frmstring = "BALANCESHEET" Then
            tempattachment = "BALANCESHEET"
        ElseIf frmstring = "INTERESTSUMM" Then
            tempattachment = "INTERESTSUMM"
        Else
            tempattachment = "PROFITLOSS"
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

            Dim oDfDopt As New DiskFileDestinationOptions
            Dim expo As ExportOptions
            If frmstring = "BALANCESHEET" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\BALANCESHEET.PDF"
                expo = RPTBS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBS.Export()
            ElseIf frmstring = "INTERESTSUMM" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\INTERESTSUMM.PDF"
                expo = RPTINTSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINTSUMM.Export()
            Else
                oDfDopt.DiskFileName = Application.StartupPath & "\PROFITLOSS.PDF"
                expo = RPTPL.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPL.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

End Class