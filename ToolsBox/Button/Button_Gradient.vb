Imports System.ComponentModel
Imports System.Threading
Imports System.Delegate

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.Button))> _
Public Class Button_Gradient
    Inherits Button

    ' Fields
    Private color_0 As Color = Color.White
    Private color_1 As Color = Color.White
    Private color_2 As Color = Color.White
    Private color_3 As Color = Color.White
    Private icontainer_0 As IContainer
    Private int_0 As Integer = 10

    ' Methods
    Public Sub New()
        Me.method_1()
        Me.method_0()
    End Sub

    Private Sub GradientPanel_Resize(ByVal sender As Object, ByVal e As EventArgs)
        Me.method_0()
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If (disposing AndAlso (Not Me.icontainer_0 Is Nothing)) Then
            Me.icontainer_0.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub method_0()
        Dim bitmap As New Bitmap(Me.Quality, Me.Quality)
        If (Me.Quality = 100) Then
            bitmap = New Bitmap(MyBase.Width, MyBase.Height)
        End If
        Dim i As Integer
        For i = 0 To bitmap.Width - 1
            Dim color As Color = ColorTransition.getColorScale(Integer.Parse(Math.Round(CDbl(((CDbl(i) / CDbl(bitmap.Width)) * 100)), 0).ToString), Me.GradientTopLeft, Me.GradientTopRight)
            Dim j As Integer
            For j = 0 To bitmap.Height - 1
                Dim color2 As Color = ColorTransition.getColorScale(Integer.Parse(Math.Round(CDbl(((CDbl(j) / CDbl(bitmap.Height)) * 100)), 0).ToString), Me.GradientBottomLeft, Me.GradientBottomRight)
                bitmap.SetPixel(i, j, Graphicals.AddColor(color, color2))
            Next j
        Next i
        If (Me.BackgroundImageLayout <> ImageLayout.Stretch) Then
            Me.BackgroundImageLayout = ImageLayout.Stretch
        End If
        Me.BackgroundImage = bitmap
    End Sub

    Private Sub method_1()
        MyBase.SuspendLayout()
        AddHandler MyBase.Resize, New EventHandler(AddressOf Me.GradientPanel_Resize)
        MyBase.ResumeLayout(False)
    End Sub

    ' Properties
    <Category("ToolsBox Herramienta")> _
    Public Property GradientBottomLeft As Color
        Get
            Return Me.color_2
        End Get
        Set(ByVal value As Color)
            Me.color_2 = value
            Me.method_0()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property GradientBottomRight As Color
        Get
            Return Me.color_3
        End Get
        Set(ByVal value As Color)
            Me.color_3 = value
            Me.method_0()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property GradientTopLeft As Color
        Get
            Return Me.color_0
        End Get
        Set(ByVal value As Color)
            Me.color_0 = value
            Me.method_0()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property GradientTopRight As Color
        Get
            Return Me.color_1
        End Get
        Set(ByVal value As Color)
            Me.color_1 = value
            Me.method_0()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property Quality As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            Me.int_0 = value
            Me.method_0()
        End Set
    End Property


    'Public Class ColorTransition
    '    Inherits Component
    '    ' Events
    '    Public Event OnValueChange As EventHandler

    '    Public Sub New()
    '        Me.color_0 = Color.White
    '        Me.color_1 = Color.White
    '        Me.color_2 = Color.White
    '        Me.method_1()
    '    End Sub

    '    Public Sub New(ByVal container As IContainer)
    '        Me.color_0 = Color.White
    '        Me.color_1 = Color.White
    '        Me.color_2 = Color.White
    '        container.Add(Me)
    '        Me.method_1()
    '    End Sub

    '    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    '        If (disposing AndAlso (Not Me.icontainer_0 Is Nothing)) Then
    '            Me.icontainer_0.Dispose()
    '        End If
    '        MyBase.Dispose(disposing)
    '    End Sub

    '    Public Shared Function getColorScale(ByVal Passentage As Integer, ByVal startColor As Color, ByVal endColor As Color) As Color
    '        Try
    '            Dim red As Integer = Integer.Parse(Math.Round(CDbl((startColor.R + (((endColor.R - startColor.R) * Passentage) * 0.01))), 0).ToString)
    '            Dim green As Integer = Integer.Parse(Math.Round(CDbl((startColor.G + (((endColor.G - startColor.G) * Passentage) * 0.01))), 0).ToString)
    '            Dim blue As Integer = Integer.Parse(Math.Round(CDbl((startColor.B + (((endColor.B - startColor.B) * Passentage) * 0.01))), 0).ToString)
    '            Return Color.FromArgb(&HFF, red, green, blue)
    '        Catch exception1 As Exception
    '            Return startColor
    '        End Try
    '    End Function

    '    Private Sub method_0()
    '        Dim num As Integer
    '        Dim num2 As Integer
    '        Dim color As Color = ColorTransition.getColorScale(Me.ProgessValue, Me.Color1, Me.Color2)
    '        If (color <> Me.Value) Then
    '            Me.color_2 = color
    '            If (Not Me.eventHandler_0 Is Nothing) Then
    '                Me.eventHandler_0.Invoke(Me, New EventArgs)
    '                Return
    '            End If
    '        End If
    '        Do While (num = num2)
    '            num2 = 1
    '            Dim num3 As Integer = num
    '            num = 1
    '            If (1 > num3) Then
    '                Return
    '            End If
    '        Loop
    '    End Sub

    '    Private Sub method_1()
    '        Me.icontainer_0 = New Container
    '    End Sub


    '    ' Properties
    '    Public Property Color1 As Color
    '        Get
    '            Return Me.color_0
    '        End Get
    '        Set(ByVal value As Color)
    '            Me.color_0 = value
    '            Me.method_0()
    '        End Set
    '    End Property

    '    Public Property Color2 As Color
    '        Get
    '            Return Me.color_1
    '        End Get
    '        Set(ByVal value As Color)
    '            Me.color_1 = value
    '            Me.method_0()
    '        End Set
    '    End Property

    '    Public Property ProgessValue As Integer
    '        Get
    '            Return Me.int_0
    '        End Get
    '        Set(ByVal value As Integer)
    '            Me.int_0 = value
    '            Me.method_0()
    '        End Set
    '    End Property

    '    Public ReadOnly Property Value As Color
    '        Get
    '            Return Me.color_2
    '        End Get
    '    End Property


    '    ' Fields
    '    Private color_0 As Color
    '    Private color_1 As Color
    '    Private color_2 As Color
    '    Private icontainer_0 As IContainer
    '    Private int_0 As Integer
    '    Private eventHandler_0 As EventHandler




    'End Class

    Friend Class Graphicals
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
                    If Not Graphicals.smethod_0(bitmap.GetPixel(j, i)) Then
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
                    If Not Graphicals.smethod_0(bitmap.GetPixel(j, i)) Then
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
                    If (((colorArray(1) = colorArray(2)) AndAlso Not Graphicals.smethod_0(colorArray(1))) AndAlso Graphicals.smethod_0(colorArray(0))) Then
                        Dim item As Integer() = New Integer() {k, i}
                        list.Add(item)
                    End If
                    If (((colorArray(0) = colorArray(3)) AndAlso Not Graphicals.smethod_0(colorArray(0))) AndAlso Graphicals.smethod_0(colorArray(2))) Then
                        Dim numArray2 As Integer() = New Integer() {k, (i + 1)}
                        list.Add(numArray2)
                    End If
                    If (((colorArray(0) = colorArray(3)) AndAlso Not Graphicals.smethod_0(colorArray(0))) AndAlso Graphicals.smethod_0(colorArray(1))) Then
                        Dim numArray3 As Integer() = New Integer() {(k + 1), i}
                        list.Add(numArray3)
                    End If
                    If (((colorArray(1) = colorArray(2)) AndAlso Not Graphicals.smethod_0(colorArray(1))) AndAlso Graphicals.smethod_0(colorArray(3))) Then
                        Dim numArray4 As Integer() = New Integer() {(k + 1), (i + 1)}
                        list.Add(numArray4)
                    End If
                Next k
            Next i
            Dim j As Integer
            For j = 0 To list.Count - 1
                bitmap.SetPixel(list.Item(j)(0), list.Item(j)(1), Graphicals.AddColor(Color.Yellow, Color.FromArgb(&HD3, &HD3, &HD3)))
            Next j
            Return bitmap
        End Function

    End Class
End Class









