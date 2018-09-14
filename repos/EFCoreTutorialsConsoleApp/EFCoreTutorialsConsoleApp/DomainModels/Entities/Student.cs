using System;

namespace EFCoreTutorialsConsoleApp.DomainModels.Entities
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public byte[] Photo { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public StudentAddress Address { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
