using Cinema.BLL.DTO;
using Cinema.BLL.Services;
using Cinema.DAL.Context;
using Cinema.DAL.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        private IPAddress ip;
        private int port;
        private TcpListener server;

        public Server(string address, int port)
        {
            this.ip = IPAddress.Parse(address);
            this.port = port;
            server = new TcpListener(ip, port);
        }

        public void Start()
        {
            server.Start();
            Console.WriteLine($"Server started at {DateTime.Now}");

            while (true)
            {
                var client = server.AcceptTcpClient();
                Task.Run(() =>
                {
                    HandleClient(client);
                });
            }
        }

        private bool UserIsAdmin(UserDTO user)
        {
            var adminService = new AdminService(new AdminRepository(new CinemaContext()));
            var adminInDb = adminService.GetAll().FirstOrDefault(admin => admin.UserId == user.UserId);

            if (adminInDb != null) return true;

            return false;
        }

        private UserDTO HandleSignIn(JObject json)
        {
            var userSignIn = new UserDTO();
            userSignIn.UserLogin = json["Data"]["UserLogin"].ToString();
            userSignIn.UserPassword = json["Data"]["UserPassword"].ToString();

            var userService = new UserService(new UserRepository(new CinemaContext()));
            var userInDb = userService.GetAll()
                .Where(user => user.IsActive)
                .FirstOrDefault(user => user.UserLogin == userSignIn.UserLogin && user.UserPassword == userSignIn.UserPassword);

            if(userInDb != null)
            {
                if (userInDb.IsActive)
                {
                    userInDb.UserLastLoginDateTime = DateTime.Now;
                    userService.Update(userInDb);

                    return userInDb;
                }
            }

            return null;
        }

        private UserDTO HandleSignUp(JObject json)
        {
            try
            {
                var userSignUp = json["Data"].ToObject<UserDTO>();
                userSignUp.IsActive = true;
                userSignUp.UserLastLoginDateTime = DateTime.Now;

                var userService = new UserService(new UserRepository(new CinemaContext()));
                userService.Create(userSignUp);
                return userSignUp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private AdminDTO PostAdmin(JObject json)
        {
            var user = json["Data"].ToObject<UserDTO>();
            user.IsActive = true;
            user.UserLastLoginDateTime = DateTime.Now;

            var userService = new UserService(new UserRepository(new CinemaContext()));
            userService.Create(user);

            var userInDb = userService.GetAll().FirstOrDefault(u => u.UserLogin == user.UserLogin);

            var admin = new AdminDTO
            {
                UserId = userInDb.UserId,
            };

            var adminService = new AdminService(new AdminRepository(new CinemaContext()));
            adminService.Create(admin);

            return admin;
        }

        private bool DeleteUser(JObject json)
        {
            try
            {
                var user = json["Data"].ToObject<UserDTO>();
                var userService = new UserService(new UserRepository(new CinemaContext()));
                userService.Delete(userService.Get(user.UserId));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void HandleClient(TcpClient client)
        {
            Console.WriteLine($"Request from {client.Client.RemoteEndPoint} at {DateTime.Now}");

            var ns = client.GetStream();
            var sr = new StreamReader(ns);
            var sw = new StreamWriter(ns);
            var textReader = new JsonTextReader(sr);
            var jsonSerializer = new JsonSerializer();

            var json = jsonSerializer.Deserialize(textReader).ToString();
            var jsonSearch = JObject.Parse(json);
            var requestType = jsonSearch["RequestType"].ToString();

            if (requestType == "sign-in")
            {
                var user = HandleSignIn(jsonSearch);
                if (user != null)
                {
                    var isAdmin = UserIsAdmin(user);
                    var response = isAdmin ? "2" : "1";
                    jsonSerializer.Serialize(sw, new { Response = response, Data = user });
                }
                else
                {
                    var data = "User not found.";
                    jsonSerializer.Serialize(sw, new { Response = "0", Data = data});
                }
            }
            else if (requestType == "sign-up")
            {
                var user = HandleSignUp(jsonSearch);
                if (user != null) jsonSerializer.Serialize(sw, new { Response = "1", Data = user });
                else jsonSerializer.Serialize(sw, new { Response = "0", Data = "Oops. Something wrong happened. Try later." });
            }
            else if (requestType == "post-admin")
            {
                var admin = PostAdmin(jsonSearch);
                if (admin != null) jsonSerializer.Serialize(sw, new { Response = "1", Data = admin });
                else jsonSerializer.Serialize(sw, new { Response = "0", Data = "Oops. Something wrong happened. Try later." });
            }
            else if (requestType == "get-admins")
            {
                var adminService = new AdminService(new AdminRepository(new CinemaContext()));
                var userService = new UserService(new UserRepository(new CinemaContext()));
                var admins = adminService.GetAll();
                var users = userService.GetAll().Where(user => user.IsActive).Join(admins, user => user.UserId, admin => admin.UserId, (user, admin) => new
                {
                    UserId = user.UserId,
                    UserLogin = user.UserLogin,
                    UserFirstName = user.UserFirstName,
                    UserLastName = user.UserLastName,
                    UserPhone = user.UserPhone,
                    UserEmail = user.UserEmail,
                });
                
                if (users != null) jsonSerializer.Serialize(sw, new { Response = "1", Data = users });
                else jsonSerializer.Serialize(sw, new { Response = "0", Data = "Oops. Something wrong happened. Try later." });
            }
            else if (requestType == "delete-user")
            {
                var res = DeleteUser(jsonSearch);
                if (res) jsonSerializer.Serialize(sw, new { Response = "1" });
                else jsonSerializer.Serialize(sw, new { Response = "0" });
            }
            sw.Flush();
        }
    }
}
