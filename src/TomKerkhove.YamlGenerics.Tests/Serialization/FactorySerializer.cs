using System.Collections.Generic;
using System.Linq;
using TomKerkhove.YamlGenerics.Tests.Model;
using TomKerkhove.YamlGenerics.Tests.Model.Enum;
using TomKerkhove.YamlGenerics.Tests.Model.Interfaces;
using YamlDotNet.RepresentationModel;

namespace TomKerkhove.YamlGenerics.Tests.Serialization
{
    public class FactorySerializer
    {
        public static Factory Deserialize(YamlStream yamlStream)
        {
            var document = yamlStream.Documents.First();
            var rootNode = (YamlMappingNode)document.RootNode;

            var carNode = (YamlSequenceNode)rootNode.Children[new YamlScalarNode("cars")];
            var cars = ParseCars(carNode);
            var merchantNode = rootNode.Children[new YamlScalarNode("merchant")];
            var merchant = ParseMerchant(merchantNode);

            var deserializedFactory = new Factory
            {
                Merchant = merchant
            };

            deserializedFactory.Cars.AddRange(cars);

            return deserializedFactory;
        }

        private static Person ParseMerchant(YamlNode yamlNode)
        {
            var items = (YamlMappingNode)yamlNode;
            var firstName = items.Children[new YamlScalarNode("firstName")];
            var lastName = items.Children[new YamlScalarNode("lastName")];
            var merchant = new Person
            {
                FirstName = firstName.ToString(),
                LastName = lastName.ToString()
            };

            return merchant;
        }

        private static List<ICar> ParseCars(YamlSequenceNode yamlNode)
        {
            var cars = new List<ICar>();
            foreach (YamlMappingNode item in yamlNode)
            {
                var rawMake = item.Children[new YamlScalarNode("make")];
                var parsedMake = System.Enum.Parse<Make>(rawMake.ToString());

                var car = CarSerializer.Deserialize(item, parsedMake);
                cars.Add(car);
            }

            return cars;
        }
    }
}