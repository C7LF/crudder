import api from "@/shared/lib/axios"

import type { Todo, UpdateTodoPayload } from "../types/todo"

export const getTodos = async () => {
  const res = await api.get<Todo[]>("/todos")
  return res.data
}

export const createTodo = async (title: string) => {
  const res = await api.post("/todos", { title })
  return res.data
}

export const updateTodo = async (todo: UpdateTodoPayload) => {
  const res = await api.put(`/todos/${todo.id}`, todo)
  return res.data
}

export const deleteTodo = async (id: number) => {
  await api.delete(`/todos/${id}`)
}
