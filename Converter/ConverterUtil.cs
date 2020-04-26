using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTANEAssistant.Converter
{
    public static class ConverterUtil
    {
        public static bool SafeConvert(object vIn, out double vOut)
        {
            if (vIn is double)
            {
                vOut = (double)vIn;
                return true;
            }
            if (vIn is int)
            {
                vOut = System.Convert.ToDouble((int)vIn);
                return true;
            }
            if (vIn is string)
            {
                try
                {
                    vOut = System.Convert.ToDouble((string)vIn);
                    return true;
                }
                catch
                { }
            }
            vOut = 1;
            return false;
        }
    }
}
