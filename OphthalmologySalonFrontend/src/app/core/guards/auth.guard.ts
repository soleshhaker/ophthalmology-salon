import { CanActivateFn, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthenticationService } from "src/app/core/services/authentication.service"
import { inject } from '@angular/core';

export const AuthGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const authService = inject(AuthenticationService);
  const router = inject(Router);

  if (authService.isUserAuthenticated()) {
    return true;
  }

  router.navigate(['/log-in'], { queryParams: { returnUrl: state.url } });

  return false;
};
