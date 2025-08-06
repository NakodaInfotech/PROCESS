Imports BL
Imports System.Windows.Forms

Public Class TypeMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public edit As Boolean              'Used for edit
    Public tempname As String           'Used for edit name
    Public tempid As Integer            'Used for edit id

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            Ep.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(txtname.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            Dim objtax As New ClsTypeMaster
            objtax.alParaval = alParaval
            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objtax.save()
                MsgBox("Details Added")

            ElseIf edit = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(tempid)
                'objCity.alParaval = alParaval
                IntResult = objtax.Update()
                MsgBox("Details Updated")
                edit = False

            End If
            clear()
            txtname.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If txtname.Text.Trim.Length = 0 Then
            Ep.SetError(txtname, "Fill Type")
            bln = False
        End If

        Return bln
    End Function

    Sub clear()
        txtname.Clear()
        txtremarks.Clear()
    End Sub

    Private Sub txtname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtname.Validating
        Try

            If (edit = False) Or (edit = True And LCase(tempname) <> LCase(txtname.Text.Trim)) Then
                ' search duplication 
                If txtname.Text.Trim <> Nothing Then
                    Dim OBJCMN As New ClsCommonMaster
                    Dim dt As DataTable
                    dt = OBJCMN.search("TYPE_name", "", " TYPEMASTER ", " and TYPE_name = '" & txtname.Text.Trim & "' AND TYPE_CMPID=" & CmpId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Type Already Exists", MsgBoxStyle.Critical, "")
                        txtname.Focus()
                    End If
                    uppercase(txtname)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TypeMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt = True And e.KeyCode = Windows.Forms.Keys.S Then       'fOR sAVE
            Call cmdok_Click(sender, e)
        ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then               'FOR EXIT
            If errorvalid() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub TypeMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'TYPE MASTER'")
        USERADD = DTROW(0).Item(1)
        USEREDIT = DTROW(0).Item(2)
        USERVIEW = DTROW(0).Item(3)
        USERDELETE = DTROW(0).Item(4)

        Me.Text = "Type Master"

        If edit = True Then

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim dttable As New DataTable
            Dim objCommon As New ClsCommonMaster
            dttable = objCommon.search(" TYPE_name, TYPE_remarks ", "", "TYPEMaster", " and TYPE_id = " & tempid)

            If dttable.Rows.Count > 0 Then
                txtname.Text = dttable.Rows(0).Item(0).ToString
                'txttax.Text = dttable.Rows(0).Item(1).ToString
                txtremarks.Text = dttable.Rows(0).Item(1).ToString
            End If
        End If
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If edit = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim tempmsg As Integer = MsgBox("Delete Type Permanently?", MsgBoxStyle.YesNo, "PROCESS")
                If tempmsg = vbYes Then

                    Dim OBJTAX As New ClsTypeMaster
                    Dim ALPARAVAL As New ArrayList

                    ALPARAVAL.Add(txtname.Text.Trim)
                    ALPARAVAL.Add(CmpId)
                    ALPARAVAL.Add(Locationid)
                    ALPARAVAL.Add(YearId)

                    OBJTAX.alParaval = ALPARAVAL
                    Dim DT As DataTable = OBJTAX.DELETE()
                    MsgBox(DT.Rows(0).Item(0).ToString)

                    clear()

                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class