using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Taller_Datos;
using Taller_Datos.DataSet;
using Taller_Escritorio_wpf.RPT;
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
            txt_Fecha_V.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if (Application.Current.Properties["Perfil"].ToString() == "2")
            {
                Btn_Generar_venta.IsEnabled = false;


            }
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
            cmb_Documento_Vta.ItemsSource =Docu_Neg.ListarDocumento();
            cmb_Documento_Vta.DisplayMemberPath = "descr_documento";
            cmb_Documento_Vta.SelectedValuePath = "id_documento";

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

            Cliente_Negocio ClienN = new Cliente_Negocio();
            cmb_Cliente_Vta.ItemsSource = ClienN.ListarClienteCMB();
            cmb_Cliente_Vta.DisplayMemberPath = "nombre_cliente";
            cmb_Cliente_Vta.SelectedValuePath = "id_cliente";

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
            txt_id_pro.Text = string.Empty;
        }

        /////////////////////////////////////////////////
        /////////////////////PRODUCTO////////////////////
        ////////////////////////////////////////////////////
        private void Btn_Agregar_p_Click(object sender, RoutedEventArgs e)
        {
            decimal total = 0;
            Producto_Negocio Prod_Neg = new Producto_Negocio();
            List<Venta_dto> list_venta = new List<Venta_dto>();

            if(cmb_Documento_Vta.Text == "")
            {
                MessageBox.Show("Favor de seleccionar tipo de documento");
                limpiar();

            }
            else
            {

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
                    string id_prod = txt_id_pro.Text.ToString();
                    int cantidad_prod = int.Parse(txt_Cant_producto.Text);
                    string txt_tipo = "P";
                    decimal subT = 0;
                    decimal IVA = 0;


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
                            entidad.T = item["T"].ToString();


                            entidad.Total = decimal.Parse(item["Total"].ToString()).ToString("n2");
                            entidad.Precio = decimal.Parse(item["Precio"].ToString()).ToString("n2");

                            if (cmb_Documento_Vta.Text == "Factura")
                            {
                                subT = ((total + decimal.Parse(entidad.Total)) * 81) / 100;
                                IVA = ((total + decimal.Parse(entidad.Total)) * 19) / 100;
                                total = total + decimal.Parse(entidad.Total);
                            }
                            else
                            {
                                subT = ((total + decimal.Parse(entidad.Total)) * 81) / 100;
                                IVA = ((total + decimal.Parse(entidad.Total)) * 19) / 100;
                                total = total + decimal.Parse(entidad.Total);
                            }

                            list_venta.Add(entidad);
                            if (id_prod == item["ID"].ToString() && item["T"].ToString() == "P")
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


                            entidad.T = txt_tipo;
                            if (cmb_Documento_Vta.Text == "Factura")
                            {
                                subT = ((total + decimal.Parse(entidad.Total)) * 81) / 100;
                                IVA = ((total + decimal.Parse(entidad.Total)) * 19) / 100;
                                total = total + decimal.Parse(entidad.Total);
                            }
                            else
                            {
                                subT = ((total + decimal.Parse(entidad.Total)) * 81) / 100;
                                IVA = ((total + decimal.Parse(entidad.Total)) * 19) / 100;
                                total = total + decimal.Parse(entidad.Total);
                            }

                            list_venta.Add(entidad);

                        }

                    }

                    // actualiza variable de sesion con los datos actuales de la grilla
                    var jsonValueToSave = JsonConvert.SerializeObject(list_venta);
                    Application.Current.Properties["ListadoVenta"] = jsonValueToSave;

                    txt_Sub.Text = subT.ToString("n2");
                    txt_iva.Text = IVA.ToString("n2");
                    txt_total.Text = total.ToString("n2");
                    Dt_G_list_pedido.ItemsSource = list_venta;
                    limpiar();

                }

            }


        }

        private void Btn_Editar_p_Click(object sender, RoutedEventArgs e)
        {
            List<Venta_dto> listado_pro = new List<Venta_dto>();

            if (txt_id_pro.Text == "")
            {
                MessageBox.Show("Debe seleccionar un producto");
            }
            else
            {
                bool editar = false;
                int id = int.Parse(txt_id_pro.Text.ToString());
                int cantidad = int.Parse(txt_Cant_producto.Text.ToString());
                decimal total = 0;
                decimal subT = 0;
                decimal IVA = 0;
                //   Producto_Negocio Prod_Neg = new Producto_Negocio();

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
                       // entidad.SKU = item["SKU"].ToString();
                        entidad.Descripción = item["Descripción"].ToString();

                        if (id == int.Parse(item["ID"].ToString()) &&  item["T"].ToString() == "P")
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
                        entidad.T = item["T"].ToString();
                        entidad.Precio = decimal.Parse(item["Precio"].ToString()).ToString("n2");

                        if (cmb_Documento_Vta.Text == "Factura")
                        {
                            subT = ((total + decimal.Parse(entidad.Total)) * 81) / 100;
                            IVA = ((total + decimal.Parse(entidad.Total)) * 19) / 100;
                            total = total + decimal.Parse(entidad.Total);
                        }
                        else
                        {
                            subT = ((total + decimal.Parse(entidad.Total)) * 81) / 100;
                            IVA = ((total + decimal.Parse(entidad.Total)) * 19) / 100;
                            total = total + decimal.Parse(entidad.Total);
                        }

                      //  total = total + decimal.Parse(entidad.Total);
                        listado_pro.Add(entidad);
                    }
                }

                var jsonValueToSave = JsonConvert.SerializeObject(listado_pro);
                Application.Current.Properties["ListadoVenta"] = jsonValueToSave;
                txt_Sub.Text = subT.ToString("n2");
                txt_iva.Text = IVA.ToString("n2");
                txt_total.Text = total.ToString("n2");
                Dt_G_list_pedido.ItemsSource = listado_pro;

                if (editar == true)
                {

                    MessageBox.Show("Cantidad Actualizada");
                    limpiar();
                }
                else
                {
                    MessageBox.Show("El Producto a actualizar no existe!");
                    limpiar();
                }
            }

        }

        private void Btn_Eliminar_p_Click(object sender, RoutedEventArgs e)
        {
            List<Venta_dto> list_prod = new List<Venta_dto>();
            if (txt_descr_producto.Text == "")
            {
                MessageBox.Show("Debe seleccionar un producto");
                limpiar();
            }
            else
            {
                bool eliminado = false;
                string id_prod = txt_id_pro.Text.ToString();
                int cantidad_prod = int.Parse(txt_Cant_producto.Text);
                decimal total = 0;
                decimal subT = 0;
                decimal IVA = 0;

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
                        Venta_dto entidad = new Venta_dto();
                        entidad.ID = int.Parse(item["ID"].ToString());
                        entidad.Descripción = item["Descripción"].ToString();
                        entidad.Cantidad = int.Parse(item["Cantidad"].ToString());
                        entidad.Total = decimal.Parse(item["Total"].ToString()).ToString("n2");
                        entidad.Precio = decimal.Parse(item["Precio"].ToString()).ToString("n2");
                        entidad.T = item["T"].ToString();
                        if (cmb_Documento_Vta.Text == "Factura")
                        {
                            subT = ((total + decimal.Parse(entidad.Total)) * 81) / 100;
                            IVA = ((total + decimal.Parse(entidad.Total)) * 19) / 100;
                            total = total + decimal.Parse(entidad.Total);
                        }
                        else
                        {
                            subT = ((total + decimal.Parse(entidad.Total)) * 81) / 100;
                            IVA = ((total + decimal.Parse(entidad.Total)) * 19) / 100;
                            total = total + decimal.Parse(entidad.Total);
                        }

                        if (id_prod == item["ID"].ToString() && item["T"].ToString() == "P")
                        {
                            eliminado = true;
                            total = total - decimal.Parse(entidad.Total);

                        }
                        else
                        {
                            list_prod.Add(entidad);
                        }
                    }
                    var jsonValueToSave = JsonConvert.SerializeObject(list_prod);
                    Application.Current.Properties["ListadoVenta"] = jsonValueToSave;

                    txt_Sub.Text = subT.ToString("n2");
                    txt_iva.Text = IVA.ToString("n2");
                    txt_total.Text = total.ToString("n2");

                    if (eliminado == true)
                    {

                        MessageBox.Show("Producto eliminado");

                    }
                    else
                    {
                        MessageBox.Show("El producto no existe!");

                    }
                    Dt_G_list_pedido.ItemsSource = list_prod;
                }
                limpiar();
            }

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
                        txt_id_pro.Text = item["id_producto"].ToString();
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

        /// ///////////////////////////////////////
        /// //////////SERVICIOS////////////////////
        /// ///////////////////////////////////////

        private void Btn_Agregar_serv_Click(object sender, RoutedEventArgs e)
        {

            decimal total = 0;
            Servicio_Negocio Serv_Neg = new Servicio_Negocio();
            List<Servicios_dto> list_serv = new List<Servicios_dto>();

            if (cmb_Documento_Vta.Text == "")
            { MessageBox.Show("Favor de seleccionar tipo de documento");
                limpiar();
            }
            else
            {
                if (cmb_servicio_Vta.Text == "")
                {
                    MessageBox.Show("Debe seleccionar un servicio");
                }
                else
                {
                    bool existe = false;
                    string id_serv = cmb_servicio_Vta.SelectedValue.ToString();
                    int cantidad_serv = int.Parse(txt_Cant_servicios.Text);
                    string txt_tipo = "S";
                    decimal subT = 0;
                    decimal IVA = 0;

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
                            entidad.T = item["T"].ToString();


                            if (cmb_Documento_Vta.Text == "Factura")
                            {
                                subT = ((total + decimal.Parse(entidad.Total)) * 81) / 100;
                                IVA = ((total + decimal.Parse(entidad.Total)) * 19) / 100;
                                total = total + decimal.Parse(entidad.Total);
                            }
                            else
                            {
                                subT = ((total + decimal.Parse(entidad.Total)) * 81) / 100;
                                IVA = ((total + decimal.Parse(entidad.Total)) * 19) / 100;
                                total = total + decimal.Parse(entidad.Total);
                            }

                            list_serv.Add(entidad);
                            if (id_serv == item["ID"].ToString() && item["T"].ToString() == "S")
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
                            entidad.T = txt_tipo;


                            if (cmb_Documento_Vta.Text == "Factura")
                            {
                                subT = ((total + decimal.Parse(entidad.Total)) * 81) / 100;
                                IVA = ((total + decimal.Parse(entidad.Total)) * 19) / 100;
                                total = total + decimal.Parse(entidad.Total);
                            }
                            else
                            {
                                subT = ((total + decimal.Parse(entidad.Total)) * 81) / 100;
                                IVA = ((total + decimal.Parse(entidad.Total)) * 19) / 100;
                                total = total + decimal.Parse(entidad.Total);
                            }
                            list_serv.Add(entidad);
                        }

                    }

                    // actualiza variable de sesion con los datos actuales de la grilla
                    var jsonValueToSave = JsonConvert.SerializeObject(list_serv);
                    Application.Current.Properties["ListadoVenta"] = jsonValueToSave;


                    txt_Sub.Text = subT.ToString("n2");
                    txt_iva.Text = IVA.ToString("n2");
                    txt_total.Text = total.ToString("n2");
                    Dt_G_list_pedido.ItemsSource = list_serv;

                }

                limpiar();

            }
            
        }

        private void Btn_Eliminar_Servi_Click(object sender, RoutedEventArgs e)
        {
            List<Servicios_dto> list_serv = new List<Servicios_dto>();
            if (cmb_servicio_Vta.Text == "")
            {
                MessageBox.Show("Debe seleccionar un servicio");
                limpiar();
            }
            else
            {
                bool eliminado = false;
                string id_prod = cmb_servicio_Vta.SelectedValue.ToString();
                int cantidad_prod = int.Parse(txt_Cant_servicios.Text);
                decimal total = 0;
                decimal subT = 0;
                decimal IVA = 0;

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
                        entidad.T = item["T"].ToString();

                        if (cmb_Documento_Vta.Text == "Factura")
                        {
                            subT = ((total + decimal.Parse(entidad.Total)) * 81) / 100;
                            IVA = ((total + decimal.Parse(entidad.Total)) * 19) / 100;
                            total = total + decimal.Parse(entidad.Total);
                        }
                        else
                        {
                            subT = ((total + decimal.Parse(entidad.Total)) * 81) / 100;
                            IVA = ((total + decimal.Parse(entidad.Total)) * 19) / 100;
                            total = total + decimal.Parse(entidad.Total);
                        }

                        if (id_prod == item["ID"].ToString() && item["T"].ToString() == "S" )
                        {
                            eliminado = true;
                            total = total - decimal.Parse(entidad.Total);
                            
                        }
                        else
                        {
                            list_serv.Add(entidad);
                        }
                    }
                    var jsonValueToSave = JsonConvert.SerializeObject(list_serv);
                    Application.Current.Properties["ListadoVenta"] = jsonValueToSave;

                    txt_Sub.Text = subT.ToString("n2");
                    txt_iva.Text = IVA.ToString("n2");
                    txt_total.Text = total.ToString("n2");

                    if (eliminado == true)
                    {

                        MessageBox.Show("Servicio eliminado");

                    }
                    else
                    {
                        MessageBox.Show("El Servicio no existe!");

                    }
                    

                    Dt_G_list_pedido.ItemsSource = list_serv;
                }
                limpiar();
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

                    if(item["T"].ToString() == "S")
                    {
                        cmb_servicio_Vta.Text = item["Descripción"].ToString();
                        txt_Cant_servicios.Text = item["Cantidad"].ToString();

                        txt_descr_producto.Text = "";
                        txt_Cant_producto.Text = "";
                        txt_stock.Text = "";
                        txt_id_pro.Text = "";
                    }
                    else
                    {
                        
                        txt_descr_producto.Text = item["Descripción"].ToString(); ;
                        txt_Cant_producto.Text = item["Cantidad"].ToString();
                        txt_id_pro.Text = item["ID"].ToString();

                        cmb_servicio_Vta.SelectedValue = null;
                        txt_Cant_servicios.Text = "";
                    }
                    
                    
                }
                i++;
            }
        }

        //////////////////////////////////////
        /////////////////VENTA////////////////
        //////////////////////////////////////
        private void Btn_Generar_venta_Click(object sender, RoutedEventArgs e)
        {
            BoletaVenta rpt = new BoletaVenta();
            VentaHDR dataset = new VentaHDR();
            ReportDocument doc = new ReportDocument();


            string cabecera = "";
            txt_Id_venta.Text = "";
            bool restar = false;
            Producto_Negocio producto_Neg = new Producto_Negocio();
            Venta_Negocio Venta_Neg = new Venta_Negocio();
            Deuda_Negocio deudaN = new Deuda_Negocio();
            if (cmb_Cliente_Vta.Text == "" || cmb_Documento_Vta.Text == "")
            {
                if (cmb_Cliente_Vta.Text == "" && cmb_Documento_Vta.Text == "")
                {
                    MessageBox.Show("Debe seleccionar un cliente y tipo de documento");
                }
                else
                {
                    if (cmb_Cliente_Vta.Text == "")
                    {
                        MessageBox.Show("Debe seleccionar un cliente");

                    }
                    if (cmb_Documento_Vta.Text == "")
                    {
                        MessageBox.Show("Debe seleccionar un tipo de documento");

                    }
                }
            }
            else
            {
                if (Application.Current.Properties["ListadoVenta"] == null || Application.Current.Properties["ListadoVenta"].ToString() == "[]")
                {
                    MessageBox.Show("Debe ingresar al menos un Producto o Servicio");
                }
                else
                {
                    cabecera = Venta_Neg.CrearVentaHDR(txt_Fecha_V.Text, txt_iva.Text, txt_total.Text, cmb_Cliente_Vta.SelectedValue.ToString(), cmb_Documento_Vta.SelectedValue.ToString(), cmb_taller_Vta.SelectedValue.ToString());
                    if (cmb_Documento_Vta.Text == "Pagare")
                    {
                        string estadodeduda = "1";
                        deudaN.CrearDeuda(txt_Fecha_V.Text, estadodeduda, txt_total.Text, cabecera, cmb_Cliente_Vta.SelectedValue.ToString());
                    }

                    try
                    {

                        var row = dataset.Tables["VentaHdr"].NewRow();
                        row["Numero_Doc"] = "N° Documento: " + cabecera;
                        row["Fecha"] = "Fecha: " + txt_Fecha_V.Text;
                        row["Tipo_Doc"] = "Tipo Documento: " + cmb_Documento_Vta.Text;
                        row["Nombre_Cliente"] = "Nombre Cliente: " + cmb_Cliente_Vta.Text;
                        row["SubTotal"] = "SUBTOTAL $ " + txt_Sub.Text;
                        row["IVA"] = "IVA $ " + txt_iva.Text;
                        row["Empresa_Nombre"] = cmb_taller_Vta.Text;
                        row["Total"] = "TOTAL $ " + txt_total.Text;

                        ///campos en duro para pdf//////

                        row["rut"] =       "RUT: 78.598.553 - 9";
                        row["giro"] =      "GIRO: Venta Mantenimiento y Reparación de Vehiculos Y sus Partes Piezas Y Accesorios";
                        row["fono"] =      "FONO: 98758789";
                        row["direccion"] = "DIRECCIÓN: Alonso de Ovale 1586 , Santiago Centro";

                        row["idp_hdr"]=       "ID";
                        row["nombrep_hdr"] =  "Descripción";
                        row["tipo_hdr"] =     "Tipo";
                        row["cantidad_hdr"] = "Cantidad";
                        row["precio_hdr"] =   "Precio";
                        row["total_hdr"] =    "Total";
                        ///////////////////////////////////////////
                        ///
                        dataset.Tables["VentaHdr"].Rows.Add(row);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    if (Application.Current.Properties["ListadoVenta"] != null)
                    {
                        // trae lo que esta en la variable de sesion77k
                        var jsonValueToGet = JsonConvert.DeserializeObject(Application.Current.Properties["ListadoVenta"].ToString());

                        // lo convierte en un array
                        JArray jsonPreservar = JArray.Parse(jsonValueToGet.ToString());

                        //lo recorre para añadir al listado que luego se mostrará en la grilla
                        foreach (JObject item in jsonPreservar.Children<JObject>())
                        {
                            var precio = Math.Round(decimal.Parse(item["Precio"].ToString()), 0).ToString().Replace(".", "");
                            var total = Math.Round(decimal.Parse(item["Total"].ToString()), 0).ToString().Replace(".", "");


                            // estos datos vienen de la grilla, creamosla entidad para añadir al listado
                            var respuesta = Venta_Neg.CrearVentaDet(item["Cantidad"].ToString(), precio, cabecera, total, item["T"].ToString(), item["ID"].ToString());
                            if (item["T"].ToString() == "P")
                            {
                                restar = producto_Neg.Restar_cant_prod(item["ID"].ToString(),
                                                item["Cantidad"].ToString());
                            }

                            var rowlist = dataset.Tables["Venta_DT"].NewRow();

                            rowlist["Descripcion"] = item["Descripción"].ToString();
                            rowlist["Id_Prod_Serv"] = item["ID"].ToString();
                            // rowlist["SKU"] = ;
                            rowlist["Tipo"] = item["T"].ToString();
                            rowlist["Cantidad"] = item["Cantidad"].ToString();
                            rowlist["Precio"] = item["Precio"].ToString();
                            rowlist["Total_Prod_Serv"] = item["Total"].ToString();

                            dataset.Tables["Venta_DT"].Rows.Add(rowlist);

                        }

                        txt_Id_venta.Text = cabecera;
                        MessageBox.Show("Venta generado con el Numero: " + txt_Id_venta.Text);
                    }

                    string reporte = "BoletaVenta.rpt";
                    string ruta = @"C:\Users\Diaz-Olivares\Documents\Proyectos\TallerMecanico\Taller_Escritorio_wpf\RPT\";
                    string rutaFinal = ruta + reporte;
                    doc.Load(rutaFinal);
                    doc.SetDataSource(dataset);
                    try
                    {
                        string nombreDoc = "Documento_Venta" + cabecera + DateTime.Now.Millisecond + ".pdf";
                        string rutaGuardar = @"C:\RPT\" + nombreDoc;
                        doc.ExportToDisk(ExportFormatType.PortableDocFormat, rutaGuardar);
                        Process.Start(rutaGuardar);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
        }

        private void Btn_Nueva_Vta_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ModuloVenta ventana = new ModuloVenta();
            ventana.ShowDialog();
        }

        private void Btn_pagare_Vta_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ModuloPagare ventana = new ModuloPagare();
            ventana.ShowDialog();
        }
    }
}
