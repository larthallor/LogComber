﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using W3CLogFileLibrary;

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

    private long _FileSizeLines;
    public long FileSizeLines
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


    internal async Task LoadFileAsync(string fileName)
    {
        if(Filename != fileName)
        {
            LogEntries.Clear();
            FileSizeLines = 0L;
        }
        Filename = fileName;

        FileSizeBytes = new FileInfo(fileName).Length;

        //LogEntries.Clear();
        //FileSizeLines = 0L;
        await Task.Yield();

        List<W3CRecord> currentRecords = new();

        await foreach (var record in W3CLogParser.GetRecordsAsync(fileName))
        {
            
            currentRecords.Add(record);
            if(!LogEntries.Contains(record))
            {
                FileSizeLines++;
                LogEntries.Add(record);
            }
        }

        List<W3CRecord> recordsToDelete = new();

        // Can't delete in this loop because it would modify
        //   the thing we are enumerating. Store items found
        //   for deletion after we finish iterating LogEntries.
        foreach(var entry in LogEntries)
        {
            if(!currentRecords.Contains(entry))
            {
                recordsToDelete.Add(entry);
            }
        }

        // Now we can update LogEntries
        foreach(var recordToDelete in recordsToDelete)
        {
            FileSizeLines--;
            LogEntries.Remove(recordToDelete);
            await Task.Yield();
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

    private void AddTestData()
    {
        LogEntries.Add(
            new W3CRecord()
            {
                //17:42:15 172.16.255.255 GET /default.htm 200 HTTP/1.0 
                LogFilename = "File1.log",
                DateUTC = new DateOnly(2020, 12, 19),
                TimeUTC = new TimeOnly(17, 42, 15, 0),
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
                DateUTC = new DateOnly(2020, 12, 19),
                TimeUTC = new TimeOnly(17, 42, 16, 0),
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
                DateUTC = new DateOnly(2020, 12, 19),
                TimeUTC = new TimeOnly(17, 42, 17, 0),
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

    public MainWindowViewModel()
    {
        //AddTestData();
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
