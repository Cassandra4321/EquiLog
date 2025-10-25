import { Injectable, inject } from '@angular/core';
import {
  ApiClient,
  HorseDto,
  CreateHorseRequest,
  UpdateHorseRequest,
} from '../domain/client';
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
      this.apiClient
        .horses()
        .then(horses => {
          observer.next(horses || []);
          observer.complete();
        })
        .catch(error => {
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

  createHorse(horse: CreateHorseRequest): Observable<HorseDto> {
    return new Observable(observer => {
      this.apiClient
        .createHorse(horse)
        .then((result: HorseDto) => {
          observer.next(result);
          observer.complete();
        })
        .catch(error => observer.error(error));
    });
  }

  updateHorse(id: number, horse: UpdateHorseRequest): Observable<HorseDto> {
    return new Observable<HorseDto>(observer => {
      this.apiClient
        .horsePUT(id, horse)
        .then((result: HorseDto) => {
          observer.next(result);
          observer.complete();
        })
        .catch(error => observer.error(error));
    });
  }

  deleteHorse(id: number): Observable<void> {
    return new Observable<void>(observer => {
      this.apiClient
        .horseDELETE(id)
        .then(() => {
          observer.next();
          observer.complete();
        })
        .catch(error => observer.error(error));
    });
  }
}
