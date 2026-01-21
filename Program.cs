using System;

namespace SystemWynagrodzen
{
    class Program
    {
        static void Main(string[] args)
        {
            SystemKadrowy system = new SystemKadrowy();
            string plikDanych = "baza_kadrowa.json";

            try { system.WczytajDane(plikDanych); } catch { }

            bool menu = true;
            while (menu)
            {
                Console.Clear();
                Console.WriteLine("=== SYSTEM KADROWY (OOP PROJECT) ===");
                Console.WriteLine("1. Dodaj Pracownika Etatowego");
                Console.WriteLine("2. Dodaj Zleceniobiorcę");
                Console.WriteLine("3. Wyświetl listę (posortowaną)");
                Console.WriteLine("4. Zapisz i Wyjdź");
                Console.Write("\nWybierz opcję: ");

                try
                {
                    string wybor = Console.ReadLine();
                    switch (wybor)
                    {
                        case "1":
                            system.DodajPracownika(new PracownikEtatowy("Adam", "Abacki", "11111111111", 6000));
                            Console.WriteLine("Dodano pomyślnie!");
                            break;
                        case "2":
                            system.DodajPracownika(new Zleceniobiorca("Beata", "Babacka", "22222222222", 50, 100));
                            Console.WriteLine("Dodano pomyślnie!");
                            break;
                        case "3":
                            system.SortujPracownikow();
                            foreach (var p in system.PobierzWszystkich())
                                Console.WriteLine($"{p.PobierzOpis()} | Wypłata: {p.ObliczPensje()} zł");
                            break;
                        case "4":
                            system.ZapiszDane(plikDanych);
                            menu = false;
                            break;
                    }
                }
                catch (KadryException ex) { Console.WriteLine($"\nBŁĄD: {ex.Message}"); }
                catch (Exception ex) { Console.WriteLine($"\nBłąd systemowy: {ex.Message}"); }

                if (menu) { Console.WriteLine("\nNaciśnij dowolny klawisz..."); Console.ReadKey(); }
            }
        }
    }
}