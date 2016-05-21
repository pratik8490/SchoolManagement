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
    public class Enquiry : BasePage
    {
        
        private LoadingIndicator _Loader;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Enquiry"/> class.
        /// </summary>
        public Enquiry()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    EnquiryLayout();
                }
                catch (Exception ex)
                {
                }
            });
        }
        #endregion

        /// <summary>
        /// Enquiry
        /// </summary>
        public void EnquiryLayout()
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
