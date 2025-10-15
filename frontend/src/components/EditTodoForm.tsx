import { useState } from "react"
import type { Todo } from "../types/todo"
import { useUpdateTodo } from "../hooks/useUpdateTodo"
import { useDebounce } from "../hooks/useDebounce"
import { Label } from "./icons/Label"
import type { Label as LabelType } from "../types/label"
import { useLabels } from "../hooks/useLabels"
import { useCreateLabel } from "../hooks/useCreateLabel"

export const EditTodoForm = ({ todo }: { todo: Todo }) => {
  const [title, setTitle] = useState(todo.title)
  const [labelBoxOpen, setLabelBoxOpen] = useState(false)
  const [selectedIds, setSelectedIds] = useState<number[]>(
    todo.labels
      ?.map((l) => l.id)
      .filter((id): id is number => id !== undefined) || []
  )

  const { data: allLabels = [] } = useLabels()
  const createLabelMutation = useCreateLabel()
  const updateMutation = useUpdateTodo()

  const debouncedUpdate = useDebounce((newTitle: string) => {
    updateMutation.mutate({
      ...todo,
      title: newTitle,
    })
  }, 500)

  const handleLabelToggle = async (label: LabelType) => {
    let labelId = label.id

    if (!labelId) {
      const created = await createLabelMutation.mutateAsync({
        text: label.text,
        colour: label.colour,
      })
      labelId = created.id
    }

    setSelectedIds((prev) =>
      prev.includes(labelId!)
        ? prev.filter((id) => id !== labelId)
        : [...prev, labelId!]
    )

    updateMutation.mutate({
      ...todo,
      labelIds: selectedIds.includes(labelId!)
        ? selectedIds.filter((id) => id !== labelId)
        : [...selectedIds, labelId!],
    })
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
        <div className="absolute bg-gray-900 p-4 mt-2 w-56 rounded-md shadow-lg h-80 overflow-y-scroll">
          <p className="text-sm pb-2 text-gray-300 ">Labels</p>

          <ul>
            {allLabels.map((label) => {
              const isSelected = selectedIds.includes(label.id!)
              return (
                <li key={label.id} className="mb-1 flex items-center gap-2">
                  <input
                    type="checkbox"
                    checked={isSelected}
                    onChange={() => handleLabelToggle(label)}
                  />
                  <div
                    className="flex flex-grow pl-2 items-center h-8 text-sm rounded"
                    style={{
                      backgroundColor: label.colour,
                      color:
                        label.colour.replace("#", "") > "888888"
                          ? "#000"
                          : "#fff",
                    }}
                  >
                    {label.text}
                  </div>
                </li>
              )
            })}
          </ul>
        </div>
      )}
    </>
  )
}
