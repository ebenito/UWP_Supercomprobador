using System.Collections.Generic;
using System.Linq;
using SuperComprobadorPRO.Models;

namespace SuperComprobadorPRO.ViewModels
{
    class MenuInicioViewModel
    {
        #region Privates

        public List<BotonesByType> Items { get; set; }

        #endregion

        
        #region Constructor

        public MenuInicioViewModel()
        {
            var botones = new List<BotonesMenu>
                             {
                                 new BotonesMenu {Title="Comprueba NIF", Image="Assets/Imagenes/BotonNIF.png", Type=BotonType.Boton},
                                 new BotonesMenu {Title="Comprueba CCC", Image="Assets/Imagenes/BotonCCC.png", Type=BotonType.Boton},
                                 new BotonesMenu {Title="Comprueba IBAN", Image="Assets/Imagenes/BotonCCC.png", Type=BotonType.Boton}
                             };

            var botonesByType = botones.GroupBy(f => f.Type).Select(f => new BotonesByType {Type = f.Key, Botones = f.ToList() });

            Items = botonesByType.ToList();
        }

        #endregion
    }
}
