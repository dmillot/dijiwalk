import axios from "axios";
import { Cookies } from 'quasar'

export default axios.create({
    baseURL: "/api",
    headers: {
        "Content-type": "application/json",
        "Authorization": `Bearer ${Cookies.get('JWTToken')}`
    }
});
