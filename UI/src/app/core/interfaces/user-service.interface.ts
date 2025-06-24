import { Observable } from "rxjs";
import { DataResponse, MessageResponse } from "../models/standard.model";
import { InjectionToken } from "@angular/core";
import { Token, UserPayload } from "../models/user.model";

export const USERS_SERVICE = new InjectionToken<IUserService>('UserService');

export interface IUserService {
  register(payload: UserPayload) : Observable<MessageResponse>;

  login(payload: UserPayload) : Observable<DataResponse<Token>>;
}
