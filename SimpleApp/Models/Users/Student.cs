using System;
using System.Text.Json.Serialization;

namespace SimpleApp.Models.Users
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; } = null!;
    }
}
