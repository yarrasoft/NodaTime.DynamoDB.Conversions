using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using NodaTime;
using NodaTime.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nodatime.DynamoDB.Conversions
{
    public class ZonedDateTimeConverter : IPropertyConverter
    {
        ZonedDateTimePattern pattern = ZonedDateTimePattern.GeneralFormatOnlyIso.WithZoneProvider(DateTimeZoneProviders.Tzdb);

        public object FromEntry(DynamoDBEntry entry)
        {
            if (entry is DynamoDBNull)
            {
                return new ZonedDateTime?();
            }
            var s = entry.AsString();
            var res = pattern.Parse(s);
            return res.GetValueOrThrow();
        }

        public DynamoDBEntry ToEntry(object value)
        {
            if (null == value)
            {
                return new DynamoDBNull();
            }
            if (!(value is ZonedDateTime zoned))
            {
                throw new ArgumentException("Can only convert ZonedDateTimes");
            }

            return new Primitive
            {
                Value = pattern.Format(zoned)
            };
        }
    }
}
