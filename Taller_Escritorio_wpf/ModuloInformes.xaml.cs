using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Taller_Datos;
using Taller_Negocio;


using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using Org.BouncyCastle.Crypto.Digests;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Taller_Escritorio_wpf
{
    /// <summary>
    /// Lógica de interacción para ModuloInformes.xaml
    /// </summary>
    public partial class ModuloInformes : System.Windows.Window
    {
        public ModuloInformes()
        {
            InitializeComponent();
            CargarComboBox();
            txt_Fecha_ini_info.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1000)));
            txt_Fecha_fn_info.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1000)));
            txt_Fecha_ini_Venta.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1000)));
            txt_Fecha_Final_Venta.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1000)));

            txt_Fecha_ini_info.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_Fecha_fn_info.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_Fecha_ini_Venta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_Fecha_Final_Venta.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BotonCerrar_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
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

        public void CargarComboBox()
        {
            Compartido_Negocio CompN = new Compartido_Negocio();

            //////////////LISTAR EMPLEADO INFORME PEDIDO/////////////////
            cmb_Empl_info.ItemsSource = CompN.ListarEmpleado();
            cmb_Empl_info.DisplayMemberPath = "p_nombre_empleado";
            cmb_Empl_info.SelectedValuePath = "id_empleado";



            //////////////LISTAR PROVEEDOR/////////////////
            cmb_prov_info.ItemsSource = CompN.ListarProveedor();
            cmb_prov_info.DisplayMemberPath = "razon_social_prov";
            cmb_prov_info.SelectedValuePath = "id_proveedor";

            //////////////LISTAR TIPO DOCUMENTO VENTA/////////////////
            cmb_Tipo_Doc_info_vta.ItemsSource = CompN.ListarDocumentos();
            cmb_Tipo_Doc_info_vta.DisplayMemberPath = "descr_documento";
            cmb_Tipo_Doc_info_vta.SelectedValuePath = "id_documento";


            //////////////LISTAR FAMILIA PRODUCTO VENTA/////////////////
            cmb_familiaProd_info_vta.ItemsSource = CompN.ListarFamProd();
            cmb_familiaProd_info_vta.DisplayMemberPath = "descr_familia";
            cmb_familiaProd_info_vta.SelectedValuePath = "id_familia_prod";


        }
        private void Limpiarpedido()
        {
            cmb_prov_info.SelectedValue = null;
            cmb_Empl_info.SelectedValue = null;
        }

        private void Btn_Busca_Ped_info_Click(object sender, RoutedEventArgs e)
        {
            Inf_Pedido_Negocio inf_pe = new Inf_Pedido_Negocio();
            List<Inf_pedido_dto> detalle = new List<Inf_pedido_dto>();
            System.Data.DataTable resp = new System.Data.DataTable();
            string proveedor = "";
            string empleado = "";
            if (cmb_prov_info.SelectedIndex == -1)
            {
                if (cmb_prov_info.SelectedIndex == -1)
                {
                    //// cmb_prov_info.SelectedIndex = 0;
                    proveedor = "0";
                }

            }
            else
            {
                proveedor = cmb_prov_info.SelectedValue.ToString();

            }
            if (cmb_Empl_info.SelectedIndex == -1)
            {
                if (cmb_Empl_info.SelectedIndex == -1)
                {
                    //cmb_Empl_info.SelectedIndex = 0;
                    empleado = "0";
                }
            }
            else
            {
                empleado = cmb_Empl_info.SelectedValue.ToString();
            }
            try
            {
                detalle = inf_pe.ListarPedido(proveedor, empleado,
                                              txt_Fecha_ini_info.SelectedDate.ToString(), txt_Fecha_fn_info.SelectedDate.ToString());
                Dt_G_list_pedido_info.ItemsSource = detalle;
                Limpiarpedido();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Busqueda no arrojó resultados");
                throw ex;
            }



        }

        private void Btn_Gene_Excel_Click(object sender, RoutedEventArgs e)
        {

            if (Dt_G_list_pedido_info.Items.Count == 0)
            {
                System.Windows.MessageBox.Show("No existen datos para exportar!");
            }
            else
            {
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                for (int j = 0; j < Dt_G_list_pedido_info.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true;
                    sheet1.Columns[j + 1].ColumnWidth = 15;
                    myRange.Value2 = Dt_G_list_pedido_info.Columns[j].Header;
                }
                int num = 0;
                for (int i = 0; i < Dt_G_list_pedido_info.Columns.Count; i++)
                {
                    int z = 0;
                    var SelectProd = Dt_G_list_pedido_info.ItemsSource;
                    var jsonValueToGet = JsonConvert.SerializeObject(SelectProd);

                    // lo convierte en un array
                    JArray jsonPreservar = JArray.Parse(jsonValueToGet.ToString());

                    //lo recorre para añadir al listado que luego se mostrará en la grilla
                    foreach (JObject item in jsonPreservar.Children<JObject>())
                    {
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[z + 2, num + 1];
                        string dato = Dt_G_list_pedido_info.Columns[num].Header.ToString();
                        if (dato != null)
                        {
                            myRange.Value2 = item[dato].ToString();
                        }
                        else
                        {
                            break;
                        }
                        z++;

                    }
                    num++;
                    if (num == 8)
                    {
                        break;
                    }

                }

            }



        }

        private void Btn_Gene_Excel_Vta_Click(object sender, RoutedEventArgs e)
        {
            if (Dt_G_list_venta.Items.Count == 0)
            {
                System.Windows.MessageBox.Show("No existen datos para exportar!");
            }
            else
            {
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                for (int j = 0; j < Dt_G_list_venta.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true;
                    sheet1.Columns[j + 1].ColumnWidth = 15;
                    myRange.Value2 = Dt_G_list_venta.Columns[j].Header;
                }
                int num = 0;
                for (int i = 0; i < Dt_G_list_venta.Columns.Count; i++)
                {
                    int z = 0;
                    var SelectProd = Dt_G_list_venta.ItemsSource;
                    var jsonValueToGet = JsonConvert.SerializeObject(SelectProd);

                    // lo convierte en un array
                    JArray jsonPreservar = JArray.Parse(jsonValueToGet.ToString());

                    //lo recorre para añadir al listado que luego se mostrará en la grilla
                    foreach (JObject item in jsonPreservar.Children<JObject>())
                    {
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[z + 2, num + 1];
                        string dato = Dt_G_list_venta.Columns[num].Header.ToString();
                        if (dato != null)
                        {
                            myRange.Value2 = item[dato].ToString();
                        }
                        else
                        {
                            break;
                        }
                        z++;

                    }
                    num++;
                    if (num == 8)
                    {
                        break;
                    }

                }


            }
        }
        private void LimpiarcmbVenta()
        {
            cmb_Tipo_Doc_info_vta.SelectedValue = null;
            cmb_familiaProd_info_vta.SelectedValue = null;
        }

        private void Btn_Busca_Vent_info_Click(object sender, RoutedEventArgs e)
        {

            Info_Venta_Negocio inf_vt = new Info_Venta_Negocio();
            List<Info_Venta_dto> detalle = new List<Info_Venta_dto>();
            System.Data.DataTable resp = new System.Data.DataTable();
            string TipoDoc = "";
            string TipoFam = "";
            if (cmb_Tipo_Doc_info_vta.SelectedIndex == -1)
            {
                if (cmb_Tipo_Doc_info_vta.SelectedIndex == -1)
                {
                    TipoDoc = "0";
                }
            }
            else
            {
                TipoDoc = cmb_Tipo_Doc_info_vta.SelectedValue.ToString();

            }
            if (cmb_familiaProd_info_vta.SelectedIndex == -1)
            {
                if (cmb_familiaProd_info_vta.SelectedIndex == -1)
                {
                    ;
                    TipoFam = "0";
                }
            }
            else
            {
                TipoFam = cmb_familiaProd_info_vta.SelectedValue.ToString();
            }
            try
            {
                detalle = inf_vt.ListarVenta(TipoDoc, TipoFam,
                                              txt_Fecha_ini_Venta.SelectedDate.ToString(), txt_Fecha_Final_Venta.SelectedDate.ToString());
                Dt_G_list_venta.ItemsSource = detalle;
                LimpiarcmbVenta();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Busqueda no arrojó resultados");
                throw ex;
            }


        }
    }
}