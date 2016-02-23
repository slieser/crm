using System.Collections.Generic;
using System.IO;
using equalidator;
using NUnit.Framework;

namespace crm.persistence.tests
{
    [TestFixture]
    public class KundenStoreTests
    {
        private KundenStore sut;

        [SetUp]
        public void Setup() {
            sut = new KundenStore();
        }

        [Test]
        public void Validate_Mapper_Configuratioh() {
            sut.mapperConfiguration.AssertConfigurationIsValid();
        }

        [Test]
        public void Save_and_Load() {
            var kunden = new[] {
                new contracts.KundeDetails {
                    Id = "1", Firma = "F1", Land = "L1", Plz = "Plz1", Ort = "Ort1", Strasse = "Str1", Nr = "nr1",
                    Kontakte = new List<contracts.Kontakt> {
                        new contracts.Kontakt { Nachname="n1", Vorname="v1"},
                        new contracts.Kontakt { Nachname="n2", Vorname="v2"},
                    }
                },
                new contracts.KundeDetails {
                    Id = "2", Firma = "F2", Land = "L2", Plz = "Plz2", Ort = "Ort2", Strasse = "Str2", Nr = "nr2"
                }
            };
            sut.Save(kunden);

            var geladene_Kunden = sut.Load();

            Equalidator.AreEqual(geladene_Kunden, kunden, true);
        }

        [Test]
        public void File_does_not_exist() {
            File.Delete(KundenStore.Dateiname);
            var geladene_Kunden = sut.Load();
            Assert.That(geladene_Kunden, Is.Empty);
        }

        [Test]
        public void Add() {
            File.Delete(KundenStore.Dateiname);
            var kunde1 = new contracts.KundeDetails {
                Id = "1", Firma = "F1", Land = "L1", Plz = "Plz1", Ort = "Ort1", Strasse = "Str1", Nr = "nr1"
            };
            sut.Add(kunde1);

            var geladene_Kunden = sut.Load();
            Equalidator.AreEqual(geladene_Kunden, new[] { kunde1 }, true);

            var kunde2 = new contracts.KundeDetails {
                Id = "2", Firma = "F2", Land = "L2", Plz = "Plz2", Ort = "Ort2", Strasse = "Str2", Nr = "nr2"
            };
            sut.Add(kunde2);

            geladene_Kunden = sut.Load();
            Equalidator.AreEqual(geladene_Kunden, new[] { kunde1, kunde2 }, true);
        }

        [Test]
        public void Update() {
            File.Delete(KundenStore.Dateiname);
            var kunden = new[] {
                new contracts.KundeDetails { Id = "1", Firma = "F1", Land = "L1", Plz = "Plz1", Ort = "Ort1", Strasse = "Str1", Nr = "nr1" },
                new contracts.KundeDetails { Id = "2", Firma = "F2", Land = "L2", Plz = "Plz2", Ort = "Ort2", Strasse = "Str2", Nr = "nr2" },
                new contracts.KundeDetails { Id = "3", Firma = "F3", Land = "L3", Plz = "Plz3", Ort = "Ort3", Strasse = "Str3", Nr = "nr3" }
            };
            sut.Save(kunden);

            var geladener_Kunde = sut.Load("2");
            sut.Update(geladener_Kunde);

            var geladene_Kunden = sut.Load();
            Equalidator.AreEqual(geladene_Kunden, new[] {kunden[0], kunden[2], kunden[1]}, true);
        }

        [Test]
        public void Get_by_Id() {
            File.Delete(KundenStore.Dateiname);
            var kunden = new[] {
                new contracts.KundeDetails { Id = "1", Firma = "F1", Land = "L1", Plz = "Plz1", Ort = "Ort1", Strasse = "Str1", Nr = "nr1" },
                new contracts.KundeDetails { Id = "2", Firma = "F2", Land = "L2", Plz = "Plz2", Ort = "Ort2", Strasse = "Str2", Nr = "nr2" }
            };
            sut.Save(kunden);

            var kunde = sut.Load("2");
            Equalidator.AreEqual(kunden[1], kunde, true);
        }
    }
}