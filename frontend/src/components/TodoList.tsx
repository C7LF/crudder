import type { Todo } from "../types/todo"
import { TodoItem } from "./TodoItem"

export const TodoList = ({ todos }: { todos?: Todo[] }) => (
  <>
    {todos?.map((todo) => (
      <TodoItem key={todo.id} todo={todo} />
    ))}
  </>
)
