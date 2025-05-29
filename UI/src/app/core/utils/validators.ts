import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { GetCoursesResponse } from '../models/student.model';

export function sameTeacherValidator(courses: GetCoursesResponse[]): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const selectedCourseIds = control.value as number[];
    if (!selectedCourseIds || selectedCourseIds.length < 2) return null;

    const selectedTeachers = new Set<number>();

    for (const courseId of selectedCourseIds) {
      const course = courses.find((c) => c.courseId === courseId);
      if (course?.teacher?.teacherId) {
        if (selectedTeachers.has(course.teacher.teacherId)) {
          return { sameTeacher: true };
        }
        selectedTeachers.add(course.teacher.teacherId);
      }
    }

    return null;
  };
}