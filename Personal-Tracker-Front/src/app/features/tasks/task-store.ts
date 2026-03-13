import { inject } from '@angular/core'
import { signalStore, withState, withMethods } from '@ngrx/signals'
import { rxMethod } from '@ngrx/signals/rxjs-interop'
import { switchMap, tap } from 'rxjs/operators'
import { Task } from '../../core/tasks/task-model'
import { TaskApi } from '../../core/api/task-api'

type TaskState = {
  tasks: Task[]
}

export const TaskStore = signalStore(
  { providedIn: 'root' },
  withState<TaskState>({ tasks: [] }),
  withMethods((store, api = inject(TaskApi)) => ({
    
    loadTasks: () => {
      api.getTasks().subscribe(tasks => store.setTasks(tasks));
    },

    addTask: ({ title }: { title: string }) => {
      api.createTask(title).subscribe(newTask => store.setTasks([...store.tasks(), newTask]));
    },

    completeTask: ({ id }: { id: string }) => {
      api.completeTask(id).subscribe(updatedTask => {
        store.setTasks(
          store.tasks().map(t => t.id === updatedTask.id ? updatedTask : t)
        );
      });
    }

  }))
)