import { Routes } from '@angular/router';
import { FrontpageComponent } from './pages/frontpage/frontpage.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { HomepageComponent } from './pages/homepage/homepage.component';

export const routes: Routes = [
  { 
    path: '', 
    component: FrontpageComponent
  },
  {
    path: 'login',
    component: LoginPageComponent
  },
  {
    path: 'homepage',
    component: HomepageComponent
  }
];