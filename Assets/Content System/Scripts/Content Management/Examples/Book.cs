using System.Collections.Generic;

namespace URIMP.Examples
{
    public class Book : ContentBase
    {
        public List<Person> Persons { get; set; } = new();

        public Book(string id, string name, List<Person> persons) : base(id, name)
        {
            Persons = persons;
        }

        public override void Delete()
        {
        }

        public override void Load()
        {
        }

        public override void Save()
        {
        }
    }

    public class Person : ISubcontent
    {
        public string Name { get; set; }
        public string PersonPagePath { get; set; }
    }
}