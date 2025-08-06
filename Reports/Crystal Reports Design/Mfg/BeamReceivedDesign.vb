
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class BeamReceivedDesign

    Public WHERECLAUSE As String
    Public FRMSTRING As String
    Public PERIOD As String


    Dim RPTBEAMRECDDTLS As New BeamRecdDtlsReport
    Dim RPTBEAMRECDSUMM As New BeamRecdSummReport
    Dim RPTBEAMRECDBEAMDTLS As New BeamRecdBeamDtlsReport
    Dim RPTBEAMRECDBEAMSUMM As New BeamRecdBeamSummReport
    Dim RPTBEAMRECDMILLDTLS As New BeamRecdMillDtlsReport
    Dim RPTBEAMRECDMILLSUMM As New BeamRecdMillSummReport
    Dim RPTBEAMRECDSIZERDTLS As New BeamRecdSizerDtlsReport
    Dim RPTBEAMRECDSIZERSUMM As New BeamRecdSizerSummReport
    
    Private Sub BeamReceivedDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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


    Private Sub BeamReceivedDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If FRMSTRING = "BEAMRECDDTLS" Then
                crTables = RPTBEAMRECDDTLS.Database.Tables
            ElseIf FRMSTRING = "BEAMRECDSUMM" Then
                crTables = RPTBEAMRECDSUMM.Database.Tables
            ElseIf FRMSTRING = "BEAMWISEDTLS" Then
                crTables = RPTBEAMRECDBEAMDTLS.Database.Tables
            ElseIf FRMSTRING = "BEAMWISESUMM" Then
                crTables = RPTBEAMRECDBEAMSUMM.Database.Tables
            ElseIf FRMSTRING = "MILLWISEDTLS" Then
                crTables = RPTBEAMRECDMILLDTLS.Database.Tables
            ElseIf FRMSTRING = "MILLWISESUMM" Then
                crTables = RPTBEAMRECDMILLSUMM.Database.Tables
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                crTables = RPTBEAMRECDSIZERDTLS.Database.Tables
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                crTables = RPTBEAMRECDSIZERSUMM.Database.Tables
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "BEAMRECDDTLS" Then
                RPTBEAMRECDDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM RECD DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTBEAMRECDDTLS
            ElseIf FRMSTRING = "BEAMRECDSUMM" Then
                RPTBEAMRECDSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM RECD SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTBEAMRECDSUMM
            ElseIf FRMSTRING = "BEAMWISEDTLS" Then
                RPTBEAMRECDBEAMDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTBEAMRECDBEAMDTLS
            ElseIf FRMSTRING = "BEAMWISESUMM" Then
                RPTBEAMRECDBEAMSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTBEAMRECDBEAMSUMM
            ElseIf FRMSTRING = "MILLWISEDTLS" Then
                RPTBEAMRECDMILLDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTBEAMRECDMILLDTLS
            ElseIf FRMSTRING = "MILLWISESUMM" Then
                RPTBEAMRECDMILLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTBEAMRECDMILLSUMM
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                RPTBEAMRECDSIZERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' SIZER WISE DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTBEAMRECDSIZERDTLS
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                RPTBEAMRECDSIZERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' SIZER WISE SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTBEAMRECDSIZERSUMM
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
            objmail.attachment = Application.StartupPath & "\Beam Recd Details.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\Beam Recd Details.PDF"

            If FRMSTRING = "BEAMRECDDTLS" Then
                expo = RPTBEAMRECDDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBEAMRECDDTLS.Export()
            ElseIf FRMSTRING = "BEAMRECDSUMM" Then
                expo = RPTBEAMRECDSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBEAMRECDSUMM.Export()
            ElseIf FRMSTRING = "BEAMWISEDTLS" Then
                expo = RPTBEAMRECDBEAMDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBEAMRECDBEAMDTLS.Export()
            ElseIf FRMSTRING = "BEAMWISESUMM" Then
                expo = RPTBEAMRECDBEAMSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBEAMRECDBEAMSUMM.Export()
            ElseIf FRMSTRING = "MILLWISEDTLS" Then
                expo = RPTBEAMRECDMILLDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBEAMRECDMILLDTLS.Export()
            ElseIf FRMSTRING = "MILLWISESUMM" Then
                expo = RPTBEAMRECDMILLSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBEAMRECDMILLSUMM.Export()
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                expo = RPTBEAMRECDSIZERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBEAMRECDSIZERDTLS.Export()
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                expo = RPTBEAMRECDSIZERSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBEAMRECDSIZERSUMM.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class