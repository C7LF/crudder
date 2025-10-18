import { useState } from "react"
import type { Todo } from "../types/todo"
import { useUpdateTodo } from "../hooks/useUpdateTodo"
import { useDebounce } from "../hooks/useDebounce"
import { Label } from "./icons/Label"
import type { Label as LabelType } from "../types/label"
import { LabelPicker } from "./LabelPicker"

export const EditTodoForm = ({ todo }: { todo: Todo }) => {
  const [title, setTitle] = useState(todo.title)
  const [labelBoxOpen, setLabelBoxOpen] = useState(false)
  const [selectedIds, setSelectedIds] = useState<number[]>(
    todo.labels?.map((l) => l.id)
      || [])

  const updateMutation = useUpdateTodo()

  const debouncedUpdate = useDebounce((newTitle: string) => {
    updateMutation.mutate({
      ...todo,
      title: newTitle,
    })
  }, 500)

  const handleLabelToggle = async (label: LabelType) => {
    // compute the new selected ids deterministically from current state
    const newSelectedIds = selectedIds.includes(label.id)
      ? selectedIds.filter((id) => id !== label.id)
      : [...selectedIds, label.id]

    // update local state and remote
    setSelectedIds(newSelectedIds)
    updateMutation.mutate({ ...todo, labelIds: newSelectedIds })
  }

  return (
    <>
      <input
        type="text"
        value={title}
        onChange={(e) => {
          setTitle(e.target.value)
          debouncedUpdate(e.target.value)
        }}
        className="w-full px-3 py-2 mb-4 border rounded"
      />

      <button
        onClick={() => setLabelBoxOpen(!labelBoxOpen)}
        className="flex items-center gap-1 py-1.5 px-3 border border-gray-400 rounded-full text-sm text-gray-400 hover:bg-gray-700 cursor-pointer"
      >
        <Label />
        Labels
      </button>

      {labelBoxOpen && (
        <LabelPicker selectedIds={selectedIds} onToggle={handleLabelToggle} />
      )}
    </>
  )
}
