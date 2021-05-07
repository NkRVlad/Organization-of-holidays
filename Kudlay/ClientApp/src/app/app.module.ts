import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AuthenticationComponent } from './authentication/authentication.component';
import { MainAdminComponent } from './main-admin/main-admin.component';
import { EditOrder, OrderComponent } from './order/order.component';
import { StatusComponent } from './status/status.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { AddStatusComponent } from './add-status/add-status.component';
import { CategoryComponent } from './category/category.component';
import { AddCategoryComponent } from './add-category/add-category.component';
import { PendingOrderComponent } from './pending-order/pending-order.component';
import { AddPendingComponent } from './add-pending/add-pending.component';
import { ReactiveFormsModule }   from '@angular/forms';
import { CompletedOrdersComponent } from './completed-orders/completed-orders.component';
import { CanceledOrdersComponent } from './canceled-orders/canceled-orders.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSortModule } from '@angular/material/sort';
import { EditOrderComponent } from './edit-order/edit-order.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    AuthenticationComponent,
    MainAdminComponent,
    OrderComponent,
    StatusComponent,
    AddStatusComponent,
    CategoryComponent,
    AddCategoryComponent,
    PendingOrderComponent,
    AddPendingComponent,
    CompletedOrdersComponent,
    CanceledOrdersComponent,
    EditOrderComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgxPaginationModule,
    ReactiveFormsModule,
    MatSortModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login-admin', component: AuthenticationComponent },
      { path: 'admin-panel', component: MainAdminComponent },
      { path: 'order', component: OrderComponent },
      { path: 'status', component: StatusComponent },
      { path: 'add-status', component: AddStatusComponent },
      { path: 'category', component: CategoryComponent },
      { path: 'add-category', component: AddCategoryComponent },
      { path: 'pending-order', component: PendingOrderComponent },
      { path: 'add-pending-order', component: AddPendingComponent },
      { path: 'completed-order', component: CompletedOrdersComponent },
      { path: 'canceled-order', component: CanceledOrdersComponent },
      { path: 'edit-order', component: EditOrderComponent }
    ]),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
