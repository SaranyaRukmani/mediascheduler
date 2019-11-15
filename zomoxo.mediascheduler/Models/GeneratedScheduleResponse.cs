using System.Collections.Generic;

namespace zomoxo.mediascheduler.Models
{
    /// <summary>
    /// Schedule Resource
    /// </summary>
    public class GeneratedScheduleResponse
    {
        public double TotalRequestedDuration { get; set; }
        public double MediaDuration { get; set; }
        public double RemainingDuration { get; set; }
        public IList<ScheduledMedia> ScheduledMedia { get; set; }
    }
}