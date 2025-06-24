import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { routes } from './app.routes';

import { ToastrModule } from 'ngx-toastr';

import { SessionService } from './core/services/session.service';
import { ProductService } from './core/services/product.service';
import { UserService } from './core/services/user.service';

import { SESSION_SERVICE } from './core/interfaces/session-service.interface';
import { PRODUCTS_SERVICE } from './core/interfaces/product-service.interface';
import { USERS_SERVICE } from './core/interfaces/user-service.interface';
import { AuthInterceptor } from './core/interceptors/token.interceptor';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(routes),
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      progressBar: true,
      closeButton: true,
    }),
  ],
  providers: [
    { provide: SESSION_SERVICE, useClass: SessionService },
    { provide: PRODUCTS_SERVICE, useClass: ProductService },
    { provide: USERS_SERVICE, useClass: UserService },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent], 
})
export class AppModule {}
