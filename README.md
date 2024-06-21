Certainly! Here's a structured README file content that you can include in your GitHub repository for your Visual Basic Windows Desktop application:

---

# Slidely Desktop Application

This repository contains a Windows Desktop application built using Visual Basic with functionality to manage submissions through a graphical user interface.

## Features

- **Create New Submission Form**: Allows users to enter details such as Name, Email, Phone Number, GitHub repo link, and stopwatch time. Includes a stopwatch functionality that can be paused and resumed.
  
- **View Submissions Form**: Displays all submitted forms with navigation buttons for "Previous" and "Next" to browse through entries. Supports keyboard shortcuts for navigation (`Ctrl + P` for Previous and `Ctrl + N` for Next).
  
- **Additional Functionality**:
  - **Deletion**: Users can delete submitted forms.
  - **Editing**: Allows users to edit existing submissions.

## Requirements

- **Visual Studio**: Developed using Visual Studio IDE.
- **Visual Basic**: Language used for development.
- **.NET Framework**: Ensure .NET Framework is installed on the machine.
- **Express Server**: Backend server running on Node.js with Express framework.

## Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/slidely-desktop.git
   ```

2. **Open Project in Visual Studio**:
   - Navigate to the cloned directory `slidely-desktop`.
   - Open `SlidelyDesktopApp.sln` file in Visual Studio.

3. **Run Backend Server**:
   - Navigate to the `backend` directory.
   - Install dependencies:
     ```bash
     npm install
     ```
   - Start the server:
     ```bash
     npm start
     ```
   - Ensure the backend server is running on `http://localhost:3000`.

4. **Build and Run the Application**:
   - Build the solution in Visual Studio.
   - Start the application to launch the main form.

## Usage

- **Create New Submission**:
  - Click on "Create New Submission" button or press `Ctrl + N`.
  - Enter details and use the stopwatch functionality.
  - Submit the form.

- **View Submissions**:
  - Click on "View Submissions" button or press `Ctrl + V`.
  - Navigate through submissions using "Previous" (`Ctrl + P`) and "Next" (`Ctrl + N`) buttons.
  - Edit or delete submissions as needed.

## Contributors

- [LOHITH R](https://github.com/lohithlohi)
