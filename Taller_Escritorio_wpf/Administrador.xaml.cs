using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
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
        private PanelAdministrador _PanelSeleccionado = PanelAdministrador.EMPLEADO;
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
            cmb_id_taller_E.SelectedValue = 1;
            cmb_id_taller_C.SelectedValue = 1;
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

            TallerNegocio TallerN = new TallerNegocio();
            //////////////LISTAR taller/////////////////
            cmb_id_taller_E.ItemsSource = TallerN.ListarTaller();
            cmb_id_taller_E.DisplayMemberPath = "nombre";
            cmb_id_taller_E.SelectedValuePath = "id_taller";

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

            TallerNegocio TallerN = new TallerNegocio();
            //////////////LISTAR taller/////////////////
            cmb_id_taller_C.ItemsSource = TallerN.ListarTaller();
            cmb_id_taller_C.DisplayMemberPath = "nombre";
            cmb_id_taller_C.SelectedValuePath = "id_taller";
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
        private void btn_MenuP_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MenuPrincipal ventana = new MenuPrincipal();
            ventana.ShowDialog();
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
            limpiarEmp();

        }

        private void Btn_Cliente_Click(object sender, RoutedEventArgs e)
        {
            this.PanelSeleccionado = PanelAdministrador.CLIENTE;
            limpiarCLiente();
        }
        private void Btn_Proveedor_Click(object sender, RoutedEventArgs e)
        {
            this.PanelSeleccionado = PanelAdministrador.PROVEEDOR;
        }
       

        private void frame_Navigated_1(object sender, NavigationEventArgs e)
        {

        }

        ////////////////////////////////////////////////////////////
        /////////////////EMPLEADO//////////////////////////////////
        ///////////////////////////////////////////////////////////
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
            Cmb_cargo_E.SelectedValue = null;
            chk_Fiado.IsChecked = false;
        }

        private void btn_Agregar_E_Click(object sender, RoutedEventArgs e)
        {
            bool resp = false;
            Empleado_Negocio empN = new Empleado_Negocio();
            if (Txt_Rut_Empleado.Text == "" || Txt_Dv_Empleado.Text == "" || Txt_P_Nombre_E.Text == "" || Txt_S_Nombre_E.Text == "" || Txt_P_Apellido_E.Text == "" ||
                Txt_S_Apellido_E.Text == "" || Txt_Direccion_E.Text == "" || Txt_numeracion_E.Text == "" || Txt_Fono_E.Text == "" || Txt_Correo_E.Text == "" ||
                Txt_NombreU_E.Text == "" || Txt_Contrasena_E.Password == "" || Cmb_comuna_E.Text == "" ||  Cmb_cargo_E.Text == "")
            {
                if (Txt_Rut_Empleado.Text == "" && Txt_Dv_Empleado.Text == "" && Txt_P_Nombre_E.Text == "" && Txt_S_Nombre_E.Text == "" && Txt_P_Apellido_E.Text == "" &&
                   Txt_S_Apellido_E.Text == "" && Txt_Direccion_E.Text == "" && Txt_numeracion_E.Text == "" && Txt_Fono_E.Text == "" && Txt_Correo_E.Text == "" &&
                   Txt_NombreU_E.Text == "" && Txt_Contrasena_E.Password == "" && Cmb_comuna_E.Text == "" && Cmb_cargo_E.Text == "")
                {
                    MessageBox.Show("Debe completar todos los campos");
                }
                else
                {
                    if (Txt_Rut_Empleado.Text == "" && !resp )
                    {
                        MessageBox.Show("Debe ingresar Rut del empleado");
                        resp = true ;
                    }
                    if (Txt_Dv_Empleado.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar dígito verificador ");
                        resp = true;
                    }
                    if (Txt_P_Nombre_E.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar primer nombre ");
                        resp = true;
                    }
                    if (Txt_S_Nombre_E.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar segundo nombre");
                        resp = true;
                    }
                    if (Txt_P_Apellido_E.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar apellido paterno");
                        resp = true;
                    }
                    if (Txt_S_Apellido_E.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar apellido materno ");
                        resp = true;
                    }
                    if (Txt_Direccion_E.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar dirección");
                        resp = true;
                    }
                    if (Txt_numeracion_E.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar numeración de domicilio ");
                        resp = true;
                    }
                    if ( Txt_Fono_E.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar número de teléfono");
                        resp = true;
                    }
                    if (Txt_Correo_E.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar correo");
                        resp = true;
                    }
                    if (Txt_NombreU_E.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar nombre de usuario");
                        resp = true;
                    }
                    if (Txt_Contrasena_E.Password == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar una contraseña");
                        resp = true;
                    }
                    if (Cmb_comuna_E.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe selecionar comuna ");
                        resp = true;
                    }
                    if (Cmb_cargo_E.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe seleccionar cargo del empleado");
                        resp = true;
                    }
                }

            }
            else
            {             
                bool resultado = false;
                try
                {
                    resultado = empN.crearEmpleado(Txt_id_E.Text, Txt_Rut_Empleado.Text, Txt_Dv_Empleado.Text, Txt_P_Nombre_E.Text, Txt_S_Nombre_E.Text, Txt_P_Apellido_E.Text, Txt_S_Apellido_E.Text,
                                                   Txt_Direccion_E.Text, Txt_numeracion_E.Text, Txt_Depto_E.Text,
                                                   Txt_Fono_E.Text, Txt_Correo_E.Text, Txt_NombreU_E.Text, Txt_Contrasena_E.Password,
                                                   Cmb_comuna_E.SelectedValue.ToString(), cmb_id_taller_E.SelectedValue.ToString(), Cmb_cargo_E.SelectedValue.ToString());
                    if (resultado)
                    {
                        if (Txt_id_E.Text != "")
                        {
                            MessageBox.Show("Empleado Modificado");
                        }
                        else
                        {
                            MessageBox.Show("Empleado Registrado");
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
        }


        private void btn_Eliminar_E_Click(object sender, RoutedEventArgs e)
        {
            Empleado_Negocio empN = new Empleado_Negocio();
            if (Txt_Rut_Empleado.Text !="")
            {
                bool retorno = false;
                string msj= "Esta seguro de eliminar este empleado";
                string titulo = "ELIMINAR EMPLEADO";              
                var resp = MessageBox.Show(msj, titulo,
                     MessageBoxButton.YesNo);
                if (resp == MessageBoxResult.Yes)
                {
                    retorno = empN.EliminarEmp(int.Parse(Txt_id_E.Text));
                    if (retorno)
                    {
                        MessageBox.Show("Empleado Eliminado");
                        limpiarEmp();
                    }
                    else
                    {
                        MessageBox.Show("El empleado no pudo ser eliminado");
                    }
                }             
            }
            else
            {
                MessageBox.Show("Debe ingresar RUT para eliminar un empleado");
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
                            cmb_id_taller_E.SelectedValue = item["id_taller"].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rut invalido");
                        //limpiarEmp();
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

        private void Txt_Rut_Empleado_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "."))
            {
                e.Handled = true;
            }
        }

        private void Txt_Dv_Empleado_KeyUp(object sender, KeyEventArgs e)
        {
            if (Txt_Rut_Empleado.Text != "")
            {
                if (!ValidaRut(Txt_Rut_Empleado.Text))
                {
                    MessageBox.Show("Dígito verificador no corresponde a rut ingresado");
                    Txt_Dv_Empleado.Text = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar Rut!");
                Txt_Dv_Empleado.Text = string.Empty;
                Txt_Rut_Empleado.Focus();
            }

        }

        /// <summary>
        /// Metodo de validación de rut con digito verificador
        /// dentro de la cadena
        /// </summary>
        /// <param name="rut">string</param>
        /// <returns>booleano</returns>
        public bool ValidaRut(string rut)
        {

            var largo = Txt_Dv_Empleado.Text.Length;
            if (largo > 1)
            {
                var nuevoValor = Txt_Dv_Empleado.Text.Substring(0, 1);
                Txt_Dv_Empleado.Text = string.Empty;
                Txt_Dv_Empleado.Text = nuevoValor;
                Txt_Dv_Empleado.Focus();
                Txt_Dv_Empleado.SelectionStart = Txt_Dv_Empleado.Text.Length;
            }

            if (Txt_Dv_Empleado.Text != Digito(int.Parse(rut)))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// método que calcula el digito verificador a partir
        /// de la mantisa del rut
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public static string Digito(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
            {
                return "0";
            }
            else if (suma == 10)
            {
                return "K";
            }
            else
            {
                return suma.ToString();
            }
        }

        private void Txt_Rut_Empleado_LostFocus(object sender, RoutedEventArgs e)
        {
            var largo = Txt_Rut_Empleado.Text.Length;
            if (largo != 7 && largo != 8)
            {
                MessageBox.Show("Largo del Rut no corresponde");
            }
            else
            {
                Txt_Dv_Empleado.Focus();
            }

        }


        ////////////////////////////////////////////////////////////
        /////////////////CLIENTE///////////////////////////////////
        ///////////////////////////////////////////////////////////


        private void limpiarCLiente()
        {
            Txt_id_C.Text = string.Empty;
            Txt_Rut_Cliente.Text = string.Empty;
            Txt_Dv_Empleado.Text = string.Empty;
            Txt_P_Nombre_C.Text = string.Empty;
            Txt_S_Nombre_C.Text = string.Empty;
            Txt_P_Apellido_C.Text = string.Empty;
            Txt_S_Apellido_C.Text = string.Empty;
            Txt_Direccion_C.Text = string.Empty;
            Txt_numeracion_C.Text = string.Empty;
            Txt_Depto_C.Text = string.Empty;
            Txt_Fono_C.Text = string.Empty;
            Txt_Correo_C.Text = string.Empty;
            Txt_NombreU_C.Text = string.Empty;
            Txt_Contrasena_C.Password = string.Empty;
            Cmb_comuna_C.SelectedValue = null;
            Cmb_Tipo_Cliente.SelectedValue = null;
        }
        private void btn_Agregar_C_Click(object sender, RoutedEventArgs e)
        {
            Cliente_Negocio clienteN = new Cliente_Negocio();
            bool resp = false;
            if (Txt_Rut_Cliente.Text == "" || Txt_Dv_Cliente.Text == "" || Txt_P_Nombre_C.Text == "" || Txt_S_Nombre_C.Text == "" || Txt_P_Apellido_C.Text == "" || Txt_S_Apellido_C.Text == "" ||
                                                   Txt_Direccion_C.Text == "" || Txt_numeracion_C.Text == "" || Txt_Fono_C.Text == "" || Txt_Correo_C.Text == "" ||  
                                                   Txt_NombreU_C.Text == "" || Txt_Contrasena_C.Password == "" || Cmb_Tipo_Cliente.Text == "" || Cmb_comuna_C.Text == "" )
            {
                if (Txt_Rut_Cliente.Text == "" && Txt_Dv_Cliente.Text == "" && Txt_P_Nombre_C.Text == "" && Txt_S_Nombre_C.Text == "" && Txt_P_Apellido_C.Text == "" &&
                   Txt_S_Apellido_C.Text == "" && Txt_Direccion_C.Text == "" && Txt_numeracion_C.Text == "" &&  Txt_Fono_C.Text == "" &&
                   Txt_Correo_C.Text == ""  && Txt_NombreU_C.Text == "" && Txt_Contrasena_C.Password == "" && Cmb_Tipo_Cliente.Text == "" && 
                   Cmb_comuna_C.Text == "" )
                {
                    MessageBox.Show("Debe completar todos los campos");
                }
                else
                {
                    if (Txt_Rut_Cliente.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar Rut del cliente");
                        resp = true;
                    }
                    if (Txt_Dv_Cliente.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar dígito verificador ");
                        resp = true;
                    }
                    if (Txt_P_Nombre_C.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar primer nombre ");
                        resp = true;
                    }
                    if (Txt_S_Nombre_C.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar segundo nombre");
                        resp = true;
                    }
                    if (Txt_P_Apellido_C.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar apellido paterno");
                        resp = true;
                    }
                    if (Txt_S_Apellido_C.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar apellido materno ");
                        resp = true;
                    }
                    if (Txt_Direccion_C.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar dirección");
                        resp = true;
                    }
                    if (Txt_numeracion_C.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar numeración del domicilio ");
                        resp = true;
                    }
                    if (Txt_Fono_C.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar número de telefono");
                        resp = true;
                    }
                    if (Txt_Correo_C.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar correo");
                        resp = true;
                    }
                    if (Txt_NombreU_C.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar nombre de usuario");
                        resp = true;
                    }
                    if (Txt_Contrasena_C.Password == "" && !resp)
                    {
                        MessageBox.Show("Debe ingresar una contraseña");
                        resp = true;
                    }
                    if (Cmb_Tipo_Cliente.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe elecionar tipo de cliente ");
                        resp = true;
                    }
                    if (Cmb_comuna_C.Text == "" && !resp)
                    {
                        MessageBox.Show("Debe seleccionar comuna ");
                        resp = true;
                    }
                }
            }
            else
            {
                bool resultado = false;
                try
                {
                    var fiado1 = "0";
                    if (chk_Fiado.IsChecked == true)
                    {
                        fiado1 = "1";
                        chk_Fiado.IsChecked = true;
                    }
                    else
                    {
                        fiado1 = "0";
                        chk_Fiado.IsChecked = false;
                    }
                    resultado = clienteN.CrearCliente(Txt_Rut_Cliente.Text, Txt_Dv_Cliente.Text, Txt_P_Nombre_C.Text, Txt_S_Nombre_C.Text, Txt_P_Apellido_C.Text, Txt_S_Apellido_C.Text,
                                                   Txt_Direccion_C.Text, Txt_numeracion_C.Text, Txt_Depto_C.Text,
                                                   Txt_Fono_C.Text, Txt_Correo_C.Text, fiado1, Txt_NombreU_C.Text, Txt_Contrasena_C.Password,
                                                   Cmb_Tipo_Cliente.SelectedValue.ToString(), Cmb_comuna_C.SelectedValue.ToString(), cmb_id_taller_C.SelectedValue.ToString());
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void btn_Buscar_C_Click(object sender, RoutedEventArgs e)
        {
            Cliente_Negocio clienteN = new Cliente_Negocio();
            DataTable resp = new DataTable();
            Compartido_Negocio comp = new Compartido_Negocio();

            if (Txt_Rut_Cliente.Text != "")
            {
                try
                {
                    resp = clienteN.ListarCliente(Txt_Rut_Cliente.Text);
                    if (resp.Rows.Count > 0)
                    {


                        foreach (DataRow item in resp.Rows)
                        {
                            

                            Txt_id_C.Text = item["ID_CLIENTE"].ToString();
                            Txt_Dv_Cliente.Text = item["DV_CLIENTE"].ToString();
                            Txt_P_Nombre_C.Text = item["p_nombre_cliente"].ToString();
                            Txt_S_Nombre_C.Text = item["s_nombre_cliente"].ToString();
                            Txt_P_Apellido_C.Text = item["p_apellido_cliente"].ToString();
                            Txt_S_Apellido_C.Text = item["s_apellido_cliente"].ToString();
                            Txt_Direccion_C.Text = item["direccion_cliente"].ToString();
                            Txt_numeracion_C.Text = item["numeracion_cliente"].ToString();
                            Txt_Fono_C.Text = item["fono_cliente"].ToString();
                            Txt_Depto_C.Text = item["dept_cliente"].ToString();
                            Txt_Correo_C.Text = item["correo_cliente"].ToString();
                            Txt_NombreU_C.Text = item["nombre_usu_cliente"].ToString();
                            Txt_Contrasena_C.Password = comp.DecrytedString(item["contrasena_cliente"].ToString());
                            Cmb_comuna_C.SelectedValue = item["id_comuna"].ToString();
                            Cmb_Tipo_Cliente.SelectedValue = item["id_tipo_cliente"].ToString();
                            cmb_id_taller_C.SelectedValue = item["id_taller"].ToString();

                            if (item["fiado"].ToString() == "1")
                            {
                                chk_Fiado.IsChecked = true;
                            }
                            else
                            {

                                chk_Fiado.IsChecked = false;
                            }
                            
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rut invalido");
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
        private void btn_Eliminar_C_Click(object sender, RoutedEventArgs e)
        {
            Cliente_Negocio clienteN = new Cliente_Negocio();
            if (Txt_Rut_Cliente.Text != "")
            {
                bool retorno = false;
                string msj = "Esta seguro de eliminar este cliente";
                string titulo = "ELIMINAR CLIENTE";
                var resp = MessageBox.Show(msj, titulo,
                     MessageBoxButton.YesNo);
                if (resp == MessageBoxResult.Yes)
                {
                    retorno = clienteN.EliminarCliente(int.Parse(Txt_id_C.Text));
                    if (retorno)
                    {
                        MessageBox.Show("Cliente Eliminado");
                        limpiarCLiente();
                    }
                    else
                    {
                        MessageBox.Show("El cliente no pudo ser eliminado");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar RUT para eliminar un cliente");
            }
        }

        ////////////////////////////////////////////////////////////
        /////////////////PROVEDOR//////////////////////////////////
        ///////////////////////////////////////////////////////////

        private void btn_agregar_P_Click(object sender, RoutedEventArgs e)
        {
            Proveedor_Negocio provNeg = new Proveedor_Negocio();
            bool resultado = false;
            try
            {
                resultado = provNeg.CrearProveedor(Txt_Razon_S_.Text, Txt_Giro.Text, Txt_Rut_Proveedor.Text, Txt_Dv_Proveedor.Text, Txt_Direccion_P.Text, Txt_numeracion_P.Text,
                                               Txt_Fono_P.Text, Txt_Correo_P.Text, Txt_NombreU_P.Text,
                                               Txt_Contrasena_P.Password, 
                                               Cmb_comuna_P.SelectedValue.ToString());
                

                if (resultado)
                {
                    //WEB//////////////////////////////////////////////////////////////////////////////////
                    string passw = "pbkdf2_sha256$180000$H4OS2un2GD4E$+am++22Sn/Azd0V62FZ3M593/+R0njKTNeLl0PAoh8I=";
                    string username = Txt_Rut_Proveedor.Text + "-" + Txt_Dv_Proveedor.Text;
                    string lastL = DateTime.Now.ToString("dd/MM/yyyy");
                    int superU = 0;
                    int staff = 0;
                    int active = 1;
                    string datej = DateTime.Now.ToString("dd/MM/yyyy");
                    provNeg.CrearProveedorWEB(passw, lastL, string.Format(superU.ToString()), username, Txt_Giro.Text, Txt_Razon_S_.Text, Txt_Correo_P.Text, string.Format(staff.ToString()), string.Format(active.ToString()), datej);
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        private void Btn_Buscar_p_Click(object sender, RoutedEventArgs e)
        {
            Proveedor_Negocio proveedorN = new Proveedor_Negocio();
            DataTable resp = new DataTable();
            try
            {
                resp = proveedorN.ListarProveedor(Txt_Rut_Proveedor.Text);

                foreach (DataRow item in resp.Rows)
                {
                    Txt_id_p.Text = item["ID_PROVEEDOR"].ToString();
                    Txt_Rut_Proveedor.Text = item["RUT_PROV"].ToString();
                    Txt_Dv_Proveedor.Text = item["DV_PROV"].ToString();
                    Txt_Razon_S_.Text = item["RAZON_SOCIAL_PROV"].ToString();
                    Txt_Giro.Text = item["GIRO_PROV"].ToString();
                    Txt_Fono_P.Text = item["FONO_PROV"].ToString();
                    Txt_Correo_P.Text = item["CORREO_PROV"].ToString();
                    Txt_Direccion_P.Text = item["DIRECCION_PROV"].ToString();
                    Txt_numeracion_P.Text = item["NUMERACION_PROV"].ToString();
                    Cmb_comuna_P.SelectedValue = item["ID_COMUNA"].ToString();
                    Txt_NombreU_P.Text = item["NOMBRE_USU_PROV"].ToString();
                    Txt_Contrasena_P.Password = item["CONTRASENA_PROV"].ToString();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void Btn_minimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
