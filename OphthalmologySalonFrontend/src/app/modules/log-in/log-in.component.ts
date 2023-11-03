import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { LoginUserDTO } from '../../core/models/loginUserDTO';
import { AuthResponseDto } from '../../core/models/authResponseDTO';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LoginComponent implements OnInit {
  private returnUrl: string;

  loginForm: FormGroup;
  errorMessage: string = '';
  showError: boolean = false;
  constructor(private fb: FormBuilder, private authService: AuthenticationService, private router: Router, private route: ActivatedRoute) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  ngOnInit() {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }



  onLogin = (loginFormValue: any) => {
    const formValues = { ...loginFormValue };
    this.showError = false;
    const user: LoginUserDTO = {
      username: formValues.username,
      password: formValues.password
    };

    if (this.loginForm.valid) {
      const body = this.loginForm.value;
      this.authService.loginUser("Login", body).subscribe({
        next: (res: AuthResponseDto) => {
          localStorage.setItem("token", res.token);
          this.authService.sendAuthStateChangeNotification(res.isAuthSuccessful);
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
        }
      })
    }
  }

  isFieldEmpty(fieldName: string): boolean {
    const field = this.loginForm.get(fieldName);
    return field !== null && !field.value;
  }

  toProperCase(fieldName: string): string {
    return fieldName.charAt(0).toUpperCase() + fieldName.slice(1);
  }
}
