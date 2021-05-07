import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { EditOrder } from '../order/order.component';
import { Order } from '../order/OrderModel';
import { PageResult } from '../PageResult';
import { Sort } from '@angular/material/sort';
@Component({
  selector: 'app-pending-order',
  templateUrl: './pending-order.component.html',
  styleUrls: ['./pending-order.component.css']
})
export class PendingOrderComponent implements OnInit {

  order: Order[];
  public pageNumber: number = 1;
  public Count: number;
  public idOrder: EditOrder = new EditOrder();
  constructor(private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http.get<PageResult<Order>>('api/order/get-pending-order').subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  loadOrder()
  {
    this.http.get<PageResult<Order>>('api/order/get-pending-order').subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  public onPageChange = (pageNumber) => {
    this.http.get<PageResult<Order>>('api/order/get-pending-order?page=' + pageNumber).subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  sortData(sort: Sort) {
    if (sort.direction === 'asc') {
      switch (sort.active) {
        
        case 'price': 
        let params = new HttpParams();
        params = params.append('sortParam', 'asc');
        this.http.get<PageResult<Order>>('api/order/sort-price', {params: params}).subscribe(result => {
          this.order = result.items;
          this.pageNumber = result.pageIndex;
          this.Count = result.count;
        }, error => console.error(error)); ;
        return;

        case 'date': 
        let params1 = new HttpParams();
        params1 = params1.append('sortParam', 'asc');
        this.http.get<PageResult<Order>>('api/order/sort-date', {params: params1}).subscribe(result => {
          this.order = result.items;
          this.pageNumber = result.pageIndex;
          this.Count = result.count;
        }, error => console.error(error)); ;
        return;
      }
    }
    else if(sort.direction === 'desc')
    {
      switch (sort.active) {
        case 'price': 
        let params = new HttpParams();
        params = params.append('sortParam', 'desc');
        this.http.get<PageResult<Order>>('api/order/sort-price', {params: params}).subscribe(result => {
          this.order = result.items;
          this.pageNumber = result.pageIndex;
          this.Count = result.count;
        }, error => console.error(error)); ;
        return;

        case 'date': 
        let params1 = new HttpParams();
        params1 = params1.append('sortParam', 'desc');
        this.http.get<PageResult<Order>>('api/order/sort-date', {params: params1}).subscribe(result => {
          this.order = result.items;
          this.pageNumber = result.pageIndex;
          this.Count = result.count;
        }, error => console.error(error)); ;
        return;
      }
    }
   
   
  }
  edit_order(id: number)
  {
    this.router.navigate(['/edit-order'], {queryParams: {id: id}} );
  }

  open_create()
  {
    this.router.navigate(['/add-pending-order']);
  }
  open_completed_order()
  {
    this.router.navigate(['/completed-order']);
  }
  open_canceled_order()
  {
    this.router.navigate(['/canceled-order']);
  }
  completed_order(id: number)
  {
    this.idOrder.id = id;
    this.http.post('api/order/completed-order', this.idOrder).subscribe(result => {this.loadOrder()});
  }
  canceled_order(id: number)
  {
    this.idOrder.id = id;
    this.http.post('api/order/canceled-order', this.idOrder).subscribe(result => {this.loadOrder()});
  }
  search(form: NgForm)
  {
    let params = new HttpParams();
    params = params.append('text', form.value.textSearch);
    this.http.get<PageResult<Order>>('api/order/get-search', {params: params}).subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  search_date(form: NgForm)
  {
    let params = new HttpParams();
    params = params.append('dateSearch', form.value.dateSearch);
    this.http.get<PageResult<Order>>('api/order/get-search-date', {params: params}).subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
}
