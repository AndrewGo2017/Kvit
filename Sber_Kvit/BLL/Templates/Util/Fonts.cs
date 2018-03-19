using iTextSharp.text;
using iTextSharp.text.pdf;
using Sber_Kvit.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sber_Kvit.BLL.Templates.Util
{
    public static class Fonts
    {
        public static readonly Font Font3;
        public static readonly Font Font8;
        public static readonly Font Font7;
        public static readonly Font Font5;
        public static readonly Font Font8_underlined;
        public static readonly Font Font10;
        public static readonly Font Font10_kerning;
        public static readonly Font Font10_bold;
        public static readonly Font Font10_underlined;
        public static readonly Font Font11;
        public static readonly Font Font11_bold;
        public static readonly Font Font11_bold_underlined;
        public static readonly Font Font12;
        public static readonly Font Font12_bold;
        public static readonly Font Font12_bold_underlined;

        static Fonts()
        {
            int fintModifier = Settings.Default.tb_set_addSet_FontSize - 3; 
            string ttf_Times = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "TIMES.TTF");
            string ttf_Times_Bold = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "TIMESBD.TTF");
            var baseFont_Times = BaseFont.CreateFont(ttf_Times, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var baseFont_Times1 = BaseFont.CreateFont(ttf_Times, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            baseFont_Times1.SetKerning(10, 10, 10);
            BaseFont baseFont_Times_Bold = BaseFont.CreateFont(ttf_Times_Bold, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            Font10 = new Font(baseFont_Times, 10 + fintModifier, Font.NORMAL);
            Font10_bold = new Font(baseFont_Times_Bold, 10 + fintModifier, Font.NORMAL);
            Font10_underlined = new Font(baseFont_Times, 10 + fintModifier, Font.NORMAL | Font.UNDERLINE);
            Font10_kerning = new Font(baseFont_Times1, 10 + fintModifier, Font.NORMAL | Font.UNDERLINE);
            Font11 = new Font(baseFont_Times, 11 + fintModifier, Font.NORMAL);
            Font11_bold = new Font(baseFont_Times_Bold, 11 + fintModifier, Font.NORMAL);
            Font11_bold_underlined = new Font(baseFont_Times_Bold, 11 + fintModifier, Font.NORMAL | Font.UNDERLINE);
            Font12 = new Font(baseFont_Times, 12 + fintModifier, Font.NORMAL);
            Font12_bold = new Font(baseFont_Times_Bold, 12 + fintModifier, Font.NORMAL);
            Font12_bold_underlined = new Font(baseFont_Times_Bold, 12 + fintModifier, Font.NORMAL | Font.UNDERLINE);
            Font3 = new Font(baseFont_Times, 3 + fintModifier, Font.NORMAL);
            Font5 = new Font(baseFont_Times, 5 + fintModifier, Font.NORMAL);
            Font7 = new Font(baseFont_Times, 7 + fintModifier, Font.NORMAL);
            Font8 = new Font(baseFont_Times, 8 + fintModifier, Font.NORMAL);
            Font8_underlined = new Font(baseFont_Times, 8 + fintModifier, Font.NORMAL | Font.UNDERLINE);
        }
    }
}
