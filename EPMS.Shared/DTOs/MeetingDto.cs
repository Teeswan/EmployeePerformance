using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Shared.DTOs
{
    public class MeetingDto
    {
        public int MeetingId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; }   
        public string Status { get; set; } = string.Empty;
        public bool CanJoin { get; set; }
    }
}
