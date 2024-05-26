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
using System.Windows.Shapes;

namespace Dictionar_2._0
{
    /// <summary>
    /// Interaction logic for Minigame.xaml
    /// </summary>
    public partial class Minigame : Window
    {
        private List<Word> words;
        private Random random;
        private SavingActions savingActions;
        List<Word> selectedWords;
        List<int> selectedHints;
        string[] guess;
        int i = 0;
        int scor = 0;
        public Minigame()
        {
            InitializeComponent();
            savingActions = new SavingActions();
            savingActions.DeserializeObject("output.txt");
            words = savingActions.Words.ToList();
            random = new Random();
            selectedWords = SelectRandomWords(5);
            selectedHints = SelectRandomHints();
            guess = new string[5];
            Start(i);
        }
        public void Start(int i)
        {
            if (guess[i] != null)
            {
                if (guess[i].Length > 0)
                {
                    userInput.Text = guess[i];
                }
                else
                {
                    userInput.Clear();
                }
            }
            else
            {
                userInput.Clear();
            }
            if (i == 0)
            {
                nextButton.Visibility = Visibility.Visible;
                finishButton.Visibility = Visibility.Collapsed;
                prevButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (i == 4)
                {
                    nextButton.Visibility = Visibility.Collapsed;
                    finishButton.Visibility = Visibility.Visible;
                    prevButton.Visibility = Visibility.Visible;
                }
                else
                {
                    nextButton.Visibility = Visibility.Visible;
                    finishButton.Visibility = Visibility.Collapsed;
                    prevButton.Visibility = Visibility.Visible;
                }
            }
            minidescriere.Visibility = Visibility.Collapsed;
            miniimagine.Visibility = Visibility.Collapsed;
            if (selectedWords[i].image != "" && selectedHints[i] == 0)
            {
                ShowImage(selectedWords[i]);
            }
            else
            {
                ShowDescription(selectedWords[i]);
            }



        }

        private List<Word> SelectRandomWords(int count)
        {
            return words.OrderBy(x => random.Next()).Take(count).ToList();
        }
        private List<int> SelectRandomHints()
        {
            List<int> selectedHints = new List<int>();
            for (int j = 0; j < 5; j++)
            {
                selectedHints.Add(random.Next(2));
            }

            return selectedHints;
        }

        private void ShowImage(Word word)
        {
            
                miniimagine.Visibility = Visibility.Visible;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                // Obțineți directorul curent al aplicației
                string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                // Construiți calea completă către imagine
                string imagePath = System.IO.Path.Combine(currentDirectory, word.image);
                bitmap.UriSource = new Uri(imagePath);
                bitmap.EndInit();
                miniimagine.Source = bitmap; // Afișați imaginea asociată cu cuvântul (word.image)
           

        }


        private void ShowDescription(Word word)
        {
            minidescriere.Visibility = Visibility.Visible;
            minidescriere.Text = word.description;
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            
            guess[i] = userInput.Text;
            i++;
            Start(i);
        }

        private void finishButton_Click(object sender, RoutedEventArgs e)
        {
            guess[i] = userInput.Text;
            for (int j = 0; j < guess.Length; j++)
                if (string.Equals(guess[j], selectedWords[j].word, StringComparison.OrdinalIgnoreCase))
                {
                    scor++;

                }
            i++;

            MessageBox.Show($"Sorul tau este: {scor}");
            restartGame();
        }

        private void restartGame()
        {
            i = 0;
            scor = 0;
            guess = new string[5];
            selectedWords = SelectRandomWords(5);
            selectedHints = SelectRandomHints();
            userInput.Clear();
            Start(i);

        }

        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            guess[i] = userInput.Text;
            i--;
            if (guess[i].Length != 0)
            {
                userInput.Text = guess[i];
            }
            else
            {
                userInput.Clear();
            }
            Start(i);
        }
    }
}
