using System;
using System.IO;
using System.Linq;
using TomKerkhove.YamlGenerics.Tests.Model;
using TomKerkhove.YamlGenerics.Tests.Model.Enum;
using TomKerkhove.YamlGenerics.Tests.Model.Interfaces;
using Xunit;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TomKerkhove.YamlGenerics.Tests
{
    public class SerializationTests
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
            var volvo = new Volvo
            {
                LicensePlate = Guid.NewGuid().ToString(),
                HasAutomaticBreak = true
            };
            var ford = new Ford
            {
                LicensePlate = Guid.NewGuid().ToString(),
                HasSpareTier = true
            };
            var factory = new Factory
            {
                Cars =
                {
                    ford,
                    audi,
                    volvo
                }
            };

            // Act
            var serializer = new SerializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();
            var rawFactory = serializer.Serialize(factory);

            var input = new StringReader(rawFactory);
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();

            var deserializedFactory = deserializer.Deserialize<Factory>(input);

            // Assert
            Assert.NotNull(deserializedFactory);
            Assert.NotNull(deserializedFactory.Cars);
            Assert.Equal(deserializedFactory.Cars.Count, factory.Cars.Count);
            var deserializedAudi = (Audi)deserializedFactory.Cars.SingleOrDefault(car => car.Make == Make.Audi);
            Assert.NotNull(deserializedAudi);
            Assert.Equal(deserializedAudi.LicensePlate, audi.LicensePlate);
            Assert.Equal(deserializedAudi.IsDiesel, audi.IsDiesel);
            var deserializedFord = (Ford)deserializedFactory.Cars.SingleOrDefault(car => car.Make == Make.Ford);
            Assert.NotNull(deserializedFord);
            Assert.Equal(deserializedFord.LicensePlate, ford.LicensePlate);
            Assert.Equal(deserializedFord.HasSpareTier, ford.HasSpareTier);
            var deserializedVolvo = (Volvo)deserializedFactory.Cars.SingleOrDefault(car => car.Make == Make.Volvo);
            Assert.NotNull(deserializedVolvo);
            Assert.Equal(deserializedVolvo.LicensePlate, volvo.LicensePlate);
            Assert.Equal(deserializedVolvo.HasAutomaticBreak, volvo.HasAutomaticBreak);
        }
    }
}