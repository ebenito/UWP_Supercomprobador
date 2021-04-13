using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using IbanLibraryPCL;

// La plantilla de elemento Página básica está documentada en http://go.microsoft.com/fwlink/?LinkId=234237

namespace SuperComprobadorPRO
{
    /// <summary>
    /// Página básica que proporciona características comunes a la mayoría de las aplicaciones.
    /// </summary>
    public sealed partial class P3Iban : SuperComprobadorPRO.Common.LayoutAwarePage
    {
        public P3Iban()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Rellena la página con el contenido pasado durante la navegación.  Cualquier estado guardado se
        /// proporciona también al crear de nuevo una página a partir de una sesión anterior.
        /// </summary>
        /// <param name="navigationParameter">Valor de parámetro pasado a
        /// <see cref="Frame.Navigate(Type, Object)"/> cuando se solicitó inicialmente esta página.
        /// </param>
        /// <param name="pageState">Diccionario del estado mantenido por esta página durante una sesión
        /// anterior.  Será null la primera vez que se visite una página.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Mantiene el estado asociado con esta página en caso de que se suspenda la aplicación o
        /// se descarte la página de la memoria caché de navegación.  Los valores deben cumplir los requisitos
        /// de serialización de <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">Diccionario vacío para rellenar con un estado serializable.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void B_CheckIban_Click(object sender, RoutedEventArgs e)
        {
            var x = IbanLibraryPCL.Iban.CheckIban(T_IBAN.Text, true);

            if (x.IsValid)
            {
                T_Resultado.Text = "OK: " + x.Message;
            }
            else
            {
                T_Resultado.Text = "NO: " + x.Message;
            }
        }
    }
}
