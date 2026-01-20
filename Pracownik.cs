using System;
using System.Text.Json.Serialization;

namespace SystemWynagrodzen
{
    // Punkt 4: Klasa abstrakcyjna
    // Punkt 9: Interfejsy IComparable, IEquatable, ICloneable
    [JsonDerivedType(typeof(PracownikEtatowy), typeDiscriminator: "etat")]
    [JsonDerivedType(typeof(Zleceniobiorca), typeDiscriminator: "zlecenie")]
    public abstract class Pracownik : IComparable<Pracownik>, IEquatable<Pracownik>, ICloneable
    {
        private string _pesel;
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        // Punkt 2: Właściwość z weryfikacją (Modyfikatory dostępu)
        public string Pesel
        {
            get => _pesel;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 11)
                    throw new KadryException("BŁĄD: PESEL musi składać się z 11 cyfr!");
                _pesel = value;
            }
        }

        protected Pracownik(string imie, string nazwisko, string pesel)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Pesel = pesel;
        }

        // Punkt 5: Metoda wirtualna
        public virtual string PobierzOpis() => $"{Imie} {Nazwisko} [{Pesel}]";

        // Punkt 3: Polimorfizm (Metoda abstrakcyjna)
        public abstract decimal ObliczPensje();

        // Implementacja IComparable (Punkt 9) - sortowanie po nazwisku
        public int CompareTo(Pracownik other)
        {
            if (other == null) return 1;
            return string.Compare(this.Nazwisko, other.Nazwisko, StringComparison.OrdinalIgnoreCase);
        }

        // Implementacja IEquatable (Punkt 9) - porównywanie po PESEL
        public bool Equals(Pracownik other) => Pesel == other?.Pesel;

        // Implementacja ICloneable (Punkt 9)
        public object Clone() => this.MemberwiseClone();
    }
}