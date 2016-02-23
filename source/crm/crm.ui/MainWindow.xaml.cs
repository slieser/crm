using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using crm.contracts;

namespace crm.ui
{
    public partial class MainWindow : Window
    {
        private readonly IMapper mapper;
        internal readonly MapperConfiguration mapperConfiguration;

        public MainWindow() {
            InitializeComponent();

            mapperConfiguration = new MapperConfiguration(
                cfg => {
                    cfg.CreateMap<contracts.KundeDetails, KundeDetails>();
                    cfg.CreateMap<contracts.Kontakt, Kontakt>()
                        .ForMember(dest => dest.AnredeIstFrau, opt =>
                            opt.MapFrom(src => src.Anrede == Anrede.Frau))
                        .ForMember(dest => dest.AnredeIstHerr, opt =>
                            opt.MapFrom(src => src.Anrede == Anrede.Herr));
                    cfg.CreateMap<KundeDetails, contracts.KundeDetails>();
                    cfg.CreateMap<Kontakt, contracts.Kontakt>()
                        .ForMember(dest => dest.Anrede, opt =>
                            opt.MapFrom(src => src.AnredeIstFrau ? Anrede.Frau : Anrede.Herr));
                });
            mapper = mapperConfiguration.CreateMapper();

            CollapseEditButtons();

            btnNeuerKunde.Click += (s, e) => Neuer_Kunde?.Invoke();
            btnAbbrechenKunde.Click += (s, e) => Kunde_bearbeiten_abbrechen?.Invoke();
            btnSpeichernKunde.Click += (s, e) => Kunde_speichern?.Invoke(
                Map((KundeDetails)DataContext));
            btnAbbrechenKontakt.Click += (s, e) => Kontakt_bearbeiten_abbrechen?.Invoke();
            btnSpeichernKontakt.Click += (s, e) => Kontakt_speichern?.Invoke(
                Map((KundeDetails)DataContext).Kontakte.First());
            btnNeuerKontakt.Click += (s, e) => { Neuer_Kontakt?.Invoke(); };
            btnBearbeiten.Click += (s, e) => Kunde_bearbeiten?.Invoke();
            tvKunden.SelectedItemChanged += (s, e) => {
                var id = SelectedId();
                if (id == null) {
                    return;
                }
                Selektion_geändert?.Invoke(id);
            };
        }

        internal string SelectedId() {
            return ((TreeViewItem)tvKunden.SelectedItem)?.Tag?.ToString();
        }

        private void CollapseEditButtons() {
            btnAbbrechenKunde.Visibility = Visibility.Collapsed;
            btnSpeichernKunde.Visibility = Visibility.Collapsed;
            btnAbbrechenKontakt.Visibility = Visibility.Collapsed;
            btnSpeichernKontakt.Visibility = Visibility.Collapsed;
            btnNeuerKunde.Visibility = Visibility.Visible;
            btnNeuerKontakt.Visibility = Visibility.Visible;
            btnBearbeiten.Visibility = Visibility.Visible;
        }

        private void ShowEditButtons_for_Kunde() {
            btnAbbrechenKunde.Visibility = Visibility.Visible;
            btnSpeichernKunde.Visibility = Visibility.Visible;
            btnAbbrechenKontakt.Visibility = Visibility.Collapsed;
            btnSpeichernKontakt.Visibility = Visibility.Collapsed;
            btnNeuerKunde.Visibility = Visibility.Collapsed;
            btnNeuerKontakt.Visibility = Visibility.Collapsed;
            btnBearbeiten.Visibility = Visibility.Collapsed;
        }

        private void ShowEditButtons_for_Kontakt() {
            btnAbbrechenKunde.Visibility = Visibility.Collapsed;
            btnSpeichernKunde.Visibility = Visibility.Collapsed;
            btnAbbrechenKontakt.Visibility = Visibility.Visible;
            btnSpeichernKontakt.Visibility = Visibility.Visible;
            btnNeuerKunde.Visibility = Visibility.Collapsed;
            btnNeuerKontakt.Visibility = Visibility.Collapsed;
            btnBearbeiten.Visibility = Visibility.Collapsed;
        }

        public void Alle_Kunden_setzen(IEnumerable<contracts.KundeDetails> kunden) {
            var selectedId = SelectedId();

            tvKunden.Items.Clear();
            foreach (var kunde in kunden) {
                var treeViewItem = new TreeViewItem { Header = kunde.Firma, Tag = kunde.Id };
                tvKunden.Items.Add(treeViewItem);
                if (selectedId == kunde.Id) {
                    treeViewItem.IsSelected = true;
                }
                foreach (var kontakt in kunde.Kontakte) {
                    var treeViewSubItem = new TreeViewItem { Header = $"{kontakt.Nachname}, {kontakt.Vorname}" };
                    treeViewItem.Items.Add(treeViewSubItem);
                }
            }
        }

        public void Setze_Tags(IEnumerable<contracts.Tag> tags) {
            tvTags.Items.Clear();

            foreach (var tag in tags) {
                var treeViewItem = new TreeViewItem { Header = tag.Bezeichnung };
                tvTags.Items.Add(treeViewItem);
            }
        }

        public void Kundendetails_setzen(contracts.KundeDetails kundeDetails) {
            CollapseEditButtons();
            DataContext = Map(kundeDetails);
        }

        private KundeDetails Map(contracts.KundeDetails kundeDetails) {
            var result = mapper.Map<contracts.KundeDetails, KundeDetails>(kundeDetails);
            return result;
        }
        private contracts.KundeDetails Map(KundeDetails kundeDetails) {
            var result = mapper.Map<KundeDetails, contracts.KundeDetails>(kundeDetails);
            return result;
        }

        public event Action Neuer_Kunde;

        public event Action Neuer_Kontakt;

        public event Action Kunde_bearbeiten;

        public event Action<contracts.KundeDetails> Kunde_speichern;

        public event Action Kunde_bearbeiten_abbrechen;

        public event Action<contracts.Kontakt> Kontakt_speichern;

        public event Action Kontakt_bearbeiten_abbrechen;

        public event Action<string> Selektion_geändert;

        public void Kunde_zur_Bearbeitung_setzen(contracts.KundeDetails kundeDetails) {
            Kundendetails_setzen(kundeDetails);
            ShowEditButtons_for_Kunde();
        }

        public void Kontakt_zur_Bearbeitung_setzen(contracts.KundeDetails kundeDetails) {
            Kundendetails_setzen(kundeDetails);
            ShowEditButtons_for_Kontakt();
        }
    }
}