using Amazon.DynamoDBv2.DocumentModel;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NodaTime.DynamoDB.Conversions.Tests
{
    public class InstantConverterTests
    {
        InstantConverter converter;
        public InstantConverterTests()
        {
            converter = new InstantConverter();
        }

        [Fact]
        public void ItShouldConvertToStringPrimitive()
        {
            var converted = CreateInstantEntry();
            Assert.NotNull(converted.AsString());
        }

        DynamoDBEntry CreateInstantEntry()
        {
            var val = Instant.FromDateTimeUtc(DateTime.UtcNow);

            return converter.ToEntry(val);
        }

        [Fact]
        public void ItShouldConvertToNull()
        {
            var converted = converter.ToEntry(null);
            Assert.IsType<DynamoDBNull>(converted.AsDynamoDBNull());
        }

        [Fact]
        public void ItShouldConvertFromStringPrimitive()
        {
            var entry = CreateInstantEntry();
            var inst = converter.FromEntry(entry);
            Assert.IsType<Instant>(inst);
        }

        [Fact]
        public void ItShouldConvertFromNull()
        {
            var entry = converter.ToEntry(null);
            Instant? inst = (Instant?)converter.FromEntry(entry);
            Assert.False(inst.HasValue);
        }
    }
}
