﻿using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using NLog.Web;

namespace NLog_Project
{
    class BankAccount
    {
        Logger logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        public string Number { get; }

        public string Owner { get; set; }

        public decimal Balance {
            get {

                decimal balance = 0;

                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }

        }

        private static int accountNumberSeed = 1234567890;

        private List<Transaction> allTransactions = new List<Transaction>();

        public BankAccount(string name, decimal initialBalance)
        {
            this.Owner = name;

            MakeDeposit(initialBalance, DateTime.Now, "Initial Balance");

            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;
            
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            logger.Info($"MakeDeposit : Making the deposit of {amount}");
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }

            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            logger.Info($"MakeWithdrawal : Making the withdrawal of {amount}");
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }

            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
        }

        public string GetAccountHistory()
        {
            logger.Info("Printing the history of the account");

            var report = new StringBuilder();

            // HEADER
            report.AppendLine("Date\t\tAmmount\t\tNote");
            foreach (var item in allTransactions)
            {
                // ROWS
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t\t{item.Notes}");
            }
            return report.ToString();
        }
    }
}
