Imports System.Drawing.Imaging
Namespace Utils
    Public Module Objetos_Transparentes
        Public Sub ObjectTransparent(ByVal lv As Control)
            Dim alpha As Integer = 64
            Dim p1 As Point = lv.Parent.PointToScreen(lv.Location)
            Dim p2 As Point = lv.PointToScreen(Point.Empty)
            p2.Offset(-p1.X, -p1.Y)
            If lv.BackgroundImage IsNot Nothing Then lv.BackgroundImage.Dispose()
            lv.Hide()
            Dim bmp As Bitmap = New Bitmap(lv.Parent.Width, lv.Parent.Height)
            lv.Parent.DrawToBitmap(bmp, lv.Parent.ClientRectangle)
            Dim r As Rectangle = lv.Bounds
            r.Offset(p2.X, p2.Y)
            bmp = bmp.Clone(r, PixelFormat.Format32bppArgb)

            Using g As Graphics = Graphics.FromImage(bmp)

                Using br As SolidBrush = New SolidBrush(Color.FromArgb(alpha, lv.BackColor))
                    g.FillRectangle(br, lv.ClientRectangle)
                End Using
            End Using

            lv.BackgroundImage = bmp
            lv.Show()
        End Sub
    End Module
End Namespace
