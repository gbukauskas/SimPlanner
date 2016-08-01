using SP.DataAccess;
using SP.Dto;
using System;
using System.Linq;
using System.Linq.Expressions;
namespace SP.Dto.Maps
{
    internal static class RoomMaps
    {
        internal static Func<RoomDto, Room> MapToDomain()
        {
            return m => new Room
            {
                Id = m.Id,
                DepartmentId = m.DepartmentId,
                ShortDescription = m.ShortDescription,
                FullDescription = m.FullDescription,
                Directions = m.Directions
            };
        }
        /*
        internal static Expression<Func<Course, CourseDto>> MapFromDomain(string[] includes) {
            if (["Scenarios", "CourseType", "CourseParticipants", "ScenarioFacultyRoles"])
            {

            } */
        internal static Expression<Func<Room, RoomDto>> MapFromDomain()
        {
            return m => new RoomDto
            {
                Id = m.Id,
                DepartmentId = m.DepartmentId,
                ShortDescription = m.ShortDescription,
                FullDescription = m.FullDescription,
                Directions = m.Directions
            };
            //Courses
            //Department
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
