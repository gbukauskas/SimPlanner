using SP.Metadata;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SP.Dto
{
    [MetadataType(typeof(CultureMetadata))]
    public class CultureDto
	{
        public string LocaleCode { get; set; }
        public string Name { get; set; }
        public int CountryCode { get; set; }
        public string DialCode { get; set; }
        public virtual ICollection<InstitutionDto> Institutions { get; set; }
    }
}
