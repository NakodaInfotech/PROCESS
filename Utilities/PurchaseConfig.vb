
Imports BL

Public Class PurchaseConfig

    Private Sub PurchaseConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            fillregister(cmbregister, " AND REGISTER_TYPE = 'PURCHASE'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim BLN As Boolean = True
            If cmbregister.Text.Trim = "" Then
                EP.SetError(cmbregister, "Select Register")
                BLN = False
            End If
            If CMBTYPE.Text.Trim = "" Then
                EP.SetError(CMBTYPE, "Select Screen Type")
                BLN = False
            End If

            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            If Not ERRORVALID Then
                Exit Sub
            End If

            'FIRST DELETE OLD ENTRY AND RE ENTER THE RECORD
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.Execute_Any_String("DELETE A FROM PURCHASEREGCONFIG AS A INNER JOIN REGISTERMASTER AS REG ON A.REGISTERID = REG.register_id WHERE REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND REGISTER_YEARID = " & YearId, "", "")

            'AFTER DELETE INSERT THE RECORD
            DT = OBJCMN.Execute_Any_String(" INSERT INTO PURCHASEREGCONFIG VALUES ((SELECT REGISTER_ID  FROM REGISTERMASTER WHERE register_yearid = " & YearId & " AND register_name = '" & cmbregister.Text.Trim & "'),'" & CMBTYPE.Text.Trim & "'," & YearId & ")", "", "")
            MsgBox("Entry Added")
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMBTYPE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTYPE.SelectedIndexChanged
        Try
            'YARN
            'COMMON
            'GREY
            'FINISH
            'SIZING(CHGS)
            'WEAVING(CHGS)
            

            If CMBTYPE.Text = "YARN PURCHASE" Then
                PBYARN.Visible = True
                PBCOMMON.Visible = False
                PBGREY.Visible = False
                PBFINISHED.Visible = False
                PBSIZING.Visible = False
                PBWEAVING.Visible = False
            ElseIf CMBTYPE.Text = "GREY PURCHASE" Then
                PBYARN.Visible = False
                PBCOMMON.Visible = False
                PBGREY.Visible = True
                PBFINISHED.Visible = False
                PBSIZING.Visible = False
                PBWEAVING.Visible = False
            ElseIf CMBTYPE.Text = "COMMON PURCHASE" Then
                PBYARN.Visible = False
                PBCOMMON.Visible = True
                PBGREY.Visible = False
                PBFINISHED.Visible = False
                PBSIZING.Visible = False
                PBWEAVING.Visible = False
            ElseIf CMBTYPE.Text = "FINISHED PURCHASE" Then
                PBYARN.Visible = False
                PBCOMMON.Visible = False
                PBGREY.Visible = False
                PBFINISHED.Visible = True
                PBSIZING.Visible = False
                PBWEAVING.Visible = False
            ElseIf CMBTYPE.Text = "SIZING CHGS" Then
                PBYARN.Visible = False
                PBCOMMON.Visible = False
                PBGREY.Visible = False
                PBFINISHED.Visible = False
                PBSIZING.Visible = True
                PBWEAVING.Visible = False
            ElseIf CMBTYPE.Text = "WEAVING CHGS" Then
                PBYARN.Visible = False
                PBCOMMON.Visible = False
                PBGREY.Visible = False
                PBFINISHED.Visible = False
                PBSIZING.Visible = False
                PBWEAVING.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class