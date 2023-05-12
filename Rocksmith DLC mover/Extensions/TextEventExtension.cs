using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocksmith_DLC_mover.Extensions
{
    public class TextEventExtension
    {
        public TextEventExtension(string message, Color color)
        {
            MessageColor = color;
            Message = message;
        }
        public Color MessageColor {  get; set; }
        public string Message { get; set; }
    }
}
