import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseClassmatesComponent } from './course-classmates.component';

describe('CourseClassmatesComponent', () => {
  let component: CourseClassmatesComponent;
  let fixture: ComponentFixture<CourseClassmatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CourseClassmatesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CourseClassmatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
