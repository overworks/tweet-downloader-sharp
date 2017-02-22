// The MIT License (MIT)
//
// Copyright (c) 2017 Minhyung Park
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net;
using CoreTweet;
using Newtonsoft.Json;

namespace TweetDownloaderSharp
{
    class InvalidArguementException : ArgumentException
    { }

    class Program
    {
        enum ArgType
        {
            Query,
            DownloadPath,
            ItemCount,
            RetweetCount,
            Silence,
            ScreenName
        }

        static void Main(string[] args)
        {
            try
            {
                Options options = Options.MakeOptions(args);
                string query = args[args.Length - 1];
                
                Tokens tokens = Credential.GetTokens();
                Downloader downloader = new Downloader(tokens);
                downloader.Download(query, options);
            }
            catch (InvalidArguementException)
            {
                DisplayUsage();
            }
            catch (TwitterException e)
            {
                foreach (var error in e.Errors)
                {
                    Console.WriteLine("{0} - {1}", error.Code, error.Message);
                }
            }
        }

        /// <summary>사용법 출력... 하 누가 영어로 번역좀 해줘...</summary>
        static void DisplayUsage()
        {
            Console.WriteLine("사용법: tweet_downloader [옵션] [인수] 검색어");
            Console.WriteLine();
            Console.WriteLine("옵션:");
            Console.WriteLine("\t-d, --directory [PATH]          : 저장할 디렉토리");
            Console.WriteLine("\t-rt, --retweet [NUMBER]         : [NUMBER] 이상 리트윗된 것만 다운로드");
            Console.WriteLine("\t-s, --silence                   : 메시지를 표시하지 않음");
            Console.WriteLine("\t-sn, --screen_name [SCREEN_NAME]: 트위터 유저 [SCREEN_NAME]의 트윗 내에서 검색");
        }
    }
}
