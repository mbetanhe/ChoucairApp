import { HttpClient } from '@angular/common/http';
import { Component, inject, Injectable } from '@angular/core';
import { appsettings } from '../../settings/appsettings';
import { RegisterRequest } from '../../Interfaces/RegisterRequest';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../Interfaces/ApiResponse';
import { LoginRequest } from '../../Interfaces/LoginRequest';
import { AuthenticationResponse } from '../../Interfaces/AuthenticationResponse';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [],
  template: '<br/>'
})

@Injectable({
  providedIn: 'root'
})

export class LoginServiceComponent {
  private clientHttp = inject(HttpClient);
  private baseUrl : string = appsettings.apiUrl;

  constructor(){}

  Register(request : RegisterRequest) : Observable<ApiResponse<string>>{
    return this.clientHttp.post<ApiResponse<string>>(`${this.baseUrl}User/Register`, request);
  }

  Login(request : LoginRequest) : Observable<AuthenticationResponse>{
    return this.clientHttp.post<AuthenticationResponse>(`${this.baseUrl}User/Login`, request);
  }
}
