import { Routes } from '@angular/router';
import { FrontpageComponent } from './pages/frontpage/frontpage.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { AllHorsesPageComponent } from './pages/all-horses-page/all-horses-page.component';
import { MyHorsesPageComponent } from './pages/my-horses-page/my-horses-page.component';

export const routes: Routes = [
  {
    path: '',
    component: FrontpageComponent,
  },
  {
    path: 'login',
    component: LoginPageComponent,
  },
  {
    path: 'all-horses',
    component: AllHorsesPageComponent,
  },
  {
    path: 'my-horses',
    component: MyHorsesPageComponent,
  },
];
