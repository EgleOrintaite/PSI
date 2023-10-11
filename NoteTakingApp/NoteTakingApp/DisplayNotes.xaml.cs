using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;

namespace NoteTakingApp
{
    public partial class DisplayNotes : Window
    {
        private ObservableCollection<Note> Notes;

        public DisplayNotes(ObservableCollection<Note> notes)
        {
            InitializeComponent();

            Notes = notes;
            foreach (Note note in Notes)
            {
                notesListBox.Items.Add(note.Number.ToString() + ": " + note.Author + " - " + note.Theme + ". " + note.Content);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = searchTextBox.Text;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                ObservableCollection<Note> matchingNotes = new ObservableCollection<Note>( Notes.Where(note => note.Theme.Contains(keyword)).ToList() );

                notesListBox.Items.Clear();
                foreach (Note note in matchingNotes)
                {
                    notesListBox.Items.Add(note.Number.ToString() + ": " + note.Author + " - " + note.Theme + ". " + note.Content);
                }
            }
            else
            {
                // If the search box is empty, show all notes
                notesListBox.Items.Clear();
                foreach (Note note in Notes)
                {
                    notesListBox.Items.Add(note.Number.ToString() + ": " + note.Author + " - " + note.Theme + ". " + note.Content);
                }
            }
        }

        private void RevertButton_Click(object sender, RoutedEventArgs e)
        {
            notesListBox.Items.Clear();
            foreach (Note note in Notes)
            {
                notesListBox.Items.Add(note.Number.ToString() + ": " + note.Author + " - " + note.Theme + ". " + note.Content);
            }
            searchTextBox.Text = "";
        }
    }
}
