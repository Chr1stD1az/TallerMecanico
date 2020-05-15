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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        {/*
            var usuario = txt_usuario.Text;
            //    var contrasenia = txt_contraseña.GetValue;
            var contrasenia = 9;
            OracleComand exec = new OracleComand();
            try
            {
                var parametros = new Dictionary<string, string>();
                DataTable dataTable = new DataTable();
                parametros.Add("USUARIO", usuario);
                parametros.Add("CLAVE", contrasenia.ToString());
                exec.ExecStoredProcedure("SP_CREARCLIENTE", parametros);
            }
            catch (Exception ex)
            {

                throw;
            }
               
            */

            this.Hide();
            MenuPrincipal ventana = new MenuPrincipal();
            ventana.ShowDialog();
        }

    }
}
