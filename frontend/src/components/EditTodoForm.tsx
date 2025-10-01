import { useState } from "react"
import type { Todo } from "../types/todo"
import { useUpdateTodo } from "../hooks/useUpdateTodo"
import { useDebounce } from "../hooks/useDebounce"

export const EditTodoForm = ({
  todo,
  onClose,
}: {
  todo: Todo
  onClose: () => void
}) => {
  const [title, setTitle] = useState(todo.title)
  const updateMutation = useUpdateTodo()

  const debouncedUpdate = useDebounce((newTitle: string) => {
    updateMutation.mutate({
      ...todo,
      title: newTitle,
    })
  }, 500)

  return (
    <div>
      <input
        type="text"
        value={title}
        onChange={(e) => {
          setTitle(e.target.value)
          debouncedUpdate(e.target.value)
        }}
        className="w-full px-3 py-2 mb-4 border-0 active:border-gray-300 rounded"
      />

      <div className="flex justify-end space-x-2">
        <button
          className="px-4 py-2 bg-gray-200 rounded hover:bg-gray-300"
          onClick={onClose}
        >
          Cancel
        </button>
        <button
          className="px-4 py-2 bg-indigo-500 text-white rounded hover:bg-indigo-600"
          onClick={() => {
            updateMutation.mutate({
              ...todo,
              title,
            })
            onClose()
          }}
        >
          Save
        </button>
      </div>
    </div>
  )
}
