using System;
using System.Collections.Generic;

namespace Trabajo1.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Usuario1 { get; set; }

    public string? Pass { get; set; }

    public int? Intentos { get; set; }

    public bool? estado { get; set; }
}


