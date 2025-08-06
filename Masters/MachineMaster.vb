
Imports BL
Imports System.Windows.Forms

Public Class MachineMaster
    Public EDIT As Boolean              'Used for edit
    Public tempname As String           'Used for edit name
    Public tempid As Integer            'Used for edit id
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean


    Sub clear()
        txtname.Clear()
        txtremarks.Clear()
        txtname.Focus()
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If txtname.Text.Trim.Length = 0 Then
            Ep.SetError(txtname, "Fill Name")
            bln = False
        End If
        Return bln
    End Function

    Private Sub citymaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt = True And e.KeyCode = Windows.Forms.Keys.S Then       'for Saving
            Call cmdok_Click(sender, e)
        ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        End If
    End Sub

    Private Sub TXTNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtname.Validating
        Try
            If txtname.Text.Trim <> "" Then
                If (EDIT = False) Or (EDIT = True And LCase(tempname) <> LCase(txtname.Text.Trim)) Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search("MACHINE_ID AS ID", "", "MACHINEMASTER", " AND MACHINE_NAME = '" & txtname.Text.Trim & "' AND MACHINE_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        MsgBox("Machine Name Already Exists", MsgBoxStyle.Critical)
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Try
                Ep.Clear()
                If Not errorvalid() Then
                    Exit Sub
                End If

                Dim OBJYARN As New ClsMachineMaster
                OBJYARN.alParaval.Add(txtname.Text.Trim)
                OBJYARN.alParaval.Add(txtremarks.Text.Trim)
                OBJYARN.alParaval.Add(CmpId)
                OBJYARN.alParaval.Add(Userid)
                OBJYARN.alParaval.Add(YearId)

                If EDIT = False Then

                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If


                    Dim INTRES As Integer = OBJYARN.SAVE()
                    MsgBox("Details Added")
                Else
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    OBJYARN.alParaval.Add(tempid)
                    Dim INTRES As Integer = OBJYARN.UPDATE()
                    MsgBox("Details Updated")
                    EDIT = False
                End If
                clear()
                txtname.Focus()

            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MillMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'QUALITY MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If EDIT = True Then
                Dim OBJYARN As New ClsMachineMaster
                OBJYARN.alParaval.Add(tempid)
                OBJYARN.alParaval.Add(YearId)
                Dim DT As DataTable = OBJYARN.GETMACHINE()
                If DT.Rows.Count > 0 Then
                    txtname.Text = DT.Rows(0).Item("NAME")
                    tempname = DT.Rows(0).Item("NAME")
                    txtremarks.Text = DT.Rows(0).Item("REMARKS")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        clear()
        EDIT = False
    End Sub

    Private Sub cmddelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try

            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class