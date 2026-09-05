import {useContext, useEffect, useState} from "react";
import {AuthContext} from "../Auth/AuthContext.tsx";
import {fetchJobEntries} from "../../api/jobEntry.ts";
import type {JobEntry} from "../../types/jobEntry.ts";
import JobEntryList from "./JobEntryList.tsx";
import CreateJobEntryForm from "./CreateJobEntryForm.tsx";

export default function Dashboard() {
    const [jobEntries, setJobEntries] = useState<JobEntry[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [hasError, setHasError] = useState(false);
    const {accessToken} = useContext(AuthContext);


    const loadEntries = async () => {
        setIsLoading(true);
        setHasError(false);
        try {
            const data = await fetchJobEntries(accessToken);
            setJobEntries(data);
        } catch (error) {
            setHasError(true);
        } finally {
            setIsLoading(false);
        }
    }

    useEffect(() => {
        loadEntries();
    }, [])

    return (
        <>
            {hasError && <p>Something went wrong...</p>}
            {isLoading && <p>Loading...</p>}
            <p>You're logged in!</p>
            <CreateJobEntryForm onCreated={loadEntries}/>
            <JobEntryList jobEntries={jobEntries}/>
        </>
    )
}