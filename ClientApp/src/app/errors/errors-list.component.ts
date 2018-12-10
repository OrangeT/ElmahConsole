import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-error-list',
    templateUrl: './errors-list.component.html'
})

export class ErrorsListComponent {
     public errors: ElmahError[];

     constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
         http.get<ElmahError[]>(baseUrl + 'api/Elmah/Errors').subscribe(result => {
             this.errors = result;
         }, error => console.error(error));
     }

     selectedError: ElmahError;

     onSelect(error: ElmahError): void {
         this.selectedError = error;
     };

     back(): void {
         this.selectedError = null;
     };
}

interface ElmahError {

    errorId: string;
    application: string;
    host: string;
    type: string;
    source: string;
    message: string;
    user: string;
    statusCode: number;
    timeUtc: Date;
    allXml: string;
}