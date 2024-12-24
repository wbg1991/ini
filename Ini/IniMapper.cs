using System;
using System.Reflection;

namespace Ini
{
    public class IniMapper
    {
        public static void Save<T>(T item, string path)
        {
            var ini = new IniFile();

            // Get Item Section
            string itemSection;
            if (typeof(T).GetCustomAttribute(typeof(SectionAttribute)) is SectionAttribute s)
            {
                itemSection = string.IsNullOrWhiteSpace(s.Section) ? typeof(T).Name : s.Section;
            }
            else
            {
                itemSection = typeof(T).Name;
            }

            // Get Props Key
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                // NotMapped Proc
                if (Attribute.GetCustomAttribute(prop, typeof(NotMappedAttribute)) is NotMappedAttribute)
                {
                    continue;
                }

                // Key Proc
                if (Attribute.GetCustomAttribute(prop, typeof(KeyAttribute)) is KeyAttribute keyAttr)
                {
                    var section = string.IsNullOrWhiteSpace(keyAttr.Section) ? itemSection : keyAttr.Section;
                    var key = string.IsNullOrWhiteSpace(keyAttr.Key) ? prop.Name : keyAttr.Key;

                    dynamic value = GetPropertyValue(prop, item) ?? throw new InvalidOperationException();

                    ini[section][key] = value;
                }
                else
                {
                    dynamic value = GetPropertyValue(prop, item) ?? throw new InvalidOperationException();
                    
                    ini[itemSection][prop.Name] = value;
                }
            }

            // Ini File Save
            ini.Save(path);
        }

        public static T Load<T>(string path) where T : new()
        {
            var obj = new T();

            // Ini File Load
            var ini = new IniFile();
            ini.Load(path);

            // Get Item Section
            string itemSection;
            if (typeof(T).GetCustomAttribute(typeof(SectionAttribute)) is SectionAttribute s)
            {
                itemSection = string.IsNullOrWhiteSpace(s.Section) ? typeof(T).Name : s.Section;
            }
            else
            {
                itemSection = typeof(T).Name;
            }

            // Get Props Key
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                // NotMapped Proc
                if (Attribute.GetCustomAttribute(prop, typeof(NotMappedAttribute)) is NotMappedAttribute)
                {
                    continue;
                }

                // Key Proc
                if (Attribute.GetCustomAttribute(prop, typeof(KeyAttribute)) is KeyAttribute keyAttr)
                {
                    var section = string.IsNullOrWhiteSpace(keyAttr.Section) ? itemSection : keyAttr.Section;
                    var key = string.IsNullOrWhiteSpace(keyAttr.Key) ? prop.Name : keyAttr.Key;

                    SetPropertyValue(prop, obj, ini[section][key]);
                }
                else
                {
                    SetPropertyValue(prop, obj, ini[itemSection][prop.Name]);
                }
            }

            // Return
            return obj;
        }

        private static void SetPropertyValue(PropertyInfo prop, object obj, IniValue value)
        {
            if (prop.PropertyType == typeof(string))
            {
                prop.SetValue(obj, value.ToString());
            }
            else if (prop.PropertyType == typeof(sbyte))
            {
                prop.SetValue(obj, value.ToSByte());
            }
            else if (prop.PropertyType == typeof(byte))
            {
                prop.SetValue(obj, value.ToByte());
            }
            else if (prop.PropertyType == typeof(short))
            {
                prop.SetValue(obj, value.ToShort());
            }
            else if (prop.PropertyType == typeof(ushort))
            {
                prop.SetValue(obj, value.ToUShort());
            }
            else if (prop.PropertyType == typeof(int))
            {
                prop.SetValue(obj, value.ToInt());
            }
            else if (prop.PropertyType == typeof(uint))
            {
                prop.SetValue(obj, value.ToUInt());
            }
            else if (prop.PropertyType == typeof(long))
            {
                prop.SetValue(obj, value.ToLong());
            }
            else if (prop.PropertyType == typeof(ulong))
            {
                prop.SetValue(obj, value.ToULong());
            }
            else if (prop.PropertyType == typeof(float))
            {
                prop.SetValue(obj, value.ToFloat());
            }
            else if (prop.PropertyType == typeof(double))
            {
                prop.SetValue(obj, value.ToDouble());
            }
            else if (prop.PropertyType == typeof(bool))
            {
                prop.SetValue(obj, value.ToBool());
            }
            else if(prop.PropertyType.IsEnum)
            {
                var saveType = GetEnumSaveType(prop);

                switch (saveType)
                {
                    case EnumValueAttribute.Saves.Number:
                        prop.SetValue(obj, value.ToInt());
                        break;
                    case EnumValueAttribute.Saves.String:
                        prop.SetValue(obj, Enum.Parse(prop.PropertyType, value.ToString()));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

        }

        public static dynamic GetPropertyValue<T>(PropertyInfo prop, T item)
        {
            if (prop.PropertyType.IsEnum)
            {
                var saveType = GetEnumSaveType(prop);

                return saveType switch
                {
                    EnumValueAttribute.Saves.Number => (int)prop.GetValue(item),
                    EnumValueAttribute.Saves.String => prop.GetValue(item),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            else
            {
                return prop.GetValue(item);
            }
        }

        public static EnumValueAttribute.Saves GetEnumSaveType(PropertyInfo prop)
        {
            EnumValueAttribute.Saves saveType;
            if (Attribute.GetCustomAttribute(prop, typeof(EnumValueAttribute)) is EnumValueAttribute enumValue)
            {
                saveType = enumValue.SaveType;
            }
            else
            {
                saveType = EnumValueAttribute.Saves.Number;
            }

            return saveType;
        }
    }

    public class SectionAttribute : Attribute
    {
        public string Section;

        public SectionAttribute(string section)
        {
            Section = section;
        }
    }

    public class KeyAttribute : Attribute
    {
        public string Section;
        public string Key;

        public KeyAttribute(string key, string section = "")
        {
            Section = section;
            Key = key;
        }
    }

    public class NotMappedAttribute : Attribute
    {
    }

    public class EnumValueAttribute : Attribute
    {
        public enum Saves
        {
            Number,
            String
        }

        public Saves SaveType;

        public EnumValueAttribute(Saves saveType = Saves.Number)
        {
            SaveType = saveType;
        }
    }
}
