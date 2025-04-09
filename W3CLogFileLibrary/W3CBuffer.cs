namespace W3CLogFileLibrary;

public class W3CBuffer
{
    public string? LogFilename { get; set; }
    public DateTime? LoggingDateTime { get; set; }
    public DateOnly? DateUTC { get; set; }
    public TimeOnly? TimeUTC { get; set; }
    public string? S_SiteName { get; set; }
    public string? S_ComputerName { get; set; }
    public string? S_Ip { get; set; }
    public string? CS_Method { get; set; }
    public string? CS_URI_Stem { get; set; }
    public string? CS_URI_Query { get; set; }
    public string? S_Port { get; set; }
    public string? CS_Username { get; set; }
    public string? C_IP { get; set; }
    public string? CS_Version { get; set; }
    public string? CS_UserAgent { get; set; }
    public string? CS_Cookie { get; set; }
    public string? CS_Referrer { get; set; }
    public string? CS_Host { get; set; }
    public string? SC_Status { get; set; }
    public string? SC_SubStatus { get; set; }
    public string? SC_Win32_Status { get; set; }
    public long? SC_Bytes { get; set; }
    public long? CS_Bytes { get; set; }
    public decimal? TimeTaken { get; set; }
    public string? StreamId { get; set; }
    public void Clear()
    {
        LogFilename = null;
        LoggingDateTime = null;
        DateUTC = null;
        TimeUTC = null;
        S_SiteName = null;
        S_ComputerName = null;
        S_Ip = null;
        CS_Method = null;
        CS_URI_Stem = null;
        CS_URI_Query = null;
        S_Port = null;
        CS_Username = null;
        C_IP = null;
        CS_Version = null;
        CS_UserAgent = null;
        CS_Cookie = null;
        CS_Referrer = null;
        CS_Host = null;
        SC_Status = null;
        SC_SubStatus = null;
        SC_Win32_Status = null;
        SC_Bytes = null;
        CS_Bytes = null;
        TimeTaken = null;
        StreamId = null;
    }
    public W3CRecord ToRecord => new ()
    {
        LogFilename = this.LogFilename,
        LoggingDateTime = this.LoggingDateTime,
        DateUTC = this.DateUTC,
        TimeUTC = this.TimeUTC,
        S_SiteName = this.S_SiteName,
        S_ComputerName = this.S_ComputerName,
        S_Ip = this.S_Ip,
        CS_Method = this.CS_Method,
        CS_URI_Stem = this.CS_URI_Stem,
        CS_URI_Query = this.CS_URI_Query,
        S_Port = this.S_Port,
        CS_Username = this.CS_Username,
        C_IP = this.C_IP,
        CS_Version = this.CS_Version,
        CS_UserAgent = CS_UserAgent,
        CS_Cookie = CS_Cookie,
        CS_Referrer = CS_Referrer,
        CS_Host = CS_Host,
        SC_Status = SC_Status,
        SC_SubStatus = SC_SubStatus,
        SC_Win32_Status = SC_Win32_Status,
        SC_Bytes = SC_Bytes,
        CS_Bytes = CS_Bytes,
        TimeTaken = TimeTaken,
        StreamId = StreamId,
    };
}
