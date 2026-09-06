import {useEffect, useState} from "react";
import {deleteJobEntry, fetchJobEntries} from "../../api/jobEntry.ts";
import type {JobEntry} from "../../types/jobEntry.ts";
import JobEntryList from "./JobEntryList.tsx";
import CreateJobEntryForm from "./CreateJobEntryForm.tsx";
import useAuthFetch from "../Auth/useAuthFetch.ts";
import useAsyncOperation from "../../hooks/useAsyncOperation.ts";


export default function Dashboard() {
    const [jobEntries, setJobEntries] = useState<JobEntry[]>([]);
    const {authFetch} = useAuthFetch();
    const {handleAsyncOp, errorState, isLoading} = useAsyncOperation()

    const getJobs = async () => await fetchJobEntries(authFetch);


    const loadEntries = async () => {
        try {
            const data = await handleAsyncOp<JobEntry[]>(getJobs);
            setJobEntries(data);
        } catch (error) {
            console.error(error);
        }
    }

    useEffect(() => {
        // eslint-disable-next-line react-hooks/set-state-in-effect
        loadEntries();
    }, [])

    const deleteJob = async (jobEntryId: string) => await deleteJobEntry(jobEntryId, authFetch);

    const handleJobEntryDelete = async (jobEntryId: string) => {
        try {
            await handleAsyncOp(() => deleteJob(jobEntryId))
            loadEntries();
        } catch (err) {
            console.error("Could not delete job entry", err);
        }
    }

    return (
        <>
            {errorState && <p>{errorState}</p>}
            {isLoading && <p>Loading...</p>}
            <p>You're logged in!</p>
            <CreateJobEntryForm onCreated={loadEntries}/>
            <JobEntryList jobEntries={jobEntries} onDelete={handleJobEntryDelete}/>
        </>
    )
}