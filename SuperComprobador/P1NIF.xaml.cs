using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
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
    public sealed partial class P1NIF : SuperComprobadorPRO.Common.LayoutAwarePage
    {
        public P1NIF()
        {
            this.InitializeComponent();

            EstableceTecladoNumerico();
            EstableceTecladoAlfaNumerico();
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

        public string MidFun(string s, decimal a, int b)
        {
            try
            {
                var x = Convert.ToInt32(a);

                string temp = s.Substring((x - 1), b);
                return temp;
            }
            catch
            {
                return "0";
            }
        }

        public bool IsNumeric(object Expression)
        {
            // Variable to collect the Return value of the TryParse method.
            bool isNum = false;
            // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum = 0;
            // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
            // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
            isNum = double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }


        public string CalcularLetraDNI(string DNI)
        {
            string LetraDNI = null;
            long nDni = long.Parse(DNI);

            LetraDNI = "TRWAGMYFPDXBNJZSQVHLCKE";
            return MidFun(LetraDNI, (nDni % 23) + 1, 1);
        }


        private void EstableceTecladoNumerico()
        {
            // http://create.msdn.com/en-US/education/quickstarts/Working_with_Text_on_the_Windows_Phone
            InputScope IPNum = new InputScope();
            InputScopeName IPNNum = new InputScopeName();
            IPNNum.NameValue = InputScopeNameValue.TelephoneNumber;
            IPNum.Names.Add(IPNNum);
            T_DNI.InputScope = IPNum;
        }

        private void EstableceTecladoAlfaNumerico()
        {
            InputScope IPNum = new InputScope();
            InputScopeName IPNNum = new InputScopeName();
            IPNNum.NameValue = InputScopeNameValue.AlphanumericFullWidth;
            IPNum.Names.Add(IPNNum);
            T_Letra.InputScope = IPNum;
        }

        private void T_DNI_LostFocus(object sender, RoutedEventArgs e)
        {
            if (T_DNI.Text.Length < 8)
            {
                while (!(T_DNI.Text.Length == 8))
                {
                    T_DNI.Text = "0" + T_DNI.Text;
                }
            }

        }

        private async void T_DNI_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (T_DNI.Text.Length > 8)
            {
                var a = new Windows.UI.Popups.MessageDialog("Debe de indicar un número de DNI (8 digitos, sin letra) para poder calcular la letra del NIF.", "Check NIF");
                var result = await a.ShowAsync();

                T_DNI.Text = MidFun(T_DNI.Text, 1, 8);
                return;
            }
        }

        private async void T_DNI_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (!e.Key.ToString().Contains("Number") && (e.Key != VirtualKey.Tab))
            {
                var a = new Windows.UI.Popups.MessageDialog("Aqui solo puede introducir las cifras númericas del NIF", "Check NIF");
                var result = await a.ShowAsync();
            }
        }

        private async void T_Letra_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                B_Comprobar_Click(this, null);
            }
            else if (e.Key.ToString().Contains("Number"))
            {
                var a = new Windows.UI.Popups.MessageDialog("Aqui solo puede introducir la letra del NIF", "Check NIF");
                var result = await a.ShowAsync();
                T_Letra.Text = string.Empty;
                T_Letra.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            }
        }

        private void T_Letra_TextChanged(object sender, TextChangedEventArgs e)
        {
            T_Letra.Text = T_Letra.Text.ToUpper();
        }


        private async void B_Comprobar_Click(object sender, RoutedEventArgs e)
        {
            if (T_DNI.Text.Length < 8)
            {
                var a = new Windows.UI.Popups.MessageDialog("Debe de indicar un número de DNI (8 digitos, sin letra) para poder calcular la letra del NIF.", "Check NIF");
                var result = await a.ShowAsync();
                return;
            }

            if (T_Letra.Text.Length != 1)
            {
                var a = new Windows.UI.Popups.MessageDialog("Debe de indicar una sola letra para poder calcular la letra del NIF.", "Check NIF");
                var result = await a.ShowAsync();
                return;
            }

            string Letra = CalcularLetraDNI(T_DNI.Text);

            if (Letra == T_Letra.Text.ToUpper())
            {
                T_Resultado.Text = "El NIF indicado es correcto";
                Img_Resultado.Source = ImageFromRelativePath(this, "Assets/Imagenes/Ok.png");
            }
            else
            {
                T_Resultado.Text = "El NIF indicado NO es correcto";
                Img_Resultado.Source = ImageFromRelativePath(this, "Assets/Imagenes/No.png");
            }

            SV_Intro.Visibility = Visibility.Collapsed;
            CanvasResultado.Visibility = Visibility.Visible;
        }

        public static BitmapImage ImageFromRelativePath(FrameworkElement parent, string path)
        {
            var uri = new Uri(parent.BaseUri, path);
            BitmapImage bmp = new BitmapImage();
            bmp.UriSource = uri;
            return bmp;
        }



    }
}
