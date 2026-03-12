import { HttpClient } from '@angular/common/http'
import { inject, Injectable } from '@angular/core'
import { Task } from '../../models/task.model'

@Injectable({ providedIn: 'root' })
export class TaskApi {

  private http = inject(HttpClient)
  private baseUrl = '/api/tasks'

  getTasks() {
    return this.http.get<Task[]>(this.baseUrl)
  }

  createTask(title: string) {
    return this.http.post<Task>(this.baseUrl, { title })
  }

  completeTask(id: number) {
    return this.http.put(`${this.baseUrl}/${id}/complete`, {})
  }

}
