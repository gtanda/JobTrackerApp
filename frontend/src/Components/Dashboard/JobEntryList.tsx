import type {JobEntry} from "../../types/jobEntry.ts";
import JobEntryCard from "./JobEntryCard.tsx";

export default function JobEntryList({jobEntries, onDelete}: {
    jobEntries: JobEntry[],
    onDelete: (jobEntryId: string) => void
}) {

    return (
        <ul>
            {jobEntries.map((jobEntry: JobEntry) => <JobEntryCard key={jobEntry.jobEntryId}
                                                                  jobEntry={jobEntry}
                                                                  onDelete={onDelete}/>)}
        </ul>
    )
}