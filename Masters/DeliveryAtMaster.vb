Imports BL
Imports System.Windows.Forms

Public Class DeliveryAtMaster
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public frmstring As String        'Used from Displaying Customer, Vendor, Employee Master
    Public edit As Boolean
    Public TEMPDELAT As String
    Public TEMPDELATID As Integer


    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub DeliveryAtMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Sub clear()
        Try
            TXTNAME.Clear()
            TXTADDRESS.Clear()
            Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DeliveryAtMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'DELIVERY AT MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)


            clear()
            TXTNAME.Text = TEMPDELAT

            If edit = True Then

                Dim dttable As New DataTable
                Dim objCommon As New ClsCommonMaster

                dttable = objCommon.search("  ISNULL(DELIVERYATMASTER.DELIVERYAT_ID, 0) AS DELATID, ISNULL(DELIVERYATMASTER.DELIVERYAT_NAME, '') AS NAME, ISNULL(DELIVERYATMASTER.DELIVERYAT_ADD, '') AS ADDRESS", "", "   DELIVERYATMASTER", " and DELIVERYAT_ID = '" & TEMPDELATID & "' and DELIVERYAT_yearid = " & YearId)
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If dttable.Rows.Count > 0 Then
                    For Each ROW As DataRow In dttable.Rows

                        TEMPDELATID = ROW("DELATID")
                        TEMPDELAT = ROW("NAME")

                        TXTNAME.Text = ROW("NAME").ToString
                        TXTADDRESS.Text = ROW("ADDRESS").ToString
                        
                    Next
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub


    Private Sub TXTNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNAME.Validating
        Try
            If TXTNAME.Text.Trim <> "" Then
                uppercase(TXTNAME)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                If (edit = False) Or (edit = True And LCase(TXTNAME.Text) <> LCase(TEMPDELAT)) Then
                    dt = OBJCMN.search("DELIVERYAT_NAME", "", "DELIVERYATMASTER", " and DELIVERYAT_NAME = '" & TXTNAME.Text.Trim & "' And DELIVERYAT_yearid = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Delivery At Name Already Exists", MsgBoxStyle.Critical, "PROCESS")
                        e.Cancel = True
                    End If
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If TXTNAME.Text.Trim.Length = 0 Then
            EP.SetError(TXTNAME, "Fill Delivery At Name")
            bln = False
        End If

        If TXTADDRESS.Text.Trim.Length = 0 Then
            EP.SetError(TXTADDRESS, "Enter Delivert At Address")
            bln = False
        End If

        Return bln
    End Function

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(TXTNAME.Text.Trim)
            alParaval.Add(TXTADDRESS.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim objdelat As New ClsDeliveryatMaster
            objdelat.alParaval = alParaval

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objdelat.save()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPDELATID)
                IntResult = objdelat.UPDATE()
                MsgBox("Details Updated")

            End If
            edit = False


            clear()
            TXTNAME.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub


End Class