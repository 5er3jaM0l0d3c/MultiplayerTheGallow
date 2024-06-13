using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TrueLetter
    {
        public int Id { get; set; }
        public int? GameId { get; set; }
        public string? Letter { get; set; }
        public virtual Game? Game { get; set; }

    }
}
