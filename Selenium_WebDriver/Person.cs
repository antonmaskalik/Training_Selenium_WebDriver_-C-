
namespace Selenium_WebDriver
{
    public class Person
    {
        private readonly string name;
        private readonly string position;
        private readonly string office;
        private readonly int age;
        private readonly int salary;

        public int Age { get { return age; } }
        public int Salary { get { return salary; } }

        public Person(string name, string position, string office, int age, int salary)
        {
            this.name = name;
            this.position = position;
            this.office = office;
            this.age = age;
            this.salary = salary;
        }
    }
}
