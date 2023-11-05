namespace Ecommerce.Common.Constants
{
    public static class ValidationRegulars
    {
        public static readonly string Password = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$";
        public static readonly string Email = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,5}$";
    }
}