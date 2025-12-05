import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreditRequest, CreditResponse } from '../Models/credit.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CreditService {
  private apiUrl = 'https://localhost:44334/api/credit/calculate';
  
  constructor(private http: HttpClient) { }

  calculateCredit(request: CreditRequest): Observable<CreditResponse> {
    console.log('Sending request to:', this.apiUrl);
    console.log('Request data:', request);
    return this.http.post<CreditResponse>(this.apiUrl, request);
  }
}