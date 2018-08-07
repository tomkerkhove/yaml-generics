using System;
using TomKerkhove.YamlGenerics.Tests.Model;
using TomKerkhove.YamlGenerics.Tests.Model.Enum;
using TomKerkhove.YamlGenerics.Tests.Model.Interfaces;
using YamlDotNet.RepresentationModel;

namespace TomKerkhove.YamlGenerics.Tests.Serialization
{
    public static class CarSerializer
    {
        public static ICar Deserialize(YamlMappingNode item, Make make)
        {
            switch (make)
            {
                case Make.Audi:
                    return DeserializeAudi(item);
                case Make.Ford:
                    return DeserializeFord(item);
                case Make.Volvo:
                    return DeserializeVolvo(item);
                default:
                    throw new ArgumentOutOfRangeException(nameof(make), $"Not able to deserizalize make '{make}'");
            }
        }

        private static ICar DeserializeAudi(YamlMappingNode item)
        {
            var licencePlate = item.Children[new YamlScalarNode(value: "licensePlate")]?.ToString();
            var rawIsDiesel = item.Children[new YamlScalarNode(value: "isDiesel")]?.ToString();
            bool.TryParse(rawIsDiesel, out var isDiesel);
            var audi = new Audi
            {
                LicensePlate = licencePlate,
                IsDiesel = isDiesel
            };

            return audi;
        }

        private static ICar DeserializeFord(YamlMappingNode item)
        {
            var licencePlate = item.Children[new YamlScalarNode(value: "licensePlate")]?.ToString();
            var rawHasSpareTire = item.Children[new YamlScalarNode(value: "hasSpareTire")]?.ToString();
            bool.TryParse(rawHasSpareTire, out var hasSpareTire);
            var ford = new Ford
            {
                LicensePlate = licencePlate,
                HasSpareTire = hasSpareTire
            };
            return ford;
        }

        private static ICar DeserializeVolvo(YamlMappingNode item)
        {
            var licencePlate = item.Children[new YamlScalarNode(value: "licensePlate")]?.ToString();
            var rawHasAutomaticBreak = item.Children[new YamlScalarNode(value: "hasAutomaticBreak")]?.ToString();
            bool.TryParse(rawHasAutomaticBreak, out var hasAutomaticBreak);
            var volvo = new Volvo
            {
                LicensePlate = licencePlate,
                HasAutomaticBreak = hasAutomaticBreak
            };

            return volvo;
        }
    }
}