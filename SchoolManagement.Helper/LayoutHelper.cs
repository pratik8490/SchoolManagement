using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Helper
{
    public class LayoutHelper
    {
        #region General Layout

        /// <summary>
        /// General color for layout.
        /// </summary>
        public static Color LoginPageBackgroundColor = Color.FromHex("#E4E4E4");
        public static Color PageBackgroundColor = Color.White;
        public static Color TitleColor = Color.FromHex("#E47911");
        public static Color LinkColor = Color.FromHex("#3366AA");
        public static Color LightBlackColor = Color.FromHex("#333");
        public static Color BlueButtonColor = Color.FromHex("#428BCA");
        public static Color RedButtonColor = Color.FromHex("#B52D11");
        public static Color ButtonColor = Color.FromHex("#5554A0");
        public static Color SeperatorColor = Color.FromHex("#D0D1D7");
        public static Color BarBackGroundColor = Color.FromHex("#428BCA");
        public static Color BarBackTextColor = Color.White;
        public static Color RedColor = Color.FromHex("#B12704");
        public static Color WhiteColor = Color.White;
        public static Color ListBackgroundColor = Color.White;
        public static Color OrangeColor = Color.FromHex("#E47911");
        public static double ButtonHeight = 35;
        public static double ButtonFontSmallSize = 12;
        public static Color NavigationButtonColor = Color.FromHex("#5f8ac7");

        public static Color PartLinkColor = Color.FromHex("#428BCA");
        public static Color NoteBackColor = Color.White;
        public static Color NoteTextColor = Color.Gray;

        public static Button NavButton(Button Button)
        {
            Button.Font = Font.SystemFontOfSize(14);
            Button.TextColor = Color.White;
            Button.BackgroundColor = Color.FromHex("5f8ac7");
            Button.BorderRadius = 0;
            Button.HeightRequest = 35;

            return Button;
        }

        public static Thickness SearchBarPadding()
        {
            var padding = new Thickness(0, 0, 0, 0);
            return padding;

        }

        public static Thickness IOSPadding(int left, int top, int right, int bottom)
        {
            var padding = new Thickness(0, 0, 0, 0);
            if (Device.OS == TargetPlatform.iOS)
            {
                padding = new Thickness(left, top, right, bottom);
            }
            return padding;

        }

        public static void SetSearchBarPadding(Page currentPage)
        {
            var padding = new Thickness(0, 0, 0, 0);
            if (Device.OS == TargetPlatform.iOS)
            {
                padding = new Thickness(0, Constants.Padding, 0, 0);
            }
            currentPage.Padding = padding;
        }

        #endregion
    }
}
