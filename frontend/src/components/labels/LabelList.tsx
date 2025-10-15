import { useState } from "react"
import type { Label } from "../../types/label"

// default colour options
const defaultLabels: Label[] = [
  { text: "", colour: "#ffffff" },
  { text: "", colour: "#ff0000" },
  { text: "", colour: "#00ff00" },
  { text: "", colour: "#0000ff" },
]

type LabelListProps = {
  labels?: Label[]
  onChange?: (selected: Label[]) => void
}

const getContrastTextColor = (hexColor: string) => {
  const hex = hexColor.replace("#", "")
  const r = parseInt(hex.substring(0, 2), 16)
  const g = parseInt(hex.substring(2, 4), 16)
  const b = parseInt(hex.substring(4, 6), 16)
  const brightness = 0.299 * r + 0.587 * g + 0.114 * b
  return brightness > 186 ? "#000000" : "#FFFFFF"
}

export const LabelList = ({ labels = [], onChange }: LabelListProps) => {
  // start with whatever the todo currently has selected
  const [selected, setSelected] = useState<Label[]>(labels)

  // toggle selection by colour
  const toggleLabel = (label: Label) => {
    const isSelected = selected.some((l) => l.colour === label.colour)
    const newSelected = isSelected
      ? selected.filter((l) => l.colour !== label.colour)
      : [...selected, label]

    setSelected(newSelected)
    onChange?.(newSelected)
  }

  // combine defaults + any labels from todo that aren’t in defaults
  const mergedLabels = [
    ...labels.filter((l) => !defaultLabels.some((d) => d.colour === l.colour)),
    ...defaultLabels,
  ]

  return (
    <ul>
      {mergedLabels.map((label) => {
        const checked = selected.some((l) => l.colour === label.colour)
        const textColor = getContrastTextColor(label.colour)
        const displayText =
          labels.find((l) => l.colour === label.colour)?.text || ""

        return (
          <li key={label.colour} className="flex items-center gap-1 w-full mb-1.5">
            <input
              type="checkbox"
              checked={checked}
              onChange={() => toggleLabel(label)}
            />
            <div
              className="w-full flex items-center pl-2 font-light h-7 text-sm rounded-md"
              style={{
                backgroundColor: label.colour,
                color: textColor,
              }}
            >
              {displayText}
            </div>
          </li>
        )
      })}
    </ul>
  )
}
