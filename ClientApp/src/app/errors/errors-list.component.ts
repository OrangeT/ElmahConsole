import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ElmahError, ElmahErrorList } from './errors.interface';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-error-list',
    templateUrl: './errors-list.component.html'
})

export class ErrorsListComponent {

    public selectedError: ElmahError;
    public errorList: ElmahErrorList;
    public pages: number[];

    public pageLimit = 5; // PAGINATION LIMIT, TOP AND TAIL

    constructor(private http: HttpClient,
        @Inject('BASE_URL') baseUrl: string,
        private route: ActivatedRoute,
        private router: Router) {

        this.route.params.subscribe(params => {

            http.get<ElmahErrorList>(baseUrl + 'api/Elmah/Errors', { params: { page: (params || {}).page }}).subscribe(result => {
                this.errorList = result;

                // Calculate pages to display.  If more than 3 * pageLimit, then display
                // first limit, last limit and limit *around* the selected page.

                // if (result.pages <= this.pageLimit * 3) {
                //     this.pages = Array(result.pages).fill(0).map((x, i) => x + i); // More complex later.
                // } else {
                //     var rawPages =
                //     // First five
                //     [// ...Array(this.pageLimit).fill(0).map((x, i) => x + i),
                //     // Middle five
                //     ...Array(this.pageLimit).fill(result.currentPage - Math.ceil(this.pageLimit / 2)).map((x, i) => x + i),
                //     // End five
                //     ...Array(this.pageLimit).fill(result.total - this.pageLimit).map((x, i) => x + i)];

                //     // Reduce duplicates
                //     var uniquePages = Array.from(new Set(rawPages)); //Can't use spread here in TS
                //     this.pages = uniquePages;
                // }

            }, error => console.error(error));
        });
    }

     changePage(page: number): void {

        // Change to the defined page number
         this.router.navigate(['errors', { page: page }]);
     };

     onSelect(error: ElmahError): void {
         this.selectedError = error;
     };

     back(): void {
         this.selectedError = null;
     };
}
