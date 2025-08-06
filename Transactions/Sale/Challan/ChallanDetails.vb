
Imports BL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Views.Grid

Public Class ChallanDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim DTMAIL As New DataTable
    Dim DTWHATSAPP As New DataTable


    Private Sub CMDEXIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub ChallanDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.Alt = True And e.KeyCode = Keys.R Then
            Call TOOLREFRESH_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.P Then
            Call TOOLEXCEL_Click(sender, e)
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub ChallanDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GDN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If


            DTMAIL.Columns.Add("CHALLANNO")
            DTMAIL.Columns.Add("REGID")
            DTMAIL.Columns.Add("REGNAME")
            DTMAIL.Columns.Add("PRINTINITIALS")
            DTMAIL.Columns.Add("DATE")
            DTMAIL.Columns.Add("NAME")
            DTMAIL.Columns.Add("PARTYEMAILID")
            DTMAIL.Columns.Add("AGENTNAME")
            DTMAIL.Columns.Add("AGENTEMAILID")
            DTMAIL.Columns.Add("GRANDTOTAL")
            DTMAIL.Columns.Add("SUBJECT")
            DTMAIL.Columns.Add("ATTACHMENT")
            DTMAIL.Columns.Add("FILENAME")

            DTWHATSAPP.Columns.Add("CHALLANNO")
            DTWHATSAPP.Columns.Add("REGID")
            DTWHATSAPP.Columns.Add("REGNAME")
            DTWHATSAPP.Columns.Add("PRINTINITIALS")
            DTWHATSAPP.Columns.Add("DATE")
            DTWHATSAPP.Columns.Add("NAME")
            DTWHATSAPP.Columns.Add("PARTYWHATSAPP")
            DTWHATSAPP.Columns.Add("AGENTNAME")
            DTWHATSAPP.Columns.Add("AGENTWHATSAPP")
            DTWHATSAPP.Columns.Add("GRANDTOTAL")
            DTWHATSAPP.Columns.Add("SUBJECT")
            DTWHATSAPP.Columns.Add("ATTACHMENT")
            DTWHATSAPP.Columns.Add("FILENAME")

            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub showform(ByVal EDITVAL As Boolean, ByVal CHALLANNO As Integer)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJCHALLAN As New Challan
            OBJCHALLAN.EDIT = EDITVAL
            OBJCHALLAN.MdiParent = MDIMain
            OBJCHALLAN.TEMPCHALLANNO = CHALLANNO
            OBJCHALLAN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim OBJCMN As New ClsCommon
            Dim dttable As DataTable = OBJCMN.Execute_Any_String(" SELECT        CAST(0 AS BIT) AS CHK, CHALLANMASTER.CHALLAN_NO AS CHALLANNO, CHALLANMASTER.CHALLAN_DATE AS CHALLANDATE, ISNULL(CHALLANMASTER.CHALLAN_ORDERNO, 0) AS ORDERNO, ISNULL(CHALLANMASTER.CHALLAN_ORDERSRNO, 0) AS ORDERSRNO, ISNULL(CHALLANMASTER.CHALLAN_ORDERTYPE, '') AS ORDERTYPE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(BROKER.Acc_cmpname, '') AS BROKER, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, ISNULL(CHALLANMASTER.CHALLAN_VEHICLENO, '') AS VEHICLENO, ISNULL(DELIVERYAT.Acc_cmpname, '') AS DELIVERYAT, ISNULL(CHALLANMASTER.CHALLAN_FORDYEING, 0) AS FORDYEING, ISNULL(CHALLANMASTER.CHALLAN_CUTPACK, 0) AS CUTPACK, ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0) AS LOTNO, ISNULL(CHALLANMASTER.CHALLAN_GRDONO, 0) AS GRDONO, ISNULL(CHALLANMASTER.CHALLAN_GRPCS, 0) AS GRPCS, ISNULL(CHALLANMASTER.CHALLAN_GRMTRS, 0) AS GRMTRS, ISNULL(CHALLANMASTER.CHALLAN_SHORTAGE, 0) AS SHORTAGE, ISNULL(CHALLANMASTER.CHALLAN_REMARKS, '') AS REMARKS, CHALLANMASTER.CHALLAN_DONE AS DONE, ISNULL(CHALLANMASTER.CHALLAN_TOTALBALES, 0) AS TOTALBALES, ISNULL(CHALLANMASTER.CHALLAN_TOTALTAKA, 0) AS TOTALTAKA, ISNULL(CHALLANMASTER.CHALLAN_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(CHALLANMASTER.CHALLAN_TOTALTP, 0) AS TOTALTP, ISNULL(CHALLANMASTER.CHALLAN_NOGR, 0) AS NOGR, ISNULL(CHALLANMASTER.CHALLAN_SERIES, 0) AS SERIES, ISNULL(CHALLANMASTER.CHALLAN_TYPE, 'GREY') AS TYPE, ISNULL(CHALLANMASTER.CHALLAN_FOLD, '') AS FOLD, ISNULL(ACOFLEDGERS.Acc_cmpname, '') AS ACOF, ISNULL(CHALLANMASTER.CHALLAN_BALEFROM, 0) AS BALEFROM, ISNULL(CHALLANMASTER.CHALLAN_BALETO, 0) AS BALETO, ISNULL(LEDGERS.Acc_mobile, '') AS PARTYWHATSAPP, ISNULL(AGENTLEDGERS.Acc_mobile, '') AS AGENTWHATSAPP, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT FROM            CHALLANMASTER INNER JOIN LEDGERS ON CHALLANMASTER.CHALLAN_LEDGERID = LEDGERS.Acc_id INNER JOIN GODOWNMASTER ON CHALLANMASTER.CHALLAN_GODOWNID = GODOWNMASTER.GODOWN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON CHALLANMASTER.CHALLAN_BROKERID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS DELIVERYAT ON CHALLANMASTER.CHALLAN_DELIVERYID = DELIVERYAT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON CHALLANMASTER.CHALLAN_BROKERID = BROKER.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON CHALLANMASTER.CHALLAN_TRANSID = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS ACOFLEDGERS ON CHALLANMASTER.CHALLAN_ACOFID = ACOFLEDGERS.Acc_id  WHERE CHALLANMASTER.CHALLAN_YEARID = " & YearId & " ORDER BY CHALLANMASTER.CHALLAN_NO ", "", "")
            'Dim dttable As DataTable = OBJCMN.Execute_Any_String(" SELECT        CAST(0 AS BIT) AS CHK, CHALLANMASTER.CHALLAN_NO AS CHALLANNO, CHALLANMASTER.CHALLAN_DATE AS CHALLANDATE, ISNULL(CHALLANMASTER.CHALLAN_ORDERNO, 0) AS ORDERNO, ISNULL(CHALLANMASTER.CHALLAN_ORDERSRNO, 0) AS ORDERSRNO, ISNULL(CHALLANMASTER.CHALLAN_ORDERTYPE, '') AS ORDERTYPE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, ISNULL(CHALLANMASTER.CHALLAN_VEHICLENO, '') AS VEHICLENO, ISNULL(DELIVERYAT.Acc_cmpname, '') AS DELIVERYAT, ISNULL(CHALLANMASTER.CHALLAN_FORDYEING, 0) AS FORDYEING, ISNULL(CHALLANMASTER.CHALLAN_CUTPACK, 0) AS CUTPACK, ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0) AS LOTNO, ISNULL(CHALLANMASTER.CHALLAN_GRDONO, 0) AS GRDONO, ISNULL(CHALLANMASTER.CHALLAN_GRPCS, 0) AS GRPCS, ISNULL(CHALLANMASTER.CHALLAN_GRMTRS, 0) AS GRMTRS, ISNULL(CHALLANMASTER.CHALLAN_SHORTAGE, 0) AS SHORTAGE, ISNULL(CHALLANMASTER.CHALLAN_REMARKS, '') AS REMARKS, CHALLANMASTER.CHALLAN_DONE AS DONE, ISNULL(CHALLANMASTER.CHALLAN_TOTALBALES, 0) AS TOTALBALES, ISNULL(CHALLANMASTER.CHALLAN_TOTALTAKA, 0) AS TOTALTAKA, ISNULL(CHALLANMASTER.CHALLAN_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(CHALLANMASTER.CHALLAN_TOTALTP, 0) AS TOTALTP, ISNULL(CHALLANMASTER.CHALLAN_NOGR, 0) AS NOGR, ISNULL(CHALLANMASTER.CHALLAN_SERIES, 0) AS SERIES, ISNULL(CHALLANMASTER.CHALLAN_TYPE, 'GREY') AS TYPE, ISNULL(CHALLANMASTER.CHALLAN_FOLD, '') AS FOLD, ISNULL(ACOFLEDGERS.Acc_cmpname, '') AS ACOF, ISNULL(CHALLANMASTER.CHALLAN_BALEFROM, 0) AS BALEFROM, ISNULL(CHALLANMASTER.CHALLAN_BALETO, 0) AS BALETO, ISNULL(LEDGERS.Acc_mobile, '') AS PARTYWHATSAPP, ISNULL(AGENTLEDGERS.Acc_mobile, '') AS AGENTWHATSAPP, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT FROM            CHALLANMASTER INNER JOIN LEDGERS ON CHALLANMASTER.CHALLAN_LEDGERID = LEDGERS.Acc_id INNER JOIN GODOWNMASTER ON CHALLANMASTER.CHALLAN_GODOWNID = GODOWNMASTER.GODOWN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON CHALLANMASTER.CHALLAN_BROKERID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS DELIVERYAT ON CHALLANMASTER.CHALLAN_DELIVERYID = DELIVERYAT.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON CHALLANMASTER.CHALLAN_TRANSID = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS ACOFLEDGERS ON CHALLANMASTER.CHALLAN_ACOFID = ACOFLEDGERS.Acc_id   WHERE CHALLANMASTER.CHALLAN_YEARID = " & YearId & " ORDER BY CHALLANMASTER.CHALLAN_NO ", "", "")
            gridbilldetails.DataSource = dttable
            If dttable.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEDIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(True, gridbill.GetFocusedRowCellValue("CHALLANNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(True, gridbill.GetFocusedRowCellValue("CHALLANNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDADD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDADD.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TOOLREFRESH.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("DONE")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                ElseIf View.GetRowCellDisplayText(e.RowHandle, View.Columns("FORDYEING")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightGreen
                ElseIf View.GetRowCellDisplayText(e.RowHandle, View.Columns("CUTPACK")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Linen
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Challan Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim workbook As String = PATH
            If FileIO.FileSystem.FileExists(PATH) = True Then Interaction.GetObject(workbook).close(False)
            GC.Collect()

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Challan Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Challan Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDDTLS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDTLS.Click
        Try
            Dim OBJCHALLAN As New ChallanGridDetails
            OBJCHALLAN.MdiParent = MDIMain
            OBJCHALLAN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If Val(TXTFROM.Text.Trim) > 0 Or Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Challan Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Wish to Print Challan from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings
                SERVERPROP(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "PRINT")
            Else
                If MsgBox("Wish to Print Selected Challan ?", MsgBoxStyle.YesNo) = vbYes Then
                    If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings
                    CMDEDIT.Focus()
                    SERVERPROPSELECTED(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim))
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLMAIL_Click(sender As Object, e As EventArgs) Handles TOOLMAIL.Click
        Try
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Challan Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Mail Challan from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROP(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "MAIL")
                End If
            Else
                If MsgBox("Wish to Mail Selected Challan ?", MsgBoxStyle.YesNo) = vbYes Then
                    CMDEDIT.Focus()
                    SERVERPROPSELECTED(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "MAIL")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROP(ByVal FROMNO As Integer, ByVal TONO As Integer, Optional ByVal NOOFCOPIES As Integer = 1, Optional ByVal FRMSTRING As String = "PRINT")
        Try
            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList

            For I As Integer = FROMNO To TONO

                '**************** SET SERVER ************************
                Dim crParameterFieldDefinitions As ParameterFieldDefinitions
                Dim crParameterFieldDefinition As ParameterFieldDefinition
                Dim crParameterValues As New ParameterValues
                Dim crParameterDiscreteValue As New ParameterDiscreteValue

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

                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions


                'WE NEED TO CHECK WHETHER THE CHALLAN IS GREY OR YARN
                Dim OBJCMN As New ClsCommon
                Dim CHALLANTYPE As String = "GREY"
                Dim DTCHALLAN As DataTable = OBJCMN.search("CHALLAN_TYPE AS TYPE", "", " CHALLANMASTER ", " AND CHALLAN_NO = " & Val(I) & " AND CHALLAN_YEARID = " & YearId)
                If DTCHALLAN.Rows.Count > 0 Then CHALLANTYPE = DTCHALLAN.Rows(0).Item("TYPE")


                Dim OBJ As New Object
                If CHALLANTYPE = "GREY" Then

                    If MULTIYARN = True Then
                        OBJ = New ChallanReport_TakaNo
                    ElseIf ClientName = "SASHWINKUMAR" Then
                        OBJ = New ChallanReport_SASHWIN
                    Else
                        OBJ = New ChallanReport

                        'GIVE BIFFURCATION OF TAKA AND PASS IT IN FORMULA FIELDS
                        Dim TOTAL01 As Double = 0
                        Dim TOTAL02 As Double = 0
                        Dim TOTAL03 As Double = 0
                        Dim TOTAL04 As Double = 0
                        Dim TOTAL05 As Double = 0
                        Dim TOTAL06 As Double = 0
                        Dim TOTAL07 As Double = 0
                        Dim TOTAL08 As Double = 0
                        Dim TOTAL09 As Double = 0
                        Dim TOTAL10 As Double = 0
                        Dim TOTAL11 As Double = 0
                        Dim TOTAL12 As Double = 0
                        Dim TOTAL13 As Double = 0
                        Dim TOTAL14 As Double = 0
                        Dim TOTAL15 As Double = 0
                        Dim TOTAL16 As Double = 0

                        Dim DT As DataTable = OBJCMN.Execute_Any_String(" SELECT CHALLAN_MTRS AS MTRS, CHALLAN_GRIDSRNO AS GRIDSRNO FROM CHALLANMASTER_MTRS WHERE CHALLAN_NO = " & Val(Val(I)) & " AND CHALLAN_YEARID = " & YearId & " ORDER BY CHALLAN_GRIDSRNO", "", "")

                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=1 AND GRIDSRNO<=25")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL01").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=1 AND GRIDSRNO<=25"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=26 AND GRIDSRNO<=50")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL02").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=26 AND GRIDSRNO<=50"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=51 AND GRIDSRNO<=75")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL03").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=51 AND GRIDSRNO<=75"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=76 AND GRIDSRNO<=100")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL04").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=76 AND GRIDSRNO<=100"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=101 AND GRIDSRNO<=125")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL05").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=101 AND GRIDSRNO<=125"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=126 AND GRIDSRNO<=150")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL06").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=126 AND GRIDSRNO<=150"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=151 AND GRIDSRNO<=175")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL07").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=151 AND GRIDSRNO<=175"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=176 AND GRIDSRNO<=200")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL08").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=176 AND GRIDSRNO<=200"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=201 AND GRIDSRNO<=225")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL09").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=201 AND GRIDSRNO<=225"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=226 AND GRIDSRNO<=250")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL10").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=226 AND GRIDSRNO<=250"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=251 AND GRIDSRNO<=275")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL11").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=251 AND GRIDSRNO<=275"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=276 AND GRIDSRNO<=300")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL12").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=276 AND GRIDSRNO<=300"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=301 AND GRIDSRNO<=325")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL13").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=301 AND GRIDSRNO<=325"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=326 AND GRIDSRNO<=350")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL14").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=326 AND GRIDSRNO<=350"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=351 AND GRIDSRNO<=375")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL15").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=351 AND GRIDSRNO<=375"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=376 AND GRIDSRNO<=400")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL16").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=376 AND GRIDSRNO<=400"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=401 AND GRIDSRNO<=425")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL17").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=401 AND GRIDSRNO<=425"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=426 AND GRIDSRNO<=450")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL18").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=426 AND GRIDSRNO<=450"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=451 AND GRIDSRNO<=475")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL19").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=451 AND GRIDSRNO<=475"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=476 AND GRIDSRNO<=500")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL20").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=476 AND GRIDSRNO<=500"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=501 AND GRIDSRNO<=525")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL21").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=501 AND GRIDSRNO<=525"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=526 AND GRIDSRNO<=550")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL22").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=526 AND GRIDSRNO<=550"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=551 AND GRIDSRNO<=575")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL23").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=551 AND GRIDSRNO<=575"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=576 AND GRIDSRNO<=600")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL24").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=576 AND GRIDSRNO<=600"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=601 AND GRIDSRNO<=625")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL25").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=601 AND GRIDSRNO<=625"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=626 AND GRIDSRNO<=650")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL26").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=626 AND GRIDSRNO<=650"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=651 AND GRIDSRNO<=675")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL27").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=651 AND GRIDSRNO<=675"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=675 AND GRIDSRNO<=700")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL28").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=676 AND GRIDSRNO<=700"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=701 AND GRIDSRNO<=725")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL29").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=701 AND GRIDSRNO<=725"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=726 AND GRIDSRNO<=750")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL30").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=726 AND GRIDSRNO<=750"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=751 AND GRIDSRNO<=775")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL31").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=751 AND GRIDSRNO<=775"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=776 AND GRIDSRNO<=800")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL32").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=776 AND GRIDSRNO<=800"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=801 AND GRIDSRNO<=825")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL33").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=801 AND GRIDSRNO<=825"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=826 AND GRIDSRNO<=850")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL34").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=826 AND GRIDSRNO<=850"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=851 AND GRIDSRNO<=875")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL35").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=851 AND GRIDSRNO<=875"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=876 AND GRIDSRNO<=900")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL36").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=876 AND GRIDSRNO<=900"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=901 AND GRIDSRNO<=925")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL37").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=901 AND GRIDSRNO<=925"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=926 AND GRIDSRNO<=950")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL38").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=926 AND GRIDSRNO<=950"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=951 AND GRIDSRNO<=975")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL39").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=951 AND GRIDSRNO<=975"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=976 AND GRIDSRNO<=1000")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL40").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=976 AND GRIDSRNO<=1000"))

                    End If

                Else
                    OBJ = New ChallanReport_Yarn
                End If

                If ClientName = "NIRMALA" Or ClientName = "YESHA" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = 1


                crTables = OBJ.Database.Tables
                For Each crTable In crTables
                    crtableLogonInfo = crTable.LogOnInfo
                    crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                    crTable.ApplyLogOnInfo(crtableLogonInfo)
                Next

                OBJ.RecordSelectionFormula = "{CHALLANMASTER.CHALLAN_NO} = " & Val(I) & " AND {CHALLANMASTER.CHALLAN_YEARID} = " & YearId

                If FRMSTRING = "PRINT" Then
                    OBJ.PrintOptions.PrinterName = PRINTDIALOG.PrinterSettings.PrinterName
                    If CHALLANTYPE = "YARN" Then OBJ.PrintOptions.PaperSize = PaperSize.PaperA5 Else OBJ.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
                    OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
                Else
                    oDfDopt.DiskFileName = Application.StartupPath & "\CHALLAN_" & Val(I) & ".pdf"
                    expo = OBJ.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    OBJ.Export()
                    ALATTACHMENT.Add(oDfDopt.DiskFileName)
                    FILENAME.Add("CHALLAN_" & Val(I) & ".pdf")

                End If
            Next

            If FRMSTRING = "MAIL" Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "Challan"
                OBJMAIL.ShowDialog()
            End If


            If FRMSTRING = "WHATSAPP" = True Then
                Dim OBJWHATSAPP As New SendWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.ShowDialog()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPSELECTED(ByVal fromno As Integer, ByVal tono As Integer, Optional ByVal NOOFCOPIES As Integer = 1, Optional ByVal FRMSTRING As String = "PRINT")
        Try

            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            DTMAIL.Rows.Clear()
            DTWHATSAPP.Rows.Clear()

            'Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To gridbill.RowCount - 1
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                If ROW("CHK") = True Then

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

                    Dim expo As New ExportOptions
                    Dim oDfDopt As New DiskFileDestinationOptions


                    Dim OBJ As New Object
                    If ROW("TYPE") = "GREY" Then

                        If MULTIYARN = True Then
                            OBJ = New ChallanReport_TakaNo
                        ElseIf ClientName = "SASHWINKUMAR" Then
                            OBJ = New ChallanReport_SASHWIN
                        Else
                            OBJ = New ChallanReport

                            'GIVE BIFFURCATION OF TAKA AND PASS IT IN FORMULA FIELDS
                            Dim TOTAL01 As Double = 0
                            Dim TOTAL02 As Double = 0
                            Dim TOTAL03 As Double = 0
                            Dim TOTAL04 As Double = 0
                            Dim TOTAL05 As Double = 0
                            Dim TOTAL06 As Double = 0
                            Dim TOTAL07 As Double = 0
                            Dim TOTAL08 As Double = 0
                            Dim TOTAL09 As Double = 0
                            Dim TOTAL10 As Double = 0
                            Dim TOTAL11 As Double = 0
                            Dim TOTAL12 As Double = 0
                            Dim TOTAL13 As Double = 0
                            Dim TOTAL14 As Double = 0
                            Dim TOTAL15 As Double = 0
                            Dim TOTAL16 As Double = 0

                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.Execute_Any_String(" SELECT CHALLAN_MTRS AS MTRS, CHALLAN_GRIDSRNO AS GRIDSRNO FROM CHALLANMASTER_MTRS WHERE CHALLAN_NO = " & Val(ROW("CHALLANNO")) & " AND CHALLAN_YEARID = " & YearId & " ORDER BY CHALLAN_GRIDSRNO", "", "")

                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=1 AND GRIDSRNO<=25")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL01").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=1 AND GRIDSRNO<=25"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=26 AND GRIDSRNO<=50")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL02").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=26 AND GRIDSRNO<=50"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=51 AND GRIDSRNO<=75")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL03").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=51 AND GRIDSRNO<=75"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=76 AND GRIDSRNO<=100")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL04").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=76 AND GRIDSRNO<=100"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=101 AND GRIDSRNO<=125")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL05").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=101 AND GRIDSRNO<=125"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=126 AND GRIDSRNO<=150")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL06").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=126 AND GRIDSRNO<=150"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=151 AND GRIDSRNO<=175")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL07").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=151 AND GRIDSRNO<=175"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=176 AND GRIDSRNO<=200")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL08").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=176 AND GRIDSRNO<=200"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=201 AND GRIDSRNO<=225")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL09").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=201 AND GRIDSRNO<=225"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=226 AND GRIDSRNO<=250")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL10").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=226 AND GRIDSRNO<=250"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=251 AND GRIDSRNO<=275")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL11").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=251 AND GRIDSRNO<=275"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=276 AND GRIDSRNO<=300")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL12").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=276 AND GRIDSRNO<=300"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=301 AND GRIDSRNO<=325")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL13").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=301 AND GRIDSRNO<=325"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=326 AND GRIDSRNO<=350")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL14").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=326 AND GRIDSRNO<=350"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=351 AND GRIDSRNO<=375")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL15").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=351 AND GRIDSRNO<=375"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=376 AND GRIDSRNO<=400")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL16").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=376 AND GRIDSRNO<=400"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=401 AND GRIDSRNO<=425")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL17").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=401 AND GRIDSRNO<=425"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=426 AND GRIDSRNO<=450")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL18").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=426 AND GRIDSRNO<=450"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=451 AND GRIDSRNO<=475")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL19").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=451 AND GRIDSRNO<=475"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=476 AND GRIDSRNO<=500")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL20").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=476 AND GRIDSRNO<=500"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=501 AND GRIDSRNO<=525")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL21").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=501 AND GRIDSRNO<=525"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=526 AND GRIDSRNO<=550")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL22").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=526 AND GRIDSRNO<=550"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=551 AND GRIDSRNO<=575")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL23").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=551 AND GRIDSRNO<=575"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=576 AND GRIDSRNO<=600")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL24").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=576 AND GRIDSRNO<=600"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=601 AND GRIDSRNO<=625")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL25").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=601 AND GRIDSRNO<=625"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=626 AND GRIDSRNO<=650")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL26").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=626 AND GRIDSRNO<=650"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=651 AND GRIDSRNO<=675")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL27").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=651 AND GRIDSRNO<=675"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=675 AND GRIDSRNO<=700")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL28").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=676 AND GRIDSRNO<=700"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=701 AND GRIDSRNO<=725")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL29").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=701 AND GRIDSRNO<=725"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=726 AND GRIDSRNO<=750")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL30").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=726 AND GRIDSRNO<=750"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=751 AND GRIDSRNO<=775")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL31").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=751 AND GRIDSRNO<=775"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=776 AND GRIDSRNO<=800")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL32").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=776 AND GRIDSRNO<=800"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=801 AND GRIDSRNO<=825")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL33").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=801 AND GRIDSRNO<=825"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=826 AND GRIDSRNO<=850")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL34").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=826 AND GRIDSRNO<=850"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=851 AND GRIDSRNO<=875")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL35").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=851 AND GRIDSRNO<=875"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=876 AND GRIDSRNO<=900")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL36").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=876 AND GRIDSRNO<=900"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=901 AND GRIDSRNO<=925")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL37").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=901 AND GRIDSRNO<=925"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=926 AND GRIDSRNO<=950")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL38").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=926 AND GRIDSRNO<=950"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=951 AND GRIDSRNO<=975")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL39").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=951 AND GRIDSRNO<=975"))
                            If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=976 AND GRIDSRNO<=1000")) = False Then OBJ.DataDefinition.FormulaFields("TOTAL40").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=976 AND GRIDSRNO<=1000"))

                        End If

                    Else
                        OBJ = New ChallanReport_Yarn
                    End If

                    If ClientName = "NIRMALA" Or ClientName = "YESHA" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = 1

                    crTables = OBJ.Database.Tables
                    For Each crTable In crTables
                        crtableLogonInfo = crTable.LogOnInfo
                        crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                        crTable.ApplyLogOnInfo(crtableLogonInfo)
                    Next

                    OBJ.RecordSelectionFormula = "{CHALLANMASTER.CHALLAN_NO} = " & Val(ROW("CHALLANNO")) & " AND {CHALLANMASTER.CHALLAN_YEARID} = " & YearId

                    If FRMSTRING = "PRINT" Then
                        OBJ.PrintOptions.PrinterName = PRINTDIALOG.PrinterSettings.PrinterName
                        If ROW("TYPE") = "YARN" Then OBJ.PrintOptions.PaperSize = PaperSize.PaperA5 Else OBJ.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
                        OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
                    Else
                        oDfDopt.DiskFileName = Application.StartupPath & "\CHALLAN_" & ROW("CHALLANNO") & ".pdf"
                        expo = OBJ.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        OBJ.Export()
                        ALATTACHMENT.Add(oDfDopt.DiskFileName)
                        FILENAME.Add("CHALLAN_" & ROW("CHALLANNO") & ".pdf")

                        'ADDINT IN DTEMAIL
                        'DTMAIL.Rows.Add(ROW("SRNO"), 0, "", ROW("SRNO"), ROW("DATE"), ROW("CMPNAME"), ROW("PARTYEMAIL"), ROW("AGENT"), ROW("AGENTEMAIL"), 0, UCase(CmpName) & " - Challan No. " & ROW("SRNO") & " Dated " & ROW("DATE"), oDfDopt.DiskFileName, ROW("CMPNAME") & "GDN_" & ROW("SRNO") & ".pdf")

                        'ADDING IN DTWHATSAPP
                        DTWHATSAPP.Rows.Add(ROW("CHALLANNO"), 0, "", ROW("CHALLANNO"), "", ROW("NAME"), ROW("PARTYWHATSAPP"), ROW("BROKER"), ROW("AGENTWHATSAPP"), 0, UCase(CmpName) & " - Challan No. " & ROW("CHALLANNO"), oDfDopt.DiskFileName, ROW("NAME") & "GDN_" & ROW("CHALLANNO") & ".pdf")
                        Dim OBJCMN As New ClsCommon
                        OBJCMN.Execute_Any_String("UPDATE CHALLANMASTER SET CHALLAN_SENDWHATSAPP = 1 FROM CHALLANMASTER  WHERE CHALLAN_NO = " & Val(ROW("CHALLANNO")) & "  AND CHALLAN_YEARID = " & YearId, "", "")

                        'DTWHATSAPP.Rows.Add(ROW("CHALLAN_" & Val(ROW("CHALLANNO")) & ".pdf"))



                    End If

                End If
            Next

            If FRMSTRING = "MAIL" Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "Challan"
                OBJMAIL.ShowDialog()
            End If


            If FRMSTRING = "WHATSAPP" = True Then
                If DTWHATSAPP.Rows.Count = 0 Then Exit Sub
                Dim OBJWHATSAPP As New SendMultipleWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.DT = DTWHATSAPP
                OBJWHATSAPP.ShowDialog()
                'Dim OBJWHATSAPP As New SendWhatsapp
                'OBJWHATSAPP.PATH = ALATTACHMENT
                'OBJWHATSAPP.FILENAME = FILENAME
                'OBJWHATSAPP.ShowDialog()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Challan Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Challan from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROP(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "WHATSAPP")
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected Challan ?", MsgBoxStyle.YesNo) = vbYes Then
                    cmdok.Focus()
                    SERVERPROPSELECTED(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), Val(TXTCOPIES.Text.Trim), "WHATSAPP")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTFROM.Validated
        If Val(TXTFROM.Text.Trim) <> 0 Then TXTTO.Focus()
    End Sub

    Private Sub TXTCOPIES_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCOPIES.Validating
        If Val(TXTCOPIES.Text.Trim) <= 0 Then TXTCOPIES.Text = 1
    End Sub

    Private Sub gridbilldetails_DoubleClick(sender As Object, e As EventArgs) Handles gridbilldetails.DoubleClick
        Try
            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(True, gridbill.GetFocusedRowCellValue("CHALLANNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class