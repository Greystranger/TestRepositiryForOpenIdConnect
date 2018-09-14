using System.Collections.Generic;

namespace EFCoreTutorialsConsoleApp.DomainModels.Entities
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Student> Students { get; set; }
    }
}
