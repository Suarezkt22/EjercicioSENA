import { createApiClient } from "../app/core/utils/utils";

const BASE_URL = "http://localhost:5270/api"; 

const SERVICES = {
    PRODUCTS_BASE_URL : `${BASE_URL}/productos/`,
    USERS_BASE_URL : `${BASE_URL}/usuarios/`,
}

const USERS_ENDPOINTS = {
    REGISTER : "register",
    LOGIN : "login",
} as const

const USERS_API = createApiClient(SERVICES.USERS_BASE_URL, USERS_ENDPOINTS);
const PRODUCTS_API = SERVICES.PRODUCTS_BASE_URL

export { PRODUCTS_API, USERS_API }