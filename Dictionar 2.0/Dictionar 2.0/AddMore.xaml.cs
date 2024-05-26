using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace Dictionar_2._0
{
    
    public partial class AddMore : Window
    {
        public ObservableCollection<string> DistinctCategories { get; set; }
        SavingActions act;
        public List<Word> AllWords { get; set; }

        Word aux = new Word();

        public AddMore()
        {
            InitializeComponent();
            act = new SavingActions();
            act.DeserializeObject("output.txt");
            AllWords = new List<Word>();  
            for (int i = 0;i<act.Words.Count;i++)
                AllWords.Add(act.Words[i]);

            //act.DeserializeObject("output.txt");

            DistinctCategories = new ObservableCollection<string>(AllWords.Select(word => word.category).Distinct());
            categoryList.ItemsSource = DistinctCategories;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstTxt.Text) || string.IsNullOrWhiteSpace(categoryBox.Text) || string.IsNullOrWhiteSpace(lastTxt.Text))
            {
                MessageBox.Show("Toate câmpurile sunt obligatorii. Vă rugăm să completați toate informațiile.", "Avertisment", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }

            //update word
            aux.word = firstTxt.Text;
            aux.description = lastTxt.Text;
            aux.category = categoryBox.Text;
            imageforword.Source=null;

            act.UpdateWord(aux);

            aux = null;
            aux = new Word();
            firstTxt.Text = "";
            lastTxt.Text=null;
            categoryBox.Text = "";

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstTxt.Text) || string.IsNullOrWhiteSpace(categoryBox.Text) || string.IsNullOrWhiteSpace(lastTxt.Text))
            {
                MessageBox.Show("Toate câmpurile sunt obligatorii. Vă rugăm să completați toate informațiile.", "Avertisment", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // add word
            aux.word = firstTxt.Text;
            aux.description = lastTxt.Text;
            aux.category = categoryBox.Text;

            // Verificați dacă categoria nu există deja în lista
            if (!DistinctCategories.Contains(aux.category))
            {
                DistinctCategories.Add(aux.category);
                categoryList.ItemsSource = DistinctCategories;
            }
            AllWords.Add(aux);
            act.SaveWord(aux);
            aux = null;
            aux = new Word();
            firstTxt.Text = "";
            lastTxt.Text = null;
            categoryBox.Text = "";

        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // delete function
            
            aux.word = firstTxt.Text;
            aux.description = lastTxt.Text;
            aux.category = categoryBox.Text;
            act.DeleteWord(aux);
            AllWords.Remove(aux);
            aux = null;
            firstTxt.Text = "";
            lastTxt.Text = null;
            categoryBox.Text = "";

        }

        private void categoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (categoryList.SelectedItem != null)
            {
                categoryBox.Text = categoryList.SelectedItem.ToString();
            }
        }

        
        public Word SearchWord(string searchTerm)
        {
            act.DeserializeObject("output.txt");
            return act.Words.FirstOrDefault(word => word.word.Equals(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                
                act.SaveImage(openFileDialog.FileName, aux);

               
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                imageforword.Source = bitmap;
            }
        }


    }
}


