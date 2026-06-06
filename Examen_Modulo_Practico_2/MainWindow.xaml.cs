using System.Windows;

namespace Examen_Modulo_Practico_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Inicio());
        }

        private void b_Inicio_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Inicio());
        }

        private void b_Pasivos_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pasivos());
        }

        private void b_Activos_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Activos());
        }

        private void b_Entradas_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Entradas());
        }

        private void b_Salidas_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Salidas());
        }

        private void b_Alimentacion_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Alimentacion());
        }
    }
}
