import { Component, inject, signal } from '@angular/core'
import { FormsModule } from '@angular/forms'
import { Router } from '@angular/router'
import { AuthStore } from '../../core/auth/auth-store'

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.html'
})
export class RegisterComponent {

  private authStore = inject(AuthStore)
  private router = inject(Router)

  email = signal('')
  password = signal('')

  register() {

    this.authStore.register({
      email: this.email(),
      password: this.password()
    })

    this.router.navigate(['/tasks'])
  }

}