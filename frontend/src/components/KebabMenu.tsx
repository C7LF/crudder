import { useEffect, useState, useRef } from "react"
import { KebabButton } from "./KebabButton"
import { Bin } from "./icons/Bin"

type MenuItem = {
  label: string
  onClick: () => void
}

type KebabMenuProps = {
  items: MenuItem[]
}

export const KebabMenu = ({ items }: KebabMenuProps) => {
  const [open, setOpen] = useState<boolean>(false)
  const menuRef = useRef<HTMLDivElement>(null)

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (menuRef.current && !menuRef.current.contains(event.target as Node)) {
        setOpen(false)
      }
    }
    document.addEventListener("mousedown", handleClickOutside)
    return () => document.removeEventListener("mousedown", handleClickOutside)
  }, [])

  return (
    <div className="relative inline-block text-left">
      <KebabButton onClick={() => setOpen(!open)} />

      {open && (
        <div
          ref={menuRef!}
          className="absolute right-10 -top-3 w-40 rounded-xl bg-white shadow-lg border border-gray-200 z-30"
        >
          <ul className="py-1 text-sm text-gray-700">
            {items.map((item, id) => (
              <li
                key={id}
                className="p-3 cursor-pointer"
                onClick={() => item.onClick()}
              >
                <div className="flex items-center gap-1">
                  <Bin /> {item.label}
                </div>
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  )
}
