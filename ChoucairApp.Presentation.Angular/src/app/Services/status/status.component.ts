import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { appsettings } from '../../settings/appsettings';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../Interfaces/ApiResponse';
import { Status } from '../../Interfaces/Status';

@Component({
  selector: 'app-status',
  standalone: true,
  imports: [],
  template: ''  
})
export class StatusComponent {
  private clientHttp = inject(HttpClient);
  private baseUrl : string = appsettings.apiUrl;

  constructor(){}


  //Obtenemos todos los estados.
  GetAll() : Observable<ApiResponse<Status[]>>{
    return this.clientHttp.get<ApiResponse<Status[]>>(`${this.baseUrl}/Status`);
  }
  
}
