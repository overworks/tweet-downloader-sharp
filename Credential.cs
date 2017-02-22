using System;
using System.IO;
using System.Diagnostics;
using CoreTweet;
using Newtonsoft.Json;

namespace TweetDownloaderSharp
{
    class Credential
    {
        const string CONSUMER_KEY = "<your consumer key>";
        const string CONSUMER_SECRET = "<your consumer secret>";

        const string TOKEN_FILE_NAME = "token.json";

        class AccessTokens
        {
            public string ACCESS_TOKEN { get; set; }
            public string ACCESS_TOKEN_SECRET { get; set; }
        }

        public static Tokens GetTokens()
        {
            Tokens tokens = null;

            if (File.Exists(TOKEN_FILE_NAME))
            {
                using (StreamReader reader = File.OpenText(TOKEN_FILE_NAME))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    AccessTokens at = (AccessTokens)serializer.Deserialize(reader, typeof(AccessTokens));
                    tokens = Tokens.Create(Credential.CONSUMER_KEY, Credential.CONSUMER_SECRET, at.ACCESS_TOKEN, at.ACCESS_TOKEN_SECRET);
                }
            }
            else
            {
                OAuth.OAuthSession session = OAuth.Authorize(Credential.CONSUMER_KEY, Credential.CONSUMER_SECRET);
                Uri authorizeUri = session.AuthorizeUri;
                Process.Start(authorizeUri.AbsoluteUri);
                Console.Write("Input PIN: ");
                string pin = Console.ReadLine();
                tokens = OAuth.GetTokens(session, pin);

                AccessTokens at = new AccessTokens() { ACCESS_TOKEN = tokens.AccessToken, ACCESS_TOKEN_SECRET = tokens.AccessTokenSecret };
                using (StreamWriter writer = File.CreateText(TOKEN_FILE_NAME))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, at);
                }
            }

            return tokens;
        }
    }
}
