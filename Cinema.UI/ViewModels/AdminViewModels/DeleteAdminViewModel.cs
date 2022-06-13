using Cinema.BLL.DTO;
using Cinema.UI.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cinema.UI.ViewModels.AdminViewModels
{
    public class DeleteAdminViewModel : BaseNotifyPropertyChanged
    {
        private ObservableCollection<UserDTO> userDTOs;

        public ObservableCollection<UserDTO> Users 
        {
            get => userDTOs;
            set
            {
                userDTOs = value;
                NotifyPropertyChanged();
            }
        }

        public UserDTO SelectedUser { get; set; }

        public DeleteAdminViewModel()
        {
            Task.Run(() =>
            {
                LoadAdmins();
            });
        }

        public ICommand DeleteUserCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedUser == null)
                    {
                        MessageBox.Show("You need to choose user to delete.");
                    }
                    else
                    {

                    }
                });
            }
        }

        private void LoadAdmins()
        {
            var address = IPAddress.Parse(ConfigurationManager.AppSettings["ServerDefaultIp"]);
            var port = int.Parse(ConfigurationManager.AppSettings["ServerDefaultPort"]);

            var client = new TcpClient();
            client.Connect(address, port);

            var ns = client.GetStream();
            var sw = new StreamWriter(ns);
            var sr = new StreamReader(ns);
            var textReader = new JsonTextReader(sr);
            var jsonSerializer = new JsonSerializer();

            var obj = JsonConvert.SerializeObject(new { RequestType = "get-admins" });
            jsonSerializer.Serialize(sw, obj);
            sw.Flush();

            var json = jsonSerializer.Deserialize(textReader).ToString();
            var jsonSearch = JObject.Parse(json);
            var response = int.Parse(jsonSearch["Response"].ToString());

            if (response == 1)
            {
                Users = new ObservableCollection<UserDTO>(jsonSearch["Data"].ToObject<List<UserDTO>>());
            }

            client.Close();
        }
    }
}
