using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página básica está documentada en http://go.microsoft.com/fwlink/?LinkId=234237

namespace SuperComprobadorPRO
{
    /// <summary>
    /// Página básica que proporciona características comunes a la mayoría de las aplicaciones.
    /// </summary>
    public sealed partial class P2CCC : SuperComprobadorPRO.Common.LayoutAwarePage
    {
        public P2CCC()
        {
            this.InitializeComponent();

            EstableceTecladoNumerico();
        }

        /// <summary>
        /// Rellena la página con el contenido pasado durante la navegación. Cualquier estado guardado se
        /// proporciona también al crear de nuevo una página a partir de una sesión anterior.
        /// </summary>
        /// <param name="navigationParameter">Valor de parámetro pasado a
        /// <see cref="Frame.Navigate(Type, Object)"/> cuando se solicitó inicialmente esta página.
        /// </param>
        /// <param name="pageState">Diccionario del estado mantenido por esta página durante una sesión
        /// anterior. Será null la primera vez que se visite una página.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Mantiene el estado asociado con esta página en caso de que se suspenda la aplicación o
        /// se descarte la página de la memoria caché de navegación. Los valores deben cumplir los requisitos
        /// de serialización de <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">Diccionario vacío para rellenar con un estado serializable.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private async void B_Comprobar_Click(object sender, RoutedEventArgs e)
        {
            bool OK = true;
            if ((T_Banco.Text.Length != 4) || !IsNumeric(T_Banco.Text))
            {
                var a = new Windows.UI.Popups.MessageDialog("El código de banco debe ser un número de 4 cifras.", "Comprueba cuenta bancaria");
                var result = await a.ShowAsync();
                OK = false;
               // return;
            }

            if ((T_Oficina.Text.Length != 4) || !IsNumeric(T_Oficina.Text))
            {
                var a = new Windows.UI.Popups.MessageDialog("El código de oficina debe ser un número de 4 cifras.", "Comprueba cuenta bancaria");
                var result = await a.ShowAsync();
                OK = false;
                // return;
            }

            if ((T_DC.Text.Length != 2) || !IsNumeric(T_DC.Text))
            {
                var a = new Windows.UI.Popups.MessageDialog("El digito de control debe ser un número de 2 cifras.", "Comprueba cuenta bancaria");
                var result = await a.ShowAsync();
                OK = false;
                // return;
            }

            if ((T_CCC.Text.Length != 10) || !IsNumeric(T_CCC.Text))
            {
                var a = new Windows.UI.Popups.MessageDialog("El número de cuenta debe ser un número de 10 cifras.", "Comprueba cuenta bancaria");
                var result = await a.ShowAsync();
                OK = false;
                // return;
            }

            if (OK == true)
            {
                string DC_OK = null;
                DC_OK = CheckCCC.CheckCCC.CheckDC(T_Banco.Text, T_Oficina.Text, T_CCC.Text);

                if (DC_OK == "NO")
                    return;



                if (T_DC.Text == DC_OK)
                {
                    T_Resultado.Text = "La cuenta indicada es correcta";
                    Img_Resultado.Source = ImageFromRelativePath(this, "Assets/Imagenes/Ok.png");
                }
                else
                {
                    T_Resultado.Text = "La cuenta indicada NO es correcta";
                    Img_Resultado.Source = ImageFromRelativePath(this, "Assets/Imagenes/No.png");
                }

                // SV_Intro.Visibility = Visibility.Collapsed;
                CanvasResultado.Visibility = Visibility.Visible;                
            }
            else
            {
                T_Resultado.Text = "No ha rellenado correctamente los campos";
            }

            T_Resultado.Visibility = Visibility.Visible;          

        }

        
        private void EstableceTecladoNumerico()
        {
            // http://create.msdn.com/en-US/education/quickstarts/Working_with_Text_on_the_Windows_Phone
            InputScope IPNum = new InputScope();
            InputScopeName IPNNum = new InputScopeName();
            IPNNum.NameValue = InputScopeNameValue.TelephoneNumber;
            IPNum.Names.Add(IPNNum);
            T_Banco.InputScope = IPNum;
            //T_Oficina.InputScope = IPNum;
            //T_DC.InputScope = IPNum;
            //T_CCC.InputScope = IPNum;
        }

        public static BitmapImage ImageFromRelativePath(FrameworkElement parent, string path)
        {
            var uri = new Uri(parent.BaseUri, path);
            BitmapImage bmp = new BitmapImage();
            bmp.UriSource = uri;
            return bmp;
        }
        
        public static System.Boolean IsNumeric (System.Object Expression)
        {
            if(Expression == null || Expression is DateTime)
                return false;

            if(Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;
   
            try 
            {
                if(Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            } 
            catch {} // just dismiss errors but return false
                return false;
        }

    } 

}

