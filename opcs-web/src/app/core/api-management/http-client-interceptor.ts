import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {catchError, Observable, switchMap, throwError} from "rxjs";
import {Injectable} from "@angular/core";
import {AuthService} from "../../service/auth/auth.service";
import {Router} from "@angular/router";

@Injectable({providedIn: 'root'})

export class HttpClientInterceptor implements HttpInterceptor {
	private endPointList: string[] = ["opcs/auth/login"]

	constructor(private authService: AuthService, private router: Router) {
	}

	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		const modifiedHeaderReq = req.clone({
			headers: req.headers.append('Access-Control-Allow-Origin', "http://localhost:4200")
		});

		return next.handle(modifiedHeaderReq).pipe(
			catchError((error: HttpErrorResponse) => {
				if (error.status != 401) {
					return throwError(() => error.message);
				}

				if (!error.url?.toLowerCase().includes("auth/refresh")) {
					return this.authService.refreshAuth("auth/refresh").pipe(
						switchMap(() => {
							const clonedReq = req.clone({
								withCredentials: true
							});

							return next.handle(clonedReq);
						})
					) as Observable<any>
				}

				if (error.url?.toLowerCase().includes("auth/refresh")) {
					return this.router.navigate(["login"]);
				}

				return next.handle(modifiedHeaderReq)
			})
		)
	}
}
