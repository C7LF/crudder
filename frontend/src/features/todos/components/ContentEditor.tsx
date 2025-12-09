import { useCallback, useMemo } from "react"
import { createEditor, type Descendant } from "slate"
import { Editable, Slate, withReact } from "slate-react"

interface ContentEditorProps {
  value: string
  onChange: (content: string) => void
}

export const ContentEditor = ({ value, onChange }: ContentEditorProps) => {
  const editor = useMemo(() => withReact(createEditor()), [])

  const initialValue: Descendant[] = useMemo(() => {
    if (!value || value.trim() === "") {
      return [{ type: "paragraph", children: [{ text: "" }] }]
    }
    try {
      return JSON.parse(value)
    } catch {
      return [{ type: "paragraph", children: [{ text: value }] }]
    }
  }, [value])

  const handleChange = useCallback(
    (newValue: Descendant[]) => {
      const isAstChange = editor.operations.some(
        (op) => "set_selection" !== op.type
      )
      if (isAstChange) {
        onChange(JSON.stringify(newValue))
      }
    },
    [editor.operations, onChange]
  )

  return (
    <div className="mt-4 p-3 border dark:border-gray-400 rounded bg-white dark:bg-gray-800">
      <Slate
        editor={editor}
        initialValue={initialValue}
        onChange={handleChange}
      >
        <Editable
          className="min-h-32 outline-none dark:text-gray-100"
          placeholder="content..."
        />
      </Slate>
    </div>
  )
}
