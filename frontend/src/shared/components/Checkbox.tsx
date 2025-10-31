import { TickIcon } from "./icons"

type CheckBoxProps = {
  checked: boolean
  onChange: () => void
}

export const Checkbox = ({ checked, onChange }: CheckBoxProps) => (
  <label className="relative flex items-center cursor-pointer">
    <input
      type="checkbox"
      className="peer h-6 w-6 cursor-pointer appearance-none rounded-full bg-slate-100 dark:bg-gray-600 shadow hover:shadow-md border border-slate-300 dark:border-gray-500 checked:bg-slate-800 dark:checked:bg-gray-900 checked:border-slate-800 dark:checked:border-gray-900"
      checked={checked}
      onChange={onChange}
    />
    <span className="absolute opacity-0 peer-checked:opacity-100 top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 text-white dark:text-gray-300">
      <TickIcon />
    </span>
  </label>
)
