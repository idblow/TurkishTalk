using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace TurkishTalk.Persistance
{
    public static class ValueConversionExtensions
    {
        public static PropertyBuilder<T> HasJsonBase64Conversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class, new()
        {
            ValueConverter<T, string> converter = new ValueConverter<T, string>
            (
                v => System.Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(v))),
                v => JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(Convert.FromBase64String(v))) ?? new T()
            );

            ValueComparer<T> comparer = new ValueComparer<T>
            (
                (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                v => v == null ? 0 : JsonConvert.SerializeObject(v).GetHashCode(),
                v => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(v))
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);
            propertyBuilder.HasColumnType("text");

            return propertyBuilder;
        }
        
        public static PropertyBuilder<string> HasBase64Conversion(this PropertyBuilder<string> propertyBuilder)
        {
            ValueConverter<string, string> converter = new ValueConverter<string, string>
            (
                v => System.Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(v))),
                v => Encoding.UTF8.GetString(Convert.FromBase64String(v)) ?? string.Empty
            );

            ValueComparer<string> comparer = new ValueComparer<string>
            (
                (l, r) => string.Equals(l, r),
                v => v == null ? 0 : v.GetHashCode(),
                v => v
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);
            propertyBuilder.HasColumnType("text");

            return propertyBuilder;
        }
    }
}
