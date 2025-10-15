import { useMutation, useQueryClient } from "@tanstack/react-query"
import { createLabel } from "../services/labels"
import type { Label } from "../types/label"

export const useCreateLabel = () => {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: (label: Label) => createLabel(label),
    onSuccess: (newLabel) => {
      queryClient.setQueryData<Label[]>(["labels"], (old = []) => [
        ...old,
        newLabel,
      ])
    },
  })
}
