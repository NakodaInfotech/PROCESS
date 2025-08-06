
Imports BL

Public Class OpeningDrCrDetails
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub OpeningBillDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf (e.Control = True And e.KeyCode = Windows.Forms.Keys.N) Then       'for AddNew 
            Call cmdadd_Click(sender, e)
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub OpeningBillDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'OPENING'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgridname()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgridname(Optional ByVal whereclause As String = "")
        Try
            Dim OBJCMN As New ClsCommonMaster
            Dim dttable As DataTable
            dttable = OBJCMN.search(" DISTINCT LEDGERS.ACC_CMPNAME as NAME", "", " OPENINGDRCR INNER JOIN LEDGERS ON OPENINGDRCR.DC_LEDGERID = LEDGERS.Acc_id AND OPENINGDRCR.DC_CMPID = LEDGERS.Acc_cmpid AND OPENINGDRCR.DC_LOCATIONID = LEDGERS.Acc_locationid AND OPENINGDRCR.DC_YEARID = LEDGERS.Acc_yearid ", whereclause & " and Acc_cmpid = " & CmpId & " and Acc_locationid = " & Locationid & " and Acc_yearid = " & YearId & " order by Acc_cmpname")
            griduserdetails.DataSource = dttable
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getdetails(ByRef name As String)
        Dim OBJCMN As New ClsCommon
        Dim dttable As DataTable
        dttable = OBJCMN.search(" LEDGERS.Acc_cmpname AS NAME, OPENINGDRCR.DC_GRIDSRNO AS SRNO, OPENINGDRCR.DC_TYPE AS TYPE, OPENINGDRCR.DC_NO AS BILLNO, OPENINGDRCR.DC_YEAR AS YEAR, OPENINGDRCR.DC_DATE AS DATE, OPENINGDRCR.DC_DUEDATE AS DUEDATE,OPENINGDRCR.DC_AMT AS AMT ", "", " OPENINGDRCR INNER JOIN LEDGERS ON OPENINGDRCR.DC_LEDGERID = LEDGERS.Acc_id AND OPENINGDRCR.DC_CMPID = LEDGERS.Acc_cmpid AND OPENINGDRCR.DC_LOCATIONID = LEDGERS.Acc_locationid AND OPENINGDRCR.DC_YEARID = LEDGERS.Acc_yearid ", " AND LEDGERS.ACC_CMPNAME = '" & name & "' AND OPENINGDRCR.DC_CMPID = " & CmpId & " AND OPENINGDRCR.DC_LOCATIONID = " & Locationid & " AND OPENINGDRCR.DC_YEARID = " & YearId & " ORDER BY OPENINGDRCR.DC_GRIDSRNO")
        griddetails.DataSource = dttable
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEDIT.Click
        Try
            showform(True, GRIDUSERNAME.GetFocusedRowCellValue("NAME"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal name As String)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If (editval = False) Or (editval = True And GRIDUSERNAME.RowCount > 0) Then
                Dim OBJOP As New OpeningDrCr
                OBJOP.MdiParent = MDIMain
                OBJOP.TEMPNAME = name
                OBJOP.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(False, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDUSERNAME_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GRIDUSERNAME.Click
        Try
            getdetails(GRIDUSERNAME.GetFocusedRowCellValue("NAME"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDUSERNAME_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GRIDUSERNAME.DoubleClick
        Try
            showform(True, GRIDUSERNAME.GetFocusedRowCellValue("NAME"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDUSERNAME_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GRIDUSERNAME.FocusedRowChanged
        Try
            getdetails(GRIDUSERNAME.GetFocusedRowCellValue("NAME"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgridname()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningBillDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ClientName = "SVS" Then Me.Close()
    End Sub
End Class