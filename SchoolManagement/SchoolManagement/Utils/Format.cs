using SchoolManagement.Helper.Controls;
using SchoolManagement.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Utils
{
    public enum sz { Micro, Small, Normal, Large, Big, Vast }

    public class Format
    {
        static int DefW = 750;

        //   static int DefH = 1334;
        public static string TimeFormatDMY(DateTime t)
        {
            return string.Format("{00}.{01}.{2}",
                t.Day,
                t.Month,
                t.Year);
        }

        public static string TimeFormat(DateTime t)
        {
            return string.Format("{0}:{01} {2} {3}.{4}.{5}",
                t.Hour > 12 ? t.Hour - 12 : t.Hour,
                t.Minute,
                t.Hour < 12 ? "Am" : "Pm",
                t.Month,
                t.Day,
                t.Year - 2000);
        }

        //public static bool BufferToFile(string _FileName, byte[] _ByteArray)
        //{
        //    try
        //    {
        //        System.IO.FileStream _FileStream =
        //           new System.IO.FileStream(_FileName, System.IO.FileMode.Create,
        //                                    System.IO.FileAccess.Write);
        //        _FileStream.Write(_ByteArray, 0, _ByteArray.Length);
        //        _FileStream.Close();
        //        return true;
        //    }
        //    catch (Exception _Exception)
        //    {
        //        //Console.WriteLine("Exception caught in process: {0}",
        //        //                  _Exception.ToString());
        //    }
        //    return false;
        //}

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                input.Dispose();
                return ms.ToArray();
            }
        }

        //public static IDisplay GetDisplay()
        //{
        //    return Resolver.Resolve<IDevice>().Display;
        //}

        public static double PixelWidth { get; set; }

        public static double PixelHeight { get; set; }

        public static double MobileWidth { get; set; }

        public static double MobileHeight { get; set; }

        public static double Val(double n)
        {
            return MobileWidth * n / DefW;
        }

        public static int iVal(double n)
        {
            return (int)(PixelWidth * n / DefW);
        }

        public static double GetFontSize(sz _sz)
        {
            double normalSize = Val(28);
            double fontSize = normalSize;

            switch (_sz)
            {
                case sz.Micro:
                    fontSize = normalSize * 0.4;
                    break;
                case sz.Small:
                    fontSize = normalSize * 0.7;
                    break;
                case sz.Normal:
                    fontSize = normalSize;
                    break;
                case sz.Large:
                    fontSize = normalSize * 1.5;
                    break;
                case sz.Big:
                    fontSize = normalSize * 2.0;
                    break;
                case sz.Vast:
                    fontSize = normalSize * 2.5;
                    break;
            }
            return fontSize;
        }

        public static Style ButtonStyle(sz _sz)
        {
            double fsize = GetFontSize(_sz);
            var buttonStyle = new Style(typeof(Button))
            {
                Setters = {
					new Setter { Property = Button.FontSizeProperty, Value = fsize },
					new Setter { Property = Button.BackgroundColorProperty, Value = Color.Transparent },
				}
            };
            return buttonStyle;
        }

        public static Style LabelStyle(sz _sz)
        {
            double fsize = GetFontSize(_sz);
            var labelStyle = new Style(typeof(Label))
            {
                Setters = {
					new Setter { Property = Label.FontProperty, Value = Font.OfSize("Open 24 Display St", fsize) },
				}
            };

            return labelStyle;
        }

        public static Style EntryStyle(sz _sz)
        {
            double fsize = GetFontSize(_sz);
            var entryStyle = new Style(typeof(ExtendedEntry))
            {
                Setters = {
					new Setter { Property = ExtendedEntry.FontProperty, Value = Font.OfSize("Open 24 Display St", fsize) },
					new Setter { Property = ExtendedEntry.PlaceholderTextColorProperty, Value = Global.PlaceHolderColor },
                    new Setter { Property = ExtendedEntry.BackgroundColorProperty, Value = Color.FromRgb(250,250,250) },
				}
            };
            return entryStyle;
        }

        //public static Style EditorStyle(sz _sz)
        //{
        //    double fsize = GetFontSize(_sz);
        //    var editorStyle = new Style(typeof(ExtendedEditor))
        //    {
        //        Setters = {
        //            new Setter { Property = ExtendedEditor.FontProperty, Value = Font.OfSize("Open 24 Display St", fsize) },
        //            //new Setter { Property = ExtendedEditor.BackgroundColorProperty, Value = Color.FromRgb(232,235,235) },
        //        }
        //    };
        //    return editorStyle;
        //}

        public static Style CheckBoxStyle(sz _sz)
        {
            double fsize = GetFontSize(_sz);
            var checkStyle = new Style(typeof(CheckBox))
            {
                Setters = {
					new Setter { Property = CheckBox.FontSizeProperty, Value = fsize },
				}
            };
            return checkStyle;
        }

        public static Style ImageStyle(sz _sz)
        {
            double fsize = GetFontSize(_sz);
            var imageStyle = new Style(typeof(Image))
            {
                Setters = {
					new Setter { Property = Image.AspectProperty, Value = Aspect.AspectFit },
                    new Setter { Property = Image.WidthRequestProperty, Value = fsize * 2 },
                    new Setter { Property = Image.HeightRequestProperty, Value = fsize * 2 },
				}
            };
            return imageStyle;
        }

        public static string GetAgoTime(DateTime registe)
        {
            TimeSpan span = DateTime.Now.Subtract(registe);
            int days = span.Days;
            int hours = span.Hours;
            int minutes = span.Minutes;
            string days_str = days != 0 ? days + " day " : "";
            string hours_str = hours != 0 ? hours + " hour " : "";
            string minutes_str = minutes != 0 ? minutes + " min " : "";
            string agoTime = "";
            if (days + hours + minutes == 0)
            {
                agoTime = "just";
            }
            else
            {
                agoTime = days_str + hours_str + minutes_str;
            }
            if (!string.IsNullOrEmpty(days_str)) return days_str.TrimEnd();
            if (!string.IsNullOrEmpty(hours_str)) return hours_str.TrimEnd();
            if (!string.IsNullOrEmpty(minutes_str)) return minutes_str.TrimEnd();
            return agoTime.TrimEnd();
        }
    }
}
