import { signalStore, withState, withMethods, patchState } from '@ngrx/signals'
import { inject } from '@angular/core'
import { AuthApi } from '../api/auth-api'
import { rxMethod } from '@ngrx/signals/rxjs-interop'
import { pipe, switchMap, tap } from 'rxjs'

export const AuthStore = signalStore(

  { providedIn: 'root' },

  withState({
    token: localStorage.getItem('token')
  }),

  withMethods((store, api = inject(AuthApi)) => ({

    login: rxMethod<{email:string,password:string}>(
      pipe(
        switchMap(credentials => api.login(credentials)),

        tap(token => {
          localStorage.setItem('token', token)
          patchState(store, { token })
        })
      )
    ),
    register: rxMethod<{ email: string; password: string }>(
      pipe(
        switchMap(credentials => api.register(credentials)),

        tap(token => {
          localStorage.setItem('token', token)
          patchState(store, { token })
      }))
    )
  }
)))