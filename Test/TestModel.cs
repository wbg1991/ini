using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ini;

namespace Test
{
    [Section("TEST")]
    internal class TestModel : IEquatable<TestModel>
    {
        [Key("INDEX")]
        public long Index { get; set; }

        [Key("IS_MAXIMAL", "ETC")]
        public bool IsMaximalSized { get; set; }

        [NotMapped]
        public byte Offset { get; set; }

        public bool Equals(TestModel? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Index == other.Index && IsMaximalSized == other.IsMaximalSized;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TestModel) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Index, IsMaximalSized);
        }
    }
}
