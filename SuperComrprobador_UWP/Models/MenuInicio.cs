using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperComrprobador_UWP.Models
{
    public enum BotonType
    {
        Boton
    }

    public class BotonesMenu
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public BotonType Type { get; set; }
    }
}
