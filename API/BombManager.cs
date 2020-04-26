using KTANEAssistant.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTANEAssistant.API
{
    public class BombManager : INotifyPropertyChanged
    {
        public static readonly char[] OddNumbers = new char[] { '1', '3', '5', '7', '9' };
        public static readonly char[] Vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
        private string _SerialNumber;
        private int _Batteries, _Holders, _Strikes;
        private bool _Dvi, _Parallel, _Ps2, _Rj45, _Serial, _Stereorca;
        private Dictionary<Indicators, bool> _indicators;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string SerialNumber
        {
            get
            {
                return _SerialNumber;
            }
            set
            {
                if (value.Length > 6)
                {
                    _SerialNumber = value.Substring(0, 6);
                }
                else
                {
                    _SerialNumber = value;
                }
                OnPropertyChanged(nameof(SerialNumber));
            }
        }
        public int Batteries {
            get
            {
                return _Batteries;
            }
            set
            {
                _Batteries = value >= 0 ? value : _Batteries;
                OnPropertyChanged(nameof(Batteries));
            }
        }
        public int BatteryHolders
        {
            get
            {
                return _Holders;
            }
            set
            {
                _Holders = value >= 0 ? value : _Holders;
                OnPropertyChanged(nameof(BatteryHolders));
            }
        }
        public int Strikes
        {
            get
            {
                return _Strikes;
            }
            set
            {
                _Strikes = value >= 0 && value <= 2 ? value : 0;
                OnPropertyChanged(nameof(Strikes));
            }
        }
        public bool DVIPort
        {
            get
            {
                return _Dvi;
            }
            set
            {
                _Dvi = value;
                OnPropertyChanged(nameof(DVIPort));
            }
        }
        public bool ParallelPort
        {
            get
            {
                return _Parallel;
            }
            set
            {
                _Parallel = value;
                OnPropertyChanged(nameof(ParallelPort));
            }
        }
        public bool PS2Port
        {
            get
            {
                return _Ps2;
            }
            set
            {
                _Ps2 = value;
                OnPropertyChanged(nameof(PS2Port));
            }
        }
        public bool RJ45Port
        {
            get
            {
                return _Rj45;
            }
            set
            {
                _Rj45 = value;
                OnPropertyChanged(nameof(RJ45Port));
            }
        }
        public bool SerialPort
        {
            get
            {
                return _Serial;
            }
            set
            {
                _Serial = value;
                OnPropertyChanged(nameof(SerialPort));
            }
        }
        public bool StereoRCAPort
        {
            get
            {
                return _Stereorca;
            }
            set
            {
                _Stereorca = value;
                OnPropertyChanged(nameof(StereoRCAPort));
            }
        }

        public List<Indicators> GetIndicators()
        {
            return _indicators.Keys.ToList();
        }

        public List<Indicators> GetLitIndicators()
        {
            return _indicators.AsEnumerable().Where(x => x.Value).Select(x => x.Key).ToList();
        }

        public bool IsIndicatorPresent(Indicators indicator)
        {
            return _indicators.ContainsKey(indicator);
        }

        public bool IsIndicatorLit(Indicators indicator)
        {
            _indicators.TryGetValue(indicator, out bool lit);
            return lit;
        }

        public bool IsLastSerialDigitOdd()
        {
            if (SerialNumber == null)
            {
                return false;
            }
            char[] chars = SerialNumber.ToCharArray();
            return OddNumbers.Any(x => x.Equals(chars[chars.Length - 1]));
        }

        public bool SerialContainsVowel()
        {
            if (string.IsNullOrEmpty(SerialNumber))
            {
                return false;
            }
            char[] chars = SerialNumber.ToLower().ToCharArray();
            return Vowels.Any(x => x.Equals(chars[chars.Length - 1]));
        }

        public void SetIndicator(Indicators indicator, int status)
        {
            _indicators.Remove(indicator);
            switch (status)
            {
                case 1:
                    _indicators.Add(indicator, false);
                    break;
                case 2:
                    _indicators.Add(indicator, true);
                    break;
                default:
                    break;
            }
        }

        public void Reset()
        {
            SerialNumber = string.Empty;
            Batteries = 0;
            BatteryHolders = 0;
            Strikes = 0;
            _indicators.Clear();
            DVIPort = false;
            ParallelPort = false;
            PS2Port = false;
            RJ45Port = false;
            SerialPort = false;
            StereoRCAPort = false;
        }
        public BombManager()
        {
            _indicators = new Dictionary<Indicators, bool>();
        }
    }
}
