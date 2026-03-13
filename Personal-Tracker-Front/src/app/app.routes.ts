import { Routes } from '@angular/router'
import { TaskListComponent } from './features/tasks/task-list/task-list'
import { LoginComponent } from './pages/login/login'
import { RegisterComponent } from './pages/register/register'

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/tasks',
    pathMatch: 'full'
  },
  {
    path: 'tasks',
    component: TaskListComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  }
]