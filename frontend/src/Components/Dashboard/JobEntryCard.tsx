import type {JobEntry} from "../../types/jobEntry.ts";

export default function JobEntryCard({jobEntry, onDelete}: {
    jobEntry: JobEntry,
    onDelete: (jobEntryId: string) => void
}) {
    return (
        <div>
            <p>{jobEntry.companyName}</p>
            <p>{jobEntry.jobTitle}</p>
            <p>{jobEntry.applicationStatus}</p>
            <button onClick={() => onDelete(jobEntry.jobEntryId)} type={"submit"}>Delete</button>
        </div>
    )
}