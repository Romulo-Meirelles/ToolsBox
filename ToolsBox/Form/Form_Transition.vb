Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging


'Public Class Form_Transition
'    Inherits Component
'    'Implements IExtenderProvider
'    ' Events

'    Public Event AllAnimationsCompleted As EventHandler

'    Public Event AnimationCompleted As EventHandler(Of AnimationCompletedEventArg)

'    Public Event FramePainted As EventHandler(Of PaintEventArgs)

'    Public Event MouseDown As EventHandler(Of MouseEventArgs)

'    Public Event NonLinearTransfromNeeded As EventHandler(Of NonLinearTransfromNeededEventArg)

'    Public Event TransfromNeeded As EventHandler(Of TransfromNeededEventArg)



'    ' Fields
'    Public animation As AnimationClass
'    Public clipRectangle As Rectangle
'    Public control As Control
'    Public controller As ControllerClass
'    <CompilerGenerated()> _
'    Private dateTime_0 As DateTime
'    Public IsActive As Boolean
'    Public mode As AnimateMode
'    <CompilerGenerated()> _
'    Private animation_0 As AnimationClass
'    Private animationType_0 As AnimationTypeEnum
'    Private control_0 As Control
'    <CompilerGenerated()> _
'    Private cursor_0 As Cursor
'    Private ReadOnly dictionary_0 As Dictionary(Of Control, DecorationControl)
'    <CompilerGenerated()> _
'    Private float_0 As Single
'    Private icontainer_0 As IContainer
'    <CompilerGenerated()> _
'    Private int_0 As Integer
'    <CompilerGenerated()> _
'    Private int_1 As Integer
'    Private int_2 As Integer
'    Private list_0 As List(Of QueueItem)
'    Protected queue As List(Of QueueItem)
'    Private thread_0 As Thread
'    Private timer_0 As System.Windows.Forms.Timer

'    ' Methods
'    Public Sub New()
'        Me.queue = New List(Of QueueItem)
'        Me.list_0 = New List(Of QueueItem)
'        Me.dictionary_0 = New Dictionary(Of Control, DecorationControl)
'        Me.Init()
'    End Sub

'    Public Sub New(ByVal container As IContainer)
'        Me.queue = New List(Of QueueItem)
'        Me.list_0 = New List(Of QueueItem)
'        Me.dictionary_0 = New Dictionary(Of Control, DecorationControl)
'        container.Add(Me)
'        Me.Init()
'    End Sub

'    Public Sub AddToQueue(ByVal control As Control, ByVal mode As AnimateMode, Optional ByVal parallel As Boolean = True, Optional ByVal animation As Animation = Nothing, Optional ByVal clipRectangle As Rectangle = New Rectangle())
'        Dim num As Integer
'        Dim num2 As Integer
'        If (animation Is Nothing) Then
'            animation = Me.DefaultAnimation
'        End If
'        If TypeOf control Is IFakeControl Then
'            control.Visible = False
'            Return
'        End If
'        Dim item As New QueueItem With {.IsActive = True}
'        '        .animation = animation, _
'        '.control = control, _
'        '.IsActive = parallel, _
'        '.mode = mode, _
'        '.clipRectangle = clipRectangle _
'        '}
'        If (mode <> AnimateMode.Show) Then
'            If ((mode = AnimateMode.Hide) AndAlso Not control.Visible) Then
'                Dim item2 As New QueueItem With { _
'                    .control = control, _
'                    .mode = mode _
'                }
'                Me.method_7(item2)
'                Return
'            End If
'        ElseIf control.Visible Then
'            Dim item3 As New QueueItem With { _
'                .control = control, _
'                .mode = mode _
'            }
'            Me.method_7(item3)
'            Return
'        End If
'        Dim queue As List(Of QueueItem) = Me.queue
'        SyncLock queue
'            Me.queue.Add(item)
'        End SyncLock
'        queue = Me.list_0
'        SyncLock queue
'            Me.list_0.Add(item)
'            GoTo Label_00F5
'        End SyncLock
'Label_00E6:
'        num2 = 1
'        Dim num3 As Integer = num
'        num = 1
'        If (1 > num3) Then
'            Return
'        End If
'Label_00F5:
'        If (num = num2) Then
'            GoTo Label_00E6
'        End If
'    End Sub

'    Public Sub BeginUpdate(ByVal control As Control, Optional ByVal parallel As Boolean = False, Optional ByVal animation As Animation = Nothing, Optional ByVal clipRectangle As Rectangle = New Rectangle())
'        Me.AddToQueue(control, AnimateMode.BeginUpdate, parallel, animation, clipRectangle)
'        Dim flag As Boolean = False
'        Do While True
'            flag = False
'            Dim queue As List(Of QueueItem) = Me.queue
'            SyncLock queue
'                Dim item As QueueItem
'                For Each item In Me.queue
'                    If (((item.control Is control) AndAlso (item.mode = AnimateMode.BeginUpdate)) AndAlso (item.controller Is Nothing)) Then
'                        flag = True
'                    End If
'                Next
'            End SyncLock
'            If flag Then
'                Application.DoEvents()
'            End If
'            If Not flag Then
'                Return
'            End If
'        Loop
'    End Sub

'    Private Sub BunifuTransition_Disposed(ByVal sender As Object, ByVal e As EventArgs)
'        Me.ClearQueue()
'        If (Me.thread_0 Is Nothing) Then
'            Dim num As Integer
'            Dim num2 As Integer
'            Do While (num = num2)
'                num2 = 1
'                Dim num3 As Integer = num
'                num = 1
'                If (1 > num3) Then
'                    Return
'                End If
'            Loop
'        Else
'            Me.thread_0.Abort()
'        End If
'    End Sub

'    Public Function CanExtend(ByVal extendee As Object) As Boolean
'        Return TypeOf extendee Is Control
'    End Function

'    Public Sub ClearQueue()
'        Dim list As List(Of QueueItem) = Nothing
'        Dim queue As List(Of QueueItem) = Me.queue
'        SyncLock queue
'            list = New List(Of QueueItem)(Me.queue)
'            Me.queue.Clear()
'        End SyncLock
'        Using enumerator As Enumerator(Of QueueItem) = list.GetEnumerator
'            Do While enumerator.MoveNext
'                Dim class2 As New Class5 With { _
'                    .item = enumerator.Current _
'                }
'                If (Not class2.item.control Is Nothing) Then
'                        class2.item.control.BeginInvoke(New MethodInvoker(AddressOf class2.<ClearQueue>b__0))
'                End If
'                Dim e As New AnimationCompletedEventArg With { _
'                    .Animation = class2.item.animation, _
'                    .Control = class2.item.control, _
'                    .Mode = class2.item.mode _
'                }
'                Me.OnAnimationCompleted(e)
'            Loop
'        End Using
'        If (list.Count <= 0) Then
'            Dim num As Integer
'            Dim num2 As Integer
'            Do While (num = num2)
'                num2 = 1
'                Dim num3 As Integer = num
'                num = 1
'                If (1 > num3) Then
'                    Return
'                End If
'            Loop
'        Else
'            Me.OnAllAnimationsCompleted()
'        End If
'    End Sub

'    Public Sub EndUpdate(ByVal control As Control)
'        Dim num As Integer
'        Dim num2 As Integer
'        Dim queue As List(Of QueueItem) = Me.queue
'        SyncLock queue
'            Dim item As QueueItem
'            For Each item In Me.queue
'                If ((item.control Is control) AndAlso (item.mode = AnimateMode.BeginUpdate)) Then
'                    item.controller.EndUpdate()
'                    item.mode = AnimateMode.Update
'                End If
'            Next
'            GoTo Label_007E
'        End SyncLock
'Label_006F:
'        num2 = 1
'        Dim num3 As Integer = num
'        num = 1
'        If (1 > num3) Then
'            Return
'        End If
'Label_007E:
'        If (num = num2) Then
'            GoTo Label_006F
'        End If
'    End Sub

'    Public Sub EndUpdateSync(ByVal control As Control)
'        Me.EndUpdate(control)
'        Me.WaitAnimation(control)
'    End Sub

'    Public Function GetDecoration(ByVal control As Control) As DecorationType
'        If Me.dictionary_0.ContainsKey(control) Then
'            Return Me.dictionary_0.Item(control).DecorationType
'        End If
'        Return DecorationType.None
'    End Function

'    Public Sub Hide(ByVal control As Control, Optional ByVal parallel As Boolean = False, Optional ByVal animation As Animation = Nothing)
'        Me.AddToQueue(control, AnimateMode.Hide, parallel, animation, New Rectangle)
'    End Sub

'    Public Sub HideSync(ByVal control As Control, Optional ByVal parallel As Boolean = False, Optional ByVal animation As Animation = Nothing)
'        Me.Hide(control, parallel, animation)
'        Me.WaitAnimation(control)
'    End Sub

'    Protected Overridable Sub Init()
'        Me.AnimationType = AnimationType.VertSlide
'        Me.DefaultAnimation = New Animation
'        Me.MaxAnimationTime = &H5DC
'        Me.TimeStep = 0.02!
'        Me.Interval = 10
'        AddHandler MyBase.Disposed, New EventHandler(AddressOf Me.BunifuTransition_Disposed)
'        Me.timer_0 = New Timer
'        AddHandler Me.timer_0.Tick, New EventHandler(AddressOf Me.timer_0_Tick)
'        Me.timer_0.Interval = 1
'        Me.timer_0.Start()
'    End Sub

'    Private Sub method_0()
'        Me.thread_0 = New Thread(New ThreadStart(AddressOf Me.method_1))
'        Me.thread_0.IsBackground = True
'        Me.thread_0.Name = "Animator thread"
'        Me.thread_0.Start()
'    End Sub

'    Private Sub method_1()
'Label_0000:
'        Thread.Sleep(Me.Interval)
'        Try
'            Dim count As Integer = 0
'            Dim list As New List(Of QueueItem)
'            Dim list2 As New List(Of QueueItem)
'            Dim queue As List(Of QueueItem) = Me.queue
'            SyncLock queue
'                count = Me.queue.Count
'                Dim flag2 As Boolean = False
'                Dim item As QueueItem
'                For Each item In Me.queue
'                    If item.IsActive Then
'                        flag2 = True
'                    End If
'                    If ((Not item.controller Is Nothing) AndAlso item.controller.IsCompleted) Then
'                        list.Add(item)
'                    ElseIf item.IsActive Then
'                        Dim span As TimeSpan = DirectCast((DateTime.Now - item.ActivateTime), TimeSpan)
'                        If (span.TotalMilliseconds > Me.MaxAnimationTime) Then
'                            list.Add(item)
'                        Else
'                            list2.Add(item)
'                        End If
'                    End If
'                Next
'                If Not flag2 Then
'                    Using enumerator As Enumerator(Of QueueItem) = Me.queue.GetEnumerator
'                        Dim current As QueueItem
'                        Do While enumerator.MoveNext
'                            current = enumerator.Current
'                            If Not current.IsActive Then
'                                GoTo Label_0108
'                            End If
'                        Loop
'                        GoTo Label_0135
'Label_0108:
'                        list2.Add(current)
'                        current.IsActive = True
'                    End Using
'                End If
'            End SyncLock
'Label_0135:
'            Dim item3 As QueueItem
'            For Each item3 In list
'                Me.method_7(item3)
'            Next
'            Dim item4 As QueueItem
'            For Each item4 In list2
'                Try
'                        Dim class2 As New Class3 With { _
'                            .<>4__this = Me, _
'                            .item2 = item4 _
'                        }
'                        Me.control_0.BeginInvoke(New MethodInvoker(AddressOf class2.<Work>b__0))
'                Catch obj1 As Object
'                    Me.method_7(item4)
'                End Try
'            Next
'            If (count = 0) Then
'                If (list.Count > 0) Then
'                    Me.OnAllAnimationsCompleted()
'                End If
'                Me.method_2()
'            End If
'            GoTo Label_0000
'        Catch obj2 As Object
'            GoTo Label_0000
'        End Try
'    End Sub

'    Private Sub method_10()
'    End Sub

'    Private Sub method_2()
'        Dim num As Integer
'        Dim num2 As Integer
'        Dim list As New List(Of QueueItem)
'        Dim list2 As List(Of QueueItem) = Me.list_0
'        SyncLock list2
'            Dim dictionary As New Dictionary(Of Control, QueueItem)
'            Dim item As QueueItem
'            For Each item In Me.list_0
'                If (Not item.control Is Nothing) Then
'                    If dictionary.ContainsKey(item.control) Then
'                        list.Add(dictionary.Item(item.control))
'                    End If
'                    dictionary.Item(item.control) = item
'                Else
'                    list.Add(item)
'                End If
'            Next
'            Dim item2 As QueueItem
'            For Each item2 In dictionary.Values
'                If ((Not item2.control Is Nothing) AndAlso Not Me.method_3(item2.control, item2.mode)) Then
'                    If (Not Me.control_0 Is Nothing) Then
'                        Me.method_4(item2.control, item2.mode)
'                    End If
'                Else
'                    list.Add(item2)
'                End If
'            Next
'            Dim item3 As QueueItem
'            For Each item3 In list
'                Me.list_0.Remove(item3)
'            Next
'            GoTo Label_015E
'        End SyncLock
'Label_014F:
'        num2 = 1
'        Dim num3 As Integer = num
'        num = 1
'        If (1 > num3) Then
'            Return
'        End If
'Label_015E:
'        If (num = num2) Then
'            GoTo Label_014F
'        End If
'    End Sub

'    Private Function method_3(ByVal control_1 As Control, ByVal animateMode_0 As AnimateMode) As Boolean
'        If (animateMode_0 = AnimateMode.Show) Then
'            Return control_1.Visible
'        End If
'        If (animateMode_0 = AnimateMode.Hide) Then
'            Return Not control_1.Visible
'        End If
'        Return True
'    End Function

'    Private Sub method_4(ByVal control_1 As Control, ByVal animateMode_0 As AnimateMode)
'        Dim class2 As New Class4 With { _
'            .mode = animateMode_0, _
'            .control = control_1 _
'        }
'            Me.control_0.Invoke(New MethodInvoker(AddressOf class2.<RepairState>b__0))
'    End Sub

'    Private Sub method_5(ByVal queueItem_0 As QueueItem)
'        Dim num As Integer
'        Dim num2 As Integer
'        Dim item As QueueItem = queueItem_0
'        SyncLock item
'            Try
'                If (queueItem_0.controller Is Nothing) Then
'                    queueItem_0.controller = Me.method_8(queueItem_0.control, queueItem_0.mode, queueItem_0.animation, queueItem_0.clipRectangle)
'                End If
'                If Not queueItem_0.controller.IsCompleted Then
'                    queueItem_0.controller.BuildNextFrame()
'                End If
'                GoTo Label_008A
'            Catch obj1 As Object
'                If (Not queueItem_0.controller Is Nothing) Then
'                    queueItem_0.controller.Dispose()
'                End If
'                Me.method_7(queueItem_0)
'                GoTo Label_008A
'            End Try
'        End SyncLock
'Label_007B:
'        num2 = 1
'        Dim num3 As Integer = num
'        num = 1
'        If (1 > num3) Then
'            Return
'        End If
'Label_008A:
'        If (num = num2) Then
'            GoTo Label_007B
'        End If
'    End Sub

'    Private Sub method_6(ByVal animationType_1 As AnimationType)
'        Select Case animationType_1
'            Case AnimationType.Custom
'                Dim num As Integer
'                Dim num2 As Integer
'                Do While (num = num2)
'                    num2 = 1
'                    Dim num3 As Integer = num
'                    num = 1
'                    If (1 > num3) Then
'                        Exit Do
'                    End If
'                Loop
'                Exit Select
'            Case AnimationType.Rotate
'                Me.DefaultAnimation = Animation.Rotate
'                Return
'            Case AnimationType.HorizSlide
'                Me.DefaultAnimation = Animation.HorizSlide
'                Return
'            Case AnimationType.VertSlide
'                Me.DefaultAnimation = Animation.VertSlide
'                Return
'            Case AnimationType.Scale
'                Me.DefaultAnimation = Animation.Scale
'                Return
'            Case AnimationType.ScaleAndRotate
'                Me.DefaultAnimation = Animation.ScaleAndRotate
'                Return
'            Case AnimationType.HorizSlideAndRotate
'                Me.DefaultAnimation = Animation.HorizSlideAndRotate
'                Return
'            Case AnimationType.ScaleAndHorizSlide
'                Me.DefaultAnimation = Animation.ScaleAndHorizSlide
'                Return
'            Case AnimationType.Transparent
'                Me.DefaultAnimation = Animation.Transparent
'                Return
'            Case AnimationType.Leaf
'                Me.DefaultAnimation = Animation.Leaf
'                Return
'            Case AnimationType.Mosaic
'                Me.DefaultAnimation = Animation.Mosaic
'                Return
'            Case AnimationType.Particles
'                Me.DefaultAnimation = Animation.Particles
'                Return
'            Case AnimationType.VertBlind
'                Me.DefaultAnimation = Animation.VertBlind
'                Return
'            Case AnimationType.HorizBlind
'                Me.DefaultAnimation = Animation.HorizBlind
'                Exit Select
'            Case Else
'                Return
'        End Select
'    End Sub

'    Private Sub method_7(ByVal queueItem_0 As QueueItem)
'        If (Not queueItem_0.controller Is Nothing) Then
'            queueItem_0.controller.Dispose()
'        End If
'        Dim queue As List(Of QueueItem) = Me.queue
'        SyncLock queue
'            Me.queue.Remove(queueItem_0)
'        End SyncLock
'        Dim e As New AnimationCompletedEventArg With { _
'            .Animation = queueItem_0.animation, _
'            .Control = queueItem_0.control, _
'            .Mode = queueItem_0.mode _
'        }
'        Me.OnAnimationCompleted(e)
'    End Sub

'    Private Function method_8(ByVal control_1 As Control, ByVal animateMode_0 As AnimateMode, ByVal animation_1 As AnimationClass, ByVal rectangle_0 As Rectangle) As ControllerClass
'        Dim controller As New ControllerClass(control_1, animateMode_0, animation_1, Me.TimeStep, rectangle_0)
'        AddHandler controller.TransfromNeeded, New EventHandler(Of TransfromNeededEventArg)(AddressOf Me.OnTransformNeeded)
'        If (Not Me.eventHandler_3 Is Nothing) Then
'            AddHandler controller.NonLinearTransfromNeeded, New EventHandler(Of NonLinearTransfromNeededEventArg)(AddressOf Me.OnNonLinearTransfromNeeded)
'        End If
'        AddHandler controller.MouseDown, New EventHandler(Of MouseEventArgs)(AddressOf Me.OnMouseDown)
'        controller.DoubleBitmap.Cursor = Me.Cursor
'        AddHandler controller.FramePainted, New EventHandler(Of PaintEventArgs)(AddressOf Me.method_9)
'        Return controller
'    End Function

'    Private Sub method_9(ByVal sender As Object, ByVal e As PaintEventArgs)
'        If (Me.eventHandler_5 Is Nothing) Then
'            Dim num As Integer
'            Dim num2 As Integer
'            Do While (num = num2)
'                num2 = 1
'                Dim num3 As Integer = num
'                num = 1
'                If (1 > num3) Then
'                    Return
'                End If
'            Loop
'        Else
'            Me.eventHandler_5.Invoke(sender, e)
'        End If
'    End Sub

'    Protected Overridable Sub OnAllAnimationsCompleted()
'        If (Me.eventHandler_1 Is Nothing) Then
'            Dim num As Integer
'            Dim num2 As Integer
'            Do While (num = num2)
'                num2 = 1
'                Dim num3 As Integer = num
'                num = 1
'                If (1 > num3) Then
'                    Return
'                End If
'            Loop
'        Else
'            Me.eventHandler_1.Invoke(Me, EventArgs.Empty)
'        End If
'    End Sub

'    Protected Overridable Sub OnAnimationCompleted(ByVal e As AnimationCompletedEventArg)
'        If (Me.eventHandler_0 Is Nothing) Then
'            Dim num As Integer
'            Dim num2 As Integer
'            Do While (num = num2)
'                num2 = 1
'                Dim num3 As Integer = num
'                num = 1
'                If (1 > num3) Then
'                    Return
'                End If
'            Loop
'        Else
'            Me.eventHandler_0.Invoke(Me, e)
'        End If
'    End Sub

'    Protected Overridable Sub OnMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
'        Dim num As Integer
'        Dim num2 As Integer
'        Try
'            Dim controller As Controller = DirectCast(sender, Controller)
'            Dim location As Point = e.Location
'            location.Offset((controller.DoubleBitmap.Left - controller.AnimatedControl.Left), (controller.DoubleBitmap.Top - controller.AnimatedControl.Top))
'            If (Not Me.eventHandler_4 Is Nothing) Then
'                Me.eventHandler_4.Invoke(sender, New MouseEventArgs(e.Button, e.Clicks, location.X, location.Y, e.Delta))
'            End If
'        Catch obj1 As Object
'        End Try
'        Do While (num = num2)
'            num2 = 1
'            Dim num3 As Integer = num
'            num = 1
'            If (1 > num3) Then
'                Exit Do
'            End If
'        Loop
'    End Sub

'    Protected Overridable Sub OnNonLinearTransfromNeeded(ByVal sender As Object, ByVal e As NonLinearTransfromNeededEventArg)
'        If (Not Me.eventHandler_3 Is Nothing) Then
'            Me.eventHandler_3.Invoke(Me, e)
'        Else
'            e.UseDefaultTransform = True
'        End If
'    End Sub

'    Protected Overridable Sub OnTransformNeeded(ByVal sender As Object, ByVal e As TransfromNeededEventArg)
'        If (Not Me.eventHandler_2 Is Nothing) Then
'            Me.eventHandler_2.Invoke(Me, e)
'        Else
'            e.UseDefaultMatrix = True
'        End If
'    End Sub

'    Public Sub SetDecoration(ByVal control As Control, ByVal decoration As DecorationTypeEnum)
'        Dim control2 As DecorationControl = If(Me.dictionary_0.ContainsKey(control), Me.dictionary_0.Item(control), Nothing)
'        If (decoration = DecorationType.None) Then
'            If (Not control2 Is Nothing) Then
'                control2.Dispose()
'            End If
'            Me.dictionary_0.Remove(control)
'        Else
'            If (control2 Is Nothing) Then
'                control2 = New DecorationControl(decoration, control)
'            End If
'            control2.DecorationType = decoration
'            Me.dictionary_0.Item(control) = control2
'        End If
'    End Sub

'    Public Sub Show(ByVal control As Control, Optional ByVal parallel As Boolean = False, Optional ByVal animation As Animation = Nothing)
'        Me.AddToQueue(control, AnimateMode.Show, parallel, animation, New Rectangle)
'    End Sub

'    Public Sub ShowSync(ByVal control As Control, Optional ByVal parallel As Boolean = False, Optional ByVal animation As Animation = Nothing)
'        Me.Show(control, parallel, animation)
'        Me.WaitAnimation(control)
'    End Sub

'    Private Sub timer_0_Tick(ByVal sender As Object, ByVal e As EventArgs)
'        Me.timer_0.Stop()
'        Me.control_0 = New Control
'        Me.control_0.CreateControl()
'        Me.method_0()
'    End Sub

'    Public Sub WaitAllAnimations()
'        Do While Not Me.IsCompleted
'            Application.DoEvents()
'        Loop
'    End Sub

'    Public Sub WaitAnimation(ByVal animatedControl As Control)
'        Dim flag As Boolean
'Label_0000:
'        flag = False
'        Dim queue As List(Of QueueItem) = Me.queue
'        SyncLock queue
'            Using enumerator As Enumerator(Of QueueItem) = Me.queue.GetEnumerator
'                Do While enumerator.MoveNext
'                    If (enumerator.Current.control Is animatedControl) Then
'                        GoTo Label_003E
'                    End If
'                Loop
'                GoTo Label_0061
'Label_003E:
'                flag = True
'                GoTo Label_0061
'            End Using
'        End SyncLock
'Label_005A:
'        Application.DoEvents()
'        GoTo Label_0000
'Label_0061:
'        If flag Then
'            GoTo Label_005A
'        End If
'    End Sub


'    ' Properties
'    Public Property AnimationType As AnimationTypeEnum
'        Get
'            Return Me.animationType_0
'        End Get
'        Set(ByVal value As AnimationTypeEnum)
'            Me.animationType_0 = value
'            Me.method_6(Me.animationType_0)
'        End Set
'    End Property

'    <DefaultValue(GetType(Cursor), "Default")> _
'    Public Property Cursor As Cursor
'        <CompilerGenerated()> _
'        Get
'            Return Me.cursor_0
'        End Get
'        <CompilerGenerated()> _
'        Set(ByVal value As Cursor)
'            Me.cursor_0 = value
'        End Set
'    End Property

'    <TypeConverter(GetType(ExpandableObjectConverter))> _
'    Public Property DefaultAnimation As AnimationClass
'        <CompilerGenerated()> _
'        Get
'            Return Me.animation_0
'        End Get
'        <CompilerGenerated()> _
'        Set(ByVal value As AnimationClass)
'            Me.animation_0 = value
'        End Set
'    End Property

'    <DefaultValue(10)> _
'    Public Property Interval As Integer
'        <CompilerGenerated()> _
'        Get
'            Return Me.int_1
'        End Get
'        <CompilerGenerated()> _
'        Set(ByVal value As Integer)
'            Me.int_1 = value
'        End Set
'    End Property

'    Public ReadOnly Property IsCompleted As Boolean
'        Get
'            Dim queue As List(Of QueueItem) = Me.queue
'            SyncLock queue
'                Return (Me.queue.Count = 0)
'            End SyncLock
'        End Get
'    End Property

'    <DefaultValue(&H5DC)> _
'    Public Property MaxAnimationTime As Integer
'        <CompilerGenerated()> _
'        Get
'            Return Me.int_0
'        End Get
'        <CompilerGenerated()> _
'        Set(ByVal value As Integer)
'            Me.int_0 = value
'        End Set
'    End Property

'    <DefaultValue(CSng(0.02!))> _
'    Public Property TimeStep As Single
'        <CompilerGenerated()> _
'        Get
'            Return Me.float_0
'        End Get
'        <CompilerGenerated()> _
'        Set(ByVal value As Single)
'            Me.float_0 = value
'        End Set
'    End Property

'    ' Nested Types
'    <CompilerGenerated()> _
'    Private NotInheritable Class Class3
'        ' Methods
'        Friend Sub Workb__0()
'            Me.__this.method_5(Me.item2)
'        End Sub


'        ' Fields
'        Public __this As Form_Transition
'        Public item2 As QueueItem
'    End Class

'    <CompilerGenerated()> _
'    Private NotInheritable Class Class4
'        ' Methods
'            Friend Sub <RepairState>b__0()
'            Dim num As Integer
'            Dim num2 As Integer
'            Try
'                Select Case Me.mode
'                    Case AnimateMode.Show
'                        Me.control.Visible = True
'                        Exit Select
'                    Case AnimateMode.Hide
'                        Me.control.Visible = False
'                        Exit Select
'                End Select
'            Catch obj1 As Object
'            End Try
'            Do While (num = num2)
'                num2 = 1
'                Dim num3 As Integer = num
'                num = 1
'                If (1 > num3) Then
'                    Exit Do
'                End If
'            Loop
'        End Sub


'        ' Fields
'        Public control As Control
'        Public mode As AnimateMode
'    End Class

'    <CompilerGenerated()> _
'    Private NotInheritable Class Class5
'        ' Methods
'        Friend Sub ClearQueueb__0()
'            Dim mode As AnimateMode = Me.item.mode
'            If (mode = AnimateMode.Show) Then
'                Me.item.control.Visible = True
'            ElseIf (mode <> AnimateMode.Hide) Then
'                Dim num As Integer
'                Dim num2 As Integer
'                Do While (num = num2)
'                    num2 = 1
'                    Dim num3 As Integer = num
'                    num = 1
'                    If (1 > num3) Then
'                        Return
'                    End If
'                Loop
'            Else
'                Me.item.control.Visible = False
'            End If
'        End Sub


'        ' Fields
'        Public item As QueueItem
'    End Class

'    Public Class QueueItem
'        ' Properties
'        Property ActivateTime As DateTime
'            <CompilerGenerated()> _
'            Get
'                Return Me.dateTime_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As DateTime)
'                Me.dateTime_0 = value
'            End Set
'        End Property

'        Public Property IsActive As Boolean
'            Get
'                Return Me.IsActive
'            End Get
'            Set(ByVal value As Boolean)
'                If (Me.IsActive <> value) Then
'                    Me.IsActive = value
'                    If Not value Then
'                        Dim num As Integer
'                        Dim num2 As Integer
'                        Do While (num = num2)
'                            num2 = 1
'                            Dim num3 As Integer = num
'                            num = 1
'                            If (1 > num3) Then
'                                Return
'                            End If
'                        Loop
'                    Else
'                        Me.ActivateTime = DateTime.Now
'                    End If
'                End If
'            End Set
'        End Property

'    End Class




'    Public Class AnimationCompletedEventArg
'        Inherits EventArgs
'        ' Properties
'        Public Property Animation As Animation
'            <CompilerGenerated()> _
'            Get
'                Return Me.animation_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Animation)
'                Me.animation_0 = value
'            End Set
'        End Property

'        Property Control As Control
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.control_0
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As Control)
'                Me.control_0 = value
'            End Set
'        End Property

'        Property Mode As AnimateMode
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.animateMode_0
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As AnimateMode)
'                Me.animateMode_0 = value
'            End Set
'        End Property


'        ' Fields
'        <CompilerGenerated()> _
'        Private animateMode_0 As AnimateMode
'        <CompilerGenerated()> _
'        Private animation_0 As Animation
'        <CompilerGenerated()> _
'        Private control_0 As Control
'    End Class



'    Public Class NonLinearTransfromNeededEventArg
'        Inherits EventArgs
'        ' Properties
'        Public Property Animation As Animation
'            <CompilerGenerated()> _
'            Get
'                Return Me.animation_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Animation)
'                Me.animation_0 = value
'            End Set
'        End Property

'        Property ClientRectangle As Rectangle
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.rectangle_0
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As Rectangle)
'                Me.rectangle_0 = value
'            End Set
'        End Property

'        Property Control As Control
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.control_0
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As Control)
'                Me.control_0 = value
'            End Set
'        End Property

'        Property CurrentTime As Single
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.float_0
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As Single)
'                Me.float_0 = value
'            End Set
'        End Property

'        Property Mode As AnimateMode
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.animateMode_0
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As AnimateMode)
'                Me.animateMode_0 = value
'            End Set
'        End Property

'        Property Pixels As Byte()
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.byte_0
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As Byte())
'                Me.byte_0 = value
'            End Set
'        End Property

'        Property SourceClientRectangle As Rectangle
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.rectangle_1
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As Rectangle)
'                Me.rectangle_1 = value
'            End Set
'        End Property

'        Property SourcePixels As Byte()
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.byte_1
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As Byte())
'                Me.byte_1 = value
'            End Set
'        End Property

'        Public Property SourceStride As Integer
'            <CompilerGenerated()> _
'            Get
'                Return Me.int_1
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Integer)
'                Me.int_1 = value
'            End Set
'        End Property

'        Property Stride As Integer
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.int_0
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As Integer)
'                Me.int_0 = value
'            End Set
'        End Property

'        Public Property UseDefaultTransform As Boolean
'            <CompilerGenerated()> _
'            Get
'                Return Me.bool_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Boolean)
'                Me.bool_0 = value
'            End Set
'        End Property


'        ' Fields
'        <CompilerGenerated()> _
'        Private animateMode_0 As AnimateMode
'        <CompilerGenerated()> _
'        Private animation_0 As Animation
'        <CompilerGenerated()> _
'        Private bool_0 As Boolean
'        <CompilerGenerated()> _
'        Private byte_0 As Byte()
'        <CompilerGenerated()> _
'        Private byte_1 As Byte()
'        <CompilerGenerated()> _
'        Private control_0 As Control
'        <CompilerGenerated()> _
'        Private float_0 As Single
'        <CompilerGenerated()> _
'        Private int_0 As Integer
'        <CompilerGenerated()> _
'        Private int_1 As Integer
'        <CompilerGenerated()> _
'        Private rectangle_0 As Rectangle
'        <CompilerGenerated()> _
'        Private rectangle_1 As Rectangle
'    End Class




'    Public Class TransfromNeededEventArg
'        Inherits EventArgs
'        ' Methods
'        Public Sub New()
'            Me.Matrix = New Matrix(1.0!, 0.0!, 0.0!, 1.0!, 0.0!, 0.0!)
'        End Sub


'        ' Properties
'        Public Property Animation As Animation
'            <CompilerGenerated()> _
'            Get
'                Return Me.animation_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Animation)
'                Me.animation_0 = value
'            End Set
'        End Property

'        Property ClientRectangle As Rectangle
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.rectangle_0
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As Rectangle)
'                Me.rectangle_0 = value
'            End Set
'        End Property

'        Property ClipRectangle As Rectangle
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.rectangle_1
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As Rectangle)
'                Me.rectangle_1 = value
'            End Set
'        End Property

'        Property Control As Control
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.control_0
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As Control)
'                Me.control_0 = value
'            End Set
'        End Property

'        Property CurrentTime As Single
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.float_0
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As Single)
'                Me.float_0 = value
'            End Set
'        End Property

'        Public Property Matrix As Matrix
'            <CompilerGenerated()> _
'            Get
'                Return Me.matrix_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Matrix)
'                Me.matrix_0 = value
'            End Set
'        End Property

'        Property Mode As AnimateMode
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.animateMode_0
'            End Get
'            <CompilerGenerated()> _
'            Friend Set(ByVal value As AnimateMode)
'                Me.animateMode_0 = value
'            End Set
'        End Property

'        Public Property UseDefaultMatrix As Boolean
'            <CompilerGenerated()> _
'            Get
'                Return Me.bool_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Boolean)
'                Me.bool_0 = value
'            End Set
'        End Property


'        ' Fields
'        <CompilerGenerated()> _
'        Private animateMode_0 As AnimateMode
'        <CompilerGenerated()> _
'        Private animation_0 As Animation
'        <CompilerGenerated()> _
'        Private bool_0 As Boolean
'        <CompilerGenerated()> _
'        Private control_0 As Control
'        <CompilerGenerated()> _
'        Private float_0 As Single
'        <CompilerGenerated()> _
'        Private matrix_0 As Matrix
'        <CompilerGenerated()> _
'        Private rectangle_0 As Rectangle
'        <CompilerGenerated()> _
'        Private rectangle_1 As Rectangle
'    End Class


'    <DebuggerStepThrough()> _
'    Class AnimationClass
'        ' Methods
'        Public Sub New()
'            Me.MinTime = 0.0!
'            Me.MaxTime = 1.0!
'            Me.AnimateOnlyDifferences = True
'        End Sub

'        Public Sub Add(ByVal a As AnimationClass)
'            Me.SlideCoeff = New PointF((Me.SlideCoeff.X + a.SlideCoeff.X), (Me.SlideCoeff.Y + a.SlideCoeff.Y))
'            Me.RotateCoeff = (Me.RotateCoeff + a.RotateCoeff)
'            Me.RotateLimit = (Me.RotateLimit + a.RotateLimit)
'            Me.ScaleCoeff = New PointF((Me.ScaleCoeff.X + a.ScaleCoeff.X), (Me.ScaleCoeff.Y + a.ScaleCoeff.Y))
'            Me.TransparencyCoeff = (Me.TransparencyCoeff + a.TransparencyCoeff)
'            Me.LeafCoeff = (Me.LeafCoeff + a.LeafCoeff)
'            Me.MosaicShift = New PointF((Me.MosaicShift.X + a.MosaicShift.X), (Me.MosaicShift.Y + a.MosaicShift.Y))
'            Me.MosaicCoeff = New PointF((Me.MosaicCoeff.X + a.MosaicCoeff.X), (Me.MosaicCoeff.Y + a.MosaicCoeff.Y))
'            Me.MosaicSize = (Me.MosaicSize + a.MosaicSize)
'            Me.BlindCoeff = New PointF((Me.BlindCoeff.X + a.BlindCoeff.X), (Me.BlindCoeff.Y + a.BlindCoeff.Y))
'            Me.TimeCoeff = (Me.TimeCoeff + a.TimeCoeff)
'            Me.Padding = (Me.Padding + a.Padding)
'        End Sub

'        Public Function Clone() As AnimationClass
'            Return DirectCast(MyBase.MemberwiseClone, Animation)
'        End Function


'        ' Properties
'        Public Property AnimateOnlyDifferences As Boolean
'            <CompilerGenerated()> _
'            Get
'                Return Me.bool_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Boolean)
'                Me.bool_0 = value
'            End Set
'        End Property

'        <TypeConverter(GetType(PointFConverter)), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
'        Public Property BlindCoeff As PointF
'            <CompilerGenerated()> _
'            Get
'                Return Me.pointF_4
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As PointF)
'                Me.pointF_4 = value
'            End Set
'        End Property

'        Public Shared ReadOnly Property HorizBlind As AnimationClass
'            Get
'                Return New Animation With { _
'                    .BlindCoeff = New PointF(1.0!, 0.0!) _
'                }
'            End Get
'        End Property

'        Public Shared ReadOnly Property HorizSlide As AnimationClass
'            Get
'                Return New Animation With { _
'                    .SlideCoeff = New PointF(1.0!, 0.0!) _
'                }
'            End Get
'        End Property

'        Public Shared ReadOnly Property HorizSlideAndRotate As AnimationClass
'            Get
'                Return New Animation With { _
'                    .SlideCoeff = New PointF(1.0!, 0.0!), _
'                    .RotateCoeff = 0.3!, _
'                    .RotateLimit = 0.2!, _
'                    .Padding = New Padding(50, 50, 50, 50) _
'                }
'            End Get
'        End Property

'        Public ReadOnly Property IsNonLinearTransformNeeded As Boolean
'            Get
'                If (((Me.BlindCoeff = PointF.Empty) AndAlso ((Me.MosaicCoeff = PointF.Empty) OrElse (Me.MosaicSize = 0))) AndAlso ((Me.TransparencyCoeff = 0.0!) AndAlso (Me.LeafCoeff = 0.0!))) Then
'                    Return False
'                End If
'                Return True
'            End Get
'        End Property

'        Public Shared ReadOnly Property Leaf As AnimationClass
'            Get
'                Return New Animation With { _
'                    .LeafCoeff = 1.0! _
'                }
'            End Get
'        End Property

'        Public Property LeafCoeff As Single
'            <CompilerGenerated()> _
'            Get
'                Return Me.float_3
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Single)
'                Me.float_3 = value
'            End Set
'        End Property

'        Public Property MaxTime As Single
'            <CompilerGenerated()> _
'            Get
'                Return Me.float_6
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Single)
'                Me.float_6 = value
'            End Set
'        End Property

'        Public Property MinTime As Single
'            <CompilerGenerated()> _
'            Get
'                Return Me.float_5
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Single)
'                Me.float_5 = value
'            End Set
'        End Property

'        Public Shared ReadOnly Property Mosaic As AnimationClass
'            Get
'                Return New Animation With { _
'                    .MosaicCoeff = New PointF(100.0!, 100.0!), _
'                    .MosaicSize = 20, _
'                    .Padding = New Padding(30, 30, 30, 30) _
'                }
'            End Get
'        End Property

'        <TypeConverter(GetType(PointFConverter)), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
'        Public Property MosaicCoeff As PointF
'            <CompilerGenerated()> _
'            Get
'                Return Me.pointF_3
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As PointF)
'                Me.pointF_3 = value
'            End Set
'        End Property

'        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Advanced), TypeConverter(GetType(PointFConverter))> _
'        Public Property MosaicShift As PointF
'            <CompilerGenerated()> _
'            Get
'                Return Me.pointF_2
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As PointF)
'                Me.pointF_2 = value
'            End Set
'        End Property

'        Public Property MosaicSize As Integer
'            <CompilerGenerated()> _
'            Get
'                Return Me.int_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Integer)
'                Me.int_0 = value
'            End Set
'        End Property

'        Public Property Padding As Padding
'            <CompilerGenerated()> _
'            Get
'                Return Me.padding_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Padding)
'                Me.padding_0 = value
'            End Set
'        End Property

'        Public Shared ReadOnly Property Particles As AnimationClass
'            Get
'                Return New Animation With { _
'                    .MosaicCoeff = New PointF(200.0!, 200.0!), _
'                    .MosaicSize = 1, _
'                    .MosaicShift = New PointF(0.0!, 0.5!), _
'                    .Padding = New Padding(100, 50, 100, 150), _
'                    .TimeCoeff = 2.0! _
'                }
'            End Get
'        End Property

'        Public Shared ReadOnly Property Rotate As AnimationClass
'            Get
'                Return New Animation With { _
'                    .RotateCoeff = 1.0!, _
'                    .TransparencyCoeff = 1.0!, _
'                    .Padding = New Padding(50, 50, 50, 50) _
'                }
'            End Get
'        End Property

'        Public Property RotateCoeff As Single
'            <CompilerGenerated()> _
'            Get
'                Return Me.float_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Single)
'                Me.float_0 = value
'            End Set
'        End Property

'        Public Property RotateLimit As Single
'            <CompilerGenerated()> _
'            Get
'                Return Me.float_1
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Single)
'                Me.float_1 = value
'            End Set
'        End Property

'        Public Shared ReadOnly Property Scale As AnimationClass
'            Get
'                Return New Animation With { _
'                    .ScaleCoeff = New PointF(1.0!, 1.0!) _
'                }
'            End Get
'        End Property

'        Public Shared ReadOnly Property ScaleAndHorizSlide As AnimationClass
'            Get
'                Return New Animation With { _
'                    .ScaleCoeff = New PointF(1.0!, 1.0!), _
'                    .SlideCoeff = New PointF(1.0!, 0.0!), _
'                    .Padding = New Padding(30, 0, 0, 0) _
'                }
'            End Get
'        End Property

'        Public Shared ReadOnly Property ScaleAndRotate As AnimationClass
'            Get
'                Return New Animation With { _
'                    .ScaleCoeff = New PointF(1.0!, 1.0!), _
'                    .RotateCoeff = 0.5!, _
'                    .RotateLimit = 0.2!, _
'                    .Padding = New Padding(30, 30, 30, 30) _
'                }
'            End Get
'        End Property

'        <TypeConverter(GetType(PointFConverter)), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Advanced)> _
'        Public Property ScaleCoeff As PointF
'            <CompilerGenerated()> _
'            Get
'                Return Me.pointF_1
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As PointF)
'                Me.pointF_1 = value
'            End Set
'        End Property

'        <EditorBrowsable(EditorBrowsableState.Advanced), TypeConverter(GetType(PointFConverter)), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
'        Public Property SlideCoeff As PointF
'            <CompilerGenerated()> _
'            Get
'                Return Me.pointF_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As PointF)
'                Me.pointF_0 = value
'            End Set
'        End Property

'        Public Property TimeCoeff As Single
'            <CompilerGenerated()> _
'            Get
'                Return Me.float_4
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Single)
'                Me.float_4 = value
'            End Set
'        End Property

'        Public Property TransparencyCoeff As Single
'            <CompilerGenerated()> _
'            Get
'                Return Me.float_2
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Single)
'                Me.float_2 = value
'            End Set
'        End Property

'        Public Shared ReadOnly Property Transparent As AnimationClass
'            Get
'                Return New Animation With { _
'                    .TransparencyCoeff = 1.0! _
'                }
'            End Get
'        End Property

'        Public Shared ReadOnly Property VertBlind As AnimationClass
'            Get
'                Return New Animation With { _
'                    .BlindCoeff = New PointF(0.0!, 1.0!) _
'                }
'            End Get
'        End Property

'        Public Shared ReadOnly Property VertSlide As AnimationClass
'            Get
'                Return New Animation With { _
'                    .SlideCoeff = New PointF(0.0!, 1.0!) _
'                }
'            End Get
'        End Property


'        ' Fields
'        <CompilerGenerated()> _
'        Private bool_0 As Boolean
'        <CompilerGenerated()> _
'        Private float_0 As Single
'        <CompilerGenerated()> _
'        Private float_1 As Single
'        <CompilerGenerated()> _
'        Private float_2 As Single
'        <CompilerGenerated()> _
'        Private float_3 As Single
'        <CompilerGenerated()> _
'        Private float_4 As Single
'        <CompilerGenerated()> _
'        Private float_5 As Single
'        <CompilerGenerated()> _
'        Private float_6 As Single
'        <CompilerGenerated()> _
'        Private int_0 As Integer
'        <CompilerGenerated()> _
'        Private padding_0 As Padding
'        <CompilerGenerated()> _
'        Private pointF_0 As PointF
'        <CompilerGenerated()> _
'        Private pointF_1 As PointF
'        <CompilerGenerated()> _
'        Private pointF_2 As PointF
'        <CompilerGenerated()> _
'        Private pointF_3 As PointF
'        <CompilerGenerated()> _
'        Private pointF_4 As PointF
'    End Class


'    <DebuggerStepThrough()> _
'    Public Class ControllerClass
'        ' Events

'        Public Event FramePainted As EventHandler(Of PaintEventArgs)

'        Public Event FramePainting As EventHandler(Of PaintEventArgs)

'        Public Event MouseDown As EventHandler(Of MouseEventArgs)

'        Public Event NonLinearTransfromNeeded As EventHandler(Of NonLinearTransfromNeededEventArg)

'        Public Event TransfromNeeded As EventHandler(Of TransfromNeededEventArg)

'        ' Methods
'        Public Sub New(ByVal control As Control, ByVal mode As AnimateMode, ByVal animation As AnimationClass, ByVal timeStep As Single, ByVal controlClipRect As Rectangle)
'            If TypeOf control Is Form Then
'                Me.DoubleBitmap = New DoubleBitmapForm
'            Else
'                Me.DoubleBitmap = New DoubleBitmapControl
'            End If
'            AddHandler TryCast(Me.DoubleBitmap, IFakeControl).FramePainting, New EventHandler(Of PaintEventArgs)(AddressOf Me.OnFramePainting)
'            AddHandler TryCast(Me.DoubleBitmap, IFakeControl).FramePainted, New EventHandler(Of PaintEventArgs)(AddressOf Me.OnFramePainting)
'            AddHandler TryCast(Me.DoubleBitmap, IFakeControl).TransfromNeeded, New EventHandler(Of TransfromNeededEventArg)(AddressOf Me.OnTransfromNeeded)
'            AddHandler Me.DoubleBitmap.MouseDown, New MouseEventHandler(AddressOf Me.OnMouseDown)
'            Me.animation_0 = animation
'            Me.AnimatedControl = control
'            Me.animateMode_0 = mode
'            Me.CustomClipRect = controlClipRect
'            If ((mode = AnimateMode.Show) OrElse (mode = AnimateMode.BeginUpdate)) Then
'                timeStep = -timeStep
'            End If
'            Me.TimeStep = (timeStep * If((animation.TimeCoeff = 0.0!), 1.0!, animation.TimeCoeff))
'            If (Me.TimeStep = 0.0!) Then
'                timeStep = 0.01!
'            End If
'            Try
'                Select Case mode
'                    Case AnimateMode.Show
'                        Me.BgBmp = Me.GetBackground(control, False, False)
'                        TryCast(Me.DoubleBitmap, IFakeControl).InitParent(control, animation.Padding)
'                        Me.DoubleBitmap.Visible = True
'                        Me.DoubleBitmap.Refresh()
'                        control.Visible = True
'                        Me.ctrlBmp = Me.GetForeground(control)
'                        GoTo Label_01E9
'                    Case AnimateMode.Hide
'                        Me.BgBmp = Me.GetBackground(control, False, False)
'                        TryCast(Me.DoubleBitmap, IFakeControl).InitParent(control, animation.Padding)
'                        Me.ctrlBmp = Me.GetForeground(control)
'                        Me.DoubleBitmap.Visible = True
'                        control.Visible = False
'                        GoTo Label_01E9
'                    Case AnimateMode.Update, AnimateMode.BeginUpdate
'                        TryCast(Me.DoubleBitmap, IFakeControl).InitParent(control, animation.Padding)
'                        Me.BgBmp = Me.GetBackground(control, True, False)
'                        Me.DoubleBitmap.Visible = True
'                        GoTo Label_01E9
'                End Select
'            Catch obj1 As Object
'                Me.Dispose()
'            End Try
'Label_01E9:
'            Me.CurrentTime = If((timeStep > 0.0!), animation.MinTime, animation.MaxTime)
'        End Sub

'        Friend Sub BuildNextFrame()
'            If (Me.animateMode_0 <> AnimateMode.BeginUpdate) Then
'                Me.DoubleBitmap.Invalidate()
'            End If
'        End Sub

'        Protected Overridable Function ControlRectToMyRect(ByVal rect As Rectangle) As Rectangle
'            Return New Rectangle((Me.animation_0.Padding.Left + rect.Left), (Me.animation_0.Padding.Top + rect.Top), ((rect.Width + Me.animation_0.Padding.Left) + Me.animation_0.Padding.Right), ((rect.Height + Me.animation_0.Padding.Top) + Me.animation_0.Padding.Bottom))
'        End Function

'        Public Sub Dispose()
'            If (Not Me.ctrlBmp Is Nothing) Then
'                Me.BgBmp.Dispose()
'            End If
'            If (Not Me.ctrlBmp Is Nothing) Then
'                Me.ctrlBmp.Dispose()
'            End If
'            If (Not Me.Frame Is Nothing) Then
'                Me.Frame.Dispose()
'            End If
'            Me.AnimatedControl = Nothing
'            Me.Hide()
'        End Sub

'        Public Sub EndUpdate()
'            Dim bitmap As Bitmap = Me.GetBackground(Me.AnimatedControl, True, True)
'            If Me.animation_0.AnimateOnlyDifferences Then
'                TransfromHelper.CalcDifference(bitmap, Me.BgBmp)
'            End If
'            Me.ctrlBmp = bitmap
'            Me.animateMode_0 = AnimateMode.Update
'        End Sub

'        Protected Overridable Function GetBackground(ByVal ctrl As Control, Optional ByVal includeForeground As Boolean = False, Optional ByVal clip As Boolean = False) As Bitmap
'            If TypeOf ctrl Is Form Then
'                Return Me.method_0(ctrl, includeForeground, clip)
'            End If
'            Dim bounds As Rectangle = Me.GetBounds
'            Dim width As Integer = bounds.Width
'            Dim height As Integer = bounds.Height
'            If (width = 0) Then
'                width = 1
'            End If
'            If (height = 1) Then
'                height = 1
'            End If
'            Dim image As New Bitmap(width, height)
'            Dim clipRect As New Rectangle(0, 0, image.Width, image.Height)
'            Dim args As New PaintEventArgs(Graphics.FromImage(image), clipRect)
'            If clip Then
'                If (Me.CustomClipRect = New Rectangle) Then
'                    args.Graphics.SetClip(New Rectangle(0, 0, width, height))
'                Else
'                    args.Graphics.SetClip(Me.CustomClipRect)
'                End If
'            End If
'            Dim i As Integer = (ctrl.Parent.Controls.Count - 1)
'            Do While (i >= 0)
'                Dim control As Control = ctrl.Parent.Controls.Item(i)
'                If ((control Is ctrl) AndAlso Not includeForeground) Then
'                    Exit Do
'                End If
'                If ((control.Visible AndAlso Not control.IsDisposed) AndAlso control.Bounds.IntersectsWith(bounds)) Then
'                    Using bitmap2 As Bitmap = New Bitmap(control.Width, control.Height)
'                        control.DrawToBitmap(bitmap2, New Rectangle(0, 0, control.Width, control.Height))
'                        args.Graphics.DrawImage(bitmap2, (control.Left - bounds.Left), (control.Top - bounds.Top), control.Width, control.Height)
'                    End Using
'                End If
'                If (control Is ctrl) Then
'                    Exit Do
'                End If
'                i -= 1
'            Loop
'            args.Graphics.Dispose()
'            Return image
'        End Function

'        Protected Overridable Function GetBounds() As Rectangle
'            Return New Rectangle((Me.AnimatedControl.Left - Me.animation_0.Padding.Left), (Me.AnimatedControl.Top - Me.animation_0.Padding.Top), ((Me.AnimatedControl.Size.Width + Me.animation_0.Padding.Left) + Me.animation_0.Padding.Right), ((Me.AnimatedControl.Size.Height + Me.animation_0.Padding.Top) + Me.animation_0.Padding.Bottom))
'        End Function

'        Protected Overridable Function GetForeground(ByVal ctrl As Control) As Bitmap
'            Dim bitmap As Bitmap = Nothing
'            If Not ctrl.IsDisposed Then
'                If (ctrl.Parent Is Nothing) Then
'                    bitmap = New Bitmap((ctrl.Width + Me.animation_0.Padding.Horizontal), (ctrl.Height + Me.animation_0.Padding.Vertical))
'                    ctrl.DrawToBitmap(bitmap, New Rectangle(Me.animation_0.Padding.Left, Me.animation_0.Padding.Top, ctrl.Width, ctrl.Height))
'                    Return bitmap
'                End If
'                bitmap = New Bitmap(Me.DoubleBitmap.Width, Me.DoubleBitmap.Height)
'                ctrl.DrawToBitmap(bitmap, New Rectangle((ctrl.Left - Me.DoubleBitmap.Left), (ctrl.Top - Me.DoubleBitmap.Top), ctrl.Width, ctrl.Height))
'            End If
'            Return bitmap
'        End Function

'        Public Sub Hide()
'            Dim num As Integer
'            Dim num2 As Integer
'            If (Not Me.DoubleBitmap Is Nothing) Then
'                Try
'                    Me.DoubleBitmap.BeginInvoke(Function 
'                    If Me.DoubleBitmap.Visible Then
'                        Me.DoubleBitmap.Hide()
'                    End If
'                    Me.DoubleBitmap.Parent = Nothing
'                    ' End Function
'                Catch obj1 As Object

'                End Try
'            End If
'            Do While (num = num2)
'                num2 = 1
'                Dim num3 As Integer = num
'                num = 1
'                If (1 > num3) Then
'                    Exit Do
'                End If
'            Loop
'        End Sub

'        Private Function method_0(ByVal control_2 As Control, ByVal bool_0 As Boolean, ByVal bool_1 As Boolean) As Bitmap
'            Dim blockRegionSize As Size = Screen.PrimaryScreen.Bounds.Size
'            Dim g As Graphics = Me.DoubleBitmap.CreateGraphics
'            Dim image As New Bitmap(blockRegionSize.Width, blockRegionSize.Height, g)
'            Graphics.FromImage(image).CopyFromScreen(0, 0, 0, 0, blockRegionSize)
'            Return image
'        End Function

'        Protected Overridable Sub OnFramePainted(ByVal sender As Object, ByVal e As PaintEventArgs)
'            If (Me.eventHandler_3 Is Nothing) Then
'                Dim num As Integer
'                Dim num2 As Integer
'                Do While (num = num2)
'                    num2 = 1
'                    Dim num3 As Integer = num
'                    num = 1
'                    If (1 > num3) Then
'                        Return
'                    End If
'                Loop
'            Else
'                Me.eventHandler_3.Invoke(Me, e)
'            End If
'        End Sub

'        Protected Overridable Sub OnFramePainting(ByVal sender As Object, ByVal e As PaintEventArgs)
'            Dim frame As Bitmap = Me.Frame
'            Me.Frame = Nothing
'            If (Me.animateMode_0 <> AnimateMode.BeginUpdate) Then
'                Me.Frame = Me.OnNonLinearTransfromNeeded
'                If ((Not frame Is Me.Frame) AndAlso (Not frame Is Nothing)) Then
'                    frame.Dispose()
'                End If
'                Dim maxTime As Single = (Me.CurrentTime + Me.TimeStep)
'                If (maxTime > Me.animation_0.MaxTime) Then
'                    maxTime = Me.animation_0.MaxTime
'                End If
'                If (maxTime < Me.animation_0.MinTime) Then
'                    maxTime = Me.animation_0.MinTime
'                End If
'                Me.CurrentTime = maxTime
'                If (Me.eventHandler_2 Is Nothing) Then
'                    Dim num2 As Integer
'                    Dim num3 As Integer
'                    Do While (num2 = num3)
'                        num3 = 1
'                        Dim num4 As Integer = num2
'                        num2 = 1
'                        If (1 > num4) Then
'                            Return
'                        End If
'                    Loop
'                Else
'                    Me.eventHandler_2.Invoke(Me, e)
'                End If
'            End If
'        End Sub

'        Protected Overridable Sub OnMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
'            If (Me.eventHandler_4 Is Nothing) Then
'                Dim num As Integer
'                Dim num2 As Integer
'                Do While (num = num2)
'                    num2 = 1
'                    Dim num3 As Integer = num
'                    num = 1
'                    If (1 > num3) Then
'                        Return
'                    End If
'                Loop
'            Else
'                Me.eventHandler_4.Invoke(Me, e)
'            End If
'        End Sub

'        Protected Overridable Function OnNonLinearTransfromNeeded() As Bitmap
'            Dim bitmap As Bitmap = Nothing
'            If (Me.ctrlBmp Is Nothing) Then
'                Return Nothing
'            End If
'            If ((Me.eventHandler_1 Is Nothing) AndAlso Not Me.animation_0.IsNonLinearTransformNeeded) Then
'                Return Me.ctrlBmp
'            End If
'            Try
'                bitmap = DirectCast(Me.ctrlBmp.Clone, Bitmap)
'                Dim rect As New Rectangle(0, 0, bitmap.Width, bitmap.Height)
'                Dim bitmapdata As BitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb)
'                Dim source As IntPtr = bitmapdata.Scan0
'                Dim length As Integer = ((bitmap.Width * bitmap.Height) * 4)
'                Dim destination As Byte() = New Byte(length - 1) {}
'                Marshal.Copy(source, destination, 0, length)
'                Dim e As New NonLinearTransfromNeededEventArg With { _
'                    .CurrentTime = Me.CurrentTime, _
'                    .ClientRectangle = Me.DoubleBitmap.ClientRectangle, _
'                    .Pixels = destination, _
'                    .Stride = bitmapdata.Stride _
'                }
'                If (Not Me.eventHandler_1 Is Nothing) Then
'                    Me.eventHandler_1.Invoke(Me, e)
'                Else
'                    e.UseDefaultTransform = True
'                End If
'                If e.UseDefaultTransform Then
'                    TransfromHelper.DoBlind(e, Me.animation_0)
'                    TransfromHelper.DoMosaic(e, Me.animation_0, Me.point_0, Me.byte_0)
'                    TransfromHelper.DoTransparent(e, Me.animation_0)
'                    TransfromHelper.DoLeaf(e, Me.animation_0)
'                End If
'                Marshal.Copy(destination, 0, source, length)
'                bitmap.UnlockBits(bitmapdata)
'            Catch obj1 As Object
'            End Try
'            Return bitmap
'        End Function

'        Protected Overridable Sub OnTransfromNeeded(ByVal sender As Object, ByVal e As TransfromNeededEventArg)
'            Dim num As Integer
'            Dim num2 As Integer
'            Try
'                If (Me.CustomClipRect <> New Rectangle) Then
'                    e.ClipRectangle = Me.ControlRectToMyRect(Me.CustomClipRect)
'                End If
'                e.CurrentTime = Me.CurrentTime
'                If (Not Me.eventHandler_0 Is Nothing) Then
'                    Me.eventHandler_0.Invoke(Me, e)
'                Else
'                    e.UseDefaultMatrix = True
'                End If
'                If e.UseDefaultMatrix Then
'                    TransfromHelper.DoScale(e, Me.animation_0)
'                    TransfromHelper.DoRotate(e, Me.animation_0)
'                    TransfromHelper.DoSlide(e, Me.animation_0)
'                End If
'            Catch obj1 As Object
'            End Try
'            Do While (num = num2)
'                num2 = 1
'                Dim num3 As Integer = num
'                num = 1
'                If (1 > num3) Then
'                    Exit Do
'                End If
'            Loop
'        End Sub


'        ' Properties
'        Public Property AnimatedControl As Control
'            <CompilerGenerated()> _
'            Get
'                Return Me.control_1
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Control)
'                Me.control_1 = value
'            End Set
'        End Property

'        Protected Property BgBmp As Bitmap
'            Get
'                Return TryCast(Me.DoubleBitmap, IFakeControl).BgBmp
'            End Get
'            Set(ByVal value As Bitmap)
'                TryCast(Me.DoubleBitmap, IFakeControl).BgBmp = value
'            End Set
'        End Property

'        Property CurrentTime As Single
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.float_0
'            End Get
'            <CompilerGenerated()> _
'            Private Set(ByVal value As Single)
'                Me.float_0 = value
'            End Set
'        End Property

'        Property DoubleBitmap As Control
'            <CompilerGenerated()> _
'            Public Get
'                Return Me.control_0
'            End Get
'            <CompilerGenerated()> _
'            Private Set(ByVal value As Control)
'                Me.control_0 = value
'            End Set
'        End Property

'        Public Property Frame As Bitmap
'            Get
'                Return TryCast(Me.DoubleBitmap, IFakeControl).Frame
'            End Get
'            Set(ByVal value As Bitmap)
'                TryCast(Me.DoubleBitmap, IFakeControl).Frame = value
'            End Set
'        End Property

'        Public ReadOnly Property IsCompleted As Boolean
'            Get
'                Return (((Me.TimeStep >= 0.0!) AndAlso (Me.CurrentTime >= Me.animation_0.MaxTime)) OrElse ((Me.TimeStep <= 0.0!) AndAlso (Me.CurrentTime <= Me.animation_0.MinTime)))
'            End Get
'        End Property

'        Property TimeStep As Single
'            <CompilerGenerated()> _
'            Protected Get
'                Return Me.float_1
'            End Get
'            <CompilerGenerated()> _
'            Private Set(ByVal value As Single)
'                Me.float_1 = value
'            End Set
'        End Property


'        ' Fields
'        Private animateMode_0 As AnimateMode
'        Private animation_0 As AnimationClass
'        Private byte_0 As Byte()
'        <CompilerGenerated()> _
'        Private control_0 As Control
'        <CompilerGenerated()> _
'        Private control_1 As Control
'        Protected ctrlBmp As Bitmap
'        Protected CustomClipRect As Rectangle
'        <CompilerGenerated()> _
'        Private float_0 As Single
'        <CompilerGenerated()> _
'        Private float_1 As Single
'        Private point_0 As Point()
'    End Class



'    Public Enum AnimateMode
'        ' Fields
'        BeginUpdate = 3
'        Hide = 1
'        Show = 0
'        Update = 2
'    End Enum


'    Public Enum AnimationTypeEnum
'        ' Fields
'        Custom = 0
'        HorizBlind = 13
'        HorizSlide = 2
'        HorizSlideAndRotate = 6
'        Leaf = 9
'        Mosaic = 10
'        Particles = 11
'        Rotate = 1
'        Scale = 4
'        ScaleAndHorizSlide = 7
'        ScaleAndRotate = 5
'        Transparent = 8
'        VertBlind = 12
'        VertSlide = 3
'    End Enum


'    Public Enum DecorationTypeEnum
'        ' Fields
'        BottomMirror = 1
'        Custom = 2
'        None = 0
'    End Enum


'    <DebuggerStepThrough()> _
'    Friend Class DecorationControl
'        Inherits UserControl
'        ' Events

'        Public Event NonLinearTransfromNeeded As EventHandler(Of NonLinearTransfromNeededEventArg)

'        ' Methods
'        Public Sub New(ByVal type As DecorationTypeEnum, ByVal decoratedControl As Control)
'            Me.DecorationType = type
'            Me.DecoratedControl = decoratedControl
'            AddHandler decoratedControl.VisibleChanged, New EventHandler(AddressOf Me.method_2)
'            AddHandler decoratedControl.ParentChanged, New EventHandler(AddressOf Me.method_2)
'            AddHandler decoratedControl.LocationChanged, New EventHandler(AddressOf Me.method_2)
'            AddHandler decoratedControl.Paint, New PaintEventHandler(AddressOf Me.method_1)
'            MyBase.SetStyle(ControlStyles.Selectable, False)
'            MyBase.SetStyle((ControlStyles.OptimizedDoubleBuffer Or (ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint)), True)
'            Me.method_0()
'            Me.timer_0 = New Timer
'            Me.timer_0.Interval = 100
'            AddHandler Me.timer_0.Tick, New EventHandler(AddressOf Me.timer_0_Tick)
'            Me.timer_0.Enabled = True
'        End Sub

'        Private Sub DecorationControl_Load(ByVal sender As Object, ByVal e As EventArgs)
'            If Not MyBase.DesignMode Then
'                Dim num As Integer
'                Dim num2 As Integer
'                Do While (num = num2)
'                    num2 = 1
'                    Dim num3 As Integer = num
'                    num = 1
'                    If (1 > num3) Then
'                        Return
'                    End If
'                Loop
'            Else
'                BunifuCustomControl.initializeComponent(Me)
'            End If
'        End Sub

'        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
'            Me.timer_0.Stop()
'            Me.timer_0.Dispose()
'            MyBase.Dispose(disposing)
'        End Sub

'        Protected Overridable Function GetForeground(ByVal ctrl As Control) As Bitmap
'            Dim bitmap As New Bitmap(MyBase.Width, MyBase.Height)
'            If Not ctrl.IsDisposed Then
'                Me.bool_0 = True
'                ctrl.DrawToBitmap(bitmap, New Rectangle(Me.Padding.Left, Me.Padding.Top, ctrl.Width, ctrl.Height))
'                Me.bool_0 = False
'            End If
'            Return bitmap
'        End Function

'        Private Sub method_0()
'            If (Me.DecorationType <> DecorationType.BottomMirror) Then
'                Dim num As Integer
'                Dim num2 As Integer
'                Do While (num = num2)
'                    num2 = 1
'                    Dim num3 As Integer = num
'                    num = 1
'                    If (1 > num3) Then
'                        Return
'                    End If
'                Loop
'            Else
'                Me.Padding = New Padding(0, 0, 0, 20)
'            End If
'        End Sub

'        Private Sub method_1(ByVal sender As Object, ByVal e As PaintEventArgs)
'            If Me.bool_0 Then
'                Dim num As Integer
'                Dim num2 As Integer
'                Do While (num = num2)
'                    num2 = 1
'                    Dim num3 As Integer = num
'                    num = 1
'                    If (1 > num3) Then
'                        Return
'                    End If
'                Loop
'            Else
'                MyBase.Invalidate()
'            End If
'        End Sub

'        Private Sub method_2(ByVal sender As Object, ByVal e As EventArgs)
'            Me.method_3()
'        End Sub

'        Private Sub method_3()
'            MyBase.Parent = Me.DecoratedControl.Parent
'            MyBase.Visible = Me.DecoratedControl.Visible
'            MyBase.Location = New Point((Me.DecoratedControl.Left - Me.Padding.Left), (Me.DecoratedControl.Top - Me.Padding.Top))
'            If (Not MyBase.Parent Is Nothing) Then
'                Dim childIndex As Integer = MyBase.Parent.Controls.GetChildIndex(Me.DecoratedControl)
'                MyBase.Parent.Controls.SetChildIndex(Me, (childIndex + 1))
'            End If
'            Dim size As New Size(((Me.DecoratedControl.Width + Me.Padding.Left) + Me.Padding.Right), ((Me.DecoratedControl.Height + Me.Padding.Top) + Me.Padding.Bottom))
'            If Not (size <> MyBase.Size) Then
'                Dim num2 As Integer
'                Dim num3 As Integer
'                Do While (num2 = num3)
'                    num3 = 1
'                    Dim num4 As Integer = num2
'                    num2 = 1
'                    If (1 > num4) Then
'                        Return
'                    End If
'                Loop
'            Else
'                MyBase.Size = size
'            End If
'        End Sub

'        Private Function method_4(ByVal bitmap_2 As Bitmap) As Byte()
'            Dim rect As New Rectangle(0, 0, bitmap_2.Width, bitmap_2.Height)
'            Dim bitmapdata As BitmapData = bitmap_2.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb)
'            Dim length As Integer = ((bitmap_2.Width * bitmap_2.Height) * 4)
'            Dim destination As Byte() = New Byte(length - 1) {}
'            Marshal.Copy(bitmapdata.Scan0, destination, 0, length)
'            bitmap_2.UnlockBits(bitmapdata)
'            Return destination
'        End Function

'        Private Sub method_5()
'            MyBase.SuspendLayout()
'            MyBase.Name = "DecorationControl"
'            AddHandler MyBase.Load, New EventHandler(AddressOf Me.DecorationControl_Load)
'            MyBase.ResumeLayout(False)
'        End Sub

'        Protected Overridable Function OnNonLinearTransfromNeeded() As Bitmap
'            Dim bitmap As Bitmap = Nothing
'            If (Me.CtrlBmp Is Nothing) Then
'                Return Nothing
'            End If
'            Try
'                bitmap = New Bitmap(MyBase.Width, MyBase.Height)
'                Dim rect As New Rectangle(0, 0, bitmap.Width, bitmap.Height)
'                Dim bitmapdata As BitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb)
'                Dim source As IntPtr = bitmapdata.Scan0
'                Dim length As Integer = ((bitmap.Width * bitmap.Height) * 4)
'                Dim destination As Byte() = New Byte(length - 1) {}
'                Marshal.Copy(source, destination, 0, length)
'                Dim e As New NonLinearTransfromNeededEventArg With { _
'                    .CurrentTime = Me.CurrentTime, _
'                    .ClientRectangle = MyBase.ClientRectangle, _
'                    .Pixels = destination, _
'                    .Stride = bitmapdata.Stride, _
'                    .SourcePixels = Me.CtrlPixels, _
'                    .SourceClientRectangle = New Rectangle(Me.Padding.Left, Me.Padding.Top, Me.DecoratedControl.Width, Me.DecoratedControl.Height), _
'                    .SourceStride = Me.CtrlStride _
'                }
'                Try
'                    If (Not Me.eventHandler_0 Is Nothing) Then
'                        Me.eventHandler_0.Invoke(Me, e)
'                    Else
'                        e.UseDefaultTransform = True
'                    End If
'                    If (e.UseDefaultTransform AndAlso (Me.DecorationType = DecorationType.BottomMirror)) Then
'                        TransfromHelper.DoBottomMirror(e)
'                    End If
'                Catch obj1 As Object
'                End Try
'                Marshal.Copy(destination, 0, source, length)
'                bitmap.UnlockBits(bitmapdata)
'            Catch obj2 As Object
'            End Try
'            Return bitmap
'        End Function

'        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
'            Me.CtrlBmp = Me.GetForeground(Me.DecoratedControl)
'            Me.CtrlPixels = Me.method_4(Me.CtrlBmp)
'            If (Not Me.Frame Is Nothing) Then
'                Me.Frame.Dispose()
'            End If
'            Me.Frame = Me.OnNonLinearTransfromNeeded
'            If (Me.Frame Is Nothing) Then
'                Dim num As Integer
'                Dim num2 As Integer
'                Do While (num = num2)
'                    num2 = 1
'                    Dim num3 As Integer = num
'                    num = 1
'                    If (1 > num3) Then
'                        Return
'                    End If
'                Loop
'            Else
'                e.Graphics.DrawImage(Me.Frame, Point.Empty)
'            End If
'        End Sub

'        Private Sub timer_0_Tick(ByVal sender As Object, ByVal e As EventArgs)
'            Dim decorationType As DecorationType = Me.DecorationType
'            If ((decorationType <> decorationType.BottomMirror) AndAlso (decorationType <> decorationType.Custom)) Then
'                Dim num As Integer
'                Dim num2 As Integer
'                Do While (num = num2)
'                    num2 = 1
'                    Dim num3 As Integer = num
'                    num = 1
'                    If (1 > num3) Then
'                        Return
'                    End If
'                Loop
'            Else
'                MyBase.Invalidate()
'            End If
'        End Sub


'        ' Properties
'        Public Property CtrlBmp As Bitmap
'            <CompilerGenerated()> _
'            Get
'                Return Me.bitmap_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Bitmap)
'                Me.bitmap_0 = value
'            End Set
'        End Property

'        Public Property CtrlPixels As Byte()
'            <CompilerGenerated()> _
'            Get
'                Return Me.byte_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Byte())
'                Me.byte_0 = value
'            End Set
'        End Property

'        Public Property CtrlStride As Integer
'            <CompilerGenerated()> _
'            Get
'                Return Me.int_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Integer)
'                Me.int_0 = value
'            End Set
'        End Property

'        Public Property CurrentTime As Single
'            <CompilerGenerated()> _
'            Get
'                Return Me.float_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Single)
'                Me.float_0 = value
'            End Set
'        End Property

'        Public Property DecoratedControl As Control
'            <CompilerGenerated()> _
'            Get
'                Return Me.control_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Control)
'                Me.control_0 = value
'            End Set
'        End Property

'        Public Property DecorationType As DecorationTypeEnum
'            <CompilerGenerated()> _
'            Get
'                Return Me.decorationType_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As DecorationTypeEnum)
'                Me.decorationType_0 = value
'            End Set
'        End Property

'        Public Property Frame As Bitmap
'            <CompilerGenerated()> _
'            Get
'                Return Me.bitmap_1
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Bitmap)
'                Me.bitmap_1 = value
'            End Set
'        End Property

'        Public Property Padding As Padding
'            <CompilerGenerated()> _
'            Get
'                Return Me.padding_0
'            End Get
'            <CompilerGenerated()> _
'            Set(ByVal value As Padding)
'                Me.padding_0 = value
'            End Set
'        End Property


'        ' Fields
'        <CompilerGenerated()> _
'        Private bitmap_0 As Bitmap
'        <CompilerGenerated()> _
'        Private bitmap_1 As Bitmap
'        Private bool_0 As Boolean
'        <CompilerGenerated()> _
'        Private byte_0 As Byte()
'        <CompilerGenerated()> _
'        Private control_0 As Control
'        <CompilerGenerated()> _
'        Private decorationType_0 As DecorationTypeEnum
'        <CompilerGenerated()> _
'        Private float_0 As Single
'        <CompilerGenerated()> _
'        Private int_0 As Integer
'        <CompilerGenerated()> _
'        Private padding_0 As Padding
'        Private timer_0 As System.Windows.Forms.Timer
'    End Class



'    Public Interface IFakeControl
'        ' Events
'        Event FramePainted As EventHandler(Of PaintEventArgs)
'        Event FramePainting As EventHandler(Of PaintEventArgs)
'        Event TransfromNeeded As EventHandler(Of TransfromNeededEventArg)

'        ' Methods
'        Sub InitParent(ByVal animatedControl As Control, ByVal padding As Padding)

'        ' Properties
'        Property BgBmp As Bitmap

'        Property Frame As Bitmap

'    End Interface



'End Class


