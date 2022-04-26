
namespace Selenium_WebDriver
{
    public class Member
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Password { get; }
        public string Address { get; }
        public string City { get; }
        public string State { get; }
        public string ZipCode { get; }
        public string Country { get; }
        public string Phone { get; }

        public Member()
        {
            FirstName = "Ivan";
            LastName = "Ivanov";
            Password = "12345";
            Address = "Lenina 10, 25, Gucci";
            City = "Maimi";
            State = "Florida";
            ZipCode = "00000";
            Country = "United States";
            Phone = "258262485428";
        }
    }
}
