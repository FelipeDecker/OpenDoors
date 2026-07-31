using SistemaGestaoLar.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoLar.Api.Helpers
{
    public static class EnumDisplayHelper
    {
        public static List<EnumItemModel> GetItems<TEnum>() where TEnum : struct, Enum
        {
            return Enum.GetValues<TEnum>()
                .Select(valor => new EnumItemModel(Convert.ToInt32(valor), GetDisplayName(valor)))
                .ToList();
        }

        private static string GetDisplayName<TEnum>(TEnum valor) where TEnum : struct, Enum
        {
            var membro = typeof(TEnum).GetMember(valor.ToString()).FirstOrDefault();
            var display = membro?.GetCustomAttributes(typeof(DisplayAttribute), false)
                .Cast<DisplayAttribute>()
                .FirstOrDefault();

            return display?.Name ?? valor.ToString();
        }
    }
}
