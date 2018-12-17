import { NgModule }       from '@angular/core';
import { CommonModule }   from '@angular/common';
import { FormsModule }    from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { ErrorsListComponent } from './errors-list.component';

import { ErrorsRoutingModule } from './errors-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ErrorsRoutingModule,
    NgbModule
  ],
  declarations: [
    ErrorsListComponent,
  ]
})
export class ErrorsModule {}