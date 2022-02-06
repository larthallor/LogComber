using System;

namespace W3CLogFileLibrary;

public record W3CRecord
{
    public string? LogFilename { get; init; }
    public DateTime? LoggingDateTime { get; set; }
    public DateTime? EventTimeUTC 
    {
        get
        {
            if (TimeUTC == null)
            {
                return null;
            }

            DateTime? eventDateTime = DateUTC?.ToDateTime(TimeUTC.Value) 
                ?? LoggingDateTime?.Date.AddTicks(TimeUTC.Value.Ticks);
            return eventDateTime;
        }
    }
    public DateTime? EventTimeLocal
    {
        get
        {
            return EventTimeUTC?.ToLocalTime();
        }
    }

    public DateOnly? DateUTC { get; init; }
    public TimeOnly? TimeUTC { get; init; }
    public string? S_SiteName { get; init; }
    public string? S_ComputerName { get; init; }
    public string? S_Ip { get; init; }
    public string? CS_Method { get; init; }
    public string? CS_URI_Stem { get; init; }
    public string? CS_URI_Query { get; init; }
    public string? S_Port { get; init; }
    public string? CS_Username { get; init; }
    public string? C_IP { get; set; }
    public string? CS_Version { get; init; }
    public string? CS_UserAgent { get; init; }
    public string? CS_Cookie { get; init; }
    public string? CS_Referrer { get; init; }
    public string? CS_Host { get; init; }
    public string? SC_Status { get; init; }
    public string? SC_SubStatus { get; init; }
    public string? SC_Win32_Status { get; init; }
    public long? SC_Bytes { get; init; }
    public long? CS_Bytes { get; init; }
    public decimal? TimeTaken { get; init; }
    public string? StreamId { get; init; }
}
