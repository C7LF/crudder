import { useMutation,useQueryClient } from "@tanstack/react-query"

import { deleteTodo } from "../services/todos"
import type { Todo } from "../types/todo"

export const useDeleteTodo = () => {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: deleteTodo,
    onMutate: async (id: number) => {
      await queryClient.cancelQueries({ queryKey: ["todos"] })
      const previousTodos = queryClient.getQueryData<Todo[]>(["todos"])

      if (previousTodos) {
        queryClient.setQueryData<Todo[]>(
          ["todos"],
          previousTodos.filter((t) => t.id !== id)
        )
      }

      return { previousTodos }
    },
    onError: (_err, _id, context) => {
      if (context?.previousTodos) {
        queryClient.setQueryData(["todos"], context.previousTodos)
      }
    }
  })
}
