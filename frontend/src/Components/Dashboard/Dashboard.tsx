import {useEffect, useState} from "react";
import {fetchJobEntries} from "../../api/jobEntry.ts";
import type {JobEntry} from "../../types/jobEntry.ts";
import JobEntryList from "./JobEntryList.tsx";
import CreateJobEntryForm from "./CreateJobEntryForm.tsx";
import useAuthFetch from "../Auth/useAuthFetch.tsx";


export default function Dashboard() {
    const [jobEntries, setJobEntries] = useState<JobEntry[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setErrorState] = useState<string>('');
    const {authFetch} = useAuthFetch();


    const loadEntries = async () => {
        setIsLoading(true);
        setErrorState('');
        try {
            const data = await fetchJobEntries(authFetch);
            setJobEntries(data);
        } catch (error) {
            setErrorState(error instanceof Error ? error.message : "Could not fetch job entries.");
        } finally {
            setIsLoading(false);
        }
    }

    useEffect(() => {
        // eslint-disable-next-line react-hooks/set-state-in-effect
        loadEntries();
    }, [])

    return (
        <>
            {error && <p>{error}</p>}
            {isLoading && <p>Loading...</p>}
            <p>You're logged in!</p>
            <CreateJobEntryForm onCreated={loadEntries}/>
            <JobEntryList jobEntries={jobEntries}/>
        </>
    )
}