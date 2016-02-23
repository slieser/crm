using crm.contracts;
using NUnit.Framework;

namespace crm.logic.tests
{
    [TestFixture]
    public class StandardwerteTests
    {
        [Test]
        public void Land_ist_D() {
            var kunde = Standardwerte.Setze_Standardwerte(new KundeDetails());
            Assert.That(kunde.Land, Is.EqualTo("D"));
        }

        [Test]
        public void Anrede_ist_Herr() {
            var kontakt = Standardwerte.Setze_Standardwerte(new Kontakt());
            Assert.That(kontakt.Anrede, Is.EqualTo(Anrede.Herr));
        }
    }
}