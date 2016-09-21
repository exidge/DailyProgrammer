using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Threading;

//Description: 
//https://www.reddit.com/r/dailyprogrammer/comments/53ijnb/20160919_challenge_284_easy_wandering_fingers/

namespace DP_284_EASY_SWYPE
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> wordsList = fromLocal().Where(e => e.Length >= 5).ToList();
            string[] testCases = {
                "qwertyuytresdftyuioknn",
                "gijakjthoijerjidsdfnokg"
            };
            Stopwatch sw = new Stopwatch();
            long totalElapsedTime = 0;
            foreach (string testCase in testCases)
            {
                sw.Start();
                Console.WriteLine("For test case: " + testCase + " found matching words:");
                List<string> charWords = wordsList.Where(e => e[0].Equals(testCase[0])).ToList();
                foreach (string word in charWords)
                {
                    if (isMatching(testCase, word))
                        Console.WriteLine(word);
                }
                sw.Stop();
                totalElapsedTime += sw.ElapsedMilliseconds;
                Console.WriteLine("Time Elapsed for this word in ms:" + sw.ElapsedMilliseconds);
                sw.Reset();
            }
            Console.WriteLine("Time Elapsed for all test cases: " + totalElapsedTime + "ms");
            Console.ReadKey();
        }
        private static bool isMatching(string input, string testWord)
        {
            if (!input[input.Length - 1].Equals(testWord[testWord.Length - 1]))
                return false;
            int charPosition = 0;
            for (int i = 1; i < testWord.Length; i++)
            {
                charPosition = input.IndexOf(testWord[i], charPosition);
                if (charPosition == -1)
                {
                    return false;
                }
            }
            return true;
        }
        //Not working, remote server always returns 403
        //private static List<string> fromWeb()
        //{
        //    WebClient client = new WebClient();
        //    string downloadedContent = client.DownloadString("http://norvig.com/ngrams/enable1.txt");
        //    return downloadedContent.Split('\n').ToList();
        //}
        private static List<string> fromLocal()
        {
            return File.ReadAllLines("enable1.txt").ToList();
        }

    }
}
