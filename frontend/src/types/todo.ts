import type { Label } from "./label"

export interface Todo {
    id: number,
    title: string,
    completed: boolean
    labels?: Label[]
}

export interface UpdateTodoPayload {
  id: number
  title?: string
  completed?: boolean
  labelIds?: number[]
}