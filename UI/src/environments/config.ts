import { createApiClient } from "../app/core/utils/utils";


const EMPLOYEES_SERVICES = {
    BASE_URL : 'http://localhost:5270/api/v1/employee/'
}

const EMPLOYEES_ENDPOINTS = {
    GET_ALL_EMPLOYEES : "all",
    GET_BY_ID_EMPLOYEE : "one"
} as const

const EMPLOYEES_API = createApiClient(EMPLOYEES_SERVICES.BASE_URL, EMPLOYEES_ENDPOINTS);

export { EMPLOYEES_API }