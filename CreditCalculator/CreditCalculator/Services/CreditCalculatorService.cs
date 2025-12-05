using CreditCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCalculator.Services
{
    public class CreditCalculatorService
    {
        public CreditResult Calculer(CreditRequest req)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));
            if (req.DureeMois <= 0) throw new ArgumentException("La durée doit être > 0");
            if (req.MontantAchat <= 0) throw new ArgumentException("Le montant d'achat doit être > 0");

            // ========== ÉTAPE 1: Calcul des frais d'achat ==========
            // Frais = 10% du montant d'achat
            decimal fraisAchat = req.MontantAchat * 0.10m;
            fraisAchat = Math.Round(fraisAchat, 2, MidpointRounding.AwayFromZero);

            // ========== ÉTAPE 2: Montant à emprunter BRUT ==========
            // Formule: MontantAchat + FraisAchat - FondsPropes
            decimal montantEmprunteBrut = req.MontantAchat + fraisAchat - req.FondsPropes;
            if (montantEmprunteBrut < 0) montantEmprunteBrut = 0;
            montantEmprunteBrut = Math.Round(montantEmprunteBrut, 2, MidpointRounding.AwayFromZero);

            // ========== ÉTAPE 3: Frais d'hypothèque ==========
            // Formule: MontantEmprunteBrut * 0.02 (2%)
            decimal fraisHypotheque = montantEmprunteBrut * 0.02m;
            fraisHypotheque = Math.Round(fraisHypotheque, 2, MidpointRounding.AwayFromZero);

            // ========== ÉTAPE 4: Montant à emprunter NET ==========
            // Formule: MontantEmprunteBrut + FraisHypotheque
            decimal montantEmprunteNet = montantEmprunteBrut + fraisHypotheque;
            montantEmprunteNet = Math.Round(montantEmprunteNet, 2, MidpointRounding.AwayFromZero);

            // ========== ÉTAPE 5: Calcul du taux mensuel ==========
            // Formule: (1 + taux annuel)^(1/12) - 1
            // Résultat arrondi à 3 décimales en %
            decimal tauxAnnuelDecimal = req.TauxAnnuel / 100m;
            decimal tauxMensuelDecimal = (decimal)Math.Pow((double)(1 + tauxAnnuelDecimal), 1.0 / 12.0) - 1;
            decimal tauxMensuelEnPourcent = Math.Round(tauxMensuelDecimal * 100, 3, MidpointRounding.AwayFromZero);
            // Pour les calculs internes, garder plus de précision
            tauxMensuelDecimal = Math.Round(tauxMensuelDecimal, 5, MidpointRounding.AwayFromZero);

            // ========== ÉTAPE 6: Calcul de la mensualité constante ==========
            // Formule: Capital * TauxMensuel * (1 + TauxMensuel)^Durée / ((1 + TauxMensuel)^Durée - 1)
            // Résultat arrondi à 2 décimales
            decimal mensualite = 0;
            if (tauxMensuelDecimal == 0)
            {
                // Cas taux = 0
                mensualite = montantEmprunteNet / req.DureeMois;
            }
            else
            {
                double tauxDouble = (double)tauxMensuelDecimal;
                double dureeDouble = (double)req.DureeMois;
                double numerateur = (double)montantEmprunteNet * tauxDouble * Math.Pow(1 + tauxDouble, dureeDouble);
                double denominateur = Math.Pow(1 + tauxDouble, dureeDouble) - 1;
                mensualite = (decimal)(numerateur / denominateur);
            }
            mensualite = Math.Round(mensualite, 2, MidpointRounding.AwayFromZero);

            // ========== ÉTAPE 7: Génération du tableau d'amortissement ==========
            // Nombre de lignes = durée en mois
            List<AmortizationItem> tableau = new List<AmortizationItem>();
            decimal soldeDebut = montantEmprunteNet;

            for (int mois = 1; mois <= req.DureeMois; mois++)
            {
                // Intérêt = SoldeDebut * TauxMensuel (arrondi à 2 décimales)
                decimal interet = Math.Round(soldeDebut * tauxMensuelDecimal, 2, MidpointRounding.AwayFromZero);
                
                // CapitalRembourse = Mensualité - Intérêt
                decimal capitalRembourse = Math.Round(mensualite - interet, 2, MidpointRounding.AwayFromZero);

                // Ajustement pour le dernier mois pour éliminer les erreurs d'arrondi
                if (mois == req.DureeMois)
                {
                    capitalRembourse = soldeDebut;
                    // La mensualité du dernier mois = intérêt + solde restant
                    decimal mensualiteDernierMois = Math.Round(interet + capitalRembourse, 2, MidpointRounding.AwayFromZero);
                    tableau.Add(new AmortizationItem
                    {
                        Mois = mois,
                        SoldeDebut = soldeDebut,
                        Mensualite = mensualiteDernierMois,
                        Interet = interet,
                        CapitalRembourse = capitalRembourse,
                        SoldeFin = 0
                    });
                    break;
                }

                // SoldeFin = SoldeDebut - CapitalRembourse
                decimal soldeFin = Math.Round(soldeDebut - capitalRembourse, 2, MidpointRounding.AwayFromZero);
                if (soldeFin < 0) soldeFin = 0;

                tableau.Add(new AmortizationItem
                {
                    Mois = mois,
                    SoldeDebut = soldeDebut,
                    Mensualite = mensualite,
                    Interet = interet,
                    CapitalRembourse = capitalRembourse,
                    SoldeFin = soldeFin
                });

                soldeDebut = soldeFin;
            }

            return new CreditResult
            {
                FraisAchat = fraisAchat,
                MontantEmprunteBrut = montantEmprunteBrut,
                FraisHypotheque = fraisHypotheque,
                MontantEmprunteNet = montantEmprunteNet,
                Mensualite = mensualite,
                TauxMensuel = tauxMensuelEnPourcent,
                TableauAmortissement = tableau
            };
        }
    }
}