import { CanActivateFn, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthenticationService } from "src/app/core/services/authentication.service"
import { inject } from '@angular/core';

export const AdminGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const authService = inject(AuthenticationService);
  const router = inject(Router);

  if (authService.isUserAdmin()) {
    return true;
  }

  router.navigate(['/forbidden'], { queryParams: { returnUrl: state.url } });
  return false;
};
