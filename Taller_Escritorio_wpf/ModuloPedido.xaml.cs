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
            cmb_Estado_P.SelectedValue = 1;
            Application.Current.Properties["ListadoPedido"] = null;

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
        public void CargarComboBoxPedido()
        {
            //////////////LISTAR FAMILIA/////////////////
            Compartido_Negocio ComparN = new Compartido_Negocio();
            cmb_Familia_P.ItemsSource = ComparN.ListarFamProd();
            cmb_Familia_P.DisplayMemberPath = "descr_familia";
            cmb_Familia_P.SelectedValuePath = "id_familia_prod";


            //////////////LISTAR ESTADO/////////////////
            cmb_Estado_P.ItemsSource = ComparN.ListarEstadoPedido();
            cmb_Estado_P.DisplayMemberPath = "desc_estado";
            cmb_Estado_P.SelectedValuePath = "id_estado";

            //////////////LISTAR EMPLEADO/////////////////
            cmb_Empleado_P.ItemsSource = ComparN.ListarEmpleado();
            cmb_Empleado_P.DisplayMemberPath = "p_nombre_empleado";
            cmb_Empleado_P.SelectedValuePath = "id_empleado";

            //////////////LISTAR PROVEEDOR/////////////////
            cmb_Proveedor_P.ItemsSource = ComparN.ListarProveedor();
            cmb_Proveedor_P.DisplayMemberPath = "razon_social_prov";
            cmb_Proveedor_P.SelectedValuePath = "id_proveedor";



        }
        private void cmb_Familia_P_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void cmb_Familia_P_DropDownClosed(object sender, EventArgs e)
        {
            if ( cmb_Familia_P.Text == "")
            {
                MessageBox.Show("Seleccionar Familiar de producto");
            }
            else
            {
                Compartido_Negocio comp = new Compartido_Negocio();
                List<Tipo_Producto_dto> respuesta = new List<Tipo_Producto_dto>();
                int familiaCmb = int.Parse(cmb_Familia_P.SelectedValue.ToString());
                respuesta = comp.ListarTipoProd(familiaCmb);
                cmb_Tipo_P.ItemsSource = respuesta;
                cmb_Tipo_P.DisplayMemberPath = "descr_tipo_prod";
                cmb_Tipo_P.SelectedValuePath = "id_tipo_prod";

            }
            limpiar1();
        }
        private void Btn_pedidos_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ModuloPedido ventana = new ModuloPedido();
            ventana.ShowDialog();
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

            if (txt_Cant_producto.Text == "" || cmb_Producto.Text == "")
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
                bool existe = false;
                string id_prod = txt_Id_producto.Text;
                int cantidad_prod = int.Parse(txt_Cant_producto.Text);

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
                        entidad.SKU =item["SKU"].ToString();
                        entidad.Descripción = item["Descripción"].ToString();
                        entidad.Cantidad = int.Parse(item["Cantidad"].ToString());
                        entidad.Total = decimal.Parse(item["Total"].ToString()).ToString("n2");
                        entidad.Precio = decimal.Parse(item["Precio"].ToString()).ToString("n2");

                        total = total + decimal.Parse(entidad.Total);

                        listado_det.Add(entidad);
                        if (id_prod == item["Producto"].ToString())
                        {

                            existe = true;

                        }

                    }
                }

                if (existe)
                {
                    MessageBox.Show(" El Producto : " + txt_descr_producto.Text + " ya existe en el listado");
                    limpiar1();
                }
                else
                {
                    DataTable respuesta = Prod_Neg.Buscar_Prod_id(id_prod);
                    foreach (DataRow item in respuesta.Rows)
                    {
                        Detalle_Pedido_dto entidad = new Detalle_Pedido_dto();
                        entidad.Producto = int.Parse(item["id_producto"].ToString());
                        entidad.SKU = item["sku_prod"].ToString();
                        entidad.Descripción = item["descr_producto"].ToString();
                        entidad.Cantidad = cantidad_prod;
                        entidad.Precio = decimal.Parse(item["precio_costo"].ToString()).ToString("n2");
                        entidad.Total = (decimal.Parse(item["precio_costo"].ToString()) * cantidad_prod).ToString("n2");

                        total = total + decimal.Parse(entidad.Total);
                        listado_det.Add(entidad);

                    }

                }

                // actualiza variable de sesion con los datos actuales de la grilla
                var jsonValueToSave = JsonConvert.SerializeObject(listado_det);
                Application.Current.Properties["ListadoPedido"] = jsonValueToSave;

                txt_total.Text = total.ToString("n2");
                Dt_G_list_pedido.ItemsSource = listado_det;
                limpiar1();

            }

        }

        private void Btn_MenuP_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MenuPrincipal ventana = new MenuPrincipal();
            ventana.ShowDialog();
        }

        private void limpiar1()
        {
            txt_buscar_prod.Text = "";
            cmb_Producto.SelectedValue = null;
            txt_descr_producto.Text = "";
            txt_Id_producto.Text = "";
            txt_Cant_producto.Text = "";
            txt_Sku_producto.Text = "";

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
            limpiar1();

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
                        txt_descr_producto.Text = cmb_Producto.Text;

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
            txt_Id_Pedido.Text = ""; 
            Generar_Pedido_Negocio Pedido_Neg = new Generar_Pedido_Negocio();
            if (cmb_Proveedor_P.Text == "" || cmb_Empleado_P.Text == "")
            {
                if (cmb_Proveedor_P.Text == "" && cmb_Empleado_P.Text == "")
                {
                    MessageBox.Show("Debe seleccionar un proveedor y empleado");
                }
                else
                {
                    if (cmb_Proveedor_P.Text == "")
                    {
                        MessageBox.Show("Debe seleccionar un proveedor");

                    }
                    if (cmb_Empleado_P.Text == "")
                    {
                        MessageBox.Show("Debe seleccionar un empleado");

                    }
                }
            }
            else
            {
                if (Application.Current.Properties["ListadoPedido"] == null || Application.Current.Properties["ListadoPedido"].ToString() == "[]")
                {
                    MessageBox.Show("Debe ingresar al menos un producto");
                }
                else
                {
                    cabecera = Pedido_Neg.CrearPedidoHDR(txt_Fecha_P.Text, cmb_Estado_P.SelectedValue.ToString(), cmb_Proveedor_P.SelectedValue.ToString(), cmb_Empleado_P.SelectedValue.ToString(), txt_total.Text);
                    if (Application.Current.Properties["ListadoPedido"] != null)
                    {
                        // trae lo que esta en la variable de sesion
                        var jsonValueToGet = JsonConvert.DeserializeObject(Application.Current.Properties["ListadoPedido"].ToString());

                        // lo convierte en un array
                        JArray jsonPreservar = JArray.Parse(jsonValueToGet.ToString());

                        //lo recorre para añadir al listado que luego se mostrará en la grilla
                        foreach (JObject item in jsonPreservar.Children<JObject>())
                        {
                            var precio = Math.Round(decimal.Parse(item["Precio"].ToString()), 0).ToString().Replace(".", "");
                            var total = Math.Round(decimal.Parse(item["Total"].ToString()), 0).ToString().Replace(".", "");
                            // estos datos vienen de la grilla, creamosla entidad para añadir al listado
                            var respuesta = Pedido_Neg.CrearPedidoDet(item["Cantidad"].ToString(), precio, total, cabecera, item["Producto"].ToString());
                        }
                        txt_Id_Pedido.Text = cabecera;
                        this.Btn_Generar_Ped.IsEnabled = false;
                        this.Dt_G_list_pedido.IsEnabled = false;
                        this.Btn_Agregar_p.IsEnabled = false;
                        this.Btn_Editar_p.IsEnabled = false;
                        this.Btn_Eliminar_p.IsEnabled = false;
                        this.Btn_Buscar_prod.IsEnabled = false;
                        MessageBox.Show("Pedido generado con el Numero :" + txt_Id_Pedido.Text);
                    }
                }
            }
        }
        //  int posProducto;
        private void Dt_G_list_pedido_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var sel = Dt_G_list_pedido.SelectedIndex;
            var i = 0;
            var SelectProd = Dt_G_list_pedido.ItemsSource;
            var JsonItemSelect = JsonConvert.SerializeObject(SelectProd);
            JArray jsonPreservar = JArray.Parse(JsonItemSelect.ToString());
            foreach (JObject item in jsonPreservar.Children<JObject>())
            {
                if (i == sel)
                {
                    txt_Id_producto.Text = item["Producto"].ToString();
                    txt_Sku_producto.Text = item["SKU"].ToString();
                    txt_descr_producto.Text = item["Descripción"].ToString();
                    txt_Cant_producto.Text = item["Cantidad"].ToString();
                }
                i++;            
            }
        }

        private void Btn_Editar_p_Click(object sender, RoutedEventArgs e)
        {
            List<Detalle_Pedido_dto> listado_det = new List<Detalle_Pedido_dto>();

            if (txt_Id_producto.Text =="")
            {
                MessageBox.Show("Debe seleccionar un producto");
            }
            else
            {
                bool editar = false;
                int id = int.Parse(txt_Id_producto.Text.ToString());
                int cantidad = int.Parse(txt_Cant_producto.Text.ToString());
                decimal total = 0;
                //   Producto_Negocio Prod_Neg = new Producto_Negocio();

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
                        entidad.SKU = item["SKU"].ToString();
                        entidad.Descripción = item["Descripción"].ToString();

                        if (id == int.Parse(item["Producto"].ToString()))
                        {
                            entidad.Cantidad = cantidad;
                            entidad.Total = (cantidad * decimal.Parse(item["Precio"].ToString())).ToString("n2");
                            editar = true;
                        }
                        else
                        {
                            entidad.Cantidad = int.Parse(item["Cantidad"].ToString());
                            entidad.Total = decimal.Parse(item["Total"].ToString()).ToString("n2");
                        }

                        entidad.Precio = decimal.Parse(item["Precio"].ToString()).ToString("n2");

                        total = total + decimal.Parse(entidad.Total);
                        listado_det.Add(entidad);
                    }

                }

                if (editar == true)
                {

                    MessageBox.Show("Cantidad Actualizada");
                    limpiar1();
                }
                else
                {
                    MessageBox.Show("El Producto a actualizar no existe!");
                    limpiar1();
                }

                var jsonValueToSave = JsonConvert.SerializeObject(listado_det);
                Application.Current.Properties["ListadoPedido"] = jsonValueToSave;

                txt_total.Text = total.ToString("n2");
                Dt_G_list_pedido.ItemsSource = listado_det;
            }
        }

        private void Btn_Eliminar_p_Click(object sender, RoutedEventArgs e)
        {
           
            List<Detalle_Pedido_dto> listado_det = new List<Detalle_Pedido_dto>();
            if (txt_Id_producto.Text =="")
            {
                MessageBox.Show("Debe seleccionar un producto");
            }
            else
            {
                bool eliminado = false;
                string id_prod = txt_Id_producto.Text;
                int cantidad_prod = int.Parse(txt_Cant_producto.Text);
                decimal total = 0;

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
                        // estos datos vienen de la grilla, creamos la entidad para añadir al listado
                        Detalle_Pedido_dto entidad = new Detalle_Pedido_dto();
                        entidad.Producto = int.Parse(item["Producto"].ToString());
                        entidad.SKU = item["SKU"].ToString();
                        entidad.Descripción = item["Descripción"].ToString();
                        entidad.Cantidad = int.Parse(item["Cantidad"].ToString());
                        entidad.Total = decimal.Parse(item["Total"].ToString()).ToString("n2");
                        entidad.Precio = decimal.Parse(item["Precio"].ToString()).ToString("n2");
                        total = total + decimal.Parse(entidad.Total);

                        if (id_prod != item["Producto"].ToString())
                        {
                            listado_det.Add(entidad);

                        }
                        else
                        {
                            eliminado = true;
                            total = total - decimal.Parse(entidad.Total);
                        }

                    }

                    if (eliminado == true)
                    {

                        MessageBox.Show("Producto eliminado");
                        limpiar1();
                    }
                    else
                    {
                        MessageBox.Show("El Producto no existe!");
                        limpiar1();
                    }
                    var jsonValueToSave = JsonConvert.SerializeObject(listado_det);
                    Application.Current.Properties["ListadoPedido"] = jsonValueToSave;

                    txt_total.Text = total.ToString("n2");
                    Dt_G_list_pedido.ItemsSource = listado_det;
                }
            }
        }


    }
}