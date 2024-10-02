import {
    HttpErrorResponse,
    HttpEvent,
    HttpHandler,
    HttpInterceptorFn,
    HttpRequest
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError, switchMap } from "rxjs/operators";
import { inject } from "@angular/core";
import { AuthService } from "../../service/auth/auth.service";
import { Router } from "@angular/router";

export const httpClientInterceptorFn: HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    const modifiedHeaderReq = req.clone({
        headers: req.headers.append('Access-Control-Allow-Origin', "http://localhost:4200")
    });

    return next(modifiedHeaderReq).pipe(
        catchError((error: HttpErrorResponse) => {
            if (error.status !== 401) {
                return throwError(() => error.message);
            }

            if (!error.url?.toLowerCase().includes("auth/refresh")) {
                return authService.refreshAuth("auth/refresh").pipe(
                    switchMap(() => {
                        const clonedReq = req.clone({
                            withCredentials: true
                        });

                        return next(clonedReq);
                    })
                ) as Observable<any>;
            }

            if (error.url?.toLowerCase().includes("auth/refresh")) {
                return router.navigate(["login"]);
            }

            return new Observable();
        })
    );
};
