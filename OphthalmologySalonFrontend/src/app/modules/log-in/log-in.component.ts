import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthenticationService) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit() {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  onLogin() {
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;
      this.authService.login("Login", username, password).subscribe(
        (result) => {
          if (result) {
            console.log("login succesfull");
            // Login successful
            // Redirect or perform other actions here
          } else {
            console.log("login failed");
            // Login failed
            // Handle error or show error message
          }
        },
        (error) => {
          // Handle HTTP error
        }
      );
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
