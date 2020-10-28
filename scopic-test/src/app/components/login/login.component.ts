import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Role } from 'src/app/models/role';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  constructor( private formBuilder: FormBuilder,private authService:AuthService, private router:Router) { 
  }

  ngOnInit(): void {
    this.initForm()
    if(this.authService.userValue){
      this.authService.userValue.role == "Admin" ? this.router.navigate(['/']):this.router.navigate(['/admin'])
    }
  }
  initForm(){
    this.loginForm = this.formBuilder.group({
      username:['',Validators.required],
      password:['', Validators.required]
    })
  }
  login(){
    if(this.loginForm.valid){
      this.authService.login(this.loginForm.value.username,this.loginForm.value.password).subscribe(response=>{
        if(response.role == Role.Admin){
          this.router.navigate(['/admin'])
        }else if(response.role == Role.User){
          this.router.navigate(['/products'])
        }        
      },
      error=>{
        console.log(error);
      })
    }
  }

}
