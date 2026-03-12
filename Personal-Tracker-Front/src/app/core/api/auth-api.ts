import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

interface RegisterRequest{
  email: string;
  password: string;
}

interface LoginRequest{
  email: string;
  password: string;
}

@Injectable({
  providedIn: 'root',
})

export class AuthApi {
  private http = inject(HttpClient);

  private baseUrl = '/api/auth';

  register(data: RegisterRequest): Observable<string>{
    return this.http.post<string>(`${this.baseUrl}/register`,data);
  }
  login(data: LoginRequest): Observable<string>{
    return this.http.post<string>(`${this.baseUrl}/login`,data);
  }
}
