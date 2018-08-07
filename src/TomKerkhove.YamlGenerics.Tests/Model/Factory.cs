using System.Collections.Generic;
using TomKerkhove.YamlGenerics.Tests.Model.Interfaces;

namespace TomKerkhove.YamlGenerics.Tests.Model
{
    public class Factory
    {
        public List<ICar> Cars { get; } = new List<ICar>();
        public Person Merchant { get; set; }
    }
}
