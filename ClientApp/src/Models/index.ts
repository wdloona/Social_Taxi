export * from './Address'
export * from './Auth'
export * from './Cookie'
export * from './Login'
export * from './Notification'
export * from './Ride'
export * from './Routing'
export * from './User'

export interface BaseModel<T> {
  success: boolean,
  errorMessage: string | undefined,
  data: T
}