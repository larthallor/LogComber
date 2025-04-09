using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace W3CLogFileLibrary;

public class W3CLogParser
{
    public static async IAsyncEnumerable<W3CRecord> GetRecordsAsync(string filename)
    {
        DateTime? defaultDate = DateTime.UtcNow.Date;

        string?[]? fieldNames = null;

        using var sr = File.OpenText(filename);
        string? lineBuffer = null;
        W3CBuffer buffer = new();
        while (!sr.EndOfStream)
        {
            lineBuffer = await sr.ReadLineAsync();
            if (lineBuffer == null) { }
            else if (lineBuffer.StartsWith("#Version:")) { }
            else if (lineBuffer.StartsWith("#Date:"))
            {
                defaultDate = ParseDateDirective(lineBuffer);
            }
            else if (lineBuffer.StartsWith("#Fields:"))
            {
                fieldNames = ParseFieldsDirective(lineBuffer);
            }
            else if (lineBuffer.StartsWith("#")) { }
            else
            {
                // if we get here, this is data, not a directive.
                if (fieldNames == null) { continue; } // If we don't have a fieldNames directive yet.

                buffer.Clear();
                buffer.LoggingDateTime = defaultDate;
                buffer.LogFilename = filename;
                var fields = lineBuffer.Split(' ');
                for (int i = 0; i < fields.Length; i++)
                {
                    SetBufferField(fields[i], fieldNames[i], buffer);
                }
                yield return buffer.ToRecord;
            }
        }
        sr.Close();
    }

    private static string[] ParseFieldsDirective(string lineBuffer)
    {
        var tokens = lineBuffer.Split(' ');
        return tokens[1..];
    }

    private static DateTime? ParseDateDirective(string lineBuffer)
    {
        var tokens = lineBuffer.Split(' ',2);
        if(tokens == null || tokens.Length <= 1 || tokens[1] == null)
        {
            return null;
        }
        string content = tokens[1].Split("//",2)[0].Trim(); // get rid of comments
        return DateTime.Parse(content);
    }
    private static void SetBufferField(string rawField, string? rawFieldName, W3CBuffer buffer)
    {
        if (rawField == "-")
        {
            return;
        }

        switch(rawFieldName)
        {
            case "time": buffer.TimeUTC = TimeOnly.Parse(rawField); break;
            case "date": buffer.DateUTC = DateOnly.Parse(rawField); break;
            case "time-taken": buffer.TimeTaken = Decimal.Parse(rawField); break;
            case "sc-bytes": buffer.SC_Bytes = Int32.Parse(rawField); break;
            case "cs-bytes" :  buffer.CS_Bytes = Int32.Parse(rawField) ; break;
            case "s-sitename" :  buffer.S_SiteName = rawField ; break;
            case "s-computername" :  buffer.S_ComputerName = rawField ; break;
            case "s-ip" :  buffer.S_Ip = rawField ; break;
            case "cs-method" :  buffer.CS_Method = rawField ; break;
            case "cs-uri-stem" :  buffer.CS_URI_Stem = rawField ; break;
            case "cs-uri-query" :  buffer.CS_URI_Query = rawField ; break;
            case "cs-uri" :
                buffer.CS_URI_Stem = rawField.Split('?', 2)[0];
                buffer.CS_URI_Query = rawField.Split('?', 2).Length == 2 ? rawField.Split('?', 2)[1] : null;
                break;
            case "s-port" :  buffer.S_Port = rawField ; break;
            case "cs-username" :  buffer.CS_Username = rawField ; break;
            case "c-ip" :  buffer.C_IP = rawField ; break;
            case "cs-version" :  buffer.CS_Version = rawField ; break;
            case "cs(User-Agent)" :  buffer.CS_UserAgent = rawField ; break;
            case "cs(Cookie)" :  buffer.CS_Cookie = rawField ; break;
            case "cs(Referer)" :  buffer.CS_Referrer = rawField ; break;
            case "cs-host" :  buffer.CS_Host = rawField ; break;
            case "sc-status" :  buffer.SC_Status = rawField ; break;
            case "sc-substatus" :  buffer.SC_SubStatus = rawField ; break;
            case "sc-win32-status" :  buffer.SC_Win32_Status = rawField ; break;
            case "streamid" :  buffer.StreamId = rawField ; break;
        };
    }
}
