import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IUserService } from "../interfaces/user-service.interface";
import { Observable } from "rxjs";
import { MessageResponse, DataResponse } from "../models/standard.model";
import { Token, UserPayload } from "../models/user.model";
import { USERS_API } from "../../../environments/config";


@Injectable({
  providedIn: 'root'
})
export class UserService implements IUserService {
  constructor(private readonly httpClient: HttpClient) {}

  public register(payload: UserPayload): Observable<MessageResponse> {
    return this.httpClient.post<MessageResponse>(
      USERS_API.REGISTER,
      payload
    );
  }

  public login(payload: UserPayload): Observable<DataResponse<Token>> {
    return this.httpClient.post<DataResponse<Token>>(
      USERS_API.LOGIN,
      payload
    );
  }
}
