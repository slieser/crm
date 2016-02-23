using crm.contracts;

namespace crm.persistence
{
    public class Kontakt
    {
        public string Nachname { get; set; }

        public string Vorname { get; set; }

        public string Rolle { get; set; }

        public string Email { get; set; }

        public string Telefon { get; set; }

        public string Handy { get; set; }

        public bool Ausgeschieden { get; set; }

        public Anrede Anrede { get; set; }
    }
}