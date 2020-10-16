using System;

namespace SearchFlight.Model
{
    public class ProgrammingLanguage
    {
        public ProgrammingLanguage(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
