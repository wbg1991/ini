using System;
using System.Collections.Generic;
using System.Text;

namespace Ini
{
    public struct IniValue
    {
        private static bool TryParseSByte(string text, out sbyte value)
        {
            if (sbyte.TryParse(text,
                    System.Globalization.NumberStyles.Integer,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var res))
            {
                value = res;
                return true;
            }
            value = 0;
            return false;
        }

        private static bool TryParseByte(string text, out byte value)
        {
            if (byte.TryParse(text,
                    System.Globalization.NumberStyles.Integer,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var res))
            {
                value = res;
                return true;
            }
            value = 0;
            return false;
        }

        private static bool TryParseShort(string text, out short value)
        {
            if (short.TryParse(text,
                    System.Globalization.NumberStyles.Integer,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var res))
            {
                value = res;
                return true;
            }
            value = 0;
            return false;
        }

        private static bool TryParseUShort(string text, out ushort value)
        {
            if (ushort.TryParse(text,
                    System.Globalization.NumberStyles.Integer,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var res))
            {
                value = res;
                return true;
            }
            value = 0;
            return false;
        }

        private static bool TryParseInt(string text, out int value)
        {
            if (int.TryParse(text,
                    System.Globalization.NumberStyles.Integer,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var res))
            {
                value = res;
                return true;
            }
            value = 0;
            return false;
        }

        private static bool TryParseUInt(string text, out uint value)
        {
            if (uint.TryParse(text,
                    System.Globalization.NumberStyles.Integer,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var res))
            {
                value = res;
                return true;
            }
            value = 0;
            return false;
        }

        private static bool TryParseLong(string text, out long value)
        {
            if (long.TryParse(text,
                    System.Globalization.NumberStyles.Integer,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var res))
            {
                value = res;
                return true;
            }
            value = 0;
            return false;
        }

        private static bool TryParseULong(string text, out ulong value)
        {
            if (ulong.TryParse(text,
                    System.Globalization.NumberStyles.Integer,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var res))
            {
                value = res;
                return true;
            }
            value = 0;
            return false;
        }

        private static bool TryParseFloat(string text, out float value)
        {
            if (float.TryParse(text,
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var res))
            {
                value = res;
                return true;
            }
            value = float.NaN;
            return false;
        }

        private static bool TryParseDouble(string text, out double value)
        {
            if (double.TryParse(text,
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var res))
            {
                value = res;
                return true;
            }
            value = double.NaN;
            return false;
        }

        public string Value;

        public IniValue(object value)
        {
            if (value is IFormattable formatter)
            {
                Value = formatter.ToString(null, System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                Value = value?.ToString();
            }
        }

        public IniValue(string value)
        {
            Value = value;
        }

        public bool ToBool(bool valueIfInvalid = false)
        {
            return TryConvertBool(out var res) ? res : valueIfInvalid;
        }

        public bool TryConvertBool(out bool result)
        {
            if (Value == null)
            {
                result = default(bool);
                return false;
            }
            var boolStr = Value.Trim().ToLowerInvariant();
            switch (boolStr)
            {
                case "true":
                    result = true;
                    return true;
                case "false":
                    result = false;
                    return true;
                default:
                    result = default(bool);
                    return false;
            }
        }

        public sbyte ToSByte(sbyte valueIfInvalid = 0)
        {
            return TryConvertSByte(out var res) ? res : valueIfInvalid;
        }

        public bool TryConvertSByte(out sbyte result)
        {
            if (Value == null)
            {
                result = default(sbyte);
                return false;
            }

            return TryParseSByte(Value.Trim(), out result);
        }

        public byte ToByte(byte valueIfInvalid = 0)
        {
            return TryConvertByte(out var res) ? res : valueIfInvalid;
        }

        public bool TryConvertByte(out byte result)
        {
            if (Value == null)
            {
                result = default(byte);
                return false;
            }

            return TryParseByte(Value.Trim(), out result);
        }

        public short ToShort(short valueIfInvalid = 0)
        {
            return TryConvertShort(out var res) ? res : valueIfInvalid;
        }

        public bool TryConvertShort(out short result)
        {
            if (Value == null)
            {
                result = default(short);
                return false;
            }

            return TryParseShort(Value.Trim(), out result);
        }

        public ushort ToUShort(ushort valueIfInvalid = 0)
        {
            return TryConvertUShort(out var res) ? res : valueIfInvalid;
        }

        public bool TryConvertUShort(out ushort result)
        {
            if (Value == null)
            {
                result = default(ushort);
                return false;
            }

            return TryParseUShort(Value.Trim(), out result);
        }

        public int ToInt(int valueIfInvalid = 0)
        {
            return TryConvertInt(out var res) ? res : valueIfInvalid;
        }

        public bool TryConvertInt(out int result)
        {
            if (Value == null)
            {
                result = default(int);
                return false;
            }

            return TryParseInt(Value.Trim(), out result);
        }

        public uint ToUInt(uint valueIfInvalid = 0)
        {
            return TryConvertUInt(out var res) ? res : valueIfInvalid;
        }

        public bool TryConvertUInt(out uint result)
        {
            if (Value == null)
            {
                result = default(uint);
                return false;
            }

            return TryParseUInt(Value.Trim(), out result);
        }

        public long ToLong(long valueIfInvalid = 0)
        {
            return TryConvertLong(out var res) ? res : valueIfInvalid;
        }

        public bool TryConvertLong(out long result)
        {
            if (Value == null)
            {
                result = default(long);
                return false;
            }

            return TryParseLong(Value.Trim(), out result);
        }

        public ulong ToULong(ulong valueIfInvalid = 0)
        {
            return TryConvertULong(out var res) ? res : valueIfInvalid;
        }

        public bool TryConvertULong(out ulong result)
        {
            if (Value == null)
            {
                result = default(ulong);
                return false;
            }

            return TryParseULong(Value.Trim(), out result);
        }

        public float ToFloat(float valueIfInvalid = 0)
        {
            if (TryConvertFloat(out var res))
            {
                return res;
            }
            return valueIfInvalid;
        }

        public bool TryConvertFloat(out float result)
        {
            if (Value == null)
            {
                result = default(float);
                return false; ;
            }

            return TryParseFloat(Value.Trim(), out result);
        }

        public double ToDouble(double valueIfInvalid = 0)
        {
            return TryConvertDouble(out var res) ? res : valueIfInvalid;
        }

        public bool TryConvertDouble(out double result)
        {
            if (Value == null)
            {
                result = default(double);
                return false; ;
            }

            return TryParseDouble(Value.Trim(), out result);
        }

        public string GetString()
        {
            return GetString(true, false);
        }

        public string GetString(bool preserveWhitespace)
        {
            return GetString(true, preserveWhitespace);
        }

        public string GetString(bool allowOuterQuotes, bool preserveWhitespace)
        {
            if (Value == null)
            {
                return "";
            }
            var trimmed = Value.Trim();
            if (allowOuterQuotes && trimmed.Length >= 2 && trimmed[0] == '"' && trimmed[trimmed.Length - 1] == '"')
            {
                var inner = trimmed.Substring(1, trimmed.Length - 2);
                return preserveWhitespace ? inner : inner.Trim();
            }
            else
            {
                return preserveWhitespace ? Value : Value.Trim();
            }
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator IniValue(byte o)
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue(short o)
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue(int o)
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue(sbyte o)
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue(ushort o)
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue(uint o)
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue(float o)
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue(double o)
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue(bool o)
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue(string o)
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue(Enum o)
        {
            return new IniValue(o);
        }

        public static IniValue Default { get; } = new IniValue();
    }
}
