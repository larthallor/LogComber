using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogComberWPF.Models
{
    public record W3CRecord
    {
        public string LogFilename { get; init; }
        public DateTime? EventTimeUTC { get; init; }
        public DateTime? EventTimeLocal
        {
            get
            {
                return EventTimeUTC?.ToLocalTime();
            }
        }
        public string S_SiteName { get; init; }
        public string S_ComputerName { get; init; }
        public string S_Ip { get; init; }
        public string CS_Method { get; init; }
        public string CS_URI_Stem { get; init; }
        public string CS_URI_Query { get; init; }
        public string S_Port { get; init; }
        public string CS_Username { get; init; }
        public string C_IP { get; set; }
        public string CS_Version { get; init; }
        public string CS_UserAgent { get; init; }
        public string CS_Cookie { get; init; }
        public string CS_Referrer { get; init; }
        public string CS_Host { get; init; }
        public string SC_Status { get; init; }
        public string SC_SubStatus { get; init; }
        public string SC_Win32_Status { get; init; }
        public long SC_Bytes { get; init; }
        public long CS_Bytes { get; init; }
        public int TimeTaken { get; init; }
        public string StreamId { get; init; }
    }
}
