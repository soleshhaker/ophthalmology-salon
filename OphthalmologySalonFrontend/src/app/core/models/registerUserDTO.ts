export interface RegisterUserDTO {

  userName: string;
  name: string;
  email: string;
  phone?: string;
  state?: string;
  city?: string;
  streetAddress?: string;
  postalCode?: string;
  role?: string;
  password: string;
  confirmPassword: string;
}
