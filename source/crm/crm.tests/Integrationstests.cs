using System.IO;
using System.Linq;
using crm.persistence;
using equalidator;
using NUnit.Framework;

namespace crm.tests
{
    [TestFixture]
    public class Integrationstests
    {
        private Interactors sut;

        [SetUp]
        public void Setup() {
            sut = new Interactors();
        }

        [Test]
        public void Start() {
            File.Delete(KundenStore.Dateiname);
            var kunden = sut.Start();
            Assert.That(kunden, Is.Empty);

            var neuer_kunde = sut.Neuer_Kunde();
            Assert.That(neuer_kunde.Land, Is.EqualTo("D"));

            neuer_kunde.Firma = "Neue Firma GmbH";
            neuer_kunde.Strasse = "Am Hang";
            neuer_kunde.Nr = "5";
            neuer_kunde.Plz = "12345";
            neuer_kunde.Ort = "Örtchen";
            Assert.That(neuer_kunde.Id, Is.Null.Or.Empty);
            sut.Kunde_speichern(neuer_kunde);
            var id = neuer_kunde.Id;
            Assert.That(id, Is.Not.Null.Or.Empty);

            sut.Kunde_selektieren(id);
            var kunde1 = sut.Kunde_bearbeiten_abbrechen();
            Equalidator.AreEqual(neuer_kunde, kunde1, true);

            var kundeDetails = sut.Neuer_Kontakt();
            kundeDetails.Kontakte.First().Vorname = "Stefan";
            kundeDetails.Kontakte.First().Nachname = "Lieser";
            sut.Kontakt_speichern(kundeDetails.Kontakte.First());

            kundeDetails.Firma = "Geändert";
            sut.Kunde_speichern(kundeDetails);
            sut.Kunde_selektieren(id);
            var kunde2 = sut.Kunde_bearbeiten_abbrechen();
            Equalidator.AreEqual(kundeDetails, kunde2, true);
        }
    }
}