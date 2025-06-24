using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class Verification
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int MethodId { get; set; }

    public string VerificationCode { get; set; } = null!;

    public DateTime? CreatedTime { get; set; }

    public DateTime ExpiresTime { get; set; }

    public bool? IsVerified { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual VerificationMethod Method { get; set; } = null!;
}
