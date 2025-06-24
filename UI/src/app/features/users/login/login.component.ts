import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { firstValueFrom } from 'rxjs';
import { UserForm, UserPayload } from '../../../core/models/user.model';
import { extractErrorMessage } from '../../../core/utils/utils';
import { SessionService } from '../../../core/services/session.service';
import { IUserService, USERS_SERVICE } from '../../../core/interfaces/user-service.interface';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  // Public properties
  public loginForm!: FormGroup<UserForm>;
  public submitted = false;
  public isLoading = false;

  constructor(
    private readonly fb: FormBuilder,
    private readonly router: Router,
    private readonly sessionService: SessionService,
    private readonly toastr: ToastrService,

    @Inject(USERS_SERVICE)
    private readonly userService: IUserService,
  ) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  // Form initialization
  private initializeForm(): void {
    this.loginForm = this.fb.group<UserForm>({
      email: this.fb.control('', Validators.required),
      password: this.fb.control('', Validators.required),
    });
  }

  // Form submission
  public async onSubmit(): Promise<void> {
    this.submitted = true;

    if (this.loginForm.invalid) {
      this.toastr.error('Por favor, completa todos los campos requeridos.');
      return;
    }

    await this.fetchLoginUser();
  }

  // API call to login user
  private async fetchLoginUser(): Promise<void> {
    try {
      this.isLoading = true;

      const result = await firstValueFrom(this.userService.login(this.userPayload));

      this.sessionService.setToken(result.data.token);
      
      this.redirectToProductsPage();
    } catch (error) {
      this.toastr.error(extractErrorMessage(error));
    } finally {
      this.isLoading = false;
    }
  }

  // Navigation
  private redirectToProductsPage(): void {
    this.router.navigate(['/products']);
  }

  public redirectToRegisterPage(): void {
    this.router.navigate(['/register']);
  }

  // Getters
  get inputs() {
    return this.loginForm.controls;
  }

  private get userPayload(): UserPayload {
    const { email, password } = this.inputs;

    return {
      email: email.value as string,
      password: password.value as string,
    };
  }
}
