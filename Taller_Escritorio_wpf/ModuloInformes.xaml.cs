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



            //////////////LISTAR ESTADO/////////////////
            cmb_estado_recep.ItemsSource = comunaN.ListarEstadoPedido();
            cmb_estado_recep.DisplayMemberPath = "desc_estado";
            cmb_estado_recep.SelectedValuePath = "id_estado";

            //////////////LISTAR EMPLEADO/////////////////
            cmb_empleado.ItemsSource = comunaN.ListarEmpleado();
            cmb_empleado.DisplayMemberPath = "p_nombre_empleado";
            cmb_empleado.SelectedValuePath = "id_empleado";

            //////////////LISTAR PROVEEDOR/////////////////
            cmb_proveedor.ItemsSource = comunaN.ListarProveedor();
            cmb_proveedor.DisplayMemberPath = "razon_social_prov";
            cmb_proveedor.SelectedValuePath = "id_proveedor";



        }
    }
}
