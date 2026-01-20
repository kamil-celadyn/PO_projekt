namespace SystemWynagrodzen
{
    // Punkt 3: Dziedziczenie
    public class PracownikEtatowy : Pracownik, IBonusowalny
    {
        public decimal PensjaZasadnicza { get; set; }

        public PracownikEtatowy(string imie, string nazwisko, string pesel, decimal pensja)
            : base(imie, nazwisko, pesel)
        {
            if (pensja < 0) throw new KadryException("Pensja nie może być ujemna!");
            PensjaZasadnicza = pensja;
        }

        public override decimal ObliczPensje() => PensjaZasadnicza;

        public override string PobierzOpis() => base.PobierzOpis() + " [UMOWA O PRACĘ]";

        public void PrzyznajPremie(decimal kwota) => PensjaZasadnicza += kwota;
    }
}