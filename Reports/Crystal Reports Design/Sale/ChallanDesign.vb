
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports BL
Imports System.IO

Public Class ChallanDesign

    Dim RPTPACKINGSLIP As New PackingSlipReport
    Dim RPTCHALLAN_SASHWIN As New ChallanReport_SASHWIN
    Dim RPTCHALLANFINISH As New ChallanReport_Finished
    Dim RPTCHALLANYARN As New ChallanReport_Yarn
    Dim RPTCHALLAN_TAKANO As New ChallanReport_TakaNo
    Dim RPTCHALLAN As New ChallanReport

    Public DIRECTPRINT As Boolean = False
    Public PSNO As Integer
    Public NOOFCOPIES As Integer = 1
    Public PRINTSETTING As Object = Nothing

    Public CHALLANNO As Integer = 0
    Public WHERECLAUSE, FRMSTRING, SCREENTYPE As String


    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Dim tempattachment As String
    Public PARTYNAME As String
    Public AGENTNAME As String
    Public BILLNO As Integer
    Public REGNAME As String

    Private Sub ChallanDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ChallanDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If DIRECTPRINT = True Then
                PRINTDIRECTLYTOPRINTER()
                Exit Sub
            End If


            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

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


            If FRMSTRING = "PACKINGSLIP" Then crTables = RPTPACKINGSLIP.Database.Tables

            If FRMSTRING = "CHALLAN" Then
                If SCREENTYPE = "GREY" Then
                    If MULTIYARN = True Then
                        crTables = RPTCHALLAN_TAKANO.Database.Tables
                    ElseIf ClientName = "SASHWINKUMAR" Then
                        crTables = RPTCHALLAN_SASHWIN.Database.Tables
                    Else
                        crTables = RPTCHALLAN.Database.Tables
                    End If
                Else
                    crTables = RPTCHALLANYARN.Database.Tables
                End If
            End If

            If FRMSTRING = "CHALLANFINISH" Then crTables = RPTCHALLANFINISH.Database.Tables


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            CRPO.SelectionFormula = WHERECLAUSE


            If FRMSTRING = "PACKINGSLIP" Then
                CRPO.ReportSource = RPTPACKINGSLIP
            ElseIf FRMSTRING = "CHALLANFINISH" Then
                CRPO.ReportSource = RPTCHALLANFINISH
            ElseIf FRMSTRING = "CHALLAN" Then
                If SCREENTYPE = "GREY" Then
                    If MULTIYARN = True Then
                        CRPO.ReportSource = RPTCHALLAN_TAKANO
                    ElseIf ClientName = "SASHWINKUMAR" Then
                        CRPO.ReportSource = RPTCHALLAN_SASHWIN
                    Else
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
                        Dim DT As DataTable = OBJCMN.Execute_Any_String(" SELECT CHALLAN_MTRS AS MTRS, CHALLAN_GRIDSRNO AS GRIDSRNO FROM CHALLANMASTER_MTRS WHERE CHALLAN_NO = " & CHALLANNO & " AND CHALLAN_YEARID = " & YearId & " ORDER BY CHALLAN_GRIDSRNO", "", "")
                        'RPTCHALLAN.DataDefinition.FormulaFields("TOTAL01").Text = Val()

                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=1 AND GRIDSRNO<=25")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL01").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=1 AND GRIDSRNO<=25"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=26 AND GRIDSRNO<=50")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL02").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=26 AND GRIDSRNO<=50"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=51 AND GRIDSRNO<=75")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL03").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=51 AND GRIDSRNO<=75"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=76 AND GRIDSRNO<=100")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL04").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=76 AND GRIDSRNO<=100"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=101 AND GRIDSRNO<=125")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL05").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=101 AND GRIDSRNO<=125"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=126 AND GRIDSRNO<=150")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL06").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=126 AND GRIDSRNO<=150"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=151 AND GRIDSRNO<=175")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL07").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=151 AND GRIDSRNO<=175"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=176 AND GRIDSRNO<=200")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL08").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=176 AND GRIDSRNO<=200"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=201 AND GRIDSRNO<=225")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL09").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=201 AND GRIDSRNO<=225"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=226 AND GRIDSRNO<=250")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL10").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=226 AND GRIDSRNO<=250"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=251 AND GRIDSRNO<=275")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL11").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=251 AND GRIDSRNO<=275"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=276 AND GRIDSRNO<=300")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL12").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=276 AND GRIDSRNO<=300"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=301 AND GRIDSRNO<=325")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL13").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=301 AND GRIDSRNO<=325"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=326 AND GRIDSRNO<=350")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL14").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=326 AND GRIDSRNO<=350"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=351 AND GRIDSRNO<=375")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL15").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=351 AND GRIDSRNO<=375"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=376 AND GRIDSRNO<=400")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL16").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=376 AND GRIDSRNO<=400"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=401 AND GRIDSRNO<=425")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL17").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=401 AND GRIDSRNO<=425"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=426 AND GRIDSRNO<=450")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL18").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=426 AND GRIDSRNO<=450"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=451 AND GRIDSRNO<=475")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL19").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=451 AND GRIDSRNO<=475"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=476 AND GRIDSRNO<=500")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL20").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=476 AND GRIDSRNO<=500"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=501 AND GRIDSRNO<=525")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL21").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=501 AND GRIDSRNO<=525"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=526 AND GRIDSRNO<=550")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL22").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=526 AND GRIDSRNO<=550"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=551 AND GRIDSRNO<=575")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL23").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=551 AND GRIDSRNO<=575"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=576 AND GRIDSRNO<=600")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL24").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=576 AND GRIDSRNO<=600"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=601 AND GRIDSRNO<=625")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL25").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=601 AND GRIDSRNO<=625"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=626 AND GRIDSRNO<=650")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL26").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=626 AND GRIDSRNO<=650"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=651 AND GRIDSRNO<=675")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL27").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=651 AND GRIDSRNO<=675"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=675 AND GRIDSRNO<=700")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL28").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=676 AND GRIDSRNO<=700"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=701 AND GRIDSRNO<=725")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL29").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=701 AND GRIDSRNO<=725"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=726 AND GRIDSRNO<=750")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL30").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=726 AND GRIDSRNO<=750"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=751 AND GRIDSRNO<=775")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL31").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=751 AND GRIDSRNO<=775"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=776 AND GRIDSRNO<=800")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL32").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=776 AND GRIDSRNO<=800"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=801 AND GRIDSRNO<=825")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL33").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=801 AND GRIDSRNO<=825"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=826 AND GRIDSRNO<=850")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL34").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=826 AND GRIDSRNO<=850"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=851 AND GRIDSRNO<=875")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL35").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=851 AND GRIDSRNO<=875"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=876 AND GRIDSRNO<=900")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL36").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=876 AND GRIDSRNO<=900"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=901 AND GRIDSRNO<=925")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL37").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=901 AND GRIDSRNO<=925"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=926 AND GRIDSRNO<=950")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL38").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=926 AND GRIDSRNO<=950"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=951 AND GRIDSRNO<=975")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL39").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=951 AND GRIDSRNO<=975"))
                        If IsDBNull(DT.Compute("SUM(MTRS)", "GRIDSRNO >=976 AND GRIDSRNO<=1000")) = False Then RPTCHALLAN.DataDefinition.FormulaFields("TOTAL40").Text = Val(DT.Compute("SUM(MTRS)", "GRIDSRNO >=976 AND GRIDSRNO<=1000"))

                        If ClientName = "NIRMALA" Or ClientName = "YESHA" Then RPTCHALLAN.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                        CRPO.ReportSource = RPTCHALLAN

                    End If
                Else
                    If ClientName = "NIRMALA" Or ClientName = "YESHA" Then RPTCHALLANYARN.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                    CRPO.ReportSource = RPTCHALLANYARN
                End If
            End If

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch Exp As LoadSaveReportException
            MsgBox("Incorrect path for loading report.",
                    MsgBoxStyle.Critical, "Load Report Error")

        Catch Exp As Exception
            MsgBox(Exp.Message, MsgBoxStyle.Critical, "General Error")

        End Try
    End Sub

    Sub PRINTDIRECTLYTOPRINTER()
        Try
            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

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

            'strsearch = "{PACKINGSLIP.PS_NO} = " & PSNO & " AND {PACKINGSLIP.PS_YEARID} = " & YearId
            'CRPO.SelectionFormula = strsearch
            If FRMSTRING = "PACKINGSLIP" Then
                strsearch = "  {PACKINGSLIP.PS_NO} = " & PSNO & " AND {PACKINGSLIP.PS_YEARID} = " & YearId
            ElseIf FRMSTRING = "CHALLAN" Then
                strsearch = "  {CHALLANMASTER.Challan_no} = " & CHALLANNO & " AND {CHALLANMASTER.challan_YEARID} = " & YearId
                CRPO.SelectionFormula = strsearch
            End If


            Dim OBJ As New Object
            If FRMSTRING = "PACKINGSLIP" Then
                OBJ = New PackingSlipReport
            ElseIf FRMSTRING = "CHALLAN" Then
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
                Dim DT As DataTable = OBJCMN.Execute_Any_String(" SELECT CHALLAN_MTRS AS MTRS, CHALLAN_GRIDSRNO AS GRIDSRNO FROM CHALLANMASTER_MTRS WHERE CHALLAN_NO = " & CHALLANNO & " AND CHALLAN_YEARID = " & YearId & " ORDER BY CHALLAN_GRIDSRNO", "", "")
                'RPTCHALLAN.DataDefinition.FormulaFields("TOTAL01").Text = Val()

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

SKIPINVOICE:
            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = strsearch
            OBJ.REFRESH()

            If DIRECTWHATSAPP = False Then
                OBJ.RecordSelectionFormula = strsearch
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            End If


            'If DIRECTMAIL = False Then
            '    OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
            '    OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            'Else
            '    Dim expo As New ExportOptions
            '    Dim oDfDopt As New DiskFileDestinationOptions
            '    oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE_" & INVNO & ".pdf"
            '    expo = OBJ.ExportOptions
            '    If ClientName = "SAKARIA" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = 1
            '    expo.ExportDestinationType = ExportDestinationType.DiskFile
            '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
            '    expo.DestinationOptions = oDfDopt
            '    OBJ.Export()
            'End If

            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions
                If FRMSTRING = "CHALLAN" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_CHALLAN_NO-" & CHALLANNO & ".pdf"
                End If


                'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                expo = OBJ.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJ.Export()
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()

            If FRMSTRING = "CHALLAN" Then
                tempattachment = "CHALLAN"


            End If
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = PARTYNAME
            OBJWHATSAPP.AGENTNAME = AGENTNAME
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & tempattachment & ".PDF")
            OBJWHATSAPP.FILENAME.Add(tempattachment & ".pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()
        Dim tempattachment As String

        If FRMSTRING = "PACKINGSLIP" Then tempattachment = "PACKINGSLIP" Else tempattachment = "CHALLAN"
        Try
            Dim objmail As New SendMail
            objmail.attachment = Application.StartupPath & "\" & tempattachment & ".PDF"
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

            If FRMSTRING = "CHALLAN" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\CHALLAN.pdf"
                If SCREENTYPE = "GREY" Then
                    If MULTIYARN = True Then
                        expo = RPTCHALLAN_TAKANO.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTCHALLAN_TAKANO.Export()
                    ElseIf ClientName = "SASHWINKUMAR" Then
                        expo = RPTCHALLAN_SASHWIN.ExportOptions
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTCHALLAN_SASHWIN.Export()
                    Else
                        expo = RPTCHALLAN.ExportOptions
                        RPTCHALLAN.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTCHALLAN.Export()
                        RPTCHALLAN.DataDefinition.FormulaFields("SENDMAIL").Text = 0
                    End If
                Else
                    expo = RPTCHALLANYARN.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTCHALLANYARN.Export()
                End If
            ElseIf FRMSTRING = "CHALLANFINISH" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\CHALLAN.pdf"
                expo = RPTCHALLANFINISH.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCHALLANFINISH.Export()

            ElseIf FRMSTRING = "PACKINGSLIP" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\PACKINGSLIP.pdf"
                expo = RPTPACKINGSLIP.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTPACKINGSLIP.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub


End Class