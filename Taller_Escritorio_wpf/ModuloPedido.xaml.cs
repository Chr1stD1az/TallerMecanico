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
    /// Lógica de interacción para ModuloPedido.xaml
    /// </summary>
    public partial class ModuloPedido : Window
    {
        public ModuloPedido()
        {
            InitializeComponent();
        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void BotonCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Btn_pedidos_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Btn_recep_p_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Agregar_p_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MenuPrincipal ventana = new MenuPrincipal();
            ventana.ShowDialog();
        }
    }
  



}
