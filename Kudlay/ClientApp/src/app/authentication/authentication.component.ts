import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ClientError } from '../ClientError';
import { Login } from './Login';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {
  private login_User: Login = new Login();
  
  constructor(private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  }
  ngOnInit() {
  }

  login(form: NgForm): void {
    
    this.login_User.loginAdmin = form.value.userName;
    this.login_User.passwordAdmin = form.value.userPassword;
    
    this.http.post('api/login/authentication-user', this.login_User).subscribe(
      result => {
      const token = (<any>result).token;
      localStorage.setItem('jwt', token);
      this.router.navigate(['/order']);
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
