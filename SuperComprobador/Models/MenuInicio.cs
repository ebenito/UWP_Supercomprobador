
namespace SuperComprobadorPRO.Models
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
