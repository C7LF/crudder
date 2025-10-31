import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query"

import { createLabel, getLabels } from "../services/labels"
import type { CreateLabelPayload,Label } from "../types/label"

export const useLabels = () => {
  const queryClient = useQueryClient()

  const { data: labels, ...query } = useQuery<Label[]>({
    queryKey: ["labels"],
    queryFn: getLabels,
  })

  const mutation = useMutation<Label, unknown, CreateLabelPayload>({
    mutationFn: createLabel,
    onSuccess: (serverLabel: Label) => {
      queryClient.setQueryData<Label[]>(["labels"], (old = []) => [
        ...old,
        serverLabel,
      ])
    },
  })

  return { labels, mutation, ...query }
}
