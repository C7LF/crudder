import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query"

import { createTodo, getTodos } from "../services/todos"
import type { Todo } from "../types/todo"

export const useTodos = () => {
  const queryClient = useQueryClient()

  const { data: todos, ...query } = useQuery({
    queryKey: ["todos"],
    queryFn: getTodos,
  })

  const mutation = useMutation({
    mutationFn: createTodo,
    onMutate: async (newTitle: string) => {
      await queryClient.cancelQueries({ queryKey: ["todos"] })

      const previousTodos = queryClient.getQueryData<Todo[]>(["todos"])

      const optimisticTodo: Todo = {
        id: -Date.now(),
        title: newTitle,
        completed: false,
      }

      // update cache immediately
      queryClient.setQueryData<Todo[]>(["todos"], (old = []) => [
        ...old,
        optimisticTodo,
      ])

      // return snapshot + temp id for onSuccess replacement
      return { previousTodos, optimisticId: optimisticTodo.id }
    },
    onSuccess: (serverTodo, _newTitle, context) => {
      // replace temp todo with real one
      queryClient.setQueryData<Todo[]>(["todos"], (old = []) =>
        old
          ? old.map((t) => (t.id === context?.optimisticId ? serverTodo : t))
          : [serverTodo]
      )
    },
    onError: (_err, _newTitle, context) => {
      // rollback if server fails
      if (context?.previousTodos) {
        queryClient.setQueryData(["todos"], context.previousTodos)
      }
    },
    onSettled: () => {
      // always refetch for absolute consistency
      queryClient.invalidateQueries({ queryKey: ["todos"] })
    },
  })

  return { todos, mutation, ...query }
}
