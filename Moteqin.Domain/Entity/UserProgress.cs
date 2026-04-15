using Moteqin.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteqin.Domain.Entity
{
    public class UserProgress : BaseEntity
    {
        public string UserId { get; set; }
        public int AyahId { get; set; }

        public ProgressStatus Status { get; set; }

        public DateTime? MemorizedAt { get; set; }
        public DateTime? LastReviewedAt { get; set; }

        public ApplicationUser User { get; set; }
        public Ayah Ayah { get; set; }
    }
}
