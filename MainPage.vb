Imports System.Text.RegularExpressions
Public Class Octopush
    Public loaded As Boolean = False
    Public Const password As String = "235"
    Public columnedgeCurrentGameWidth As Integer
    Public columncentreCurrentGameWidth As Integer
    Public doublecolumnedgeCurrentGameWidth As Integer
    Public doublecolumncentreCurrentGameWidth As Integer
    Public endcolumnCurrentGameWidth As Integer
    Public halfPanelWidth As Integer
    Public panelRowHeight As Integer
    Public buttonrowCurrentGameHeight As Integer
    Public labelrow1CurrentGameHeight As Integer
    Public labelrow2CurrentGameHeight As Integer
    Public panelCurrentGameHeight As Integer
    Public column1CurrentGameXPosition As Integer
    Public column15CurrentGameXPosition As Integer
    Public column2CurrentGameXPosition As Integer
    Public column25CurrentGameXPosition As Integer
    Public column3CurrentGameXPosition As Integer
    Public column35CurrentGameXPosition As Integer
    Public column4CurrentGameXPosition As Integer
    Public row1CurrentGameYPosition As Integer
    Public row2CurrentGameYPosition As Integer
    Public row3CurrentGameYPosition As Integer
    Public row4CurrentGameYPosition As Integer
    Public row5CurrentGameYPosition As Integer
    Public panelrow2CurrentGameYPosition As Integer
    Public regex As New Regex("^\d+(.5)?$")
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim BlankGame As New GameData
        GameList.Add(BlankGame)
        Me.BackColor = Color.RoyalBlue
        Me.Size = New Size(714, 436)
        Me.MinimumSize = New Size(714, 436)
        addTabs()
        setupCurrentGame()
        setupGameSetup()
    End Sub
    Private Sub addTabs()
        Dim TabControl1 As New TabControl
        TabControl1.Name = "TabControl1"
        Me.Controls.Add(TabControl1)
        SizeTabControl(Me.Size)
        For i As Integer = 1 To 3
            Dim tabPage As New TabPage()
            If i = 1 Then
                tabPage.Name = "tabCurrentGame"
                tabPage.Text = "Current Game"
            ElseIf i = 2 Then
                tabPage.Name = "tabGameSetup"
                tabPage.Text = "Game Setup"
                AddHandler tabPage.Enter, AddressOf Me.tabGameSetup_Enter
            Else
                tabPage.Name = "tabPreviousResults"
                tabPage.Text = "Previous Results"
            End If
            tabPage.BackColor = Color.RoyalBlue
            TabControl1.TabPages.Add(tabPage)
            tabPage.Show()
        Next
    End Sub
    Private Sub SizeTabControl(FormSize As Size)
        Dim TabControl1 As TabControl = Me.Controls("TabControl1")
        TabControl1.Location = New Point(0, 0)
        TabControl1.Size = New Size(FormSize.Width, FormSize.Height)
    End Sub
    Private Sub tabGameSetup_Enter(sender As Object, e As System.EventArgs)
        Dim con As Control
        For controlIndex As Integer = Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Count - 1 To 0 Step -1
            con = Me.Controls("TabControl1").Controls("tabGameSetup").Controls(controlIndex)
            Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Remove(con)
        Next
        setupGameSetup()
        Me.Enabled = False
        Dim Login As New Login
        Login.Tag = "Game Setup"
        Login.Show()
    End Sub
    Private Sub setupCurrentGame()
        createCurrentGameButtons()
        createCurrentGameLabels()
        createCurrentGamePanels()
    End Sub
    Private Sub createCurrentGameButtons()
        Dim colour As String
        For i As Integer = 0 To 1
            If i = 0 Then
                colour = "White"
            Else
                colour = "Black"
            End If
            createTeamNameButtons(colour)
            createChangeGoalButtons(colour)
            createTeamTimeoutButtons(colour)
            createTeamSinBinButtons(colour)
        Next
        createRefereeButton()
        createRefereeTimeoutButton()
    End Sub
    Private Sub createTeamNameButtons(colour As String)
        Dim btnTeamName As New Button
        btnTeamName.Name = "btn" & colour & "TeamName"
        btnTeamName.Text = "Enter " & colour & " Team Name"
        btnTeamName.Tag = colour
        Me.Controls("TabControl1").Controls("tabCurrentGame").Controls.Add(btnTeamName)
        btnTeamName.BackColor = Color.Gainsboro
        sizeTeamNameButtons(colour)
        AddHandler btnTeamName.Click, AddressOf Me.btnTeamName_Click
    End Sub
    Private Sub createRefereeButton()
        Dim btnRefereeName As New Button
        btnRefereeName.Name = "btnRefereeName"
        btnRefereeName.Text = "Enter Referee's Names"
        Me.Controls("TabControl1").Controls("tabCurrentGame").Controls.Add(btnRefereeName)
        sizeRefereeButton()
        btnRefereeName.BackColor = Color.Gainsboro
        AddHandler btnRefereeName.Click, AddressOf Me.btnRefereeName_Click
    End Sub
    Private Sub createChangeGoalButtons(colour As String)
        Dim addorremove As String
        For i As Integer = 0 To 1
            If i = 0 Then
                addorremove = "Add"
            Else
                addorremove = "Remove"
            End If
            Dim btnChangeGoal As New Button
            btnChangeGoal.Name = "btn" & colour & addorremove & "Goal"
            btnChangeGoal.Text = addorremove & " Goal"
            btnChangeGoal.Tag = colour & " " & addorremove
            Me.Controls("TabControl1").Controls("tabCurrentGame").Controls.Add(btnChangeGoal)
            btnChangeGoal.BackColor = Color.Gainsboro
            sizeAddGoalButtons(colour, addorremove)
            AddHandler btnChangeGoal.Click, AddressOf Me.btnChangeGoal_Click
        Next
    End Sub
    Private Sub createTeamTimeoutButtons(colour As String)
        Dim btnTeamTimeout As New Button
        btnTeamTimeout.Name = "btn" & colour & "Timeout"
        btnTeamTimeout.Text = colour & " Team Timeout"
        btnTeamTimeout.Tag = colour
        Me.Controls("TabControl1").Controls("tabCurrentGame").Controls.Add(btnTeamTimeout)
        btnTeamTimeout.BackColor = Color.Gainsboro
        sizeTeamTimeoutButtons(colour)
        AddHandler btnTeamTimeout.Click, AddressOf Me.btnTeamTimeout_Click
    End Sub
    Private Sub createTeamSinBinButtons(colour As String)
        Dim btnSinBin As New Button
        btnSinBin.Name = "btn" & colour & "SinBin"
        btnSinBin.Text = "Add Sin Bin to the " & colour & " Team"
        btnSinBin.Tag = colour
        Me.Controls("TabControl1").Controls("tabCurrentGame").Controls.Add(btnSinBin)
        btnSinBin.BackColor = Color.Gainsboro
        sizeTeamSinBinButtons(colour)
        AddHandler btnSinBin.Click, AddressOf Me.btnSinBin_Click
    End Sub
    Private Sub createRefereeTimeoutButton()
        Dim btnRefereeTimeout As New Button
        btnRefereeTimeout.Name = "btnRefereeTimeout"
        btnRefereeTimeout.Text = "Referee Timeout"
        Me.Controls("TabControl1").Controls("tabCurrentGame").Controls.Add(btnRefereeTimeout)
        btnRefereeTimeout.BackColor = Color.Gainsboro
        sizeRefereeTimeoutButton()
        AddHandler btnRefereeTimeout.Click, AddressOf Me.btnRefereeTimeout_Click
    End Sub
    Private Sub sizeTeamNameButtons(colour As String)
        Dim TeamNameButtons As Button = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btn" & colour & "TeamName")
        TeamNameButtons.Size = New Size(columnedgeCurrentGameWidth, buttonrowCurrentGameHeight)
        If colour = "White" Then
            TeamNameButtons.Location = New Point(column1CurrentGameXPosition, row1CurrentGameYPosition)
            Dim fontcount As Integer = 0
            Dim sizeOfString As New SizeF
            Dim g As Graphics = Me.CreateGraphics
            Dim fits As Boolean = True
            Dim numberLinesNeeded As Decimal
            Dim numberLinesAvailable As Decimal
            Dim lineSpacingPixel As Decimal
            Dim tempFontFamily As FontFamily
            Do
                fontcount += 1
                TeamNameButtons.Font = New Font(TeamNameButtons.Font.Name, fontcount)
                tempFontFamily = TeamNameButtons.Font.FontFamily
                sizeOfString = g.MeasureString(TeamNameButtons.Text, TeamNameButtons.Font)
                lineSpacingPixel = (TeamNameButtons.Font.Size * tempFontFamily.GetLineSpacing(FontStyle.Regular)) / (tempFontFamily.GetEmHeight(FontStyle.Regular))
                numberLinesNeeded = sizeOfString.Width / (TeamNameButtons.Width - 6)
                numberLinesAvailable = ((TeamNameButtons.Height - 6 + Int(lineSpacingPixel)) / (sizeOfString.Height + Int(lineSpacingPixel)))
                If numberLinesNeeded - Int(numberLinesNeeded) >= 1 - (0.15 * (Int(numberLinesNeeded) + 1)) Then
                    numberLinesNeeded += 0.16 * (Int(numberLinesNeeded) + 1)
                End If
            Loop While numberLinesAvailable >= 1 And numberLinesNeeded <= Int(numberLinesAvailable)
            fontcount += -1
            TeamNameButtons.Font = New Font(TeamNameButtons.Font.Name, fontcount)
        Else
            TeamNameButtons.Location = New Point(column3CurrentGameXPosition, row1CurrentGameYPosition)
            TeamNameButtons.Font = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btnWhiteTeamName").Font
        End If

    End Sub
    Private Sub sizeRefereeButton()
        Dim refereeButton As Button = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btnRefereeName")
        Dim TeamNameButtons As Button = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btnWhiteTeamName")
        refereeButton.Size = New Size(columncentreCurrentGameWidth, buttonrowCurrentGameHeight)
        refereeButton.Location = New Point(column2CurrentGameXPosition, row1CurrentGameYPosition)
        refereeButton.Font = TeamNameButtons.Font
    End Sub
    Private Sub sizeAddGoalButtons(colour As String, addorremove As String)
        Dim ChangeGoalButtons As Button = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btn" & colour & addorremove & "Goal")
        ChangeGoalButtons.Size = New Size(doublecolumnedgeCurrentGameWidth, buttonrowCurrentGameHeight)
        If colour = "White" Then
            If addorremove = "Add" Then
                ChangeGoalButtons.Location = New Point(column1CurrentGameXPosition, row4CurrentGameYPosition)
            Else
                ChangeGoalButtons.Location = New Point(column15CurrentGameXPosition, row4CurrentGameYPosition)
                Dim fontcount As Integer = 0
                Dim sizeOfString As New SizeF
                Dim g As Graphics = Me.CreateGraphics
                Dim fits As Boolean = True
                Dim numberLinesNeeded As Decimal
                Dim numberLinesAvailable As Decimal
                Dim lineSpacingPixel As Decimal
                Dim tempFontFamily As FontFamily
                Do
                    fontcount += 1
                    ChangeGoalButtons.Font = New Font(ChangeGoalButtons.Font.Name, fontcount)
                    tempFontFamily = ChangeGoalButtons.Font.FontFamily
                    sizeOfString = g.MeasureString(ChangeGoalButtons.Text, ChangeGoalButtons.Font)
                    lineSpacingPixel = (ChangeGoalButtons.Font.Size * tempFontFamily.GetLineSpacing(FontStyle.Regular)) / (tempFontFamily.GetEmHeight(FontStyle.Regular))
                    numberLinesNeeded = sizeOfString.Width / (ChangeGoalButtons.Width - 6)
                    numberLinesAvailable = ((ChangeGoalButtons.Height - 6 + Int(lineSpacingPixel)) / (sizeOfString.Height + Int(lineSpacingPixel)))
                    If numberLinesNeeded - Int(numberLinesNeeded) >= 1 - (0.15 * (Int(numberLinesNeeded) + 1)) Then
                        numberLinesNeeded += 0.16 * (Int(numberLinesNeeded) + 1)
                    End If
                Loop While numberLinesAvailable >= 1 And numberLinesNeeded <= Int(numberLinesAvailable)
                fontcount += -1
                ChangeGoalButtons.Font = New Font(ChangeGoalButtons.Font.Name, fontcount)
                Dim ChangeAddGoalButton As Button = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btn" & colour & "AddGoal")
                ChangeAddGoalButton.Font = ChangeGoalButtons.Font
            End If
        Else
            If addorremove = "Add" Then
                ChangeGoalButtons.Location = New Point(column3CurrentGameXPosition, row4CurrentGameYPosition)
            Else
                ChangeGoalButtons.Location = New Point(column35CurrentGameXPosition, row4CurrentGameYPosition)
                Dim ChangeAddGoalButton As Button = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btn" & colour & "AddGoal")
                Dim WhiteChangeGoalButton As Button = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btnWhiteAddGoal")
                ChangeGoalButtons.Font = WhiteChangeGoalButton.Font
                ChangeAddGoalButton.Font = WhiteChangeGoalButton.Font
            End If
        End If
        If addorremove = "Remove" Then

        End If

    End Sub
    Private Sub sizeTeamTimeoutButtons(colour As String)
        Dim TimeoutButton = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btn" & colour & "Timeout")
        TimeoutButton.Size = New Size(doublecolumncentreCurrentGameWidth, buttonrowCurrentGameHeight)
        If colour = "White" Then
            TimeoutButton.Location = New Point(column2CurrentGameXPosition, row4CurrentGameYPosition)
            Dim fontcount As Integer = 0
            Dim sizeOfString As New SizeF
            Dim g As Graphics = Me.CreateGraphics
            Dim fits As Boolean = True
            Dim numberLinesNeeded As Decimal
            Dim numberLinesAvailable As Decimal
            Dim lineSpacingPixel As Decimal
            Dim tempFontFamily As FontFamily
            Do
                fontcount += 1
                TimeoutButton.Font = New Font(TimeoutButton.Font.Name, fontcount)
                tempFontFamily = TimeoutButton.Font.FontFamily
                sizeOfString = g.MeasureString(TimeoutButton.Text, TimeoutButton.Font)
                lineSpacingPixel = (TimeoutButton.Font.Size * tempFontFamily.GetLineSpacing(FontStyle.Regular)) / (tempFontFamily.GetEmHeight(FontStyle.Regular))
                numberLinesNeeded = sizeOfString.Width / (TimeoutButton.Width - 6)
                numberLinesAvailable = ((TimeoutButton.Height - 6 + Int(lineSpacingPixel)) / (sizeOfString.Height + Int(lineSpacingPixel)))
                If numberLinesNeeded - Int(numberLinesNeeded) >= 1 - (0.15 * (Int(numberLinesNeeded) + 1)) Then
                    numberLinesNeeded += 0.16 * (Int(numberLinesNeeded) + 1)
                End If
            Loop While numberLinesAvailable >= 1 And numberLinesNeeded <= Int(numberLinesAvailable)
            fontcount += -1
            TimeoutButton.Font = New Font(TimeoutButton.Font.Name, fontcount)
        Else
            TimeoutButton.Location = New Point(column25CurrentGameXPosition, row4CurrentGameYPosition)
            TimeoutButton.Font = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btnWhiteTimeout").Font
        End If

    End Sub
    Private Sub sizeRefereeTimeoutButton()
        Dim RefereeTimeoutButton = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btnRefereeTimeout")
        RefereeTimeoutButton.Size = New Size(columncentreCurrentGameWidth, buttonrowCurrentGameHeight)
        RefereeTimeoutButton.Location = New Point(column2CurrentGameXPosition, row5CurrentGameYPosition)
        Dim SinBinButton As Button = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btnWhiteSinBin")
        RefereeTimeoutButton.Font = SinBinButton.Font
    End Sub
    Private Sub sizeTeamSinBinButtons(colour As String)
        Dim SinBinButton = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btn" & colour & "SinBin")
        SinBinButton.Size = New Size(columnedgeCurrentGameWidth, buttonrowCurrentGameHeight)
        If colour = "White" Then
            SinBinButton.Location = New Point(column1CurrentGameXPosition, row5CurrentGameYPosition)
            Dim fontcount As Integer = 0
            Dim sizeOfString As New SizeF
            Dim g As Graphics = Me.CreateGraphics
            Dim fits As Boolean = True
            Dim numberLinesNeeded As Decimal
            Dim numberLinesAvailable As Decimal
            Dim lineSpacingPixel As Decimal
            Dim tempFontFamily As FontFamily
            Do
                fontcount += 1
                SinBinButton.Font = New Font(SinBinButton.Font.Name, fontcount)
                tempFontFamily = SinBinButton.Font.FontFamily
                sizeOfString = g.MeasureString(SinBinButton.Text, SinBinButton.Font)
                lineSpacingPixel = (SinBinButton.Font.Size * tempFontFamily.GetLineSpacing(FontStyle.Regular)) / (tempFontFamily.GetEmHeight(FontStyle.Regular))
                numberLinesNeeded = sizeOfString.Width / (SinBinButton.Width - 6)
                numberLinesAvailable = ((SinBinButton.Height - 6 + Int(lineSpacingPixel)) / (sizeOfString.Height + Int(lineSpacingPixel)))
                If numberLinesNeeded - Int(numberLinesNeeded) >= 1 - (0.15 * (Int(numberLinesNeeded) + 1)) Then
                    numberLinesNeeded += 0.16 * (Int(numberLinesNeeded) + 1)
                End If
            Loop While numberLinesAvailable >= 1 And numberLinesNeeded <= Int(numberLinesAvailable)
            fontcount += -1
            SinBinButton.Font = New Font(SinBinButton.Font.Name, fontcount)
            
        Else
            SinBinButton.Location = New Point(column3CurrentGameXPosition, row5CurrentGameYPosition)
            SinBinButton.Font = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btnWhiteSinBin").Font
        End If
    End Sub
    Private Sub btnTeamName_Click(sender As System.Object, e As System.EventArgs)
        Dim Form2 As New TeamName
        Form2.Tag = CType(sender, Button).Tag
        Form2.Show()
    End Sub
    Private Sub btnRefereeName_Click(sender As System.Object, e As System.EventArgs)
        Dim RefereeName As New RefereeName
        RefereeName.Show()
    End Sub
    Private Sub btnChangeGoal_Click(sender As System.Object, e As System.EventArgs)
        Dim AddGoal As New AddGoal
        AddGoal.Tag = CType(sender, Button).Tag
        AddGoal.Show()
    End Sub
    Private Sub btnTeamTimeout_Click(sender As System.Object, e As System.EventArgs)
        Dim btnTeamTimeout As New Button
        btnTeamTimeout.Tag = CType(sender, Button).Tag
        If (Timetable(0).getGameState = "First Half" Or Timetable(0).getGameState = "Second Half") And RefereeTimeout = False And TeamTimeout = False Then
            If (btnTeamTimeout.Tag = "White" And WhiteTimeout = True Or btnTeamTimeout.Tag = "Black") And BlackTimeout = True Then
                If btnTeamTimeout.Tag = "White" Then
                    WhiteTimeout = False
                Else
                    BlackTimeout = False
                End If
                TeamTimeout = True
                addTimeToTimeTable(1, 0, 0)
                TimeoutTimeEdit = TimeOfDay
                TimeoutTimeEdit = TimeoutTimeEdit.AddMinutes(1)
                Dim tabCurrentGame = Me.Controls("TabControl1").Controls("tabCurrentGame")
                Dim lblGameTime As Label = tabCurrentGame.Controls("lblGameTime")
                Dim lblGameState As Label = tabCurrentGame.Controls("lblGameState")
                lblGameState.BackColor = Color.Red
                lblGameTime.BackColor = Color.Red
                If numberinSinBin > 0 Then
                    Dim tempDate As Date
                    For i As Integer = 1 To numberinSinBin
                        Dim lblSinBin As Label = tabCurrentGame.Controls("pnlSinBin").Controls("lblSinBinTime" & i)
                        tempDate = lblSinBin.Tag
                        tempDate = tempDate.AddMinutes(1)
                        lblSinBin.Tag = tempDate
                    Next

                End If
            End If

        End If

    End Sub
    Private Sub addTimeToTimeTable(timeToAddMins As Integer, timeToAddSeconds As Integer, iteration As Integer)
        Dim temp As Date
        If iteration = Timetable.Count Then

        Else
            If iteration = 0 Then
                temp = Timetable(iteration).getStartTime
                temp = temp.AddMinutes(timeToAddMins)
                temp = temp.AddSeconds(timeToAddSeconds)
                Timetable(iteration).setStartTime(temp)
            Else
                Timetable(iteration).setStartTime(Timetable(iteration - 1).getTimeMarker)
            End If
            Timetable(iteration).setTimeMarker()
            changeTimetableTimes(iteration + 1)
        End If
    End Sub
    Private Sub btnRefereeTimeout_Click(sender As System.Object, e As System.EventArgs)
        Dim tabCurrentGame = Me.Controls("TabControl1").Controls("tabCurrentGame")
        Dim lblGameTime As Label = tabCurrentGame.Controls("lblGameTime")
        Dim lblGameState As Label = tabCurrentGame.Controls("lblGameState")
        If (Timetable(0).getGameState = "First Half" Or Timetable(0).getGameState = "Second Half") And TeamTimeout = False Then
            If RefereeTimeout = False Then
                RefereeTimeout = True
                TimeoutTimeEdit = TimeOfDay
                lblGameState.BackColor = Color.Red
                lblGameTime.BackColor = Color.Red
            Else

                RefereeTimeout = False
                addTimeToTimeTable(DateDiff(DateInterval.Second, TimeoutTimeEdit, TimeOfDay) \ 60, DateDiff(DateInterval.Second, TimeoutTimeEdit, TimeOfDay) Mod 60, 0)
                lblGameState.BackColor = Color.White
                lblGameTime.BackColor = Color.White
                If numberinSinBin > 0 Then
                    For i As Integer = 1 To numberinSinBin
                        Dim lblSinBin As Label = tabCurrentGame.Controls("pnlSinBin").Controls("lblSinBinTime" & i)
                        Dim tempDate As Date
                        tempDate = lblSinBin.Tag
                        tempDate = tempDate.AddMinutes(DateDiff(DateInterval.Second, TimeoutTimeEdit, TimeOfDay) \ 60)
                        tempDate = tempDate.AddSeconds(DateDiff(DateInterval.Second, TimeoutTimeEdit, TimeOfDay) Mod 60)
                        lblSinBin.Tag = tempDate
                    Next

                End If
            End If
        End If
    End Sub
    Private Sub btnSinBin_Click(sender As System.Object, e As System.EventArgs)
        If Timetable.Count > 0 Then
            If Timetable(0).getGameState = "First Half" Or Timetable(0).getGameState = "Second Half" Then
                Dim SinBin As New SinBin
                SinBin.Tag = CType(sender, Button).Tag
                SinBin.Show()
            End If
        End If
    End Sub
    Private Sub createCurrentGameLabels()
        Dim colour As String
        createGameStatelabel()
        createGameTimeLabel()
        For i As Integer = 0 To 1
            If i = 0 Then
                colour = "White"
            Else
                colour = "Black"
            End If
            createTeamNamelabels(colour)
            createGoalScoredlabel(colour)
        Next
    End Sub
    Private Sub createTeamNamelabels(colour As String)
        Dim lblTeamName As New Label
        lblTeamName.Name = "lbl" & colour & "TeamName"
        lblTeamName.TextAlign = ContentAlignment.MiddleCenter
        If colour = "White" Then
            lblTeamName.Text = GameList(GameNumber).getTeamName(0)
        Else
            lblTeamName.Text = GameList(GameNumber).getTeamName(1)
        End If
        lblTeamName.Tag = colour
        Me.Controls("TabControl1").Controls("tabCurrentGame").Controls.Add(lblTeamName)
        lblTeamName.BackColor = Color.White
        sizeTeamNamelabels(colour)
        lblTeamName.BorderStyle = BorderStyle.FixedSingle
    End Sub
    Private Sub createGameStatelabel()
        Dim lblGameState As New Label
        lblGameState.Name = "lblGameState"
        lblGameState.Text = ""
        Me.Controls("TabControl1").Controls("tabCurrentGame").Controls.Add(lblGameState)
        lblGameState.BackColor = Color.White
        sizeGameStatelabel()
        lblGameState.BorderStyle = BorderStyle.FixedSingle
        lblGameState.TextAlign = ContentAlignment.MiddleCenter
    End Sub
    Private Sub createGoalScoredlabel(colour As String)
        Dim lblGoalScored As New Label
        lblGoalScored.Name = "lbl" & colour & "GoalScored"
        lblGoalScored.TextAlign = ContentAlignment.MiddleCenter
        lblGoalScored.Text = "00"
        lblGoalScored.Tag = colour
        Me.Controls("TabControl1").Controls("tabCurrentGame").Controls.Add(lblGoalScored)
        lblGoalScored.BackColor = Color.White
        sizeGoalScoredlabel(colour)
        lblGoalScored.BorderStyle = BorderStyle.FixedSingle
    End Sub
    Private Sub createGameTimeLabel()
        Dim lblGameTime As New Label
        lblGameTime.Name = "lblGameTime"
        lblGameTime.TextAlign = ContentAlignment.MiddleCenter
        lblGameTime.Text = "00:00"
        Me.Controls("TabControl1").Controls("tabcurrentGame").Controls.Add(lblGameTime)
        lblGameTime.BackColor = Color.White
        lblGameTime.BorderStyle = BorderStyle.FixedSingle
        sizeGameTimelabel()
    End Sub
    Private Sub sizeTeamNamelabels(colour As String)
        Dim teamName = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("lbl" & colour & "TeamName")
        Dim btnTeamName = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btn" & colour & "TeamName")
        teamName.Size = New Size(columnedgeCurrentGameWidth, labelrow1CurrentGameHeight)
        If colour = "White" Then
            teamName.Location = New Point(column1CurrentGameXPosition, row2CurrentGameYPosition)
        Else
            teamName.Location = New Point(column3CurrentGameXPosition, row2CurrentGameYPosition)
        End If
        teamName.Font = btnTeamName.Font
    End Sub
    Private Sub sizeGameStatelabel()
        Dim GameStateLabel = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("lblGameState")
        Dim btnRefereeName = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btnRefereeName")
        GameStateLabel.Size = New Size(columncentreCurrentGameWidth, labelrow1CurrentGameHeight)
        GameStateLabel.Location = New Point(column2CurrentGameXPosition, row2CurrentGameYPosition)
        GameStateLabel.Font = btnRefereeName.Font
    End Sub
    Private Sub sizeGoalScoredlabel(colour As String)
        Dim GoalScoredLabel = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("lbl" & colour & "GoalScored")
        Dim GameTimelabel As Label = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("lblGameTime")
        GoalScoredLabel.Size = New Size(columnedgeCurrentGameWidth, labelrow2CurrentGameHeight)
        If colour = "White" Then
            GoalScoredLabel.Location = New Point(column1CurrentGameXPosition, row3CurrentGameYPosition)
        Else
            GoalScoredLabel.Location = New Point(column3CurrentGameXPosition, row3CurrentGameYPosition)
        End If
        GoalScoredLabel.Font = GameTimelabel.Font
    End Sub
    Private Sub sizeGameTimelabel()
        Dim GameTimelabel As Label = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("lblGameTime")
        GameTimelabel.Size = New Size(columncentreCurrentGameWidth, labelrow2CurrentGameHeight)
        GameTimelabel.Location = New Point(column2CurrentGameXPosition, row3CurrentGameYPosition)
        Dim fontcount As Integer = 0
        Dim sizeOfString As New SizeF
        Dim g As Graphics = Me.CreateGraphics
        Dim fits As Boolean = True
        Dim numberLinesNeeded As Decimal
        Dim numberLinesAvailable As Decimal
        Dim lineSpacingPixel As Decimal
        Dim tempFontFamily As FontFamily
        Do
            fontcount += 1
            GameTimelabel.Font = New Font(GameTimelabel.Font.Name, fontcount)
            tempFontFamily = GameTimelabel.Font.FontFamily
            sizeOfString = g.MeasureString(GameTimelabel.Text, GameTimelabel.Font)
            lineSpacingPixel = (GameTimelabel.Font.Size * tempFontFamily.GetLineSpacing(FontStyle.Regular)) / (tempFontFamily.GetEmHeight(FontStyle.Regular))
            numberLinesNeeded = sizeOfString.Width / (GameTimelabel.Width - 6)
            numberLinesAvailable = ((GameTimelabel.Height - 6 + Int(lineSpacingPixel)) / (sizeOfString.Height + Int(lineSpacingPixel)))
            If numberLinesNeeded - Int(numberLinesNeeded) >= 1 - (0.15 * (Int(numberLinesNeeded) + 1)) Then
                numberLinesNeeded += 0.16 * (Int(numberLinesNeeded) + 1)
            End If
        Loop While numberLinesAvailable >= 1 And numberLinesNeeded <= Int(numberLinesAvailable)
        fontcount += -1
        GameTimelabel.Font = New Font(GameTimelabel.Font.Name, fontcount)
    End Sub
    Private Sub createCurrentGamePanels()
        createSinBinPanel()
        createGoalScorersPanel()
    End Sub
    Private Sub createSinBinPanel()
        Dim pnlSinBin As New Panel
        pnlSinBin.Name = "pnlSinBin"
        pnlSinBin.BackColor = Color.FromArgb(128, 255, 255)
        Me.Controls("TabControl1").Controls("tabCurrentGame").Controls.Add(pnlSinBin)
        sizeSinBinPanel()
        createpnlSinBinTitleLabel()
    End Sub
    Private Sub sizeSinBinPanel()
        Dim pnlSinBin = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlSinBin")
        pnlSinBin.Size = New Size(endcolumnCurrentGameWidth, panelCurrentGameHeight)
        pnlSinBin.Location = New Point(column4CurrentGameXPosition, row1CurrentGameYPosition)
    End Sub
    Private Sub createpnlSinBinTitleLabel()
        Dim pnlSinBin = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlSinBin")
        Dim pnlSinBinTitleLabel As New Label
        pnlSinBinTitleLabel.Name = "pnlSinBinTitleLabel"
        pnlSinBinTitleLabel.Text = "Sin Bins"
        pnlSinBinTitleLabel.BackColor = Color.White
        pnlSinBinTitleLabel.TextAlign = ContentAlignment.MiddleCenter
        pnlSinBinTitleLabel.BorderStyle = BorderStyle.FixedSingle
        pnlSinBin.Controls.Add(pnlSinBinTitleLabel)
        sizepnlSinBinTitleLabel()
    End Sub
    Private Sub sizepnlSinBinTitleLabel()
        Dim pnlSinBinLabel As Label = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlSinBin").Controls("pnlSinBInTitleLabel")
        pnlSinBinLabel.Size = New Size(endcolumnCurrentGameWidth - 20, panelRowHeight)
        pnlSinBinLabel.Location = New Point(10, 5)
        Dim fontcount As Integer = 0
        Dim sizeOfString As New SizeF
        Dim g As Graphics = Me.CreateGraphics
        Dim fits As Boolean = True
        Dim numberLinesNeeded As Decimal
        Dim numberLinesAvailable As Decimal
        Dim lineSpacingPixel As Decimal
        Dim tempFontFamily As FontFamily
        Do
            fontcount += 1
            pnlSinBinLabel.Font = New Font(pnlSinBinLabel.Font.Name, fontcount)
            tempFontFamily = pnlSinBinLabel.Font.FontFamily
            sizeOfString = g.MeasureString(pnlSinBinLabel.Text, pnlSinBinLabel.Font)
            lineSpacingPixel = (pnlSinBinLabel.Font.Size * tempFontFamily.GetLineSpacing(FontStyle.Regular)) / (tempFontFamily.GetEmHeight(FontStyle.Regular))
            numberLinesNeeded = sizeOfString.Width / (pnlSinBinLabel.Width - 6)
            numberLinesAvailable = ((pnlSinBinLabel.Height - 6 + Int(lineSpacingPixel)) / (sizeOfString.Height + Int(lineSpacingPixel)))
            If numberLinesNeeded - Int(numberLinesNeeded) >= 1 - (0.15 * (Int(numberLinesNeeded) + 1)) Then
                numberLinesNeeded += 0.16 * (Int(numberLinesNeeded) + 1)
            End If
        Loop While numberLinesAvailable >= 1 And numberLinesNeeded <= Int(numberLinesAvailable)
        fontcount += -1
        pnlSinBinLabel.Font = New Font(pnlSinBinLabel.Font.Name, fontcount)
    End Sub
    Private Sub createGoalScorersPanel()
        Dim colour As String = "White"
        Dim pnlGoalScorers As New Panel
        pnlGoalScorers.Name = "pnlGoalScorers"
        pnlGoalScorers.BackColor = Color.FromArgb(128, 255, 255)
        Me.Controls("TabControl1").Controls("tabCurrentGame").Controls.Add(pnlGoalScorers)
        sizeGoalScorersPanel()
        createpnlGoalScorersTitleLabel()
        For i As Integer = 0 To 1
            createpnlGoalScorersSubTitlesLabels(colour)
            colour = "Black"
        Next
    End Sub
    Private Sub sizeGoalScorersPanel()
        Dim pnlGoalScorers = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlGoalScorers")
        pnlGoalScorers.Size = New Size(endcolumnCurrentGameWidth, panelCurrentGameHeight)
        pnlGoalScorers.Location = New Point(column4CurrentGameXPosition, panelrow2CurrentGameYPosition)
    End Sub
    Private Sub createpnlGoalScorersTitleLabel()
        Dim pnlGoalScorers = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlGoalScorers")
        Dim pnlGoalScorersTitleLabel As New Label
        pnlGoalScorersTitleLabel.Name = "pnlGoalScorersTitleLabel"
        pnlGoalScorersTitleLabel.Text = "Goal Scorers"
        pnlGoalScorersTitleLabel.BackColor = Color.White
        pnlGoalScorersTitleLabel.TextAlign = ContentAlignment.MiddleCenter
        pnlGoalScorersTitleLabel.BorderStyle = BorderStyle.FixedSingle
        pnlGoalScorers.Controls.Add(pnlGoalScorersTitleLabel)
        sizepnlGoalScorersTitleLabel()
    End Sub
    Private Sub sizepnlGoalScorersTitleLabel()
        Dim pnlGoalScorersLabel = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlGoalScorers").Controls("pnlGoalScorersTitleLabel")
        pnlGoalScorersLabel.Size = New Size(endcolumnCurrentGameWidth - 20, panelRowHeight)
        pnlGoalScorersLabel.Location = New Point(10, 5)
        Dim pnlSinBinLabel As Label = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlSinBin").Controls("pnlSinBInTitleLabel")
        pnlGoalScorersLabel.Font = pnlSinBinLabel.Font
    End Sub
    Private Sub createpnlGoalScorersSubTitlesLabels(colour As String)
        Dim pnlGoalScorers = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlGoalScorers")
        Dim pnlGoalScorersSubTitleLabel As New Label
        pnlGoalScorersSubTitleLabel.Name = "pnlGoalScorers" & colour & "SubTitleLabel"
        pnlGoalScorersSubTitleLabel.Text = colour
        pnlGoalScorersSubTitleLabel.BackColor = Color.White
        pnlGoalScorersSubTitleLabel.TextAlign = ContentAlignment.MiddleCenter
        pnlGoalScorersSubTitleLabel.BorderStyle = BorderStyle.FixedSingle
        pnlGoalScorers.Controls.Add(pnlGoalScorersSubTitleLabel)
        sizepnlGoalScorersSubTitlesLabels(colour)
    End Sub
    Private Sub sizepnlGoalScorersSubTitlesLabels(colour As String)
        Dim pnlGoalScorersLabel = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlGoalScorers").Controls("pnlGoalScorers" & colour & "SubTitleLabel")
        pnlGoalScorersLabel.Size = New Size(halfPanelWidth, panelRowHeight)
        If colour = "White" Then
            pnlGoalScorersLabel.Location = New Point(column1CurrentGameXPosition, 10 + panelRowHeight)
        Else
            pnlGoalScorersLabel.Location = New Point(column1CurrentGameXPosition + 10 + halfPanelWidth, 10 + panelRowHeight)
        End If
        Dim pnlSinBinLabel As Label = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlSinBin").Controls("pnlSinBInTitleLabel")
        pnlGoalScorersLabel.Font = pnlSinBinLabel.Font
    End Sub
    Private Sub setupGameSetup()
        createNumberBreakLabel()
        createNumberBreakTextBox()
        createNumberBreakButton()
        createGameSetupPanel()
    End Sub
    Private Sub createNumberBreakLabel()
        Dim lblNumberBreak As New Label
        lblNumberBreak.Name = "lblNumberBreak"
        lblNumberBreak.Text = "Enter the number of breaks"
        lblNumberBreak.TextAlign = ContentAlignment.MiddleCenter
        lblNumberBreak.BackColor = Color.White
        lblNumberBreak.BorderStyle = BorderStyle.FixedSingle
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(lblNumberBreak)
        sizelblNumberBreak()
    End Sub
    Private Sub createNumberBreakTextBox()
        Dim txtNumberBreak As New RichTextBox
        txtNumberBreak.Name = "txtNumberBreak"
        txtNumberBreak.Text = ""
        txtNumberBreak.BackColor = Color.White
        txtNumberBreak.BorderStyle = BorderStyle.FixedSingle
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(txtNumberBreak)
        sizetxtNumberBreak()
    End Sub
    Private Sub createNumberBreakButton()
        Dim btnNumberBreak As New Button
        btnNumberBreak.Name = "btnNumberBreak"
        btnNumberBreak.Text = "Enter"
        btnNumberBreak.BackColor = Color.Gainsboro
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(btnNumberBreak)
        sizebtnNumberBreak()
        AddHandler btnNumberBreak.Click, AddressOf Me.btnNumberBreak_Click
    End Sub
    Private Sub sizelblNumberBreak()
        Dim lblNumberBreak = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("lblNumberBreak")
        lblNumberBreak.Size = New Size(columnedgeCurrentGameWidth, buttonrowCurrentGameHeight)
        lblNumberBreak.Location = New Point(column1CurrentGameXPosition, row1CurrentGameYPosition)
        Dim fontcount As Integer = 0
        Dim sizeOfString As New SizeF
        Dim g As Graphics = Me.CreateGraphics
        Dim fits As Boolean = True
        Dim numberLinesNeeded As Decimal
        Dim numberLinesAvailable As Decimal
        Dim lineSpacingPixel As Decimal
        Dim tempFontFamily As FontFamily
        Do
            fontcount += 1
            lblNumberBreak.Font = New Font(lblNumberBreak.Font.Name, fontcount)
            tempFontFamily = lblNumberBreak.Font.FontFamily
            sizeOfString = g.MeasureString(lblNumberBreak.Text, lblNumberBreak.Font)
            lineSpacingPixel = (lblNumberBreak.Font.Size * tempFontFamily.GetLineSpacing(FontStyle.Regular)) / (tempFontFamily.GetEmHeight(FontStyle.Regular))
            numberLinesNeeded = sizeOfString.Width / (lblNumberBreak.Width - 6)
            numberLinesAvailable = ((lblNumberBreak.Height - 6 + Int(lineSpacingPixel)) / (sizeOfString.Height + Int(lineSpacingPixel)))
            If numberLinesNeeded - Int(numberLinesNeeded) >= 1 - (0.15 * (Int(numberLinesNeeded) + 1)) Then
                numberLinesNeeded += 0.16 * (Int(numberLinesNeeded) + 1)
            End If
        Loop While numberLinesAvailable >= 1 And numberLinesNeeded <= Int(numberLinesAvailable)
        fontcount += -1
        lblNumberBreak.Font = New Font(lblNumberBreak.Font.Name, fontcount)
    End Sub
    Private Sub sizetxtNumberBreak()
        Dim fontcount As Integer = 0
        Dim sizeOfString As New SizeF
        Dim g As Graphics = Me.CreateGraphics
        Dim txtNumberBreak As RichTextBox = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("txtNumberBreak")
        txtNumberBreak.Size = New Size(doublecolumncentreCurrentGameWidth - 5, buttonrowCurrentGameHeight - 2)
        txtNumberBreak.Location = New Point(column2CurrentGameXPosition, row1CurrentGameYPosition + 1)
        Do
            fontcount += 1
            txtNumberBreak.Font = New Font(txtNumberBreak.Font.Name, fontcount)
            sizeOfString = g.MeasureString(" ", txtNumberBreak.Font)
        Loop Until sizeOfString.Height > txtNumberBreak.Height
        fontcount += -2
        txtNumberBreak.Font = New Font(txtNumberBreak.Font.Name, fontcount)
    End Sub
    Private Sub sizebtnNumberBreak()
        Dim btnNumberBreak = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("btnNumberBreak")
        btnNumberBreak.Size = New Size(doublecolumncentreCurrentGameWidth - 5, buttonrowCurrentGameHeight)
        btnNumberBreak.Location = New Point(column25CurrentGameXPosition + 5, row1CurrentGameYPosition)
        Dim fontcount As Integer = 0
        Dim sizeOfString As New SizeF
        Dim g As Graphics = Me.CreateGraphics
        Dim fits As Boolean = True
        Dim numberLinesNeeded As Decimal
        Dim numberLinesAvailable As Decimal
        Dim lineSpacingPixel As Decimal
        Dim tempFontFamily As FontFamily
        Do
            fontcount += 1
            btnNumberBreak.Font = New Font(btnNumberBreak.Font.Name, fontcount)
            tempFontFamily = btnNumberBreak.Font.FontFamily
            sizeOfString = g.MeasureString(btnNumberBreak.Text, btnNumberBreak.Font)
            lineSpacingPixel = (btnNumberBreak.Font.Size * tempFontFamily.GetLineSpacing(FontStyle.Regular)) / (tempFontFamily.GetEmHeight(FontStyle.Regular))
            numberLinesNeeded = sizeOfString.Width / (btnNumberBreak.Width - 6)
            numberLinesAvailable = ((btnNumberBreak.Height - 6 + Int(lineSpacingPixel)) / (sizeOfString.Height + Int(lineSpacingPixel)))
            If numberLinesNeeded - Int(numberLinesNeeded) >= 1 - (0.15 * (Int(numberLinesNeeded) + 1)) Then
                numberLinesNeeded += 0.16 * (Int(numberLinesNeeded) + 1)
            End If
        Loop While numberLinesAvailable >= 1 And numberLinesNeeded <= Int(numberLinesAvailable)
        fontcount += -1
        btnNumberBreak.Font = New Font(btnNumberBreak.Font.Name, fontcount)
    End Sub
    Private Sub btnNumberBreak_Click(sender As System.Object, e As System.EventArgs)
        Dim tabGameSetup As TabPage = Me.Controls("TabControl1").Controls("tabGameSetup")
        Dim numberBreakTextBox As RichTextBox = tabGameSetup.Controls("txtNumberBreak")
        Dim con As Control
        If regex.IsMatch(numberBreakTextBox.Text, "^\d+$") Then
            If numberBreakTextBox.Text > -1 Then
                numberOfBreaks = numberBreakTextBox.Text
                For controlIndex As Integer = tabGameSetup.Controls.Count - 1 To 0 Step -1
                    con = tabGameSetup.Controls(controlIndex)
                    tabGameSetup.Controls.Remove(con)
                Next
                setupGameSetup()
                createGameSetupDataEntry()
                If numberOfBreaks > 0 Then
                    createEnterBreakLengthLabel()
                    createEnterBreakLengthTextBox()
                End If
            End If
        End If
    End Sub
    Private Sub createGameSetupPanel()
        Dim pnlGameSetup As New Panel
        pnlGameSetup.Name = "pnlGameSetup"
        pnlGameSetup.BackColor = Color.FromArgb(128, 255, 255)
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(pnlGameSetup)
        sizeGameSetupPanel()
    End Sub
    Private Sub sizeGameSetupPanel()
        Dim pnlGameSetup = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("pnlGameSetup")
        pnlGameSetup.Size = New Size(columnedgeCurrentGameWidth + endcolumnCurrentGameWidth + 10, panelCurrentGameHeight * 2)
        pnlGameSetup.Location = New Point(column3CurrentGameXPosition, row1CurrentGameYPosition)
    End Sub
    Private Sub createGameSetupDataEntry()
        createNumberOfGameLabel()
        createNumberOfGameTextBox()
        createEnterHalfLengthLabel()
        createEnterHalfLengthTextBox()
        createEnterHalfTimeLengthLabel()
        createEnterHalfTimeLengthTextBox()
        createEnterWarmUpLengthLabel()
        createEnterWarmUpLengthTextBox()
        createEnterDataButton()
    End Sub
    Private Sub createNumberOfGameLabel()
        Dim lblNumberOfGame As New Label
        lblNumberOfGame.Name = "lblNumberOfGame"
        lblNumberOfGame.Text = "Enter the number of Games in section 1"
        lblNumberOfGame.TextAlign = ContentAlignment.MiddleCenter
        lblNumberOfGame.BackColor = Color.White
        lblNumberOfGame.BorderStyle = BorderStyle.FixedSingle
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(lblNumberOfGame)
        sizelblNumberOfGame()
    End Sub
    Private Sub createNumberOfGameTextBox()
        Dim txtNumberOfGame As New RichTextBox
        txtNumberOfGame.Name = "txtNumberOfGame"
        txtNumberOfGame.Text = ""
        txtNumberOfGame.BackColor = Color.White
        txtNumberOfGame.BorderStyle = BorderStyle.FixedSingle
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(txtNumberOfGame)
        sizetxtNumberOfGame()
    End Sub
    Private Sub sizelblNumberOfGame()
        Dim lblNumberOfGame = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("lblNumberOfGame")
        lblNumberOfGame.Size = New Size(columnedgeCurrentGameWidth, buttonrowCurrentGameHeight)
        lblNumberOfGame.Location = New Point(column1CurrentGameXPosition, row2CurrentGameYPosition)
        Dim lblNumberBreak = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("lblNumberBreak")
        lblNumberOfGame.Font = lblNumberBreak.Font
    End Sub
    Private Sub sizetxtNumberOfGame()
        Dim fontcount As Integer = 0
        Dim sizeOfString As New SizeF
        Dim g As Graphics = Me.CreateGraphics
        Dim txtNumberOfGame As RichTextBox = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("txtNumberOfGame")
        txtNumberOfGame.Size = New Size(columncentreCurrentGameWidth, buttonrowCurrentGameHeight - 2)
        txtNumberOfGame.Location = New Point(column2CurrentGameXPosition, row2CurrentGameYPosition)
        Do
            fontcount += 1
            txtNumberOfGame.Font = New Font(txtNumberOfGame.Font.Name, fontcount)
            sizeOfString = g.MeasureString(" ", txtNumberOfGame.Font)
        Loop Until sizeOfString.Height > txtNumberOfGame.Height
        fontcount += -2
        txtNumberOfGame.Font = New Font(txtNumberOfGame.Font.Name, fontcount)
    End Sub
    Private Sub createEnterHalfLengthLabel()
        Dim lblEnterHalfLength As New Label
        lblEnterHalfLength.Name = "lblEnterHalfLength"
        lblEnterHalfLength.Text = "Enter half length in section 1"
        lblEnterHalfLength.TextAlign = ContentAlignment.MiddleCenter
        lblEnterHalfLength.BackColor = Color.White
        lblEnterHalfLength.BorderStyle = BorderStyle.FixedSingle
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(lblEnterHalfLength)
        sizelblEnterHalfLength()
    End Sub
    Private Sub createEnterHalfLengthTextBox()
        Dim txtEnterHalfLength As New RichTextBox
        txtEnterHalfLength.Name = "txtEnterHalfLength"
        txtEnterHalfLength.Text = ""
        txtEnterHalfLength.BackColor = Color.White
        txtEnterHalfLength.BorderStyle = BorderStyle.FixedSingle
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(txtEnterHalfLength)
        sizetxtEnterHalfLength()
    End Sub
    Private Sub sizelblEnterHalfLength()
        Dim lblEnterHalfLength = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("lblEnterHalfLength")
        lblEnterHalfLength.Size = New Size(columnedgeCurrentGameWidth, buttonrowCurrentGameHeight)
        lblEnterHalfLength.Location = New Point(column1CurrentGameXPosition, row2CurrentGameYPosition + (10 + buttonrowCurrentGameHeight))
        Dim lblNumberBreak = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("lblNumberBreak")
        lblEnterHalfLength.Font = lblNumberBreak.Font
    End Sub
    Private Sub sizetxtEnterHalfLength()
        Dim fontcount As Integer = 0
        Dim sizeOfString As New SizeF
        Dim g As Graphics = Me.CreateGraphics
        Dim txtEnterHalfLength As RichTextBox = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("txtEnterHalfLength")
        txtEnterHalfLength.Size = New Size(columncentreCurrentGameWidth, buttonrowCurrentGameHeight - 2)
        txtEnterHalfLength.Location = New Point(column2CurrentGameXPosition, row2CurrentGameYPosition + (10 + buttonrowCurrentGameHeight) + 1)
        Do
            fontcount += 1
            txtEnterHalfLength.Font = New Font(txtEnterHalfLength.Font.Name, fontcount)
            sizeOfString = g.MeasureString(" ", txtEnterHalfLength.Font)
        Loop Until sizeOfString.Height > txtEnterHalfLength.Height
        fontcount += -2
        txtEnterHalfLength.Font = New Font(txtEnterHalfLength.Font.Name, fontcount)
    End Sub
    Private Sub createEnterHalfTimeLengthLabel()
        Dim lblEnterHalfTimeLength As New Label
        lblEnterHalfTimeLength.Name = "lblEnterHalfTimeLength"
        lblEnterHalfTimeLength.Text = "Enter half time length in section 1"
        lblEnterHalfTimeLength.TextAlign = ContentAlignment.MiddleCenter
        lblEnterHalfTimeLength.BackColor = Color.White
        lblEnterHalfTimeLength.BorderStyle = BorderStyle.FixedSingle
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(lblEnterHalfTimeLength)
        sizelblEnterHalfTimeLength()
    End Sub
    Private Sub createEnterHalfTimeLengthTextBox()
        Dim txtEnterHalfTimeLength As New RichTextBox
        txtEnterHalfTimeLength.Name = "txtEnterHalfTimeLength"
        txtEnterHalfTimeLength.Text = ""
        txtEnterHalfTimeLength.BackColor = Color.White
        txtEnterHalfTimeLength.BorderStyle = BorderStyle.FixedSingle
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(txtEnterHalfTimeLength)
        sizetxtEnterHalfTimeLength()
    End Sub
    Private Sub sizelblEnterHalfTimeLength()
        Dim lblEnterHalfTimeLength = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("lblEnterHalfTimeLength")
        lblEnterHalfTimeLength.Size = New Size(columnedgeCurrentGameWidth, buttonrowCurrentGameHeight)
        lblEnterHalfTimeLength.Location = New Point(column1CurrentGameXPosition, row2CurrentGameYPosition + 2 * (10 + buttonrowCurrentGameHeight))
        Dim lblNumberBreak = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("lblNumberBreak")
        lblEnterHalfTimeLength.Font = lblNumberBreak.Font
    End Sub
    Private Sub sizetxtEnterHalfTimeLength()
        Dim fontcount As Integer = 0
        Dim sizeOfString As New SizeF
        Dim g As Graphics = Me.CreateGraphics
        Dim txtEnterHalfTimeLength As RichTextBox = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("txtEnterHalfTimeLength")
        txtEnterHalfTimeLength.Size = New Size(columncentreCurrentGameWidth, buttonrowCurrentGameHeight - 2)
        txtEnterHalfTimeLength.Location = New Point(column2CurrentGameXPosition, row2CurrentGameYPosition + 2 * (10 + buttonrowCurrentGameHeight) + 1)
        Do
            fontcount += 1
            txtEnterHalfTimeLength.Font = New Font(txtEnterHalfTimeLength.Font.Name, fontcount)
            sizeOfString = g.MeasureString(" ", txtEnterHalfTimeLength.Font)
        Loop Until sizeOfString.Height > txtEnterHalfTimeLength.Height
        fontcount += -2
        txtEnterHalfTimeLength.Font = New Font(txtEnterHalfTimeLength.Font.Name, fontcount)
    End Sub
    Private Sub createEnterWarmUpLengthLabel()
        Dim lblEnterWarmUpLength As New Label
        lblEnterWarmUpLength.Name = "lblEnterWarmUpLength"
        lblEnterWarmUpLength.Text = "Enter warm up length in section 1"
        lblEnterWarmUpLength.TextAlign = ContentAlignment.MiddleCenter
        lblEnterWarmUpLength.BackColor = Color.White
        lblEnterWarmUpLength.BorderStyle = BorderStyle.FixedSingle
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(lblEnterWarmUpLength)
        sizelblEnterWarmUpLength()
    End Sub
    Private Sub createEnterWarmUpLengthTextBox()
        Dim txtEnterWarmUpLength As New RichTextBox
        txtEnterWarmUpLength.Name = "txtEnterWarmUpLength"
        txtEnterWarmUpLength.Text = ""
        txtEnterWarmUpLength.BackColor = Color.White
        txtEnterWarmUpLength.BorderStyle = BorderStyle.FixedSingle
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(txtEnterWarmUpLength)
        sizetxtEnterWarmUpLength()
    End Sub
    Private Sub sizelblEnterWarmUpLength()
        Dim lblEnterWarmUpLength = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("lblEnterWarmUpLength")
        lblEnterWarmUpLength.Size = New Size(columnedgeCurrentGameWidth, buttonrowCurrentGameHeight)
        lblEnterWarmUpLength.Location = New Point(column1CurrentGameXPosition, row2CurrentGameYPosition + 3 * (10 + buttonrowCurrentGameHeight))
        Dim lblNumberBreak = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("lblNumberBreak")
        lblEnterWarmUpLength.Font = lblNumberBreak.Font
    End Sub
    Private Sub sizetxtEnterWarmUpLength()
        Dim fontcount As Integer = 0
        Dim sizeOfString As New SizeF
        Dim g As Graphics = Me.CreateGraphics
        Dim txtEnterWarmUpLength As RichTextBox = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("txtEnterWarmUpLength")
        txtEnterWarmUpLength.Size = New Size(columncentreCurrentGameWidth, buttonrowCurrentGameHeight - 2)
        txtEnterWarmUpLength.Location = New Point(column2CurrentGameXPosition, row2CurrentGameYPosition + 3 * (10 + buttonrowCurrentGameHeight) + 1)
        Do
            fontcount += 1
            txtEnterWarmUpLength.Font = New Font(txtEnterWarmUpLength.Font.Name, fontcount)
            sizeOfString = g.MeasureString(" ", txtEnterWarmUpLength.Font)
        Loop Until sizeOfString.Height > txtEnterWarmUpLength.Height
        fontcount += -2
        txtEnterWarmUpLength.Font = New Font(txtEnterWarmUpLength.Font.Name, fontcount)
    End Sub
    Private Sub createEnterBreakLengthLabel()
        Dim lblEnterBreakLength As New Label
        lblEnterBreakLength.Name = "lblEnterBreakLength"
        lblEnterBreakLength.Text = "Enter break length after section 1"
        lblEnterBreakLength.TextAlign = ContentAlignment.MiddleCenter
        lblEnterBreakLength.BackColor = Color.White
        lblEnterBreakLength.BorderStyle = BorderStyle.FixedSingle
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(lblEnterBreakLength)
        sizelblEnterBreakLength()
    End Sub
    Private Sub createEnterBreakLengthTextBox()
        Dim txtEnterBreakLength As New RichTextBox
        txtEnterBreakLength.Name = "txtEnterBreakLength"
        txtEnterBreakLength.Text = ""
        txtEnterBreakLength.BackColor = Color.White
        txtEnterBreakLength.BorderStyle = BorderStyle.FixedSingle
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(txtEnterBreakLength)
        sizetxtEnterBreakLength()
    End Sub
    Private Sub sizelblEnterBreakLength()
        Dim lblEnterBreakLength = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("lblEnterBreakLength")
        lblEnterBreakLength.Size = New Size(columnedgeCurrentGameWidth, buttonrowCurrentGameHeight)
        lblEnterBreakLength.Location = New Point(column1CurrentGameXPosition, row2CurrentGameYPosition + 4 * (10 + buttonrowCurrentGameHeight))
        Dim lblNumberBreak = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("lblNumberBreak")
        lblEnterBreakLength.Font = lblNumberBreak.Font
    End Sub
    Private Sub sizetxtEnterBreakLength()
        Dim fontcount As Integer = 0
        Dim sizeOfString As New SizeF
        Dim g As Graphics = Me.CreateGraphics
        Dim txtEnterBreakLength As RichTextBox = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("txtEnterBreakLength")
        txtEnterBreakLength.Size = New Size(columncentreCurrentGameWidth, buttonrowCurrentGameHeight - 2)
        txtEnterBreakLength.Location = New Point(column2CurrentGameXPosition, row2CurrentGameYPosition + 4 * (10 + buttonrowCurrentGameHeight) + 1)
        Do
            fontcount += 1
            txtEnterBreakLength.Font = New Font(txtEnterBreakLength.Font.Name, fontcount)
            sizeOfString = g.MeasureString(" ", txtEnterBreakLength.Font)
        Loop Until sizeOfString.Height > txtEnterBreakLength.Height
        fontcount += -2
        txtEnterBreakLength.Font = New Font(txtEnterBreakLength.Font.Name, fontcount)
    End Sub
    Private Sub createEnterDataButton()
        Dim btnEnterData As New Button
        btnEnterData.Name = "btnEnterData"
        btnEnterData.Text = "Enter section data"
        btnEnterData.BackColor = Color.Gainsboro
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(btnEnterData)
        sizebtnEnterData()
        AddHandler btnEnterData.Click, AddressOf Me.btnEnterData_Click
    End Sub
    Private Sub sizebtnEnterData()
        Dim btnEnterData = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("btnEnterData")
        btnEnterData.Size = New Size(columnedgeCurrentGameWidth, buttonrowCurrentGameHeight)
        btnEnterData.Location = New Point(column1CurrentGameXPosition, row2CurrentGameYPosition + 5 * (10 + buttonrowCurrentGameHeight))
        Dim fontcount As Integer = 0
        Dim sizeOfString As New SizeF
        Dim g As Graphics = Me.CreateGraphics
        Dim fits As Boolean = True
        Dim numberLinesNeeded As Decimal
        Dim numberLinesAvailable As Decimal
        Dim lineSpacingPixel As Decimal
        Dim tempFontFamily As FontFamily
        Do
            fontcount += 1
            btnEnterData.Font = New Font(btnEnterData.Font.Name, fontcount)
            tempFontFamily = btnEnterData.Font.FontFamily
            sizeOfString = g.MeasureString(btnEnterData.Text, btnEnterData.Font)
            lineSpacingPixel = (btnEnterData.Font.Size * tempFontFamily.GetLineSpacing(FontStyle.Regular)) / (tempFontFamily.GetEmHeight(FontStyle.Regular))
            numberLinesNeeded = sizeOfString.Width / (btnEnterData.Width - 6)
            numberLinesAvailable = ((btnEnterData.Height - 6 + Int(lineSpacingPixel)) / (sizeOfString.Height + Int(lineSpacingPixel)))
            If numberLinesNeeded - Int(numberLinesNeeded) >= 1 - (0.15 * (Int(numberLinesNeeded) + 1)) Then
                numberLinesNeeded += 0.16 * (Int(numberLinesNeeded) + 1)
            End If
        Loop While numberLinesAvailable >= 1 And numberLinesNeeded <= Int(numberLinesAvailable)
        fontcount += -1
        btnEnterData.Font = New Font(btnEnterData.Font.Name, fontcount)
    End Sub
    Private Sub btnEnterData_Click(sender As System.Object, e As System.EventArgs)
        Dim tabGameSetup = Me.Controls("tabcontrol1").Controls("tabGameSetup")
        Dim updated = False
        If numberSetup = numberOfBreaks + 1 Then

        Else
            Dim NumberOfGame As RichTextBox = tabGameSetup.Controls("txtNumberOfGame")
            Dim HalfLength As RichTextBox = tabGameSetup.Controls("txtEnterHalfLength")
            Dim HalfTimeLength As RichTextBox = tabGameSetup.Controls("txtEnterHalfTimeLength")
            Dim WarmUpLength As RichTextBox = tabGameSetup.Controls("txtEnterWarmUpLength")
            If regex.IsMatch(HalfLength.Text) And regex.IsMatch(HalfTimeLength.Text) And regex.IsMatch(WarmUpLength.Text) And regex.IsMatch(NumberOfGame.Text, "^\d+$") Then
                If NumberOfGame.Text > 0 And HalfLength.Text > 0 And WarmUpLength.Text > 0 Then

                    If numberOfBreaks - numberSetup > 0 Then
                        Dim BreakLength As RichTextBox = tabGameSetup.Controls("txtEnterBreakLength")
                        If regex.IsMatch(BreakLength.Text) Then
                            creategametimetable(NumberOfGame.Text, HalfLength.Text, HalfTimeLength.Text, WarmUpLength.Text, BreakLength.Text)
                            numberSetup += 1
                            updated = True
                        Else

                        End If
                        BreakLength.Text = ""
                    Else
                        creategametimetable(NumberOfGame.Text, HalfLength.Text, HalfTimeLength.Text, WarmUpLength.Text, 0)
                        numberSetup += 1
                        updated = True
                    End If
                End If
            End If
            NumberOfGame.Text = ""
            HalfLength.Text = ""
            HalfTimeLength.Text = ""
            WarmUpLength.Text = ""
            If numberOfBreaks - numberSetup > 0 Then
                Dim BreakLength As RichTextBox = tabGameSetup.Controls("txtEnterBreakLength")
                BreakLength.Text = ""
            End If
        End If
        If updated = True Then
            Dim con As Control
            For controlIndex As Integer = tabGameSetup.Controls("pnlGameSetup").Controls.Count - 1 To 0 Step -1
                con = tabGameSetup.Controls("pnlGameSetup").Controls(controlIndex)
                tabGameSetup.Controls("pnlGameSetup").Controls.Remove(con)
            Next
            For i As Integer = 0 To Timetable.Count - 1

                createpnlLabels(i)
            Next
        End If
        If updated = True And numberSetup < numberOfBreaks + 1 Then
            changeGameSetupText()
        ElseIf updated = True And numberSetup = numberOfBreaks + 1 Then
            createStartTimetableButton()
        End If
        If numberOfBreaks = numberSetup And numberOfBreaks > 0 Then
            Dim tempCon As Control = tabGameSetup.Controls("txtEnterBreakLength")
            tabGameSetup.Controls.Remove(tempCon)
            tempCon = tabGameSetup.Controls("lblEnterBreakLength")
            tabGameSetup.Controls.Remove(tempCon)
        End If
    End Sub
    Private Sub changeGameSetupText()
        Dim tabGameSetup = Me.Controls("tabcontrol1").Controls("tabGameSetup")
        Dim lblNumberOfGame As Label = tabGameSetup.Controls("lblNumberOfGame")
        Dim lblHalfLength As Label = tabGameSetup.Controls("lblEnterHalfLength")
        Dim lblHalfTimeLength As Label = tabGameSetup.Controls("lblEnterHalfTimeLength")
        Dim lblWarmUpLength As Label = tabGameSetup.Controls("lblEnterWarmUpLength")
        Dim lblBreakLength As Label = tabGameSetup.Controls("lblEnterBreakLength")
        Dim iteration As Integer
        iteration = lblNumberOfGame.Text.Substring(lblNumberOfGame.Text.Length - 1, 1)
        iteration += 1
        lblNumberOfGame.Text = "Enter the number of Games in section " & iteration
        lblHalfLength.Text = "Enter the half length in section " & iteration
        lblHalfTimeLength.Text = "Enter the half time length in section " & iteration
        lblWarmUpLength.Text = "Enter the warm up length in section " & iteration
        lblBreakLength.Text = "Enter break length after section " & iteration

    End Sub
    Private Sub createStartTimetableButton()
        Dim btnStartTimetable As New Button
        btnStartTimetable.Name = "btnStartTimetable"
        btnStartTimetable.Text = "Start Game Timteable"
        btnStartTimetable.BackColor = Color.Gainsboro
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls.Add(btnStartTimetable)
        sizebtnStartTimetable()
        AddHandler btnStartTimetable.Click, AddressOf Me.btnStartTimetable_Click
    End Sub
    Private Sub sizebtnStartTimetable()
        Dim btnStartTimetable = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("btnStartTimetable")
        btnStartTimetable.Size = New Size(columncentreCurrentGameWidth, buttonrowCurrentGameHeight)
        btnStartTimetable.Location = New Point(column2CurrentGameXPosition, row2CurrentGameYPosition + 5 * (10 + buttonrowCurrentGameHeight))
        Dim btnEnterData = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("btnEnterData")
        btnStartTimetable.Font = btnEnterData.Font
    End Sub
    Private Sub btnStartTimetable_Click(sender As System.Object, e As System.EventArgs)
        clearData()
        Dim temp As New GameData
        changeTimetableTimes(0)
        GameNumber = 1
        GameList.Add(temp)
        'Dim PlayerScreen As New PlayerScreen
        PlayerScreen.Show()
        createTimer()
    End Sub
    Private Sub createTimer()
        Dim GameTimer As New Timer
        GameTimer.Interval = 1000
        AddHandler GameTimer.Tick, AddressOf Me.GameTimer_Tick
        GameTimer.Start()

    End Sub
    Private Sub GameTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tabCurrentGame = Me.Controls("TabControl1").Controls("tabCurrentGame")
        Dim lblGameTime As Label = tabCurrentGame.Controls("lblGameTime")
        Dim lblGameState As Label = tabCurrentGame.Controls("lblGameState")
        If Timetable.Count = 0 Then

        Else
            lblGameState.Text = Timetable(0).getGameState
            If TeamTimeout = False And RefereeTimeout = False Then
                If DateDiff(DateInterval.Second, TimeOfDay, Timetable(0).getTimeMarker) < 0 Then
                    Timetable.Dequeue()
                    If Timetable.Count = 0 Then

                    Else
                        lblGameState.Text = Timetable(0).getGameState
                        If Timetable(0).getGameState = "Warm Up" Then
                            clearData()
                        ElseIf Timetable(0).getGameState = "Half Time" And numberinSinBin > 0 Then
                                For i As Integer = 1 To numberinSinBin
                                    Dim lblSinBin As Label = tabCurrentGame.Controls("pnlSinBin").Controls("lblSinBinTime" & i)
                                    Dim tempDate As Date
                                    tempDate = lblSinBin.Tag
                                tempDate = tempDate.AddMinutes(Timetable(0).getDuration)
                                    lblSinBin.Tag = tempDate
                                Next
                        End If
                    End If

                End If
                If Timetable.Count = 0 Then
                    lblGameTime.Text = "00:00"
                    GameNumber = 0
                Else
                    Dim tempMin As String
                    Dim tempSec As String
                    tempMin = DateDiff(DateInterval.Second, TimeOfDay, Timetable(0).getTimeMarker) \ 60
                    If tempMin.Length = 1 Then
                        tempMin = "0" + tempMin
                    End If
                    tempSec = DateDiff(DateInterval.Second, TimeOfDay, Timetable(0).getTimeMarker) Mod 60
                    If tempSec.Length = 1 Then
                        tempSec = "0" + tempSec
                    End If
                    lblGameTime.Text = tempMin & ":" & tempSec
                End If
                If Timetable.Count > 0 Then
                    If (Timetable(0).getGameState = "First Half" Or Timetable(0).getGameState = "Second Half") And numberinSinBin > 0 Then
                        Dim tempInteger As Integer = numberinSinBin
                        For i As Integer = tempInteger To 1 Step -1
                            Dim lblSinBin As Label = tabCurrentGame.Controls("pnlSinBin").Controls("lblSinBinTime" & i)
                            Dim lblSinBinName As Label = tabCurrentGame.Controls("pnlSinBin").Controls("lblSinBinPlayer" & i)
                            Dim tempMin As String
                            Dim tempSec As String
                            tempMin = DateDiff(DateInterval.Second, TimeOfDay, lblSinBin.Tag) \ 60
                            If tempMin.Length = 1 Then
                                tempMin = "0" + tempMin
                            End If
                            tempSec = DateDiff(DateInterval.Second, TimeOfDay, lblSinBin.Tag) Mod 60
                            If tempSec.Length = 1 Then
                                tempSec = "0" + tempSec
                            End If
                            lblSinBin.Text = tempMin & ":" & tempSec
                            If (tempMin = "00" And tempSec = "00") Or DateDiff(DateInterval.Second, TimeOfDay, lblSinBin.Tag) < 1 Then
                                Dim playerLeavingSinBin As String = lblSinBinName.Text
                                If i <> tempInteger Then
                                    For j As Integer = i To tempInteger - 1
                                        Dim lblPlayerToBeChanged As Label = tabCurrentGame.Controls("pnlSinBin").Controls("lblSinBinPlayer" & j)
                                        Dim lblTimeToBeChanged As Label = tabCurrentGame.Controls("pnlSinBin").Controls("lblSinBinTime" & j)
                                        lblPlayerToBeChanged.Text = tabCurrentGame.Controls("pnlSinBin").Controls("lblSinBinPlayer" & j + 1).Text
                                        lblTimeToBeChanged.Text = tabCurrentGame.Controls("pnlSinBin").Controls("lblSinBinTime" & j + 1).Text
                                        lblTimeToBeChanged.Tag = tabCurrentGame.Controls("pnlSinBin").Controls("lblSinBinTime" & j + 1).Tag
                                    Next
                                End If
                                Dim lblPlayerToRemove As Label = tabCurrentGame.Controls("pnlSinBin").Controls("lblSinBinPlayer" & tempInteger)
                                Dim lblTimeToRemove As Label = tabCurrentGame.Controls("pnlSinBin").Controls("lblSinBinTime" & tempInteger)
                                tabCurrentGame.Controls("pnlSinBin").Controls.Remove(lblPlayerToRemove)
                                tabCurrentGame.Controls("pnlSinBin").Controls.Remove(lblTimeToRemove)
                                numberinSinBin += -1

                                MessageBox.Show(playerLeavingSinBin & " Done")
                            End If

                        Next

                    End If
                End If

            Else
                If TeamTimeout = True Then
                    lblGameState.Text = "Team Timeout"
                    Dim tempMin As String
                    Dim tempSec As String
                    tempMin = DateDiff(DateInterval.Second, TimeOfDay, TimeoutTimeEdit) \ 60
                    If tempMin.Length = 1 Then
                        tempMin = "0" + tempMin
                    End If
                    tempSec = DateDiff(DateInterval.Second, TimeOfDay, TimeoutTimeEdit) Mod 60
                    If tempSec.Length = 1 Then
                        tempSec = "0" + tempSec
                    End If
                    lblGameTime.Text = tempMin & ":" & tempSec
                    If (tempMin = "00" And tempSec = "00") Or DateDiff(DateInterval.Second, TimeOfDay, TimeoutTimeEdit) < 1 Then
                        TeamTimeout = False
                        lblGameState.BackColor = Color.White
                        lblGameTime.BackColor = Color.White
                    End If
                ElseIf RefereeTimeout = True Then
                    lblGameState.Text = "Referee Timeout"
                    Dim tempMin As String
                    Dim tempSec As String
                    tempMin = DateDiff(DateInterval.Second, TimeoutTimeEdit, TimeOfDay) \ 60
                    If tempMin.Length = 1 Then
                        tempMin = "0" + tempMin
                    End If
                    tempSec = DateDiff(DateInterval.Second, TimeoutTimeEdit, TimeOfDay) Mod 60
                    If tempSec.Length = 1 Then
                        tempSec = "0" + tempSec
                    End If
                    lblGameTime.Text = tempMin & ":" & tempSec
                End If

            End If
            If Timetable.Count > 0 Then
                'MessageBox.Show(PlayerScreen.Controls.Count)
                PlayerScreen.updateTimeControls()
            End If
        End If

        '''''more stuff
    End Sub
    Private Sub clearData()
        Dim tabCurrentGame = Me.Controls("TabControl1").Controls("tabCurrentGame")
        Dim temp As New GameData
        Dim colour As String = "White"
        Dim lblTeamName As Label
        Dim GoalScoredLabel As Label
        Dim GoalScorerPanel = tabCurrentGame.Controls("pnlGoalScorers")
        Dim SinBinPanel = tabCurrentGame.Controls("pnlSinBin")

        For i As Integer = 0 To 1
            lblTeamName = tabCurrentGame.Controls("lbl" & colour & "TeamName")
            GoalScoredLabel = tabCurrentGame.Controls("lbl" & colour & "GoalScored")
            lblTeamName.Text = ""
            GoalScoredLabel.Text = "0"
            colour = "Black"
        Next
        For i As Integer = 1 To 13
            If GameList(GameNumber).getWhiteTeamGoalScorers(i) > 0 Then
                colour = "White"
                GoalScorerPanel.Controls.Remove(GoalScorerPanel.Controls("GoalScorer" & colour & numberdifferentscored(0)))
                numberdifferentscored(0) += -1
            End If
            If GameList(GameNumber).getBlackTeamGoalScorers(i) > 0 Then
                colour = "Black"
                GoalScorerPanel.Controls.Remove(GoalScorerPanel.Controls("GoalScorer" & colour & numberdifferentscored(1)))
                numberdifferentscored(1) += -1
            End If

        Next
        If numberinSinBin > 0 Then
            For i As Integer = 1 To numberinSinBin
                SinBinPanel.Controls.Remove(SinBinPanel.Controls("lblSinBinPlayer" & i))
                SinBinPanel.Controls.Remove(SinBinPanel.Controls("lblSinBinTime" & i))
            Next
        End If
        numberinSinBin = 0
        numberdifferentscored(0) = 0
        numberdifferentscored(1) = 0
        WhiteTimeout = True
        BlackTimeout = True
        GameList.Add(temp)
        GameNumber += 1
    End Sub
    Private Sub changeTimetableTimes(Iteration As Integer)
        If Iteration = Timetable.Count Then

        Else
            If Iteration = 0 Then
                Timetable(Iteration).setStartTime(TimeOfDay)
            Else
                Timetable(Iteration).setStartTime(Timetable(Iteration - 1).getTimeMarker)
            End If
            Timetable(Iteration).setTimeMarker()
            changeTimetableTimes(Iteration + 1)

        End If
    End Sub
    Private Sub creategametimetable(NumberOfGames As Integer, HalfLength As Decimal, HalfTimeLength As Decimal, WarmUpLength As Decimal, BreakLength As Decimal)
        Dim input As New GameTimetable
        For i As Integer = 1 To NumberOfGames
            Timetable.Enqueue(input)
            Timetable(Timetable.Count - 1).SetGameState("Warm Up")
            Timetable(Timetable.Count - 1).SetDuration(WarmUpLength)
            If Timetable.Count = 1 Then
                Timetable(Timetable.Count - 1).setStartTime(TimeOfDay)
            Else
                Timetable(Timetable.Count - 1).setStartTime(Timetable(Timetable.Count - 2).getTimeMarker())
            End If
            Timetable(Timetable.Count - 1).setTimeMarker()
            input = New GameTimetable
            Timetable.Enqueue(input)
            Timetable(Timetable.Count - 1).SetGameState("First Half")
            Timetable(Timetable.Count - 1).SetDuration(HalfLength)
            Timetable(Timetable.Count - 1).setStartTime(Timetable(Timetable.Count - 2).getTimeMarker())
            Timetable(Timetable.Count - 1).setTimeMarker()
            input = New GameTimetable
            If HalfTimeLength = 0 Then

            Else
                Timetable.Enqueue(input)
                Timetable(Timetable.Count - 1).SetGameState("Half Time")
                Timetable(Timetable.Count - 1).SetDuration(HalfTimeLength)
                Timetable(Timetable.Count - 1).setStartTime(Timetable(Timetable.Count - 2).getTimeMarker())
                Timetable(Timetable.Count - 1).setTimeMarker()
                input = New GameTimetable
                Timetable.Enqueue(input)
                Timetable(Timetable.Count - 1).SetGameState("Second Half")
                Timetable(Timetable.Count - 1).SetDuration(HalfLength)
                Timetable(Timetable.Count - 1).setStartTime(Timetable(Timetable.Count - 2).getTimeMarker())
                Timetable(Timetable.Count - 1).setTimeMarker()
                input = New GameTimetable
            End If
        Next
        If BreakLength > 0 Then
            Timetable.Enqueue(input)
            Timetable(Timetable.Count - 1).SetGameState("Break")
            Timetable(Timetable.Count - 1).SetDuration(BreakLength)
            Timetable(Timetable.Count - 1).setStartTime(Timetable(Timetable.Count - 2).getTimeMarker())
            Timetable(Timetable.Count - 1).setTimeMarker()
            input = New GameTimetable
        End If
    End Sub
    Private Sub createpnlLabels(iteration As Integer)
        Dim lblGameSetupPanel As New Label
        lblGameSetupPanel.Name = "lblGameSetupPanel" & iteration
        lblGameSetupPanel.Text = Timetable(iteration).getGameState & " " & Timetable(iteration).getDuration & " " & Timetable(iteration).getStartTime()
        lblGameSetupPanel.TextAlign = ContentAlignment.MiddleCenter
        lblGameSetupPanel.BackColor = Color.White
        lblGameSetupPanel.BorderStyle = BorderStyle.FixedSingle
        Me.Controls("TabControl1").Controls("tabGameSetup").Controls("pnlGameSetup").Controls.Add(lblGameSetupPanel)
        sizepnlLabels(iteration)
    End Sub
    Private Sub sizepnlLabels(iteration As Integer)
        Dim pnlGameSetup As Panel = Me.Controls("TabControl1").Controls("tabGameSetup").Controls("pnlGameSetup")
        Dim lblGameSetupPanel As Label = pnlGameSetup.Controls("lblGameSetupPanel" & iteration)
        lblGameSetupPanel.Size = New Size((pnlGameSetup.Size.Width) - 10, (pnlGameSetup.Size.Height / 10) - 10)
        lblGameSetupPanel.Location = New Point(5, (iteration) * lblGameSetupPanel.Size.Height + 5)
        If iteration = 0 Then
            Dim fontcount As Integer = 0
            Dim sizeOfString As New SizeF
            Dim g As Graphics = Me.CreateGraphics
            Dim fits As Boolean = True
            Dim numberLinesNeeded As Decimal
            Dim numberLinesAvailable As Decimal
            Dim lineSpacingPixel As Decimal
            Dim tempFontFamily As FontFamily
            Do
                fontcount += 1
                lblGameSetupPanel.Font = New Font(lblGameSetupPanel.Font.Name, fontcount)
                tempFontFamily = lblGameSetupPanel.Font.FontFamily
                sizeOfString = g.MeasureString(lblGameSetupPanel.Text, lblGameSetupPanel.Font)
                lineSpacingPixel = (lblGameSetupPanel.Font.Size * tempFontFamily.GetLineSpacing(FontStyle.Regular)) / (tempFontFamily.GetEmHeight(FontStyle.Regular))
                numberLinesNeeded = sizeOfString.Width / (lblGameSetupPanel.Width - 6)
                numberLinesAvailable = ((lblGameSetupPanel.Height - 6 + Int(lineSpacingPixel)) / (sizeOfString.Height + Int(lineSpacingPixel)))
                If numberLinesNeeded - Int(numberLinesNeeded) >= 1 - (0.15 * (Int(numberLinesNeeded) + 1)) Then
                    numberLinesNeeded += 0.16 * (Int(numberLinesNeeded) + 1)
                End If
            Loop While numberLinesAvailable >= 1 And numberLinesNeeded <= Int(numberLinesAvailable)
            fontcount += -1
            lblGameSetupPanel.Font = New Font(lblGameSetupPanel.Font.Name, fontcount)
        Else
            Dim lblGameSetupPanel1 As Label = pnlGameSetup.Controls("lblGameSetupPanel" & 0)
            lblGameSetupPanel.Font = lblGameSetupPanel1.Font
        End If
    End Sub
    Private Sub Octopush_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        columnedgeCurrentGameWidth = (Me.Size.Width - 75) * 0.21
        columncentreCurrentGameWidth = (Me.Size.Width - 75) * 0.33
        doublecolumnedgeCurrentGameWidth = (columnedgeCurrentGameWidth / 2)
        doublecolumncentreCurrentGameWidth = (columncentreCurrentGameWidth / 2)
        endcolumnCurrentGameWidth = (Me.Size.Width - 75) * 0.25
        halfPanelWidth = (endcolumnCurrentGameWidth - 30) * 0.5
        buttonrowCurrentGameHeight = (Me.Size.Height - 70) * 0.1
        labelrow1CurrentGameHeight = (Me.Size.Height - 70) * 0.1
        labelrow2CurrentGameHeight = (Me.Size.Height - 70) * 0.2
        panelCurrentGameHeight = (Me.Size.Height - 90) * 0.5
        panelRowHeight = (panelCurrentGameHeight - 40) / 7
        column1CurrentGameXPosition = 10
        column15CurrentGameXPosition = column1CurrentGameXPosition + doublecolumnedgeCurrentGameWidth
        column2CurrentGameXPosition = 10 + column1CurrentGameXPosition + columnedgeCurrentGameWidth
        column25CurrentGameXPosition = column2CurrentGameXPosition + doublecolumncentreCurrentGameWidth
        column3CurrentGameXPosition = 10 + column2CurrentGameXPosition + columncentreCurrentGameWidth
        column35CurrentGameXPosition = column3CurrentGameXPosition + doublecolumnedgeCurrentGameWidth
        column4CurrentGameXPosition = 10 + column3CurrentGameXPosition + columnedgeCurrentGameWidth
        row1CurrentGameYPosition = 10
        row2CurrentGameYPosition = 10 + row1CurrentGameYPosition + buttonrowCurrentGameHeight
        row3CurrentGameYPosition = 10 + row2CurrentGameYPosition + labelrow1CurrentGameHeight
        row4CurrentGameYPosition = 10 + row3CurrentGameYPosition + labelrow2CurrentGameHeight
        row5CurrentGameYPosition = 10 + row4CurrentGameYPosition + buttonrowCurrentGameHeight
        panelrow2CurrentGameYPosition = 10 + panelCurrentGameHeight + row1CurrentGameYPosition
        If loaded = False Then
            loaded = True
        Else
            SizeTabControl(Me.Size)
            reSizeCurrentGame()
            reSizeGameSetup()
        End If
    End Sub
    Private Sub reSizeCurrentGame()
        reSizeCurrentGameButtons()
        reSizeCurrentGameLabels()
        reSizeCurrentGamePanel()
    End Sub
    Private Sub reSizeCurrentGameButtons()
        Dim colour, addorremove As String
        colour = "White"
        For i As Integer = 0 To 1
            addorremove = "Add"
            sizeTeamNameButtons(colour)
            sizeTeamTimeoutButtons(colour)
            sizeTeamSinBinButtons(colour)
            For j As Integer = 0 To 1
                sizeAddGoalButtons(colour, addorremove)
                addorremove = "Remove"
            Next
            colour = "Black"
        Next
        sizeRefereeButton()
        sizeRefereeTimeoutButton()

    End Sub
    Private Sub reSizeCurrentGameLabels()
        Dim colour As String
        colour = "White"
        For i As Integer = 0 To 1
            sizeTeamNamelabels(colour)
            sizeGoalScoredlabel(colour)
            colour = "Black"
        Next
        sizeGameStatelabel()
        sizeGameTimelabel()
    End Sub
    Private Sub reSizeCurrentGamePanel()
        Dim colour As String = "White"
        sizeSinBinPanel()
        sizepnlSinBinTitleLabel()
        sizeGoalScorersPanel()
        sizepnlGoalScorersTitleLabel()
        For i As Integer = 0 To 1
            sizepnlGoalScorersSubTitlesLabels(colour)
            If numberdifferentscored(i) > 0 Then
                For j As Integer = 1 To numberdifferentscored(i)
                    AddGoal.sizeGoalScorerLabels(j, colour)
                Next
            End If
            colour = "Black"
        Next
    End Sub
    Private Sub reSizeGameSetup()
        sizelblNumberBreak()
        sizetxtNumberBreak()
        sizebtnNumberBreak()
        sizeGameSetupPanel()
        If Timetable.Count > 0 Then
            For i As Integer = 0 To Timetable.Count - 1
                sizepnlLabels(i)
            Next
        End If
        If numberOfBreaks > -1 Then
            sizelblNumberOfGame()
            sizetxtNumberOfGame()
            sizelblEnterHalfLength()
            sizetxtEnterHalfLength()
            sizelblEnterHalfTimeLength()
            sizetxtEnterHalfTimeLength()
            sizelblEnterWarmUpLength()
            sizetxtEnterWarmUpLength()
            sizebtnEnterData()
            If numberSetup = numberOfBreaks + 1 Then
                sizebtnStartTimetable()
            ElseIf numberOfBreaks > 0 And numberSetup < numberOfBreaks Then
                sizelblEnterBreakLength()
                sizetxtEnterBreakLength()
            End If
        End If


    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        Dim TimeoutButton = Me.Controls("TabControl1").Controls("tabCurrentGame").Controls("btnWhiteTimeout")
        TimeoutButton.Size = New Size(doublecolumncentreCurrentGameWidth, buttonrowCurrentGameHeight)
        Dim fontcount As Integer = 0
        Dim sizeOfString As New SizeF
        Dim g As Graphics = Me.CreateGraphics
        Dim fits As Boolean = True
        Dim numberLinesNeeded As Decimal
        Dim numberLinesAvailable As Decimal
        Dim lineSpacingPixel As Decimal
        Dim tempFontFamily As FontFamily


        fontcount = TimeoutButton.Font.Size
        tempFontFamily = TimeoutButton.Font.FontFamily
        sizeOfString = g.MeasureString(TimeoutButton.Text, TimeoutButton.Font)
        lineSpacingPixel = (TimeoutButton.Font.Size * tempFontFamily.GetLineSpacing(FontStyle.Regular)) / (tempFontFamily.GetEmHeight(FontStyle.Regular))
        numberLinesNeeded = sizeOfString.Width / (TimeoutButton.Width - 6)
        numberLinesAvailable = ((TimeoutButton.Height - 6 + Int(lineSpacingPixel)) / (sizeOfString.Height + Int(lineSpacingPixel)))
        MessageBox.Show(fontcount & " Needed " & numberLinesNeeded & " Available " & numberLinesAvailable)


    End Sub
End Class
