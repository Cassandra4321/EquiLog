import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-auth-button',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './auth-button.component.html',
  styleUrls: ['./auth-button.component.scss']
})
export class AuthButtonComponent {
  private authService = inject(AuthService);
  private router = inject(Router);

  isLoggedIn$ = this.authService.isLoggedIn();

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  login() {
    this.router.navigate(['/login']);
  }
}
