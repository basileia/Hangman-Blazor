using System.ComponentModel;

namespace Hangman.Data.Models
{
    public enum Category
    {
        [Description("Programování")]
        Programovani,
        [Description("Učitelství")]
        Ucitelstvi,
        [Description("Zvířata")]
        Zvirata,
        Farmacie,
        Montessori,
        [Description("Povolání")]
        Povolani
    }
}
