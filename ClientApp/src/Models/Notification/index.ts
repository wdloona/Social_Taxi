
export interface PushSubscription {
  endPoint?: string | undefined;
  auth?: string | undefined;
  p256Dh?: string | undefined;
}

export class SwaggerException extends Error {
  message: string;
  status: number;
  response: string;
  headers: { [key: string]: any; };
  result: any;

  constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
    super();

    this.message = message;
    this.status = status;
    this.response = response;
    this.headers = headers;
    this.result = result;
  }

  protected isSwaggerException = true;

  static isSwaggerException(obj: any): obj is SwaggerException {
    return obj.isSwaggerException === true;
  }
}

export interface INotificationState {
  swRegistration: ServiceWorkerRegistration | undefined,
  isSubscribed: boolean,
  isSupported: boolean,
  isInProgress: boolean
}