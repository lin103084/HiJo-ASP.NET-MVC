using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class ChatMessage
{
    public int Id { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string? Message { get; set; }

    public string? MessageType { get; set; }

    public byte[]? MesImage { get; set; }

    public string? Source { get; set; }

    public DateTime CreaTime { get; set; }

    public bool IsRead { get; set; }

    public virtual Member Receiver { get; set; } = null!;

    public virtual Member Sender { get; set; } = null!;
}
