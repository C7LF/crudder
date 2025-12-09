import type { Label } from "@/features/labels"

export interface Todo {
    id: number,
    title: string,
    completed: boolean
    content: string
    labels?: Label[]
}

export interface UpdateTodoPayload {
  id: number
  title?: string
  completed?: boolean
  content?: string
  labelIds?: number[]
}