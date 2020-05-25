using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Taller_Datos;
using Taller_Negocio;

namespace Taller_Escritorio_wpf



{

    public enum PanelAdministrador
    {
        EMPLEADO,
        CLIENTE,
        PROVEEDOR,
        NINGUNO
        
    }

    /// <summary>
    /// Lógica de interacción para Administrador.xaml
    /// </summary>
    /// 

    public partial class Administrador : Window, INotifyPropertyChanged
    {
        private PanelAdministrador _PanelSeleccionado = PanelAdministrador.NINGUNO;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool PanelEmpleadoActivo
        {
            get
            {
                return this._PanelSeleccionado == PanelAdministrador.EMPLEADO;
                
            }
           
        }

        public PanelAdministrador PanelSeleccionado
        {
            get
            {
                return _PanelSeleccionado;
            }
            set
            {
                this._PanelSeleccionado = value;
                OnPropertyChanged("PanelEmpleadoActivo");
                OnPropertyChanged("PanelClienteActivo");
                OnPropertyChanged("PanelProveedorActivo");
                limpiarEmp();

            }
        }

        public bool PanelClienteActivo
        {
            get
            {
                return this._PanelSeleccionado == PanelAdministrador.CLIENTE;
            }
        }
        public bool PanelProveedorActivo
        {
            get
            {
                return this._PanelSeleccionado == PanelAdministrador.PROVEEDOR;

            }
        }

       
        public  Administrador()
        {
            InitializeComponent();
            CargarComboBoxEmpleado();
            CargarComboBoxCliente();
            CargarComboBoxProveedor();
        }
        /// <summary>
        /// Método que Carga de informacion los cmb de la vista
        /// </summary>
        /// 
        
        public void CargarComboBoxEmpleado()
        {
            //////////////LISTAR COMUNA//////////////////
            Comuna_Negocio comunaN = new Comuna_Negocio();
            Cmb_comuna_E.ItemsSource = comunaN.ListarComuna();
            Cmb_comuna_E.DisplayMemberPath = "desc_comuna";
            Cmb_comuna_E.SelectedValuePath = "id_comuna";

            ///////////////LISTAR CARGO/////////////////
            CargoEmp_Negocio cargoempN = new CargoEmp_Negocio();
            Cmb_cargo_E.ItemsSource = cargoempN.ListarCargoEmp();
            Cmb_cargo_E.DisplayMemberPath = "desc_cargo";
            Cmb_cargo_E.SelectedValuePath = "id_cargo";

        }
        
        public void CargarComboBoxCliente()
        {
            //////////////LISTAR TIPO CLIENTE//////////////////
            Tipo_Cliente_Negocio tipoN = new Tipo_Cliente_Negocio();
            Cmb_Tipo_Cliente.ItemsSource = tipoN.ListarTipoCliente();
            Cmb_Tipo_Cliente.DisplayMemberPath = "descr_tipo_cl";
            Cmb_Tipo_Cliente.SelectedValuePath = "id_tipo_cliente";

            ///////////////LISTAR COMUNA/////////////////
            Comuna_Negocio comunaN = new Comuna_Negocio();
            Cmb_comuna_C.ItemsSource = comunaN.ListarComuna();
            Cmb_comuna_C.DisplayMemberPath = "desc_comuna";
            Cmb_comuna_C.SelectedValuePath = "id_comuna";
        }
        public void CargarComboBoxProveedor()
        {

            ///////////////LISTAR COMUNA/////////////////
            Comuna_Negocio comunaN = new Comuna_Negocio();
            Cmb_comuna_P.ItemsSource = comunaN.ListarComuna();
            Cmb_comuna_P.DisplayMemberPath = "desc_comuna";
            Cmb_comuna_P.SelectedValuePath = "id_comuna";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BotonCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void frame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void Btn_Empleado_Click(object sender, RoutedEventArgs e)
        {
            this.PanelSeleccionado = PanelAdministrador.EMPLEADO;

        }

        private void Btn_Cliente_Click(object sender, RoutedEventArgs e)
        {
            this.PanelSeleccionado = PanelAdministrador.CLIENTE;
        }
        private void Btn_Proveedor_Click(object sender, RoutedEventArgs e)
        {
            this.PanelSeleccionado = PanelAdministrador.PROVEEDOR;
        }
       

        private void frame_Navigated_1(object sender, NavigationEventArgs e)
        {

        }
        
        private void limpiarEmp()
        {
            Txt_id_E.Text = string.Empty;
            Txt_Rut_Empleado.Text = string.Empty;
            Txt_Dv_Empleado.Text = string.Empty;
            Txt_P_Nombre_E.Text = string.Empty;
            Txt_S_Nombre_E.Text = string.Empty;
            Txt_P_Apellido_E.Text = string.Empty;
            Txt_S_Apellido_E.Text = string.Empty;
            Txt_Direccion_E.Text = string.Empty;
            Txt_numeracion_E.Text = string.Empty;
            Txt_Depto_E.Text = string.Empty;
            Txt_Fono_E.Text = string.Empty;
            Txt_Correo_E.Text = string.Empty;
            Txt_NombreU_E.Text = string.Empty;
            Txt_Contrasena_E.Password = string.Empty;
            Cmb_comuna_E.SelectedValue = null;
            Txt_id_taller_E.Text = string.Empty;
            Cmb_cargo_E.SelectedValue = null;
        }

        private void btn_Agregar_E_Click(object sender, RoutedEventArgs e)
        {
            Empleado_Negocio empN = new Empleado_Negocio();
            if (Txt_Rut_Empleado.Text != "" && Txt_Dv_Empleado.Text != "" && Txt_P_Nombre_E.Text != "" && Txt_S_Nombre_E.Text != "" && Txt_P_Apellido_E.Text != "" &&
               Txt_S_Apellido_E.Text != "" && Txt_Direccion_E.Text != "" && Txt_numeracion_E.Text != "" && Txt_Fono_E.Text != "" && Txt_Correo_E.Text != "" &&
               Txt_NombreU_E.Text != "" && Txt_Contrasena_E.Password != "" && Cmb_comuna_E.Text != "" && Txt_id_taller_E.Text != "" && Cmb_cargo_E.Text != "")
            {
                bool resultado = false;
                try
                {
                    resultado = empN.crearEmpleado(Txt_id_E.Text ,Txt_Rut_Empleado.Text, Txt_Dv_Empleado.Text, Txt_P_Nombre_E.Text, Txt_S_Nombre_E.Text, Txt_P_Apellido_E.Text, Txt_S_Apellido_E.Text,
                                                   Txt_Direccion_E.Text, Txt_numeracion_E.Text, Txt_Depto_E.Text,
                                                   Txt_Fono_E.Text, Txt_Correo_E.Text, Txt_NombreU_E.Text, Txt_Contrasena_E.Password,
                                                   Cmb_comuna_E.SelectedValue.ToString(), Txt_id_taller_E.Text, Cmb_cargo_E.SelectedValue.ToString());
                    if (resultado)
                    {
                        if( Txt_id_E.Text != "")
                        {
                            MessageBox.Show("Cliente Modificado");
                        }
                        else
                        {
                            MessageBox.Show("Cliente creado");
                            limpiarEmp();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Empleado no pudo ser creado");
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Empleado no pudo ser creado");
                    throw;
                    
                }

            }
            else
            {
                MessageBox.Show("Debe completar todos los campos");

            }

        }


        private void btn_Eliminar_E_Click(object sender, RoutedEventArgs e)
        {
            Empleado_Negocio empN = new Empleado_Negocio();
            if (Txt_Rut_Empleado.Text !="")
            {
                bool retorno = false;

                retorno = empN.EliminarEmp(int.Parse(Txt_id_E.Text));
                if (retorno)
                {

                    MessageBox.Show("Cliente Eliminado");
                    limpiarEmp();
                }
                else
                {
                    MessageBox.Show("Cliente no pudo ser Eliminado");
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar RUT para Eliminar cliente");

            }

            

        }

        private void btn_Buscar_E_Click(object sender, RoutedEventArgs e)
        {
            Empleado_Negocio empN = new Empleado_Negocio();
            DataTable resp = new DataTable();
            Compartido_Negocio comp = new Compartido_Negocio();
            if (Txt_Rut_Empleado.Text != "")
            {
                try
                {
                    resp = empN.ListarEmpleado(Txt_Rut_Empleado.Text);
                    if (resp.Rows.Count>0)
                    {
                        foreach (DataRow item in resp.Rows)
                        {
                            Txt_id_E.Text = item["ID_EMPLEADO"].ToString();
                            Txt_Dv_Empleado.Text = item["DV_EMPLEADO"].ToString();
                            Txt_P_Nombre_E.Text = item["p_nombre_empleado"].ToString();
                            Txt_S_Nombre_E.Text = item["s_nombre_empleado"].ToString();
                            Txt_P_Apellido_E.Text = item["p_apellido_empleado"].ToString();
                            Txt_S_Apellido_E.Text = item["s_apellido_empleado"].ToString();
                            Txt_Direccion_E.Text = item["direccion_empleado"].ToString();
                            Txt_numeracion_E.Text = item["numeracion_empleado"].ToString();
                            Txt_Fono_E.Text = item["fono_empleado"].ToString();
                            Txt_Depto_E.Text = item["depto_empleado"].ToString();
                            Txt_Correo_E.Text = item["correo_empleado"].ToString();
                            Txt_NombreU_E.Text = item["nombre_usu_empleado"].ToString();
                            Txt_Contrasena_E.Password = comp.DecrytedString(item["contrasena_empleado"].ToString());
                            Cmb_comuna_E.SelectedValue = item["id_comuna"].ToString();
                            Cmb_cargo_E.SelectedValue = item["id_cargo"].ToString();
                            Txt_id_taller_E.Text = item["id_taller"].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rut invalido");
                        limpiarEmp();
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
                MessageBox.Show("Debe ingresar un Rut para la busqueda");
            }


        }

        private void btn_Agregar_C_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Eliminar_C_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_agregar_P_Click(object sender, RoutedEventArgs e)
        {
            Proveedor_Negocio provNeg = new Proveedor_Negocio();
            bool resultado = false;
            try
            {
                resultado = provNeg.CrearProveedor(Txt_Razon_S_.Text, Txt_Giro.Text, Txt_Rut_Proveedor.Text, Txt_Dv_Proveedor.Text, Txt_Direccion_P.Text, Txt_numeracion_P.Text,
                                               Txt_Fono_P.Text, Txt_Correo_P.Text, Txt_NombreU_P.Text,
                                               Txt_Contrasena_P.Text, 
                                               Cmb_comuna_P.SelectedValue.ToString());
                if (resultado)
                {
                    MessageBox.Show("Proveedor creado");
                   
                }
                else
                {
                    MessageBox.Show("Proveedor no pudo ser creado");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Proveedor no pudo ser creado2");
                throw;

            }

        }

        private void btn_Eliminar_P_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_MenuP_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MenuPrincipal ventana = new MenuPrincipal();
            ventana.ShowDialog();
        }
    }
}
