using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCalculator.Models
{
    public class CreditResult
    {
        // Montants intermédiaires
        public decimal FraisAchat { get; set; }          
        public decimal MontantEmprunteBrut { get; set; }  
        public decimal FraisHypotheque { get; set; }      
        public decimal MontantEmprunteNet { get; set; }   
        
        // Résultats principaux
        public decimal Mensualite { get; set; }          
        public decimal TauxMensuel { get; set; }      
        
        // Tableau d'amortissement
        public List<AmortizationItem> TableauAmortissement { get; set; }
    }
}
