using System;
using NLog.Web;
using NLog;

namespace NLog_Project
{   
    class MainClass
    {
        Logger logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            new MainClass().StartBank();
        }

        public void StartBank()
        {   
            var account = new BankAccount("Vinicio", 1000000);
            Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance}.\n");
            logger.Info("Created an account for Vinicio");

            account.MakeWithdrawal(120, DateTime.Now, "Hammock");
            logger.Info("Hammock withdraw 120");
            Console.WriteLine($"Account {account.Number} withdrew 120 for hammock\n");

            Console.WriteLine(account.GetAccountHistory());

            Console.WriteLine($"\nNew account balance: {account.Balance}");
            logger.Info($"Current balance is {account.Balance}");
        }
    }
}
