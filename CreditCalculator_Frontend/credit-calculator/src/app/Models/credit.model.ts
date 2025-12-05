export interface CreditRequest {
  MontantAchat: number;
  FondsPropes: number; 
  DureeMois: number;
  TauxAnnuel: number;
  
}

export interface Amortissement {
  Mois: number;
  SoldeDebut: number;
  Mensualite: number;
  Interet: number;
  CapitalRembourse: number;
  SoldeFin: number;
}

export interface CreditResponse {
  // 
  FraisAchat: number;
  MontantEmprunteBrut: number;
  FraisHypotheque: number;
  MontantEmprunteNet: number;
  
  //
  Mensualite: number;
  TauxMensuel: number;
  TableauAmortissement: Amortissement[];
}