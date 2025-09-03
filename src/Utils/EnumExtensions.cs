using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace edu_for_you.Utils
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var memberInfo = value.GetType().GetMember(value.ToString());
            var displayAttribute = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.Name ?? value.ToString();
        }
    }
}
