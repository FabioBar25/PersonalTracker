import { Component, inject, OnInit } from '@angular/core'
import { TaskStore } from '../task-store'

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.html'
})
export class TaskListComponent implements OnInit {

  store = inject(TaskStore)

  tasks = this.store.entities
  loading = this.store.loading

  ngOnInit() {
    this.store.loadTasks()
  }

  completeTask(id: number) {
    this.store.completeTask(id)
  }
}
