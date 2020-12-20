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
        private ObservableCollection<W3CRecord> _dummyLogEntries = new();

        public ObservableCollection<W3CRecord> LogEntries 
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

        private ObservableCollection<string> _filenames = new();
        public ObservableCollection<string> Filenames
        {
            get
            {
                return _filenames;
            }
            set
            {
                if(_filenames != value)
                {
                    _filenames = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private string _fileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public string FileDirectory
        {
            get
            {
                return _fileDirectory;
            }
            set
            {
                if(_fileDirectory != value)
                {
                    _fileDirectory = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public MainWindowViewModel()
        {
            LogEntries.Add(
                new W3CRecord()
                {
                    //17:42:15 172.16.255.255 GET /default.htm 200 HTTP/1.0 
                    LogFilename = "File1.log",
                    EventTimeUTC = new DateTime(2020,12,19,17,42,15,0,DateTimeKind.Utc),
                    S_Ip = "172.16.255.255",
                    CS_Method = "GET",
                    CS_URI_Stem = "/default.htm",
                    SC_Status = "200",
                    CS_Version = "HTTP/1.0",
                    S_ComputerName = "PATH-MISDEV",
                    S_Port = "80",
                    S_SiteName = "Birthdays",

                }
                ); ;

            LogEntries.Add(
                new W3CRecord()
                {
                    //17:42:15 172.16.255.255 GET /default.htm 200 HTTP/1.0
                    LogFilename = "File1.log",
                    EventTimeUTC = new DateTime(2020, 12, 19, 17, 42, 16, 0, DateTimeKind.Utc),
                    S_Ip = "172.16.255.255",
                    CS_Method = "GET",
                    CS_URI_Stem = "/images/flower.jpg",
                    SC_Status = "200",
                    CS_Version = "HTTP/1.0",
                    S_ComputerName = "PATH-MISDEV",
                    S_Port = "80",
                    S_SiteName = "Birthdays",
                }
                );
            LogEntries.Add(
                new W3CRecord()
                {
                   //17:42:15 172.16.255.255 GET /default.htm 200 HTTP/1.0
                   LogFilename = "File2.log",
                    EventTimeUTC = new DateTime(2020, 12, 19, 17, 42, 17, 0, DateTimeKind.Utc),
                    S_Ip = "172.16.255.255",
                    CS_Method = "GET",
                    CS_URI_Stem = "/EmployeeDetails",
                    CS_URI_Query = "id=123456",
                    SC_Status = "200",
                    CS_Version = "HTTP/1.0",
                    S_ComputerName = "PATH-MISDEV",
                    S_Port = "80",
                    S_SiteName = "Birthdays",
                    
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
