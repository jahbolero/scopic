import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private authService:AuthService, private router :Router) { }
  user:User;
  ngOnInit(): void {
    this.authService.user.subscribe(user=>this.user = user);
  }
  logout(){
    this.authService.logout();
    this.router.navigate['/login']
  }

}
