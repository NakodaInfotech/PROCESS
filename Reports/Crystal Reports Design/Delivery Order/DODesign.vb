Imports BL
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared

Public Class DODesign

    Dim RPTDO As New DeliveryReport
    Dim RPTDOREGISTER As New DORegisterReport
    Dim RPTDOPARTYWISE As New DOPartyWiseReport
    Dim RPTDOMILLWISE As New DOMillWiseReport
    Dim RPTDOSUPPLIERWISE As New DOSupplierWiseReport

    Public WHERECLAUSE As String
    Public FRMSTRING As String
    Public PERIOD As String

    Private Sub DODesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DODesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If FRMSTRING = "DOREPORT" Then
                crTables = RPTDO.Database.Tables
            ElseIf FRMSTRING = "DOREGISTER" Then
                crTables = RPTDOREGISTER.Database.Tables
            ElseIf FRMSTRING = "DOPARTYWISE" Then
                crTables = RPTDOPARTYWISE.Database.Tables
            ElseIf FRMSTRING = "DOMILLWISE" Then
                crTables = RPTDOMILLWISE.Database.Tables
            ElseIf FRMSTRING = "DOSUPPLIERWISE" Then
                crTables = RPTDOSUPPLIERWISE.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "DOREPORT" Then
                CRPO.ReportSource = RPTDO
            ElseIf FRMSTRING = "DOREGISTER" Then
                RPTDOREGISTER.DataDefinition.FormulaFields("PERIOD").Text = "'DO REISTER - " & PERIOD & "'"
                CRPO.ReportSource = RPTDOREGISTER
            ElseIf FRMSTRING = "DOPARTYWISE" Then
                RPTDOPARTYWISE.DataDefinition.FormulaFields("PERIOD").Text = "'DO REISTER - " & PERIOD & "'"
                CRPO.ReportSource = RPTDOPARTYWISE
            ElseIf FRMSTRING = "DOMILLWISE" Then
                RPTDOMILLWISE.DataDefinition.FormulaFields("PERIOD").Text = "'DO REISTER - " & PERIOD & "'"
                CRPO.ReportSource = RPTDOMILLWISE
            ElseIf FRMSTRING = "DOSUPPLIERWISE" Then
                RPTDOSUPPLIERWISE.DataDefinition.FormulaFields("PERIOD").Text = "'DO REISTER - " & PERIOD & "'"
                CRPO.ReportSource = RPTDOSUPPLIERWISE
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

    Private Sub sendmailtool_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()
        Dim tempattachment As String = "Invoice"
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
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions

            oDfDopt.DiskFileName = Application.StartupPath & "\Delivery Order.PDF"
            If FRMSTRING = "DOREPORT" Then
                expo = RPTDO.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDO.Export()
            ElseIf FRMSTRING = "DOREGISTER" Then
                expo = RPTDOREGISTER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDOREGISTER.Export()
            ElseIf FRMSTRING = "DOPARTYWISE" Then
                expo = RPTDOPARTYWISE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDOPARTYWISE.Export()
            ElseIf FRMSTRING = "DOMILLWISE" Then
                expo = RPTDOMILLWISE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDOMILLWISE.Export()
            ElseIf FRMSTRING = "DOSUPPLIERWISE" Then
                expo = RPTDOSUPPLIERWISE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDOSUPPLIERWISE.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class