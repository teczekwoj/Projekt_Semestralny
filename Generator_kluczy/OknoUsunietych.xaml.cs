using System.Collections.Generic;
using System.Windows;

namespace Generator_kluczy
{
    /// <summary>
    /// Logika interakcji dla klasy OknoUsunietych.xaml
    /// </summary>
    public partial class OknoUsunietych : Window
    {
        public OknoUsunietych(List<Dane> dane)
        {
            InitializeComponent();

            UILista.ItemsSource = dane;
            UILista.Items.Refresh();
        }
    }
}
