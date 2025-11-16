// Time Entry domain interfaces based on EventStore API schema
export enum ActivityTypeApiValue {
  Analysis = "analysis",
  Development = "development",
  Testing = "testing",
  CodeReview = "codeReview",
  Documentation = "documentation",
  Meeting = "meeting",
  BugFix = "bugFix",
  Deployment = "deployment",
  Research = "research",
  Other = "other",
}

export const ActivityTypeLabels: Record<ActivityTypeApiValue, string> = {
  [ActivityTypeApiValue.Analysis]: "🛠️ Support",
  [ActivityTypeApiValue.Development]: "💻 Development",
  [ActivityTypeApiValue.Testing]: "🧪 Testing",
  [ActivityTypeApiValue.CodeReview]: "🔍 Code Review",
  [ActivityTypeApiValue.Documentation]: "📝 Documentation",
  [ActivityTypeApiValue.Meeting]: "📅 Meeting",
  [ActivityTypeApiValue.BugFix]: "🧨 Debugging",
  [ActivityTypeApiValue.Deployment]: "🚀 Deployment",
  [ActivityTypeApiValue.Research]: "🔬 Research",
  [ActivityTypeApiValue.Other]: "📌 Other",
};

export interface ReadTimeEntryUserModel {
  id: string;
  userName: string;
}

export interface ReadTimeEntryProjectModel {
  id: string;
  code: string;
}

export interface ReadTimeEntryModel {
  aggregateRootId: string;
  from: string; // ISO date-time string
  until: string; // ISO date-time string
  user: ReadTimeEntryUserModel;
  project: ReadTimeEntryProjectModel;
  activityType: ActivityTypeApiValue;
  comment: string | null;
  isRemoved: boolean;
  version: number;
}

export interface RegisterTimeEntryRequest {
  from: string; // ISO date-time string
  until: string; // ISO date-time string
  userId: string;
  projectId: string;
  activityType: ActivityTypeApiValue;
  comment?: string;
}

export interface RegisterTimeEntryResponse {
  timeEntryId: string;
}

export interface UpdateTimeEntryRequest {
  from: string; // ISO date-time string
  until: string; // ISO date-time string
  userId: string;
  projectId: string;
  activityType: ActivityTypeApiValue;
  comment?: string;
}

export interface UpdateTimeEntryResponse {
  timeEntryId: string;
}

export interface LoadAllTimeEntriesResponse {
  timeEntries: ReadTimeEntryModel[];
}

export interface LoadSingleTimeEntryResponse {
  timeEntry: ReadTimeEntryModel;
}

// Helper types for UI components
export interface TimeEntrySummary {
  date: string; // YYYY-MM-DD
  totalHours: number;
  entries: ReadTimeEntryModel[];
}

export interface WeekSummary {
  weekNumber: number;
  year: number;
  startDate: string; // YYYY-MM-DD
  endDate: string; // YYYY-MM-DD
  totalHours: number;
  dailySummaries: TimeEntrySummary[];
}

export interface MonthSummary {
  month: number; // 1-12
  year: number;
  totalHours: number;
  dailySummaries: TimeEntrySummary[];
}