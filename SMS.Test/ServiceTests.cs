
using Xunit;
using SMS.Data.Models;
using SMS.Data.Services;

namespace SMS.Test
{

    public class ServiceTests
    {
        private readonly IStudentService svc;

        public ServiceTests()
        {
            // general arrangement
            svc = new StudentServiceDb();

            // ensure data source is empty before each test
            svc.Initialise();
        }

        [Fact] // --- AddStudent Duplicate Test
        public void AddStudent_WhenDuplicateEmail_ShouldReturnNull()
        {
            // act 
            var s1 = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0, "");
            // this is a duplicate as the email address is same as previous student
            var s2 = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0, "");
            
            // assert
            Assert.NotNull(s1); // this student should have been added correctly
            Assert.Null(s2); // this student should NOT have been added        
        }

        [Fact]
        public void AddStudent_WhenNone_ShouldSetAllProperties()
        {
            // act 
            var added = svc.AddStudent("XXX", "Computing", "xxx@email.com", 20, 0, "");
            
            // retrieve student just added by using the Id returned by EF
            var s = svc.GetStudent(added.Id);

            // assert - that student is not null
            Assert.NotNull(s);
            
            // now assert that the properties were set properly
            Assert.Equal(s.Id, s.Id);
            Assert.Equal("XXX", s.Name);
            Assert.Equal("xxx@email.com", s.Email);
            Assert.Equal("Computing", s.Course);
            Assert.Equal(20, s.Age);
            Assert.Equal(0, s.Grade);
        }

        [Fact]
        public void UpdateStudent_ThatExists_ShouldSetAllProperties()
        {
            // arrange - create test student
            var s = svc.AddStudent("ZZZ", "zzz@email.com",  "Maths", 30, 100, "");
                        
            // act - create a copy and update any student properties (except Id) 
            var u = new Student {
                Id = s.Id,
                Name = "XXX",
                Email = "xxx@email.com",
                Course = "Computing",
                Age = 31,
                Grade = 50,
                PhotoUrl = "http://photo.com"
            };
            // save updated student
            svc.UpdateStudent(u); 

            // reload updated student from database into us
            var us = svc.GetStudent(s.Id);

            // assert
            Assert.NotNull(u);           

            // now assert that the properties were set properly           
            Assert.Equal(u.Name, us.Name);
            Assert.Equal(u.Email, us.Email);
            Assert.Equal(u.Course, us.Course);
            Assert.Equal(u.Age, us.Age);
            Assert.Equal(u.Grade, us.Grade);
            Assert.Equal(u.PhotoUrl, us.PhotoUrl);
            
        }

        [Fact] 
        public void GetAllStudents_WhenNone_ShouldReturn0()
        {
            // act 
            var students = svc.GetStudents();
            var count = students.Count;

            // assert
            Assert.Equal(0, count);
        }


        [Fact]
        public void GetStudents_With2Added_ShouldReturn2()
        {
            // arrange
            var s1 = svc.AddStudent("XXX", "Computing",   "xxx@email.com", 20, 0, "");
            var s2 = svc.AddStudent("YYY", "Engineering", "yyy@email.com", 23, 0, "");

            // act
            var students = svc.GetStudents();
            var count = students.Count;

            // assert
            Assert.Equal(2, count);
        }


        [Fact] 
        public void GetStudent_WhenNone_ShouldReturnNull()
        {
            // act 
            var student = svc.GetStudent(1); // non existent student

            // assert
            Assert.Null(student);
        }



        [Fact] 
        public void GetStudent_WhenAdded_ShouldReturnStudent()
        {
            // act 
            var s = svc.AddStudent("XXX", "Computing", "xxx@email.com", 20, 0, "");

            var ns = svc.GetStudent(s.Id);

            // assert
            Assert.NotNull(ns);
            Assert.Equal(s.Id, ns.Id);
        }


        [Fact]
        public void DeleteStudent_ThatExists_ShouldReturnTrue()
        {
            // act 
            var s = svc.AddStudent("XXX", "Computing", "xxx@email.com", 20, 0, "");
            var deleted = svc.DeleteStudent(s.Id);

            // try to retrieve deleted student
            var s1 = svc.GetStudent(s.Id);

            // assert
            Assert.True(deleted); // delete student should return true
            Assert.Null(s1);      // s1 should be null
        }


        [Fact]
        public void DeleteStudent_ThatDoesntExist_ShouldReturnFalse()
        {
            // act 	
            var deleted = svc.DeleteStudent(0);

            // assert
            Assert.False(deleted);
        }        

        [Fact]
        public void UpdateStudent_ExistingStudentWithAgePlusOne_ShouldWork()
        {
            // arrange
            var added = svc.AddStudent("Clare", "Computing", "clare@gmail.com", 21, 0, "");

            // act
            // create a copy of added student and increment age by 1
            var s = new Student {
                Id = added.Id,
                Name = added.Name,
                Course = added.Course,
                Email = added.Email,
                Age =  added.Age + 1,
                Grade = added.Grade                
            };
            // update this student
            svc.UpdateStudent(s);

            // now load the student and verify age was updated
            var su = svc.GetStudent(s.Id);

            // assert
            Assert.Equal(s.Age, su.Age);
        }
            
    }
}