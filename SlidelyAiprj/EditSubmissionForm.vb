Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class EditSubmissionForm
    Private submission As Submission
    Private index As Integer

    Public Sub New(submission As Submission, index As Integer)
        InitializeComponent()
        Me.submission = submission
        Me.index = index
        LoadSubmissionDetails()
    End Sub

    Private Sub LoadSubmissionDetails()
        txtName.Text = submission.Name
        txtEmail.Text = submission.Email
        txtPhone.Text = submission.Phone
        txtGithubLink.Text = submission.GithubLink
        txtStopwatchTime.Text = submission.StopwatchTime
    End Sub

    Private Async Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim name As String = txtName.Text
        Dim email As String = txtEmail.Text
        Dim phone As String = txtPhone.Text
        Dim githubLink As String = txtGithubLink.Text
        Dim stopwatchTime As String = txtStopwatchTime.Text

        Await UpdateSubmission(index, name, email, phone, githubLink, stopwatchTime)
        Me.Close()
    End Sub

    Private Async Function UpdateSubmission(index As Integer, name As String, email As String, phone As String, githubLink As String, stopwatchTime As String) As Task
        Dim client As New HttpClient()
        Dim url As String = $"http://localhost:3000/update?index={index}"

        Dim updatedSubmission As New Submission() With {
            .Name = name,
            .Email = email,
            .Phone = phone,
            .GithubLink = githubLink,
            .StopwatchTime = stopwatchTime
        }

        Dim json As String = JsonConvert.SerializeObject(updatedSubmission)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        Dim response As HttpResponseMessage = Await client.PutAsync(url, content)

        If response.IsSuccessStatusCode Then
            MessageBox.Show("Submission updated successfully!")
        Else
            MessageBox.Show("Failed to update submission.")
        End If
    End Function
End Class
