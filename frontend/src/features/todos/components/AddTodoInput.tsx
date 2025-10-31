import { useState } from "react"

import { AddIcon, Input } from "@/shared/components"

export const AddTodoInput = ({
  onCreate,
}: {
  onCreate: (title: string) => void
}) => {
  const [isOpen, setIsOpen] = useState(false)
  const [value, setValue] = useState("")

  const handleSubmit = () => {
    if (value.trim()) {
      onCreate(value.trim())
      setValue("")
      setIsOpen(false)
    }
  }

  return (
    <div className="flex justify-end mb-4 relative gap-2">
      {isOpen && (
        <Input
          placeholder="title..."
          value={value}
          onChange={(e) => setValue(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && handleSubmit()}
        />
      )}
      <button onClick={() => setIsOpen(!isOpen)}>
        <AddIcon
          className={`size-11 hover:cursor-pointer transition-transform duration-300 ${
            isOpen && "rotate-45"
          }`}
        />
      </button>
    </div>
  )
}
