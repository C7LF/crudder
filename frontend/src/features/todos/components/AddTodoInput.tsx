import { useState } from "react"

import { AddIcon } from "@/shared/components"

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
        <input
          type="text"
          autoFocus
          className="block w-full rounded-md 
                     bg-white dark:bg-gray-800 
                     text-gray-900 dark:text-gray-100 
                     px-3 py-1.5 text-base 
                     placeholder:text-gray-500 dark:placeholder:text-gray-400
                     outline-1 -outline-offset-1 outline-white/10 dark:outline-gray-700
                     focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-500 
                     sm:text-sm/6"
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
