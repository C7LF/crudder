import { type ReactNode,useEffect, useState } from "react"

import { AuthContext } from "@/features/auth/context/AuthContext"
import api from "@/shared/lib/axios"

interface AuthProviderProps {
  children: ReactNode
}

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false)
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    const checkAuth = async () => {
      try {
        await api.post("/auth/refresh")
        setIsAuthenticated(true)
      } catch {
        setIsAuthenticated(false)
      } finally {
        setLoading(false)
      }
    }

    checkAuth()
  }, [])

  if (loading) return <p>Loading...</p>

  return (
    <AuthContext.Provider
      value={{
        isAuthenticated,
        setIsAuthenticated,
        loading,
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}
