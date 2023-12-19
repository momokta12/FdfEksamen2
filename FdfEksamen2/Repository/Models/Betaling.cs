using System;
using System.Collections.Generic;

namespace FDFVANLØSEEKSAMEN.Repository.Models
{
    public partial class Betaling
    {
        public int BetalingsId { get; set; }
        public DateTime? BetalingsDato { get; set; }
        public int? Beløbet { get; set; }
        public int? AntalLod { get; set; }
        public int? BarnId { get; set; }

        public virtual Barn? Barn { get; set; }
    }
}
