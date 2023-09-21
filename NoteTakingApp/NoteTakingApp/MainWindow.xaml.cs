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

namespace NoteTakingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> Notes;
        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists("SavedNotes.txt")) Notes = File.ReadAllLines("SavedNotes.txt").ToList();
            else Notes = new List<string>();
        }
        private void DisplayNotes(object sender, RoutedEventArgs e)
        {
            DisplayNotes newDisplayNotes = new DisplayNotes(Notes);
            newDisplayNotes.Show();
        }
        private void AddNote(object sender, RoutedEventArgs e)
        {
            AddNote newAddNote = new AddNote(Notes);
            newAddNote.Show();
        }
        private void Quit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
