Public Class Team
    Private TeamName As String
    Private Timeout As Boolean = True
    Private GoalScorers(13) As Integer
    Private SinBinTimes(13) As Integer

    Public Function getTeamName()
        Return TeamName
    End Function
    Public Function getTimeout()
        Return Timeout
    End Function
    Public Function getGoalScorer(ByVal PlayerNumber As Integer)
        Return GoalScorers(PlayerNumber)
    End Function
    Public Function getSinBinTimes(ByVal PlayerNumber As Integer)
        Return SinBinTimes(PlayerNumber)
    End Function
    Public Function setTeamName(ByVal Name As String)
        Me.TeamName = Name
        Return True
    End Function
    Public Function setTimeout(ByVal ChangeTo As Boolean)
        Me.Timeout = ChangeTo
        Return True
    End Function
    Public Function setGoalScorer(ByVal PlayerNumber As Integer)
        Me.GoalScorers(PlayerNumber) += 1
        Return True
    End Function
    Public Function setSinBinTimes(ByVal PlayerNumber As Integer, ByVal Time As Integer)
        Me.SinBinTimes(PlayerNumber) += Time
        Return True
    End Function

End Class
