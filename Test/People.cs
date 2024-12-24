using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ini;

namespace Test
{
    public enum Sex
    {
        MALE = 0,
        FEMALE = 0xfc,
        NON_SEX = 0xff
    }

    internal class People : IEquatable<People>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsMarried { get; set; }
        public Sex Sex { get; set; }
        [EnumValue(EnumValueAttribute.Saves.String)]
        public Sex Sex2 { get; set; }

        public bool Equals(People? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Name == other.Name && Age == other.Age && IsMarried == other.IsMarried && Sex == other.Sex && Sex2 == other.Sex2;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((People) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Age, IsMarried, Sex, Sex2);
        }
    }
}
