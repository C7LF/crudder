import { ThemeToggle } from "./ThemeToggle"

export const TodosHeader = ({ count }: { count?: number }) => (
  <div className="bg-indigo-400 dark:bg-gray-900">
    <div className="container flex justify-end mx-auto pt-6">
      <ThemeToggle />
    </div>
    <div className="container mx-auto py-38">
      <h1 className="font-extrabold text-4xl text-white dark:text-gray-100">
        Task List
      </h1>
      <div className="flex mt-3">
        <div
          className="text-sm font-semibold text-gray-100 bg-amber-500 px-2 py-1 rounded-sm 
                        dark:bg-gray-700 dark:text-gray-200"
        >
          <span>Count: {count}</span>
        </div>
      </div>
    </div>
  </div>
)
