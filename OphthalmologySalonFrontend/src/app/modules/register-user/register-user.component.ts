import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { RegisterUserDTO } from 'src/app/core/models/registerUserDTO';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {

  registerForm: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthenticationService) {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit() {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required]],
      name: ['', [Validators.required]],
      state: [''],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]],
      email: ['', [Validators.required]],
      phone: [''],
      city: [''],
      role: [''],
      streetAddress: [''],
      postalCode: [''],
    });
  }

  public registerUser = (registerFormValue: any) => {
    const formValues = { ...registerFormValue };

    const user: RegisterUserDTO = {
      userName: formValues.username,
      name: formValues.name,
      email: formValues.email,
      phone: formValues.phone,
      state: formValues.state,
      city: formValues.city,
      streetAddress: formValues.streetAddress,
      postalCode: formValues.postalCode,
      role: formValues.role,
      password: formValues.password,
      confirmPassword: formValues.confirmPassword,
    };

    console.log(user.userName);

    this.authService.registerUser("Registration", user)
      .subscribe({
        next: (_) => console.log("Successful registration"),
        error: (err: HttpErrorResponse) => console.log(err.error.errors)
      })

  }
}
