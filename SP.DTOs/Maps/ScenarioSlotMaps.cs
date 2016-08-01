using SP.DataAccess;
using SP.Dto;
using System;
using System.Linq.Expressions;
namespace SP.Dto.Maps
{
    internal static class ScenarioSlotMaps
    {
        internal static Func<CourseSlotDto, CourseSlot> MapToDomain()
        {
            return m => new CourseSlot
            {
                Id = m.Id,
                MinutesDuration = m.MinutesDuration,
                Day = m.Day,
                Order = m.Order
                //CourseTypes = m.CourseTypes
            };
        }

        internal static Expression<Func<CourseSlot, CourseSlotDto>> MapFromDomain()
        {
            return m => new CourseSlotDto
            {
                Id = m.Id,
                MinutesDuration = m.MinutesDuration,
                Day = m.Day,
                Order = m.Order
                //CourseTypes = m.CourseTypes
            };
        }
    }
}
