import {Component, OnInit} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {JsonPipe} from "@angular/common";
import {IFormGroup} from "../../../../../utils/type/i-form-group";
import {LoginModel} from "../../../../../model/auth/login-model";
import {AuthService} from "../../../../../service/auth/auth.service";

@Component({
	selector: 'app-login',
	standalone: true,
	imports: [
		ReactiveFormsModule,
		JsonPipe
	],
	templateUrl: './login.component.html',
	styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
	loginForm: IFormGroup<LoginModel>;

	constructor(private formBuilder: FormBuilder, private authService: AuthService) {
		this.loginForm = this.formBuilder.nonNullable.group({
			usernameOrEmail: ['', Validators.required],
			password: ['', Validators.required]
		})
	}

	ngOnInit() {
		this.loginForm.valueChanges.subscribe(console.log)
	}

	onSubmit() {
		if (!this.loginForm.valid) return;

		const loginModel: LoginModel = {
			usernameOrEmail: this.loginForm.value.usernameOrEmail!,
			password: this.loginForm.value.password!
		}

		this.authService.userLogin(`auth/login`, loginModel).subscribe(console.log)
	}
}
