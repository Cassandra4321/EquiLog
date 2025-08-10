import { Component, inject } from '@angular/core';
import { FooterComponent } from '../../components/footer/footer.component';
import { AuthButtonComponent } from '../../components/auth-button/auth-button.component';
import { HeaderComponent } from '../../components/header/header.component';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-frontpage',
  imports: [FooterComponent, AuthButtonComponent, HeaderComponent, CommonModule],
  templateUrl: './frontpage.component.html'
})
export class FrontpageComponent {
  private authService = inject(AuthService);
  
  isLoggedIn$ = this.authService.isLoggedIn();
}
