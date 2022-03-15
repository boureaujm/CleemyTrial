using System.ComponentModel.DataAnnotations;

namespace CleemyCommons.Types
{
    public enum CurrencyEnum
    {
        [Display(Description ="US Dollar",ShortName = "USD")]
        USDollar,

        [Display(Description = "Russian ruble", ShortName = "RUB")]
        Ruble,
    }
}