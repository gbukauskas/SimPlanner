﻿using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using LinqKit;
using SP.DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;

namespace SP.DTOs.ProcessBreezeRequests
{
    internal sealed class ValidateMedSim : IDisposable
    {
        private readonly IPrincipal _user;
        private MedSimDbContext _context;

        public ValidateMedSim(IPrincipal user)
        {
            _user = user;
        }

        private MedSimDbContext Context
        {
            get
            {
                return _context ?? (_context = new MedSimDbContext());
            }
        }

        public Dictionary<Type, List<EntityInfo>> Process(Dictionary<Type, List<EntityInfo>> saveMap)
        {
            IEnumerable<EFEntityError> errors = new EFEntityError[0];

            List<EntityInfo> currentInfos;
            if (saveMap.TryGetValue(typeof(CourseFormat), out currentInfos))
            {
                errors = errors.Concat(GetCourseFormatErrors(currentInfos));
            }

            if (saveMap.TryGetValue(typeof(Institution), out currentInfos))
            {
                errors = errors.Concat(GetInstitutionErrors(currentInfos));
            }

            if (errors.Any())
            {
                throw new EntityErrorsException(errors);
            }
            return saveMap;
        }

        IEnumerable<EFEntityError> GetCourseFormatErrors(List<EntityInfo> currentInfos)
        {
            var cfs = TypedEntityinfo<CourseFormat>.GetTyped(currentInfos);

            //multiple individual queries may be the way to go here
            var pred = cfs.Aggregate(PredicateBuilder.New<CourseFormat>(), (prev, cur) => prev.Or(
                c => cur.Entity.Id == c.Id && 
                    c.CourseTypeId != cur.Entity.CourseTypeId));
            if (Context.CourseFormats.Any(pred.Compile()))
            {
                throw new InvalidDataException();
            }

            var ids = cfs.Select(cf => cf.Entity.Id).ToList();
            var courseTypeIds = cfs.Select(cf => cf.Entity.CourseTypeId);

            var newFormatsForType = (from c in Context.CourseFormats
                                     where courseTypeIds.Contains(c.CourseTypeId) && !ids.Contains(c.Id)
                                     select new { c.Id, c.Description }).ToList();

            newFormatsForType.AddRange(cfs.Select(c => new { c.Entity.Id, c.Entity.Description }));

            return (from c in newFormatsForType
                    group c by c.Description into cg
                    where cg.Count() > 1
                    select cg).SelectMany(i => i)
                    .Where(i => ids.Contains(i.Id))
                    .Select(i => new EFEntityError(cfs.First(ci => ci.Entity.Id == i.Id).Info,
                        "RepeatWithinGroup",
                        string.Format("Each course format description must be unique within course type. [{0}]", i.Description),
                        "Description"));
        }

        IEnumerable<EFEntityError> GetParticipantErrors(List<EntityInfo> currentInfos)
        {
            var ps = TypedEntityinfo<Participant>.GetTyped(currentInfos);

            /* too dificult, and there are exceptions - had been trying to keep drs as drs etc
            var pred = PredicateBuilder.False<ProfessionalRole>();
            foreach (var p in ps)
            {
                object pr;
                if (p.Info.OriginalValuesMap.TryGetValue("ProfessionalRoleId", out pr) && !p.Entity.DefaultProfessionalRoleId.Equals(pr))
                {
                    Context.ProfessionalRoles.;
                }
            }
            */
            List<EFEntityError> returnVar = new List<EFEntityError>();
            foreach (var p in ps)
            {
                var dup = (from u in Context.Users
                           where p.Entity.Id != u.Id &&
                               p.Entity.FullName == u.FullName &&
                               p.Entity.DefaultDepartmentId == u.DefaultDepartmentId
                           select u.DefaultProfessionalRoleId).FirstOrDefault();
                if (dup != default(Guid) 
                    && ((dup == p.Entity.DefaultProfessionalRoleId 
                        || (from r in Context.ProfessionalRoles
                            where (new[] { dup, p.Entity.DefaultProfessionalRoleId}).Contains(r.Id)
                            group r by r.Description into c
                            select c).Count() == 1)))
                { 
                    returnVar.Add(new EFEntityError(p.Info,
                        "DuplicateUser",
                        "2 users with the same name, department and profession",
                        "FullName"));
                }

            }
            return returnVar;

        }

        IEnumerable<EFEntityError> GetInstitutionErrors(List<EntityInfo> currentInfos)
        {
            var insts = TypedEntityinfo<Institution>.GetTyped(currentInfos);
            List<EFEntityError> returnVar = new List<EFEntityError>();

            foreach (var i in insts)
            {
                try
                {
                    var ci = CultureInfo.GetCultureInfo(i.Entity.LocaleCode);
                    //not great separation of concerns here- this is not a buisness logic problem
                    if (i.Info.EntityState == EntityState.Added && !Context.Cultures.Any(c => c.LocaleCode == ci.Name))
                    {
                        CreateCulture(ci);
                    }
                }
                catch (CultureNotFoundException)
                {
                    returnVar.Add(new EFEntityError(i.Info,
                        "UnknownLocale",
                        "The Locale Code specified is not valid",
                        "LocaleCode"));
                }
                if (!string.IsNullOrWhiteSpace(i.Entity.StandardTimeZone))
                {
                    try
                    {
                        TimeZoneInfo.FindSystemTimeZoneById(i.Entity.StandardTimeZone);
                    }
                    catch (TimeZoneNotFoundException)
                    {
                        returnVar.Add(new EFEntityError(i.Info,
                            "UnknownTimeZone",
                            "The Time Zone specified is not valid",
                            "StandardTimeZone"));
                    }
                }
            }
            return returnVar;
        }
        //not great separation of concerns here- this is not a buisness logic problem 
        /*
        IEnumerable<EFEntityError> GetCourseSlotErrors(List<EntityInfo> currentInfos)
        {
            var insts = TypedEntityinfo<CourseSlot>.GetTyped(currentInfos);

            foreach (var i in insts)
            {
                object wasActive;
                if (!i.Entity.IsActive && i.Info.OriginalValuesMap.TryGetValue("IsActive", out wasActive) 
                    && (bool)wasActive)
                {
                    //could check all collections, but probably easiest to have a crack and see how we go
                    try
                    {
                        //need to do this as Participant of update
                    }
                }
            }
        }
        */

        private void CreateCulture(CultureInfo ci)
        {
            var ri = new RegionInfo(ci.LCID);
            var iso = ISO3166.FromAlpha2(ri.TwoLetterISORegionName);
            var c = new Culture
            {
                Name = ci.DisplayName,
                LocaleCode = ci.Name,
                CountryCode = iso.NumericCode
            };
            Context.Cultures.Add(c);
            Context.SaveChanges();
        }

        [Serializable]
        public class InvalidDataException : Exception
        {
            public InvalidDataException() : base() { }
            public InvalidDataException(string msg) : base(msg) { }
        }

        class TypedEntityinfo<T>
        {
            internal T Entity;
            internal EntityInfo Info;

            internal static IEnumerable<TypedEntityinfo<T>> GetTyped(IEnumerable<EntityInfo> info)
            {
                return info.Select(i => new TypedEntityinfo<T> { Info = i, Entity = (T)i.Entity }).ToList();
            }
        }

        #region IDisposable
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~ValidateMedSim() { Dispose(false); }
        void Dispose(bool disposing)
        { // would be protected virtual if not sealed 
            if (disposing && _context!=null)
            { // only run this logic when Dispose is called
                _context.Dispose();
            }
        }
        #endregion //IDisposable
    }
}
