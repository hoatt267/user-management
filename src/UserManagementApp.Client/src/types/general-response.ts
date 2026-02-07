export interface GeneralResponse<T> {
  success: boolean;
  message: string | null;
  data: T;
  errors?: Record<string, string[]> | null;
}
