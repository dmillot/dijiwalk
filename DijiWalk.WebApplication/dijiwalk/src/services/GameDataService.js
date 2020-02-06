import http from "./http-common";

class GameDataService {

    path = "/game/";

    getAll() {
        return http.get(this.path);
    }

    get(id) {
        return http.get(this.path + id);
    }

    create(data) {
        return http.post(this.path, data);
    }

    update(id, data) {
        return http.put(this.path + id, data);
    }

    delete(id) {
        return http.delete(this.path + id);
    }
}

export default new GameDataService();
