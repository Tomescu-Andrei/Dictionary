using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar_2._0
{
    internal class UserList
    {
        public ObservableCollection<User> users { get; set; }

        public UserList() {
            users = new ObservableCollection<User>();
        }


        public void deserializeUsers()
        {
            using (StreamReader reader = new StreamReader("users.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var parts = line.Split('/');
                        if (parts.Length > 1)
                        {
                            var username = parts[0];
                            var password = parts[1];

                            users.Add(new User { id = username ,password=password});
                        }
                    }

                }

            }
        }
    }
}
