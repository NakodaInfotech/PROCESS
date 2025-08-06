
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class BeamReturnDesign

    Public WHERECLAUSE As String = ""
    Public FRMSTRING As String = ""
    Public PERIOD As String = ""

    Dim RPTALLDATA As New BeamReturnAllDataReport
    Dim RPTBEAMISSUEDTLS As New BeamReturnBeamDtlsReport
    Dim RPTBEAMISSUESUMM As New BeamReturnBeamSummReport

    Dim RPTGODOWNDTLS As New BeamReturnGodownDtlsReport
    Dim RPTGODOWNSUMM As New BeamReturnGodownSummReport

    Dim RPTSIZERRETURNDTLS As New BeamReturnSizerDtlsReport
    Dim RPTSIZERRETURNSUMM As New BeamReturnSizerSummReport

    Dim RPTWEAVERDTLS As New BeamReturnWeaverDtlsReport
    Dim RPTWEAVERSUMM As New BeamReturnWeaverSummReport

    Private Sub BeamIssueDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub BeamIssueDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If FRMSTRING = "BEAMRETURN" Then
                crTables = RPTALLDATA.Database.Tables
            ElseIf FRMSTRING = "BEAMWISEDTLS" Then
                crTables = RPTBEAMISSUEDTLS.Database.Tables
            ElseIf FRMSTRING = "BEAMWISESUMM" Then
                crTables = RPTBEAMISSUESUMM.Database.Tables
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                crTables = RPTGODOWNDTLS.Database.Tables
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                crTables = RPTGODOWNSUMM.Database.Tables
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                crTables = RPTSIZERRETURNDTLS.Database.Tables
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                crTables = RPTSIZERRETURNSUMM.Database.Tables
            ElseIf FRMSTRING = "WEAVERWISEDTLS" Then
                crTables = RPTWEAVERDTLS.Database.Tables
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                crTables = RPTWEAVERSUMM.Database.Tables
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "BEAMRETURN" Then
                CRPO.ReportSource = RPTALLDATA
            ElseIf FRMSTRING = "BEAMWISEDTLS" Then
                RPTBEAMISSUEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM RETURN DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTBEAMISSUEDTLS
            ElseIf FRMSTRING = "BEAMWISESUMM" Then
                RPTBEAMISSUESUMM.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM RETURN SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTBEAMISSUESUMM
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                RPTGODOWNDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM RETURN GODOWN DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTGODOWNDTLS
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                RPTGODOWNSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM RETURN GODOWN SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTGODOWNSUMM
            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                RPTSIZERRETURNDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM RETURN SIZER DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERRETURNDTLS
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                RPTSIZERRETURNSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM RETURN SIZER SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERRETURNSUMM
            ElseIf FRMSTRING = "WEAVERWISEDTLS" Then
                RPTWEAVERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM RETURN WEAVER DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERDTLS
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                RPTWEAVERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM RETURN WEAVER SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERSUMM
           
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
            objmail.attachment = Application.StartupPath & "\Beam Return Details.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\Beam Return Details.PDF"

            If FRMSTRING = "BEAMRETURN" Then
                expo = RPTALLDATA.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTALLDATA.Export()

            ElseIf FRMSTRING = "BEAMWISEDTLS" Then
                expo = RPTBEAMISSUEDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBEAMISSUEDTLS.Export()
            ElseIf FRMSTRING = "BEAMWISESUMM" Then
                expo = RPTBEAMISSUESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBEAMISSUESUMM.Export()

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

            ElseIf FRMSTRING = "SIZERWISEDTLS" Then
                expo = RPTSIZERRETURNDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERRETURNDTLS.Export()
            ElseIf FRMSTRING = "SIZERWISESUMM" Then
                expo = RPTSIZERRETURNSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSIZERRETURNSUMM.Export()

            ElseIf FRMSTRING = "WEAVERWISEDTLS" Then
                expo = RPTWEAVERDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERDTLS.Export()
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                expo = RPTWEAVERSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERSUMM.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class