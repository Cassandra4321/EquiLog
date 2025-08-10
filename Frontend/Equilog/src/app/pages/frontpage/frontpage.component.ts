import { Component } from '@angular/core';
import { FooterComponent } from '../../components/footer/footer.component';
import { AuthButtonComponent } from '../../components/auth-button/auth-button.component';

@Component({
  selector: 'app-frontpage',
  imports: [FooterComponent, AuthButtonComponent],
  templateUrl: './frontpage.component.html'
})
export class FrontpageComponent {

}
