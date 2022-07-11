Namespace Utils
    Public Class Resize_Image
        Public Function ResizeImage(ByVal InputImage As Image, ByVal Width As Int32, ByVal Height As Int32) As Image
            Return New Bitmap(InputImage, New Size(Width, Height))
        End Function
    End Class
End Namespace
