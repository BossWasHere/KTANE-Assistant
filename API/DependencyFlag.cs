using System.ComponentModel;
using System.Linq;

namespace KTANEAssistant.API
{
    public enum DependencyFlag
    {
        [Description("Serial Number")]
        SerialNumber,
        [Description("Batteries")]
        Batteries,
        [Description("Battery Holders")]
        BatteryHolders,
        [Description("Strikes")]
        Strikes,
        [Description("Ports")]
        GenericPort,
        [Description("DVI Port")]
        DVIPort,
        [Description("Parallel Port")]
        ParallelPort,
        [Description("PS/2 Port")]
        PS2Port,
        [Description("RJ-45 Port")]
        RJ45Port,
        [Description("Serial Port")]
        SerialPort,
        [Description("Stereo RCA Port")]
        StereoRCAPort,
        [Description("Indicators")]
        GenericIndicator,
        [Description("Indicator SND")]
        IndicatorSND,
        [Description("Indicator CLR")]
        IndicatorCLR,
        [Description("Indicator CAR")]
        IndicatorCAR,
        [Description("Indicator IND")]
        IndicatorIND,
        [Description("Indicator FRQ")]
        IndicatorFRQ,
        [Description("Indicator SIG")]
        IndicatorSIG,
        [Description("Indicator NSA")]
        IndicatorNSA,
        [Description("Indicator MSA")]
        IndicatorMSA,
        [Description("Indicator TRN")]
        IndicatorTRN,
        [Description("Indicator BOB")]
        IndicatorBOB,
        [Description("Indicator FRK")]
        IndicatorFRK
    }

    public static class DependencyExtensions
    {
        public static string ToExtendedString(this DependencyFlag[] flags)
        {
            if (flags == null) return string.Empty;
            return string.Join(", ", flags.Select(x => GetDescription(x)));
        }

        public static string GetDescription(this DependencyFlag flag)
        {
            DescriptionAttribute[] attribs = typeof(DependencyFlag).GetField(flag.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            if (attribs.Length > 0) {
                return attribs[0].Description;
            }
            else
            {
                return flag.ToString();
            }
        }
    }
}