import {useState} from "react";

export default function useAsyncOperation() {
    const [errorState, setErrorState] = useState<string>('');
    const [isLoading, setIsLoading] = useState<boolean>(false);


    const handleAsyncOp = async <T>(apiCall: () => Promise<T>): Promise<T> => {
        setIsLoading(true);
        setErrorState('');
        try {
            return await apiCall();
        } catch (err) {
            setErrorState(err instanceof Error ? err.message : "Something went wrong");
            throw err;
        } finally {
            setIsLoading(false)
        }

    }

    return {handleAsyncOp, errorState, isLoading};
}