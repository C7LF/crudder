export interface Todo {
    id: number,
    title: string,
    completed: boolean
    label?: "primary" | "secondary" | "blue" | "green"
}