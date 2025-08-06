
Imports BL
Imports DevExpress.XtraEditors.Controls

Public Class PendingReturnDate

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub PendingReturnDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PendingReturnDate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GDN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            FILLTRANS()
            fillgrid()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLTRANS()
        Try
            CMBTRANSPORT.Items.Clear()
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("ACC_CMPNAME AS NAME", "", " LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID  ", " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT' AND ACC_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                For Each DTROW As DataRow In DT.Rows
                    CMBTRANSPORT.Items.Add(DTROW("NAME"))
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim WHERECLAUSE As String = ""
            Dim OPWHERECLAUSE As String = ""
            Dim SALRETWHERECLAUSE As String = ""

            'WE NEED TO FETCH DATA FROM SALERETTURN, AND GOODSRETURN WHERE FOR DYEING IS TRUE
            'If RBPENDING.Checked = True Then WHERECLAUSE = " AND ISNULL(GOODSRETURNMASTER.GR_RETURNDATE,'') = '' " Else WHERECLAUSE = " AND ISNULL(GOODSRETURNMASTER.GR_RETURNDATE,'') <> ''"
            If RBPENDING.Checked = True Then WHERECLAUSE = " AND GR_FORDYEING = 'TRUE' AND ISNULL(GOODSRETURNMASTER.GR_RETURNDATE,'') = '' " Else WHERECLAUSE = " AND ISNULL(GOODSRETURNMASTER.GR_RETURNDATE,'') <> ''"
            If RBPENDING.Checked = True Then OPWHERECLAUSE = " AND ISNULL(STOCKMASTER_RETDATE.SMRETDATE_RETDODATE,'__/__/____') = '__/__/____' " Else OPWHERECLAUSE = " AND ISNULL(STOCKMASTER_RETDATE.SMRETDATE_RETDODATE,'__/__/____') <> '__/__/____'"
            If RBPENDING.Checked = True Then SALRETWHERECLAUSE = " AND ISNULL(SALERETURN.SALRET_RETURNDATE,'') = '' " Else SALRETWHERECLAUSE = " AND ISNULL(SALERETURN.SALRET_RETURNDATE,'') <> ''"
            Dim OBJCMN As New ClsCommonMaster

            Dim dt As DataTable = OBJCMN.search("*", "", " (SELECT DISTINCT ISNULL(GOODSRETURNMASTER.GR_NO, '') AS [GRNO], 'GR' AS [TYPE], ISNULL(INVOICEMASTER.INVOICE_NO,0) AS INVOICENO, GOODSRETURNMASTER.GR_DATE AS DATE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(DYEINGLEDGERS.ACC_CMPNAME,'') AS DYEINGNAME, ISNULL(GOODSRETURNMASTER.GR_LOTNO, 0) AS LOTNO, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS QUALITY, ISNULL(GOODSRETURNMASTER.GR_CHALLANTAKA, 0) AS CTAKA, ISNULL(GOODSRETURNMASTER.GR_CHALLANMTRS, 0) AS CMTRS, ISNULL(GOODSRETURNMASTER.GR_RETURNTAKA, 0) AS RTAKA, ISNULL(GOODSRETURNMASTER.GR_RETURNMTRS, 0) AS RMTRS, ISNULL(GOODSRETURNMASTER.GR_RETURNDONO, '') AS RDONO, ISNULL(GOODSRETURNMASTER.GR_SHORTAGE, 0) AS SHORTAGE, ISNULL(GOODSRETURNMASTER.GR_NETTTAKA, 0) AS NTAKA, ISNULL(GOODSRETURNMASTER.GR_NETTMTRS, 0) AS NMTRS, (CASE WHEN CAST(GOODSRETURNMASTER.GR_RETURNDATE AS DATE) = '' THEN NULL ELSE CAST(GOODSRETURNMASTER.GR_RETURNDATE AS DATE) END) AS RDATE, ISNULL(TRANSLEDGERS.ACC_CMPNAME ,'') AS TRANSNAME FROM LEDGERS AS TRANSLEDGERS RIGHT OUTER JOIN GOODSRETURNMASTER INNER JOIN GODOWNMASTER ON GOODSRETURNMASTER.GR_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN GREYQUALITYMASTER ON GOODSRETURNMASTER.GR_GREYQUALITYID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN LEDGERS ON GOODSRETURNMASTER.GR_LEDGERID = LEDGERS.Acc_id INNER JOIN LEDGERS AS DYEINGLEDGERS ON GOODSRETURNMASTER.GR_DYEINGID = DYEINGLEDGERS.Acc_id LEFT OUTER JOIN INVOICEMASTER INNER JOIN INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID ON GOODSRETURNMASTER.GR_DYEINGID = INVOICEMASTER.INVOICE_DELIVERYID AND GOODSRETURNMASTER.GR_LOTNO = INVOICEMASTER_DESC.INVOICE_LOTNO ON TRANSLEDGERS.Acc_id = GOODSRETURNMASTER.GR_TRANSID WHERE 1=1 " & WHERECLAUSE & " AND (GOODSRETURNMASTER.GR_YEARID = '" & YearId & "') AND (GOODSRETURNMASTER.GR_LOTNO <> '') UNION ALL SELECT DISTINCT ISNULL(SALERETURN.SALRET_NO, 0) AS [GRNO], 'SALRET' AS [TYPE], ISNULL(INVOICEMASTER.INVOICE_NO,0) AS INVOICENO, SALERETURN.SALRET_DATE AS DATE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(DYEINGLEDGERS.ACC_CMPNAME,'') AS DYEINGNAME, ISNULL(SALERETURN_DESC.SALRET_LOTNO, 0) AS LOTNO, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS QUALITY, ISNULL(INVOICEMASTER.INVOICE_TOTALPCS, 0) AS CTAKA, ISNULL(INVOICEMASTER.INVOICE_TOTALMTRS, 0) AS CMTRS, ISNULL(SALERETURN_DESC.SALRET_PCS, 0) AS RTAKA, ISNULL(SALERETURN_DESC.SALRET_MTRS, 0) AS RMTRS, ISNULL(SALERETURN.SALRET_DONO, '') AS RDONO, ISNULL(SALERETURN.SALRET_SHORTAGE, 0) AS SHORTAGE, ISNULL(SALERETURN.SALRET_NETTTAKA, 0) AS NTAKA, ISNULL(SALERETURN.SALRET_NETTMTRS, 0) AS NMTRS, (CASE WHEN SALERETURN.SALRET_RETURNDATE = '' THEN NULL ELSE CAST(SALERETURN.SALRET_RETURNDATE AS DATE) END) AS RDATE, ISNULL(TRANSLEDGERS.ACC_CMPNAME ,'') AS TRANSNAME FROM SALERETURN INNER JOIN GODOWNMASTER ON SALERETURN.SALRET_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN LEDGERS ON SALERETURN.SALRET_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS DYEINGLEDGERS ON SALERETURN.SALRET_DYEINGID = DYEINGLEDGERS.Acc_id LEFT OUTER JOIN INVOICEMASTER ON SALERETURN.SALRET_INVOICENO = INVOICEMASTER.INVOICE_NO AND SALERETURN.SALRET_INVOICEREGID = INVOICEMASTER.INVOICE_REGISTERID LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON TRANSLEDGERS.Acc_id = SALERETURN.SALRET_TRANSID INNER JOIN SALERETURN_DESC ON SALERETURN.SALRET_NO = SALERETURN_DESC.SALRET_NO AND SALERETURN.SALRET_REGISTERID = SALERETURN_DESC.SALRET_REGISTERID AND SALERETURN.SALRET_YEARID = SALERETURN_DESC.SALRET_YEARID INNER JOIN GREYQUALITYMASTER ON SALERETURN_DESC.SALRET_QUALITYID = GREYQUALITYMASTER.GREY_ID WHERE 1=1 " & SALRETWHERECLAUSE & " AND (SALERETURN.SALRET_YEARID = " & YearId & ") AND (SALERETURN_DESC.SALRET_LOTNO <> '') UNION ALL SELECT ISNULL(STOCKMASTER_RETDATE.SMRETDATE_NO, '') AS [GRNO], 'OPENING' AS [TYPE], ISNULL(STOCKMASTER_RETDATE.SMRETDATE_INVNO,0) AS INVOICENO, STOCKMASTER_RETDATE.SMRETDATE_INVDATE AS DATE, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(DYEINGLEDGERS.ACC_CMPNAME,'') AS DYEINGNAME, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_LOTNO, 0) AS LOTNO, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS QUALITY, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_RETTAKA, 0) AS CTAKA, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_RETMTRS, 0) AS CMTRS, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_RETTAKA, 0) AS RTAKA, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_RETMTRS, 0) AS RMTRS, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_RETDONO, '') AS RDONO, 0 AS SHORTAGE, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_RETTAKA, 0) AS NTAKA, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_RETMTRS, 0) AS NMTRS, (CASE WHEN STOCKMASTER_RETDATE.SMRETDATE_RETDODATE  = '__/__/____' THEN NULL ELSE CAST(STOCKMASTER_RETDATE.SMRETDATE_RETDODATE AS DATE) END) AS RDATE, ISNULL(TRANSLEDGERS.ACC_CMPNAME ,'') AS TRANSNAME FROM LEDGERS AS TRANSLEDGERS RIGHT OUTER JOIN STOCKMASTER_RETDATE INNER JOIN GODOWNMASTER ON STOCKMASTER_RETDATE.SMRETDATE_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN GREYQUALITYMASTER ON STOCKMASTER_RETDATE.SMRETDATE_GREYID = GREYQUALITYMASTER.GREY_ID INNER JOIN LEDGERS ON STOCKMASTER_RETDATE.SMRETDATE_LEDGERID = LEDGERS.Acc_id INNER JOIN LEDGERS AS DYEINGLEDGERS ON STOCKMASTER_RETDATE.SMRETDATE_DYEINGID = DYEINGLEDGERS.Acc_id LEFT OUTER JOIN INVOICEMASTER INNER JOIN INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID ON STOCKMASTER_RETDATE.SMRETDATE_DYEINGID = INVOICEMASTER.INVOICE_DELIVERYID AND STOCKMASTER_RETDATE.SMRETDATE_LOTNO = INVOICEMASTER_DESC.INVOICE_LOTNO ON TRANSLEDGERS.Acc_id = STOCKMASTER_RETDATE.SMRETDATE_TRANSID WHERE 1=1 " & OPWHERECLAUSE & " AND STOCKMASTER_RETDATE.SMRETDATE_YEARID = '" & YearId & "' AND STOCKMASTER_RETDATE.SMRETDATE_LOTNO <> '') AS T ", " ORDER BY T.GRNO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal GRNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then

                Dim ROW As DataRow = gridbill.GetFocusedDataRow
                If ROW Is Nothing Then Exit Sub

                If ROW("TYPE") = "GR" Then
                    Dim OBJGRNO As New GoodsReturn
                    OBJGRNO.MdiParent = MDIMain
                    OBJGRNO.EDIT = editval
                    OBJGRNO.TEMPGRNO = GRNO
                    OBJGRNO.Show()
                ElseIf ROW("TYPE") = "SALRET" Then
                    Dim OBJSR As New SaleReturn
                    OBJSR.MdiParent = MDIMain
                    OBJSR.edit = editval
                    OBJSR.TEMPSALERETNO = GRNO
                    OBJSR.TEMPREGNAME = "SALE RETURN REGISTER"
                    OBJSR.Show()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub gridbilldetails_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            Dim ROW As DataRow = gridbill.GetFocusedDataRow
            If ROW Is Nothing Then Exit Sub
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            'If IsDBNull(ROW("RDATE")) = False Then DT = OBJCMN.Execute_Any_String("UPDATE GOODSRETURNMASTER SET GR_RETURNDATE = '" & Format(ROW("RDATE"), "MM/dd/yyyy") & "' WHERE GR_NO = " & ROW("GRNO") & " AND GR_YEARID = " & YearId, "", "")
            If ROW("TRANSNAME") <> "" And IsDBNull(ROW("RDATE")) = False Then
                Dim TEMPNAMEID As Integer = 0
                DT = OBJCMN.search(" ACC_ID AS NAMEID", "", " LEDGERS ", " AND ACC_CMPNAME = '" & ROW("TRANSNAME") & "' AND ACC_YEARID = " & YearId)
                TEMPNAMEID = DT.Rows(0).Item(0)
                If ROW("TYPE") = "GR" Then
                    DT = OBJCMN.Execute_Any_String("UPDATE GOODSRETURNMASTER SET GR_RETURNDATE = '" & Format(ROW("RDATE"), "MM/dd/yyyy") & "', GR_TRANSID = " & TEMPNAMEID & " WHERE GR_NO = " & Val(ROW("GRNO")) & " AND GR_YEARID = " & YearId, "", "")
                ElseIf ROW("TYPE") = "SALRET" Then
                    DT = OBJCMN.Execute_Any_String("UPDATE SALERETURN SET SALRET_RETURNDATE = '" & Format(ROW("RDATE"), "MM/dd/yyyy") & "', SALRET_TRANSID = " & TEMPNAMEID & " WHERE SALRET_NO = " & Val(ROW("GRNO")) & " AND SALRET_YEARID = " & YearId, "", "")
                Else
                    DT = OBJCMN.Execute_Any_String("UPDATE STOCKMASTER_RETDATE SET SMRETDATE_RETDODATE = '" & Format(ROW("RDATE"), "MM/dd/yyyy") & "', SMRETDATE_TRANSID = " & TEMPNAMEID & " WHERE SMRETDATE_NO = " & ROW("GRNO") & " AND SMRETDATE_YEARID = " & YearId, "", "")
                End If
            End If

            fillgrid()
            gridbill.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles gridbill.InvalidRowException
        e.ExceptionMode = ExceptionMode.NoAction
    End Sub

    Private Sub gridbill_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles gridbill.ValidateRow
        Try
            If IsDBNull(gridbill.GetRowCellValue(e.RowHandle, "RDATE")) = False Then
                If gridbill.GetRowCellValue(e.RowHandle, "RDATE") < Convert.ToDateTime(gridbill.GetRowCellValue(e.RowHandle, "DATE")).Date Then
                    e.Valid = False
                    gridbill.SetColumnError(GRDATE, "Date must be After GR Date")
                    Exit Sub
                End If
            End If
            If IsDBNull(gridbill.GetRowCellValue(e.RowHandle, "RDATE")) = False And IsDBNull(gridbill.GetRowCellValue(e.RowHandle, "TRANSPORT")) = False Then If MsgBox("Save Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then Call CMDOK_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try

            Dim ROW As DataRow = gridbill.GetFocusedDataRow
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If IsDBNull(ROW("RDATE")) = True Then
                MsgBox("No Row To Delete")
                Exit Sub
            End If

            If MsgBox("Delete Data?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If ROW("TYPE") = "GR" Then
                DT = OBJCMN.Execute_Any_String("UPDATE GOODSRETURNMASTER SET GR_RETURNDATE = ''  WHERE GR_NO = " & Val(ROW("GRNO")) & " AND GR_YEARID = " & YearId, "", "")
            ElseIf ROW("TYPE") = "SALRET" Then
                DT = OBJCMN.Execute_Any_String("UPDATE SALERETURN SET SALRET_RETURNDATE = ''  WHERE SALRET_NO = " & Val(ROW("GRNO")) & " AND SALRET_YEARID = " & YearId, "", "")
            Else
                DT = OBJCMN.Execute_Any_String("UPDATE STOCKMASTER_RETDATE SET SMRETDATE_RETDODATE = '__/__/____'  WHERE SMRETDATE_NO = " & ROW("GRNO") & " AND SMRETDATE_YEARID = " & YearId, "", "")
            End If
            fillgrid()
            gridbill.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class