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
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace NoteTakingApp
{
    public partial class AddNote : Window
    {
        private ObservableCollection<Note> Notes;
        private MainWindow mainWindow;
        public NoteDbContext dbContext;

        public AddNote(ObservableCollection<Note> notes, MainWindow mainwindow)
        {
            InitializeComponent();
            Notes = notes;
            mainWindow = mainwindow;

            dbContext = new NoteDbContext();

            privacyComboBox.Items.Clear();
            privacyComboBox.ItemsSource = Enum.GetValues(typeof(PrivacySetting));
            privacyComboBox.SelectedItem = PrivacySetting.Private;
        }

        public void AddNewNote(object sender, RoutedEventArgs e)
        {
            var author = authorTextBox.Text.Trim();
            var content = noteContentTextBox.Text.Trim();
            var theme = themeTextBox.Text.Trim();
            var privacy = (PrivacySetting)privacyComboBox.SelectedItem;
            var tag = tagTextBox.Text.Trim();
            string namePattern = @"^[A-Za-z\s]+$";

            if (Regex.IsMatch(author, namePattern))
            {
                if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(content) || string.IsNullOrEmpty(theme))
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var noteNumber = Notes.Count + 1;
                var newNote = new Note(theme, content, privacy, tag);
                Notes.Add(newNote);
                mainWindow.SaveNotesToDatabase();
                //mainWindow.SaveNotesToFile();

                Close();

            }
            else MessageBox.Show("Invalid input. Please use only letters and spaces.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


}
