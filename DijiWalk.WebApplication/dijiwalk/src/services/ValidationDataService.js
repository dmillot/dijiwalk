import http from "./http-common";

class ValidationDataService {

    path = "/teamroute/";

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

    valid(data) {
        return http.post(this.path + "valid", data);
    }

    reject(data) {
        return http.post(this.path + "reject", data);
    }
}

export default new ValidationDataService();
