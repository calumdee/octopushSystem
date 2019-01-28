Public Class GameData
    Private TeamName(1) As String
    Private WhiteTeamGoalScorers(13) As Integer
    Private BlackTeamGoalScorers(13) As Integer
    Private WhiteSinBinTime(13) As Integer
    Private BlackSinBinTime(13) As Integer
    Private RefereeNames(3) As String
    Public Function getTeamName(Number As Integer)
            Return TeamName(Number)
    End Function
    Public Function getWhiteTeamGoalScorers(PlayerNumber As Integer)
        Return WhiteTeamGoalScorers(PlayerNumber)
    End Function
    Public Function getBlackTeamGoalScorers(PlayerNumber As Integer)
        Return BlackTeamGoalScorers(PlayerNumber)
    End Function
    Public Function getWhiteSinBinTime(PlayerNumber As Integer)
        Return WhiteSinBinTime(PlayerNumber)
    End Function
    Public Function getBlackSinBinTime(PlayerNumber As Integer)
        Return BlackSinBinTime(PlayerNumber)
    End Function
    Public Function getRefereeNames(Number As Integer)
        Return RefereeNames(Number)
    End Function
    Public Function setTeamName(number As Integer, name As String)
        TeamName(number) = name
        Return True
    End Function
    Public Function setWhiteTeamGoalScorers(PlayerNumber As Integer, addorremove As String)
        If PlayerNumber < 14 And PlayerNumber > 0 Then
            If addorremove = "Add" Then
                WhiteTeamGoalScorers(PlayerNumber) += 1
                Return True
            ElseIf addorremove = "Remove" Then
                If WhiteTeamGoalScorers(PlayerNumber) > 0 Then
                    WhiteTeamGoalScorers(PlayerNumber) += -1
                    Return True
                End If
            End If
        End If
        Return False
    End Function
    Public Function setBlackTeamGoalScorers(PlayerNumber As Integer, addorremove As String)
        If PlayerNumber < 14 And PlayerNumber > 0 Then
            If addorremove = "Add" Then
                BlackTeamGoalScorers(PlayerNumber) += 1
                Return True
            ElseIf addorremove = "Remove" Then
                If BlackTeamGoalScorers(PlayerNumber) > 0 Then
                    BlackTeamGoalScorers(PlayerNumber) += -1
                    Return True
                End If
            End If
        End If
        Return False
    End Function
    Public Function setWhiteSinBinTime(PlayerNumber As Integer, time As String)
        If PlayerNumber < 14 And PlayerNumber > 0 Then
            WhiteSinBinTime(PlayerNumber) += time
            Return True
        End If
        Return False
    End Function
    Public Function setBlackSinBinTime(PlayerNumber As Integer, time As String)
        If PlayerNumber < 14 And PlayerNumber > 0 Then
            BlackSinBinTime(PlayerNumber) += time
            Return True
        End If
        Return False
    End Function
    Public Function setRefereeNames(number As Integer, Name As String)
        RefereeNames(number) = Name
        Return True
    End Function
End Class
