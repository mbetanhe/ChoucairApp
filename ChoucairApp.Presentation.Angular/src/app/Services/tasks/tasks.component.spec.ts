import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TasksComponentService } from './tasks.component';

describe('TasksComponent', () => {
  let component: TasksComponentService;
  let fixture: ComponentFixture<TasksComponentService>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TasksComponentService]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TasksComponentService);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
