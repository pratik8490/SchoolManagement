using SchoolManagement.Core.Context;
using SchoolManagement.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Pages
{
    public class MasterPage : MasterDetailPage
    {
        MenuPage menuPage;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Master Page"/> class.
        /// </summary>
        public MasterPage(ContentPage DetailPage)
        {
            //Check user Student/Faculty/Admin
            menuPage = new MenuPage { Icon = Constants.ImagePath.CategoryLineMenuIcon };
            
            this.Master = menuPage;
            this.Detail = DetailPage;
        }

        public MasterPage(Carousel DetailPage)
        {
            menuPage = new MenuPage { Icon = Constants.ImagePath.CategoryLineMenuIcon };

            this.Master = menuPage;
            this.Detail = DetailPage;
        }

        public MasterPage(bool IsTabed, TabbedPage DetailPage)
        {
            //Check user Student/Faculty/Admin
            menuPage = new MenuPage();
            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as SchoolManagement.Core.Models.MenuItem);

            this.Master = new MenuPage { Icon = Constants.ImagePath.CategoryLineMenuIcon };
            this.Detail = DetailPage;
        }
        #endregion

        protected override void OnDisappearing()
        {
            GC.Collect();
        }
        public void NavigateTo(SchoolManagement.Core.Models.MenuItem menu)
        {
            if (menu == null)
                return;

            Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);

            Detail = displayPage;

            menuPage.Menu.SelectedItem = null;
            IsPresented = false;
        }
    }
}
