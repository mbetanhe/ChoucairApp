import { Routes } from '@angular/router';
import { RegisterComponent } from './Pages/Identity/register/register.component';
import { HomeComponent } from './Pages/home/home.component';
import { LoginComponent } from './Pages/Identity/login/login.component';

export const routes: Routes = [
    {path:"", component : LoginComponent},
    {path:"Register", component : RegisterComponent},
    {path:"Login", component : LoginComponent},
    {path:"Home", component : HomeComponent},
];
