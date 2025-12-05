using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCalculator.Models
{
    public class AmortizationItem
    {
        public int Mois { get; set; }              
        public decimal SoldeDebut { get; set; }   
        public decimal Mensualite { get; set; }  
        public decimal Interet { get; set; }     
        public decimal CapitalRembourse { get; set; } 
        public decimal SoldeFin { get; set; }  
    }
}