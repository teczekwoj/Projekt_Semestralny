using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

/// <summary>
/// Logika interakcji dla klasy MainWindow.xaml
/// </summary>

namespace Generator_kluczy
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            UILista.ItemsSource = Itemy;

            Aktualizuj();
        }

        public List<Dane> Itemy { get; set; } = new List<Dane>();

        private void Aktualizuj()
        {
            Itemy.Clear();
            using (BazaDanych db = new BazaDanych())
            {
                foreach (var klucz in db.Keys.ToList())
                {
                    Itemy.Add(new Dane() { Klucz = klucz.Key, Produkt = db.Product.Single(produkt => produkt.ID == klucz.ProductID).Name, Status = db.Status.Single(status => status.ID == klucz.StatusID).Name });
                }
            }
            UILista.Items.Refresh();
        }

        private void Nowy(object sender, RoutedEventArgs e)
        {
            var generator = new Random();
            var slownik = "abcdefghijklmnoperstuw0123456789";

            using (BazaDanych db = new BazaDanych())
            {
                db.Add(new Keys() { Key = string.Join("", "XXXXX-XXXXX-XXXX".Select(c => c == 'X' ? char.ToUpper(slownik[generator.Next(0, slownik.Length)]) : c)), ProductID = 1, StatusID = 1 });
                db.SaveChanges();
            }

            Aktualizuj();
        }

        private void Usun(object sender, RoutedEventArgs e)
        {
            if (UILista.SelectedItem == null) return;

            var wybrany = UILista.SelectedItem as Dane;

            using (BazaDanych db = new BazaDanych())
            {
                var klucz = db.Keys.Where(klucz => klucz.Key.Equals(wybrany.Klucz)).Single();

                db.Remove(db.Keys.Where(klucz => klucz.Key.Equals(wybrany.Klucz)).Single());
                db.Deleted.Add(new Deleted() { Key = klucz.Key, ProductID = klucz.ProductID, StatusID = klucz.StatusID });
                db.SaveChanges();
            }

            Aktualizuj();
        }

        private void Status(object sender, RoutedEventArgs e)
        {
            if (UILista.SelectedItem == null) return;

            var wybrany = UILista.SelectedItem as Dane;

            using (BazaDanych db = new BazaDanych())
            {
                var klucz = db.Keys.Where(klucz => klucz.Key.Equals(wybrany.Klucz)).Single();

                klucz.StatusID = klucz.StatusID >= db.Status.Max(status => status.ID) ? 1 : klucz.StatusID + 1;

                db.Update(klucz);
                db.SaveChanges();
            }

            Aktualizuj();
        }

        private void Produkt(object sender, RoutedEventArgs e)
        {
            if (UILista.SelectedItem == null) return;

            var wybrany = UILista.SelectedItem as Dane;

            using (BazaDanych db = new BazaDanych())
            {
                var klucz = db.Keys.Where(klucz => klucz.Key.Equals(wybrany.Klucz)).Single();

                klucz.ProductID = klucz.ProductID >= db.Product.Max(product => product.ID) ? 1 : klucz.ProductID + 1;

                db.Update(klucz);
                db.SaveChanges();
            }

            Aktualizuj();
        }

        private void Usuniete(object sender, RoutedEventArgs e)
        {
            var dane = new List<Dane>();

            using (BazaDanych db = new BazaDanych())
            {
                foreach (var klucz in db.Deleted.ToList())
                {
                    dane.Add(new Dane() { Klucz = klucz.Key, Produkt = db.Product.Single(produkt => produkt.ID == klucz.ProductID).Name, Status = db.Status.Single(status => status.ID == klucz.StatusID).Name });
                }
            }

            var okno = new OknoUsunietych(dane);
            okno.ShowDialog();
        }
    }

    public class Dane
    {
        public string Klucz { get; set; }
        public string Produkt { get; set; }
        public string Status { get; set; }
    }
}
