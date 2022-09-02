using System.Globalization;
using Newtonsoft.Json;

namespace WorldsAdriftServer.Objects.UnityObjects
{
    internal class ColorConverter : JsonConverter
    {
        public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer )
        {
            UnityColor color = (UnityColor)value;
            writer.WriteStartObject();
            writer.WritePropertyName("r");
            serializer.Serialize(writer, color.r);
            writer.WritePropertyName("g");
            serializer.Serialize(writer, color.g);
            writer.WritePropertyName("b");
            serializer.Serialize(writer, color.b);
            writer.WritePropertyName("a");
            serializer.Serialize(writer, color.a);
            writer.WriteEndObject();
        }

        public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer )
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        public override bool CanConvert( Type objectType )
        {
            return objectType == typeof(UnityColor);
        }
    }
    internal struct UnityColor
    {
        public float a { get; set; }
        public float b { get; set; }
        public float g { get; set; }
        public float r { get; set; }

        public UnityColor( float r, float g, float b, float a )
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
        public static int HexToInt( char hexValue )
        {
            return int.Parse(hexValue.ToString(), NumberStyles.HexNumber);
        }
        public static UnityColor FromHex( string color )
        {
            if (color.IndexOf("#") == 0)
            {
                color = color.Substring(1);
            }
            float r = ((float)HexToInt(color[1]) + (float)HexToInt(color[0]) * 16f) / 255f;
            float g = ((float)HexToInt(color[3]) + (float)HexToInt(color[2]) * 16f) / 255f;
            float b = ((float)HexToInt(color[5]) + (float)HexToInt(color[4]) * 16f) / 255f;
            float a = (color.Length <= 6) ? 1f : (((float)HexToInt(color[7]) + (float)HexToInt(color[6]) * 16f) / 255f);
            return new UnityColor
            {
                r = r,
                g = g,
                b = b,
                a = a
            };
        }
    }
}
