namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var server = new Server("127.0.0.1", 30000);
            server.Start();
        }
    }
}
