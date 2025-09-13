export const TodosHeader = ({ count }: { count?: number }) => (
  <div className="bg-indigo-400">
    <div className="container mx-auto py-38">
      <h1 className="font-extrabold text-4xl text-white">Task List</h1>
      <div className="flex mt-3">
        <div className="text-sm font-semibold text-gray-100 bg-amber-500 px-2 py-1 rounded-sm">
          <span>Count: {count}</span>
        </div>
      </div>
    </div>
  </div>
)
