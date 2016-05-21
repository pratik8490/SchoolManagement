using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Helper.Controls
{
    public class ButtonView : ContentView
    {
        public ButtonView(Button button, string buttonText, Color buttonColor, double width = 0, double height = 0, double fontSize = 0)
        {
            button.Text = buttonText;
            button.TextColor = Color.White;
            button.BackgroundColor = buttonColor;

            Padding = new Thickness(8, 0, 8, 0);

            Content = button;
            if (width != 0)
            {
                button.WidthRequest = width;
                button.HorizontalOptions = LayoutOptions.Start;
            }
            if (fontSize != 0)
                button.Font = Font.SystemFontOfSize(fontSize);
            if (height != 0)
                button.HeightRequest = height;
        }
    }
}
