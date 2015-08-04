using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENTech.Store.Api.App_Start
{
    class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            // base.DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss'Z'";// "yyyy-MM-dd";
            //base.DateTimeStyles = System.Globalization.DateTimeStyles.AdjustToUniversal;
            //base.DateTimeStyles = System.Globalization.DateTimeStyles.None;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var v = reader.Value;

            if (v.GetType() == typeof(DateTime))
            {
                var d = (DateTime)v;
                if (d.Kind == DateTimeKind.Unspecified)
                    d = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second, DateTimeKind.Utc);
                else if (d.Kind == DateTimeKind.Local)
                    d = d.ToUniversalTime();
                return d;
            }
            else if (v.GetType() == typeof(string))
            {
                var s = v.ToString();
                if (s.Contains("T"))
                {
                    var parts = s.Split('T');
                    var datePart  = parts[0];
                    var timePart  = parts[1];
                    var timeParts = parts[1].Split(':');
                    var hh = timeParts.Length > 0 && !string.IsNullOrWhiteSpace(timeParts[0])? Convert.ToInt32(timeParts[0]) : 0;
                    var mm = timeParts.Length > 1 && !string.IsNullOrWhiteSpace(timeParts[1])? Convert.ToInt32(timeParts[1]) : 0;
                    var ss = timeParts.Length > 2 && !string.IsNullOrWhiteSpace(timeParts[2])? Convert.ToInt32(timeParts[2]) : 0;

                    var d = DateTime.ParseExact(datePart, "yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);

                    var result = new DateTime(d.Year, d.Month, d.Day, hh, mm, ss, DateTimeKind.Utc);
                    return result;
                }
            }

            return base.ReadJson(reader, objectType, existingValue, serializer);
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var d = (DateTime)value;
            var v = d.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
            writer.WriteValue(v);
            //base.WriteJson(writer, value, serializer);
        }
    }

    class MyDateTimeConvertor : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
