Module Module1

    Sub Main()
        Dim Count As Integer = 0
        Dim Sum As Integer = 0
        Dim n As Integer

        Dim Max As Integer = 500000000 / 2
        Dim Numbers As New BitArray(Max)

        For i As Integer = 0 To Max - 1
            Numbers.Item(i) = True
        Next

        For index As Integer = 1 To Max - 1
            If Numbers.Item(index) = True Then
                n = 2 * index + 1
                For i As Integer = index + n To Max - 1 Step n
                    Numbers.Item(i) = False
                Next
                Count += 1
                Sum += n
            End If
        Next
        Console.WriteLine(Count & " " & Sum)
    End Sub

End Module
