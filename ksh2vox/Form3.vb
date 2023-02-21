Public Class Form3

    Private Sub Form3_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Me.Close()
    End Sub
End Class