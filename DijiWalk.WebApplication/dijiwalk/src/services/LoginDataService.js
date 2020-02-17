import http from "./http-login";

class LoginDataService {

    path = "/token/organizer";

    login(login, password) {
        return http.post(this.path, { Login: login, Password: password });
    }
}

export default new LoginDataService();
