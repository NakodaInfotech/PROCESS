

Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class BeamWastageDesign

    Public WHERECLAUSE As String
    Public FRMSTRING As String
    Public PERIOD As String

    Dim RPTALLDATAINHOUSE As New BeamWastageAllDataInhouseReport
    Dim RPTINHOUSEBEAMDTLS As New BeamWastageBeamInhouseDtlsReport
    Dim RPTINHOUSEBEAMSUMM As New BeamWastageBeamInhouseSummReport

    Dim RPTINHOUSEGODOWNDTLS As New BeamWastageGodownDtlsReport
    Dim RPTINHOUSEGODOWNSUMM As New BeamWastageGodownSummReport
   

    Dim RPTALLDATAWEAVER As New BeamWastageWeaverAllDataReport
    Dim RPTWEAVERDTLS As New BeamWastageWeaverDtlsReport
    Dim RPTWEAVERSUMM As New BeamWastageWeaverSummReport
    Dim RPTWEAVERBEAMDTLS As New BeamWastageBeamWeaverDtlsReport
    Dim RPTWEAVERBEAMSUMM As New BeamWastageBeamWeaverSummReport

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

    Private Sub BeamWastageDesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

            If FRMSTRING = "BEAMWASTAGEALLDATAINHOUSE" Then
                crTables = RPTALLDATAINHOUSE.Database.Tables
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                crTables = RPTINHOUSEGODOWNDTLS.Database.Tables
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                crTables = RPTINHOUSEGODOWNSUMM.Database.Tables
            ElseIf FRMSTRING = "BEAMWISEDTLS" Then
                crTables = RPTINHOUSEBEAMDTLS.Database.Tables
            ElseIf FRMSTRING = "BEAMWISESUMM" Then
                crTables = RPTINHOUSEBEAMSUMM.Database.Tables

          
            ElseIf FRMSTRING = "BEAMWASTAGEALLDATAWEAVER" Then
                crTables = RPTALLDATAWEAVER.Database.Tables
            ElseIf FRMSTRING = "WEAVERWISEDTLS" Then
                crTables = RPTWEAVERDTLS.Database.Tables
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                crTables = RPTWEAVERSUMM.Database.Tables

            ElseIf FRMSTRING = "BEAMWISEDTLSWEAVER" Then
                crTables = RPTWEAVERBEAMDTLS.Database.Tables
            ElseIf FRMSTRING = "BEAMWISESUMMWEAVER" Then
                crTables = RPTWEAVERBEAMSUMM.Database.Tables

            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE


            If FRMSTRING = "BEAMWASTAGEALLDATAINHOUSE" Then
                RPTALLDATAINHOUSE.DataDefinition.FormulaFields("PERIOD").Text = "'BEAM WASTAGE ALL DATA INHOUSE - " & PERIOD & "'"
                CRPO.ReportSource = RPTALLDATAINHOUSE
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                RPTINHOUSEGODOWNDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM WASTAGE GODOWN WISE DETAILS- " & PERIOD & "'"
                CRPO.ReportSource = RPTINHOUSEGODOWNDTLS
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                RPTINHOUSEGODOWNSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM WASTAGE GODOWN WISE SUMMARY- " & PERIOD & "'"
                CRPO.ReportSource = RPTINHOUSEGODOWNSUMM
            ElseIf FRMSTRING = "BEAMWISEDTLS" Then
                RPTINHOUSEBEAMDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'BEAM WASTAGE BEAM WISE INHOUSE DETAILS- " & PERIOD & "'"
                CRPO.ReportSource = RPTINHOUSEBEAMSUMM
            ElseIf FRMSTRING = "BEAMWISESUMM" Then
                RPTINHOUSEBEAMSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'BEAM WASTAGE BEAM WISE INHOUSE  SUMMARY- " & PERIOD & "'"
                CRPO.ReportSource = RPTINHOUSEBEAMSUMM

            ElseIf FRMSTRING = "BEAMWASTAGEALLDATAWEAVER" Then
                RPTALLDATAWEAVER.DataDefinition.FormulaFields("PERIOD").Text = "'BEAM WASTAGE ALL DATA WEAVER  - " & PERIOD & "'"
                CRPO.ReportSource = RPTALLDATAWEAVER
            ElseIf FRMSTRING = "WEAVERWISEDTLS" Then
                RPTWEAVERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM WASTAGE WEAVER WISE DETAILS- " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERDTLS
            ElseIf FRMSTRING = "WEAVERWISESUMM" Then
                RPTWEAVERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM WASTAGE WEAVER WISE SUMMARY- " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERSUMM
            ElseIf FRMSTRING = "BEAMWISEDTLSWEAVER" Then
                RPTWEAVERBEAMDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'BEAM WASTAGE BEAM WEAVER WISE  DETAILS- " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERBEAMDTLS
            ElseIf FRMSTRING = "BEAMWISESUMMWEAVER" Then
                RPTWEAVERBEAMSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'BEAM WASTAGE BEAM WEAVER WISE  SUMMARY- " & PERIOD & "'"
                CRPO.ReportSource = RPTWEAVERBEAMSUMM


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
            objmail.attachment = Application.StartupPath & "\BEAM WASTAGE Details.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\BEAM WASTAGE Details.PDF"

            If FRMSTRING = "BEAMWASTAGEALLDATAINHOUSE" Then
                expo = RPTALLDATAINHOUSE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTALLDATAINHOUSE.Export()
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                expo = RPTINHOUSEGODOWNDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINHOUSEGODOWNDTLS.Export()
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                expo = RPTINHOUSEGODOWNSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINHOUSEGODOWNSUMM.Export()
            ElseIf FRMSTRING = "BEAMWISEDTLS" Then
                expo = RPTINHOUSEBEAMDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINHOUSEBEAMDTLS.Export()
            ElseIf FRMSTRING = "BEAMWISESUMM" Then
                expo = RPTINHOUSEBEAMSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTINHOUSEBEAMSUMM.Export()

            ElseIf FRMSTRING = "BEAMWASTAGEALLDATAWEAVER" Then
                expo = RPTALLDATAWEAVER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTALLDATAWEAVER.Export()
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
            ElseIf FRMSTRING = "BEAMWISEDTLSWEAVER" Then
                expo = RPTWEAVERBEAMSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERBEAMSUMM.Export()
            ElseIf FRMSTRING = "BEAMWISESUMMWEAVER" Then
                expo = RPTWEAVERBEAMSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTWEAVERBEAMSUMM.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class

