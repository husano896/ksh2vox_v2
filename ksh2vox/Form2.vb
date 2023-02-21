Public Class Form2
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If (TextBox1.Text = "KONMAI" Or TextBox1.Text = "NIMABE") Then
            Form1.Enabled = True
            Form1.Opacity = 1.0
            Form1.Button1.Enabled = True
            Form1.Button2.Enabled = True
            Me.Enabled = False
            Me.Opacity = 0.0
            Me.Size = New Size(1, 1)
        Else
            Label2.Text -= 1
            TextBox1.Text = ""
            If (Label2.Text = 0) Then
                End
            End If
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Form3.Show()
        LinkLabel1.Enabled = False
    End Sub
End Class