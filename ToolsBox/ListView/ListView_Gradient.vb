Imports System.ComponentModel
Imports System.Threading
Imports System.Delegate

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.ListView))> _
Public Class ListView_Gradient
    Inherits ListView

    ' Fields
    Private color_0 As Color = Color.White
    Private color_1 As Color = Color.White
    Private color_2 As Color = Color.White
    Private color_3 As Color = Color.White
    Private icontainer_0 As IContainer
    Private int_0 As Integer = 10
    Private BMP As Bitmap


    ' Methods
    Public Sub New()
        Me.method_1()

        Dim Thread_1 As Threading.Thread
        'Chama a função dentro da Thread
        Control.CheckForIllegalCrossThreadCalls = False
        Thread_1 = New Threading.Thread(AddressOf Me.method_1)
        Thread_1.IsBackground = True
        Thread_1.Start()

        Dim Thread As Threading.Thread
        'Chama a função dentro da Thread
        Control.CheckForIllegalCrossThreadCalls = False
        Thread = New Threading.Thread(AddressOf Me.method_0)
        Thread.IsBackground = True
        Thread.Start()

        '  Thread = Threading.Thread
        '  Me.method_0()
    End Sub

    Private Sub GradientPanel_Resize(ByVal sender As Object, ByVal e As EventArgs)
        On Error Resume Next
        Me.method_0()
        Me.BackgroundImage = New Bitmap(BMP, MyBase.Width, MyBase.Height)
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If (disposing AndAlso (Not Me.icontainer_0 Is Nothing)) Then
            Me.icontainer_0.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub method_0()
        BMP = New Bitmap(Me.Quality, Me.Quality)
        ' bitmap = New Bitmap(MyBase.Width, MyBase.Height)
        If (Me.Quality = 100) Then
            BMP = New Bitmap(MyBase.Width, MyBase.Height)
        End If

        Dim i As Integer

        For i = 0 To BMP.Width - 1
            Dim color As Color = ListView_ColorTransition.getColorScale(Integer.Parse(Math.Round(CDbl(((CDbl(i) / CDbl(BMP.Width)) * 100)), 0).ToString), Me.GradientTopLeft, Me.GradientTopRight)
            Dim j As Integer
            For j = 0 To BMP.Height - 1
                Dim color2 As Color = ListView_ColorTransition.getColorScale(Integer.Parse(Math.Round(CDbl(((CDbl(j) / CDbl(BMP.Height)) * 100)), 0).ToString), Me.GradientBottomLeft, Me.GradientBottomRight)
                BMP.SetPixel(i, j, ListView_Graphicals.AddColor(color, color2))
            Next j
        Next i

        If (Me.BackgroundImageLayout <> ImageLayout.Stretch) Then
            Me.BackgroundImageLayout = ImageLayout.Stretch
        End If

        ' Me.BackgroundImage = BMP
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
            Me.Invalidate()
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
            Me.Invalidate()
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
            Me.Invalidate()
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
            Me.Invalidate()
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
            Me.Invalidate()
        End Set
    End Property

End Class