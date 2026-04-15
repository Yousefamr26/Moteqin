using Moteqin.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteqin.Domain.Entity
{
    public class Surah : BaseEntity
    {
        public string Name { get; set; }
        public int NumberOfAyahs { get; set; } // 👈 ضيف ده
        public ICollection<Ayah> Ayahs { get; set; }
    }
}
