Imports System.Management
Public Class Form1
    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Dim mclass As New ManagementClass("WmiMonitorBrightnessMethods")
        mclass.Scope = New ManagementScope("\\.\root\wmi")
        Dim instances As ManagementObjectCollection = mclass.GetInstances

        For Each instance As ManagementObject In instances
            Dim timeout As ULong = 1
            Dim brightness As UShort = CUShort(TrackBar1.Value * 10)
            Dim args As Object() = New Object() {timeout, brightness}
            instance.InvokeMethod("WmiSetBrightness", args)
        Next
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Try
            If Me.WindowState = FormWindowState.Minimized Then
                Me.Visible = False
                NotifyIcon1.Visible = True
                NotifyIcon1.Icon = SystemIcons.Application
                NotifyIcon1.ShowBalloonTip(1, "Brightness Control", "Running Minimized", ToolTipIcon.Info)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Try
            Me.Visible = True
            Me.WindowState = FormWindowState.Normal
            NotifyIcon1.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        Try
            Me.Visible = True
            Me.WindowState = FormWindowState.Normal
            NotifyIcon1.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

End Class
