using TomKerkhove.YamlGenerics.Tests.Model.Enum;
using TomKerkhove.YamlGenerics.Tests.Model.Interfaces;

namespace TomKerkhove.YamlGenerics.Tests.Model
{
    public class Ford : Car, ICar
    {
        public Make Make { get; set; } = Make.Ford;
        public bool HasSpareTire { get; set; }
    }
}