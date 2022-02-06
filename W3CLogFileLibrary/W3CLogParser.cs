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
        while (!sr.EndOfStream)
        {
            lineBuffer = await sr.ReadLineAsync();
            if (lineBuffer == null) {}
            else if (lineBuffer.StartsWith("#Version:")) {}
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
                W3CRecord record = new() { LoggingDateTime = defaultDate, LogFilename = filename };
                
                var fields = lineBuffer.Split(' ');
                for (int i = 0; i < fields.Length; i++)
                {
                    record = SetData(fields[i], fieldNames[i], record);
                }
                yield return record;
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

    private static W3CRecord SetData(string rawField, string? rawFieldName, W3CRecord record)
    {
        if(rawField == "-")
        {
            return record;
        }

        return rawFieldName switch
        {
            "time" => record with
            {

                //EventTimeUTC = defaultDateTime.Add(TimeSpan.Parse(rawField))
                TimeUTC = TimeOnly.Parse(rawField)
            },
            "date" => record with {DateUTC = DateOnly.Parse(rawField)},
            "time-taken" => record with { TimeTaken = Decimal.Parse(rawField)},
            "sc-bytes" => record with { SC_Bytes = Int32.Parse(rawField)},
            "cs-bytes" => record with { CS_Bytes = Int32.Parse(rawField)},
            "s-sitename" => record with { S_SiteName = rawField},
            "s-computername" => record with { S_ComputerName = rawField},
            "s-ip" => record with { S_Ip = rawField},
            "cs-method" => record with { CS_Method = rawField },
            "cs-uri-stem" => record with { CS_URI_Stem = rawField },
            "cs-uri-query" => record with { CS_URI_Query = rawField },
            "cs-uri" =>  record with {
                CS_URI_Stem = rawField.Split('?', 2)[0],
                CS_URI_Query = rawField.Split('?', 2).Length == 2 ? rawField.Split('?', 2)[1] : null,
                },
            "s-port" => record with { S_Port = rawField },
            "cs-username" => record with { CS_Username = rawField },
            "c-ip" => record with { C_IP = rawField },
            "cs-version" => record with { CS_Version = rawField },
            "cs(User-Agent)" => record with { CS_UserAgent = rawField },
            "cs(Cookie)" => record with { CS_Cookie = rawField },
            "cs(Referer)" => record with { CS_Referrer = rawField },
            "cs-host" => record with { CS_Host = rawField },
            "sc-status" => record with { SC_Status = rawField },
            "sc-substatus" => record with { SC_SubStatus = rawField },
            "sc-win32-status" => record with { SC_Win32_Status = rawField},
            "streamid" => record with { StreamId = rawField },
            _ => record,
        };
    }
}
