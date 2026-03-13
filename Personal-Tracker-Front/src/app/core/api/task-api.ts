import { Injectable, inject } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { Task } from '../tasks/task-model'

@Injectable({ providedIn: 'root' })
export class TaskApi {
  private http = inject(HttpClient)
  private baseUrl = 'https://localhost:4200/tasks'

  getTasks() {
    return this.http.get<Task[]>(this.baseUrl)
  }

  createTask(title: string) {
    return this.http.post<Task>(this.baseUrl, { title })
  }

  completeTask(id: string) {
    return this.http.patch<Task>(`${this.baseUrl}/${id}/complete`, {})
  }
}