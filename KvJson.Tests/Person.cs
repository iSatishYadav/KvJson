using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace KvJson.Tests
{
    public class Person : IEquatable<Person>
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("website")]
        public string Website { get; set; }

        //public override string ToString() => string.Join("|", typeof(Person).GetProperties().Select(x=> $"{x.Name}:{x.GetValue(this)}"));
        public override bool Equals(object obj)
        {
            return obj is Person person &&  Equals(person);
        }

        public bool Equals(Person other)
        {
            return other != null &&
                   Id == other.Id &&
                   Name == other.Name &&
                   Username == other.Username &&
                   Email == other.Email &&
                   Phone == other.Phone &&
                   Website == other.Website;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Username, Email, Phone, Website);
        }

        public static bool operator ==(Person left, Person right)
        {
            return EqualityComparer<Person>.Default.Equals(left, right);
        }

        public static bool operator !=(Person left, Person right)
        {
            return !(left == right);
        }
    }
}
