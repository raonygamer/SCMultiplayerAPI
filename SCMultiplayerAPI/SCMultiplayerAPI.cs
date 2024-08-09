using NetCoreServer;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Saturn
{
    public class SCMultiplayerAPI
    {
        #region Singleton & Main  
        private static SCMultiplayerAPI? _Instance = null;
        public static SCMultiplayerAPI Instance => _Instance ??= new SCMultiplayerAPI();
        static void Main(string[] _args) => Start(_args).GetAwaiter().GetResult();
        #endregion

        static async Task Start(string[] args)
        {

            await Task.Delay(-1);
        }
    }
}
    