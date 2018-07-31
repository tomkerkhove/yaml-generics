using TomKerkhove.YamlGenerics.Tests.Model.Enum;

namespace TomKerkhove.YamlGenerics.Tests.Model.Interfaces
{
    public interface ICar
    {
        string LicensePlate { get; set; }
        Make Make { get; set; }
    }
}