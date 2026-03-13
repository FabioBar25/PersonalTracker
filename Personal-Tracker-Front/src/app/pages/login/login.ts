import { Component, inject, signal } from '@angular/core'
import { FormsModule } from '@angular/forms'
import { Router } from '@angular/router'
import { AuthStore } from '../../core/auth/auth-store'

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.html'
})
export class LoginComponent {

  private authStore = inject(AuthStore)
  private router = inject(Router)

  email = signal('')
  password = signal('')

  login() {
    this.authStore.login({
      email: this.email(),
      password: this.password()
    })

    this.router.navigate(['/tasks'])
  }

}