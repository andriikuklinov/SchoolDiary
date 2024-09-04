export class AuthService {
    private _serverUrl: string;

    constructor(serverUrl: string) {
        this._serverUrl = serverUrl;
    }

    async login(login: string, password: string) {
        try {
            const response = await fetch(`${this._serverUrl}/Auth/Login`, {
                body: JSON.stringify({ login: login, password: password }),
                method: 'POST'
            });

            return response;
        }
        catch (error) {
            console.error('Error during login: ', error);
        }
    }
}