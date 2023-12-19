using System;
using System.Collections.Generic;

namespace FDFVANLØSEEKSAMEN.Repository.Models
{
    public partial class BørneGruppe
    {
        public BørneGruppe()
        {
            Barns = new HashSet<Barn>();
        }

        public int Bgid { get; set; }
        public string? Bgnavn { get; set; }
        public int? LederId { get; set; }
        public int? ModtagetLod { get; set; }
        public int? RetuneretLod { get; set; }

        public virtual Bruger? Leder { get; set; }
        public virtual ICollection<Barn> Barns { get; set; }
    }
}
