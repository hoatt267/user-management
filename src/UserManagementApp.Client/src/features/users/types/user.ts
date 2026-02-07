export interface User {
  id: string;
  fullName: string;
  email: string;
  isActive: boolean;
  role: string | number;
}

export interface UserFormData {
  id?: string;
  fullName?: string;
  email?: string;
  role?: string | number;
  isActive?: boolean;
}
