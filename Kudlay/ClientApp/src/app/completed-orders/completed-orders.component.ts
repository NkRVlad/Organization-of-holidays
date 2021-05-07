import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { Order } from '../order/OrderModel';
import { PageResult } from '../PageResult';

@Component({
  selector: 'app-completed-orders',
  templateUrl: './completed-orders.component.html',
  styleUrls: ['./completed-orders.component.css']
})
export class CompletedOrdersComponent implements OnInit {
  
  order: Order[];
  public pageNumber: number = 1;
  public Count: number;

  constructor(private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http.get<PageResult<Order>>('api/order/get-completed-order').subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  

  ngOnInit() {
  }
  loadCompletedOrder()
  {
    this.http.get<PageResult<Order>>('api/order/get-completed-order').subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  public onPageChange = (pageNumber) => {
    this.http.get<PageResult<Order>>('api/order/get-completed-order?page=' + pageNumber).subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  search(form: NgForm)
  {
    let params = new HttpParams();
    params = params.append('text', form.value.textSearch);
    this.http.get<PageResult<Order>>('api/order/get-search-orders-completed', {params: params}).subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  sortData(sort: Sort) {
    if (sort.direction === 'asc') {
      switch (sort.active) {
        
        case 'price': 
        let params = new HttpParams();
        params = params.append('sortParam', 'asc');
        this.http.get<PageResult<Order>>('api/order/sort-price-orders-completed', {params: params}).subscribe(result => {
          this.order = result.items;
          this.pageNumber = result.pageIndex;
          this.Count = result.count;
        }, error => console.error(error)); ;
        return;

        case 'date': 
        let params1 = new HttpParams();
        params1 = params1.append('sortParam', 'asc');
        this.http.get<PageResult<Order>>('api/order/sort-date-orders-completed', {params: params1}).subscribe(result => {
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
        this.http.get<PageResult<Order>>('api/order/sort-price-orders-completed', {params: params}).subscribe(result => {
          this.order = result.items;
          this.pageNumber = result.pageIndex;
          this.Count = result.count;
        }, error => console.error(error)); ;
        return;

        case 'date': 
        let params1 = new HttpParams();
        params1 = params1.append('sortParam', 'desc');
        this.http.get<PageResult<Order>>('api/order/sort-date-orders-completed', {params: params1}).subscribe(result => {
          this.order = result.items;
          this.pageNumber = result.pageIndex;
          this.Count = result.count;
        }, error => console.error(error)); ;
        return;
      }
    }
  }
}
