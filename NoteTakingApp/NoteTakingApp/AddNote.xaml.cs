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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace NoteTakingApp
{
    public partial class AddNote : Window
    {
        private ObservableCollection<Note> Notes;
        private MainWindow mainWindow;

        public AddNote(ObservableCollection<Note> notes, MainWindow mainwindow)
        {
            InitializeComponent();
            Notes = notes;
            mainWindow = mainwindow;
        }

        private void AddNewNote(object sender, RoutedEventArgs e)
        {
            string author = authorTextBox.Text.Trim();
            string content = noteContentTextBox.Text.Trim();
            string theme = themeTextBox.Text.Trim();

            if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(content) || string.IsNullOrEmpty(theme))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int noteNumber = Notes.Count + 1;
            Note newNote = new Note(noteNumber, author, theme, content);
            Notes.Add(newNote);

            mainWindow.SaveNotesToFile();

            Close();
        }
    }
}
