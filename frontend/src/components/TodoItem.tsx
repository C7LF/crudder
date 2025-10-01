import type { Todo } from "../types/todo"
import { KebabMenu } from "./KebabMenu"
import { useDeleteTodo } from "../hooks/useDeleteTodo"
import { useUpdateTodo } from "../hooks/useUpdateTodo"
import { Tick } from "./icons/Tick"
import { useState } from "react"
import { Modal } from "./Modal"
import { EditTodoForm } from "./EditTodoForm"

export const TodoItem = ({ todo }: { todo: Todo }) => {
  const [popupOpen, setPopupOpen] = useState(false)

  const updateMutation = useUpdateTodo()
  const deleteMutation = useDeleteTodo()

  return (
    <div
      className="relative flex mb-2 shadow-sm rounded-sm cursor-pointer"
      onClick={() => setPopupOpen(true)}
    >
      <div className="flex items-center justify-between bg-gray-100 dark:bg-gray-700 p-5 w-full rounded-sm">
        <p className="dark:text-gray-100">{todo.title}</p>
        <div className="flex space-x-2">
          <div
            className="inline-flex items-center"
            onClick={(e) => e.stopPropagation()}
          >
            <label className="flex items-center cursor-pointer relative">
              <input
                type="checkbox"
                className="peer h-6 w-6 cursor-pointer transition-all appearance-none rounded-full 
               bg-slate-100 dark:bg-gray-600
               shadow hover:shadow-md 
               border border-slate-300 dark:border-gray-500
               checked:bg-slate-800 dark:checked:bg-gray-900
               checked:border-slate-800 dark:checked:border-gray-900"
                id="check-custom-style"
                checked={todo.completed}
                onChange={() =>
                  updateMutation.mutate({
                    ...todo,
                    completed: !todo.completed,
                  })
                }
              />
              <span
                className="absolute opacity-0 peer-checked:opacity-100 
               top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2
               text-white dark:text-gray-300"
              >
                <Tick />
              </span>
            </label>
          </div>
          <div onClick={(e) => e.stopPropagation()}>
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
      </div>

      <Modal isOpen={popupOpen} onClose={() => setPopupOpen(false)}>
        <EditTodoForm todo={todo} onClose={() => setPopupOpen(false)} />
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
