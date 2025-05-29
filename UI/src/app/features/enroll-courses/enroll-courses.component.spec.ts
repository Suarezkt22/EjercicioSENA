import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrollCoursesComponent } from './enroll-courses.component';

describe('EnrollCoursesComponent', () => {
  let component: EnrollCoursesComponent;
  let fixture: ComponentFixture<EnrollCoursesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EnrollCoursesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EnrollCoursesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
