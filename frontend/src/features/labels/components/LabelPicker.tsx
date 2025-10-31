import { useMemo, useState } from "react"

import { LoadingSpinnerIcon } from "@/shared/components"

import { useLabels } from "../hooks/useLabels"
import type { Label } from "../types/label"
import { CreateLabelForm } from "./CreateLabelForm"
import { LabelItem } from "./LabelItem"

type LabelPickerProps = {
  selectedIds: number[]
  onToggle: (label: Label) => Promise<void> | void
}

export const LabelPicker = ({ selectedIds, onToggle }: LabelPickerProps) => {
  const { labels: allLabels, isLoading } = useLabels()

  const [createLabelState, setCreateLabelState] = useState(false)

  const labels = useMemo(() => allLabels, [allLabels])

  return (
    <div className="absolute bg-gray-900 p-4 mt-2 w-56 rounded-md shadow-lg min-h-30 overflow-y-scroll items-center">
      {!createLabelState ? (
        <>
          <p className="text-sm pb-2 text-gray-300">Labels</p>

          {isLoading && (
            <div className="flex justify-center">
              <LoadingSpinnerIcon />
            </div>
          )}

          <ul>
            {labels?.map((label) => {
              const isSelected = selectedIds.includes(label.id!)
              return (
                <LabelItem
                  key={label.id}
                  label={label}
                  checked={isSelected}
                  onChange={() => onToggle(label)}
                />
              )
            })}
          </ul>
          <button
            type="button"
            className="mt-5 py-1.5 px-3 bg-amber-700 rounded-sm text-sm hover:bg-amber-800 cursor-pointer"
            onClick={() => {
              setCreateLabelState(true)
            }}
          >
            New label
          </button>
        </>
      ) : (
        <CreateLabelForm
          onCancel={() => {
            setCreateLabelState(false)
          }}
          onCreated={() => setCreateLabelState(false)}
        />
      )}
    </div>
  )
}
