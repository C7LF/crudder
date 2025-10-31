import type { InputHTMLAttributes } from "react"

export const Input = (props: InputHTMLAttributes<HTMLInputElement>) => (
  <input
    type="text"
    className="block w-full rounded-md 
                     bg-white dark:bg-gray-800 
                     text-gray-900 dark:text-gray-100 
                     px-3 py-1.5 text-base 
                     placeholder:text-gray-500 dark:placeholder:text-gray-400
                     outline-1 -outline-offset-1 outline-white/10 dark:outline-gray-700
                     focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-500 
                     sm:text-sm/6"
    {...props}
  />
)
