Imports System.Net.Http
Imports Newtonsoft.Json

Public Class ViewSubmissionsForm
    Private submissions As List(Of Submission)
    Private currentIndex As Integer = 0

    Private Async Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set the form's key preview property to True to capture key events
        Me.KeyPreview = True
        Await LoadSubmissions()
        If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
            DisplaySubmission(0)
        Else
            MessageBox.Show("No submissions found.")
            btnPrevious.Enabled = False
            btnNext.Enabled = False
            btnDelete.Enabled = False
            btnEdit.Enabled = False

        End If
    End Sub

    Private Sub ViewSubmissionsForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.D Then
            btnDelete.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.E Then
            btnEdit.PerformClick()
        End If
    End Sub

    Private Async Function LoadSubmissions() As Task
        Dim client As New HttpClient()
        Dim url As String = "http://localhost:3000/read_all"

        Dim response As HttpResponseMessage = Await client.GetAsync(url)

        If response.IsSuccessStatusCode Then
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()
            submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(responseBody)
        Else
            MessageBox.Show("Failed to load submissions.")
        End If
    End Function

    Private Sub DisplaySubmission(index As Integer)
        If index >= 0 AndAlso index < submissions.Count Then
            txtName.Text = submissions(index).Name
            txtEmail.Text = submissions(index).Email
            txtPhone.Text = submissions(index).Phone
            txtGithubLink.Text = submissions(index).GithubLink
            txtStopwatchTime.Text = submissions(index).StopwatchTime
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim client As New HttpClient()
        Dim url As String = $"http://localhost:3000/delete?index={currentIndex}"

        Dim response As HttpResponseMessage = Await client.DeleteAsync(url)

        If response.IsSuccessStatusCode Then
            MessageBox.Show("Submission deleted successfully!")
            Await LoadSubmissions()
            If submissions.Count > 0 Then
                If currentIndex >= submissions.Count Then
                    currentIndex = submissions.Count - 1
                End If
                DisplaySubmission(currentIndex)
            Else
                ClearSubmissionFields()
                MessageBox.Show("No submissions found.")
            End If
        Else
            MessageBox.Show("Failed to delete submission.")
        End If
    End Sub

    Private Sub ClearSubmissionFields()
        txtName.Clear()
        txtEmail.Clear()
        txtPhone.Clear()
        txtGithubLink.Clear()
        txtStopwatchTime.Clear()
    End Sub

    Private Async Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If currentIndex >= 0 AndAlso currentIndex < submissions.Count Then
            Dim editForm As New EditSubmissionForm(submissions(currentIndex), currentIndex)
            editForm.ShowDialog()

            ' Reload submissions and display the current one
            Await LoadSubmissions()
            DisplaySubmission(currentIndex)
        End If
    End Sub
End Class



Public Class Submission
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String
    Public Property GithubLink As String
    Public Property StopwatchTime As String
End Class
