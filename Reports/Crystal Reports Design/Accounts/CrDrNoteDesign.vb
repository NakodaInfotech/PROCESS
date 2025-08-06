
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms.Form
Imports CrystalDecisions.Shared

Public Class CrDrNoteDesign

    Public BILLNO As Integer
    Public REGNAME As String
    Public FRMSTRING As String
    Dim OBJCRNOTE As New CreditNoteReport
    Dim OBJDRNOTE As New DebitNoteReport
    Dim OBJDRNIRMALA As New DebitNoteReport_NIRMALA
    Public printst As Boolean
    Public printot As Boolean

    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public NOOFCOPIES As Integer = 1
    Public PRINTSETTING As Object = Nothing
    Dim tempattachment As String
    Public PARTYNAME As String
    Public AGENTNAME As String

    Private Sub CrDrNoteDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CrDrNoteDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strsearch As String
        strsearch = ""

        Try

            If DIRECTPRINT = True Then
                PRINTDIRECTLYTOPRINTER()
                Exit Sub
            End If

            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table

            'RPTINVOICE_AKS.DataDefinition.FormulaFields("SENDMAIL").Text = 1
            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            If FRMSTRING = "CREDIT" Then
                crTables = OBJCRNOTE.Database.Tables
            ElseIf FRMSTRING = "DEBIT" Then
                'If ClientName = "NIRMALA" Then
                '    crTables = OBJDRNIRMALA.Database.Tables
                'Else
                '    crTables = OBJDRNOTE.Database.Tables

                'End If
                crTables = OBJDRNOTE.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next
            '************* END *******************

            If FRMSTRING = "CREDIT" Then
                strsearch = strsearch & "  {CREDITNOTEMASTER.CN_NO}= " & BILLNO & " and {REGISTERMASTER.REGISTER_NAME} = '" & REGNAME & "' and {CREDITNOTEMASTER.CN_CMPID} = " & CmpId & " and {CREDITNOTEMASTER.CN_LOCATIONID} = " & Locationid & " and {CREDITNOTEMASTER.CN_YEARID} = " & YearId
                CRPO.SelectionFormula = strsearch
                CRPO.ReportSource = OBJCRNOTE
                If ClientName = "NIRMALA" Or ClientName = "YESHA" Then OBJCRNOTE.DataDefinition.FormulaFields("SENDMAIL").Text = 1
            ElseIf FRMSTRING = "DEBIT" Then
                'If ClientName = "NIRMALA" Then
                '    strsearch = strsearch & "  {DEBITNOTEMASTER.DN_NO}= " & BILLNO & " AND {REGISTERMASTER.REGISTER_NAME}= '" & REGNAME & "' AND {DEBITNOTEMASTER.DN_CMPID} = " & CmpId & " and {DEBITNOTEMASTER.DN_LOCATIONID} = " & Locationid & " and {DEBITNOTEMASTER.DN_YEARID} = " & YearId
                '    CRPO.SelectionFormula = strsearch
                '    CRPO.ReportSource = OBJDRNIRMALA
                'Else
                '    strsearch = strsearch & "  {DEBITNOTEMASTER.DN_NO}= " & BILLNO & " AND {REGISTERMASTER.REGISTER_NAME}= '" & REGNAME & "' AND {DEBITNOTEMASTER.DN_CMPID} = " & CmpId & " and {DEBITNOTEMASTER.DN_LOCATIONID} = " & Locationid & " and {DEBITNOTEMASTER.DN_YEARID} = " & YearId
                '    CRPO.SelectionFormula = strsearch
                '    CRPO.ReportSource = OBJDRNOTE
                '    If ClientName = "YESHA" Then OBJDRNOTE.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                'End If
                strsearch = strsearch & "  {DEBITNOTEMASTER.DN_NO}= " & BILLNO & " AND {REGISTERMASTER.REGISTER_NAME}= '" & REGNAME & "' AND {DEBITNOTEMASTER.DN_CMPID} = " & CmpId & " and {DEBITNOTEMASTER.DN_LOCATIONID} = " & Locationid & " and {DEBITNOTEMASTER.DN_YEARID} = " & YearId
                CRPO.SelectionFormula = strsearch
                CRPO.ReportSource = OBJDRNOTE
                If ClientName = "NIRMALA" Or ClientName = "YESHA" Then OBJDRNOTE.DataDefinition.FormulaFields("SENDMAIL").Text = 1
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

            If FRMSTRING = "CREDIT" Then
                strsearch = "  {CREDITNOTEMASTER.CN_NO}= " & BILLNO & " and {REGISTERMASTER.REGISTER_NAME} = '" & REGNAME & "' and {CREDITNOTEMASTER.CN_CMPID} = " & CmpId & " and {CREDITNOTEMASTER.CN_LOCATIONID} = " & Locationid & " and {CREDITNOTEMASTER.CN_YEARID} = " & YearId
            ElseIf FRMSTRING = "DEBIT" Then
                strsearch = "  {DEBITNOTEMASTER.DN_NO}= " & BILLNO & " AND {REGISTERMASTER.REGISTER_NAME}= '" & REGNAME & "' AND {DEBITNOTEMASTER.DN_CMPID} = " & CmpId & " and {DEBITNOTEMASTER.DN_LOCATIONID} = " & Locationid & " and {DEBITNOTEMASTER.DN_YEARID} = " & YearId
            End If
            CRPO.SelectionFormula = strsearch

            Dim OBJ As New Object
            If FRMSTRING = "CREDIT" Then
                OBJ = New CreditNoteReport
            ElseIf FRMSTRING = "DEBIT" Then
                OBJ = New DebitNoteReport
            End If

SKIPINVOICE:
            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = strsearch

            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                If ClientName = "NIRMALA" Or ClientName = "YESHA" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
                If ClientName = "NIRMALA" Or ClientName = "YESHA" Then OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
            Else
                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions
                If FRMSTRING = "CREDIT" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\CN_" & BILLNO & ".pdf"
                ElseIf FRMSTRING = "DEBIT" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\DN_" & BILLNO & ".pdf"
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

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()
        Dim tempattachment As String
        If FRMSTRING = "CREDIT" Then
            tempattachment = "CREDIT NOTE"
        ElseIf FRMSTRING = "DEBIT" Then
            tempattachment = "DEBIT NOTE"
        End If
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

            If FRMSTRING = "CREDIT" Then
                If ClientName = "NIRMALA" Or ClientName = "YESHA" Then OBJCRNOTE.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                oDfDopt.DiskFileName = Application.StartupPath & "\CREDIT NOTE.PDF"
                expo = OBJCRNOTE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJCRNOTE.Export()
                If ClientName = "NIRMALA" Or ClientName = "YESHA" Then OBJCRNOTE.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
            ElseIf FRMSTRING = "DEBIT" Then
                'If ClientName = "NIRMALA" Then
                '    oDfDopt.DiskFileName = Application.StartupPath & "\DEBIT NOTE.PDF"
                '    expo = OBJDRNIRMALA.ExportOptions
                '    expo.ExportDestinationType = ExportDestinationType.DiskFile
                '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                '    expo.DestinationOptions = oDfDopt
                '    OBJDRNIRMALA.Export()
                'Else
                '    oDfDopt.DiskFileName = Application.StartupPath & "\DEBIT NOTE.PDF"
                '    expo = OBJDRNOTE.ExportOptions
                '    expo.ExportDestinationType = ExportDestinationType.DiskFile
                '    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                '    expo.DestinationOptions = oDfDopt
                '    OBJDRNOTE.Export()
                'End If

                If ClientName = "NIRMALA" Or ClientName = "YESHA" Then OBJDRNOTE.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                oDfDopt.DiskFileName = Application.StartupPath & "\DEBIT NOTE.PDF"
                expo = OBJDRNOTE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJDRNOTE.Export()
                If ClientName = "NIRMALA" Or ClientName = "YESHA" Then OBJDRNOTE.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()

            If FRMSTRING = "CREDIT" Or FRMSTRING = "PROFORMACREDIT" Then
                tempattachment = "CREDIT NOTE"

            ElseIf FRMSTRING = "DEBIT" Or FRMSTRING = "PROFORMADEBIT" Then
                tempattachment = "DEBIT NOTE"

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
End Class