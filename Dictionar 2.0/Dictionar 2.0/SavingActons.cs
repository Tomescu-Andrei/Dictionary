using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Dictionar_2._0
{
    class SavingActions
    {
        public ObservableCollection<Word> Words;

        public SavingActions()
        {
            this.Words = new ObservableCollection<Word>();
        }

        public void SaveWord(Word entity)
        {
            bool found = false;
            DeserializeObject("output.txt");
            foreach (var word in Words)
            {
                if (word.word.Trim() == entity.word.Trim())
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Words.Add(entity);
                SerializeObject("output.txt");
                MessageBox.Show("Salvare reușită cu succes!!!");
            }
            else
            {
                MessageBox.Show($"Cuvântul '{entity.word}' există deja în fișier.");
            }
        }

        public void DeleteWord(Word entity)
        {
            DeserializeObject("output.txt");
            for (int i = 0; i < Words.Count; i++)
            {
                if (Words[i].word.Trim() == entity.word.Trim())
                {
                    Words.RemoveAt(i);
                    SerializeObject("output.txt");
                    MessageBox.Show("Ștergere reușită cu succes!!!");
                    break;
                }
            }
        }

        public void UpdateWord(Word updatedWord)
        {
            DeserializeObject("output.txt");
            bool wordUpdated = false;

            for (int i = 0; i < Words.Count; i++)
            {
                if (Words[i].word.Trim() == updatedWord.word.Trim())
                {

                    Words[i].description = updatedWord.description.Trim();
                    Words[i].category = updatedWord.category.Trim();
                    Words[i].image = updatedWord.image;
                    wordUpdated = true;
                    SerializeObject("output.txt");
                    MessageBox.Show("Actualizare reușită cu succes!!!");
                    break;
                }
            }

            if (!wordUpdated)
            {
                MessageBox.Show($"Cuvântul '{updatedWord.word}' nu a fost găsit în fișier.");
            }
        }

        public void DeserializeObject(string file)
        {
            Words.Clear();
            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            var termAndCategory = parts[0].Split('(');
                            if (termAndCategory.Length == 2)
                            {
                                var term = termAndCategory[0].Trim();
                                var category = termAndCategory[1].Replace(")", "").Trim();
                                var definitions = parts[1].Trim().Split('#'); 
                                if (definitions.Length == 2)
                                {
                                    var description = definitions[0].Trim();
                                    var image = definitions[1].Trim();
                                    Words.Add(new Word { word = term, category = category, description = description, image = image });
                                }
                            }
                        }
                    }
                }
            }
        }

        public void SerializeObject(string file)
        {
            using (StreamWriter writer = new StreamWriter(file))
            {
                foreach (var word in Words)
                {
                    writer.WriteLine($"{word.word}({word.category})={word.description}#{word.image}");
                }
            }
        }
        public void SerializeObject(Word word, string file)
        {
            using (StreamWriter writer = new StreamWriter(file))
            {
                foreach (var auxword in Words)
                {
                    writer.WriteLine($"{auxword.word}({auxword.category})={auxword.description}#{auxword.image}");
                }
            }
        }


        public void SaveImage(string imagePath, Word word)
        {
            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                string imagesDirectory = "../../images/";

                if (!Directory.Exists(imagesDirectory))
                {
                    Directory.CreateDirectory(imagesDirectory);
                }

                string imageName = System.IO.Path.GetFileName(imagePath);
                string destinationPath = System.IO.Path.Combine(imagesDirectory, imageName);

                File.Copy(imagePath, destinationPath, true); 

                
                word.image = destinationPath;

                SerializeObject(word, "output.txt");

                MessageBox.Show("Imaginea a fost salvată cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Calea imaginii este invalidă.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



    }
}
