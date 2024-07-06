import { Component, inject } from '@angular/core';
import { TasksComponentService } from '../../../Services/tasks/tasks.component';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Task } from '../../../Interfaces/Task';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-index-task',
  standalone: true,
  imports: [ReactiveFormsModule, DatePipe],
  templateUrl: './index.component.html',
  styleUrl: './index.component.css'
})
export class IndexComponent {

  private _taskService = inject(TasksComponentService);
  private _router = inject(Router);
  private toastr = inject(ToastrService);
  public datepipe = inject(DatePipe);

  public IsLoaded : boolean = false;
  public IsCreating : boolean = false;
  public EditMode : boolean = false;
  public EditId :number = 0;

  private userId : string = "";

  public taskList : Task[] = [];

  public formBuild = inject(FormBuilder);

  
  public formTask: FormGroup = this.formBuild.group({
    Title: ['', Validators.required],
    Description: ['', Validators.required],
    EndDate: ['', Validators.required]
  });

  constructor(){
    this.taskList = [];
    this._taskService.GetAll().subscribe({
      next:(result) => {
        if(result.data.length > 0)
        {
          this.taskList = result.data;
          this.IsLoaded = true;
        }
      },
      error:(errorRes) => {
        console.log(errorRes);
        this.toastr.error("Error al cargar las tareas");
        this.IsLoaded = false;
      }
    });
  }

  DeleteConfirmation(id : number) {
    if(confirm("¿Estas seguro que desea eliminar la tarea?")) {
      this.DeleteTask(id);
    }
  }

  DeleteTask(id : number)
  {
    this._taskService.DeleteTask(id).subscribe({
      next:(result) => {
        if(result.data)
        {
          this.toastr.success("Se ha eliminado la tarea");
          let index = this.taskList.findIndex(d => d.id == id);
          this.taskList.splice(index,1);

          if(this.taskList.length <= 0)
          {
            this.IsLoaded = false;
          }
        }
        else{
          this.toastr.error("No se pudo eliminar las tarea");
        }
      }
    });
  }

  CreateTask(){
    var idUser = localStorage.getItem("userId")?.toString() || {};

    const data : Task = {
      id : 0,
      title : this.formTask.value.Title,
      descripcion : this.formTask.value.Description,
      endDate: this.formTask.value.EndDate,
      statusId : 1,
      statusDesc : 'Abierta',
      userID: localStorage.getItem("userId") || ""
    };


    this._taskService.CreateTask(data).subscribe({
      next:(result) => {
        if(result.succeeded)
        {
          console.log(result);
          data.id = result.data;
          this.taskList.push(data);
          if(this.taskList.length > 0)
          {
            this.IsLoaded = true;
            console.log(this.taskList);
          }
          this.toastr.success("Se ha creado la tarea");
          this.IsCreating = false;
        }
        else{
          this.toastr.error(result.messages[0]);
        }
      },
      error:(errorRes) =>
      {
        console.log(errorRes);
        this.toastr.error("Se ha producido un error la cargar la tarea");
      }
    });
  }

  EnableEdit(id : number)
  {
    var data = this.taskList.find(x => x.id == id);

    this.formTask.patchValue({
      Title : data?.title,
      Description : data?.descripcion,
      endDate : data?.endDate
    });

    this.EditMode = true;
    this.IsCreating = true;
    this.EditId = data?.id || 0;
  }

  UpdateTask(id: number){
    var data = this.taskList.find(x => x.id == id);
    const newData : Task = {
      id : this.EditId,
      title : this.formTask.value.Title,
      descripcion : this.formTask.value.Description,
      endDate: this.formTask.value.EndDate,
      statusId : data?.statusId || 0,
      statusDesc : data?.statusDesc || "",
      userID: data?.userID || ""
    };

    this._taskService.UpdateTask(newData).subscribe({
      next:(result) => {
        if(result.succeeded)
        {
          let index = this.taskList.findIndex(d => d.id == id);
          this.taskList.splice(index,1);
          newData.id = result.data;
          this.taskList.push(newData);
          if(this.taskList.length > 0)
          {
            this.IsLoaded = true;
            console.log(this.taskList);
          }
          this.toastr.success("Se ha actualizado la tarea");
          this.IsCreating = false;
        }
        else{
          this.toastr.error(result.messages[0]);
        }
      },
      error:(errorRes) =>
      {
        console.log(errorRes);
        this.toastr.error("Se ha producido un error al actualizar la tarea");
      }
    });
  }

  completedTask(id : number)
  {
    var indexData = this.taskList.findIndex(e => e.id == id);
    var data = this.taskList.find(e => e.id == id);
    var newData : Task ={
      id : data?.id || 0,
      title : data?.title || "",
      descripcion : data?.descripcion || "",
      endDate : data?.endDate || new Date(),
      statusDesc : "Completada" || "",
      statusId : 3 || 0,
      userID :data?.userID || ""
    };

    this._taskService.CompletedTask(id).subscribe({
      next:(result) => {
        if(result.succeeded)
        {
          this.taskList[indexData] = newData;
          this.toastr.success(result.messages[0]);
        }
        else
        {
          this.toastr.warning(result.messages[0]);
        }
      },
      error:(errorRes) => {
        this.toastr.error("Se ha producido un error");
      }
    })
  }

  enableForm()
  {
    this.IsCreating = true;
  }

  Cancel()
  {
    this.IsCreating = false;
  }
}
