import { Route, Routes } from "react-router-dom"
import { WithAuth } from "./components/WithAuth"
import { TodosPage } from "./pages/TodosPage"
import { LoginPage } from "./pages/LoginPage"

function App() {
  return (
      <Routes>
        <Route path="/login" element={<LoginPage />} />

        <Route element={<WithAuth />}>
          <Route path="/" element={<TodosPage />} />
        </Route>
      </Routes>
  )
}

export default App
