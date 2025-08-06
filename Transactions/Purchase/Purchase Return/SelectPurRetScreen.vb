
Imports BL

Public Class SelectPurRetScreen

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public FRMSTRING As String = ""

    Private Sub SelectPurRetScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("REGISTER_NAME AS REGNAME, SCREENNAME AS FRMSTRING", "", "PURCHASERETURNREGCONFIG INNER JOIN REGISTERMASTER ON REGISTER_ID = REGISTERID", " AND YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                For Each DTROW As DataRow In DT.Rows
                    GRIDREGISTER.Rows.Add(DTROW("REGNAME"), DTROW("FRMSTRING"))
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SHOWFORM()
        Try
            If FRMSTRING = "ADDPURRETURN" Then
                Dim OBJPUR As New PurchaseReturn
                OBJPUR.MdiParent = MDIMain
                OBJPUR.SELECTEDREG = GRIDREGISTER.CurrentRow.Cells(GREGNAME.Index).Value
                OBJPUR.PURTYPE = GRIDREGISTER.CurrentRow.Cells(GFRMSTRING.Index).Value
                OBJPUR.Show()
                Me.Close()
            Else
                Dim OBJPUR As New PurchaseReturnDetails
                OBJPUR.MdiParent = MDIMain
                OBJPUR.SELECTEDREG = GRIDREGISTER.CurrentRow.Cells(GREGNAME.Index).Value
                OBJPUR.PURTYPE = GRIDREGISTER.CurrentRow.Cells(GFRMSTRING.Index).Value
                OBJPUR.Show()
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDREGISTER_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDREGISTER.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Sub
            SHOWFORM()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDREGISTER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDREGISTER.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If GRIDREGISTER.CurrentRow.Index < 0 Then Exit Sub
                SHOWFORM()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class