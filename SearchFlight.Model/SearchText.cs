using System;

namespace SearchFlight.Model
{
    public class SearchText
    {
        public SearchText(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
