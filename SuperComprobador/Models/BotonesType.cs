using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperComprobadorPRO.Models
{
    public class BotonesByType
    {
        public BotonType Type { get; set; }
        public List<BotonesMenu> Botones { get; set; }
    }
}
