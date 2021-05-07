import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ClientError } from '../ClientError';
import { PageResult } from '../PageResult';
import { Category } from './CategoryModel';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  category: Category[];
  public pageNumber: number = 1;
  public Count: number;
  
  constructor(private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http.get<PageResult<Category>>('api/category/get-category').subscribe(result => {
      this.category = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  public onPageChange = (pageNumber) => {
    this.http.get<PageResult<Category>>('api/category/get-category?page=' + pageNumber).subscribe(result => {
      this.category = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  load()
  {
    this.http.get<PageResult<Category>>('api/category/get-category').subscribe(result => {
      this.category = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  delete(id: number)
  {
    this.http.delete('api/category/' + id).subscribe(
      result => {
            const resultMessage: ClientError = <ClientError>result;
            alert(resultMessage.message);
            this.load();
        },
        (error:HttpErrorResponse) => { 
          if(error.status===400){            
             const errors: ClientError = error.error;
                alert(errors.message);
          }
        } 
      );
  }
  open_create(): void
  {
    this.router.navigate(['/add-category']);
  }
}
