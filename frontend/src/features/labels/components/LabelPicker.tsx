import { useMemo, useState } from "react"

import { CloseIcon, LoadingSpinnerIcon } from "@/shared/components"

import { useLabels } from "../hooks/useLabels"
import type { Label } from "../types/label"
import { CreateLabelForm } from "./CreateLabelForm"
import { LabelItem } from "./LabelItem"

type LabelPickerProps = {
  selectedIds: number[]
  onToggle: (label: Label) => Promise<void> | void
  onClose: () => void
}

export const LabelPicker = ({
  selectedIds,
  onToggle,
  onClose,
}: LabelPickerProps) => {
  const { labels: allLabels, isLoading } = useLabels()

  const [createLabelState, setCreateLabelState] = useState(false)

  const labels = useMemo(() => allLabels, [allLabels])

  return (
    <div className="absolute bg-gray-100 drop-shadow-lg dark:bg-gray-900 p-4 mt-2 w-56 rounded-md shadow-lg min-h-30 items-center">
      {!createLabelState ? (
        <>
          <p className="text-sm pb-2 dark:text-gray-300">Labels</p>

          {isLoading && (
            <div className="flex justify-center">
              <LoadingSpinnerIcon />
            </div>
          )}

          <CloseIcon
            className="size-5 absolute -right-2 -top-2 text-white bg-amber-500 dark:bg-amber-700 rounded-full cursor-pointer"
            onClick={() => {
              onClose()
            }}
          />

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
            className="mt-5 py-1.5 px-3 font-semibold text-white bg-amber-500 hover:bg-amber-600 dark:bg-amber-700 dark:hover:bg-amber-800 rounded-sm text-sm cursor-pointer"
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
