namespace SystemWynagrodzen
{
    public class Zleceniobiorca : Pracownik
    {
        public decimal StawkaGodzinowa { get; set; }
        public int LiczbaGodzin { get; set; }

        public Zleceniobiorca(string imie, string nazwisko, string pesel, decimal stawka, int godziny)
            : base(imie, nazwisko, pesel)
        {
            if (stawka < 0 || godziny < 0) throw new KadryException("Stawka i godziny muszą być dodatnie!");
            StawkaGodzinowa = stawka;
            LiczbaGodzin = godziny;
        }

        public override decimal ObliczPensje() => StawkaGodzinowa * LiczbaGodzin;

        public override string PobierzOpis() => base.PobierzOpis() + " [UMOWA ZLECENIE]";
    }
}