Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class CreateSubmissionForm
    Private stopwatch As Stopwatch
    Private isRunning As Boolean = False
    Private WithEvents timer As Timer

    Private Sub CreateSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        stopwatch = New Stopwatch()
        timer = New Timer()
        timer.Interval = 1000 ' Update the stopwatch every second
        AddHandler timer.Tick, AddressOf Timer_Tick
    End Sub

    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.T Then
            btnToggleStopwatch.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
        End If
    End Sub

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        If isRunning Then
            stopwatch.Stop()
            timer.Stop()
        Else
            stopwatch.Start()
            timer.Start()
        End If
        isRunning = Not isRunning
        UpdateStopwatch()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        UpdateStopwatch()
    End Sub

    Private Sub UpdateStopwatch()
        txtStopwatchTime.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Async Sub SubmitFormData(name As String, email As String, phone As String, githubLink As String, stopwatchTime As String)
        Dim client As New HttpClient()
        Dim url As String = "http://localhost:3000/submit"

        Dim submission As New Dictionary(Of String, String) From {
            {"name", name},
            {"email", email},
            {"phone", phone},
            {"githublink", githubLink},
            {"stopwatchtime", stopwatchTime}
        }

        Dim json As String = JsonConvert.SerializeObject(submission)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        Try
            Dim response As HttpResponseMessage = Await client.PostAsync(url, content)

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Submission successful!")
            Else
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()
                MessageBox.Show($"Failed to submit data. Status Code: {response.StatusCode}. Response: {responseBody}")
            End If
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}")
        End Try
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim name As String = txtName.Text
        Dim email As String = txtEmail.Text
        Dim phone As String = txtPhone.Text
        Dim githubLink As String = txtGithubLink.Text
        Dim stopwatchTime As String = txtStopwatchTime.Text

        ' Validate inputs
        If String.IsNullOrWhiteSpace(name) OrElse
           String.IsNullOrWhiteSpace(email) OrElse
           String.IsNullOrWhiteSpace(phone) OrElse
           String.IsNullOrWhiteSpace(githubLink) OrElse
           String.IsNullOrWhiteSpace(stopwatchTime) Then

            MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        SubmitFormData(name, email, phone, githubLink, stopwatchTime)
    End Sub

End Class
