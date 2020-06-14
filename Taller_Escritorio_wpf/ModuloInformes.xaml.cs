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
using Taller_Negocio;

namespace Taller_Escritorio_wpf
{
    /// <summary>
    /// Lógica de interacción para ModuloInformes.xaml
    /// </summary>
    public partial class ModuloInformes : Window
    {
        public ModuloInformes()
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

        private void Btn_Menu_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MenuPrincipal ventana = new MenuPrincipal();
            ventana.ShowDialog();
        }

        public void CargarComboBox()
        {

            Compartido_Negocio comunaN = new Compartido_Negocio();

            //////////////LISTAR EMPLEADO/////////////////
            cmb_Empl_info.ItemsSource = comunaN.ListarEmpleado();
            cmb_Empl_info.DisplayMemberPath = "p_nombre_empleado";
            cmb_Empl_info.SelectedValuePath = "id_empleado";

            //////////////LISTAR PROVEEDOR/////////////////
            cmb_prov_info.ItemsSource = comunaN.ListarProveedor();
            cmb_prov_info.DisplayMemberPath = "razon_social_prov";
            cmb_prov_info.SelectedValuePath = "id_proveedor";



        }
    }
}
