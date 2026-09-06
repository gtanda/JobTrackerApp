import {createContext} from 'react'

interface AuthContextType {
    accessToken: string,
    updateAndSetAccessToken: (accessToken: string) => void,
    getAccessToken: () => string,
}

export const AuthContext = createContext<AuthContextType>({
    accessToken: '',
    updateAndSetAccessToken: () => {
    },
    getAccessToken: () => ''
});