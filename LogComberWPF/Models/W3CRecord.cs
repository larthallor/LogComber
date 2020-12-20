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

        [Display(Name ="Event Time")]
        public DateTime EventTime { get; init; }

        [Display(Name = "Service Name")]
        public string S_SiteName { get; init; }

        [Display(Name ="Server Name")]
        public string S_ComputerName { get; init; }

        [Display(Name = "Server IP")]
        public string S_Ip { get; init; }

        [Display(Name ="Method")]
        public string CS_Method { get; init; }

        [Display(Name ="URI Stem")]
        public string CS_URI_Stem { get; init; }

        [Display(Name = "URI Query")]
        public string CS_URI_Query { get; init; }

        [Display(Name = "Server Port")]
        public string S_Port { get; init; }

        [Display(Name ="Http Status")]
        public string SC_Status { get; init; }

        [Display(Name = "Protocol Version")]
        public string CS_Version { get; init; }


    }
}
