import {ApplicationConfig, ErrorHandler, provideZoneChangeDetection} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {
	HTTP_INTERCEPTORS,
	provideHttpClient,
	withFetch, withInterceptors,
	withInterceptorsFromDi
} from "@angular/common/http";
import {HttpClientInterceptor} from "./core/api-management/http-client-interceptor";
import {httpClientInterceptorFn} from "./core/api-management/http-client-interceptor-fn";
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {provideAnimations} from "@angular/platform-browser/animations";


export const appConfig: ApplicationConfig = {
  providers: [
	  provideAnimations(),
	  provideZoneChangeDetection({ eventCoalescing: true }),
	  provideRouter(routes),
	  //provideHttpClient(withInterceptors([httpClientInterceptorFn]))
	  provideHttpClient(withInterceptorsFromDi()),
	  {
		  provide: HTTP_INTERCEPTORS, useClass: HttpClientInterceptor, multi: true
	  }, provideAnimationsAsync()
  ]
};
