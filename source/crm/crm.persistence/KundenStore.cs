using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;

namespace crm.persistence
{
    public class KundenStore
    {
        private readonly IMapper mapper;
        internal readonly MapperConfiguration mapperConfiguration;
        internal const string Dateiname = "kunden.json";

        public KundenStore() {
            mapperConfiguration = new MapperConfiguration(
                cfg => {
                    cfg.CreateMap<contracts.KundeDetails, KundeDetails>();
                    cfg.CreateMap<contracts.Kontakt, Kontakt>();
                    cfg.CreateMap<KundeDetails, contracts.KundeDetails>();
                    cfg.CreateMap<Kontakt, contracts.Kontakt>();
                });
            mapper = mapperConfiguration.CreateMapper();
        }

        public IEnumerable<contracts.KundeDetails> Load() {
            if (!File.Exists(Dateiname)) {
                return new contracts.KundeDetails[] { };
            }

            var json = File.ReadAllText(Dateiname);
            var kundenDm = JsonConvert.DeserializeObject<IEnumerable<KundeDetails>>(json);
            var kunden = Map(kundenDm);
            return kunden;
        }

        public contracts.KundeDetails Load(string id) {
            var kunden = Load();
            var kunde = kunden.First(k => k.Id == id);
            return kunde;
        }

        public void Add(contracts.KundeDetails kundeDetails) {
            var kunden = Load().ToList();
            kunden.Add(kundeDetails);
            Save(kunden);
        }

        public void Update(contracts.KundeDetails kundeDetails) {
            var kunden = Load().ToList();
            kunden.RemoveAll(k => k.Id == kundeDetails.Id);
            kunden.Add(kundeDetails);
            Save(kunden);
        }

        internal void Save(IEnumerable<contracts.KundeDetails> kunden) {
            var kundenDm = Map(kunden);
            var json = JsonConvert.SerializeObject(kundenDm);
            File.WriteAllText(Dateiname, json);
        }

        private IEnumerable<contracts.KundeDetails> Map(IEnumerable<KundeDetails> kundenDm) {
            return mapper.Map<IEnumerable<KundeDetails>, IEnumerable<contracts.KundeDetails>>(kundenDm);
        }

        private IEnumerable<KundeDetails> Map(IEnumerable<contracts.KundeDetails> kunden) {
            return kunden.Select(Map);
        }

        private KundeDetails Map(contracts.KundeDetails kundeDetails) {
            return mapper.Map<contracts.KundeDetails, KundeDetails>(kundeDetails);
        }
    }
}