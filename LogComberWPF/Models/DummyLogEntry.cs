using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogComberWPF.Models
{
    public class DummyLogEntry : INotifyPropertyChanged
    {
        private string _fileSource = null;
        public string FileSource
        {
            get
            {
                return _fileSource;
            }
            set
            {
                if (value != _fileSource)
                {
                    _fileSource = value;
                    NotifyPropertyChanged();
                }
            }
        }


        //<DataGridTextColumn Header = "time" />
        private string _time = null;
        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                if(value != _time)
                {
                    _time = value;
                    NotifyPropertyChanged();
                }
            }
        }
        //< DataGridTextColumn Header="c-ip"/>
        private string _cIp;
        public string CIP
        {
            get { return _cIp; }
            set 
            {
                if (_cIp != value)
                {
                    _cIp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //<DataGridTextColumn Header = "cs-method" />
        private string _csMethod;

        public string CsMethod
        {
            get { return _csMethod; }
            set
            {
                if (_csMethod != value)
                {
                    _csMethod = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //< DataGridTextColumn Header="cs-uri-stem"/>
        private string _csUriStem;

        public string CsUriStem
        {
            get { return _csUriStem; }
            set 
            {
                if (_csUriStem != value)
                {
                    _csUriStem = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //<DataGridTextColumn Header = "sc-status" />

        private string _scStatus;
        public string ScStatus
        {
            get { return _scStatus; }
            set
            {
                if (_scStatus != value)
                {
                    _scStatus = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //< DataGridTextColumn Header="cs-version"/>
        private string _csVersion;

        public string CsVersion
        {
            get { return _csVersion; }
            set
            {
                if (_csVersion != value)
                {
                    _csVersion = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
