Module Globals
    Public GameNumber As Integer = 0
    Public GameList As New List(Of GameData)
    Public Timetable As New Queue(Of GameTimetable)
    Public numberdifferentscored(1) As Integer
    Public numberinSinBin As Integer = 0
    Public numberOfBreaks As Integer = -1
    Public numberSetup As Integer = 0
    Public TeamTimeout As Boolean = False
    Public RefereeTimeout As Boolean = False
    Public WhiteTimeout As Boolean = True
    Public BlackTimeout As Boolean = True
    Public TimeoutTimeEdit As Date

End Module
