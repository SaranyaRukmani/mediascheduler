using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using zomoxo.mediascheduler.Models;
using zomoxo.mediascheduler.Repository;


namespace zomoxo.mediascheduler
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //Read input from user
                //1. From Time yyyy-MM-dd HH:mm:ss (2019-10-01 10:00:00)
                //2. To Time   yyyy-MM-dd HH:mm:ss (2019-10-01 10:30:00)
                var mediaRepository = new MediaRepository();

                //Create request object based on the above user input then use Media.json from Media folder
                //to create media object. Construct the below request using both the input
                var request = new GenerateScheduleRequest();
                Console.WriteLine("Enter From Date");
                request.FromDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Enter To Date");
                request.ToDate = Convert.ToDateTime(Console.ReadLine());
                string medias = File.ReadAllText("Media/Medias.json");
                request.Medias = JsonConvert.DeserializeObject<List<Media>>(medias);
                //Generate schedule
                var schedule = mediaRepository.GenerateSchedule(request);

                //Write schedule as plain json file
                var parsedSchedule = JsonConvert.SerializeObject(schedule);
                var fileName = $"generated-schedule-{request.FromDate.ToString("yyyyMMddHHmmss")}";
                System.IO.File.WriteAllText($@"C:\Users\sarn\Desktop\tmp\{fileName}.json", parsedSchedule);
                Console.WriteLine("Schedule Generated Successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong {e}");
            }

            Console.ReadKey();
        }
    }
}