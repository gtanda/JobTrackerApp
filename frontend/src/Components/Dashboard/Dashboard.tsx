import {useContext, useEffect, useState} from "react";
import {AuthContext} from "../Auth/AuthContext.tsx";
import {fetchJobEntries} from "../../api/jobEntry.ts";
import type {JobEntry} from "../../types/jobEntry.ts";
import JobEntryList from "./JobEntryList.tsx";
export default function Dashboard() {
    const [jobEntries, setJobEntries] = useState<JobEntry[]>([]);
    const {accessToken} = useContext(AuthContext);
    
    useEffect(() => {
        const getEntries = async () => {
            const data = await fetchJobEntries(accessToken);
            setJobEntries(data);
        }
        getEntries();
    }, [])
    
    return(
        <>
        <p>You're logged in!</p>
        <JobEntryList jobEntries={jobEntries} />
        </>
    )
}