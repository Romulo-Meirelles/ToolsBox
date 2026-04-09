Imports ToolsBox.Utils
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim PixPayload As PixPayload = New PixPayload With {.Cidade = "SÃO PAULO", .Comentario = "COE2026032288022666", .TxId = "COE2026032288022666", .Nome = "VITOR", .Valor = "0.01", .PixKey = "21980345490", .Metodo = Method.CELULAR}
        PictureBox1.Image = PixGerarQRCodeImage(PixPayload)
        Label1.Text = PixGerarQRCode(PixPayload)
    End Sub
End Class
