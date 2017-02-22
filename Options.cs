using System;

namespace TweetDownloaderSharp
{
    class Options
    {
        public bool Silence { get; private set; }
        public string DownloadPath { get; private set; }
        public int RetweetCount { get; private set; }
        public string ScreenName { get; private set; }

        public static Options MakeOptions(string[] args)
        {
            if (args.Length == 0)
            {
                throw new InvalidArguementException();
            }

            Options options = new Options();

            try
            {
                int index = 0;
                while (index < args.Length - 1)
                {
                    switch (args[index])
                    {
                        case "-d":
                        case "--directory":
                            index++;
                            options.DownloadPath = args[index];
                            break;

                        case "-rt":
                        case "--retweet":
                            index++;
                            // 파싱이 안되면 여기서 예외발생.
                            options.RetweetCount = int.Parse(args[index]);
                            break;

                        case "-s":
                        case "--silence":
                            options.Silence = true;
                            break;

                        case "-sn":
                        case "--screen_name":
                            index++;
                            options.ScreenName = args[index];
                            break;

                        default:
                            throw new InvalidArguementException();
                    }

                    index++;
                }
            }
            catch (FormatException)
            {
                throw new InvalidArguementException();
            }

            return options;
        }
    }
}
