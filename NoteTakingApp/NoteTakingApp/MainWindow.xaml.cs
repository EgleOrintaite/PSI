﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace NoteTakingApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Note> Notes;

        public MainWindow()
        {
            InitializeComponent();
            Notes = LoadNotesFromFile();
            NoteVisibilityToggle(Notes);
        }

        private void DisplayNotes(object sender, RoutedEventArgs e)
        {
            var newDisplayNotes = new DisplayNotes(Notes);
            newDisplayNotes.Show();
        }
        private void ClearNotes(object sender, RoutedEventArgs e)
        {
            Notes.Clear();
            NoteVisibilityToggle(Notes);
        }

        private void AddNote(object sender, RoutedEventArgs e)
        {
            var newAddNote = new AddNote(Notes, this);
            newAddNote.Show();
        }

        public void NoteVisibilityToggle(ObservableCollection<Note> Notes)
        {
            if (Notes.Count > 0) pageDisplay.Visibility = Visibility.Visible;
            else pageDisplay.Visibility = Visibility.Hidden;
        }

        private ObservableCollection<Note> LoadNotesFromFile()
        {
            ObservableCollection<Note> loadedNotes = new ObservableCollection<Note>();
            string filePath = "SavedNotes.txt";

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i += 4)
                {
                    int number = int.Parse(lines[i]);
                    string author = lines[i + 1];
                    string theme = lines[i + 2];
                    string content = lines[i + 3];

                    Note note = new Note(number, author, theme, content);
                    loadedNotes.Add(note);
                }
            }

            return loadedNotes;
        }

        public void SaveNotesToFile()
        {
            ObservableCollection<string> linesToWrite = new ObservableCollection<string>();

            foreach (Note note in Notes)
            {
                linesToWrite.Add(note.Number.ToString());
                linesToWrite.Add(note.Author);
                linesToWrite.Add(note.Theme);
                linesToWrite.Add(note.Content);
            }

            File.WriteAllLines("SavedNotes.txt", linesToWrite);
        }
    }
}
