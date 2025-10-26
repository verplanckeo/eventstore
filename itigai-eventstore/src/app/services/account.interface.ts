import { User } from "../models/user";

export class GetAllUsersResponse{
    users: UserResponseModel[];
}

export class UserResponseModel{
    aggregateRootId: string;
    userName: string;
    password: string;
    firstName: string;
    lastName: string;
}