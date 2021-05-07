import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Category } from '../category/CategoryModel';
import { ClientError } from '../ClientError';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnInit {

  
  category: Category = new Category();
  minTime: number = 1;
  constructor(private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  }

  ngOnInit() {
  }
  create(form: NgForm): void {
    
    this.category.name = form.value.name;
    this.category.price = form.value.price;
    this.category.minTime = form.value.minTime;
    
    this.http.post('api/category/add-category', this.category).subscribe(
      result =>
      {
        this.router.navigate(['/category']);
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
    this.router.navigate(['/category']);
  }
}
