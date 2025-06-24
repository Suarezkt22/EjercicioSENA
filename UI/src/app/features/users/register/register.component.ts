import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserForm, UserPayload } from '../../../core/models/user.model';
import { firstValueFrom } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { extractErrorMessage } from '../../../core/utils/utils';
import { Router } from '@angular/router';
import { USERS_SERVICE, IUserService } from '../../../core/interfaces/user-service.interface';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  // Public properties
  public registerForm!: FormGroup<UserForm>;
  public submitted = false;
  public isLoading = false;

  constructor(
    private readonly fb: FormBuilder,
    private readonly router: Router,
    private readonly toastr: ToastrService,

    @Inject(USERS_SERVICE)
    private readonly userService: IUserService,
        
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  // Form initialization
  private initializeForm(): void {
    this.registerForm = this.fb.group<UserForm>({
      email: this.fb.control('', Validators.required),
      password: this.fb.control('', Validators.required),
    });
  }

  // Form submission
  public async onSubmit(): Promise<void> {
    this.submitted = true;
     
    if (this.registerForm.invalid) {
      this.toastr.error('Por favor, completa todos los campos requeridos.');
      return;
    }

    await this.fetchRegisterUser();
  }

  // API call to register user
  private async fetchRegisterUser(): Promise<void> {
    try {
      this.isLoading = true;
      const result = await firstValueFrom(this.userService.register(this.userPayload));
      this.toastr.success(result.message)
      this.redirectToLoginPage();
    } catch (error) {
      this.toastr.error(extractErrorMessage(error));
    }finally{
      this.isLoading = false;
    }
  }

  // Navigation
  public redirectToLoginPage(): void {
    this.router.navigate(['/']);
  }

  // Getters
  get inputs() {
    return this.registerForm.controls;
  }

  private get userPayload(): UserPayload {
    const { email, password } = this.inputs;

    return {
      email: email.value as string,
      password: password.value as string,
    };
  }
}
