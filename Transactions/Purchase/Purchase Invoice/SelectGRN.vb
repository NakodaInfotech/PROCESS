
Imports System.Windows.Forms
Imports BL

Public Class SelectGRN

    Dim addcol As Integer = 0
    Public DT As New DataTable
    Dim N As Integer = 0
    Dim tempindex, i As Integer
    Dim col As New DataGridViewCheckBoxColumn  'Dim dt As New DataTable
    Public PARTYNAME As String = ""  'for whereclause in fillgrid
    Public MILLNAME As String = ""  'for whereclause in fillgrid
    Public FRMSTRING As String = ""

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectGRN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectGRN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Top = 100
        fillgrid()
        If FRMSTRING = "FINISHED" Then
            GMILLNAME.Caption = "Dyeing Name"
            GTOTALBAGS.Caption = "Total Taka"
            GTOTALWT.Caption = "Total Mtrs"
        ElseIf FRMSTRING = "GREY" Then
            GMILLNAME.Caption = "Delivery At"
            GTOTALBAGS.Caption = "Total Pcs"
            GTOTALWT.Caption = "Total Mtrs"
        ElseIf FRMSTRING = "PROCESS" Then
            GGRNNO.Caption = "Lot No"
            GDATE.Visible = False
            GGODOWN.Visible = False
            GMILLNAME.Visible = False
            GTRANSPORT.Visible = False
            GQUALITY.Visible = False
            GLRNO.Visible = False
            GTOTALBAGS.Caption = "Total Pcs"
            GTOTALWT.Caption = "Total Mtrs"
            GBROKER.Visible = False
        End If
    End Sub

    Sub fillgrid()
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            Dim WHERECLAUSE As String = ""
            If PARTYNAME <> "" Then WHERECLAUSE = WHERECLAUSE & " AND LEDGERS.Acc_cmpname= '" & PARTYNAME & "'"
            If FRMSTRING = "YARN" Or FRMSTRING = "FINISHED" Or FRMSTRING = "GREY" Then
                If MILLNAME <> "" Then WHERECLAUSE = WHERECLAUSE & " AND MILLNAME.Acc_cmpname= '" & MILLNAME & "'"
                If FRMSTRING = "YARN" Then
                    DT = OBJCMN.search(" CAST (0 AS BIT) AS CHK , GRN_DESC.GRN_GRIDSRNO AS GRIDSRNO, GRN.grn_no AS GRNNO, ISNULL(GRN.grn_challanno, '') AS RECNO,LEDGERS.Acc_cmpname AS NAME, GRN.grn_DATE AS DATE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(MILLNAME.Acc_cmpname, '') AS MILLNAME, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, QUALITY_NAME AS QUALITY, GRN_DESC.GRN_LRNO AS LRNO, GRN_DESC.GRN_BAGS - ISNULL(GRN_DESC.GRN_PURBAGS, 0) AS BAGS, ISNULL(GRN_DESC.GRN_WT, 0) - ISNULL(GRN_DESC.GRN_PURWT, 0) AS WT, ISNULL(BROKER.Acc_cmpname, '') AS BROKER,  ISNULL(PURCHASEORDER_DESC.PO_RATE, 0) AS RATE ", "", " GRN INNER JOIN LEDGERS ON GRN.grn_ledgerid = LEDGERS.Acc_id INNER JOIN GODOWNMASTER ON GRN.GRN_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS AS MILLNAME ON GRN.GRN_MILLID = MILLNAME.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON GRN.grn_transledgerid = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON GRN.GRN_BROKERID = BROKER.Acc_id INNER JOIN QUALITYMASTER ON QUALITY_ID = GRN_QUALITYID LEFT OUTER JOIN PURCHASEORDER_DESC ON GRN_DESC.GRN_POSRNO = PURCHASEORDER_DESC.PO_GRIDSRNO AND GRN_DESC.GRN_PONO = PURCHASEORDER_DESC.PO_NO AND GRN_DESC.GRN_YEARID = PURCHASEORDER_DESC.PO_YEARID ", WHERECLAUSE & " AND GRN_TYPE = '" & FRMSTRING & "' AND grn.grn_YEARID = " & YearId & " AND GRN_DESC.GRN_BAGS - ISNULL(GRN_DESC.GRN_PURBAGS, 0) > 0 ")
                ElseIf FRMSTRING = "GREY" Then
                    DT = OBJCMN.search(" CAST (0 AS BIT) AS CHK , GRN_DESC.GRN_GRIDSRNO AS GRIDSRNO, GRN.grn_no AS GRNNO, ISNULL(GRN.grn_challanno, '') AS RECNO,LEDGERS.Acc_cmpname AS NAME, GRN.grn_CHALLANDT AS DATE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(MILLNAME.Acc_cmpname, '') AS MILLNAME, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, GREY_NAME AS QUALITY, GRN_DESC.GRN_LRNO AS LRNO, GRN_DESC.GRN_BAGS AS BAGS, ISNULL(GRN_DESC.GRN_WT, 0) - ISNULL(GRN_DESC.GRN_PURWT, 0) AS WT, ISNULL(BROKER.Acc_cmpname, '') AS BROKER,  ISNULL(PURCHASEORDER_DESC.PO_RATE, 0) AS RATE ", "", " GRN INNER JOIN LEDGERS ON GRN.grn_ledgerid = LEDGERS.Acc_id INNER JOIN GODOWNMASTER ON GRN.GRN_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS AS MILLNAME ON GRN.GRN_MILLID = MILLNAME.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON GRN.grn_transledgerid = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON GRN.GRN_BROKERID = BROKER.Acc_id INNER JOIN GREYQUALITYMASTER ON GREY_ID = GRN_QUALITYID LEFT OUTER JOIN PURCHASEORDER_DESC ON GRN_DESC.GRN_POSRNO = PURCHASEORDER_DESC.PO_GRIDSRNO AND GRN_DESC.GRN_PONO = PURCHASEORDER_DESC.PO_NO AND GRN_DESC.GRN_YEARID = PURCHASEORDER_DESC.PO_YEARID ", WHERECLAUSE & " AND GRN_TYPE = '" & FRMSTRING & "' AND grn.grn_YEARID = " & YearId & "  AND GRN_DESC.GRN_BAGS - ISNULL(GRN_DESC.GRN_PURBAGS, 0) > 0 ")
                Else
                    DT = OBJCMN.search(" CAST (0 AS BIT) AS CHK , GRN_DESC.GRN_GRIDSRNO AS GRIDSRNO, GRN.grn_no AS GRNNO, ISNULL(GRN.grn_challanno, '') AS RECNO,LEDGERS.Acc_cmpname AS NAME, GRN.grn_CHALLANDT AS DATE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(MILLNAME.Acc_cmpname, '') AS MILLNAME, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, GREY_NAME AS QUALITY, GRN_DESC.GRN_LRNO AS LRNO, GRN_DESC.GRN_BAGS AS BAGS, ISNULL(GRN_DESC.GRN_WT, 0) - ISNULL(GRN_DESC.GRN_PURWT, 0) AS WT, ISNULL(BROKER.Acc_cmpname, '') AS BROKER,  ISNULL(PURCHASEORDER_DESC.PO_RATE, 0) AS RATE ", "", " GRN INNER JOIN LEDGERS ON GRN.grn_ledgerid = LEDGERS.Acc_id INNER JOIN GODOWNMASTER ON GRN.GRN_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS AS MILLNAME ON GRN.GRN_MILLID = MILLNAME.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON GRN.grn_transledgerid = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON GRN.GRN_BROKERID = BROKER.Acc_id INNER JOIN GREYQUALITYMASTER ON GREY_ID = GRN_QUALITYID LEFT OUTER JOIN PURCHASEORDER_DESC ON GRN_DESC.GRN_POSRNO = PURCHASEORDER_DESC.PO_GRIDSRNO AND GRN_DESC.GRN_PONO = PURCHASEORDER_DESC.PO_NO AND GRN_DESC.GRN_YEARID = PURCHASEORDER_DESC.PO_YEARID ", WHERECLAUSE & " AND GRN_TYPE = '" & FRMSTRING & "' AND grn.grn_YEARID = " & YearId & "  AND GRN_DESC.GRN_BAGS - ISNULL(GRN_DESC.GRN_PURBAGS, 0) > 0 ")
                End If
            Else
                'WE HAVE ADDED SAREEJOBIN IN THE PROCESS CHGS
                'DT = OBJCMN.search(" CAST (0 AS BIT) AS CHK , GRECDPROCESSOR_LOTNO AS GRNNO, GRECDPROCESSOR_GRIDSRNO AS GRIDSRNO, SUM(GRECDPROCESSOR_TAKA) AS BAGS, SUM(GRECDPROCESSOR_MTRS) AS WT, '' AS RECNO, LEDGERS.Acc_cmpname AS NAME, GETDATE() AS DATE, '' AS GODOWN, '' AS MILLNAME, '' AS TRANSPORT,'' AS QUALITY, '' AS LRNO, '' AS BROKER, 0 as RATE ", "", " GREYRECEIVEDPROCESSING INNER JOIN LEDGERS ON GREYRECEIVEDPROCESSING.GRECDPROCESSOR_LEDGERID = LEDGERS.Acc_id INNER JOIN GREYRECEIVEDPROCESSING_DESC ON GREYRECEIVEDPROCESSING.GRECDPROCESSOR_NO = GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_NO AND GREYRECEIVEDPROCESSING.GRECDPROCESSOR_YEARID = GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_YEARID ", WHERECLAUSE & " AND ISNULL(GREYRECEIVEDPROCESSING.GRECDPROCESSOR_PURDONE,0) = 0 AND GREYRECEIVEDPROCESSING.GRECDPROCESSOR_YEARID = " & YearId & " GROUP BY GRECDPROCESSOR_LOTNO, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_GRIDSRNO, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_LOTNO, LEDGERS.ACC_CMPNAME ORDER BY GRNNO")
                DT = OBJCMN.search(" * ", "", "(SELECT CAST (0 AS BIT) AS CHK , GRECDPROCESSOR_LOTNO AS GRNNO, GRECDPROCESSOR_GRIDSRNO AS GRIDSRNO, SUM(GRECDPROCESSOR_TAKA) AS BAGS, SUM(GRECDPROCESSOR_MTRS) AS WT, '' AS RECNO, LEDGERS.Acc_cmpname AS NAME, GETDATE() AS DATE, '' AS GODOWN, '' AS MILLNAME, '' AS TRANSPORT,'' AS QUALITY, '' AS LRNO, '' AS BROKER, 0 as RATE FROM GREYRECEIVEDPROCESSING INNER JOIN LEDGERS ON GREYRECEIVEDPROCESSING.GRECDPROCESSOR_LEDGERID = LEDGERS.Acc_id INNER JOIN GREYRECEIVEDPROCESSING_DESC ON GREYRECEIVEDPROCESSING.GRECDPROCESSOR_NO = GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_NO AND GREYRECEIVEDPROCESSING.GRECDPROCESSOR_YEARID = GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_YEARID WHERE ISNULL(GREYRECEIVEDPROCESSING.GRECDPROCESSOR_PURDONE,0) = 0 " & WHERECLAUSE & " AND GREYRECEIVEDPROCESSING.GRECDPROCESSOR_YEARID = " & YearId & " GROUP BY GRECDPROCESSOR_LOTNO, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_GRIDSRNO, GREYRECEIVEDPROCESSING_DESC.GRECDPROCESSOR_LOTNO, LEDGERS.ACC_CMPNAME UNION ALL SELECT CAST (0 AS BIT) AS CHK , JI_LOTNO AS GRNNO, JI_GRIDSRNO AS GRIDSRNO, SUM(JI_PCS) AS BAGS, SUM(JI_MTRS) AS WT, '' AS RECNO, LEDGERS.Acc_cmpname AS NAME, GETDATE() AS DATE, '' AS GODOWN, '' AS MILLNAME, '' AS TRANSPORT,'' AS QUALITY, '' AS LRNO, '' AS BROKER, 0 as RATE FROM SAREEJOBIN INNER JOIN LEDGERS ON SAREEJOBIN.JI_LEDGERID = LEDGERS.Acc_id INNER JOIN SAREEJOBIN_DESC ON SAREEJOBIN.JI_NO = SAREEJOBIN_DESC.JI_NO AND SAREEJOBIN.JI_YEARID = SAREEJOBIN_DESC.JI_YEARID WHERE ISNULL(SAREEJOBIN.JI_PURDONE,0) = 0 " & WHERECLAUSE & " AND SAREEJOBIN.JI_YEARID = " & YearId & " GROUP BY JI_LOTNO, SAREEJOBIN_DESC.JI_GRIDSRNO, SAREEJOBIN_DESC.JI_LOTNO, LEDGERS.ACC_CMPNAME) AS T", " ORDER BY T.GRNNO ")
            End If
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
            If FRMSTRING <> "YARN" Then
                Dim n As String = ""
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If n <> "" Then
                            If n = (dtrow("GRNNO")) Then
                                GoTo Line1
                            Else
                                MsgBox("Pls select Only One GRN !")
                                Exit Sub
                            End If
                        End If
Line1:
                        n = (dtrow("GRNNO"))
                    End If
                Next
            End If

            DT.Columns.Add("GRNNO")
            DT.Columns.Add("NAME")
            DT.Columns.Add("DATE")
            DT.Columns.Add("GODOWN")
            DT.Columns.Add("MILLNAME")
            DT.Columns.Add("TRANSPORT")
            DT.Columns.Add("TOTALBAGS")
            DT.Columns.Add("TOTALWT")
            DT.Columns.Add("BROKER")
            DT.Columns.Add("RATE")
            DT.Columns.Add("GRIDSRNO")


            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(Val(dtrow("GRNNO")), dtrow("NAME"), dtrow("DATE"), dtrow("GODOWN"), dtrow("MILLNAME"), dtrow("TRANSPORT"), Val(dtrow("BAGS")), Val(dtrow("WT")), dtrow("BROKER"), Val(dtrow("RATE")), Val(dtrow("GRIDSRNO")))
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