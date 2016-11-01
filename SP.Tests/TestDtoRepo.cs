﻿using SP.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SP.DataAccess;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SimPlanner.Tests
{
    /// <summary>
    /// Summary description for TestDtoRepo
    /// </summary>
    [TestClass]
    public sealed class TestDtoRepo: IDisposable
    {
        public TestDtoRepo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private MedSimDtoRepository _repo;
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext) {}
        //
        // Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup() {}
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize() {
            Guid userId;
            IEnumerable<string> roles;
            using (var db = new MedSimDbContext())
            {
                userId = (from u in db.Users
                          where u.Email == "brentm@adhb.govt.nz"
                          select u.Id).First();
                roles = (from r in db.Roles
                         where r.Users.Any(u => u.UserId == userId)
                         select r.Name).ToList();
            }
            _repo = new MedSimDtoRepository(new MockIPrincipal());
        }
        //
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup() {
            if (_repo != null) {
                _repo.Dispose();
                _repo = null;
            }
        }
        //
        #endregion

        [TestMethod]
        public void TestCourses()
        {
            var courses = (from c in _repo.GetCourses(new[] { "CourseParticipants", "CourseDays" })
                           let firstDay = c.CourseDays.FirstOrDefault(cd => cd.Day == 2)
                           where firstDay.StartUtc > DateTime.UtcNow
                           orderby firstDay.StartUtc
                           select new { firstDay.StartUtc, c.CourseParticipants }).ToList();
            Console.WriteLine(string.Join("\r\n",courses.Select(a => a.StartUtc.ToString("s"))));
            Assert.IsFalse(courses.Zip(courses.Skip(1), (a, b) => a.StartUtc < b.StartUtc).Contains(false));
        }

        #region IDisposable

        bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TestDtoRepo()
        {
            Dispose(false);
        }

        void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing && _repo!=null)
            {
                // free other managed objects that implement
                // IDisposable only
                _repo.Dispose();
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }
        #endregion //IDisposable
    }

    class MockIPrincipal : System.Security.Principal.IPrincipal
    {
        internal MockIPrincipal() { Identity = new MockIIdentity(); }
        public System.Security.Principal.IIdentity Identity { get; private set; }
        public bool IsInRole(string roleName) { return true; }

        public class MockIIdentity : System.Security.Principal.IIdentity
        {
            public string Name { get { return "brentm@adhb.govt.nz"; } }
            public bool IsAuthenticated { get { return true; } }
            public string AuthenticationType { get { return "Mock"; } }
        }
    }
}
