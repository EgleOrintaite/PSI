using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Windows;
using System.Windows.Controls;
using System;
using System.IO;

namespace NoteTakingApp
{
    public class Middleware
    {
        private readonly MainWindow mainWindow;
        private readonly string logFilePath = "user_actions.log";

        public Middleware(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            // Ensure the log file exists
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }
        }

        public bool AuthenticateUser()
        {
            LogAction("User attempted authentication");
            var loginWindow = new LoginWindow();
            return loginWindow.ShowDialog() ?? false;
        }

        public void ChangeUser()
        {
            if (!AuthenticateUser())
            {
                return;
            }

            Properties.Settings.Default.Reload();
            mainWindow.Author = Properties.Settings.Default.SavedUsername;

            mainWindow.dbContext = new NoteDbContext();
            mainWindow.Notes.Clear();
            var newNotes = mainWindow.LoadUserNotes(mainWindow.Author);
            foreach (var note in newNotes)
            {
                mainWindow.Notes.Add(note);
            }

            LogAction($"User changed to {mainWindow.Author}");
        }

        public void AddNote()
        {
            var newAddNote = new AddNote(mainWindow, mainWindow.dbContext);
            newAddNote.Show();

            LogAction($"User added a new note");
        }

        public void HandleNotesCardClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.DataContext is Note selectedNote)
            {
                var noteWindow = new NoteWindow(mainWindow, selectedNote);
                noteWindow.Show();
                mainWindow.Visibility = Visibility.Collapsed;
            }

            LogAction($"User viewed a note");
        }

        private void LogAction(string action)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mainWindow.Author}: {action}";
            File.AppendAllLines(logFilePath, new[] { logEntry });
        }
    }
}
