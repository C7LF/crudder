import { KebabVerticalIcon } from "./icons"

type KebabButtonProps = {
  onClick?: () => void
}

export const KebabButton = ({ onClick }: KebabButtonProps) => {
  return (
    <button
      onClick={onClick}
      className="p-2 rounded-full 
                 hover:bg-gray-100 active:bg-gray-200 
                 dark:hover:bg-gray-700 dark:active:bg-gray-600 
                 transition-colors 
                 flex items-center justify-center 
                 hover:cursor-pointer"
    >
      <KebabVerticalIcon />
    </button>
  )
}
