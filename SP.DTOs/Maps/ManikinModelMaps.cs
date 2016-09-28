using SP.DataAccess;

{
    internal class ManikinModelMaps: DomainDtoMap<ManikinModel, ManikinModelDto>
    {
        public ManikinModelMaps() : base(m => new ManikinModel
            {
                Id = m.Id,
                Description = m.Description,
                ManufacturerId = m.ManufacturerId
            },
            m => new ManikinModelDto
            {
                Id = m.Id,
                Description = m.Description,
                ManufacturerId = m.ManufacturerId
            })
        { }
    }
}