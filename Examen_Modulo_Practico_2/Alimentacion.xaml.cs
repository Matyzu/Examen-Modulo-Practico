using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Examen_Modulo_Practico_2
{
    public partial class Alimentacion : Page
    {
        public List<ComponentInfo> Components { get; } = ComponentRepository.BySection("Alimentacion");

        public Alimentacion()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is ComponentInfo component)
            {
                NavigationService?.Navigate(new ComponentDetailPage(component));
            }
        }
    }
}
