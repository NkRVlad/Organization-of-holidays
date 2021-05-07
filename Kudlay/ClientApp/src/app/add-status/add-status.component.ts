import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ClientError } from '../ClientError';
import { Status } from '../status/StatusModel';

@Component({
  selector: 'app-add-status',
  templateUrl: './add-status.component.html',
  styleUrls: ['./add-status.component.css']
})
export class AddStatusComponent implements OnInit {
  

  status: Status = new Status();

  constructor(private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  }

  ngOnInit() {
  }
  create(form: NgForm): void {
    
    this.status.name = form.value.name;
    
    this.http.post('api/status/add-status', this.status).subscribe(
      result =>
      {
        this.router.navigate(['/status']);
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
    this.router.navigate(['/status']);
  }
}
