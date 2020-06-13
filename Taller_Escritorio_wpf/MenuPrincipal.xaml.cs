using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Taller_Escritorio_wpf
{
    /// <summary>
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            lblSaludo.Content = "Bienvenid@ " + Application.Current.Properties["NombreUsuario"].ToString();
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BotonCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void BTN_Administrador_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Administrador ventana = new Administrador();
            ventana.ShowDialog();
        }

        private void BTN_admPedido_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ModuloPedido ventana = new ModuloPedido();
            ventana.ShowDialog();
        }

        private void BTN_modReservas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_modVenta_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ModuloVenta ventana = new ModuloVenta();
            ventana.ShowDialog();
        }

        private void BTN_Informes_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ModuloInformes ventana = new ModuloInformes();
            ventana.ShowDialog();
        }
    }
}
