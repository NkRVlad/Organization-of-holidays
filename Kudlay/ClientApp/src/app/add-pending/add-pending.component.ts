import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Category } from '../category/CategoryModel';
import { ClientError } from '../ClientError';
import { Order } from '../order/OrderModel';
import { PageResult } from '../PageResult';
import { Status } from '../status/StatusModel';

@Component({
  selector: 'app-add-pending',
  templateUrl: './add-pending.component.html',
  styleUrls: ['./add-pending.component.css']
})
export class AddPendingComponent implements OnInit {

  order: Order = new Order();
  status: Status[];
  category: Category[];
  test: FormGroup = new FormGroup({});
  constructor(private fb: FormBuilder, private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
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

  cancel()
  {
    this.router.navigate(['/pending-order']);
  }
  create(form: NgForm): void {
    this.order.name = form.value.name;
    this.order.phone = form.value.phone;
    this.order.city = form.value.city;
    this.order.street = form.value.street;
    this.order.time = form.value.time;
    this.order.date = form.value.date;
    this.order.duration = form.value.duration;
    this.order.statusId = form.value.status;
    this.order.categoryId = form.value.category;
    
    this.http.post('api/order/add-order', this.order).subscribe(result => {
      this.router.navigate(['/pending-order']);
    },
    (error:HttpErrorResponse) => { 
      if(error.status===400){            
         const errors: ClientError = error.error;
            alert(errors.message);
      }
    } 
    );
  }
}
