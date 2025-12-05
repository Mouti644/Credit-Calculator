using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCalculator.Models
{
    public class CreditRequest
    {
        public decimal MontantAchat { get; set; }      // Input: Montant d'achat
        public decimal FondsPropes { get; set; }       // Input: Fonds propres (apport)
        public int DureeMois { get; set; }             // Input: Durée du crédit en mois
        public decimal TauxAnnuel { get; set; }        // Input: Taux annuel en %
    }
}
