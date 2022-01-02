using LogComberWPF.Models;
using LogComberWPF.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LogComberWPF.ViewModels;

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
            if (_dummyLogEntries != value)
            {
                _dummyLogEntries = value;
                NotifyPropertyChanged();
            }
        }
    }

    public string MainWindowTitle
    {
        get
        {
            string appTitle = "IIS W3C Log Comber";
            if (string.IsNullOrEmpty(Filename))
            {
                return appTitle;
            }
            return appTitle + " - " + Path.GetFileName(Filename);
        }
    }

    private long? _FileSizeBytes;
    public long? FileSizeBytes
    {
        get
        {
            return _FileSizeBytes;
        }
        set
        {
            if (_FileSizeBytes != value)
            {
                _FileSizeBytes = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(StatusDisplay));
            }
        }
    }

    private long? _FileSizeLines;
    public long? FileSizeLines
    {
        get
        {
            return _FileSizeLines;
        }
        set
        {
            if (_FileSizeLines != value)
            {
                _FileSizeLines = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(StatusDisplay));
            }
        }
    }

    public string StatusDisplay
    {
        get
        {
            if (string.IsNullOrEmpty(Filename))
            {
                return "";
            }
            return $"{FileSizeBytes:N0} bytes in {FileSizeLines:N0} lines.";
        }
    }


    internal async Task NewFileSelectedAsync(string fileName)
    {
        Filename = fileName;

        FileSizeBytes = new FileInfo(fileName).Length;

        var parser = new W3CLogParser();

        LogEntries.Clear();
        FileSizeLines = 0L;
        await Task.Yield();

        await foreach (var record in parser.GetRecordsAsync(fileName))
        {
            FileSizeLines++;
            LogEntries.Add(record);
        }
    }

    private string _filename;
    public string Filename
    {
        get
        {
            return _filename;
        }
        set
        {
            if (_filename != value)
            {
                _filename = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(MainWindowTitle));
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
                EventTimeUTC = new DateTime(2020, 12, 19, 17, 42, 15, 0, DateTimeKind.Utc),
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
