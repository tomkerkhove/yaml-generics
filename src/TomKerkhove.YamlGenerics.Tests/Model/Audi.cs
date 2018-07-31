using TomKerkhove.YamlGenerics.Tests.Model.Enum;
using TomKerkhove.YamlGenerics.Tests.Model.Interfaces;

namespace TomKerkhove.YamlGenerics.Tests.Model
{
    public class Audi : Car, ICar
    {
        public bool IsDiesel { get; set; }
        public Make Make { get; set; } = Make.Audi;
    }
}