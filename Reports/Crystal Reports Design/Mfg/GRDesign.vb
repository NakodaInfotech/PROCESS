
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.IO

Public Class GRDesign

    Public WHERECLAUSE As String = ""
    Public FRMSTRING As String = ""
    Public PERIOD As String = ""


    Dim RPTGRDETAILREPORT As New GRDetailReport

    Dim RPTGRBUYERWISEDTLSREPORT As New GRBuyerWiseDtlsReport
    Dim RPTGRBUYERWISESUMMREPORT As New GRBuyerWiseSummReport
    Dim RPTGRDYEINGWISEDTLSREPORT As New GRDyeingWiseDtlsReport
    Dim RPTGRDYEINGWISESUMMREPORT As New GRDyeingWiseSummReport
    Dim RPTGRGREYQUALITYWISEDTLSREPORT As New GRGreyQualityWiseDtlsReport
    Dim RPTGRGREYQUALITYWISESUMMREPORT As New GRGreyQualityWiseSummReport

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

            If FRMSTRING = "ALLDATA" Then
                crTables = RPTGRDETAILREPORT.Database.Tables
            ElseIf FRMSTRING = "BUYERWISEDTLS" Then
                crTables = RPTGRBUYERWISEDTLSREPORT.Database.Tables
            ElseIf FRMSTRING = "BUYERWISESUMM" Then
                crTables = RPTGRBUYERWISESUMMREPORT.Database.Tables
            ElseIf FRMSTRING = "DYEINGWISEDTLS" Then
                crTables = RPTGRDYEINGWISEDTLSREPORT.Database.Tables
            ElseIf FRMSTRING = "DYEINGWISESUMM" Then
                crTables = RPTGRDYEINGWISESUMMREPORT.Database.Tables
            ElseIf FRMSTRING = "GREYQUALITYWISEDTLS" Then
                crTables = RPTGRGREYQUALITYWISEDTLSREPORT.Database.Tables
            ElseIf FRMSTRING = "GREYQUALITYWISESUMM" Then
                crTables = RPTGRGREYQUALITYWISESUMMREPORT.Database.Tables
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "ALLDATA" Then
                CRPO.ReportSource = RPTGRDETAILREPORT
                RPTGRDETAILREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' Goods Return Report - " & PERIOD & "'"
            ElseIf FRMSTRING = "BUYERWISEDTLS" Then
                CRPO.ReportSource = RPTGRBUYERWISEDTLSREPORT
                RPTGRBUYERWISEDTLSREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' Buyerwise Goods Return Details Report - " & PERIOD & "'"
            ElseIf FRMSTRING = "BUYERWISESUMM" Then
                CRPO.ReportSource = RPTGRBUYERWISESUMMREPORT
                RPTGRBUYERWISESUMMREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' Buyerwise Goods Return Summary Report - " & PERIOD & "'"
            ElseIf FRMSTRING = "DYEINGWISEDTLS" Then
                CRPO.ReportSource = RPTGRDYEINGWISEDTLSREPORT
                RPTGRDYEINGWISEDTLSREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' Dyeingwise Goods Return Details Report - " & PERIOD & "'"
            ElseIf FRMSTRING = "DYEINGWISESUMM" Then
                CRPO.ReportSource = RPTGRDYEINGWISESUMMREPORT
                RPTGRDYEINGWISESUMMREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' Dyeingwise Goods Return Summary Report - " & PERIOD & "'"
            ElseIf FRMSTRING = "GREYQUALITYWISEDTLS" Then
                CRPO.ReportSource = RPTGRGREYQUALITYWISEDTLSREPORT
                RPTGRGREYQUALITYWISEDTLSREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' Grey Qualitywise Goods Return Details Report - " & PERIOD & "'"
            ElseIf FRMSTRING = "GREYQUALITYWISESUMM" Then
                CRPO.ReportSource = RPTGRGREYQUALITYWISESUMMREPORT
                RPTGRGREYQUALITYWISESUMMREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' Grey Qualitywise Goods Return Summary Report - " & PERIOD & "'"
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
            objmail.attachment = Application.StartupPath & "\Goods Return Details.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\Goods Return Details.PDF"

            If FRMSTRING = "ALLDATA" Then
                expo = RPTGRDETAILREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGRDETAILREPORT.Export()
            ElseIf FRMSTRING = "BUYERWISEDTLS" Then
                expo = RPTGRBUYERWISEDTLSREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGRBUYERWISEDTLSREPORT.Export()
            ElseIf FRMSTRING = "BUYERWISESUMM" Then
                expo = RPTGRBUYERWISESUMMREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGRBUYERWISESUMMREPORT.Export()
            ElseIf FRMSTRING = "DYEINGWISEDTLS" Then
                expo = RPTGRDYEINGWISEDTLSREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGRDYEINGWISEDTLSREPORT.Export()
            ElseIf FRMSTRING = "DYEINGWISESUMM" Then
                expo = RPTGRDYEINGWISESUMMREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGRDYEINGWISESUMMREPORT.Export()
            ElseIf FRMSTRING = "GREYQUALITYWISEDTLS" Then
                expo = RPTGRGREYQUALITYWISEDTLSREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGRGREYQUALITYWISEDTLSREPORT.Export()
            ElseIf FRMSTRING = "GREYQUALITYWISESUMM" Then
                expo = RPTGRGREYQUALITYWISESUMMREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGRGREYQUALITYWISESUMMREPORT.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

End Class