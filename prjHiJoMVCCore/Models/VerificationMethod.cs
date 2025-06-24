using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class VerificationMethod
{
    public int Id { get; set; }

    public string MethodName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<Verification> Verifications { get; set; } = new List<Verification>();
}
