using LogComberWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogComberWPF.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DummyLogEntry> _dummyLogEntries = new();

        public ObservableCollection<DummyLogEntry> DummyLogEntries 
        { 
            get
            {
                return _dummyLogEntries;
            }
            set
            {
                if(_dummyLogEntries != value)
                {
                    _dummyLogEntries = value;
                    NotifyPropertyChanged();
                }
            }
        } 

        public MainWindowViewModel()
        {
            DummyLogEntries.Add(
                new DummyLogEntry()
                {
                    //17:42:15 172.16.255.255 GET /default.htm 200 HTTP/1.0 
                    FileSource = @"C:\Users\abennett\Downloads\U12346.log",
                    Time = "17:42:15",
                    CIP = "172.16.255.255",
                    CsMethod = "GET",
                    CsUriStem = "/default.htm",
                    ScStatus = "200",
                    CsVersion = "HTTP/1.0"
                }
                ) ;

            DummyLogEntries.Add(
                new DummyLogEntry()
                {
                    //17:42:15 172.16.255.255 GET /default.htm 200 HTTP/1.0
                    FileSource = @"C:\Users\abennett\Downloads\U12346.log",
                    Time = "17:42:16",
                    CIP = "172.16.255.255",
                    CsMethod = "GET",
                    CsUriStem = "/images/flower.jpg",
                    ScStatus = "200",
                    CsVersion = "HTTP/1.0"
                }
                );

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
