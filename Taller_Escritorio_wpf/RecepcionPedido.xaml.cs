using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para RecepcionPedido.xaml
    /// </summary>
    public partial class RecepcionPedido : Window
    {
        public RecepcionPedido()
        {
            InitializeComponent();
            CargarComboBox();
            txt_fecha_recep.Text = DateTime.Now.ToString("dd/MM/yyyy");
            DP_fecha_Venc.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddDays(-1000), DateTime.Today));
            //DP_fecha_Venc.DisplayDateStart = DateTime.Now;


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
            this.Hide();
            ModuloPedido ventana = new ModuloPedido();
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

        private void Btn_recep_p_Click(object sender, RoutedEventArgs e)
        {
            //Boton Limpiar
        }

        private void btn_MenuP_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MenuPrincipal ventana = new MenuPrincipal();
            ventana.ShowDialog();
        }

        private void Btn_Busca_Ped_Click(object sender, RoutedEventArgs e)
        {

            Recepcionar_Pedido_Negocio RecepN = new Recepcionar_Pedido_Negocio();
            List<Recepcion_pedido_dto> detalle = new List<Recepcion_pedido_dto>();
            DataTable resp = new DataTable();
            Compartido_Negocio comp = new Compartido_Negocio();

            if (txt_id_pedido.Text != "")
            {
                try
                {
                    resp = RecepN.ObternerPedidoHDR(txt_id_pedido.Text);
                    if (resp.Rows.Count > 0)
                    {
                        foreach (DataRow item in resp.Rows)
                        {
                            txt_id_pedido.Text = item["ID_PEDIDO"].ToString();
                         //   txt_id_prod_recep.Text = item["ID_PRODUCTO"].ToString();
                            cmb_estado_recep.SelectedValue = item["id_estado"].ToString();
                            cmb_empleado.SelectedValue = item["id_empleado"].ToString();
                            cmb_proveedor.SelectedValue = item["id_proveedor"].ToString();
                            txt_total_recep.Text = item["id_proveedor"].ToString();
                            txt_total_recep.Text = item["TOTAL_PEDIDO"].ToString();
                            

                        }

                        detalle = RecepN.ListarDetallePedido(txt_id_pedido.Text);
                        DG_Recep.ItemsSource = detalle;
                    }
                    else
                    {
                        MessageBox.Show("ID invalido");
                       
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Busqueda no arrojó resultados");
                    throw ex;
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un ID Pedido para la busqueda");
            }


        }

        private void Btn_Confirmar_Rec_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
