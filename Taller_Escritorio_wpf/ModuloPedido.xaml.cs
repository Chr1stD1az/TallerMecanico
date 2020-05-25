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
using Taller_Datos;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            CargarComboBoxPedido();
            txt_Fecha_P.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void BotonCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void CargarComboBoxPedido()
        {
            //////////////LISTAR FAMILIA/////////////////
            Compartido_Negocio comunaN = new Compartido_Negocio();
            cmb_Familia_P.ItemsSource = comunaN.ListarFamProd();
            cmb_Familia_P.DisplayMemberPath = "descr_familia";
            cmb_Familia_P.SelectedValuePath = "id_familia_prod";


            //////////////LISTAR ESTADO/////////////////
            cmb_Estado_P.ItemsSource = comunaN.ListarEstadoPedido();
            cmb_Estado_P.DisplayMemberPath = "desc_estado";
            cmb_Estado_P.SelectedValuePath = "id_estado";

            //////////////LISTAR EMPLEADO/////////////////
            cmb_Empleado_P.ItemsSource = comunaN.ListarEmpleado();
            cmb_Empleado_P.DisplayMemberPath = "p_nombre_empleado";
            cmb_Empleado_P.SelectedValuePath = "id_empleado";

            //////////////LISTAR PROVEEDOR/////////////////
            cmb_Proveedor_P.ItemsSource = comunaN.ListarProveedor();
            cmb_Proveedor_P.DisplayMemberPath = "razon_social_prov";
            cmb_Proveedor_P.SelectedValuePath = "id_proveedor";



        }
        private void cmb_Familia_P_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void cmb_Familia_P_DropDownClosed(object sender, EventArgs e)
        {
            Compartido_Negocio comp = new Compartido_Negocio();
            List<Tipo_Producto_dto> respuesta = new List<Tipo_Producto_dto>();
            int familiaCmb = int.Parse(cmb_Familia_P.SelectedValue.ToString());
            respuesta = comp.ListarTipoProd(familiaCmb);
            cmb_Tipo_P.ItemsSource = respuesta;
            cmb_Tipo_P.DisplayMemberPath = "descr_tipo_prod";
            cmb_Tipo_P.SelectedValuePath = "id_tipo_prod";

        }
        private void Btn_pedidos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_recep_p_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            RecepcionPedido ventana = new RecepcionPedido();
            ventana.ShowDialog();
        }

        private void Btn_Agregar_p_Click(object sender, RoutedEventArgs e)
        {

            decimal total = 0; 
            Producto_Negocio Prod_Neg = new Producto_Negocio();

            List<Detalle_Pedido_dto> listado_det = new List<Detalle_Pedido_dto>();


            
            if(txt_Cant_producto.Text == "" || cmb_Producto.Text == "")
            {
                if (txt_Cant_producto.Text == "" && cmb_Producto.Text == "")
                {
                    MessageBox.Show("Debe seleccionar un producto y su cantidad");
                }
                else
                {
                    if (cmb_Producto.Text == "")
                    {
                        MessageBox.Show("Debe seleccionar un producto");

                    }
                    if (txt_Cant_producto.Text == "")
                    {
                        MessageBox.Show("Debe ingresar cantidad");

                    }
                }
                
            }
            else
            {
               
                string id_prod = txt_Id_producto.Text;
                int cantidad_prod = int.Parse(txt_Cant_producto.Text);
                DataTable respuesta = Prod_Neg.Buscar_Prod_id(id_prod);
                // Aqui trae lo previamente guardado en la grilla, si la variable se session esta nula no entra
                if (Application.Current.Properties["ListadoPedido"] != null)
                {
                    // trae lo que esta en la variable de sesion
                    var jsonValueToGet = JsonConvert.DeserializeObject(Application.Current.Properties["ListadoPedido"].ToString());

                    // lo convierte en un array
                    JArray jsonPreservar = JArray.Parse(jsonValueToGet.ToString());

                    //lo recorre para añadir al listado que luego se mostrará en la grilla
                    foreach (JObject item in jsonPreservar.Children<JObject>())
                    {
                        // estos datos vienen de la grilla, creamosla entidad para añadir al listado

                        Detalle_Pedido_dto entidad = new Detalle_Pedido_dto();
                        entidad.Producto = int.Parse(item["Producto"].ToString());
                        entidad.Descripción = item["Descripción"].ToString();
                        entidad.Cantidad = int.Parse(item["Cantidad"].ToString());
                        entidad.Total = decimal.Parse(item["Total"].ToString()).ToString("n2");
                        entidad.Precio = decimal.Parse(item["Precio"].ToString()).ToString("n2");
                        
                        total = total + decimal.Parse(entidad.Total);
                        listado_det.Add(entidad);
                    }
                }


                foreach (DataRow item in respuesta.Rows)
                {
                    Detalle_Pedido_dto entidad = new Detalle_Pedido_dto();
                    entidad.Producto = int.Parse(item["id_producto"].ToString());
                    entidad.Descripción = item["descr_producto"].ToString();
                    entidad.Cantidad = cantidad_prod;
                    entidad.Precio = decimal.Parse(item["precio_prod"].ToString()).ToString("n2");
                    entidad.Total = (decimal.Parse(item["precio_prod"].ToString()) * cantidad_prod).ToString("n2");

                    total = total + decimal.Parse(entidad.Total);
                    listado_det.Add(entidad);

                }

                // actualiza variable de sesion con los datos actuales de la grilla
                var jsonValueToSave = JsonConvert.SerializeObject(listado_det);
                Application.Current.Properties["ListadoPedido"] = jsonValueToSave;

                txt_total.Text = total.ToString("n2");
                Dt_G_list_pedido.ItemsSource = listado_det;

            }

        }

        private void Btn_MenuP_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MenuPrincipal ventana = new MenuPrincipal();
            ventana.ShowDialog();
        }

        private void Btn_Buscar_prod_Click(object sender, RoutedEventArgs e)
        {
            Producto_Negocio Prod_Nego = new Producto_Negocio();
            List<Producto_dto> respuesta = new List<Producto_dto>();

            if (cmb_Tipo_P.Text !="")
            {
                string producto_buscar = txt_buscar_prod.Text;
                int tipo_prod = int.Parse(cmb_Tipo_P.SelectedValue.ToString());
                respuesta = Prod_Nego.ListarProducto(tipo_prod, producto_buscar);
                cmb_Producto.ItemsSource = respuesta;
                cmb_Producto.DisplayMemberPath = "descr_producto";
                cmb_Producto.SelectedValuePath = "id_producto";
            }
            else
            {
                MessageBox.Show("Debe seleccionar tipo de producto");
            }

        }

        private void cmb_Producto_DropDownClosed(object sender, EventArgs e)
        {
            Producto_Negocio prod_neg = new Producto_Negocio();
            DataTable resp = new DataTable();
            if( cmb_Producto.Text == "")
            {
                MessageBox.Show("Debe seleccionar un producto");
            }
            else
            {
                int id_prod = int.Parse(cmb_Producto.SelectedValue.ToString());

                resp = prod_neg.Buscar_Prod_id(id_prod.ToString());
                if (resp.Rows.Count > 0)
                {
                    foreach (DataRow item in resp.Rows)
                    {
                        txt_Id_producto.Text = item["id_producto"].ToString();
                        txt_Sku_producto.Text = item["sku_prod"].ToString();

                    }

                }
                else
                {
                    MessageBox.Show("invalido");

                }
            }

        }

        private void Btn_Generar_Ped_Click(object sender, RoutedEventArgs e)
        {
            string cabecera = "";
            Generar_Pedido_Negocio Pedido_Neg = new Generar_Pedido_Negocio();

            cabecera = Pedido_Neg.CrearPedidoHDR(txt_Fecha_P.Text, cmb_Estado_P.SelectedValue.ToString(), cmb_Proveedor_P.SelectedValue.ToString(), cmb_Empleado_P.SelectedValue.ToString());

            if (Application.Current.Properties["ListadoPedido"] != null)
            {
                // trae lo que esta en la variable de sesion
                var jsonValueToGet = JsonConvert.DeserializeObject(Application.Current.Properties["ListadoPedido"].ToString());

                // lo convierte en un array
                JArray jsonPreservar = JArray.Parse(jsonValueToGet.ToString());

                //lo recorre para añadir al listado que luego se mostrará en la grilla
                foreach (JObject item in jsonPreservar.Children<JObject>())
                {

                    var precio = Math.Round(decimal.Parse(item["Precio"].ToString()), 0).ToString().Replace(".","");
                    var total = Math.Round(decimal.Parse(item["Total"].ToString()), 0).ToString().Replace(".", "");
                    // estos datos vienen de la grilla, creamosla entidad para añadir al listado
                    var respuesta = Pedido_Neg.CrearPedidoDet(item["Cantidad"].ToString(), precio, total, cabecera, item["Producto"].ToString());
                    
                }
            }

        }
    }
}