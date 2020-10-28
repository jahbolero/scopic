import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { Role } from '../models/role';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private router:Router,
    private authenticationService:AuthService)
    {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) {
      const user = this.authenticationService.userValue;
      
      if(user){
        if(route.data.roles && !route.data.roles.includes(user.role)){
          if(user.role == Role.Admin){
            this.router.navigate(['/admin']);
          }else if(user.role == Role.User){
            this.router.navigate(['/products']);
          } 
          console.log(route.url)
          return false;
        }
        console.log("Can go in")
        return true;
      }
      console.log(route.url)
      this.router.navigate(['/login']);
      return false;
  }
  
}
