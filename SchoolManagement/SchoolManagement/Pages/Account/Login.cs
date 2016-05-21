using Acr.UserDialogs;
using SchoolManagement.Core.Context;
using SchoolManagement.Core.Models;
using SchoolManagement.Helper;
using SchoolManagement.Helper.Controls;
using SchoolManagement.Helper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Pages
{
    public class Login : BasePage
    {
        private ExtendedEntry txtUserName, txtPassword;
        private Button btnLogin;
        private LoadingIndicator _loadingIndicator;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Login Page"/> class.
        /// </summary>
        public Login()
        {
            try
            {
                LoginLayout();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        #endregion

        #region Overrride methods
        protected override void OnAppearing()
        {
            try
            {
                bool isNetworkAvailable = DependencyService.Get<INetworkOperation>().IsInternetConnectionAvailable();

                if (!isNetworkAvailable)
                {
                    DisplayAlert("Connection", "Please check your internet connection.", Messages.Ok);
                }
            }
            catch (Exception ex)
            {
            }

        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (SchoolManagementContext.IsLoggedIn)
                {
                    base.OnBackButtonPressed();
                }
            });
            return true;
        }
        #endregion

        #region LoginLayout
        /// <summary>
        /// Login Page Layout.
        /// </summary>
        public void LoginLayout()
        {
            txtPassword = new ExtendedEntry();
            txtPassword.IsPassword = true;
            
            txtUserName = new ExtendedEntry();
            txtUserName.Text = string.Empty;//"manan@gmail.com";
            txtUserName.BackgroundColor = Color.Gray;
            txtUserName.TextColor = Color.Black;
            txtUserName.Placeholder = "Username";

            txtPassword.Text = string.Empty;//"manan";
            txtPassword.BackgroundColor = Color.Gray;
            txtPassword.TextColor = Color.Black;
            txtPassword.Placeholder = "Password";

            var imageLogo = new Image { Source = "school" };
            var cvTxtUserName = new ContentView
            {
                Padding = new Thickness(10, 10, 10, 5),
                Content = txtUserName
            };
            var cvTxtPassword = new ContentView
            {
                Padding = new Thickness(10, 5, 10, 10),
                Content = txtPassword
            };

            btnLogin = new Button();
            btnLogin.Text = "Login";
            btnLogin.TextColor = Color.White;
            btnLogin.BackgroundColor = LayoutHelper.ButtonColor;

            btnLogin.Clicked += (sender, e) =>
            {

                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    DisplayAlert(string.Empty, "Username is required.", "Ok");
                }

                else if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    DisplayAlert(string.Empty, "Password is required.", "Ok");
                }

                else if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            var loader = UserDialogs.Instance.Loading(string.Empty, null, null, true);
                            //_loadingIndicator.IsShowLoading = true;
                            btnLogin.IsVisible = false;

                            //Login call
                            UserToken tokenRespnseModel = await LoginModel.LogIn(txtUserName.Text, txtPassword.Text);

                            //Add reponse to context
                            SchoolManagementContext.TokenResponseModel = tokenRespnseModel;

                            if (tokenRespnseModel.Token != null)
                            {
                                SchoolManagementContext.UserName = txtUserName.Text;
                                SchoolManagementContext.IsLoggedIn = true;

                                //redirect to page
                                Navigation.PushModalAsync(App.MainDashboardPage());
                                //if (SchoolManagementContext.TokenResponseModel.UserType == Convert.ToInt32(UserType.Principal))
                                //{
                                //    Navigation.PushModalAsync(App.ViewAttendance());
                                //}
                                //else if (SchoolManagementContext.TokenResponseModel.UserType == Convert.ToInt32(UserType.Teacher))
                                //{
                                //    //Navigation.PushModalAsync(App.FillUpAttendance());
                                //    Navigation.PushModalAsync(App.MainDashboardPage());
                                //}
                                //else if (SchoolManagementContext.TokenResponseModel.UserType == Convert.ToInt32(UserType.Parents))
                                //{
                                //   Navigation.PushModalAsync(App.ViewAttendance());
                                //}
                            }
                            else
                            {
                                await DisplayAlert(string.Empty, Messages.Login.InvalidUserNameOrPassword, Messages.Ok);
                            }
                            loader.Hide();
                        }
                        catch (System.NullReferenceException ex)
                        {
                            throw ex;
                        }
                        catch (Exception ex)
                        {
                            btnLogin.IsVisible = true;
                            _loadingIndicator.IsShowLoading = false;
                        }

                        btnLogin.IsVisible = true;
                        //_loadingIndicator.IsShowLoading = false;
                    });
                }
            };

            var cvBtnLogin = new ContentView
            {
                Padding = new Thickness(10, 5, 10, 10),
                Content = btnLogin
            };

            Label lblForgot = new Label
            {
                Text = "Forgot Password?",
                TextColor = LayoutHelper.LinkColor,
                BackgroundColor = Color.Transparent,
                FontSize = 16,
                HeightRequest = 36,
                HorizontalOptions = LayoutOptions.End,
            };

            ContentView cvLblForgetPassword = new ContentView
            {
                Padding = new Thickness(8, 5, 8, 0),
                Content = lblForgot
            };


            Label lblCopyrights = new Label
            {
                Text = "Copyright © 2015. All rights reserved.",
                TextColor = Color.White,
                BackgroundColor = Color.Black,
                FontSize = 14,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };

            var barLower = new StackLayout
            {
                BackgroundColor = Color.Black,
                Spacing = 0,
                Padding = new Thickness(0, 0, 0, 0),
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Fill,
                Children =
                {
                    lblCopyrights,
                },

            };

            _loadingIndicator = new LoadingIndicator();

            StackLayout loginLayout = new StackLayout
            {
                Children = {
                    imageLogo,
                    cvTxtUserName,
                    cvTxtPassword ,
                    cvBtnLogin,
                    cvLblForgetPassword,
                    _loadingIndicator,
                    barLower,
                    
                },
                BackgroundColor = LayoutHelper.LoginPageBackgroundColor,
            };

            loginLayout.Padding = LayoutHelper.IOSPadding(0, 20, 0, 0);

            ScrollView scrollCartNewLayout = new ScrollView
            {
                Content = loginLayout
            };
            Content = scrollCartNewLayout;
        }
        #endregion
    }
}
