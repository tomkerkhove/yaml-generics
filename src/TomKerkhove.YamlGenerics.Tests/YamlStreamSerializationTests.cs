using System;
using System.IO;
using System.Linq;
using TomKerkhove.YamlGenerics.Tests.Model;
using TomKerkhove.YamlGenerics.Tests.Model.Enum;
using TomKerkhove.YamlGenerics.Tests.Serialization;
using Xunit;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TomKerkhove.YamlGenerics.Tests
{
    public class YamlStreamSerializationTests
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var audi = new Audi
            {
                LicensePlate = Guid.NewGuid().ToString(),
                IsDiesel = true
            };
            var merchant = new Person
            {
                FirstName = "Bill",
                LastName = "Bracket"
            };
            var volvo = new Volvo
            {
                LicensePlate = Guid.NewGuid().ToString(),
                HasAutomaticBreak = true
            };
            var ford = new Ford
            {
                LicensePlate = Guid.NewGuid().ToString(),
                HasSpareTire = true
            };
            var factory = new Factory
            {
                Cars =
                {
                    ford,
                    audi,
                    volvo
                },
                Merchant = merchant
            };

            // Act
            var serializer = new SerializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .EmitDefaults()
                .Build();
            var rawFactoryYaml = serializer.Serialize(factory);

            var input = new StringReader(rawFactoryYaml);
            var yamlStream = new YamlStream();
            yamlStream.Load(input);

            var deserializedFactory = FactorySerializer.Deserialize(yamlStream);

            // Assert
            Assert.NotNull(deserializedFactory);
            Assert.NotNull(deserializedFactory.Cars);
            Assert.Equal(deserializedFactory.Cars.Count, factory.Cars.Count);
            var deserializedAudi = (Audi) deserializedFactory.Cars.SingleOrDefault(car => car.Make == Make.Audi);
            Assert.NotNull(deserializedAudi);
            Assert.Equal(deserializedAudi.LicensePlate, audi.LicensePlate);
            Assert.Equal(deserializedAudi.IsDiesel, audi.IsDiesel);
            var deserializedFord = (Ford) deserializedFactory.Cars.SingleOrDefault(car => car.Make == Make.Ford);
            Assert.NotNull(deserializedFord);
            Assert.Equal(deserializedFord.LicensePlate, ford.LicensePlate);
            Assert.Equal(deserializedFord.HasSpareTire, ford.HasSpareTire);
            var deserializedVolvo = (Volvo) deserializedFactory.Cars.SingleOrDefault(car => car.Make == Make.Volvo);
            Assert.NotNull(deserializedVolvo);
            Assert.Equal(deserializedVolvo.LicensePlate, volvo.LicensePlate);
            Assert.Equal(deserializedVolvo.HasAutomaticBreak, volvo.HasAutomaticBreak);
        }
    }
}