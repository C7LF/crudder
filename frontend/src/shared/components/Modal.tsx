import { useEffect } from "react"
import { createPortal } from "react-dom"

import { CloseIcon } from "./icons"

type ModalProps = {
  isOpen: boolean
  onClose: () => void
  children: React.ReactNode
}

export const Modal = ({ isOpen, onClose, children }: ModalProps) => {
  // Prevent scrolling when modal is open
  useEffect(() => {
    if (isOpen) document.body.style.overflow = "hidden"
    else document.body.style.overflow = ""
    return () => {
      document.body.style.overflow = ""
    }
  }, [isOpen])

  if (!isOpen) return null

  return createPortal(
    <div
      className="fixed inset-0 z-50 flex items-center justify-center bg-black/50"
      onClick={() => {
        onClose()
      }}
    >
      <div
        className="bg-white dark:bg-gray-800 dark:text-white rounded-lg shadow-lg p-6 w-10/12 relative"
        onClick={(e) => e.stopPropagation()}
      >
        <CloseIcon
          className="size-5 absolute -right-1 -top-1 bg-amber-700 rounded-full cursor-pointer"
          onClick={() => {
            onClose()
          }}
        />
        {children}
      </div>
    </div>,
    document.body
  )
}
