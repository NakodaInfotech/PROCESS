
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class GreyWastageDesign

    Public WHERECLAUSE As String
    Public FRMSTRING As String
    Public PERIOD As String

    Dim RPTALLDATA As New GreyWastageAllDataReport

    Dim RPTGODOWNDTLS As New GreyWastageGodownWiseDtlsReport
    Dim RPTGODOWNSUMM As New GreyWastageGodownWiseSummReport

    Dim RPTQUALITYDTLS As New GreyWastageQualityWiseReport
    Dim RPTQUALITYSUMM As New GreyWastageQualityWiseSummReport

    Dim RPTTYPEDTLS As New GreyWastageTypeWiseDtlsReport
    Dim RPTTYPESUMM As New GreyWastageTypeWiseSummReport


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

    Private Sub GreyWastageDesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

            If FRMSTRING = "GREYWASTAGE" Then
                crTables = RPTALLDATA.Database.Tables
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                crTables = RPTGODOWNDTLS.Database.Tables
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                crTables = RPTGODOWNSUMM.Database.Tables

            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                crTables = RPTQUALITYDTLS.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                crTables = RPTQUALITYSUMM.Database.Tables

            ElseIf FRMSTRING = "TYPEWISEDTLS" Then
                crTables = RPTTYPEDTLS.Database.Tables
            ElseIf FRMSTRING = "TYPEWISESUMM" Then
                crTables = RPTTYPESUMM.Database.Tables

            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE


            If FRMSTRING = "GREYWASTAGE" Then
                RPTALLDATA.DataDefinition.FormulaFields("PERIOD").Text = "'GREY WASTAGE ALL DATA - " & PERIOD & "'"
                CRPO.ReportSource = RPTALLDATA
            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                RPTGODOWNDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' GREY WASTAGE GODOWN WISE DETAILS- " & PERIOD & "'"
                CRPO.ReportSource = RPTGODOWNDTLS
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                RPTGODOWNSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' GREY WASTAGE GODOWN WISE SUMMARY- " & PERIOD & "'"
                CRPO.ReportSource = RPTGODOWNSUMM

            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                RPTQUALITYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' GREY WASTAGE QUALITY WISE DETAILS- " & PERIOD & "'"
                CRPO.ReportSource = RPTQUALITYDTLS
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                RPTQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' GREY WASTAGE QUALITY WISE SUMMARY- " & PERIOD & "'"
                CRPO.ReportSource = RPTQUALITYSUMM

            ElseIf FRMSTRING = "TYPEWISEDTLS" Then
                RPTTYPEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' GREY WASTAGE TYPE WISE DETAILS- " & PERIOD & "'"
                CRPO.ReportSource = RPTTYPEDTLS
            ElseIf FRMSTRING = "TYPEWISESUMM" Then
                RPTTYPESUMM.DataDefinition.FormulaFields("PERIOD").Text = "' GREY WASTAGE TYPE WISE SUMMARY- " & PERIOD & "'"
                CRPO.ReportSource = RPTTYPESUMM

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
            objmail.attachment = Application.StartupPath & "\GREY WASTAGE Details.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\GREY WASTAGE Details.PDF"

            If FRMSTRING = "GREYWASTAGE" Then
                expo = RPTALLDATA.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTALLDATA.Export()

            ElseIf FRMSTRING = "GODOWNWISEDTLS" Then
                expo = RPTGODOWNDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGODOWNDTLS.Export()
            ElseIf FRMSTRING = "GODOWNWISESUMM" Then
                expo = RPTGODOWNDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGODOWNDTLS.Export()

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

            ElseIf FRMSTRING = "TYPEWISEDTLS" Then
                expo = RPTTYPEDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTTYPEDTLS.Export()
            ElseIf FRMSTRING = "TYPEWISESUMM" Then
                expo = RPTTYPESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTTYPESUMM.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

  
End Class
