namespace SP.DataAccess
{
    using Data.Interfaces;
    using SP.Metadata;
    using System;
    using System.ComponentModel.DataAnnotations;
    using Utilities;

    [MetadataType(typeof(CourseDayMetadata))]
    public class CourseDay : ICourseDay
    {
        public Guid CourseId { get; set; }
        public int Day { get; set; }
        public int DurationFacultyMins { get; set; }
        public int DurationParticipantMins { get; set; }
        DateTime _startFacultyUtc;
        public DateTime StartFacultyUtc
        {
            get
            {
                return _startFacultyUtc;
            }
            set
            {
                _startFacultyUtc = value.AsUtc();
            }
        }
        public int DelayStartParticipantMins { get; set; }

        public virtual Course Course {get; set;}
    }
}
