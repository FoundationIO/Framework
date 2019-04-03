using System.ComponentModel.DataAnnotations;

namespace Framework.Infrastructure.Utils
{
    public static class ValidationUtils
    {
        public static bool IsEmail(string str)
        {
            return new EmailAddressAttribute().IsValid(str);
        }
    }
}
