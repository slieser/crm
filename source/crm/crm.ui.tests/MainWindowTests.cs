using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using crm.contracts;
using NUnit.Framework;

namespace crm.ui.tests
{
    [TestFixture]
    public class MainWindowTests
    {
        private MainWindow sut;

        [SetUp]
        public void Setup() {
            sut = new MainWindow();
        }

        [Test, Apartment(ApartmentState.STA)]
        public void Verify_AutoMapper_Configuration() {
            sut.mapperConfiguration.AssertConfigurationIsValid();
        }

        [Test, Explicit, Apartment(ApartmentState.STA)]
        public void Dialog_mit_Beispieldaten_anzeigen() {
            sut.Alle_Kunden_setzen(Beispieldaten_Kunden());
            sut.Kundendetails_setzen(Beispieldaten_KundenDetails());

            sut.ShowDialog();
        }

        [Test, Apartment(ApartmentState.STA)]
        public void SelectedId_wenn_kein_Eintrag_selektiert_ist() {
            Assert.That(sut.SelectedId(), Is.Null);

            sut.Alle_Kunden_setzen(Beispieldaten_Kunden());
            Assert.That(sut.SelectedId(), Is.Null);

            ((TreeViewItem)sut.tvKunden.Items[2]).IsSelected = true;
            Assert.That(sut.SelectedId(), Is.EqualTo("3"));

            ((TreeViewItem)sut.tvKunden.Items[2]).Tag = "42";
            Assert.That(sut.SelectedId(), Is.EqualTo("42"));

            ((TreeViewItem)sut.tvKunden.Items[2]).Tag = null;
            Assert.That(sut.SelectedId(), Is.Null);
        }

        [Test, Apartment(ApartmentState.STA)]
        public void Selektion_bleibt_erhalten() {
            sut.Alle_Kunden_setzen(Beispieldaten_Kunden());

            ((TreeViewItem)sut.tvKunden.Items[2]).IsSelected = true;
            Assert.That(sut.SelectedId(), Is.EqualTo("3"));

            sut.Alle_Kunden_setzen(Beispieldaten_Kunden());
            Assert.That(sut.SelectedId(), Is.EqualTo("3"));
        }

        private static contracts.KundeDetails[] Beispieldaten_Kunden() {
            return new[] {
                new contracts.KundeDetails { Firma = "Albern AG", Id = "1" },
                new contracts.KundeDetails { Firma = "Blöd AG", Id = "2" },
                new contracts.KundeDetails { Firma = "Chips AG", Id = "3" },
                new contracts.KundeDetails { Firma = "Doof AG", Id = "4" },
                new contracts.KundeDetails { Firma = "Elite AG", Id = "5" },
            };
        }

        private static Tag[] Beispieldaten_Tags() {
            return new[] {
                new Tag { Bezeichnung = "CCD-School" },
                new Tag { Bezeichnung = "SIGS-DATACOM" },
                new Tag { Bezeichnung = "Eigener Kunde" },
                new Tag { Bezeichnung = ".NET" },
                new Tag { Bezeichnung = "Java" },
                new Tag { Bezeichnung = "Ruby on Rails" }
            };
        }

        private static contracts.KundeDetails Beispieldaten_KundenDetails() {
            return new contracts.KundeDetails {
                Id = "42",
                Firma = "Eine kleine AG",
                Strasse = "Am Hand",
                Nr = "42a",
                Land = "D",
                Plz = "54321",
                Ort = "Stadthausen",
                Tags = ".NET, Java, CCD-School",
                Kontakte = new List<contracts.Kontakt>(new[] {
                    new contracts.Kontakt { Vorname = "Peter", Nachname = "Pan", Ausgeschieden = true, Anrede = Anrede.Herr },
                    new contracts.Kontakt { Vorname = "Maria", Nachname = "Heilig", Anrede = Anrede.Frau }
                })
            };
        }
    }
}