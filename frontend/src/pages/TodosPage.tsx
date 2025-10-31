import {
  AddTodoInput,
  CompletedTodos,
  TodoList,
  TodosHeader,
  useTodos,
} from "@/features/todos"

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
