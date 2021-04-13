Module Funciones

    Function MidFun(ByVal s As String, ByVal a As Integer, ByVal b As Integer) As String
        Try
            Dim temp As String = s.Substring((a - 1), b)
            Return temp
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Function IsNumeric(ByVal Expression As Object) As Boolean
        ' Variable to collect the Return value of the TryParse method.
        Dim isNum As Boolean
        ' Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
        Dim retNum As Double
        ' The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
        ' The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
        isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, retNum)
        Return isNum
    End Function



End Module
