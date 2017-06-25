using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace connect4
{
    static class CommonProperties
    {
        public enum MyFont { AgencyFB, ElectroShackle }

        static PrivateFontCollection pfc = new PrivateFontCollection();

        public static void Init()
        {
            SetupFont(Properties.Resources.Agency_FB);
            SetupFont(Properties.Resources.Electro_Shackle);
        }

        static void SetupFont(byte[] fontData)
        {
            int fontLength = fontData.Length;
            IntPtr dataPtr = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontData, 0, dataPtr, fontLength);
            pfc.AddMemoryFont(dataPtr, fontLength);
        }

        public static FontFamily GetFontFamily(MyFont myFont)
        {
            return pfc.Families[(int)myFont];
        }

        public static Icon GetIcon()
        {
            return Properties.Resources.icon;
        }

        public static string GetProductVersion()
        {
            return "v1.0.0";
        }
    }
}
