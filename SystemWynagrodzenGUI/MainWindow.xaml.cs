using System;
using System.Windows;
using System.Windows.Controls;

namespace SystemWynagrodzen
{
    public partial class MainWindow : Window
    {
        private SystemKadrowy _system = new SystemKadrowy();
        private const string PlikDanych = "baza.json";

        public MainWindow()
        {
            InitializeComponent();
            OdswiezListe();
        }

        private void OdswiezListe()
        {
            lstPracownicy.Items.Clear();
            foreach (var p in _system.PobierzWszystkich())
            {
                lstPracownicy.Items.Add($"{p.PobierzOpis()} | Wypłata: {p.ObliczPensje()} zł");
            }
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string imie = txtImie.Text;
                string nazwisko = txtNazwisko.Text;
                string pesel = txtPesel.Text;

                if (cmbTyp.SelectedIndex == 0)
                {
                    decimal pensja = decimal.Parse(txtZmienna1.Text);
                    _system.DodajPracownika(new PracownikEtatowy(imie, nazwisko, pesel, pensja));
                }
                else
                {
                    decimal stawka = decimal.Parse(txtZmienna1.Text);
                    int godziny = int.Parse(txtZmienna2.Text);
                    _system.DodajPracownika(new Zleceniobiorca(imie, nazwisko, pesel, stawka, godziny));
                }

                OdswiezListe();
                MessageBox.Show("Dodano pracownika!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                WyczyscFormularz();
            }
            catch (KadryException ex)
            {
                MessageBox.Show(ex.Message, "Błąd logiczny", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (FormatException)
            {
                MessageBox.Show("Wprowadź poprawne liczby!", "Błąd formatu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbTyp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lblZmienna1 == null) return;

            if (cmbTyp.SelectedIndex == 0)
            {
                lblZmienna1.Content = "Pensja (PLN):";
                lblZmienna2.Visibility = Visibility.Collapsed;
                txtZmienna2.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblZmienna1.Content = "Stawka za godz (PLN):";
                lblZmienna2.Visibility = Visibility.Visible;
                txtZmienna2.Visibility = Visibility.Visible;
            }
        }

        private void btnZapisz_Click(object sender, RoutedEventArgs e)
        {
            _system.ZapiszDane(PlikDanych);
            MessageBox.Show("Zapisano dane do pliku JSON.");
        }

        private void btnWczytaj_Click(object sender, RoutedEventArgs e)
        {
            _system.WczytajDane(PlikDanych);
            OdswiezListe();
        }

        private void btnSortuj_Click(object sender, RoutedEventArgs e)
        {
            _system.SortujPracownikow();
            OdswiezListe();
        }

        private void btnUsun_Click(object sender, RoutedEventArgs e)
        {
            int zaznaczonyIndeks = lstPracownicy.SelectedIndex;

            if (zaznaczonyIndeks == -1)
            {
                MessageBox.Show("Proszę zaznaczyć pracownika do usunięcia.", "Brak wyboru", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var wynik = MessageBox.Show("Czy na pewno chcesz usunąć tego pracownika?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (wynik == MessageBoxResult.Yes)
            {
                try
                {
                    _system.UsunPracownika(zaznaczonyIndeks);

                    OdswiezListe();
                    MessageBox.Show("Usunięto pomyślnie.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas usuwania: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void WyczyscFormularz()
        {
            txtImie.Clear(); txtNazwisko.Clear(); txtPesel.Clear();
            txtZmienna1.Clear(); txtZmienna2.Clear();
        }
    }
}