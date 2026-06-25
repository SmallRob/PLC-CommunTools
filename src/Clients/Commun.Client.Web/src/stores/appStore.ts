import { create } from 'zustand'

interface AppState {
  connections: Map<string, boolean>
  setConnected: (id: string, connected: boolean) => void
  isConnected: (id: string) => boolean
}

export const useAppStore = create<AppState>((set, get) => ({
  connections: new Map(),
  setConnected: (id, connected) => set((state) => {
    const newMap = new Map(state.connections)
    newMap.set(id, connected)
    return { connections: newMap }
  }),
  isConnected: (id) => get().connections.get(id) ?? false,
}))
