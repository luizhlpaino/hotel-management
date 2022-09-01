namespace Domain
{
    public static class Utils
    {
        public static bool ValidateEmail(string email)
        {
            if (
                !email.Contains("@") ||
                !email.Contains(".")
            )
                return false;

            return true;
        }
    }
}