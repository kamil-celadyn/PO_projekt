using System;
using System.Text.Json.Serialization;

namespace SystemWynagrodzen
{
    // Punkt 3: Dziedziczenie
    public class PracownikEtatowy : Pracownik, IBonusowalny
    {
        public decimal PensjaZasadnicza { get; set; }

        [JsonConstructor]
        public PracownikEtatowy(string imie, string nazwisko, string pesel, decimal pensjaZasadnicza)
            : base(imie, nazwisko, pesel)
        {
            if (pensjaZasadnicza < 0) throw new KadryException("Pensja nie może być ujemna!");
            PensjaZasadnicza = pensjaZasadnicza;
        }

        public override decimal ObliczPensje() => PensjaZasadnicza;

        public override string PobierzOpis() => base.PobierzOpis() + " [UMOWA O PRACĘ]";

        public void PrzyznajPremie(decimal kwota) => PensjaZasadnicza += kwota;
    }
}