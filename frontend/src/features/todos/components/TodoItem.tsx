import { useState } from "react"

import { KebabMenu, Modal, TickIcon } from "@/shared/components"

import { useDeleteTodo, useUpdateTodo } from "../hooks"
import type { Todo } from "../types/todo"
import { EditTodoForm } from "./EditTodoForm"

const LabelThumbnail = ({ colour }: { colour: string }) => (
  <div
    className="w-8 h-2.5 mr-2 rounded-full"
    style={{
      backgroundColor: colour,
    }}
  ></div>
)

export const TodoItem = ({ todo }: { todo: Todo }) => {
  const [popupOpen, setPopupOpen] = useState(false)

  const updateMutation = useUpdateTodo()
  const deleteMutation = useDeleteTodo()

  return (
    <>
      <div
        className="relative flex mb-2 shadow-sm rounded-sm cursor-pointer"
        onClick={() => setPopupOpen(true)}
      >
        <div className="flex items-center justify-between bg-gray-100 dark:bg-gray-700 p-5 w-full rounded-sm">
          <div className="flex flex-col flex-grow">
            <p className="dark:text-gray-100">{todo.title}</p>
            {todo.labels && todo.labels.length > 0 && (
              <div className="flex mt-4">
                {todo.labels.map((label) => (
                  <LabelThumbnail key={label.id} colour={label.colour!} />
                ))}
              </div>
            )}
          </div>
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
                  <TickIcon />
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
      </div>

      <Modal isOpen={popupOpen} onClose={() => setPopupOpen(false)}>
        <EditTodoForm todo={todo} />
      </Modal>
    </>
  )
}
