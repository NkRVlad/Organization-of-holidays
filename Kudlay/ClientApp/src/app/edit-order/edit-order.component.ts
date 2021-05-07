import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../category/CategoryModel';
import { ClientError } from '../ClientError';
import { Order } from '../order/OrderModel';
import { PageResult } from '../PageResult';
import { Status } from '../status/StatusModel';

@Component({
  selector: 'app-edit-order',
  templateUrl: './edit-order.component.html',
  styleUrls: ['./edit-order.component.css']
})
export class EditOrderComponent implements OnInit {

  id: number;
  test: FormGroup = new FormGroup({});
  order: Order= new Order();
  status: Status[];
  category: Category[];
  constructor(private router: ActivatedRoute,private route: Router,private fb: FormBuilder,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.router.queryParams.subscribe(
      result => 
      {
        this.id = <number>result.id;
        let params = new HttpParams();
        params = params.append('idOrder',  this.id.toString());
        this.http.get<Order>('api/order/get-edit-order', {params: params}).subscribe(result => {
          this.order = result;
        }, error => console.error(error));
      }
    );
    this.test = fb.group({
      phone: ['phone', [Validators.required, Validators.pattern("[0-9]{10}$")]],
      name: ['name', [Validators.required, Validators.maxLength(20), Validators.minLength(4)]],
      city: ['city', [Validators.required]],
      street: ['street', [Validators.required]],
      duration: ['duration', [Validators.required]],
      date: ['date'],
      time: ['time'],
      status: ['status'],
      category: ['category']
    });
    this.http.get<PageResult<Status>>('api/status/get-status').subscribe(result => {
      this.status = result.items;
    }, error => console.error(error));
    
    this.http.get<PageResult<Category>>('api/category/get-category').subscribe(result => {
      this.category = result.items;
    }, error => console.error(error));
  }
  get f(){
    return this.test.controls;
  }
   


  submit(){
    console.log(this.test.value);
  }

  ngOnInit() {
    

  }

  edit(form: NgForm): void {
    this.order.id = Number(this.id);
    this.order.name = form.value.name;
    this.order.phone = form.value.phone;
    this.order.city = form.value.city;
    this.order.street = form.value.street;
    this.order.time = form.value.time;
    this.order.date = form.value.date;
    this.order.duration = form.value.duration;
    this.order.statusId = form.value.status;
    this.order.categoryId = form.value.category;
    
    this.http.put('api/order/edit-order', this.order).subscribe(result => {
      this.route.navigate(['/pending-order']);
    },
    (error:HttpErrorResponse) => { 
      if(error.status===400){            
         const errors: ClientError = error.error;
            alert(errors.message);
      }
    } 
    );
  }
  cancel()
  {
    this.route.navigate(['/pending-order']);
  }
}
