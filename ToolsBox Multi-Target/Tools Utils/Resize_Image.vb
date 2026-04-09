Imports System.Windows.Forms
Imports System.Drawing
Namespace Utils
    Public Module Resize_Image
        Public Function ResizeImage(ByVal InputImage As Image, ByVal Width As Int32, ByVal Height As Int32) As Image
            Return New Bitmap(InputImage, New Size(Width, Height))
        End Function

        Public Function ResizeImage(ByVal InputImage As Bitmap, ByVal Width As Int32, ByVal Height As Int32) As Bitmap
            Dim result As New Bitmap(Width, Height)
            Using g = Graphics.FromImage(result)
                g.DrawImage(InputImage, 0, 0, Width, Height)
            End Using
            Return result
        End Function
    End Module
End Namespace

