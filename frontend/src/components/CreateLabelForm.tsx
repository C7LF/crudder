import { useState } from "react"
import type { CreateLabelPayload } from "../types/label"

type CreateLabelFormProps = {
  onCancel: () => void
}

export const CreateLabelForm = ({ onCancel }: CreateLabelFormProps) => {
  const [newLabel, setNewLabel] = useState<CreateLabelPayload>({
    text: "",
    colour: "#000000",
  })

  return (
    <>
      <p className="text-sm pb-2 text-gray-300">Create label</p>

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
        placeholder="text..."
        onChange={(e) => setNewLabel({ ...newLabel, text: e.target.value })}
      />

      <div className="flex items-center gap-2 pt-2">
        <label
          htmlFor="color-input"
          className="text-sm font-light text-gray-300"
        >
          Colour
        </label>
        <input
          type="color"
          className="p-1 h-10 w-14 cursor-pointer rounded-lg disabled:opacity-50 disabled:pointer-events-none"
          id="color-input"
          title="Choose your color"
          onChange={(e) => setNewLabel({ ...newLabel, colour: e.target.value })}
        ></input>
      </div>

      <div className="flex justify-between">
        <button
          className="mt-5 py-1.5 px-3 rounded-sm text-sm cursor-pointer"
          onClick={onCancel}
        >
          Cancel
        </button>
        <button className="mt-5 py-1.5 px-3 bg-amber-700 rounded-sm text-sm hover:bg-amber-800 cursor-pointer">
          Create
        </button>
      </div>
    </>
  )
}
