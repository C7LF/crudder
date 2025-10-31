import { Checkbox } from "@/shared/components"

import type { Label } from "../types/label"
import { getContrastColor } from "../utils/color"

type LabelItemProps = {
  label: Label
  checked: boolean
  onChange: () => void
}

export const LabelItem = ({ label, checked, onChange }: LabelItemProps) => {
  return (
    <li className="mb-1 flex items-center gap-2">
      <Checkbox checked={checked} onChange={onChange} />
      <div
        className="flex flex-grow pl-2 items-center h-8 text-sm rounded"
        style={{
          backgroundColor: label.colour,
          color: getContrastColor(label.colour ?? "#000000"),
        }}
      >
        {label.text}
      </div>
    </li>
  )
}
