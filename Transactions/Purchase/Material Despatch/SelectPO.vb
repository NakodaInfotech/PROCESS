
Imports BL
Imports System.Windows.Forms

Public Class SelectPO

    Public PONO As Integer = 0
    Dim tempindex, i As Integer
    Dim ADDCOL As Boolean = False
    Public PARTYNAME As String = ""
    Public MILLNAME As String = ""
    Public TYPE As String = ""
    Dim col As New DataGridViewCheckBoxColumn
    Public DT As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectPO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectPO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Top = 100
        Me.Text = " Select PO"
        fillgrid()
    End Sub

    Sub fillgrid()
        Try
            Cursor.Current = Cursors.WaitCursor

            Dim where As String = ""
            If PONO <> 0 Then where = " AND T.[PO No]= " & PONO
            If PARTYNAME <> "" Then where = where & " AND T.[Supplier Name]= '" & PARTYNAME & "'"
            If MILLNAME <> "" Then where = where & " AND T.[Mill Name]= '" & MILLNAME & "'"

            Dim objclspreq As New ClsCommon()
            Dim dt As DataTable
            'dt = objclspreq.search(" PURCHASEORDER.PO_NO AS [Sr No.], ISNULL(LEDGERS.Acc_cmpname, '') AS Name, PURCHASEORDER.PO_DATE AS Date,PURCHASEORDER_DESC.PO_GRIDSRNO AS GRIDSRNO, ISNULL(ITEMMASTER.item_name, '') AS [Item Name], ISNULL(QUALITYMASTER.QUALITY_name, '') AS Quality, ISNULL(PURCHASEORDER_DESC.PO_GRIDREMARKS, '') AS Description, ISNULL(PURCHASEORDER_DESC.PO_REED, '') AS Reed, ISNULL(PURCHASEORDER_DESC.PO_PICK, '') AS Pick, ISNULL(COLORMASTER.COLOR_name, '') AS Color, (PURCHASEORDER_DESC.PO_QTY - PURCHASEORDER_DESC.PO_RECDQTY) AS Qty,PURCHASEORDER_DESC.PO_CUT AS Cut, ROUND((PURCHASEORDER_DESC.PO_CUT * (PURCHASEORDER_DESC.PO_QTY - PURCHASEORDER_DESC.PO_RECDQTY)),2) AS Mtrs  , ISNULL(UNITMASTER.unit_abbr, '') AS Unit, ISNULL(LEDGERS_1.Acc_cmpname,'') AS TONAME, PO_DUEDATE AS DUEDATE, ISNULL(GROUPMASTER.group_name,'') AS GROUPNAME ", "", "   PURCHASEORDER INNER JOIN PURCHASEORDER_DESC ON PURCHASEORDER.PO_CMPID = PURCHASEORDER_DESC.PO_CMPID AND PURCHASEORDER.PO_LOCATIONID = PURCHASEORDER_DESC.PO_LOCATIONID AND PURCHASEORDER.PO_YEARID = PURCHASEORDER_DESC.PO_YEARID AND PURCHASEORDER.PO_NO = PURCHASEORDER_DESC.PO_NO INNER JOIN LEDGERS ON PURCHASEORDER.PO_CMPID = LEDGERS.Acc_cmpid AND PURCHASEORDER.PO_LOCATIONID = LEDGERS.Acc_locationid AND PURCHASEORDER.PO_YEARID = LEDGERS.Acc_yearid AND PURCHASEORDER.PO_LEDGERID = LEDGERS.Acc_id INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id AND LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON PURCHASEORDER_DESC.PO_TOLEDGERID = LEDGERS_1.Acc_id AND PURCHASEORDER_DESC.PO_CMPID = LEDGERS_1.Acc_cmpid AND PURCHASEORDER_DESC.PO_LOCATIONID = LEDGERS_1.Acc_locationid AND PURCHASEORDER_DESC.PO_YEARID = LEDGERS_1.Acc_yearid LEFT OUTER JOIN UNITMASTER ON PURCHASEORDER_DESC.PO_YEARID = UNITMASTER.unit_yearid AND PURCHASEORDER_DESC.PO_LOCATIONID = UNITMASTER.unit_locationid AND PURCHASEORDER_DESC.PO_CMPID = UNITMASTER.unit_cmpid AND PURCHASEORDER_DESC.PO_QTYUNITID = UNITMASTER.unit_id LEFT OUTER JOIN COLORMASTER ON PURCHASEORDER_DESC.PO_YEARID = COLORMASTER.COLOR_yearid AND PURCHASEORDER_DESC.PO_LOCATIONID = COLORMASTER.COLOR_locationid AND PURCHASEORDER_DESC.PO_CMPID = COLORMASTER.COLOR_cmpid AND PURCHASEORDER_DESC.PO_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON PURCHASEORDER_DESC.PO_YEARID = QUALITYMASTER.QUALITY_yearid AND PURCHASEORDER_DESC.PO_LOCATIONID = QUALITYMASTER.QUALITY_locationid AND PURCHASEORDER_DESC.PO_CMPID = QUALITYMASTER.QUALITY_cmpid AND PURCHASEORDER_DESC.PO_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON PURCHASEORDER_DESC.PO_ITEMID = ITEMMASTER.item_id AND PURCHASEORDER_DESC.PO_CMPID = ITEMMASTER.item_cmpid AND PURCHASEORDER_DESC.PO_LOCATIONID = ItemMaster.item_locationid And PURCHASEORDER_DESC.PO_YEARID = ItemMaster.item_yearid ", " and PURCHASEORDER.PO_done='False' AND (PURCHASEORDER_DESC.PO_QTY - PURCHASEORDER_DESC.PO_RECDQTY) > 0 AND PO_VERIFIED = 'True' " & where & "  AND PURCHASEORDER.PO_CMPID = " & CmpId & " AND PURCHASEORDER.PO_LOCATIONID = " & Locationid & " AND PURCHASEORDER.PO_YEARID = " & YearId & "  order by PURCHASEORDER.PO_no")
            If TYPE = "YARN" Then
                'dt = objclspreq.search(" PURCHASEORDER.PO_NO AS [PO No], PURCHASEORDER.PO_DATE AS [PO Date],  ISNULL(LEDGERS.Acc_cmpname,'') AS [Supplier Name], ISNULL(MILLLEDGERS.Acc_cmpname, '') AS [Mill Name], ISNULL(GROUPMASTER.group_name, '') AS [Group Name],'' AS [Type], ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS [Quality], ISNULL(PURCHASEORDER_DESC.PO_count, 0) AS [Count], ISNULL((PURCHASEORDER_DESC.PO_BAGS - PURCHASEORDER_DESC.PO_OUTBAGS), 0) AS [Bags], ISNULL((PURCHASEORDER_DESC.PO_WT - PURCHASEORDER_DESC.PO_OUTWT), 0) AS [Wt], ISNULL(PURCHASEORDER_DESC.PO_RATE,0) AS [Rate], ISNULL(PURCHASEORDER_DESC.PO_NARRATION, '') AS [Description], ISNULL(BROKER.Acc_cmpname, '') AS [Broker], ISNULL(TRANSPORT.Acc_cmpname, '') AS [Transport Name],ISNULL(PURCHASEORDER_DESC.PO_GRIDSRNO, 0) AS [Gridsrno]", "", "   PURCHASEORDER INNER JOIN PURCHASEORDER_DESC ON PURCHASEORDER.PO_NO = PURCHASEORDER_DESC.PO_NO AND PURCHASEORDER.PO_YEARID = PURCHASEORDER_DESC.PO_YEARID LEFT OUTER JOIN LEDGERS ON PURCHASEORDER.PO_LEDGERID = LEDGERS.Acc_id AND PURCHASEORDER.PO_YEARID = LEDGERS.Acc_yearid LEFT OUTER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid  LEFT OUTER JOIN LEDGERS AS TRANSPORT ON PURCHASEORDER.PO_TRANS1ID = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON PURCHASEORDER.PO_BROKERID = BROKER.Acc_id LEFT OUTER JOIN QUALITYMASTER ON PURCHASEORDER_DESC.PO_QUALITYID = QUALITYMASTER.QUALITY_ID INNER JOIN LEDGERS AS MILLLEDGERS ON PO_MILLID = MILLLEDGERS.ACC_ID", " and (PURCHASEORDER_DESC.PO_BAGS - PURCHASEORDER_DESC.PO_OUTBAGS) > 0 AND PO_VERIFIED = 'True' " & where & "  AND PURCHASEORDER.PO_CMPID = " & CmpId & " AND PURCHASEORDER.PO_LOCATIONID = " & Locationid & " AND PURCHASEORDER.PO_YEARID = " & YearId & " AND PO_TYPE = '" & TYPE & "' order by PURCHASEORDER.PO_no")
                dt = objclspreq.search(" * ", "", " (SELECT OPENINGPURCHASEORDER.OPPO_NO AS [PO No], OPENINGPURCHASEORDER.OPPO_DATE AS [PO Date],  ISNULL(LEDGERS.Acc_cmpname,'') AS [Supplier Name], ISNULL(MILLLEDGERS.Acc_cmpname, '') AS [Mill Name], ISNULL(GROUPMASTER.group_name, '') AS [Group Name],'OPENING' AS [Type], ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS [Quality], ISNULL(OPENINGPURCHASEORDER_DESC.OPPO_count, 0) AS [Count], ISNULL((OPENINGPURCHASEORDER_DESC.OPPO_BAGS - OPENINGPURCHASEORDER_DESC.OPPO_OUTBAGS), 0) AS [Bags], ISNULL((OPENINGPURCHASEORDER_DESC.OPPO_WT - OPENINGPURCHASEORDER_DESC.OPPO_OUTWT), 0) AS [Wt], ISNULL(OPENINGPURCHASEORDER_DESC.OPPO_RATE,0) AS [Rate], ISNULL(OPENINGPURCHASEORDER_DESC.OPPO_NARRATION, '') AS [Description], ISNULL(BROKER.Acc_cmpname, '') AS [Broker], ISNULL(TRANSPORT.Acc_cmpname, '') AS [Transport Name],ISNULL(OPENINGPURCHASEORDER_DESC.OPPO_GRIDSRNO, 0) AS [Gridsrno], OPPO_TYPE AS POTYPE FROM OPENINGPURCHASEORDER INNER JOIN OPENINGPURCHASEORDER_DESC ON OPENINGPURCHASEORDER.OPPO_NO = OPENINGPURCHASEORDER_DESC.OPPO_NO AND OPENINGPURCHASEORDER.OPPO_YEARID = OPENINGPURCHASEORDER_DESC.OPPO_YEARID LEFT OUTER JOIN LEDGERS ON OPENINGPURCHASEORDER.OPPO_LEDGERID = LEDGERS.Acc_id AND OPENINGPURCHASEORDER.OPPO_YEARID = LEDGERS.Acc_yearid LEFT OUTER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid  LEFT OUTER JOIN LEDGERS AS TRANSPORT ON OPENINGPURCHASEORDER.OPPO_TRANS1ID = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON OPENINGPURCHASEORDER.OPPO_BROKERID = BROKER.Acc_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGPURCHASEORDER_DESC.OPPO_QUALITYID = QUALITYMASTER.QUALITY_ID INNER JOIN LEDGERS AS MILLLEDGERS ON OPPO_MILLID = MILLLEDGERS.ACC_ID WHERE (OPENINGPURCHASEORDER_DESC.OPPO_BAGS - OPENINGPURCHASEORDER_DESC.OPPO_OUTBAGS) > 0 AND OPPO_VERIFIED = 'True' AND OPENINGPURCHASEORDER.OPPO_YEARID = " & YearId & " UNION ALL SELECT PURCHASEORDER.PO_NO AS [PO No], PURCHASEORDER.PO_DATE AS [PO Date],  ISNULL(LEDGERS.Acc_cmpname,'') AS [Supplier Name], ISNULL(MILLLEDGERS.Acc_cmpname, '') AS [Mill Name], ISNULL(GROUPMASTER.group_name, '') AS [Group Name],'PO' AS [Type], ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS [Quality], ISNULL(PURCHASEORDER_DESC.PO_count, 0) AS [Count], ISNULL((PURCHASEORDER_DESC.PO_BAGS - PURCHASEORDER_DESC.PO_OUTBAGS), 0) AS [Bags], ISNULL((PURCHASEORDER_DESC.PO_WT - PURCHASEORDER_DESC.PO_OUTWT), 0) AS [Wt], ISNULL(PURCHASEORDER_DESC.PO_RATE,0) AS [Rate], ISNULL(PURCHASEORDER_DESC.PO_NARRATION, '') AS [Description], ISNULL(BROKER.Acc_cmpname, '') AS [Broker], ISNULL(TRANSPORT.Acc_cmpname, '') AS [Transport Name],ISNULL(PURCHASEORDER_DESC.PO_GRIDSRNO, 0) AS [Gridsrno], PO_TYPE  AS POTYPE FROM PURCHASEORDER INNER JOIN PURCHASEORDER_DESC ON PURCHASEORDER.PO_NO = PURCHASEORDER_DESC.PO_NO AND PURCHASEORDER.PO_YEARID = PURCHASEORDER_DESC.PO_YEARID LEFT OUTER JOIN LEDGERS ON PURCHASEORDER.PO_LEDGERID = LEDGERS.Acc_id AND PURCHASEORDER.PO_YEARID = LEDGERS.Acc_yearid LEFT OUTER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid  LEFT OUTER JOIN LEDGERS AS TRANSPORT ON PURCHASEORDER.PO_TRANS1ID = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON PURCHASEORDER.PO_BROKERID = BROKER.Acc_id LEFT OUTER JOIN QUALITYMASTER ON PURCHASEORDER_DESC.PO_QUALITYID = QUALITYMASTER.QUALITY_ID INNER JOIN LEDGERS AS MILLLEDGERS ON PO_MILLID = MILLLEDGERS.ACC_ID WHERE (PURCHASEORDER_DESC.PO_BAGS - PURCHASEORDER_DESC.PO_OUTBAGS) > 0 AND PO_VERIFIED = 'True' AND PURCHASEORDER.PO_YEARID = " & YearId & ") AS T ", where & " AND T.POTYPE = '" & TYPE & "' ORDER BY T.[PO No]")
            Else
                'dt = objclspreq.search(" PURCHASEORDER.PO_NO AS [PO No], PURCHASEORDER.PO_DATE AS [PO Date],  ISNULL(LEDGERS.Acc_cmpname,'') AS [Supplier Name], ISNULL(MILLLEDGERS.Acc_cmpname, '') AS [Delivery At], ISNULL(GROUPMASTER.group_name, '') AS [Group Name],'' AS [Type], ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS [Quality], ISNULL(PURCHASEORDER_DESC.PO_count, 0) AS [Count], ISNULL((PURCHASEORDER_DESC.PO_BAGS - PURCHASEORDER_DESC.PO_OUTBAGS), 0) AS [Bags], ISNULL((PURCHASEORDER_DESC.PO_WT - PURCHASEORDER_DESC.PO_OUTWT), 0) AS [Wt], ISNULL(PURCHASEORDER_DESC.PO_RATE,0) AS [Rate], ISNULL(PURCHASEORDER_DESC.PO_NARRATION, '') AS [Description], ISNULL(BROKER.Acc_cmpname, '') AS [Broker], ISNULL(TRANSPORT.Acc_cmpname, '') AS [Transport Name],ISNULL(PURCHASEORDER_DESC.PO_GRIDSRNO, 0) AS [Gridsrno]", "", "   PURCHASEORDER INNER JOIN PURCHASEORDER_DESC ON PURCHASEORDER.PO_NO = PURCHASEORDER_DESC.PO_NO AND PURCHASEORDER.PO_YEARID = PURCHASEORDER_DESC.PO_YEARID LEFT OUTER JOIN LEDGERS ON PURCHASEORDER.PO_LEDGERID = LEDGERS.Acc_id AND PURCHASEORDER.PO_YEARID = LEDGERS.Acc_yearid LEFT OUTER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid  LEFT OUTER JOIN LEDGERS AS TRANSPORT ON PURCHASEORDER.PO_TRANS1ID = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON PURCHASEORDER.PO_BROKERID = BROKER.Acc_id LEFT OUTER JOIN GREYQUALITYMASTER ON PURCHASEORDER_DESC.PO_QUALITYID = GREYQUALITYMASTER.GREY_ID INNER JOIN LEDGERS AS MILLLEDGERS ON PO_MILLID = MILLLEDGERS.ACC_ID", " and (PURCHASEORDER_DESC.PO_BAGS - PURCHASEORDER_DESC.PO_OUTBAGS) > 0 AND PO_VERIFIED = 'True' " & where & "  AND PURCHASEORDER.PO_CMPID = " & CmpId & " AND PURCHASEORDER.PO_LOCATIONID = " & Locationid & " AND PURCHASEORDER.PO_YEARID = " & YearId & " AND PO_TYPE = '" & TYPE & "' order by PURCHASEORDER.PO_no")
                dt = objclspreq.search(" * ", "", " (SELECT OPENINGPURCHASEORDER.OPPO_NO AS [PO No], OPENINGPURCHASEORDER.OPPO_DATE AS [PO Date],  ISNULL(LEDGERS.Acc_cmpname,'') AS [Supplier Name], ISNULL(MILLLEDGERS.Acc_cmpname, '') AS [Mill Name], ISNULL(GROUPMASTER.group_name, '') AS [Group Name],'OPENING' AS [Type], ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS [Quality], ISNULL(OPENINGPURCHASEORDER_DESC.OPPO_count, 0) AS [Count], ISNULL((OPENINGPURCHASEORDER_DESC.OPPO_BAGS - OPENINGPURCHASEORDER_DESC.OPPO_OUTBAGS), 0) AS [Bags], ISNULL((OPENINGPURCHASEORDER_DESC.OPPO_WT - OPENINGPURCHASEORDER_DESC.OPPO_OUTWT), 0) AS [Wt], ISNULL(OPENINGPURCHASEORDER_DESC.OPPO_RATE,0) AS [Rate], ISNULL(OPENINGPURCHASEORDER_DESC.OPPO_NARRATION, '') AS [Description], ISNULL(BROKER.Acc_cmpname, '') AS [Broker], ISNULL(TRANSPORT.Acc_cmpname, '') AS [Transport Name],ISNULL(OPENINGPURCHASEORDER_DESC.OPPO_GRIDSRNO, 0) AS [Gridsrno], OPPO_TYPE AS POTYPE FROM OPENINGPURCHASEORDER INNER JOIN OPENINGPURCHASEORDER_DESC ON OPENINGPURCHASEORDER.OPPO_NO = OPENINGPURCHASEORDER_DESC.OPPO_NO AND OPENINGPURCHASEORDER.OPPO_YEARID = OPENINGPURCHASEORDER_DESC.OPPO_YEARID LEFT OUTER JOIN LEDGERS ON OPENINGPURCHASEORDER.OPPO_LEDGERID = LEDGERS.Acc_id AND OPENINGPURCHASEORDER.OPPO_YEARID = LEDGERS.Acc_yearid LEFT OUTER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid  LEFT OUTER JOIN LEDGERS AS TRANSPORT ON OPENINGPURCHASEORDER.OPPO_TRANS1ID = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON OPENINGPURCHASEORDER.OPPO_BROKERID = BROKER.Acc_id LEFT OUTER JOIN GREYQUALITYMASTER ON OPENINGPURCHASEORDER_DESC.OPPO_QUALITYID = GREYQUALITYMASTER.GREY_ID INNER JOIN LEDGERS AS MILLLEDGERS ON OPPO_MILLID = MILLLEDGERS.ACC_ID WHERE (OPENINGPURCHASEORDER_DESC.OPPO_BAGS - OPENINGPURCHASEORDER_DESC.OPPO_OUTBAGS) > 0 AND OPPO_VERIFIED = 'True' AND OPENINGPURCHASEORDER.OPPO_YEARID = " & YearId & " UNION ALL SELECT PURCHASEORDER.PO_NO AS [PO No], PURCHASEORDER.PO_DATE AS [PO Date],  ISNULL(LEDGERS.Acc_cmpname,'') AS [Supplier Name], ISNULL(MILLLEDGERS.Acc_cmpname, '') AS [Mill Name], ISNULL(GROUPMASTER.group_name, '') AS [Group Name],'PO' AS [Type], ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS [Quality], ISNULL(PURCHASEORDER_DESC.PO_count, 0) AS [Count], ISNULL((PURCHASEORDER_DESC.PO_BAGS - PURCHASEORDER_DESC.PO_OUTBAGS), 0) AS [Bags], ISNULL((PURCHASEORDER_DESC.PO_WT - PURCHASEORDER_DESC.PO_OUTWT), 0) AS [Wt], ISNULL(PURCHASEORDER_DESC.PO_RATE,0) AS [Rate], ISNULL(PURCHASEORDER_DESC.PO_NARRATION, '') AS [Description], ISNULL(BROKER.Acc_cmpname, '') AS [Broker], ISNULL(TRANSPORT.Acc_cmpname, '') AS [Transport Name],ISNULL(PURCHASEORDER_DESC.PO_GRIDSRNO, 0) AS [Gridsrno], PO_TYPE  AS POTYPE FROM PURCHASEORDER INNER JOIN PURCHASEORDER_DESC ON PURCHASEORDER.PO_NO = PURCHASEORDER_DESC.PO_NO AND PURCHASEORDER.PO_YEARID = PURCHASEORDER_DESC.PO_YEARID LEFT OUTER JOIN LEDGERS ON PURCHASEORDER.PO_LEDGERID = LEDGERS.Acc_id AND PURCHASEORDER.PO_YEARID = LEDGERS.Acc_yearid LEFT OUTER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid  LEFT OUTER JOIN LEDGERS AS TRANSPORT ON PURCHASEORDER.PO_TRANS1ID = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS BROKER ON PURCHASEORDER.PO_BROKERID = BROKER.Acc_id LEFT OUTER JOIN GREYQUALITYMASTER ON PURCHASEORDER_DESC.PO_QUALITYID = GREYQUALITYMASTER.GREY_ID INNER JOIN LEDGERS AS MILLLEDGERS ON PO_MILLID = MILLLEDGERS.ACC_ID WHERE (PURCHASEORDER_DESC.PO_BAGS - PURCHASEORDER_DESC.PO_OUTBAGS) > 0 AND PO_VERIFIED = 'True' AND PURCHASEORDER.PO_YEARID = " & YearId & ") AS T ", where & " AND T.POTYPE = '" & TYPE & "' ORDER BY T.[PO No]")
            End If

            If dt.Rows.Count > 0 Then

                gridquotation.DataSource = dt
                'Dim col As New DataGridViewCheckBoxColumn
                If ADDCOL = False Then
                    gridquotation.Columns.Insert(0, col)
                    ADDCOL = True
                End If
                'gridquotation.Columns(0).DataGridView. = 70
                gridquotation.Columns(0).Width = 50 'CHECK BOK
                gridquotation.Columns(1).Width = 50  'PONO
                gridquotation.Columns(2).Width = 80  'DATE
                gridquotation.Columns(3).Width = 150  'NAME
                gridquotation.Columns(4).Width = 160  'MILLNAME
                gridquotation.Columns(5).Visible = False 'GROUPNAME

                gridquotation.Columns(6).Width = 70 'TYPE
                gridquotation.Columns(7).Width = 150 'Quality
                'gridquotation.Columns(6).Width = 150  'desc
                'gridquotation.Columns(7).Width = 70  'Quality

                gridquotation.Columns(8).Visible = False

                gridquotation.Columns(9).Width = 60 'BAGS
                gridquotation.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                gridquotation.Columns(9).DefaultCellStyle.Format = "N"

                gridquotation.Columns(10).Width = 70 'WT
                gridquotation.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                gridquotation.Columns(10).DefaultCellStyle.Format = "N3"

                gridquotation.Columns(11).Width = 70 'Rate
                gridquotation.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                gridquotation.Columns(11).DefaultCellStyle.Format = "N2"

                gridquotation.Columns(12).Visible = False  'DESC
                gridquotation.Columns(13).Width = 150 'BROKER
                gridquotation.Columns(14).Visible = False 'TRANSPORT
                gridquotation.Columns(15).Visible = False 'GRIDSRNO
                gridquotation.Columns(16).Visible = False  'POTYPE




                gridquotation.FirstDisplayedScrollingRowIndex = gridquotation.RowCount - 1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default

        End Try
    End Sub

    Private Sub txtname_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearch.Validated
        If cmbselect.Text.Trim <> "" Then
            Dim rowno, b As Integer
            gridquotation.Columns.Clear()
            fillgrid()
            rowno = 0
            For b = 1 To gridquotation.RowCount
                If cmbselect.Text = "Sr No." Then
                    txttempname.Text = gridquotation.Item(1, rowno).Value
                ElseIf cmbselect.Text = "Name" Then
                    txttempname.Text = gridquotation.Item(2, rowno).Value.ToString()
                End If

                'txttempname.Text = gridquotation.Item(0, rowno).Value.ToString()
                txttempname.SelectionStart = 0
                txttempname.SelectionLength = txtsearch.TextLength
                If LCase(txtsearch.Text.Trim) <> LCase(txttempname.SelectedText.Trim) Then
                    gridquotation.Rows.RemoveAt(rowno)
                Else
                    rowno = rowno + 1
                End If
            Next

        End If
    End Sub

    Private Sub gridquotation_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridquotation.CellClick
        Dim N As String = ""
        Dim POTYPE As String = ""
        Dim tempindex As Integer
        Dim i As Integer

        'CHECKING SIMILAR LOCATION
        For i = 0 To gridquotation.RowCount - 1
            With gridquotation.Rows(i).Cells(0)
                If .Value = True Then
                    N = gridquotation.Item(1, i).Value.ToString
                    POTYPE = gridquotation.Item(6, i).Value.ToString
                End If
            End With
        Next

        If e.RowIndex >= 0 Then
            With gridquotation.Rows(e.RowIndex).Cells(0)
                If .Value = True Then
                    .Value = False
                Else
                    If ((gridquotation.Item(1, e.RowIndex).Value.ToString = N) And (gridquotation.Item(6, e.RowIndex).Value.ToString = POTYPE)) Or N = Nothing Then
                        .Value = True
                        tempindex = e.RowIndex
                    End If
                End If
            End With
        End If
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            DT.Columns.Add("PONO")
            DT.Columns.Add("GRIDSRNO")
            DT.Columns.Add("TYPE")
         

            For Each row As DataGridViewRow In gridquotation.Rows
                If row.Cells(0).Value = True Then
                    DT.Rows.Add(row.Cells(1).Value, row.Cells(15).Value, row.Cells(6).Value)
                End If
            Next
            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridquotation_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridquotation.CellDoubleClick
        Try
            cmdok_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class