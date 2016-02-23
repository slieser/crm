using System.Windows.Controls;

namespace crm.ui
{
    internal partial class KundenDetailsControl : UserControl
    {
        public KundenDetailsControl() {
            InitializeComponent();
        }

        public void Setze_KundenDetails(contracts.KundeDetails kundeDetails) {
            txtFirma.Text = kundeDetails.Firma;
            txtStrasse.Text = kundeDetails.Strasse;
            txtNr.Text = kundeDetails.Nr;
            txtLand.Text = kundeDetails.Land;
            txtPlz.Text = kundeDetails.Plz;
            txtOrt.Text = kundeDetails.Ort;
            txtTags.Text = kundeDetails.Tags;
        }
    }
}