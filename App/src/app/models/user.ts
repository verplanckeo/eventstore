export class User{
    id: string;
    token: string;
    userName: string;
    password: string;
    firstName: string;
    lastName: string;
    isDeleting: boolean;

    constructor(){}

    public static CreateUser(
        userName: string,
        password: string,
        firstName: string,
        lastName: string,
        id?: string): User{
        let user =  new User();

        if(id){
            user.id = id;
        }

        user.userName = userName;
        user.password = password;
        user.firstName = firstName;
        user.lastName = lastName;
        user.isDeleting = false;

        return user;
    }
}
