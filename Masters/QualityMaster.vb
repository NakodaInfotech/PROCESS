
Imports BL
Imports System.Windows.Forms
Imports System.Data

Public Class QualityMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim IntResult As Integer
    Public edit As Boolean
    Public tempQualityName As String
    Public tempQualityId As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer

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

            alParaval.Add(cmbQuality.Text.Trim)

            If CMBYARNQUALITY.Text.Trim = "" Then
                alParaval.Add(cmbQuality.Text.Trim)
            Else
                alParaval.Add(CMBYARNQUALITY.Text.Trim)
            End If

            alParaval.Add(Val(TXTRATE.Text.Trim))
            alParaval.Add(TXTHSNCODE.Text.Trim)
            alParaval.Add(TXTDENIER.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)


            Dim YARNQUALITY As String = ""
            Dim PER As String = ""

            For Each ROW As DataGridViewRow In GRIDCOMP.Rows
                If ROW.Cells(GYARNQUALITY.Index).Value <> Nothing Then
                    If YARNQUALITY = "" Then
                        YARNQUALITY = ROW.Cells(GYARNQUALITY.Index).Value.ToString
                        PER = Val(ROW.Cells(GPER.Index).Value)
                    Else
                        YARNQUALITY = YARNQUALITY & "|" & ROW.Cells(GYARNQUALITY.Index).Value.ToString
                        PER = PER & "|" & Val(ROW.Cells(GPER.Index).Value)
                    End If
                End If
            Next

            alParaval.Add(YARNQUALITY)
            alParaval.Add(PER)




            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            Dim OBJQUALITYProcessMaster As New ClsQualityMaster
            OBJQUALITYProcessMaster.alParaval = alParaval

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = OBJQUALITYProcessMaster.SAVE()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(tempQualityId)
                IntResult = OBJQUALITYProcessMaster.UPDATE()
                MsgBox("Details Updated")
                edit = False
            End If

            clear()
            cmbQuality.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Function CHECKDUPLICATE() As Boolean
        Try
            Dim BLN As Boolean = True
            uppercase(cmbQuality)
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable
            If (edit = False) Or (edit = True And LCase(cmbQuality.Text) <> LCase(tempQualityName)) Then
                dt = OBJCMN.search("QUALITY_name", "", "QUALITYMaster", " and QUALITY_name = '" & cmbQuality.Text.Trim & "'  And QUALITY_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Quality Already Exists", MsgBoxStyle.Critical, "PROCESS")
                    BLN = False
                End If
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If cmbQuality.Text.Trim.Length = 0 Then
            Ep.SetError(cmbQuality, "Fill Quality Name")
            bln = False
        End If

        If Not CHECKDUPLICATE() Then
            Ep.SetError(cmbQuality, "Quality Already Exists")
            bln = False
        End If

        'THIS IS DONE BY GULKIT
        'IF THERE IS NO COMPOSITION THEN SAME ITEMNAME SHOULD BE PASSED IN THE GRID WITH 100%
        If GRIDCOMP.RowCount = 0 And cmbQuality.Text.Trim <> "" Then
            GRIDCOMP.Rows.Add(cmbQuality.Text.Trim, 100)
            TXTTOTALPER.Text = 100
        End If

        If Val(TXTTOTALPER.Text.Trim) <> 100 And GRIDCOMP.RowCount > 0 Then
            Ep.SetError(TXTTOTALPER, "Check %")
            bln = False
        End If

        If TXTHSNCODE.Text.Trim.Length = 0 Then
            Ep.SetError(TXTHSNCODE, "Fill HSN Code")
            bln = False
        End If
        Return bln
    End Function

    Sub clear()
        Try
            cmbQuality.Text = ""
            CMBYARNQUALITY.Text = ""
            TXTRATE.Clear()
            TXTHSNCODE.Clear()
            TXTDENIER.Clear()
            txtremarks.Clear()
            CMBYARNCOMPOSITION.Text = ""
            TXTPER.Clear()

            'clearing grid
            GRIDCOMP.RowCount = 0
            TXTTOTALPER.Clear()

            'gridstock.RowCount = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub QualityMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Sub fillcmb()
        Try
            If cmbQuality.Text.Trim = "" Then fillQUALITY(cmbQuality, edit)
            If CMBYARNQUALITY.Text.Trim = "" Then fillQUALITY(CMBYARNQUALITY, edit)
            If CMBYARNCOMPOSITION.Text.Trim = "" Then fillQUALITY(CMBYARNCOMPOSITION, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ProcessMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'QUALITY MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillcmb()
            clear()
            cmbQuality.Text = tempQualityName

            If edit = True Then
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objCommon As New ClsCommonMaster

                Dim dttable As DataTable = objCommon.search(" ISNULL(QUALITYMASTER.QUALITY_ID, 0) AS QUALITYID, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(YARNQUALITY.QUALITY_NAME, '')  AS YARNQUALITY, ISNULL(QUALITYMASTER.QUALITY_REMARKS, '') AS REMARKS, ISNULL(QUALITYMASTER.QUALITY_RATE,0) AS RATE, ISNULL(HSNMASTER.HSN_CODE,'') AS HSNCODE, ISNULL(QUALITYMASTER.QUALITY_DENIER, 0) AS DENIER", "", "   QUALITYMASTER LEFT OUTER JOIN QUALITYMASTER AS YARNQUALITY ON QUALITYMASTER.QUALITY_EFFECTQUALITYID = YARNQUALITY.QUALITY_ID LEFT OUTER JOIN HSNMASTER ON QUALITYMASTER.QUALITY_HSNCODEID = HSN_ID", " and QUALITYMASTER.QUALITY_Name = '" & tempQualityName & "' and QUALITYMASTER.QUALITY_yearid = " & YearId)
                tempQualityId = dttable.Rows(0).Item("QUALITYID")
                cmbQuality.Text = dttable.Rows(0).Item("QUALITY")
                CMBYARNQUALITY.Text = dttable.Rows(0).Item("YARNQUALITY")
                TXTRATE.Text = Val(dttable.Rows(0).Item("RATE"))
                TXTHSNCODE.Text = dttable.Rows(0).Item("HSNCODE")
                TXTDENIER.Text = dttable.Rows(0).Item("DENIER")
                txtremarks.Text = dttable.Rows(0).Item("REMARKS")

                Dim OBJCMN As New ClsCommon
                Dim dttable1 As DataTable = OBJCMN.search(" ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS YARNCOMPOSITION, ISNULL(QUALITYMASTER_COMPOSITION.QUALITY_PER, 0) AS PER ", "", " QUALITYMASTER INNER JOIN QUALITYMASTER_COMPOSITION ON QUALITYMASTER.QUALITY_ID = QUALITYMASTER_COMPOSITION.QUALITY_YARNQUALITYID AND QUALITYMASTER.QUALITY_YEARID = QUALITYMASTER_COMPOSITION.QUALITY_YEARID  ", " AND QUALITYMASTER_COMPOSITION.QUALITY_ID = " & tempQualityId & " AND QUALITYMASTER_COMPOSITION.QUALITY_YEARID = " & YearId)
                If dttable1.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable1.Rows
                        GRIDCOMP.Rows.Add(DTR("YARNCOMPOSITION"), DTR("PER"))
                    Next

                    total()
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub CMBQUALITYNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbQuality.Validating
        Try
            If cmbQuality.Text.Trim <> "" Then
                uppercase(cmbQuality)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                If (edit = False) Or (edit = True And LCase(cmbQuality.Text) <> LCase(tempQualityName)) Then
                    dt = OBJCMN.search("QUALITY_name", "", "QUALITYMaster", " and QUALITY_name = '" & cmbQuality.Text.Trim & "' And QUALITY_yearid = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Quality Name Already Exists", MsgBoxStyle.Critical, "PROCESS")
                        e.Cancel = True
                    End If
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbQuality.Enter
        Try
            If cmbQuality.Text.Trim = "" Then fillQUALITY(cmbQuality, edit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If edit = False Then Exit Sub

            Dim OBJQUALITY As New ClsQualityMaster
            If MsgBox("Wish To Delete?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim alParaval As New ArrayList
            alParaval.Add(tempQualityId)
            alParaval.Add(YearId)
            OBJQUALITY.alParaval = alParaval
            Dim DT As DataTable = OBJQUALITY.DELETE
            If DT.Rows.Count > 0 Then
                MsgBox(DT.Rows(0).Item(0))
                clear()
                edit = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBYARNQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBYARNQUALITY.Enter
        Try
            If CMBYARNQUALITY.Text.Trim = "" Then fillQUALITY(CMBYARNQUALITY, edit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBYARNQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBYARNQUALITY.Validating
        Try
            If CMBYARNQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBYARNQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBYARNQUALITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBYARNQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJYARN As New SelectQuality
                OBJYARN.ShowDialog()
                If OBJYARN.TEMPNAME <> "" Then CMBYARNQUALITY.Text = OBJYARN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRATE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRATE.KeyPress, TXTDENIER.KeyPress, TXTPER.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTHSNCODE_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TXTHSNCODE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectHSN
                OBJLEDGER.STRSEARCH = " AND HSN_TYPE='GOODS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then TXTHSNCODE.Text = OBJLEDGER.TEMPCODE
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTPER.Validating
        Try
            If Val(TXTPER.Text.Trim) < 0 And Val(TXTPER.Text.Trim) > 100 Then e.Cancel = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTPER.Validated
        Try
            If Val(TXTPER.Text.Trim) > 0 And CMBYARNCOMPOSITION.Text.Trim <> "" Then
                If Not checkPERTYPE() Then
                    MsgBox("% already Present in Grid below")
                    Exit Sub
                End If

                fillgridCOMP()
                total()

                CMBYARNCOMPOSITION.Text = ""
                TXTPER.Clear()
                CMBYARNQUALITY.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgridCOMP()

        If GRIDDOUBLECLICK = False Then
            GRIDCOMP.Rows.Add(CMBYARNCOMPOSITION.Text.Trim, Val(TXTPER.Text.Trim))
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDCOMP.Item("GYARNQUALITY", TEMPROW).Value = CMBYARNCOMPOSITION.Text.Trim
            GRIDCOMP.Item("GPER", TEMPROW).Value = Val(TXTPER.Text.Trim)
            GRIDDOUBLECLICK = False
        End If

        total()
        CMBYARNQUALITY.Text = ""
        TXTPER.Clear()

        GRIDCOMP.ClearSelection()

    End Sub

    Function checkPERTYPE() As Boolean
        Try
            Dim bln As Boolean = True
            For Each row As DataGridViewRow In GRIDCOMP.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> row.Index) Or GRIDDOUBLECLICK = False Then
                    If CMBYARNCOMPOSITION.Text.Trim = row.Cells(GYARNQUALITY.Index).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GRIDCOMP_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDCOMP.CellDoubleClick
        Try
            If e.RowIndex >= 0 And GRIDCOMP.Item("GYARNQUALITY", e.RowIndex).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                TEMPROW = e.RowIndex
                CMBYARNCOMPOSITION.Text = GRIDCOMP.Item("GYARNQUALITY", e.RowIndex).Value
                TXTPER.Text = GRIDCOMP.Item("GPER", e.RowIndex).Value
                CMBYARNCOMPOSITION.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub total()
        Try
            TXTTOTALPER.Text = "0.00"

            For Each ROW As DataGridViewRow In GRIDCOMP.Rows
                'If ROW.Cells(gsrno.Index).Value <> Nothing Then
                TXTTOTALPER.Text = Format(Val(TXTTOTALPER.Text) + Val(ROW.Cells(GPER.Index).EditedFormattedValue), "0.00")

                'End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCOMP_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCOMP.KeyDown
        If e.KeyCode = Keys.Delete Then
            GRIDCOMP.Rows.RemoveAt(GRIDCOMP.CurrentRow.Index)
        End If
    End Sub

    Private Sub CMBCMBYARNCOMPOSITION_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBYARNCOMPOSITION.Enter
        Try
            If CMBYARNCOMPOSITION.Text.Trim = "" Then fillQUALITY(CMBYARNCOMPOSITION, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBYARNCOMPOSITION_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBYARNCOMPOSITION.Validating
        Try
            If CMBYARNCOMPOSITION.Text.Trim <> "" Then QUALITYVALIDATE(CMBYARNQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class