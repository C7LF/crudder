import { useMutation, useQueryClient } from "@tanstack/react-query"
import { updateTodo } from "../services/todos"
import type { Todo } from "../types/todo"

export const useUpdateTodo = () => {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: updateTodo,
    onMutate: async (updated: Partial<Todo> & { id: number }) => {
      await queryClient.cancelQueries({ queryKey: ["todos"] })
      const previousTodos = queryClient.getQueryData<Todo[]>(["todos"])

      if (previousTodos) {
        queryClient.setQueryData<Todo[]>(
          ["todos"],
          previousTodos.map((t) =>
            t.id === updated.id ? { ...t, ...updated } : t
          )
        )
      }

      return { previousTodos }
    },
    onError: (_err, _updated, context) => {
      if (context?.previousTodos) {
        queryClient.setQueryData(["todos"], context.previousTodos)
      }
    },
    onSuccess: (serverTodo: Todo) => {
      queryClient.setQueryData<Todo[]>(["todos"], (old) =>
        old ? old.map((t) => (t.id === serverTodo.id ? serverTodo : t)) : []
      )
    }
  })
}
