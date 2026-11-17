import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';
import { NgxImageZoomModule } from 'ngx-image-zoom';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { credentialInterceptor } from './core/interceptor/credential.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptors([credentialInterceptor])),
    provideAnimations(),
    importProvidersFrom(NgxImageZoomModule),
    provideToastr({ positionClass: 'toast-top-right', timeOut: 3000, closeButton: true, progressBar: true }),
    provideAnimationsAsync()
  ]
};