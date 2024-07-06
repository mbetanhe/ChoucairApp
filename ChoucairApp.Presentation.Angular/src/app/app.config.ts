import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { HttpClientModule, provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';
import { authInterceptor } from './Custom/auth.interceptor';
import { DatePipe } from '@angular/common';

export const appConfig: ApplicationConfig = {
  providers: [
    DatePipe,
    provideRouter(routes), 
    provideAnimations(),
    provideToastr(
      { timeOut: 10000,
        positionClass : 'toast-top-center',
        preventDuplicates: true,
      }),
    provideClientHydration(),
    importProvidersFrom(HttpClientModule),
    //provideHttpClient(withInterceptors({authInterceptor}))
    provideHttpClient(withFetch(), withInterceptors([authInterceptor]))
  ]
};
