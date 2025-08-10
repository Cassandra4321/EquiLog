import { Component } from '@angular/core';
import { FooterComponent } from '../../components/footer/footer.component';
import { AuthButtonComponent } from '../../components/auth-button/auth-button.component';

@Component({
  selector: 'app-homepage',
  imports: [FooterComponent, AuthButtonComponent],
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent {

}
