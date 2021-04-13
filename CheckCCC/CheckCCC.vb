Public Class CheckCCC

    Shared Function CheckDC(ByVal Banco As String, ByVal Oficina As String, ByVal CCC As String) As String
        'DÍGITOS DE CONTROL
        'Como se expuso anteriormente, verifican los códigos de Entidad y oficina, el
        'primero, y el número de cuenta, el segundo.
        'Para la obtención de cada uno de ellos, se utiliza el módulo 11: la suma de los
        'productos obtenidos al multiplicar cada una de las cifras componentes de los
        'elementos por los pesos asignados, se divide entre 11, el resto de tal división se
        'deduce de 11, cuyo resultado es el dígito de control, con las excepciones
        'siguientes: si fuese 10, se considerará 1, y si fuera 11, cero.

        'Los pesos a utilizar, son los siguientes:
        'Unidad ...................................................................................... 6
        'Decena....................................................................................... 3
        'Centena ..................................................................................... 7
        'Unidad de millar ............................................................................ 9
        'Decena de millar ........................................................................... 10
        'Centena de millar............................................................................ 5
        'Unidad de millón ............................................................................ 8
        'Decena de millón ............................................................................ 4
        'Centena de millón ........................................................................... 2
        'Unidad de millar de millón .................................................................. 1

        Dim Pesos(9) As Byte

        Pesos(0) = 6  ' Unidad
        Pesos(1) = 3  ' Decena
        Pesos(2) = 7  ' Centena
        Pesos(3) = 9  ' Unidad de millar
        Pesos(4) = 10 ' Decena de millar
        Pesos(5) = 5  ' Centena de millar
        Pesos(6) = 8  ' Unidad de millón
        Pesos(7) = 4  ' Decena de millón
        Pesos(8) = 2  ' Centena de millón
        Pesos(9) = 1  ' Unidad de millar de millón

        Dim Posi As Byte
        Dim PrCad As String
        Dim ValorSumado As Integer
        Dim DC1 As Byte
        Dim DC2 As Byte

        'Calculo del Primer digito
        PrCad = Banco & Oficina  ' Para calcular el primer digito hay que anexar ambos valores
        For Posi = 0 To 7
            ValorSumado = ValorSumado + (Pesos(Posi) * MidFun(PrCad, (8 - Posi), 1))
        Next Posi

        DC1 = 11 - (ValorSumado Mod 11)
        If DC1 = 10 Then
            DC1 = 1
        Else
            If DC1 = 11 Then DC1 = 0
        End If

        ' Calculo del Segundo digito
        ValorSumado = 0
        For Posi = 0 To 9
            ValorSumado = ValorSumado + (Pesos(Posi) * MidFun(CCC, (10 - Posi), 1))
        Next Posi

        DC2 = 11 - (ValorSumado Mod 11)
        If DC2 = 10 Then
            DC2 = 1
        Else
            If DC2 = 11 Then DC2 = 0
        End If

        CheckDC = CStr(DC1) & CStr(DC2)
    End Function

End Class
