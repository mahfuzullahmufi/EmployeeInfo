using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EmployeeInfo.Extentions
{
    public static class EnumDisplayExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var displayAttribute = value.GetType()
                .GetMember(value.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.Name ?? value.ToString();
        }
    }
}
