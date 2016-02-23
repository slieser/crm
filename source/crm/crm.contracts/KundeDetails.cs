using System.Collections.Generic;

namespace crm.contracts
{
    public class KundeDetails
    {
        public KundeDetails() {
            Kontakte = new List<Kontakt>();
        }

        public string Id { get; set; }
        public string Firma { get; set; }
        public string Strasse { get; set; }
        public string Nr { get; set; }
        public string Land { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Tags { get; set; }

        public IList<Kontakt> Kontakte { get; set; }
    }
}