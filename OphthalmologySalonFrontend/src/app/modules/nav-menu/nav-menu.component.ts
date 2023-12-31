import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  public isUserAuthenticated: boolean = false;
  constructor(private authService: AuthenticationService, private router: Router) {
    this.authService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      })
  }

  ngOnInit(): void {
    this.authService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      })
  }

  public logout = () => {
    this.authService.logout();
    this.router.navigate(["/"]);
  }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
