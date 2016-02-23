using crm.contracts;

namespace crm.logic
{
    public static class Standardwerte
    {
        public static KundeDetails Setze_Standardwerte(KundeDetails kunde) {
            kunde.Land = "D";
            return kunde;
        }

        public static Kontakt Setze_Standardwerte(Kontakt kontakt) {
            kontakt.Anrede = Anrede.Herr;
            return kontakt;
        }
    }
}