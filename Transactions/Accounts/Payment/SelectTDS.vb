
Imports BL

Public Class SelectTDS

    Public TDSNAME As String = ""
    Public PAYCHQNO As String = ""
    Public CHQAMT As Double = 0
    Public VALIDATETDS As Boolean = True

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        VALIDATETDS = False
        Me.Close()
    End Sub

    Private Sub SelectTDS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TXTCHQAMT.Text = Val(CHQAMT)
        fillgrid()
    End Sub

    Sub TOTAL()
        Try
            TXTTDSAMT.Clear()
            For I As Integer = 0 To GRIDBILL.RowCount - 1
                Dim DTROW As DataRow = GRIDBILL.GetDataRow(I)
                If Convert.ToBoolean(DTROW("CHK")) = True Then
                    TXTTDSAMT.Text = Val(TXTTDSAMT.Text) + Val(DTROW("TDSAMT"))
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()

        Dim dt As New DataTable()
        Dim ALPARAVAL As New ArrayList

        Dim objregister As New ClsTdsChallan

        ALPARAVAL.Add(TDSNAME)
        If chkdate.Checked = True Then
            ALPARAVAL.Add(dtfrom.Value.Date)
            ALPARAVAL.Add(dtto.Value.Date)
        Else
            ALPARAVAL.Add(AccFrom)
            ALPARAVAL.Add(AccTo)
        End If

        ALPARAVAL.Add(CmpId)
        ALPARAVAL.Add(Locationid)
        ALPARAVAL.Add(YearId)

        objregister.alParaval = ALPARAVAL
        dt = objregister.GETUNPAID
        GRIDBILLDETAILS.DataSource = dt

    End Sub

    Private Sub chkdate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkdate.CheckedChanged
        Try
            dtfrom.Enabled = chkdate.CheckState
            dtto.Enabled = chkdate.CheckState
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim BLN As Boolean = True
            If TXTCHALLANDATE.Text = "__/__/____" Then
                EP.SetError(TXTCHALLANDATE, "Enter Proper Date")
                BLN = False
            ElseIf Not datecheck(TXTCHALLANDATE.Text) Then
                EP.SetError(TXTCHALLANDATE, "Date Not in Current Accounting Year")
                BLN = False
            End If

            If Val(TXTTDSAMT.Text.Trim) <> Val(TXTCHQAMT.Text.Trim) Then
                EP.SetError(TXTTDSAMT, "Amount Does not Match")
                BLN = False
            End If

            If TXTCHALLANNO.Text.Trim = "" Then
                EP.SetError(TXTCHALLANNO, "Enter Challan No")
                BLN = False
            End If

            If TXTBSRCODE.Text.Trim = "" Then
                EP.SetError(TXTBSRCODE, "Enter BSR Code")
                BLN = False
            End If

            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList
            Dim OBJTDS As New ClsTDSChallan

            Dim INTRESULT As Integer

            Dim BILLINITIALS As String = ""
            Dim BILLDATE As String = ""
            Dim CHALLANNO As String = ""
            Dim CHALLANDATE As String = ""
            Dim CHQNO As String = ""
            Dim BANKNAME As String = ""

            If GRIDBILL.FilterPanelText <> "" Then GRIDBILL.ActiveFilterEnabled = False

            For i As Integer = 0 To GRIDBILL.DataRowCount - 1
                Dim dtrow As DataRow = GRIDBILL.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then

                    If BILLINITIALS = "" Then
                        BILLINITIALS = dtrow("BILLINITIALS").ToString
                        BILLDATE = Format(Convert.ToDateTime(dtrow("DATE")).Date, "MM/dd/yyyy")
                        CHALLANNO = TXTCHALLANNO.Text.Trim
                        CHALLANDATE = Format(Convert.ToDateTime(TXTCHALLANDATE.Text).Date, "MM/dd/yyyy")
                        CHQNO = PAYCHQNO
                        BANKNAME = TXTBSRCODE.Text.Trim
                    Else
                        BILLINITIALS = BILLINITIALS & "," & dtrow("BILLINITIALS").ToString
                        BILLDATE = BILLDATE & "," & Format(Convert.ToDateTime(dtrow("DATE")).Date, "MM/dd/yyyy")
                        CHALLANNO = CHALLANNO & "," & TXTCHALLANNO.Text
                        CHALLANDATE = CHALLANDATE & "," & Format(Convert.ToDateTime(TXTCHALLANDATE.Text).Date, "MM/dd/yyyy")
                        CHQNO = CHQNO & "," & PAYCHQNO
                        BANKNAME = BANKNAME & "," & TXTBSRCODE.Text.Trim
                    End If

                End If
            Next

            ALPARAVAL.Add(TDSNAME)
            If chkdate.Checked = True Then
                ALPARAVAL.Add(dtfrom.Value.Date)
                ALPARAVAL.Add(dtto.Value.Date)
            Else
                ALPARAVAL.Add(AccFrom)
                ALPARAVAL.Add(AccTo)
            End If

            ALPARAVAL.Add(BILLINITIALS)
            ALPARAVAL.Add(BILLDATE)
            ALPARAVAL.Add(CHALLANNO)
            ALPARAVAL.Add(CHALLANDATE)
            ALPARAVAL.Add(CHQNO)
            ALPARAVAL.Add(BANKNAME)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)
            ALPARAVAL.Add(1)

            OBJTDS.alParaval = ALPARAVAL
            INTRESULT = OBJTDS.SAVE()
            VALIDATETDS = True
            Me.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHALLANDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHALLANDATE.Validating
        Try
            If TXTCHALLANDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(TXTCHALLANDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBILL_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GRIDBILL.CellValueChanged
        TOTAL()
    End Sub
End Class