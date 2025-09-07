import { Injectable, inject } from '@angular/core';
import { ApiClient, HorseDto } from '../domain/client';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HorsesService {
  private apiClient = inject(ApiClient);

  constructor() {
    const baseUrl = 'https://localhost:7062';

    const customFetch = async (
      url: RequestInfo,
      init?: RequestInit
    ): Promise<Response> => {
      const token = localStorage.getItem('equilog_auth_token');

      const headers = new Headers(init?.headers);
      if (token) {
        headers.set('Authorization', `Bearer ${token}`);
      }

      console.log('Custom fetch körs för:', url, 'med token:', !!token);

      return fetch(url, { ...init, headers });
    };

    this.apiClient = new ApiClient(baseUrl, { fetch: customFetch });
  }

  getAllHorses(): Observable<HorseDto[]> {
    return new Observable(observer => {
      console.log('Anropar apiClient.horses()...');
      this.apiClient
        .horses()
        .then(horses => {
          console.log('Lyckades hämta hästar:', horses);
          observer.next(horses || []);
          observer.complete();
        })
        .catch(error => {
          console.error('Error fetching all horses:', error);
          console.error('Fullständigt error-objekt:', error);
          console.error('Error message:', error.message);
          console.error('Error status:', error.status);
          console.error('Error response:', error.response);
          observer.error(error);
        });
    });
  }

  getMyHorses(): Observable<HorseDto[]> {
    return new Observable(observer => {
      this.apiClient
        .mine()
        .then(horses => {
          observer.next(horses || []);
          observer.complete();
        })
        .catch(error => {
          console.error('Error fetching my horses:', error);
          observer.error(error);
        });
    });
  }
}
