import type {JobEntry} from "../../types/jobEntry.ts";
import JobEntryCard from "./JobEntryCard.tsx";

export default function JobEntryList ({jobEntries}: {jobEntries: JobEntry[]}) {
    
    return (
        <ul>
            {jobEntries.map((jobEntry: JobEntry) => <JobEntryCard key={jobEntry.jobEntryId} jobEntry={jobEntry}/>)}
        </ul>
    )
}