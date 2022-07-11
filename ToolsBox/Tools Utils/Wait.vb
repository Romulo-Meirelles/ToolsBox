Namespace Utils
    Public Module WaitTime
        Public Sub Wait(ByVal Second As Integer)
            Dim Final As Date = TimeOfDay.AddSeconds(Second)
            While Not TimeOfDay.Second = Final.Second
                Application.DoEvents()
            End While
        End Sub
    End Module
End Namespace
