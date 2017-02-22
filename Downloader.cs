using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Web;
using CoreTweet;

namespace TweetDownloaderSharp
{
    class Downloader
    {
        private WebClient client = new WebClient();
        private Tokens tokens;

        public Downloader(Tokens tokens)
        {
            this.tokens = tokens;
        }

        public void Download(string query, Options options)
        {
            HashSet<long> idSet = new HashSet<long>();
            WebClient webClient = new WebClient();

            // search metadata를 조사해가면서 계속 처리해가야 함.
            SearchResult result = null;
            long? max_id = null;
            int itemCount = 100;
            do
            {
                result = tokens.Search.Tweets(query, count: itemCount, max_id: max_id);

                foreach (Status status in result)
                {
                    if (status.Entities.Media != null)
                    {
                        int statusRt = (status.RetweetCount.HasValue ? status.RetweetCount.Value : 0);
                        if (statusRt >= options.RetweetCount)
                        {
                            // search로 얻어온 media 정보에는 하나밖에 표시되지 않으므로 원본 트윗을 가져와야 함.
                            MediaEntity media = status.Entities.Media[0];
                            long sourceId = status.Id;
                            if (media.SourceStatusId.HasValue)
                            {
                                sourceId = media.SourceStatusId.Value;
                            }

                            if (!idSet.Contains(sourceId))
                            {
                                StatusResponse response = tokens.Statuses.Show(sourceId);

                                if (!options.Silence)
                                {
                                    string statusUrl = response.Entities.Media[0].Url;
                                    Console.WriteLine("downloading tweet media from {0} (rt count: {1})", statusUrl, statusRt);
                                }

                                int mediaIndex = 1;
                                foreach (MediaEntity media_ in response.Entities.Media)
                                {
                                    string medialUrl = media_.MediaUrlHttps;
                                    string filename = string.Format("{0}-{1}{2}", sourceId, mediaIndex, Path.GetExtension(medialUrl));
                                    string filepath = filename;
                                    if (!string.IsNullOrEmpty(options.DownloadPath))
                                    {
                                        if (!Directory.Exists(options.DownloadPath))
                                        {
                                            Directory.CreateDirectory(options.DownloadPath);
                                        }
                                        filepath = Path.Combine(options.DownloadPath, filename);
                                    }

                                    if (!File.Exists(filepath))
                                    {
                                        if (!options.Silence)
                                        {
                                            Console.WriteLine("\tsaving {0} to {1}", medialUrl, filepath);
                                        }
                                        webClient.DownloadFile(medialUrl, filepath);
                                    }
                                    else
                                    {
                                        if (!options.Silence)
                                        {
                                            Console.WriteLine("\t{0} already exists", filepath);
                                        }
                                    }

                                    mediaIndex++;
                                }

                                idSet.Add(sourceId);
                            }
                        }
                    }
                }

                SearchMetadata metadata = result.SearchMetadata;
                if (metadata.NextResults != null)
                {
                    var nvc = HttpUtility.ParseQueryString(metadata.NextResults);
                    max_id = long.Parse(nvc.Get("max_id"));
                    itemCount = int.Parse(nvc.Get("count"));
                }
                else
                {
                    max_id = null;
                }
            }
            while (max_id.HasValue);
        }
    }
}
