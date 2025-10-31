import { useState } from "react"

import { Input } from "@/shared/components"

import { useLabels } from "../hooks/useLabels"
import type { CreateLabelPayload } from "../types/label"

type CreateLabelFormProps = {
  onCancel: () => void
  onCreated: () => void
}

export const CreateLabelForm = ({
  onCancel,
  onCreated,
}: CreateLabelFormProps) => {
  const [newLabel, setNewLabel] = useState<CreateLabelPayload>({
    text: "",
    colour: "#000000",
  })

  const { mutation } = useLabels()

  const handleOnCreate = async () => {
    try {
      await mutation.mutateAsync(newLabel)
      onCreated()
    } catch (err) {
      console.error("Failed to create label", err)
    }
  }

  return (
    <>
      <p className="text-sm pb-2 dark:text-gray-300">Create label</p>

      <Input
        placeholder="text..."
        value={newLabel.text}
        onChange={(e) => setNewLabel({ ...newLabel, text: e.target.value })}
      />

      <div className="flex items-center gap-2 pt-2">
        <label
          htmlFor="color-input"
          className="text-sm font-light dark:text-gray-300"
        >
          Colour
        </label>
        <input
          type="color"
          className="p-1 h-10 w-14 cursor-pointer rounded-lg disabled:opacity-50 disabled:pointer-events-none"
          id="color-input"
          title="Choose your color"
          value={newLabel.colour}
          onChange={(e) => setNewLabel({ ...newLabel, colour: e.target.value })}
        ></input>
      </div>

      <div className="flex justify-between">
        <button
          className="mt-5 py-1.5rounded-sm text-sm cursor-pointer underline underline-offset-3"
          onClick={onCancel}
        >
          Cancel
        </button>
        <button
          className="mt-5 py-1.5 px-3 font-semibold text-white bg-amber-500 hover:bg-amber-600 dark:bg-amber-700 dark:hover:bg-amber-800 rounded-sm text-sm cursor-pointer"
          onClick={handleOnCreate}
          disabled={mutation.status === "pending"}
        >
          {mutation.status === "pending" ? "Creating..." : "Create"}
        </button>
      </div>
    </>
  )
}
