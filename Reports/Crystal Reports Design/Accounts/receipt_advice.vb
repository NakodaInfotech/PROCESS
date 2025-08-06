
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms.Form
Imports CrystalDecisions.Shared
Public Class receipt_advice

    Public recno As Integer
    Public recname As String
    Public REGNAME As String
    Dim obj_rectype As New recreport

    Private Sub receipt_advice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control = True And e.KeyCode = Keys.P Then
            CRPO.PrintReport()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub receipt_advice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strsearch As String
        strsearch = ""

        Try


            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table


            With crConnecttionInfo
                .ServerName = Servername
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With
            crTables = obj_rectype.Database.Tables
            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next
            '************* END *******************


            strsearch = strsearch & "  {receiptmaster.receipt_no}= " & recno & " and {RECEIPT_REPORT.REGNAME}= '" & REGNAME & "' and {ledgermaster.Acc_cmpname} = '" & recname & "' and {receiptmaster.receipt_cmpid} = " & CmpId & " and {receiptmaster.receipt_LOCATIONid} = " & Locationid & " and {receiptmaster.receipt_YEARid} = " & YearId
            CRPO.SelectionFormula = strsearch
            CRPO.ReportSource = obj_rectype
            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch Exp As LoadSaveReportException
            MsgBox("Incorrect path for loading report.", _
                    MsgBoxStyle.Critical, "Load Report Error")

        Catch Exp As Exception
            MsgBox(Exp.Message, MsgBoxStyle.Critical, "General Error")

        End Try
    End Sub
End Class