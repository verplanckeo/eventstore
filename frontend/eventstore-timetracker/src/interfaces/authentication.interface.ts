export interface AuthenticateRequest {
  userName: string;
  password: string;
}

export interface AuthenticateResponse {
  id: string;
  token: string;
}

export interface AuthUser {
  id: string;
  userName: string;
}