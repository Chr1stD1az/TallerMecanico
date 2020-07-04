using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using Taller_Datos;
using Taller_Negocio;

namespace Taller_Escritorio_wpf
{
    /// <summary>
    /// Lógica de interacción para ModuloVenta.xaml
    /// </summary>
    public partial class ModuloVenta : Window
    {
        public ModuloVenta()
        {
            InitializeComponent();
            CargarComboBox();
            cmb_taller_Vta.SelectedValue = 1;
            Application.Current.Properties["ListadoVenta"] = null;
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
        public void CargarComboBox()
        {
            Tipo_Documento_Negocio Docu_Neg = new Tipo_Documento_Negocio();
            //////////////LISTAR FAMILIA/////////////////
            cmb_Docuemento_Vta.ItemsSource =Docu_Neg.ListarDocumento();
            cmb_Docuemento_Vta.DisplayMemberPath = "descr_documento";
            cmb_Docuemento_Vta.SelectedValuePath = "id_documento";

            TallerNegocio TallerN = new TallerNegocio();
            //////////////LISTAR taller/////////////////
            cmb_taller_Vta.ItemsSource = TallerN.ListarTaller();
            cmb_taller_Vta.DisplayMemberPath = "nombre";
            cmb_taller_Vta.SelectedValuePath = "id_taller";

            
            Servicio_Negocio Serv_N = new Servicio_Negocio();
            //////////////LISTAR ESTADO/////////////////
            cmb_servicio_Vta.ItemsSource = Serv_N.ListaServicio();
            cmb_servicio_Vta.DisplayMemberPath = "Descripción";
            cmb_servicio_Vta.SelectedValuePath = "ID";

            //////////////LISTAR FAMILIA/////////////////
            Compartido_Negocio ComparN = new Compartido_Negocio();
            cmb_Familia_prod_Vta.ItemsSource = ComparN.ListarFamProd();
            cmb_Familia_prod_Vta.DisplayMemberPath = "descr_familia";
            cmb_Familia_prod_Vta.SelectedValuePath = "id_familia_prod";


        }

        private void Btn_MenuP_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MenuPrincipal ventana = new MenuPrincipal();
            ventana.ShowDialog();

        }

        private void limpiar()
        {
            txt_Cant_servicios.Text = string.Empty;
            cmb_servicio_Vta.SelectedValue = null;
            cmb_Familia_prod_Vta.SelectedValue = null;
            cmb_Tipo_Prod_Vta.SelectedValue = null;
            cmb_Producto.SelectedValue = null;
            txt_Cant_producto.Text = string.Empty;
            txt_Sku_producto.Text = string.Empty;
            txt_descr_producto.Text = string.Empty;
            txt_stock.Text = string.Empty;
        }

        private void Btn_Agregar_p_Click(object sender, RoutedEventArgs e)
        {
            decimal total = 0;
            Producto_Negocio Prod_Neg = new Producto_Negocio();
            List<Venta_dto> list_venta = new List<Venta_dto>();

            if (txt_Cant_producto.Text == "" || txt_descr_producto.Text == "")
            {
                if (txt_Cant_producto.Text == "" && txt_descr_producto.Text == "")
                {
                    MessageBox.Show("Debe seleccionar un producto y su cantidad");
                }
                else
                {
                    if (txt_descr_producto.Text == "")
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
                string id_prod = cmb_Producto.SelectedValue.ToString();
                int cantidad_prod = int.Parse(txt_Cant_producto.Text);

                // Aqui trae lo previamente guardado en la grilla, si la variable se session esta nula no entra
                if (Application.Current.Properties["ListadoVenta"] != null)
                {
                    // trae lo que esta en la variable de sesion
                    var jsonValueToGet = JsonConvert.DeserializeObject(Application.Current.Properties["ListadoVenta"].ToString());

                    // lo convierte en un array
                    JArray jsonPreservar = JArray.Parse(jsonValueToGet.ToString());

                    //lo recorre para añadir al listado que luego se mostrará en la grilla
                    foreach (JObject item in jsonPreservar.Children<JObject>())
                    {

                        // estos datos vienen de la grilla, creamosla entidad para añadir al listado

                        Venta_dto entidad = new Venta_dto();
                        entidad.ID = int.Parse(item["ID"].ToString());
                        //entidad.SKU = item["SKU"].ToString();
                        entidad.Descripción = item["Descripción"].ToString();
                        entidad.Cantidad = int.Parse(item["Cantidad"].ToString());
                        entidad.Total = decimal.Parse(item["Total"].ToString()).ToString("n2");
                        entidad.Precio = decimal.Parse(item["Precio"].ToString()).ToString("n2");

                        total = total + decimal.Parse(entidad.Total);

                        list_venta.Add(entidad);
                        if (id_prod == item["ID"].ToString())
                        {

                            existe = true;

                        }

                    }
                }

                if (existe)
                {
                    MessageBox.Show(" El Producto : " + txt_descr_producto.Text + " ya existe en el listado");

                }
                else
                {
                    DataTable respuesta = Prod_Neg.Buscar_Prod_id(id_prod);
                    foreach (DataRow item in respuesta.Rows)
                    {
                        Venta_dto entidad = new Venta_dto();
                        entidad.ID = int.Parse(item["id_producto"].ToString());
                       // entidad.SKU = item["sku_prod"].ToString();
                        entidad.Descripción = item["descr_producto"].ToString();
                        entidad.Cantidad = cantidad_prod;
                        entidad.Precio = decimal.Parse(item["precio_prod"].ToString()).ToString("n2");
                        entidad.Total = (decimal.Parse(item["precio_prod"].ToString()) * cantidad_prod).ToString("n2");

                        total = total + decimal.Parse(entidad.Total);
                        list_venta.Add(entidad);

                    }

                }

                // actualiza variable de sesion con los datos actuales de la grilla
                var jsonValueToSave = JsonConvert.SerializeObject(list_venta);
                Application.Current.Properties["ListadoVenta"] = jsonValueToSave;

                txt_total.Text = total.ToString("n2");
                Dt_G_list_pedido.ItemsSource = list_venta;
               
            }

        }

        private void Btn_Editar_p_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Eliminar_p_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Btn_Generar_venta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Nueva_Vta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Cancelar_Vta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmb_Familia_prod_Vta_DropDownClosed(object sender, EventArgs e)
        {
            if (cmb_Familia_prod_Vta.Text == "")
            {
                MessageBox.Show("Seleccionar Familiar de producto");
            }
            else
            {
                Compartido_Negocio comp = new Compartido_Negocio();
                List<Tipo_Producto_dto> respuesta = new List<Tipo_Producto_dto>();
                int familiaCmb = int.Parse(cmb_Familia_prod_Vta.SelectedValue.ToString());
                respuesta = comp.ListarTipoProd(familiaCmb);
                cmb_Tipo_Prod_Vta.ItemsSource = respuesta;
                cmb_Tipo_Prod_Vta.DisplayMemberPath = "descr_tipo_prod";
                cmb_Tipo_Prod_Vta.SelectedValuePath = "id_tipo_prod";

            }
        }

        private void cmb_Tipo_Prod_Vta_DropDownClosed(object sender, EventArgs e)
        {
            Producto_Negocio Prod_Nego = new Producto_Negocio();
            List<Producto_dto> respuesta = new List<Producto_dto>();
            string txt = "";
            if (cmb_Tipo_Prod_Vta.Text != "")
            {
                string producto_buscar = txt;
                int tipo_prod = int.Parse(cmb_Tipo_Prod_Vta.SelectedValue.ToString());
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
            if (cmb_Producto.Text == "")
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
                        txt_Sku_producto.Text = item["sku_prod"].ToString();
                        txt_descr_producto.Text = cmb_Producto.Text;
                        txt_stock.Text = item["stock"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("invalido");
                }
            }
        }

        private void Btn_Agregar_serv_Click(object sender, RoutedEventArgs e)
        {

            decimal total = 0;
            Servicio_Negocio Serv_Neg = new Servicio_Negocio();
            List<Servicios_dto> list_serv = new List<Servicios_dto>();

            if (cmb_servicio_Vta.Text == "" )
            {
                 MessageBox.Show("Debe seleccionar un servicio");
            }
            else
            {
                bool existe = false;
                string id_serv = cmb_servicio_Vta.SelectedValue.ToString();
                int cantidad_serv = int.Parse(txt_Cant_servicios.Text);

                // Aqui trae lo previamente guardado en la grilla, si la variable se session esta nula no entra
                if (Application.Current.Properties["ListadoVenta"] != null)
                {
                    // trae lo que esta en la variable de sesion
                    var jsonValueToGet = JsonConvert.DeserializeObject(Application.Current.Properties["ListadoVenta"].ToString());

                    // lo convierte en un array
                    JArray jsonPreservar = JArray.Parse(jsonValueToGet.ToString());

                    //lo recorre para añadir al listado que luego se mostrará en la grilla
                    foreach (JObject item in jsonPreservar.Children<JObject>())
                    {
                        // estos datos vienen de la grilla, creamosla entidad para añadir al listado
                        Servicios_dto entidad = new Servicios_dto();
                        entidad.ID = int.Parse(item["ID"].ToString());
                        entidad.Descripción = item["Descripción"].ToString();
                        entidad.Cantidad = int.Parse(item["Cantidad"].ToString());
                        entidad.Total = decimal.Parse(item["Total"].ToString()).ToString("n2");
                        entidad.Precio = decimal.Parse(item["Precio"].ToString()).ToString("n2");

                        total = total + decimal.Parse(entidad.Total);

                        list_serv.Add(entidad);
                        if (id_serv == item["ID"].ToString())
                        {
                            existe = true;
                            
                        }
                    }
                    
                }

                if (existe)
                {
                    MessageBox.Show(" El Servicio : " + cmb_servicio_Vta.Text + " ya existe en el listado");
                }
                else
                {
                    DataTable respuesta = Serv_Neg.Buscar_SERV_id(id_serv);
                    foreach (DataRow item in respuesta.Rows)
                    {
                        Servicios_dto entidad = new Servicios_dto();
                        entidad.ID = int.Parse(item["ID_SERVICIO"].ToString());
                        entidad.Descripción = item["DESC_SERVICIO"].ToString();
                        entidad.Cantidad = cantidad_serv;
                        entidad.Precio = decimal.Parse(item["VALOR_SERVICIO"].ToString()).ToString("n2");
                        entidad.Total = (decimal.Parse(item["VALOR_SERVICIO"].ToString()) * cantidad_serv).ToString("n2");

                        total = total + decimal.Parse(entidad.Total);
                        list_serv.Add(entidad);
                    }

                }

                // actualiza variable de sesion con los datos actuales de la grilla
                var jsonValueToSave = JsonConvert.SerializeObject(list_serv);
                Application.Current.Properties["ListadoVenta"] = jsonValueToSave;

                txt_total.Text = total.ToString("n2");
                Dt_G_list_pedido.ItemsSource = list_serv;

            }

            limpiar();
        }

        private void Btn_Eliminar_Servi_Click(object sender, RoutedEventArgs e)
        {
            List<Servicios_dto> list_serv = new List<Servicios_dto>();
            if (cmb_servicio_Vta.Text == "")
            {
                MessageBox.Show("Debe seleccionar un servicio");
            }
            else
            {
                bool eliminado = false;
                string id_prod = cmb_servicio_Vta.SelectedValue.ToString();
                int cantidad_prod = int.Parse(txt_Cant_servicios.Text);
                decimal total = 0;

                // Aqui trae lo previamente guardado en la grilla, si la variable se session esta nula no entra
                if (Application.Current.Properties["ListadoVenta"] != null)
                {
                    // trae lo que esta en la variable de sesion
                    var jsonValueToGet = JsonConvert.DeserializeObject(Application.Current.Properties["ListadoVenta"].ToString());

                    // lo convierte en un array
                    JArray jsonPreservar = JArray.Parse(jsonValueToGet.ToString());

                    //lo recorre para añadir al listado que luego se mostrará en la grilla
                    foreach (JObject item in jsonPreservar.Children<JObject>())
                    {
                        Servicios_dto entidad = new Servicios_dto();
                        entidad.ID = int.Parse(item["ID"].ToString());
                        entidad.Descripción = item["Descripción"].ToString();
                        entidad.Cantidad = int.Parse(item["Cantidad"].ToString());
                        entidad.Total = decimal.Parse(item["Total"].ToString()).ToString("n2");
                        entidad.Precio = decimal.Parse(item["Precio"].ToString()).ToString("n2");
                        total = total + decimal.Parse(entidad.Total);

                        if (id_prod != item["ID"].ToString())
                        {
                            list_serv.Add(entidad);
                        }
                        else
                        {
                            eliminado = true;
                            total = total - decimal.Parse(entidad.Total);
                        }
                    }

                    if (eliminado == true)
                    {

                        MessageBox.Show("Servicio eliminado");

                    }
                    else
                    {
                        MessageBox.Show("El Servicio no existe!");

                    }
                    var jsonValueToSave = JsonConvert.SerializeObject(list_serv);
                    Application.Current.Properties["ListadoVenta"] = jsonValueToSave;

                    txt_total.Text = total.ToString("n2");
                    Dt_G_list_pedido.ItemsSource = list_serv;
                }
            }

        }

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
                    cmb_servicio_Vta.Text = item["Descripción"].ToString();
                    txt_descr_producto.Text = item["Descripción"].ToString();
                    if (cmb_servicio_Vta.Text =="")
                    {

                        txt_descr_producto.Text = item["Descripción"].ToString();
                        txt_Cant_producto.Text = item["Cantidad"].ToString();
                        txt_Cant_servicios.Text = "";
                    }
                    else
                    {
                        txt_Cant_servicios.Text = item["Cantidad"].ToString();
                        txt_descr_producto.Text = "";
                        txt_Cant_producto.Text = "";
                    }
                    //   txt_Id_producto.Text = item["Producto"].ToString();
                    //  txt_Sku_producto.Text = item["SKU"].ToString();
                    
                    
                }
                i++;
            }
        }
    }
}
