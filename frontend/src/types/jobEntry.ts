type ApplicationStatus = "NotApplied" | "Applied" | "InterviewRequested" | "Interviewing" |
    "Offer" | "Rejected" | "AutoRejected" | "NoResponse"
type JobSource = "CompanyWebsite" | "LinkedIn" | "Indeed" | "Referral" | "Recruiter" |
    "JobBoard" | "Other"

export interface JobEntry {
    jobEntryId: string;
    companyName: string;
    jobTitle: string;
    applicationStatus: ApplicationStatus;
    jobSource: JobSource | null;
    notes: string | null;
    dateApplied: string | null;
    postingUrl: string | null;
    salaryMin: number | null;
    salaryMax: number | null;
    recruiterName: string | null;
    recruiterEmail: string | null;
    interviewDate: string | null;
}

export interface CreateJobEntry {
    companyName: string;
    jobTitle: string;
    applicationStatus?: ApplicationStatus;
    jobSource?: JobSource | null;
    notes?: string | null;
    dateApplied?: string | null;
    postingUrl?: string | null;
    salaryMin?: number | null;
    salaryMax?: number | null;
    recruiterName?: string | null;
    recruiterEmail?: string | null;
    interviewDate?: string | null;
}