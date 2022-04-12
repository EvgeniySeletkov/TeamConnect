using System.Text.RegularExpressions;

namespace TeamConnect.Helpers
{
    public static class Validator
    {
        public static bool CheckIsEmailValid(string email)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            return regex.IsMatch(email);
        }

        public static bool CheckIsPasswordValid(string password)
        {
            var regex = new Regex(@"^[A-Z](?=.*[a-z])(?=.*\d)[a-zA-Z\d]{5,15}$"); ;

            return regex.IsMatch(password);
        }
    }
}
