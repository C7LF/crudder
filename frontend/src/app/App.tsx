import { Route, Routes } from "react-router-dom"

import { WithAuth } from "@/features/auth"
import { LoginPage, TodosPage } from "@/pages"

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
