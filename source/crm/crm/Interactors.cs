using System.Collections.Generic;
using crm.logic;
using crm.persistence;

namespace crm
{
    public class Interactors
    {
        private KundenStore kundenStore;
        private readonly State<string> idState = new State<string>();

        public IEnumerable<contracts.KundeDetails> Start() {
            kundenStore = new KundenStore();
            var kunden = kundenStore.Load();
            return kunden;
        }

        public contracts.KundeDetails Kunde_selektieren(string id) {
            idState.Put(id);
            var kundeDetails = kundenStore.Load(id);
            return kundeDetails;
        }

        public contracts.KundeDetails Neuer_Kunde() {
            var neuer_kunde = new contracts.KundeDetails();
            neuer_kunde = Standardwerte.Setze_Standardwerte(neuer_kunde);
            return neuer_kunde;
        }

        public contracts.KundeDetails Neuer_Kontakt() {
            var neuer_kontakt = new contracts.Kontakt();
            neuer_kontakt = Standardwerte.Setze_Standardwerte(neuer_kontakt);
            var id = idState.Get();
            var kundenDetails = kundenStore.Load(id);
            kundenDetails.Kontakte.Insert(0, neuer_kontakt);

            return kundenDetails;
        }

        public IEnumerable<contracts.KundeDetails> Kunde_speichern(contracts.KundeDetails kundeDetails) {
            IdGenerator.Hat_Id(kundeDetails,
                k => {
                    kundenStore.Update(k);
                },
                k => {
                    IdGenerator.Id_hinzufügen(k);
                    kundenStore.Add(k);
                });
            var kunden = kundenStore.Load();
            return kunden;
        }

        public IEnumerable<contracts.KundeDetails> Kontakt_speichern(contracts.Kontakt kontakt) {
            var id = idState.Get();
            var kundenDetails = kundenStore.Load(id);
            kundenDetails.Kontakte.Add(kontakt);
            kundenStore.Update(kundenDetails);
            var kunden = kundenStore.Load();
            return kunden;
        }

        public contracts.KundeDetails Kunde_bearbeiten_abbrechen() {
            var id = idState.Get();
            var kundeDetails = kundenStore.Load(id);
            return kundeDetails;
        }

        public contracts.KundeDetails Kunde_bearbeiten() {
            var id = idState.Get();
            var kundeDetails = kundenStore.Load(id);
            return kundeDetails;
        }
    }
}