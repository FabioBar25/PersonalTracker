import { Component, inject, signal } from '@angular/core'
import { TaskStore } from '../task-store'

@Component({
  selector: 'app-task-create',
  template: `
  <input
    [value]="title()"
    (input)="title.set($any($event.target).value)"
    placeholder="New task"
  />

  <button (click)="create()">Add</button>
  `
})
export class TaskCreateComponent {

  store = inject(TaskStore)

  title = signal('')

  create() {
    if (!this.title()) return

    this.store.addTask({ title: this.title() })

    this.title.set('')
  }

}
