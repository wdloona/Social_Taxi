interface LoginResponse
{
  success: boolean,
  message: string
}

interface LoginType {
  Login: string;
  Password: string;
}

export type { LoginResponse, LoginType }