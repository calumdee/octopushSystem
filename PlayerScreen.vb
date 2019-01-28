Public Class PlayerScreen
    Dim halfwidth As Integer
    Dim dataheight As Integer
    Dim wordboxheight As Integer
    Dim column1Xposition As Integer
    Dim column2Xposition As Integer
    Dim row1Yposition As Integer
    Dim row2Yposition As Integer
    Dim row3Yposition As Integer
    Dim row4Yposition As Integer
    Dim loaded As Boolean = False

    Private Sub PlayerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = Color.RoyalBlue
        Me.Size = New Size(600, 600)
        Me.MinimumSize = New Size(500, 500)
        createFormControls()
    End Sub
    Private Sub createFormControls()
        Dim colour As String = "White"
        For i As Integer = 0 To 1
            createTeamNameLabels(colour)
            createTeamScoreLabels(colour)
            colour = "Black"
        Next
        createGameTimeLabel()
        createGameStateLabel()
    End Sub
    Private Sub createTeamNameLabels(colour As String)
        Dim lblTeamName As New Label
        lblTeamName.Name = "lbl" & colour & "TeamName"
        lblTeamName.TextAlign = ContentAlignment.MiddleCenter
        If colour = "White" Then
            lblTeamName.Text = GameList(GameNumber).getTeamName(0)
            lblTeamName.BackColor = Color.White
            lblTeamName.ForeColor = Color.Black
        Else
            lblTeamName.Text = GameList(GameNumber).getTeamName(1)
            lblTeamName.BackColor = Color.Black
            lblTeamName.ForeColor = Color.White
        End If
        lblTeamName.Tag = colour
        Me.Controls.Add(lblTeamName)
        sizeTeamNamelabels(colour)
        lblTeamName.BorderStyle = BorderStyle.FixedSingle
    End Sub
    Private Sub createTeamScoreLabels(colour As String)
        Dim lblTeamScore As New Label
        lblTeamScore.Name = "lbl" & colour & "Score"
        lblTeamScore.TextAlign = ContentAlignment.MiddleCenter
        lblTeamScore.BorderStyle = BorderStyle.FixedSingle
        Dim lblCurrentGameScore As Label = Octopush.Controls("TabControl1").Controls("tabCurrentGame").Controls("lbl" & colour & "GoalScored")
        lblTeamScore.Text = lblCurrentGameScore.Text
        lblTeamScore.Tag = colour
        If colour = "White" Then
            lblTeamScore.BackColor = Color.White
            lblTeamScore.ForeColor = Color.Black
        Else
            lblTeamScore.BackColor = Color.Black
            lblTeamScore.ForeColor = Color.White
        End If
        Me.Controls.Add(lblTeamScore)

        sizeTeamScoreLabels(colour)
    End Sub
    Private Sub createGameTimeLabel()
        Dim lblGameTime As New Label
        lblGameTime.Name = "lblGameTime"
        lblGameTime.TextAlign = ContentAlignment.MiddleCenter
        lblGameTime.BorderStyle = BorderStyle.FixedSingle
        lblGameTime.BackColor = Color.FromArgb(200, 255, 255)
        lblGameTime.Text = "00:00"
        Me.Controls.Add(lblGameTime)
        sizeGameTimeLabel()
    End Sub
    Private Sub createGameStateLabel()
        Dim lblGameState As New Label
        lblGameState.Name = "lblGameState"
        lblGameState.TextAlign = ContentAlignment.MiddleCenter
        lblGameState.BorderStyle = BorderStyle.FixedSingle
        lblGameState.BackColor = Color.FromArgb(200, 255, 255)
        Me.Controls.Add(lblGameState)
        sizeGameStateLabel()
    End Sub
    Private Sub sizeTeamNameLabels(colour)
        Dim lblTeamName As Label = Me.Controls("lbl" & colour & "TeamName")
        lblTeamName.Size = New Size(halfwidth, wordboxheight)
        If colour = "White" Then
            lblTeamName.Location = New Point(column1Xposition, row1Yposition)
        Else
            lblTeamName.Location = New Point(column2Xposition, row1Yposition)
        End If
        Dim fontcount As Integer = 0
        Dim sizeOfString As New SizeF
        Dim g As Graphics = Me.CreateGraphics
        Do
            fontcount += 1
            lblTeamName.Font = New Font(lblTeamName.Font.Name, fontcount)
            sizeOfString = g.MeasureString(" ", lblTeamName.Font)
        Loop Until sizeOfString.Height > lblTeamName.Height
        fontcount += -2
        lblTeamName.Font = New Font(lblTeamName.Font.Name, fontcount, FontStyle.Bold)
    End Sub
    Private Sub sizeTeamScoreLabels(colour As String)
        Dim lblTeamScore As Label = Me.Controls("lbl" & colour & "Score")
        lblTeamScore.Size = New Size(halfwidth, dataheight)
        If colour = "White" Then
            lblTeamScore.Location = New Point(column1Xposition, row2Yposition)
        Else
            lblTeamScore.Location = New Point(column2Xposition, row2Yposition)
        End If

    End Sub
    Private Sub sizeGameTimeLabel()
        Dim lblGameTime As Label = Me.Controls("lblGameTime")
        lblGameTime.Size = New Size(halfwidth * 2 + 20, dataheight)
        lblGameTime.Location = New Point(column1Xposition, row3Yposition)
        Dim fontcount As Integer = 0
        Dim sizeOfString As New SizeF
        Dim g As Graphics = Me.CreateGraphics
        Do
            fontcount += 1
            lblGameTime.Font = New Font(lblGameTime.Font.Name, fontcount)
            sizeOfString = g.MeasureString(lblGameTime.Text, lblGameTime.Font)
        Loop Until sizeOfString.Height > lblGameTime.Height
        fontcount += -2
        lblGameTime.Font = New Font(lblGameTime.Font.Name, fontcount, FontStyle.Bold)
        Dim colour As String = "White"
        For i As Integer = 0 To 1
            Dim lblTeamScore As Label = Me.Controls("lbl" & colour & "Score")
            lblTeamScore.Font = lblGameTime.Font
            colour = "Black"
        Next
    End Sub
    Private Sub sizeGameStateLabel()
        Dim lblGameState As Label = Me.Controls("lblGameState")
        lblGameState.Size = New Size(halfwidth * 2 + 20, wordboxheight)
        lblGameState.Location = New Point(column1Xposition, row4Yposition)
        Dim lblTeamName As Label = Me.Controls("lblWhiteTeamName")
        lblGameState.Font = lblTeamName.Font
    End Sub
    Private Sub PlayerScreen_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        halfwidth = (Me.Width - 75) * 0.5
        dataheight = (Me.Height - 135) * 0.4
        wordboxheight = (Me.Height - 135) * 0.1
        column1Xposition = 20
        column2Xposition = column1Xposition + halfwidth + 20
        row1Yposition = 20
        row2Yposition = row1Yposition + wordboxheight + 20
        row3Yposition = row2Yposition + dataheight + 20
        row4Yposition = row3Yposition + dataheight + 20
        If loaded = False Then
            loaded = True
        Else
            resizeControls()
        End If
    End Sub
    Private Sub resizeControls()
        Dim colour As String = "White"
        For i As Integer = 0 To 1
            sizeTeamNameLabels(colour)
            sizeTeamScoreLabels(colour)
            colour = "Black"
        Next
        sizeGameTimeLabel()
        sizeGameStateLabel()
    End Sub
    Public Sub updateTimeControls()
        Dim lblGameTime As Label = Me.Controls("lblGameTime")
        Dim lblGameState As Label = Me.Controls("lblGameState")
        Dim GameTimelabel As Label = Octopush.Controls("TabControl1").Controls("tabCurrentGame").Controls("lblGameTime")
        lblGameState.Text = Timetable(0).getGameState
        lblGameTime.Text = GameTimelabel.Text
    End Sub
End Class