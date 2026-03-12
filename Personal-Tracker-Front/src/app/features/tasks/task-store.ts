import { inject } from '@angular/core'
import {
  signalStore,
  withState,
  withMethods,
  patchState
} from '@ngrx/signals'

import {
  withEntities,
  setAllEntities,
  addEntity,
  updateEntity
} from '@ngrx/signals/entities'

import { rxMethod } from '@ngrx/signals/rxjs-interop'
import { pipe, switchMap, tap, map } from 'rxjs'
import { TaskApi } from '../../core/api/task-api'
import { Task } from '../../models/task.model'

type TaskState = {
  loading: boolean
}

export const TaskStore = signalStore(

  { providedIn: 'root' },

  withState<TaskState>({
    loading: false
  }),

  withEntities<Task>(),

  withMethods((store, api = inject(TaskApi)) => ({

    loadTasks: rxMethod<void>(
      pipe(
        tap(() => patchState(store, { loading: true })),

        switchMap(() => api.getTasks()),

        tap((tasks) => {
          patchState(store, setAllEntities(tasks))
          patchState(store, { loading: false })
        })
      )
    ),

    createTask: rxMethod<string>(
      pipe(
        switchMap(title => api.createTask(title)),

        tap(task => {
          patchState(store, addEntity(task))
        })
      )
    ),

    completeTask: rxMethod<number>(
      pipe(
        switchMap(id => api.completeTask(id).pipe(
          map(() => id)
        )),

        tap((id: number) => {
          patchState(store,
            updateEntity({
              id,
              changes: { isCompleted: true }
            })
          )
        })
      )
    )

  }))
)
