Imports System.Text.RegularExpressions
Public Class SinBin
    Dim colour As String
    Private Sub SinBin_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        colour = Me.Tag
        Me.Text = "Add a sin bin to the " & Me.Tag & " Team"
        Me.MaximumSize = New Size(360, 100)
        Me.MinimumSize = Me.MaximumSize
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.BackColor = Color.RoyalBlue
        createFormControls()
    End Sub
    Private Sub createFormControls()
        Dim tempFont As Font = Nothing
        For i As Integer = 0 To 1
            Dim txtSinBin As New TextBox
            If i = 0 Then
                txtSinBin.Name = "txtSinBinNum"
                txtSinBin.Text = "Number"
            Else
                txtSinBin.Name = "txtSinBinTime"
                txtSinBin.Text = "Time"
            End If
            tempFont = txtSinBin.Font
            txtSinBin.Font = New Font(tempFont.Name, 18)
            Me.Controls.Add(txtSinBin)
            AddHandler txtSinBin.KeyDown, AddressOf Me.txtSinBin_KeyDown
            AddHandler txtSinBin.Click, AddressOf Me.txtSinBin_Click
        Next
        Dim btnSinBin As New Button
        btnSinBin.Name = "btnSinBin"
        btnSinBin.BackColor = Color.Gainsboro
        btnSinBin.Text = "Enter"
        btnSinBin.Font = tempFont
        Me.Controls.Add(btnSinBin)
        AddHandler btnSinBin.Click, AddressOf Me.btnSinBin_Click
        sizecontrols(Me.Size)
    End Sub
    Private Sub sizecontrols(FormSize As Size)
        Me.Controls("txtSinBinNum").Location = New Point(10, 10)
        Me.Controls("txtSinBinNum").Size = New Size(((FormSize.Width - 45) * 0.32), 0)
        Me.Controls("txtSinBinTime").Location = New Point(Me.Controls("txtSinBinNum").Size.Width + 20, 10)
        Me.Controls("txtSinBinTime").Size = New Size((FormSize.Width - 45) * 0.32, Me.Controls("txtSinBinNum").Size.Height)
        Me.Controls("btnSinBin").Location = New Point(Me.Controls("txtSinBinTime").Location.X + Me.Controls("txtSinBinTime").Size.Width + 10, 10)
        Me.Controls("btnSinBin").Size = New Size((FormSize.Width - 45) * 0.32, Me.Controls("txtSinBinNum").Size.Height)
        ''''
    End Sub
    Private Sub txtSinBin_Click(sender, e)
        Dim textbox As TextBox = sender
        textbox.Text = ""
    End Sub
    Private Sub SinBin_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        EnterClick(sender, e)
    End Sub
    Private Sub txtSinBin_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        EnterClick(sender, e)
    End Sub
    Private Sub EnterClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode.Equals(Keys.Enter) Then
            btnSinBin_Click(sender, e)
        End If
    End Sub
    Private Sub btnSinBin_Click(sender As System.Object, e As System.EventArgs)
        Dim txtSinBin As TextBox = Me.Controls("txtSinBinNum")
        Dim txtSinBinTime As TextBox = Me.Controls("txtSinBinTime")
        Dim hasSinBin As Boolean = False
        Dim tempDate As New Date
        If Regex.IsMatch(txtSinBin.Text, "^(1[0-3]|[1-9])$") And Regex.IsMatch(txtSinBinTime.Text, "1|2|5") Then
            If numberinSinBin = 0 Then

            Else
                For i As Integer = 1 To numberinSinBin
                    Dim SinBinPanelLabel = Octopush.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlSinBin").Controls("lblSinBinPlayer" & i)
                    If SinBinPanelLabel.Text = colour & " " & txtSinBin.Text Then
                        hasSinBin = True
                        Dim SinBinPanelLabelTime = Octopush.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlSinBin").Controls("lblSinBinTime" & i)
                        tempDate = SinBinPanelLabelTime.Tag
                        tempDate = tempDate.AddMinutes(txtSinBinTime.Text)
                        SinBinPanelLabelTime.Tag = tempDate
                    End If
                Next
            End If
            If hasSinBin = True Then

            Else
                numberinSinBin += 1
                Dim SinBinPanelLabel As New Label
                SinBinPanelLabel.Name = "lblSinBinPlayer" & numberinSinBin
                SinBinPanelLabel.Text = colour & " " & txtSinBin.Text
                SinBinPanelLabel.BackColor = Color.White
                SinBinPanelLabel.BorderStyle = BorderStyle.FixedSingle
                SinBinPanelLabel.TextAlign = ContentAlignment.MiddleCenter
                Octopush.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlSinBin").Controls.Add(SinBinPanelLabel)
                Dim SinBinPanelTime As New Label
                SinBinPanelTime.Name = "lblSinBinTime" & numberinSinBin
                SinBinPanelTime.Text = "0" & txtSinBinTime.Text & ":00"
                SinBinPanelTime.BackColor = Color.White
                SinBinPanelTime.BorderStyle = BorderStyle.FixedSingle
                SinBinPanelTime.TextAlign = ContentAlignment.MiddleCenter
                tempDate = TimeOfDay
                tempDate = tempDate.AddMinutes(txtSinBinTime.Text)
                SinBinPanelTime.Tag = tempDate

                Octopush.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlSinBin").Controls.Add(SinBinPanelTime)
                sizeSinBinLabels(numberinSinBin)
            End If
            Me.Close()
        End If
    End Sub
    Public Sub sizeSinBinLabels(currentlabel As Integer)
        Dim SinBinPanel = Octopush.Controls("TabControl1").Controls("tabCurrentGame").Controls("pnlSinBin")
        Dim SinBin = SinBinPanel.Controls("lblSinBinPlayer" & currentlabel)
        Dim SinBinTime = SinBinPanel.Controls("lblsinBinTime" & currentlabel)
        SinBin.Size = New Size(Octopush.halfPanelWidth, Octopush.panelRowHeight)
        SinBin.Location = New Point(Octopush.column1CurrentGameXPosition, 5 + (currentlabel) * (5 + Octopush.panelRowHeight))
        SinBinTime.Size = New Size(Octopush.halfPanelWidth, Octopush.panelRowHeight)
        SinBinTime.Location = New Point(Octopush.column1CurrentGameXPosition + 10 + Octopush.halfPanelWidth, 5 + (currentlabel) * (5 + Octopush.panelRowHeight))
        ''''
    End Sub

End Class