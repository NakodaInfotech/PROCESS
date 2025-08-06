
Imports BL

Public Class SelectGDN

    Public DT As New DataTable
    Public PARTYNAME As String = ""  'for whereclause in fillgrid
    Public TYPE As String = ""  'for whereclause in  FOR YARN OR GREY

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectGDN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Sub fillgrid(Optional ByVal WHERECLAUSE As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon
            If PARTYNAME <> "" Then WHERECLAUSE = WHERECLAUSE & " and LEDGERS.Acc_cmpname='" & PARTYNAME & "' "
            Dim DT As DataTable = OBJCMN.search("CAST (0 AS BIT) AS CHK ,T.CHALLANNO,T.CHALLANDATE,T.NAME, T.AGENT,T.TRANSNAME,T.ORDERNO,T.ORDERSRNO,T.TOTALTAKA,T.TOTALMTRS,T.CHALLANTYPE, T.LOTNO, T.DELIVERYAT, T.SHIPTO ", "", " (SELECT CHALLANMASTER.CHALLAN_NO AS CHALLANNO, CHALLANMASTER.CHALLAN_DATE AS CHALLANDATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '') AS DELIVERYAT, ISNULL(AGENT.Acc_cmpname, '') AS AGENT, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSNAME, ISNULL(CHALLANMASTER.CHALLAN_ORDERNO, 0) AS ORDERNO,ISNULL(CHALLANMASTER.CHALLAN_ORDERSRNO, 0) AS ORDERSRNO,  ISNULL(CHALLANMASTER.CHALLAN_TOTALTAKA, 0) AS TOTALTAKA, ISNULL(CHALLANMASTER.CHALLAN_TOTALMTRS, 0) AS TOTALMTRS, 'CHALLAN' AS CHALLANTYPE , ISNULL(CHALLAN_LOTNO,0) AS LOTNO, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '') AS SHIPTO FROM CHALLANMASTER INNER JOIN LEDGERS ON CHALLANMASTER.CHALLAN_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON CHALLANMASTER.CHALLAN_DELIVERYID = DELIVERYLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON CHALLANMASTER.CHALLAN_TRANSID = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS AGENT ON CHALLANMASTER.CHALLAN_BROKERID = AGENT.Acc_id WHERE CHALLANMASTER.CHALLAN_TYPE = '" & TYPE & "' AND CHALLANMASTER.CHALLAN_DONE= 0 and CHALLANMASTER.CHALLAN_FORDYEING = 0 " & WHERECLAUSE & " AND CHALLANMASTER.CHALLAN_YEARID = " & YearId & " UNION ALL SELECT   DISTINCT  CHALLANFINISHMASTER.CHALLANFINISH_NO AS CHALLANNO, CHALLANFINISHMASTER.CHALLANFINISH_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '') AS DELIVERYAT, ISNULL(AGENT.Acc_cmpname, '') AS AGENT, ISNULL(TRANSNAME.Acc_cmpname, '') AS TRANSNAME, CHALLANFINISHMASTER.CHALLANFINISH_ORDERNO AS ORDERNO, ISNULL(CHALLANFINISHMASTER.CHALLANFINISH_ORDERSRNO, 0) AS ORDERSRNO, ISNULL(CHALLANFINISHMASTER.CHALLANFINISH_TOTALTAKA, 0) AS TOTALTAKA, ISNULL(CHALLANFINISHMASTER.CHALLANFINISH_TOTALMTRS, 0) AS TOTALMTRS, 'CHALLANFINISH' AS CHALLANTYPE, CHALLANFINISHMASTER_DESC.CHALLANFINISH_LOTNO, ISNULL(SHIPTOLEDGERS.Acc_cmpname, '') AS SHIPTO FROM CHALLANFINISHMASTER INNER JOIN LEDGERS ON CHALLANFINISHMASTER.CHALLANFINISH_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS SHIPTOLEDGERS ON CHALLANFINISHMASTER.CHALLANFINISH_SHIPTOID = SHIPTOLEDGERS.Acc_id INNER JOIN CHALLANFINISHMASTER_DESC ON CHALLANFINISHMASTER.CHALLANFINISH_NO = CHALLANFINISHMASTER_DESC.CHALLANFINISH_NO AND CHALLANFINISHMASTER.CHALLANFINISH_YEARID = CHALLANFINISHMASTER_DESC.CHALLANFINISH_YEARID LEFT OUTER JOIN LEDGERS AS TRANSNAME ON CHALLANFINISHMASTER.CHALLANFINISH_TRANSID = TRANSNAME.Acc_id LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON CHALLANFINISHMASTER.CHALLANFINISH_DELIVERYID = DELIVERYLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENT ON CHALLANFINISHMASTER.CHALLANFINISH_BROKERID = AGENT.Acc_id WHERE CHALLANFINISHMASTER.CHALLANFINISH_DONE= 0 " & WHERECLAUSE & " AND CHALLANFINISHMASTER.CHALLANFINISH_YEARID = " & YearId & ") AS T  ", "")
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

            Dim TEMPCHALLANTYPE As String = ""
            Dim ISSALECHALLANTYPE As Boolean = True
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If TEMPCHALLANTYPE = "" Then
                        TEMPCHALLANTYPE = dtrow("CHALLANTYPE")
                    Else
                        'CHECK WHETHER IT IS OF SAME LOTNO OR NO
                        If dtrow("CHALLANTYPE") <> TEMPCHALLANTYPE Then
                            ISSALECHALLANTYPE = False
                            Exit For
                        End If
                    End If
                End If
            Next
            If Not (ISSALECHALLANTYPE) Then
                MsgBox("Please select Same Challan Type Only", MsgBoxStyle.Critical)
                Exit Sub
            End If

            DT.Columns.Add("CHALLANNO")
            DT.Columns.Add("CHALLANDATE")
            DT.Columns.Add("NAME")
            DT.Columns.Add("AGENT")
            DT.Columns.Add("TRANSNAME")
            DT.Columns.Add("DELIVERYAT")
            DT.Columns.Add("CHALLANTYPE")
            DT.Columns.Add("SHIPTO")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("CHALLANNO"), dtrow("CHALLANDATE"), dtrow("NAME"), dtrow("AGENT"), dtrow("TRANSNAME"), dtrow("DELIVERYAT"), dtrow("CHALLANTYPE"), dtrow("SHIPTO"))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectGDN_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName <> "SASHWINKUMAR" Then
                GNAME.Visible = True
                GNAME.VisibleIndex = 2
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class