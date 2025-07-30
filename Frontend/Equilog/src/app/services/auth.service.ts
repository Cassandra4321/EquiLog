import { Injectable, inject } from '@angular/core';
import { ApiClient, LoginRequest, LoginResponse } from '../domain/client';
import { BehaviorSubject, Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

  interface JwtPayload {
  roles: string[];
  email: string;
  exp: number;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly TOKEN_KEY = 'auth_token';
  private readonly USER_EMAIL_KEY = 'user_email';

  private loggedIn$ = new BehaviorSubject<boolean>(this.hasToken());

  private apiClient = inject(ApiClient);

  login(email: string, password: string): Observable<LoginResponse> {
    const request = new LoginRequest({ email, password });

    return new Observable(observer => {
      this.apiClient.login(request).then(response => {
        if (response?.token) {
          localStorage.setItem(this.TOKEN_KEY, response.token);
          localStorage.setItem(this.USER_EMAIL_KEY, response.email || '');
          this.loggedIn$.next(true);
        }
        observer.next(response);
        observer.complete();
      }).catch(error => {
        observer.error(error);
      });
    });
  }

  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.USER_EMAIL_KEY);
    this.loggedIn$.next(false);
  }

  isLoggedIn(): Observable<boolean> {
    return this.loggedIn$.asObservable();
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  getUserEmail(): string | null {
    return localStorage.getItem(this.USER_EMAIL_KEY);
  }

  private hasToken(): boolean {
    return !!localStorage.getItem(this.TOKEN_KEY);
  }

  getUserRoles(): string[] {
    const token = this.getToken();
    if(!token) {
        return [];
    }

    const decodedToken = jwtDecode<JwtPayload>(token);
    return decodedToken.roles || [];
  }
}


