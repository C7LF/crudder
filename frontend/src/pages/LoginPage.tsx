import { LoginForm } from "../components/LoginForm"
// import { useAuth } from "../hooks/useAuth"

export const LoginPage = () => {
//   const { logout } = useAuth()

  return (
    <div className="fancy-bg min-h-screen flex flex-col justify-center">
      <LoginForm />
      {/* <button
        onClick={logout}
        className="px-4 py-2 bg-red-500 text-white rounded"
      >
        Logout
      </button> */}
    </div>
  )
}
