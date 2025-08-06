
Imports BL

Public Class SelectOrder

    Public DT As New DataTable
    Public PARTYNAME As String = ""  'for whereclause in fillgrid
    Public TYPE As String = ""

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectGDN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Top = 100
        fillgrid()
    End Sub

    Sub fillgrid(Optional ByVal where As String = "")
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" * ", "", " (SELECT CAST(0 AS BIT) AS CHK, OPENINGSALEORDER.OPSO_NO AS ORDERNO, OPENINGSALEORDER_DESC.OPSO_GRIDSRNO AS ORDERSRNO, OPENINGSALEORDER.OPSO_DATE AS DATE, OPENINGSALEORDER.OPSO_DELDATE AS DELDATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGER.Acc_cmpname, '') AS AGENT, CASE WHEN ISNULL(OPSO_TYPE, 'GREY') = 'GREY' THEN GREY_NAME ELSE QUALITY_NAME END AS QUALITY, ISNULL(OPENINGSALEORDER_DESC.OPSO_PCS - OPENINGSALEORDER_DESC.OPSO_OUTPCS, 0) AS PCS, ISNULL(OPENINGSALEORDER_DESC.OPSO_MTRS - OPENINGSALEORDER_DESC.OPSO_OUTMTRS, 0) AS MTRS, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, 'OPENING' AS TYPE, ISNULL(DELIVERYAT.Acc_cmpname, '') AS DELIVERYAT, ISNULL(OPENINGSALEORDER_DESC.OPSO_RATE, 0) AS RATE FROM OPENINGSALEORDER INNER JOIN OPENINGSALEORDER_DESC ON OPENINGSALEORDER.OPSO_NO = OPENINGSALEORDER_DESC.OPSO_NO AND OPENINGSALEORDER.OPSO_YEARID = OPENINGSALEORDER_DESC.OPSO_YEARID INNER JOIN LEDGERS ON OPENINGSALEORDER.OPSO_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGSALEORDER_DESC.OPSO_GREYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN GREYQUALITYMASTER ON OPENINGSALEORDER_DESC.OPSO_GREYID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN LEDGERS AS DELIVERYAT ON OPENINGSALEORDER_DESC.OPSO_DELIVERYATID = DELIVERYAT.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGER ON OPENINGSALEORDER.OPSO_AGENTID = AGENTLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON OPENINGSALEORDER.OPSO_TRANSID = TRANSLEDGERS.Acc_id WHERE OPENINGSALEORDER_DESC.OPSO_CLOSED = 'False' AND OPENINGSALEORDER.OPSO_VERIFIED = 'True' AND  ISNULL(OPENINGSALEORDER_DESC.OPSO_PCS - OPENINGSALEORDER_DESC.OPSO_OUTPCS, 0) > 0 and LEDGERS.Acc_cmpname='" & PARTYNAME & "' AND ISNULL(OPENINGSALEORDER.OPSO_TYPE,'GREY') = '" & TYPE & "' AND OPENINGSALEORDER.OPSO_YEARID = " & YearId & " UNION ALL  SELECT CAST(0 AS BIT) AS CHK, SALEORDER.SO_NO AS ORDERNO, SALEORDER_DESC.SO_GRIDSRNO AS ORDERSRNO, SALEORDER.SO_DATE AS DATE, SALEORDER.SO_DELDATE AS DELDATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGER.Acc_cmpname, '') AS AGENT, CASE WHEN ISNULL(SO_TYPE, 'GREY') = 'GREY' THEN GREY_NAME ELSE QUALITY_NAME END AS QUALITY, ISNULL(SALEORDER_DESC.SO_PCS - SALEORDER_DESC.SO_OUTPCS, 0) AS PCS, ISNULL(SALEORDER_DESC.SO_MTRS - SALEORDER_DESC.SO_OUTMTRS, 0) AS MTRS, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, 'SO' AS TYPE,  ISNULL(DELIVERYAT.Acc_cmpname, '') AS DELIVERYAT, ISNULL(SALEORDER_DESC.SO_RATE, 0) AS RATE FROM SALEORDER INNER JOIN SALEORDER_DESC ON SALEORDER.SO_NO = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID INNER JOIN LEDGERS ON SALEORDER.SO_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN QUALITYMASTER ON SALEORDER_DESC.SO_GREYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN GREYQUALITYMASTER ON SALEORDER_DESC.SO_GREYID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN LEDGERS AS DELIVERYAT ON SALEORDER_DESC.SO_DELIVERYATID = DELIVERYAT.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGER ON SALEORDER.SO_AGENTID = AGENTLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON SALEORDER.SO_TRANSID = TRANSLEDGERS.Acc_id WHERE SALEORDER_DESC.SO_CLOSED = 'False' AND SALEORDER.SO_VERIFIED = 'True' AND  ISNULL(SALEORDER_DESC.SO_PCS - SALEORDER_DESC.SO_OUTPCS, 0) > 0 and LEDGERS.Acc_cmpname='" & PARTYNAME & "' AND ISNULL(SALEORDER.SO_TYPE,'GREY') = '" & TYPE & "' AND SALEORDER.SO_YEARID = " & YearId & ") AS T ", "")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            Dim COUNT As Integer
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    COUNT = COUNT + 1
                End If
            Next

            If COUNT > 1 And ClientName <> "SONU" Then
                MsgBox("You Can Select Only One Order")
                Exit Sub
            End If

            DT.Columns.Add("ORDERNO")
            DT.Columns.Add("ORDERSRNO")
            DT.Columns.Add("ORDERDATE")
            DT.Columns.Add("NAME")
            DT.Columns.Add("AGENT")
            DT.Columns.Add("QUALITY")
            DT.Columns.Add("PCS")
            DT.Columns.Add("MTRS")
            DT.Columns.Add("DELIVERYAT")
            DT.Columns.Add("TRANSPORT")
            DT.Columns.Add("ORDERTYPE")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("ORDERNO"), dtrow("ORDERSRNO"), dtrow("DATE"), dtrow("NAME"), dtrow("AGENT"), dtrow("QUALITY"), Val(dtrow("PCS")), Val(dtrow("MTRS")), dtrow("DELIVERYAT"), dtrow("TRANSPORT"), dtrow("TYPE"))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class