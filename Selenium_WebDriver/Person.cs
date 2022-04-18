
namespace Selenium_WebDriver
{
    public class Person
    {
        private readonly string NAME;
        private readonly string POSITION;
        private readonly string OFFICE;
        private readonly int AGE;
        private readonly int SALARY;

        public int Age { get { return AGE; } }
        public int Salary { get { return SALARY; } }

        public Person(string name, string position, string office, int age, int salary)
        {
            NAME = name;
            POSITION = position;
            OFFICE = office;
            AGE = age;
            SALARY = salary;
        }
    }
}
