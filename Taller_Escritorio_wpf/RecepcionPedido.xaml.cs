using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private void limpiar1()
        {
            txt_cantidad_recep.Text = "";
            DP_fecha_Venc.SelectedDate = null;
            txt_Sku_producto.Text = "";
            txt_descr_producto.Text = "";
            txt_IdProd.Text = "";

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
                            txt_total_recep.Text = decimal.Parse(item["TOTAL_PEDIDO"].ToString()).ToString("n2");
                            

                        }
                        if (cmb_estado_recep.Text == "Finalizado"|| cmb_estado_recep.Text == "Anulado")
                        { 
                            if (cmb_estado_recep.Text == "Finalizado")
                            {

                                detalle = RecepN.ListarDetallePedido(txt_id_pedido.Text);
                                DG_Recep.ItemsSource = detalle;
                                this.Btn_Confirmar_Rec.IsEnabled = false;
                                this.DG_Recep.IsEnabled = false;
                                MessageBox.Show("El pedido número : " + txt_id_pedido.Text + " ya fue recepcionado");
                            }
                            else
                            {
                                detalle = RecepN.ListarDetallePedido(txt_id_pedido.Text);
                                DG_Recep.ItemsSource = detalle;
                                this.Btn_Confirmar_Rec.IsEnabled = false;
                                this.DG_Recep.IsEnabled = false;
                                MessageBox.Show("El pedido número : " + txt_id_pedido.Text + " fue anulado");
                            }
                        

                        }
                        else {
                            detalle = RecepN.ListarDetallePedido(txt_id_pedido.Text);
                            DG_Recep.ItemsSource = detalle;
                            this.Btn_Confirmar_Rec.IsEnabled = true;
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
                MessageBox.Show("Debe ingresar un ID Pedido para la busqueda");
            }
            

        }

        private void Btn_Confirmar_Rec_Click(object sender, RoutedEventArgs e)
        {
            bool SkuVacio = false;
            string cabecera = "";
            bool aumentar = false ;
            bool new_estado = false;
            Producto_Negocio producto_Neg = new Producto_Negocio();
             Recepcionar_Pedido_Negocio Recepnegocio = new Recepcionar_Pedido_Negocio();

            var SelectProd = DG_Recep.ItemsSource;
            var JsonItemSelect = JsonConvert.SerializeObject(SelectProd);
            JArray jsonPreservar = JArray.Parse(JsonItemSelect.ToString());
            foreach (JObject item in jsonPreservar.Children<JObject>())
            {
                if (item["SKU"].ToString() == "" && SkuVacio == false)
                {
                    MessageBox.Show(" El Producto : " + item["Descripción"].ToString() + " se encuentra sin su SKU");
                    SkuVacio = true;
                }
            }

            if (SkuVacio == false)
            {
               // var total = Math.Round(decimal.Parse(item["TOTAL"].ToString()), 0).ToString().Replace(".", "");
                cabecera = Recepnegocio.CrearRecepcionHDR(txt_fecha_recep.Text, cmb_estado_recep.SelectedValue.ToString(), txt_id_pedido.Text, cmb_empleado.SelectedValue.ToString(), cmb_proveedor.SelectedValue.ToString(), txt_total_recep.Text);
                new_estado = Recepnegocio.Cambiar_Estado_Pedido(txt_id_pedido.Text, cmb_estado_recep.SelectedValue.ToString());
                foreach (JObject item in jsonPreservar.Children<JObject>())
                {
                    try
                    {
                        var precio = Math.Round(decimal.Parse(item["Precio"].ToString()), 0).ToString().Replace(".", "");
                        var total = Math.Round(decimal.Parse(item["Total"].ToString()), 0).ToString().Replace(".", "");

                        Recepnegocio.CrearRecepcionDet(item["Recibido"].ToString(),
                                                       precio,
                                                       item["SKU"].ToString(),
                                                       cabecera,
                                                       item["IDProducto"].ToString(),
                                                       total);
                        aumentar = producto_Neg.Aumentar_cant_prod(item["IDProducto"].ToString(),
                                                                    item["Recibido"].ToString());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
               // aumentar = producto_Neg.Aumentar_cant_prod();
                txt_id_recep.Text = cabecera;
                MessageBox.Show("Recepción realizada con éxito");
                this.Btn_Confirmar_Rec.IsEnabled = false;
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DG_Recep_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var sel = DG_Recep.SelectedIndex;
            var i = 0;
            var SelectProd = DG_Recep.ItemsSource;
            var JsonItemSelect = JsonConvert.SerializeObject(SelectProd);
            JArray jsonPreservar = JArray.Parse(JsonItemSelect.ToString());
            foreach (JObject item in jsonPreservar.Children<JObject>())
            {
                if (i == sel)
                {
                    txt_IdProd.Text = item["IDProducto"].ToString();
                    txt_cantidad_recep.Text = item["Cantidad"].ToString();
                    txt_Sku_producto.Text = item["SKU"].ToString();
                    txt_descr_producto.Text = item["Descripción"].ToString();
                }
                i++;
            }
        }

        private void Btn_Confirmar_Prod_Click(object sender, RoutedEventArgs e)
        {
            List<Recepcion_pedido_dto> listado_det = new List<Recepcion_pedido_dto>();
            //  int id = item["Producto"].ToString();

            if (txt_id_pedido.Text == "")
            {
                MessageBox.Show("Debe ingresar un pedido");
            }
            else
            {
                if (txt_IdProd.Text == "" || DP_fecha_Venc.Text == "" || txt_cantidad_recep.Text == "")
                {
                    if (txt_IdProd.Text == "" && DP_fecha_Venc.Text == "" && txt_cantidad_recep.Text == "")
                    {
                        MessageBox.Show("Debe seleccionar un producto su Fecha de Vencimiento y cantidad recepcionada");
                    }
                    else
                    {
                        if (txt_IdProd.Text == "")
                        {
                            MessageBox.Show("Debe seleccionar un producto");
                        }
                        if (DP_fecha_Venc.Text == "")
                        {
                            MessageBox.Show("Debe seleccionar un Fecha de Vencimiento");
                        }
                        if (txt_cantidad_recep.Text == "")
                        {
                            MessageBox.Show("Debe ingresar cantidad recepcionada");
                        }
                    }
                }
                else
                {
                    bool editar = false;
                    int cantidadRecep = int.Parse(txt_cantidad_recep.Text.ToString());
                    decimal total = 0;
                    var SelectProd = DG_Recep.ItemsSource;
                    var JsonItemSelect = JsonConvert.SerializeObject(SelectProd);
                    JArray jsonPreservar = JArray.Parse(JsonItemSelect.ToString());
                    if (jsonPreservar.Count > 0 || jsonPreservar != null)
                    {
                        foreach (JObject item in jsonPreservar.Children<JObject>())
                        {
                            int id = int.Parse(txt_IdProd.Text.ToString());

                            Recepcion_pedido_dto entidad = new Recepcion_pedido_dto();

                            entidad.IDProducto = int.Parse(item["IDProducto"].ToString());
                            //  entidad.SKU =      int.Parse(item["SKU"].ToString());
                            entidad.Descripción = item["Descripción"].ToString();
                            entidad.Cantidad = int.Parse(item["Cantidad"].ToString());
                            entidad.Familia = item["Familia"].ToString();
                            entidad.Tipo = item["Tipo"].ToString();

                            if (id == int.Parse(item["IDProducto"].ToString()))
                            {
                                entidad.Recibido = cantidadRecep;
                                entidad.Total = (cantidadRecep * decimal.Parse(item["Precio"].ToString())).ToString("n2");


                                string sku = "";
                                string idProd = cmb_proveedor.SelectedValue.ToString();
                                string Fam = item["Familia"].ToString();
                                string n2 = Fam.Substring(0, 3);
                                string tipo = item["Tipo"].ToString();
                                string n3 = tipo.Substring(10, 3);
                                string FechVen = DP_fecha_Venc.Text.Replace("-", "");
                                string n1 = idProd.Substring(0, 3);
                                string idP = item["IDProducto"].ToString();
                                sku = n1 + "" + n2 + "" + FechVen + "" + n3 + "" + idP;
                                entidad.SKU = sku.ToUpper(); ;
                                txt_Sku_producto.Text = sku.ToUpper();
                                editar = true;
                            }
                            else
                            {
                                entidad.SKU = item["SKU"].ToString();
                                entidad.Recibido = int.Parse(item["Cantidad"].ToString());
                                entidad.Total = decimal.Parse(item["Total"].ToString()).ToString("n2");
                            }

                            entidad.Precio = decimal.Parse(item["Precio"].ToString()).ToString("n2");

                            total = total + decimal.Parse(entidad.Total);
                            listado_det.Add(entidad);
                        }

                    }
                    var jsonValueToSave = JsonConvert.SerializeObject(listado_det);
                    Application.Current.Properties["listado_recep"] = jsonValueToSave;

                    txt_total_recep.Text = total.ToString("n2");
                    DG_Recep.ItemsSource = listado_det;
                    limpiar1();
                }

            }

        }

       /* public string CrearSku()
         {
            
            string sku = "";
            string idProd = cmb_proveedor.SelectedValue.ToString();
            
            string Fam = Familia.Substring(1, 3);
            string ttipo = entidad.Tipo.Substring(11, 3);
            string FechVen = DP_fecha_Venc.Text.Replace("-","");
            string val1 = idProd.Substring(0, 3);
            return sku;
         }*/
    }  
}
