using LogComberWPF.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace LogComberWPF.Services;

public class W3CLogParser
{
    public async IAsyncEnumerable<W3CRecord> GetRecordsAsync(string filename)
    {
        var defaultDate = DateTime.UtcNow.Date;

        string[] fieldNames = null;

        using var sr = File.OpenText(filename);
        string lineBuffer = null;
        while (!sr.EndOfStream)
        {
            lineBuffer = await sr.ReadLineAsync();
            if (lineBuffer.StartsWith("#Version:"))
            {
                await Task.Yield();
                continue;
            }

            if (lineBuffer.StartsWith("#Date:"))
            {
                defaultDate = ParseDateDirective(lineBuffer);
                await Task.Yield();
                continue;
            }

            if (lineBuffer.StartsWith("#Fields:"))
            {
                fieldNames = ParseFieldsDirective(lineBuffer);


                await Task.Yield();
                continue;
            }

            if (lineBuffer.StartsWith("#"))
            {
                await Task.Yield();
                continue;
            }

            // if we get here, this is data, not a directive.

            W3CRecord record = new();
            var fields = lineBuffer.Split(' ');
            for (int i = 0; i < fields.Length; i++)
            {
                record = SetData(fields[i], fieldNames[i], record, defaultDate);
            }
            yield return record;
        }
        sr.Close();
    }

    private string[] ParseFieldsDirective(string lineBuffer)
    {
        var tokens = lineBuffer.Split(' ');
        return tokens[1..];
    }

    private DateTime ParseDateDirective(string lineBuffer)
    {
        var tokens = lineBuffer.Split(' ');
        return DateTime.Parse(tokens[1]);
    }

    private W3CRecord SetData(string rawField, string rawFieldName, W3CRecord record, DateTime defaultDateTime)
    {
        return rawFieldName switch
        {
            "time" => record with
            {
                EventTimeUTC = defaultDateTime.Add(TimeSpan.Parse(rawField))
            },
            "cs-method" => record with { CS_Method = rawField },
            "cs-uri" => record with { CS_URI_Stem = rawField },
            _ => record,
        };
    }
}
