using System;
using System.Collections.Generic;

namespace FDFVANLØSEEKSAMEN.Repository.Models
{
    public partial class Barn
    {
        public Barn()
        {
            Betalings = new HashSet<Betaling>();
        }

        public int BarnId { get; set; }
        public string? Bnavn { get; set; }
        public int? Bgid { get; set; }
        public int? ModtagetLod { get; set; }
        public int? RetuneretLod { get; set; }

        public virtual BørneGruppe? Bg { get; set; }
        public virtual ICollection<Betaling> Betalings { get; set; }
    }
}
