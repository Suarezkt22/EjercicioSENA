export interface DataResponse<T> {
  succeeded: boolean;
  data: T;
}

export interface MessageResponse {
  succeeded: boolean;
  message: string;
}
