import api from "../api/axios"
import type { Label } from "../types/label"

export const createLabel = async (label: Label) => {
  const res = await api.post("/labels", {
    text: label.text,
    colour: label.colour,
  })
  return res.data
}

export const getLabels = async () => {
  const res = await api.get<Label[]>("/labels")
  return res.data
}
