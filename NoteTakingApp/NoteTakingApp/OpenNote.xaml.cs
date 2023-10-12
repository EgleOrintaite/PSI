﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace NoteTakingApp
{
    /// <summary>
    /// Interaction logic for OpenNote.xaml
    /// </summary>
    public partial class OpenNote : Window
    {
        private ObservableCollection<Note> Notes;
        public OpenNote(ObservableCollection<Note> notes)
        {
            InitializeComponent();
            Notes = notes;
        }
    }
}
