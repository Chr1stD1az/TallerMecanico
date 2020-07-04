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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Taller_Negocio;

namespace Taller_Escritorio_wpf
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BotonCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        
        private void BTN_iniciar_Click(object sender, RoutedEventArgs e)
        {
            Empleado_Negocio empN = new Empleado_Negocio();
            Compartido_Negocio compartido = new Compartido_Negocio();

            var item = empN.LoginUsuario(txt_usuario.Text, compartido.Encriptar(txt_contraseña.Password.ToString()));
            //regEnt = reg.LoginUsuario(usu, passw);

            if (item.Rows.Count > 0)
            {
                foreach (DataRow resp in item.Rows)
                {
                    Application.Current.Properties["NombreUsuario"] = resp["p_nombre_empleado"].ToString() + " " + resp["p_apellido_empleado"].ToString();
                }

                this.Hide();
                MenuPrincipal ventana = new MenuPrincipal();
                ventana.ShowDialog();
            }
            else
            {
                txt_contraseña.Password = "";
                txt_usuario.Text = "";
                MessageBox.Show("Usuario o Contraseña invalida");

            }

        }

        private void Btn_minimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
