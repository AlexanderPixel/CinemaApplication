using System.Net;

namespace Cinema.BLL.Managers
{
    public class NetworkManager
    {
        public string GetJson(string url)
        {
            return new WebClient().DownloadString(url);
        }
    }
}
