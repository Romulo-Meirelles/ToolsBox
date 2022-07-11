Imports System
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.Windows.Forms

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.Form))> _
Public Class Form_Hide
    Inherits Component

    ' Fields
    Private backgroundWorker_0 As BackgroundWorker
    Private backgroundWorker_Hide As BackgroundWorker
    Private double_0 As Double
    Private form_0 As New Form
    Private icontainer_0 As IContainer
    Private int_0 As Integer
    Private eventHandler_0 As EventHandler
    Private Close_0 As Boolean

    ' Events
    Public Event TransitionEnd As EventHandler

    ' Methods
    Public Sub New()
        Me.int_0 = 1
        Me.method_0()
    End Sub

    Public Sub New(ByVal container As IContainer)
        Me.int_0 = 1
        container.Add(Me)
        Me.method_0()
    End Sub


    Private Sub backgroundWorker_0_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Try
            Me.double_0 = 0
            Do While (Me.double_0 < 1)
                Me.backgroundWorker_0.ReportProgress(0)
                Thread.Sleep(Me.Delay)
                Me.double_0 = (Me.double_0 + 0.001)
            Loop
        Catch ex As Exception
        End Try

    End Sub

    Private Sub backgroundWorker_0_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        Try
            Me.form_0.Opacity = Me.double_0
        Catch ex As Exception
        End Try
    End Sub

    Private Sub backgroundWorker_0_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Try
            Me.form_0.Opacity = 1
            If (Me.eventHandler_0 Is Nothing) Then
                Dim num As Integer
                Dim num2 As Integer
                Do While (num = num2)
                    num2 = 1
                    Dim num3 As Integer = num
                    num = 1
                    If (1 > num3) Then
                        Return
                    End If
                Loop
            Else
                Me.eventHandler_0.Invoke(sender, e)
            End If
        Catch ex As Exception

        End Try

    End Sub

    'Hide
    Private Sub backgroundWorker_Hide_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Try
            Me.double_0 = 1
            Do While (Me.double_0 > 0)
                Me.backgroundWorker_Hide.ReportProgress(0)
                Thread.Sleep(Me.Delay)
                Me.double_0 = (Me.double_0 - 0.001)
            Loop
        Catch ex As Exception
        End Try
    End Sub
    Private Sub backgroundWorker_Hide_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Try
            Me.form_0.Opacity = 0

            If Close_0 = True Then
                Me.form_0.Close()
            End If

            If (Me.eventHandler_0 Is Nothing) Then
                Dim num As Integer
                Dim num2 As Integer
                Do While (num = num2)
                    num2 = 1
                    Dim num3 As Integer = num
                    num = 1
                    If (1 > num3) Then
                        Return
                    End If
                Loop
            Else
                Me.eventHandler_0.Invoke(sender, e)
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub backgroundWorker_Hide_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        Try
            Me.form_0.Opacity = Me.double_0
        Catch ex As Exception
        End Try
    End Sub
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If (disposing AndAlso (Not Me.icontainer_0 Is Nothing)) Then
            Me.icontainer_0.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Public Sub HideAsyc(ByVal frm As Form, Optional ByVal Close As Boolean = False)
        Dim num As Integer
        Dim num2 As Integer

        Try
            Close_0 = Close
            Me.form_0 = frm
            Me.backgroundWorker_Hide.RunWorkerAsync()

        Catch exception1 As Exception
        End Try

        Do While (num = num2)
            num2 = 1
            Dim num3 As Integer = num
            num = 1
            If (1 > num3) Then
                Exit Do
            End If
        Loop
    End Sub

    Private Sub method_0()
        Me.backgroundWorker_0 = New BackgroundWorker
        Me.backgroundWorker_0.WorkerReportsProgress = True
        AddHandler Me.backgroundWorker_0.DoWork, New DoWorkEventHandler(AddressOf Me.backgroundWorker_0_DoWork)
        AddHandler Me.backgroundWorker_0.ProgressChanged, New ProgressChangedEventHandler(AddressOf Me.backgroundWorker_0_ProgressChanged)
        AddHandler Me.backgroundWorker_0.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf Me.backgroundWorker_0_RunWorkerCompleted)

        Me.backgroundWorker_Hide = New BackgroundWorker
        Me.backgroundWorker_Hide.WorkerReportsProgress = True
        AddHandler Me.backgroundWorker_Hide.DoWork, New DoWorkEventHandler(AddressOf Me.backgroundWorker_Hide_DoWork)
        AddHandler Me.backgroundWorker_Hide.ProgressChanged, New ProgressChangedEventHandler(AddressOf Me.backgroundWorker_Hide_ProgressChanged)
        AddHandler Me.backgroundWorker_Hide.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf Me.backgroundWorker_Hide_RunWorkerCompleted)


    End Sub

    Public Sub ShowAsyc(ByVal frm As Form)
        Dim num As Integer
        Dim num2 As Integer
        Try
            Me.form_0 = frm
            Me.backgroundWorker_0.RunWorkerAsync()
        Catch exception1 As Exception
        End Try
        Do While (num = num2)
            num2 = 1
            Dim num3 As Integer = num
            num = 1
            If (1 > num3) Then
                Exit Do
            End If
        Loop
    End Sub

    ' Properties
    Public Property Delay As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            Me.int_0 = value
        End Set
    End Property
End Class

