import type { Todo } from "../types/todo"
import { KebabMenu } from "./KebabMenu"
import { useDeleteTodo } from "../hooks/useDeleteTodo"
import { useUpdateTodo } from "../hooks/useUpdateTodo"
import { KebabHorizontal } from "./icons/KebabHorizontal"
import { Tick } from "./icons/Tick"
import { useState } from "react"
import { Modal } from "./Modal"

export const TodoItem = ({ todo }: { todo: Todo }) => {
  const [popupOpen, setPopupOpen] = useState(false)

  const updateMutation = useUpdateTodo()
  const deleteMutation = useDeleteTodo()

  return (
    <div className="relative flex mb-2 shadow-sm group w-full overflow-hidden rounded-sm">
      <div
        onClick={() => setPopupOpen(true)}
        className="absolute left-0 top-0 bottom-0 w-6 bg-indigo-300 flex justify-center items-center
               rounded-l-sm transform -translate-x-full opacity-0
               group-hover:translate-x-0 group-hover:opacity-100
               transition-all duration-300 ease-out z-10 hover:cursor-pointer"
      >
        <KebabHorizontal />
      </div>

      <div className="flex grow items-center justify-between bg-gradient-to-l from-gray-100 to-blue-100 p-5 w-full rounded-sm">
        <p className="transition-transform duration-300 ease-out group-hover:translate-x-6">
          {todo.title}
        </p>
        <div className="flex space-x-2">
          <div className="inline-flex items-center">
            <label className="flex items-center cursor-pointer relative">
              <input
                type="checkbox"
                className="peer h-6 w-6 cursor-pointer transition-all appearance-none rounded-full bg-slate-100 shadow hover:shadow-md border border-slate-300 checked:bg-slate-800 checked:border-slate-800"
                id="check-custom-style"
                checked={todo.completed}
                onChange={() =>
                  updateMutation.mutate({
                    ...todo,
                    completed: !todo.completed,
                  })
                }
              />
              <span className="absolute text-white opacity-0 peer-checked:opacity-100 top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2">
                <Tick />
              </span>
            </label>
          </div>
          <KebabMenu
            items={[
              {
                label: "Delete",
                onClick: () => deleteMutation.mutate(todo.id),
              },
            ]}
          />
        </div>
      </div>

      <Modal isOpen={popupOpen} onClose={() => setPopupOpen(false)}>
        <h2 className="text-lg font-semibold mb-4">{todo.title}</h2>
        <p className="text-gray-700 mb-4">
          This is a full-page modal popup for this todo.
        </p>
        <button
          className="px-4 py-2 bg-indigo-500 text-white rounded hover:bg-indigo-600"
          onClick={() => setPopupOpen(false)}
        >
          Close
        </button>
      </Modal>
    </div>
  )
}
