Public Class GameTimetable
    Private GameState As String
    Private Duration As Double
    Private StartTime As Date
    Private TimeMarker As Date
    Public Function getGameState()
        Return GameState
    End Function
    Public Function getDuration()
        Return Duration
    End Function
    Public Function getStartTime()
        Return StartTime
    End Function
    Public Function getTimeMarker()
        Return TimeMarker
    End Function
    Public Function SetGameState(ByVal GameState As String)
        Me.GameState = GameState
        Return True
    End Function
    Public Function SetDuration(ByVal Duration As Double)
        Me.Duration = Duration
        Return True
    End Function
    Public Function setStartTime(ByVal StartTime As Date)
        Me.StartTime = StartTime
        Return True
    End Function
    Public Function setTimeMarker()
        Dim durationMin As Double
        Dim durationSec As Double
        durationMin = Int(Me.Duration)
        durationSec = (Me.Duration - Int(Me.Duration)) * 60
        Me.TimeMarker = Me.StartTime
        Me.TimeMarker = Me.TimeMarker.AddMinutes(durationMin).AddSeconds(durationSec)
        Return True
    End Function

End Class
