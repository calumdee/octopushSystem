Public Class TeamName

    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = New Size(360, 100)
        Me.MinimumSize = Me.MaximumSize
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.BackColor = Color.RoyalBlue
        Me.Text = "Enter the Team name of the " & Me.Tag & " Team"
        createFormControls()
    End Sub
    Private Sub Login_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        EnterClick(sender, e)
    End Sub
    Private Sub txtEnterTeamName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        EnterClick(sender, e)
    End Sub
    Private Sub EnterClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode.Equals(Keys.Enter) Then
            btnEnterTeamName_Click()
        End If
    End Sub
    Private Sub createFormControls()
        Dim txtEnterTeamName As New TextBox
        txtEnterTeamName.Name = "txtEnterTeamName"
        Dim tempFont As Font
        tempFont = txtEnterTeamName.Font
        txtEnterTeamName.Font = New Font(tempFont.Name, 18)
        txtEnterTeamName.Text = "Enter Team Name"
        Me.Controls.Add(txtEnterTeamName)
        AddHandler txtEnterTeamName.KeyDown, AddressOf Me.txtEnterTeamName_KeyDown
        AddHandler txtEnterTeamName.Click, AddressOf Me.txtEnterTeamName_Click
        Dim btnEnterTeamName As New Button
        btnEnterTeamName.Name = "btnEnterTeamName"
        btnEnterTeamName.BackColor = Color.Gainsboro
        btnEnterTeamName.Text = "Enter"
        btnEnterTeamName.Font = txtEnterTeamName.Font
        Me.Controls.Add(btnEnterTeamName)
        AddHandler btnEnterTeamName.Click, AddressOf Me.btnEnterTeamName_Click
        sizecreatelogin(Me.Size)
    End Sub
    Private Sub sizecreatelogin(FormSize As Size)
        Me.Controls("txtEnterTeamName").Location = New Point(10, 10)
        Me.Controls("txtEnterTeamName").Size = New Size(((FormSize.Width - 45) * 0.67), 0)
        Me.Controls("btnEnterTeamName").Location = New Point(Me.Controls("txtEnterTeamName").Size.Width + 20, 10)
        Me.Controls("btnEnterTeamName").Size = New Size((FormSize.Width - 45) * 0.33, Me.Controls("txtEnterTeamName").Size.Height)
        ''''
    End Sub
    Private Sub txtEnterTeamName_Click()
        Dim textbox As TextBox = Me.Controls("txtEnterTeamName")
        textbox.Text = ""
    End Sub
    Private Sub btnEnterTeamName_Click()
        Dim TeamNameLabel As Label = Octopush.Controls("TabControl1").Controls("tabCurrentGame").Controls("lbl" & Me.Tag & "TeamName")
        If Me.Controls("txtEnterTeamName").Text = "Enter Team Name" Then
            TeamNameLabel.Text = ""
        Else
            TeamNameLabel.Text = Me.Controls("txtEnterTeamName").Text
        End If
        If Me.Tag = "White" Then
            GameList(GameNumber).setTeamName(0, TeamNameLabel.Text)
        Else
            GameList(GameNumber).setTeamName(1, TeamNameLabel.Text)
        End If
        Me.Close()
    End Sub

End Class
