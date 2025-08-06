
Imports System.Windows.Forms
Imports BL

Public Class SelectStock

    Dim addcol As Integer = 0
    Public DT As New DataTable
    Dim N As Integer = 0
    Dim tempindex, i As Integer
    Dim col As New DataGridViewCheckBoxColumn  'Dim dt As New DataTable
    Public ENQname As String = ""  'for whereclause in fillgrid
    Public TYPE As String = ""

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectGRNforPurchase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectGRNforPurchase_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Top = 100
        fillgrid()
    End Sub

    Sub fillgrid(Optional ByVal where As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            ''DT = OBJCMN.search(" CAST (0 AS BIT) AS CHK ,GRN.grn_no AS GRN, GRN.grn_date AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(GRN.grn_pono, '') AS PONO, GRN.grn_podate AS PODATE, ISNULL(GRN.grn_challanno, '') AS CHALLAN,GRN.grn_challandt AS CHALLANDATE,ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(BROKER.Acc_cmpname, '') AS BROKER, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, ISNULL(MILLNAME.Acc_cmpname, '') AS MILLNAME, GRN_DESC.GRN_GRIDSRNO AS SRNO, ISNULL(TYPEMASTER.TYPE_name, '') AS TYPE, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(GRN_DESC.GRN_COUNT, 0) AS COUNT, ISNULL(GRN_DESC.GRN_BAGS - GRN_DESC.GRN_OUTBAGS, 0) AS BAGS, ISNULL(GRN_DESC.GRN_WT, 0) AS WT, ISNULL(GRN_DESC.GRN_LRNO, '') AS LRNO, GRN_DESC.GRN_LRDATE AS LRDATE, ISNULL(GRN_DESC.GRN_NARRATION, '') AS NARRATION ", "", " GRN INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID INNER JOIN LEDGERS ON GRN.grn_ledgerid = LEDGERS.Acc_id AND GRN.grn_yearid = LEDGERS.Acc_yearid LEFT OUTER JOIN LEDGERS AS BROKER ON GRN.grn_yearid = BROKER.Acc_yearid AND GRN.GRN_BROKERID = BROKER.Acc_id LEFT OUTER JOIN QUALITYMASTER ON GRN_DESC.GRN_YEARID = QUALITYMASTER.QUALITY_YEARID AND GRN_DESC.GRN_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN TYPEMASTER ON GRN_DESC.GRN_YEARID = TYPEMASTER.TYPE_yearid AND GRN_DESC.GRN_TYPEID = TYPEMASTER.TYPE_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON GRN.grn_yearid = TRANSPORT.Acc_yearid AND GRN.grn_transledgerid = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS MILLNAME ON GRN.grn_yearid = MILLNAME.Acc_yearid AND GRN.GRN_MILLID = MILLNAME.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GRN.grn_yearid = GODOWNMASTER.GODOWN_YEARID AND GRN.GRN_GODOWNID = GODOWNMASTER.GODOWN_ID ", " AND GRN_DESC.GRN_GRIDDONE=0  AND (GRN_DESC.GRN_BAGS - GRN_DESC.GRN_OUTBAGS) > 0 AND grn.grn_YEARID = " & YearId & where & "  ORDER BY GRN.grn_no")
            DT = OBJCMN.search("CAST (0 AS BIT) AS CHK ,GRN,DATE,NAME,PONO,PODATE, CHALLAN, CHALLANDATE, GODOWN, BROKER, TRANSPORT, MILLNAME,SRNO,TYPE,QUALITY,COUNT,BAGS,WT,LRNO,LRDATE,NARRATION ", "", " STOCKVIEW", " AND BAGS > 0 AND YEARID = " & YearId)

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

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            If TYPE <> "GDN" Then
                Dim n As String = ""
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If n <> "" Then
                            If n = (dtrow("LRNO")) Then
                                GoTo Line1
                            Else
                                MsgBox("select only one LR No !")
                                Exit Sub
                            End If
                        End If
Line1:
                        n = (dtrow("LRNO"))
                    End If
                Next
            End If

            DT.Columns.Add("GRN")
            DT.Columns.Add("DATE")
            DT.Columns.Add("NAME")
            DT.Columns.Add("PONO")
            DT.Columns.Add("PODATE")
            DT.Columns.Add("CHALLAN")
            DT.Columns.Add("CHALLANDATE")
            DT.Columns.Add("GODOWN")
            DT.Columns.Add("BROKER")
            DT.Columns.Add("TRANSPORT")
            DT.Columns.Add("MILLNAME")
            DT.Columns.Add("SRNO")
            DT.Columns.Add("TYPE")
            DT.Columns.Add("QUALITY")
            DT.Columns.Add("COUNT")
            DT.Columns.Add("BAGS")
            DT.Columns.Add("WT")
            DT.Columns.Add("LRNO")
            DT.Columns.Add("LRDATE")
            DT.Columns.Add("NARRATION")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("GRN"), dtrow("DATE"), dtrow("NAME"), dtrow("PONO"), dtrow("PODATE"), dtrow("CHALLAN"), dtrow("CHALLANDATE"), dtrow("GODOWN"), dtrow("BROKER"), dtrow("TRANSPORT"), dtrow("MILLNAME"), dtrow("SRNO"), dtrow("TYPE"), dtrow("QUALITY"), dtrow("COUNT"), dtrow("BAGS"), dtrow("WT"), dtrow("LRNO"), dtrow("LRDATE"), dtrow("NARRATION"))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

End Class