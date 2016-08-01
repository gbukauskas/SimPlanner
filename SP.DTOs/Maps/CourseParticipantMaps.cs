using SP.DataAccess;
using SP.Dto;
using System;
using System.Linq.Expressions;
namespace SP.Dto.Maps
{
    public static class CourseParticipantMaps
    {
        public static Func<CourseParticipantDto, CourseParticipant> MapToDomain()
        {
            return m => new CourseParticipant
            {
                ParticipantId = m.ParticipantId,
                CourseId = m.CourseId,
                IsConfirmed = m.IsConfirmed,
                IsFaculty = m.IsFaculty,
                IsOrganiser = m.IsOrganiser,
                DepartmentId = m.DepartmentId,
                ProfessionalRoleId = m.ProfessionalRoleId
                //Participant = m.Participant,
                //Course = m.Course
            };
        }

        public static Expression<Func<CourseParticipant, CourseParticipantDto>> MapFromDomain()
        {
            return m => new CourseParticipantDto
            {
                ParticipantId = m.ParticipantId,
                CourseId = m.CourseId,
                IsConfirmed = m.IsConfirmed,
                IsFaculty = m.IsFaculty,
                IsOrganiser = m.IsOrganiser,
                DepartmentId = m.DepartmentId,
                ProfessionalRoleId = m.ProfessionalRoleId
                //Participant = m.Participant,
                //Course = m.Course
            };
        }
    }
}
