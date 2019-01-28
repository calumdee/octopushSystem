Public Class Login

    Private Sub Login_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim tabReference As TabControl = Octopush.Controls("TabControl1")
        Octopush.Enabled = True
        If Me.Tag <> "" Then
            tabReference.SelectTab(0)
        Else

        End If

    End Sub
    Private Sub Login_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        EnterClick(sender, e)
    End Sub
    Private Sub txtEnterLogin_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        EnterClick(sender, e)
    End Sub
    Private Sub EnterClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode.Equals(Keys.Enter) Then
            btnEnterLogin_Click()
        End If
    End Sub
    Private Sub Login_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = New Size(320, 100)
        Me.MinimumSize = Me.MaximumSize
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.BackColor = Color.RoyalBlue
        Dim txtEnterLogin As New TextBox
        txtEnterLogin.Name = "txtEnterLogin"
        Dim tempFont As Font
        tempFont = txtEnterLogin.Font
        txtEnterLogin.Font = New Font(tempFont.Name, 18)
        txtEnterLogin.Text = "Enter Password"
        Me.Controls.Add(txtEnterLogin)
        AddHandler txtEnterLogin.KeyDown, AddressOf Me.txtEnterLogin_KeyDown
        AddHandler txtEnterLogin.Click, AddressOf Me.txtEnterLogin_Click
        Dim btnEnterLogin As New Button
        btnEnterLogin.Name = "btnEnterLogin"
        btnEnterLogin.BackColor = Color.Gainsboro
        btnEnterLogin.Text = "Enter"
        btnEnterLogin.Font = txtEnterLogin.Font
        Me.Controls.Add(btnEnterLogin)
        AddHandler btnEnterLogin.Click, AddressOf Me.btnEnterLogin_Click

        sizecreatelogin(Me.Size)
    End Sub
    Private Sub sizecreatelogin(FormSize As Size)
        Me.Controls("txtEnterLogin").Location = New Point(10, 10)
        Me.Controls("txtEnterLogin").Size = New Size(((FormSize.Width - 45) * 0.67), 0)
        Me.Controls("btnEnterLogin").Location = New Point(Me.Controls("txtEnterLogin").Size.Width + 20, 10)
        Me.Controls("btnEnterLogin").Size = New Size((FormSize.Width - 45) * 0.33, Me.Controls("txtEnterLogin").Size.Height)
        ''''
    End Sub
    Private Sub txtEnterLogin_Click()
        Dim textbox As TextBox = Me.Controls("txtEnterLogin")
        textbox.Text = ""
    End Sub
    Private Sub btnEnterLogin_Click()
        Dim textbox As TextBox = Me.Controls("txtEnterLogin")
        If textbox.Text = Octopush.password Then
            Me.Tag = ""
            Me.Close()
        Else
            textbox.Text = ""
        End If

    End Sub

End Class