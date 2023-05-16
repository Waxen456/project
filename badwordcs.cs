using System;
using System.Collections.Generic;

namespace SocialMediaApp
{
    class InappropriateWordFilter
    {
        private List<string> inappropriateWords;

        public InappropriateWordFilter()
        {
            inappropriateWords = new List<string> { "bad", "offensive", "inappropriate" }; // List of inappropriate words
        }

        public bool ContainsInappropriateWord(string sentence)
        {
            foreach (string word in inappropriateWords)
            {
                if (sentence.ToLower().Contains(word.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }
    }

    class Account
    {
        public string Username { get; set; }
        public int StrikeCount { get; set; }
        public bool IsBanned { get; set; }
    }

    class SocialMediaApp
    {
        private List<Account> accounts;
        private InappropriateWordFilter wordFilter;

        public SocialMediaApp()
        {
            accounts = new List<Account>();
            wordFilter = new InappropriateWordFilter();
        }

        public void RegisterAccount(string username)
        {
            Account newAccount = new Account
            {
                Username = username,
                StrikeCount = 0,
                IsBanned = false
            };
            accounts.Add(newAccount);
            Console.WriteLine("Account registration successful.");
        }

        public void CheckSentence(string sentence, string username)
        {
            if (wordFilter.ContainsInappropriateWord(sentence))
            {
                Account account = accounts.Find(a => a.Username == username);
                account.StrikeCount++;

                if (account.StrikeCount >= 3)
                {
                    account.IsBanned = true;
                    Console.WriteLine("Account '" + account.Username + "' has been banned due to multiple rule violations.");
                }
                else
                {
                    Console.WriteLine("Warning: Inappropriate content detected. Strike " + account.StrikeCount + "/3.");
                }
            }
            else
            {
                Console.WriteLine("Sentence is clear of inappropriate content.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SocialMediaApp app = new SocialMediaApp();

            Console.WriteLine("Welcome to the Social Media App!");

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("1. Register Account");
                Console.WriteLine("2. Check Sentence");
                Console.WriteLine("3. Exit");
                Console.Write("Please select an option: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Enter username: ");
                        string username = Console.ReadLine();
                        app.RegisterAccount(username);
                        break;
                    case "2":
                        Console.Write("Enter sentence: ");
                        string sentence = Console.ReadLine();
                        Console.Write("Enter username: ");
                        string user = Console.ReadLine();
                        app.CheckSentence(sentence, user);
                        break;
                    case "3":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
