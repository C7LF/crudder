import { useTheme } from "@/app/providers/ThemeProvider"

import { MoonIcon, SunIcon } from "./icons"

export const ThemeToggle = () => {
  const { theme, toggleTheme } = useTheme()

  return (
    <button
      onClick={toggleTheme}
      className="relative inline-flex h-6 w-12 items-center rounded-full bg-gray-300 dark:bg-gray-700 transition-colors duration-300"
    >
      <span
        className={`absolute left-1 flex h-4 w-4 items-center justify-center rounded-full bg-white dark:bg-gray-900 shadow-md transform transition-transform duration-300 ${
          theme === "dark" ? "translate-x-6" : "translate-x-0"
        }`}
      >
        {theme === "dark" ? (
          <MoonIcon />
        ) : (
          <SunIcon />
        )}
      </span>
    </button>
  )
}
