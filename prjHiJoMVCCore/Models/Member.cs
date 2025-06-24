using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class Member
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PassWordHash { get; set; } = null!;

    public string PassWordSalt { get; set; } = null!;

    public string? Sex { get; set; }

    public DateOnly? Birthday { get; set; }

    public int? DistrictId { get; set; }

    public string? Resume { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsVerified { get; set; }

    public DateTime? LastOnline { get; set; }

    public int? CityId { get; set; }

    public string? AvatarPath { get; set; }

    public virtual ICollection<ChatMessage> ChatMessageReceivers { get; set; } = new List<ChatMessage>();

    public virtual ICollection<ChatMessage> ChatMessageSenders { get; set; } = new List<ChatMessage>();

    public virtual City? City { get; set; }

    public virtual District? District { get; set; }

    public virtual ICollection<Match> MatchUserId1Navigations { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchUserId2Navigations { get; set; } = new List<Match>();

    public virtual ICollection<MemberInterest> MemberInterests { get; set; } = new List<MemberInterest>();

    public virtual MemberPreference? MemberPreference { get; set; }

    public virtual ICollection<MemberRole> MemberRoles { get; set; } = new List<MemberRole>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Swipe> SwipeSwipers { get; set; } = new List<Swipe>();

    public virtual ICollection<Swipe> SwipeTargets { get; set; } = new List<Swipe>();

    public virtual ICollection<Verification> Verifications { get; set; } = new List<Verification>();

    public virtual ICollection<VipMember> VipMembers { get; set; } = new List<VipMember>();
}
