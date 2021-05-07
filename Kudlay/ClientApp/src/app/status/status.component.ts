import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ClientError } from '../ClientError';
import { PageResult } from '../PageResult';
import { Status } from './StatusModel';

@Component({
  selector: 'app-status',
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.css']
})
export class StatusComponent implements OnInit {
  public pageNumber: number = 1;
  public Count: number;
  public status: Status[];
  tableMode: boolean = true;  

  constructor(private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http.get<PageResult<Status>>('api/status/get-status').subscribe(result => {
      this.status = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  ngOnInit() {
  }
  
  public onPageChange = (pageNumber) => {
    this.http.get<PageResult<Status>>('api/status/get-status?page=' + pageNumber).subscribe(result => {
      this.status = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  
  load()
  {
    this.http.get<PageResult<Status>>('api/status/get-status').subscribe(result => {
      this.status = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  delete(id: number)
  {
    this.http.delete('api/status/' + id).subscribe(
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
    this.router.navigate(['/add-status']);
  }
}
