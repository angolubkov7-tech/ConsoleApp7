using System;

namespace Medvedev
{
    
    enum AccountType
    {
        Deposit,    // депозитный
        Credit,     // кредитный
        Settlement, // расчётный
        Savings     // сберегательный
    }

    class BankAccount
    {
        private static int nextAccountNumber = 1;

        private int accountNumber;   // номер счёта 
        private decimal balance;        // баланс 
        private AccountType type;       // тип счёта — ENUM


         private static int GenerateAccountNumber()
         {
            int number = nextAccountNumber;   // взяли текущий
            nextAccountNumber++;              // увеличили для следующего
            return number;                     // вернули уникальный
         }

        public BankAccount()
        {
            accountNumber = GenerateAccountNumber();
        }

        public int AccountNumber
        {
            get { return accountNumber; }
        }


        public decimal Balance
        {
            get { return balance; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Баланс не может быть отрицательным");
                balance = value;
            }
        }

        public AccountType Type
        {
            get { return type; }
            set { type = value; }
        }

        public void Access()
        {
            

            Console.Write("Введите баланс: ");
            decimal b = decimal.Parse(Console.ReadLine());
            
            Balance = b;

            Console.WriteLine("Выберите тип счёта:");
            Console.WriteLine("  1 — депозитный");
            Console.WriteLine("  2 — кредитный");
            Console.WriteLine("  3 — расчётный");
            Console.WriteLine("  4 — сберегательный");
            Console.Write("Ваш выбор: ");

            int choice = int.Parse(Console.ReadLine());
            

            if (choice == 1) Type = AccountType.Deposit;
            else if (choice == 2) Type = AccountType.Credit;
            else if (choice == 3) Type = AccountType.Settlement;
            else Type = AccountType.Savings;
        }

        public void Conclusion()
        {
            Console.WriteLine($"Номер счёта: {accountNumber}");
            Console.WriteLine($"Баланс: {balance:F2} ₽");
            Console.WriteLine($"Тип: {TypeToRussian(type)}");
        }

        private static string TypeToRussian(AccountType t)
        {
            if (t == AccountType.Deposit) return "депозитный";
            if (t == AccountType.Credit) return "кредитный";
            if (t == AccountType.Settlement) return "расчётный";
            if (t == AccountType.Savings) return "сберегательный";
            return "неизвестный";
        }

        public override string ToString()
        {
            return $"Счёт № {accountNumber} | тип: {TypeToRussian(type)} | баланс: {balance:F2} ₽";
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            BankAccount bank1 = new BankAccount();
            Console.WriteLine("--- Счёт № 1 ---");
            bank1.Access();
            bank1.Conclusion();
            Console.WriteLine("Через ToString(): " + bank1);

            Console.WriteLine();
            BankAccount bank2 = new BankAccount();
            Console.WriteLine("--- Счёт № 2 ---");
            bank2.Access();
            bank2.Conclusion();
            Console.WriteLine("Через ToString(): " + bank2);
        }
    }
}