using System.Collections.Generic;

namespace crm.ui
{
    internal class Kunde
    {
        public string Name { get; set; }

        public IEnumerable<Kontakt> Kontakte { get; set; } 
    }
}