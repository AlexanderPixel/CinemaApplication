using Cinema.BLL.DTO;
using Cinema.UI.Infrastructure;
using Cinema.UI.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;

namespace Cinema.UI.ViewModels
{
    public class SignUpViewModel : BaseNotifyPropertyChanged
    {
        private IPAddress address = IPAddress.Parse("127.0.0.1");
        private int port = 30000;

        public UserDTO NewUser { get; set; }

        public ICommand SignUpCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (HandleClient())
                    {
                        var signUpWindow = obj as SignUpWindow;
                        var mainWindow = new MainWindow(NewUser);
                        mainWindow.Show();
                        signUpWindow.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error occurred.");
                    }
                });
            }
        }

        private bool HandleClient()
        {
            var client = new TcpClient();
            client.Connect(address, port);

            var ns = client.GetStream();
            var sw = new StreamWriter(ns);
            var sr = new StreamReader(ns);
            var textReader = new JsonTextReader(sr);
            var jsonSerializer = new JsonSerializer();

            var obj = JsonConvert.SerializeObject(new { RequestType = "sign-up", Data = NewUser });

            jsonSerializer.Serialize(sw, obj);
            sw.Flush();

            var json = jsonSerializer.Deserialize(textReader).ToString();
            var jsonSearch = JObject.Parse(json);
            var response = int.Parse(jsonSearch["Response"].ToString());

            if (response == 1)
            {
                NewUser = jsonSearch["Data"].ToObject<UserDTO>();
                return true;
            }

            client.Close();
            return false;
        }

        public SignUpViewModel()
        {
            NewUser = new UserDTO();
        }
    }
}
