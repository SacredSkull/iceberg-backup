using System;

namespace Iceberg.Entity {
    public class Hash : IEquatable<Hash> {
        public Hash(string val) {
            Value = val;
        }
        public string Value { get; protected set; }

        public bool Equals(Hash other) => Value.Equals(other.Value);

        public override string ToString() => Value;
    }
}