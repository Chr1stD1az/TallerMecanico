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
using Taller_Datos;
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
            CargarComboBox();
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
            Compartido_Negocio CompN = new Compartido_Negocio();

            //////////////LISTAR EMPLEADO INFORME PEDIDO/////////////////
            cmb_Empl_info.ItemsSource = CompN.ListarEmpleado();
            cmb_Empl_info.DisplayMemberPath = "p_nombre_empleado";
            cmb_Empl_info.SelectedValuePath = "id_empleado";

            //////////////LISTAR PROVEEDOR INFORME PEDIDO/////////////////
            cmb_prov_info.ItemsSource = CompN.ListarProveedor();
            cmb_prov_info.DisplayMemberPath = "razon_social_prov";
            cmb_prov_info.SelectedValuePath = "id_proveedor";

            //////////////LISTAR FAMILIA INFORME PEDIDO/////////////////
            cmb_Familia_prod_info.ItemsSource = CompN.ListarFamProd();
            cmb_Familia_prod_info.DisplayMemberPath = "descr_familia";
            cmb_Familia_prod_info.SelectedValuePath = "id_familia_prod";

            //////////////LISTAR TIPO DOCUMENTO VENTA/////////////////
            cmb_Tipo_Doc_info_vta.ItemsSource = CompN.ListarDocumentos();
            cmb_Tipo_Doc_info_vta.DisplayMemberPath = "descr_documento";
            cmb_Tipo_Doc_info_vta.SelectedValuePath = "id_documento";


            //////////////LISTAR SERVICIO VENTA/////////////////
            cmb_Servicio_info_vta.ItemsSource = CompN.ListarServicios();
            cmb_Servicio_info_vta.DisplayMemberPath = "desc_servicio";
            cmb_Servicio_info_vta.SelectedValuePath = "id_servicio";


        }

        private void cmb_Familia_prod_info_DropDownClosed(object sender, EventArgs e)
        {
            if (cmb_Familia_prod_info.Text == "")
            {
                MessageBox.Show("Seleccionar Familiar de producto");
            }
            else
            {
                Compartido_Negocio comp = new Compartido_Negocio();
                List<Tipo_Producto_dto> respuesta = new List<Tipo_Producto_dto>();

                int familiaCmb = int.Parse(cmb_Familia_prod_info.SelectedValue.ToString());
                respuesta = comp.ListarTipoProd(familiaCmb);
                cmb_Tipo_Prod_info.ItemsSource = respuesta;
                cmb_Tipo_Prod_info.DisplayMemberPath = "descr_tipo_prod";
                cmb_Tipo_Prod_info.SelectedValuePath = "id_tipo_prod";

            }
        }

        private void cmb_Tipo_Prod_info_DropDownClosed(object sender, EventArgs e)
        {
            Producto_Negocio Prod_Nego = new Producto_Negocio();
            List<Producto_dto> respuesta = new List<Producto_dto>();

            if (cmb_Tipo_Prod_info.Text != "")
            {
                string  producto_buscar ="";
                int tipo_prod = int.Parse(cmb_Tipo_Prod_info.SelectedValue.ToString());
                respuesta = Prod_Nego.ListarProducto(tipo_prod, producto_buscar);
                cmb_Producto_info.ItemsSource = respuesta;
                cmb_Producto_info.DisplayMemberPath = "descr_producto";
                cmb_Producto_info.SelectedValuePath = "id_producto";
                
            }
            else
            {
                MessageBox.Show("Debe seleccionar tipo de producto");
            }
        }
    }
}
