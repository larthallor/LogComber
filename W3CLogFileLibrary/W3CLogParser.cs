using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace W3CLogFileLibrary;

public class W3CLogParser
{
    public static async IAsyncEnumerable<W3CRecord> GetRecordsAsync(string filename)
    {
        var defaultDate = DateTime.UtcNow.Date;

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
                W3CRecord record = new();
                var fields = lineBuffer.Split(' ');
                for (int i = 0; i < fields.Length; i++)
                {
                    record = SetData(fields[i], fieldNames[i], record, defaultDate);
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

    private static DateTime ParseDateDirective(string lineBuffer)
    {
        var tokens = lineBuffer.Split(' ');
        return DateTime.Parse(tokens[1]);
    }

    private static W3CRecord SetData(string rawField, string? rawFieldName, W3CRecord record, DateTime defaultDateTime)
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
