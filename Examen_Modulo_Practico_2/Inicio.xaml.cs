using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Linq;

namespace Examen_Modulo_Practico_2
{
    public partial class Inicio : Page
    {
        public Inicio()
        {
            InitializeComponent();
            // ensure initial theme is dark (as App.xaml merged DarkTheme by default)
            ThemeManager.ApplyTheme("Dark");
        }

        private void Pasivos_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Pasivos());
        }

        private void Activos_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Activos());
        }

        private void Entradas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Entradas());
        }

        private void Salidas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Salidas());
        }

        private void Alimentacion_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Alimentacion());
        }

        private void Pasivos_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UpdatePreview("Pasivos", "Controlan, almacenan o filtran energia sin amplificar. Son la base para limitar corriente, dividir voltaje y estabilizar senales.", "Componente destacado: Resistencia", "Assets/Components/resistencia.png");
        }

        private void Activos_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UpdatePreview("Activos", "Necesitan polarizacion o alimentacion para controlar corriente, amplificar senales o procesar informacion dentro del circuito.", "Componente destacado: Circuito integrado 555", "Assets/Components/circuito-integrado-555.png");
        }

        private void Entradas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UpdatePreview("Entrada", "Permiten que el circuito reciba informacion del ambiente o del usuario mediante sensores, botones y modulos de lectura.", "Componente destacado: Sensor ultrasonico HC-SR04", "Assets/Components/sensor-ultrasonico-hc-sr04.png");
        }

        private void Salidas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UpdatePreview("Salida", "Transforman una senal electrica en luz, sonido, movimiento o informacion visual para mostrar una respuesta del sistema.", "Componente destacado: Servomotor", "Assets/Components/servomotor.png");
        }

        private void Alimentacion_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UpdatePreview("Alimentacion", "Entregan, regulan o protegen la energia del proyecto para que los demas componentes trabajen de forma estable.", "Componente destacado: Regulador buck LM2596", "Assets/Components/regulador-buck-lm2596.png");
        }

        private void UpdatePreview(string title, string description, string component, string imagePath)
        {
            PreviewTitle.Text = title;
            PreviewDescription.Text = description;
            PreviewComponent.Text = component;
            PreviewImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteSearch(SearchBox.Text);
        }

        private void SearchBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                ExecuteSearch(SearchBox.Text);
            }
        }

        private void ExecuteSearch(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return;

            // Check if matches a category
            string q = query.Trim();
            if (string.Equals(q, "Pasivos", StringComparison.OrdinalIgnoreCase) || string.Equals(q, "Pasivo", StringComparison.OrdinalIgnoreCase))
            {
                NavigationService?.Navigate(new Pasivos());
                return;
            }
            if (string.Equals(q, "Activos", StringComparison.OrdinalIgnoreCase) || string.Equals(q, "Activo", StringComparison.OrdinalIgnoreCase))
            {
                NavigationService?.Navigate(new Activos());
                return;
            }
            if (string.Equals(q, "Entradas", StringComparison.OrdinalIgnoreCase) || string.Equals(q, "Entrada", StringComparison.OrdinalIgnoreCase))
            {
                NavigationService?.Navigate(new Entradas());
                return;
            }
            if (string.Equals(q, "Salidas", StringComparison.OrdinalIgnoreCase) || string.Equals(q, "Salida", StringComparison.OrdinalIgnoreCase))
            {
                NavigationService?.Navigate(new Salidas());
                return;
            }
            if (string.Equals(q, "Alimentacion", StringComparison.OrdinalIgnoreCase) || string.Equals(q, "Alimentación", StringComparison.OrdinalIgnoreCase))
            {
                NavigationService?.Navigate(new Alimentacion());
                return;
            }

            // Search for component name
            var results = ComponentRepository.Search(q);
            var items = new List<object>();

            // If query matches category names, add category first
            var catMatches = new List<string>();
            if ("pasivos".Contains(q, StringComparison.OrdinalIgnoreCase) || "pasivo".Contains(q, StringComparison.OrdinalIgnoreCase)) catMatches.Add("Pasivos");
            if ("activos".Contains(q, StringComparison.OrdinalIgnoreCase) || "activo".Contains(q, StringComparison.OrdinalIgnoreCase)) catMatches.Add("Activos");
            if ("entradas".Contains(q, StringComparison.OrdinalIgnoreCase) || "entrada".Contains(q, StringComparison.OrdinalIgnoreCase)) catMatches.Add("Entradas");
            if ("salidas".Contains(q, StringComparison.OrdinalIgnoreCase) || "salida".Contains(q, StringComparison.OrdinalIgnoreCase)) catMatches.Add("Salidas");
            if ("alimentacion".Contains(q, StringComparison.OrdinalIgnoreCase) || "alimentación".Contains(q, StringComparison.OrdinalIgnoreCase)) catMatches.Add("Alimentacion");

            foreach (var c in catMatches.Distinct())
                items.Add(new { Type = "Categoria", Title = c, Payload = c });

            if (results != null && results.Count > 0)
            {
                // Add components, prefer exact name first
                var exact = results.Where(cmp => string.Equals(cmp.Name, q, StringComparison.OrdinalIgnoreCase)).ToList();
                foreach (var e in exact) items.Add(new { Type = "Componente", Title = e.Name, Payload = e });

                var others = results.Where(cmp => !string.Equals(cmp.Name, q, StringComparison.OrdinalIgnoreCase));
                foreach (var o in others) items.Add(new { Type = "Componente", Title = o.Name, Payload = o });
            }

            // Bind to listbox
            SearchResultsList.ItemsSource = items;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // update search results but do not auto-navigate
            ExecuteSearch(SearchBox.Text);
            if (SearchResultsList.ItemsSource != null)
            {
                SearchPopup.IsOpen = true;
            }
            else
            {
                SearchPopup.IsOpen = false;
            }
        }

        private void SearchResultsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchResultsList.SelectedItem == null)
                return;

            dynamic sel = SearchResultsList.SelectedItem;
            string type = sel.Type;
            if (type == "Categoria")
            {
                string cat = sel.Payload;
                switch (cat)
                {
                    case "Pasivos": NavigationService?.Navigate(new Pasivos()); break;
                    case "Activos": NavigationService?.Navigate(new Activos()); break;
                    case "Entradas": NavigationService?.Navigate(new Entradas()); break;
                    case "Salidas": NavigationService?.Navigate(new Salidas()); break;
                    case "Alimentacion": NavigationService?.Navigate(new Alimentacion()); break;
                }
            }
            else if (type == "Componente")
            {
                ComponentInfo comp = sel.Payload;
                NavigationService?.Navigate(new ComponentDetailPage(comp));
            }

            SearchResultsList.SelectedItem = null;
            SearchPopup.IsOpen = false;
        }

        private void ThemeToggle_Checked(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("Light");
        }

        private void ThemeToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("Dark");
        }
    }
}
