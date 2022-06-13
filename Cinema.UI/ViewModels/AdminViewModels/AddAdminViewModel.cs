using Cinema.BLL.DTO;
using Cinema.BLL.Services;
using Cinema.DAL.Context;
using Cinema.DAL.Repositories;
using Cinema.UI.Infrastructure;
using Cinema.UI.Views;
using Cinema.UI.Views.AdminViews;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cinema.UI.ViewModels.AdminViewModels
{
    public class AddAdminViewModel : BaseNotifyPropertyChanged
    {
        private IPAddress address;
        private int port;
        public UserDTO NewUser { get; set; }

        public ICommand AddAdminCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (HandleClient())
                    {
                       if (MessageBoxResult.OK == MessageBox.Show($"Admin added."))
                       {
                            var addAdminWindow = obj as AddAdminWindow;
                            addAdminWindow.Close();
                       }
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

            var obj = JsonConvert.SerializeObject(new { RequestType = "post-admin", Data = NewUser });

            jsonSerializer.Serialize(sw, obj);
            sw.Flush();

            var json = jsonSerializer.Deserialize(textReader).ToString();
            var jsonSearch = JObject.Parse(json);
            var response = int.Parse(jsonSearch["Response"].ToString());

            client.Close();

            if (response == 1) return true;

            return false;
        }

        public AddAdminViewModel()
        {
            address = IPAddress.Parse(ConfigurationManager.AppSettings["ServerDefaultIp"]);
            port = int.Parse(ConfigurationManager.AppSettings["ServerDefaultPort"]);

            NewUser = new UserDTO();
        }
    }
}
