using System;
using System.Windows;
using crm.ui;

namespace crm
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args) {
            // Build
            var mainWindow = new MainWindow();
            var interactors = new Interactors();

            // Bind
            mainWindow.Neuer_Kunde += () => {
                var kunde = interactors.Neuer_Kunde();
                mainWindow.Kunde_zur_Bearbeitung_setzen(kunde);
            };
            mainWindow.Kunde_speichern += kunde => {
                var alle_kunden = interactors.Kunde_speichern(kunde);
                mainWindow.Alle_Kunden_setzen(alle_kunden);
            };
            mainWindow.Kunde_bearbeiten_abbrechen += () => {
                var kunde = interactors.Kunde_bearbeiten_abbrechen();
                mainWindow.Kundendetails_setzen(kunde);
            };
            mainWindow.Kunde_bearbeiten += () => {
                var kunde = interactors.Kunde_bearbeiten();
                mainWindow.Kunde_zur_Bearbeitung_setzen(kunde);
            };

            mainWindow.Neuer_Kontakt += () => {
                var kunde = interactors.Neuer_Kontakt();
                mainWindow.Kontakt_zur_Bearbeitung_setzen(kunde);
            };
            mainWindow.Kontakt_speichern += kontakt => {
                var alle_kunden = interactors.Kontakt_speichern(kontakt);
                mainWindow.Alle_Kunden_setzen(alle_kunden);
            };
            mainWindow.Kontakt_bearbeiten_abbrechen += () => {
                var kunde = interactors.Kunde_bearbeiten_abbrechen();
                mainWindow.Kundendetails_setzen(kunde);
            };

            mainWindow.Selektion_geändert += id => {
                var kunde = interactors.Kunde_selektieren(id);
                mainWindow.Kundendetails_setzen(kunde);
            };

            // Start
            var kunden = interactors.Start();
            mainWindow.Alle_Kunden_setzen(kunden);

            // Run
            var app = new Application { MainWindow = mainWindow };
            app.Run(mainWindow);
        }
    }
}