import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ErrorsListComponent }    from './errors-list.component';

const errorsRoutes: Routes = [
  { path: 'errors',  component: ErrorsListComponent },
];

@NgModule({
  imports: [
    RouterModule.forChild(errorsRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class ErrorsRoutingModule { }