
Imports BL

Public Class UploadExcelStatement

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMDUPLOAD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDUPLOAD.Click
        Try
            OpenFileDialog1.Filter = "Excel (*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv"
            OpenFileDialog1.ShowDialog()

            OpenFileDialog1.AddExtension = True
            TXTFILENAME.Text = OpenFileDialog1.SafeFileName
            txtimgpath.Text = OpenFileDialog1.FileName
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim BLN As Boolean = True

            If Val(TXTSTART.Text.Trim) = 0 Then
                EP.SetError(TXTSTART, "Enter Starting Row No")
                BLN = False
            End If

            If Val(TXTSTART.Text.Trim) > Val(TXTEND.Text.Trim) Then
                EP.SetError(TXTSTART, "Starting Row No cannot be greater then Ending No")
                BLN = False
            End If

            If Val(TXTEND.Text.Trim) = 0 Then
                EP.SetError(TXTEND, "Enter Ending Row No")
                BLN = False
            End If

            If TXTDATECOL.Text.Trim = "" Then
                EP.SetError(TXTDATECOL, "Enter Date Column")
                BLN = False
            End If

            If TXTCHQCOL.Text.Trim = "" Then
                EP.SetError(TXTCHQCOL, "Enter Cheque Column")
                BLN = False
            End If

            If TXTDRAMTCOL.Text.Trim = "" Then
                EP.SetError(TXTDRAMTCOL, "Enter Debit Amount Column")
                BLN = False
            End If

            If TXTCRAMTCOL.Text.Trim = "" Then
                EP.SetError(TXTCRAMTCOL, "Enter Credit Amount Column")
                BLN = False
            End If

            If txtimgpath.Text.Trim = "" Then
                EP.SetError(TXTDRAMTCOL, "Upload Excel Statement")
                BLN = False
            End If

            If TXTSHEETNAME.Text.Trim = "" Then
                EP.SetError(TXTSHEETNAME, "Enter Excel Sheet Name")
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
            Me.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTAMTCOL_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTDRAMTCOL.KeyPress
        ALPHEBETKEYPRESS(e, TXTDRAMTCOL, Me)
    End Sub

    Private Sub TXTCRAMTCOL_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCRAMTCOL.KeyPress
        ALPHEBETKEYPRESS(e, TXTCRAMTCOL, Me)
    End Sub

    Private Sub TXTCHQCOL_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCHQCOL.KeyPress
        ALPHEBETKEYPRESS(e, TXTCHQCOL, Me)
    End Sub

    Private Sub TXTDATECOL_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTDATECOL.KeyPress
        ALPHEBETKEYPRESS(e, TXTDATECOL, Me)
    End Sub

    Private Sub TXTEND_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTEND.KeyPress
        numkeypress(e, TXTEND, Me)
    End Sub

    Private Sub TXTSTART_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTSTART.KeyPress
        numkeypress(e, TXTSTART, Me)
    End Sub
End Class