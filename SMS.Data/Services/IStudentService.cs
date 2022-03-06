using System;
using System.Collections.Generic;
	
using SMS.Data.Models;
	
namespace SMS.Data.Services
{
    // This interface describes the operations that a StudentService class should implement
    public interface IStudentService
    {
        // Initialise the repository (database)  
        void Initialise();
        
        // new interface method
        Student GetStudentByEmail(string email);

        // ---------------- Student Management --------------
        List<Student> GetStudents();
        Student GetStudent(int id);
        Student AddStudent(string name, string course, string email, int age, double grade, string photoUrl);
        Student UpdateStudent(Student updated);  
        bool DeleteStudent(int id);

    }
    
}