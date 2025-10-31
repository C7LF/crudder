export function getContrastColor(hex: string): string {
  // Normalize hex
  const clean = hex.replace('#', '')
  const r = parseInt(clean.substring(0, 2), 16)
  const g = parseInt(clean.substring(2, 4), 16)
  const b = parseInt(clean.substring(4, 6), 16)

  // Perceived luminance
  const brightness = (r * 299 + g * 587 + b * 114) / 1000

  return brightness > 128 ? '#000' : '#fff'
}
