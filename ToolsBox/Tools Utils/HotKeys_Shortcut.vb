Imports System.Runtime.InteropServices
Namespace Utils
    Public Class HotKeys
        Inherits NativeWindow
        Implements IDisposable

#Region " Declarations "
        Protected Declare Function UnregisterHotKey Lib "user32.dll" (ByVal handle As IntPtr, ByVal id As Integer) As Boolean
        Protected Declare Function RegisterHotKey Lib "user32.dll" (ByVal handle As IntPtr, ByVal id As Integer, ByVal modifier As Integer, ByVal vk As Integer) As Boolean

        Event Press(ByVal sender As Object, ByVal e As HotKeyEventArgs)
        Protected EventArgs As HotKeyEventArgs, ID As Integer

        Enum Modifier As Integer
            None = 0
            Alt = &H1
            Control = &H2
            AltAndControl = &H3
            Shift = &H4
            ShiftAndAlt = &H5
            ShiftAndControl = &H6
            Winkey = &H8
            WinkeyAndAlt = &H9
            WinkeyAndControl = &H10
            WinkeyAndShift = &H12
        End Enum
        Class HotKeyEventArgs
            Inherits EventArgs
            Property Modifier As HotKeys.Modifier
            Property Key As Keys
        End Class
        Class RegisteredException
            Inherits Exception
            Protected Const s As String = "Shortcut combination is in use."
            Sub New()
                MyBase.New(s)
            End Sub
        End Class
#End Region

#Region " IDisposable "
        Private disposed As Boolean
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not disposed Then UnregisterHotKey(Handle, ID)
            disposed = True
        End Sub
        Protected Overrides Sub Finalize()
            Dispose(False)
            MyBase.Finalize()
        End Sub
        Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

        <DebuggerStepperBoundary()>
        Sub New(ByVal modifier As Modifier, ByVal key As Keys)
            On Error Resume Next
            CreateHandle(New CreateParams)
            ID = GetHashCode()
            EventArgs = New HotKeyEventArgs With {.Key = key, .Modifier = modifier}
            If Not RegisterHotKey(Handle, ID, modifier, key) Then Throw New RegisteredException
        End Sub

        Shared Function Create(ByVal modifier As Modifier, ByVal key As Keys) As HotKeys
            On Error Resume Next
            Return New HotKeys(modifier, key)
        End Function

        Protected Sub New()

        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            Select Case m.Msg
                Case 786
                    RaiseEvent Press(Me, EventArgs)
                Case Else
                    MyBase.WndProc(m)
            End Select
        End Sub

      
           
        Public Const WM_HOTKEY As Integer = &H312


        Public Shared Sub RegisterHotkey(ByRef sourceForm As Form, ByVal triggerKey As Keys, ByVal modifier As Modifier)
            RegisterHotKey(sourceForm.Handle, 1, modifier, triggerKey)
        End Sub
        Public Shared Sub UnregisterHotkeys(ByRef sourceForm As Form)
            UnregisterHotKey(sourceForm.Handle, 1)  'Remember to call unregisterHotkeys() when closing your application.
        End Sub
        Public Shared Sub HandleHotKeyEvent(ByVal hotkeyID As IntPtr)
            MsgBox("The hotkey was pressed")
        End Sub

        'Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        '    If m.Msg = HotKeys.WM_HOTKEY Then
        '        HotKeys.handleHotKeyEvent(m.WParam)
        '    End If
        '    MyBase.WndProc(m)
        'End Sub 'System wide hotkey event handling

    End Class
End Namespace


