using Amazon.DynamoDBv2.DocumentModel;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NodaTime.DynamoDB.Conversions.Tests
{
    public class ZonedDateTimeConverterTests
    {

        ZonedDateTimeConverter converter;
        public ZonedDateTimeConverterTests()
        {
            converter = new ZonedDateTimeConverter();
        }

        [Fact]
        public void ItShouldConvertToStringPrimitive()
        {
            var converted = CreateZonedEntry();
            Assert.NotNull(converted.AsString());
        }

        DynamoDBEntry CreateZonedEntry()
        {
            var val = ZonedDateTime.FromDateTimeOffset(DateTimeOffset.Now);

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
            var entry = CreateZonedEntry();
            var inst = converter.FromEntry(entry);
            Assert.IsType<ZonedDateTime>(inst);
        }

        [Fact]
        public void ItShouldConvertFromNull()
        {
            var entry = converter.ToEntry(null);
            ZonedDateTime? val = (ZonedDateTime?)converter.FromEntry(entry);
            Assert.False(val.HasValue);
        }

    }
}
