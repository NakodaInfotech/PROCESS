Imports BL

Public Class MergeParameter
    Public EDIT As Boolean

    Private Sub cmbtype_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtype.Validated
        Try
            If cmbtype.Text.Trim = "AREA" Then
                cmbOldName.Text = ""
                cmbReplace.Text = ""
                If cmbOldName.Text.Trim = "" Then FILLAREA(cmbOldName)
                If cmbReplace.Text.Trim = "" Then FILLAREA(cmbReplace)
            ElseIf cmbtype.Text.Trim = "CITY" Then
                cmbOldName.Text = ""
                cmbReplace.Text = ""
                If cmbOldName.Text.Trim = "" Then fillCITY(cmbOldName, EDIT)
                If cmbReplace.Text.Trim = "" Then fillCITY(cmbReplace, EDIT)
            ElseIf cmbtype.Text.Trim = "COUNTRY" Then
                cmbOldName.Text = ""
                cmbReplace.Text = ""
                If cmbOldName.Text.Trim = "" Then fillCOUNTRY(cmbOldName)
                If cmbReplace.Text.Trim = "" Then fillCOUNTRY(cmbReplace)
            ElseIf cmbtype.Text.Trim = "QUALITY" Then
                cmbOldName.Text = ""
                cmbReplace.Text = ""
                If cmbOldName.Text.Trim = "" Then fillQUALITY(cmbOldName, EDIT)
                If cmbReplace.Text.Trim = "" Then fillQUALITY(cmbReplace, EDIT)
            
            ElseIf cmbtype.Text.Trim = "STATE" Then
                cmbOldName.Text = ""
                cmbReplace.Text = ""
                If cmbOldName.Text.Trim = "" Then fillSTATE(cmbOldName)
                If cmbReplace.Text.Trim = "" Then fillSTATE(cmbReplace)
            ElseIf cmbtype.Text.Trim = "GODOWN" Then
                cmbOldName.Text = ""
                cmbReplace.Text = ""
                If cmbOldName.Text.Trim = "" Then fillGODOWN(cmbOldName, EDIT)
                If cmbReplace.Text.Trim = "" Then fillGODOWN(cmbReplace, EDIT)
            ElseIf cmbtype.Text.Trim = "TAX" Then
                cmbOldName.Text = ""
                cmbReplace.Text = ""
                If cmbOldName.Text.Trim = "" Then filltax(cmbOldName, EDIT)
                If cmbReplace.Text.Trim = "" Then filltax(cmbReplace, EDIT)
            ElseIf cmbtype.Text.Trim = "TYPE" Then
                cmbOldName.Text = ""
                cmbReplace.Text = ""
                If cmbOldName.Text.Trim = "" Then fillTYPE(cmbOldName)
                If cmbReplace.Text.Trim = "" Then fillTYPE(cmbReplace)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If cmbOldName.Text.Trim = cmbReplace.Text.Trim Then
                EP.SetError(cmbReplace, " Please Fill Diff. Value!")
                bln = False
            End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBSAVE.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If


            Cursor.Current = Cursors.WaitCursor
            Dim alParaval As New ArrayList
            Dim intresult As Integer

            alParaval.Add(cmbtype.Text)
            alParaval.Add(cmbOldName.Text)
            alParaval.Add(cmbReplace.Text)
            alParaval.Add(CmpId)
            alParaval.Add(0)
            alParaval.Add(YearId)

            Dim OBJMFG As New ClsCommon()
            OBJMFG.alParaval = alParaval
            intresult = OBJMFG.mergeparameter()
            MsgBox("Parameter Merged Successfully")
            cmbtype.Text = ""
            cmbOldName.Text = ""
            cmbReplace.Text = ""

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub MergeParameter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class