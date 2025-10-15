import { useQuery } from "@tanstack/react-query"
import type { Label } from "../types/label"
import { getLabels } from "../services/labels"

export const useLabels = () => {
  return useQuery<Label[]>({
    queryKey: ["labels"],
    queryFn: getLabels,
    staleTime: 5 * 60 * 1000,
  })
}
