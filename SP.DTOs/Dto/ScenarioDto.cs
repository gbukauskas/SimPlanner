using SP.DataAccess;
using SP.DataAccess.Enums;
using SP.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SP.Dto
{
    [MetadataType(typeof(ScenarioMetadata))]
    public class ScenarioDto
	{
        public Guid Id { get; set; }
        public string BriefDescription { get; set; }
        public string FullDescription { get; set; }
        public Guid DepartmentId { get; set; }
        public Difficulty? Complexity { get; set; }
        public Emersion? EmersionCategory { get; set; }
        public SharingLevel Access { get; set; }
        public Guid CourseTypeId { get; set; }

        public virtual CourseTypeDto CourseType { get; set; }
        public virtual DepartmentDto Department { get; set; }

        public virtual ICollection<CourseDto> Courses { get; set; }
        public virtual ICollection<ScenarioResourceDto> ScenarioResources { get; set; }
        public virtual ICollection<CourseSlotScenarioDto> CourseSlotScenarios { get; set; }
    }
}
