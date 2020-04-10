using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Models
{
    public partial class TableData
    {
        [JsonProperty("Strings")]
        public Columns Strings { get; set; }

        [JsonProperty("Columns")]
        public Columns Columns { get; set; }
    }

    public partial class Columns
    {
        [JsonProperty("Table")]
        public string Table { get; set; }

        [JsonProperty("Data")]
        public string Data { get; set; }
    }

    public partial class TableData
    {
        public static TableData FromJson(string json) => JsonConvert.DeserializeObject<TableData>(json, ReportApp.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this TableData self) => JsonConvert.SerializeObject(self, ReportApp.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
