import { useMutation } from "@tanstack/react-query"
import { useNavigate } from "react-router-dom"

import api from "@/shared/lib/axios"

import { useAuth } from "./useAuth"

export const useLogin = () => {
  const navigate = useNavigate()
  const { setIsAuthenticated } = useAuth()

  return useMutation({
    mutationFn: async (credentials: { email: string; password: string }) => {
      await api.post("/auth/login", credentials)
    },
    onSuccess: () => {
      setIsAuthenticated(true)
      navigate("/")
    },
    onError: () => {
      setIsAuthenticated(false)
    },
  })
}
