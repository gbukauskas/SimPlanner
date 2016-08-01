using SP.DataAccess;
using System;
using System.Linq;namespace SP.Dto.Maps
{
    internal class CourseDayMaps: DomainDtoMap<CourseDay, CourseDayDto>
    {
        public CourseDayMaps() : base(
        m => new CourseDay
        {
            CourseId = m.CourseId,
            Day = m.Day,
            Duration =m.Duration,
            StartUtc = m.Start
        },
        m => new CourseDayDto
        {
            CourseId = m.CourseId,
            Day = m.Day,
            Duration = m.Duration,
            Start = m.StartUtc
        })
        { }

    static void IsAllowed(string[] includes,params string[] allowed)
    {
        var disallowed = includes.Except(includes);
        if (disallowed.Any())
        {
            throw new ArgumentException(
                string.Format("the include parameter(s){0} are not allowed: allowed parameters include ({1})",
                string.Join(",", disallowed), string.Join(",", allowed)));
        }
    }

        /*
        internal static Expression<Func<Course, CourseDto>> mapBriefFromRepo = m => new CourseDto
        {
            Id = m.Id,
            StartTime = m.StartTime,
            DepartmentId = m.DepartmentId,
            OutreachingDepartmentId = m.OutreachingDepartmentId,
            FacultyNoRequired = m.FacultyNoRequired,
            CourseTypeId = m.CourseTypeId,

            CourseParticipants = m.CourseParticipants.Select(cp=>new CourseParticipantDto
            {
                ParticipantId = cp.ParticipantId,
                CourseId = cp.CourseId,
                IsConfirmed = cp.IsConfirmed,
                IsFaculty = cp.IsFaculty,
                DepartmentId = cp.DepartmentId,
                ProfessionalRoleId = cp.ProfessionalRoleId
            }).ToList()

            //Department = m.Department,

            //OutreachingDepartment = m.OutreachingDepartment,

            //CourseType = m.CourseType,


            //ScenarioFacultyRoles = m.ScenarioFacultyRoles
        };
        */

    }
}
