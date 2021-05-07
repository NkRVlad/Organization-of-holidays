import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PageResult } from '../PageResult';
import { Order } from './OrderModel';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  order: Order[];
  public pageNumber: number = 1;
  public Count: number;
  public idOrder: EditOrder = new EditOrder();
  constructor(private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http.get<PageResult<Order>>('api/order/get-new-order').subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  ngOnInit() {
  }

  loadOrder()
  {
    this.http.get<PageResult<Order>>('api/order/get-new-order').subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  public onPageChange = (pageNumber) => {
    this.http.get<PageResult<Order>>('api/order/get-new-order?page=' + pageNumber).subscribe(result => {
      this.order = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  move(id: number)
  {
    this.idOrder.id = id;
    this.http.put<PageResult<Order>>('api/order/edit-status-order', this.idOrder).subscribe(result => {this.loadOrder()});
   
  }
}

export class EditOrder
{
  public id: number;
}
