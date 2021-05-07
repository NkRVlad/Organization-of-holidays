import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Order } from '../order/OrderModel';
import { PageResult } from '../PageResult';

@Component({
  selector: 'app-canceled-orders',
  templateUrl: './canceled-orders.component.html',
  styleUrls: ['./canceled-orders.component.css']
})
export class CanceledOrdersComponent implements OnInit {

  order: Order[];
  public pageNumber: number = 1;
  public Count: number;

  constructor(private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http.get<PageResult<Order>>('api/order/get-canceled-order').subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  loadCanceledOrder()
  {
    this.http.get<PageResult<Order>>('api/order/get-canceled-order').subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  public onPageChange = (pageNumber) => {
    this.http.get<PageResult<Order>>('api/order/get-canceled-order?page=' + pageNumber).subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  search(form: NgForm)
  {
    let params = new HttpParams();
    params = params.append('text', form.value.textSearch);
    this.http.get<PageResult<Order>>('api/order/get-search-orders-canceled', {params: params}).subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

}
