
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.IO

Public Class GreyStockDyeingDesign

    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Public PERIOD As String
    Public FROMDATE As Date
    Public TODATE As Date
    Public PARTYNAME As String
    Public PARTYADDRESS As String
    Public SHOWHEADER As Boolean

    Dim RPTGREYSTOCKDYEINGWISEDTLSREPORT As New GreyStockDyeingDtlsReport
    Dim RPTGREYSTOCKDYEINGWISESUMMREPORT As New GreyStockDyeingSummReport
    Dim RPTGREYSTOCKQUALITYWISEDTLSREPORT As New GreyStockDyeingQualityDtlsReport
    Dim RPTGREYSTOCKQUALITYWISESUMMREPORT As New GreyStockDyeingQualitySummReport

    'LOTREPORTS
    Dim RPTPENDINGLOTREGISTER As New PendingLotReport
    Dim RPTLOTHISTORY As New LotHistoryReport

    Private Sub GreyStockDyeingDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub GreyStockDyeingDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If FRMSTRING = "DYEINGWISEDTLS" Then
                crTables = RPTGREYSTOCKDYEINGWISEDTLSREPORT.Database.Tables
            ElseIf FRMSTRING = "DYEINGWISESUMM" Then
                crTables = RPTGREYSTOCKDYEINGWISESUMMREPORT.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                crTables = RPTGREYSTOCKQUALITYWISEDTLSREPORT.Database.Tables
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                crTables = RPTGREYSTOCKQUALITYWISESUMMREPORT.Database.Tables
            ElseIf FRMSTRING = "PENDINGLOTREGISTER" Then
                crTables = RPTPENDINGLOTREGISTER.Database.Tables
            ElseIf FRMSTRING = "LOTHISTORY" Then
                crTables = RPTLOTHISTORY.Database.Tables
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "DYEINGWISEDTLS" Then
                CRPO.ReportSource = RPTGREYSTOCKDYEINGWISEDTLSREPORT
                RPTGREYSTOCKDYEINGWISEDTLSREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' Dyeing Grey Stock Dyeing Details Report - " & PERIOD & "'"
                RPTGREYSTOCKDYEINGWISEDTLSREPORT.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTGREYSTOCKDYEINGWISEDTLSREPORT.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
            ElseIf FRMSTRING = "DYEINGWISESUMM" Then
                CRPO.ReportSource = RPTGREYSTOCKDYEINGWISESUMMREPORT
                RPTGREYSTOCKDYEINGWISESUMMREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' Dyeing Grey Stock Dyeing Report - " & PERIOD & "'"
                RPTGREYSTOCKDYEINGWISESUMMREPORT.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTGREYSTOCKDYEINGWISESUMMREPORT.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                CRPO.ReportSource = RPTGREYSTOCKQUALITYWISEDTLSREPORT
                RPTGREYSTOCKQUALITYWISEDTLSREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' Grey Stock Qualitywise Details Report - " & PERIOD & "'"
                RPTGREYSTOCKQUALITYWISEDTLSREPORT.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTGREYSTOCKQUALITYWISEDTLSREPORT.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                CRPO.ReportSource = RPTGREYSTOCKQUALITYWISESUMMREPORT
                RPTGREYSTOCKQUALITYWISESUMMREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' Grey Stock Qualitywise Summary Report - " & PERIOD & "'"
                RPTGREYSTOCKQUALITYWISESUMMREPORT.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTGREYSTOCKQUALITYWISESUMMREPORT.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                'LOT REPORTS
            ElseIf FRMSTRING = "PENDINGLOTREGISTER" Then
                CRPO.ReportSource = RPTPENDINGLOTREGISTER
                RPTPENDINGLOTREGISTER.DataDefinition.FormulaFields("PERIOD").Text = "' Pending Lot Register - " & PERIOD & "'"
            ElseIf FRMSTRING = "LOTHISTORY" Then
                CRPO.ReportSource = RPTLOTHISTORY
                RPTLOTHISTORY.DataDefinition.FormulaFields("PERIOD").Text = "' Lot History Report - " & PERIOD & "'"
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
            objmail.attachment = Application.StartupPath & "\Grey Stock Dyeing Details.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\Grey Stock Dyeing Details.PDF"

            If FRMSTRING = "DYEINGWISEDTLS" Then
                expo = RPTGREYSTOCKDYEINGWISEDTLSREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYSTOCKDYEINGWISEDTLSREPORT.Export()
            ElseIf FRMSTRING = "DYEINGWISESUMM" Then
                expo = RPTGREYSTOCKDYEINGWISESUMMREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYSTOCKDYEINGWISESUMMREPORT.Export()
            ElseIf FRMSTRING = "QUALITYWISEDTLS" Then
                expo = RPTGREYSTOCKQUALITYWISEDTLSREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYSTOCKQUALITYWISEDTLSREPORT.Export()
            ElseIf FRMSTRING = "QUALITYWISESUMM" Then
                expo = RPTGREYSTOCKQUALITYWISESUMMREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYSTOCKQUALITYWISESUMMREPORT.Export()


                'LOTREPORTS
            ElseIf FRMSTRING = "PENDINGLOTREGISTER" Then
                expo = RPTPENDINGLOTREGISTER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPENDINGLOTREGISTER.Export()
            ElseIf FRMSTRING = "LOTHISTORY" Then
                expo = RPTLOTHISTORY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTLOTHISTORY.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

End Class