import { useMemo, useState } from "react"
import type { Label as LabelType } from "../types/label"
import { useLabels } from "../hooks/useLabels"
import { LabelItem } from "./LabelItem"
import { LoadingSpinner } from "./icons/LoadingSpinner"
import { CreateLabelForm } from "./CreateLabelForm"

type LabelPickerProps = {
  selectedIds: number[]
  onToggle: (label: LabelType) => Promise<void> | void
}

export const LabelPicker = ({ selectedIds, onToggle }: LabelPickerProps) => {
  const { data: allLabels, isLoading } = useLabels()

  const [createLabelState, setCreateLabelState] = useState(false)

  // memoize labels to avoid unnecessary re-renders
  const labels = useMemo(() => allLabels, [allLabels])

  return (
    <div className="absolute bg-gray-900 p-4 mt-2 w-56 rounded-md shadow-lg min-h-30 overflow-y-scroll items-center">
      {createLabelState ? (
        <>
          <p className="text-sm pb-2 text-gray-300 ">Labels</p>

          {isLoading && (
            <div className="flex justify-center">
              <LoadingSpinner />
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
        </>
      ) : (
        <CreateLabelForm />
      )}
    </div>
  )
}
