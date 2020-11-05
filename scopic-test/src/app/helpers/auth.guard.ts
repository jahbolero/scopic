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
          return false;
        }
        return true;
      }
      this.router.navigate(['/login']);
      return false;
  }
  
}
