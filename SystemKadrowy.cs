using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SystemWynagrodzen
{
    public class SystemKadrowy
    {
        // Punkt 1: Wykorzystanie kolekcji List<T>
        private List<Pracownik> _pracownicy = new List<Pracownik>();

        public void DodajPracownika(Pracownik p)
        {
            if (_pracownicy.Contains(p)) throw new KadryException("Pracownik z tym PESEL już istnieje!");
            _pracownicy.Add(p);
        }

        public void SortujPracownikow() => _pracownicy.Sort();

        public List<Pracownik> PobierzWszystkich() => new List<Pracownik>(_pracownicy);

        // Punkt 6: Zapis i odczyt danych (JSON)
        public void ZapiszDane(string nazwaPliku)
        {
            var opcje = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_pracownicy, opcje);
            File.WriteAllText(nazwaPliku, json);
        }

        public void WczytajDane(string nazwaPliku)
        {
            if (!File.Exists(nazwaPliku)) return;
            string json = File.ReadAllText(nazwaPliku);
            _pracownicy = JsonSerializer.Deserialize<List<Pracownik>>(json) ?? new List<Pracownik>();
        }
    }
}