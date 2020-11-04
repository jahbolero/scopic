import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Role } from 'src/app/models/role';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registrationForm: FormGroup;
  error:boolean =false;
  constructor( private formBuilder: FormBuilder,private authService:AuthService, private router:Router) { 
  }

  ngOnInit(): void {
    this.initForm()
    if(this.authService.userValue){
      this.authService.userValue.role == "Admin" ? this.router.navigate(['/']):this.router.navigate(['/admin'])
    }
  }
  initForm(){
    this.registrationForm = this.formBuilder.group({
      username:['',[Validators.required, Validators.email]],
      password:['', Validators.required]
    })
  }
  register(){
    if(this.registrationForm.valid){
      this.authService.register(this.registrationForm.value.username,this.registrationForm.value.password).subscribe(response=>{
        if(response.role == Role.Admin){
          this.router.navigate(['/admin'])
        }else if(response.role == Role.User){
          this.error = false;
          this.router.navigate(['/products'])
        }        
      },
      error=>{
        this.error = true;
      })
    }
  }
  get username(){return this.registrationForm.get("username")};
  get password(){return this.registrationForm.get("password")};

}
