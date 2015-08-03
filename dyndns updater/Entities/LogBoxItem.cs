using System;
using System.Drawing;

namespace DynDNS_Updater.Entities
{
    public class LogBoxItem
    {
        public LogBoxItem(Color c, string m)
        {
            ItemColor = c;
            Message = m.TrimEnd(Environment.NewLine.ToCharArray()); ;
            Timestamp = DateTime.Now;

            Logic.Helper.WriteLogFile(Timestamp.ToString("dd.MM.yyyy - hh:mm:ss - ") + Message);
        }
        public Color ItemColor { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
