import { Component } from '@angular/core';
import { CreditRequest, CreditResponse } from '../../Models/credit.model';
import { CreditService } from '../../Services/credit.service';

@Component({
  selector: 'app-credit-form',
  templateUrl: './credit-form.component.html',
  styleUrl: './credit-form.component.css'
})
export class CreditFormComponent {

  MontantAchat: number = 0;
  FondsPropes: number = 0;  
  DureeMois: number = 0;
  TauxAnnuel: number = 0;

  // Résultat
  creditResult: CreditResponse | null = null;

  // Pour afficher les erreurs
  errorMessage: string = '';

  constructor(private creditService: CreditService) { }

  onCalculate() {
    this.errorMessage = '';
    const request: CreditRequest = {
      MontantAchat: this.MontantAchat,
      FondsPropes: this.FondsPropes, 
      DureeMois: this.DureeMois,
      TauxAnnuel: this.TauxAnnuel
      
    };

     this.creditService.calculateCredit(request).subscribe({
      next: (res) => this.creditResult = res,
      error: (err) => this.errorMessage = err.error?.Message || 'Erreur serveur'
    });
  }

}