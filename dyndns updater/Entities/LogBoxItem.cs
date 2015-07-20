using System.Drawing;

namespace DynDNS_Updater.Entities
{
    public class LogBoxItem
    {
        public LogBoxItem(Color c, string m)
        {
            ItemColor = c;
            Message = m;
        }
        public Color ItemColor { get; set; }
        public string Message { get; set; }
    }
}
