using SchoolManagement.Core.Models;
using SchoolManagement.Helper.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Pages.Principal
{
    public class ViewTeacherLogbook : BasePage
    {
        private LoadingIndicator _Loader;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewTeacherLogbook"/> class.
        /// </summary>
        public ViewTeacherLogbook()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    ViewTeacherLogbookLayout();
                }
                catch (Exception ex)
                {
                }
            });
        }
        #endregion

        /// <summary>
        /// View Teacher Logbook
        /// </summary>
        public void ViewTeacherLogbookLayout()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
