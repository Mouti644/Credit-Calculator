import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { CreditFormComponent } from './Components/credit-form/credit-form.component';
import { CreditService } from './Services/credit.service';

@NgModule({
  declarations: [
    AppComponent,
    CreditFormComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [CreditService],
  bootstrap: [AppComponent]
})
export class AppModule { }
