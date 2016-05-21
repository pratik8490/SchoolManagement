using SchoolManagement.Core.Models;
using SchoolManagement.Helper;
using SchoolManagement.Helper.Controls;
using SchoolManagement.Pages.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Pages.Principal
{
    public class Dashboard : BasePage
    {

        private LoadingIndicator _Loader;

        public class Menu
        {
            public string Name { get; set; }
        }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Dashboard"/> class.
        /// </summary>
        public Dashboard()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    DashboardLayout();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
        #endregion

        /// <summary>
        /// Dashboard
        /// </summary>
        public void DashboardLayout()
        {
            try
            {

                Grid grid = new Grid { };

                List<SchoolManagement.Core.Models.MenuItem> lstPage = new MenuListData();

                WrapLayout layout = new WrapLayout
                {
                    Spacing = 5,
                    Padding = new Thickness(5, Device.OnPlatform(20, 0, 0), 5, 0),
                    //HorizontalOptions = LayoutOptions.Center,
                    //VerticalOptions = LayoutOptions.Center,
                };


                //for (int i = 0; i < lstPage.Count; i++)
                //{
                //    var cell = new StackLayout
                //    {
                //        //WidthRequest = 50,
                //        //HeightRequest = 50,
                //        Children = {
                //        new Image {Source = lstPage[i].imagePath, 
                //            VerticalOptions = LayoutOptions.Start,
                //            //BackgroundColor = Color.Blue,
                //            WidthRequest=30,
                //            HeightRequest=30},
                //        new Label {Text = lstPage[i].item, 
                //            //FontSize = 9,
                //            LineBreakMode = LineBreakMode.TailTruncation,

                //        }
                //    }
                //    };

                //    layout.Children.Add(cell);
                //}

                //for (int i = 0; i < lstPage.Count; i++)
                //{

                //    if (i == 0)
                //    {
                //        grid.Children.Add(new Label
                //        {
                //            Text = lstPage[i].MobileName,
                //            TextColor = Color.Blue,
                //            BackgroundColor = Color.Yellow,
                //            XAlign = TextAlignment.Center,
                //            YAlign = TextAlignment.Center,
                //        }, 0, 0);
                //    }

                //    else if (i % 2 == 0)
                //    {
                //        grid.Children.Add(new Label
                //        {
                //            Text = lstPage[i].MobileName,
                //            TextColor = Color.Blue,
                //            BackgroundColor = Color.Yellow,
                //            XAlign = TextAlignment.Center,
                //            YAlign = TextAlignment.Center,
                //        }, i, 1);
                //    }
                //    else
                //    {
                //        grid.Children.Add(new Label
                //        {
                //            Text = lstPage[i].MobileName,
                //            TextColor = Color.Blue,
                //            BackgroundColor = Color.Yellow,
                //            XAlign = TextAlignment.Center,
                //            YAlign = TextAlignment.Center,
                //        }, i, 0);
                //    }
                //}

                List<Menu> data = new List<Menu>();
                for (int i = 1; i <= 10; i++)
                {
                    data.Add(new Menu() { Name = string.Format("data 1 {0}", i) });
                }

                int noOfCol = 5;
                StackLayout slLAble = new StackLayout();
                int totaLoop = (int)Math.Ceiling((double)data.Count / noOfCol);
                for (int i = 0; i < totaLoop; i++)
                {
                    string d1 = string.Empty;
                    for (int j = 0; j < noOfCol; j++)
                    {
                        int index = (i * noOfCol) + j;
                        if (data.Count <= index)
                            break;
                        d1 += data[index].Name + ",";

                        grid.Children.Add(new Label { Text = i + " " + j, TextColor = Color.White }, i, j);
                    }
                }


                ////grid.Children.Add(new Label
                ////{
                ////    Text = "Span 1",
                ////    TextColor = Color.Blue,
                ////    BackgroundColor = Color.Yellow,
                ////    XAlign = TextAlignment.Center,
                ////    YAlign = TextAlignment.Center,
                ////}, 0, 0);


                ////grid.Children.Add(new Label
                ////{
                ////    Text = "Span 1",
                ////    TextColor = Color.Silver,
                ////    BackgroundColor = Color.Yellow,
                ////    XAlign = TextAlignment.Center,
                ////    YAlign = TextAlignment.Center,
                ////}, 0, 1);

                ////grid.Children.Add(new Label
                ////{
                ////    Text = "Span 2",
                ////    TextColor = Color.Blue,
                ////    BackgroundColor = Color.Yellow,
                ////    XAlign = TextAlignment.Center,
                ////    YAlign = TextAlignment.Center,
                ////}, 1, 0);


                ////grid.Children.Add(new Label
                ////{
                ////    Text = "Span 2",
                ////    TextColor = Color.Silver,
                ////    BackgroundColor = Color.Yellow,
                ////    XAlign = TextAlignment.Center,
                ////    YAlign = TextAlignment.Center,
                ////}, 1, 1);

                ////grid.Children.Add(new Label
                ////{
                ////    Text = "Span 3",
                ////    TextColor = Color.Blue,
                ////    BackgroundColor = Color.Yellow,
                ////    XAlign = TextAlignment.Center,
                ////    YAlign = TextAlignment.Center,
                ////}, 2, 0);


                ////grid.Children.Add(new Label
                ////{
                ////    Text = "Span 3",
                ////    TextColor = Color.Silver,
                ////    BackgroundColor = Color.Yellow,
                ////    XAlign = TextAlignment.Center,
                ////    YAlign = TextAlignment.Center,
                ////}, 2, 1);

                //grid.Children.Add(new Label
                //{
                //    Text = "Span 2 columns",
                //    TextColor = Color.Blue,
                //    BackgroundColor = Color.Yellow,
                //    XAlign = TextAlignment.Center,
                //    YAlign = TextAlignment.Center,
                //}, 0, 2);


                //grid.Children.Add(new Label
                //{
                //    Text = "Span 2 columns",
                //    TextColor = Color.Silver,
                //    BackgroundColor = Color.Yellow,
                //    XAlign = TextAlignment.Center,
                //    YAlign = TextAlignment.Center,
                //    //HorizontalTextAlignment = TextAlignment.Center,
                //    //VerticalTextAlignment = TextAlignment.Center
                //}, 1, 2);


                //grid.Children.Add(new Label
                //{
                //    Text = "Span 3 columns",
                //    TextColor = Color.Blue,
                //    BackgroundColor = Color.Yellow,
                //    XAlign = TextAlignment.Center,
                //    YAlign = TextAlignment.Center,
                //}, 0, 3);


                //grid.Children.Add(new Label
                //{
                //    Text = "Span 3 columns",
                //    TextColor = Color.Silver,
                //    BackgroundColor = Color.Yellow,
                //    XAlign = TextAlignment.Center,
                //    YAlign = TextAlignment.Center,
                //}, 1, 3);

                // Accomodate iPhone status bar.
                this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

                // Build the page.
                Content = new ScrollView
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Content = grid
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
