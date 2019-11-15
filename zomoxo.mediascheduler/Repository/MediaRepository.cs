using zomoxo.mediascheduler.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace zomoxo.mediascheduler.Repository
{
    public class MediaRepository : IMediaRepository
    {
        /// <inheritdoc />
        public GeneratedScheduleResponse GenerateSchedule(GenerateScheduleRequest request)
        {
            DateTime current = request.FromDate;
            DateTime endVideo = request.FromDate;
            GeneratedScheduleResponse response = new GeneratedScheduleResponse();
            response.ScheduledMedia = new List<ScheduledMedia>();
            TimeSpan totalrequestedDuration = request.ToDate - request.FromDate;
            double totalreqTime = totalrequestedDuration.TotalSeconds;
            response.TotalRequestedDuration = totalreqTime;
            foreach (Media media in request.Medias.OrderBy(i => i.Priority))
            {
                for (int i = 0; i < media.NumberOfTimesToPlay; i++)
                {
                    endVideo = current.AddSeconds(media.Duration);
                    if (endVideo > request.ToDate)
                        break;
                    ScheduledMedia schedule = new ScheduledMedia();
                    schedule.Duration = media.Duration;
                    schedule.MediaName = media.MediaName;
                    schedule.ScheduledTime = current;
                    response.ScheduledMedia.Add(schedule);
                    response.MediaDuration += media.Duration;
                    current = endVideo;
                }

                if (endVideo > request.ToDate)
                    break;
            }
            response.RemainingDuration = response.TotalRequestedDuration - response.MediaDuration;
            return response;
        }
    }
}