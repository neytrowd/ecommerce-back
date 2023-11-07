namespace Ecommerce.Common.Utils
{
    public static class StringUtils
    {
        public static string InsertParamsIntoString(string str, Dictionary<string, string> prms)
        {
            var result = str;
            foreach(var prm in prms)
            {
                result = result.Replace($"{{{prm.Key}}}", prm.Value);
            }

            return result;
        }
    }
}