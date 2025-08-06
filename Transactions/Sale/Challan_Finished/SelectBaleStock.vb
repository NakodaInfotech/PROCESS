
Imports BL
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls

Public Class SelectBaleStock
    Public DT As New DataTable
    Public PROCESSORNAME As String = ""
    Public TEMPGODOWNNAME As String = ""
    Public FRMSTRING As String = ""
    Dim BEAMNO As Integer

    Sub fillgrid(ByVal WHERE As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon()
            If PROCESSORNAME <> "" Then WHERE = WHERE & " AND NAME = '" & PROCESSORNAME & "'"

            'DONE BY GULKIT, TO SELECT BALES WHICH ARE AT PROCESSOR
            If FRMSTRING = "BALESTOCKPROCESSOR" Then WHERE = WHERE & " AND GODOWN='" & TEMPGODOWNNAME & "'"

            If TEMPGODOWNNAME <> "" Then WHERE = WHERE & " AND GODOWN='" & TEMPGODOWNNAME & "'"
            Dim DT As DataTable = OBJCMN.search(" CAST (0 AS BIT) AS CHK , NAME, GREYQUALITY, LOTNO,BALENO,PCS, MTRS,YEARID, NO AS FROMNO, SRNO AS  FROMSRNO, TYPE ", "", " BALESTOCK ", WHERE & " AND YEARID = " & YearId)
            gridbilldetails.DataSource = DT

            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            Dim TEMPLOTNO As Integer = 0
            Dim ISUNIQUELOTNO As Boolean = True
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If TEMPLOTNO = 0 Then
                        TEMPLOTNO = Val(dtrow("LOTNO"))
                    Else
                        'CHECK WHETHER IT IS OF SAME LOTNO OR NO
                        If Val(dtrow("LOTNO")) <> TEMPLOTNO Then
                            ISUNIQUELOTNO = False
                            Exit For
                        End If
                    End If
                End If
            Next
            If Not (ISUNIQUELOTNO) Then
                MsgBox("Please select Bales of same Lot No Only", MsgBoxStyle.Critical)
                Exit Sub
            End If


            DT.Columns.Add("GREYQUALITY")
            DT.Columns.Add("LOTNO")
            DT.Columns.Add("BALENO")
            DT.Columns.Add("PCS")
            DT.Columns.Add("MTRS")
            DT.Columns.Add("FROMNO")
            DT.Columns.Add("FROMSRNO")
            DT.Columns.Add("TYPE")


            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("GREYQUALITY"), dtrow("LOTNO"), dtrow("BALENO"), dtrow("PCS"), dtrow("MTRS"), dtrow("FROMNO"), dtrow("FROMSRNO"), dtrow("TYPE"))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SelectBaleStock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectBaleStock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillgrid("")
    End Sub

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
End Class