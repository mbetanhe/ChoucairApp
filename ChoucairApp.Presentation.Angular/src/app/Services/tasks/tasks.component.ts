import { HttpClient } from '@angular/common/http';
import { Component, inject, Injectable } from '@angular/core';
import { appsettings } from '../../settings/appsettings';
import { Task } from '../../Interfaces/Task';
import { ApiResponse } from '../../Interfaces/ApiResponse';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [],
  template: ''
})

@Injectable({
  providedIn: 'root'
})

export class TasksComponentService {
  private clientHttp = inject(HttpClient);
  private baseUrl : string = appsettings.apiUrl;
  

  constructor(){}


  //Obtenemos todos las tareas.
  GetAll() : Observable<ApiResponse<Task[]>>{
    var userId = localStorage.getItem("userId");
    return this.clientHttp.get<ApiResponse<Task[]>>(`${this.baseUrl}Task/`+ userId);
  }

  CreateTask(request : Task) : Observable<ApiResponse<number>>{
    return this.clientHttp.post<ApiResponse<number>>(`${this.baseUrl}Task`, request);
  }

  UpdateTask(request : Task) : Observable<ApiResponse<number>>{
    return this.clientHttp.post<ApiResponse<number>>(`${this.baseUrl}Task`, request);
  }

  DeleteTask(id : number) : Observable<ApiResponse<boolean>>{
    return this.clientHttp.delete<ApiResponse<boolean>>(`${this.baseUrl}Task/` + id);
  }

  CompletedTask(id : number) : Observable<ApiResponse<boolean>>{
    return this.clientHttp.post<ApiResponse<boolean>>(`${this.baseUrl}Task/CompletedTask`, id);
  }
  
}
