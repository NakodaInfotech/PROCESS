
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class YarnStockDesign

    Public WHERECLAUSE As String = ""
    Public FRMSTRING As String = ""
    Public PERIOD As String = ""
    Public FROMDATE As Date
    Public TODATE As Date

    'CHECKED REPORTS
    Dim RPTSUMM As New YarnQualitySummReport
    Dim RPTSHADESUMM As New YarnQualityShadeSummReport
    Dim RPTDETAILS As New YarnStockDetailsReport
    Dim RPTSTOCKREGISTERBAGS As New YarnQualityStockRegisterBagsReport
    Dim RPTQUALITYMILLSUMM As New YarnQualityMillSummReport

    Dim RPTSUMMWAREHOUSE As New YarnQualitySummWareHouseReport
    Dim RPTDETAILSWAREHOUSE As New YarnStockDetailsWareHouseReport
    Dim RPTDOWAREHOUSE As New YarnStockDOWareHouse


    'WARPER YARN STOCK REPORTS
    Dim RPTWARPERDTLS As New WarperYarnStockDetailsReport
    Dim RPTWARPERROLLDTLS As New WarperYarnStockDetailsReport
    Dim RPTWARPERSUMM As New WarperYarnStockSummReport
    Dim RPTADDASUMM As New WarperAddaSummReport
    Dim RPTWARPERMILLSUMM As New WarperYarnStockMillSummReport


    'SIZER YARN STOCK REPORTS
    Dim RPTSIZERDTLS As New SizerYarnStockDetailsReport
    Dim RPTSIZERBEAMSUMM As New SizerYarnStockBeamSummReport
    Dim RPTSIZERBEAMDTLS As New SizerYarnStockBeamDetailsReport
    Dim RPTSIZERSUMM As New SizerYarnStockSummReport

    'WEAVER YARN STOCK REPORTS
    Dim RPTWEAVERDTLS As New WeaverYarnStockDetailsReport
    Dim RPTWEAVERGREYDTLS As New WeaverYarnStockGreyDetailsReport
    Dim RPTWEAVERSUMM As New WeaverYarnStockSummReport
    Dim RPTWEAVERQUALITYSUMM As New WeaverQualityYarnStockSummReport

    'MACHINE YARN STOCK REPORTS
    Dim RPTMACDTLS As New MachineYarnStockDetailsReport
    Dim RPTMACSUMM As New MachineYarnStockSummReport
    Dim RPTMACQUALITYSUMM As New MachineQualityYarnStockSummReport

    'DYEING YARN STOCK REPORTS
    Dim RPTDYEINGDTLS As New DyeingYarnStockDetailsReport
    Dim RPTDYEINGSUMM As New DyeingYarnStockSummReport
    Dim RPTDYEINGQUALITYSUMM As New DyeingQualityYarnStockSummReport



    Dim RPTOPCLO As New YarnLedgerOnlyOpCloReport
    Dim RPTSTOCKINHAND As New YarnStockInHandReport
    Dim RPTSTOCKINHANDDTLS As New YarnStockRegisterReports

    Private Sub YarnLedgerDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

            If FRMSTRING = "DETAILS" Then
                crTables = RPTDETAILS.Database.Tables
            ElseIf FRMSTRING = "SUMMARY" Then
                crTables = RPTSUMM.Database.Tables
            ElseIf FRMSTRING = "SHADESUMMARY" Then
                crTables = RPTSHADESUMM.Database.Tables
            ElseIf FRMSTRING = "STOCKREGISTER" Then
                crTables = RPTSTOCKREGISTERBAGS.Database.Tables
            ElseIf FRMSTRING = "QUALITYMILLSUMM" Then
                crTables = RPTQUALITYMILLSUMM.Database.Tables


            ElseIf FRMSTRING = "DETAILSWAREHOUSE" Then
                crTables = RPTDETAILSWAREHOUSE.Database.Tables
            ElseIf FRMSTRING = "SUMMARYWAREHOUSE" Then
                crTables = RPTSUMMWAREHOUSE.Database.Tables
            ElseIf FRMSTRING = "DOWAREHOUSE" Then
                crTables = RPTDOWAREHOUSE.Database.Tables


            ElseIf FRMSTRING = "WARPERDTLS" Then
                crTables = RPTWARPERDTLS.Database.Tables
            ElseIf FRMSTRING = "WARPERBEAMDTLS" Then
                crTables = RPTWARPERROLLDTLS.Database.Tables
            ElseIf FRMSTRING = "WARPERSUMM" Then
                crTables = RPTWARPERSUMM.Database.Tables
            ElseIf FRMSTRING = "ADDASUMMARY" Then
                crTables = RPTADDASUMM.Database.Tables
            ElseIf FRMSTRING = "WARPERMILLSUMM" Then
                crTables = RPTWARPERMILLSUMM.Database.Tables


            ElseIf FRMSTRING = "SIZERDTLS" Then
                crTables = RPTSIZERDTLS.Database.Tables
            ElseIf FRMSTRING = "SIZERBEAMSUMM" Then
                crTables = RPTSIZERBEAMSUMM.Database.Tables
            ElseIf FRMSTRING = "SIZERBEAMDTLS" Then
                crTables = RPTSIZERBEAMDTLS.Database.Tables
            ElseIf FRMSTRING = "SIZERSUMM" Then
                crTables = RPTSIZERSUMM.Database.Tables



            ElseIf FRMSTRING = "WEAVERDTLS" Then
                crTables = RPTWEAVERDTLS.Database.Tables
            ElseIf FRMSTRING = "WEAVERGREYDTLS" Then
                crTables = RPTWEAVERGREYDTLS.Database.Tables
            ElseIf FRMSTRING = "WEAVERSUMM" Then
                crTables = RPTWEAVERSUMM.Database.Tables
            ElseIf FRMSTRING = "WEAVERQUALITYSUMM" Then
                crTables = RPTWEAVERQUALITYSUMM.Database.Tables


            ElseIf FRMSTRING = "MACDTLS" Then
                crTables = RPTMACDTLS.Database.Tables
            ElseIf FRMSTRING = "MACSUMM" Then
                crTables = RPTMACSUMM.Database.Tables
            ElseIf FRMSTRING = "MACQUALITYSUMM" Then
                crTables = RPTMACQUALITYSUMM.Database.Tables


            ElseIf FRMSTRING = "DYEINGDTLS" Then
                crTables = RPTDYEINGDTLS.Database.Tables
            ElseIf FRMSTRING = "DYEINGSUMM" Then
                crTables = RPTDYEINGSUMM.Database.Tables
            ElseIf FRMSTRING = "DYEINGQUALITYSUMM" Then
                crTables = RPTDYEINGQUALITYSUMM.Database.Tables


            ElseIf FRMSTRING = "OPCLO" Then
                crTables = RPTOPCLO.Database.Tables
            ElseIf FRMSTRING = "STOCKINHAND" Then
                crTables = RPTSTOCKINHAND.Database.Tables
            ElseIf FRMSTRING = "STOCKINHANDDTLS" Then
                crTables = RPTSTOCKINHANDDTLS.Database.Tables
            End If


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "DETAILS" Then
                RPTDETAILS.DataDefinition.FormulaFields("PERIOD").Text = "' YARN STOCK DETAILS - " & PERIOD & "'"
                RPTDETAILS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTDETAILS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTDETAILS
            ElseIf FRMSTRING = "SUMMARY" Then
                RPTSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' YARN QUALITY SUMMARY - " & PERIOD & "'"
                RPTSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTSUMM
            ElseIf FRMSTRING = "SHADESUMMARY" Then
                RPTSHADESUMM.DataDefinition.FormulaFields("PERIOD").Text = "' YARN QUALITY - SHADE WISE SUMMARY - " & PERIOD & "'"
                RPTSHADESUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTSHADESUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTSHADESUMM
            ElseIf FRMSTRING = "STOCKREGISTER" Then
                RPTSTOCKREGISTERBAGS.DataDefinition.FormulaFields("PERIOD").Text = "' YARN STOCK REGISTER - " & PERIOD & "'"
                RPTSTOCKREGISTERBAGS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTSTOCKREGISTERBAGS.Subreports(0).DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTSTOCKREGISTERBAGS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTSTOCKREGISTERBAGS
            ElseIf FRMSTRING = "QUALITYMILLSUMM" Then
                RPTQUALITYMILLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' YARN STOCK REGISTER - " & PERIOD & "'"
                RPTQUALITYMILLSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTQUALITYMILLSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTQUALITYMILLSUMM


            ElseIf FRMSTRING = "DETAILSWAREHOUSE" Then
                RPTDETAILSWAREHOUSE.DataDefinition.FormulaFields("PERIOD").Text = "' YARN STOCK DETAILS WAREHOUSE - " & PERIOD & "'"
                RPTDETAILSWAREHOUSE.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTDETAILSWAREHOUSE.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTDETAILSWAREHOUSE
            ElseIf FRMSTRING = "SUMMARYWAREHOUSE" Then
                RPTSUMMWAREHOUSE.DataDefinition.FormulaFields("PERIOD").Text = "' YARN QUALITY SUMMARY WAREHOUSE - " & PERIOD & "'"
                RPTSUMMWAREHOUSE.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTSUMMWAREHOUSE.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTSUMMWAREHOUSE
            ElseIf FRMSTRING = "DOWAREHOUSE" Then
                RPTDOWAREHOUSE.DataDefinition.FormulaFields("PERIOD").Text = "' DO WISE STOCK WAREHOUSE - " & PERIOD & "'"
                CRPO.ReportSource = RPTDOWAREHOUSE


            ElseIf FRMSTRING = "WARPERDTLS" Then
                RPTWARPERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' WARPER YARN LEDGER DETAILS - " & PERIOD & "'"
                RPTWARPERDTLS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTWARPERDTLS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTWARPERDTLS
            ElseIf FRMSTRING = "WARPERBEAMDTLS" Then
                RPTWARPERROLLDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'WARPER YARN LEDGER BEAM DETAILS - " & PERIOD & "'"
                RPTWARPERROLLDTLS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTWARPERROLLDTLS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTWARPERROLLDTLS
            ElseIf FRMSTRING = "WARPERSUMM" Then
                RPTWARPERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' YARN LEDGER SUMMARY - " & PERIOD & "'"
                RPTWARPERSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTWARPERSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTWARPERSUMM
            ElseIf FRMSTRING = "ADDASUMMARY" Then
                RPTADDASUMM.DataDefinition.FormulaFields("PERIOD").Text = "' ADDA WISE SUMMARY - " & PERIOD & "'"
                RPTADDASUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTADDASUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTADDASUMM
            ElseIf FRMSTRING = "WARPERMILLSUMM" Then
                RPTWARPERMILLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' MILL WISE YARN LEDGER SUMMARY - " & PERIOD & "'"
                RPTWARPERMILLSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTWARPERMILLSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTWARPERMILLSUMM



            ElseIf FRMSTRING = "SIZERDTLS" Then
                RPTSIZERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' SIZER YARN LEDGER DETAILS - " & PERIOD & "'"
                RPTSIZERDTLS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTSIZERDTLS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTSIZERDTLS
            ElseIf FRMSTRING = "SIZERBEAMSUMM" Then
                RPTSIZERBEAMSUMM.DataDefinition.FormulaFields("PERIOD").Text = "'SIZER YARN LEDGER BEAM SUMMARY - " & PERIOD & "'"
                RPTSIZERBEAMSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTSIZERBEAMSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTSIZERBEAMSUMM
            ElseIf FRMSTRING = "SIZERBEAMDTLS" Then
                RPTSIZERBEAMDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'SIZER YARN LEDGER BEAM DETAILS - " & PERIOD & "'"
                RPTSIZERBEAMDTLS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTSIZERBEAMDTLS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTSIZERBEAMDTLS
            ElseIf FRMSTRING = "SIZERSUMM" Then
                RPTSIZERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' YARN LEDGER SUMMARY - " & PERIOD & "'"
                RPTSIZERSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTSIZERSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTSIZERSUMM



            ElseIf FRMSTRING = "WEAVERDTLS" Then
                RPTWEAVERDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' WEAVER YARN LEDGER DETAILS - " & PERIOD & "'"
                RPTWEAVERDTLS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTWEAVERDTLS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTWEAVERDTLS
            ElseIf FRMSTRING = "WEAVERGREYDTLS" Then
                RPTWEAVERGREYDTLS.DataDefinition.FormulaFields("PERIOD").Text = "'WEAVER YARN LEDGER GREY DETAILS - " & PERIOD & "'"
                RPTWEAVERGREYDTLS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTWEAVERGREYDTLS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTWEAVERGREYDTLS
            ElseIf FRMSTRING = "WEAVERSUMM" Then
                RPTWEAVERSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' YARN LEDGER SUMMARY - " & PERIOD & "'"
                RPTWEAVERSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTWEAVERSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTWEAVERSUMM
            ElseIf FRMSTRING = "WEAVERQUALITYSUMM" Then
                RPTWEAVERQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' WEAVER WISE YARN SUMMARY - " & PERIOD & "'"
                RPTWEAVERQUALITYSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTWEAVERQUALITYSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTWEAVERQUALITYSUMM



            ElseIf FRMSTRING = "MACDTLS" Then
                RPTMACDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' MACHINE WISE YARN LEDGER DETAILS - " & PERIOD & "'"
                RPTMACDTLS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTMACDTLS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTMACDTLS
            ElseIf FRMSTRING = "MACSUMM" Then
                RPTMACSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' YARN LEDGER SUMMARY - " & PERIOD & "'"
                RPTMACSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTMACSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTMACSUMM
            ElseIf FRMSTRING = "MACQUALITYSUMM" Then
                RPTMACQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' MACHINE WISE YARN SUMMARY - " & PERIOD & "'"
                RPTMACQUALITYSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTMACQUALITYSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTMACQUALITYSUMM


            ElseIf FRMSTRING = "DYEINGDTLS" Then
                RPTDYEINGDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' DYEING YARN LEDGER DETAILS - " & PERIOD & "'"
                RPTDYEINGDTLS.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTDYEINGDTLS.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTDYEINGDTLS
            ElseIf FRMSTRING = "DYEINGSUMM" Then
                RPTDYEINGSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' YARN LEDGER SUMMARY - " & PERIOD & "'"
                RPTDYEINGSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTDYEINGSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTDYEINGSUMM
            ElseIf FRMSTRING = "DYEINGQUALITYSUMM" Then
                RPTDYEINGQUALITYSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' DYEING WISE YARN SUMMARY - " & PERIOD & "'"
                RPTDYEINGQUALITYSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTDYEINGQUALITYSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTDYEINGQUALITYSUMM



            ElseIf FRMSTRING = "OPCLO" Then
                RPTOPCLO.DataDefinition.FormulaFields("PERIOD").Text = "' YARN SUMMARY - " & PERIOD & "'"
                CRPO.ReportSource = RPTOPCLO
            ElseIf FRMSTRING = "STOCKINHAND" Then
                RPTSTOCKINHAND.DataDefinition.FormulaFields("PERIOD").Text = "' YARN STOCK IN HAND - " & PERIOD & "'"
                RPTSTOCKINHAND.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTSTOCKINHAND.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                CRPO.ReportSource = RPTSTOCKINHAND
            ElseIf FRMSTRING = "STOCKINHANDDTLS" Then
                RPTSTOCKINHANDDTLS.DataDefinition.FormulaFields("PERIOD").Text = "' YARN STOCK IN DETAILS - " & PERIOD & "'"
                CRPO.ReportSource = RPTSTOCKINHANDDTLS
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

            If FRMSTRING = "ALLDATA" Then
                expo = RPTDETAILS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDETAILS.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class