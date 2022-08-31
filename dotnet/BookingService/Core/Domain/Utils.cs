namespace Domain
{
    public static class Utils
    {
        public static bool ValidateEmail(string email)
        {
            if (
                string.IsNullOrEmpty(email) ||
                !email.Contains("@") ||
                !email.Contains(".")
            )
                return false;

            return true;
        }
    }
}