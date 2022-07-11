Namespace Utils
    Public Class MouseNoClick
        Inherits NativeWindow

        Private Const WM_LBUTTONDOWN As Integer = &H201
        Private Const WM_LBUTTONUP As Integer = &H202
        Private Const WM_LBUTTONDOUBLECLICK As Integer = &H203
        Private Const WM_RBUTTONBLOCKCLICK As Integer = &H204

        Private COMMANDO As Integer = 0

        Enum MOUSEF
            LEFTBLOCKCLICK
            LEFTBLOCKLEAVE
            LEFTBLOCKDOUBLECLICK
            RIGHTBLOCKCLICK
        End Enum
        Public Sub New(ByVal Handle As IntPtr, ByVal MOUSE As MOUSEF)
            On Error Resume Next
            Select Case MOUSE
                Case MOUSEF.LEFTBLOCKCLICK
                    COMMANDO = WM_LBUTTONDOWN
                Case MOUSEF.LEFTBLOCKLEAVE
                    COMMANDO = WM_LBUTTONUP
                Case MOUSEF.LEFTBLOCKDOUBLECLICK
                    COMMANDO = WM_LBUTTONDOUBLECLICK
                Case MOUSEF.RIGHTBLOCKCLICK
                    COMMANDO = WM_RBUTTONBLOCKCLICK
            End Select
            Me.AssignHandle(Handle)
        End Sub
        'Ignore window messages raised by the right mouse button.
        Protected Overrides Sub WndProc(ByRef m As Message)
            On Error Resume Next
            If Not (m.Msg = COMMANDO) Then
                MyBase.WndProc(m)
            End If
        End Sub
    End Class

    Public Module ClicMouse
        Enum MOUSEF
            LEFT_BLOCKCLICK
            LEFT_BLOCKLEAVE
            LEFT_BLOCKDOUBLECLICK
            RIGHT_BLOCKCLICK
        End Enum

        Public Sub MouseBlockRightClick(ByVal Objecto As Object, ByVal BLOCK As MOUSEF)
            Dim FlashWatcher As MouseNoClick = Nothing
            FlashWatcher = New MouseNoClick(Objecto.Handle, BLOCK)
        End Sub
    End Module
End Namespace
