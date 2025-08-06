Imports System.Windows.Forms
Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class SelectGrey
    Public DT As New DataTable
    Public WEAVERNAME As String = ""  'for whereclause in fillgrid
    Public TEMPGREYRECDNO As Integer

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub SelectGrey_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{TAB}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectGrey_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FILLGRID()
    End Sub

    Sub FILLGRID()
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon
            'Dim DT As DataTable = OBJCMN.search(" DISTINCT CAST(0 AS BIT) AS CHK, ISNULL(challanmaster.challan_no, 0) AS CHALLANNO, ISNULL(challanmaster.challan_pono, '') AS PONO, challanmaster.challan_date AS DATE, ISNULL(itemmaster.item_code, '') AS ITEMCODE, ISNULL(challanmaster.CHALLAN_QTY, 0) AS QTY, LEDGERS.ACC_CMPNAME AS NAME", "", " challanmaster INNER JOIN CHALLANMASTER_DESC ON challanmaster.challan_no = CHALLANMASTER_DESC.CHALLAN_NO AND challanmaster.challan_yearid = CHALLANMASTER_DESC.CHALLAN_YEARID INNER JOIN itemmaster ON CHALLANMASTER_DESC.CHALLAN_ITEMID = itemmaster.item_id AND CHALLANMASTER_DESC.CHALLAN_YEARID = itemmaster.item_yearid INNER JOIN LEDGERS ON CHALLAN_LEDGERID = LEDGERS.ACC_ID ", " AND CHALLANMASTER.CHALLAN_DONE = 0 AND CHALLANMASTER.CHALLAN_YEARID = " & YearId)
            Dim DT As DataTable = OBJCMN.search(" CAST(0 AS BIT) AS CHK, ISNULL(GREYRECEIVEDWEAVER.GRECDWEAVER_NO, 0) AS GREYRECNO, GREYRECEIVEDWEAVER.GRECDWEAVER_DATE AS GREYRECDATE, ISNULL(WEAVER.Acc_cmpname, '') AS WEAVER, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANS, ISNULL(GREYRECEIVEDWEAVER.GRECDWEAVER_CHALLANNO, '') AS CHALLANNO, ISNULL(GREYRECEIVEDWEAVER.GRECDWEAVER_TOTALPCS, 0) AS TOTALPCS, ISNULL(GREYRECEIVEDWEAVER.GRECDWEAVER_TOTALMTRS, 0) AS TOTALMTRS", "", "  GREYRECEIVEDWEAVER INNER JOIN GODOWNMASTER ON GREYRECEIVEDWEAVER.GRECDWEAVER_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN LEDGERS AS WEAVER ON GREYRECEIVEDWEAVER.GRECDWEAVER_LEDGERID = WEAVER.Acc_id INNER JOIN LEDGERS AS TRANSPORT ON GREYRECEIVEDWEAVER.GRECDWEAVER_TRANSID = TRANSPORT.Acc_id", " AND WEAVER.Acc_cmpname='" & WEAVERNAME & "' AND GREYRECEIVEDWEAVER.GRECDWEAVER_DONE = 0 AND GREYRECEIVEDWEAVER.GRECDWEAVER_YEARID = " & YearId)
            GridGreyRecd.DataSource = DT
            If DT.Rows.Count > 0 Then
                GridGrey.FocusedRowHandle = GridGrey.RowCount - 1
                GridGrey.TopRowIndex = GridGrey.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try

            Dim n As String = ""
            For i As Integer = 0 To GridGrey.RowCount - 1
                Dim dtrow As DataRow = GridGrey.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If n <> "" Then
                        If n = (dtrow("GREYRECNO")) Then
                            GoTo Line1
                        Else
                            MsgBox("Pls select Only One Sale Invoice !")
                            Exit Sub
                        End If
                    End If
Line1:
                    n = (dtrow("GREYRECNO"))
                End If
            Next

            DT.Columns.Add("GREYRECNO")

            For i As Integer = 0 To GridGrey.RowCount - 1
                Dim dtrow As DataRow = GridGrey.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("GREYRECNO"))
                End If
            Next

            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class