using System;
using System.Collections.Generic;
using System.IO;

namespace ATMApp {
class Program
{
    static void Main()
    {
        Dictionary<string, string> users = new Dictionary<string, string>
        {
            { "Armagan", "123456" },
            { "armia", "673453" }
        };

        List<string> transactions = new List<string>();
        List<string> failedAttempts = new List<string>();

        bool userAuthenticated = false;
        while (!userAuthenticated)
        {
            Console.Write("Kullanıcı adı: ");
            string username = Console.ReadLine();
            Console.Write("Şifre: ");
            string password = Console.ReadLine();

            if (users.ContainsKey(username) && users[username] == password)
            {
                Console.WriteLine("Giriş başarılı.");
                userAuthenticated = true;

                bool continueRunning = true;
                while (continueRunning)
                {
                    Console.WriteLine("Yapmak istediğiniz işlemi seçin:");
                    Console.WriteLine("1 - Para çekme");
                    Console.WriteLine("2 - Para yatırma");
                    Console.WriteLine("3 - Ödeme yapma");
                    Console.WriteLine("4 - Gün sonu raporu");
                    Console.WriteLine("5 - Çıkış");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            transactions.Add("Para çekme işlemi yapıldı.");
                            Console.WriteLine("Para çekme işlemi gerçekleştirildi.");
                            break;
                        case "2":
                            transactions.Add("Para yatırma işlemi yapıldı.");
                            Console.WriteLine("Para yatırma işlemi gerçekleştirildi.");
                            break;
                        case "3":
                            transactions.Add("Ödeme yapıldı.");
                            Console.WriteLine("Ödeme işlemi gerçekleştirildi.");
                            break;
                        case "4":
                            GenerateEndOfDayReport(transactions, failedAttempts);
                            break;
                        case "5":
                            continueRunning = false;
                            Console.WriteLine("Çıkış yapılıyor...");
                                Console.ReadKey();
                                break;
                        default:
                            Console.WriteLine("Geçersiz seçenek. Tekrar deneyin.");
                            break;
                    }
                }
            }
            else
            {
                failedAttempts.Add($"Başarısız giriş denemesi: {DateTime.Now}");
                Console.WriteLine("Kullanıcı adı veya şifre yanlış. Tekrar deneyin.");
            }
        }

        Console.WriteLine("Program sonlandırılıyor...");
        Console.ReadKey();
    }

    static void GenerateEndOfDayReport(List<string> transactions, List<string> failedAttempts)
    {
        string fileName = $"EOD_{DateTime.Now:ddMMyyyy}.txt";
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine("Gün Sonu Raporu - " + DateTime.Now.ToShortDateString());
            writer.WriteLine();

            writer.WriteLine("İşlemler:");
            foreach (string transaction in transactions)
            {
                writer.WriteLine(transaction);
            }

            writer.WriteLine();
            writer.WriteLine("Hatalı Giriş Denemeleri:");
            foreach (string failedAttempt in failedAttempts)
            {
                writer.WriteLine(failedAttempt);
            }
        }
        Console.WriteLine($"Gün sonu raporu '{fileName}' dosyasına yazıldı.");
        Console.WriteLine("Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }
}
}