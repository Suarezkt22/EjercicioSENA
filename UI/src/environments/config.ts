import { createApiClient } from "../app/core/utils/utils";


const STUDENTS_SERVICES = {
    STUDENTS_BASE_URL : 'http://localhost:5270/api/v1/students/',
    PROGRAMS_BASE_URL : 'http://localhost:5270/api/v1/programs/',
    COURSES_BASE_URL : 'http://localhost:5270/api/v1/courses/',
}

const STUDENTS_ENDPOINTS = {
    APPLY_PROGRAM : "apply-program",
    DELETE_STUDENT : "delete",
    GET_ENROLLED_COURSES : "enrolled-courses",
    ENROLL_COURSES : "enroll-courses",
    GET_CLASSMATES : "classmates",
    REGISTER_STUDENT : "register"
} as const

const PROGRAMS_ENDPOINTS = {
    GET_ALL_PROGRAMS : "all",
} as const

const COURSES_ENDPOINTS = {
    GET_COURSES_BY_PROGRAM : "per-program"
} as const

const STUDENTS_API = createApiClient(STUDENTS_SERVICES.STUDENTS_BASE_URL, STUDENTS_ENDPOINTS);
const PROGRAMS_API = createApiClient(STUDENTS_SERVICES.PROGRAMS_BASE_URL, PROGRAMS_ENDPOINTS);
const COURSES_API = createApiClient(STUDENTS_SERVICES.COURSES_BASE_URL, COURSES_ENDPOINTS);

export { STUDENTS_API , PROGRAMS_API, COURSES_API }