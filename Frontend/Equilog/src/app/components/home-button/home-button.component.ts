import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home-button',
  imports: [CommonModule, RouterModule],
  templateUrl: './home-button.component.html',
  styleUrls: ['./home-button.component.scss']
})
export class HomeButtonComponent {
  @Input() text = 'Home';
  @Input() routerLink = '/homepage';
}
