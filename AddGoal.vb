Imports System.Text.RegularExpressions
Public Class AddGoal
    Dim colour As String
    Dim addorremove As String
    Dim grammar As String

    Private Sub AddGoal_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim temp As String = Me.Tag
        colour = temp.Substring(0, 5)
        addorremove = temp.Substring(6, temp.Length - 6)
        If addorremove = "Add" Then
            grammar = "to"
        Else
            grammar = "from"
        End If
        Me.Text = addorremove & " goal " & grammar & " the " & colour & " team"
        Me.MaximumSize = New Size(360, 100)
        Me.MinimumSize = Me.MaximumSize
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.BackColor = Color.RoyalBlue
        createFormControls()
    End Sub
    Private Sub AddGoal_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        EnterClick(sender, e)
    End Sub
    Private Sub txtAddGoal_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        EnterClick(sender, e)
    End Sub
    Private Sub EnterClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode.Equals(Keys.Enter) Then
            btnAddGoal_Click(sender, e)
        End If
    End Sub
    Private Sub createFormControls()
        Dim txtAddGoal As New TextBox
        txtAddGoal.Name = "txtAddGoal"
        Dim tempFont As Font
        tempFont = txtAddGoal.Font
        txtAddGoal.Font = New Font(tempFont.Name, 18)
        txtAddGoal.Text = "Number of player"
        Me.Controls.Add(txtAddGoal)
        AddHandler txtAddGoal.KeyDown, AddressOf Me.txtAddGoal_KeyDown
        AddHandler txtAddGoal.Click, AddressOf Me.txtAddGoal_Click
        Dim btnAddGoal As New Button
        btnAddGoal.Name = "btnAddGoal"
        btnAddGoal.BackColor = Color.Gainsboro
        btnAddGoal.Text = "Enter"
        btnAddGoal.Font = txtAddGoal.Font
        Me.Controls.Add(btnAddGoal)
        AddHandler btnAddGoal.Click, AddressOf Me.btnAddGoal_Click
        sizecontrols(Me.Size)
    End Sub
    Private Sub sizecontrols(FormSize As Size)
        Me.Controls("txtAddGoal").Location = New Point(10, 10)
        Me.Controls("txtAddGoal").Size = New Size(((FormSize.Width - 45) * 0.67), 0)
        Me.Controls("btnAddGoal").Location = New Point(Me.Controls("txtAddGoal").Size.Width + 20, 10)
        Me.Controls("btnAddGoal").Size = New Size((FormSize.Width - 45) * 0.33, Me.Controls("txtAddGoal").Size.Height)
        ''''
    End Sub
    Private Sub txtAddGoal_Click()
        Dim textbox As TextBox = Me.Controls("txtAddGoal")
        textbox.Text = ""
    End Sub
    Private Sub btnAddGoal_Click(sender As System.Object, e As System.EventArgs)
        Dim GoalScoredLabel As Label = Octopush.Controls("TabControl1").Controls("tabCurrentGame").Controls("lbl" & Me.colour & "GoalScored")
        Dim txtAddGoal As TextBox = Me.Controls("txtAddGoal")
        If Regex.IsMatch(txtAddGoal.Text, "^(1[0-3]|[1-9])$") Then
            Dim playernumber As Integer
            playernumber = txtAddGoal.Text
            If addorremove = "Add" Then
                GoalScoredLabel.Text += 1
                If colour = "White" Then
                    GameList(GameNumber).setWhiteTeamGoalScorers(playernumber, addorremove)
                Else
                    GameList(GameNumber).setBlackTeamGoalScorers(playernumber, addorremove)
                End If
                AddScorerLabel(playernumber, GoalScoredLabel, colour)
                Me.Close()
            Else
                If (GameList(GameNumber).getWhiteTeamGoalScorers(playernumber) > 0 And colour = "White") Or (GameList(GameNumber).getBlackTeamGoalScorers(playernumber) > 0 And colour = "Black") Then
                    GoalScoredLabel.Text += -1
                    If colour = "White" Then
                        GameList(GameNumber).setWhiteTeamGoalScorers(playernumber, addorremove)
                    Else
                        GameList(GameNumber).setBlackTeamGoalScorers(playernumber, addorremove)
                    End If
                    RemoveScorerLabel(playernumber, GoalScoredLabel, colour)
                    Me.Close()
                End If
            End If
        End If

    End Sub
    Private Sub AddScorerLabel(playernumber As Integer, goalscoredlabel As Label, colour As String)
        Dim GoalScorerPanel = Octopush.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlGoalScorers")
        Dim temp As Integer
        If colour = "White" Then
            temp = 0
        Else
            temp = 1
        End If
        If (GameList(GameNumber).getWhiteTeamGoalScorers(playernumber) > 1 And colour = "White") Or (GameList(GameNumber).getBlackTeamGoalScorers(playernumber) > 1 And colour = "Black") Then
            For i As Integer = 1 To numberdifferentscored(temp)
                If GoalScorerPanel.Controls("GoalScorer" & colour & i).Text = (playernumber & " - " & GameList(GameNumber).getWhiteTeamGoalScorers(playernumber) - 1) And colour = "White" Or GoalScorerPanel.Controls("GoalScorer" & colour & i).Text = (playernumber & " - " & GameList(GameNumber).getBlackTeamGoalScorers(playernumber) - 1) And colour = "Black" Then
                    GoalScorerPanel.Controls("GoalScorer" & colour & i).Tag += 1
                    GoalScorerPanel.Controls("GoalScorer" & colour & i).Text = playernumber & " - " & GoalScorerPanel.Controls("GoalScorer" & colour & i).Tag

                End If
            Next
        Else
            numberdifferentscored(temp) += 1
            Dim GoalScorer As New Label
            GoalScorer.Name = "GoalScorer" & colour & numberdifferentscored(temp)
            GoalScorer.Tag = 1
            GoalScorer.Text = playernumber & " - " & GoalScorer.Tag
            GoalScorer.BackColor = Color.White
            GoalScorer.BorderStyle = BorderStyle.FixedSingle
            GoalScorer.TextAlign = ContentAlignment.MiddleCenter
            GoalScorerPanel.Controls.Add(GoalScorer)
            sizeGoalScorerLabels(numberdifferentscored(temp), colour)
        End If

    End Sub
    Public Sub sizeGoalScorerLabels(currentlabel As Integer, colour As String)
        Dim GoalScorerPanel = Octopush.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlGoalScorers")
        Dim GoalScorer = GoalScorerPanel.Controls("GoalScorer" & colour & currentlabel)
        GoalScorer.Size = New Size(Octopush.halfPanelWidth, Octopush.panelRowHeight)
        If colour = "White" Then
            GoalScorer.Location = New Point(Octopush.column1CurrentGameXPosition, 5 + (currentlabel + 1) * (5 + Octopush.panelRowHeight))
        Else
            GoalScorer.Location = New Point(Octopush.column1CurrentGameXPosition + 10 + Octopush.halfPanelWidth, 5 + (currentlabel + 1) * (5 + Octopush.panelRowHeight))
        End If
    End Sub
    Private Sub RemoveScorerLabel(playernumber As Integer, goalscoredlabel As Label, colour As String)
        Dim GoalScorerPanel = Octopush.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlGoalScorers")
        Dim removed As Boolean = False
        Dim temp As Integer
        Dim counter As Integer = 1
        If colour = "White" Then
            temp = 0
        Else
            temp = 1
        End If
        If (GameList(GameNumber).getWhiteTeamGoalScorers(playernumber) < 1 And colour = "White") Or (GameList(GameNumber).getBlackTeamGoalScorers(playernumber) < 1 And colour = "Black") Then
            Do
                If GoalScorerPanel.Controls("GoalScorer" & colour & counter).Text = (playernumber & " - " & GameList(GameNumber).getWhiteTeamGoalScorers(playernumber) + 1) And colour = "White" Or GoalScorerPanel.Controls("GoalScorer" & colour & counter).Text = (playernumber & " - " & GameList(GameNumber).getBlackTeamGoalScorers(playernumber) + 1) And colour = "Black" Then
                    For k As Integer = counter To numberdifferentscored(temp)
                        If k = numberdifferentscored(temp) Then
                            numberdifferentscored(temp) += -1
                            GoalScorerPanel.Controls.Remove(GoalScorerPanel.Controls("GoalScorer" & colour & k))
                        Else
                            GoalScorerPanel.Controls("GoalScorer" & colour & k).Tag = GoalScorerPanel.Controls("GoalScorer" & colour & (k + 1)).Tag
                            GoalScorerPanel.Controls("GoalScorer" & colour & k).Text = GoalScorerPanel.Controls("GoalScorer" & colour & (k + 1)).Text
                        End If

                    Next
                    removed = True
                End If
                counter += 1
            Loop Until removed = True Or counter = numberdifferentscored(temp) + 1
        Else
            For i As Integer = 1 To numberdifferentscored(temp)
                If GoalScorerPanel.Controls("GoalScorer" & colour & i).Text = (playernumber & " - " & GameList(GameNumber).getWhiteTeamGoalScorers(playernumber) + 1) And colour = "White" Or GoalScorerPanel.Controls("GoalScorer" & colour & i).Text = (playernumber & " - " & GameList(GameNumber).getBlackTeamGoalScorers(playernumber) + 1) And colour = "Black" Then
                    GoalScorerPanel.Controls("GoalScorer" & colour & i).Tag += -1
                    GoalScorerPanel.Controls("GoalScorer" & colour & i).Text = playernumber & " - " & GoalScorerPanel.Controls("GoalScorer" & colour & i).Tag

                End If
            Next
        End If
    End Sub
End Class