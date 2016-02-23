using System.Collections.ObjectModel;
using PropertyChanged;

namespace crm.ui
{
    [ImplementPropertyChanged]
    internal class KundeDetails
    {
        public string Id { get; set; }
        public string Firma { get; set; }
        public string Strasse { get; set; }
        public string Nr { get; set; }
        public string Land { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Tags { get; set; }

        public ObservableCollection<Kontakt> Kontakte { get; set; }
    }
}