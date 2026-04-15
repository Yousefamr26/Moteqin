using Moteqin.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteqin.Domain.Entity
{
    public class Ayah : BaseEntity
    {
        public int SurahId { get; set; }
        public int AyahNumber { get; set; }
        public string Text { get; set; }

        public Surah Surah { get; set; }
    }
}
