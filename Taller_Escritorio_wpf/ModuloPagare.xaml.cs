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
    /// Lógica de interacción para ModuloPagare.xaml
    /// </summary>
    public partial class ModuloPagare : Window
    {
        public ModuloPagare()
        {
            InitializeComponent();

            Deuda_Negocio deudaN = new Deuda_Negocio();
            cmb_estado_deuda.ItemsSource = deudaN.ListarEstadoDeuda();
            cmb_estado_deuda.SelectedValuePath = "id_estado";
            cmb_estado_deuda.DisplayMemberPath = "desc_estado";

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
        private void Btn_Menu_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MenuPrincipal ventana = new MenuPrincipal();
            ventana.ShowDialog();
        }

        private void Btn_venta_deuda_Click(object sender, RoutedEventArgs e)
        {
            Deuda_Negocio deudaN = new Deuda_Negocio();
            DataTable resp = new DataTable();

            if (txt_idventa.Text != "")
            {
                try
                {
                    resp = deudaN.ObterneRDeuda(txt_idventa.Text);
                    if (resp.Rows.Count > 0)
                    {
                        foreach (DataRow item in resp.Rows)
                        {
                            txt_idventa.Text = item["ID_VENTA"].ToString();
                            //   txt_id_prod_recep.Text = item["ID_PRODUCTO"].ToString();
                            cmb_estado_deuda.SelectedValue = item["ID_ESTADO_DEUDA"].ToString();
                            txt_fecha.Text = DateTime.Parse(item["FECHA"].ToString()).ToString("dd/MM/yyyy");
                            txt_monto.Text = "$ " + decimal.Parse(item["MONTO_DEUDA"].ToString()).ToString("n2");
                            if (cmb_estado_deuda.Text == "Pago pendiente")
                            {
                                txt_monto.Foreground = Brushes.Red;
                            }


                            txt_id_fiado.Text = item["ID_DEUDA"].ToString();


                            txt_cliente.Text = item["P_NOMBRE_CLIENTE"].ToString() + " "+ item["S_NOMBRE_CLIENTE"].ToString() + " "+ item["P_APELLIDO_CLIENTE"].ToString() + " " + item["S_APELLIDO_CLIENTE"].ToString();
                            txt_rut.Text = item["RUT_CLIENTE"].ToString() + " - " + item["DV_CLIENTE"].ToString();
                            txt_correo.Text = item["CORREO_CLIENTE"].ToString();
                            txt_fono.Text = item["FONO_CLIENTE"].ToString();
                        }
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
                MessageBox.Show("Debe ingresar un ID VENTA para la busqueda");
            }

        }

        private void Btn_confirmar_cam_Click(object sender, RoutedEventArgs e)
        {

            Deuda_Negocio deudaN = new Deuda_Negocio();
            bool newEstado = false;
            if (cmb_estado_deuda.Text != "Pago pendiente")
            {

                newEstado = deudaN.Cambiar_Estado_Deuda(txt_idventa.Text, cmb_estado_deuda.SelectedValue.ToString());

                if(newEstado)
                {
                    txt_monto.Foreground = Brushes.Green;
                    MessageBox.Show("Estado del pago actualizadp");
                }
            }
            else
            {
                MessageBox.Show("Estado del pago indica PAGO PENDIENTE");
            }



        }


    }
}
