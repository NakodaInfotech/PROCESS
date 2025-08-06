
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.IO

Public Class BaleStockDesign

    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Public PERIOD As String
    Public FROMDATE As Date
    Public TODATE As Date
    Public PARTYNAME As String
    Public PARTYADDRESS As String
    Public SHOWHEADER As Boolean

    'AT DYEING REPORTS
    Dim RPTBALESTOCKDYEINGREPORT As New BaleStockDyeingDetailReport
    Dim RPTBALESTOCKQUALITYWISEDTLSREPORT As New BaleStockQualityWiseDtlslReport
    Dim RPTBALESTOCKQUALITYWISESUMMREPORT As New BaleStockQualityWiseSummReport
    Dim RPTBALESTOCKDYEINGWISEDTLSREPORT As New BaleStockDyeingWiseDtlslReport
    Dim RPTBALESTOCKDYEINGWISESUMMREPORT As New BaleStockDyeingWiseSummReport
    Dim RPTBALESTOCKDYEINGHISTORY As New BaleStockDyeingHistoryReport

    'AT GODOWN REPORTS
    Dim RPTBALESTOCKGODOWNREPORT As New BaleStockGodownReport
    Dim RPTBALESTOCKGODOWNQUALITYDTLSREPORT As New BaleStockGodownQualityDtlslReport
    Dim RPTBALESTOCKGODOWNQUALITYSUMMREPORT As New BaleStockGodownQualitySummReport
    Dim RPTBALESTOCKGODOWNDYEINGDTLSREPORT As New BaleStockGodownDyeingDtlslReport
    Dim RPTBALESTOCKGODOWNDYEINGSUMMREPORT As New BaleStockGodownDyeingSummReport
    Dim RPTBALESTOCKGODOWNHISTORY As New BaleStockGodownHistoryReport

    Private Sub GRDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub GRDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If FRMSTRING = "BALESTOCKDYEING" Then
                crTables = RPTBALESTOCKDYEINGREPORT.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                crTables = RPTBALESTOCKQUALITYWISEDTLSREPORT.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                crTables = RPTBALESTOCKQUALITYWISESUMMREPORT.Database.Tables
            ElseIf FRMSTRING = "DYEINGWISEDTLS" Then
                crTables = RPTBALESTOCKDYEINGWISEDTLSREPORT.Database.Tables
            ElseIf FRMSTRING = "DYEINGWISESUMM" Then
                crTables = RPTBALESTOCKDYEINGWISESUMMREPORT.Database.Tables
            ElseIf FRMSTRING = "BALEHISTORYDYEING" Then
                crTables = RPTBALESTOCKDYEINGHISTORY.Database.Tables


            ElseIf FRMSTRING = "BALESTOCKGODOWN" Then
                crTables = RPTBALESTOCKDYEINGREPORT.Database.Tables
            ElseIf FRMSTRING = "GODOWNQUALITYWISEDTLS" Then
                crTables = RPTBALESTOCKQUALITYWISEDTLSREPORT.Database.Tables
            ElseIf FRMSTRING = "GODOWNQUALITYWISESUMM" Then
                crTables = RPTBALESTOCKQUALITYWISESUMMREPORT.Database.Tables
            ElseIf FRMSTRING = "GODOWNDYEINGWISEDTLS" Then
                crTables = RPTBALESTOCKDYEINGWISEDTLSREPORT.Database.Tables
            ElseIf FRMSTRING = "GODOWNDYEINGWISESUMM" Then
                crTables = RPTBALESTOCKDYEINGWISESUMMREPORT.Database.Tables
            ElseIf FRMSTRING = "BALEHISTORYGODOWN" Then
                crTables = RPTBALESTOCKGODOWNHISTORY.Database.Tables
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "BALESTOCKDYEING" Then
                CRPO.ReportSource = RPTBALESTOCKDYEINGREPORT
                RPTBALESTOCKDYEINGREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' BALE STOCK AT DYEING - " & PERIOD & "'"
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                CRPO.ReportSource = RPTBALESTOCKQUALITYWISEDTLSREPORT
                RPTBALESTOCKQUALITYWISEDTLSREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' BALE STOCK QUALITYWISE DETAILS - " & PERIOD & "'"
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                CRPO.ReportSource = RPTBALESTOCKQUALITYWISESUMMREPORT
                RPTBALESTOCKQUALITYWISESUMMREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' BALE STOCK QUALITYWISE SUMMARY - " & PERIOD & "'"
            ElseIf FRMSTRING = "DYEINGWISEDTLS" Then
                CRPO.ReportSource = RPTBALESTOCKDYEINGWISEDTLSREPORT
                RPTBALESTOCKDYEINGWISEDTLSREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' BALE STOCK DYEINGWISE DETAILS - " & PERIOD & "'"
            ElseIf FRMSTRING = "DYEINGWISESUMM" Then
                CRPO.ReportSource = RPTBALESTOCKDYEINGWISESUMMREPORT
                RPTBALESTOCKDYEINGWISESUMMREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' BALE STOCK DYEINGWISE SUMMARY - " & PERIOD & "'"
            ElseIf FRMSTRING = "BALEHISTORYDYEING" Then
                CRPO.ReportSource = RPTBALESTOCKDYEINGHISTORY
                RPTBALESTOCKDYEINGHISTORY.DataDefinition.FormulaFields("PERIOD").Text = "' BALE HISTORY (DYEING) - " & PERIOD & "'"


                'FOR GODOWN REPORTS
            ElseIf FRMSTRING = "BALESTOCKGODOWN" Then
                CRPO.ReportSource = RPTBALESTOCKGODOWNREPORT
                RPTBALESTOCKDYEINGREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' BALE STOCK AT GODOWN - " & PERIOD & "'"
            ElseIf FRMSTRING = "GODOWNQUALITYWISEDTLS" Then
                CRPO.ReportSource = RPTBALESTOCKGODOWNQUALITYDTLSREPORT
                RPTBALESTOCKQUALITYWISEDTLSREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' BALE STOCK AT GODOWN QUALITYWISE DETAILS - " & PERIOD & "'"
            ElseIf FRMSTRING = "GODOWNQUALITYWISESUMM" Then
                CRPO.ReportSource = RPTBALESTOCKGODOWNQUALITYSUMMREPORT
                RPTBALESTOCKQUALITYWISESUMMREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' BALE STOCK AT GODOWN QUALITYWISE SUMMARY - " & PERIOD & "'"
            ElseIf FRMSTRING = "GODOWNDYEINGWISEDTLS" Then
                CRPO.ReportSource = RPTBALESTOCKGODOWNDYEINGDTLSREPORT
                RPTBALESTOCKDYEINGWISEDTLSREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' BALE STOCK AT GODOWN DYEINGWISE DETAILS - " & PERIOD & "'"
            ElseIf FRMSTRING = "GODOWNDYEINGWISESUMM" Then
                CRPO.ReportSource = RPTBALESTOCKGODOWNDYEINGSUMMREPORT
                RPTBALESTOCKDYEINGWISESUMMREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' BALE STOCK AT GODOWN DYEINGWISE SUMMARY - " & PERIOD & "'"
            ElseIf FRMSTRING = "BALEHISTORYGODOWN" Then
                CRPO.ReportSource = RPTBALESTOCKGODOWNHISTORY
                RPTBALESTOCKGODOWNHISTORY.DataDefinition.FormulaFields("PERIOD").Text = "' BALE HISTORY (GODOWN) - " & PERIOD & "'"

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
            objmail.attachment = Application.StartupPath & "\Bale Stock.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\Bale Stock.PDF"

            If FRMSTRING = "BALESTOCKDYEING" Then
                expo = RPTBALESTOCKDYEINGREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBALESTOCKDYEINGREPORT.Export()
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                expo = RPTBALESTOCKQUALITYWISEDTLSREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBALESTOCKQUALITYWISEDTLSREPORT.Export()
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                expo = RPTBALESTOCKQUALITYWISESUMMREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBALESTOCKQUALITYWISESUMMREPORT.Export()
            ElseIf FRMSTRING = "DYEINGWISEDTLS" Then
                expo = RPTBALESTOCKDYEINGWISEDTLSREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBALESTOCKDYEINGWISEDTLSREPORT.Export()
            ElseIf FRMSTRING = "DYEINGWISESUMM" Then
                expo = RPTBALESTOCKDYEINGWISESUMMREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBALESTOCKDYEINGWISESUMMREPORT.Export()
            ElseIf FRMSTRING = "BALEHISTORYDYEING" Then
                expo = RPTBALESTOCKDYEINGHISTORY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBALESTOCKDYEINGHISTORY.Export()


                'FOR GODOWN REPORTS
            ElseIf FRMSTRING = "BALESTOCKGODOWN" Then
                expo = RPTBALESTOCKGODOWNREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBALESTOCKGODOWNREPORT.Export()
            ElseIf FRMSTRING = "GODOWNQUALITYWISEDTLS" Then
                expo = RPTBALESTOCKGODOWNQUALITYDTLSREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBALESTOCKGODOWNQUALITYDTLSREPORT.Export()
            ElseIf FRMSTRING = "GODOWNQUALITYWISESUMM" Then
                expo = RPTBALESTOCKGODOWNQUALITYSUMMREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBALESTOCKGODOWNQUALITYSUMMREPORT.Export()
            ElseIf FRMSTRING = "GODOWNDYEINGWISEDTLS" Then
                expo = RPTBALESTOCKGODOWNDYEINGDTLSREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBALESTOCKGODOWNDYEINGDTLSREPORT.Export()
            ElseIf FRMSTRING = "GODOWNDYEINGWISESUMM" Then
                expo = RPTBALESTOCKGODOWNDYEINGSUMMREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBALESTOCKGODOWNDYEINGSUMMREPORT.Export()
            ElseIf FRMSTRING = "BALEHISTORYGODOWN" Then
                expo = RPTBALESTOCKGODOWNHISTORY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBALESTOCKGODOWNHISTORY.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

End Class