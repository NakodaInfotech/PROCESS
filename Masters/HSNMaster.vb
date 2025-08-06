
Imports BL
Imports System.Windows.Forms
Imports System.Data
Imports System.IO

Public Class HSNMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public tempItemName As String
    Public TEMPHSNNO, tempHSNCODE As String
    Dim tempId As Integer

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If CMBTYPE.Text.Trim.Length = 0 Then
            EP.SetError(CMBTYPE, "Fill Type")
            bln = False
        End If


        If EDIT = False Or (EDIT = True And tempHSNCODE <> TXTHSNCODE.Text.Trim) Then
            Dim OBJCMN As New ClsCommon
            Dim dttable As DataTable = OBJCMN.search("HSN_CODE", "", "HSNMASTER", " and HSN_CODE = '" & TXTHSNCODE.Text.Trim & "' And HSN_yearid = " & YearId)
            If dttable.Rows.Count > 0 Then
                EP.SetError(TXTHSNCODE, "Code  No Already Exist")
                bln = False
            End If
        End If


        If TXTHSNCODE.Text.Trim = "" Then
            EP.SetError(TXTHSNCODE, "Fill Code")
            bln = False
        End If

        If TXTITEMDESC.Text.Trim = "" Then
            EP.SetError(TXTITEMDESC, "Enter Item Description")
            bln = False
        End If

        If Val(TXTCGST.Text.Trim) = 0 Then
            EP.SetError(TXTCGST, "Enter CGST")
            bln = False
        End If

        If Val(TXTSGST.Text.Trim) = 0 Then
            EP.SetError(TXTSGST, "Enter SGST")
            bln = False
        End If

        If Val(TXTIGST.Text.Trim) = 0 Then
            EP.SetError(TXTIGST, "Enter IGST")
            bln = False
        End If

        Return bln
    End Function

    Function CHECKDUPLICATE() As Boolean
        Try
            Dim BLN As Boolean = True
            pcase(TXTHSNCODE)
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable
            If (EDIT = False) Or (EDIT = True And LCase(TXTHSNCODE.Text) <> LCase(tempHSNCODE)) Then
                dt = objclscommon.search("HSN_CODE", "", "HSNMASTER", " and HSN_CODE = '" & TXTHSNCODE.Text.Trim & "'  And HSN_cmpid = " & CmpId & " And HSN_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Code Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                    BLN = False
                End If
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub clear()
        CMBTYPE.Text = "Goods"
        LBLITEMDESC.Text = "Item Description"
        TXTHSNCODE.Clear()
        TXTITEMDESC.Clear()
        TXTDESC.Clear()
        TXTCGST.Clear()
        TXTSGST.Clear()
        TXTIGST.Clear()
        CHKNOITC.CheckState = CheckState.Unchecked
        getmax_HSN_NO()
    End Sub

    Sub getmax_HSN_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(HSN_ID),0) + 1 ", " HSNMASTER ", " AND HSN_cmpid=" & CmpId & " and HSN_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTHSNNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        Try
            clear()
            EDIT = False
            CMBTYPE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HSNMaster_KEYDOWN(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub HSNMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ACCOUNTS MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
            clear()

            TXTHSNCODE.Text = tempHSNCODE
            If EDIT = True Then
                Dim OBJDEPT As New ClsHSNMaster
                OBJDEPT.alParaval.Add(TEMPHSNNO)
                OBJDEPT.alParaval.Add(YearId)
                Dim DT As DataTable = OBJDEPT.GETHSN()
                If DT.Rows.Count > 0 Then

                    TXTHSNNO.Text = TEMPHSNNO

                    'TXTHSNCODE.Text = Val(tempHSNCODE)
                    CMBTYPE.Text = DT.Rows(0).Item("TYPE")
                    TXTHSNCODE.Text = DT.Rows(0).Item("CODE")
                    tempHSNCODE = DT.Rows(0).Item("CODE")
                    TXTITEMDESC.Text = DT.Rows(0).Item("ITEMDESC")
                    TXTDESC.Text = DT.Rows(0).Item("DESC")
                    TXTCGST.Text = DT.Rows(0).Item("CGST")
                    TXTSGST.Text = DT.Rows(0).Item("SGST")
                    TXTIGST.Text = DT.Rows(0).Item("IGST")
                    CHKNOITC.Checked = Convert.ToBoolean(DT.Rows(0).Item("NOITC"))

                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(CMBTYPE.Text.Trim)
            alParaval.Add(TXTHSNCODE.Text.Trim)
            alParaval.Add(TXTITEMDESC.Text.Trim)
            alParaval.Add(TXTDESC.Text.Trim)
            alParaval.Add(Val(TXTCGST.Text.Trim))
            alParaval.Add(Val(TXTSGST.Text.Trim))
            alParaval.Add(Val(TXTIGST.Text.Trim))
            alParaval.Add(CHKNOITC.Checked)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim objclsHSNMaster As New ClsHSNMaster
            objclsHSNMaster.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objclsHSNMaster.save()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPHSNNO)
                IntResult = objclsHSNMaster.update()
                MsgBox("Details Updated")

            End If
            EDIT = False

            clear()
            CMBTYPE.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try
            If EDIT = True Then
                Dim OBJHSN As New ClsHSNMaster
                OBJHSN.alParaval.Add(TEMPHSNNO)
                OBJHSN.alParaval.Add(YearId)

                Dim intResult As Integer = OBJHSN.Delete
                MsgBox("HSN Deleted Successfully")
                clear()
                EDIT = False
                CMBTYPE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCGST_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTSGST.KeyPress, TXTIGST.KeyPress, TXTCGST.KeyPress
        Try
            numdotkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTHSNCODE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTHSNCODE.Validating
        Try
            If TXTHSNCODE.Text <> "" Then
                If EDIT = False Or (EDIT = True And tempHSNCODE <> TXTHSNCODE.Text.Trim) Then
                    Dim OBJCMN As New ClsCommon
                    Dim dttable As DataTable = OBJCMN.search("HSN_CODE", "", "HSNMASTER", " and HSN_CODE = '" & TXTHSNCODE.Text.Trim & "'  And HSN_cmpid = " & CmpId & " And HSN_yearid = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        MsgBox("Code  No Already Exist")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    
    Private Sub CMBTYPE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTYPE.Validated
        Try
            If CMBTYPE.Text.Trim = "Services" Then LBLITEMDESC.Text = "Service Description" Else LBLITEMDESC.Text = "Item Description"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCGST_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCGST.Validating, TXTSGST.Validating, TXTIGST.Validating
        Try
            If Val(TXTCGST.Text.Trim) > 100 Or Val(TXTSGST.Text.Trim) > 100 Or Val(TXTIGST.Text.Trim) > 100 Then
                e.Cancel = True
                MsgBox("Tax % cannot be greter then 100", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class