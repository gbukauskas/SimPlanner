using SP.DataAccess;
using SP.Dto;
using System;
using System.Linq.Expressions;
namespace SP.Dto.Maps
{
    public static class DepartmentMaps
    {
        public static Func<DepartmentDto, Department> mapToRepo()
        {
            return m => new Department
            {
                Id = m.Id,
                Name = m.Name,
                InstitutionId = m.InstitutionId,
                InvitationLetterFilename = m.InvitationLetterFilename,
                CertificateFilename = m.CertificateFilename,
                Abbreviation = m.Abbreviation,
                Colour = m.Colour
            };
        }


        public static Expression<Func<Department, DepartmentDto>> mapFromRepo()
        {
            return m => new DepartmentDto
            {
                Id = m.Id,
                Name = m.Name,
                InstitutionId = m.InstitutionId,
                InvitationLetterFilename = m.InvitationLetterFilename,
                CertificateFilename = m.CertificateFilename,
                Abbreviation = m.Abbreviation,
                Colour = m.Colour 
                //CourseTypes = null,
                //Institution = m.Institution,
                //Manequins = m.Manequins,
                //Courses = m.Courses,
                //Scenarios = m.Scenarios,
                //Departments = null
            };
        }
    }
}
