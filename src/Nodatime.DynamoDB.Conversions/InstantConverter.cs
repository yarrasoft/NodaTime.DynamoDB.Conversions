using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using NodaTime;
using NodaTime.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nodatime.DynamoDB.Conversions
{
    public class InstantConverter : IPropertyConverter
    {
        InstantPattern pattern = InstantPattern.ExtendedIso;

        public object FromEntry(DynamoDBEntry entry)
        {
            if(entry is DynamoDBNull)
            {
                return new Instant?();
            }
            var s = entry.AsString();
            var res = pattern.Parse(s);
            return res.GetValueOrThrow();
        }

        public DynamoDBEntry ToEntry(object value)
        {
            if(null ==value)
            {
                return new DynamoDBNull();
            }
            if (!(value is Instant inst))
            {
                throw new ArgumentException("Can only convert Instants");
            }

            return new Primitive
            {
                Value = pattern.Format(inst)
            };
        }
    }
}
