using Microsoft.AspNetCore.Mvc;
using TalCalenderApi1.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TalCalenderApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeSlotsController : ControllerBase
    {
        private static List<TimeSlot> _timeSlots = GenerateTimeSlots();

        private static List<TimeSlot> GenerateTimeSlots()
        {
            // Generate 30-minute time slots from 9AM to 5PM
            var timeSlots = new List<TimeSlot>();
            var startTime = new TimeSpan(9, 0, 0);
            var endTime = new TimeSpan(17, 0, 0);
            var currentTime = startTime;

            int slotId = 1;
            while (currentTime < endTime)
            {
                // Exclude specific booked times
                if (!(currentTime == new TimeSpan(11, 0, 0) ||
                      (currentTime >= new TimeSpan(12, 0, 0) && currentTime < new TimeSpan(13, 0, 0)) ||
                      currentTime == new TimeSpan(15, 30, 0)))
                {
                    timeSlots.Add(new TimeSlot
                    {
                        SlotId = slotId++,
                        StartTime = currentTime,
                        EndTime = currentTime.Add(new TimeSpan(0, 30, 0)),
                        Available = true
                    });
                }
                else
                {
                    timeSlots.Add(new TimeSlot
                    {
                        SlotId = slotId++,
                        StartTime = currentTime,
                        EndTime = currentTime.Add(new TimeSpan(0, 30, 0)),
                        Available = false
                    });
                }

                currentTime = currentTime.Add(new TimeSpan(0, 30, 0));
            }

            return timeSlots;
        }

        [HttpGet("available")]
        public ActionResult<IEnumerable<TimeSlot>> GetAvailableTimeSlots([FromQuery] DateTime date)
        {
            // Filter time slots for a given date and return available slots
            var bookedSlots = new List<TimeSlot>();
            // Assume booked slots are retrieved from database
            var bookedSlotIds = _timeSlots.Where(ts => !ts.Available).Select(ts => ts.SlotId);

            var availableSlots = _timeSlots.Where(ts => ts.Available && !bookedSlotIds.Contains(ts.SlotId)).ToList();
            return Ok(availableSlots);
        }



        [HttpGet("AllSlot")]
        public ActionResult<IEnumerable<TimeSlot>> GetTimeSlots([FromQuery] DateTime date)
        {
            // Filter time slots for a given date and return available slots
            var bookedSlots = new List<TimeSlot>();
            // Assume booked slots are retrieved from database
            var bookedSlotIds = _timeSlots.Where(ts => !ts.Available).Select(ts => ts.SlotId);

            var availableSlots = _timeSlots.Where(ts => !ts.Available && bookedSlotIds.Contains(ts.SlotId)).ToList();


            var allSlots = _timeSlots.ToList();
            return Ok(allSlots);
        }

        [HttpGet("Booked")]
        public ActionResult<IEnumerable<TimeSlot>> GetBookedSlots([FromQuery] DateTime date)
        {
            // Filter time slots for a given date and return available slots
            var bookedSlots = new List<TimeSlot>();
            // Assume booked slots are retrieved from database
            var bookedSlotIds = _timeSlots.Where(ts => !ts.Available).Select(ts => ts.SlotId);

            var availableSlots = _timeSlots.Where(ts => !ts.Available && bookedSlotIds.Contains(ts.SlotId)).ToList();


            return Ok(availableSlots);
        }

        [HttpPost("book")]
        public ActionResult<BookedSlot> BookTimeSlot(BookedSlot bookedSlot)
        {
            // Book the selected time slot
            var selectedSlot = _timeSlots.FirstOrDefault(ts => ts.SlotId == bookedSlot.SlotId);
            if (selectedSlot == null || !selectedSlot.Available)
            {
                return BadRequest("Selected slot is not available.");
            }

            selectedSlot.Available = false;

            // Save booked slot to database or other storage mechanism

            return new BookedSlot
            {
                BookingId = 1, // Generate or retrieve booking ID
                SlotId = selectedSlot.SlotId,
                BookingDate = bookedSlot.BookingDate
            };
        }
    }
}
