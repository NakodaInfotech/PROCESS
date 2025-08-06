
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class BeamStockDesign

    Public WHERECLAUSE As String = ""
    Public FRMSTRING As String = ""
    Public PERIOD As String = ""
    Public FROMDATE As Date
    Public TODATE As Date
    Public ONLYBALANCE As Integer = 0

    'CHECKED REPORTS
    Dim RPTWEAVERBEAMDTLS As New WeaverBeamStockDetailsReport
    Dim RPTWEAVERWISEBEAMSTOCK As New WeaverBeamWiseStockSummReport
    Dim RPTBEAMWISEWEAVERSTOCK As New BeamWeaverWiseStockSummReport
    Dim RPTBEAMINOUT As New WeaverBeamInOutReport
    Dim RPTBEAMCUTBALANCE As New WeaverBeamCutBalanceReport

    Dim RPTSIZERBEAMSTOCKINHAND As New SizerBeamStockInHandReport
    Dim RPTSIZERBEAMSTOCKDTLS As New SizerBeamStockDetailsReport

    Private Sub BeamStockDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub CRPO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CRPO.Load
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

            If FRMSTRING = "WEAVERBEAMDTLS" Then
                crTables = RPTWEAVERBEAMDTLS.Database.Tables
            ElseIf FRMSTRING = "WEAVERWISEBEAMSTOCK" Then
                crTables = RPTWEAVERWISEBEAMSTOCK.Database.Tables
            ElseIf FRMSTRING = "BEAMWISEWEAVERSTOCK" Then
                crTables = RPTBEAMWISEWEAVERSTOCK.Database.Tables
            ElseIf FRMSTRING = "BEAMINOUT" Then
                crTables = RPTBEAMINOUT.Database.Tables
            ElseIf FRMSTRING = "BEAMCUTBALANCE" Then
                crTables = RPTBEAMCUTBALANCE.Database.Tables


            ElseIf FRMSTRING = "SIZERBEAMSTOCKINHAND" Then
                crTables = RPTSIZERBEAMSTOCKINHAND.Database.Tables
            ElseIf FRMSTRING = "SIZERBEAMSTOCKDTLS" Then
                crTables = RPTSIZERBEAMSTOCKDTLS.Database.Tables
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "WEAVERBEAMDTLS" Then
                RPTWEAVERBEAMDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' WEAVER BEAM STOCK DETAILS - " & PERIOD & "'"
                RPTWEAVERBEAMDTLS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTWEAVERBEAMDTLS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTWEAVERBEAMDTLS
            ElseIf FRMSTRING = "WEAVERWISEBEAMSTOCK" Then
                RPTWEAVERWISEBEAMSTOCK.DataDefinition.FormulaFields("PERIOD").Text = "' WEAVER WISE - BEAM STOCK - " & PERIOD & "'"
                RPTWEAVERWISEBEAMSTOCK.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTWEAVERWISEBEAMSTOCK.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTWEAVERWISEBEAMSTOCK
            ElseIf FRMSTRING = "BEAMWISEWEAVERSTOCK" Then
                RPTBEAMWISEWEAVERSTOCK.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM WISE - WEAVER STOCK - " & PERIOD & "'"
                RPTBEAMWISEWEAVERSTOCK.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTBEAMWISEWEAVERSTOCK.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTBEAMWISEWEAVERSTOCK
            ElseIf FRMSTRING = "BEAMINOUT" Then
                RPTBEAMINOUT.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM WISE - IN OUT - " & PERIOD & "'"
                RPTBEAMINOUT.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTBEAMINOUT.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTBEAMINOUT
            ElseIf FRMSTRING = "BEAMCUTBALANCE" Then
                RPTBEAMCUTBALANCE.DataDefinition.FormulaFields("SHOWONLYBALANCE").Text = Val(ONLYBALANCE)
                RPTBEAMCUTBALANCE.DataDefinition.FormulaFields("PERIOD").Text = "' BEAM CUT BALANCE - " & PERIOD & "'"
                RPTBEAMCUTBALANCE.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTBEAMCUTBALANCE.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTBEAMCUTBALANCE



            ElseIf FRMSTRING = "SIZERBEAMSTOCKINHAND" Then
                RPTSIZERBEAMSTOCKINHAND.DataDefinition.FormulaFields("PERIOD").Text = "' SIZER BEAM STOCK IN HAND - " & PERIOD & "'"
                CRPO.ReportSource = RPTSIZERBEAMSTOCKINHAND
            ElseIf FRMSTRING = "SIZERBEAMSTOCKDTLS" Then
                RPTSIZERBEAMSTOCKDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' SIZER BEAM STOCK DETAILS - " & PERIOD & "'"
                RPTSIZERBEAMSTOCKDTLS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTSIZERBEAMSTOCKDTLS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTSIZERBEAMSTOCKDTLS
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
            objmail.attachment = Application.StartupPath & "\Yarn Ledger.PDF"
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
            oDfDopt.DiskFileName = Application.StartupPath & "\Yarn Ledger.PDF"

            'If FRMSTRING = "ALLDATA" Then
            '    expo = RPTDETAILS.ExportOptions
            '    expo.ExportDestinationType = ExportDestinationType.DiskFile
            '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
            '    expo.DestinationOptions = oDfDopt
            '    RPTDETAILS.Export()
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class