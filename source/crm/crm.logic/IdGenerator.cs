using System;
using crm.contracts;

namespace crm.logic
{
    public static class IdGenerator
    {
        public static KundeDetails Id_hinzufügen(KundeDetails kunde) {
            kunde.Id = GenerateId();
            return kunde;
        }

        internal static string GenerateId() {
            return Guid.NewGuid().ToString();
        }

        public static void Hat_Id(KundeDetails kundeDetails, Action<KundeDetails> onJa, Action<KundeDetails> onNein) {
            if (string.IsNullOrWhiteSpace(kundeDetails.Id)) {
                onNein(kundeDetails);
            }
            else {
                onJa(kundeDetails);
            }
        }
    }
}