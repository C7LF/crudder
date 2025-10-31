import { useMutation } from "@tanstack/react-query"

import api from "@/shared/lib/axios"

import { useAuth } from "./useAuth"

export const useLogout = () => {
  const { setIsAuthenticated } = useAuth()

  return useMutation({
    mutationFn: async () => {
      await api.post("/auth/logout")
    },
    onSuccess: () => {
      setIsAuthenticated(false)
    }
  })
}
