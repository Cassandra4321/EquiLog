import { Injectable, inject } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})

export class AdminGuard implements CanActivate {
  private authService = inject(AuthService);
  private router = inject(Router);

  canActivate(): boolean {
    const roles = this.authService.getUserRoles();
    if (roles.includes("StableOwner")) {
      return true;
    }
    this.router.navigate(['/homepage']);
    return false;
  }
}