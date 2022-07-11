Imports System.ComponentModel
Imports System.Threading
Imports System.Delegate

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.ListView))> _
Class ListView_Graphicals
    ' Methods
    Public Shared Function AddColor(ByVal col1 As Color, ByVal col2 As Color) As Color
        Dim R As Int32 = ((CInt(col1.R) + CInt(col2.R)) / 2)
        Dim G As Int32 = ((CInt(col1.G) + CInt(col2.G)) / 2)
        Dim B As Int32 = ((CInt(col1.B) + CInt(col2.B)) / 2)

        Return Color.FromArgb(R, G, B)
    End Function

    Function IntegerToColor(ByRef RGB As Int32) As Color
        Dim Bytes As Byte() = BitConverter.GetBytes(RGB)
        Dim Alpha As Byte = Bytes(3)
        Dim Red As Byte = Bytes(2)
        Dim Green As Byte = Bytes(1)
        Dim Blue As Byte = Bytes(0)
        Return Color.FromArgb(Alpha, Red, Green, Blue)
    End Function

    Public Shared Function getColorScale(ByVal Passentage As Integer, ByVal startColor As Color, ByVal endColor As Color) As Color
        Dim red As Integer = Integer.Parse(Math.Round(CDbl((startColor.R + (((endColor.R - startColor.R) * Passentage) * 0.01))), 0).ToString)
        Dim green As Integer = Integer.Parse(Math.Round(CDbl((startColor.G + (((endColor.G - startColor.G) * Passentage) * 0.01))), 0).ToString)
        Dim blue As Integer = Integer.Parse(Math.Round(CDbl((startColor.B + (((endColor.B - startColor.B) * Passentage) * 0.01))), 0).ToString)
        Return Color.FromArgb(&HFF, red, green, blue)
    End Function

    Public Shared Function OverlayColor(ByVal _Image As Image, ByVal Replace As Color) As Image
        Dim bitmap As New Bitmap(_Image)
        Dim i As Integer
        For i = 0 To bitmap.Height - 1
            Dim j As Integer
            For j = 0 To bitmap.Width - 1
                If Not ListView_Graphicals.smethod_0(bitmap.GetPixel(j, i)) Then
                    bitmap.SetPixel(j, i, Replace)
                End If
            Next j
        Next i
        Return bitmap
    End Function

    Public Shared Function OverlayColor(ByVal _Image As Image, ByVal Find As Color, ByVal Replace As Color) As Image
        Dim bitmap As New Bitmap(_Image)
        Dim i As Integer
        For i = 0 To bitmap.Height - 1
            Dim j As Integer
            For j = 0 To bitmap.Width - 1
                If Not ListView_Graphicals.smethod_0(bitmap.GetPixel(j, i)) Then
                    bitmap.SetPixel(j, i, Replace)
                End If
            Next j
        Next i
        Return bitmap
    End Function

    Private Shared Function smethod_0(ByVal color_0 As Color) As Boolean
        Return (((color_0.R = 0) AndAlso (color_0.G = 0)) AndAlso (color_0.B = 0))
    End Function

    Public Shared Function Smoothen(ByVal _img As Image) As Image
        Dim bitmap As New Bitmap(_img)
        Dim list As New List(Of Integer())
        Dim i As Integer
        For i = 0 To (bitmap.Height - 1) - 1
            Dim k As Integer
            For k = 0 To (bitmap.Width - 1) - 1
                Dim colorArray As Color() = New Color(4 - 1) {}
                colorArray(0) = bitmap.GetPixel(k, i)
                colorArray(2) = bitmap.GetPixel(k, (i + 1))
                colorArray(1) = bitmap.GetPixel((k + 1), i)
                colorArray(3) = bitmap.GetPixel((k + 1), (i + 1))
                If (((colorArray(1) = colorArray(2)) AndAlso Not ListView_Graphicals.smethod_0(colorArray(1))) AndAlso ListView_Graphicals.smethod_0(colorArray(0))) Then
                    Dim item As Integer() = New Integer() {k, i}
                    list.Add(item)
                End If
                If (((colorArray(0) = colorArray(3)) AndAlso Not ListView_Graphicals.smethod_0(colorArray(0))) AndAlso ListView_Graphicals.smethod_0(colorArray(2))) Then
                    Dim numArray2 As Integer() = New Integer() {k, (i + 1)}
                    list.Add(numArray2)
                End If
                If (((colorArray(0) = colorArray(3)) AndAlso Not ListView_Graphicals.smethod_0(colorArray(0))) AndAlso ListView_Graphicals.smethod_0(colorArray(1))) Then
                    Dim numArray3 As Integer() = New Integer() {(k + 1), i}
                    list.Add(numArray3)
                End If
                If (((colorArray(1) = colorArray(2)) AndAlso Not ListView_Graphicals.smethod_0(colorArray(1))) AndAlso ListView_Graphicals.smethod_0(colorArray(3))) Then
                    Dim numArray4 As Integer() = New Integer() {(k + 1), (i + 1)}
                    list.Add(numArray4)
                End If
            Next k
        Next i
        Dim j As Integer
        For j = 0 To list.Count - 1
            bitmap.SetPixel(list.Item(j)(0), list.Item(j)(1), ListView_Graphicals.AddColor(Color.Yellow, Color.FromArgb(&HD3, &HD3, &HD3)))
        Next j
        Return bitmap
    End Function

End Class
