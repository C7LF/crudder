import { useMemo } from "react"
import type { Label as LabelType } from "../types/label"
import { useLabels } from "../hooks/useLabels"
import { LabelItem } from "./LabelItem"

type LabelPickerProps = {
  selectedIds: number[]
  onToggle: (label: LabelType) => Promise<void> | void
}

export const LabelPicker = ({ selectedIds, onToggle }: LabelPickerProps) => {
  const { data: allLabels = [] } = useLabels()

  // memoize labels to avoid unnecessary re-renders
  const labels = useMemo(() => allLabels, [allLabels])

  return (
    <div className="absolute bg-gray-900 p-4 mt-2 w-56 rounded-md shadow-lg h-80 overflow-y-scroll">
      <p className="text-sm pb-2 text-gray-300 ">Labels</p>

      <ul>
        {labels.map((label) => {
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
    </div>
  )
}
