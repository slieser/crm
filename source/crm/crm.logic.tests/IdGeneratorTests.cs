using crm.contracts;
using NUnit.Framework;

namespace crm.logic.tests
{
    [TestFixture]
    public class IdGeneratorTests
    {
        [Test]
        public void Id_is_assigned() {
            var kunde = new KundeDetails();
            Assert.That(kunde.Id, Is.Null.Or.Empty);

            kunde = IdGenerator.Id_hinzufügen(kunde);
            Assert.That(kunde.Id.Length, Is.GreaterThan(8));
        }

        [Test]
        public void Empty_means_no_Id() {
            var onJa = false;
            var onNein = false;
            IdGenerator.Hat_Id(new KundeDetails(),
                k => onJa = true,
                k => onNein = true);
            Assert.That(onNein, Is.True);
            Assert.That(onJa, Is.False);
        }

        [Test]
        public void Not_empty_means_it_has_an_Id() {
            var onJa = false;
            var onNein = false;
            IdGenerator.Hat_Id(IdGenerator.Id_hinzufügen(new KundeDetails()),
                k => onJa = true,
                k => onNein = true);
            Assert.That(onNein, Is.False);
            Assert.That(onJa, Is.True);
        }
    }
}