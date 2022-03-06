using System;
using System.Linq;
using System.Collections.Generic;
using SMS.Data.Models;
using SMS.Data.Repository;

namespace SMS.Data.Services
{
    public class StudentServiceDb : IStudentService
    {
        private readonly DataContext db;

        public StudentServiceDb()
        {
            db = new DataContext();
        }

        public void Initialise()
        {
            db.Initialise(); // recreate database
        }

        // -------- Student Related Operations ------------

        // retrieve list of Students
        public List<Student> GetStudents()
        {
            return db.Students.ToList();
        }


        // Retrive student by Id 
        public Student GetStudent(int id)
        {
            return db.Students.FirstOrDefault(s => s.Id == id);
        }

        // Add a new student checking email is unique
        public Student AddStudent(string name, string course, string email,
                                    int age, double grade, string photoUrl)
        {
            // check if student with email exists            
            var exists = GetStudentByEmail(email);
            if (exists != null)
            {
                return null;
            } 

            // create new student
            var s = new Student
            {
                Name = name,
                Course = course,
                Email = email,
                Age = age,
                Grade = grade,
                PhotoUrl = photoUrl
            };
            db.Students.Add(s); // add student to the list
            db.SaveChanges();
            return s; // return newly added student
        }

        // Delete the student identified by Id returning true if 
        // deleted and false if not found
        public bool DeleteStudent(int id)
        {
            var s = GetStudent(id);
            if (s == null)
            {
                return false;
            }
            db.Students.Remove(s);
            db.SaveChanges();
            return true;
        }

        // Update the student with the details in updated 
        public Student UpdateStudent(Student updated)
        {
            // verify the student exists
            var student = GetStudent(updated.Id);
            if (student == null)
            {
                return null;
            }
            // update the details of the student retrieved and save
            student.Name = updated.Name;
            student.Email = updated.Email;
            student.Course = updated.Course;
            student.Age = updated.Age;
            student.Grade = updated.Grade;
            student.PhotoUrl = updated.PhotoUrl;

            db.SaveChanges();
            return student;
        }

        public Student GetStudentByEmail(string email)
        {
            return db.Students.FirstOrDefault(s => s.Email == email);
        }
    }
}
