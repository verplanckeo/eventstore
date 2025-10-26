import { environment } from '../../environments/environment';

export const localStorageKeys = {
    user: 'user',
    users: 'users'
};

export const apiUrls = {
    user: {
        login: `${environment.apiUrl}/api/users/authenticate`,
        register: `${environment.apiUrl}/api/users/register`,
        users: `${environment.apiUrl}/api/users`
    }
};

export const applicationUrls = {
    account: {
        login: ['/account/login']
    },
    users: ['/users'],
    root: ['/']
}
