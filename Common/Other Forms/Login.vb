
Imports BL

Public Class Login

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        End
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        End
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            CHECKVERSION()
            Ep.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim objlogin As New clsLogin
            UserName = txtusername.Text.Trim
            Mydate = objlogin.getdate()
            Cmpdetails.Show()
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If txtusername.Text.Trim.Length = 0 Then
            EP.SetError(txtusername, "Fill User Name")
            bln = False
        End If

        If txtpassword.Text.Trim.Length = 0 Then
            EP.SetError(txtpassword, "Fill Password")
            bln = False
        End If

        Dim objcmn As New ClsCommon
        Dim dt As DataTable = objcmn.search("User_id, User_password", "", "UserMaster", " and user_namE= '" & txtusername.Text.Trim & "'")
        If dt.Rows.Count > 0 Then
            For Each DTROW As DataRow In dt.Rows
                If txtpassword.Text.Trim <> DTROW(1).ToString Then
                    bln = False
                Else
                    Userid = DTROW(0)

                    ''*********SESSION CHECKING****************

                    'If txtusername.Text.Trim = "Admin" Then
                    '    GoTo line1
                    'End If

                    'Dim dt1 As DataTable = objcmn.search(" USER_CHK", "", " USERMASTER", " and user_namE= '" & txtusername.Text.Trim & "' and user_cmpid='" & CmpId & "' and user_locationid='" & Locationid & "' and user_yearid='" & YearId & "'")
                    'If dt1.Rows.Count > 0 Then
                    '    If dt1.Rows(0).Item("USER_CHK") = "1" Then
                    '        'Ep.SetError(txtpassword, "Please Logout from another system !")
                    '        MsgBox("Please Logout from another system", MsgBoxStyle.Critical)
                    '        bln = False
                    '        End
                    '    End If
                    'Else
                    '    'IF CLIENTNAME IS NOT PRESENT
                    '    End
                    'End If
line1:
                    bln = True
                    Exit For
                End If
            Next
        Else
            Ep.SetError(txtusername, "Invalid User")
            bln = False
        End If
        If bln = False Then Ep.SetError(txtpassword, "Incorrect Password")

        Return bln
    End Function

    Private Sub txtusername_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtusername.Validated
        txtusername.Text = StrConv(txtusername.Text, VbStrConv.ProperCase)
    End Sub

    Private Sub Login_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            End
        End If
    End Sub

    Sub CHECKVERSION()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable

            DT = OBJCMN.search(" VERSION_NO AS VERSION, VERSION_CLIENTNAME AS CLIENTNAME, VERSION_REPORTTYPE AS REPORTTYPE, ISNULL(VERSION_ALLOWEWAYBILL,0) AS ALLOWEWAYBILL, ISNULL(VERSION_PRINTEWAYBILL,0) AS PRINTEWAYBILL, ISNULL(VERSION_INVOICELINEGST,0) AS INVLINEGST, ISNULL(VERSION_MULTIYARN,0) AS MULTIYARN, ISNULL(VERSION_MANUALCNDN,0) AS MANUALCNDN, ISNULL(VERSION_ALLOWEINVOICE,0) AS ALLOWEINVOICE , ISNULL(VERSION_ALLOWWHATSAPP,0) AS ALLOWWHATSAPP, ISNULL(VERSION_WHATSAPPACT,GETDATE()) AS WHATSAPPACT, ISNULL(VERSION_WHATSAPPAUTOCC,0) AS WHATSAPPAUTOCC ", "", " VERSION", "")
            If DT.Rows.Count > 0 Then
                REPORTTYPE = DT.Rows(0).Item("REPORTTYPE")
                ClientName = DT.Rows(0).Item("CLIENTNAME")
                ALLOWEWAYBILL = Convert.ToBoolean(DT.Rows(0).Item("ALLOWEWAYBILL"))
                PRINTEWAYBILL = Convert.ToBoolean(DT.Rows(0).Item("PRINTEWAYBILL"))
                If Convert.ToBoolean(DT.Rows(0).Item("INVLINEGST")) = True Then INVOICESCREENTYPE = "LINE GST" Else INVOICESCREENTYPE = "TOTAL GST"
                MULTIYARN = Convert.ToBoolean(DT.Rows(0).Item("MULTIYARN"))
                ALLOWMANUALCNDN = Convert.ToBoolean(DT.Rows(0).Item("MANUALCNDN"))
                ALLOWEINVOICE = Convert.ToBoolean(DT.Rows(0).Item("ALLOWEINVOICE"))
                ALLOWWHATSAPP = Convert.ToBoolean(DT.Rows(0).Item("ALLOWWHATSAPP"))
                If ALLOWWHATSAPP = True Then WHATSAPPEXPDATE = Convert.ToDateTime(DT.Rows(0).Item("WHATSAPPACT")).Date.AddYears(1) Else WHATSAPPEXPDATE = Now.Date
                WHATSAPPAUTOCC = Convert.ToBoolean(DT.Rows(0).Item("WHATSAPPAUTOCC"))

                If ClientName = "NIRMALA" Then
                    If Now.Date > DateTime.Parse("15.07.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SASHWINKUMAR" Then
                    If Now.Date > DateTime.Parse("15.07.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "YESHA" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "DEMO" Then
                    If Now.Date > DateTime.Parse("10.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                End If

                If DT.Rows(0).Item("VERSION") <> "1.0.039" Then
                    MsgBox("Please Uninstall previous copy of PROCESS and Re-Install the Latest Version.", MsgBoxStyle.Critical)
LINE1:
                    MsgBox(" VERSION EXPIRED PLEASE CONTACT NAKODA INFOTECH ON +9987603607", MsgBoxStyle.Critical)
                    End
                End If
            Else
                'IF CLIENTNAME IS NOT PRESENT
                End
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtpassword_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpassword.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If txtusername.Text.Trim <> "" And txtpassword.Text.Trim <> "" Then Call cmdok_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Login_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "dd/MM/yyyy")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
