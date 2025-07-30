import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { routes } from './app/app.routes';
import { ApiClient } from './app/domain/client';
import { provideRouter } from '@angular/router';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    {
      provide: ApiClient,
      useFactory: () => new ApiClient('https://localhost:7062')
    }
  ]
});
