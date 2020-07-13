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
using Taller_Negocio;

namespace Taller_Escritorio_wpf
{
    /// <summary>
    /// Lógica de interacción para ModuloReserva.xaml
    /// </summary>
    public partial class ModuloReserva : Window
    {
        public ModuloReserva()
        {
            InitializeComponent();
           // CargarServicio();
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void BotonCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Btn_minimizar_Click(object sender, RoutedEventArgs e)
        {

            this.WindowState = WindowState.Minimized;

        }
       /* public void CargarServicio()
        {
            //////////////LISTAR SERVICIO//////////////////
            Compartido_Negocio ServicioN = new Compartido_Negocio();
            cmb_Estado.ItemsSource = ServicioN.ListarEstado();
            cmb_Estado.DisplayMemberPath = "id_descr_estado_r";
            cmb_Estado.SelectedValuePath = "id_estado_r";
        }*/

        public void LimpiarReserva()
        {
            txt_Id_Reserva.Text = "";
            txt_Id_Rut.Text = "";
            txt_Id_P_N_Cliente.Text = "";
            txt_Id_P_A_Cliente.Text = "";
            cmb_Servicio.Text = "";
            cmb_Empleado.Text = "";
            cmb_Hora.Text = "";
           // cmb_Estado.Text = "";
            fecha_Reserva.Text = "";
        }
        private void Btn_Reserva_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ModuloReserva ventana = new ModuloReserva();
            ventana.ShowDialog();
        }

        private void Btn_Buscar_reserva_Click(object sender, RoutedEventArgs e)
        {
            Reserva_Negocio reserN = new Reserva_Negocio();
            DataTable resp = new DataTable();
            Compartido_Negocio comp = new Compartido_Negocio();
            try
            {
                resp = reserN.ListarReservaPorIDCliente(txt_Id_Reserva.Text);

                if (resp.Rows.Count > 0)
                {
                    foreach (DataRow item in resp.Rows)
                    {
                        txt_Id_Reserva.Text = item["ID_RESERVA"].ToString();
                        txt_Id_Rut.Text = item["RUT_CLIENTE"].ToString();
                        txt_Id_P_N_Cliente.Text = item["P_NOMBRE_CLIENTE"].ToString();
                        txt_Id_P_A_Cliente.Text = item["P_APELLIDO_CLIENTE"].ToString();
                        cmb_Servicio.Text = item["DESC_SERVICIO"].ToString();
                        cmb_Empleado.Text = item["P_NOMBRE_EMPLEADO"].ToString();
                        cmb_Hora.Text = item["HORA_RESERVA"].ToString();
                     //   cmb_Estado.Text = item["ID_DESCR_ESTADO_R"].ToString();
                        fecha_Reserva.Text = item["FECHA_RESERVA"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("ID Reserva inválido");

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Busqueda no arrojó resultados");
                throw ex;
            }
        }

        private void Btn_Buscar_rut_Click(object sender, RoutedEventArgs e)
        {

            Reserva_Negocio reserN = new Reserva_Negocio();
            DataTable resp = new DataTable();
            Compartido_Negocio comp = new Compartido_Negocio();
            try
            {
                resp = reserN.ListarReservaPorRutCliente(txt_Id_Rut.Text);

                if (resp.Rows.Count > 0)
                {
                    foreach (DataRow item in resp.Rows)
                    {
                        txt_Id_Reserva.Text = item["ID_RESERVA"].ToString();
                        txt_Id_Rut.Text = item["RUT_CLIENTE"].ToString();
                        txt_Id_P_N_Cliente.Text = item["P_NOMBRE_CLIENTE"].ToString();
                        txt_Id_P_A_Cliente.Text = item["P_APELLIDO_CLIENTE"].ToString();
                        cmb_Servicio.Text = item["DESC_SERVICIO"].ToString();
                        cmb_Empleado.Text = item["P_NOMBRE_EMPLEADO"].ToString();
                        cmb_Hora.Text = item["HORA_RESERVA"].ToString();
                       // cmb_Estado.Text = item["ID_DESCR_ESTADO_R"].ToString();
                        fecha_Reserva.Text = item["FECHA_RESERVA"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("RUT Reserva inválido");

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Busqueda no arrojó resultados");
                throw ex;
            }

        }

        private void btn_MenuP_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MenuPrincipal ventana = new MenuPrincipal();
            ventana.ShowDialog();
        }
    }
}
