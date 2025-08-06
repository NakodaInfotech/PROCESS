
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class GreyRecdFromProcessingDesign

    Public WHERECLAUSE As String
    Public FRMSTRING As String
    Public PERIOD As String

    Dim RPTDTLS As New BaleRecdDetailsReport
    Dim RPTPROCESSORDTLS As New BaleRecdProcessorWiseReport
    Dim RPTQUALITYDTLS As New BaleRecdQualityWiseReport

    Private Sub GreyRecdFromProcessingDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyRecdFromProcessingDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If FRMSTRING = "BALEDTLS" Then
                crTables = RPTDTLS.Database.Tables
            ElseIf FRMSTRING = "PROCESSORWISE" Then
                crTables = RPTPROCESSORDTLS.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISE" Then
                crTables = RPTQUALITYDTLS.Database.Tables
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE


            If FRMSTRING = "BALEDTLS" Then
                RPTDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BALE RECD - " & PERIOD & "'"
                CRPO.ReportSource = RPTDTLS
            ElseIf FRMSTRING = "PROCESSORWISE" Then
                RPTPROCESSORDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BALE RECD PROCESSOR WISE - " & PERIOD & "'"
                CRPO.ReportSource = RPTPROCESSORDTLS
            ElseIf FRMSTRING = "QUALITYWISE" Then
                RPTQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BALE RECD QUALITY WISE - " & PERIOD & "'"
                CRPO.ReportSource = RPTQUALITYDTLS
            End If

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub SendMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendMail.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()

        Try
            Dim objmail As New SendMail
            objmail.attachment = Application.StartupPath & "\Bale Recd Details.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\Bale Recd Details.PDF"

            If FRMSTRING = "BALEDTLS" Then
                expo = RPTDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDTLS.Export()
            ElseIf FRMSTRING = "PROCESSORWISE" Then
                expo = RPTPROCESSORDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPROCESSORDTLS.Export()
            ElseIf FRMSTRING = "QUALITYWISE" Then
                expo = RPTQUALITYDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTQUALITYDTLS.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class