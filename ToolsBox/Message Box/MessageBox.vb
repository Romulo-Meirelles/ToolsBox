Imports System.Windows.Forms
Imports ToolsBox.Utils
Imports System.ComponentModel

Friend Class MSG

    'DECLARA A VARIAVEIS DO TAMANHO DA TELA X = WIDTH E Y = HEIGHT
    Dim X, Y As Integer
    Private PositionTick As ScreenMost
#Region "Entradas"
    Private _Border As Boolean = False
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property Border() As Boolean
        Get
            Return _Border
        End Get
        Set(ByVal v As Boolean)
            _Border = v
            Invalidate()
        End Set
    End Property

    Private _PictureBorder As Boolean
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property PictureBorder() As Boolean
        Get
            Return _PictureBorder
        End Get
        Set(ByVal v As Boolean)
            _PictureBorder = v
            Invalidate()
        End Set
    End Property

    Private _Sound As SoundPlay
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property Sound() As SoundPlay
        Get
            Return _Sound
        End Get
        Set(ByVal v As SoundPlay)
            _Sound = v
            Invalidate()
        End Set
    End Property

    Private _Velocity As Integer
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property Velocity() As Integer
        Get
            Return _Velocity
        End Get
        Set(ByVal v As Integer)
            _Velocity = v
            Invalidate()
        End Set
    End Property

    Private _Edge_Spacing As Integer
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property Edge_Spacing() As Integer
        Get
            Return _Edge_Spacing
        End Get
        Set(ByVal v As Integer)
            _Edge_Spacing = v
            Invalidate()
        End Set
    End Property

    Private _OpacityVelocity As Double
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property OpacityVelocity() As Double
        Get
            Return _OpacityVelocity
        End Get
        Set(ByVal v As Double)
            _OpacityVelocity = v
            Invalidate()
        End Set
    End Property

    Private _Time_To_Open As Integer
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property Time_To_Open() As Integer
        Get
            Return _Time_To_Open
        End Get
        Set(ByVal v As Integer)
            _Time_To_Open = v
            Invalidate()
        End Set
    End Property

    Private _Time_To_Close As Integer
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property Time_To_Close() As Integer
        Get
            Return _Time_To_Close
        End Get
        Set(ByVal v As Integer)
            _Time_To_Close = v
            Invalidate()
        End Set
    End Property

    Private _Wait_To_Close As Integer
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property Wait_To_Close() As Integer
        Get
            Return _Wait_To_Close
        End Get
        Set(ByVal v As Integer)
            _Wait_To_Close = v
            Invalidate()
        End Set
    End Property

    Private _MeSize As Size
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property MeSize() As Size
        Get
            Return _MeSize
        End Get
        Set(ByVal v As Size)
            _MeSize = v
            Invalidate()
        End Set
    End Property

    Private _TituloForeColor As Color
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property TituloForeColor() As Color
        Get
            Return _TituloForeColor
        End Get
        Set(ByVal v As Color)
            _TituloForeColor = v
            Invalidate()
        End Set
    End Property

    Private _TituloFont As Font
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property TituloFont() As Font
        Get
            Return _TituloFont
        End Get
        Set(ByVal v As Font)
            _TituloFont = v
            Invalidate()
        End Set
    End Property

    Private _TituloBackColor As Color
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property TituloBackColor() As Color
        Get
            Return _TituloBackColor
        End Get
        Set(ByVal v As Color)
            _TituloBackColor = v
            Invalidate()
        End Set
    End Property

    Private _TituloSize As Size
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property TituloSize() As Size
        Get
            Return _TituloSize
        End Get
        Set(ByVal v As Size)
            _TituloSize = v
            Invalidate()
        End Set
    End Property

    Private _TextoForeColor As Color
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property TextoForeColor() As Color
        Get
            Return _TextoForeColor
        End Get
        Set(ByVal v As Color)
            _TextoForeColor = v
            Invalidate()
        End Set
    End Property

    Private _TextoBackColor As Color
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property TextoBackColor() As Color
        Get
            Return _TextoBackColor
        End Get
        Set(ByVal v As Color)
            _TextoBackColor = v
            Invalidate()
        End Set
    End Property

    Private _TextoFont As Font
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property TextoFont() As Font
        Get
            Return _TextoFont
        End Get
        Set(ByVal v As Font)
            _TextoFont = v
            Invalidate()
        End Set
    End Property

    Private _TextoSize As Size
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property TextoSize() As Size
        Get
            Return _TextoSize
        End Get
        Set(ByVal v As Size)
            _TextoSize = v
            Invalidate()
        End Set
    End Property

    Private _GradientBottonLeft As Color
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property GradientBottonLeft() As Color
        Get
            Return _GradientBottonLeft
        End Get
        Set(ByVal v As Color)
            _GradientBottonLeft = v
            Invalidate()
        End Set
    End Property

    Private _GradientBottonRight As Color
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property GradientBottonRight() As Color
        Get
            Return _GradientBottonRight
        End Get
        Set(ByVal v As Color)
            _GradientBottonRight = v
            Invalidate()
        End Set
    End Property

    Private _GradientTopLeft As Color
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property GradientTopLeft() As Color
        Get
            Return _GradientTopLeft
        End Get
        Set(ByVal v As Color)
            _GradientTopLeft = v
            Invalidate()
        End Set
    End Property

    Private _GradientTopRight As Color
    <Category("ToolsBox Herramienta")> _
    Protected Friend Property GradientTopRight() As Color
        Get
            Return _GradientTopRight
        End Get
        Set(ByVal v As Color)
            _GradientTopRight = v
            Invalidate()
        End Set
    End Property
#End Region


    Protected Friend Enum ScreenMost
        TopRight
        TopLeft
        BottonRight
        BottonLeft
        TopRightSide
        TopLeftSide
        BottonRightSide
        BottonLeftSide
        TopRightOpacity
        TopLeftOpacity
        BottonRightOpacity
        BottonLeftOpacity
    End Enum

    Protected Friend Enum MyColor
        White
        Black
        Green
        Blue
        Red
        Yellow
        NoColor
    End Enum

    Protected Friend Enum SoundPlay
        None
        Play01
        Mensseger
        Play02
        Play03
        Play04
    End Enum

    Private Sub LoadScreen(ByVal Position As ScreenMost)

        'DIZ A X = LARGURA DA TELA. (ONDE O FORM FICARÁ NA LATERAL DIREITA DA TELA)
        'DIZ A Y = TAMANHO DA TELA. (ONDE O FORM INICIARÁ, ABAIXO DA BARRA DE TAREFAS)
        Select Case Position
            Case ScreenMost.TopRight
                X = Screen.PrimaryScreen.WorkingArea.Width - Width - _Edge_Spacing
                Y = Screen.PrimaryScreen.WorkingArea.Height - Screen.PrimaryScreen.WorkingArea.Height - Height

            Case ScreenMost.TopLeft
                X = Screen.PrimaryScreen.WorkingArea.Width - Screen.PrimaryScreen.WorkingArea.Width + _Edge_Spacing
                Y = Screen.PrimaryScreen.WorkingArea.Height - Screen.PrimaryScreen.WorkingArea.Height - Height

            Case ScreenMost.BottonRight
                X = Screen.PrimaryScreen.WorkingArea.Width - Width - _Edge_Spacing
                Y = Screen.PrimaryScreen.WorkingArea.Height

            Case ScreenMost.BottonLeft
                X = Screen.PrimaryScreen.WorkingArea.Width - Screen.PrimaryScreen.WorkingArea.Width + _Edge_Spacing
                Y = Screen.PrimaryScreen.WorkingArea.Height + Height

            Case ScreenMost.TopRightSide
                X = Screen.PrimaryScreen.WorkingArea.Width
                Y = Screen.PrimaryScreen.WorkingArea.Height - Screen.PrimaryScreen.WorkingArea.Height + _Edge_Spacing

            Case ScreenMost.TopLeftSide

                X = Screen.PrimaryScreen.WorkingArea.Width - Screen.PrimaryScreen.WorkingArea.Width - Width
                Y = Screen.PrimaryScreen.WorkingArea.Height - Screen.PrimaryScreen.WorkingArea.Height + _Edge_Spacing

            Case ScreenMost.BottonRightSide
                X = Screen.PrimaryScreen.WorkingArea.Width
                Y = Screen.PrimaryScreen.WorkingArea.Height - Height - Edge_Spacing

            Case ScreenMost.BottonLeftSide
                X = Screen.PrimaryScreen.WorkingArea.Width - Screen.PrimaryScreen.WorkingArea.Width - Width
                Y = Screen.PrimaryScreen.WorkingArea.Height - Height - _Edge_Spacing

            Case ScreenMost.TopRightOpacity
                X = Screen.PrimaryScreen.WorkingArea.Width - Width - _Edge_Spacing
                Y = Screen.PrimaryScreen.WorkingArea.Height - Screen.PrimaryScreen.WorkingArea.Height + _Edge_Spacing
                Me.Opacity = 0

            Case ScreenMost.TopLeftOpacity
                X = Screen.PrimaryScreen.WorkingArea.Width - Screen.PrimaryScreen.WorkingArea.Width + _Edge_Spacing
                Y = Screen.PrimaryScreen.WorkingArea.Height - Screen.PrimaryScreen.WorkingArea.Height + _Edge_Spacing
                Me.Opacity = 0

            Case ScreenMost.BottonRightOpacity
                X = Screen.PrimaryScreen.WorkingArea.Width - Width - _Edge_Spacing
                Y = Screen.PrimaryScreen.WorkingArea.Height - Height
                Me.Opacity = 0

            Case ScreenMost.BottonLeftOpacity
                X = Screen.PrimaryScreen.WorkingArea.Width - Screen.PrimaryScreen.WorkingArea.Width + _Edge_Spacing
                Y = Screen.PrimaryScreen.WorkingArea.Height - Height
                Me.Opacity = 0
        End Select


        Me.Location = New Point(X, Y)

        'DIZ A FORM QUE FICARÁ AFRENTE DE QUALQUER TELA
        Me.TopMost = True
    End Sub

    Protected Friend Function MessageBoxPop(ByVal Title As String, ByVal Text As String, ByVal Position As ScreenMost, Optional ByVal MyImage As PictureBox = Nothing, Optional ByVal BackColor As MyColor = MyColor.NoColor)

        'TEMPO PARA ABRIR E FECHAR POPUPS
        Open_Timer.Interval = _Time_To_Open
        Close_Timer.Interval = _Time_To_Close
        Wait_Close_Timer.Interval = _Wait_To_Close

        'TAMANHO DA FORM MESSAGEBOX
        Me.Size = _MeSize

        'FAZ A LEITURA DA TELA E COLOCA NO CANTO DIREITO
        Select Case Position
            Case ScreenMost.TopRight
                LoadScreen(ScreenMost.TopRight)
                PositionTick = Position

            Case ScreenMost.TopLeft
                LoadScreen(ScreenMost.TopLeft)
                PositionTick = Position

            Case ScreenMost.BottonRight
                LoadScreen(ScreenMost.BottonRight)
                PositionTick = Position

            Case ScreenMost.BottonLeft
                LoadScreen(ScreenMost.BottonLeft)
                PositionTick = Position

            Case ScreenMost.TopRightSide
                LoadScreen(ScreenMost.TopRightSide)
                PositionTick = Position

            Case ScreenMost.TopLeftSide
                LoadScreen(ScreenMost.TopLeftSide)
                PositionTick = Position

            Case ScreenMost.BottonRightSide
                LoadScreen(ScreenMost.BottonRightSide)
                PositionTick = Position

            Case ScreenMost.BottonLeftSide
                LoadScreen(ScreenMost.BottonLeftSide)
                PositionTick = Position

            Case ScreenMost.TopRightOpacity
                LoadScreen(ScreenMost.TopRightOpacity)
                PositionTick = Position

            Case ScreenMost.TopLeftOpacity
                LoadScreen(ScreenMost.TopLeftOpacity)
                PositionTick = Position

            Case ScreenMost.BottonRightOpacity
                LoadScreen(ScreenMost.BottonRightOpacity)
                PositionTick = Position

            Case ScreenMost.BottonLeftOpacity
                LoadScreen(ScreenMost.BottonLeftOpacity)
                PositionTick = Position

        End Select


        Select Case BackColor
            Case MyColor.White
                Panel_Gradient.GradientBottomLeft = Color.White
                Panel_Gradient.GradientBottomRight = Color.White
                Panel_Gradient.GradientTopLeft = Color.White
                Panel_Gradient.GradientTopRight = Color.White

            Case MyColor.Black
                Panel_Gradient.GradientBottomLeft = Color.Black
                Panel_Gradient.GradientBottomRight = Color.Black
                Panel_Gradient.GradientTopLeft = Color.Black
                Panel_Gradient.GradientTopRight = Color.Black

            Case MyColor.Green
                Panel_Gradient.GradientBottomLeft = Color.FromArgb(29, 171, 118)
                Panel_Gradient.GradientBottomRight = Color.FromArgb(29, 171, 118)
                Panel_Gradient.GradientTopLeft = Color.FromArgb(29, 171, 118)
                Panel_Gradient.GradientTopRight = Color.Turquoise

            Case MyColor.Blue
                Panel_Gradient.GradientBottomLeft = Color.Blue
                Panel_Gradient.GradientBottomRight = Color.Blue
                Panel_Gradient.GradientTopLeft = Color.FromArgb(29, 171, 118)
                Panel_Gradient.GradientTopRight = Color.Turquoise

            Case MyColor.Yellow
                Panel_Gradient.GradientBottomLeft = Color.FromArgb(192, 192, 0)
                Panel_Gradient.GradientBottomRight = Color.FromArgb(192, 192, 0)
                Panel_Gradient.GradientTopLeft = Color.Gold
                Panel_Gradient.GradientTopRight = Color.Turquoise

            Case MyColor.Red
                Panel_Gradient.GradientBottomLeft = Color.Red
                Panel_Gradient.GradientBottomRight = Color.Red
                Panel_Gradient.GradientTopLeft = Color.Brown
                Panel_Gradient.GradientTopRight = Color.Gold
            Case Else
        End Select

        Try
            Titulo_lbl.Text = Title
            Texto_lbl.Text = Text
            Icon_Pic.SizeMode = MyImage.SizeMode
            Icon_Pic.Image = MyImage.Image
        Catch ex As Exception

        End Try

        If _Border = True Then
            CarregaBordas(Me, 5, 5)
        Else
        End If

        If _PictureBorder = True Then
            CarregaBordas(Icon_Pic, 5, 5)
        Else
        End If




        Me.Show()

        Select Case _Sound
            Case SoundPlay.None
            Case SoundPlay.Play01
                My.Computer.Audio.Play(My.Resources.Play01, AudioPlayMode.Background)
            Case SoundPlay.Play02
                My.Computer.Audio.Play(My.Resources.Play02, AudioPlayMode.Background)
            Case SoundPlay.Play03
                My.Computer.Audio.Play(My.Resources.Play03, AudioPlayMode.Background)
            Case SoundPlay.Play04
                My.Computer.Audio.Play(My.Resources.Play04, AudioPlayMode.Background)
            Case SoundPlay.Mensseger
                My.Computer.Audio.Play(My.Resources.Mensseger, AudioPlayMode.Background)
        End Select


        Open_Timer.Start()
        Return Nothing

    End Function

    Private Sub Open_timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Open_Timer.Tick

        'FAZ A LEITURA DA TELA E COLOCA NO CANTO DIREITO
        Select Case PositionTick
            Case ScreenMost.TopRight
                If Not Y >= Screen.PrimaryScreen.WorkingArea.Height - Screen.PrimaryScreen.WorkingArea.Height + _Edge_Spacing Then
                    'DIMINUI Y - 5
                    Y += _Velocity

                    'ATUALIZA A NOVA LOCALIZAÇÃO DA TELA
                    Me.Location = New Point(X, Y)

                    'SE Y FOR MAIOR QUE A LARGURA DA TELA ENTÃO CHAMA O ELSE
                Else
                    Wait_Close_Timer.Start()
                    Open_Timer.Stop()
                End If


            Case ScreenMost.TopLeft
                If Not Y >= Screen.PrimaryScreen.WorkingArea.Height - Screen.PrimaryScreen.WorkingArea.Height + _Edge_Spacing Then
                    Y += _Velocity
                    Me.Location = New Point(X, Y)
                Else
                    Wait_Close_Timer.Start()
                    Open_Timer.Stop()
                End If

            Case ScreenMost.BottonRight
                If Not Y <= Screen.PrimaryScreen.WorkingArea.Height - Me.Height Then
                    Y -= _Velocity
                    Me.Location = New Point(X, Y)
                Else
                    Wait_Close_Timer.Start()
                    Open_Timer.Stop()
                End If

            Case ScreenMost.BottonLeft
                If Not Y <= Screen.PrimaryScreen.WorkingArea.Height - Me.Height Then
                    Y -= _Velocity
                    Me.Location = New Point(X, Y)
                Else
                    Wait_Close_Timer.Start()
                    Open_Timer.Stop()
                End If

            Case ScreenMost.TopRightSide
                If Not X <= Screen.PrimaryScreen.WorkingArea.Width - Width Then
                    X -= _Velocity
                    Me.Location = New Point(X, Y)
                Else
                    Wait_Close_Timer.Start()
                    Open_Timer.Stop()
                End If

            Case ScreenMost.TopLeftSide
                If Not X >= Screen.PrimaryScreen.WorkingArea.Width - Screen.PrimaryScreen.WorkingArea.Width Then
                    X += _Velocity
                    Me.Location = New Point(X, Y)
                Else
                    Wait_Close_Timer.Start()
                    Open_Timer.Stop()
                End If

            Case ScreenMost.BottonRightSide
                If Not X <= Screen.PrimaryScreen.WorkingArea.Width - Me.Width Then
                    X -= _Velocity
                    Me.Location = New Point(X, Y)
                Else
                    Wait_Close_Timer.Start()
                    Open_Timer.Stop()
                End If
            Case ScreenMost.BottonLeftSide
                If Not X >= Screen.PrimaryScreen.WorkingArea.Width - Screen.PrimaryScreen.WorkingArea.Width Then
                    X += _Velocity
                    Me.Location = New Point(X, Y)
                Else
                    Wait_Close_Timer.Start()
                    Open_Timer.Stop()
                End If

            Case ScreenMost.TopRightOpacity
                If Not Me.Opacity >= 1.0 Then
                    Me.Opacity += _OpacityVelocity
                Else
                    Wait_Close_Timer.Start()
                    Open_Timer.Stop()
                End If

            Case ScreenMost.TopLeftOpacity
                If Not Me.Opacity >= 1.0 Then
                    Me.Opacity += _OpacityVelocity
                Else
                    Wait_Close_Timer.Start()
                    Open_Timer.Stop()
                End If

            Case ScreenMost.BottonRightOpacity
                If Not Me.Opacity >= 1.0 Then
                    Me.Opacity += _OpacityVelocity
                Else
                    Wait_Close_Timer.Start()
                    Open_Timer.Stop()
                End If

            Case ScreenMost.BottonLeftOpacity
                If Not Me.Opacity >= 1.0 Then
                    Me.Opacity += _OpacityVelocity
                Else
                    Wait_Close_Timer.Start()
                    Open_Timer.Stop()
                End If

        End Select


    End Sub

    Private Sub Close_timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Close_Timer.Tick


        Select Case PositionTick

            Case ScreenMost.TopRight
                Dim H As Integer = -Height
                'SE Y FOR MAIOR QUE 785 ENTÃO FECHA O POPUP 785
                If Y >= H Then
                    'ADICIONA Y + 6
                    Y -= _Velocity
                    'ATUALIZA A NOVA LOCALIZAÇÃO DA TELA
                    Me.Location = New Point(X, Y)
                Else
                    Close_Timer.Stop()
                    Wait_Close_Timer.Stop()
                    Me.Close()
                End If

            Case ScreenMost.TopLeft
                Dim H As Integer = -Height
                If Y >= H Then
                    Y -= _Velocity
                    Me.Location = New Point(X, Y)
                Else
                    Close_Timer.Stop()
                    Wait_Close_Timer.Stop()
                    Me.Close()
                End If

            Case ScreenMost.BottonRight

                Dim H As Integer = Screen.PrimaryScreen.WorkingArea.Height + Height
                If Y <= H Then
                    Y += _Velocity
                    Me.Location = New Point(X, Y)
                Else
                    Close_Timer.Stop()
                    Wait_Close_Timer.Stop()
                    Me.Close()
                End If

            Case ScreenMost.BottonLeft
                Dim H As Integer = Screen.PrimaryScreen.WorkingArea.Height + Height
                If Y <= H Then
                    Y += _Velocity
                    Me.Location = New Point(X, Y)
                Else
                    Close_Timer.Stop()
                    Wait_Close_Timer.Stop()
                    Me.Close()
                End If

            Case ScreenMost.TopRightSide
                Dim W As Integer = Screen.PrimaryScreen.WorkingArea.Width

                If X <= W Then
                    X += _Velocity
                    Me.Location = New Point(X, Y)
                Else
                    Close_Timer.Stop()
                    Wait_Close_Timer.Stop()
                    Me.Close()
                End If
            Case ScreenMost.TopLeftSide
                Dim W As Integer = Screen.PrimaryScreen.WorkingArea.Width - Screen.PrimaryScreen.WorkingArea.Width - Width
                If X >= W Then
                    X -= _Velocity
                    Me.Location = New Point(X, Y)
                Else
                    Close_Timer.Stop()
                    Wait_Close_Timer.Stop()
                    Me.Close()
                End If

            Case ScreenMost.BottonRightSide
                Dim W As Integer = Screen.PrimaryScreen.WorkingArea.Width + Width
                If X <= W Then
                    X += _Velocity
                    Me.Location = New Point(X, Y)
                Else
                    Close_Timer.Stop()
                    Wait_Close_Timer.Stop()
                    Me.Close()
                End If
            Case ScreenMost.BottonLeftSide

                Dim W As Integer = Screen.PrimaryScreen.WorkingArea.Width - Screen.PrimaryScreen.WorkingArea.Width - Width
                If X >= W Then
                    X -= _Velocity
                    Me.Location = New Point(X, Y)
                Else
                    Close_Timer.Stop()
                    Wait_Close_Timer.Stop()
                    Me.Close()
                End If

            Case ScreenMost.TopRightOpacity
                If Not Me.Opacity <= 0.0 Then
                    Me.Opacity -= _OpacityVelocity
                Else
                    Close_Timer.Stop()
                    Wait_Close_Timer.Stop()
                    Me.Close()
                End If

            Case ScreenMost.TopLeftOpacity
                If Not Me.Opacity <= 0.0 Then
                    Me.Opacity -= _OpacityVelocity
                Else
                    Close_Timer.Stop()
                    Wait_Close_Timer.Stop()
                    Me.Close()
                End If

            Case ScreenMost.BottonRightOpacity
                If Not Me.Opacity <= 0.0 Then
                    Me.Opacity -= _OpacityVelocity
                Else
                    Close_Timer.Stop()
                    Wait_Close_Timer.Stop()
                    Me.Close()
                End If

            Case ScreenMost.BottonLeftOpacity
                If Not Me.Opacity <= 0.0 Then
                    Me.Opacity -= _OpacityVelocity
                Else
                    Close_Timer.Stop()
                    Wait_Close_Timer.Stop()
                    Me.Close()
                End If
        End Select

    End Sub

    Private Sub Wait_Closer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Wait_Close_Timer.Tick
        'TIMER DE 5000 INTERVAL / ACTIVE FALSE
        Close_Timer.Start()
    End Sub

    Private Sub Close_Pic_Click(sender As System.Object, e As System.EventArgs) Handles Close_Pic.Click
        Me.Close()
    End Sub

End Class





Public Class MessageBox
    'DECLARA A VARIAVEIS DO TAMANHO DA TELA X = WIDTH E Y = HEIGHT
    Dim X, Y As Integer
    Private PositionTick As ScreenMost
    Private M As New MSG

#Region "Entradas"
    Private _Border As Boolean = False
    <Category("ToolsBox Herramienta")> _
    Public Property Border() As Boolean
        Get
            Return _Border
        End Get
        Set(ByVal v As Boolean)
            _Border = v
            M.Invalidate()
        End Set
    End Property

    Private _PictureBorder As Boolean = False
    <Category("ToolsBox Herramienta")> _
    Public Property PictureBorder() As Boolean
        Get
            Return _PictureBorder
        End Get
        Set(ByVal v As Boolean)
            _PictureBorder = v
            M.Invalidate()
        End Set
    End Property

    Private _Sound As SoundPlay = SoundPlay.None
    <Category("ToolsBox Herramienta")> _
    Public Property Sound() As SoundPlay
        Get
            Return _Sound
        End Get
        Set(ByVal v As SoundPlay)
            _Sound = v
            M.Invalidate()
        End Set
    End Property

    Private _Velocity As Integer = 6
    <Category("ToolsBox Herramienta")> _
    Public Property Velocity() As Integer
        Get
            Return _Velocity
        End Get
        Set(ByVal v As Integer)
            _Velocity = v
            M.Invalidate()
        End Set
    End Property

    Private _Edge_Spacing As Integer = 5
    <Category("ToolsBox Herramienta")> _
    Public Property Edge_Spacing() As Integer
        Get
            Return _Edge_Spacing
        End Get
        Set(ByVal v As Integer)
            _Edge_Spacing = v
            M.Invalidate()
        End Set
    End Property

    Private _OpacityVelocity As Double = 0.01
    <Category("ToolsBox Herramienta")> _
    Public Property OpacityVelocity() As Double
        Get
            Return _OpacityVelocity
        End Get
        Set(ByVal v As Double)
            _OpacityVelocity = v
            M.Invalidate()
        End Set
    End Property

    Private _Time_To_Open As Integer = 8
    <Category("ToolsBox Herramienta")> _
    Public Property Time_To_Open() As Integer
        Get
            Return _Time_To_Open
        End Get
        Set(ByVal v As Integer)
            _Time_To_Open = v
            M.Invalidate()
        End Set
    End Property

    Private _Time_To_Close As Integer = 8
    <Category("ToolsBox Herramienta")> _
    Public Property Time_To_Close() As Integer
        Get
            Return _Time_To_Close
        End Get
        Set(ByVal v As Integer)
            _Time_To_Close = v
            M.Invalidate()
        End Set
    End Property

    Private _Wait_To_Close As Integer = 5000
    <Category("ToolsBox Herramienta")> _
    Public Property Wait_To_Close() As Integer
        Get
            Return _Wait_To_Close
        End Get
        Set(ByVal v As Integer)
            _Wait_To_Close = v
            M.Invalidate()
        End Set
    End Property

    Private _MeSize As Size = New Size(350, 150)
    <Category("ToolsBox Herramienta")> _
    Public Property MeSize() As Size
        Get
            Return _MeSize
        End Get
        Set(ByVal v As Size)
            _MeSize = v
            M.Invalidate()
        End Set
    End Property

    Private _TituloForeColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TituloForeColor() As Color
        Get
            Return _TituloForeColor
        End Get
        Set(ByVal v As Color)
            _TituloForeColor = v
            M.Invalidate()
        End Set
    End Property

    Private _TituloFont As Font
    <Category("ToolsBox Herramienta")> _
    Public Property TituloFont() As Font
        Get
            Return _TituloFont
        End Get
        Set(ByVal v As Font)
            _TituloFont = v
            M.Invalidate()
        End Set
    End Property

    Private _TituloBackColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TituloBackColor() As Color
        Get
            Return _TituloBackColor
        End Get
        Set(ByVal v As Color)
            _TituloBackColor = v
            M.Invalidate()
        End Set
    End Property

    Private _TituloSize As Size
    <Category("ToolsBox Herramienta")> _
    Public Property TituloSize() As Size
        Get
            Return _TituloSize
        End Get
        Set(ByVal v As Size)
            _TituloSize = v
            M.Invalidate()
        End Set
    End Property

    Private _TextoForeColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TextoForeColor() As Color
        Get
            Return _TextoForeColor
        End Get
        Set(ByVal v As Color)
            _TextoForeColor = v
            M.Invalidate()
        End Set
    End Property

    Private _TextoBackColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TextoBackColor() As Color
        Get
            Return _TextoBackColor
        End Get
        Set(ByVal v As Color)
            _TextoBackColor = v
            M.Invalidate()
        End Set
    End Property

    Private _TextoFont As Font
    <Category("ToolsBox Herramienta")> _
    Public Property TextoFont() As Font
        Get
            Return _TextoFont
        End Get
        Set(ByVal v As Font)
            _TextoFont = v
            M.Invalidate()
        End Set
    End Property

    Private _TextoSize As Size
    <Category("ToolsBox Herramienta")> _
    Public Property TextoSize() As Size
        Get
            Return _TextoSize
        End Get
        Set(ByVal v As Size)
            _TextoSize = v
            M.Invalidate()
        End Set
    End Property

    Private _GradientBottonLeft As Color
    <Category("ToolsBox Herramienta")> _
    Public Property GradientBottonLeft() As Color
        Get
            Return _GradientBottonLeft
        End Get
        Set(ByVal v As Color)
            _GradientBottonLeft = v
            M.Invalidate()
        End Set
    End Property

    Private _GradientBottonRight As Color
    <Category("ToolsBox Herramienta")> _
    Public Property GradientBottonRight() As Color
        Get
            Return _GradientBottonRight
        End Get
        Set(ByVal v As Color)
            _GradientBottonRight = v
            M.Invalidate()
        End Set
    End Property

    Private _GradientTopLeft As Color
    <Category("ToolsBox Herramienta")> _
    Public Property GradientTopLeft() As Color
        Get
            Return _GradientTopLeft
        End Get
        Set(ByVal v As Color)
            _GradientTopLeft = v
            M.Invalidate()
        End Set
    End Property

    Private _GradientTopRight As Color
    <Category("ToolsBox Herramienta")> _
    Public Property GradientTopRight() As Color
        Get
            Return _GradientTopRight
        End Get
        Set(ByVal v As Color)
            _GradientTopRight = v
            M.Invalidate()
        End Set
    End Property
#End Region

    Public Enum ScreenMost
        TopRight
        TopLeft
        BottonRight
        BottonLeft
        TopRightSide
        TopLeftSide
        BottonRightSide
        BottonLeftSide
        TopRightOpacity
        TopLeftOpacity
        BottonRightOpacity
        BottonLeftOpacity
    End Enum

    Public Enum MyColor
        White
        Black
        Green
        Blue
        Red
        Yellow
        NoColor
    End Enum

    Public Enum SoundPlay
        None
        Play01
        Mensseger
        Play02
        Play03
        Play04
    End Enum

    Public Function MessageBoxShow(ByVal Title As String, ByVal Text As String, ByVal Position As ScreenMost, Optional ByVal MyImage As PictureBox = Nothing, Optional ByVal BackColor As MyColor = MyColor.NoColor)

        M.Border = _Border
        M.PictureBorder = _PictureBorder
        M.Sound = _Sound
        M.Velocity = _Velocity
        M.Edge_Spacing = _Edge_Spacing
        M.OpacityVelocity = _OpacityVelocity
        M.Time_To_Open = _Time_To_Open
        M.Time_To_Close = _Time_To_Close
        M.Wait_To_Close = _Wait_To_Close
        M.MeSize = _MeSize
        M.TituloForeColor = _TituloForeColor
        M.TituloFont = _TituloFont
        M.TituloBackColor = _TituloBackColor
        M.TituloSize = _TituloSize
        M.TextoForeColor = _TextoForeColor
        M.TextoFont = _TextoFont
        M.TextoBackColor = _TextoBackColor
        M.TextoSize = _TextoSize
        M.GradientBottonLeft = _GradientBottonLeft
        M.GradientBottonRight = _GradientBottonRight
        M.GradientTopLeft = _GradientTopLeft
        M.GradientTopRight = _GradientTopRight
        M.MessageBoxPop(Title, Text, Position, MyImage, BackColor)
        Return Nothing
    End Function

End Class