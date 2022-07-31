using System;

namespace A_Test.Domain
{
    public class Info
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public int Age { get; private set; }

        public Info(string name, string family, int age)
        {
            Name = name;
            Family = family;
            Age = age;
        }


        public void Edit(string name, string family, int age)
        {
            Name = name;
            Family = family;
            Age = age;
        }
    }
}
