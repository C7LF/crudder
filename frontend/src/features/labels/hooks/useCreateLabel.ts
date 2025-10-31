import { useMutation, useQueryClient } from "@tanstack/react-query"

import { createLabel } from "../services/labels"
import type { CreateLabelPayload } from "../types/label"

export const useCreateLabel = () => {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: (label: CreateLabelPayload) => createLabel(label),
    onSuccess: (newLabel) => {
      queryClient.setQueryData<CreateLabelPayload[]>(["labels"], (old = []) => [
        ...old,
        newLabel,
      ])
    },
  })
}
