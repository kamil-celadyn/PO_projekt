using System;
using System.Text.Json.Serialization;

namespace SystemWynagrodzen
{
    public class Zleceniobiorca : Pracownik
    {
        public decimal StawkaGodzinowa { get; set; }
        public int LiczbaGodzin { get; set; }

        [JsonConstructor]
        public Zleceniobiorca(string imie, string nazwisko, string pesel, decimal stawkaGodzinowa, int liczbaGodzin)
            : base(imie, nazwisko, pesel)
        {
            if (stawkaGodzinowa < 0 || liczbaGodzin < 0) throw new KadryException("Stawka i godziny muszą być dodatnie!");
            StawkaGodzinowa = stawkaGodzinowa;
            LiczbaGodzin = liczbaGodzin;
        }

        public override decimal ObliczPensje() => StawkaGodzinowa * LiczbaGodzin;

        public override string PobierzOpis() => base.PobierzOpis() + " [UMOWA ZLECENIE]";
    }
}