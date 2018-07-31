using System.Drawing;
using TomKerkhove.YamlGenerics.Tests.Model.Enum;
using TomKerkhove.YamlGenerics.Tests.Model.Interfaces;

namespace TomKerkhove.YamlGenerics.Tests.Model
{
    public class Volvo : Car, ICar
    {
        public bool HasAutomaticBreak { get; set; }
        public Make Make { get; set; } = Make.Volvo;
    }
}