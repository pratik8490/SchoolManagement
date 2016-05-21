using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Helper.Controls
{
    public class ExtendedToolbarItem : ToolbarItem
    {
        public ExtendedToolbarItem(string text, string icon, ToolbarItemOrder itemOrder, Action doCommand)
        {
            Text = text;
            Icon = icon;
            Order = itemOrder;
            Command = new Command(doCommand);
        }
    }
}
