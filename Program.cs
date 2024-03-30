using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        PhoneBook phoneBook = new PhoneBook();
        phoneBook.AddDefaultContacts();

        while (true)
        {
            Console.WriteLine("*******************************************");
            Console.WriteLine("* Lütfen yapmak istediğiniz işlemi seçiniz:");
            Console.WriteLine("*******************************************");
            Console.WriteLine("* (1) Yeni Numara Kaydetmek");
            Console.WriteLine("* (2) Varolan Numarayı Silmek");
            Console.WriteLine("* (3) Varolan Numarayı Güncelleme");
            Console.WriteLine("* (4) Rehberi Listelemek");
            Console.WriteLine("* (5) Rehberde Arama Yapmak");
            Console.WriteLine("* (6) Çıkış");

            Console.Write("Seçiminizi yapınız (1-6): ");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Geçersiz giriş. Lütfen tekrar deneyin.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    phoneBook.AddContact();
                    break;
                case 2:
                    phoneBook.DeleteContact();
                    break;
                case 3:
                    phoneBook.UpdateContact();
                    break;
                case 4:
                    phoneBook.ListContacts();
                    break;
                case 5:
                    phoneBook.SearchContacts();
                    break;
                case 6:
                    Console.WriteLine("Programdan çıkılıyor...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }
        }
    }
}

class PhoneBook
{
    private Dictionary<string, string> contacts = new Dictionary<string, string>();

    public void AddDefaultContacts()
    {
        contacts.Add("John Doe", "1234567890");
        contacts.Add("Jane Smith", "9876543210");
        contacts.Add("Michael Johnson", "4561237890");
        contacts.Add("Emily Davis", "7894561230");
        contacts.Add("David Brown", "3216549870");
    }

    public void AddContact()
    {
        Console.WriteLine("Lütfen isim giriniz:");
        string name = Console.ReadLine();

        Console.WriteLine("Lütfen soyisim giriniz:");
        string surname = Console.ReadLine();

        Console.WriteLine("Lütfen telefon numarası giriniz:");
        string phoneNumber = Console.ReadLine();

        contacts.Add(name + " " + surname, phoneNumber);
        Console.WriteLine("Kişi rehbere eklendi.");
    }

    public void DeleteContact()
    {
        Console.WriteLine("Lütfen numarasını silmek istediğiniz kişinin adını ya da soyadını giriniz:");
        string searchTerm = Console.ReadLine();

        var contactToRemove = contacts.FirstOrDefault(x => x.Key.ToLower().Contains(searchTerm.ToLower()));

        if (contactToRemove.Equals(default(KeyValuePair<string, string>)))
        {
            Console.WriteLine("Aradığınız kriterlere uygun veri rehberde bulunamadı.");
            return;
        }

        contacts.Remove(contactToRemove.Key);
        Console.WriteLine($"{contactToRemove.Key} isimli kişi rehberden silindi.");
    }

    public void UpdateContact()
    {
        Console.WriteLine("Lütfen numarasını güncellemek istediğiniz kişinin adını ya da soyadını giriniz:");
        string searchTerm = Console.ReadLine();

        var contactToUpdate = contacts.FirstOrDefault(x => x.Key.ToLower().Contains(searchTerm.ToLower()));

        if (contactToUpdate.Equals(default(KeyValuePair<string, string>)))
        {
            Console.WriteLine("Aradığınız kriterlere uygun veri rehberde bulunamadı.");
            return;
        }

        Console.WriteLine($"Lütfen {contactToUpdate.Key} için yeni telefon numarası giriniz:");
        string newPhoneNumber = Console.ReadLine();

        contacts[contactToUpdate.Key] = newPhoneNumber;
        Console.WriteLine($"{contactToUpdate.Key} isimli kişinin numarası güncellendi.");
    }

    public void ListContacts()
    {
        Console.WriteLine("Telefon Rehberi");
        Console.WriteLine("**********************************************");
        foreach (var contact in contacts.OrderBy(x => x.Key))
        {
            Console.WriteLine($"isim: {contact.Key} Telefon Numarası: {contact.Value}");
        }
    }

    public void SearchContacts()
    {
        Console.WriteLine("Arama yapmak istediğiniz tipi seçiniz:");
        Console.WriteLine("**********************************************");
        Console.WriteLine("* İsim veya soyisime göre arama yapmak için: (1)");
        Console.WriteLine("* Telefon numarasına göre arama yapmak için: (2)");

        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Geçersiz giriş. Lütfen tekrar deneyin.");
            return;
        }

        switch (choice)
        {
            case 1:
                Console.WriteLine("Lütfen aramak istediğiniz ismi veya soyismi giriniz:");
                string searchName = Console.ReadLine().ToLower();
                var nameResults = contacts.Where(x => x.Key.ToLower().Contains(searchName));
                if (nameResults.Count() == 0)
                {
                    Console.WriteLine("Aranan isim veya soyisim rehberde bulunamadı.");
                    return;
                }
                Console.WriteLine("Arama Sonuçlarınız:");
                Console.WriteLine("**********************************************");
                foreach (var contact in nameResults)
                {
                    Console.WriteLine($"isim: {contact.Key} Telefon Numarası: {contact.Value}");
                }
                break;
            case 2:
                Console.WriteLine("Lütfen aramak istediğiniz telefon numarasını giriniz:");
                string searchNumber = Console.ReadLine();
                var numberResult = contacts.FirstOrDefault(x => x.Value == searchNumber);
                if (numberResult.Equals(default(KeyValuePair<string, string>)))
                {
                    Console.WriteLine("Aranan telefon numarası rehberde bulunamadı.");
                    return;
                }
                Console.WriteLine("Arama Sonucunuz:");
                Console.WriteLine("**********************************************");
                Console.WriteLine($"isim: {numberResult.Key} Telefon Numarası: {numberResult.Value}");
                break;
            default:
                Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                break;
        }
    }
}
