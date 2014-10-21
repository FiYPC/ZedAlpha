Imports System.ComponentModel

Module Controls
    Public Sub copyFILE(Src As String, Dest As String)
        System.IO.File.Copy(Src, Dest)
    End Sub

    Public Sub deleteFILE(location As String, file As String)
        On Error GoTo ender
        System.IO.File.Delete(location + file)
ender:
    End Sub

    Public Sub createDirectory(location As String)
        System.IO.Directory.CreateDirectory(location)
    End Sub

    Public Sub ExtractMultiISO(ziploc As String, zipargs As String, SRCfolder As String, DESTfolder As String, ISOListbox As Object, JBListbox As Object, statusbar As Object, processeboot As Boolean)
        For i = 0 To ISOListbox.Items.Count - 1
            ISOListbox.selectedindex = i
            statusbar.text = "Extracting......["
            Dim p As New Process
            p.StartInfo.CreateNoWindow = True
            p.EnableRaisingEvents = True
            p.StartInfo.UseShellExecute = False
            p.StartInfo.Arguments = zipargs + Chr(34) & ISOListbox.GetItemText(ISOListbox.SelectedItem) & Chr(34) & " -o" & Chr(34) & Replace(Replace(ISOListbox.GetItemText(ISOListbox.SelectedItem), SRCfolder, DESTfolder), ".iso", "") & Chr(34)
            p.StartInfo.FileName = ziploc + "\7z.exe"
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            p.Start()
            p.WaitForExit()
            statusbar.Text = "ISO Extraction Completed"
            If processeboot = True Then
                MsgBox("process eboot")
            End If
        Next i
    End Sub

    Public Sub Process_Eboot(SCELocation As String, statusbar As Object)
        statusbar.text = "Processing Eboot..."
        Dim p As New Process
        p.StartInfo.CreateNoWindow = True
        p.StartInfo.UseShellExecute = False
        p.StartInfo.Arguments = " --decrypt .\EBOOT.BIN .\EBOOT.ELF"
        p.StartInfo.FileName = SCELocation & "\scetool.exe"
        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        p.StartInfo.WorkingDirectory = SCELocation + "\"
        p.Start()
        p.WaitForExit()
        statusbar.text = "Eboot Processed..."
    End Sub

    Public Sub deletejbrip(isofile As String, jbfolder2del As String, statusbar As Object)
        If System.IO.File.Exists(isofile) = True Then
            statusbar.text = isofile & " found, deleting: -[ " & jbfolder2del & " ]-"
            System.IO.Directory.Delete(jbfolder2del, True)
            Exit Sub
        ElseIf System.IO.File.Exists(isofile) = False Then
            statusbar.text = isofile & " not found - [delete skipped]"
            Exit Sub
        End If
    End Sub

    Public Sub generate_iso(jblistbox As Object, isolistbox As Object, tooldir As String, destfolder As String, sourcefolder As String, statusbar As Object)
        For i = 0 To jblistbox.items.count - 1
            jblistbox.selectedindex = i
            statusbar.Text = "Generating [" & Replace(isolistbox.getitemtext(isolistbox.selecteditem.ToString), sourcefolder, destfolder) & "]"
            Dim p As New Process
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            p.StartInfo.CreateNoWindow = True
            p.StartInfo.UseShellExecute = False
            p.StartInfo.Arguments = " " & Chr(34) & jblistbox.GetItemText(jblistbox.SelectedItem) & Chr(34) & " " & Chr(34) & sourcefolder & "\" & Replace(jblistbox.getitemtext(jblistbox.selecteditem.ToString), sourcefolder, destfolder) & ".iso" & Chr(34)
            p.StartInfo.FileName = tooldir & "\genps3iso.exe"
            p.StartInfo.WorkingDirectory = tooldir & "\"
            Application.DoEvents()
            p.Start()
            p.WaitForExit()
            Application.DoEvents()
            statusbar.Text = "Generating [" & Replace(isolistbox.getitemtext(isolistbox.selecteditem.ToString), sourcefolder, destfolder) & "]"
        Next
        statusbar.Text = "Completed ISO Generation..."
    End Sub

    Public Sub loadlistboxes(ISODir As String, ISOListbox As Object, JBDir As String, JBListbox As Object, filefilter As String, statusbar As Object)
        Dim folder As New IO.DirectoryInfo(ISODir)
        Dim isos = folder.GetFiles(filefilter)
        With ISOListbox
            .ValueMember = "FullName"
            .DisplayMember = "FullName"
            .DataSource = isos
        End With
        statusbar.text = "-[ " & ISOListbox.items.count & " ] iso's and [ " & JBListbox.items.count & " ] JB Folders found."
    End Sub

    Public Sub ChangeDIR(NewDir As Object, CANCELDIR As String)
        Dim p As New FolderBrowserDialog
        If DialogResult.Cancel = True Then NewDir.text = CANCELDIR
        p.ShowDialog()
        NewDir.Text = p.SelectedPath
    End Sub

    Public Sub LoadFilters(Filterlist As Object, filter As String, Location As String)
        Filterlist.items.clear()
        Dim newfolderinfo As New IO.DirectoryInfo(Location)
        Dim arrfilesinfolder() As IO.FileInfo
        arrfilesinfolder = newfolderinfo.GetFiles(filter)
        For Each fileinfolder In arrfilesinfolder
            Filterlist.items.add(Location + fileinfolder.Name)
            Application.DoEvents()
        Next
    End Sub

    Public Sub listdirs(lister As Object, path As String)
        Dim folder As New IO.DirectoryInfo(path)
        With lister
            .ValueMember = "FullName"
            .DisplayMember = "FullName"
            .DataSource = folder.GetDirectories()
        End With
    End Sub

    Public Sub listisos(lister As Object, path As String)
        Dim folder As New IO.DirectoryInfo(path)
        Dim isos = folder.GetFiles("*.iso")
        With lister
            .ValueMember = "FullName"
            .DisplayMember = "FullName"
            .DataSource = isos
        End With
    End Sub


End Module
