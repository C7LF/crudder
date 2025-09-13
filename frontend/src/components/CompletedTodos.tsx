import { useState } from "react"
import { ChevronDown } from "./icons/ChevronDown"
import { TodoItem } from "./TodoItem"
import type { Todo } from "../types/todo"

export const CompletedTodos = ({ todos }: { todos?: Todo[] }) => {
  const [isOpen, setIsOpen] = useState(false)

  return (
    <div className="mt-12">
      <button
        type="button"
        onClick={() => setIsOpen(!isOpen)}
        className="flex items-center gap-2 py-2 font-semibold hover:cursor-pointer"
        aria-expanded={isOpen}
      >
        Completed
        <ChevronDown
          className={`size-4 transition-transform duration-300 ${
            isOpen ? "rotate-180" : ""
          }`}
        />
      </button>

      <div
        className={`overflow-hidden transition-[max-height] duration-300 ease-in-out ${
          isOpen ? "max-h-screen" : "max-h-0"
        }`}
      >
        {todos?.map((todo) => (
          <TodoItem key={todo.id} todo={todo} />
        ))}
      </div>
    </div>
  )
}
