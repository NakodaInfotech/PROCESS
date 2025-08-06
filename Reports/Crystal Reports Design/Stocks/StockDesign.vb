
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class StockDesign

    Public FRMSTRING As String
    Public WHERECLAUSE As String
    Public PERIOD As String
    Public FROMDATE As Date
    Public TODATE As Date
    Public PARTYNAME As String
    Public PARTYADDRESS As String
    Public SHOWHEADER As Boolean


    Dim RPTCOMPLETEDTLS As New StockComplete
    Dim RPTCOMPLETESUMM As New StockCompleteSummary

    Dim RPTSTOCKONHANDDTLS As New StockOnHandDetails
    Dim RPTSTOCKONHANDSUMM As New StockOnHandSumm

    Dim RPTUNLIFTEDDTLS As New UnliftedStockDetails
    Dim RPTUNLIFTEDSUMM As New UnliftedStockSummary

    Dim RPTSTOCKREGDTLS As New StockRegisterDetails
    Dim RPTSTOCKREGSUMM As New StockRegisterSumm

    Dim RPTMIS As New WhatWhereStockReport

    Private Sub GDNDESIGN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub GRNDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If FRMSTRING = "COMPLETEDTLS" Then crTables = RPTCOMPLETEDTLS.Database.Tables
            If FRMSTRING = "COMPLETESUMM" Then crTables = RPTCOMPLETESUMM.Database.Tables

            If FRMSTRING = "STOCKONHANDDTLS" Then crTables = RPTSTOCKONHANDDTLS.Database.Tables
            If FRMSTRING = "STOCKONHANDSUMM" Then crTables = RPTSTOCKONHANDSUMM.Database.Tables

            If FRMSTRING = "STOCKUNLIFTEDDTLS" Then crTables = RPTUNLIFTEDDTLS.Database.Tables
            If FRMSTRING = "STOCKUNLIFTEDSUMM" Then crTables = RPTUNLIFTEDSUMM.Database.Tables

            If FRMSTRING = "STOCKREGISTERDTLS" Then crTables = RPTSTOCKREGDTLS.Database.Tables
            If FRMSTRING = "STOCKREGISTERSUMM" Then crTables = RPTSTOCKREGSUMM.Database.Tables

            'DO NOT USE OLEDB CONN FOR MIS, INSTEAD WE USE ODBC
            'THIS GIVE ERROR ON CLIENT MACHIENS
            'DONE BY GULKIT
            'If FRMSTRING = "MIS" Then crTables = RPTMIS.Database.Tables

            If FRMSTRING <> "MIS" Then
                For Each crTable In crTables
                    crtableLogonInfo = crTable.LogOnInfo
                    crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                    crTable.ApplyLogOnInfo(crtableLogonInfo)
                Next
            End If


            crpo.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "COMPLETEDTLS" Then
                crpo.ReportSource = RPTCOMPLETEDTLS
                RPTCOMPLETEDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "COMPLETESUMM" Then
                crpo.ReportSource = RPTCOMPLETESUMM
                RPTCOMPLETESUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "STOCKONHANDDTLS" Then
                crpo.ReportSource = RPTSTOCKONHANDDTLS
                RPTSTOCKONHANDDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "STOCKONHANDSUMM" Then
                crpo.ReportSource = RPTSTOCKONHANDSUMM
                RPTSTOCKONHANDSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "STOCKUNLIFTEDDTLS" Then
                crpo.ReportSource = RPTUNLIFTEDDTLS
                RPTUNLIFTEDDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            ElseIf FRMSTRING = "STOCKUNLIFTEDSUMM" Then
                crpo.ReportSource = RPTUNLIFTEDSUMM
                RPTUNLIFTEDSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"


            ElseIf FRMSTRING = "STOCKREGISTERDTLS" Then
                crpo.ReportSource = RPTSTOCKREGDTLS
                RPTSTOCKREGDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTSTOCKREGDTLS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
            ElseIf FRMSTRING = "STOCKREGISTERSUMM" Then
                crpo.ReportSource = RPTSTOCKREGSUMM
                RPTSTOCKREGSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                RPTSTOCKREGSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                If SHOWHEADER = True Then RPTSTOCKREGSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'1'" Else RPTSTOCKREGSUMM.DataDefinition.FormulaFields("SHOWHEADER").Text = "'0'"
                RPTSTOCKREGSUMM.ReportHeaderSection1.SectionFormat.EnableSuppress = Not SHOWHEADER


            ElseIf FRMSTRING = "MIS" Then
                crpo.ReportSource = RPTMIS

                'For I As Integer = 0 To 7
                '    crTables = RPTMIS.Subreports(I).Database.Tables
                '    For Each crTable In crTables
                '        crtableLogonInfo = crTable.LogOnInfo
                '        crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                '        crTable.ApplyLogOnInfo(crtableLogonInfo)
                '    Next
                'Next



                RPTMIS.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                'RPTMIS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                'RPTMIS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                'RPTMIS.Subreports("YARNWAREHOUSE_SUMM").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTMIS.Subreports("YARNWAREHOUSE_SUMM").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                RPTMIS.Subreports("YARNGODOWN").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTMIS.Subreports("YARNGODOWN").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                RPTMIS.Subreports("YARNSIZER").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTMIS.Subreports("YARNSIZER").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                RPTMIS.Subreports("YARNWEAVER").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTMIS.Subreports("YARNWEAVER").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                RPTMIS.Subreports("BEAMSIZER").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTMIS.Subreports("BEAMSIZER").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                RPTMIS.Subreports("BEAMWEAVER").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTMIS.Subreports("BEAMWEAVER").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                RPTMIS.Subreports("GREYGODOWN").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTMIS.Subreports("GREYGODOWN").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                RPTMIS.Subreports("GREYPROCESS").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTMIS.Subreports("GREYPROCESS").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"

                RPTMIS.Subreports("BALEPROCESS").DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTMIS.Subreports("BALEPROCESS").DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
            End If

            crpo.Zoom(100)
            crpo.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Try
            Dim emailid As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Transfer()
            Dim TEMPATTACHMENT As String
            TEMPATTACHMENT = Application.StartupPath & "\STOCKS.pdf"
            Dim objmail As New SendMail
            objmail.attachment = TEMPATTACHMENT
            If emailid <> "" Then
                objmail.cmbfirstadd.Text = emailid
            End If
            objmail.Show()
            objmail.BringToFront()
            Windows.Forms.Cursor.Current = Cursors.Arrow
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\STOCKS.pdf"

            If FRMSTRING = "COMPLETEDTLS" Then
                expo = RPTCOMPLETEDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCOMPLETEDTLS.Export()

            ElseIf FRMSTRING = "COMPLETESUMM" Then
                expo = RPTCOMPLETESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCOMPLETESUMM.Export()

            ElseIf FRMSTRING = "STOCKONHANDDTLS" Then
                expo = RPTSTOCKONHANDDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSTOCKONHANDDTLS.Export()
            ElseIf FRMSTRING = "STOCKONHANDSUMM" Then
                expo = RPTSTOCKONHANDSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSTOCKONHANDSUMM.Export()
            ElseIf FRMSTRING = "STOCKUNLIFTEDDTLS" Then
                expo = RPTUNLIFTEDDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTUNLIFTEDDTLS.Export()
            ElseIf FRMSTRING = "STOCKUNLIFTEDSUMM" Then
                expo = RPTUNLIFTEDSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTUNLIFTEDSUMM.Export()

            ElseIf FRMSTRING = "STOCKREGISTERDTLS" Then

                expo = RPTSTOCKREGDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSTOCKREGDTLS.Export()
            ElseIf FRMSTRING = "STOCKREGISTERSUMM" Then
                expo = RPTSTOCKREGSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSTOCKREGSUMM.Export()


            ElseIf FRMSTRING = "MIS" Then
                expo = RPTMIS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTMIS.Export()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

End Class