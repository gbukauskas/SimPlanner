using SP.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SP.Dto
{
    [MetadataType(typeof(InstitutionMetadata))]
    public class InstitutionDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Abbreviation { get; set; }
        public string LocaleCode { get; set; }
        public string StandardTimeZone { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public CultureDto Culture { get; set; }
        public virtual ICollection<DepartmentDto> Departments { get; set; }
        public virtual ICollection<ProfessionalRoleInstitutionDto> ProfessionalRoleInstitutions { get; set; }


    }
}
