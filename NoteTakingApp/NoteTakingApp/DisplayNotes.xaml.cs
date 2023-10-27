using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NoteTakingApp
{
    public partial class DisplayNotes : Window
    {
        private ObservableCollection <Note> Notes;
        private string sortedColumn;
        private bool ascendingOrder;

        public DisplayNotes(ObservableCollection<Note> notes)
        {
            InitializeComponent();

            Notes = notes;
            sortedColumn = "";
            ascendingOrder = true;
            DisplayNotesInListBox(Notes);
        }

        private void DisplayNotesInListBox(ObservableCollection<Note> notes)
        {
            notesListBox.Items.Clear();
            foreach (Note note in notes)
            {
                notesListBox.Items.Add(note.Number.ToString() + ": " + note.Author + " - " + note.Theme + ". " + note.Content);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var keyword = searchTextBox.Text;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var matchingNotes = new ObservableCollection<Note>(Notes.Where(note => note.Theme.Contains(keyword)).ToList());
                DisplayNotesInListBox(matchingNotes);
            }
            else
            {
                DisplayNotesInListBox(Notes);
            }
        }

        private void RevertButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayNotesInListBox(Notes);
            searchTextBox.Text = "";
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var columnName = button.Tag.ToString();

            if (sortedColumn == columnName)
            {
                ascendingOrder = !ascendingOrder;
            }
            else
            {
                sortedColumn = columnName;
                ascendingOrder = true;
            }

            switch (columnName)
            {
                case "Number":
                    Notes = new ObservableCollection<Note>(ascendingOrder
                        ? Notes.OrderBy(note => note.Number)
                        : Notes.OrderByDescending(note => note.Number));
                    break;
                case "Author":
                    Notes = new ObservableCollection<Note>(ascendingOrder
                        ? Notes.OrderBy(note => note.Author)
                        : Notes.OrderByDescending(note => note.Author));
                    break;
                case "Theme":
                    Notes = new ObservableCollection<Note>(ascendingOrder
                        ? Notes.OrderBy(note => note.Theme)
                        : Notes.OrderByDescending(note => note.Theme));
                    break;
            }


            DisplayNotesInListBox(Notes);
        }
        private void notesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedIndex = notesListBox.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < Notes.Count)
            {
                var selectedNote = Notes[selectedIndex];

                var noteDetails = $"Number: {selectedNote.Number}\nAuthor: {selectedNote.Author}\nTheme: {selectedNote.Theme}\nContent: {selectedNote.Content}";

                var noteWindow = new NoteWindow(noteDetails);
                noteWindow.ShowDialog();
            }
        }

    }
}