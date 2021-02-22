using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Blogger.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Blog_Recovery_Tool
{
    public static class Auth
    {
        public static BloggerService AuthenticateOauth()
        {
            try
            {
                string credPath = Directory.GetCurrentDirectory();
                credPath = Path.Combine(credPath, "credentials.json");
                // These are the scopes of permissions you need. It is best to request only what you need and not all of them
                string[] scopes = new string[] { BloggerService.Scope.BloggerReadonly,	//View your Blogger account
                                                 BloggerService.Scope.Blogger};         //Manage your Blogger account
                UserCredential credential;
                using (var stream = new FileStream(credPath, FileMode.Open, FileAccess.Read))
                {


                    // Requesting Authentication or loading previously stored authentication for userName
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
                                                                             scopes,
                                                                             Form1.SessionId,
                                                                             CancellationToken.None,
                                                                             new FileDataStore("oauth", true)).Result;
                }
                // Create Drive API service.
                return new BloggerService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Blogger Recovery Tool"
                });
            }
            catch
            {
                return null;
            }
            return null;
        }
    }
}