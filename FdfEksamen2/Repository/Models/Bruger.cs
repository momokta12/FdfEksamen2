using System;
using System.Collections.Generic;

namespace FDFVANLØSEEKSAMEN.Repository.Models
{
    public partial class Bruger
    {
        public Bruger()
        {
            BørneGruppes = new HashSet<BørneGruppe>();
        }

        public int BrugerId { get; set; }
        public string? Brugernavn { get; set; }
        public string? Kodeord { get; set; }
        public string? Rolle { get; set; }

        public virtual ICollection<BørneGruppe> BørneGruppes { get; set; }
    }
}
