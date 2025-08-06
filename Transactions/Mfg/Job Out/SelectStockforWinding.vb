
Imports BL

Public Class SelectStockforWinding

    Public DT As New DataTable
    Public STOCKTYPE As String = ""
    Public STOCKNAME As String = ""

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectStockforWinding_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectStockforWinding_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FILLGRID(" ")
    End Sub

    Sub FILLGRID(ByVal WHERE As String)
        Try
            Dim objcmn As New ClsCommon
            Dim dt As New DataTable
            If STOCKTYPE = "SELF" Then
                dt = objcmn.search("CAST(0 AS BIT) AS CHK,NAME,MILLNAME,QUALITY,LOTNO, SHADE,BAGS,WT,NO AS FROMNO ,SRNO AS FROMSRNO,TYPE AS FROMTYPE, FRESH, WINDING, FIRKA,BAGNO ", "", "STOCKVIEW_OURGODOWN", " AND GODOWN = '" & STOCKNAME & "' AND YEARID = " & YearId)
            ElseIf STOCKTYPE = "WARPER" Then
                dt = objcmn.search("CAST(0 AS BIT) AS CHK,WARPERNAME AS NAME,MILLNAME,QUALITY, '' as LOTNO, '' AS SHADE, 0 AS BAGS, SUM(WT) AS WT,0 AS FROMNO ,0 AS FROMSRNO,''AS FROMTYPE, 0 AS FRESH, 0 AS WINDING, 0 AS FIRKA, '' AS BAGNO ", "", "WARPERYARNSTOCK", " AND WARPERNAME = '" & STOCKNAME & "' AND YEARID = " & YearId & " GROUP BY WARPERNAME,MILLNAME,QUALITY")
            ElseIf STOCKTYPE = "SIZER" Then
                dt = objcmn.search("CAST(0 AS BIT) AS CHK,SIZERNAME AS NAME,MILLNAME,QUALITY, '' as LOTNO, '' AS SHADE, 0 AS BAGS, SUM(WT) AS WT,0 AS FROMNO ,0 AS FROMSRNO,''AS FROMTYPE, 0 AS FRESH, 0 AS WINDING, 0 AS FIRKA, '' AS BAGNO ", "", "SIZERYARNSTOCK", " AND SIZERNAME = '" & STOCKNAME & "' AND YEARID = " & YearId & " GROUP BY SIZERNAME,MILLNAME,QUALITY")
            ElseIf STOCKTYPE = "WEAVER" Then
                dt = objcmn.search("CAST(0 AS BIT) AS CHK,WEAVERNAME AS NAME,MILLNAME,QUALITY, '' as LOTNO, '' AS SHADE, 0 AS BAGS, SUM(WT) AS WT,0 AS FROMNO ,0 AS FROMSRNO,''AS FROMTYPE, 0 AS FRESH, 0 AS WINDING, 0 AS FIRKA, '' AS BAGNO  ", "", "WEAVERYARNSTOCK", " AND WEAVERNAME = '" & STOCKNAME & "' AND YEARID = " & YearId & " GROUP BY WEAVERNAME,MILLNAME,QUALITY")
            End If
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            DT.Columns.Add("NAME")
            DT.Columns.Add("MILLNAME")
            DT.Columns.Add("QUALITY")
            DT.Columns.Add("LOTNO")
            DT.Columns.Add("SHADE")
            DT.Columns.Add("BAGS")
            DT.Columns.Add("BAGNO")
            DT.Columns.Add("GROSSWT")
            DT.Columns.Add("TAREWT")
            DT.Columns.Add("WT")
            DT.Columns.Add("FRESH")
            DT.Columns.Add("WINDING")
            DT.Columns.Add("FIRKA")
            DT.Columns.Add("FROMNO")
            DT.Columns.Add("FROMSRNO")
            DT.Columns.Add("FROMTYPE")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("NAME"), dtrow("MILLNAME"), dtrow("QUALITY"), dtrow("LOTNO"), dtrow("SHADE"), Val(dtrow("BAGS")), Val(dtrow("BAGNO")), 0, 0, Val(dtrow("WT")), Val(dtrow("FRESH")), Val(dtrow("WINDING")), Val(dtrow("FIRKA")), dtrow("FROMNO"), dtrow("FROMSRNO"), dtrow("FROMTYPE"))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class