using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rocksmith_DLC_mover.Data_Helpers
{
    internal class ControlHelper
    {
        public Button MakeButton(int left, int right, int height, string name, string text)
        {
            Button newButton = new Button();
            newButton.Width = right - left;
            newButton.Height = height;
            newButton.Name = name;
            newButton.Text = text;
            newButton.Visible = true;
            newButton.Enabled = true;
            

            return newButton;
        }


        public Point MakePoint(Point referencePoint, int offset, bool isWidth)
        {
            Point newPoint = new Point();
            newPoint.X = referencePoint.X;
            newPoint.Y = referencePoint.Y;
            if (isWidth)
            {
                newPoint.X += offset; 
            }
            else
            {
                newPoint.Y += offset;
            }
            return newPoint;
        }
    }
}
