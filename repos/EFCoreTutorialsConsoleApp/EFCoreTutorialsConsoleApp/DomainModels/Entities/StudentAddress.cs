using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTutorialsConsoleApp.DomainModels.Entities
{
    public class StudentAddress
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}
