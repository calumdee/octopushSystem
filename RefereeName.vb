Public Class RefereeName
    Dim FirstClick As Boolean = True
    Private Sub RefereeName_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = New Size(360, 300)
        Me.MinimumSize = Me.MaximumSize
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.BackColor = Color.RoyalBlue
        Me.Text = "Enter the names of the referees"
        createFormControls()
        Me.MaximumSize = New Size(360, (Me.Controls("txtEnterRefereeName1").Size.Height + 10) * 3 + 45)
    End Sub
    Private Sub createFormControls()
        For i As Integer = 1 To 3
            Dim txtEnterRefereeName As New TextBox
            txtEnterRefereeName.Name = "txtEnterRefereeName" & i
            Dim tempFont As Font
            tempFont = txtEnterRefereeName.Font
            txtEnterRefereeName.Font = New Font(tempFont.Name, 18)
            Select Case i
                Case Is = 1
                    txtEnterRefereeName.Text = "Enter Referee Name"
                Case Is = 2
                    txtEnterRefereeName.Text = "Put - for no referee"
                Case Is = 3
                    txtEnterRefereeName.Text = "? if name not known"
            End Select

            Me.Controls.Add(txtEnterRefereeName)
            sizetextbox(i, Me.Size)
            AddHandler txtEnterRefereeName.KeyDown, AddressOf Me.txtEnterRefereeName_KeyDown
            AddHandler txtEnterRefereeName.Click, AddressOf Me.txtEnterRefereeName_Click
        Next

        Dim btnEnterRefereeName As New Button
        btnEnterRefereeName.Name = "btnEnterRefereeName"
        btnEnterRefereeName.BackColor = Color.Gainsboro
        btnEnterRefereeName.Text = "Enter"
        btnEnterRefereeName.Font = Me.Controls("txtEnterRefereeName1").Font
        Me.Controls.Add(btnEnterRefereeName)
        AddHandler btnEnterRefereeName.Click, AddressOf Me.btnEnterRefereeName_Click
        sizebutton(Me.Size)
    End Sub
    Private Sub sizetextbox(number As Integer, FormSize As Size)
        Me.Controls("txtEnterRefereeName" & number).Size = New Size(((FormSize.Width - 45) * 0.72), 0)
        Me.Controls("txtEnterRefereeName" & number).Location = New Point(10, 10 * number + (number - 1) * Me.Controls("txtEnterRefereeName" & number).Size.Height)

    End Sub
    Private Sub sizebutton(FormSize As Size)
        Me.Controls("btnEnterRefereeName").Location = New Point(Me.Controls("txtEnterRefereeName1").Size.Width + 20, 10)
        Me.Controls("btnEnterRefereeName").Size = New Size((FormSize.Width - 45) * 0.28, Me.Controls("txtEnterRefereeName1").Size.Height)
    End Sub
    Private Sub txtEnterRefereeName_Click()
        If FirstClick = True Then
            For i As Integer = 1 To 3
                Dim textbox As TextBox = Me.Controls("txtEnterRefereeName" & i)
                textbox.Text = ""
            Next
            FirstClick = False
        End If
    End Sub
    Private Sub btnEnterRefereeName_Click()
        Dim TempString As String
        For i As Integer = 1 To 3
            Dim textbox As TextBox = Me.Controls("txtEnterRefereeName" & i)
            If textbox.Text = "Enter Referee Name" Then
                TempString = "-"
            ElseIf textbox.Text = "Put - for no referee" Then
                TempString = "-"
            ElseIf textbox.Text = "? if name not known" Then
                TempString = "-"
            ElseIf textbox.Text = "" Then
                TempString = "-"
            Else
                TempString = textbox.Text
            End If
            GameList(GameNumber).setRefereeNames(i, TempString)
        Next
        Me.Close()
    End Sub
    Private Sub RefereeName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        EnterClick(sender, e)
    End Sub
    Private Sub txtEnterRefereeName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        EnterClick(sender, e)
    End Sub
    Private Sub EnterClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode.Equals(Keys.Enter) Then
            btnEnterRefereeName_Click()
        End If
    End Sub
End Class