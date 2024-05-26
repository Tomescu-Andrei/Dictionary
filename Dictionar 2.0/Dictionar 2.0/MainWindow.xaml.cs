using Microsoft.Win32;
using System;
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

namespace Dictionar_2._0
{
    public partial class MainWindow : Window
    {
        private AddMore addMoreInstance;
        private LogIn loginInstance;
        private Minigame game;
        public MainWindow()
        {
            InitializeComponent();
            loginInstance = new LogIn();  
            addMoreInstance = new AddMore();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loginInstance.ShowDialog();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            
            string searchTerm = searchBar.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                Word foundWord = addMoreInstance.SearchWord(searchTerm);
                if (foundWord != null)
                {
                    categoryBar.Text = foundWord.category;
                    suggestionCategory.Visibility = Visibility.Collapsed;
                    descrptionShow.Text = $"Meaning: {foundWord.description}";

                    // Afisati imaginea in controlul Image
                    if (!string.IsNullOrEmpty(foundWord.image))
                    {
                        try
                        {
                           
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            
                            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            
                            string imagePath = System.IO.Path.Combine(currentDirectory, foundWord.image);
                            bitmap.UriSource = new Uri(imagePath);
                            bitmap.EndInit();
                            imageforword.Source = bitmap;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Eroare la încărcarea imaginii: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        
                    }
                    else
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri("C:\\Users\\tomes\\Desktop\\MAP\\Dictionar 2.0\\Dictionar 2.0\\bin\\Debug\\Images\\default.png");
                        bitmap.EndInit();
                        imageforword.Source = bitmap;
                    }
                }
                else
                {
                    descrptionShow.Text = "Cuvântul nu a fost găsit.";
                }

            }
            else
            {
                descrptionShow.Text = "Introduceți un cuvânt în bara de căutare.";
            }
        }
        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBar.Text != "")
            {
                if (addMoreInstance != null)
                {
                    string searchTerm = searchBar.Text.Trim();

                    List<string> suggestions = addMoreInstance.AllWords
                        .Where(word => word.word.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                        .Select(word => word.word)
                        .ToList();

                    suggestionsList.ItemsSource = suggestions;
                    suggestionsList.Visibility = Visibility.Visible;
                }
            }
            else
            {
                suggestionsList.Visibility = Visibility.Collapsed;
            }
        }

        private void categoryBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (categoryBar.Text != "")
            {
                if (addMoreInstance != null)
                {
                    string searchTerm = categoryBar.Text.Trim();

                    List<string> suggestions = addMoreInstance.DistinctCategories
                        .Where(category => category.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                        .Select(category => category)
                        .ToList();

                    suggestionCategory.ItemsSource = suggestions;
                    suggestionCategory.Visibility = Visibility.Visible;
                }
            }
            else
            {
                suggestionCategory.Visibility = Visibility.Collapsed;

            }

        }

        private void suggestionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (suggestionsList.SelectedItem != null)
            {
                searchBar.Text = suggestionsList.SelectedItem.ToString();
                suggestionsList.Visibility = Visibility.Collapsed;
            }
        }
        private void suggestionCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (suggestionCategory.SelectedItem != null)
            {
                string selectedCategory = suggestionCategory.SelectedItem.ToString();
                categoryBar.Text = selectedCategory;
                suggestionCategory.Visibility = Visibility.Collapsed;

                List<string> filteredWords = addMoreInstance.AllWords
                    .Where(word => word.category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase))
                    .Select(word => word.word)  
                    .ToList();

                
                suggestionsList.ItemsSource = filteredWords;
               //suggestionsList.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            game = new Minigame();
            game.Show();
        }
    }


}
