using Cinema.UI.Infrastructure;
using System.Windows.Input;
using Cinema.UI.Views;
using System.Windows;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Cinema.BLL.DTO;
using System.Text;
using System.Security.Cryptography;
using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Cinema.UI.ViewModels
{
    public class SignInViewModel : BaseNotifyPropertyChanged
    {
        private IPAddress address = IPAddress.Parse("127.0.0.1");
        private int port = 30000;

        public UserDTO User { get; set; }
        public AdminDTO Admin { get; set; }

        #region commands
        public ICommand SignInCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (HandleClient() == "1")
                    {
                        var signInWindow = obj as SignInWindow;
                        var mainWindow = new MainWindow(User);
                        mainWindow.Show();
                        signInWindow.Close();
                    }
                    else if (HandleClient() == "2")
                    {
                        var signInWindow = obj as SignInWindow;
                        var adminWindow = new AdminWindow(Admin);
                        adminWindow.Show();
                        signInWindow.Close();
                    }
                    else
                    {
                        MessageBox.Show("User not found. Try again.");
                    }
                });
            }
        }

        public ICommand SignUpCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var signInWindow = obj as SignInWindow;
                    var signUpWindow = new SignUpWindow();
                    signUpWindow.Show();
                    signInWindow.Close();
                });
            }
        }
        #endregion

        #region properties
        public string Login { get; set; }
        public string Password { get; set; }
        #endregion

        #region methods
        public SignInViewModel()
        {
        }

        private string GetEncryptedPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        private string HandleClient()
        {
            var user = new UserDTO { UserLogin = Login, UserPassword = Password };
            var client = new TcpClient();
            client.Connect(address, port);

            var ns = client.GetStream();
            var sw = new StreamWriter(ns);
            var sr = new StreamReader(ns);
            var textReader = new JsonTextReader(sr);
            var jsonSerializer = new JsonSerializer();

            var obj = JsonConvert.SerializeObject(new { RequestType = "sign-in", Data = user });
            jsonSerializer.Serialize(sw, obj);
            sw.Flush();

            var json = jsonSerializer.Deserialize(textReader).ToString();
            var jsonSearch = JObject.Parse(json);
            var response = int.Parse(jsonSearch["Response"].ToString());

            if (response == 1)
            {
                User = jsonSearch["Data"].ToObject<UserDTO>();
                client.Close();
                return "1";
            }
            else if (response == 2)
            {
                Admin = jsonSearch["Data"].ToObject<AdminDTO>();
                client.Close();
                return "2";
            }

            client.Close();
            return null;
        }

        #endregion
    }
}
