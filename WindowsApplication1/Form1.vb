Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadFilters(CheckedListBox1, "*.rip", Label4.Text & "\data\Patterns\")
        Label2.Text = GetSetting("PsyConvert 4", "Settings", "ISO Directory", "F:\PS3ISO2")
        Label3.Text = GetSetting("PsyConvert 4", "Settings", "JBRip Directory", "F:\c2iso2")
        Label4.Text = GetSetting("PsyConvert 4", "Settings", "Tools Directory", "F:\tools2")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        listdirs(ListBox2, Label3.Text)
        listisos(ListBox1, Label2.Text)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.Items.Count = 0 Then Exit Sub
        ExtractMultiISO(Label4.Text, " x -y -mmt=on ", Label2.Text, Label3.Text, ListBox1, ListBox2, Label1, False)
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        ChangeDIR(Label2, "F:\PS3ISO")
        If Label2.Text = "" Then Label2.Text = "F:\PS3ISO2"
        SaveSetting("PsyConvert 4", "Settings", "ISO Directory", Label2.Text)
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        ChangeDIR(Label3, "F:\c2iso")
        If Label3.Text = "" Then Label3.Text = "F:\c2iso"
        SaveSetting("PsyConvert 4", "Settings", "JBRip Directory", Label3.Text)
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        ChangeDIR(Label4, "F:\tools")
        If Label4.Text = "" Then Label4.Text = "F:\tools"
        SaveSetting("PsyConvert 4", "Settings", "Tools Directory", Label4.Text)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        loadlistboxes(Label2.Text, ListBox1, Label3.Text, ListBox2, "*.iso", Label1)
        For i = 0 To ListBox2.Items.Count - 1
            deleteFILE(Label4.Text, "\EBOOT.BIN")
            ListBox2.SelectedIndex = i
            Dim copyzfile = (Label3.Text & "\" & ListBox2.SelectedItem.ToString & "\PS3_GAME\USRDIR\EBOOT.BIN")
            copyFILE(copyzfile, Label4.Text + "\EBOOT.BIN")
            If System.IO.File.Exists(Label4.Text & "\EBOOT.BIN") Then Process_Eboot(Label4.Text, Label1)
            If System.IO.File.Exists(Label4.Text & "\EBOOT.ELF") Then deleteFILE(Label3.Text & "\" & ListBox2.SelectedItem.ToString & "\PS3_GAME\USRDIR", "\EBOOT.BIN")
            copyFILE(Label4.Text & "\EBOOT.ELF", Label3.Text & "\" & ListBox2.SelectedItem.ToString & "\PS3_GAME\USRDIR\EBOOT.BIN")
            deleteFILE(Label4.Text, "\EBOOT.ELF") : deleteFILE(Label4.Text, "\EBOOT.ELF")
        Next
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        generate_iso(ListBox2, ListBox1, Label4.Text, Label3.Text, Label2.Text, Label1)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ListBox3.Items.Clear()
        For i = 0 To CheckedListBox1.Items.Count - 1
            CheckedListBox1.SelectedIndex = i
            If CheckedListBox1.GetItemCheckState(i) = CheckState.Checked Then
                ListBox3.Items.AddRange(IO.File.ReadAllLines(CheckedListBox1.SelectedItem))
            End If
        Next
        
        For i = 0 To ListBox3.Items.Count - 1
            ListBox3.SelectedIndex = i
            Dim dirs() As String = System.IO.Directory.GetDirectories(Label3.Text)
            Dim files() As String = System.IO.Directory.GetFiles(Label3.Text, ListBox3.SelectedItem.ToString)


        Next
    End Sub
End Class
