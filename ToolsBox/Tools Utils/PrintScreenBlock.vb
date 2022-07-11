Imports System
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Drawing

Namespace Utils
    Partial Public Class PrintScreenBlock
        Const WDA_NONE As UInteger = 0
        Const WDA_MONITOR As UInteger = 1

        <DllImport("user32.dll")>
        Protected Shared Function SetWindowDisplayAffinity(ByVal hWnd As IntPtr, ByVal dwAffinity As UInteger) As UInteger
        End Function

        Shared Function BlockPrintScreen(ByVal Handle As IntPtr) As Boolean
            Try
                SetWindowDisplayAffinity(Handle, WDA_MONITOR)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Shared Function EnablePrintScreen(ByVal Handle As IntPtr) As Boolean
            Try
                SetWindowDisplayAffinity(Handle, WDA_NONE)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Class
    ' Must inherit Control, not Component, in order to have Handle
    <DefaultEvent("ClipboardChanged")>
    Partial Public Class ClipboardMonitor
        Inherits Control
        Private nextClipboardViewer As IntPtr

        Public Sub New()
            Me.BackColor = Color.Red
            Me.Visible = False

            nextClipboardViewer = CType(ClipboardMonitor.SetClipboardViewer(CInt(Me.Handle)), IntPtr)
        End Sub

        ''' <summary>
        ''' Clipboard contents changed.
        ''' </summary>
        Public Event ClipboardChanged As EventHandler(Of ClipboardChangedEventArgs)

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            ChangeClipboardChain(Me.Handle, nextClipboardViewer)
        End Sub

        <DllImport("User32.dll")>
        Protected Shared Function SetClipboardViewer(ByVal hWndNewViewer As Integer) As Integer
        End Function

        <DllImport("User32.dll", CharSet:=CharSet.Auto)>
        Public Shared Function ChangeClipboardChain(ByVal hWndRemove As IntPtr, ByVal hWndNewNext As IntPtr) As Boolean
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Auto)>
        Public Shared Function SendMessage(ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
        End Function

        Protected Overrides Sub WndProc(ByRef m As Windows.Forms.Message)
            ' defined in winuser.h
            Const WM_DRAWCLIPBOARD = &H308
            Const WM_CHANGECBCHAIN = &H30D

            Select Case m.Msg
                Case WM_DRAWCLIPBOARD
                    OnClipboardChanged()
                    SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam)

                Case WM_CHANGECBCHAIN
                    If m.WParam = nextClipboardViewer Then
                        nextClipboardViewer = m.LParam
                    Else
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam)
                    End If

                Case Else
                    MyBase.WndProc(m)
            End Select
        End Sub

        Private Sub OnClipboardChanged()
            Try
                Dim iData As IDataObject = Clipboard.GetDataObject()
                RaiseEvent ClipboardChanged(Me, New ClipboardChangedEventArgs(iData))
            Catch e As Exception
                ' Swallow or pop-up, not sure
                ' Trace.Write(e.ToString());
                MsgBox(e.ToString())
            End Try
        End Sub
    End Class

    Public Class ClipboardChangedEventArgs
        Inherits EventArgs
        Public ReadOnly DataObject As IDataObject

        Public Sub New(ByVal dataObject As IDataObject)
            Me.DataObject = dataObject
        End Sub
    End Class

    Partial Public Class PrintScreenBlockHotKey
        Protected Shared ReadOnly HotKeyID As Short = 100
        Private Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
        Private Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer

        Shared Sub BlockKey(ByVal Form As Form)
            RegisterHotKey(Form.Handle, HotKeyID, Nothing, Keys.PrintScreen)
        End Sub

        Shared Sub UnBlockKey(ByVal Form As Form)
            UnregisterHotKey(Form.Handle, HotKeyID)
        End Sub
    End Class
End Namespace
