import { TodosHeader } from "../components/TodosHeader"
import { AddTodoInput } from "../components/AddTodoInput"
import { TodoList } from "../components/TodoList"
import { CompletedTodos } from "../components/CompletedTodos"
import { useTodos } from "../hooks/useTodos"

export const TodosPage = () => {
  const { todos, mutation } = useTodos()

  const createNewTodo = (title: string) => {
    mutation.mutate(title)
  }

  const incomplete = todos?.filter((t) => !t.completed)
  const completed = todos?.filter((t) => t.completed)

  return (
    <>
      <TodosHeader count={todos?.length} />

      <div className="container mx-auto -mt-20">
        <AddTodoInput onCreate={createNewTodo} />
        <TodoList todos={incomplete} />
        <CompletedTodos todos={completed} />
      </div>
    </>
  )
}
