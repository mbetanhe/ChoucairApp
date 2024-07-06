import { Component, inject } from '@angular/core';
import { LoginServiceComponent } from '../../../Services/login/loginservice.component';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { RegisterRequest } from '../../../Interfaces/RegisterRequest';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})


export class RegisterComponent {
  
  private _loginService = inject(LoginServiceComponent);
  private _router = inject(Router);
  public formBuild = inject(FormBuilder);
  private toastr = inject(ToastrService);

  public formRegister: FormGroup = this.formBuild.group({
    email: ['', Validators.required],
    document: ['', Validators.required],
    firsname: ['', Validators.required],
    lastname: ['', Validators.required],
    userName: ['', Validators.required],
    password: ['', Validators.required]
  });

  Register(){
    if(this.formRegister.invalid){
      this.toastr.info("El formulario no se encuentra completo");
      return;
    }

    const data : RegisterRequest = {
      document: this.formRegister.value.document.toString(),
      firsname: this.formRegister.value.firsname,
      lastname: this.formRegister.value.lastname,
      userName: this.formRegister.value.userName,
      email: this.formRegister.value.email,
      password: this.formRegister.value.password,
    }

    this._loginService.Register(data).subscribe({
      next:(result) => {
        if(result.succeeded)
        {
          this.toastr.success(result.messages[0]);
          this._router.navigate(['Login'])
        }
        else
        {
          this.toastr.error("Se ha producido un error al registrarse");
        }
      },
        error:(errorRes) =>{
          console.log(errorRes)
        }
    });
  }

  Back() {
    this._router.navigate(['Login']);
  }

  showSuccess() {
    this.toastr.success('Hello world!', 'Toastr fun!');
  }

}
