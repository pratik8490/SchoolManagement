using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SchoolManagement.Helper
{
    public class Constants
    {
        public static Thickness IOSPadding = new Thickness(0, Device.OnPlatform(40, 0, 0), 0, 0);
        public static int Padding = 43;
        public static string ServiceUrl = "http://schoolapi.demoapplication.in/";

        /// <summary>
        /// Image path constant.
        /// </summary>
        public class ImagePath
        {
            public static string SlideOutMenu = "slideout.png";
            public static string CategoryLineMenuIcon = "left_menu.png";
            public static string LeftMenuIcon = "line_menu.png";
            public static string DropDownArrow = "down_arrow.png";
            public static string SearchIcon = "search.png";
            public static string ArrowRight = "arrowright.png";
            public static string ArrowLeft = "arrowleft.png";
            public static string FailIcon = "error.png";
            public static string PassIcon = "success.png";

            public static string ToolbarHomeIcon()
            {
                string homeIcon = "home.png";
                if (Device.OS == TargetPlatform.iOS)
                    homeIcon = "home.png";

                return homeIcon;
            }
        }
    }
}
