using System;
using System.Collections.Generic;

namespace elective.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime Enrollmentdate { get; set; }
    public int Queue_number { get; set; }
}
