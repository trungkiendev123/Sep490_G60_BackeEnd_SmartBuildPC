using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public Guid CustomerId { get; set; }
        public int ProductId { get; set; }
        public string CommentText { get; set; } = null!;
        public DateTime CommentDate { get; set; }
        public string? ReplyText { get; set; }
        public Guid? ReplyStaffId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual staff? ReplyStaff { get; set; }
    }
}
