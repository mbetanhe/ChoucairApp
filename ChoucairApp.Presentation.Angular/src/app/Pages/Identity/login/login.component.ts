import { Component, inject } from '@angular/core';
import { LoginServiceComponent } from '../../../Services/login/loginservice.component';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrService, ToastNoAnimation } from 'ngx-toastr';
import { LoginRequest } from '../../../Interfaces/LoginRequest';
import { error } from 'console';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  private _loginService = inject(LoginServiceComponent);
  private _router = inject(Router);
  public formBuild = inject(FormBuilder);
  private toastr = inject(ToastrService);

  public formLogin: FormGroup = this.formBuild.group({
    email: ['', Validators.required],
    password: ['', Validators.required]
  });

  login(){
    if(this.formLogin.invalid){
      this.toastr.info("El formulario no se encuentra completo");
      return;
    }

    const data : LoginRequest = {
      email : this.formLogin.value.email,
      password : this.formLogin.value.password,
    }

    this._loginService.Login(data).subscribe({
      next:(result) => {
        if(result.isAuthenticated)
        {
          localStorage.setItem("token", result.token);
          localStorage.setItem("userId", result.id);
          localStorage.setItem("userName", result.userName);
          this.toastr.success(`Bienvenido ${result.userName}`);
          this._router.navigate(['Home'])
        }
        else
        {
          this.toastr.warning("Las credenciales son incorrectas");
        }
      },
        error:(errorRes) =>{;
          this.toastr.error("No se pudo establecer conexión")
          console.log(errorRes);
        }
    });
  }

  Register(){
    this._router.navigate(['Register']);
  }

}
