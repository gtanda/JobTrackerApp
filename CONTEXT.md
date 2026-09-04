# Job Tracker

The domain of a job seeker recording and tracking the jobs they have applied to: what was applied for, where it came from, and how far each application has progressed.

## Language

**Job Entry**:
A single job the user is tracking — one record per application, holding the company, role, status, and surrounding notes.
_Avoid_: Application (overloaded with the running ASP.NET application), Job, Posting, Listing.

**User**:
The job seeker who owns Job Entries. Every Job Entry belongs to exactly one User.
_Avoid_: Account, Member, Applicant.

**Application Status**:
Where a Job Entry sits in its lifecycle. The lifecycle begins *before* an application is sent: a Job Entry can sit in `NotApplied` while the user is still deciding, then move through `Applied` and `Interviewing` to a terminal outcome (`Offer`, `Rejected`, `AutoRejected`, `NoResponse`). `NotApplied` is the starting point of the lifecycle.
_Avoid_: State, Stage, Phase.

**Job Source**:
Where the user found the job — e.g. company website, LinkedIn, a referral, or a recruiter.
_Avoid_: Channel, Origin, Medium.
