namespace EmployeeInfo.Extentions
{
    public static class NameDevideExtensions
    {
        public static string GetFirstName(this string name)
        {
            string[] names = name.Split(' ');
            return names[0];
        }
    }
}
