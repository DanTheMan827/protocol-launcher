Imports System.IO
Module modMain

    Sub Main(ByVal sArgs() As String)
        If sArgs.Length = 1 Then
            Process.Start(sArgs(0))
            Console.WriteLine("Press any key to continue . . .")
            Console.ReadKey()
        End If
    End Sub

End Module
