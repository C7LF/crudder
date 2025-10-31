import { createContext } from "react"

interface AuthContextType {
  isAuthenticated: boolean
  setIsAuthenticated: (value: boolean) => void
  loading: boolean
}

export const AuthContext = createContext<AuthContextType | undefined>(undefined)
