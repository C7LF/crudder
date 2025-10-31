import axios from "axios"

const api = axios.create({
  baseURL: "/api",
  withCredentials: true,
})

let isRefreshing = false
let failedQueue: Array<{
  resolve: (value?: unknown) => void
  reject: (err: unknown) => void
}> = []

const processQueue = (error: unknown = null) => {
  failedQueue.forEach((p) => {
    if (error) p.reject(error)
    else p.resolve()
  })
  failedQueue = []
}

api.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config

    if (error.response?.status === 401 && !originalRequest._retry) {
      if (originalRequest.url?.includes("/auth/refresh")) {
        // refresh failed → logout handled in provider
        return Promise.reject(error)
      }

      if (isRefreshing) {
        // queue requests while refreshing
        return new Promise((resolve, reject) => {
          failedQueue.push({ resolve, reject })
        })
          .then(() => api(originalRequest))
          .catch((err) => Promise.reject(err))
      }

      originalRequest._retry = true
      isRefreshing = true

      try {
        await api.post("/auth/refresh")
        processQueue()
        return api(originalRequest) // retry original request
      } catch (err) {
        processQueue(err)
        return Promise.reject(err)
      } finally {
        isRefreshing = false
      }
    }

    return Promise.reject(error)
  }
)
export default api
